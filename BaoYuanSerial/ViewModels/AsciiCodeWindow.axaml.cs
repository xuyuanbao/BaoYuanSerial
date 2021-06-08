using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BaoYuanSerial.ViewModels
{
    public partial class AsciiCodeWindow : Window
    {
        public AsciiCodeWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
