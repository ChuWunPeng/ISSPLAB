using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OWB.SnipDemo.SDK;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TEST
{
    public partial class SettingForm : Form
    {
        private DeviceManager _deviceManager;
       

        public SettingForm()
        {
            InitializeComponent();
            _deviceManager = new DeviceManager();
            RefreshSettingParameter();
        }
        private void RefreshSettingParameter()
        {
            if (_deviceManager.Login(LoginForm.IP, LoginForm.GetUserName, LoginForm.GetPassword))
            { 
                InstrumentJconfig tempParam = _deviceManager.GetTempParameter();
                nudEmissivityValue.Value = (decimal)tempParam.Emission;     //發射率
                nudRelativeHumidityValue.Value = (decimal)tempParam.RH;     //相對濕度
                nudReflectedTemperatureValue.Value = (decimal)tempParam.Reflection_T;   //反射溫度
                nudAtmosphericTemperatureValue.Value = (decimal)tempParam.Atmosphere_T; //環境溫度
                nudDistanceValue.Value = (decimal)tempParam.Distance;   //距離
                nudLensTValue.Value = (decimal)tempParam.Lens_T;        //外部光學溫度
                nudLensTransValue.Value = (decimal)tempParam.Lens_Transmission;     //外部光學透射率
                nudOffsetValue.Value = (decimal)tempParam.Offset;       //偏移


               
            }
            else
            {
                MessageBox.Show("連接失敗", "連接", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cbTemperatureRange.Items.Clear();
            foreach (TemperatureRange tRange in OWBGlobal.Camera.TRanges)
            {
                cbTemperatureRange.Items.Add(tRange);
            }
            cbTemperatureRange.SelectedIndex = OWBGlobal.Camera.TRangeIndex;
        }
        private void GettingSettingParameter()
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
            OWBGlobal.Camera.TRangeIndex = cbTemperatureRange.SelectedIndex;
        }

        private void SettingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _deviceManager.Logout();
        }
    
        private void SettingForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GettingSettingParameter();
            MessageBox.Show("設定成功", "設定", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
