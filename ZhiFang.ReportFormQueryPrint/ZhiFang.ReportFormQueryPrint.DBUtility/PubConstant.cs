using System;
using System.Configuration;
namespace ZhiFang.ReportFormQueryPrint.DBUtility
{
    
    public class PubConstant
    {        
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionString
        {           
            get 
            {
                string _connectionString = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportFormQueryPrintConnectionString");       
                string ConStringEncrypt = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ConStringEncrypt");
                if (ConStringEncrypt == "true")
                {
                    _connectionString = DESEncrypt.Decrypt(_connectionString);
                }
                return _connectionString; 
            }
        }

        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString(configName);
            string ConStringEncrypt = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ConStringEncrypt");
            if (ConStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }


    }
}
