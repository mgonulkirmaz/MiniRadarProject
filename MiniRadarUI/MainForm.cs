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
        public static float resolution = 15.0f;
        private int r = 60, maxCircles = 20; // r = 2 * desired resolution, maxCircles = maximum amount of background reference circles ->every circles = 10cm real distance
        private int redDotRadius = 18;
        private Brush brush;

        private Point origin = new Point(620 + 200, 26 + 700);
        private Point tempPoint = new Point(0, 0);
        private Point prevPoint = new Point(600, 0);
        private Rectangle rect = new Rectangle(0, 0, 100, 100);

        //Test variables

        private float counter1 = 0, multiplier = 1.2f, buf = 0.0f;
       
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
            timer.Interval = 500;
            timer.Tick += TimerTickEvent;
            timer.Start();
            RadarCore.numberOfData = (int)(180 / resolution);
            RadarCore.maxPrevValues = 6;
            RadarCore.scale = 3;
            RadarCore.InitValues();
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
                    using (Pen pen2 = new Pen(ControlPaint.Light(Color.Green), 2))
                    {
                        for (int i = 1; i <= maxCircles; i++)
                        {
                            if (i % 5 == 0)
                            {
                                pen2.Width = 4;
                                pen2.Color = ControlPaint.Light(Color.Green);
                            }                                 
                            else
                            {
                                pen2.Width = 2;
                                pen2.Color = Color.Green;
                            }                               
                            rect.X = 0 - r / 2 * i;
                            rect.Y = 0 - r / 2 * i;
                            rect.Width = r * i;
                            rect.Height = r * i;
                            g.DrawArc(pen2, rect, 0.0f, 180.0f);
                        }
                        pen2.Width = 2;
                        pen2.Color = Color.Green;
                        for (int i = 0; i <= 12; i++)
                        {
                            g.DrawLine(pen2, 0, 0, (float)((r * maxCircles * 0.5f + 30) * (Math.Cos(i * resolution * (Math.PI / 180)))), (float)((r * maxCircles * 0.5f + 30) * (Math.Sin(i * resolution * (Math.PI / 180)))));
                        }
                    }
                    for (int i = 0; i < RadarCore.numberOfData; i++)
                    {
                        value = RadarCore.GetLastValue(i);
                        tempPoint = RadarCore.CalculatePoint(value, i * resolution);

                        if (RadarCore.DetectMovement(i) == Utilities.MOVEMENT.MOVING)
                        {
                            brush = Brushes.Red;
                            g.FillEllipse(brush, tempPoint.X - redDotRadius / 2, tempPoint.Y - redDotRadius / 2, redDotRadius, redDotRadius);
                        }                           
                        else
                        {
                            if(showStaticObjects.Checked)
                            {
                                brush = Brushes.Gray;
                                g.FillEllipse(brush, tempPoint.X - redDotRadius / 2, tempPoint.Y - redDotRadius / 2, redDotRadius, redDotRadius);
                            }
                        }                                               
                    }                   
               }
            }
        }
        private void TimerTickEvent(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.Date.ToShortDateString();
            timeLabel.Text = DateTime.Now.ToShortTimeString();
            if(counter1 >= 5 && counter1 <= 10)
            {
                multiplier = -1.00f;
            }          
            if(counter1 > 10)
            {
                counter1 = 0;
                multiplier = 1.00f;
            }
            buf += 10 * multiplier; 
            RadarCore.AddNewValue(20.0f  + buf, 0);
            RadarCore.AddNewValue(150.0f + buf, 1);
            RadarCore.AddNewValue(180.0f + buf, 2);
            RadarCore.AddNewValue(60.0f + buf, 3);
            RadarCore.AddNewValue(180.0f - buf, 4);
            RadarCore.AddNewValue(200.0f - buf, 5);
            RadarCore.AddNewValue(25.0f, 6);
            RadarCore.AddNewValue(90.0f + buf, 7);
            RadarCore.AddNewValue(110.0f + buf, 8);
            RadarCore.AddNewValue(70.0f - buf, 9);
            RadarCore.AddNewValue(140.0f + buf, 10);
            RadarCore.AddNewValue(75.0f - buf, 11);

            counter1 += 1;
            updateDrawing();
        }
    }
}
