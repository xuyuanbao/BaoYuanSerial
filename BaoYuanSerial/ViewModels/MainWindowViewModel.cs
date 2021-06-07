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

namespace BaoYuanSerial.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
       
        public MainWindowViewModel()
        {
            SerialPortList = SerialService.GetSerials();
            DataBitsIndex = 3;
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
