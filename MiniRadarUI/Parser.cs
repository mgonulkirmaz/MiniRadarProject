using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRadarUI
{
    public class Parser
    {

        public static Utilities.Node Split(string input)   //Splits input string to Angle and Distance values
        {
            string[] inputSplitted = input.Split(';');
            Utilities.Node node;

            if (Utilities.CommandControl(inputSplitted[0]) == Utilities.COMMAND.NEW_DATA)
            {
                node.Command = Utilities.COMMAND.NEW_DATA;
                node.Angle = Convert.ToDouble(inputSplitted[1]);
                node.Distance = Convert.ToDouble(inputSplitted[2]);
            }
            else
            {
                node.Command = Utilities.COMMAND.UNKNOWN_COMMAND;
                node.Angle = 0;
                node.Distance = 0;
            }
                
            return node;
        }

        public static Utilities.CommData CreateCommData(Utilities.COMMAND cmd, string data)
        {
            Utilities.CommData CommData;
            CommData.Command = cmd;
            CommData.Data = data;

            return CommData;
        }
    }
}
