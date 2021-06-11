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
            this.DataContext = new AsciiCodeViewModel();
                        
            Button btnOK = this.FindControl<Button>("btnOK");
            //...
            btnOK.Tapped += (s, e) =>
            {
                this.Close();
            };

        }



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
