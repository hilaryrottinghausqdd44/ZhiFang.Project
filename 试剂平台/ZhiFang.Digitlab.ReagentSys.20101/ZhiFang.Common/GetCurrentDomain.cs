    using System;
    using System.Web;
namespace ZhiFang.Common.Public
{
    public class GetCurrentDomain
    {
        public static string GetCurent()
        {
            string str = "";
            string str2 = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToLower().Trim();
            string[] strArray = str2.Split(new char[] { '.' });
            if (strArray.Length <= 2)
            {
                return str2;
            }
            for (int i = 0; i < strArray.Length; i++)
            {
                if (str2.EndsWith("com.cn") || str2.EndsWith("net.cn"))
                {
                    if (i >= (strArray.Length - 3))
                    {
                        str = str + string.Format(".{0}", strArray[i]);
                    }
                }
                else if (i >= (strArray.Length - 2))
                {
                    str = str + string.Format(".{0}", strArray[i]);
                }
            }
            if (str.StartsWith("."))
            {
                str = str.Substring(1);
            }
            return str;
        }
    }
}

