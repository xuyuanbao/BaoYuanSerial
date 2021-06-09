using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTool
{
  public static  class DataConvertUtility
    {
        /// <summary>
        /// Get the two short value from one float value for write float values into PLC.
        /// </summary>
        /// <param name="fValue">fValue:浮点数表示的数值；</param>
        /// <returns></returns>
        public static ushort[] GetUshortFromFloat(float fValue)
        {
            byte[] btArray = BitConverter.GetBytes(fValue);
            ushort[] uBuf = new ushort[2];
            uBuf[0] = BitConverter.ToUInt16(btArray, 0);
            uBuf[1] = BitConverter.ToUInt16(btArray, 2);
            return uBuf;
        }

        public static ushort[] GetUshortFromINT32(int nValue)
        {
            byte[] btArray = BitConverter.GetBytes(nValue);
            ushort[] uBuf = new ushort[2];
            uBuf[0] = BitConverter.ToUInt16(btArray, 0);
            uBuf[1] = BitConverter.ToUInt16(btArray, 2);
            return uBuf;
        }

        public static ushort[] GetUshortFromUINT32(uint nValue)
        {
            byte[] btArray = BitConverter.GetBytes(nValue);
            ushort[] uBuf = new ushort[2];
            uBuf[0] = BitConverter.ToUInt16(btArray, 0);
            uBuf[1] = BitConverter.ToUInt16(btArray, 2);
            return uBuf;
        }

        /// <summary>
        /// 从字节角度将数据转换成uShort.写入寄存器都要采用uShort.
        /// </summary>
        /// <param name="nValue"></param>
        /// <returns></returns>
        public static ushort GetUshortFromINT16(Int16 nValue)
        {
            byte[] btArray = BitConverter.GetBytes(nValue);
            ushort uBuf = BitConverter.ToUInt16(btArray, 0);
            return uBuf;
        }

        /// <summary>
        /// 从HEX字符串解码出Uint16；HEX字符串的正确与否，需要调用程序自行判断。
        /// </summary>
        /// <param name="strHex"></param>
        /// <returns></returns>
        public static ushort GetUshortFromHEXString(string strHex)
        {
            byte[] btArray = new byte[2];
            btArray[1] = Convert.ToByte(strHex.Substring(0, 2), 16);
            btArray[0] = Convert.ToByte(strHex.Substring(2, 2), 16);
            return BitConverter.ToUInt16(btArray, 0);
        }

        //把4个HEX字符转换为一个INT16数值。
        public static Int16 GetINT16FromHEXString(string strHex)
        {
            byte[] btArray = new byte[2];
            btArray[0] = Convert.ToByte(strHex.Substring(0, 2), 16);
            btArray[1] = Convert.ToByte(strHex.Substring(2, 2), 16);
            return BitConverter.ToInt16(btArray, 0);
        }

        //把8个HEX字符转成一个int32.
        public static Int32 GetINT32FromHEXString(string strHex)
        {
            byte[] btArray = new byte[4];
            btArray[0] = Convert.ToByte(strHex.Substring(0, 2), 16);
            btArray[1] = Convert.ToByte(strHex.Substring(2, 2), 16);
            btArray[2] = Convert.ToByte(strHex.Substring(4, 2), 16);
            btArray[3] = Convert.ToByte(strHex.Substring(6, 2), 16);

            return BitConverter.ToInt32(btArray, 0);
        }

        //把8个HEX字符转成一个Uint32.
        public static UInt32 GetUINT32FromHEXString(string strHex)
        {
            byte[] btArray = new byte[4];
            btArray[0] = Convert.ToByte(strHex.Substring(0, 2), 16);
            btArray[1] = Convert.ToByte(strHex.Substring(2, 2), 16);
            btArray[2] = Convert.ToByte(strHex.Substring(4, 2), 16);
            btArray[3] = Convert.ToByte(strHex.Substring(6, 2), 16);

            return BitConverter.ToUInt32(btArray, 0);
        }

        //把8个HEX字符转成一个Single.HEX顺序1234
        public static float GetFloatFromHEXString(string strHex)
        {
            byte[] btArray = new byte[4];
            btArray[0] = Convert.ToByte(strHex.Substring(0, 2), 16);
            btArray[1] = Convert.ToByte(strHex.Substring(2, 2), 16);
            btArray[2] = Convert.ToByte(strHex.Substring(4, 2), 16);
            btArray[3] = Convert.ToByte(strHex.Substring(6, 2), 16);

            return BitConverter.ToSingle(btArray, 0);
        }

        //把8个HEX字符转成一个Single. HEX顺序4321;
        public static float GetFloatFromHEXString_4321(string strHex)
        {
            byte[] btArray = new byte[4];
            btArray[3] = Convert.ToByte(strHex.Substring(0, 2), 16);
            btArray[2] = Convert.ToByte(strHex.Substring(2, 2), 16);
            btArray[1] = Convert.ToByte(strHex.Substring(4, 2), 16);
            btArray[0] = Convert.ToByte(strHex.Substring(6, 2), 16);

            return BitConverter.ToSingle(btArray, 0);
        }

        //把4个HEX字符转成一个Uint32.
        public static float GetFloatFromHEXString_4(string strHex)
        {
            byte[] btArray = new byte[4];
            btArray[0] = Convert.ToByte(strHex.Substring(0, 1), 16);
            btArray[1] = Convert.ToByte(strHex.Substring(1, 1), 16);
            btArray[2] = Convert.ToByte(strHex.Substring(2, 1), 16);
            btArray[3] = Convert.ToByte(strHex.Substring(3, 1), 16);

            return BitConverter.ToChar(btArray, 0) / 10;
        }
      /// <summary>
      ///  十六进制字符串转换成字节数组 
      /// </summary>
      /// <param name="strHex"></param>
      /// <returns></returns>
      public static byte[] HexStringToByte(string strHex)
        {
            strHex.Replace(" ", "");  //去除多余空格
            int nLen = strHex.Length;
            nLen = nLen / 2;
            byte[] btArrayCmd = new byte[nLen];
           
            for (int i = 0; i < nLen; i++)
            {
                btArrayCmd[i] = Convert.ToByte(strHex.Substring(i * 2, 2), 16);

            }
            return btArrayCmd;
        }
      /// <summary>
      /// 字节数组转为十六进制字符串,每个字节之间没有空格 
      /// </summary>
      /// <param name="data"></param>
      /// <returns></returns>
      public static string ByteArrayToHexString(byte[] data)//字节数组转为十六进制字符串  
      {
          StringBuilder sb = new StringBuilder(data.Length * 3);
          foreach (byte b in data)
              sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));  //如果想每个字节之间有空格，只需要sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3,' ')即可
          return sb.ToString().ToUpper();
      }
    }
}
