using SDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OWB.TAGDemo
{
    public partial class OWBMainForm : Form
    {
        private const int COLUMN_MARKERNAME = 0;
        private const int COLUMN_MAXTEMP = 1;
        private const int COLUMN_MINTEMP = 2;
        private const int COLUMN_AVGTEMP = 3;

        private OWBCamera _Camera;
        private bool _TempAcqDone;
        private Thread _TempAcqThread;

        private object RefreshMarkerLock = new object();

        public OWBMainForm()
        {
            InitializeComponent();
            Camera = new OWBCamera();
            TempAcqDone = true;
            TempAcqThread = null;
        }

        public OWBCamera Camera { get => _Camera; set => _Camera = value; }

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
                if (!Camera.LoginCamera(tbIPAddress.Text, tbUserName.Text, tbPassword.Text))
                {
                    MessageBox.Show("连接失败", "连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (Camera.StartStream())
                    {
                        TempAcqDone = false;
                        TempAcqThread = new Thread(TempAcqCallback);
                        TempAcqThread.Priority = ThreadPriority.Lowest;
                        TempAcqThread.Start();
                    }
                }
                Cursor = Cursors.Default;
            }
            Form_Refresh();
        }

        private bool DisConnect()
        {
            if (Camera.IsConnected)
            {
                Camera.LogoutCamera();
                TempAcqDone = true;
                if (TempAcqThread != null)
                {
                    TempAcqThread.Interrupt();
                    TempAcqThread.Join();
                    TempAcqThread = null;
                }
                return true;
            }
            return false;
        }

        private void TempAcqCallback()
        {
            while (!TempAcqDone)
            {
                try
                {
                    Thread.Sleep(1000);
                    RefreshMarker();
                }
                catch (Exception)
                {

                }
            }
        }

        private void RefreshMarker()
        {
            lock (RefreshMarkerLock)
            {
                dgvMarker.BeginInvoke((MethodInvoker)delegate ()
                {
                    if (Camera.TagMap == null)
                    {
                        return;
                    }
                    dgvMarker.Rows.Clear();
                    int index = -1;
                    index = dgvMarker.Rows.Add();

                    DataGridViewCellCollection cell0 = dgvMarker.Rows[index].Cells;
                    cell0[COLUMN_MARKERNAME].Value = "全局";
                    Dictionary<string, object> tagmap = Camera.TagMap;
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

                    foreach (KeyValuePair<string, object> kv in Camera.TagMap)
                    {
                        if (kv.Key == "max" || kv.Key == "min")
                            continue;

                        index = dgvMarker.Rows.Add();
                        DataGridViewCellCollection cell = dgvMarker.Rows[index].Cells;
                        cell[COLUMN_MARKERNAME].Value = kv.Key;
                        Dictionary<string, object> valueMap = OWBJson.parse(kv.Value.ToString());

                        try
                        {
                            if (valueMap.ContainsKey("max"))
                            {
                                OWBTypes.TMotion tmotion = OWBJson.parse<OWBTypes.TMotion>(kv.Value.ToString());
                                cell[COLUMN_MAXTEMP].Value = tmotion.Max.T.ToString("F1") + " ℃";
                                cell[COLUMN_MINTEMP].Value = tmotion.Min.T.ToString("F1") + " ℃";
                                cell[COLUMN_AVGTEMP].Value = "-";
                            }
                            else
                            {
                                OWBTypes.IspTItem ispTItem = OWBJson.parse<OWBTypes.IspTItem>(kv.Value.ToString());
                                cell[COLUMN_MAXTEMP].Value = "-";
                                cell[COLUMN_MINTEMP].Value = "-";
                                cell[COLUMN_AVGTEMP].Value = ispTItem.T.ToString("F1") + " ℃";
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.Write(ex.Message);
                        }
                    }
                });
            }
        }

        private void Form_Refresh()
        {
            if (Camera.IsConnected)
            {
                btnConnect.Text = "断开";
            }
            else
            {
                btnConnect.Text = "连接";
            }
        }

        private void OWBMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Camera.IsConnected)
            {
                Camera.LogoutCamera();
            }
            TempAcqDone = true;
            if (TempAcqThread != null)
            {
                TempAcqThread.Interrupt();
                TempAcqThread.Join();
                TempAcqThread = null;
            }
        }
    }
}
