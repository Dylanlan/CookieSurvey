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
        // Used to save information about the receipt
        public static int month;
        public static int day;
        public static int year;
        public static int hour;
        public static int minute;
        public static bool MWF;

        // Used to see whether the form was cancelled
        public static bool abort;

        /// <summary>
        /// Initializes the form for receipt input
        /// </summary>
        public Form1()
        {
            abort = true;
            InitializeComponent();
        }

        /// <summary>
        /// Called when the Form should be shown, ensures that
        /// it is displayed properly
        /// </summary>
        /// <param name="sender">
        /// The caller of this method
        /// </param>
        /// <param name="e">
        /// The arguments associated with this operation
        /// </param>
        void Form1_Shown(object sender, EventArgs e)
        {
            // Gets the current process and window handle to focus it
            var process = Process.GetCurrentProcess();
            var handle = process.MainWindowHandle;
            Program.SetForegroundWindow(handle);
            Program.ShowWindow(handle, 9); // restore
        }

        /// <summary>
        /// Called when the 'Ok' button is clicked. Saves the
        /// receipt information, and signals that the form was not
        /// cancelled
        /// </summary>
        /// <param name="sender">
        /// The caller of this method
        /// </param>
        /// <param name="e">
        /// The arguments associated with this operation
        /// </param>
        private void button1_Click(object sender, EventArgs e)
        {
            // Form was not cancelled
            abort = false;

            // Save receipt information
            month = this.DatePicker.Value.Month;
            day = this.DatePicker.Value.Day;
            year = this.DatePicker.Value.Year;
            hour = (int)this.HourPicker.Value;
            minute = (int)this.MinutePicker.Value;
            MWF = this.CheckBox.Checked;

            // Close this form
            this.Close();
        }

        /// <summary>
        /// When the numericUpDown is entered, it selects all the
        /// current text, so it is easy to input the receipt information
        /// </summary>
        /// <param name="sender">
        /// The caller of this method
        /// </param>
        /// <param name="e">
        /// Arguments for this operation
        /// </param>
        private void HourPicker_Enter(object sender, EventArgs e)
        {
            // Highlights all current text, easy to overwrite with correct information
            this.HourPicker.Select(0, this.HourPicker.Text.Length);
        }

        /// <summary>
        /// When the numericUpDown is entered, it selects all the
        /// current text, so it is easy to input the receipt information
        /// </summary>
        /// <param name="sender">
        /// The caller of this method
        /// </param>
        /// <param name="e">
        /// Arguments for this operation
        /// </param>
        private void MinutePicker_Enter(object sender, EventArgs e)
        {
            // Highlights all current text, easy to overwrite with correct information
            this.MinutePicker.Select(0, this.MinutePicker.Text.Length);
        }

        /// <summary>
        /// Called when the 'Cancel' button is clicked. Since the abort field is only
        /// set to false from clicking 'Ok', it should still be true. So it just
        /// closes the form.
        /// </summary>
        /// <param name="sender">
        /// The caller of this method
        /// </param>
        /// <param name="e">
        /// The arguments associated with this operation
        /// </param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
