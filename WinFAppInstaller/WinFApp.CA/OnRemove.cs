using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WinFApp.CA
{
    public class OnRemove
    {
        [CustomAction]
        public static ActionResult RemoveAutorunLink(Session session)
        {
            var result = ActionResult.Success;
            var actionName = $"WinFApp.CA.{MethodBase.GetCurrentMethod().Name}";
            session.Log($"started: {actionName}");
            try
            {
                string startUpFolderPath =
                   Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                var productName = session["ProductName"];
                var fileName = Path.Combine(startUpFolderPath, $"{productName}.lnk");
                if (File.Exists(fileName))
                    File.Delete(fileName);
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
