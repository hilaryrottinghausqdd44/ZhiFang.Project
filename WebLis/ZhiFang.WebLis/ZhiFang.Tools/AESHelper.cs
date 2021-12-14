using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ZhiFang.Tools
{
    public class AESHelper
    {
        private static string password = "C805ACBD643745EF8678E1F95F265B4C";
        /// <summary>
        /// 与客户端约定的密钥
        /// </summary>
        public static string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        /// <summary>  
        /// AES解密
        /// </summary>  
        /// <param name="encryptedStr">被加密的明文</param>  
        /// <param name="password">密钥</param>  
        /// <returns>明文</returns>  
        public static string AESDecryptOf128(string encryptedStr, string password)
        {
            password = GetPasswordOfSubAndPad(password);
            Byte[] encryptedBytes = strToToHexByte(encryptedStr);
            MemoryStream mStream = new MemoryStream(encryptedBytes);
            RijndaelManaged aes = new RijndaelManaged();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.Zeros;
            aes.KeySize = 128;
            aes.BlockSize = 128;

            int keyBytesLen = GetKeyBytesLenByKeyLength(password.Length);
            byte[] keyBytes = new byte[keyBytesLen];
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            //指定加密的Key
            System.Array.Copy(pwdBytes, keyBytes, len);
            aes.Key = keyBytes;

            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            try
            {
                byte[] tmp = new byte[encryptedBytes.Length + 32];
                int len2 = cryptoStream.Read(tmp, 0, encryptedBytes.Length + 32);
                byte[] ret = new byte[len2];
                Array.Copy(tmp, 0, ret, 0, len2);
                string result = Encoding.UTF8.GetString(ret).Replace("\u0000", "");
                return result;
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                aes.Clear();
            }
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">密钥</param>
        /// <returns></returns>
        public static string AESEncryptOf128(string text, string password)
        {
            password = GetPasswordOfSubAndPad(password);
            RijndaelManaged aes = new RijndaelManaged();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.Zeros;
            aes.KeySize = 128;
            aes.BlockSize = 128;

           int keyBytesLen= GetKeyBytesLenByKeyLength(password.Length);
            byte[] keyBytes = new byte[keyBytesLen];
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            //指定加密的Key
            System.Array.Copy(pwdBytes, keyBytes, len);
            aes.Key = keyBytes;

            //开始按格式生成加密字符数组
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            ICryptoTransform transform = aes.CreateEncryptor();
            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);

            //将生成的加密字符的字节数组转换拼接为十六进制字符串
            StringBuilder strb = new StringBuilder();
            foreach (var letter in cipherBytes)
            {
                strb.Append(letter.ToString("X2"));
            }
            return strb.ToString().ToUpper();
        }
        /// <summary>
        /// 计算加密的密码的字节数组长度
        /// </summary>
        /// <param name="keyLength"></param>
        /// <returns></returns>
        private static int GetKeyBytesLenByKeyLength(int keyLength)
        {
            int keyBytesLen = 16;
            if (keyLength < 16) keyBytesLen = 16;
            else if (keyLength > 16 && keyLength <= 24) keyBytesLen = 24;
            else if (keyLength > 24) keyBytesLen = 32;
            //ZhiFang.Common.Log.Log.Debug("GetKeyBytesLenByKeyLength.KeyBytesLen:" + keyBytesLen);
            return keyBytesLen;
        }
        private static string GetPasswordOfSubAndPad(string password)
        {
            if (string.IsNullOrEmpty(password)) password = AESHelper.Password;
            return password;
        }
        /// <summary> 
        /// 字符串转16进制字节数组 
        /// </summary> 
        /// <param name="hexString"></param> 
        /// <returns></returns> 
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
