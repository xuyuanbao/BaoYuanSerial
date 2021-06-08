using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI;
using BaoYuanSerial.Base;

namespace BaoYuanSerial.Models
{
    public class LogPara : NotifyPropertyBase
    {
        private bool _SaveLogMsg = false;

        public bool SaveLogMsg
        {
            get => _SaveLogMsg;
            set
            {
                _SaveLogMsg = value;
                this.RaisePropertyChanged();
            }
        }

        private bool _EnableWriteBuf = false;

        public bool EnableWriteBuf
        {
            get => _EnableWriteBuf;
            set
            {
                _EnableWriteBuf = value;
                this.RaisePropertyChanged();
            }
        }
        private string _FileName = "log_"+DateTime.Now.ToString("yyyyMMdd")+".txt";

        public string FileName
        {
            get => _FileName;
            set
            {
                _FileName = value;
                this.RaisePropertyChanged();
            }
        }

        private int _MaxFileSize = 1024;  //MB

        public int MaxFileSize
        {
            get => _MaxFileSize;
            set
            {
                _MaxFileSize = value;
                this.RaisePropertyChanged();
            }
        }

        private int _BufSize = 1024;  //KB

        public int BufSize
        {
            get => _BufSize;
            set
            {
                _BufSize = value;
                this.RaisePropertyChanged();
            }
        }

    }
}
