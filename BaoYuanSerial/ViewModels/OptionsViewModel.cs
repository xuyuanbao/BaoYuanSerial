using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI;
using BaoYuanSerial.Models;

namespace BaoYuanSerial.ViewModels
{
   public class OptionsViewModel:ViewModelBase
    {

        public OptionsViewModel()
        {
            _DisplayPara = GloabalPara.DisplayPara;
        }

        private DisplayPara _DisplayPara;
        public DisplayPara DisplayPara
        {
            get => _DisplayPara;
            set
            {
                _DisplayPara = value;
                this.RaisePropertyChanged();
            }
        }
    }
}
