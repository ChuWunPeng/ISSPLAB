namespace OWB.TADemo
{
    partial class OWBSerialSetForm
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
            this.tbPairty = new System.Windows.Forms.TextBox();
            this.tbBaudrate = new System.Windows.Forms.TextBox();
            this.tbSize = new System.Windows.Forms.TextBox();
            this.lblParity = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblBaudrate = new System.Windows.Forms.Label();
            this.lblStopbit = new System.Windows.Forms.Label();
            this.tbStopbit = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbPairty
            // 
            this.tbPairty.Location = new System.Drawing.Point(131, 98);
            this.tbPairty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbPairty.Name = "tbPairty";
            this.tbPairty.Size = new System.Drawing.Size(204, 21);
            this.tbPairty.TabIndex = 28;
            // 
            // tbBaudrate
            // 
            this.tbBaudrate.Location = new System.Drawing.Point(131, 28);
            this.tbBaudrate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbBaudrate.Name = "tbBaudrate";
            this.tbBaudrate.Size = new System.Drawing.Size(204, 21);
            this.tbBaudrate.TabIndex = 27;
            // 
            // tbSize
            // 
            this.tbSize.Location = new System.Drawing.Point(131, 63);
            this.tbSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbSize.Name = "tbSize";
            this.tbSize.Size = new System.Drawing.Size(204, 21);
            this.tbSize.TabIndex = 26;
            // 
            // lblParity
            // 
            this.lblParity.AutoSize = true;
            this.lblParity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblParity.Location = new System.Drawing.Point(32, 104);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(53, 12);
            this.lblParity.TabIndex = 32;
            this.lblParity.Text = "奇偶校验";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSize.Location = new System.Drawing.Point(32, 68);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(41, 12);
            this.lblSize.TabIndex = 31;
            this.lblSize.Text = "数据位";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(179, 172);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(260, 172);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 29;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblBaudrate
            // 
            this.lblBaudrate.AutoSize = true;
            this.lblBaudrate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBaudrate.Location = new System.Drawing.Point(32, 32);
            this.lblBaudrate.Name = "lblBaudrate";
            this.lblBaudrate.Size = new System.Drawing.Size(41, 12);
            this.lblBaudrate.TabIndex = 25;
            this.lblBaudrate.Text = "波特率";
            // 
            // lblStopbit
            // 
            this.lblStopbit.AutoSize = true;
            this.lblStopbit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStopbit.Location = new System.Drawing.Point(32, 140);
            this.lblStopbit.Name = "lblStopbit";
            this.lblStopbit.Size = new System.Drawing.Size(41, 12);
            this.lblStopbit.TabIndex = 32;
            this.lblStopbit.Text = "停止位";
            // 
            // tbStopbit
            // 
            this.tbStopbit.Location = new System.Drawing.Point(131, 133);
            this.tbStopbit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbStopbit.Name = "tbStopbit";
            this.tbStopbit.Size = new System.Drawing.Size(204, 21);
            this.tbStopbit.TabIndex = 28;
            // 
            // OWBSerialSetForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(379, 209);
            this.Controls.Add(this.tbStopbit);
            this.Controls.Add(this.tbPairty);
            this.Controls.Add(this.tbBaudrate);
            this.Controls.Add(this.lblStopbit);
            this.Controls.Add(this.tbSize);
            this.Controls.Add(this.lblParity);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblBaudrate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OWBSerialSetForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "串口设置";
            this.Load += new System.EventHandler(this.OWBSerialSetForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPairty;
        private System.Windows.Forms.TextBox tbBaudrate;
        private System.Windows.Forms.TextBox tbSize;
        private System.Windows.Forms.Label lblParity;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblBaudrate;
        private System.Windows.Forms.Label lblStopbit;
        private System.Windows.Forms.TextBox tbStopbit;
    }
}