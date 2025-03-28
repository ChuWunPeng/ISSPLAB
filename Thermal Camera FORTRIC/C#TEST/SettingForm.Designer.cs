namespace TEST
{
    partial class SettingForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.nudDistanceValue = new System.Windows.Forms.NumericUpDown();
            this.nudReflectedTemperatureValue = new System.Windows.Forms.NumericUpDown();
            this.nudAtmosphericTemperatureValue = new System.Windows.Forms.NumericUpDown();
            this.nudLensTValue = new System.Windows.Forms.NumericUpDown();
            this.nudOffsetValue = new System.Windows.Forms.NumericUpDown();
            this.nudLensTransValue = new System.Windows.Forms.NumericUpDown();
            this.nudRelativeHumidityValue = new System.Windows.Forms.NumericUpDown();
            this.nudEmissivityValue = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.cbTemperatureRange = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistanceValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReflectedTemperatureValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAtmosphericTemperatureValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLensTValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLensTransValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRelativeHumidityValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEmissivityValue)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(272, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 35);
            this.button1.TabIndex = 52;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nudDistanceValue
            // 
            this.nudDistanceValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.nudDistanceValue.DecimalPlaces = 2;
            this.nudDistanceValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.nudDistanceValue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudDistanceValue.Location = new System.Drawing.Point(262, 186);
            this.nudDistanceValue.Margin = new System.Windows.Forms.Padding(0);
            this.nudDistanceValue.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            131072});
            this.nudDistanceValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudDistanceValue.Name = "nudDistanceValue";
            this.nudDistanceValue.Size = new System.Drawing.Size(105, 27);
            this.nudDistanceValue.TabIndex = 51;
            this.nudDistanceValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudReflectedTemperatureValue
            // 
            this.nudReflectedTemperatureValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.nudReflectedTemperatureValue.DecimalPlaces = 2;
            this.nudReflectedTemperatureValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.nudReflectedTemperatureValue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudReflectedTemperatureValue.Location = new System.Drawing.Point(262, 108);
            this.nudReflectedTemperatureValue.Margin = new System.Windows.Forms.Padding(0);
            this.nudReflectedTemperatureValue.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudReflectedTemperatureValue.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudReflectedTemperatureValue.Name = "nudReflectedTemperatureValue";
            this.nudReflectedTemperatureValue.Size = new System.Drawing.Size(105, 27);
            this.nudReflectedTemperatureValue.TabIndex = 50;
            this.nudReflectedTemperatureValue.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nudAtmosphericTemperatureValue
            // 
            this.nudAtmosphericTemperatureValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.nudAtmosphericTemperatureValue.DecimalPlaces = 2;
            this.nudAtmosphericTemperatureValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.nudAtmosphericTemperatureValue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudAtmosphericTemperatureValue.Location = new System.Drawing.Point(262, 147);
            this.nudAtmosphericTemperatureValue.Margin = new System.Windows.Forms.Padding(0);
            this.nudAtmosphericTemperatureValue.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudAtmosphericTemperatureValue.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudAtmosphericTemperatureValue.Name = "nudAtmosphericTemperatureValue";
            this.nudAtmosphericTemperatureValue.Size = new System.Drawing.Size(105, 27);
            this.nudAtmosphericTemperatureValue.TabIndex = 49;
            this.nudAtmosphericTemperatureValue.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nudLensTValue
            // 
            this.nudLensTValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.nudLensTValue.DecimalPlaces = 2;
            this.nudLensTValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.nudLensTValue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudLensTValue.Location = new System.Drawing.Point(262, 225);
            this.nudLensTValue.Margin = new System.Windows.Forms.Padding(0);
            this.nudLensTValue.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudLensTValue.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudLensTValue.Name = "nudLensTValue";
            this.nudLensTValue.Size = new System.Drawing.Size(105, 27);
            this.nudLensTValue.TabIndex = 48;
            this.nudLensTValue.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nudOffsetValue
            // 
            this.nudOffsetValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.nudOffsetValue.DecimalPlaces = 2;
            this.nudOffsetValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.nudOffsetValue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudOffsetValue.Location = new System.Drawing.Point(262, 303);
            this.nudOffsetValue.Margin = new System.Windows.Forms.Padding(0);
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
            this.nudOffsetValue.Size = new System.Drawing.Size(105, 27);
            this.nudOffsetValue.TabIndex = 47;
            // 
            // nudLensTransValue
            // 
            this.nudLensTransValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.nudLensTransValue.DecimalPlaces = 2;
            this.nudLensTransValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.nudLensTransValue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudLensTransValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudLensTransValue.Location = new System.Drawing.Point(262, 264);
            this.nudLensTransValue.Margin = new System.Windows.Forms.Padding(0);
            this.nudLensTransValue.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.nudLensTransValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudLensTransValue.Name = "nudLensTransValue";
            this.nudLensTransValue.Size = new System.Drawing.Size(105, 27);
            this.nudLensTransValue.TabIndex = 46;
            this.nudLensTransValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudRelativeHumidityValue
            // 
            this.nudRelativeHumidityValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.nudRelativeHumidityValue.DecimalPlaces = 2;
            this.nudRelativeHumidityValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.nudRelativeHumidityValue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudRelativeHumidityValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudRelativeHumidityValue.Location = new System.Drawing.Point(262, 69);
            this.nudRelativeHumidityValue.Margin = new System.Windows.Forms.Padding(0);
            this.nudRelativeHumidityValue.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.nudRelativeHumidityValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudRelativeHumidityValue.Name = "nudRelativeHumidityValue";
            this.nudRelativeHumidityValue.Size = new System.Drawing.Size(105, 27);
            this.nudRelativeHumidityValue.TabIndex = 45;
            this.nudRelativeHumidityValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // nudEmissivityValue
            // 
            this.nudEmissivityValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.nudEmissivityValue.DecimalPlaces = 2;
            this.nudEmissivityValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.nudEmissivityValue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nudEmissivityValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudEmissivityValue.Location = new System.Drawing.Point(262, 30);
            this.nudEmissivityValue.Margin = new System.Windows.Forms.Padding(0);
            this.nudEmissivityValue.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.nudEmissivityValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudEmissivityValue.Name = "nudEmissivityValue";
            this.nudEmissivityValue.Size = new System.Drawing.Size(105, 27);
            this.nudEmissivityValue.TabIndex = 44;
            this.nudEmissivityValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(139, 306);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 20);
            this.label8.TabIndex = 43;
            this.label8.Text = "(-200 ~ 200)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(139, 267);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 20);
            this.label10.TabIndex = 42;
            this.label10.Text = "(0.01 ~ 1.00)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(139, 228);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 20);
            this.label11.TabIndex = 41;
            this.label11.Text = "(-200 ~ 200)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label12.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(139, 111);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 20);
            this.label12.TabIndex = 40;
            this.label12.Text = "(-200 ~ 200)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label13.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(139, 189);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 20);
            this.label13.TabIndex = 39;
            this.label13.Text = "(0.1 ~ 1000.0)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label14.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(139, 150);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 20);
            this.label14.TabIndex = 38;
            this.label14.Text = "(-200 ~ 200)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label15.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(139, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 20);
            this.label15.TabIndex = 36;
            this.label15.Text = "(0.01 ~ 1.00)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label16.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(139, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 20);
            this.label16.TabIndex = 37;
            this.label16.Text = "(0.01 ~ 1.00)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label9.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(7, 306);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 20);
            this.label9.TabIndex = 35;
            this.label9.Text = "偏移";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(7, 267);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 20);
            this.label7.TabIndex = 34;
            this.label7.Text = "外部光學透射率";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(7, 228);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 20);
            this.label6.TabIndex = 33;
            this.label6.Text = "外部光學溫度";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(7, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.TabIndex = 32;
            this.label5.Text = "反射溫度";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(7, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 20);
            this.label4.TabIndex = 31;
            this.label4.Text = "距離";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(7, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "環境溫度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(7, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 28;
            this.label1.Text = "發射率";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(7, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 29;
            this.label3.Text = "濕度";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.cbTemperatureRange);
            this.GroupBox1.Controls.Add(this.label17);
            this.GroupBox1.Controls.Add(this.label10);
            this.GroupBox1.Controls.Add(this.nudDistanceValue);
            this.GroupBox1.Controls.Add(this.button1);
            this.GroupBox1.Controls.Add(this.nudReflectedTemperatureValue);
            this.GroupBox1.Controls.Add(this.nudLensTransValue);
            this.GroupBox1.Controls.Add(this.nudAtmosphericTemperatureValue);
            this.GroupBox1.Controls.Add(this.label3);
            this.GroupBox1.Controls.Add(this.nudLensTValue);
            this.GroupBox1.Controls.Add(this.label1);
            this.GroupBox1.Controls.Add(this.nudOffsetValue);
            this.GroupBox1.Controls.Add(this.label2);
            this.GroupBox1.Controls.Add(this.label4);
            this.GroupBox1.Controls.Add(this.nudRelativeHumidityValue);
            this.GroupBox1.Controls.Add(this.label5);
            this.GroupBox1.Controls.Add(this.nudEmissivityValue);
            this.GroupBox1.Controls.Add(this.label6);
            this.GroupBox1.Controls.Add(this.label8);
            this.GroupBox1.Controls.Add(this.label7);
            this.GroupBox1.Controls.Add(this.label9);
            this.GroupBox1.Controls.Add(this.label11);
            this.GroupBox1.Controls.Add(this.label16);
            this.GroupBox1.Controls.Add(this.label12);
            this.GroupBox1.Controls.Add(this.label15);
            this.GroupBox1.Controls.Add(this.label13);
            this.GroupBox1.Controls.Add(this.label14);
            this.GroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.GroupBox1.Location = new System.Drawing.Point(2, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(379, 438);
            this.GroupBox1.TabIndex = 53;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Setting";
            // 
            // cbTemperatureRange
            // 
            this.cbTemperatureRange.DropDownWidth = 150;
            this.cbTemperatureRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTemperatureRange.FormattingEnabled = true;
            this.cbTemperatureRange.IntegralHeight = false;
            this.cbTemperatureRange.ItemHeight = 20;
            this.cbTemperatureRange.Location = new System.Drawing.Point(217, 343);
            this.cbTemperatureRange.Name = "cbTemperatureRange";
            this.cbTemperatureRange.Size = new System.Drawing.Size(150, 28);
            this.cbTemperatureRange.TabIndex = 54;
           
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.label17.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(10, 346);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 20);
            this.label17.TabIndex = 53;
            this.label17.Text = "溫度範圍";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(384, 578);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettingForm";
            this.Text = "SettingForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingForm_FormClosed);
            this.Load += new System.EventHandler(this.SettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDistanceValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReflectedTemperatureValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAtmosphericTemperatureValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLensTValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLensTransValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRelativeHumidityValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEmissivityValue)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.NumericUpDown nudDistanceValue;
        public System.Windows.Forms.NumericUpDown nudReflectedTemperatureValue;
        public System.Windows.Forms.NumericUpDown nudAtmosphericTemperatureValue;
        public System.Windows.Forms.NumericUpDown nudLensTValue;
        public System.Windows.Forms.NumericUpDown nudOffsetValue;
        public System.Windows.Forms.NumericUpDown nudLensTransValue;
        public System.Windows.Forms.NumericUpDown nudRelativeHumidityValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox GroupBox1;
        public System.Windows.Forms.NumericUpDown nudEmissivityValue;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbTemperatureRange;
    }
}