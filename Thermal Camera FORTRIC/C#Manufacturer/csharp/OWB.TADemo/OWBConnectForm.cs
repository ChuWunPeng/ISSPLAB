using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OWB.TADemo
{
    public partial class OWBConnectForm : Form
    {
        public OWBConnectForm()
        {
            InitializeComponent();
        }

        public string ConnectString
        {
            get
            {
                return tbConnectString.Text.Trim();
            }
            set
            {
                tbConnectString.Text = value;
            }
        }

        public string UserName
        {
            get
            {
                return tbUserName.Text.Trim();
            }
        }

        public string Password
        {
            get
            {
                return tbPassword.Text.Trim();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tbConnectString.Text == string.Empty || tbConnectString.Text == null)
            {
                tbConnectString.Focus();
                ttConnectForm.Show("地址不能为空。", tbConnectString, 0, -15, 1000);
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
