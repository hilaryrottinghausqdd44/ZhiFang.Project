using System;
using System.Configuration;
using System.Collections.Specialized;

namespace ZhiFang.Common.Public
{
    public sealed class ConfigHelper
    {
        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            try
            {
                string CacheKey = "AppSettings-" + key;
                object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
                if (objModel == null)
                {
                    try
                    {
                        objModel = ConfigurationManager.AppSettings[key];
                        if (objModel != null)
                        {
                            ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                        }
                        else
                            return "";
                    }
                    catch (Exception e)
                    {
                        Common.Log.Log.Error("读取配置文件中Key为" + key + "的内容出错！" + e.ToString());
                        return "";
                    }
                }
                return objModel.ToString();
            }
            catch(Exception e)
            {
                Common.Log.Log.Error("读取配置文件中Key为" + key + "的内容出错！" + e.ToString());
                return "";
            }
        }

        /// <summary>
        /// 得到AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool(string key)
        {
            bool result = false;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException e)
                {
                    Common.Log.Log.Error("配置文件中Key为" + key + "的内容转换为Bool类型时出错！" + e.ToString());
                    // Ignore format exceptions.
                }
            }
            return result;
        }

        /// <summary>
        /// 得到AppSettings中的配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException e)
                {
                    Common.Log.Log.Error("配置文件中Key为" + key + "的内容转换为Decimal类型时出错！" + e.ToString());
                    // Ignore format exceptions.
                }
            }

            return result;
        }

        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt(string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException e)
                {
                    Common.Log.Log.Error("配置文件中Key为" + key + "的内容转换为Int类型时出错！" + e.ToString());
                    // Ignore format exceptions.
                }
            }

            return result;
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="connectName">数据连接名称</param>
        /// <returns>string</returns>
        public static string GetDataConnectionString(string connectName)
        {
            try
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings[connectName].ToString();
            }
            catch (Exception e)
            {
                Common.Log.Log.Error("读取配置文件中数据连接名为【" + connectName + "】的内容出错！" + e.ToString());
                return "";
            }
        }

        /// <summary>
        /// 得到databaseSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDataBaseSettings(string sectionName, string key)
        {
            string result = "";
            try
            {
                object dbSection = System.Configuration.ConfigurationManager.GetSection(sectionName);
                if (dbSection != null)
                {
                    try
                    {
                        result = ((NameValueCollection)dbSection)[key].ToString();
                    }
                    catch (Exception ex)
                    {
                        Common.Log.Log.Error("读取配置文件" + sectionName + "中Key为" + key + "的内容出错！" + ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Log.Log.Error("读取配置文件中dbSectionName为" + sectionName + "的内容出错！" + ex.ToString());
            }
            return result;
        }
    }
}
