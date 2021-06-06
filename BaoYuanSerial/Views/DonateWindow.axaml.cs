using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BaoYuanSerial.Views
{
    public partial class DonateWindow : Window
    {
        public DonateWindow()
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
