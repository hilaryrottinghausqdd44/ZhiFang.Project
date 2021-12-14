using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ZhiFang.Tools
{
    public class Tool
    {
        /// <summary>
        /// 返回一个GUID+"_"+传来的参数
        /// </summary>
        /// <param name="strKey">拼接字符串</param>
        /// <returns>guid_strKey</returns>
        public static string GetGUID(string strKey)
        {
            if (strKey.Trim() != "")
                return System.Guid.NewGuid().ToString() + "_" + strKey;
            else
                return System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 获取配置文件 DBSourceType，判断字段是否显示
        /// </summary>
        /// <returns>页面控件是否显示，控制标签style的display属性返回 none/block</returns>
        public static string GetFieldVisible()
        {
            string strValue = "block";
            try
            {
                string str = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType");
                if (str.ToLower().IndexOf("mssql.weblis") >= 0)
                {
                    strValue = "none";
                }
            }
            catch
            {
                strValue = "none";                
            }
            return strValue;
        }

        public static string StringLength(string strSource, int iLength)
        {
            System.Text.Encoding en = System.Text.Encoding.GetEncoding("GB2312");
            String str = strSource;
            str = Regex.Replace(str, "(&)[Nn][Bb][Ss][Pp](;)", "");
            //str = Regex.Replace(str, "(&)[Nn][Bb][Ss][Pp](;)", "");


            bool bLong = false;
            while (en.GetByteCount(str) > iLength * 2)
            {
                str = str.Substring(0, str.Length - 1);
                bLong = true;
            }
            if (bLong)
            {
                //最后为两个字符ASCII
                if (en.GetByteCount(str.Substring(str.Length - 2)) == 2)
                    str = str.Substring(0, str.Length - 2) + "..";

                //最后为两个汉字UNICODE
                else if (en.GetByteCount(str.Substring(str.Length - 2)) == 4)
                    str = str.Substring(0, str.Length - 1) + "..";

                //最后为一个字符ASCII＋一个汉字UNICODE
                else if (en.GetByteCount(str.Substring(str.Length - 2)) == 3)
                {
                    if (en.GetByteCount(str.Substring(str.Length - 1)) == 1)
                        str = str.Substring(0, str.Length - 1) + ".";
                    else if (en.GetByteCount(str.Substring(str.Length - 1)) == 2)
                        str = str.Substring(0, str.Length - 1) + "..";

                }
            }
            return str;
        }
    }
}
