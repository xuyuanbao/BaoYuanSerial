using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using BaoYuanSerial.Views;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using System.Collections.ObjectModel;
using BaoYuanSerial.Services;
using Avalonia.Media;
using BaoYuanSerial.Models;

namespace BaoYuanSerial.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
       
        public MainWindowViewModel()
        {
            SerialPortList = SerialService.GetSerials();
            DataBitsIndex = 3;
            _ComPortState = SerialPortList[PortNameIndex] + " ClOSED";

            ReceivePara = GloabalPara.ReceivePara;
            SendPara = GloabalPara.SendPara;

        }

        private ObservableCollection<string> _SerialPortList = new ObservableCollection<string>();

        public ObservableCollection<string> SerialPortList
        {
            get => _SerialPortList;
            set
            {
                _SerialPortList = value;
                this.RaisePropertyChanged();
            }
        }

        private int _PortNameIndex = 0;

        public int PortNameIndex
        {
            get => _PortNameIndex;
            set
            {
                _PortNameIndex = value;
                this.RaisePropertyChanged();
            }
        }

       

        private ObservableCollection<int> _BaudRateList = new ObservableCollection<int>() { 9600,19200,38400,57600,115200,1200,2400,4800};

        public ObservableCollection<int> BaudRateList
        {
            get => _BaudRateList;
            set
            {
                _BaudRateList = value;
                this.RaisePropertyChanged();
            }
        }

        private int _BaudRateIndex = 0;

        public int BaudRateIndex
        {
            get => _BaudRateIndex;
            set
            {
                _BaudRateIndex = value;
                this.RaisePropertyChanged();
            }
        }

        private ObservableCollection<int> _DataBitsList = new ObservableCollection<int>() { 5,6,7,8};

        public ObservableCollection<int> DataBitsList
        {
            get => _DataBitsList;
            set
            {
                _DataBitsList = value;
                this.RaisePropertyChanged();
            }
        }

        private int _DataBitsIndex = 0;
        public int DataBitsIndex
        {
            get => _DataBitsIndex;
            set
            {
                _DataBitsIndex = value;
                this.RaisePropertyChanged();
            }
        }

        private int _Parity = 0;
        public int Parity
        {
            get => _Parity;
            set
            {
                _Parity = value;
                this.RaisePropertyChanged();
            }
        }

        private int _StopBits= 0;
        public int StopBits
        {
            get => _StopBits;
            set
            {
                _StopBits = value;
                this.RaisePropertyChanged();
            }
        }

        private int _FlowType = 0;
        public int FlowType
        {
            get => _FlowType;
            set
            {
                _FlowType = value;
                this.RaisePropertyChanged();
            }
        }

        private string _ComPortState = "";

        public string ComPortState
        {
            get => _ComPortState;
            set
            {
                _ComPortState = value;
                this.RaisePropertyChanged();
            }
        }
        private SolidColorBrush _ComPortStateColor = new SolidColorBrush(Colors.Red);
        public SolidColorBrush ComPortStateColor
        {
            get => _ComPortStateColor;
            set
            {
                _ComPortStateColor = value;
                this.RaisePropertyChanged();
            }
        }

        private string _SendBytesStr = "Tx:0 Bytes";
        public string SendBytesStr
        {
            get => _SendBytesStr;
            set
            {
                _SendBytesStr = value;
                this.RaisePropertyChanged();
            }
        }

        private SolidColorBrush _SendBytesColor = new SolidColorBrush(Colors.Green);
        public SolidColorBrush SendBytesColor
        {
            get => _SendBytesColor;
            set
            {
                _SendBytesColor = value;
                this.RaisePropertyChanged();
            }
        }
        private string _ReceiveBytesStr="Rx:0 Bytes";
        public string ReceiveBytesStr
        {
            get => _ReceiveBytesStr;
            set
            {
                _ReceiveBytesStr = value;
                this.RaisePropertyChanged();
            }
        }
        private SolidColorBrush _ReceiveBytesColor = new SolidColorBrush(Colors.BlueViolet);
        public SolidColorBrush ReceiveBytesColor
        {
            get => _ReceiveBytesColor;
            set
            {
                _ReceiveBytesColor = value;
                this.RaisePropertyChanged();
            }
        }

        private ReceivePara _ReceivePara = new ReceivePara();
        public ReceivePara ReceivePara
        {
            get=> _ReceivePara;
            set
            {
                _ReceivePara = value;
                this.RaisePropertyChanged();
            }

        }

        private SendPara _SendPara = new SendPara();
        public SendPara SendPara
        {
            get => _SendPara;
            set
            {
                _SendPara = value;
                this.RaisePropertyChanged();
            }

        }

        private string _ReciveTxt = "";

        public string ReciveTxt
        {
            get => _ReciveTxt;
            set
            {
                _ReciveTxt = value;
                this.RaisePropertyChanged();
            }
        }

        


    }
}
