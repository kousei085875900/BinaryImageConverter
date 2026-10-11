using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BinaryImageConverter
{
    public partial class Form1 : Form
    {
        private readonly Color[] COLOR_PALETTE_16 = new Color[]
        {
            Color.FromArgb(0, 0, 0),       Color.FromArgb(0, 0, 170),     Color.FromArgb(0, 170, 0),     Color.FromArgb(0, 170, 170),
            Color.FromArgb(170, 0, 0),     Color.FromArgb(170, 0, 170),   Color.FromArgb(170, 85, 0),    Color.FromArgb(170, 170, 170),
            Color.FromArgb(85, 85, 85),    Color.FromArgb(85, 85, 255),   Color.FromArgb(85, 255, 85),   Color.FromArgb(85, 255, 255),
            Color.FromArgb(255, 85, 85),   Color.FromArgb(255, 85, 255),  Color.FromArgb(255, 255, 85),  Color.FromArgb(255, 255, 255)
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnBrowseEncode_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtEncodeFile.Text = ofd.FileName;
                }
            }
        }

        private void BtnBrowseDecode_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "PNG画像 (*.png)|*.png|すべてのファイル (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtDecodeFile.Text = string.Join(";", ofd.FileNames);
                }
            }
        }

        // --- エンコード処理 ---
        private void BtnEncode_Click(object sender, EventArgs e)
        {
            string filePath = txtEncodeFile.Text;
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("エンコードする元ファイルを正しく選択してください。");
                return;
            }

            int width = int.Parse(txtWidth.Text);
            int height = int.Parse(txtHeight.Text);

            string colorMode = "1677";
            if (rb16.Checked) colorMode = "16";
            else if (rb256.Checked) colorMode = "256";

            string baseDir = Path.GetDirectoryName(filePath);
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "生成する連番画像の保存先フォルダを選択してください";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    baseDir = fbd.SelectedPath;
                }
            }

            try
            {
                lblStatus.Text = "エンコード中...";
                progressBar.Value = 0;

                byte[] fileBytes = File.ReadAllBytes(filePath);
                string fileName = Path.GetFileName(filePath);
                byte[] nameBytes = System.Text.Encoding.UTF8.GetBytes(fileName);

                byte modeCode = 24;
                if (colorMode == "16") modeCode = 16;
                else if (colorMode == "256") modeCode = 25;
                else if (colorMode == "1677") modeCode = 24;

                int headerSize = 2 + 1 + 4 + nameBytes.Length + 8;
                byte[] fullData = new byte[headerSize + fileBytes.Length];

                fullData[0] = 0x42; // 'B'
                fullData[1] = 0x49; // 'I'
                fullData[2] = modeCode;

                byte[] nameLenBytes = BitConverter.GetBytes(nameBytes.Length);
                if (BitConverter.IsLittleEndian) Array.Reverse(nameLenBytes);
                Array.Copy(nameLenBytes, 0, fullData, 3, 4);

                Array.Copy(nameBytes, 0, fullData, 7, nameBytes.Length);

                byte[] fileSizeBytes = BitConverter.GetBytes((double)fileBytes.Length);
                if (BitConverter.IsLittleEndian) Array.Reverse(fileSizeBytes);
                Array.Copy(fileSizeBytes, 0, fullData, 7 + nameBytes.Length, 8);

                Array.Copy(fileBytes, 0, fullData, headerSize, fileBytes.Length);

                int pixelsPerFrame = width * height;
                int bytesPerFrame = pixelsPerFrame * 3;
                int totalFrames = (int)Math.Ceiling((double)fullData.Length / bytesPerFrame);

                string outFolderName = Path.Combine(baseDir, Path.GetFileNameWithoutExtension(filePath) + "_" + colorMode + "col_frames");
                Directory.CreateDirectory(outFolderName);

                for (int f = 0; f < totalFrames; f++)
                {
                    int startIdx = f * bytesPerFrame;
                    int length = Math.Min(bytesPerFrame, fullData.Length - startIdx);
                    byte[] frameChunk = new byte[length];
                    Array.Copy(fullData, startIdx, frameChunk, 0, length);

                    using (Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb))
                    {
                        BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bmp.PixelFormat);
                        int stride = bmpData.Stride;
                        IntPtr ptr = bmpData.Scan0;

                        int bytesCount = Math.Abs(stride) * height;
                        byte[] pixelValues = new byte[bytesCount];

                        int pIdx = 0;
                        for (int i = 0; i < frameChunk.Length; i += 3)
                        {
                            if (pIdx >= pixelsPerFrame) continue;
                            byte r = frameChunk[i];
                            byte g = i + 1 < frameChunk.Length ? frameChunk[i + 1] : (byte)0;
                            byte b = i + 2 < frameChunk.Length ? frameChunk[i + 2] : (byte)0;

                            int x = pIdx % width;
                            int y = pIdx / width;
                            int index = y * stride + x * 4;

                            pixelValues[index] = b;
                            pixelValues[index + 1] = g;
                            pixelValues[index + 2] = r;
                            pixelValues[index + 3] = 255;
                            pIdx++;
                        }

                        for (int p = pIdx; p < pixelsPerFrame; p++)
                        {
                            int x = p % width;
                            int y = p / width;
                            int index = y * stride + x * 4;
                            pixelValues[index] = 0;
                            pixelValues[index + 1] = 0;
                            pixelValues[index + 2] = 0;
                            pixelValues[index + 3] = 255;
                        }

                        Marshal.Copy(pixelValues, 0, ptr, bytesCount);
                        bmp.UnlockBits(bmpData);

                        string outPngPath = Path.Combine(outFolderName, $"frame_{f + 1:D4}.png");
                        bmp.Save(outPngPath, ImageFormat.Png);
                    }

                    progressBar.Value = (int)((f + 1) / (double)totalFrames * 100);
                }

                lblStatus.Text = "エンコード完了！";
                MessageBox.Show("連番PNGの生成が完了しました:\n" + outFolderName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー: " + ex.Message);
                lblStatus.Text = "エラー発生";
            }
        }

        // --- デコード処理 (完全安定版) ---
        private void BtnDecode_Click(object sender, EventArgs e)
        {
            string pathsText = txtDecodeFile.Text;
            if (string.IsNullOrEmpty(pathsText))
            {
                MessageBox.Show("デコードするPNG画像ファイルを複数選択してください。");
                return;
            }

            string[] filePaths = pathsText.Split(';');
            Array.Sort(filePaths);

            string outputDirectory = Path.GetDirectoryName(filePaths[0]);
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "復元したファイルの保存先フォルダを選択してください";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    outputDirectory = fbd.SelectedPath;
                }
            }

            try
            {
                lblStatus.Text = "デコード中...";
                progressBar.Value = 0;

                using (MemoryStream fullDataStream = new MemoryStream())
                {
                    int count = filePaths.Length;
                    for (int i = 0; i < count; i++)
                    {
                        string path = filePaths[i];
                        if (!File.Exists(path)) continue;

                        using (Bitmap bmp = new Bitmap(path))
                        {
                            using (Bitmap bmp32 = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb))
                            {
                                using (Graphics g = Graphics.FromImage(bmp32))
                                {
                                    g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
                                }

                                BitmapData bmpData = bmp32.LockBits(new Rectangle(0, 0, bmp32.Width, bmp32.Height), ImageLockMode.ReadOnly, bmp32.PixelFormat);
                                int stride = bmpData.Stride;
                                IntPtr ptr = bmpData.Scan0;

                                int bytesCount = Math.Abs(stride) * bmp32.Height;
                                byte[] pixelValues = new byte[bytesCount];
                                Marshal.Copy(ptr, pixelValues, 0, bytesCount);
                                bmp32.UnlockBits(bmpData);

                                int width = bmp32.Width;
                                int height = bmp32.Height;

                                for (int y = 0; y < height; y++)
                                {
                                    for (int x = 0; x < width; x++)
                                    {
                                        int index = y * stride + x * 4;
                                        byte b = pixelValues[index];
                                        byte g = pixelValues[index + 1];
                                        byte r = pixelValues[index + 2];

                                        fullDataStream.WriteByte(r);
                                        fullDataStream.WriteByte(g);
                                        fullDataStream.WriteByte(b);
                                    }
                                }
                            }
                        }
                        progressBar.Value = (int)((i + 1) / (double)count * 100);
                    }

                    byte[] allBytes = fullDataStream.ToArray();

                    // ヘッダー解析 ('BI' = 0x42, 0x49)
                    int headerIndex = 0; // 先頭にあるはずなので0固定でOK
                    if (allBytes.Length > 2 && allBytes[0] == 0x42 && allBytes[1] == 0x49)
                    {
                        // modeCode = allBytes[2] (24)

                        // ファイル名長の取得 (オフセット3から4バイト、Big-Endian)
                        byte[] lenBytes = new byte[4];
                        Array.Copy(allBytes, 3, lenBytes, 0, 4);
                        if (BitConverter.IsLittleEndian) Array.Reverse(lenBytes);
                        int nameLen = BitConverter.ToInt32(lenBytes, 0);

                        int nameStartIndex = 7;
                        if (nameLen > 0 && nameLen < 256 && allBytes.Length >= nameStartIndex + nameLen + 8)
                        {
                            string originalFileName = System.Text.Encoding.UTF8.GetString(allBytes, nameStartIndex, nameLen);

                            int sizeIndex = nameStartIndex + nameLen;
                            byte[] sizeBytes = new byte[8];
                            Array.Copy(allBytes, sizeIndex, sizeBytes, 0, 8);
                            if (BitConverter.IsLittleEndian) Array.Reverse(sizeBytes);
                            long originalSize = (long)BitConverter.ToDouble(sizeBytes, 0);

                            int headerSize = sizeIndex + 8;
                            if (originalSize > 0 && allBytes.Length >= headerSize + originalSize)
                            {
                                byte[] originalData = new byte[(int)originalSize];
                                Array.Copy(allBytes, headerSize, originalData, 0, (int)originalSize);

                                string outPath = Path.Combine(outputDirectory, "recovered_" + originalFileName);
                                File.WriteAllBytes(outPath, originalData);

                                lblStatus.Text = "デコード完了！";
                                MessageBox.Show("ファイルを正常に復元しました:\n" + outPath);
                                return;
                            }
                        }
                    }

                    MessageBox.Show("有効なバイナリ画像データが見つかりませんでした。（ヘッダーの解析に失敗しました）");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("デコードエラー: " + ex.Message);
                lblStatus.Text = "エラー発生";
            }
        }
    }
}