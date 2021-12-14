using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace ZhiFang.Tools
{
    public class Tools
    {
         /// <summary>
        /// 检测使用WebService方法的用户的合法性
        /// </summary>
        /// <param name="token">身份信息的加密串|身份信息的解密串
        /// 用户名称,用户密码,调用时间
        /// 如:admin,a,2009-08-25 13:45:10</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>true/false</returns>
        public static bool checkCallWebServiceUserValidate(string token, out string errorMsg)
        {
            //Log.Info("token=" + token);
            errorMsg = "";
            if (token == null || token == "")
            {
                return true;
            }
            try
            {
                string[] tokenList = token.Split(new char[] { '|' });
                if (tokenList.Length != 2)
                {
                    errorMsg = "用户身份验证:用户token格式不合法,应该使用|对token进行分隔不能使用该接口!";
                    return false;
                }
                //用户的token
                string tokenEncrypt = tokenList[0];//加密的token
                //加密的用户信息(用户名称,用户密码,调用时间)
                string[] tokenEncryptList = tokenEncrypt.Split(new char[] { ',' });
                if (tokenEncryptList.Length != 3)
                {
                    errorMsg = "用户身份验证:用户token格式不合法,加密串应该由:用户名称,用户密码,调用时间组成!";
                    return false;
                }
                string userNameEncrypt = tokenEncryptList[0];//用户名称
                string passWordEncrypt = tokenEncryptList[1];//密码
                string loginTimeEncrypt = tokenEncryptList[2];//登录时间

                //解密的用户信息(用户名称,用户密码,调用时间)
                string tokenDecrypt = tokenList[1];//解密的token
                string[] tokenDecryptList = tokenDecrypt.Split(new char[] { ',' });
                if (tokenDecryptList.Length != 3)
                {
                    errorMsg = "用户身份验证:用户token格式不合法,解密串应该由:用户名称,用户密码,调用时间组成!";
                    return false;
                }
                string userNameDecrypt = tokenDecryptList[0];//用户名称
                string passWordDecrypt = tokenDecryptList[1];//密码
                string loginTimeDecrypt = tokenDecryptList[2];//登录时间

                //原始的用户信息(只加密一次)
                string userName = UnCovertPassword(userNameEncrypt);//用户名称
                string passWord = UnCovertPassword(passWordEncrypt);//密码
                string loginTime = UnCovertPassword(loginTimeEncrypt);//登录时间
                //原始的用户信息(加密两次变换过的)
                string userName1 = UnCovertPassword(UnCovertPassword(userNameDecrypt));//用户名称
                string passWord1 = UnCovertPassword(UnCovertPassword(passWordDecrypt));//密码
                string loginTime1 = UnCovertPassword(UnCovertPassword(loginTimeDecrypt));//登录时间

                string tag = "_";
                if (userName1.EndsWith(tag) == false)
                {
                    errorMsg = "不是本系统的合法用户,不能调用WebService的方法!用户名称应该以下面的字符做结尾:" + tag;
                    return false;
                }
                if (((userName + tag) != userName1) || (passWord != passWord1) || (loginTime != loginTime1))
                {
                    errorMsg = "用户身份验证:用户token格式不合法,用户名称和密码不一致!";
                    return false;

                }
                try
                {
                    System.DateTime time = Convert.ToDateTime(loginTime);
                    TimeSpan ts = System.DateTime.Now - time;
                    double secondCALL = ts.TotalDays * 24 * 60 * 60 + ts.TotalHours * 60 * 60 + ts.TotalMinutes * 60 + ts.TotalSeconds;
                    secondCALL = Math.Abs(secondCALL);
                    int allowTime = 24*60*60;//允许调用运行的时间(必须在一天之内调用才行!)
                    if (secondCALL > allowTime)
                    {
                        errorMsg = "调用WebService的方法超时!超过" + allowTime.ToString() + "秒";
                        return false;
                    }
                }
                catch (System.Exception exTime)
                {
                    errorMsg = "调用WebService时,从Token里获取调用时间:" + exTime.Message;
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                errorMsg = "调用WebService时,进行用户身份验证出错:" + ex.Message;
                return false;
            }
            return true;
        }
     
        public static string Get256Str(string StrConst, string astr, int iLength, int i1, string Result)
        {
            int iCount64 = 0;
            for (int i = 0; i < i1; i++)
            {
                int ichar = StrConst.IndexOf(astr[iLength + i - i1]);
                if (ichar < 0)
                    ichar = 0;
                iCount64 = iCount64 * 64 + ichar;
            }
            for (int i = 1; i <= 3; i++)
            {
                if (iCount64 != 0)
                {
                    int imod256 = iCount64 % 256;
                    Result = (char)(imod256) + Result;
                    iCount64 = iCount64 / 256;
                }
            }
            return Result;
        }
        /// <summary>
        /// 拆分带路径的文件Path和FileName两部分
        /// </summary>
        public class SplitFileName
        {
            public string Path;//文件路径
            public string FileName; //文件名称
            public char[] Split;//分隔符
            public SplitFileName()
            { }

            public SplitFileName(string path, string fileName, char[] split)
            {
                Path = path;
                FileName = fileName;
                Split = split;
            }
        }

        // <summary>
        /// 拆分文件为路径和文件名称两部分
        /// 返回SplitFileName类
        /// </summary>
        /// <param name="fileName">文件名称（带路径）</param>
        /// <param name="split">分隔符，一般为“/”</param>
        /// <returns>SplitFileName类</returns>
        public static SplitFileName getSplitFileName(string fileName, char[] split)
        {
            SplitFileName splitFileName = new SplitFileName();
            splitFileName.Path = "";
            splitFileName.FileName = "";
            splitFileName.Split = split;
            if (fileName == "")
                return splitFileName;
            string[] splitArray = fileName.Split(splitFileName.Split);
            if (splitArray.Length == 0)
            {
                splitFileName.FileName = "";
                splitFileName.Path = "";
            }
            else
            {
                splitFileName.FileName = splitArray[splitArray.Length - 1];//文件名称
                if (splitFileName.FileName == "")
                    splitFileName.Path = fileName;
                else
                    splitFileName.Path = fileName.Replace(splitFileName.FileName, "");
            }
            return splitFileName;
        }

        /// <summary>
        /// 解密用户运行WebService方法的字符串,解密为:ZHIFANG+'yyyy-mm-dd hh:nn:ss'
        /// </summary>
        /// <param name="astr"></param>
        /// <returns></returns>
        public static string UnCovertPassword(string astr)
        {
            string Result = "";
            string StrConst = "=qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890+";
            int iLength = astr.Length;
            if (iLength == 0)
                return Result;
            int idiv4 = iLength / 4;
            int iMod4 = iLength % 4;
            while (idiv4 > 0)
            {
                Result = Get256Str(StrConst, astr, iLength, 4, Result);
                iLength = iLength - 4;
                idiv4--;
            }
            switch (iMod4)
            {
                case 1:
                    Result = Get256Str(StrConst, astr, iLength, 1, Result);
                    break;
                case 2:
                    Result = Get256Str(StrConst, astr, iLength, 2, Result);
                    break;
                case 3:
                    Result = Get256Str(StrConst, astr, iLength, 3, Result);
                    break;
            }
            return Result;
        }

        /// <summary>
        /// 将某个字符串写到指定的文件中，文件的编码为UTF8
        /// </summary>
        /// <param name="file">指定的文件名称</param>
        /// <param name="text">写到文件的指定文本</param>
        public static void writeStringToLocalFile(string file, string text)
        {
            try
            {
                SplitFileName splitFileName = Tools.getSplitFileName(file, new char[] { '\\' });
                string path = splitFileName.Path;
                //创建目录
                if (path.Length > 0)
                {
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                }
                //删除旧文件
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.SetAttributes(file, FileAttributes.Normal);
                    System.IO.File.Delete(file);
                }
                System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(file, false, Encoding.UTF8);//重新创建
                fileWriter.Write(text);
                fileWriter.Close();
                //System.IO.File.SetAttributes(file, FileAttributes.ReadOnly);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return;
        }
        /// <summary>
        /// 将某个字符串写到指定的文件中，文件的编码为UTF8
        /// </summary>
        /// <param name="file">指定的文件名称</param>
        /// <param name="text">写到文件的byte数组</param>
        public static void writeBytesToLocalFile(string file, byte[] text)
        {
            try
            {
                SplitFileName splitFileName = Tools.getSplitFileName(file, new char[] { '\\' });
                string path = splitFileName.Path;
                //创建目录
                if (path.Length > 0)
                {
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                }
                //删除旧文件
                FileAttributes oldAttr = FileAttributes.Normal;
                if (System.IO.File.Exists(file))
                {
                    //取原来文件的属性
                    oldAttr = System.IO.File.GetAttributes(file);
                    System.IO.File.SetAttributes(file, FileAttributes.Normal);
                    System.IO.File.Delete(file);
                }
                System.IO.FileStream fileStream = new System.IO.FileStream(file, FileMode.OpenOrCreate);
                fileStream.Write(text, 0, text.Length);
                fileStream.Close();
                //保留旧文件的属性
                System.IO.File.SetAttributes(file, oldAttr);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return;
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

        public static string convertESCToHtml(string esc)
        {
            string ret = esc;
            //转义
            ret = ret.Replace("&quot;", "\"");
            ret = ret.Replace("&apos;", "'");
            ret = ret.Replace("&gt;", ">");
            ret = ret.Replace("&lt;", "<");
            ret = ret.Replace("%20", " ");//空格
            ret = ret.Replace("&nbsp;", " ");//空格
            ret = ret.Replace("&amp;", "&");//要最后转，这样可以保证二次转义的正确性，比如输入的是&lt;会先转义为&amp;lt;
            return ret;
        }
    }
}