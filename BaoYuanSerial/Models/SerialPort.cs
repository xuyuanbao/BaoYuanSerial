using flyfire.IO.Ports;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace BaoYuanSerial.Models
{
    public class SerialPort : CustomSerialPort
    {

        public SerialPort(string portName, int baudRate = 115200, Parity parity = Parity.None, int databits = 8, StopBits stopBits = StopBits.One)
            : base(portName, baudRate, parity, databits, stopBits)
        {
            
        }

        public SerialPort(string portname, int baudrate, int parity, int databits, int stopbits, bool dtrenable, bool rtsenable) :base(portname, baudrate, 
            (Parity)parity,databits,(StopBits)stopbits)
        {
            this.DtrEnable = dtrenable;
            this.RtsEnable = rtsenable;
        }


        //无意义，只是因为父类的 Sp_DataReceived() 不是 public
        public void StartMonitorReceive()
        {

            Sp_DataReceived(new object(), new SerialDataReceivedEventArgs(SerialData.Eof));
        }
        /// <summary>
        /// 获取计算机的所有串口
        /// </summary>
        public static ObservableCollection<string> GetSerials()
        {
            ObservableCollection<string> lsStr = new ObservableCollection<string>();
            try
            {
                //CustomSerialPort.GetPortNames() 静态方法，获取计算机的所有串口名称
                //因为已经继承，也可以使用 string[] vs = 串口通讯.GetPortNames();
                string[] vs = CustomSerialPort.GetPortNames();
                lsStr = new ObservableCollection<string>(vs);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return lsStr;
        }

    }
}
