using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Software_ICT
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
            //Application.Run(new Form1());
            Splash.Show();
            Console.WriteLine("splash show done.");
            bool continueFlag = true;
            //循环判断是否登陆OK
            while (!Splash.loginIsOk)
            {
                System.Threading.Thread.Sleep(100);
                //判断点击关闭事件
                if (Splash.clickClose)
                {
                    Splash.Close();
                    continueFlag = false;
                    break;
                }
            }

            if (continueFlag)
            {
                DoStartup();
                Console.WriteLine("do startup done.");
            }
        }
        static void DoStartup()
        {
            //做需要的事情
            //开启主窗体
            Form1 mainform = new Form1();
            Application.Run(mainform);
        }
    }
}
