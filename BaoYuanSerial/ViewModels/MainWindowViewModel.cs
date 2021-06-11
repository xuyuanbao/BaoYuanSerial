using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using BaoYuanSerial.Views;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using System.Collections.ObjectModel;

using Avalonia.Media;
using BaoYuanSerial.Models;
using System.IO;
using Avalonia.Controls;
using System.Threading;

namespace BaoYuanSerial.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public SerialPort _serialPort;
        

        public MainWindowViewModel()
        {
            SerialPortList = SerialPort.GetSerials();
            DataBitsIndex = 3;
            _ComPortState = SerialPortList[PortNameIndex] + " ClOSED";

            ReceivePara = GloabalPara.ReceivePara;
            SendPara = GloabalPara.SendPara;
            LogPara = GloabalPara.LogPara;


            OnSendCommand = ReactiveCommand.CreateFromTask(SendCommand);
            OnLogCommand = ReactiveCommand.Create(OnLogClick);
            OnStartCommand = ReactiveCommand.Create(OnStartClick);
            OnPauseCommand = ReactiveCommand.Create(OnPauseClick);
            OnStopCommand = ReactiveCommand.Create(OnStopClick);
            OnClearCommand = ReactiveCommand.Create(OnClearClick);
            
        }


        public async Task SendCmdLoop()
        {
            while (SendPara.IsLoop)
            {
                await SendCommand();
                Thread.Sleep(SendPara.LoopInterval);
            }
        }
        public ReactiveCommand<Unit, Unit> OnSendCommand { get; }

        private async Task SendCommand()
        {
            try
            {
                // SendTxt����ʽ�� �����HEXҪȥ�����пո�
               // string cmd = SendTxt.Replace("", " ");
                if (SendTxt.Trim() == "") return;
                string txtsend = SendTxt.Trim().Replace(" ", "").ToUpper();
                if (SendPara.FormatSend)
                {
                    if (SendPara.FormatCRNL)
                    {
                        txtsend += "0D0A";
                    }
                    else if (SendPara.FormatNLCR)
                    {
                        txtsend += "0A0D";
                    }
                    else if (SendPara.FormatNewLine)
                    {
                        txtsend += "0A";
                    }
                    else if (SendPara.FormatCarReturn)
                    {
                        txtsend += "0D";
                    }
                }
                int byteNum = 0;   //�˴η��˼����ֽ�

                if (SendPara.IsHex)
                {
                    byte[] btSend = Util.DataConvertUtility.HexStringToByte(txtsend);
                    _serialPort.Write(btSend);
                    byteNum = btSend.Length;
                }
                else if(SendPara.IsText)
                {
                    _serialPort.Write(txtsend);
                    byteNum = Encoding.ASCII.GetBytes(txtsend).Length;
                }

                if(ReceivePara.DisplaySend)
                {
                    if (ReceivePara.AutoFeed)
                    {
                        if(SendPara.IsHex)
                        {
                            txtsend = Util.DataConvertUtility.InsertFormat(txtsend,2," ")+"\r\n";
                        }
                        else
                        {
                            txtsend += "\r\n";
                        }
                    }
                    if (ReceivePara.DisplayTime)
                    {
                        txtsend = DateTime.Now.ToString(ReceivePara.TimeFormat) + txtsend;
                    }
                    ReceiveTxt += txtsend;
                    if(LogPara.SaveLogMsg)
                    {
                       SaveLogAsync(txtsend);
                    }
                }

                //�Ѿ������˼����ֽ�
                _SendedBytesNum += byteNum;
                SendBytesStr = "Tx: "+_SendedBytesNum+" Bytes";
                AddSendHistory(SendTxt);
            }
            catch(Exception ex)
            {
                var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                {
                    ButtonDefinitions = ButtonEnum.Ok,
                    ContentTitle = "Errors Happend",
                    ContentMessage = ex.Message,
                    Icon = Icon.Error,
                    FontFamily = "Microsoft YaHei,Simsun",
                    WindowStartupLocation=Avalonia.Controls.WindowStartupLocation.CenterScreen,
#if Ubuntu
                    Style = Style.UbuntuLinux
#elif MSWindow
                    Style = Style.Windows
#endif
                });
                 msBoxStandardWindow.Show();
            }
        }
              

        #region Toolbar command

        public ReactiveCommand<Unit, Unit> OnLogCommand { get; }

        private void OnLogClick()
        {
            LogPara.SaveLogMsg = !LogPara.SaveLogMsg;
            if(LogPara.SaveLogMsg)
            {
                LogBtnBackColor= GloabalPara.RedBrush;
            }
            else
            {
                LogBtnBackColor = GloabalPara.TransparentBrush;
            }
        }

        private SolidColorBrush _LogBtnBackColor = new SolidColorBrush(Colors.Transparent);

        public SolidColorBrush LogBtnBackColor
        {
            get => _LogBtnBackColor;
            set
            {
                _LogBtnBackColor = value;
                this.RaisePropertyChanged();
            }
        }

        private bool _IsStartCan = true;

        public bool IsStartCan
        {
            get => _IsStartCan;
            set
            {
                _IsStartCan = value;
                this.RaisePropertyChanged();
            }
        }

        private bool _IsStopCan = false;
        public bool IsStopCan
        {
            get => _IsStopCan;
            set
            {
                _IsStopCan = value;
                this.RaisePropertyChanged();
            }
        }

        

        public ReactiveCommand<Unit, Unit> OnStartCommand { get; }

        private void OnStartClick()
        {
            try
            {
                if(_serialPort!=null)
                {                    
                    if (_serialPort.IsOpen) _serialPort.Close();
                }
                IsStartCan = false;
                IsStopCan = true;
                IsPauseCan = true;               
                PauseBtnBackColor = GloabalPara.TransparentBrush;

                bool bdtr = false;bool brts = false;

                if(FlowType==1)
                {
                    brts = true;
                }
                else if (FlowType == 2)
                {
                    bdtr = true;
                }                
                _serialPort = new SerialPort(SerialPortList[PortNameIndex], BaudRateList[BaudRateIndex], Parity, DataBitsList[DataBitsIndex], StopBits, bdtr, brts);
                _serialPort.ReceivedEvent += DataReceived;
                _serialPort.Open();

                ComPortState = SerialPortList[PortNameIndex] + " Opend";
                ComPortStateColor = GloabalPara.GreenBrush;
            }
            catch
            {
                ComPortState = SerialPortList[PortNameIndex]+ " Can't open!";
                ComPortStateColor = GloabalPara.RedBrush;
            }
        }

        private SolidColorBrush _PauseBtnBackColor = new SolidColorBrush(Colors.Red);
        public SolidColorBrush PauseBtnBackColor
        {
            get => _PauseBtnBackColor;
            set
            {
                _PauseBtnBackColor = value;
                this.RaisePropertyChanged();
            }
        }


        public ReactiveCommand<Unit, Unit> OnPauseCommand { get; }

        private bool _IsPauseCan = false;

        public bool IsPauseCan
        {
            get => _IsPauseCan;
            set
            {
                _IsPauseCan = value;
                this.RaisePropertyChanged();
            }
        }
        
        private void OnPauseClick()
        {
            
            if(PauseBtnBackColor==GloabalPara.GreenBrush)
            {
                PauseBtnBackColor = GloabalPara.RedBrush;
            }
            else
            {
                PauseBtnBackColor = GloabalPara.GreenBrush;
            }
        }

      
        public ReactiveCommand<Unit, Unit> OnStopCommand { get; }

        private void OnStopClick()
        {            
            IsStopCan = false;
            IsStartCan = true;
            IsPauseCan = false;            
            PauseBtnBackColor = GloabalPara.RedBrush;
            if (_serialPort!=null)
            {
                if (_serialPort.IsOpen) _serialPort.Close();
                ComPortState = SerialPortList[PortNameIndex] + " Closed";
                ComPortStateColor = new SolidColorBrush(Colors.Green);
            }
        }
        public ReactiveCommand<Unit, Unit> OnClearCommand { get; }

        private void OnClearClick()
        {
            ReceiveTxt = "";
            _SendedBytesNum = 0;
            _ReceivedBytesNum = 0;
            SendBytesStr = "Tx: 0 Bytes";
            ReceiveBytesStr = "Rx: 0 Bytes";
        }

        #endregion

        #region Port Operate

        
        private void DataReceived(object sender, byte[] bytes)
        {
            try
            {
                if (!_IsPauseCan) return;
                string receivestr = "";
                if(ReceivePara.IsHex)
                {
                    receivestr= SerialPort.ByteToHexStr(bytes);
                }
                else
                {
                    receivestr = Encoding.UTF8.GetString(bytes);
                }

                if (ReceivePara.AutoFeed)
                {
                    receivestr +="\r\n";
                }
                if (ReceivePara.DisplayTime)
                {
                    receivestr = DateTime.Now.ToString(ReceivePara.TimeFormat) + receivestr;
                }
                ReceiveTxt += receivestr;
                _ReceivedBytesNum += bytes.Length;
                ReceiveBytesStr = "Rx: " + _ReceivedBytesNum + " Bytes";
                if (LogPara.SaveLogMsg)
                {
                    SaveLogAsync(receivestr);
                }
            }
            catch
            {
                ComPortState = SerialPortList[PortNameIndex] + " Recive Process Error!";
                ComPortStateColor = GloabalPara.RedBrush;
            }
        }


       

        #endregion



        #region PortParaGetSet
        private ObservableCollection<string> _SerialPortList = new ObservableCollection<string>();

        public ObservableCollection<string> SerialPortList
        {
            get => _SerialPortList;
            set
            {
                _SerialPortList = value;
                this.RaisePropertyChanged();
            }
        }

        private int _PortNameIndex = 0;

        public int PortNameIndex
        {
            get => _PortNameIndex;
            set
            {
                _PortNameIndex = value;
                this.RaisePropertyChanged();
            }
        }

       

        private ObservableCollection<int> _BaudRateList = new ObservableCollection<int>() { 9600,19200,38400,57600,115200,1200,2400,4800};

        public ObservableCollection<int> BaudRateList
        {
            get => _BaudRateList;
            set
            {
                _BaudRateList = value;
                this.RaisePropertyChanged();
            }
        }

        private int _BaudRateIndex = 0;

        public int BaudRateIndex
        {
            get => _BaudRateIndex;
            set
            {
                _BaudRateIndex = value;
                this.RaisePropertyChanged();
            }
        }

        private ObservableCollection<int> _DataBitsList = new ObservableCollection<int>() { 5,6,7,8};

        public ObservableCollection<int> DataBitsList
        {
            get => _DataBitsList;
            set
            {
                _DataBitsList = value;
                this.RaisePropertyChanged();
            }
        }

        private int _DataBitsIndex = 0;
        public int DataBitsIndex
        {
            get => _DataBitsIndex;
            set
            {
                _DataBitsIndex = value;
                this.RaisePropertyChanged();
            }
        }

        private int _Parity = 0;
        public int Parity
        {
            get => _Parity;
            set
            {
                _Parity = value;
                this.RaisePropertyChanged();
            }
        }

        private int _StopBits= 0;
        public int StopBits
        {
            get => _StopBits;
            set
            {
                _StopBits = value;
                this.RaisePropertyChanged();
            }
        }

        private int _FlowType = 0;
        public int FlowType
        {
            get => _FlowType;
            set
            {
                _FlowType = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion

        #region PortSendReceiveState
        private string _ComPortState = "";

        public string ComPortState
        {
            get => _ComPortState;
            set
            {
                _ComPortState = value;
                this.RaisePropertyChanged();
            }
        }
        private SolidColorBrush _ComPortStateColor = new SolidColorBrush(Colors.Red);
        public SolidColorBrush ComPortStateColor
        {
            get => _ComPortStateColor;
            set
            {
                _ComPortStateColor = value;
                this.RaisePropertyChanged();
            }
        }
        /// <summary>
        /// �Ѿ����͵��ֽ���
        /// </summary>
        private long _SendedBytesNum = 0;

        private string _SendBytesStr = "Tx:0 Bytes";
        public string SendBytesStr
        {
            get => _SendBytesStr;
            set
            {
                _SendBytesStr = value;
                this.RaisePropertyChanged();
            }
        }

        private SolidColorBrush _SendBytesColor = new SolidColorBrush(Colors.Green);
        public SolidColorBrush SendBytesColor
        {
            get => _SendBytesColor;
            set
            {
                _SendBytesColor = value;
                this.RaisePropertyChanged();
            }
        }
        /// <summary>
        /// �Ѿ����յ��ֽ���
        /// </summary>
        private long _ReceivedBytesNum = 0;
        private string _ReceiveBytesStr="Rx:0 Bytes";
        public string ReceiveBytesStr
        {
            get => _ReceiveBytesStr;
            set
            {
                _ReceiveBytesStr = value;
                this.RaisePropertyChanged();
            }
        }
        private SolidColorBrush _ReceiveBytesColor = new SolidColorBrush(Colors.BlueViolet);
        public SolidColorBrush ReceiveBytesColor
        {
            get => _ReceiveBytesColor;
            set
            {
                _ReceiveBytesColor = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion

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


        private ReceivePara _ReceivePara;
        public ReceivePara ReceivePara
        {
            get=> _ReceivePara;
            set
            {
                _ReceivePara = value;
                this.RaisePropertyChanged();
            }

        }

        private SendPara _SendPara ;
        public SendPara SendPara
        {
            get => _SendPara;
            set
            {
                _SendPara = value;
                this.RaisePropertyChanged();
            }

        }

        private string _ReceiveTxt = "";

        public string ReceiveTxt
        {
            get => _ReceiveTxt;
            set
            {
                _ReceiveTxt = value;
                this.RaisePropertyChanged();
            }
        }

        private string _SendTxt = "";

        public string SendTxt
        {
            get => _SendTxt;
            set
            {
                _SendTxt = value;
                this.RaisePropertyChanged();
            }
        }
        private ObservableCollection<string> _SendTxtHistory = new ObservableCollection<string>();

        public ObservableCollection<string> SendTxtHistory
        {
            get => _SendTxtHistory;
            set
            {
                _SendTxtHistory = value;
                this.RaisePropertyChanged();
            }
        }

        private void AddSendHistory(string str)
        {
            if(SendTxtHistory.Count>15)
            {
                SendTxtHistory.RemoveAt(0);
            }
            if (SendTxtHistory.Contains(str)) return;
            SendTxtHistory.Add(str);
        }

        private int _SendTxtHisSelIndex = 0;
        public int SendTxtHisSelIndex
        {
            get => _SendTxtHisSelIndex;
            set
            {
                _SendTxtHisSelIndex = value;
                this.RaisePropertyChanged();
            }
        }

        private bool _SendCmdIsEnable = true;

        public bool SendCmdIsEnable
        {
            get => _SendCmdIsEnable;
            set
            {
                _SendCmdIsEnable = value;
                this.RaisePropertyChanged();
            }
        }

        #region Log

        private List<string> _LogList = new List<string>();

        private async Task<bool> SaveLogAsync(string strlog)
        {
            bool bret = false;
            if(LogPara.EnableWriteBuf)
            {
                long len = 0;
                long listlen = _LogList.Count;
                if (listlen > 0)
                {
                    for(int i=0;i< listlen; i++)
                    {
                        len+=System.Text.Encoding.Default.GetBytes(_LogList[i]).Length;
                    }
                }
                if(len>=LogPara.BufSize)
                {
                    for (int i = 0; i < listlen; i++)
                    {
                        await SaveLogBaseAsync(_LogList[i]);
                    }
                    _LogList.Clear();
                }
                else
                {
                    _LogList.Add(strlog);
                }
            }
            else
            {
                await SaveLogBaseAsync(strlog);
            }

            return bret;
        }

        private async Task<bool> SaveLogBaseAsync(string strlog)
        {
            bool bret = false;
            try
            {
                if (!LogPara.SaveLogMsg) return true;

                System.IO.FileInfo fileInfo = null;
                try
                {
                    fileInfo = new System.IO.FileInfo(LogPara.FileName);
                }
                catch (Exception ex)
                {
                    Util.FileTool.SaveFailLog(ex.Message);
                    return false;
                }
                // ����ļ�����,���ļ���С�����趨���ļ���С�����ؽ����ĵ�
                if (fileInfo != null && fileInfo.Exists)
                {
                    System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(LogPara.FileName);
                    long filsizeMB = fileInfo.Length;
                    if(filsizeMB>=LogPara.MaxFileSize)
                    {
                        string filepath = Path.GetDirectoryName(LogPara.FileName);
                        if(filepath==null || filepath=="")
                        {
                            filepath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                        }
                        LogPara.FileName =Path.Combine( filepath,"Log_"+DateTime.Now.ToString("yyyyMMdd")+".txt");
                    }                    
                }

                bret = Util.FileTool.SaveDebugLog(LogPara.FileName,strlog);
            }
            catch(Exception ex)
            {
                Util.FileTool.SaveFailLog(ex.Message);
                ComPortState = "Save Log Err";
                ComPortStateColor = GloabalPara.RedBrush;
            }
            return bret;
        }

        #endregion

    }
}
