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

namespace BaoYuanSerial.ViewModels
{
    public class AsciiCodeViewModel:ViewModelBase
    {

        public AsciiCodeViewModel()
        {
            string txtjson = "";
            try
            {
                //string name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".ascii.json";
                //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                //System.IO.Stream stream = assembly.GetManifestResourceStream(name);
                string filename = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Assets\\ascii.json";
                txtjson = File.ReadAllText(filename);
                AsciiList = JSONHelper.DeserializeJsonToObject<ObservableCollection<AsciiJson>>(txtjson);
            }
            catch
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
