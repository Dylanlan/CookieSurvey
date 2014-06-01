using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CookieSurvey
{
    static class Program
    {
        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FocusChrome();
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("25544");
            SendKeys.SendWait("{ENTER}");

            var form = new Form1();
            Application.Run(form);


            if (Form1.abort)
            {
                return;
            }

            string month = Form1.month.ToString();
            string day = Form1.day.ToString();
            string year = Form1.year.ToString();
            int hour = Form1.hour;
            int minute = Form1.minute;
            bool MWF = Form1.MWF;

            string hourString = hour.ToString();
            string minuteString = minute.ToString();

            if (hour < 10 && hour > 0)
            {
                hourString = "0" + hourString;
            }

            if (minute < 10 && minute > 0)
            {
                minuteString = "0" + minuteString;
            }

            FocusChrome();


            for (int i = 0; i < 5; i++)
            {
                SendKeys.SendWait("{TAB}");
                System.Threading.Thread.Sleep(100);
            }

            SendKeys.SendWait(day);
            SendKeys.SendWait("/");
            SendKeys.SendWait(month);
            SendKeys.SendWait("/");
            SendKeys.SendWait(year);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait(hourString);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait(minuteString);


            for (int j = 0; j < 4; j++)
            {
                SendKeys.SendWait("{TAB}");
                for (int i = 0; i < 9; i++)
                {
                    SendKeys.SendWait("{RIGHT}");
                }
            }

            for (int j = 0; j < 3; j++)
            {
                SendKeys.SendWait("{TAB}");
                for (int i = 0; i < 8; i++)
                {
                    SendKeys.SendWait("{RIGHT}");
                }
            }

            SendKeys.SendWait("{TAB}");
            for (int i = 0; i < 9; i++)
            {
                SendKeys.SendWait("{RIGHT}");
            }

            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("n");

            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("n");

            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("20");

            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("20");

            SendKeys.SendWait("{TAB}");
            if (MWF)
            {
                SendKeys.SendWait("stankiev@ualberta.ca");
            }
            else
            {
                SendKeys.SendWait("dylan.stank@gmail.com");
            }


            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("n");

            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("n");

            SendKeys.SendWait("{TAB}");
            //SendKeys.SendWait("{ENTER}");
        }


        public static void FocusChrome()
        {
            Process[] procs = Process.GetProcessesByName("chrome");

            if (procs.Length == 0)
            {
                Console.WriteLine("Google Chrome is not currently open");
                return;
            }

            List<string> titles = new List<string>();

            IntPtr hWnd = IntPtr.Zero;
            int id = 0;

            foreach (Process p in procs)
            {
                if (p.MainWindowTitle.Length > 0)
                {
                    hWnd = p.MainWindowHandle;
                    id = p.Id;
                    //break;
                }

                if (p.MainWindowTitle == "SUBWAY Customer Survey - Google Chrome")
                {
                    hWnd = p.MainWindowHandle;
                    id = p.Id;
                    break;
                }
            }

            bool isMinimized = IsIconic(hWnd);

            if (isMinimized)
            {
                ShowWindow(hWnd, 9); // restore
                System.Threading.Thread.Sleep(100);
            }

            SetForegroundWindow(hWnd); // set focus to Google Chrome
            //SendKeys.SendWait("+{ESC}"); // pop up Chrome Task Manager window     
            System.Threading.Thread.Sleep(100);
        }
    }
}
