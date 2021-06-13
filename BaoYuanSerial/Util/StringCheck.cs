using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYuanSerial.Util
{
    /// <summary>
    /// Common Check
    /// </summary>
    public class CommonCheck
    {
        
        /// <summary>
        /// XOR Check
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string  CheckXor(string src)
        {            
            byte[] data = DataConvertUtility.HexStringToByte(src);
            byte CheckCode = 0;
            int len = data.Length;
            for (int i = 0; i < len; i++)
            {
                CheckCode ^= data[i];
            }

            return CheckCode.ToString("X2");
        }

        public static string CheckSum(string src)
        {
            byte[] data = DataConvertUtility.HexStringToByte(src);
            byte[] sum = GetCheckSum(data, 0, data.Length);

            return DataConvertUtility.ByteArrayToHexString(sum);
        }

        public static byte[] GetCheckSum(byte[] buffer, int start = 0, int len = 0)
        {
            if (buffer == null || buffer.Length == 0) return null;
            if (start < 0) return null;
            if (len == 0) len = buffer.Length - start;
            int length = start + len;
            if (length > buffer.Length) return null;
            int sum = 0;// Initial value
            for (int i = start; i < len; i++)
            {
                sum += buffer[i];
            }
            if(sum>0xFF)
            {
                sum = sum % 256;
            }
            return new byte[] { (byte)sum };
        }
        /// <summary>
        /// Modbus 基于ASCII的LRC校验
        /// 纵向冗余校验（Longitudinal Redundancy Check，简称：LRC）
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string CheckLRC(string src)
        {
            byte[] data = DataConvertUtility.HexStringToByte(src);
            byte[] lrc= GetLrc(data, 0, data.Length);

            return DataConvertUtility.ByteArrayToHexString(lrc);
        }
        /// <summary>
        /// 纵向冗余校验（Longitudinal Redundancy Check，简称：LRC）
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte[] GetLrc(byte[] buffer, int start = 0, int len = 0)
        {
            if (buffer == null || buffer.Length == 0) return null;
            if (start < 0) return null;
            if (len == 0) len = buffer.Length - start;
            int length = start + len;
            if (length > buffer.Length) return null;
            byte lrc = 0;// Initial value
            for (int i = start; i < len; i++)
            {
                lrc += buffer[i];
            }
            lrc = (byte)((lrc ^ 0xFF) + 1);
            return new byte[] { lrc };
        }
        /// <summary>
        /// BCC(Block Check Character/信息组校验码)
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte[] Bcc(byte[] buffer, int start = 0, int len = 0)
        {
            if (buffer == null || buffer.Length == 0) return null;
            if (start < 0) return null;
            if (len == 0) len = buffer.Length - start;
            int length = start + len;
            if (length > buffer.Length) return null;
            byte bcc = 0;// Initial value
            for (int i = start; i < len; i++)
            {
                bcc ^= buffer[i];
            }
            return new byte[] { bcc };
        }

        //帧校验序列码FCS ( Frame Check Sequences)
        /// <summary>
        /// FCS, Frame Check Sequences(帧校验序列码FCS )
        /// Chinese characters are not supported, only English characters are supported
        /// (不支持汉字，只支持英文字符)
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string CheckFCS(string src)
        {            
            byte[] lrc = GetFCS(src);

            return DataConvertUtility.ByteArrayToHexString(lrc);
        }
        /// <summary>
        /// Fcs
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetFCS(string data)
        {
            data=data.Replace(" ", "").Replace(Environment.NewLine, "");
            int fcsresult = 0;            
            for (int i = 0; i < data.Length; i++)
            {
                fcsresult = fcsresult +Convert.ToInt32(data[i]);
            }
            if(fcsresult>0xFF)
            {
                fcsresult = fcsresult % 256;
            }
            return new byte[] { (Byte)fcsresult};
        }

        public static string CheckCRC16Modbus(string src)
        {
            byte[] data = DataConvertUtility.HexStringToByte(src);
            byte[] crc16 = GetCrc16(data,0,data.Length);

            return DataConvertUtility.ByteArrayToHexString(crc16);
        }

        /// <summary>
        /// CRC-16/MODBUS    x16+x15+x2+1
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte[] GetCrc16(byte[] buffer, int start = 0, int len = 0)
        {
            if (buffer == null || buffer.Length == 0) return null;
            if (start < 0) return null;
            if (len == 0) len = buffer.Length - start;
            int length = start + len;
            if (length > buffer.Length) return null;
            ushort crc = 0xFFFF;// Initial value
            for (int i = start; i < length; i++)
            {
                crc ^= buffer[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 1) > 0)
                        crc = (ushort)((crc >> 1) ^ 0xA001);// 0xA001 = reverse 0x8005 
                    else
                        crc = (ushort)(crc >> 1);
                }
            }
            byte[] ret = BitConverter.GetBytes(crc);
           // Array.Reverse(ret);
            return ret;
        }
    }
}
