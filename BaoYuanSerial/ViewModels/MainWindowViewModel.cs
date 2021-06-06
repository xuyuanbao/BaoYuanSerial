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

    }
}
