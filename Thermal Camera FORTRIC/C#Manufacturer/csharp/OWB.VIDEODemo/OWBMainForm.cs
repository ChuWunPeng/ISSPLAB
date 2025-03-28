using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OWB.VIDEODemo
{
    public partial class OWBMainForm : Form
    {
        private OWBCamera _Camera;
        private bool _TempAcqDone;
        private Thread _TempAcqThread;
        private Rectangle _SettingRegion;

        private object RefreshTemperTableLock = new object();

        public OWBMainForm()
        {
            InitializeComponent();
            Camera = new OWBCamera();
            TempAcqDone = true;
            TempAcqThread = null;
            SettingRegion = new Rectangle();
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

        public Rectangle SettingRegion { get => _SettingRegion; set => _SettingRegion = value; }

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
                    RefreshTemperTable();
                }
                catch (Exception)
                {

                }
            }
        }

        private void RefreshTemperTable()
        {
            lock (RefreshTemperTableLock)
            {
                if (rbTemper.Checked)
                {
                    float[,] temperatureValues = null;
                    Camera.GetTemperatureFrame(out temperatureValues);
                    dgvTemperTable.BeginInvoke((MethodInvoker)delegate ()
                    {
                        if (temperatureValues == null)
                        {
                            return;
                        }
                        for (int i = 0; i < SettingRegion.Height; i++)
                        {
                            DataGridViewCellCollection cell = dgvTemperTable.Rows[i].Cells;
                            for (int j = 0; j < SettingRegion.Width; j++)
                            {
                                cell[j].Value = temperatureValues[j + SettingRegion.X, i + SettingRegion.Y];
                            }
                        }
                    });
                }
                else if (rbRaw.Checked)
                {
                    dgvTemperTable.BeginInvoke((MethodInvoker)delegate ()
                    {
                        for (int i = 0; i < SettingRegion.Height; i++)
                        {
                            DataGridViewCellCollection cell = dgvTemperTable.Rows[i].Cells;
                            for (int j = 0; j < SettingRegion.Width; j++)
                            {
                                cell[j].Value = Camera.RawValues[j + SettingRegion.X, i + SettingRegion.Y];
                            }
                        }
                    });
                }
            }
        }

        private void Form_Refresh()
        {
            if (Camera.IsConnected)
            {
                btnConnect.Text = "断开";
                nudRegionX.Maximum = Camera.Width - 1;
                nudRegionY.Maximum = Camera.Height - 1;
                RefreshTableColumn();
            }
            else
            {
                btnConnect.Text = "连接";
            }
        }

        private void OWBMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisConnect();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            RefreshTableColumn();
        }

        private void RefreshTableColumn()
        {
            if (nudRegionWidth.Value + nudRegionX.Value > Camera.Width)
            {
                nudRegionWidth.Value = Camera.Width - nudRegionX.Value;
            }
            if (nudRegionHeight.Value + nudRegionY.Value > Camera.Height)
            {
                nudRegionHeight.Value = Camera.Height - nudRegionY.Value;
            }
            SettingRegion = new Rectangle((int)nudRegionX.Value, (int)nudRegionY.Value, (int)nudRegionWidth.Value, (int)nudRegionHeight.Value);
            dgvTemperTable.Columns.Clear();
            for (int i = SettingRegion.X; i < SettingRegion.X + SettingRegion.Width; i++)
            {
                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = i.ToString();
                dgvc.HeaderText = i.ToString();
                dgvc.Width = 50;
                dgvTemperTable.Columns.Add(dgvc);
            }
            dgvTemperTable.Rows.Clear();
            for (int i = SettingRegion.Y; i < SettingRegion.Y + SettingRegion.Height; i++)
            {
                int index = dgvTemperTable.Rows.Add();
                DataGridViewHeaderCell headerCell = dgvTemperTable.Rows[index].HeaderCell;
                headerCell.Value = i.ToString();
            }
        }
    }
}