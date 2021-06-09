using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTool
{
   public  class BitOperate
   {
       #region 32位Int操作
       /// <summary>
        /// 根据Int类型的值，返回用1或0(对应True或Flase)填充的数组
        /// <remarks>从右侧开始向左索引(0~31)</remarks>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<bool> GetBitList(int value)
        {
            var list = new List<bool>(32);
            for (var i = 0; i <= 31; i++)
            {
                var val = 1 << i;
                list.Add((value & val) == val);
            }
            return list;
        }

        /// <summary>
        /// 返回Int数据中某一位是否为1
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index">32位数据的从右向左的偏移位索引(0~31)</param>
        /// <returns>true表示该位为1，false表示该位为0</returns>
        public static bool GetBitValue(int value, ushort index)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index"); //索引出错
            var val = 1 << index;
            return (value & val) == val;
        }

        /// <summary>
        /// 设定Int数据中某一位的值
        /// </summary>
        /// <param name="value">位设定前的值</param>
        /// <param name="index">32位数据的从右向左的偏移位索引(0~31)</param>
        /// <param name="bitValue">true设该位为1,false设为0</param>
        /// <returns>返回位设定后的值</returns>
        public static int SetBitValue(int value, ushort index, bool bitValue)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index"); //索引出错
            var val = 1 << index;
            return bitValue ? (value | val) : (value & ~val);
        }
       #endregion
       #region 字节位操作
 
        /// <summary>
        /// 取整数或字节的第index位 
        /// </summary>
        /// <param name="b">要取的整数或字节</param>
        /// <param name="index">要取的位置索引，自右至左为0-7</param>
        /// <returns>返回某一位的值（0或者1）</returns>
        public static int GetBit(byte b, ushort index){ return ((b & (1 << index)) > 0) ? 1 : 0; }

        /// <summary>
        /// 取整数或字节的第index位 
        /// </summary>
        /// <param name="b">要取的整数或字节</param>
        /// <param name="index">要取的位置索引，自右至左为0-7</param>
        /// <returns>返回某一位的值（0或者1）</returns>
        public static bool GetBitValue(byte b, ushort index) { return ((b & (1 << index)) > 0); }
       
       /// <summary>
        /// 设置整数或字节的第index位为0或为1 
        /// </summary>
        /// <param name="b">要设置的整数或字节</param>
        /// <param name="index">要设置的位置索引，自右至左为0-7</param>
        /// <param name="flag">是否置1，TURE表示置1，FALSE表示置0</param>
        /// <returns>返回修改过的值（0或者1）</returns>
        public static int SetBit(byte b, int index,bool flag) 
        {
            if(flag)
            {
                b |= (byte)(0x1 << index);
            }
            else
            {
                b &=(byte) ~(0x1 << index);
            }
            return b;
        }

        /// <summary>
        /// 将第index位取反 
        /// </summary>
        /// <param name="b">要设置的整数或字节</param>
        /// <param name="index">要设置的位置索引，自右至左为0-7</param>       
        /// <returns>返回修改过的值（0或者1）</returns>
        public static int ReverseBit(byte b, int index) { return (byte)(b ^ (byte)(1 << index)); }

        #endregion



   }
}
