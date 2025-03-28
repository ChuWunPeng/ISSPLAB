using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

using OWB.SnipDemo.SDK;

namespace OWB.SnipDemo
{
    public partial class MainForm : Form
    {
        private DeviceSDKType.LoginType _loginType;
        private DeviceSDKType.StreamType _streamType;
        private bool _formClosed;

        private DeviceManager _deviceManager;

        public MainForm()
        {
            InitializeComponent();

            _loginType = DeviceSDKType.LoginType.H264;
            _streamType = DeviceSDKType.StreamType.PRI;
            _formClosed = false;

            _deviceManager = new DeviceManager();
            RefreshCamera();

            Task.Factory.StartNew(PlayVideo);
        }

        private void PlayVideo()
        {
            while (!_formClosed)
            {
                try
                {
                    if (_deviceManager.IsConnected && _deviceManager.IsPlay())
                    {
                        Image image;
                        if (_loginType == DeviceSDKType.LoginType.H264)
                        {
                            image = _deviceManager.GetH264Frame();
                        }
                        else
                        {
                            image = _deviceManager.GetRawFrame();
                        }

                        pbCamera.Invoke(new Action(delegate() { pbCamera.Image = image; }));
                    }
                }
                finally
                {
                    Task.Delay(10);
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!_deviceManager.Logout())
            {
                if (tbIPAddress.Text == string.Empty || tbIPAddress.Text == null)
                {
                    tbIPAddress.Focus();
                    return;
                }
                Cursor = Cursors.WaitCursor;
                if (_deviceManager.Login(tbIPAddress.Text, tbUserName.Text, tbPassword.Text))
                {
                    InstrumentJconfig tempParam = _deviceManager.GetTempParameter();
                    nudEmissivityValue.Value = (decimal)tempParam.Emission;
                    nudRelativeHumidityValue.Value = (decimal)tempParam.RH;
                    nudReflectedTemperatureValue.Value = (decimal)tempParam.Reflection_T;
                    nudAtmosphericTemperatureValue.Value = (decimal)tempParam.Atmosphere_T;
                    nudDistanceValue.Value = (decimal)tempParam.Distance;
                    nudLensTValue.Value = (decimal)tempParam.Lens_T;
                    nudLensTransValue.Value = (decimal)tempParam.Lens_Transmission;
                    nudOffsetValue.Value = (decimal)tempParam.Offset;

                    string[] allPalette = _deviceManager.GetAllPalette();
                    string currentPalette = _deviceManager.GetCurrentPalette();
                    cbPalette.Items.Clear();
                    cbPalette.Items.AddRange(allPalette);
                    cbPalette.SelectedItem = currentPalette;
                }
                else
                {
                    MessageBox.Show("连接失败", "连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor = Cursors.Default;
            }
            RefreshCamera();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            _deviceManager.StartStream(_loginType, _streamType);
            RefreshCamera();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _deviceManager.StopStream();
            pbCamera.Image = null;
        }

        private void OWBMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _formClosed = true;
            _deviceManager.Logout();
        }

        private void rbH264_CheckedChanged(object sender, EventArgs e)
        {
            if (rbH264.Checked)
            {
                _loginType = DeviceSDKType.LoginType.H264;
                RefreshCamera();
                _deviceManager.RefreshStream(_loginType, _streamType);
            }
        }

        private void rbRaw_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRaw.Checked)
            {
                _loginType = DeviceSDKType.LoginType.RAW;
                RefreshCamera();
                _deviceManager.RefreshStream(_loginType, _streamType);
            }
        }

        private void btnSnip_Click(object sender, EventArgs e)
        {
            FileManager fileManager = _deviceManager.Snip();
            fileManager.SaveAs(Path.Combine(Constants.DefaultIRFolder, "1.jpg"));
            Image paletteImage = fileManager.GetPaletteImage();
            paletteImage.Save(Path.Combine(Constants.DefaultIRFolder, "2.jpg"));
            float[,] tempValues = fileManager.GetTemperature();
            List<FileSDKType.OWBFileMarker> markers = fileManager.GetMarkerList();
            FileSDKType.OWBFileSpotMarker spotMarker = new FileSDKType.OWBFileSpotMarker("SnipDemoSpot", true, 1, new Point(50, 50), 20, 1, 0);
            markers.Add(spotMarker);
            fileManager.SetMarkerList(markers);
            fileManager.SaveAs(Path.Combine(Constants.DefaultIRFolder, "3.jpg"));
        }

        private void RefreshCamera()
        {
            if (_deviceManager.IsConnected)
            {
                gbConnInfo.Enabled = false;
                gbConnMode.Enabled = true;
                btnConnect.Text = "断开";
                btnPlay.Enabled = true;
                btnStop.Enabled = true;
                btnSnip.Enabled = true;
                plMenu.Enabled = true;
            }
            else
            {
                gbConnInfo.Enabled = true;
                gbConnMode.Enabled = false;
                btnConnect.Text = "连接";
                btnPlay.Enabled = false;
                btnStop.Enabled = false;
                btnSnip.Enabled = false;
                plMenu.Enabled = false;
                pbCamera.Image = null;
            }
        }

        private void cbPalette_SelectedValueChanged(object sender, EventArgs e)
        {
            string currentPalette = cbPalette.Text;
            _deviceManager.SetCurrentPalette(currentPalette);
        }

        private void btnNearFocus_Click(object sender, EventArgs e)
        {
            _deviceManager.NearFocus();
        }

        private void btnFarFocus_Click(object sender, EventArgs e)
        {
            _deviceManager.FarFocus();
        }

        private void btnAutoFocus_Click(object sender, EventArgs e)
        {
            _deviceManager.AutoFocus();
        }

        private void btnDoCali_Click(object sender, EventArgs e)
        {
            _deviceManager.DoCalibrate();
        }

        private void deviceParamValue_ValueChanged(object sender, EventArgs e)
        {
            InstrumentJconfig tempParam = new InstrumentJconfig
            {
                Emission = (float)nudEmissivityValue.Value,
                RH = (float)nudRelativeHumidityValue.Value,
                Reflection_T = (float)nudReflectedTemperatureValue.Value,
                Atmosphere_T = (float)nudAtmosphericTemperatureValue.Value,
                Distance = (float)nudDistanceValue.Value,
                Lens_T = (float)nudLensTValue.Value,
                Lens_Transmission = (float)nudLensTransValue.Value,
                Offset = (float)nudOffsetValue.Value
            };
            _deviceManager.SetTempParameter(tempParam);
        }
    }
}
