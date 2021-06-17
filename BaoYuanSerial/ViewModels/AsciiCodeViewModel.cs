using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using BaoYuanSerial.Models;
using BaoYuanSerial.Util;
using ReactiveUI;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Platform;
using Newtonsoft.Json;

namespace BaoYuanSerial.ViewModels
{
    public class AsciiCodeViewModel:ViewModelBase
    {

        public AsciiCodeViewModel()
        {
            //string txtjson = "";
            try
            {
                var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                Uri uri = new Uri($"avares://BaoYuanSerial/Assets/ascii.json");
                if (assets.Exists(uri))
                {
                    using (StreamReader sr = new StreamReader(assets.Open(uri), Encoding.UTF8))
                    {
                        AsciiList = JsonConvert.DeserializeObject<ObservableCollection<AsciiJson>>(sr.ReadToEnd());
                    }
                }
                //string filename = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Assets/ascii.json";
                //txtjson = File.ReadAllText(filename);
                //AsciiList = JSONHelper.DeserializeJsonToObject<ObservableCollection<AsciiJson>>(txtjson);
            }
            catch(Exception ex)
            {
                
            }
            
            
        }
        private ObservableCollection<AsciiJson> _AsciiList = new ObservableCollection<AsciiJson>();
       public ObservableCollection<AsciiJson> AsciiList
        {
            get => _AsciiList;
            set
            {
                _AsciiList = value;
                this.RaisePropertyChanged();
            }
            
        }
    }
}
