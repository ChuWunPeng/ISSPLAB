using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SDK;

namespace OWB.TADemo
{
    public partial class OWBMainForm : Form
    {
        private const int COLUMN_MARKERNAME = 0;
        private const int COLUMN_MAXTEMP = 1;
        private const int COLUMN_MINTEMP = 2;
        private const int COLUMN_AVGTEMP = 3;

        private object TimerLock = new object();
        private object RefreshMarkerLock = new object();
        private string _IP;

        private int _CameraWidth;
        private int _CameraHeight;
        private float _WidthRatio;
        private float _HeightRatio;

        private Point _MouseHoverPoint;
        private bool _TempAcqDone;
        private Thread _TempAcqThread;

        public OWBMainForm()
        {
            InitializeComponent();
            OWBGlobal.Camera = new OWBCamera();

            MouseHoverPoint = new Point(-1, -1);

            TempAcqDone = true;
            TempAcqThread = null;
        }

        public string IP
        {
            get { return _IP; }
            set { _IP = value; }
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

        private void OWBMainForm_Shown(object sender, EventArgs e)
        {
            Form_Refresh();
        }

        private void tsmiConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            OWBAboutForm aboutForm = new OWBAboutForm();
            aboutForm.ShowDialog();
        }

        private void Connect()
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
            }
            else
            {
                OWBConnectForm connectForm = new OWBConnectForm();
                if (connectForm.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    if (OWBGlobal.Camera.LoginCamera(connectForm.ConnectString, connectForm.UserName, connectForm.Password))
                    {
                        if (OWBGlobal.Camera.StartStream())
                        {
                            tMainForm.Enabled = true;
                            TempAcqDone = false;
                            TempAcqThread = new Thread(TempAcqCallback);
                            TempAcqThread.Priority = ThreadPriority.Lowest;
                            TempAcqThread.Start();
                        }
                    }
                    else
                        MessageBox.Show("连接失败", "连接", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Cursor = Cursors.Default;
                }
            }

            Form_Refresh();
        }

        private void Form_Refresh()
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                CameraWidth = OWBGlobal.Camera.Width;
                CameraHeight = OWBGlobal.Camera.Height;

                tsmiSerialSet.Enabled = true;
                pConfig.Enabled = true;
                tsmiConnect.Text = "断开";
                tsslDeviceStatus.Text = "热像仪已连接：" + OWBGlobal.Camera.IP + "  固件版本：" + string.Join(".", Array.ConvertAll<int, string>(OWBGlobal.Camera.FirmwareVersion, delegate (int v) { return v.ToString(); }));
                RefreshTempParameter();
            }
            else
            {
                tsmiSerialSet.Enabled = false;
                pConfig.Enabled = false;
                tsmiConnect.Text = "连接";
                tsslDeviceStatus.Text = "热像仪未连接";
            }
        }

        private void tMainForm_Tick(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(TimerLock))
            {
                try
                {
                    Image image = null;
                    if (OWBGlobal.Camera.IsConnected)
                    {
                        image = OWBGlobal.Camera.ChannelImage;
                        pbCamera.Image = image;
                    }
                }
                finally
                {
                    Monitor.Exit(TimerLock);
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
                    Thread.Sleep(1000);
                    RefreshTempAcq();
                    RefreshMarker();
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
                string temp = OWBGlobal.Camera.GetIspTItem(MouseHoverPoint);
                if (IsDisposed || !this.IsHandleCreated)
                    return;
                else
                {
                    lblCurrentLoc.BeginInvoke((MethodInvoker)delegate ()
                    {
                        lblCurrentLoc.Text = "(" + MouseHoverPoint.X + "," + MouseHoverPoint.Y + ")";
                    });
                    lblCurrentTemp.BeginInvoke((MethodInvoker)delegate ()
                    {
                        lblCurrentTemp.Text = temp;
                    });
                }
            }
        }

        private void pbCamera_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = new Point();
            p.X = (int)((float)e.X / WidthRatio);
            p.Y = (int)((float)e.Y / HeightRatio);
            MouseHoverPoint = p;
        }

        private void pbCamera_MouseLeave(object sender, EventArgs e)
        {
            MouseHoverPoint = new Point(-1, -1);
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
            }
        }

        private bool SetTempParameter()
        {
            OWBTypes.InstrumentJconfig instrumentJconfig = new OWBTypes.InstrumentJconfig();
            instrumentJconfig.Atmosphere_t = (float)nudAtmosphericTemperatureValue.Value;
            instrumentJconfig.Emission = (float)nudEmissivityValue.Value;
            instrumentJconfig.Distance = (float)nudDistanceValue.Value;
            instrumentJconfig.RH = (float)nudRelativeHumidityValue.Value;
            instrumentJconfig.Reflection_t = (float)nudReflectedTemperatureValue.Value;
            instrumentJconfig.Lens_t = (float)nudLensTValue.Value;
            instrumentJconfig.Lens_transmission = (float)nudLensTransValue.Value;
            instrumentJconfig.Offset = (float)nudOffsetValue.Value;
            return OWBGlobal.Camera.PutInstrumentJconfig(instrumentJconfig);
        }

        private void RefreshTempParameter()
        {
            try
            {
                OWBTypes.InstrumentJconfig instrumentJconfig = OWBGlobal.Camera.GetInstrumentJconfig();
                nudAtmosphericTemperatureValue.Value = (decimal)instrumentJconfig.Atmosphere_t;
                nudEmissivityValue.Value = (decimal)instrumentJconfig.Emission;
                nudDistanceValue.Value = (decimal)instrumentJconfig.Distance;
                nudRelativeHumidityValue.Value = (decimal)instrumentJconfig.RH;
                nudReflectedTemperatureValue.Value = (decimal)instrumentJconfig.Reflection_t;
                nudLensTValue.Value = (decimal)instrumentJconfig.Lens_t;
                nudLensTransValue.Value = (decimal)instrumentJconfig.Lens_transmission;
                nudOffsetValue.Value = (decimal)instrumentJconfig.Offset;
            }
            catch
            { }
        }

        private void nudEmissivityValue_ValueChanged(object sender, EventArgs e)
        {
            if (SetTempParameter())
            {
                OWBTypes.InstrumentJconfig instrumentJconfig = OWBGlobal.Camera.GetInstrumentJconfig();
                nudEmissivityValue.Value = (decimal)instrumentJconfig.Emission;
            }
        }

        private void nudRelativeHumidityValue_ValueChanged(object sender, EventArgs e)
        {
            if (SetTempParameter())
            {
                OWBTypes.InstrumentJconfig instrumentJconfig = OWBGlobal.Camera.GetInstrumentJconfig();
                nudRelativeHumidityValue.Value = (decimal)instrumentJconfig.RH;
            }
        }

        private void nudReflectedTemperatureValue_ValueChanged(object sender, EventArgs e)
        {
            if (SetTempParameter())
            {
                OWBTypes.InstrumentJconfig instrumentJconfig = OWBGlobal.Camera.GetInstrumentJconfig();
                nudReflectedTemperatureValue.Value = (decimal)instrumentJconfig.Reflection_t;
            }
        }

        private void nudAtmosphericTemperatureValue_ValueChanged(object sender, EventArgs e)
        {
            if (SetTempParameter())
            {
                OWBTypes.InstrumentJconfig instrumentJconfig = OWBGlobal.Camera.GetInstrumentJconfig();
                nudAtmosphericTemperatureValue.Value = (decimal)instrumentJconfig.Atmosphere_t;
            }
        }

        private void nudDistanceValue_ValueChanged(object sender, EventArgs e)
        {
            bool setParamResult = SetTempParameter();
            bool compensationResult = OWBGlobal.Camera.CompensationDistance((float)nudDistanceValue.Value);
            if (setParamResult && compensationResult)
            {
                OWBTypes.InstrumentJconfig instrumentJconfig = OWBGlobal.Camera.GetInstrumentJconfig();
                nudDistanceValue.Value = (decimal)instrumentJconfig.Distance;
            }
        }

        private void nudLensTValue_ValueChanged(object sender, EventArgs e)
        {
            if (SetTempParameter())
            {
                OWBTypes.InstrumentJconfig instrumentJconfig = OWBGlobal.Camera.GetInstrumentJconfig();
                nudLensTValue.Value = (decimal)instrumentJconfig.Lens_t;
            }
        }

        private void nudLensTrans_ValueChanged(object sender, EventArgs e)
        {
            if (SetTempParameter())
            {
                OWBTypes.InstrumentJconfig instrumentJconfig = OWBGlobal.Camera.GetInstrumentJconfig();
                nudLensTransValue.Value = (decimal)instrumentJconfig.Lens_transmission;
            }
        }

        private void nudOffset_ValueChanged(object sender, EventArgs e)
        {
            if (SetTempParameter())
            {
                OWBTypes.InstrumentJconfig instrumentJconfig = OWBGlobal.Camera.GetInstrumentJconfig();
                nudOffsetValue.Value = (decimal)instrumentJconfig.Offset;
            }
        }

        private void OWBMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                OWBGlobal.Camera.LogoutCamera();
            }
            tMainForm.Enabled = false;
            TempAcqDone = true;
            if (TempAcqThread != null)
            {
                TempAcqThread.Interrupt();
                TempAcqThread.Join();
                TempAcqThread = null;
            }
        }

        private void RefreshMarker()
        {
            lock (RefreshMarkerLock)
            {
                dgvMarker.BeginInvoke((MethodInvoker)delegate ()
                {
                    if (OWBGlobal.Camera.TagMap == null)
                    {
                        return;
                    }
                    dgvMarker.Rows.Clear();
                    int index = -1;
                    index = dgvMarker.Rows.Add();

                    DataGridViewCellCollection cell0 = dgvMarker.Rows[index].Cells;
                    cell0[COLUMN_MARKERNAME].Value = "全局";
                    Dictionary<string, object> tagmap = OWBGlobal.Camera.TagMap;
                    if (tagmap.ContainsKey("max"))
                    {
                        try
                        {
                            OWBTypes.TMotionItem ti = OWBJson.parse<OWBTypes.TMotionItem>(tagmap["max"].ToString());
                            cell0[COLUMN_MAXTEMP].Value = ti.T.ToString("F1") + " ℃";
                        }
                        catch
                        { }
                    }

                    if (tagmap.ContainsKey("min"))
                    {
                        try
                        {
                            OWBTypes.TMotionItem ti = OWBJson.parse<OWBTypes.TMotionItem>(tagmap["min"].ToString());
                            cell0[COLUMN_MINTEMP].Value = ti.T.ToString("F1") + " ℃";
                        }
                        catch
                        { }
                    }

                    foreach (KeyValuePair<string, object> kv in OWBGlobal.Camera.TagMap)
                    {
                        if (kv.Key == "max" || kv.Key == "min")
                            continue;

                        index = dgvMarker.Rows.Add();
                        DataGridViewCellCollection cell = dgvMarker.Rows[index].Cells;
                        cell[COLUMN_MARKERNAME].Value = kv.Key;
                        if (kv.Value == null)
                        {
                            continue;
                        }
                        Dictionary<string, object> valueMap = OWBJson.parse(kv.Value.ToString());

                        try
                        {
                            if (valueMap.ContainsKey("max") || valueMap.ContainsKey("min") || valueMap.ContainsKey("avg"))
                            {
                                if (valueMap.ContainsKey("max"))
                                {
                                    OWBTypes.IspTItem ispTItem = OWBJson.parse<OWBTypes.IspTItem>(valueMap["max"].ToString());
                                    cell[COLUMN_MAXTEMP].Value = ispTItem.T.ToString("F1") + " ℃";
                                }
                                else
                                {
                                    cell[COLUMN_MAXTEMP].Value = "-";
                                }
                                if (valueMap.ContainsKey("min"))
                                {
                                    OWBTypes.IspTItem ispTItem = OWBJson.parse<OWBTypes.IspTItem>(valueMap["min"].ToString());
                                    cell[COLUMN_MINTEMP].Value = ispTItem.T.ToString("F1") + " ℃";
                                }
                                else
                                {
                                    cell[COLUMN_MINTEMP].Value = "-";
                                }
                                if (valueMap.ContainsKey("avg"))
                                {
                                    OWBTypes.IspTItem ispTItem = OWBJson.parse<OWBTypes.IspTItem>(valueMap["avg"].ToString());
                                    cell[COLUMN_AVGTEMP].Value = ispTItem.T.ToString("F1") + " ℃";
                                }
                                else
                                {
                                    cell[COLUMN_AVGTEMP].Value = "-";
                                }
                            }
                            else
                            {
                                OWBTypes.IspTItem ispTItem = OWBJson.parse<OWBTypes.IspTItem>(kv.Value.ToString());
                                cell[COLUMN_MAXTEMP].Value = "-";
                                cell[COLUMN_MINTEMP].Value = "-";
                                cell[COLUMN_AVGTEMP].Value = ispTItem.T.ToString("F1") + " ℃";
                            }
                        }
                        catch
                        {

                        }
                    }
                });
            }
        }

        private void tsmiSerialSet_Click(object sender, EventArgs e)
        {
            OWBSerialSetForm serialSetForm = new OWBSerialSetForm();
            serialSetForm.ShowDialog();
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                OWBISPForm ispForm = new OWBISPForm();
                ispForm.ShowDialog();
            }
        }
    }
}
