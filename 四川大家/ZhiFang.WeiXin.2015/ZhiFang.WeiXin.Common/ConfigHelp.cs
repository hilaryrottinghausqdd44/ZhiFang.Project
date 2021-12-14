using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace ZhiFang.WeiXin.Common
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
                object objModel = null;// Common.DataCache.GetCache(CacheKey);
                if (objModel == null)
                {
                    try
                    {
                        objModel = ConfigurationSettings.AppSettings[key];
                        //if (objModel != null)
                        //{
                        //    Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                        //}
                    }
                    catch
                    { return ""; }
                }
                return objModel.ToString();
            }
            catch
            {
                return null;
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
                catch (FormatException)
                {
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
                catch (FormatException)
                {
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
        public static int? GetConfigInt(string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal && cfgVal.Trim().Length != 0)
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    return null;
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        /// <summary>
        /// 获取自定义配置参数(string)
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="configxmlpath">配置文件相对地址</param>
        /// <returns></returns>
        public static string GetConfigString(string key, string configxmlpath)
        {
            string xmlpath = "";
            if (configxmlpath == null || configxmlpath.Trim() == "")
            {
                xmlpath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationSettings.AppSettings["ConfigPath"].ToString();
            }
            else
            {
                xmlpath = System.AppDomain.CurrentDomain.BaseDirectory + configxmlpath;
            }
            XmlDocument xd = new XmlDocument();
            xd.Load(xmlpath);
            XmlNodeList xnl = xd.SelectNodes("appSettings/add");
            for (int i = 0; i < xnl.Count; i++)
            {
                if (xnl.Item(i).Attributes["key"].Value.Trim() == key)
                {
                    return xnl.Item(i).Attributes["value"].Value.Trim();
                }
            }
            return null;
        }
        /// <summary>
        /// 获取自定义配置参数(bool)
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="configxmlpath">配置文件相对地址</param>
        /// <returns></returns>
        public static bool GetConfigBool(string key, string configxmlpath)
        {
            bool result = false;
            string cfgVal = GetConfigString(key, configxmlpath);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }
        /// <summary>
        /// 获取自定义配置参数(decimal)
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="configxmlpath">配置文件相对地址</param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string key, string configxmlpath)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        /// <summary>
        /// 获取自定义配置参数(int?)
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="configxmlpath">配置文件相对地址</param>
        /// <returns></returns>
        public static int? GetConfigInt(string key, string configxmlpath)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal && cfgVal.Trim().Length != 0)
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    return null;
                    // Ignore format exceptions.
                }
            }

            return result;
        }
    }
}
