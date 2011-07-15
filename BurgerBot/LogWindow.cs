using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BurgerBot
{
    public partial class LogWindow : Form
    {
        private Timer timer = new Timer();
        public LogWindow()
        {
            InitializeComponent();
            listBox1.SelectedItem = "Error";
            timer.Tick += timer_Tick;
            timer.Interval = 100;
            timer.Start();
        }

        public static string LastString = "";
        void timer_Tick(object sender, EventArgs e)
        {
            if (Log.LogString != LastString)
            {
                LastString = Log.LogString;
                richTextBox1.Text = Log.LogString;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                richTextBox1.Refresh();
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Log.LogLevel = (Log.LoggingLevel)Enum.Parse(typeof(Log.LoggingLevel), listBox1.SelectedItem.ToString(), true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Log.Clear();
            richTextBox1.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Log.SaveLogToDisk = checkBox1.Checked;
        }

        private void LogWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
        }

    }
}
