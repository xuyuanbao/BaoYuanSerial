using System;
using System.Text;
using System.Security.Cryptography;

namespace CommonTool
{
    public class DES
    {
        /// <summary>
        /// DES加密（加密成功返回密文，失败返回明文）
        /// </summary>
        /// <param name="sPlainText">明文</param>
        /// <param name="sKey">密钥（长度必须为8位）</param>
        /// <param name="sIV">初始向量（长度必须为8位）</param>
        /// <returns>密文</returns>
        public static string Encrypt(string sPlainText, string sKey, string sIV)
        {
            try
            {
                if (sKey.Length < 8) sKey = sKey + "[REMORT]";
                if (sKey.Length > 8) sKey = sKey.Substring(0, 8);
                if (sIV.Length < 8) sIV = sIV + "[remort]";
                if (sIV.Length > 8) sIV = sIV.Substring(0, 8);

                byte[] data = Encoding.UTF8.GetBytes(sPlainText);
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sIV);
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
                return BitConverter.ToString(result);
            }
            catch { }

            return sPlainText;
        }

        /// <summary>
        /// DES解密（解密成功返回明文，失败返回密文）
        /// </summary>
        /// <param name="sCypherText">密文</param>
        /// <param name="sKey">密钥（和加密密钥相同）</param>
        /// <param name="sIV">初始向量（和加密初始向量相同）</param>
        /// <returns>明文</returns>
        public static string Decrypt(string sCypherText, string sKey, string sIV)
        {
            try
            {
                if (sKey.Length < 8) sKey = sKey + "[REMORT]";
                if (sKey.Length > 8) sKey = sKey.Substring(0, 8);
                if (sIV.Length < 8) sIV = sIV + "[remort]";
                if (sIV.Length > 8) sIV = sIV.Substring(0, 8);

                string[] sInput = sCypherText.Split("-".ToCharArray());
                byte[] data = new byte[sInput.Length];
                for (int i = 0; i < sInput.Length; i++)
                {
                    data[i] = byte.Parse(sInput[i], System.Globalization.NumberStyles.HexNumber);
                }
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sIV);
                ICryptoTransform desencrypt = DES.CreateDecryptor();
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
                return Encoding.UTF8.GetString(result);
            }
            catch { }

            return sCypherText;
        }

    }
}
