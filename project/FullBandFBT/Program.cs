using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace CWDM1To4
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //启动控制台
            //ConsoleEx.AllocConsole();

            string MName = Process.GetCurrentProcess().MainModule.ModuleName;
            string PName = Path.GetFileNameWithoutExtension(MName);
            Process[] myProcess = Process.GetProcessesByName(PName);
            if (myProcess.Length > 1)
            {
                MessageBox.Show(" 系统已经在运行不能重复运行！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                FormLogin fl = new FormLogin();
                if (fl.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new FrmMain());
                }
                
            }
        }
        /// <summary>
        /// 让窗体和控制台同时存在
        /// </summary>
        class ConsoleEx
        {
            /// <summary>
            /// 启动控制台
            /// </summary>
            [DllImport("kernel32.dll")]
            public static extern bool AllocConsole();

            /// <summary>
            /// 释放控制台
            /// </summary>
            [DllImport("kernel32.dll")]
            public static extern bool FreeConsole();
        }
    }
}
