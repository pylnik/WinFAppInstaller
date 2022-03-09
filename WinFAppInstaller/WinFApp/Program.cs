using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFApp
{
    static class Program
    {
        private static FormServer _Server;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                using (NotifyIcon icon = new NotifyIcon())
                {
                    icon.Icon = new System.Drawing.Icon("Resources/logo.ico");
                    icon.Text = "Test app in Taskbar";
                    icon.Visible = true;
                    icon.MouseClick += Icon_MouseClick;

                    _Server = new FormServer
                    {
                        NotifiyIcon = icon,
                        ShowInTaskbar = false,
                        Visible = false
                    };

                    icon.ContextMenu = new ContextMenu(new MenuItem[] {
                        new MenuItem("Show", (s, e) => {
                            ShowMainForm(icon);
                        }),
                        new MenuItem("Exit", (s, e) => {
                            ExitApp();
                        }),
                        });
                    icon.Visible = true;

                    Application.Run();
                    icon.Visible = false;
                }
            }
            catch (Exception e)
            {
            }
        }

        private static void ExitApp()
        {
            Application.Exit();
            Application.ExitThread();
        }

        private static void ShowMainForm(NotifyIcon icon)
        {
            _Server.Show();
            _Server.Visible = true;
            icon.Visible = true;
            _Server.Activate();
        }

        private static void Icon_MouseClick(object sender, MouseEventArgs e)
        {
            var icon = sender as NotifyIcon;
            if (e.Button == MouseButtons.Left)
                if (_Server.Visible && icon.Visible)
                    _Server.HideMainForm(null);
                else
                    ShowMainForm(icon);
        }
    }
}
