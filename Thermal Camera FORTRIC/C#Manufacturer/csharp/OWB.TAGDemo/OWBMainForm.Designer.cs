namespace OWB.TAGDemo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pConnect = new System.Windows.Forms.Panel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.gbConnInfo = new System.Windows.Forms.GroupBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.dgvMarker = new System.Windows.Forms.DataGridView();
            this.标记流名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.最高温 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.最低温 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.平均温 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ttMainForm = new System.Windows.Forms.ToolTip(this.components);
            this.pConnect.SuspendLayout();
            this.gbConnInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarker)).BeginInit();
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
            this.pConnect.TabIndex = 58;
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
            // dgvMarker
            // 
            this.dgvMarker.AllowUserToAddRows = false;
            this.dgvMarker.AllowUserToDeleteRows = false;
            this.dgvMarker.AllowUserToResizeRows = false;
            this.dgvMarker.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.dgvMarker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMarker.GridColor = System.Drawing.SystemColors.Control;
            this.dgvMarker.Location = new System.Drawing.Point(0, 54);
            this.dgvMarker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvMarker.MultiSelect = false;
            this.dgvMarker.Name = "dgvMarker";
            this.dgvMarker.RowHeadersVisible = false;
            this.dgvMarker.RowTemplate.Height = 27;
            this.dgvMarker.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMarker.Size = new System.Drawing.Size(704, 387);
            this.dgvMarker.TabIndex = 63;
            // 
            // 标记流名称
            // 
            this.标记流名称.FillWeight = 75F;
            this.标记流名称.HeaderText = "标记流名称";
            this.标记流名称.Name = "标记流名称";
            // 
            // 最高温
            // 
            this.最高温.FillWeight = 60F;
            this.最高温.HeaderText = "最高温";
            this.最高温.Name = "最高温";
            // 
            // 最低温
            // 
            this.最低温.FillWeight = 65F;
            this.最低温.HeaderText = "最低温";
            this.最低温.Name = "最低温";
            // 
            // 平均温
            // 
            this.平均温.FillWeight = 70F;
            this.平均温.HeaderText = "平均温";
            this.平均温.Name = "平均温";
            // 
            // OWBMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.dgvMarker);
            this.Controls.Add(this.pConnect);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OWBMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TAGDemo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OWBMainForm_FormClosed);
            this.pConnect.ResumeLayout(false);
            this.gbConnInfo.ResumeLayout(false);
            this.gbConnInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarker)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvMarker;
        private System.Windows.Forms.DataGridViewTextBoxColumn 标记流名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 最高温;
        private System.Windows.Forms.DataGridViewTextBoxColumn 最低温;
        private System.Windows.Forms.DataGridViewTextBoxColumn 平均温;
        private System.Windows.Forms.ToolTip ttMainForm;
    }
}

