using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDK;

namespace OWB.TADemo
{
    public partial class OWBSerialSetForm : Form
    {
        public OWBSerialSetForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OWBTypes.SerialPort serialport = new OWBTypes.SerialPort();
            try
            {
                serialport.Baudrate = Convert.ToInt32(tbBaudrate.Text);
                serialport.CSize = Convert.ToInt32(tbSize.Text);
                serialport.Parity = tbPairty.Text;
                serialport.Stopbit = tbStopbit.Text;
                OWBGlobal.Camera.PutSerialport(serialport);
            }
            catch
            {

            }
        }

        private void OWBSerialSetForm_Load(object sender, EventArgs e)
        {
            OWBTypes.SerialPort serialPort = OWBGlobal.Camera.GetSerialport();
            if (serialPort != null)
            {
                tbBaudrate.Text = Convert.ToString(serialPort.Baudrate);
                tbSize.Text = Convert.ToString(serialPort.CSize);
                tbPairty.Text = Convert.ToString(serialPort.Parity);
                tbStopbit.Text = Convert.ToString(serialPort.Stopbit);
            }
        }
    }
}
