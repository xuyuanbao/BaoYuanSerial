using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI;
using BaoYuanSerial.Base;

namespace BaoYuanSerial.Models
{
    public class DisplayPara : NotifyPropertyBase
    {

        private bool _FormatDisColor = false;

        public bool FormatDisColor
        {
            get => _FormatDisColor;
            set
            {
                _FormatDisColor = value;
                this.RaisePropertyChanged();
            }
        }

        private string _ReceiveTxtColor = "#000000";

        public string ReceiveTxtColor
        {
            get => _ReceiveTxtColor;
            set
            {
                _ReceiveTxtColor = value;
                this.RaisePropertyChanged();
            }
        }
        private string _SendTxtColor = "#000000";

        public string SendTxtColor
        {
            get => _SendTxtColor;
            set
            {
                _SendTxtColor = value;
                this.RaisePropertyChanged();
            }
        }

    }
}
