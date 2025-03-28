using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace OWB.MCDemo
{
    public partial class MainForm : Form
    {
        private OWBCamera _Camera1;
        private OWBCamera _Camera2;
        private OWBCamera _Camera3;
        private OWBCamera _Camera4;

        private Thread _TempAcqThread1;
        private bool _TempAcqDone1;
        private Thread _TempAcqThread2;
        private bool _TempAcqDone2;
        private Thread _TempAcqThread3;
        private bool _TempAcqDone3;
        private Thread _TempAcqThread4;
        private bool _TempAcqDone4;

        private object TimerLock1 = new object();
        private object TimerLock2 = new object();
        private object TimerLock3 = new object();
        private object TimerLock4 = new object();

        public MainForm()
        {
            InitializeComponent();
            Camera1 = new OWBCamera();
            Camera2 = new OWBCamera();
            Camera3 = new OWBCamera();
            Camera4 = new OWBCamera();
        }

        public OWBCamera Camera1
        {
            get { return _Camera1; }
            set { _Camera1 = value; }
        }

        public OWBCamera Camera2
        {
            get { return _Camera2; }
            set { _Camera2 = value; }
        }

        public OWBCamera Camera3
        {
            get { return _Camera3; }
            set { _Camera3 = value; }
        }

        public OWBCamera Camera4
        {
            get { return _Camera4; }
            set { _Camera4 = value; }
        }

        public Thread TempAcqThread1
        {
            get { return _TempAcqThread1; }
            set { _TempAcqThread1 = value; }
        }

        public Thread TempAcqThread2
        {
            get { return _TempAcqThread2; }
            set { _TempAcqThread2 = value; }
        }

        public Thread TempAcqThread3
        {
            get { return _TempAcqThread3; }
            set { _TempAcqThread3 = value; }
        }

        public Thread TempAcqThread4
        {
            get { return _TempAcqThread4; }
            set { _TempAcqThread4 = value; }
        }

        public bool TempAcqDone1
        {
            get { return _TempAcqDone1; }
            set { _TempAcqDone1 = value; }
        }

        public bool TempAcqDone2
        {
            get { return _TempAcqDone2; }
            set { _TempAcqDone2 = value; }
        }

        public bool TempAcqDone3
        {
            get { return _TempAcqDone3; }
            set { _TempAcqDone3 = value; }
        }

        public bool TempAcqDone4
        {
            get { return _TempAcqDone4; }
            set { _TempAcqDone4 = value; }
        }

        private void btnConnect1_Click(object sender, EventArgs e)
        {
            if (Camera1.IsConnected)
            {
                Camera1.LogoutCamera();
                TempAcqDone1 = true;
                if (TempAcqThread1 != null)
                {
                    TempAcqThread1.Interrupt();
                    TempAcqThread1.Join();
                    TempAcqThread1 = null;
                }
                btnConnect1.Text = "连接";
            }
            else
            {
                if (Connect(Camera1, tbIPAddress1.Text.Trim(), string.Empty, string.Empty))
                {
                    RefreshCamera1();
                    TempAcqDone1 = false;
                    TempAcqThread1 = new Thread(new ParameterizedThreadStart(TempAcqCallback1));
                    TempAcqThread1.Priority = ThreadPriority.Lowest;
                    TempAcqThread1.Start(Camera1);
                    btnConnect1.Text = "断开";
                }
            }
        }

        private void RefreshCamera1()
        {
            string[] caliModes = Camera1.GetCaliModes();
            cbCaliMode1.Items.Clear();
            for (int i = 0; i < caliModes.Length; i++)
            {
                cbCaliMode1.Items.Add(caliModes[i]);
            }
            string caliMode = Camera1.GetCurrentCaliMode();
            cbCaliMode1.SelectedItem = caliMode;
        }

        private void btnAgcMode1_Click(object sender, EventArgs e)
        {
            Camera1.PutIspAGCMode();
        }

        private void btnCali1_Click(object sender, EventArgs e)
        {
            Camera1.PostCali();
        }

        private void btnConnect2_Click(object sender, EventArgs e)
        {
            if (Camera2.IsConnected)
            {
                Camera2.LogoutCamera();
                TempAcqDone2 = true;
                if (TempAcqThread2 != null)
                {
                    TempAcqThread2.Interrupt();
                    TempAcqThread2.Join();
                    TempAcqThread2 = null;
                }
                btnConnect2.Text = "连接";
            }
            else
            {
                if (Connect(Camera2, tbIPAddress2.Text.Trim(), string.Empty, string.Empty))
                {
                    RefreshCamera2();
                    TempAcqDone2 = false;
                    TempAcqThread2 = new Thread(new ParameterizedThreadStart(TempAcqCallback2));
                    TempAcqThread2.Priority = ThreadPriority.Lowest;
                    TempAcqThread2.Start(Camera2);
                    btnConnect2.Text = "断开";
                }
            }
        }

        private void RefreshCamera2()
        {
            string[] caliModes = Camera2.GetCaliModes();
            cbCaliMode2.Items.Clear();
            for (int i = 0; i < caliModes.Length; i++)
            {
                cbCaliMode2.Items.Add(caliModes[i]);
            }
            string caliMode = Camera2.GetCurrentCaliMode();
            cbCaliMode2.SelectedItem = caliMode;
        }

        private void btnAgcMode2_Click(object sender, EventArgs e)
        {
            Camera2.PutIspAGCMode();
        }

        private void btnCali2_Click(object sender, EventArgs e)
        {
            Camera2.PostCali();
        }

        private void btnConnect3_Click(object sender, EventArgs e)
        {
            if (Camera3.IsConnected)
            {
                Camera3.LogoutCamera();
                TempAcqDone3 = true;
                if (TempAcqThread3 != null)
                {
                    TempAcqThread3.Interrupt();
                    TempAcqThread3.Join();
                    TempAcqThread3 = null;
                }
                btnConnect3.Text = "连接";
            }
            else
            {
                if (Connect(Camera3, tbIPAddress3.Text.Trim(), string.Empty, string.Empty))
                {
                    RefreshCamera3();
                    TempAcqDone3 = false;
                    TempAcqThread3 = new Thread(new ParameterizedThreadStart(TempAcqCallback3));
                    TempAcqThread3.Priority = ThreadPriority.Lowest;
                    TempAcqThread3.Start(Camera3);
                    btnConnect3.Text = "断开";
                }
            }
        }

        private void RefreshCamera3()
        {
            string[] caliModes = Camera3.GetCaliModes();
            cbCaliMode3.Items.Clear();
            for (int i = 0; i < caliModes.Length; i++)
            {
                cbCaliMode3.Items.Add(caliModes[i]);
            }
            string caliMode = Camera3.GetCurrentCaliMode();
            cbCaliMode3.SelectedItem = caliMode;
        }

        private void btnAgcMode3_Click(object sender, EventArgs e)
        {
            Camera3.PutIspAGCMode();
        }

        private void btnCali3_Click(object sender, EventArgs e)
        {
            Camera3.PostCali();
        }

        private void btnConnect4_Click(object sender, EventArgs e)
        {
            if (Camera4.IsConnected)
            {
                Camera4.LogoutCamera();
                TempAcqDone4 = true;
                if (TempAcqThread4 != null)
                {
                    TempAcqThread4.Interrupt();
                    TempAcqThread4.Join();
                    TempAcqThread4 = null;
                }
                btnConnect4.Text = "连接";
            }
            else
            {
                if (Connect(Camera4, tbIPAddress4.Text.Trim(), string.Empty, string.Empty))
                {
                    RefreshCamera4();
                    TempAcqDone4 = false;
                    TempAcqThread4 = new Thread(new ParameterizedThreadStart(TempAcqCallback4));
                    TempAcqThread4.Priority = ThreadPriority.Lowest;
                    TempAcqThread4.Start(Camera4);
                    btnConnect4.Text = "断开";
                }
            }
        }

        private void RefreshCamera4()
        {
            string[] caliModes = Camera4.GetCaliModes();
            cbCaliMode4.Items.Clear();
            for (int i = 0; i < caliModes.Length; i++)
            {
                cbCaliMode4.Items.Add(caliModes[i]);
            }
            string caliMode = Camera4.GetCurrentCaliMode();
            cbCaliMode4.SelectedItem = caliMode;
        }

        private void btnAgcMode4_Click(object sender, EventArgs e)
        {
            Camera4.PutIspAGCMode();
        }

        private void btnCali4_Click(object sender, EventArgs e)
        {
            Camera4.PostCali();
        }

        private bool Connect(OWBCamera camera, string conn, string userName, string password)
        {
            Cursor = Cursors.WaitCursor;
            if (camera.LoginCamera(conn, userName, password))
            {
                if (camera.StartStream(LoginType.H264, StreamType.PRI))
                {
                    Cursor = Cursors.Default;
                    return true;
                }
            }
            else
            {
                MessageBox.Show("连接失败", "连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor = Cursors.Default;
            return false;
        }

        private void TempAcqCallback1(object obj)
        {
            while (!TempAcqDone1)
            {
                try
                {
                    if (Monitor.TryEnter(TimerLock1))
                    {
                        try
                        {
                            OWBCamera camera = obj as OWBCamera;
                            Image image = null;
                            if (camera.IsConnected)
                            {
                                image = camera.ChannelImage;
                                pbCamera1.Image = image;
                            }
                        }
                        finally
                        {
                            Monitor.Exit(TimerLock1);
                        }
                        Thread.Sleep(40);
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void TempAcqCallback2(object obj)
        {
            while (!TempAcqDone2)
            {
                try
                {
                    if (Monitor.TryEnter(TimerLock2))
                    {
                        try
                        {
                            OWBCamera camera = obj as OWBCamera;
                            Image image = null;
                            if (camera.IsConnected)
                            {
                                image = camera.ChannelImage;
                                pbCamera2.Image = image;
                            }
                        }
                        finally
                        {
                            Monitor.Exit(TimerLock2);
                        }
                        Thread.Sleep(40);
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void TempAcqCallback3(object obj)
        {
            while (!TempAcqDone3)
            {
                try
                {
                    if (Monitor.TryEnter(TimerLock3))
                    {
                        try
                        {
                            OWBCamera camera = obj as OWBCamera;
                            Image image = null;
                            if (camera.IsConnected)
                            {
                                image = camera.ChannelImage;
                                pbCamera3.Image = image;
                            }
                        }
                        finally
                        {
                            Monitor.Exit(TimerLock3);
                        }
                        Thread.Sleep(40);
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void TempAcqCallback4(object obj)
        {
            while (!TempAcqDone4)
            {
                try
                {
                    if (Monitor.TryEnter(TimerLock4))
                    {
                        try
                        {
                            OWBCamera camera = obj as OWBCamera;
                            Image image = null;
                            if (camera.IsConnected)
                            {
                                image = camera.ChannelImage;
                                pbCamera4.Image = image;
                            }
                        }
                        finally
                        {
                            Monitor.Exit(TimerLock4);
                        }
                        Thread.Sleep(40);
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void btnblackLevelMinus1_Click(object sender, EventArgs e)
        {
            Camera1.BlackLevelMinus();
        }

        private void btnblackLevelPlus1_Click(object sender, EventArgs e)
        {
            Camera1.BlackLevelPlus();
        }

        private void btnContrastMinus1_Click(object sender, EventArgs e)
        {
            Camera1.ContrastMinus();
        }

        private void btnContrastPlus1_Click(object sender, EventArgs e)
        {
            Camera1.ContrastPlus();
        }

        private void btnblackLevelMinus2_Click(object sender, EventArgs e)
        {
            Camera2.BlackLevelMinus();
        }

        private void btnblackLevelPlus2_Click(object sender, EventArgs e)
        {
            Camera2.BlackLevelPlus();
        }

        private void btnContrastMinus2_Click(object sender, EventArgs e)
        {
            Camera2.ContrastMinus();
        }

        private void btnContrastPlus2_Click(object sender, EventArgs e)
        {
            Camera2.ContrastPlus();
        }

        private void btnblackLevelMinus3_Click(object sender, EventArgs e)
        {
            Camera3.BlackLevelMinus();
        }

        private void btnblackLevelPlus3_Click(object sender, EventArgs e)
        {
            Camera3.BlackLevelPlus();
        }

        private void btnContrastMinus3_Click(object sender, EventArgs e)
        {
            Camera3.ContrastMinus();
        }

        private void btnContrastPlus3_Click(object sender, EventArgs e)
        {
            Camera3.ContrastPlus();
        }

        private void btnblackLevelMinus4_Click(object sender, EventArgs e)
        {
            Camera4.BlackLevelMinus();
        }

        private void btnblackLevelPlus4_Click(object sender, EventArgs e)
        {
            Camera4.BlackLevelPlus();
        }

        private void btnContrastMinus4_Click(object sender, EventArgs e)
        {
            Camera4.ContrastMinus();
        }

        private void btnContrastPlus4_Click(object sender, EventArgs e)
        {
            Camera4.ContrastPlus();
        }

        private void cbCaliMode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Camera1.PutManualCaliMode(cbCaliMode1.SelectedItem.ToString());
        }

        private void cbCaliMode2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Camera2.PutManualCaliMode(cbCaliMode2.SelectedItem.ToString());
        }

        private void cbCaliMode3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Camera3.PutManualCaliMode(cbCaliMode3.SelectedItem.ToString());
        }

        private void cbCaliMode4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Camera4.PutManualCaliMode(cbCaliMode4.SelectedItem.ToString());
        }
    }
}
