using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRadarUI
{
    public class Utilities
    {
        // Useable commands
        public enum COMMAND {NEW_DATA, MANUAL_CONTROL, AUTO_CONTROL, CLEAR_DATA, UNKNOWN_COMMAND};
        // Connection status
        public enum CONNECTION {CONNECTION_OFF, CONNECTION_ON};
        // Useable connection types
        public enum COMMUNICATION_TYPE {SERIAL}
        // For movement detection
        public enum MOVEMENT {STATIC, MOVING};
        // Communication data object
        public class CommData
        {
            public COMMAND Command;
            public string Data;
        };
        // Returns inputted string as Command type
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
