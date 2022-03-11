using IWshRuntimeLibrary;
using Microsoft.Deployment.WindowsInstaller;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WinFApp.CA
{
    public class OnIntall
    {
        [CustomAction]
        public static ActionResult CheckRunAppOnSystemStartupAndLauchApplication(Session session)
        {
            var result = ActionResult.Success;
            var actionName = $"WinFApp.CA.{MethodBase.GetCurrentMethod().Name}";
            session.Log($"started: {actionName}");
            try
            {
                var runOnWinStartupValue = session["FA_RUNAPPONSYSTEMSTARTUPCHECKBOX"];
                var launchAppValue = session["FA_LAUNCHAPPCHECKBOX"];
                var installDir = session["INSTALLDIR"];
                var exeName = "WinFApp.exe";
                var exePath = System.IO.Path.Combine(session["INSTALLDIR"], exeName);
                var runOnWinStartup = session["FA_RUNAPPONSYSTEMSTARTUPCHECKBOX"].Equals("1");
                var launchApp = session["FA_LAUNCHAPPCHECKBOX"].Equals("1");
                if (launchApp)
                {
                    var startInfo = new ProcessStartInfo()
                    {
                        FileName = "WinFApp.exe",
                        WorkingDirectory = installDir,
                    };
                    var process = Process.Start(startInfo);
                    process.WaitForInputIdle();
                }

                if (runOnWinStartup)
                {
                    var productName = session["ProductName"];
                    WshShellClass wshShell = new WshShellClass();
                    IWshRuntimeLibrary.IWshShortcut shortcut;
                    string startUpFolderPath =
                      Environment.GetFolderPath(Environment.SpecialFolder.Startup);

                    // Create the shortcut
                    shortcut =
                      (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(
                        startUpFolderPath + "\\" +
                        productName + ".lnk");

                    shortcut.TargetPath = exePath;
                    shortcut.WorkingDirectory = installDir;
                    shortcut.Description = $"Launch {productName}";
                    shortcut.Save();
                }

            }
            catch (Exception ex)
            {
                session.Log($"{actionName}|{ex}");
                result = ActionResult.Failure;
            }

            session.Log($"ended: {actionName}");
            return result;

        }
    }
}
