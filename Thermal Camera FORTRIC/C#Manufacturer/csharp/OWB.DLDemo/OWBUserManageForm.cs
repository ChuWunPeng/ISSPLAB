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
    public partial class OWBUserManageForm : Form
    {
        private const int COLUMN_USERNAME = 0;
        private const int COLUMN_USERGROUP = 1;

        public OWBUserManageForm()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            OWBUserForm addForm = new OWBUserForm(UserFormType.Add, null, null);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                if (MessageBox.Show(this, "添加用户成功，只有重启设备才能有效，是否重启设备？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    OWBGlobal.Camera.PostReBoot();
                    OWBGlobal.RefreshOnCloseConnect();
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (dgvUser.SelectedRows.Count != 0)
            {
                string userName = Convert.ToString(dgvUser.SelectedRows[0].Cells[COLUMN_USERNAME].Value);
                string userGroup = Convert.ToString(dgvUser.SelectedRows[0].Cells[COLUMN_USERGROUP].Value);
                OWBUserForm updateForm = new OWBUserForm(UserFormType.Update, userName, userGroup);
                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    if (MessageBox.Show(this, "添加用户成功，只有重启设备才能有效，是否重启设备？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        OWBGlobal.Camera.PostReBoot();
                        OWBGlobal.RefreshOnCloseConnect();
                        DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUser.SelectedRows.Count != 0)
            {
                string userName = Convert.ToString(dgvUser.SelectedRows[0].Cells[COLUMN_USERNAME].Value);
                if (!OWBGlobal.Camera.DeleteUser(userName))
                {
                    MessageBox.Show(this, "删除用户失败，请查看开发手册", "提示");
                }
                else
                {
                    if (MessageBox.Show(this, "添加用户成功，只有重启设备才能有效，是否重启设备？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        OWBGlobal.Camera.PostReBoot();
                        OWBGlobal.RefreshOnCloseConnect();
                        DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        private void OWBUserManageForm_Load(object sender, EventArgs e)
        {
            dgvRefresh();
        }

        private void dgvRefresh()
        {
            dgvUser.Rows.Clear();
            Dictionary<string, object> userList = OWBGlobal.Camera.GetUserList();
            if (userList == null)
            {
                return;
            }
            foreach (KeyValuePair<string, object> kvp in userList)
            {
                int index = dgvUser.Rows.Add();
                DataGridViewCellCollection cell = dgvUser.Rows[index].Cells;
                cell[COLUMN_USERNAME].Value = kvp.Key;
                cell[COLUMN_USERGROUP].Value = Convert.ToString(kvp.Value);
            }
        }
    }
}
