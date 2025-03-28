using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEST
{
    public partial class UserForm : Form
    {
        private Thread _MouseMonitorThread;

        public UserForm()
        {
            InitializeComponent();
            RefreshTemp();
        }
        private void RefreshTemp()
        {
            if (MainForm._MouseIsRunning)
            {
                _MouseMonitorThread = new Thread(RefreshTempAcq);
                _MouseMonitorThread.IsBackground = true; // 設定為背景執行緒
                _MouseMonitorThread.Start();
            }
          
        }
        private void RefreshTempAcq()
        {
            while (MainForm._MouseIsRunning)
            {
                Thread.Sleep(500);
                if (MainForm.MouseHoverPoint.X >= 0 && MainForm.MouseHoverPoint.Y >= 0)
                {
                    string temperature = OWBGlobal.Camera.GetIspTItem(MainForm.MouseHoverPoint).ToString("F1") + " ℃";
                    if (IsDisposed || !this.IsHandleCreated)
                        return;
                    lblLocValue.BeginInvoke((MethodInvoker)delegate ()
                    {
                        lblLocValue.Text = "(" + MainForm.MouseHoverPoint.X + "," + MainForm.MouseHoverPoint.Y + ")";
                    });
                    lblTempValue.BeginInvoke((MethodInvoker)delegate ()
                    {
                        lblTempValue.Text = temperature;
                    });
                }
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            RefreshTemp();
        }
    }
}
