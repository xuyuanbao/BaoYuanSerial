using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI;
using BaoYuanSerial.Base;

namespace BaoYuanSerial.Models
{
    public class SendPara: NotifyPropertyBase
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

        private bool _IsLoop = false;

        public bool IsLoop
        {
            get => _IsLoop;
            set
            {
                _IsLoop = value;
                this.RaisePropertyChanged();
            }
        }

        private int _LoopInterval = 1000;

        public int LoopInterval
        {
            get => _LoopInterval;
            set
            {
                _LoopInterval = value;
                this.RaisePropertyChanged();
            }
        }

        private bool _CommentSupport = false;

        public bool CommentSupport
        {
            get => _CommentSupport;
            set
            {
                _CommentSupport = value;
                this.RaisePropertyChanged();
            }
        }
        private bool _FormatSend = false;

        public bool FormatSend
        {
            get => _FormatSend;
            set
            {
                _FormatSend = value;
                this.RaisePropertyChanged();
            }
        }

        private bool _FormatNewLine = true;

        public bool FormatNewLine
        {
            get => _FormatNewLine;
            set
            {
                _FormatNewLine = value;
                this.RaisePropertyChanged();
            }
        }
        private bool _FormatCarReturn = false;

        public bool FormatCarReturn
        {
            get => _FormatCarReturn;
            set
            {
                _FormatCarReturn = value;
                this.RaisePropertyChanged();
            }
        }
        private bool _FormatNLCR = false;

        public bool FormatNLCR
        {
            get => _FormatNLCR;
            set
            {
                _FormatNLCR = value;
                this.RaisePropertyChanged();
            }
        }
        private bool _FormatCLNR = false;

        public bool FormatCLNR
        {
            get => _FormatCLNR;
            set
            {
                _FormatCLNR = value;
                this.RaisePropertyChanged();
            }
        }

    }
}
