using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SDK;

namespace OWB.TADemo
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
            OWBGlobal.StreamSDKVersion = new int[4] { 0, 0, 0, 0 };
            int streamRes = StreamSDK.streamsdk_get_version(OWBGlobal.StreamSDKVersion);
            Application.Run(new OWBMainForm());
        }
    }
}
