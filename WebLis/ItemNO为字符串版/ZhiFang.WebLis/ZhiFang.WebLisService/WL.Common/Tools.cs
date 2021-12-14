using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using ECDS.Common;

namespace ZhiFang.WebLisService.WL.Common
{
    public class Tools
    {

        #region 使用WebService方法的用户的合法性
        

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






        public static string Get64Str(string StrConst, string astr, int iLength, int i1, string Result)
        {
            int iCount64 = 0;
            for (int i = 0; i < i1; i++)
            {
                int ichar = StrConst.IndexOf(astr[iLength + i - i1]);
                if (ichar < 0)
                    ichar = 0;
                iCount64 = iCount64 * 256 + ichar;
            }
            for (int i = 1; i <= 4; i++)
            {
                if (iCount64 == 0)
                {
                    Result = "=" + Result;
                }
                else
                {
                    int imod64 = iCount64 % 64;
                    iCount64 = iCount64 / 64;
                    Result = astr[imod64] + Result;
                }
            }
            return Result;
        }


        /// <summary>
        /// 解密用户运行WebService方法的字符串,解密为:ZHIFANG+'yyyy-mm-dd hh:nn:ss'
        /// </summary>
        /// <param name="astr"></param>
        /// <returns></returns>
        public static string CovertPassword(string astr)
        {
            string Result = "";
            string StrConst = "=qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890+";
            int iLength = astr.Length;
            if (iLength == 0)
            {
                Result = "=";
                return Result;
            }
            int idiv3 = iLength / 3;
            int iMod3 = iLength % 3;
            while (idiv3 > 0)
            {
                Result = Get64Str(StrConst, astr, iLength, 3, Result);
                iLength = iLength - 3;
                idiv3--;
            }
            switch (iMod3)
            {
                case 1:
                    Result = Get64Str(StrConst, astr, iLength, 1, Result);
                    break;
                case 2:
                    Result = Get64Str(StrConst, astr, iLength, 2, Result);
                    break;
            }
            return Result;
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


        #endregion




        /// <summary>
        /// 生成插入数据的SQL脚本
        /// 因为我们都以insert方式插入数据,不涉及到数据的修改,所以没有数据的字段就不生成在SQL脚本中
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="hashColumn"></param>
        /// <param name="hashContent"></param>
        /// <returns></returns>
        public static string getInsertSQL(string tableName, Hashtable hashColumn, Hashtable hashContent)
        {
            string sql = "";
            string insertModal = "INSERT INTO \"{0}\"({1}) VALUES({2})";
            string insertFieldNameSQL = "";
            string insertFieldValueSQL = "";
            //遍历字段内容
            System.Collections.IDictionaryEnumerator myEnumerator = hashContent.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string fieldName = myEnumerator.Key.ToString().ToUpper();
                if (hashColumn[fieldName] == null)
                {
                    string msg = "表 " + tableName + " 中不存在字段 " + fieldName + "!该字段内容将不被导入!";
                    Log.Info(msg);
                    continue;
                }
                string fieldValue = myEnumerator.Value.ToString();
                if (fieldValue == "")//没有内容,不生成SQL脚本
                    continue;
                if (insertFieldNameSQL != "")
                    insertFieldNameSQL += ",";
                insertFieldNameSQL += fieldName;
                if (insertFieldValueSQL != "")
                    insertFieldValueSQL += ",";
                //对内容进行转义(如&lt;为<等)
                fieldValue = clsCommon.ConvertData.convertESCToHtml(fieldValue);
                insertFieldValueSQL += "'" + fieldValue + "'";
            }
            if (insertFieldNameSQL != "")
            {
                //插入主表数据
                sql = string.Format(insertModal, tableName, insertFieldNameSQL, insertFieldValueSQL);
            }
            Log.Info("sql:" + sql);
            return sql;
        }







    }
}
