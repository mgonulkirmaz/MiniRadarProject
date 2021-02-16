using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRadarUI
{
    public class Parser
    {

        public static Utilities.CommData CommData = new Utilities.CommData();
        //Creates communication data
        public static Utilities.CommData CreateCommData(Utilities.COMMAND cmd, string data) 
        {
            CommData.Command = cmd;
            CommData.Data = data;

            return CommData;
        }
    }
}
