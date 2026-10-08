namespace BinaryImageConverter 
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.TextBox txtEncodeFile;
        private System.Windows.Forms.TextBox txtDecodeFile;
        private System.Windows.Forms.RadioButton rb16;
        private System.Windows.Forms.RadioButton rb256;
        private System.Windows.Forms.RadioButton rb1677;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            grpEncode = new GroupBox();
            rb16 = new RadioButton();
            rb256 = new RadioButton();
            rb1677 = new RadioButton();
            lblW = new Label();
            txtWidth = new TextBox();
            lblH = new Label();
            txtHeight = new TextBox();
            lblEncFile = new Label();
            txtEncodeFile = new TextBox();
            btnBrowseEncode = new Button();
            btnEncode = new Button();
            grpDecode = new GroupBox();
            lblDecFile = new Label();
            txtDecodeFile = new TextBox();
            btnBrowseDecode = new Button();
            btnDecode = new Button();
            progressBar = new ProgressBar();
            lblStatus = new Label();
            toolTip1 = new ToolTip(components);
            groupBox1 = new GroupBox();
            grpEncode.SuspendLayout();
            grpDecode.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // grpEncode
            // 
            grpEncode.Controls.Add(rb16);
            grpEncode.Controls.Add(rb256);
            grpEncode.Controls.Add(rb1677);
            grpEncode.Controls.Add(lblW);
            grpEncode.Controls.Add(txtWidth);
            grpEncode.Controls.Add(lblH);
            grpEncode.Controls.Add(txtHeight);
            grpEncode.Controls.Add(lblEncFile);
            grpEncode.Controls.Add(txtEncodeFile);
            grpEncode.Controls.Add(btnBrowseEncode);
            grpEncode.Controls.Add(btnEncode);
            grpEncode.Location = new Point(20, 15);
            grpEncode.Name = "grpEncode";
            grpEncode.Size = new Size(490, 195);
            grpEncode.TabIndex = 0;
            grpEncode.TabStop = false;
            grpEncode.Text = "【エンコード設定】";
            // 
            // rb16
            // 
            rb16.Location = new Point(15, 25);
            rb16.Name = "rb16";
            rb16.Size = new Size(100, 24);
            rb16.TabIndex = 0;
            rb16.Text = "16色 (4bit)";
            // 
            // rb256
            // 
            rb256.Location = new Point(125, 25);
            rb256.Name = "rb256";
            rb256.Size = new Size(100, 24);
            rb256.TabIndex = 1;
            rb256.Text = "256色 (8bit)";
            // 
            // rb1677
            // 
            rb1677.Checked = true;
            rb1677.Location = new Point(235, 25);
            rb1677.Name = "rb1677";
            rb1677.Size = new Size(130, 24);
            rb1677.TabIndex = 2;
            rb1677.TabStop = true;
            rb1677.Text = "1677万色 (24bit)";
            // 
            // lblW
            // 
            lblW.Location = new Point(15, 60);
            lblW.Name = "lblW";
            lblW.Size = new Size(30, 23);
            lblW.TabIndex = 3;
            lblW.Text = "幅:";
            // 
            // txtWidth
            // 
            txtWidth.Location = new Point(45, 57);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(70, 27);
            txtWidth.TabIndex = 4;
            txtWidth.Text = "640";
            // 
            // lblH
            // 
            lblH.Location = new Point(135, 60);
            lblH.Name = "lblH";
            lblH.Size = new Size(35, 23);
            lblH.TabIndex = 5;
            lblH.Text = "高さ:";
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(175, 57);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(70, 27);
            txtHeight.TabIndex = 6;
            txtHeight.Text = "360";
            // 
            // lblEncFile
            // 
            lblEncFile.Location = new Point(15, 95);
            lblEncFile.Name = "lblEncFile";
            lblEncFile.Size = new Size(70, 23);
            lblEncFile.TabIndex = 7;
            lblEncFile.Text = "元ファイル:";
            // 
            // txtEncodeFile
            // 
            txtEncodeFile.Location = new Point(15, 115);
            txtEncodeFile.Name = "txtEncodeFile";
            txtEncodeFile.ReadOnly = true;
            txtEncodeFile.Size = new Size(380, 27);
            txtEncodeFile.TabIndex = 8;
            // 
            // btnBrowseEncode
            // 
            btnBrowseEncode.Location = new Point(405, 113);
            btnBrowseEncode.Name = "btnBrowseEncode";
            btnBrowseEncode.Size = new Size(70, 25);
            btnBrowseEncode.TabIndex = 9;
            btnBrowseEncode.Text = "参照...";
            toolTip1.SetToolTip(btnBrowseEncode, "「参照（さんしょう）」とは「資料・文献・データなどを照らし合わせて見たり、必要な情報を確認したりすること」を意味する言葉です。");
            btnBrowseEncode.Click += BtnBrowseEncode_Click;
            // 
            // btnEncode
            // 
            btnEncode.Location = new Point(15, 150);
            btnEncode.Name = "btnEncode";
            btnEncode.Size = new Size(460, 35);
            btnEncode.TabIndex = 10;
            btnEncode.Text = "エンコード実行 (ZIP生成)";
            toolTip1.SetToolTip(btnEncode, "元ファイルのビットを各色に当てはめてバイナリ画像を生成します");
            btnEncode.Click += BtnEncode_Click;
            // 
            // grpDecode
            // 
            grpDecode.Controls.Add(lblDecFile);
            grpDecode.Controls.Add(txtDecodeFile);
            grpDecode.Controls.Add(btnBrowseDecode);
            grpDecode.Controls.Add(btnDecode);
            grpDecode.Location = new Point(20, 220);
            grpDecode.Name = "grpDecode";
            grpDecode.Size = new Size(490, 115);
            grpDecode.TabIndex = 1;
            grpDecode.TabStop = false;
            grpDecode.Text = "【デコード設定】";
            // 
            // lblDecFile
            // 
            lblDecFile.Location = new Point(15, 25);
            lblDecFile.Name = "lblDecFile";
            lblDecFile.Size = new Size(75, 23);
            lblDecFile.TabIndex = 0;
            lblDecFile.Text = "ZIPファイル:";
            // 
            // txtDecodeFile
            // 
            txtDecodeFile.Location = new Point(15, 45);
            txtDecodeFile.Name = "txtDecodeFile";
            txtDecodeFile.ReadOnly = true;
            txtDecodeFile.Size = new Size(380, 27);
            txtDecodeFile.TabIndex = 1;
            // 
            // btnBrowseDecode
            // 
            btnBrowseDecode.Location = new Point(405, 43);
            btnBrowseDecode.Name = "btnBrowseDecode";
            btnBrowseDecode.Size = new Size(70, 25);
            btnBrowseDecode.TabIndex = 2;
            btnBrowseDecode.Text = "参照...";
            toolTip1.SetToolTip(btnBrowseDecode, "「参照（さんしょう）」とは「資料・文献・データなどを照らし合わせて見たり、必要な情報を確認したりすること」を意味する言葉です。");
            btnBrowseDecode.Click += BtnBrowseDecode_Click;
            // 
            // btnDecode
            // 
            btnDecode.Location = new Point(15, 75);
            btnDecode.Name = "btnDecode";
            btnDecode.Size = new Size(460, 32);
            btnDecode.TabIndex = 3;
            btnDecode.Text = "デコード実行 (ファイル復元)";
            toolTip1.SetToolTip(btnDecode, "バイナリ画像をファイルに復元します");
            btnDecode.Click += BtnDecode_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(6, 26);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(473, 15);
            progressBar.TabIndex = 2;
            toolTip1.SetToolTip(progressBar, "焦らず待て");
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(20, 447);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(490, 25);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "準備完了";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(progressBar);
            groupBox1.Location = new Point(20, 369);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(490, 51);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "進行状況";
            // 
            // Form1
            // 
            ClientSize = new Size(524, 523);
            Controls.Add(groupBox1);
            Controls.Add(grpEncode);
            Controls.Add(grpDecode);
            Controls.Add(lblStatus);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "マルチカラー・バイナリ画像変換ツール (エンコード/デコード分離)";
            grpEncode.ResumeLayout(false);
            grpEncode.PerformLayout();
            grpDecode.ResumeLayout(false);
            grpDecode.PerformLayout();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private GroupBox grpEncode;
        private Label lblW;
        private Label lblH;
        private Label lblEncFile;
        private Button btnBrowseEncode;
        private GroupBox grpDecode;
        private Label lblDecFile;
        private Button btnBrowseDecode;
        private ToolTip toolTip1;
        private GroupBox groupBox1;
    }
}