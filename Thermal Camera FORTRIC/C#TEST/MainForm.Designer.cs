namespace TEST
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Mainpanel = new System.Windows.Forms.Panel();
            this.MainWindowPbx = new System.Windows.Forms.PictureBox();
            this.Playtmr = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Menubtn = new System.Windows.Forms.Button();
            this.Infopanel = new System.Windows.Forms.Panel();
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.Settingbtn = new System.Windows.Forms.Button();
            this.Homebtn = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TempCalbtn = new System.Windows.Forms.Button();
            this.Capturebtn = new System.Windows.Forms.Button();
            this.Recordbtn = new System.Windows.Forms.Button();
            this.Playbtn = new System.Windows.Forms.Button();
            this.Menutmr = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Mainpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainWindowPbx)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.MenuPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Mainpanel
            // 
            this.Mainpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Mainpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Mainpanel.Controls.Add(this.MainWindowPbx);
            resources.ApplyResources(this.Mainpanel, "Mainpanel");
            this.Mainpanel.Name = "Mainpanel";
            // 
            // MainWindowPbx
            // 
            this.MainWindowPbx.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            resources.ApplyResources(this.MainWindowPbx, "MainWindowPbx");
            this.MainWindowPbx.Name = "MainWindowPbx";
            this.MainWindowPbx.TabStop = false;
            this.MainWindowPbx.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindowPbx_Paint);
            this.MainWindowPbx.MouseLeave += new System.EventHandler(this.MainWindowPbx_MouseLeave);
            this.MainWindowPbx.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindowPbx_MouseMove);
            // 
            // Playtmr
            // 
            this.Playtmr.Interval = 33;
            this.Playtmr.Tick += new System.EventHandler(this.Playtmr_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(127)))), ((int)(((byte)(255)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.Menubtn);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Name = "label2";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::TEST.Properties.Resources.cctv;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // Menubtn
            // 
            this.Menubtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(127)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.Menubtn, "Menubtn");
            this.Menubtn.FlatAppearance.BorderSize = 0;
            this.Menubtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Menubtn.Image = global::TEST.Properties.Resources.list;
            this.Menubtn.Name = "Menubtn";
            this.Menubtn.UseVisualStyleBackColor = false;
            this.Menubtn.Click += new System.EventHandler(this.Menubtn_Click);
            // 
            // Infopanel
            // 
            this.Infopanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Infopanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.Infopanel, "Infopanel");
            this.Infopanel.Name = "Infopanel";
            // 
            // MenuPanel
            // 
            this.MenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(23)))), ((int)(((byte)(29)))));
            this.MenuPanel.Controls.Add(this.Settingbtn);
            this.MenuPanel.Controls.Add(this.Homebtn);
            resources.ApplyResources(this.MenuPanel, "MenuPanel");
            this.MenuPanel.Name = "MenuPanel";
            // 
            // Settingbtn
            // 
            this.Settingbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(23)))), ((int)(((byte)(29)))));
            resources.ApplyResources(this.Settingbtn, "Settingbtn");
            this.Settingbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(23)))), ((int)(((byte)(29)))));
            this.Settingbtn.FlatAppearance.BorderSize = 5;
            this.Settingbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.Settingbtn.Image = global::TEST.Properties.Resources.settings;
            this.Settingbtn.Name = "Settingbtn";
            this.Settingbtn.UseVisualStyleBackColor = false;
            this.Settingbtn.Click += new System.EventHandler(this.Settingbtn_Click);
            // 
            // Homebtn
            // 
            this.Homebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(23)))), ((int)(((byte)(29)))));
            resources.ApplyResources(this.Homebtn, "Homebtn");
            this.Homebtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(23)))), ((int)(((byte)(29)))));
            this.Homebtn.FlatAppearance.BorderSize = 5;
            this.Homebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.Homebtn.Image = global::TEST.Properties.Resources.home_icon_silhouette;
            this.Homebtn.Name = "Homebtn";
            this.Homebtn.UseVisualStyleBackColor = false;
            this.Homebtn.Click += new System.EventHandler(this.Homebtn_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.TempCalbtn);
            this.panel3.Controls.Add(this.Capturebtn);
            this.panel3.Controls.Add(this.Recordbtn);
            this.panel3.Controls.Add(this.Playbtn);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // TempCalbtn
            // 
            this.TempCalbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            resources.ApplyResources(this.TempCalbtn, "TempCalbtn");
            this.TempCalbtn.FlatAppearance.BorderSize = 0;
            this.TempCalbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.TempCalbtn.Image = global::TEST.Properties.Resources.aim__1_;
            this.TempCalbtn.Name = "TempCalbtn";
            this.TempCalbtn.UseVisualStyleBackColor = false;
            this.TempCalbtn.Click += new System.EventHandler(this.TempCalbtn_Click);
            // 
            // Capturebtn
            // 
            this.Capturebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Capturebtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.Capturebtn, "Capturebtn");
            this.Capturebtn.FlatAppearance.BorderSize = 0;
            this.Capturebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.Capturebtn.Image = global::TEST.Properties.Resources.camera__1_;
            this.Capturebtn.Name = "Capturebtn";
            this.Capturebtn.UseVisualStyleBackColor = false;
            this.Capturebtn.Click += new System.EventHandler(this.Capturebtn_Click);
            // 
            // Recordbtn
            // 
            this.Recordbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            resources.ApplyResources(this.Recordbtn, "Recordbtn");
            this.Recordbtn.FlatAppearance.BorderSize = 0;
            this.Recordbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.Recordbtn.Image = global::TEST.Properties.Resources.record;
            this.Recordbtn.Name = "Recordbtn";
            this.Recordbtn.UseVisualStyleBackColor = false;
            this.Recordbtn.Click += new System.EventHandler(this.Recordbtn_Click);
            // 
            // Playbtn
            // 
            this.Playbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Playbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.Playbtn, "Playbtn");
            this.Playbtn.FlatAppearance.BorderSize = 0;
            this.Playbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.Playbtn.Image = global::TEST.Properties.Resources.play__3_;
            this.Playbtn.Name = "Playbtn";
            this.Playbtn.UseVisualStyleBackColor = false;
            this.Playbtn.Click += new System.EventHandler(this.Playbtn_Click);
            // 
            // Menutmr
            // 
            this.Menutmr.Interval = 10;
            this.Menutmr.Tick += new System.EventHandler(this.Menutmr_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(8)))), ((int)(((byte)(22)))));
            this.Controls.Add(this.Mainpanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.Infopanel);
            this.Controls.Add(this.MenuPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Mainpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainWindowPbx)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.MenuPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel Mainpanel;
        private System.Windows.Forms.Button Playbtn;
        private System.Windows.Forms.Timer Playtmr;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button Recordbtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button TempCalbtn;
        private System.Windows.Forms.Panel Infopanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button Capturebtn;
        private System.Windows.Forms.Button Menubtn;
        private System.Windows.Forms.Timer Menutmr;
        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.Button Settingbtn;
        private System.Windows.Forms.Button Homebtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.PictureBox MainWindowPbx;
    }
}

