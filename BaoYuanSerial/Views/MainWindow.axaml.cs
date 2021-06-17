using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using BaoYuanSerial.ViewModels;
using ReactiveUI;

using System.Reactive;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Avalonia.Styling;

using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using BaoYuanSerial.Models;
using System;
using Avalonia.Markup.Xaml.MarkupExtensions;
using BaoYuanSerial.Util;

namespace BaoYuanSerial.Views
{
    public partial class MainWindow :Window
    {
        private readonly List<Window> _windows = new List<Window>();
        
        MainWindowViewModel _thisViewModel;
        CheckBox _chbLoop;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            //Instance = this;
            var AboutMenu = this.FindControl<MenuItem>("AboutMenu");
            AboutMenu.Command = ReactiveCommand.Create(AboutMenuClicked);
            var DonateMenu = this.FindControl<MenuItem>("DonateMenu");
            DonateMenu.Command = ReactiveCommand.Create(DonateMenuClicked);
            var OptionsMenu = this.FindControl<MenuItem>("OptionsMenu");
            OptionsMenu.Command = ReactiveCommand.Create(OptionsMenuClicked);
            var tlbtnOptions = this.FindControl<Button>("tlbtnOptions");
            tlbtnOptions.Command = ReactiveCommand.Create(OptionsMenuClicked);
            var ToolBoxMenu = this.FindControl<MenuItem>("ToolBoxMenu");
            ToolBoxMenu.Command = ReactiveCommand.Create(ToolBoxMenuClicked);
            var AsciiCodeMenu = this.FindControl<MenuItem>("AsciiCodeMenu");
            AsciiCodeMenu.Command = ReactiveCommand.Create(AsciiCodeMenuClicked);
            var ChineseMenu = this.FindControl<MenuItem>("ChineseMenu");
            ChineseMenu.Command = ReactiveCommand.Create(ChineseMenuClicked);
            var EnglishMenu = this.FindControl<MenuItem>("EnglishMenu");
            EnglishMenu.Command = ReactiveCommand.Create(EnglishMenuClicked);
            Button btnHideLeft = this.FindControl<Button>("btnHideLeft");
            btnHideLeft.Click += btnHideLeft_Click;
            ComboBox CbbHistory = this.FindControl<ComboBox>("CbbHistory");
            CbbHistory.SelectionChanged += SendHistoryComb_Changed;
            _chbLoop = this.FindControl<CheckBox>("chbLoop");            
            _chbLoop.Checked += LoopChbox_Checked;  //当checkbox未选中转为选中时触发。
            _chbLoop.Unchecked += LoopChbox_UnChecked;  //当checkbox选中转为未选中时触发

        }
               
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }


        public void LoopChbox_Checked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _thisViewModel = (MainWindowViewModel)this.DataContext;
            if (_thisViewModel != null)
            {
                if (_thisViewModel._serialPort == null || _thisViewModel.SendTxt.Trim() == "")
                {
                    var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {
                        ButtonDefinitions = ButtonEnum.Ok,
                        ContentTitle = "Tips",
                        ContentMessage = "Please Open ComPort First Or SendText is not Empty",
                        Icon = MessageBox.Avalonia.Enums.Icon.Info,
                        FontFamily = "Microsoft YaHei,Simsun",
                        WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterScreen,
#if Ubuntu
                    Style =MessageBox.Avalonia.Enums.Style.UbuntuLinux
#elif MSWindow
                    Style = MessageBox.Avalonia.Enums.Style.Windows
#endif
                });
                    msBoxStandardWindow.Show();
                    _chbLoop.IsChecked = false;
                    return;
                }               
                _thisViewModel.SendCmdIsEnable = false;
                _thisViewModel.SendPara.IsLoop = true;
                Task.Factory.StartNew(async() => {
                    while(_thisViewModel.SendPara.IsLoop)
                    {
                        await _thisViewModel.SendCmdLoop();
                    }                       
                });
            }
        }

        public void LoopChbox_UnChecked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _thisViewModel = (MainWindowViewModel)this.DataContext;
            if (_thisViewModel != null)
            {
                _thisViewModel.SendCmdIsEnable = true;
                _thisViewModel.SendPara.IsLoop = false;
            }
        }

        private void btnHideLeft_Click(object obj, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SplitView splitView = this.FindControl<SplitView>("viewSplit");
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }
        private void SendHistoryComb_Changed(object obj, SelectionChangedEventArgs e)
        {
            _thisViewModel = (MainWindowViewModel)this.DataContext;
            if(_thisViewModel != null)
            {
                _thisViewModel.SendTxt= _thisViewModel.SendTxtHistory[_thisViewModel.SendTxtHisSelIndex];
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            foreach (var window in _windows)
            {
                window.Close();
            }
        }
        private void AboutMenuClicked()
        {
            var window = new AboutWindow
            {
                DataContext = new AboutViewModel(),
                Topmost = true,
                CanResize = false
               
            };
            window.ShowDialog(this);
            window.Activate();

            _windows.Add(window);
        }
        private void DonateMenuClicked()
        {
            var window = new DonateWindow
            {                
                Topmost = true,
                CanResize = false
            };
            window.ShowDialog(this);
            window.Activate();

            _windows.Add(window);
        }
        private void OptionsMenuClicked()
        {
            var window = new OptionsWindow
            {
                DataContext = new OptionsViewModel(),
                Topmost = true,
                CanResize = false
            };
            window.Show();
            window.Activate();

            _windows.Add(window);
        }

        private void ToolBoxMenuClicked()
        {
            var window = new ToolBoxWindow
            {
                DataContext=new ToolBoxViewModel(),
                Topmost = true,
                CanResize = false
            };
            window.ShowDialog(this);
            window.Activate();

            _windows.Add(window);
        }

        private void AsciiCodeMenuClicked()
        {
            var window = new AsciiCodeWindow
            {
                Topmost = true,
                CanResize = false
            };
            window.ShowDialog(this);
            window.Activate();

            _windows.Add(window);
        }

        private void ChineseMenuClicked()
        {
            Localizer.Instance.LoadLanguage("zh-CN");
        }
        private void EnglishMenuClicked()
        {
            Localizer.Instance.LoadLanguage("en-US");
        }
        private void OnLanguageChanged(object sender, SelectionChangedEventArgs args)
        {
            var cb = sender as ComboBox;
            var language = cb.SelectedIndex == 0 ? "en-US" : "zh-CN";
            Localizer.Instance.LoadLanguage(language);
        }
    }
}
