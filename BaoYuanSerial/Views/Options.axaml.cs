using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BaoYuanSerial.ViewModels;
using System;

namespace BaoYuanSerial.Views
{
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.DataContext = new OptionsViewModel();
            TextBox txtColorReceive = this.FindControl<TextBox>("txtColorReceive");
            AvaloniaColorPicker.ColorButton button = this.FindControl<AvaloniaColorPicker.ColorButton>("btnColorReceive");
            //...
            button.PropertyChanged += (s, e) =>
            {
                if (e.Property == AvaloniaColorPicker.ColorButton.ColorProperty)
                {
                    OptionsViewModel ovm = (OptionsViewModel)this.DataContext;
                    if (ovm != null)
                    { 
                        ovm.DisplayPara.ReceiveTxtColor = button.Color.ToString();
                    }
                   // txtColorReceive.Text = button.Color.ToString();
                }
            };
            TextBox txtColorSend = this.FindControl<TextBox>("txtColorSend");
            AvaloniaColorPicker.ColorButton btnColorSend = this.FindControl<AvaloniaColorPicker.ColorButton>("btnColorSend");
            //...
            btnColorSend.PropertyChanged += (s, e) =>
            {
                if (e.Property == AvaloniaColorPicker.ColorButton.ColorProperty)
                {
                    OptionsViewModel ovm = (OptionsViewModel)this.DataContext;
                    if (ovm != null)
                    {
                        ovm.DisplayPara.SendTxtColor = btnColorSend.Color.ToString();
                    }
                    
                }
            };

            DataContextChanged += OptionsWindow_DataContextChanged;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);


        }

        private void OptionsWindow_DataContextChanged(object sender, EventArgs e)
        {
            var vm = DataContext as OptionsViewModel;
            //vm.NoteEditClicked += Note_Edit;
            //vm.NoteDeleteClicked += Note_Delete;
        }
    }
}
