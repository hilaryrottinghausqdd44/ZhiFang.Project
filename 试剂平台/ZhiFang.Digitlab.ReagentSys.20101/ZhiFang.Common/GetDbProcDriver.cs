using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Common.Public
{
    public class GetDbProcDriver
    {
        /// <summary>
        /// 获取财务报表统计使用的数据库是sqlerver还是oracle
        /// </summary>
        /// <returns>空代表使用sqlserver/非空表示使用oracle</returns>
        public static string GetDbProcDriverByConfig()
        {
            string strDbProcDriver = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DbProcDriver");
            if (strDbProcDriver.Trim() == "" || strDbProcDriver.Trim().ToLower() == "sqlserver")
            {
                strDbProcDriver = "";
            }
            else
            {
                strDbProcDriver = "_" + strDbProcDriver.Trim().ToLower();
            }
            return strDbProcDriver;
        }
    }
}
