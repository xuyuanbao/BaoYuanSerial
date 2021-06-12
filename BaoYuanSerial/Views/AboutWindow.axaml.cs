using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MessageBox.Avalonia.DTO;
using ReactiveUI;
using System;

namespace BaoYuanSerial.Views
{
    public partial class AboutWindow :Window
    {
        public AboutWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var OkBtn = this.FindControl<Button>("OkBtn");
            OkBtn.Command = ReactiveCommand.Create(AboutMenuClicked);

            TextBox txtBlog = this.FindControl<TextBox>("txtBlog");
            txtBlog.Tapped += new System.EventHandler<Avalonia.Interactivity.RoutedEventArgs>((obj,e)=> {
                try
                {
                    int len = txtBlog.Text.Length;
                    string html = txtBlog.Text.Substring(5, len - 5);

                    Util.FileTool.OpenWebWithUrl(html);
                }
                catch (Exception ex)
                {
                    var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {
                        ButtonDefinitions = MessageBox.Avalonia.Enums.ButtonEnum.OkAbort,
                        FontFamily = "Microsoft YaHei,Simsun",
                        ContentTitle = "Err Message",
                        ContentMessage = ex.Message,
                        Icon = MessageBox.Avalonia.Enums.Icon.Error,
                        Style = MessageBox.Avalonia.Enums.Style.UbuntuLinux
                    });
                    msBoxStandardWindow.Show();
                    
                }
                
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void AboutMenuClicked()
        {
            this.Close();
        }
     }
}
