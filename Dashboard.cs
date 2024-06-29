using ExamApp.Properties;
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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private bool isLanguageDropDownCollapsed;


        //xử lý khi người dùng di chuyển cái titlePanel thì capture theo giống như 1 cái title bar
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void titlePanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void maximizeBtn_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                // Chuyển sang chế độ full màn hình
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                // Thoát khỏi chế độ full màn hình
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            // Xử lý minimize
            this.WindowState = FormWindowState.Minimized;
        }

        private void exitBtn_Click(object obj, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowUserPanelBtn_Click(object sender, EventArgs e)
        {
            //Ẩn hiện user panel
            if (UserPanel.Visible == false) 
            {
                UserPanel.Visible = true;
            }
            else
            {
                UserPanel.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Khởi động event drop down
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isLanguageDropDownCollapsed)
            {
                //Xoay image 90 độ
                LanguageDropDownPanel.Height += 10;
                if (LanguageDropDownPanel.Size == LanguageDropDownPanel.MaximumSize)
                {
                    timer1.Stop();
                    isLanguageDropDownCollapsed = false;
                }
            }
            else
            {
                //return icon về lại ban đầu
                LanguageDropDownPanel.Height -= 10;
                if (LanguageDropDownPanel.Size == LanguageDropDownPanel.MinimumSize)
                {
                    timer1.Stop();
                    isLanguageDropDownCollapsed = true;
                }
            }
        }
    }
}
