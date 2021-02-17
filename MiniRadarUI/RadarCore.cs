using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRadarUI
{
    public class RadarCore
    {
        public static Dictionary<int, List<float>> values = new Dictionary<int, List<float>>(); 

        public static int maxPrevValues;
        public static int numberOfData;
        public static int scale;
        public static void InitValues()
        {
            for (int i = 0; i < numberOfData; i++)
            {
                values.Add(i, new List<float>());
                for (int j = 0; j < maxPrevValues; j++)
                {                   
                    values[i].Add(0.0f);
                }
            }
        }           
        public static void AddNewValue(float value, int angleIndex)
        {
            if(angleIndex >= 0 && angleIndex < numberOfData)
            {
                for (int i = 0; i < maxPrevValues - 1; i++)
                {
                    values[angleIndex][i] = values[angleIndex][i + 1];
                }
                values[angleIndex][maxPrevValues - 1] = value;
            }           
        }
        public static float GetLastValue(int angleIndex)
        {
            if (angleIndex >= 0 && angleIndex < numberOfData)
                return values[angleIndex][maxPrevValues - 1];
            else
                return 0.0f;
        }
        public static Point CalculateEndPoint(float value, float angle)
        {
            Point endPoint = new Point(0, 0);

            endPoint.X = (int)(scale * value * Math.Cos(angle * (Math.PI / 180)));
            endPoint.Y = (int)(scale * value * Math.Sin(angle * (Math.PI / 180)));

            return endPoint;
        }

        public static Point CalculateNextPoint(float value, float angle)
        {
            Point endPoint = new Point(0, 0);

            endPoint.X = (int)(scale * value * Math.Cos((angle + 180 / numberOfData) * (Math.PI / 180)));
            endPoint.Y = (int)(scale * value * Math.Sin((angle + 180 / numberOfData) * (Math.PI / 180)));

            return endPoint;
        }
    }
}
