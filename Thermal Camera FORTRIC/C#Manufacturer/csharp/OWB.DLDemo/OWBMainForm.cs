using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using SDK;
using System.Threading;
using System.IO;
using System.Net;

namespace OWB.DLDemo
{
    public partial class OWBMainForm : Form
    {
        private object TimerLock = new object();
        private LoginType _CameraLoginType;
        private StreamType _CameraStreamType;

        private static string DefaultDirectory = string.Empty;

        private int _CameraWidth;
        private int _CameraHeight;
        private float _WidthRatio;
        private float _HeightRatio;

        private bool _isOpenFile;

        private Point _MouseHoverPoint;
        private bool _TempAcqDone;
        private Thread _TempAcqThread;

        private bool _SnapThreadDone;
        private Thread _SnapThread;

        public OWBMainForm()
        {
            InitializeComponent();

            IP = string.Empty;

            OWBGlobal.Camera = new OWBCamera();
            rbRaw.Checked = true;
            MouseHoverPoint = new Point(-1, -1);

            IsOpenFile = false;

            TempAcqDone = true;
            TempAcqThread = null;

            RecordDone = true;
            RecordThread = null;

            SnapThreadDone = true;
            SnapThread = null;
            DefaultDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "SnapIR");
        }

        private LoginType CameraLoginType
        {
            get { return _CameraLoginType; }
            set
            {
                _CameraLoginType = value;
                RefreshPanelLayout();
            }
        }

        private StreamType CameraStreamType
        {
            get { return _CameraStreamType; }
            set
            {
                _CameraStreamType = value;
            }
        }

        private int CameraWidth
        {
            get { return _CameraWidth; }
            set
            {
                _CameraWidth = value;
                WidthRatio = (float)pbCamera.Width / _CameraWidth;
            }
        }

        private int CameraHeight
        {
            get { return _CameraHeight; }
            set
            {
                _CameraHeight = value;
                HeightRatio = (float)pbCamera.Height / _CameraHeight;
            }
        }

        private float WidthRatio
        {
            get { return _WidthRatio; }
            set { _WidthRatio = value; }
        }

        private float HeightRatio
        {
            get { return _HeightRatio; }
            set { _HeightRatio = value; }
        }

        private bool IsOpenFile
        {
            get { return _isOpenFile; }
            set
            {
                _isOpenFile = value;
                if (_isOpenFile)
                {
                    tsmiOpenFile.Text = "关闭";
                }
                else
                {
                    tsmiOpenFile.Text = "打开";
                }
                RefreshPanelLayout();
            }
        }

        private Point MouseHoverPoint
        {
            get { return _MouseHoverPoint; }
            set
            {
                if (_MouseHoverPoint != value)
                {
                    _MouseHoverPoint = value;
                }
            }
        }

        public bool TempAcqDone
        {
            get { return _TempAcqDone; }
            set { _TempAcqDone = value; }
        }

        public Thread TempAcqThread
        {
            get { return _TempAcqThread; }
            set { _TempAcqThread = value; }
        }

        public bool RecordDone { get; set; }

        public Thread RecordThread { get; set; }

        public string IP
        {
            get { return tbIPAddress.Text; }
            set
            {
                tbIPAddress.Text = value;
            }
        }

        private bool SnapThreadDone
        {
            get { return _SnapThreadDone; }
            set { _SnapThreadDone = value; }
        }

        private Thread SnapThread
        {
            get { return _SnapThread; }
            set { _SnapThread = value; }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!DisConnect())
            {
                if (tbIPAddress.Text == string.Empty || tbIPAddress.Text == null)
                {
                    tbIPAddress.Focus();
                    ttMainForm.Show("地址不能为空。", tbIPAddress, 0, -15, 1000);
                    return;
                }
                Cursor = Cursors.WaitCursor;
                if (!OWBGlobal.Camera.LoginCamera(tbIPAddress.Text, tbUserName.Text, tbPassword.Text))
                {
                    MessageBox.Show("连接失败", "连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor = Cursors.Default;
            }
            RefreshCamera();
        }

        private bool DisConnect()
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                OWBGlobal.Camera.LogoutCamera();
                tMainForm.Enabled = false;
                TempAcqDone = true;
                if (TempAcqThread != null)
                {
                    TempAcqThread.Interrupt();
                    TempAcqThread.Join();
                    TempAcqThread = null;
                }
                RecordDone = true;
                if (RecordThread != null)
                {
                    RecordThread.Interrupt();
                    RecordThread.Join();
                    RecordThread = null;
                }
                return true;
            }
            return false;
        }

        public void RefreshOnCloseConnect()
        {
            DisConnect();
            RefreshCamera();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                if (OWBGlobal.Camera.IsPlay)
                {
                    OWBGlobal.Camera.StopStream();
                    tMainForm.Enabled = false;
                    TempAcqDone = true;
                    if (TempAcqThread != null)
                    {
                        TempAcqThread.Interrupt();
                        TempAcqThread.Join();
                        TempAcqThread = null;
                    }
                }
                else
                {
                    if (OWBGlobal.Camera.StartStream(CameraLoginType, CameraStreamType))
                    {
                        tMainForm.Enabled = true;
                        TempAcqDone = false;
                        TempAcqThread = new Thread(TempAcqCallback);
                        TempAcqThread.Priority = ThreadPriority.Lowest;
                        TempAcqThread.Start();
                    }
                }
                RefreshCamera();
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                if (OWBGlobal.Camera.IsRecord)
                {
                    OWBGlobal.Camera.StopRecordStream();
                    RecordDone = true;
                    if (RecordThread != null)
                    {
                        RecordThread.Interrupt();
                        RecordThread.Join();
                        RecordThread = null;
                    }
                }
                else
                {
                    SaveFileDialog sfd = new SaveFileDialog();

                    sfd.Filter = "视频文件(*.mp4) | *.mp4";
                    if (sfd.ShowDialog(this) == DialogResult.OK)
                    {
                        if (OWBGlobal.Camera.StartRecordStream(sfd.FileName))
                        {
                            RecordDone = false;
                            RecordThread = new Thread(RecordCallback);
                            RecordThread.Priority = ThreadPriority.Lowest;
                            RecordThread.Start();
                        }
                    }
                }
                RefreshCamera();
            }
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            OWBAboutForm aboutForm = new OWBAboutForm();
            aboutForm.ShowDialog();
        }

        private void tMainForm_Tick(object sender, EventArgs e)
        {
            if (IsOpenFile)
            {
                return;
            }

            if (Monitor.TryEnter(TimerLock))
            {
                try
                {
                    Image image = null;
                    if (OWBGlobal.Camera.IsConnected)
                    {
                        if (CameraLoginType == LoginType.H264)
                        {
                            OWBGlobal.Camera.GetH264Frame(out image);
                        }
                        else
                        {
                            ushort[,] values = null;
                            if (OWBGlobal.Camera.GetRawFrame(out values))
                            {
                                image = OWBGlobal.Camera.ToImage(cbPalette.Text.ToString(), values);
                            }
                            float[,] temperatureValues = null;
                            OWBGlobal.Camera.GetTemperatureFrame(out temperatureValues);
                        }

                        pbCamera.Image = image;
                    }
                }
                finally
                {
                    Monitor.Exit(TimerLock);
                }
            }
        }

        private void OWBMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisConnect();
        }

        private string SnapPicture(SnapType snapType)
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                try
                {
                    if (Directory.Exists(DefaultDirectory) == false)
                    {
                        Directory.CreateDirectory(DefaultDirectory);
                    }
                    switch (snapType)
                    {
                        case SnapType.ISP:
                        case SnapType.OSD:
                            string fileName = Path.Combine(DefaultDirectory, "IR_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp");
                            Bitmap bmp = new Bitmap(OWBGlobal.Camera.Width, OWBGlobal.Camera.Height);
                            byte[] buf = OWBGlobal.Camera.Snapshot(snapType);
                            bmp = OWBGlobal.Camera.CreateBitmap(buf);
                            bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                            return fileName;
                        case SnapType.T:
                            fileName = Path.Combine(DefaultDirectory, "IR_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".p7");
                            buf = OWBGlobal.Camera.Snapshot(snapType);
                            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                            {
                                fileStream.Write(buf, 0, buf.Length);
                                return fileName;
                            }
                    }
                }
                catch
                {

                }
            }
            return null;
        }

        private void btnRawSnapshot_Click(object sender, EventArgs e)
        {
            if (tbIPAddress.Text == string.Empty || tbIPAddress.Text == null)
            {
                tbIPAddress.Focus();
                ttMainForm.Show("地址不能为空。", tbIPAddress, 0, -15, 1000);
                return;
            }

            if (DefaultDirectory == string.Empty)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "请选择快照默认保存文件夹";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(fbd.SelectedPath))
                    {
                        MessageBox.Show(this, "文件夹路径不能为空", "提示");
                        return;
                    }
                    DefaultDirectory = fbd.SelectedPath;
                }
            }
            string fileName = SnapPicture(SnapType.ISP);
            MessageBox.Show("快照保存位置：" + fileName);
        }

        private void btnH264Snapshot_Click(object sender, EventArgs e)
        {
            if (tbIPAddress.Text == string.Empty || tbIPAddress.Text == null)
            {
                tbIPAddress.Focus();
                ttMainForm.Show("地址不能为空。", tbIPAddress, 0, -15, 1000);
                return;
            }

            if (DefaultDirectory == string.Empty)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "请选择快照默认保存文件夹";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(fbd.SelectedPath))
                    {
                        MessageBox.Show(this, "文件夹路径不能为空", "提示");
                        return;
                    }
                    DefaultDirectory = fbd.SelectedPath;
                }
            }
            string fileName = SnapPicture(SnapType.OSD);
            MessageBox.Show("快照保存位置：" + fileName);
        }

        private void btntSnapshot_Click(object sender, EventArgs e)
        {
            if (tbIPAddress.Text == string.Empty || tbIPAddress.Text == null)
            {
                tbIPAddress.Focus();
                ttMainForm.Show("地址不能为空。", tbIPAddress, 0, -15, 1000);
                return;
            }

            if (DefaultDirectory == string.Empty)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "请选择快照默认保存文件夹";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(fbd.SelectedPath))
                    {
                        MessageBox.Show(this, "文件夹路径不能为空", "提示");
                        return;
                    }
                    DefaultDirectory = fbd.SelectedPath;
                }
            }
            string fileName = SnapPicture(SnapType.T);
            MessageBox.Show("快照保存位置：" + fileName);
        }

        private void btnAutoFocus_Click(object sender, EventArgs e)
        {
            if (tbIPAddress.Text == string.Empty || tbIPAddress.Text == null)
            {
                tbIPAddress.Focus();
                ttMainForm.Show("地址不能为空。", tbIPAddress, 0, -15, 1000);
                return;
            }
            OWBGlobal.Camera.PostIspAF();
        }

        private void btnNearFocus_Click(object sender, EventArgs e)
        {
            if (tbIPAddress.Text == string.Empty || tbIPAddress.Text == null)
            {
                tbIPAddress.Focus();
                ttMainForm.Show("地址不能为空。", tbIPAddress, 0, -15, 1000);
                return;
            }
            OWBGlobal.Camera.PostNearFocus();
        }

        private void btnFarFocus_Click(object sender, EventArgs e)
        {
            if (tbIPAddress.Text == string.Empty || tbIPAddress.Text == null)
            {
                tbIPAddress.Focus();
                ttMainForm.Show("地址不能为空。", tbIPAddress, 0, -15, 1000);
                return;
            }
            OWBGlobal.Camera.PostFarFocus();
        }

        private void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            if (IsOpenFile)
            {
                IsOpenFile = false;
                pbCamera.Image = null;
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "打开温度图快照";
                ofd.Multiselect = false;
                ofd.Filter = "温度图快照文件(*.p7)|*.p7";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream fileStream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            int length = (int)fileStream.Length;
                            byte[] buffer = new byte[length];
                            fileStream.Read(buffer, 0, length);
                            using (BinaryReader br = new BinaryReader(new MemoryStream(buffer)))
                            {
                                br.BaseStream.Position = 2;
                                int width = (int)IPAddress.HostToNetworkOrder(br.ReadInt16());
                                int height = (int)IPAddress.HostToNetworkOrder(br.ReadInt16());
                                int depth = br.ReadByte();
                                int type = br.ReadByte();
                                uint linesize = br.ReadUInt32();

                                br.BaseStream.Position = OWBCamera.HEADERLENGTH;
                                float[,] tValues = new float[width, height];
                                for (int i = 0; i < height; i++)
                                {
                                    for (int j = 0; j < width; j++)
                                    {
                                        int t_src = br.ReadInt16();
                                        int t_interger = (t_src & 65528) >> 3;
                                        float t_float = (float)(t_src & 7) / 8;
                                        tValues[j, i] = t_interger + t_float;
                                    }
                                }
                                OWBGlobal.Camera.TValues = tValues;
                            }
                        }
                        IsOpenFile = true;
                    }
                    catch
                    {

                    }
                }
            }
            RefreshFile();
        }

        private void rbH264_CheckedChanged(object sender, EventArgs e)
        {
            if (rbH264.Checked)
            {
                CameraLoginType = LoginType.H264;
                RefreshCamera();
                if (OWBGlobal.Camera.IsPlay)
                {
                    OWBGlobal.Camera.RefreshStream(CameraLoginType, CameraStreamType);
                }
            }
        }

        private void rbRaw_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRaw.Checked)
            {
                CameraLoginType = LoginType.RAW;
                RefreshCamera();
                if (OWBGlobal.Camera.IsPlay)
                {
                    OWBGlobal.Camera.RefreshStream(CameraLoginType, CameraStreamType);
                }
            }
        }

        private void rbPri_CheckedChanged(object sender, EventArgs e)
        {
            CameraStreamType = StreamType.PRI;
            if (OWBGlobal.Camera.IsPlay)
            {
                OWBGlobal.Camera.RefreshStream(CameraLoginType, CameraStreamType);
            }
        }

        private void rbSub_CheckedChanged(object sender, EventArgs e)
        {
            CameraStreamType = StreamType.SUB;
            if (OWBGlobal.Camera.IsPlay)
            {
                OWBGlobal.Camera.RefreshStream(CameraLoginType, CameraStreamType);
            }
        }

        private void cbPalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                if (CameraLoginType == LoginType.H264)
                {
                    OWBGlobal.Camera.PutCurrentPlt(cbPalette.Text);
                }
            }
        }

        private void cbCapture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                OWBGlobal.Camera.PutCurrentCaptureMode(cbCapture.Text);
            }
        }

        private void ckShowPalette_CheckedChanged(object sender, EventArgs e)
        {
            OWBGlobal.Camera.PutPltVisible(ckShowPalette.Checked);
        }

        private void ckShowTitle_CheckedChanged(object sender, EventArgs e)
        {
            OWBGlobal.Camera.PutTitleVisible(ckShowTitle.Checked);
        }

        private void ckShowTime_CheckedChanged(object sender, EventArgs e)
        {
            OWBGlobal.Camera.PutTimeVisible(ckShowTime.Checked);
        }

        private void ckShowMaxMin_CheckedChanged(object sender, EventArgs e)
        {
            OWBGlobal.Camera.PutMaxMinVisible(ckShowMaxMin.Checked);
        }

        private void cbUnit_CheckedChanged(object sender, EventArgs e)
        {
            OWBGlobal.Camera.PutUnitVisible(cbUnit.Checked);
        }

        private void cbEmissivity_CheckedChanged(object sender, EventArgs e)
        {
            OWBGlobal.Camera.PutEmissivityVisible(cbEmissivity.Checked);
        }

        private void cbLens_SelectedIndexChanged(object sender, EventArgs e)
        {
            OWBGlobal.Camera.LensIndex = cbLens.SelectedIndex;
        }

        private void cbTemperatureRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            OWBGlobal.Camera.TRangeIndex = cbTemperatureRange.SelectedIndex;
        }

        private void cbFilePalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsOpenFile)
            {
                pbCamera.Image = OWBGlobal.Camera.ToImage(cbFilePalette.Text, OWBGlobal.Camera.TValues);
            }
        }

        private void pbCamera_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = new Point();
            p.X = (int)((float)e.X / WidthRatio);
            p.Y = (int)((float)e.Y / HeightRatio);
            MouseHoverPoint = p;
            if (IsOpenFile)
            {
                pbCamera.Invalidate();

                string temperature = OWBGlobal.Camera.TValues[MouseHoverPoint.X, MouseHoverPoint.Y].ToString("F1") + "℃";
                if (IsDisposed || !this.IsHandleCreated)
                    return;
                lblFileLocValue.BeginInvoke((MethodInvoker)delegate ()
                {
                    lblFileLocValue.Text = "(" + MouseHoverPoint.X + "," + MouseHoverPoint.Y + ")";
                });
                lblFileTempValue.BeginInvoke((MethodInvoker)delegate ()
                {
                    lblFileTempValue.Text = temperature;
                });
            }
        }

        private void pbCamera_MouseLeave(object sender, EventArgs e)
        {
            MouseHoverPoint = new Point(-1, -1);
        }

        private void pbCamera_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (tbIPAddress.Text == string.Empty || tbIPAddress.Text == null)
            {
                tbIPAddress.Focus();
                ttMainForm.Show("地址不能为空。", tbIPAddress, 0, -15, 1000);
                return;
            }
            OWBTypes.Pos pos = new OWBTypes.Pos();
            pos.X = (int)((float)e.X / WidthRatio);
            pos.Y = (int)((float)e.Y / HeightRatio);
            //OWBGlobal.Camera.PostIspAFXY(pos);
        }

        private void OWBMainForm_SizeChanged(object sender, EventArgs e)
        {
            if (OWBGlobal.Camera != null)
            {
                if (OWBGlobal.Camera.IsConnected)
                {
                    CameraWidth = OWBGlobal.Camera.Width;
                    CameraHeight = OWBGlobal.Camera.Height;
                }
                else
                {
                    if (IsOpenFile)
                    {
                        CameraWidth = OWBGlobal.Camera.TValues.GetLength(0);
                        CameraHeight = OWBGlobal.Camera.TValues.GetLength(1);
                    }
                }
            }
            else
            {
                if (IsOpenFile)
                {
                    CameraWidth = OWBGlobal.Camera.TValues.GetLength(0);
                    CameraHeight = OWBGlobal.Camera.TValues.GetLength(1);
                }
            }
        }

        private void pbCamera_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int rx = (int)(MouseHoverPoint.X * WidthRatio);
            int ry = (int)(MouseHoverPoint.Y * HeightRatio);
            g.DrawLine(Pens.White, rx, ry - 4, rx, ry + 4);
            g.DrawLine(Pens.White, rx - 4, ry, rx + 4, ry);
        }

        private void TempAcqCallback()
        {
            while (!TempAcqDone)
            {
                try
                {
                    Thread.Sleep(500);
                    RefreshTempAcq();
                }
                catch (Exception)
                {

                }
            }
        }

        private void RefreshTempAcq()
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                string temperature = OWBGlobal.Camera.GetIspTItem(MouseHoverPoint).ToString("F1") + " ℃";
                if (IsDisposed || !this.IsHandleCreated)
                    return;
                lblLocValue.BeginInvoke((MethodInvoker)delegate ()
                {
                    lblLocValue.Text = "(" + MouseHoverPoint.X + "," + MouseHoverPoint.Y + ")";
                });
                lblTempValue.BeginInvoke((MethodInvoker)delegate ()
                {
                    lblTempValue.Text = temperature;
                });
            }
        }

        private void RecordCallback()
        {
            while (!RecordDone)
            {
                try
                {
                    Thread.Sleep(500);
                    RefreshTempAcq();
                }
                catch (Exception)
                {

                }
            }
        }

        public void RefreshPanelLayout()
        {
            if (IsOpenFile)
            {
                pH264.Visible = false;
                pCommon.Visible = false;
                pFile.Visible = true;
                pFile.Dock = DockStyle.Fill;
            }
            else
            {
                switch (CameraLoginType)
                {
                    case LoginType.H264:
                        pH264.Visible = true;
                        pCommon.Visible = true;
                        pFile.Visible = false;
                        pH264.Dock = DockStyle.Top;
                        pCommon.Dock = DockStyle.Fill;
                        rbPri.Checked = true;
                        break;
                    case LoginType.RAW:
                        pH264.Visible = true;
                        pCommon.Visible = true;
                        pFile.Visible = false;
                        pH264.Dock = DockStyle.None;
                        pCommon.Dock = DockStyle.Fill;
                        break;
                }
            }
        }

        private void RefreshCamera()
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                CameraWidth = OWBGlobal.Camera.Width;
                CameraHeight = OWBGlobal.Camera.Height;

                cbPalette.Items.Clear();
                if (CameraLoginType == LoginType.RAW)
                {
                    int index = 0;
                    foreach (string paletteKey in OWBGlobal.Palettes.Keys)
                    {
                        cbPalette.Items.Add(paletteKey);
                        if (index == 0)
                        {
                            cbPalette.SelectedItem = paletteKey;
                        }
                        index++;
                    }
                }
                else
                {
                    Cursor = Cursors.WaitCursor;
                    string[] palettes = OWBGlobal.Camera.GetPalettes();
                    if (palettes != null)
                    {
                        foreach (string paletteKey in palettes)
                        {
                            cbPalette.Items.Add(paletteKey);
                        }
                        string pltName = OWBGlobal.Camera.GetCurrentPlt();
                        cbPalette.SelectedItem = pltName;
                    }
                    Cursor = Cursors.Default;
                }

                cbLens.Items.Clear();
                foreach (string lens in OWBGlobal.Camera.Lens)
                {
                    cbLens.Items.Add(lens);
                }
                cbLens.SelectedIndex = OWBGlobal.Camera.LensIndex;

                cbTemperatureRange.Items.Clear();
                foreach (TemperatureRange tRange in OWBGlobal.Camera.TRanges)
                {
                    cbTemperatureRange.Items.Add(tRange);
                }
                cbTemperatureRange.SelectedIndex = OWBGlobal.Camera.TRangeIndex;

                cbCapture.Items.Clear();
                string[] captureModes = OWBGlobal.Camera.GetCaptureModes();
                if (captureModes != null)
                {
                    foreach (string captureMode in captureModes)
                    {
                        cbCapture.Items.Add(captureMode);
                    }
                    string currentCaptureMode = OWBGlobal.Camera.GetCurrentCaptureMode();
                    cbCapture.SelectedItem = currentCaptureMode;
                }

                bool pltVisible = OWBGlobal.Camera.GetPltVisible();
                ckShowPalette.Checked = pltVisible;
                bool titleVisible = OWBGlobal.Camera.GetTitleVisible();
                ckShowTitle.Checked = titleVisible;
                bool timeVisible = OWBGlobal.Camera.GetTimeVisible();
                ckShowTime.Checked = timeVisible;
                bool maxminVisible = OWBGlobal.Camera.GetMaxMinVisible();
                ckShowMaxMin.Checked = maxminVisible;
                bool unitVisible = OWBGlobal.Camera.GetUnitVisible();
                cbUnit.Checked = unitVisible;
                bool emissivityVisible = OWBGlobal.Camera.GetEmissivityVisible();
                cbEmissivity.Checked = emissivityVisible;

                tsmiUserManager.Enabled = true;
                gbConnInfo.Enabled = false;
                gbConnMode.Enabled = true;
                pH264.Enabled = true;
                pCommon.Enabled = true;
                btnConnect.Text = "断开";
                btnPlay.Enabled = true;
                btnRecord.Enabled = true;
                tsslDeviceStatus.Text = "热像仪已连接：" + OWBGlobal.Camera.IP + "  固件版本：" + string.Join(".", Array.ConvertAll<int, string>(OWBGlobal.Camera.FirmwareVersion, delegate (int v) { return v.ToString(); }));
            }
            else
            {
                tsmiUserManager.Enabled = false;
                gbConnInfo.Enabled = true;
                gbConnMode.Enabled = false;
                pH264.Enabled = false;
                pCommon.Enabled = false;
                btnConnect.Text = "连接";
                btnPlay.Enabled = false;
                btnRecord.Enabled = false;
                tsslDeviceStatus.Text = "热像仪未连接";
            }

            if (OWBGlobal.Camera.IsPlay)
            {
                btnPlay.Text = "停止";
            }
            else
            {
                btnPlay.Text = "播放";
            }

            if (OWBGlobal.Camera.IsRecord)
            {
                btnRecord.Text = "停止";
            }
            else
            {
                btnRecord.Text = "录制";
            }
        }

        private void RefreshFile()
        {
            cbFilePalette.Items.Clear();
            if (IsOpenFile)
            {
                CameraWidth = OWBGlobal.Camera.TValues.GetLength(0);
                CameraHeight = OWBGlobal.Camera.TValues.GetLength(1);

                int index = 0;
                foreach (string paletteKey in OWBGlobal.Palettes.Keys)
                {
                    cbFilePalette.Items.Add(paletteKey);
                    if (index == 0)
                    {
                        cbFilePalette.SelectedItem = paletteKey;
                    }
                    index++;
                }

                pbCamera.Image = OWBGlobal.Camera.ToImage(cbFilePalette.Text, OWBGlobal.Camera.TValues);
                pFile.Enabled = true;
            }
            else
            {
                pFile.Enabled = false;
            }
        }

        private void tsmiUserManager_Click(object sender, EventArgs e)
        {
            OWBUserManageForm userManageForm = new OWBUserManageForm();
            userManageForm.ShowDialog();
        }

        private void btnSnap_Click(object sender, EventArgs e)
        {
            if (SnapThreadDone)
            {
                SnapThreadDone = false;
                SnapThread = new Thread(SnapCallback);
                SnapThread.Priority = ThreadPriority.Lowest;
                SnapThread.Start();
                btnSnap.Text = "取消";
                nudSnapInterval.Enabled = false;
            }
            else
            {
                SnapThreadDone = true;
                if (SnapThread != null)
                {
                    SnapThread.Interrupt();
                    SnapThread.Join();
                    SnapThread = null;
                }
                btnSnap.Text = "抓图";
                nudSnapInterval.Enabled = true;
            }

        }

        private void SnapCallback()
        {
            while (!SnapThreadDone)
            {
                try
                {
                    int interval = (int)(nudSnapInterval.Value * 1000);
                    Thread.Sleep(interval);
                    SnapPicture(SnapType.T);
                }
                catch (Exception)
                {

                }
            }
        }

        private Image Filter(Bitmap image)
        {
            Bitmap outImage = new Bitmap(image.Width, image.Height);
            int[,,] inputArray = new int[3, image.Width, image.Height];
            int[,,] outputArray = new int[3, image.Width, image.Height];
            Color color = new Color();
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    color = image.GetPixel(i, j);
                    inputArray[0, i, j] = outputArray[0, i, j] = color.R;
                    inputArray[1, i, j] = outputArray[1, i, j] = color.G;
                    inputArray[2, i, j] = outputArray[2, i, j] = color.B;
                }
            }

            for (int j = 0; j < image.Height - 3; j++)
            {
                for (int i = 0; i < image.Width - 3; i++)
                {
                    int maxR = 0, maxG = 0, maxB = 0;
                    int[,] tempArrayR = new int[3, 3];
                    int[,] tempArrayG = new int[3, 3];
                    int[,] tempArrayB = new int[3, 3];
                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            int temp = inputArray[0, i + x, j + y];
                            if (temp > maxR)
                            {
                                maxR = temp;
                            }
                            temp = inputArray[1, i + x, j + y];
                            if (temp > maxG)
                            {
                                maxG = temp;
                            }
                            temp = inputArray[2, i + x, j + y];
                            if (temp > maxB)
                            {
                                maxB = temp;
                            }
                        }
                    }
                    if (maxR > 200 && maxG > 200 && maxB > 200)
                    {
                        for (int x = 0; x < 3; x++)
                        {
                            for (int y = 0; y < 3; y++)
                            {
                                outputArray[0, i + x, j + y] = 255;
                                outputArray[1, i + x, j + y] = 255;
                                outputArray[2, i + x, j + y] = 255;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    color = Color.FromArgb(outputArray[0, i, j], outputArray[1, i, j], outputArray[2, i, j]);
                    outImage.SetPixel(i, j, color);
                }
            }
            return outImage;
        }
    }
}
