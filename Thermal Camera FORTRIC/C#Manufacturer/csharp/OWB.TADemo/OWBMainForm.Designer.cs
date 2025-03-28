namespace OWB.TADemo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OWBMainForm));
            this.msMainForm = new System.Windows.Forms.MenuStrip();
            this.tsmiConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSerialSet = new System.Windows.Forms.ToolStripMenuItem();
            this.ssMainForm = new System.Windows.Forms.StatusStrip();
            this.tsslDeviceStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pMainForm = new System.Windows.Forms.Panel();
            this.pCamera = new System.Windows.Forms.Panel();
            this.pbCamera = new System.Windows.Forms.PictureBox();
            this.cmsMarker = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.pConfig = new System.Windows.Forms.Panel();
            this.dgvMarker = new System.Windows.Forms.DataGridView();
            this.标记流名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.最高温 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.最低温 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.平均温 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.gbTempAcq = new System.Windows.Forms.GroupBox();
            this.lblCurrentTemp = new System.Windows.Forms.Label();
            this.lblCurrentLoc = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.tMainForm = new System.Windows.Forms.Timer(this.components);
            this.msMainForm.SuspendLayout();
            this.ssMainForm.SuspendLayout();
            this.pMainForm.SuspendLayout();
            this.pCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).BeginInit();
            this.cmsMarker.SuspendLayout();
            this.pConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarker)).BeginInit();
            this.gbTempParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEmissivityValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRelativeHumidityValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLensTValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReflectedTemperatureValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLensTransValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAtmosphericTemperatureValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistanceValue)).BeginInit();
            this.gbTempAcq.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMainForm
            // 
            this.msMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConnect,
            this.tsmiHelp,
            this.tsmiSerialSet});
            this.msMainForm.Location = new System.Drawing.Point(0, 0);
            this.msMainForm.Name = "msMainForm";
            this.msMainForm.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.msMainForm.Size = new System.Drawing.Size(992, 25);
            this.msMainForm.TabIndex = 62;
            this.msMainForm.Text = "menuStrip1";
            // 
            // tsmiConnect
            // 
            this.tsmiConnect.Name = "tsmiConnect";
            this.tsmiConnect.Size = new System.Drawing.Size(44, 21);
            this.tsmiConnect.Text = "连接";
            this.tsmiConnect.Click += new System.EventHandler(this.tsmiConnect_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(44, 21);
            this.tsmiHelp.Text = "帮助";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(100, 22);
            this.tsmiAbout.Text = "关于";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // tsmiSerialSet
            // 
            this.tsmiSerialSet.Name = "tsmiSerialSet";
            this.tsmiSerialSet.Size = new System.Drawing.Size(68, 21);
            this.tsmiSerialSet.Text = "串口设置";
            this.tsmiSerialSet.Click += new System.EventHandler(this.tsmiSerialSet_Click);
            // 
            // ssMainForm
            // 
            this.ssMainForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ssMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslDeviceStatus});
            this.ssMainForm.Location = new System.Drawing.Point(0, 509);
            this.ssMainForm.Name = "ssMainForm";
            this.ssMainForm.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.ssMainForm.Size = new System.Drawing.Size(992, 22);
            this.ssMainForm.TabIndex = 64;
            this.ssMainForm.Text = "statusStrip1";
            // 
            // tsslDeviceStatus
            // 
            this.tsslDeviceStatus.Name = "tsslDeviceStatus";
            this.tsslDeviceStatus.Size = new System.Drawing.Size(80, 17);
            this.tsslDeviceStatus.Text = "热像仪未连接";
            // 
            // pMainForm
            // 
            this.pMainForm.Controls.Add(this.pCamera);
            this.pMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMainForm.Location = new System.Drawing.Point(0, 25);
            this.pMainForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pMainForm.Name = "pMainForm";
            this.pMainForm.Size = new System.Drawing.Size(992, 484);
            this.pMainForm.TabIndex = 65;
            // 
            // pCamera
            // 
            this.pCamera.Controls.Add(this.pbCamera);
            this.pCamera.Controls.Add(this.pConfig);
            this.pCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pCamera.Location = new System.Drawing.Point(0, 0);
            this.pCamera.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pCamera.Name = "pCamera";
            this.pCamera.Size = new System.Drawing.Size(992, 484);
            this.pCamera.TabIndex = 60;
            // 
            // pbCamera
            // 
            this.pbCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCamera.ContextMenuStrip = this.cmsMarker;
            this.pbCamera.Location = new System.Drawing.Point(345, 0);
            this.pbCamera.Margin = new System.Windows.Forms.Padding(4);
            this.pbCamera.Name = "pbCamera";
            this.pbCamera.Padding = new System.Windows.Forms.Padding(4);
            this.pbCamera.Size = new System.Drawing.Size(640, 480);
            this.pbCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCamera.TabIndex = 58;
            this.pbCamera.TabStop = false;
            this.pbCamera.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCamera_Paint);
            this.pbCamera.MouseLeave += new System.EventHandler(this.pbCamera_MouseLeave);
            this.pbCamera.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCamera_MouseMove);
            // 
            // cmsMarker
            // 
            this.cmsMarker.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEdit});
            this.cmsMarker.Name = "cmsSplitView";
            this.cmsMarker.Size = new System.Drawing.Size(125, 26);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(124, 22);
            this.tsmiEdit.Text = "标记编辑";
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // pConfig
            // 
            this.pConfig.Controls.Add(this.dgvMarker);
            this.pConfig.Controls.Add(this.gbTempParameter);
            this.pConfig.Controls.Add(this.gbTempAcq);
            this.pConfig.Dock = System.Windows.Forms.DockStyle.Left;
            this.pConfig.Location = new System.Drawing.Point(0, 0);
            this.pConfig.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pConfig.Name = "pConfig";
            this.pConfig.Size = new System.Drawing.Size(345, 484);
            this.pConfig.TabIndex = 59;
            // 
            // dgvMarker
            // 
            this.dgvMarker.AllowUserToAddRows = false;
            this.dgvMarker.AllowUserToDeleteRows = false;
            this.dgvMarker.AllowUserToResizeRows = false;
            this.dgvMarker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMarker.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMarker.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.dgvMarker.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMarker.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(212)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMarker.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMarker.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMarker.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.标记流名称,
            this.最高温,
            this.最低温,
            this.平均温});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMarker.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMarker.GridColor = System.Drawing.SystemColors.Control;
            this.dgvMarker.Location = new System.Drawing.Point(4, 348);
            this.dgvMarker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvMarker.MultiSelect = false;
            this.dgvMarker.Name = "dgvMarker";
            this.dgvMarker.RowHeadersVisible = false;
            this.dgvMarker.RowTemplate.Height = 27;
            this.dgvMarker.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMarker.Size = new System.Drawing.Size(335, 132);
            this.dgvMarker.TabIndex = 62;
            // 
            // 标记流名称
            // 
            this.标记流名称.FillWeight = 75F;
            this.标记流名称.HeaderText = "标记流名称";
            this.标记流名称.Name = "标记流名称";
            this.标记流名称.Width = 75;
            // 
            // 最高温
            // 
            this.最高温.FillWeight = 60F;
            this.最高温.HeaderText = "最高温";
            this.最高温.Name = "最高温";
            this.最高温.Width = 60;
            // 
            // 最低温
            // 
            this.最低温.FillWeight = 65F;
            this.最低温.HeaderText = "最低温";
            this.最低温.Name = "最低温";
            this.最低温.Width = 65;
            // 
            // 平均温
            // 
            this.平均温.FillWeight = 70F;
            this.平均温.HeaderText = "平均温";
            this.平均温.Name = "平均温";
            this.平均温.Width = 65;
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
            this.gbTempParameter.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbTempParameter.Location = new System.Drawing.Point(4, 62);
            this.gbTempParameter.Margin = new System.Windows.Forms.Padding(4);
            this.gbTempParameter.Name = "gbTempParameter";
            this.gbTempParameter.Padding = new System.Windows.Forms.Padding(4);
            this.gbTempParameter.Size = new System.Drawing.Size(335, 280);
            this.gbTempParameter.TabIndex = 61;
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
            this.nudEmissivityValue.ValueChanged += new System.EventHandler(this.nudEmissivityValue_ValueChanged);
            // 
            // lblOffsetRange
            // 
            this.lblOffsetRange.AutoSize = true;
            this.lblOffsetRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.lblDistanceRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.nudRelativeHumidityValue.ValueChanged += new System.EventHandler(this.nudRelativeHumidityValue_ValueChanged);
            // 
            // lblLensTransRange
            // 
            this.lblLensTransRange.AutoSize = true;
            this.lblLensTransRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.lblAtmosphericTemperatureRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.nudLensTValue.ValueChanged += new System.EventHandler(this.nudLensTValue_ValueChanged);
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
            this.nudReflectedTemperatureValue.ValueChanged += new System.EventHandler(this.nudReflectedTemperatureValue_ValueChanged);
            // 
            // lblLensTRange
            // 
            this.lblLensTRange.AutoSize = true;
            this.lblLensTRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.lblReflectedTemperatureRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.nudLensTransValue.ValueChanged += new System.EventHandler(this.nudLensTrans_ValueChanged);
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
            this.nudAtmosphericTemperatureValue.ValueChanged += new System.EventHandler(this.nudAtmosphericTemperatureValue_ValueChanged);
            // 
            // lblRelativeHumidityRange
            // 
            this.lblRelativeHumidityRange.AutoSize = true;
            this.lblRelativeHumidityRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.nudOffsetValue.ValueChanged += new System.EventHandler(this.nudOffset_ValueChanged);
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
            this.nudDistanceValue.ValueChanged += new System.EventHandler(this.nudDistanceValue_ValueChanged);
            // 
            // lblEmissivityRange
            // 
            this.lblEmissivityRange.AutoSize = true;
            this.lblEmissivityRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            // gbTempAcq
            // 
            this.gbTempAcq.Controls.Add(this.lblCurrentTemp);
            this.gbTempAcq.Controls.Add(this.lblCurrentLoc);
            this.gbTempAcq.Controls.Add(this.lblCurrent);
            this.gbTempAcq.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbTempAcq.Location = new System.Drawing.Point(4, 8);
            this.gbTempAcq.Margin = new System.Windows.Forms.Padding(4);
            this.gbTempAcq.Name = "gbTempAcq";
            this.gbTempAcq.Padding = new System.Windows.Forms.Padding(4);
            this.gbTempAcq.Size = new System.Drawing.Size(335, 46);
            this.gbTempAcq.TabIndex = 60;
            this.gbTempAcq.TabStop = false;
            this.gbTempAcq.Text = "温度采集";
            // 
            // lblCurrentTemp
            // 
            this.lblCurrentTemp.AutoSize = true;
            this.lblCurrentTemp.Location = new System.Drawing.Point(251, 21);
            this.lblCurrentTemp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentTemp.Name = "lblCurrentTemp";
            this.lblCurrentTemp.Size = new System.Drawing.Size(41, 12);
            this.lblCurrentTemp.TabIndex = 8;
            this.lblCurrentTemp.Text = "0.0 ℃";
            // 
            // lblCurrentLoc
            // 
            this.lblCurrentLoc.AutoSize = true;
            this.lblCurrentLoc.Location = new System.Drawing.Point(153, 21);
            this.lblCurrentLoc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentLoc.Name = "lblCurrentLoc";
            this.lblCurrentLoc.Size = new System.Drawing.Size(35, 12);
            this.lblCurrentLoc.TabIndex = 8;
            this.lblCurrentLoc.Text = "(0,0)";
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(40, 21);
            this.lblCurrent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(41, 12);
            this.lblCurrent.TabIndex = 8;
            this.lblCurrent.Text = "当前点";
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
            this.ClientSize = new System.Drawing.Size(992, 531);
            this.Controls.Add(this.pMainForm);
            this.Controls.Add(this.ssMainForm);
            this.Controls.Add(this.msMainForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "OWBMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "温度采集";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OWBMainForm_FormClosed);
            this.Shown += new System.EventHandler(this.OWBMainForm_Shown);
            this.SizeChanged += new System.EventHandler(this.OWBMainForm_SizeChanged);
            this.msMainForm.ResumeLayout(false);
            this.msMainForm.PerformLayout();
            this.ssMainForm.ResumeLayout(false);
            this.ssMainForm.PerformLayout();
            this.pMainForm.ResumeLayout(false);
            this.pCamera.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).EndInit();
            this.cmsMarker.ResumeLayout(false);
            this.pConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarker)).EndInit();
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
            this.gbTempAcq.ResumeLayout(false);
            this.gbTempAcq.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMainForm;
        private System.Windows.Forms.ToolStripMenuItem tsmiConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.StatusStrip ssMainForm;
        private System.Windows.Forms.ToolStripStatusLabel tsslDeviceStatus;
        private System.Windows.Forms.Panel pMainForm;
        private System.Windows.Forms.Panel pCamera;
        private System.Windows.Forms.PictureBox pbCamera;
        private System.Windows.Forms.Panel pConfig;
        private System.Windows.Forms.GroupBox gbTempAcq;
        private System.Windows.Forms.Label lblCurrentTemp;
        private System.Windows.Forms.Label lblCurrentLoc;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.DataGridView dgvMarker;
        private System.Windows.Forms.GroupBox gbTempParameter;
        private System.Windows.Forms.NumericUpDown nudEmissivityValue;
        private System.Windows.Forms.Label lblDistanceRange;
        private System.Windows.Forms.NumericUpDown nudRelativeHumidityValue;
        private System.Windows.Forms.Label lblAtmosphericTemperatureRange;
        private System.Windows.Forms.NumericUpDown nudReflectedTemperatureValue;
        private System.Windows.Forms.Label lblReflectedTemperatureRange;
        private System.Windows.Forms.NumericUpDown nudAtmosphericTemperatureValue;
        private System.Windows.Forms.Label lblRelativeHumidityRange;
        private System.Windows.Forms.NumericUpDown nudDistanceValue;
        private System.Windows.Forms.Label lblEmissivityRange;
        private System.Windows.Forms.Label lblEmissivity;
        private System.Windows.Forms.Label lblRelativeHumidity;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label lblReflectedTemperature;
        private System.Windows.Forms.Label lblAtmosphericTemperature;
        private System.Windows.Forms.Label lblOffsetRange;
        private System.Windows.Forms.Label lblLensTransRange;
        private System.Windows.Forms.NumericUpDown nudLensTValue;
        private System.Windows.Forms.Label lblLensTRange;
        private System.Windows.Forms.NumericUpDown nudLensTransValue;
        private System.Windows.Forms.NumericUpDown nudOffsetValue;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.Label lblLensT;
        private System.Windows.Forms.Label lblLensTrans;
        private System.Windows.Forms.Timer tMainForm;
        private System.Windows.Forms.ContextMenuStrip cmsMarker;
        private System.Windows.Forms.ToolStripMenuItem tsmiSerialSet;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn 标记流名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 最高温;
        private System.Windows.Forms.DataGridViewTextBoxColumn 最低温;
        private System.Windows.Forms.DataGridViewTextBoxColumn 平均温;
    }
}

