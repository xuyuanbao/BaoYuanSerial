using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;



namespace CommonTool
{
    /// <summary>
    /// INI文件读写类。
    /// </summary>
    public class IniIO
    {
        public string path;
        public IniIO(string INIPath)
        {
            path = INIPath;
        }
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,string key,string val,string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,string key,string def, StringBuilder retVal,int size,string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);
        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void IniWriteValue(string Section,string Key,string Value)
        {
          WritePrivateProfileString(Section,Key,Value,this.path);
        }
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string IniReadValue(string Section,string Key)
        {
          StringBuilder temp = new StringBuilder(255);
          int i = GetPrivateProfileString(Section,Key,"",temp, 255, this.path);
          return temp.ToString();
        }
        public byte[] IniReadValues(string section, string key)
        {
          byte[] temp = new byte[255];
          int i = GetPrivateProfileString(section, key, "", temp, 255, this.path);
          return temp;
        }
        /// <summary>
        /// 删除ini文件下所有段落
        /// </summary>
        public void ClearAllSection()
        {
          IniWriteValue(null,null,null);
        }
        /// <summary>
        /// 删除ini文件下personal段落下的所有键
        /// </summary>
        /// <param name="Section"></param>
        public void ClearSection(string Section)
        {
          IniWriteValue(Section,null,null);
        }
        public static bool SaveFailLog(string str)
        {
            string logPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "ErrLog.log"; ;
            try
            {
                
                FileStream fs = new FileStream(logPath, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":" + str + "\r\n");
                sw.Flush();  //清空缓冲区
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;               
            }
            return true;
        }
        public static bool SaveLog(string str)
        {
            string logPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "MsgLog.log"; ;
            try
            {

                FileStream fs = new FileStream(logPath, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":" + str + "\r\n");
                sw.Flush();  //清空缓冲区
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

    }
}
