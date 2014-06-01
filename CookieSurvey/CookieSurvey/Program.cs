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

            // Tries to find the subway cookies tab in chrome
            // Focuses chrome for input if it found it
            // Exits if it can't find it
            FocusChrome();

            // To get to the input box
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");

            // The store number
            SendKeys.SendWait("25544");

            // Submits to get to the survey
            SendKeys.SendWait("{ENTER}");

            // Opens a form to get the date of the receipt
            Application.Run(new Form1());

            // If the form was cancelled
            if (Form1.abort)
            {
                return;
            }

            // Get the saved information about the receipt
            string month = Form1.month.ToString();
            string day = Form1.day.ToString();
            string year = Form1.year.ToString();
            int hour = Form1.hour;
            int minute = Form1.minute;
            bool MWF = Form1.MWF;

            string hourString = hour.ToString();
            string minuteString = minute.ToString();

            // If hour is 1 to 9, needs a 0 in front of it
            if (hour < 10 && hour > 0)
            {
                hourString = "0" + hourString;
            }

            // If minute is 1 to 9, needs a 0 in front of it
            if (minute < 10 && minute > 0)
            {
                minuteString = "0" + minuteString;
            }

            // Finds the survey in chrome again, or exits if not found
            FocusChrome();

            // 5 tabs to get to the date input
            for (int i = 0; i < 5; i++)
            {
                SendKeys.SendWait("{TAB}");
                System.Threading.Thread.Sleep(100);
            }

            // Input the saved date and time of the receipt
            SendKeys.SendWait(day);
            SendKeys.SendWait("/");
            SendKeys.SendWait(month);
            SendKeys.SendWait("/");
            SendKeys.SendWait(year);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait(hourString);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait(minuteString);

            // Chooses the radio button 9 for the first 4 questions
            for (int j = 0; j < 4; j++)
            {
                SendKeys.SendWait("{TAB}");
                for (int i = 0; i < 9; i++)
                {
                    SendKeys.SendWait("{RIGHT}");
                }
            }

            // Chooses the radio button 8 for the next 3 questions
            for (int j = 0; j < 3; j++)
            {
                SendKeys.SendWait("{TAB}");
                for (int i = 0; i < 8; i++)
                {
                    SendKeys.SendWait("{RIGHT}");
                }
            }

            // Chooses the radio button 9 for the last question
            SendKeys.SendWait("{TAB}");
            for (int i = 0; i < 9; i++)
            {
                SendKeys.SendWait("{RIGHT}");
            }

            // Chooses the drop down 'No' for if there's a compliment
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("n");

            // Chooses the drop down 'No' for if there's a complaint
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("n");

            // Chooses the drop down 20 for fast food visits per month
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("20");

            // Chooses the drop down 20 for subway visits per month
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("20");

            // They try to make sure you can only use the same email once for every
            // 24 hours, so I just alternate email addresses
            // Depending on if I will be handing in the receipt on a MWF or TR
            SendKeys.SendWait("{TAB}");
            if (MWF)
            {
                SendKeys.SendWait("stankiev@ualberta.ca");
            }
            else
            {
                SendKeys.SendWait("dylan.stank@gmail.com");
            }

            // No, I don't want any 'valuable' offers
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("n");

            // No, I don't want to be contacted
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("n");

            // Will submit the survey
            SendKeys.SendWait("{TAB}");
            //SendKeys.SendWait("{ENTER}");
        }

        /// <summary>
        /// Checks for the subway survey tab in google chrome. If one exists, it
        /// will focus it for input. If it couldn't find it, it will display a
        /// message box and exit the program.
        /// </summary>
        public static void FocusChrome()
        {
            // Get all current processes with the name 'chrome'
            Process[] procs = Process.GetProcessesByName("chrome");

            // If there were no chrome processes, say so and exit
            if (procs.Length == 0)
            {
                MessageBox.Show("Google Chrome is not currently open");
                Environment.Exit(0);
            }

            List<string> titles = new List<string>();

            IntPtr hWnd = IntPtr.Zero;
            int id = 0;
            bool foundSubway = false;


            foreach (Process p in procs)
            {
                // Break if any process has the subway title
                if (p.MainWindowTitle == "SUBWAY Customer Survey - Google Chrome")
                {
                    foundSubway = true;
                    hWnd = p.MainWindowHandle;
                    id = p.Id;
                    break;
                }
            }

            // If no process had the correct title, say so and exit
            if (!foundSubway)
            {
                MessageBox.Show("Couldn't find a Subway cookies tab in google chrome!");
                Environment.Exit(0);
            }

            bool isMinimized = IsIconic(hWnd);

            // If the window was minimized, restore it
            if (isMinimized)
            {
                ShowWindow(hWnd, 9);
                System.Threading.Thread.Sleep(100);
            }

            // Set focus to Google Chrome
            SetForegroundWindow(hWnd);    
            System.Threading.Thread.Sleep(100);
        }
    }
}
