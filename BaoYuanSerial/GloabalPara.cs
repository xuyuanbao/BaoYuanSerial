using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using BaoYuanSerial.Models;
using Newtonsoft.Json;

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
        public static LocSet LocSet=new LocSet();
        

        public static void GetLocSet()
        {            
            try
            {
                var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

                Uri uri = new Uri($"avares://BaoYuanSerial/Assets/locSet.json");
                if (assets.Exists(uri))
                {
                    using (StreamReader sr = new StreamReader(assets.Open(uri), Encoding.UTF8))
                    {
                        LocSet = Util.JSONHelper.DeserializeJsonToObject<LocSet>(sr.ReadToEnd());
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }
       

    }
}
