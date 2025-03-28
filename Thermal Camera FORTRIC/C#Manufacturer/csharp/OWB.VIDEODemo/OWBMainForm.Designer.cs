namespace OWB.VIDEODemo
{
    partial class OWBMainForm
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
            this.pConnect = new System.Windows.Forms.Panel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.gbConnInfo = new System.Windows.Forms.GroupBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.ttMainForm = new System.Windows.Forms.ToolTip(this.components);
            this.pH264 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbTemper = new System.Windows.Forms.RadioButton();
            this.rbRaw = new System.Windows.Forms.RadioButton();
            this.gbStream = new System.Windows.Forms.GroupBox();
            this.nudRegionHeight = new System.Windows.Forms.NumericUpDown();
            this.nudRegionWidth = new System.Windows.Forms.NumericUpDown();
            this.nudRegionY = new System.Windows.Forms.NumericUpDown();
            this.nudRegionX = new System.Windows.Forms.NumericUpDown();
            this.btnSet = new System.Windows.Forms.Button();
            this.lblLens = new System.Windows.Forms.Label();
            this.lblTemperatureRange = new System.Windows.Forms.Label();
            this.lblShow = new System.Windows.Forms.Label();
            this.lblPalette = new System.Windows.Forms.Label();
            this.dgvTemperTable = new System.Windows.Forms.DataGridView();
            this.pConnect.SuspendLayout();
            this.gbConnInfo.SuspendLayout();
            this.pH264.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbStream.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegionHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegionWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTemperTable)).BeginInit();
            this.SuspendLayout();
            // 
            // pConnect
            // 
            this.pConnect.Controls.Add(this.btnConnect);
            this.pConnect.Controls.Add(this.gbConnInfo);
            this.pConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.pConnect.Location = new System.Drawing.Point(0, 0);
            this.pConnect.Margin = new System.Windows.Forms.Padding(4);
            this.pConnect.Name = "pConnect";
            this.pConnect.Size = new System.Drawing.Size(704, 54);
            this.pConnect.TabIndex = 59;
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
            // gbConnInfo
            // 
            this.gbConnInfo.Controls.Add(this.tbPassword);
            this.gbConnInfo.Controls.Add(this.lblIPAddress);
            this.gbConnInfo.Controls.Add(this.tbIPAddress);
            this.gbConnInfo.Controls.Add(this.lblUserName);
            this.gbConnInfo.Controls.Add(this.tbUserName);
            this.gbConnInfo.Controls.Add(this.lblPassword);
            this.gbConnInfo.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            // pH264
            // 
            this.pH264.Controls.Add(this.groupBox1);
            this.pH264.Controls.Add(this.gbStream);
            this.pH264.Dock = System.Windows.Forms.DockStyle.Left;
            this.pH264.Location = new System.Drawing.Point(0, 54);
            this.pH264.Name = "pH264";
            this.pH264.Size = new System.Drawing.Size(176, 387);
            this.pH264.TabIndex = 60;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbTemper);
            this.groupBox1.Controls.Add(this.rbRaw);
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(7, 187);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(162, 74);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据类型";
            // 
            // rbTemper
            // 
            this.rbTemper.AutoSize = true;
            this.rbTemper.Location = new System.Drawing.Point(41, 45);
            this.rbTemper.Name = "rbTemper";
            this.rbTemper.Size = new System.Drawing.Size(59, 16);
            this.rbTemper.TabIndex = 40;
            this.rbTemper.Text = "温度值";
            this.rbTemper.UseVisualStyleBackColor = true;
            // 
            // rbRaw
            // 
            this.rbRaw.AutoSize = true;
            this.rbRaw.Checked = true;
            this.rbRaw.Location = new System.Drawing.Point(41, 21);
            this.rbRaw.Name = "rbRaw";
            this.rbRaw.Size = new System.Drawing.Size(59, 16);
            this.rbRaw.TabIndex = 39;
            this.rbRaw.TabStop = true;
            this.rbRaw.Text = "原始值";
            this.rbRaw.UseVisualStyleBackColor = true;
            // 
            // gbStream
            // 
            this.gbStream.Controls.Add(this.nudRegionHeight);
            this.gbStream.Controls.Add(this.nudRegionWidth);
            this.gbStream.Controls.Add(this.nudRegionY);
            this.gbStream.Controls.Add(this.nudRegionX);
            this.gbStream.Controls.Add(this.btnSet);
            this.gbStream.Controls.Add(this.lblLens);
            this.gbStream.Controls.Add(this.lblTemperatureRange);
            this.gbStream.Controls.Add(this.lblShow);
            this.gbStream.Controls.Add(this.lblPalette);
            this.gbStream.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbStream.Location = new System.Drawing.Point(6, 4);
            this.gbStream.Margin = new System.Windows.Forms.Padding(4);
            this.gbStream.Name = "gbStream";
            this.gbStream.Padding = new System.Windows.Forms.Padding(4);
            this.gbStream.Size = new System.Drawing.Size(163, 175);
            this.gbStream.TabIndex = 52;
            this.gbStream.TabStop = false;
            this.gbStream.Text = "区域设置";
            // 
            // nudRegionHeight
            // 
            this.nudRegionHeight.Location = new System.Drawing.Point(73, 112);
            this.nudRegionHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudRegionHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRegionHeight.Name = "nudRegionHeight";
            this.nudRegionHeight.Size = new System.Drawing.Size(71, 21);
            this.nudRegionHeight.TabIndex = 79;
            this.nudRegionHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudRegionWidth
            // 
            this.nudRegionWidth.Location = new System.Drawing.Point(73, 85);
            this.nudRegionWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudRegionWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRegionWidth.Name = "nudRegionWidth";
            this.nudRegionWidth.Size = new System.Drawing.Size(71, 21);
            this.nudRegionWidth.TabIndex = 79;
            this.nudRegionWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudRegionY
            // 
            this.nudRegionY.Location = new System.Drawing.Point(73, 58);
            this.nudRegionY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudRegionY.Name = "nudRegionY";
            this.nudRegionY.Size = new System.Drawing.Size(71, 21);
            this.nudRegionY.TabIndex = 79;
            // 
            // nudRegionX
            // 
            this.nudRegionX.Location = new System.Drawing.Point(73, 30);
            this.nudRegionX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudRegionX.Name = "nudRegionX";
            this.nudRegionX.Size = new System.Drawing.Size(71, 21);
            this.nudRegionX.TabIndex = 79;
            // 
            // btnSet
            // 
            this.btnSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnSet.Location = new System.Drawing.Point(73, 142);
            this.btnSet.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(71, 23);
            this.btnSet.TabIndex = 55;
            this.btnSet.Text = "设置";
            this.btnSet.UseVisualStyleBackColor = false;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // lblLens
            // 
            this.lblLens.AutoSize = true;
            this.lblLens.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLens.Location = new System.Drawing.Point(11, 88);
            this.lblLens.Name = "lblLens";
            this.lblLens.Size = new System.Drawing.Size(29, 12);
            this.lblLens.TabIndex = 29;
            this.lblLens.Text = "宽度";
            // 
            // lblTemperatureRange
            // 
            this.lblTemperatureRange.AutoSize = true;
            this.lblTemperatureRange.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTemperatureRange.Location = new System.Drawing.Point(11, 116);
            this.lblTemperatureRange.Name = "lblTemperatureRange";
            this.lblTemperatureRange.Size = new System.Drawing.Size(29, 12);
            this.lblTemperatureRange.TabIndex = 28;
            this.lblTemperatureRange.Text = "高度";
            // 
            // lblShow
            // 
            this.lblShow.AutoSize = true;
            this.lblShow.Location = new System.Drawing.Point(11, 60);
            this.lblShow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShow.Name = "lblShow";
            this.lblShow.Size = new System.Drawing.Size(47, 12);
            this.lblShow.TabIndex = 26;
            this.lblShow.Text = "左上角Y";
            // 
            // lblPalette
            // 
            this.lblPalette.AutoSize = true;
            this.lblPalette.Location = new System.Drawing.Point(11, 32);
            this.lblPalette.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPalette.Name = "lblPalette";
            this.lblPalette.Size = new System.Drawing.Size(47, 12);
            this.lblPalette.TabIndex = 27;
            this.lblPalette.Text = "左上角X";
            // 
            // dgvTemperTable
            // 
            this.dgvTemperTable.AllowUserToAddRows = false;
            this.dgvTemperTable.AllowUserToDeleteRows = false;
            this.dgvTemperTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTemperTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTemperTable.Location = new System.Drawing.Point(176, 54);
            this.dgvTemperTable.Name = "dgvTemperTable";
            this.dgvTemperTable.ReadOnly = true;
            this.dgvTemperTable.RowHeadersWidth = 55;
            this.dgvTemperTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvTemperTable.RowTemplate.Height = 23;
            this.dgvTemperTable.Size = new System.Drawing.Size(528, 387);
            this.dgvTemperTable.TabIndex = 61;
            // 
            // OWBMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.dgvTemperTable);
            this.Controls.Add(this.pH264);
            this.Controls.Add(this.pConnect);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OWBMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VIDEODemo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OWBMainForm_FormClosed);
            this.pConnect.ResumeLayout(false);
            this.gbConnInfo.ResumeLayout(false);
            this.gbConnInfo.PerformLayout();
            this.pH264.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbStream.ResumeLayout(false);
            this.gbStream.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegionHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegionWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTemperTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pConnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox gbConnInfo;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.ToolTip ttMainForm;
        private System.Windows.Forms.Panel pH264;
        private System.Windows.Forms.GroupBox gbStream;
        private System.Windows.Forms.Label lblLens;
        private System.Windows.Forms.Label lblTemperatureRange;
        private System.Windows.Forms.Label lblShow;
        private System.Windows.Forms.Label lblPalette;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.NumericUpDown nudRegionHeight;
        private System.Windows.Forms.NumericUpDown nudRegionWidth;
        private System.Windows.Forms.NumericUpDown nudRegionY;
        private System.Windows.Forms.NumericUpDown nudRegionX;
        private System.Windows.Forms.DataGridView dgvTemperTable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbRaw;
        private System.Windows.Forms.RadioButton rbTemper;
    }
}

