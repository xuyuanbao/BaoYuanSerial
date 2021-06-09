using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaoYuanSerial.Models;

namespace BaoYuanSerial
{
    public class GloabalPara
    {
        public static string SysPlattform = "";

        public static ReceivePara ReceivePara = new ReceivePara();
        public static SendPara SendPara = new SendPara();
        public static LogPara LogPara = new LogPara();
        public static DisplayPara DisplayPara = new DisplayPara();
       
    }
}
