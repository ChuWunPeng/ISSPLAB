using SDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OWB.RestDemo
{
    public partial class OWBMainForm : Form
    {
        private const int PORT = 10080;
        public OWBMainForm()
        {
            InitializeComponent();
            ColumnHeader ch = new ColumnHeader();
            ch.Text = "模板内容";
            ch.Width = lvTemplate.Width;
            ch.TextAlign = HorizontalAlignment.Left;
            lvTemplate.Columns.Add(ch);
            RefreshTemplate();
        }

        private void RefreshTemplate()
        {
            lvTemplate.Items.Clear();
            try
            {
                List<OWBTypes.Template> templateList = new List<OWBTypes.Template>();
                JArray templateArray = ToJObject("Template.json");
                lvTemplate.BeginUpdate();
                for (int i = 0; i < templateArray.Count; i++)
                {
                    OWBTypes.Template template = new OWBTypes.Template(templateArray[i]["name"].ToString(), templateArray[i]["method"].ToString(), templateArray[i]["url"].ToString(), templateArray[i]["data"].ToString());
                    ListViewItem item = new ListViewItem();
                    item.Text = template.ToString();
                    item.Tag = template;
                    lvTemplate.Items.Add(item);
                }
                lvTemplate.EndUpdate();
            }
            catch { }

        }

        private JArray ToJObject(string fileName)
        {
            try
            {
                using (System.IO.StreamReader file = System.IO.File.OpenText(fileName))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        return (JArray)JToken.ReadFrom(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private void lvTemplate_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lvTemplate.GetItemAt(e.X, e.Y);
            if (item == null)
            {
                return;
            }
            OWBTypes.Template template = item.Tag as OWBTypes.Template;
            cbMethod.SelectedItem = template.Method;
            cbUrl.Text = template.URL;
            rtbSend.Text = Convert.ToString(template.Data);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbIPAddress.Text == string.Empty || tbIPAddress.Text == null)
                {
                    tbIPAddress.Focus();
                    ttMainForm.Show("地址不能为空。", tbIPAddress, 0, -15, 1000);
                    return;
                }
                rtbResponse.Text = string.Empty;
                lblStatusCode.Text = string.Empty;
                string ip = tbIPAddress.Text.Trim();
                string url = cbUrl.Text.Trim();
                string data = rtbSend.Text.Trim();
                byte[] EncryptPassword = Encoding.UTF8.GetBytes(tbPassword.Text);

                RestSDK.Rest_set_authroization(tbUserName.Text.Trim(), EncryptPassword);
                byte[] buf = new byte[1024];
                uint length = 0;
                int statusCode = 0;
                switch (cbMethod.Text)
                {
                    case "GET":
                        statusCode = RestSDK.Get(ip, (ushort)PORT, url, ref buf, ref length);
                        break;
                    case "PUT":
                        OWBString.StringToBytes(data, data.Length, Encoding.UTF8).CopyTo(buf, 0);
                        statusCode = RestSDK.Put(ip, (ushort)PORT, url, ref buf, ref length);
                        break;
                    case "POST":
                        OWBString.StringToBytes(data, data.Length, Encoding.UTF8).CopyTo(buf, 0);
                        statusCode = RestSDK.Post(ip, (ushort)PORT, url, ref buf, ref length);
                        break;
                    case "DELETE":
                        statusCode = RestSDK.Delete(ip, (ushort)PORT, url, ref buf, ref length);
                        break;
                }
                string response = OWBString.BytesToString(buf, Encoding.UTF8);
                rtbResponse.Text = response;
                lblStatusCode.Text = Convert.ToString(statusCode);
                pbStatus.Image = OWBTypes.CheckStatus(statusCode) ? RESTDemo.Properties.Resources.success : RESTDemo.Properties.Resources.error;
            }
            catch (Exception ex)
            {
                rtbResponse.Text = ex.Message;
            }
        }

        private void cbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMethod.Text == "GET" || cbMethod.Text == "DELETE")
            {
                rtbSend.Enabled = false;
            }
            else
            {
                rtbSend.Enabled = true;
            }
        }
    }
}
