namespace OWB.TADemo
{
    partial class OWBAboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OWBAboutForm));
            this.pbAbout = new System.Windows.Forms.PictureBox();
            this.lblCopyRightName = new System.Windows.Forms.Label();
            this.lblStreamSDKVersion = new System.Windows.Forms.Label();
            this.lblCopyRightValue = new System.Windows.Forms.Label();
            this.lblStreamSDKVersionValue = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbAbout)).BeginInit();
            this.SuspendLayout();
            // 
            // pbAbout
            // 
            resources.ApplyResources(this.pbAbout, "pbAbout");
            this.pbAbout.Name = "pbAbout";
            this.pbAbout.TabStop = false;
            // 
            // lblCopyRightName
            // 
            resources.ApplyResources(this.lblCopyRightName, "lblCopyRightName");
            this.lblCopyRightName.Name = "lblCopyRightName";
            // 
            // lblStreamSDKVersion
            // 
            resources.ApplyResources(this.lblStreamSDKVersion, "lblStreamSDKVersion");
            this.lblStreamSDKVersion.Name = "lblStreamSDKVersion";
            // 
            // lblCopyRightValue
            // 
            resources.ApplyResources(this.lblCopyRightValue, "lblCopyRightValue");
            this.lblCopyRightValue.Name = "lblCopyRightValue";
            // 
            // lblStreamSDKVersionValue
            // 
            resources.ApplyResources(this.lblStreamSDKVersionValue, "lblStreamSDKVersionValue");
            this.lblStreamSDKVersionValue.Name = "lblStreamSDKVersionValue";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // OWBAboutForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblStreamSDKVersionValue);
            this.Controls.Add(this.lblCopyRightValue);
            this.Controls.Add(this.lblStreamSDKVersion);
            this.Controls.Add(this.lblCopyRightName);
            this.Controls.Add(this.pbAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OWBAboutForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pbAbout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbAbout;
        private System.Windows.Forms.Label lblCopyRightName;
        private System.Windows.Forms.Label lblStreamSDKVersion;
        private System.Windows.Forms.Label lblCopyRightValue;
        private System.Windows.Forms.Label lblStreamSDKVersionValue;
        private System.Windows.Forms.Button btnOK;
    }
}