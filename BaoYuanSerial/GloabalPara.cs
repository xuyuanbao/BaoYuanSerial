using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
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
       
        public static readonly SolidColorBrush RedBrush = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush GreenBrush = new SolidColorBrush(Colors.Green);
        public static readonly SolidColorBrush TransparentBrush = new SolidColorBrush(Colors.Transparent);
    }
}
