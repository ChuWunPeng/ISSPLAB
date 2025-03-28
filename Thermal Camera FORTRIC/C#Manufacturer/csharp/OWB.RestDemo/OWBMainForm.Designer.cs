namespace OWB.RestDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OWBMainForm));
            this.pConnect = new System.Windows.Forms.Panel();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.pTemplate = new System.Windows.Forms.Panel();
            this.lvTemplate = new System.Windows.Forms.ListView();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.pExecute = new System.Windows.Forms.Panel();
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.rtbResponse = new System.Windows.Forms.RichTextBox();
            this.lblResponse = new System.Windows.Forms.Label();
            this.lblSend = new System.Windows.Forms.Label();
            this.rtbSend = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.cbUrl = new System.Windows.Forms.TextBox();
            this.cbMethod = new System.Windows.Forms.ComboBox();
            this.ttMainForm = new System.Windows.Forms.ToolTip(this.components);
            this.lblStatusCode = new System.Windows.Forms.Label();
            this.pConnect.SuspendLayout();
            this.pTemplate.SuspendLayout();
            this.pExecute.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // pConnect
            // 
            this.pConnect.Controls.Add(this.tbPassword);
            this.pConnect.Controls.Add(this.lblIPAddress);
            this.pConnect.Controls.Add(this.tbIPAddress);
            this.pConnect.Controls.Add(this.lblUserName);
            this.pConnect.Controls.Add(this.tbUserName);
            this.pConnect.Controls.Add(this.lblPassword);
            this.pConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.pConnect.Location = new System.Drawing.Point(0, 0);
            this.pConnect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pConnect.Name = "pConnect";
            this.pConnect.Size = new System.Drawing.Size(710, 47);
            this.pConnect.TabIndex = 58;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(451, 13);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(152, 21);
            this.tbPassword.TabIndex = 40;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(17, 17);
            this.lblIPAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(29, 12);
            this.lblIPAddress.TabIndex = 36;
            this.lblIPAddress.Text = "地址";
            // 
            // tbIPAddress
            // 
            this.tbIPAddress.Location = new System.Drawing.Point(62, 13);
            this.tbIPAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbIPAddress.Name = "tbIPAddress";
            this.tbIPAddress.Size = new System.Drawing.Size(131, 21);
            this.tbIPAddress.TabIndex = 37;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(210, 17);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(41, 12);
            this.lblUserName.TabIndex = 38;
            this.lblUserName.Text = "用户名";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(266, 13);
            this.tbUserName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(117, 21);
            this.tbUserName.TabIndex = 39;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(401, 17);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(29, 12);
            this.lblPassword.TabIndex = 41;
            this.lblPassword.Text = "密码";
            // 
            // pTemplate
            // 
            this.pTemplate.Controls.Add(this.lvTemplate);
            this.pTemplate.Controls.Add(this.lblTemplate);
            this.pTemplate.Dock = System.Windows.Forms.DockStyle.Left;
            this.pTemplate.Location = new System.Drawing.Point(0, 47);
            this.pTemplate.Name = "pTemplate";
            this.pTemplate.Size = new System.Drawing.Size(271, 417);
            this.pTemplate.TabIndex = 60;
            // 
            // lvTemplate
            // 
            this.lvTemplate.FullRowSelect = true;
            this.lvTemplate.GridLines = true;
            this.lvTemplate.Location = new System.Drawing.Point(19, 30);
            this.lvTemplate.MultiSelect = false;
            this.lvTemplate.Name = "lvTemplate";
            this.lvTemplate.Size = new System.Drawing.Size(246, 372);
            this.lvTemplate.TabIndex = 31;
            this.lvTemplate.UseCompatibleStateImageBehavior = false;
            this.lvTemplate.View = System.Windows.Forms.View.Details;
            this.lvTemplate.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvTemplate_MouseDoubleClick);
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(17, 11);
            this.lblTemplate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(29, 12);
            this.lblTemplate.TabIndex = 30;
            this.lblTemplate.Text = "模板";
            // 
            // pExecute
            // 
            this.pExecute.Controls.Add(this.lblStatusCode);
            this.pExecute.Controls.Add(this.pbStatus);
            this.pExecute.Controls.Add(this.rtbResponse);
            this.pExecute.Controls.Add(this.lblResponse);
            this.pExecute.Controls.Add(this.lblSend);
            this.pExecute.Controls.Add(this.rtbSend);
            this.pExecute.Controls.Add(this.btnSend);
            this.pExecute.Controls.Add(this.cbUrl);
            this.pExecute.Controls.Add(this.cbMethod);
            this.pExecute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pExecute.Location = new System.Drawing.Point(271, 47);
            this.pExecute.Name = "pExecute";
            this.pExecute.Size = new System.Drawing.Size(439, 417);
            this.pExecute.TabIndex = 61;
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(8, 246);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(32, 32);
            this.pbStatus.TabIndex = 56;
            this.pbStatus.TabStop = false;
            // 
            // rtbResponse
            // 
            this.rtbResponse.Location = new System.Drawing.Point(45, 225);
            this.rtbResponse.Name = "rtbResponse";
            this.rtbResponse.Size = new System.Drawing.Size(375, 180);
            this.rtbResponse.TabIndex = 55;
            this.rtbResponse.Text = "";
            // 
            // lblResponse
            // 
            this.lblResponse.AutoSize = true;
            this.lblResponse.Location = new System.Drawing.Point(9, 228);
            this.lblResponse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(29, 12);
            this.lblResponse.TabIndex = 30;
            this.lblResponse.Text = "接收";
            // 
            // lblSend
            // 
            this.lblSend.AutoSize = true;
            this.lblSend.Location = new System.Drawing.Point(9, 45);
            this.lblSend.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSend.Name = "lblSend";
            this.lblSend.Size = new System.Drawing.Size(29, 12);
            this.lblSend.TabIndex = 30;
            this.lblSend.Text = "请求";
            // 
            // rtbSend
            // 
            this.rtbSend.Location = new System.Drawing.Point(45, 42);
            this.rtbSend.Name = "rtbSend";
            this.rtbSend.Size = new System.Drawing.Size(375, 180);
            this.rtbSend.TabIndex = 55;
            this.rtbSend.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnSend.Location = new System.Drawing.Point(366, 7);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(54, 23);
            this.btnSend.TabIndex = 54;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // cbUrl
            // 
            this.cbUrl.Location = new System.Drawing.Point(81, 7);
            this.cbUrl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbUrl.Name = "cbUrl";
            this.cbUrl.Size = new System.Drawing.Size(277, 21);
            this.cbUrl.TabIndex = 35;
            // 
            // cbMethod
            // 
            this.cbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMethod.FormattingEnabled = true;
            this.cbMethod.Items.AddRange(new object[] {
            "GET",
            "PUT",
            "POST",
            "DELETE"});
            this.cbMethod.Location = new System.Drawing.Point(9, 7);
            this.cbMethod.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbMethod.Name = "cbMethod";
            this.cbMethod.Size = new System.Drawing.Size(64, 20);
            this.cbMethod.TabIndex = 1;
            this.cbMethod.SelectedIndexChanged += new System.EventHandler(this.cbMethod_SelectedIndexChanged);
            // 
            // lblStatusCode
            // 
            this.lblStatusCode.AutoSize = true;
            this.lblStatusCode.Location = new System.Drawing.Point(9, 281);
            this.lblStatusCode.Name = "lblStatusCode";
            this.lblStatusCode.Size = new System.Drawing.Size(0, 12);
            this.lblStatusCode.TabIndex = 57;
            // 
            // OWBMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(710, 464);
            this.Controls.Add(this.pExecute);
            this.Controls.Add(this.pTemplate);
            this.Controls.Add(this.pConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OWBMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REST";
            this.pConnect.ResumeLayout(false);
            this.pConnect.PerformLayout();
            this.pTemplate.ResumeLayout(false);
            this.pTemplate.PerformLayout();
            this.pExecute.ResumeLayout(false);
            this.pExecute.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pConnect;
        private System.Windows.Forms.Panel pTemplate;
        private System.Windows.Forms.Panel pExecute;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.ComboBox cbMethod;
        private System.Windows.Forms.RichTextBox rtbResponse;
        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.Label lblSend;
        private System.Windows.Forms.RichTextBox rtbSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox cbUrl;
        private System.Windows.Forms.ToolTip ttMainForm;
        private System.Windows.Forms.ListView lvTemplate;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.PictureBox pbStatus;
        private System.Windows.Forms.Label lblStatusCode;
    }
}

