namespace OWB.TADemo
{
    partial class OWBISPForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OWBISPForm));
            this.pIsp = new System.Windows.Forms.Panel();
            this.tbLayer = new System.Windows.Forms.TextBox();
            this.tbAlarm = new System.Windows.Forms.TextBox();
            this.cbAlarm = new System.Windows.Forms.CheckBox();
            this.cbEmissivityVisible = new System.Windows.Forms.CheckBox();
            this.cbDistanceVisible = new System.Windows.Forms.CheckBox();
            this.cbAPI = new System.Windows.Forms.CheckBox();
            this.cbOSD = new System.Windows.Forms.CheckBox();
            this.cbModbus = new System.Windows.Forms.CheckBox();
            this.cbMarkerName = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvISP = new System.Windows.Forms.DataGridView();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblLayer = new System.Windows.Forms.Label();
            this.lblMbRange = new System.Windows.Forms.Label();
            this.lblAlarm = new System.Windows.Forms.Label();
            this.lblAPI = new System.Windows.Forms.Label();
            this.lblOSD = new System.Windows.Forms.Label();
            this.nudMbSlot = new System.Windows.Forms.NumericUpDown();
            this.lblMbSlot = new System.Windows.Forms.Label();
            this.nudEmissivityValue = new System.Windows.Forms.NumericUpDown();
            this.lblOffsetRange = new System.Windows.Forms.Label();
            this.lblDistanceRange = new System.Windows.Forms.Label();
            this.nudReflectedTemperatureValue = new System.Windows.Forms.NumericUpDown();
            this.lblReflectedTemperatureRange = new System.Windows.Forms.Label();
            this.nudOffsetValue = new System.Windows.Forms.NumericUpDown();
            this.nudDistanceValue = new System.Windows.Forms.NumericUpDown();
            this.lblEmissivityRange = new System.Windows.Forms.Label();
            this.lblEmissivity = new System.Windows.Forms.Label();
            this.lblOffset = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.lblReflectedTemperature = new System.Windows.Forms.Label();
            this.lblMarkerName = new System.Windows.Forms.Label();
            this.tsISP = new System.Windows.Forms.ToolStrip();
            this.tsbArrow = new System.Windows.Forms.ToolStripButton();
            this.tsbPoint = new System.Windows.Forms.ToolStripButton();
            this.tsbPolygon = new System.Windows.Forms.ToolStripButton();
            this.tsbLine = new System.Windows.Forms.ToolStripButton();
            this.tsbMask = new System.Windows.Forms.ToolStripButton();
            this.cmsISP = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDel = new System.Windows.Forms.ToolStripMenuItem();
            this.tpbTA = new OWB.TADemo.OWBTAPictureBox(this.components);
            this.cbAvg = new System.Windows.Forms.CheckBox();
            this.cbMin = new System.Windows.Forms.CheckBox();
            this.cbMax = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pIsp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvISP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMbSlot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEmissivityValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReflectedTemperatureValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistanceValue)).BeginInit();
            this.tsISP.SuspendLayout();
            this.cmsISP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpbTA)).BeginInit();
            this.SuspendLayout();
            // 
            // pIsp
            // 
            this.pIsp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pIsp.Controls.Add(this.cbAvg);
            this.pIsp.Controls.Add(this.cbMin);
            this.pIsp.Controls.Add(this.cbMax);
            this.pIsp.Controls.Add(this.label3);
            this.pIsp.Controls.Add(this.label2);
            this.pIsp.Controls.Add(this.label1);
            this.pIsp.Controls.Add(this.tbLayer);
            this.pIsp.Controls.Add(this.tbAlarm);
            this.pIsp.Controls.Add(this.cbAlarm);
            this.pIsp.Controls.Add(this.cbEmissivityVisible);
            this.pIsp.Controls.Add(this.cbDistanceVisible);
            this.pIsp.Controls.Add(this.cbAPI);
            this.pIsp.Controls.Add(this.cbOSD);
            this.pIsp.Controls.Add(this.cbModbus);
            this.pIsp.Controls.Add(this.cbMarkerName);
            this.pIsp.Controls.Add(this.btnSave);
            this.pIsp.Controls.Add(this.dgvISP);
            this.pIsp.Controls.Add(this.lblLayer);
            this.pIsp.Controls.Add(this.lblMbRange);
            this.pIsp.Controls.Add(this.lblAlarm);
            this.pIsp.Controls.Add(this.lblAPI);
            this.pIsp.Controls.Add(this.lblOSD);
            this.pIsp.Controls.Add(this.nudMbSlot);
            this.pIsp.Controls.Add(this.lblMbSlot);
            this.pIsp.Controls.Add(this.nudEmissivityValue);
            this.pIsp.Controls.Add(this.lblOffsetRange);
            this.pIsp.Controls.Add(this.lblDistanceRange);
            this.pIsp.Controls.Add(this.nudReflectedTemperatureValue);
            this.pIsp.Controls.Add(this.lblReflectedTemperatureRange);
            this.pIsp.Controls.Add(this.nudOffsetValue);
            this.pIsp.Controls.Add(this.nudDistanceValue);
            this.pIsp.Controls.Add(this.lblEmissivityRange);
            this.pIsp.Controls.Add(this.lblEmissivity);
            this.pIsp.Controls.Add(this.lblOffset);
            this.pIsp.Controls.Add(this.lblDistance);
            this.pIsp.Controls.Add(this.lblReflectedTemperature);
            this.pIsp.Controls.Add(this.lblMarkerName);
            this.pIsp.Dock = System.Windows.Forms.DockStyle.Right;
            this.pIsp.Location = new System.Drawing.Point(640, 0);
            this.pIsp.Name = "pIsp";
            this.pIsp.Size = new System.Drawing.Size(280, 505);
            this.pIsp.TabIndex = 1;
            // 
            // tbLayer
            // 
            this.tbLayer.Location = new System.Drawing.Point(197, 325);
            this.tbLayer.Name = "tbLayer";
            this.tbLayer.Size = new System.Drawing.Size(73, 21);
            this.tbLayer.TabIndex = 110;
            this.tbLayer.Text = "0";
            // 
            // tbAlarm
            // 
            this.tbAlarm.Location = new System.Drawing.Point(197, 289);
            this.tbAlarm.Name = "tbAlarm";
            this.tbAlarm.Size = new System.Drawing.Size(73, 21);
            this.tbAlarm.TabIndex = 110;
            // 
            // cbAlarm
            // 
            this.cbAlarm.AutoSize = true;
            this.cbAlarm.Enabled = false;
            this.cbAlarm.Location = new System.Drawing.Point(5, 292);
            this.cbAlarm.Name = "cbAlarm";
            this.cbAlarm.Size = new System.Drawing.Size(15, 14);
            this.cbAlarm.TabIndex = 109;
            this.cbAlarm.UseVisualStyleBackColor = true;
            // 
            // cbEmissivityVisible
            // 
            this.cbEmissivityVisible.AutoSize = true;
            this.cbEmissivityVisible.Location = new System.Drawing.Point(5, 47);
            this.cbEmissivityVisible.Name = "cbEmissivityVisible";
            this.cbEmissivityVisible.Size = new System.Drawing.Size(15, 14);
            this.cbEmissivityVisible.TabIndex = 109;
            this.cbEmissivityVisible.UseVisualStyleBackColor = true;
            // 
            // cbDistanceVisible
            // 
            this.cbDistanceVisible.AutoSize = true;
            this.cbDistanceVisible.Location = new System.Drawing.Point(5, 117);
            this.cbDistanceVisible.Name = "cbDistanceVisible";
            this.cbDistanceVisible.Size = new System.Drawing.Size(15, 14);
            this.cbDistanceVisible.TabIndex = 109;
            this.cbDistanceVisible.UseVisualStyleBackColor = true;
            // 
            // cbAPI
            // 
            this.cbAPI.AutoSize = true;
            this.cbAPI.Location = new System.Drawing.Point(5, 257);
            this.cbAPI.Name = "cbAPI";
            this.cbAPI.Size = new System.Drawing.Size(15, 14);
            this.cbAPI.TabIndex = 109;
            this.cbAPI.UseVisualStyleBackColor = true;
            // 
            // cbOSD
            // 
            this.cbOSD.AutoSize = true;
            this.cbOSD.Location = new System.Drawing.Point(5, 222);
            this.cbOSD.Name = "cbOSD";
            this.cbOSD.Size = new System.Drawing.Size(15, 14);
            this.cbOSD.TabIndex = 109;
            this.cbOSD.UseVisualStyleBackColor = true;
            // 
            // cbModbus
            // 
            this.cbModbus.AutoSize = true;
            this.cbModbus.Location = new System.Drawing.Point(5, 187);
            this.cbModbus.Name = "cbModbus";
            this.cbModbus.Size = new System.Drawing.Size(15, 14);
            this.cbModbus.TabIndex = 109;
            this.cbModbus.UseVisualStyleBackColor = true;
            // 
            // cbMarkerName
            // 
            this.cbMarkerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMarkerName.FormattingEnabled = true;
            this.cbMarkerName.Location = new System.Drawing.Point(149, 9);
            this.cbMarkerName.Name = "cbMarkerName";
            this.cbMarkerName.Size = new System.Drawing.Size(121, 20);
            this.cbMarkerName.TabIndex = 108;
            this.cbMarkerName.SelectedIndexChanged += new System.EventHandler(this.cbMarkerName_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnSave.Location = new System.Drawing.Point(195, 476);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 107;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvISP
            // 
            this.dgvISP.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvISP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvISP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvISP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.X,
            this.Y});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvISP.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvISP.Location = new System.Drawing.Point(5, 352);
            this.dgvISP.Name = "dgvISP";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvISP.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvISP.RowHeadersVisible = false;
            this.dgvISP.RowTemplate.Height = 27;
            this.dgvISP.Size = new System.Drawing.Size(265, 117);
            this.dgvISP.TabIndex = 106;
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblLayer
            // 
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new System.Drawing.Point(29, 329);
            this.lblLayer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(53, 12);
            this.lblLayer.TabIndex = 103;
            this.lblLayer.Text = "区域层级";
            // 
            // lblMbRange
            // 
            this.lblMbRange.AutoSize = true;
            this.lblMbRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMbRange.Location = new System.Drawing.Point(131, 188);
            this.lblMbRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMbRange.Name = "lblMbRange";
            this.lblMbRange.Size = new System.Drawing.Size(41, 12);
            this.lblMbRange.TabIndex = 104;
            this.lblMbRange.Text = "(0~63)";
            // 
            // lblAlarm
            // 
            this.lblAlarm.AutoSize = true;
            this.lblAlarm.Location = new System.Drawing.Point(29, 293);
            this.lblAlarm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAlarm.Name = "lblAlarm";
            this.lblAlarm.Size = new System.Drawing.Size(77, 12);
            this.lblAlarm.TabIndex = 103;
            this.lblAlarm.Text = "温度报警阈值";
            // 
            // lblAPI
            // 
            this.lblAPI.AutoSize = true;
            this.lblAPI.Location = new System.Drawing.Point(29, 258);
            this.lblAPI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAPI.Name = "lblAPI";
            this.lblAPI.Size = new System.Drawing.Size(65, 12);
            this.lblAPI.TabIndex = 103;
            this.lblAPI.Text = "标记流输出";
            // 
            // lblOSD
            // 
            this.lblOSD.AutoSize = true;
            this.lblOSD.Location = new System.Drawing.Point(29, 223);
            this.lblOSD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOSD.Name = "lblOSD";
            this.lblOSD.Size = new System.Drawing.Size(47, 12);
            this.lblOSD.TabIndex = 103;
            this.lblOSD.Text = "OSD显示";
            // 
            // nudMbSlot
            // 
            this.nudMbSlot.Location = new System.Drawing.Point(197, 184);
            this.nudMbSlot.Margin = new System.Windows.Forms.Padding(4);
            this.nudMbSlot.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.nudMbSlot.Name = "nudMbSlot";
            this.nudMbSlot.Size = new System.Drawing.Size(73, 21);
            this.nudMbSlot.TabIndex = 105;
            // 
            // lblMbSlot
            // 
            this.lblMbSlot.AutoSize = true;
            this.lblMbSlot.Location = new System.Drawing.Point(29, 188);
            this.lblMbSlot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMbSlot.Name = "lblMbSlot";
            this.lblMbSlot.Size = new System.Drawing.Size(77, 12);
            this.lblMbSlot.TabIndex = 103;
            this.lblMbSlot.Text = "Modbus数据块";
            // 
            // nudEmissivityValue
            // 
            this.nudEmissivityValue.DecimalPlaces = 2;
            this.nudEmissivityValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudEmissivityValue.Location = new System.Drawing.Point(197, 44);
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
            this.nudEmissivityValue.TabIndex = 99;
            this.nudEmissivityValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            // 
            // lblOffsetRange
            // 
            this.lblOffsetRange.AutoSize = true;
            this.lblOffsetRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOffsetRange.Location = new System.Drawing.Point(107, 153);
            this.lblOffsetRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOffsetRange.Name = "lblOffsetRange";
            this.lblOffsetRange.Size = new System.Drawing.Size(65, 12);
            this.lblOffsetRange.TabIndex = 96;
            this.lblOffsetRange.Text = "(-200~200)";
            // 
            // lblDistanceRange
            // 
            this.lblDistanceRange.AutoSize = true;
            this.lblDistanceRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDistanceRange.Location = new System.Drawing.Point(95, 118);
            this.lblDistanceRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDistanceRange.Name = "lblDistanceRange";
            this.lblDistanceRange.Size = new System.Drawing.Size(77, 12);
            this.lblDistanceRange.TabIndex = 97;
            this.lblDistanceRange.Text = "(0.1~1000.0)";
            // 
            // nudReflectedTemperatureValue
            // 
            this.nudReflectedTemperatureValue.DecimalPlaces = 1;
            this.nudReflectedTemperatureValue.Location = new System.Drawing.Point(197, 79);
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
            this.nudReflectedTemperatureValue.TabIndex = 102;
            this.nudReflectedTemperatureValue.Value = new decimal(new int[] {
            200,
            0,
            0,
            65536});
            // 
            // lblReflectedTemperatureRange
            // 
            this.lblReflectedTemperatureRange.AutoSize = true;
            this.lblReflectedTemperatureRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReflectedTemperatureRange.Location = new System.Drawing.Point(107, 83);
            this.lblReflectedTemperatureRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReflectedTemperatureRange.Name = "lblReflectedTemperatureRange";
            this.lblReflectedTemperatureRange.Size = new System.Drawing.Size(65, 12);
            this.lblReflectedTemperatureRange.TabIndex = 95;
            this.lblReflectedTemperatureRange.Text = "(-200~200)";
            // 
            // nudOffsetValue
            // 
            this.nudOffsetValue.DecimalPlaces = 1;
            this.nudOffsetValue.Location = new System.Drawing.Point(197, 149);
            this.nudOffsetValue.Margin = new System.Windows.Forms.Padding(4);
            this.nudOffsetValue.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            65536});
            this.nudOffsetValue.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147418112});
            this.nudOffsetValue.Name = "nudOffsetValue";
            this.nudOffsetValue.Size = new System.Drawing.Size(73, 21);
            this.nudOffsetValue.TabIndex = 100;
            // 
            // nudDistanceValue
            // 
            this.nudDistanceValue.DecimalPlaces = 1;
            this.nudDistanceValue.Location = new System.Drawing.Point(197, 114);
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
            this.nudDistanceValue.TabIndex = 101;
            this.nudDistanceValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblEmissivityRange
            // 
            this.lblEmissivityRange.AutoSize = true;
            this.lblEmissivityRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmissivityRange.Location = new System.Drawing.Point(101, 48);
            this.lblEmissivityRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmissivityRange.Name = "lblEmissivityRange";
            this.lblEmissivityRange.Size = new System.Drawing.Size(71, 12);
            this.lblEmissivityRange.TabIndex = 98;
            this.lblEmissivityRange.Text = "(0.01~1.00)";
            // 
            // lblEmissivity
            // 
            this.lblEmissivity.AutoSize = true;
            this.lblEmissivity.Location = new System.Drawing.Point(29, 48);
            this.lblEmissivity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmissivity.Name = "lblEmissivity";
            this.lblEmissivity.Size = new System.Drawing.Size(41, 12);
            this.lblEmissivity.TabIndex = 93;
            this.lblEmissivity.Text = "发射率";
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Location = new System.Drawing.Point(29, 153);
            this.lblOffset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(29, 12);
            this.lblOffset.TabIndex = 92;
            this.lblOffset.Text = "偏移";
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(29, 118);
            this.lblDistance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(29, 12);
            this.lblDistance.TabIndex = 91;
            this.lblDistance.Text = "距离";
            // 
            // lblReflectedTemperature
            // 
            this.lblReflectedTemperature.AutoSize = true;
            this.lblReflectedTemperature.Location = new System.Drawing.Point(29, 83);
            this.lblReflectedTemperature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReflectedTemperature.Name = "lblReflectedTemperature";
            this.lblReflectedTemperature.Size = new System.Drawing.Size(53, 12);
            this.lblReflectedTemperature.TabIndex = 94;
            this.lblReflectedTemperature.Text = "反射温度";
            // 
            // lblMarkerName
            // 
            this.lblMarkerName.AutoSize = true;
            this.lblMarkerName.Font = new System.Drawing.Font("SimSun", 9F);
            this.lblMarkerName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMarkerName.Location = new System.Drawing.Point(29, 13);
            this.lblMarkerName.Name = "lblMarkerName";
            this.lblMarkerName.Size = new System.Drawing.Size(53, 12);
            this.lblMarkerName.TabIndex = 90;
            this.lblMarkerName.Text = "标记名称";
            // 
            // tsISP
            // 
            this.tsISP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(212)))), ((int)(((byte)(233)))));
            this.tsISP.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsISP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbArrow,
            this.tsbPoint,
            this.tsbPolygon,
            this.tsbLine,
            this.tsbMask});
            this.tsISP.Location = new System.Drawing.Point(0, 0);
            this.tsISP.Name = "tsISP";
            this.tsISP.Size = new System.Drawing.Size(640, 27);
            this.tsISP.TabIndex = 3;
            // 
            // tsbArrow
            // 
            this.tsbArrow.Checked = true;
            this.tsbArrow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbArrow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbArrow.Image = ((System.Drawing.Image)(resources.GetObject("tsbArrow.Image")));
            this.tsbArrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArrow.Name = "tsbArrow";
            this.tsbArrow.Size = new System.Drawing.Size(24, 24);
            this.tsbArrow.Text = "指针";
            this.tsbArrow.Click += new System.EventHandler(this.tsbArrow_Click);
            // 
            // tsbPoint
            // 
            this.tsbPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPoint.Image = ((System.Drawing.Image)(resources.GetObject("tsbPoint.Image")));
            this.tsbPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPoint.Name = "tsbPoint";
            this.tsbPoint.Size = new System.Drawing.Size(24, 24);
            this.tsbPoint.Text = "点";
            this.tsbPoint.Click += new System.EventHandler(this.tsbPoint_Click);
            // 
            // tsbPolygon
            // 
            this.tsbPolygon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPolygon.Image = ((System.Drawing.Image)(resources.GetObject("tsbPolygon.Image")));
            this.tsbPolygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPolygon.Name = "tsbPolygon";
            this.tsbPolygon.Size = new System.Drawing.Size(24, 24);
            this.tsbPolygon.Text = "区域";
            this.tsbPolygon.Click += new System.EventHandler(this.tsbPolygon_Click);
            // 
            // tsbLine
            // 
            this.tsbLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLine.Image = ((System.Drawing.Image)(resources.GetObject("tsbLine.Image")));
            this.tsbLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLine.Name = "tsbLine";
            this.tsbLine.Size = new System.Drawing.Size(24, 24);
            this.tsbLine.Text = "线";
            this.tsbLine.Click += new System.EventHandler(this.tsbLine_Click);
            // 
            // tsbMask
            // 
            this.tsbMask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMask.Image = ((System.Drawing.Image)(resources.GetObject("tsbMask.Image")));
            this.tsbMask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMask.Name = "tsbMask";
            this.tsbMask.Size = new System.Drawing.Size(24, 24);
            this.tsbMask.Text = "蒙版";
            this.tsbMask.Click += new System.EventHandler(this.tsbMask_Click);
            // 
            // cmsISP
            // 
            this.cmsISP.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsISP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDel});
            this.cmsISP.Name = "cmsISP";
            this.cmsISP.Size = new System.Drawing.Size(101, 26);
            this.cmsISP.Opening += new System.ComponentModel.CancelEventHandler(this.cmsISP_Opening);
            // 
            // tsmiDel
            // 
            this.tsmiDel.Name = "tsmiDel";
            this.tsmiDel.Size = new System.Drawing.Size(100, 22);
            this.tsmiDel.Text = "删除";
            // 
            // tpbTA
            // 
            this.tpbTA.ContextMenuStrip = this.cmsISP;
            this.tpbTA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpbTA.Draw = null;
            this.tpbTA.Location = new System.Drawing.Point(0, 27);
            this.tpbTA.Name = "tpbTA";
            this.tpbTA.Size = new System.Drawing.Size(640, 478);
            this.tpbTA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.tpbTA.TabIndex = 4;
            this.tpbTA.TabStop = false;
            this.tpbTA.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tpbTA_MouseDoubleClick);
            this.tpbTA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tpbTA_MouseDown);
            this.tpbTA.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tpbTA_MouseMove);
            // 
            // cbAvg
            // 
            this.cbAvg.AutoSize = true;
            this.cbAvg.Location = new System.Drawing.Point(208, 257);
            this.cbAvg.Name = "cbAvg";
            this.cbAvg.Size = new System.Drawing.Size(15, 14);
            this.cbAvg.TabIndex = 137;
            this.cbAvg.UseVisualStyleBackColor = true;
            // 
            // cbMin
            // 
            this.cbMin.AutoSize = true;
            this.cbMin.Location = new System.Drawing.Point(152, 257);
            this.cbMin.Name = "cbMin";
            this.cbMin.Size = new System.Drawing.Size(15, 14);
            this.cbMin.TabIndex = 138;
            this.cbMin.UseVisualStyleBackColor = true;
            // 
            // cbMax
            // 
            this.cbMax.AutoSize = true;
            this.cbMax.Location = new System.Drawing.Point(96, 257);
            this.cbMax.Name = "cbMax";
            this.cbMax.Size = new System.Drawing.Size(15, 14);
            this.cbMax.TabIndex = 139;
            this.cbMax.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 258);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 134;
            this.label3.Text = "平均温";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 258);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 135;
            this.label2.Text = "最低温";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 258);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 136;
            this.label1.Text = "最高温";
            // 
            // OWBISPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(920, 505);
            this.Controls.Add(this.tpbTA);
            this.Controls.Add(this.tsISP);
            this.Controls.Add(this.pIsp);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OWBISPForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标记编辑";
            this.pIsp.ResumeLayout(false);
            this.pIsp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvISP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMbSlot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEmissivityValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReflectedTemperatureValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistanceValue)).EndInit();
            this.tsISP.ResumeLayout(false);
            this.tsISP.PerformLayout();
            this.cmsISP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tpbTA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pIsp;
        private System.Windows.Forms.ToolStrip tsISP;
        private OWBTAPictureBox tpbTA;
        private System.Windows.Forms.ToolStripButton tsbArrow;
        private System.Windows.Forms.ToolStripButton tsbPoint;
        private System.Windows.Forms.ToolStripButton tsbPolygon;
        private System.Windows.Forms.ToolStripButton tsbLine;
        private System.Windows.Forms.ToolStripButton tsbMask;
        private System.Windows.Forms.Label lblMbRange;
        private System.Windows.Forms.NumericUpDown nudMbSlot;
        private System.Windows.Forms.Label lblMbSlot;
        private System.Windows.Forms.NumericUpDown nudEmissivityValue;
        private System.Windows.Forms.Label lblOffsetRange;
        private System.Windows.Forms.Label lblDistanceRange;
        private System.Windows.Forms.NumericUpDown nudReflectedTemperatureValue;
        private System.Windows.Forms.Label lblReflectedTemperatureRange;
        private System.Windows.Forms.NumericUpDown nudOffsetValue;
        private System.Windows.Forms.NumericUpDown nudDistanceValue;
        private System.Windows.Forms.Label lblEmissivityRange;
        private System.Windows.Forms.Label lblEmissivity;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label lblReflectedTemperature;
        private System.Windows.Forms.Label lblMarkerName;
        private System.Windows.Forms.DataGridView dgvISP;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbMarkerName;
        private System.Windows.Forms.TextBox tbAlarm;
        private System.Windows.Forms.CheckBox cbAlarm;
        private System.Windows.Forms.CheckBox cbAPI;
        private System.Windows.Forms.CheckBox cbOSD;
        private System.Windows.Forms.CheckBox cbModbus;
        private System.Windows.Forms.Label lblAlarm;
        private System.Windows.Forms.Label lblAPI;
        private System.Windows.Forms.Label lblOSD;
        private System.Windows.Forms.ContextMenuStrip cmsISP;
        private System.Windows.Forms.ToolStripMenuItem tsmiDel;
        private System.Windows.Forms.TextBox tbLayer;
        private System.Windows.Forms.Label lblLayer;
        private System.Windows.Forms.CheckBox cbEmissivityVisible;
        private System.Windows.Forms.CheckBox cbDistanceVisible;
        private System.Windows.Forms.CheckBox cbAvg;
        private System.Windows.Forms.CheckBox cbMin;
        private System.Windows.Forms.CheckBox cbMax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}