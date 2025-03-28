using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OWB.TADemo
{
    public partial class OWBAboutForm : Form
    {
        public OWBAboutForm()
        {
            InitializeComponent();
            pbAbout.Image = OWB.TADemo.Properties.Resources.ta;
            if (OWBGlobal.StreamSDKVersion != null)
            {
                string[] streamSDKVersionArray = Array.ConvertAll<int, string>(OWBGlobal.StreamSDKVersion, delegate(int v) { return v.ToString(); });
                lblStreamSDKVersionValue.Text = string.Join(".", streamSDKVersionArray);
            }
        }
    }
}
