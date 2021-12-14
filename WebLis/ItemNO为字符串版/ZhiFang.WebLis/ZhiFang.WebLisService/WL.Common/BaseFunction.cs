using System;
using System.Data;
using System.Collections;
using System.Text;
using ECDS.Common;
using System.Xml;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

using ECDS.Common.Enums;
using ECDS.Common.SysInfo;

namespace ZhiFang.WebLisService.WL.Common
{
    public class BaseFunction
    {

        /// <summary>
        /// 读取ini.xml文件的配置信息
        /// </summary>
        /// <param name="ConfigParaKey"></param>
        /// <returns></returns>
        public static string ReadINIConfig(string ConfigParaKey)
        {
            string strnode = "";
            try
            {
                XmlDocument domDebug =new  XmlDocument();
                domDebug.Load(AppDefaultValue.ConfigPath + "ini.xml");
                XmlNode nodeDeveloping = domDebug.DocumentElement.SelectSingleNode(ConfigParaKey);
                if (nodeDeveloping != null)
                {
                    strnode = nodeDeveloping.InnerXml;
                }
            }
            catch (Exception ex)
            {
                Log.Error("读取ini.xml文件内容" + ConfigParaKey + "失败", ex);
            }
            return strnode;
        }

        /// <summary>
        /// 获取数据源属于哪个部门
        /// </summary>
        /// <param name="name">数据源名称</param>
        /// <returns></returns>
        public static string GetDataBaseDepName(string name)
        {
            if (name.ToLower() == "lisdb")
            {
                return "LIS";
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 获取XML配置文件所在的磁盘目录
        /// </summary>
        /// <returns></returns>
        public static string getXmlConfigPath()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "xml\\";
            return path;
        }



        /// <summary>
        /// 获取发布数据存放的磁盘目录
        /// 从web.config的配置项ReportConfigPath获取,如果没有该配置,则返回系统的启动目录
        /// 获取配置项后,在后面自动加上report\\作为目录返回
        /// </summary>
        /// <returns></returns>
        public static string getReportConfigPath()
        {
            string path = "";
            if (ConfigurationSettings.AppSettings["ReportConfigPath"] != null)
            {
                path = ConfigurationSettings.AppSettings["ReportConfigPath"];
            }
            else
            {
                path = System.AppDomain.CurrentDomain.BaseDirectory;
            }
            if (ConfigurationSettings.AppSettings["ReportFormFilesDir"] != null)
            {
                path = path + "\\" + ConfigurationSettings.AppSettings["ReportFormFilesDir"];
            }
            path += "\\report\\";
            path = path.Replace("\\\\", "\\");
            return path;
        }




    }
}
