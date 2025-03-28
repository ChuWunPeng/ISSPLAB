namespace OWB.TADemo
{
    partial class OWBISPAddForm
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
            this.btnSave = new System.Windows.Forms.Button();
            this.tbAlarm = new System.Windows.Forms.TextBox();
            this.cbAlarm = new System.Windows.Forms.CheckBox();
            this.cbAPI = new System.Windows.Forms.CheckBox();
            this.cbOSD = new System.Windows.Forms.CheckBox();
            this.cbModbus = new System.Windows.Forms.CheckBox();
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
            this.tbMarkerName = new System.Windows.Forms.TextBox();
            this.tbLayer = new System.Windows.Forms.TextBox();
            this.lblLayer = new System.Windows.Forms.Label();
            this.ttISPAddForm = new System.Windows.Forms.ToolTip(this.components);
            this.cbEmissivityVisible = new System.Windows.Forms.CheckBox();
            this.cbDistanceVisible = new System.Windows.Forms.CheckBox();
            this.cbMax = new System.Windows.Forms.CheckBox();
            this.cbMin = new System.Windows.Forms.CheckBox();
            this.cbAvg = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudMbSlot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEmissivityValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReflectedTemperatureValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistanceValue)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnSave.Location = new System.Drawing.Point(205, 360);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 66;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbAlarm
            // 
            this.tbAlarm.Enabled = false;
            this.tbAlarm.Location = new System.Drawing.Point(157, 298);
            this.tbAlarm.Name = "tbAlarm";
            this.tbAlarm.Size = new System.Drawing.Size(121, 21);
            this.tbAlarm.TabIndex = 135;
            // 
            // cbAlarm
            // 
            this.cbAlarm.AutoSize = true;
            this.cbAlarm.Location = new System.Drawing.Point(13, 301);
            this.cbAlarm.Name = "cbAlarm";
            this.cbAlarm.Size = new System.Drawing.Size(15, 14);
            this.cbAlarm.TabIndex = 134;
            this.cbAlarm.UseVisualStyleBackColor = true;
            this.cbAlarm.CheckedChanged += new System.EventHandler(this.cbAlarm_CheckedChanged);
            // 
            // cbAPI
            // 
            this.cbAPI.AutoSize = true;
            this.cbAPI.Checked = true;
            this.cbAPI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAPI.Location = new System.Drawing.Point(13, 266);
            this.cbAPI.Name = "cbAPI";
            this.cbAPI.Size = new System.Drawing.Size(15, 14);
            this.cbAPI.TabIndex = 133;
            this.cbAPI.UseVisualStyleBackColor = true;
            // 
            // cbOSD
            // 
            this.cbOSD.AutoSize = true;
            this.cbOSD.Checked = true;
            this.cbOSD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOSD.Location = new System.Drawing.Point(13, 231);
            this.cbOSD.Name = "cbOSD";
            this.cbOSD.Size = new System.Drawing.Size(15, 14);
            this.cbOSD.TabIndex = 132;
            this.cbOSD.UseVisualStyleBackColor = true;
            // 
            // cbModbus
            // 
            this.cbModbus.AutoSize = true;
            this.cbModbus.Location = new System.Drawing.Point(13, 196);
            this.cbModbus.Name = "cbModbus";
            this.cbModbus.Size = new System.Drawing.Size(15, 14);
            this.cbModbus.TabIndex = 131;
            this.cbModbus.UseVisualStyleBackColor = true;
            this.cbModbus.CheckedChanged += new System.EventHandler(this.cbModbus_CheckedChanged);
            // 
            // lblMbRange
            // 
            this.lblMbRange.AutoSize = true;
            this.lblMbRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMbRange.Location = new System.Drawing.Point(139, 197);
            this.lblMbRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMbRange.Name = "lblMbRange";
            this.lblMbRange.Size = new System.Drawing.Size(41, 12);
            this.lblMbRange.TabIndex = 128;
            this.lblMbRange.Text = "(0~63)";
            // 
            // lblAlarm
            // 
            this.lblAlarm.AutoSize = true;
            this.lblAlarm.Location = new System.Drawing.Point(37, 302);
            this.lblAlarm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAlarm.Name = "lblAlarm";
            this.lblAlarm.Size = new System.Drawing.Size(77, 12);
            this.lblAlarm.TabIndex = 126;
            this.lblAlarm.Text = "温度报警阈值";
            // 
            // lblAPI
            // 
            this.lblAPI.AutoSize = true;
            this.lblAPI.Location = new System.Drawing.Point(37, 267);
            this.lblAPI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAPI.Name = "lblAPI";
            this.lblAPI.Size = new System.Drawing.Size(65, 12);
            this.lblAPI.TabIndex = 125;
            this.lblAPI.Text = "标记流输出";
            // 
            // lblOSD
            // 
            this.lblOSD.AutoSize = true;
            this.lblOSD.Location = new System.Drawing.Point(37, 232);
            this.lblOSD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOSD.Name = "lblOSD";
            this.lblOSD.Size = new System.Drawing.Size(47, 12);
            this.lblOSD.TabIndex = 127;
            this.lblOSD.Text = "OSD显示";
            // 
            // nudMbSlot
            // 
            this.nudMbSlot.Enabled = false;
            this.nudMbSlot.Location = new System.Drawing.Point(205, 193);
            this.nudMbSlot.Margin = new System.Windows.Forms.Padding(4);
            this.nudMbSlot.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.nudMbSlot.Name = "nudMbSlot";
            this.nudMbSlot.Size = new System.Drawing.Size(73, 21);
            this.nudMbSlot.TabIndex = 129;
            // 
            // lblMbSlot
            // 
            this.lblMbSlot.AutoSize = true;
            this.lblMbSlot.Location = new System.Drawing.Point(37, 197);
            this.lblMbSlot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMbSlot.Name = "lblMbSlot";
            this.lblMbSlot.Size = new System.Drawing.Size(77, 12);
            this.lblMbSlot.TabIndex = 124;
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
            this.nudEmissivityValue.Location = new System.Drawing.Point(205, 53);
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
            this.nudEmissivityValue.TabIndex = 120;
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
            this.lblOffsetRange.Location = new System.Drawing.Point(115, 162);
            this.lblOffsetRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOffsetRange.Name = "lblOffsetRange";
            this.lblOffsetRange.Size = new System.Drawing.Size(65, 12);
            this.lblOffsetRange.TabIndex = 117;
            this.lblOffsetRange.Text = "(-200~200)";
            // 
            // lblDistanceRange
            // 
            this.lblDistanceRange.AutoSize = true;
            this.lblDistanceRange.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDistanceRange.Location = new System.Drawing.Point(103, 127);
            this.lblDistanceRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDistanceRange.Name = "lblDistanceRange";
            this.lblDistanceRange.Size = new System.Drawing.Size(77, 12);
            this.lblDistanceRange.TabIndex = 118;
            this.lblDistanceRange.Text = "(0.1~1000.0)";
            // 
            // nudReflectedTemperatureValue
            // 
            this.nudReflectedTemperatureValue.DecimalPlaces = 1;
            this.nudReflectedTemperatureValue.Location = new System.Drawing.Point(205, 88);
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
            this.nudReflectedTemperatureValue.TabIndex = 123;
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
            this.lblReflectedTemperatureRange.Location = new System.Drawing.Point(115, 92);
            this.lblReflectedTemperatureRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReflectedTemperatureRange.Name = "lblReflectedTemperatureRange";
            this.lblReflectedTemperatureRange.Size = new System.Drawing.Size(65, 12);
            this.lblReflectedTemperatureRange.TabIndex = 116;
            this.lblReflectedTemperatureRange.Text = "(-200~200)";
            // 
            // nudOffsetValue
            // 
            this.nudOffsetValue.DecimalPlaces = 1;
            this.nudOffsetValue.Location = new System.Drawing.Point(205, 158);
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
            this.nudOffsetValue.TabIndex = 121;
            // 
            // nudDistanceValue
            // 
            this.nudDistanceValue.DecimalPlaces = 1;
            this.nudDistanceValue.Location = new System.Drawing.Point(205, 123);
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
            this.nudDistanceValue.TabIndex = 122;
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
            this.lblEmissivityRange.Location = new System.Drawing.Point(109, 57);
            this.lblEmissivityRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmissivityRange.Name = "lblEmissivityRange";
            this.lblEmissivityRange.Size = new System.Drawing.Size(71, 12);
            this.lblEmissivityRange.TabIndex = 119;
            this.lblEmissivityRange.Text = "(0.01~1.00)";
            // 
            // lblEmissivity
            // 
            this.lblEmissivity.AutoSize = true;
            this.lblEmissivity.Location = new System.Drawing.Point(37, 57);
            this.lblEmissivity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmissivity.Name = "lblEmissivity";
            this.lblEmissivity.Size = new System.Drawing.Size(41, 12);
            this.lblEmissivity.TabIndex = 114;
            this.lblEmissivity.Text = "发射率";
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Location = new System.Drawing.Point(37, 162);
            this.lblOffset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(29, 12);
            this.lblOffset.TabIndex = 113;
            this.lblOffset.Text = "偏移";
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(37, 127);
            this.lblDistance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(29, 12);
            this.lblDistance.TabIndex = 112;
            this.lblDistance.Text = "距离";
            // 
            // lblReflectedTemperature
            // 
            this.lblReflectedTemperature.AutoSize = true;
            this.lblReflectedTemperature.Location = new System.Drawing.Point(37, 92);
            this.lblReflectedTemperature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReflectedTemperature.Name = "lblReflectedTemperature";
            this.lblReflectedTemperature.Size = new System.Drawing.Size(53, 12);
            this.lblReflectedTemperature.TabIndex = 115;
            this.lblReflectedTemperature.Text = "反射温度";
            // 
            // lblMarkerName
            // 
            this.lblMarkerName.AutoSize = true;
            this.lblMarkerName.Font = new System.Drawing.Font("SimSun", 9F);
            this.lblMarkerName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMarkerName.Location = new System.Drawing.Point(37, 22);
            this.lblMarkerName.Name = "lblMarkerName";
            this.lblMarkerName.Size = new System.Drawing.Size(53, 12);
            this.lblMarkerName.TabIndex = 111;
            this.lblMarkerName.Text = "标记名称";
            // 
            // tbMarkerName
            // 
            this.tbMarkerName.Location = new System.Drawing.Point(157, 18);
            this.tbMarkerName.Name = "tbMarkerName";
            this.tbMarkerName.Size = new System.Drawing.Size(121, 21);
            this.tbMarkerName.TabIndex = 136;
            // 
            // tbLayer
            // 
            this.tbLayer.Location = new System.Drawing.Point(205, 332);
            this.tbLayer.Name = "tbLayer";
            this.tbLayer.Size = new System.Drawing.Size(73, 21);
            this.tbLayer.TabIndex = 138;
            this.tbLayer.Text = "0";
            // 
            // lblLayer
            // 
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new System.Drawing.Point(37, 336);
            this.lblLayer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(53, 12);
            this.lblLayer.TabIndex = 137;
            this.lblLayer.Text = "区域层级";
            // 
            // cbEmissivityVisible
            // 
            this.cbEmissivityVisible.AutoSize = true;
            this.cbEmissivityVisible.Checked = true;
            this.cbEmissivityVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEmissivityVisible.Location = new System.Drawing.Point(13, 56);
            this.cbEmissivityVisible.Name = "cbEmissivityVisible";
            this.cbEmissivityVisible.Size = new System.Drawing.Size(15, 14);
            this.cbEmissivityVisible.TabIndex = 133;
            this.cbEmissivityVisible.UseVisualStyleBackColor = true;
            this.cbEmissivityVisible.CheckedChanged += new System.EventHandler(this.cbEmissivityVisible_CheckedChanged);
            // 
            // cbDistanceVisible
            // 
            this.cbDistanceVisible.AutoSize = true;
            this.cbDistanceVisible.Checked = true;
            this.cbDistanceVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDistanceVisible.Location = new System.Drawing.Point(13, 126);
            this.cbDistanceVisible.Name = "cbDistanceVisible";
            this.cbDistanceVisible.Size = new System.Drawing.Size(15, 14);
            this.cbDistanceVisible.TabIndex = 133;
            this.cbDistanceVisible.UseVisualStyleBackColor = true;
            this.cbDistanceVisible.CheckedChanged += new System.EventHandler(this.cbDistanceVisible_CheckedChanged);
            // 
            // cbMax
            // 
            this.cbMax.AutoSize = true;
            this.cbMax.Checked = true;
            this.cbMax.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMax.Location = new System.Drawing.Point(104, 266);
            this.cbMax.Name = "cbMax";
            this.cbMax.Size = new System.Drawing.Size(15, 14);
            this.cbMax.TabIndex = 133;
            this.cbMax.UseVisualStyleBackColor = true;
            // 
            // cbMin
            // 
            this.cbMin.AutoSize = true;
            this.cbMin.Checked = true;
            this.cbMin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMin.Location = new System.Drawing.Point(168, 266);
            this.cbMin.Name = "cbMin";
            this.cbMin.Size = new System.Drawing.Size(15, 14);
            this.cbMin.TabIndex = 133;
            this.cbMin.UseVisualStyleBackColor = true;
            // 
            // cbAvg
            // 
            this.cbAvg.AutoSize = true;
            this.cbAvg.Checked = true;
            this.cbAvg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAvg.Location = new System.Drawing.Point(232, 266);
            this.cbAvg.Name = "cbAvg";
            this.cbAvg.Size = new System.Drawing.Size(15, 14);
            this.cbAvg.TabIndex = 133;
            this.cbAvg.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 267);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 127;
            this.label1.Text = "最高温";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 267);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 127;
            this.label2.Text = "最低温";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 267);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 127;
            this.label3.Text = "平均温";
            // 
            // OWBISPAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(298, 395);
            this.Controls.Add(this.tbLayer);
            this.Controls.Add(this.lblLayer);
            this.Controls.Add(this.tbMarkerName);
            this.Controls.Add(this.tbAlarm);
            this.Controls.Add(this.cbAlarm);
            this.Controls.Add(this.cbDistanceVisible);
            this.Controls.Add(this.cbEmissivityVisible);
            this.Controls.Add(this.cbAvg);
            this.Controls.Add(this.cbMin);
            this.Controls.Add(this.cbMax);
            this.Controls.Add(this.cbAPI);
            this.Controls.Add(this.cbOSD);
            this.Controls.Add(this.cbModbus);
            this.Controls.Add(this.lblMbRange);
            this.Controls.Add(this.lblAlarm);
            this.Controls.Add(this.lblAPI);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblOSD);
            this.Controls.Add(this.nudMbSlot);
            this.Controls.Add(this.lblMbSlot);
            this.Controls.Add(this.nudEmissivityValue);
            this.Controls.Add(this.lblOffsetRange);
            this.Controls.Add(this.lblDistanceRange);
            this.Controls.Add(this.nudReflectedTemperatureValue);
            this.Controls.Add(this.lblReflectedTemperatureRange);
            this.Controls.Add(this.nudOffsetValue);
            this.Controls.Add(this.nudDistanceValue);
            this.Controls.Add(this.lblEmissivityRange);
            this.Controls.Add(this.lblEmissivity);
            this.Controls.Add(this.lblOffset);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.lblReflectedTemperature);
            this.Controls.Add(this.lblMarkerName);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OWBISPAddForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建测温对象";
            this.Load += new System.EventHandler(this.OWBISPAddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMbSlot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEmissivityValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReflectedTemperatureValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistanceValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbAlarm;
        private System.Windows.Forms.CheckBox cbAlarm;
        private System.Windows.Forms.CheckBox cbAPI;
        private System.Windows.Forms.CheckBox cbOSD;
        private System.Windows.Forms.CheckBox cbModbus;
        private System.Windows.Forms.Label lblMbRange;
        private System.Windows.Forms.Label lblAlarm;
        private System.Windows.Forms.Label lblAPI;
        private System.Windows.Forms.Label lblOSD;
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
        private System.Windows.Forms.TextBox tbMarkerName;
        private System.Windows.Forms.TextBox tbLayer;
        private System.Windows.Forms.Label lblLayer;
        private System.Windows.Forms.ToolTip ttISPAddForm;
        private System.Windows.Forms.CheckBox cbEmissivityVisible;
        private System.Windows.Forms.CheckBox cbDistanceVisible;
        private System.Windows.Forms.CheckBox cbMax;
        private System.Windows.Forms.CheckBox cbMin;
        private System.Windows.Forms.CheckBox cbAvg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}