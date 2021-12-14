using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace ZhiFang.BLL.Common
{
    public class MD5
    {
        public static string md5(string str)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }

            return ret.PadLeft(32, '0');
        }

        //加密
        public static string Encode(string data)
        {
            string KEY_64 = "VavicApp";
            string IV_64 = "VavicApp";

            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);

        }
        //解密
        public static string Decode(string data)
        {
            string KEY_64 = "VavicApp";
            string IV_64 = "VavicApp";

            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 字符串加密类
        /// </summary>
        public static class Encrypt
        {
            private const string mstr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

            /// <summary>
            /// 字符串加密
            /// </summary>
            /// <param name="str">待加密的字符串</param>
            /// <returns>加密后的字符串</returns>
            public static string EnCode(string str)
            {
                if (string.IsNullOrEmpty(str))
                {
                    return "";
                }

                byte[] buff = Encoding.Default.GetBytes(str);
                int j, k, m;
                int len = mstr.Length;
                StringBuilder sb = new StringBuilder();
                Random r = new Random();

                for (int i = 0; i < buff.Length; i++)
                {
                    j = (byte)r.Next(6);
                    buff[i] = (byte)((int)buff[i] ^ j);
                    k = (int)buff[i] % len;
                    m = (int)buff[i] / len;
                    m = m * 8 + j;
                    sb.Append(mstr.Substring(k, 1) + mstr.Substring(m, 1));
                }

                return sb.ToString();
            }
            /// <summary>
            /// 字符串解密
            /// </summary>
            /// <param name="str">待解密的字符串</param>
            /// <returns>解密后的字符串</returns>
            public static string DeCode(string str)
            {
                if (string.IsNullOrEmpty(str))
                {
                    return "";
                }

                try
                {
                    int j, k, m, n = 0;
                    int len = mstr.Length;
                    byte[] buff = new byte[str.Length / 2];

                    for (int i = 0; i < str.Length; i += 2)
                    {
                        k = mstr.IndexOf(str[i]);
                        m = mstr.IndexOf(str[i + 1]);
                        j = m / 8;
                        m = m - j * 8;
                        buff[n] = (byte)(j * len + k);
                        buff[n] = (byte)((int)buff[n] ^ m);
                        n++;
                    }
                    return Encoding.Default.GetString(buff);
                }
                catch
                {
                    return "";
                }
            }

        }

        //public static string get6(string outXMLFilePath, string jia)
        //{
        //    byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //    if (jia.Equals("加密"))
        //    {
        //        byte[] rgbKey = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //        byte[] rgbIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //        byte[] inputByteArray = Encoding.UTF8.GetBytes(outXMLFilePath);
        //        DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
        //        MemoryStream mStream = new MemoryStream();
        //        CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
        //        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        //        cStream.FlushFinalBlock();
        //        return Convert.ToBase64String(mStream.ToArray());
        //    }
        //    else
        //    {
        //        byte[] rgbKey = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //        byte[] rgbIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //        byte[] inputByteArray = Convert.FromBase64String(outXMLFilePath);
        //        DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
        //        MemoryStream mStream = new MemoryStream();
        //        CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
        //        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        //        cStream.FlushFinalBlock();
        //        return Encoding.UTF8.GetString(mStream.ToArray());

        //    }
        //}
    }
}
