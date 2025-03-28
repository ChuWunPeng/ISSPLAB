using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OWB.DLDemo
{
    public partial class OWBUserForm : Form
    {
        private UserFormType _FormType;
        private string _UserName;
        private string _UserGroup;

        public OWBUserForm(UserFormType formType, string userName, string userGroup)
        {
            InitializeComponent();
            UserName = userName;
            UserGroup = userGroup;
            FormType = formType;
        }

        public UserFormType FormType
        {
            get { return _FormType; }
            set
            {
                _FormType = value;
                switch (_FormType)
                {
                    case UserFormType.Add:
                        Text = "添加";
                        break;
                    case UserFormType.Update:
                        Text = "修改";
                        break;
                }
            }
        }

        private string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string UserGroup
        {
            get { return _UserGroup; }
            set { _UserGroup = value; }
        }

        private void OWBUserForm_Load(object sender, EventArgs e)
        {
            cbUserGroup.Text = "root";
            cbUserGroup.Enabled = false;
            if (FormType == UserFormType.Update)
            {
                tbUserCode.Text = UserName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!OWBGlobal.Camera.UpdateUser(tbUserCode.Text, tbPassword.Text, cbUserGroup.Text))
            {
                MessageBox.Show(this, "添加或修改用户失败，请查看开发手册", "提示");
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
