﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI;
using BaoYuanSerial.Models;
using System.Reactive;
using Avalonia.Controls;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using BaoYuanSerial.Views;

namespace BaoYuanSerial.ViewModels
{
   public class OptionsViewModel:ViewModelBase
    {

        public OptionsViewModel()
        {
            _DisplayPara = new DisplayPara();  //  GloabalPara.DisplayPara;
            _ReceivePara = new ReceivePara();
            _LogPara = new LogPara();
            _SendPara = new SendPara();



            OnCancelCommand = ReactiveCommand.Create<Window>((wd)=>{ OnCancel(wd); });
            OnOKCommand = ReactiveCommand.Create<Window>((wd) => { OnOK(wd); });
            OnApplyCommand = ReactiveCommand.Create(OnApply);
            OpenFileCommand= ReactiveCommand.Create<Window>((wd) => { OpenFile(wd); });
        }


        private ReceivePara _ReceivePara;

        public ReceivePara ReceivePara
        {
            get => _ReceivePara;
            set
            {
                _ReceivePara = value;
                this.RaisePropertyChanged();
            }
        }

        private SendPara _SendPara;

        public SendPara SendPara
        {
            get => _SendPara;
            set
            {
                _SendPara = value;
                this.RaisePropertyChanged();
            }
        }

        private LogPara _LogPara;

        public LogPara LogPara
        {
            get => _LogPara;
            set
            {
                _LogPara = value;
                this.RaisePropertyChanged();
            }
        }

        private DisplayPara _DisplayPara;
        public DisplayPara DisplayPara
        {
            get => _DisplayPara;
            set
            {
                _DisplayPara = value;
                this.RaisePropertyChanged();
            }
        }

        private int _OptionsTabSelectedIndex = 0;
        /// <summary>
        /// Options Tab当前选中项序号
        /// </summary>
        public int OptionsTabSelectedIndex
        {
            get => _OptionsTabSelectedIndex;
            set
            {
                _OptionsTabSelectedIndex = value;
                this.RaisePropertyChanged();
            }
        }

        public ReactiveCommand<Window,Unit> OnCancelCommand { get; }
        public ReactiveCommand<Window, Unit> OnOKCommand { get; }

        public ReactiveCommand<Unit, Unit> OnApplyCommand { get; }

        public ReactiveCommand<Window, Unit> OpenFileCommand { get; }



        private void OnCancel(Window window)
        {
            if (window!=null)
            {
                window.Close();
            }            
        }
        private void OnOK(Window window)
        {
            GloabalPara.ReceivePara.MinimalInterval = ReceivePara.MinimalInterval;
            GloabalPara.ReceivePara.TimeFormat = ReceivePara.TimeFormat;

            GloabalPara.SendPara.FormatSend = SendPara.FormatSend;
            GloabalPara.SendPara.FormatNewLine = SendPara.FormatNewLine;
            GloabalPara.SendPara.FormatCarReturn = SendPara.FormatCarReturn;
            GloabalPara.SendPara.FormatCRNL = SendPara.FormatCRNL;
            GloabalPara.SendPara.FormatNLCR = SendPara.FormatNLCR;

            GloabalPara.LogPara.SaveLogMsg = LogPara.SaveLogMsg;
            GloabalPara.LogPara.FileName = LogPara.FileName;
            GloabalPara.LogPara.MaxFileSize = LogPara.MaxFileSize;
            GloabalPara.LogPara.EnableWriteBuf = LogPara.EnableWriteBuf;
            GloabalPara.LogPara.BufSize = LogPara.BufSize;

            GloabalPara.DisplayPara.FormatDisColor = DisplayPara.FormatDisColor;
            GloabalPara.DisplayPara.ReceiveTxtColor = DisplayPara.ReceiveTxtColor;
            GloabalPara.DisplayPara.SendTxtColor = DisplayPara.SendTxtColor;

            if (window != null)
            {
                window.Close();
            }
        }

        private void OnApply()
        {
           // var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
           // .GetMessageBoxStandardWindow(new MessageBoxStandardParams
           // {
           //     ButtonDefinitions = ButtonEnum.YesNo,
           //     ContentTitle = "Tips",
           //     ContentMessage = "Save Now?",
           //     Icon = Icon.Plus,
           //     FontFamily = "Microsoft YaHei,Simsun",
           //     Style = Style.UbuntuLinux

           // });
           //ButtonResult buttonResult= msBoxStandardWindow.Show().Result;
           // if (buttonResult == ButtonResult.No) return;

            switch (OptionsTabSelectedIndex)
            {
                case 0:
                    GloabalPara.ReceivePara.MinimalInterval = ReceivePara.MinimalInterval;
                    GloabalPara.ReceivePara.TimeFormat = ReceivePara.TimeFormat;
                    break;
                case 1:
                    GloabalPara.SendPara.FormatSend = SendPara.FormatSend;
                    GloabalPara.SendPara.FormatNewLine = SendPara.FormatNewLine;
                    GloabalPara.SendPara.FormatCarReturn = SendPara.FormatCarReturn;
                    GloabalPara.SendPara.FormatCRNL = SendPara.FormatCRNL;
                    GloabalPara.SendPara.FormatNLCR = SendPara.FormatNLCR;              
                    break;
                case 2:
                    GloabalPara.LogPara.SaveLogMsg = LogPara.SaveLogMsg;
                    GloabalPara.LogPara.FileName = LogPara.FileName;
                    GloabalPara.LogPara.MaxFileSize = LogPara.MaxFileSize;
                    GloabalPara.LogPara.EnableWriteBuf = LogPara.EnableWriteBuf;
                    GloabalPara.LogPara.BufSize = LogPara.BufSize;
                    break;
                case 3:
                    GloabalPara.DisplayPara.FormatDisColor = DisplayPara.FormatDisColor;
                    GloabalPara.DisplayPara.ReceiveTxtColor = DisplayPara.ReceiveTxtColor;
                    GloabalPara.DisplayPara.SendTxtColor = DisplayPara.SendTxtColor;
                    break;
            }
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="wd"></param>
        /// <returns></returns>
        private async Task OpenFile(Window wd)
        {
            
            OpenFileDialog ofd = new OpenFileDialog() { Title="Select Exists Txt File",AllowMultiple=false};
            ofd.InitialDirectory = Environment.CurrentDirectory;
            //FileDialogFilter fdf = new FileDialogFilter() { Name="Txt File",Extensions=new List<string>() { ".txt"} };
            ofd.Filters.Add(new FileDialogFilter() { Name = "Text", Extensions = { "txt" } });
            //注意：调用ofd.ShowAsync的方法必须用async Task异步方法调用，否则无法显示文件选择框
            string[] resultstr= await ofd.ShowAsync(wd);
            if(resultstr.Length>0)
            {
                LogPara.FileName = resultstr[0];
            }
        }

    }
}
