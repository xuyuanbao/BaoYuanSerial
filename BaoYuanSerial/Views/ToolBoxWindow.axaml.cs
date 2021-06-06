using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BaoYuanSerial.Views
{
    public partial class ToolBoxWindow : Window
    {
        public ToolBoxWindow()
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
