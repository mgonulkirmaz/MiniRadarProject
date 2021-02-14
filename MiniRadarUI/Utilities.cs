using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRadarUI
{
    public class Utilities
    {
        public enum COMMAND {NEW_DATA, MANUAL_CONTROL, AUTO_CONTROL, CLEAR_DATA, UNKNOWN_COMMAND};
        public enum CONNECTION {CONNECTION_OFF, CONNECTION_ON};
        public enum MOVEMENT {NO_MOVEMENT, MOVING};
        public struct Node  //Single Radar data for an Angle
        {
            public COMMAND Command;
            public double Angle;
            public double Distance;
        };
        public struct CommData
        {
            public COMMAND Command;
            public string Data;
        };
        public static COMMAND CommandControl(string input)
        {
            switch (Convert.ToInt32(input))
            {
                case 0:
                    return COMMAND.NEW_DATA;
                case 1:
                    return COMMAND.MANUAL_CONTROL;
                case 2:
                    return COMMAND.AUTO_CONTROL;
                case 3:
                    return COMMAND.CLEAR_DATA;
                default:
                    return COMMAND.UNKNOWN_COMMAND;
            }
        }
    }
}
