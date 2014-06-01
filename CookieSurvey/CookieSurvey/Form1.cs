using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace CookieSurvey
{
    public partial class Form1 : Form
    {
        public static int month;
        public static int day;
        public static int year;
        public static int hour;
        public static int minute;
        public static bool MWF;
        public static bool abort;

        public Form1()
        {
            abort = true;
            InitializeComponent();

        }

        void Form1_Shown(object sender, System.EventArgs e)
        {
            var process = Process.GetCurrentProcess();
            var handle = process.MainWindowHandle;
            Program.SetForegroundWindow(handle);
            Program.ShowWindow(handle, 9); // restore
        }


        private void button1_Click(object sender, EventArgs e)
        {
            abort = false;
            month = this.DatePicker.Value.Month;
            day = this.DatePicker.Value.Day;
            year = this.DatePicker.Value.Year;
            hour = (int)this.HourPicker.Value;
            minute = (int)this.MinutePicker.Value;
            MWF = this.CheckBox.Checked;

            this.Close();
        }

        private void HourPicker_Enter(object sender, EventArgs e)
        {
            this.HourPicker.Select(0, this.HourPicker.Text.Length);
        }

        private void MinutePicker_Enter(object sender, EventArgs e)
        {
            this.MinutePicker.Select(0, this.MinutePicker.Text.Length);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
