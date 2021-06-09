using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary; //正则类命名空间
using Windows.UI.Xaml.Controls;
using Windows.UI;
//using System.Reflection.Emit;

namespace CommonTool
{
   public class CommonMethod
    { 

       /// <summary>
       /// 更改界面元素字符显示
       /// </summary>
       /// <param name="message">数字信息</param>
       /// <param name="obj">常见支持文字输入的界面控件
       /// 现支持：Label,Button,ListBox,TextBox</param>
       /// <returns></returns>
       public static bool ShowMessageToUI(string message,object obj)
       {
           
           if(obj is ListBox)
           {
               message += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
               ListBox listB = (ListBox)obj;
               
                if (listB.Items.Count >= 50)
                {
                    listB.Items.RemoveAt(0);
                    listB.Items.Add(message);
                    listB.SelectedIndex = listB.Items.Count - 1;
                    
                }
                else
                {
                    listB.Items.Add(message);
                    listB.SelectedIndex = listB.Items.Count - 1;
                }
              
           }
           else if(obj is Windows.UI.Xaml.Controls.TextBlock)
           {
                TextBlock lbl = (TextBlock)obj;
               
                lbl.Text = message;
               
           }
           else if(obj is TextBox)
           {
               TextBox tbox = (TextBox)obj;
               
               tbox.Text = message;
               
           }
           else if(obj is Button)
           {
               Button btn = (Button)obj;
              
               btn.Content = message;
               
           }
           return true;
       }
     /// <summary>
       /// 更改界面背景前景颜色
     /// </summary>
     /// <param name="cor">颜色</param>
     /// <param name="iBackFore">前后，=0后，=1前</param>
     /// <param name="obj">需要修改颜色的控件</param>
     /// <returns></returns>
       public static bool ShowMessageToUI(Color cor,int iBackFore,object obj)
       {
           if(obj is TextBlock)
           {
                TextBlock lbl = (TextBlock)obj;
              
                   if(iBackFore==0)
                   {
                       lbl.Background = new Windows.UI.Xaml.Media.SolidColorBrush(cor);
                   }
                   else
                   {
                       lbl.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(cor);
                }
              
           }
           else if(obj is Button)
           {
               Button btn = (Button)obj;
               
                if (iBackFore == 0)
                {
                    btn.Background = new Windows.UI.Xaml.Media.SolidColorBrush(cor);
                }
                else
                {
                    btn.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(cor);
                }
               
           }
           else if(obj is TextBox)
           {
               TextBox tbox = (TextBox)obj;
               
                if (iBackFore == 0)
                {
                    tbox.Background = new Windows.UI.Xaml.Media.SolidColorBrush(cor);
                }
                else
                {
                    tbox.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(cor);
                }
               
           }
           return true;
       }
       /// <summary>
       /// 保存错误记录
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static bool SaveFailLog(string str)
       {
           string logPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Err" + DateTime.Now.ToString("yyyy-MM-dd") + ".log"; ; 

           try
           {
               StreamWriter sw;
               if (!File.Exists(logPath))
               {
                   sw = File.CreateText(logPath);
               }
               else
               {
                   sw = File.AppendText(logPath);
               }              
               sw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + str);
               sw.Flush();  //清空缓冲区
               sw.Close();
               
           }
           catch (Exception ex)
           {
                throw ex;
           }
           return true;
       }
       /// <summary>
       /// 保存调试记录
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static bool SaveDebugLog(string str)
       {
           string logPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Debug"+DateTime.Now.ToString("yyyy-MM-dd")+".log"; 

           try
           {
               StreamWriter sw;
               if (!File.Exists(logPath))
               {
                   sw = File.CreateText(logPath);
               }
               else
               {
                   sw = File.AppendText(logPath);
               }
               sw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":\r\n" + str);
               sw.Flush();  //清空缓冲区
               sw.Close();

           }
           catch (Exception ex)
           {
                throw ex;
               return false;
           }
           return true;
       }

       /// <summary>
       /// 强制转换字符串日期为DateTime类型
       /// </summary>
       /// <param name="strDate">日期字符串 </param>
       /// <param name="strDateType">字符串格式，例如 yyyyMMddHHmmss或yyyy-MM-dd hh:mm:ss </param>
       /// <returns></returns>
       public static DateTime ConvertStringToDateTime(string strDate, string strDateType)
       {
           DateTime dtm = DateTime.Now;
           try
           {
               if (strDate != "")
               {
                   dtm = DateTime.ParseExact(strDate, strDateType, System.Globalization.CultureInfo.CurrentCulture);
               }
               else
               {
                   dtm = Convert.ToDateTime("0000-00-00");
               }
           }
           catch
           {
               dtm = Convert.ToDateTime("0000-00-00");
           }
           return dtm;
       }
       /// <summary>
       /// 字符串转double
       /// </summary>
       /// <param name="strNum">数字字符串</param>
       /// <returns></returns>
       public static double ConvertStringToDouble(string strNum)
       {
           double value = 0.0;
           if (IsNumeric(strNum))
           {
               value = double.Parse(strNum);
           }
           else
           {
               value = 0.0;
           }
           return value;
       }
       
       /// <summary>
       /// 是否是数字
       /// </summary>
       /// <param name="value"></param>
       /// <returns></returns>
       public static bool IsNumeric(string value)
       {
           return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
       }
       #region 调用CMD执行命令
       //public static void CMDExecute(string command)
       // {
       //     var processInfo = new ProcessStartInfo("cmd.exe", "/S /C " + command)
       //     {
       //         CreateNoWindow = true,
       //         UseShellExecute = true,
       //         WindowStyle = ProcessWindowStyle.Hidden
       //     };

       //     Process.Start(processInfo);
       // }

       //public static string CMDExecuteWithOutput(string command)
       //{
       //    var processInfo = new ProcessStartInfo("cmd.exe", "/S /C " + command)
       //    {
       //        CreateNoWindow = true,
       //        UseShellExecute = false,
       //        WindowStyle = ProcessWindowStyle.Hidden,
       //        RedirectStandardOutput = true
       //    };

       //    var process = new Process { StartInfo = processInfo };
       //    process.Start();
       //    var outpup = process.StandardOutput.ReadToEnd();

       //    process.WaitForExit();
       //    return outpup;
       //}
       #endregion

       /// <summary>
       /// 调用CMD命令执行
       /// </summary>
       /// <param name="command">CMD命令字符串</param>
       /// <param name="outPut">命令返回码</param>
       public static void RunCMDCommand(string command, out string outPut)
       {
           using (Process pc = new Process())
           {
               command = command.Trim().TrimEnd('&') + "&exit";

               pc.StartInfo.FileName = "cmd.exe";
               pc.StartInfo.CreateNoWindow = true;//隐藏窗口运行
               pc.StartInfo.RedirectStandardError = true;//重定向错误流
               pc.StartInfo.RedirectStandardInput = true;//重定向输入流
               pc.StartInfo.RedirectStandardOutput = true;//重定向输出流
               pc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
               pc.StartInfo.UseShellExecute = false;
               pc.Start();
               pc.StandardInput.WriteLine(command);//输入CMD命令
               pc.StandardInput.AutoFlush = true;

               outPut = pc.StandardOutput.ReadToEnd();//读取结果        

               pc.WaitForExit();
               pc.Close();
           }
       }
       /// <summary>
       /// Bitmap深拷贝1
       /// </summary>
       /// <param name="bitmap"></param>
       /// <returns></returns>
       public static System.Drawing.Bitmap DeepCopyBitmap1(System.Drawing.Bitmap bitmap)
       {
            System.Drawing.Bitmap dstBitmap = null;
           try
           {
               using(MemoryStream mStream=new MemoryStream())
               {
                   BinaryFormatter bf = new BinaryFormatter();
                   bf.Serialize(mStream, bitmap);
                   mStream.Seek(0, SeekOrigin.Begin);
                   dstBitmap = (System.Drawing.Bitmap)bf.Deserialize(mStream);
                   mStream.Close();
               }
           }
           catch(Exception ex)
           {
               throw ex;
           }
           return dstBitmap;
       }
       /// <summary>
       /// Bitmap深拷贝2
       /// </summary>
       /// <param name="bitmap"></param>
       /// <returns></returns>
       public static System.Drawing.Bitmap DeepCopyBitmap2(System.Drawing.Bitmap bitmap)
       {
            System.Drawing.Bitmap dstBitmap = null;
           try
           {
               dstBitmap = bitmap.Clone(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return dstBitmap;
       }


    }
}
