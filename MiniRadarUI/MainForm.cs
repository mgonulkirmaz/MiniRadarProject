using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniRadarUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitAppButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        Timer timerOneSec;

        private void MainForm_Load(object sender, EventArgs e)
        {
            timerOneSec = new Timer();
            timerOneSec.Interval = 1000;
            timerOneSec.Tick += TimerOneSecTick;
            timerOneSec.Start();
        }

        private void TimerOneSecTick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.Date.ToShortDateString();
            timeLabel.Text = DateTime.Now.ToShortTimeString();
        }
    }
}
