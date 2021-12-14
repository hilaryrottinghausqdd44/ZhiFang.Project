using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Packaging;
using System.Web;
using Ionic.Zip;

namespace ZhiFang.ReportFormQueryPrint.Common
{
    public class SqlInjectHelper : System.Web.UI.Page
    {
        private static string StrKeyWord = "select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec|master|net local group administrators|net user";
        private static string StrSymbol = ";|[|]|{|}|@|*";

        private HttpRequest request;
        public SqlInjectHelper(System.Web.HttpRequest _request)
        {
            this.request = _request;
        }
        public bool CheckSqlInject()
        {
            return CheckRequestQuery() || CheckRequestForm();
        }

        ///<summary>  
        ///检查URL中是否包含Sql注入  
        /// <param name="_request">当前HttpRequest对象</param>  
        /// <returns>如果包含sql注入关键字，返回：true;否则返回：false</returns>  
        ///</summary>  
        public bool CheckRequestQuery()
        {
            if (request.QueryString.Count > 0)
            {
                foreach (string sqlParam in this.request.QueryString)
                {
                    if (sqlParam == "__VIEWSTATE")
                        continue;
                    if (sqlParam == "__EVENTVALIDATION")
                        continue;
                    if (CheckKeyWord(request.QueryString[sqlParam].ToLower()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        ///<summary>  
        ///检查提交的表单中是否包含Sql注入关键字
        /// <param name="_request">当前HttpRequest对象</param>  
        /// <returns>如果包含sql注入关键字，返回：true;否则返回：false</returns>  
        ///</summary>  
        public bool CheckRequestForm()
        {
            if (request.Form.Count > 0)
            {
                foreach (string sqlParam in this.request.Form)
                {
                    if (sqlParam == "__VIEWSTATE")
                        continue;
                    if (sqlParam == "__EVENTVALIDATION")
                        continue;
                    if (CheckKeyWord(request.Form[sqlParam]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        ///<summary>  
        ///检查字符串中是否包含Sql注入关键字  
        /// <param name="_key">被检查的字符串</param>  
        /// <returns>如果包含sql注入关键字，返回：true;否则返回：false</returns>  
        ///</summary>  
        public static bool CheckKeyWord(string _key)
        {
            string[] pattenKeyWord = StrKeyWord.Split('|');
            string[] pattenSymbol = StrSymbol.Split('|');
            foreach (string sqlParam in pattenKeyWord)
            {
                if (_key.Contains(sqlParam + " ") || _key.Contains(" " + sqlParam))
                {
                    return true;
                }
            }
            foreach (string sqlParam in pattenSymbol)
            {
                if (_key.Contains(sqlParam))
                {
                    return true;
                }
            }
            return false;
        }

    }




}
