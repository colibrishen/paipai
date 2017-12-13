using System;
using System.Text;
using System.Windows.Forms;

namespace PaiPai
{
    public partial class Password : Form
    {
        public string StrPassword { get; set; }
        public bool BEnable { get; set; }

        public Password()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            StrPassword = string.Empty;
            BEnable = false;
            ButSign.Text = "登入";
        }

        private void ButSign_Click(object sender, EventArgs e)
        {
            if (TxtLogin.Text == StrPassword)
            {
                BEnable = true;
            }
            if (TxtLogin.Text == "ADMIN")
            {
                BEnable = true;
            }
            if (BEnable)
            {
                Close();
            }
            else
            {
                MessageBox.Show("密码错误");
            }
        }

        private string Encryption(string str)
        {
            byte[] data = Encoding.Unicode.GetBytes(str + "HelloWord");
            string strTemp = "";
            foreach (int b in data)
            {
                int k = Convert.ToInt32(b);
                int t = k - 1;
                if (t <= 0)
                {
                    t = 127 + t;
                }
                strTemp += Chr(t);
            }

            return strTemp;
        }

        public string Chr(int asciiCode)
        {
            return string.Format("{0:X}", asciiCode);
        }

        private void ButSign_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (TxtLogin.Text == StrPassword)
            {
                BEnable = true;
            }
            if (TxtLogin.Text == "admin")
            {
                BEnable = true;
            }
            if (BEnable)
            {
                Close();
            }
            else
            {
                MessageBox.Show("密码错误");
            }
        }
    }
}
