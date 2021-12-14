namespace ZhiFang.Tools
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public class Validate
    {
        public static string AddLeftRightChar(string content, char splitchar, string leftstring, string rightstring, char newsplitchar)
        {
            try
            {
                string str = "";
                string[] strArray = content.Split(new char[] { splitchar });
                foreach (string str2 in strArray)
                {
                    object obj2 = str;
                    str = string.Concat(new object[] { obj2, leftstring, str2, rightstring, newsplitchar });
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        public static string CheckString(string str)
        {
            if (str.Trim() == "")
            {
                return "";
            }
            return str.Trim().Replace("'", "''").Replace("$", "").Replace("\"", "");
        }

        public static Color ConvertCColorToColor(string color)
        {
            Color color2 = new Color();
            int red = Convert.ToInt16("0x" + color.Substring(1, 2), 0x10);
            int green = Convert.ToInt16("0x" + color.Substring(3, 2), 0x10);
            int blue = Convert.ToInt16("0x" + color.Substring(5, 2), 0x10);
            return Color.FromArgb(red, green, blue);
        }

        public static string convertCoding(string srcString, Encoding encodeSrc, Encoding encodeDst)
        {
            byte[] bytes = encodeSrc.GetBytes(srcString);
            byte[] buffer2 = Encoding.Convert(encodeSrc, encodeDst, bytes);
            char[] chars = new char[encodeDst.GetCharCount(buffer2, 0, buffer2.Length)];
            encodeDst.GetChars(buffer2, 0, buffer2.Length, chars, 0);
            return new string(chars);
        }

        public static double ConvertDateTimeInt(DateTime time)
        {
            DateTime time2 = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1));
            TimeSpan span = (TimeSpan) (time - time2);
            return span.TotalSeconds;
        }

        public static DateTime ConvertIntDateTime(double d)
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1)).AddSeconds(d);
        }

        public static string CutString(string original, int leftLength)
        {
            if (original == null)
            {
                return null;
            }
            int length = original.Length;
            int num2 = 0;
            int num3 = 0;
            Regex regex = new Regex(@"[^\x00-\xff]");
            for (int i = 0; i < length; i++)
            {
                int num5 = 0;
                if (regex.Match(original, i, 1).Success)
                {
                    num2 += 2;
                    num5 = 2;
                }
                else
                {
                    num2++;
                    num5 = 1;
                }
                if (num2 > (leftLength * 2))
                {
                    return original.Substring(0, i - ((num3 == 1) ? 2 : 1));
                }
                num3 = num5;
            }
            return original;
        }

        public static string DecodeString(string chr)
        {
            if (chr == null)
            {
                return "";
            }
            chr = chr.Replace("&nbsp;", " ");
            chr = chr.Replace("&lt;", "<");
            chr = chr.Replace("&gt;", ">");
            chr = chr.Replace("<br>", "\r\n");
            chr = chr.Replace("&nbsp;&nbsp;&nbsp;", "\t");
            return chr;
        }

        public static string EncodeMd5(string cleanString)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(cleanString);
            byte[] buffer2 = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(bytes);
            string str = null;
            for (int i = 0; i < buffer2.Length; i++)
            {
                str = str + buffer2[i].ToString("x");
            }
            return str;
        }

        public static string EncodeString(string chr)
        {
            if (chr == null)
            {
                return "";
            }
            chr = chr.Replace(" ", "&nbsp;");
            chr = chr.Replace("<", "&lt;");
            chr = chr.Replace(">", "&gt;");
            chr = chr.Replace("\r\n", "<br>");
            chr = chr.Replace("\t", "&nbsp;&nbsp;&nbsp;");
            return chr;
        }

        public static string FormatTimeToDbString(string datetime)
        {
            string str = "";
            try
            {
                str = Convert.ToDateTime(datetime).ToString("yyyy-MM-dd");
            }
            catch
            {
            }
            return str;
        }

        public static string GetDateNowString()
        {
            return (DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString());
        }

        public static Hashtable GetPageParameHashTable(string url)
        {
            Hashtable hashtable = null;
            try
            {
                hashtable = getParaFromURL(urlEscape(HttpUtility.UrlDecode(url)));
            }
            catch
            {
            }
            return hashtable;
        }

        public static Hashtable getParaFromURL(string url)
        {
            Hashtable hashtable = new Hashtable();
            char[] separator = new char[] { '?' };
            string[] strArray = url.Split(separator);
            if (strArray.Length >= 2)
            {
                foreach (string str2 in url.Substring(strArray[0].Length + 1).Split(new char[] { '&' }))
                {
                    string[] strArray3 = str2.Split(new char[] { '=' });
                    string key = strArray3[0];
                    if (key != "")
                    {
                        string str = "";
                        if (strArray3.Length > 1)
                        {
                            str = str2.Replace(key + "=", "");
                        }
                        if (hashtable[key] == null)
                        {
                            hashtable.Add(key, CheckString(str));
                        }
                    }
                }
            }
            return hashtable;
        }

        public static string GetSubString(string str, int length)
        {
            string str2 = str;
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < str2.Length; i++)
            {
                if (Regex.IsMatch(str2.Substring(i, 1), @"[\u4e00-\u9fa5]+"))
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                if (num <= length)
                {
                    num2++;
                }
                if (num >= length)
                {
                    return str2.Substring(0, num2);
                }
            }
            return str2;
        }

        public static bool IsDate(string dt)
        {
            try
            {
                DateTime.Parse(dt);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool IsDecimal(string de)
        {
            try
            {
                decimal.Parse(de);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool IsDouble(string dl)
        {
            try
            {
                double.Parse(dl);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool IsInt(string x)
        {
            try
            {
                int.Parse(x);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool IsNum(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsNumber(str, i))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool JudageIsNoIncludeChinese(string s)
        {
            Regex regex = new Regex("[一-龥]");
            return regex.IsMatch(s);
        }

        public static bool JudgeIsNoMobile(string mobile)
        {
            return Regex.IsMatch(mobile, @"^(13[0-9]|15[0-9]|18[0-9])\d{8}$");
        }

        public static string RndNum(int VcodeNum)
        {
            string[] strArray = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z".Split(new char[] { ',' });
            string str2 = "";
            int num = -1;
            Random random = new Random();
            for (int i = 1; i < (VcodeNum + 1); i++)
            {
                if (num != -1)
                {
                    random = new Random((i * num) * ((int) DateTime.Now.Ticks));
                }
                int index = random.Next(0x23);
                if ((num != -1) && (num == index))
                {
                    return RndNum(VcodeNum);
                }
                num = index;
                str2 = str2 + strArray[index];
            }
            return str2;
        }

        public static int SqlTimeOut()
        {
            return 0x5f5e0ff;
        }

        public static string ToPageParameUrl(string pageurl, string pam)
        {
            if (pageurl.IndexOf("?") >= 0)
            {
                return (pageurl + HttpUtility.UrlEncode(pam));
            }
            return (pageurl + "?" + HttpUtility.UrlEncode(pam));
        }

        public static string urlEscape(string url)
        {
            string str = url;
            return str.Replace("+LIKE+", " LIKE ").Replace("%3b", ";").Replace("&apos;", "'").Replace("%20", " ").Replace("%25", "%");
        }

        public static bool Verify(string str)
        {
            bool flag = true;
            if (((str.Length <= 0) || (str == "")) || (str == null))
            {
                flag = false;
            }
            return flag;
        }

        public static bool CheckSqlStr(string p)
        {
            bool flag = true;
            if (p.ToLower().IndexOf("select") >= 0 || p.ToLower().IndexOf("insert") >= 0 || p.ToLower().IndexOf("update") >= 0 || p.ToLower().IndexOf("into") >= 0 || p.ToLower().IndexOf("form") >= 0)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 防止Sql注入方法
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string ReplacSpecialCharacters(string inputString)
        {
            inputString = inputString.Trim();
            inputString = inputString.Replace("'", "");
            inputString = inputString.Replace(";--", "");
            inputString = inputString.Replace("--", "");
            inputString = inputString.Replace("=", "");
            inputString = inputString.Replace("and", "");
            inputString = inputString.Replace("exec", "");
            inputString = inputString.Replace("insert", "");
            inputString = inputString.Replace("select", "");
            inputString = inputString.Replace("delete", "");
            inputString = inputString.Replace("update", "");
            inputString = inputString.Replace("chr", "");
            inputString = inputString.Replace("mid", "");
            inputString = inputString.Replace("master", "");
            inputString = inputString.Replace("or", "");
            inputString = inputString.Replace("truncate", "");
            inputString = inputString.Replace("char", "");
            inputString = inputString.Replace("declare", "");
            inputString = inputString.Replace("join", "");
            inputString = inputString.Replace("count", "");
            inputString = inputString.Replace("*", "");
            inputString = inputString.Replace("%", "");
            inputString = inputString.Replace("union", "");
            inputString = inputString.Replace("<", "");
            inputString = inputString.Replace("[", "");
            inputString = inputString.Replace("{", "");
            inputString = inputString.Replace("}", "");
            inputString = inputString.Replace("]", "");
            inputString = inputString.Replace(">", "");
            return inputString;
        }

        /// <summary>
        /// 防止Sql注入，替换where串中的特殊字符
        /// </summary>
        /// <param name="whereString"></param>
        /// <returns></returns>
        public static string ReplaceWhereString(string whereString)
        {
            string newWhereStr = string.Empty;
            whereString = whereString.ToLower();
            if (whereString.Contains("("))
                whereString = whereString.Replace("(", "");
            if (whereString.Contains(")"))
                whereString = whereString.Replace(")", "");
            if (whereString.Contains("$"))
                whereString = whereString.Replace("$", "");
            string[] tempArray = whereString.Replace("and", "$").Split('$');


            if (tempArray.Length > 0)
            {


                foreach (string strt in tempArray)
                {
                    if (strt.Trim() == "")
                        continue;
                    string str = strt.ToLower();
                    string[] strFileds;
                    string strFiledName = string.Empty;
                    string strFiledValue = string.Empty;
                    if (str.Contains("="))
                    {
                        strFileds = str.Split('=');
                        strFiledName = strFileds[0];
                        strFiledValue = ReplacSpecialCharacters(strFileds[1]);
                        if (newWhereStr != string.Empty)
                            newWhereStr += " and " + strFiledName + "=" + " '" + strFiledValue.Trim() + " '";
                        else
                            newWhereStr += strFiledName + "=" + " '" + strFiledValue.Trim() + " '";
                    }

                    else if (str.Contains(" like "))
                    {
                        strFileds = str.Replace("like", "=").Split('=');
                        strFiledName = strFileds[0];
                        strFiledValue = ReplacSpecialCharacters(strFileds[1]);
                        if (newWhereStr != string.Empty)
                            newWhereStr += " and " + strFiledName + "like" + " '%" + strFiledValue.Trim() + "%'";
                        else
                            newWhereStr += strFiledName + "like" + " '%" + strFiledValue.Trim() + "%'";

                    }

                    else if (str.Contains(" in "))
                    {
                        strFileds = str.Replace(" in ", "=").Split('=');
                        strFiledName = strFileds[0];

                        string[] tempValues = strFileds[1].Split(',');
                        foreach (string tempValue in tempValues)
                        {
                            if (tempValue.Trim() == "")
                                continue;

                            if (strFiledValue == string.Empty)
                                strFiledValue += "'" + ReplacSpecialCharacters(tempValue) + "'";
                            else
                                strFiledValue += "," + "'" + ReplacSpecialCharacters(tempValue) + "'";

                        }
                        if (newWhereStr != string.Empty)
                            newWhereStr += " and " + strFiledName + " in " + " (" + strFiledValue.Trim() + " )";
                        else
                            newWhereStr += strFiledName + " in " + " (" + strFiledValue.Trim() + " )";

                    }
                    else
                    {
                        if (newWhereStr != string.Empty)
                            newWhereStr += " and " + str.Trim();
                        else
                            newWhereStr += str.Trim();
                    }

                }
            }

            return newWhereStr;
        }
    }
}

