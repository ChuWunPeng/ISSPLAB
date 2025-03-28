using System;
using System.Windows.Forms;

namespace TEST
{
    public partial class LoginForm : Form
    {
       

        public LoginForm()
        {
            InitializeComponent();
            OWBGlobal.Camera = new OWBCamera();
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            Loginbtn.Enabled = false;
            Cursor = System.Windows.Forms.Cursors.WaitCursor; // 改變滑鼠指標為圓圈

            if (Equipment_IP.Text == string.Empty || Equipment_IP.Text == null)
            {
                Equipment_IP.Focus();
                ttMainForm.Show("不可為空", Equipment_IP, 0, -15, 1000);
                Cursor = System.Windows.Forms.Cursors.Default; // 恢復滑鼠指標
                return;
            }

            if (!OWBGlobal.Camera.LoginCamera(Equipment_IP.Text, UserName.Text, Password.Text))
            {
                MessageBox.Show(this, "連接失敗", "連接", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                OWBGlobal.InitSDKVersion();
                OWBGlobal.InitDefaultPalettes();
                MessageBox.Show(this, "連接成功", "連接", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IP = Equipment_IP.Text;
                GetUserName = UserName.Text;
                GetPassword = Password.Text;
                this.DialogResult = DialogResult.OK;
            }

            Cursor = System.Windows.Forms.Cursors.Default; // 恢復滑鼠指標
            Loginbtn.Enabled = true;
        }
        public bool RefreshOnCloseConnect()
        {
            if (OWBGlobal.Camera.IsConnected)
            {

                OWBGlobal.Camera.LogoutCamera();

                return true;
            }
            return false;
            

        }

        private void Login_Leave(object sender, EventArgs e)
        {
            this.Close();

        }
        public static string IP { get; private set; }
      
        public static string GetUserName { get; private set; }

        public static string GetPassword { get; private set; }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
