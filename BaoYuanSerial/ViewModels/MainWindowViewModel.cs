using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using BaoYuanSerial.Views;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;

namespace BaoYuanSerial.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
       
        public MainWindowViewModel()
        {
                        
        }


        private string _ReciveTxt = "123";

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
