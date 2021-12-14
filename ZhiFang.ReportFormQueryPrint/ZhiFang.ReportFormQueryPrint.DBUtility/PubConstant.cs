using System;
using System.Configuration;
namespace ZhiFang.ReportFormQueryPrint.DBUtility
{
    
    public class PubConstant
    {        
        /// <summary>
        /// ��ȡ�����ַ���
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
        /// �õ�web.config������������ݿ������ַ�����
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
