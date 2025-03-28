namespace OWB.SnipDemo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pConnect = new System.Windows.Forms.Panel();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSnip = new System.Windows.Forms.Button();
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
            this.pMainForm = new System.Windows.Forms.Panel();
            this.plMenu = new System.Windows.Forms.Panel();
            this.gbSetting = new System.Windows.Forms.GroupBox();
            this.btnDoCali = new System.Windows.Forms.Button();
            this.btnNearFocus = new System.Windows.Forms.Button();
            this.btnFarFocus = new System.Windows.Forms.Button();
            this.btnAutoFocus = new System.Windows.Forms.Button();
            this.lblPalette = new System.Windows.Forms.Label();
            this.cbPalette = new System.Windows.Forms.ComboBox();
            this.gbTempParameter = new System.Windows.Forms.GroupBox();
            this.nudEmissivityValue = new System.Windows.Forms.NumericUpDown();
            this.lblOffsetRange = new System.Windows.Forms.Label();
            this.lblDistanceRange = new System.Windows.Forms.Label();
            this.nudRelativeHumidityValue = new System.Windows.Forms.NumericUpDown();
            this.lblLensTransRange = new System.Windows.Forms.Label();
            this.lblAtmosphericTemperatureRange = new System.Windows.Forms.Label();
            this.nudLensTValue = new System.Windows.Forms.NumericUpDown();
            this.nudReflectedTemperatureValue = new System.Windows.Forms.NumericUpDown();
            this.lblLensTRange = new System.Windows.Forms.Label();
            this.lblReflectedTemperatureRange = new System.Windows.Forms.Label();
            this.nudLensTransValue = new System.Windows.Forms.NumericUpDown();
            this.nudAtmosphericTemperatureValue = new System.Windows.Forms.NumericUpDown();
            this.lblRelativeHumidityRange = new System.Windows.Forms.Label();
            this.nudOffsetValue = new System.Windows.Forms.NumericUpDown();
            this.nudDistanceValue = new System.Windows.Forms.NumericUpDown();
            this.lblEmissivityRange = new System.Windows.Forms.Label();
            this.lblEmissivity = new System.Windows.Forms.Label();
            this.lblRelativeHumidity = new System.Windows.Forms.Label();
            this.lblOffset = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.lblLensT = new System.Windows.Forms.Label();
            this.lblLensTrans = new System.Windows.Forms.Label();
            this.lblReflectedTemperature = new System.Windows.Forms.Label();
            this.lblAtmosphericTemperature = new System.Windows.Forms.Label();
            this.pbCamera = new System.Windows.Forms.PictureBox();
            this.pConnect.SuspendLayout();
            this.gbConnMode.SuspendLayout();
            this.gbConnInfo.SuspendLayout();
            this.pMainForm.SuspendLayout();
            this.plMenu.SuspendLayout();
            this.gbSetting.SuspendLayout();
            this.gbTempParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEmissivityValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRelativeHumidityValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLensTValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReflectedTemperatureValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLensTransValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAtmosphericTemperatureValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistanceValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // pConnect
            // 
            this.pConnect.Controls.Add(this.btnStop);
            this.pConnect.Controls.Add(this.btnSnip);
            this.pConnect.Controls.Add(this.btnPlay);
            this.pConnect.Controls.Add(this.btnConnect);
            this.pConnect.Controls.Add(this.gbConnMode);
            this.pConnect.Controls.Add(this.gbConnInfo);
            this.pConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.pConnect.Location = new System.Drawing.Point(0, 0);
            this.pConnect.Margin = new System.Windows.Forms.Padding(4);
            this.pConnect.Name = "pConnect";
            this.pConnect.Size = new System.Drawing.Size(1194, 68);
            this.pConnect.TabIndex = 57;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnStop.Location = new System.Drawing.Point(1017, 15);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 56;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSnip
            // 
            this.btnSnip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnSnip.Location = new System.Drawing.Point(1100, 15);
            this.btnSnip.Margin = new System.Windows.Forms.Padding(4);
            this.btnSnip.Name = "btnSnip";
            this.btnSnip.Size = new System.Drawing.Size(75, 23);
            this.btnSnip.TabIndex = 55;
            this.btnSnip.Text = "抓图";
            this.btnSnip.UseVisualStyleBackColor = false;
            this.btnSnip.Click += new System.EventHandler(this.btnSnip_Click);
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
            this.rbH264.Checked = true;
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
            // pMainForm
            // 
            this.pMainForm.Controls.Add(this.plMenu);
            this.pMainForm.Controls.Add(this.pbCamera);
            this.pMainForm.Controls.Add(this.pConnect);
            this.pMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMainForm.Location = new System.Drawing.Point(0, 0);
            this.pMainForm.Name = "pMainForm";
            this.pMainForm.Size = new System.Drawing.Size(1194, 681);
            this.pMainForm.TabIndex = 62;
            // 
            // plMenu
            // 
            this.plMenu.Controls.Add(this.gbSetting);
            this.plMenu.Controls.Add(this.gbTempParameter);
            this.plMenu.Dock = System.Windows.Forms.DockStyle.Right;
            this.plMenu.Location = new System.Drawing.Point(835, 68);
            this.plMenu.Name = "plMenu";
            this.plMenu.Size = new System.Drawing.Size(359, 613);
            this.plMenu.TabIndex = 59;
            // 
            // gbSetting
            // 
            this.gbSetting.Controls.Add(this.btnDoCali);
            this.gbSetting.Controls.Add(this.btnNearFocus);
            this.gbSetting.Controls.Add(this.btnFarFocus);
            this.gbSetting.Controls.Add(this.btnAutoFocus);
            this.gbSetting.Controls.Add(this.lblPalette);
            this.gbSetting.Controls.Add(this.cbPalette);
            this.gbSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSetting.Location = new System.Drawing.Point(0, 280);
            this.gbSetting.Name = "gbSetting";
            this.gbSetting.Size = new System.Drawing.Size(359, 128);
            this.gbSetting.TabIndex = 63;
            this.gbSetting.TabStop = false;
            this.gbSetting.Text = "设备设置";
            // 
            // btnDoCali
            // 
            this.btnDoCali.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnDoCali.Font = new System.Drawing.Font("宋体", 9F);
            this.btnDoCali.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDoCali.Location = new System.Drawing.Point(210, 78);
            this.btnDoCali.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnDoCali.Name = "btnDoCali";
            this.btnDoCali.Size = new System.Drawing.Size(75, 23);
            this.btnDoCali.TabIndex = 66;
            this.btnDoCali.Text = "手动校准";
            this.btnDoCali.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDoCali.UseVisualStyleBackColor = false;
            this.btnDoCali.Click += new System.EventHandler(this.btnDoCali_Click);
            // 
            // btnNearFocus
            // 
            this.btnNearFocus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnNearFocus.Location = new System.Drawing.Point(42, 78);
            this.btnNearFocus.Margin = new System.Windows.Forms.Padding(4);
            this.btnNearFocus.Name = "btnNearFocus";
            this.btnNearFocus.Size = new System.Drawing.Size(23, 23);
            this.btnNearFocus.TabIndex = 64;
            this.btnNearFocus.Text = "-";
            this.btnNearFocus.UseVisualStyleBackColor = false;
            this.btnNearFocus.Click += new System.EventHandler(this.btnNearFocus_Click);
            // 
            // btnFarFocus
            // 
            this.btnFarFocus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnFarFocus.Location = new System.Drawing.Point(163, 78);
            this.btnFarFocus.Margin = new System.Windows.Forms.Padding(4);
            this.btnFarFocus.Name = "btnFarFocus";
            this.btnFarFocus.Size = new System.Drawing.Size(23, 23);
            this.btnFarFocus.TabIndex = 65;
            this.btnFarFocus.Text = "+";
            this.btnFarFocus.UseVisualStyleBackColor = false;
            this.btnFarFocus.Click += new System.EventHandler(this.btnFarFocus_Click);
            // 
            // btnAutoFocus
            // 
            this.btnAutoFocus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnAutoFocus.Font = new System.Drawing.Font("宋体", 9F);
            this.btnAutoFocus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAutoFocus.Location = new System.Drawing.Point(75, 78);
            this.btnAutoFocus.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnAutoFocus.Name = "btnAutoFocus";
            this.btnAutoFocus.Size = new System.Drawing.Size(75, 23);
            this.btnAutoFocus.TabIndex = 63;
            this.btnAutoFocus.Text = "自动对焦";
            this.btnAutoFocus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAutoFocus.UseVisualStyleBackColor = false;
            this.btnAutoFocus.Click += new System.EventHandler(this.btnAutoFocus_Click);
            // 
            // lblPalette
            // 
            this.lblPalette.AutoSize = true;
            this.lblPalette.Location = new System.Drawing.Point(36, 30);
            this.lblPalette.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPalette.Name = "lblPalette";
            this.lblPalette.Size = new System.Drawing.Size(41, 12);
            this.lblPalette.TabIndex = 10;
            this.lblPalette.Text = "调色板";
            // 
            // cbPalette
            // 
            this.cbPalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPalette.FormattingEnabled = true;
            this.cbPalette.Location = new System.Drawing.Point(135, 26);
            this.cbPalette.Margin = new System.Windows.Forms.Padding(4);
            this.cbPalette.Name = "cbPalette";
            this.cbPalette.Size = new System.Drawing.Size(150, 20);
            this.cbPalette.TabIndex = 9;
            this.cbPalette.SelectedValueChanged += new System.EventHandler(this.cbPalette_SelectedValueChanged);
            // 
            // gbTempParameter
            // 
            this.gbTempParameter.Controls.Add(this.nudEmissivityValue);
            this.gbTempParameter.Controls.Add(this.lblOffsetRange);
            this.gbTempParameter.Controls.Add(this.lblDistanceRange);
            this.gbTempParameter.Controls.Add(this.nudRelativeHumidityValue);
            this.gbTempParameter.Controls.Add(this.lblLensTransRange);
            this.gbTempParameter.Controls.Add(this.lblAtmosphericTemperatureRange);
            this.gbTempParameter.Controls.Add(this.nudLensTValue);
            this.gbTempParameter.Controls.Add(this.nudReflectedTemperatureValue);
            this.gbTempParameter.Controls.Add(this.lblLensTRange);
            this.gbTempParameter.Controls.Add(this.lblReflectedTemperatureRange);
            this.gbTempParameter.Controls.Add(this.nudLensTransValue);
            this.gbTempParameter.Controls.Add(this.nudAtmosphericTemperatureValue);
            this.gbTempParameter.Controls.Add(this.lblRelativeHumidityRange);
            this.gbTempParameter.Controls.Add(this.nudOffsetValue);
            this.gbTempParameter.Controls.Add(this.nudDistanceValue);
            this.gbTempParameter.Controls.Add(this.lblEmissivityRange);
            this.gbTempParameter.Controls.Add(this.lblEmissivity);
            this.gbTempParameter.Controls.Add(this.lblRelativeHumidity);
            this.gbTempParameter.Controls.Add(this.lblOffset);
            this.gbTempParameter.Controls.Add(this.lblDistance);
            this.gbTempParameter.Controls.Add(this.lblLensT);
            this.gbTempParameter.Controls.Add(this.lblLensTrans);
            this.gbTempParameter.Controls.Add(this.lblReflectedTemperature);
            this.gbTempParameter.Controls.Add(this.lblAtmosphericTemperature);
            this.gbTempParameter.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTempParameter.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbTempParameter.Location = new System.Drawing.Point(0, 0);
            this.gbTempParameter.Margin = new System.Windows.Forms.Padding(4);
            this.gbTempParameter.Name = "gbTempParameter";
            this.gbTempParameter.Padding = new System.Windows.Forms.Padding(4);
            this.gbTempParameter.Size = new System.Drawing.Size(359, 280);
            this.gbTempParameter.TabIndex = 62;
            this.gbTempParameter.TabStop = false;
            this.gbTempParameter.Text = "测温参数";
            // 
            // nudEmissivityValue
            // 
            this.nudEmissivityValue.DecimalPlaces = 2;
            this.nudEmissivityValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudEmissivityValue.Location = new System.Drawing.Point(253, 30);
            this.nudEmissivityValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudEmissivityValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEmissivityValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudEmissivityValue.Name = "nudEmissivityValue";
            this.nudEmissivityValue.Size = new System.Drawing.Size(73, 21);
            this.nudEmissivityValue.TabIndex = 49;
            this.nudEmissivityValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.nudEmissivityValue.ValueChanged += new System.EventHandler(this.deviceParamValue_ValueChanged);
            // 
            // lblOffsetRange
            // 
            this.lblOffsetRange.AutoSize = true;
            this.lblOffsetRange.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOffsetRange.Location = new System.Drawing.Point(137, 258);
            this.lblOffsetRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOffsetRange.Name = "lblOffsetRange";
            this.lblOffsetRange.Size = new System.Drawing.Size(65, 12);
            this.lblOffsetRange.TabIndex = 47;
            this.lblOffsetRange.Text = "(-200~200)";
            // 
            // lblDistanceRange
            // 
            this.lblDistanceRange.AutoSize = true;
            this.lblDistanceRange.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDistanceRange.Location = new System.Drawing.Point(137, 162);
            this.lblDistanceRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDistanceRange.Name = "lblDistanceRange";
            this.lblDistanceRange.Size = new System.Drawing.Size(77, 12);
            this.lblDistanceRange.TabIndex = 47;
            this.lblDistanceRange.Text = "(0.1~1000.0)";
            // 
            // nudRelativeHumidityValue
            // 
            this.nudRelativeHumidityValue.DecimalPlaces = 2;
            this.nudRelativeHumidityValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudRelativeHumidityValue.Location = new System.Drawing.Point(253, 62);
            this.nudRelativeHumidityValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudRelativeHumidityValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRelativeHumidityValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudRelativeHumidityValue.Name = "nudRelativeHumidityValue";
            this.nudRelativeHumidityValue.Size = new System.Drawing.Size(73, 21);
            this.nudRelativeHumidityValue.TabIndex = 51;
            this.nudRelativeHumidityValue.Value = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.nudRelativeHumidityValue.ValueChanged += new System.EventHandler(this.deviceParamValue_ValueChanged);
            // 
            // lblLensTransRange
            // 
            this.lblLensTransRange.AutoSize = true;
            this.lblLensTransRange.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLensTransRange.Location = new System.Drawing.Point(137, 226);
            this.lblLensTransRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLensTransRange.Name = "lblLensTransRange";
            this.lblLensTransRange.Size = new System.Drawing.Size(71, 12);
            this.lblLensTransRange.TabIndex = 44;
            this.lblLensTransRange.Text = "(0.01~1.00)";
            // 
            // lblAtmosphericTemperatureRange
            // 
            this.lblAtmosphericTemperatureRange.AutoSize = true;
            this.lblAtmosphericTemperatureRange.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAtmosphericTemperatureRange.Location = new System.Drawing.Point(137, 130);
            this.lblAtmosphericTemperatureRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAtmosphericTemperatureRange.Name = "lblAtmosphericTemperatureRange";
            this.lblAtmosphericTemperatureRange.Size = new System.Drawing.Size(65, 12);
            this.lblAtmosphericTemperatureRange.TabIndex = 44;
            this.lblAtmosphericTemperatureRange.Text = "(-200~200)";
            // 
            // nudLensTValue
            // 
            this.nudLensTValue.DecimalPlaces = 1;
            this.nudLensTValue.Location = new System.Drawing.Point(253, 190);
            this.nudLensTValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudLensTValue.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            65536});
            this.nudLensTValue.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147418112});
            this.nudLensTValue.Name = "nudLensTValue";
            this.nudLensTValue.Size = new System.Drawing.Size(73, 21);
            this.nudLensTValue.TabIndex = 52;
            this.nudLensTValue.Value = new decimal(new int[] {
            200,
            0,
            0,
            65536});
            this.nudLensTValue.ValueChanged += new System.EventHandler(this.deviceParamValue_ValueChanged);
            // 
            // nudReflectedTemperatureValue
            // 
            this.nudReflectedTemperatureValue.DecimalPlaces = 1;
            this.nudReflectedTemperatureValue.Location = new System.Drawing.Point(253, 94);
            this.nudReflectedTemperatureValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudReflectedTemperatureValue.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            65536});
            this.nudReflectedTemperatureValue.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147418112});
            this.nudReflectedTemperatureValue.Name = "nudReflectedTemperatureValue";
            this.nudReflectedTemperatureValue.Size = new System.Drawing.Size(73, 21);
            this.nudReflectedTemperatureValue.TabIndex = 52;
            this.nudReflectedTemperatureValue.Value = new decimal(new int[] {
            200,
            0,
            0,
            65536});
            this.nudReflectedTemperatureValue.ValueChanged += new System.EventHandler(this.deviceParamValue_ValueChanged);
            // 
            // lblLensTRange
            // 
            this.lblLensTRange.AutoSize = true;
            this.lblLensTRange.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLensTRange.Location = new System.Drawing.Point(137, 194);
            this.lblLensTRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLensTRange.Name = "lblLensTRange";
            this.lblLensTRange.Size = new System.Drawing.Size(65, 12);
            this.lblLensTRange.TabIndex = 45;
            this.lblLensTRange.Text = "(-200~200)";
            // 
            // lblReflectedTemperatureRange
            // 
            this.lblReflectedTemperatureRange.AutoSize = true;
            this.lblReflectedTemperatureRange.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReflectedTemperatureRange.Location = new System.Drawing.Point(137, 98);
            this.lblReflectedTemperatureRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReflectedTemperatureRange.Name = "lblReflectedTemperatureRange";
            this.lblReflectedTemperatureRange.Size = new System.Drawing.Size(65, 12);
            this.lblReflectedTemperatureRange.TabIndex = 45;
            this.lblReflectedTemperatureRange.Text = "(-200~200)";
            // 
            // nudLensTransValue
            // 
            this.nudLensTransValue.DecimalPlaces = 2;
            this.nudLensTransValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudLensTransValue.Location = new System.Drawing.Point(253, 222);
            this.nudLensTransValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudLensTransValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLensTransValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudLensTransValue.Name = "nudLensTransValue";
            this.nudLensTransValue.Size = new System.Drawing.Size(73, 21);
            this.nudLensTransValue.TabIndex = 53;
            this.nudLensTransValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLensTransValue.ValueChanged += new System.EventHandler(this.deviceParamValue_ValueChanged);
            // 
            // nudAtmosphericTemperatureValue
            // 
            this.nudAtmosphericTemperatureValue.DecimalPlaces = 1;
            this.nudAtmosphericTemperatureValue.Location = new System.Drawing.Point(253, 126);
            this.nudAtmosphericTemperatureValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudAtmosphericTemperatureValue.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            65536});
            this.nudAtmosphericTemperatureValue.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147418112});
            this.nudAtmosphericTemperatureValue.Name = "nudAtmosphericTemperatureValue";
            this.nudAtmosphericTemperatureValue.Size = new System.Drawing.Size(73, 21);
            this.nudAtmosphericTemperatureValue.TabIndex = 53;
            this.nudAtmosphericTemperatureValue.Value = new decimal(new int[] {
            200,
            0,
            0,
            65536});
            this.nudAtmosphericTemperatureValue.ValueChanged += new System.EventHandler(this.deviceParamValue_ValueChanged);
            // 
            // lblRelativeHumidityRange
            // 
            this.lblRelativeHumidityRange.AutoSize = true;
            this.lblRelativeHumidityRange.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRelativeHumidityRange.Location = new System.Drawing.Point(137, 66);
            this.lblRelativeHumidityRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRelativeHumidityRange.Name = "lblRelativeHumidityRange";
            this.lblRelativeHumidityRange.Size = new System.Drawing.Size(71, 12);
            this.lblRelativeHumidityRange.TabIndex = 46;
            this.lblRelativeHumidityRange.Text = "(0.01~1.00)";
            // 
            // nudOffsetValue
            // 
            this.nudOffsetValue.DecimalPlaces = 1;
            this.nudOffsetValue.Location = new System.Drawing.Point(253, 254);
            this.nudOffsetValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudOffsetValue.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudOffsetValue.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudOffsetValue.Name = "nudOffsetValue";
            this.nudOffsetValue.Size = new System.Drawing.Size(73, 21);
            this.nudOffsetValue.TabIndex = 50;
            this.nudOffsetValue.ValueChanged += new System.EventHandler(this.deviceParamValue_ValueChanged);
            // 
            // nudDistanceValue
            // 
            this.nudDistanceValue.DecimalPlaces = 1;
            this.nudDistanceValue.Location = new System.Drawing.Point(253, 158);
            this.nudDistanceValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudDistanceValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDistanceValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudDistanceValue.Name = "nudDistanceValue";
            this.nudDistanceValue.Size = new System.Drawing.Size(73, 21);
            this.nudDistanceValue.TabIndex = 50;
            this.nudDistanceValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDistanceValue.ValueChanged += new System.EventHandler(this.deviceParamValue_ValueChanged);
            // 
            // lblEmissivityRange
            // 
            this.lblEmissivityRange.AutoSize = true;
            this.lblEmissivityRange.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmissivityRange.Location = new System.Drawing.Point(137, 34);
            this.lblEmissivityRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmissivityRange.Name = "lblEmissivityRange";
            this.lblEmissivityRange.Size = new System.Drawing.Size(71, 12);
            this.lblEmissivityRange.TabIndex = 48;
            this.lblEmissivityRange.Text = "(0.01~1.00)";
            // 
            // lblEmissivity
            // 
            this.lblEmissivity.AutoSize = true;
            this.lblEmissivity.Location = new System.Drawing.Point(31, 34);
            this.lblEmissivity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmissivity.Name = "lblEmissivity";
            this.lblEmissivity.Size = new System.Drawing.Size(41, 12);
            this.lblEmissivity.TabIndex = 41;
            this.lblEmissivity.Text = "发射率";
            // 
            // lblRelativeHumidity
            // 
            this.lblRelativeHumidity.AutoSize = true;
            this.lblRelativeHumidity.Location = new System.Drawing.Point(31, 66);
            this.lblRelativeHumidity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRelativeHumidity.Name = "lblRelativeHumidity";
            this.lblRelativeHumidity.Size = new System.Drawing.Size(29, 12);
            this.lblRelativeHumidity.TabIndex = 41;
            this.lblRelativeHumidity.Text = "湿度";
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Location = new System.Drawing.Point(31, 258);
            this.lblOffset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(29, 12);
            this.lblOffset.TabIndex = 41;
            this.lblOffset.Text = "偏移";
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(31, 162);
            this.lblDistance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(29, 12);
            this.lblDistance.TabIndex = 41;
            this.lblDistance.Text = "距离";
            // 
            // lblLensT
            // 
            this.lblLensT.AutoSize = true;
            this.lblLensT.Location = new System.Drawing.Point(31, 194);
            this.lblLensT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLensT.Name = "lblLensT";
            this.lblLensT.Size = new System.Drawing.Size(77, 12);
            this.lblLensT.TabIndex = 41;
            this.lblLensT.Text = "外部光学温度";
            // 
            // lblLensTrans
            // 
            this.lblLensTrans.AutoSize = true;
            this.lblLensTrans.Location = new System.Drawing.Point(31, 226);
            this.lblLensTrans.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLensTrans.Name = "lblLensTrans";
            this.lblLensTrans.Size = new System.Drawing.Size(89, 12);
            this.lblLensTrans.TabIndex = 41;
            this.lblLensTrans.Text = "外部光学透射率";
            // 
            // lblReflectedTemperature
            // 
            this.lblReflectedTemperature.AutoSize = true;
            this.lblReflectedTemperature.Location = new System.Drawing.Point(31, 98);
            this.lblReflectedTemperature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReflectedTemperature.Name = "lblReflectedTemperature";
            this.lblReflectedTemperature.Size = new System.Drawing.Size(53, 12);
            this.lblReflectedTemperature.TabIndex = 41;
            this.lblReflectedTemperature.Text = "反射温度";
            // 
            // lblAtmosphericTemperature
            // 
            this.lblAtmosphericTemperature.AutoSize = true;
            this.lblAtmosphericTemperature.Location = new System.Drawing.Point(31, 130);
            this.lblAtmosphericTemperature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAtmosphericTemperature.Name = "lblAtmosphericTemperature";
            this.lblAtmosphericTemperature.Size = new System.Drawing.Size(53, 12);
            this.lblAtmosphericTemperature.TabIndex = 41;
            this.lblAtmosphericTemperature.Text = "环境温度";
            // 
            // pbCamera
            // 
            this.pbCamera.Location = new System.Drawing.Point(7, 75);
            this.pbCamera.Name = "pbCamera";
            this.pbCamera.Size = new System.Drawing.Size(640, 480);
            this.pbCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCamera.TabIndex = 58;
            this.pbCamera.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1194, 681);
            this.Controls.Add(this.pMainForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备抓图";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OWBMainForm_FormClosed);
            this.pConnect.ResumeLayout(false);
            this.gbConnMode.ResumeLayout(false);
            this.gbConnMode.PerformLayout();
            this.gbConnInfo.ResumeLayout(false);
            this.gbConnInfo.PerformLayout();
            this.pMainForm.ResumeLayout(false);
            this.plMenu.ResumeLayout(false);
            this.gbSetting.ResumeLayout(false);
            this.gbSetting.PerformLayout();
            this.gbTempParameter.ResumeLayout(false);
            this.gbTempParameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEmissivityValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRelativeHumidityValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLensTValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReflectedTemperatureValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLensTransValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAtmosphericTemperatureValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistanceValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Panel pMainForm;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnSnip;
        private System.Windows.Forms.PictureBox pbCamera;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Panel plMenu;
        private System.Windows.Forms.GroupBox gbTempParameter;
        private System.Windows.Forms.NumericUpDown nudEmissivityValue;
        private System.Windows.Forms.Label lblOffsetRange;
        private System.Windows.Forms.Label lblDistanceRange;
        private System.Windows.Forms.NumericUpDown nudRelativeHumidityValue;
        private System.Windows.Forms.Label lblLensTransRange;
        private System.Windows.Forms.Label lblAtmosphericTemperatureRange;
        private System.Windows.Forms.NumericUpDown nudLensTValue;
        private System.Windows.Forms.NumericUpDown nudReflectedTemperatureValue;
        private System.Windows.Forms.Label lblLensTRange;
        private System.Windows.Forms.Label lblReflectedTemperatureRange;
        private System.Windows.Forms.NumericUpDown nudLensTransValue;
        private System.Windows.Forms.NumericUpDown nudAtmosphericTemperatureValue;
        private System.Windows.Forms.Label lblRelativeHumidityRange;
        private System.Windows.Forms.NumericUpDown nudOffsetValue;
        private System.Windows.Forms.NumericUpDown nudDistanceValue;
        private System.Windows.Forms.Label lblEmissivityRange;
        private System.Windows.Forms.Label lblEmissivity;
        private System.Windows.Forms.Label lblRelativeHumidity;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label lblLensT;
        private System.Windows.Forms.Label lblLensTrans;
        private System.Windows.Forms.Label lblReflectedTemperature;
        private System.Windows.Forms.Label lblAtmosphericTemperature;
        private System.Windows.Forms.GroupBox gbSetting;
        private System.Windows.Forms.Label lblPalette;
        private System.Windows.Forms.ComboBox cbPalette;
        private System.Windows.Forms.Button btnNearFocus;
        private System.Windows.Forms.Button btnFarFocus;
        private System.Windows.Forms.Button btnAutoFocus;
        private System.Windows.Forms.Button btnDoCali;
    }
}

