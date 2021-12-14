using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ZhiFang.LabInformationIntegratePlatform.BusinessObject
{
    public class EncodeFileName
    {
        #region 
        /// <summary>
        /// 为字符串中的非英文字符编码Encodes non-US-ASCII characters in a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }
            return builder.ToString();
        }
        /// <summary>
        ///指定一个字符是否应该被编码 Determines if the character needs to be encoded.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        public static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";
            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;
            return true;
        }
        /// <summary>
        /// 为非英文字符串编码Encodes a non-US-ASCII character.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        public static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }
            return builder.ToString();
        }
        public static string ToEncodeFileName(string fileName)
        {
            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") == -1)
            {
                string encodefileName = ToHexString(fileName);       //使用自定义的
                if (System.Web.HttpContext.Current.Request.Browser.Browser.Contains("IE"))
                {
                    string ext = encodefileName.Substring(encodefileName.LastIndexOf('.'));//得到扩展名
                    string name = encodefileName.Remove(encodefileName.Length - ext.Length);//得到文件名称
                    name = name.Replace(".", "%2e"); //关键代码
                    fileName = name + ext;
                }
                else
                {
                    fileName = encodefileName;
                }
                //火狐浏览器不需将中文文件名进行编码格式转换
                //fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
            }
            //ZhiFang.Common.Log.Log.Debug("fileName:"+ fileName);
            return fileName;
        }
        #endregion
    }
}