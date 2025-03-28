using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace OWB.DLDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OWBGlobal.InitDefaultPalettes();
            OWBGlobal.InitSDKVersion();
            OWBGlobal.MainForm = new OWBMainForm();
            Application.Run(OWBGlobal.MainForm);
        }
    }
}
