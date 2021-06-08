using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI;
using BaoYuanSerial.Base;

namespace BaoYuanSerial.Models
{
    public class ReceivePara:NotifyPropertyBase
    {

        private bool _IsText = false;

        public bool IsText
        {
            get => _IsText;
            set
            {
                _IsText = value;
                this.RaisePropertyChanged();
            }
        }
        private bool _IsHex = true;

        public bool IsHex
        {
            get => _IsHex;
            set
            {
                _IsHex = value;
                this.RaisePropertyChanged();
            }
        }

        private bool _AutoFeed = false;

        public bool AutoFeed
        {
            get => _AutoFeed;
            set
            {
                _AutoFeed = value;
                this.RaisePropertyChanged();
            }
        }
        private bool _DisplaySend = false;

        public bool DisplaySend
        {
            get => _DisplaySend;
            set
            {
                _DisplaySend = value;
                this.RaisePropertyChanged();
            }
        }

        private bool _DisplayTime = false;

        public bool DisplayTime
        {
            get => _DisplayTime;
            set
            {
                _DisplayTime = value;
                this.RaisePropertyChanged();
            }
        }

        private int _MinimalInterval = 500;

        public int MinimalInterval
        {
            get => _MinimalInterval;
            set
            {
                _MinimalInterval = value;
                this.RaisePropertyChanged();
            }
        }

        private string _TimeFormat = "[hh:mm:ss.zzz] ";

        public string TimeFormat
        {
            get => _TimeFormat;
            set
            {
                _TimeFormat = value;
                this.RaisePropertyChanged();
            }
        }

    }
}
