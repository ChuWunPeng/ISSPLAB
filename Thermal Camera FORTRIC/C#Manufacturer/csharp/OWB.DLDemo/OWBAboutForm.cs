using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OWB.DLDemo
{
    public partial class OWBAboutForm : Form
    {
        public OWBAboutForm()
        {
            InitializeComponent();
            pbAbout.Image = OWB.DLDemo.Properties.Resources.dl;
            if (OWBGlobal.StreamSDKVersion != null)
            {
                string[] streamSDKVersionArray = Array.ConvertAll<int, string>(OWBGlobal.StreamSDKVersion, delegate(int v) { return v.ToString(); });
                lblStreamSDKVersionValue.Text = string.Join(".", streamSDKVersionArray);
            }
        }
    }
}
