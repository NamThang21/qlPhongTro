using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongTro
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void ptbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ptbState_Click(object sender, EventArgs e)
        {
            if(this.WindowState== FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;

            }else
            {
                this.WindowState= FormWindowState.Normal;
            }
        }

        private void ptbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //ham add form len groupbox co ten grbContent
        private void AddForm(Form f)
        {
            this.grbContent.Controls.Clear();//xôa các control hiện có trên groupbox
            f.TopLevel = false;
            f.AutoScroll = true;
            f.FormBorderStyle = FormBorderStyle.None;//bo vien của form
            f.Dock = DockStyle.Fill;
            this.Text = f.Text;
            this.grbContent.Controls.Add(f);
            f.Show();
        }

        private void phòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f= new frmLoaiPhong();
            AddForm(f);
        }
    }
}
