using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFApp
{
    public partial class FormServer : System.Windows.Forms.Form
    {
        public NotifyIcon NotifiyIcon { get; set; }

        public FormServer()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HideMainForm(e);
        }

        public void HideMainForm(FormClosingEventArgs e)
        {
            Hide();
            NotifiyIcon.Visible = true;
            ShowInTaskbar = false;
            if (e != null)
                e.Cancel = true;
        }

        private void FormServer_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                NotifiyIcon.Visible = true;
                ShowInTaskbar = false;
            }
        }
    }
}
