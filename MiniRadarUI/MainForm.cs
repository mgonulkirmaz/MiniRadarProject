using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MiniRadarUI
{
    public partial class MainForm : Form
    {

        private Point origin = new Point(620 + 200, 467 + 26 + 180);
        private Point tempPoint = new Point(0, 0);

        public float value = 0.0f;
        private int resolution = 15;
        private int r = 20, maxCircles = 60;
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
            RadarCore.maxAngleCount = 180 / resolution;
            RadarCore.maxPrevValues = 10;
            RadarCore.scale = 3;
            RadarCore.InitValues();

            RadarCore.AddNewNode(50.0f, 0);
            RadarCore.AddNewNode(90.0f, 1);
            RadarCore.AddNewNode(180.0f, 2);
            RadarCore.AddNewNode(80.0f, 3);
            RadarCore.AddNewNode(100.0f, 4);
            RadarCore.AddNewNode(200.0f, 5);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.TranslateTransform(origin.X, origin.Y);
                g.ScaleTransform(1.0f, -1.0f);
                g.Clear(this.BackColor);
                using (Pen pen = new Pen(ControlPaint.Light(Color.Green), 0.1f))
                {
                    for (int i = 1; i <= maxCircles; i++)
                    {
                        g.DrawArc(pen, 0 - (r / 2) * i, 0 - (r / 2) * i, r * i, r * i, 0.0f, 180.0f);
                    }

                    for (int i = 0; i <= 12; i++)
                    {
                        g.DrawLine(pen, 0, 0, (float)((r * maxCircles * 0.5f + 30) * (Math.Cos(i * 15 * (Math.PI / 180)))), (float)((r * maxCircles * 0.5f + 30) * (Math.Sin(i * 15 * (Math.PI / 180)))));
                    }
                }
            }
        }
        private void updateDrawing()
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.TranslateTransform(origin.X, origin.Y);
                g.ScaleTransform(1.0f, -1.0f);
                g.Clear(this.BackColor);
                using (Pen pen = new Pen(Color.White, 2))
                {
                    using (Pen pen2 = new Pen(ControlPaint.Light(Color.Green), 1))
                    {
                        for (int i = 1; i <= maxCircles; i++)
                        {
                            g.DrawArc(pen2, 0 - (r / 2) * i, 0 - (r / 2) * i, r * i, r * i, 0.0f, 180.0f);
                        }

                        for (int i = 0; i <= 12; i++)
                        {
                            g.DrawLine(pen2, 0, 0, (float)((r * maxCircles * 0.5f + 30) * (Math.Cos(i * 15 * (Math.PI / 180)))), (float)((r * maxCircles * 0.5f + 30) * (Math.Sin(i * 15 * (Math.PI / 180)))));
                        }
                    }

                    for (int i = 0; i < RadarCore.maxAngleCount; i++)
                    {
                        value = RadarCore.GetLastValue(i);
                        tempPoint = RadarCore.CalculateEndPoint(value, i * 15);
                        g.DrawLine(pen, 0, 0, tempPoint.X, tempPoint.Y);

                        if (tempPoint.X != 0 || tempPoint.Y != 0)
                        {
                            g.DrawPie(pen, 0 - value * 3.0f, 0 - value * 3.0f, value * 2 * RadarCore.scale, value * 2 * RadarCore.scale, 0.0f + i * 15.0f, 15.0f);
                        }
                    }                   
               }
            }
        }
        private void TimerOneSecTick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.Date.ToShortDateString();
            timeLabel.Text = DateTime.Now.ToShortTimeString();

            updateDrawing();
        }
    }
}
