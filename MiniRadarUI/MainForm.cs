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

        public float value = 0.0f;
        private int resolution = 15;
        private int r = 20, maxCircles = 60;

        private Point origin = new Point(620 + 200, 467 + 26 + 180);
        private Point tempPoint = new Point(0, 0);
        private Point prevPoint = new Point(600, 0);

        //Test variables

        private float counter1 = 0, multiplier = 1.2f;
       
        public MainForm()
        {
            InitializeComponent();
        }
        private void exitAppButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Timer timer;
        private void MainForm_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += TimerOneSecTick;
            timer.Start();
            RadarCore.numberOfData = 180 / resolution;
            RadarCore.maxPrevValues = 10;
            RadarCore.scale = 3;
            RadarCore.InitValues();
            // Test data
            RadarCore.AddNewValue(50.0f, 0);
            RadarCore.AddNewValue(90.0f, 1);
            RadarCore.AddNewValue(180.0f, 2);
            RadarCore.AddNewValue(80.0f, 3);
            RadarCore.AddNewValue(100.0f, 4);
            RadarCore.AddNewValue(200.0f, 5);
            RadarCore.AddNewValue(85.0f, 6);
            RadarCore.AddNewValue(90.0f, 7);
            RadarCore.AddNewValue(110.0f, 8);
            RadarCore.AddNewValue(20.0f, 9);
            RadarCore.AddNewValue(190.0f, 10);
            RadarCore.AddNewValue(55.0f, 11);


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

                    for (int i = 0; i <= 180 / resolution; i++)
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

                prevPoint.X = 600;
                prevPoint.X = 0;

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
                            g.DrawLine(pen2, 0, 0, (float)((r * maxCircles * 0.5f + 30) * (Math.Cos(i * resolution * (Math.PI / 180)))), (float)((r * maxCircles * 0.5f + 30) * (Math.Sin(i * resolution * (Math.PI / 180)))));
                        }
                    }

                    for (int i = 0; i < RadarCore.numberOfData; i++)
                    {
                        value = RadarCore.GetLastValue(i);
                        tempPoint = RadarCore.CalculateEndPoint(value, i * resolution);

                        if(i != 0)
                            g.DrawLine(pen, prevPoint.X, prevPoint.Y, tempPoint.X, tempPoint.Y);

                        prevPoint = RadarCore.CalculateNextPoint(value, i * resolution);

                        if (tempPoint.X != 0 || tempPoint.Y != 0)
                        {
                            g.DrawArc(pen, 0 - value * RadarCore.scale, 0 - value * RadarCore.scale, value * 2 * RadarCore.scale, value * 2 * RadarCore.scale, 0.0f + i * resolution, resolution);
                        }
                    }                   
               }
            }
        }
        private void TimerOneSecTick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.Date.ToShortDateString();
            timeLabel.Text = DateTime.Now.ToShortTimeString();

            if(counter1 >= 5)
            {
                multiplier = 0.95f;
            }
            
            if(counter1 > 10)
            {
                counter1 = 0;
                multiplier = 1.05f;
            }

            RadarCore.AddNewValue(50.0f * multiplier, 0);
            RadarCore.AddNewValue(90.0f * multiplier, 1);
            RadarCore.AddNewValue(180.0f * multiplier, 2);
            RadarCore.AddNewValue(80.0f * multiplier, 3);
            RadarCore.AddNewValue(100.0f * multiplier, 4);
            RadarCore.AddNewValue(200.0f * multiplier, 5);
            RadarCore.AddNewValue(85.0f * multiplier, 6);
            RadarCore.AddNewValue(90.0f * multiplier, 7);
            RadarCore.AddNewValue(110.0f * multiplier, 8);
            RadarCore.AddNewValue(70.0f * multiplier, 9);
            RadarCore.AddNewValue(190.0f * multiplier, 10);
            RadarCore.AddNewValue(55.0f * multiplier, 11);

            counter1 += 1;

            updateDrawing();
        }
    }
}
