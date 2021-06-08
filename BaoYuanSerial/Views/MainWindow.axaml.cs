using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using BaoYuanSerial.ViewModels;
using ReactiveUI;
using Avalonia.ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;

namespace BaoYuanSerial.Views
{
    public partial class MainWindow :Window
    {
        private readonly List<Window> _windows = new List<Window>();
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
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
        }
               
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
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
                Topmost = true,
                CanResize = false
            };
            window.ShowDialog(this);
            window.Activate();

            _windows.Add(window);
        }

        private void ToolBoxMenuClicked()
        {
            var window = new ToolBoxWindow
            {
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
    }
}
