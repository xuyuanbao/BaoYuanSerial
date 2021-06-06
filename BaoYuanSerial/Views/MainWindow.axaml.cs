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
    }
}
