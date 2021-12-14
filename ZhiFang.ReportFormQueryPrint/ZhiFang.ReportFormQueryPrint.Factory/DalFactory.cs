using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ZhiFang.ReportFormQueryPrint.Factory
{
    public static class DalFactory<T>
    {
        
        public static T obj;
        public static T GetDal(string classname)
        {
            try
            {
                string path = System.Configuration.ConfigurationSettings.AppSettings["DBSourceType"];
                string className = path + "." + classname;
                //Type t = T;
                // Using the evidence given in the config file load the appropriate assembly and class
                //ZhiFang.Common.Log.Log.Error("path:" + path + ".classname:" + classname);
                return (T)Assembly.Load(path.Substring(0,path.LastIndexOf('.'))).CreateInstance(className);
            }
            catch(Exception e)
            {
                ZhiFang.Common.Log.Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——数据类工厂错误！" + e.ToString());
                return (T)(new object());
            }
        }
        public static T GetDal(string classname,string DBSourceType,string ConnStr)
        {
            try
            {
                string path = DBSourceType;
                string className = path + "." + classname;
                //Type t = T;
                // Using the evidence given in the config file load the appropriate assembly and class
                //ZhiFang.Common.Log.Log.Error("path:" + path + ".classname:" + classname);
                return (T)Assembly.Load(path.Substring(0, path.LastIndexOf('.'))).CreateInstance(className,false , System.Reflection.BindingFlags.Default, null,new object[1] { ConnStr},null,null);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——数据类工厂错误！" + e.ToString());
                return (T)(new object());
            }
        }
        public static T GetDal(string classname, string DBSourceType)
        {
            try
            {
                string path = DBSourceType;
                string className = path + "." + classname;
                //Type t = T;
                // Using the evidence given in the config file load the appropriate assembly and class
                //ZhiFang.Common.Log.Log.Error("path:" + path + ".classname:" + classname);
                return (T)Assembly.Load(path.Substring(0, path.LastIndexOf('.'))).CreateInstance(className);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——数据类工厂错误！" + e.ToString());
                return (T)(new object());
            }
        }
    }
}
