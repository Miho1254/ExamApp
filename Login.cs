using ExamApp.Customs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            InitializeLoginPanel();
            // Đặt ngôn ngữ mặc định là tiếng Anh
            cbx_Language.SelectedIndex = 0;
        }

        // Xử lý khi người dùng di chuyển titlePanel giống như một title bar
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void titlePanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        // Xử lý sự kiện thoát chương trình
        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Xử lý sự kiện chuyển đổi chế độ full màn hình
        private void maximizeBtn_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
            }
        }

        // Xử lý sự kiện minimize
        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Sự kiện load form (chưa triển khai)
        private void Login_Load(object sender, EventArgs e) { }

        // Sự kiện paint của loginForm (chưa triển khai)
        private void loginForm_Paint(object sender, PaintEventArgs e) { }

        // Đặt vị trí của loginPanel ở giữa MainForm
        private void InitializeLoginPanel()
        {
            setPanelCenter(loginPanel);
        }

        // Sự kiện khi form thay đổi kích thước
        private void Login_Resize(object sender, EventArgs e)
        {
            setPanelCenter(loginPanel);
            setPanelCenter(recoveryPasswordPanel);
            setPanelCenter(UserRegPanel);
        }

        // Xử lý sự kiện khi người dùng click vào "Forgot Password"
        private void labelForgotPassword_Click(object sender, EventArgs e)
        {
            loginPanel.Visible = false;
            recoveryPasswordPanel.Visible = true;
            setPanelCenter(recoveryPasswordPanel);
        }

        // Xử lý sự kiện khi người dùng click vào nút "Back" của form recovery
        private void recoveryPasswordBackBtn_Click(object sender, EventArgs e)
        {
            loginPanel.Visible = true;
            recoveryPasswordPanel.Visible = false;
            setPanelCenter(loginPanel);
        }

        // Đặt panel ở giữa parent form
        private void setPanelCenter(RoundedPanel panel)
        {
            int x = (this.ClientSize.Width - panel.Width) / 2;
            int y = (this.ClientSize.Height - panel.Height) / 2;
            panel.Location = new Point(x, y);
        }

        // Xử lý sự kiện khi người dùng click vào nút "Back" của User Registration Panel
        private void UserRegBackBtn_Click(object sender, EventArgs e)
        {
            UserRegPanel.Visible = false;
            loginPanel.Visible = true;
            setPanelCenter(loginPanel);
        }

        // Xử lý sự kiện khi người dùng click vào "Sign Up"
        private void lbl_Signup_Click(object sender, EventArgs e)
        {
            UserRegPanel.Visible = true;
            loginPanel.Visible = false;
            setPanelCenter(UserRegPanel);
        }

        // Sự kiện double click của titlePanel (bỏ trống)
        private void titlePanel_MouseDoubleClick(object sender, MouseEventArgs e) { }

        // Sự kiện paint của User Registration Panel
        private void UserRegPanel_Paint(object sender, PaintEventArgs e)
        {
            UserRegNextBtn.BackColor = Color.Silver;
            UserRegNextBtn.Enabled = false;
        }

        // Xử lý sự kiện thay đổi trạng thái checkbox "Accept License"
        private void cbx_accptLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_accptLicense.Checked)
            {
                UserRegNextBtn.Enabled = true;
                UserRegNextBtn.BackColor = Color.FromArgb(45, 53, 135);
            }
            else
            {
                UserRegNextBtn.Enabled = false;
                UserRegNextBtn.BackColor = Color.Silver;
            }
        }
    }

}
