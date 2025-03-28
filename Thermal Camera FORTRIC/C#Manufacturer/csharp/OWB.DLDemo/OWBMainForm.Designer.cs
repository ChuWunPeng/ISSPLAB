namespace OWB.DLDemo
{
    partial class OWBMainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OWBMainForm));
            this.pConnect = new System.Windows.Forms.Panel();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.gbConnMode = new System.Windows.Forms.GroupBox();
            this.rbH264 = new System.Windows.Forms.RadioButton();
            this.rbRaw = new System.Windows.Forms.RadioButton();
            this.gbConnInfo = new System.Windows.Forms.GroupBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.pbCamera = new System.Windows.Forms.PictureBox();
            this.pConfig = new System.Windows.Forms.Panel();
            this.pFile = new System.Windows.Forms.Panel();
            this.gbFile = new System.Windows.Forms.GroupBox();
            this.lblFileTempValue = new System.Windows.Forms.Label();
            this.lblFileLocValue = new System.Windows.Forms.Label();
            this.lblFileTemp = new System.Windows.Forms.Label();
            this.lblFileLoc = new System.Windows.Forms.Label();
            this.lblFilePalette = new System.Windows.Forms.Label();
            this.cbFilePalette = new System.Windows.Forms.ComboBox();
            this.pCommon = new System.Windows.Forms.Panel();
            this.btnNearFocus = new System.Windows.Forms.Button();
            this.btnFarFocus = new System.Windows.Forms.Button();
            this.gbTempAcq = new System.Windows.Forms.GroupBox();
            this.lblTempValue = new System.Windows.Forms.Label();
            this.lblLocValue = new System.Windows.Forms.Label();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblLoc = new System.Windows.Forms.Label();
            this.btnH264Snapshot = new System.Windows.Forms.Button();
            this.btnAutoFocus = new System.Windows.Forms.Button();
            this.btnRawSnapshot = new System.Windows.Forms.Button();
            this.btntSnapshot = new System.Windows.Forms.Button();
            this.gbAdjust = new System.Windows.Forms.GroupBox();
            this.nudSnapInterval = new System.Windows.Forms.NumericUpDown();
            this.lblCapture = new System.Windows.Forms.Label();
            this.lblSnapInterval = new System.Windows.Forms.Label();
            this.lblUserSnapIntervalUnit = new System.Windows.Forms.Label();
            this.btnSnap = new System.Windows.Forms.Button();
            this.cbCapture = new System.Windows.Forms.ComboBox();
            this.cbTemperatureRange = new System.Windows.Forms.ComboBox();
            this.lblLens = new System.Windows.Forms.Label();
            this.lblTemperatureRange = new System.Windows.Forms.Label();
            this.cbLens = new System.Windows.Forms.ComboBox();
            this.ckShowMaxMin = new System.Windows.Forms.CheckBox();
            this.cbEmissivity = new System.Windows.Forms.CheckBox();
            this.cbUnit = new System.Windows.Forms.CheckBox();
            this.ckShowTime = new System.Windows.Forms.CheckBox();
            this.ckShowTitle = new System.Windows.Forms.CheckBox();
            this.ckShowPalette = new System.Windows.Forms.CheckBox();
            this.lblShow = new System.Windows.Forms.Label();
            this.lblPalette = new System.Windows.Forms.Label();
            this.cbPalette = new System.Windows.Forms.ComboBox();
            this.pH264 = new System.Windows.Forms.Panel();
            this.gbStream = new System.Windows.Forms.GroupBox();
            this.rbPri = new System.Windows.Forms.RadioButton();
            this.rbSub = new System.Windows.Forms.RadioButton();
            this.ssMainForm = new System.Windows.Forms.StatusStrip();
            this.tsslDeviceStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.msMainForm = new System.Windows.Forms.MenuStrip();
            this.tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserManager = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.pMainForm = new System.Windows.Forms.Panel();
            this.pCamera = new System.Windows.Forms.Panel();
            this.tMainForm = new System.Windows.Forms.Timer(this.components);
            this.ttMainForm = new System.Windows.Forms.ToolTip(this.components);
            this.pConnect.SuspendLayout();
            this.gbConnMode.SuspendLayout();
            this.gbConnInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).BeginInit();
            this.pConfig.SuspendLayout();
            this.pFile.SuspendLayout();
            this.gbFile.SuspendLayout();
            this.pCommon.SuspendLayout();
            this.gbTempAcq.SuspendLayout();
            this.gbAdjust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSnapInterval)).BeginInit();
            this.pH264.SuspendLayout();
            this.gbStream.SuspendLayout();
            this.ssMainForm.SuspendLayout();
            this.msMainForm.SuspendLayout();
            this.pMainForm.SuspendLayout();
            this.pCamera.SuspendLayout();
            this.SuspendLayout();
            // 
            // pConnect
            // 
            this.pConnect.Controls.Add(this.btnRecord);
            this.pConnect.Controls.Add(this.btnPlay);
            this.pConnect.Controls.Add(this.btnConnect);
            this.pConnect.Controls.Add(this.gbConnMode);
            this.pConnect.Controls.Add(this.gbConnInfo);
            this.pConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.pConnect.Location = new System.Drawing.Point(0, 0);
            this.pConnect.Margin = new System.Windows.Forms.Padding(4);
            this.pConnect.Name = "pConnect";
            this.pConnect.Size = new System.Drawing.Size(1137, 54);
            this.pConnect.TabIndex = 57;
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnRecord.Location = new System.Drawing.Point(1030, 15);
            this.btnRecord.Margin = new System.Windows.Forms.Padding(4);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(75, 23);
            this.btnRecord.TabIndex = 54;
            this.btnRecord.Text = "录制";
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnPlay.Location = new System.Drawing.Point(936, 15);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(4);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 54;
            this.btnPlay.Text = "播放";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnConnect.Location = new System.Drawing.Point(619, 15);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 54;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // gbConnMode
            // 
            this.gbConnMode.Controls.Add(this.rbH264);
            this.gbConnMode.Controls.Add(this.rbRaw);
            this.gbConnMode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbConnMode.Location = new System.Drawing.Point(723, 1);
            this.gbConnMode.Margin = new System.Windows.Forms.Padding(0);
            this.gbConnMode.Name = "gbConnMode";
            this.gbConnMode.Padding = new System.Windows.Forms.Padding(0);
            this.gbConnMode.Size = new System.Drawing.Size(209, 46);
            this.gbConnMode.TabIndex = 52;
            this.gbConnMode.TabStop = false;
            // 
            // rbH264
            // 
            this.rbH264.AutoSize = true;
            this.rbH264.Location = new System.Drawing.Point(13, 18);
            this.rbH264.Name = "rbH264";
            this.rbH264.Size = new System.Drawing.Size(59, 16);
            this.rbH264.TabIndex = 36;
            this.rbH264.TabStop = true;
            this.rbH264.Text = "H264流";
            this.rbH264.UseVisualStyleBackColor = true;
            this.rbH264.CheckedChanged += new System.EventHandler(this.rbH264_CheckedChanged);
            // 
            // rbRaw
            // 
            this.rbRaw.AutoSize = true;
            this.rbRaw.Location = new System.Drawing.Point(112, 18);
            this.rbRaw.Name = "rbRaw";
            this.rbRaw.Size = new System.Drawing.Size(71, 16);
            this.rbRaw.TabIndex = 36;
            this.rbRaw.TabStop = true;
            this.rbRaw.Text = "全辐射流";
            this.rbRaw.UseVisualStyleBackColor = true;
            this.rbRaw.CheckedChanged += new System.EventHandler(this.rbRaw_CheckedChanged);
            // 
            // gbConnInfo
            // 
            this.gbConnInfo.Controls.Add(this.tbPassword);
            this.gbConnInfo.Controls.Add(this.lblIPAddress);
            this.gbConnInfo.Controls.Add(this.tbIPAddress);
            this.gbConnInfo.Controls.Add(this.lblUserName);
            this.gbConnInfo.Controls.Add(this.tbUserName);
            this.gbConnInfo.Controls.Add(this.lblPassword);
            this.gbConnInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbConnInfo.Location = new System.Drawing.Point(7, 1);
            this.gbConnInfo.Margin = new System.Windows.Forms.Padding(0);
            this.gbConnInfo.Name = "gbConnInfo";
            this.gbConnInfo.Padding = new System.Windows.Forms.Padding(0);
            this.gbConnInfo.Size = new System.Drawing.Size(608, 46);
            this.gbConnInfo.TabIndex = 52;
            this.gbConnInfo.TabStop = false;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(444, 16);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(4);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(152, 21);
            this.tbPassword.TabIndex = 34;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(10, 20);
            this.lblIPAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(29, 12);
            this.lblIPAddress.TabIndex = 30;
            this.lblIPAddress.Text = "地址";
            // 
            // tbIPAddress
            // 
            this.tbIPAddress.Location = new System.Drawing.Point(55, 16);
            this.tbIPAddress.Margin = new System.Windows.Forms.Padding(4);
            this.tbIPAddress.Name = "tbIPAddress";
            this.tbIPAddress.Size = new System.Drawing.Size(131, 21);
            this.tbIPAddress.TabIndex = 31;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(203, 20);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(41, 12);
            this.lblUserName.TabIndex = 32;
            this.lblUserName.Text = "用户名";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(259, 16);
            this.tbUserName.Margin = new System.Windows.Forms.Padding(4);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(117, 21);
            this.tbUserName.TabIndex = 33;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(394, 20);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(29, 12);
            this.lblPassword.TabIndex = 35;
            this.lblPassword.Text = "密码";
            // 
            // pbCamera
            // 
            this.pbCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCamera.Location = new System.Drawing.Point(345, 0);
            this.pbCamera.Margin = new System.Windows.Forms.Padding(4);
            this.pbCamera.Name = "pbCamera";
            this.pbCamera.Padding = new System.Windows.Forms.Padding(4);
            this.pbCamera.Size = new System.Drawing.Size(792, 595);
            this.pbCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCamera.TabIndex = 58;
            this.pbCamera.TabStop = false;
            this.pbCamera.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCamera_Paint);
            this.pbCamera.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbCamera_MouseDoubleClick);
            this.pbCamera.MouseLeave += new System.EventHandler(this.pbCamera_MouseLeave);
            this.pbCamera.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCamera_MouseMove);
            // 
            // pConfig
            // 
            this.pConfig.Controls.Add(this.pFile);
            this.pConfig.Controls.Add(this.pCommon);
            this.pConfig.Controls.Add(this.pH264);
            this.pConfig.Dock = System.Windows.Forms.DockStyle.Left;
            this.pConfig.Location = new System.Drawing.Point(0, 0);
            this.pConfig.Name = "pConfig";
            this.pConfig.Size = new System.Drawing.Size(345, 597);
            this.pConfig.TabIndex = 59;
            // 
            // pFile
            // 
            this.pFile.Controls.Add(this.gbFile);
            this.pFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFile.Location = new System.Drawing.Point(0, 476);
            this.pFile.Name = "pFile";
            this.pFile.Size = new System.Drawing.Size(345, 118);
            this.pFile.TabIndex = 2;
            // 
            // gbFile
            // 
            this.gbFile.Controls.Add(this.lblFileTempValue);
            this.gbFile.Controls.Add(this.lblFileLocValue);
            this.gbFile.Controls.Add(this.lblFileTemp);
            this.gbFile.Controls.Add(this.lblFileLoc);
            this.gbFile.Controls.Add(this.lblFilePalette);
            this.gbFile.Controls.Add(this.cbFilePalette);
            this.gbFile.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbFile.Location = new System.Drawing.Point(6, 7);
            this.gbFile.Margin = new System.Windows.Forms.Padding(4);
            this.gbFile.Name = "gbFile";
            this.gbFile.Padding = new System.Windows.Forms.Padding(4);
            this.gbFile.Size = new System.Drawing.Size(335, 106);
            this.gbFile.TabIndex = 52;
            this.gbFile.TabStop = false;
            this.gbFile.Text = "快照分析";
            // 
            // lblFileTempValue
            // 
            this.lblFileTempValue.AutoSize = true;
            this.lblFileTempValue.Location = new System.Drawing.Point(146, 81);
            this.lblFileTempValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileTempValue.Name = "lblFileTempValue";
            this.lblFileTempValue.Size = new System.Drawing.Size(41, 12);
            this.lblFileTempValue.TabIndex = 43;
            this.lblFileTempValue.Text = "0.0 ℃";
            // 
            // lblFileLocValue
            // 
            this.lblFileLocValue.AutoSize = true;
            this.lblFileLocValue.Location = new System.Drawing.Point(146, 57);
            this.lblFileLocValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileLocValue.Name = "lblFileLocValue";
            this.lblFileLocValue.Size = new System.Drawing.Size(35, 12);
            this.lblFileLocValue.TabIndex = 44;
            this.lblFileLocValue.Text = "(0,0)";
            // 
            // lblFileTemp
            // 
            this.lblFileTemp.AutoSize = true;
            this.lblFileTemp.Location = new System.Drawing.Point(44, 81);
            this.lblFileTemp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileTemp.Name = "lblFileTemp";
            this.lblFileTemp.Size = new System.Drawing.Size(41, 12);
            this.lblFileTemp.TabIndex = 41;
            this.lblFileTemp.Text = "温度值";
            // 
            // lblFileLoc
            // 
            this.lblFileLoc.AutoSize = true;
            this.lblFileLoc.Location = new System.Drawing.Point(44, 57);
            this.lblFileLoc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileLoc.Name = "lblFileLoc";
            this.lblFileLoc.Size = new System.Drawing.Size(29, 12);
            this.lblFileLoc.TabIndex = 42;
            this.lblFileLoc.Text = "位置";
            // 
            // lblFilePalette
            // 
            this.lblFilePalette.AutoSize = true;
            this.lblFilePalette.Location = new System.Drawing.Point(44, 33);
            this.lblFilePalette.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilePalette.Name = "lblFilePalette";
            this.lblFilePalette.Size = new System.Drawing.Size(41, 12);
            this.lblFilePalette.TabIndex = 40;
            this.lblFilePalette.Text = "调色板";
            // 
            // cbFilePalette
            // 
            this.cbFilePalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilePalette.FormattingEnabled = true;
            this.cbFilePalette.Location = new System.Drawing.Point(146, 29);
            this.cbFilePalette.Margin = new System.Windows.Forms.Padding(4);
            this.cbFilePalette.Name = "cbFilePalette";
            this.cbFilePalette.Size = new System.Drawing.Size(150, 20);
            this.cbFilePalette.TabIndex = 39;
            this.cbFilePalette.SelectedIndexChanged += new System.EventHandler(this.cbFilePalette_SelectedIndexChanged);
            // 
            // pCommon
            // 
            this.pCommon.Controls.Add(this.btnNearFocus);
            this.pCommon.Controls.Add(this.btnFarFocus);
            this.pCommon.Controls.Add(this.gbTempAcq);
            this.pCommon.Controls.Add(this.btnH264Snapshot);
            this.pCommon.Controls.Add(this.btnAutoFocus);
            this.pCommon.Controls.Add(this.btnRawSnapshot);
            this.pCommon.Controls.Add(this.btntSnapshot);
            this.pCommon.Controls.Add(this.gbAdjust);
            this.pCommon.Dock = System.Windows.Forms.DockStyle.Top;
            this.pCommon.Location = new System.Drawing.Point(0, 52);
            this.pCommon.Name = "pCommon";
            this.pCommon.Size = new System.Drawing.Size(345, 424);
            this.pCommon.TabIndex = 1;
            // 
            // btnNearFocus
            // 
            this.btnNearFocus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnNearFocus.Location = new System.Drawing.Point(83, 367);
            this.btnNearFocus.Margin = new System.Windows.Forms.Padding(4);
            this.btnNearFocus.Name = "btnNearFocus";
            this.btnNearFocus.Size = new System.Drawing.Size(23, 23);
            this.btnNearFocus.TabIndex = 61;
            this.btnNearFocus.Text = "-";
            this.btnNearFocus.UseVisualStyleBackColor = false;
            this.btnNearFocus.Click += new System.EventHandler(this.btnNearFocus_Click);
            // 
            // btnFarFocus
            // 
            this.btnFarFocus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnFarFocus.Location = new System.Drawing.Point(204, 367);
            this.btnFarFocus.Margin = new System.Windows.Forms.Padding(4);
            this.btnFarFocus.Name = "btnFarFocus";
            this.btnFarFocus.Size = new System.Drawing.Size(23, 23);
            this.btnFarFocus.TabIndex = 62;
            this.btnFarFocus.Text = "+";
            this.btnFarFocus.UseVisualStyleBackColor = false;
            this.btnFarFocus.Click += new System.EventHandler(this.btnFarFocus_Click);
            // 
            // gbTempAcq
            // 
            this.gbTempAcq.Controls.Add(this.lblTempValue);
            this.gbTempAcq.Controls.Add(this.lblLocValue);
            this.gbTempAcq.Controls.Add(this.lblTemp);
            this.gbTempAcq.Controls.Add(this.lblLoc);
            this.gbTempAcq.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbTempAcq.Location = new System.Drawing.Point(6, 280);
            this.gbTempAcq.Margin = new System.Windows.Forms.Padding(4);
            this.gbTempAcq.Name = "gbTempAcq";
            this.gbTempAcq.Padding = new System.Windows.Forms.Padding(4);
            this.gbTempAcq.Size = new System.Drawing.Size(335, 77);
            this.gbTempAcq.TabIndex = 60;
            this.gbTempAcq.TabStop = false;
            this.gbTempAcq.Text = "温度采集";
            // 
            // lblTempValue
            // 
            this.lblTempValue.AutoSize = true;
            this.lblTempValue.Location = new System.Drawing.Point(153, 53);
            this.lblTempValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTempValue.Name = "lblTempValue";
            this.lblTempValue.Size = new System.Drawing.Size(41, 12);
            this.lblTempValue.TabIndex = 8;
            this.lblTempValue.Text = "0.0 ℃";
            // 
            // lblLocValue
            // 
            this.lblLocValue.AutoSize = true;
            this.lblLocValue.Location = new System.Drawing.Point(153, 28);
            this.lblLocValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocValue.Name = "lblLocValue";
            this.lblLocValue.Size = new System.Drawing.Size(35, 12);
            this.lblLocValue.TabIndex = 8;
            this.lblLocValue.Text = "(0,0)";
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.Location = new System.Drawing.Point(44, 53);
            this.lblTemp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(41, 12);
            this.lblTemp.TabIndex = 8;
            this.lblTemp.Text = "温度值";
            // 
            // lblLoc
            // 
            this.lblLoc.AutoSize = true;
            this.lblLoc.Location = new System.Drawing.Point(44, 28);
            this.lblLoc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(29, 12);
            this.lblLoc.TabIndex = 8;
            this.lblLoc.Text = "位置";
            // 
            // btnH264Snapshot
            // 
            this.btnH264Snapshot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnH264Snapshot.Font = new System.Drawing.Font("宋体", 9F);
            this.btnH264Snapshot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnH264Snapshot.Location = new System.Drawing.Point(204, 395);
            this.btnH264Snapshot.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnH264Snapshot.Name = "btnH264Snapshot";
            this.btnH264Snapshot.Size = new System.Drawing.Size(75, 23);
            this.btnH264Snapshot.TabIndex = 59;
            this.btnH264Snapshot.Text = "视频快照";
            this.btnH264Snapshot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnH264Snapshot.UseVisualStyleBackColor = false;
            this.btnH264Snapshot.Click += new System.EventHandler(this.btnH264Snapshot_Click);
            // 
            // btnAutoFocus
            // 
            this.btnAutoFocus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnAutoFocus.Font = new System.Drawing.Font("宋体", 9F);
            this.btnAutoFocus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAutoFocus.Location = new System.Drawing.Point(116, 367);
            this.btnAutoFocus.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnAutoFocus.Name = "btnAutoFocus";
            this.btnAutoFocus.Size = new System.Drawing.Size(75, 23);
            this.btnAutoFocus.TabIndex = 59;
            this.btnAutoFocus.Text = "自动对焦";
            this.btnAutoFocus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAutoFocus.UseVisualStyleBackColor = false;
            this.btnAutoFocus.Click += new System.EventHandler(this.btnAutoFocus_Click);
            // 
            // btnRawSnapshot
            // 
            this.btnRawSnapshot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnRawSnapshot.Font = new System.Drawing.Font("宋体", 9F);
            this.btnRawSnapshot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRawSnapshot.Location = new System.Drawing.Point(31, 395);
            this.btnRawSnapshot.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnRawSnapshot.Name = "btnRawSnapshot";
            this.btnRawSnapshot.Size = new System.Drawing.Size(75, 23);
            this.btnRawSnapshot.TabIndex = 59;
            this.btnRawSnapshot.Text = "原始图快照";
            this.btnRawSnapshot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRawSnapshot.UseVisualStyleBackColor = false;
            this.btnRawSnapshot.Click += new System.EventHandler(this.btnRawSnapshot_Click);
            // 
            // btntSnapshot
            // 
            this.btntSnapshot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btntSnapshot.Font = new System.Drawing.Font("宋体", 9F);
            this.btntSnapshot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btntSnapshot.Location = new System.Drawing.Point(116, 395);
            this.btntSnapshot.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btntSnapshot.Name = "btntSnapshot";
            this.btntSnapshot.Size = new System.Drawing.Size(75, 23);
            this.btntSnapshot.TabIndex = 59;
            this.btntSnapshot.Text = "温度图快照";
            this.btntSnapshot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btntSnapshot.UseVisualStyleBackColor = false;
            this.btntSnapshot.Click += new System.EventHandler(this.btntSnapshot_Click);
            // 
            // gbAdjust
            // 
            this.gbAdjust.Controls.Add(this.nudSnapInterval);
            this.gbAdjust.Controls.Add(this.lblCapture);
            this.gbAdjust.Controls.Add(this.lblSnapInterval);
            this.gbAdjust.Controls.Add(this.lblUserSnapIntervalUnit);
            this.gbAdjust.Controls.Add(this.btnSnap);
            this.gbAdjust.Controls.Add(this.cbCapture);
            this.gbAdjust.Controls.Add(this.cbTemperatureRange);
            this.gbAdjust.Controls.Add(this.lblLens);
            this.gbAdjust.Controls.Add(this.lblTemperatureRange);
            this.gbAdjust.Controls.Add(this.cbLens);
            this.gbAdjust.Controls.Add(this.ckShowMaxMin);
            this.gbAdjust.Controls.Add(this.cbEmissivity);
            this.gbAdjust.Controls.Add(this.cbUnit);
            this.gbAdjust.Controls.Add(this.ckShowTime);
            this.gbAdjust.Controls.Add(this.ckShowTitle);
            this.gbAdjust.Controls.Add(this.ckShowPalette);
            this.gbAdjust.Controls.Add(this.lblShow);
            this.gbAdjust.Controls.Add(this.lblPalette);
            this.gbAdjust.Controls.Add(this.cbPalette);
            this.gbAdjust.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbAdjust.Location = new System.Drawing.Point(6, 11);
            this.gbAdjust.Margin = new System.Windows.Forms.Padding(4);
            this.gbAdjust.Name = "gbAdjust";
            this.gbAdjust.Padding = new System.Windows.Forms.Padding(4);
            this.gbAdjust.Size = new System.Drawing.Size(335, 261);
            this.gbAdjust.TabIndex = 58;
            this.gbAdjust.TabStop = false;
            this.gbAdjust.Text = "视频设置";
            // 
            // nudSnapInterval
            // 
            this.nudSnapInterval.DecimalPlaces = 1;
            this.nudSnapInterval.Location = new System.Drawing.Point(146, 208);
            this.nudSnapInterval.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSnapInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudSnapInterval.Name = "nudSnapInterval";
            this.nudSnapInterval.Size = new System.Drawing.Size(71, 21);
            this.nudSnapInterval.TabIndex = 78;
            this.nudSnapInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // lblCapture
            // 
            this.lblCapture.AutoSize = true;
            this.lblCapture.Font = new System.Drawing.Font("宋体", 9F);
            this.lblCapture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCapture.Location = new System.Drawing.Point(47, 237);
            this.lblCapture.Name = "lblCapture";
            this.lblCapture.Size = new System.Drawing.Size(53, 12);
            this.lblCapture.TabIndex = 77;
            this.lblCapture.Text = "触发模式";
            // 
            // lblSnapInterval
            // 
            this.lblSnapInterval.AutoSize = true;
            this.lblSnapInterval.Font = new System.Drawing.Font("宋体", 9F);
            this.lblSnapInterval.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSnapInterval.Location = new System.Drawing.Point(47, 212);
            this.lblSnapInterval.Name = "lblSnapInterval";
            this.lblSnapInterval.Size = new System.Drawing.Size(65, 12);
            this.lblSnapInterval.TabIndex = 77;
            this.lblSnapInterval.Text = "自定义间隔";
            // 
            // lblUserSnapIntervalUnit
            // 
            this.lblUserSnapIntervalUnit.AutoSize = true;
            this.lblUserSnapIntervalUnit.Font = new System.Drawing.Font("宋体", 9F);
            this.lblUserSnapIntervalUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUserSnapIntervalUnit.Location = new System.Drawing.Point(222, 212);
            this.lblUserSnapIntervalUnit.Name = "lblUserSnapIntervalUnit";
            this.lblUserSnapIntervalUnit.Size = new System.Drawing.Size(11, 12);
            this.lblUserSnapIntervalUnit.TabIndex = 77;
            this.lblUserSnapIntervalUnit.Text = "s";
            // 
            // btnSnap
            // 
            this.btnSnap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnSnap.Location = new System.Drawing.Point(232, 207);
            this.btnSnap.Margin = new System.Windows.Forms.Padding(4);
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.Size = new System.Drawing.Size(65, 23);
            this.btnSnap.TabIndex = 54;
            this.btnSnap.Text = "抓图";
            this.btnSnap.UseVisualStyleBackColor = false;
            this.btnSnap.Click += new System.EventHandler(this.btnSnap_Click);
            // 
            // cbCapture
            // 
            this.cbCapture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCapture.FormattingEnabled = true;
            this.cbCapture.Location = new System.Drawing.Point(146, 233);
            this.cbCapture.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.cbCapture.Name = "cbCapture";
            this.cbCapture.Size = new System.Drawing.Size(150, 20);
            this.cbCapture.TabIndex = 23;
            this.cbCapture.SelectedIndexChanged += new System.EventHandler(this.cbCapture_SelectedIndexChanged);
            // 
            // cbTemperatureRange
            // 
            this.cbTemperatureRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTemperatureRange.FormattingEnabled = true;
            this.cbTemperatureRange.Location = new System.Drawing.Point(146, 181);
            this.cbTemperatureRange.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.cbTemperatureRange.Name = "cbTemperatureRange";
            this.cbTemperatureRange.Size = new System.Drawing.Size(150, 20);
            this.cbTemperatureRange.TabIndex = 23;
            this.cbTemperatureRange.SelectedIndexChanged += new System.EventHandler(this.cbTemperatureRange_SelectedIndexChanged);
            // 
            // lblLens
            // 
            this.lblLens.AutoSize = true;
            this.lblLens.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLens.Location = new System.Drawing.Point(47, 158);
            this.lblLens.Name = "lblLens";
            this.lblLens.Size = new System.Drawing.Size(29, 12);
            this.lblLens.TabIndex = 25;
            this.lblLens.Text = "镜头";
            // 
            // lblTemperatureRange
            // 
            this.lblTemperatureRange.AutoSize = true;
            this.lblTemperatureRange.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTemperatureRange.Location = new System.Drawing.Point(47, 185);
            this.lblTemperatureRange.Name = "lblTemperatureRange";
            this.lblTemperatureRange.Size = new System.Drawing.Size(53, 12);
            this.lblTemperatureRange.TabIndex = 24;
            this.lblTemperatureRange.Text = "温度范围";
            // 
            // cbLens
            // 
            this.cbLens.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLens.FormattingEnabled = true;
            this.cbLens.Location = new System.Drawing.Point(146, 154);
            this.cbLens.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.cbLens.Name = "cbLens";
            this.cbLens.Size = new System.Drawing.Size(150, 20);
            this.cbLens.TabIndex = 22;
            this.cbLens.SelectedIndexChanged += new System.EventHandler(this.cbLens_SelectedIndexChanged);
            // 
            // ckShowMaxMin
            // 
            this.ckShowMaxMin.AutoSize = true;
            this.ckShowMaxMin.Location = new System.Drawing.Point(146, 131);
            this.ckShowMaxMin.Name = "ckShowMaxMin";
            this.ckShowMaxMin.Size = new System.Drawing.Size(102, 16);
            this.ckShowMaxMin.TabIndex = 9;
            this.ckShowMaxMin.Text = "最高温/最低温";
            this.ckShowMaxMin.UseVisualStyleBackColor = true;
            this.ckShowMaxMin.CheckedChanged += new System.EventHandler(this.ckShowMaxMin_CheckedChanged);
            // 
            // cbEmissivity
            // 
            this.cbEmissivity.AutoSize = true;
            this.cbEmissivity.Location = new System.Drawing.Point(146, 106);
            this.cbEmissivity.Name = "cbEmissivity";
            this.cbEmissivity.Size = new System.Drawing.Size(60, 16);
            this.cbEmissivity.TabIndex = 9;
            this.cbEmissivity.Text = "辐射率";
            this.cbEmissivity.UseVisualStyleBackColor = true;
            this.cbEmissivity.CheckedChanged += new System.EventHandler(this.cbEmissivity_CheckedChanged);
            // 
            // cbUnit
            // 
            this.cbUnit.AutoSize = true;
            this.cbUnit.Location = new System.Drawing.Point(246, 81);
            this.cbUnit.Name = "cbUnit";
            this.cbUnit.Size = new System.Drawing.Size(48, 16);
            this.cbUnit.TabIndex = 9;
            this.cbUnit.Text = "单位";
            this.cbUnit.UseVisualStyleBackColor = true;
            this.cbUnit.CheckedChanged += new System.EventHandler(this.cbUnit_CheckedChanged);
            // 
            // ckShowTime
            // 
            this.ckShowTime.AutoSize = true;
            this.ckShowTime.Location = new System.Drawing.Point(146, 81);
            this.ckShowTime.Name = "ckShowTime";
            this.ckShowTime.Size = new System.Drawing.Size(48, 16);
            this.ckShowTime.TabIndex = 9;
            this.ckShowTime.Text = "时间";
            this.ckShowTime.UseVisualStyleBackColor = true;
            this.ckShowTime.CheckedChanged += new System.EventHandler(this.ckShowTime_CheckedChanged);
            // 
            // ckShowTitle
            // 
            this.ckShowTitle.AutoSize = true;
            this.ckShowTitle.Location = new System.Drawing.Point(246, 56);
            this.ckShowTitle.Name = "ckShowTitle";
            this.ckShowTitle.Size = new System.Drawing.Size(48, 16);
            this.ckShowTitle.TabIndex = 9;
            this.ckShowTitle.Text = "标题";
            this.ckShowTitle.UseVisualStyleBackColor = true;
            this.ckShowTitle.CheckedChanged += new System.EventHandler(this.ckShowTitle_CheckedChanged);
            // 
            // ckShowPalette
            // 
            this.ckShowPalette.AutoSize = true;
            this.ckShowPalette.Location = new System.Drawing.Point(146, 56);
            this.ckShowPalette.Name = "ckShowPalette";
            this.ckShowPalette.Size = new System.Drawing.Size(60, 16);
            this.ckShowPalette.TabIndex = 9;
            this.ckShowPalette.Text = "调色板";
            this.ckShowPalette.UseVisualStyleBackColor = true;
            this.ckShowPalette.CheckedChanged += new System.EventHandler(this.ckShowPalette_CheckedChanged);
            // 
            // lblShow
            // 
            this.lblShow.AutoSize = true;
            this.lblShow.Location = new System.Drawing.Point(47, 58);
            this.lblShow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShow.Name = "lblShow";
            this.lblShow.Size = new System.Drawing.Size(29, 12);
            this.lblShow.TabIndex = 8;
            this.lblShow.Text = "显示";
            // 
            // lblPalette
            // 
            this.lblPalette.AutoSize = true;
            this.lblPalette.Location = new System.Drawing.Point(47, 27);
            this.lblPalette.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPalette.Name = "lblPalette";
            this.lblPalette.Size = new System.Drawing.Size(41, 12);
            this.lblPalette.TabIndex = 8;
            this.lblPalette.Text = "调色板";
            // 
            // cbPalette
            // 
            this.cbPalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPalette.FormattingEnabled = true;
            this.cbPalette.Location = new System.Drawing.Point(146, 23);
            this.cbPalette.Margin = new System.Windows.Forms.Padding(4);
            this.cbPalette.Name = "cbPalette";
            this.cbPalette.Size = new System.Drawing.Size(150, 20);
            this.cbPalette.TabIndex = 0;
            this.cbPalette.SelectedIndexChanged += new System.EventHandler(this.cbPalette_SelectedIndexChanged);
            // 
            // pH264
            // 
            this.pH264.Controls.Add(this.gbStream);
            this.pH264.Dock = System.Windows.Forms.DockStyle.Top;
            this.pH264.Location = new System.Drawing.Point(0, 0);
            this.pH264.Name = "pH264";
            this.pH264.Size = new System.Drawing.Size(345, 52);
            this.pH264.TabIndex = 0;
            // 
            // gbStream
            // 
            this.gbStream.Controls.Add(this.rbPri);
            this.gbStream.Controls.Add(this.rbSub);
            this.gbStream.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbStream.Location = new System.Drawing.Point(6, 4);
            this.gbStream.Margin = new System.Windows.Forms.Padding(4);
            this.gbStream.Name = "gbStream";
            this.gbStream.Padding = new System.Windows.Forms.Padding(4);
            this.gbStream.Size = new System.Drawing.Size(335, 45);
            this.gbStream.TabIndex = 52;
            this.gbStream.TabStop = false;
            this.gbStream.Text = "码流";
            // 
            // rbPri
            // 
            this.rbPri.AutoSize = true;
            this.rbPri.Location = new System.Drawing.Point(49, 18);
            this.rbPri.Name = "rbPri";
            this.rbPri.Size = new System.Drawing.Size(59, 16);
            this.rbPri.TabIndex = 38;
            this.rbPri.TabStop = true;
            this.rbPri.Text = "主码流";
            this.rbPri.UseVisualStyleBackColor = true;
            this.rbPri.CheckedChanged += new System.EventHandler(this.rbPri_CheckedChanged);
            // 
            // rbSub
            // 
            this.rbSub.AutoSize = true;
            this.rbSub.Location = new System.Drawing.Point(187, 18);
            this.rbSub.Name = "rbSub";
            this.rbSub.Size = new System.Drawing.Size(59, 16);
            this.rbSub.TabIndex = 37;
            this.rbSub.TabStop = true;
            this.rbSub.Text = "子码流";
            this.rbSub.UseVisualStyleBackColor = true;
            this.rbSub.CheckedChanged += new System.EventHandler(this.rbSub_CheckedChanged);
            // 
            // ssMainForm
            // 
            this.ssMainForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ssMainForm.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ssMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslDeviceStatus});
            this.ssMainForm.Location = new System.Drawing.Point(0, 675);
            this.ssMainForm.Name = "ssMainForm";
            this.ssMainForm.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.ssMainForm.Size = new System.Drawing.Size(1137, 22);
            this.ssMainForm.TabIndex = 60;
            this.ssMainForm.Text = "statusStrip1";
            // 
            // tsslDeviceStatus
            // 
            this.tsslDeviceStatus.Name = "tsslDeviceStatus";
            this.tsslDeviceStatus.Size = new System.Drawing.Size(79, 17);
            this.tsslDeviceStatus.Text = "热像仪未连接";
            // 
            // msMainForm
            // 
            this.msMainForm.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenFile,
            this.tsmiUserManager,
            this.tsmiHelp});
            this.msMainForm.Location = new System.Drawing.Point(0, 0);
            this.msMainForm.Name = "msMainForm";
            this.msMainForm.Size = new System.Drawing.Size(1137, 24);
            this.msMainForm.TabIndex = 61;
            this.msMainForm.Text = "menuStrip1";
            // 
            // tsmiOpenFile
            // 
            this.tsmiOpenFile.Name = "tsmiOpenFile";
            this.tsmiOpenFile.Size = new System.Drawing.Size(43, 20);
            this.tsmiOpenFile.Text = "打开";
            this.tsmiOpenFile.Click += new System.EventHandler(this.tsmiOpenFile_Click);
            // 
            // tsmiUserManager
            // 
            this.tsmiUserManager.Name = "tsmiUserManager";
            this.tsmiUserManager.Size = new System.Drawing.Size(43, 20);
            this.tsmiUserManager.Text = "用户";
            this.tsmiUserManager.Click += new System.EventHandler(this.tsmiUserManager_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(43, 20);
            this.tsmiHelp.Text = "帮助";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(98, 22);
            this.tsmiAbout.Text = "关于";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // pMainForm
            // 
            this.pMainForm.Controls.Add(this.pCamera);
            this.pMainForm.Controls.Add(this.pConnect);
            this.pMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMainForm.Location = new System.Drawing.Point(0, 24);
            this.pMainForm.Name = "pMainForm";
            this.pMainForm.Size = new System.Drawing.Size(1137, 651);
            this.pMainForm.TabIndex = 62;
            // 
            // pCamera
            // 
            this.pCamera.Controls.Add(this.pbCamera);
            this.pCamera.Controls.Add(this.pConfig);
            this.pCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pCamera.Location = new System.Drawing.Point(0, 54);
            this.pCamera.Name = "pCamera";
            this.pCamera.Size = new System.Drawing.Size(1137, 597);
            this.pCamera.TabIndex = 60;
            // 
            // tMainForm
            // 
            this.tMainForm.Interval = 10;
            this.tMainForm.Tick += new System.EventHandler(this.tMainForm_Tick);
            // 
            // OWBMainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1137, 697);
            this.Controls.Add(this.pMainForm);
            this.Controls.Add(this.ssMainForm);
            this.Controls.Add(this.msMainForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OWBMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备连接";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OWBMainForm_FormClosed);
            this.SizeChanged += new System.EventHandler(this.OWBMainForm_SizeChanged);
            this.pConnect.ResumeLayout(false);
            this.gbConnMode.ResumeLayout(false);
            this.gbConnMode.PerformLayout();
            this.gbConnInfo.ResumeLayout(false);
            this.gbConnInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).EndInit();
            this.pConfig.ResumeLayout(false);
            this.pFile.ResumeLayout(false);
            this.gbFile.ResumeLayout(false);
            this.gbFile.PerformLayout();
            this.pCommon.ResumeLayout(false);
            this.gbTempAcq.ResumeLayout(false);
            this.gbTempAcq.PerformLayout();
            this.gbAdjust.ResumeLayout(false);
            this.gbAdjust.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSnapInterval)).EndInit();
            this.pH264.ResumeLayout(false);
            this.gbStream.ResumeLayout(false);
            this.gbStream.PerformLayout();
            this.ssMainForm.ResumeLayout(false);
            this.ssMainForm.PerformLayout();
            this.msMainForm.ResumeLayout(false);
            this.msMainForm.PerformLayout();
            this.pMainForm.ResumeLayout(false);
            this.pCamera.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pConnect;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.GroupBox gbConnMode;
        private System.Windows.Forms.RadioButton rbH264;
        private System.Windows.Forms.RadioButton rbRaw;
        private System.Windows.Forms.GroupBox gbConnInfo;
        private System.Windows.Forms.PictureBox pbCamera;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Panel pConfig;
        private System.Windows.Forms.Panel pCommon;
        private System.Windows.Forms.Panel pH264;
        private System.Windows.Forms.GroupBox gbStream;
        private System.Windows.Forms.RadioButton rbPri;
        private System.Windows.Forms.RadioButton rbSub;
        private System.Windows.Forms.StatusStrip ssMainForm;
        private System.Windows.Forms.ToolStripStatusLabel tsslDeviceStatus;
        private System.Windows.Forms.GroupBox gbTempAcq;
        private System.Windows.Forms.Label lblTempValue;
        private System.Windows.Forms.Label lblLocValue;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.Button btntSnapshot;
        private System.Windows.Forms.GroupBox gbAdjust;
        private System.Windows.Forms.Label lblPalette;
        private System.Windows.Forms.ComboBox cbPalette;
        private System.Windows.Forms.MenuStrip msMainForm;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.Panel pMainForm;
        private System.Windows.Forms.Panel pCamera;
        private System.Windows.Forms.Timer tMainForm;
        private System.Windows.Forms.ToolTip ttMainForm;
        private System.Windows.Forms.Button btnH264Snapshot;
        private System.Windows.Forms.Button btnAutoFocus;
        private System.Windows.Forms.Button btnRawSnapshot;
        private System.Windows.Forms.CheckBox ckShowMaxMin;
        private System.Windows.Forms.CheckBox ckShowPalette;
        private System.Windows.Forms.Label lblShow;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Panel pFile;
        private System.Windows.Forms.GroupBox gbFile;
        private System.Windows.Forms.Label lblFileTempValue;
        private System.Windows.Forms.Label lblFileLocValue;
        private System.Windows.Forms.Label lblFileTemp;
        private System.Windows.Forms.Label lblFileLoc;
        private System.Windows.Forms.Label lblFilePalette;
        private System.Windows.Forms.ComboBox cbFilePalette;
        private System.Windows.Forms.CheckBox ckShowTime;
        private System.Windows.Forms.CheckBox ckShowTitle;
        private System.Windows.Forms.ToolStripMenuItem tsmiUserManager;
        private System.Windows.Forms.CheckBox cbUnit;
        private System.Windows.Forms.CheckBox cbEmissivity;
        private System.Windows.Forms.Button btnNearFocus;
        private System.Windows.Forms.Button btnFarFocus;
        private System.Windows.Forms.ComboBox cbTemperatureRange;
        private System.Windows.Forms.Label lblLens;
        private System.Windows.Forms.Label lblTemperatureRange;
        private System.Windows.Forms.ComboBox cbLens;
        private System.Windows.Forms.Button btnSnap;
        private System.Windows.Forms.Label lblUserSnapIntervalUnit;
        private System.Windows.Forms.NumericUpDown nudSnapInterval;
        private System.Windows.Forms.Label lblSnapInterval;
        private System.Windows.Forms.Label lblCapture;
        private System.Windows.Forms.ComboBox cbCapture;
        private System.Windows.Forms.Button btnRecord;
    }
}

