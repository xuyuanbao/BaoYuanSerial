using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;

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
            Button btnOK = this.FindControl<Button>("btnOK");
            btnOK.Command= ReactiveCommand.Create(()=> {
                this.Close();
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
