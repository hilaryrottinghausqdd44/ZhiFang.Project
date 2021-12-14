using System;
using System.Configuration;
using System.Reflection;
using ZhiFang.Common.Log;

namespace ZhiFang.DALFactory
{
    public static class DalFactory<T>
    {
        public static T obj;

        public static T GetDal(string classname,string dbparamter)
        {
            try
            {
                string str = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType");
                string typeName = str + "." + classname;
                string[] p={dbparamter};
                return (T)Assembly.Load(str.Substring(0, str.LastIndexOf('.'))).CreateInstance(typeName, false, System.Reflection.BindingFlags.CreateInstance, null, p, null,null);
            }
            catch (Exception exception)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——数据类工厂错误！" , exception);
                return (T)new object();
            }
        }
        public static T GetDalByDB(string dbparamter)
        {
            try
            {
                string str = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType");
                string typeName = str + "." + typeof(T).Name.Replace("ID","");
                string[] p = { dbparamter };
                return (T)Assembly.Load(str.Substring(0, str.LastIndexOf('.'))).CreateInstance(typeName, false, System.Reflection.BindingFlags.CreateInstance, null, p, null, null);
            }
            catch (Exception exception)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——数据类工厂错误！", exception);
                return (T)new object();
            }
        }
        public static T GetDal()
        {
            try
            {
                string str = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType");
                string typeName = str + "." + typeof(T).Name.Replace("ID", "");
                return (T)Assembly.Load(str.Substring(0, str.LastIndexOf('.'))).CreateInstance(typeName);
            }
            catch (Exception exception)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——数据类工厂错误！", exception);
                return (T)new object();
            }
        }
        public static T GetDalByClassName(string classname)
        {
            try
            {
                string str = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType");
                string typeName = str + "." + classname;
                return (T)Assembly.Load(str.Substring(0, str.LastIndexOf('.'))).CreateInstance(typeName);
            }
            catch (Exception exception)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——数据类工厂错误！", exception);
                return (T)new object();
            }
        }

        public static T GetBaseDal()
        {
            try
            {
                string str = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType");
                string typeName = str.Substring(0,str.LastIndexOf('.')) + ".BaseDALLisDB";
                return (T)Assembly.Load(str.Substring(0, str.LastIndexOf('.'))).CreateInstance(typeName);
            }
            catch (Exception exception)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——数据类工厂错误！", exception);
                return (T)new object();
            }
        }
        public static T GetDalHisDB(string classname, string dbparamter)
        {
            try
            {
                string str = ZhiFang.Common.Public.ConfigHelper.GetConfigString("HisDBSourceType");
                string typeName = str.Substring(0,str.LastIndexOf('.')) + "." + classname;
                string[] p = { dbparamter };
                return (T)Assembly.Load(str.Substring(0, str.LastIndexOf('.'))).CreateInstance(typeName, false, System.Reflection.BindingFlags.CreateInstance, null, p, null, null);
            }
            catch (Exception exception)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——数据类工厂错误！", exception);
                return (T)new object();
            }
        }
        public static T GetDalHisDB(string dbparamter)
        {
            try
            {
                string str = ZhiFang.Common.Public.ConfigHelper.GetConfigString("HisDBSourceType");
                string typeName = str.Substring(0, str.LastIndexOf('.'))  +"." + typeof(T).Name.Replace("ID", "");
                string[] p = { dbparamter };
                return (T)Assembly.Load(str.Substring(0, str.LastIndexOf('.'))).CreateInstance(typeName, false, System.Reflection.BindingFlags.CreateInstance, null, p, null, null);
            }
            catch (Exception exception)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——数据类工厂错误！", exception);
                return (T)new object();
            }
        }
    }
    public static class DalFactory
    {
        public static object GetDal(string classname,string dbparamter)
        {
            try
            {
                string str = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType");
                string typeName = str + "." + classname;
                string[] p = { dbparamter };
                return Assembly.Load(str.Substring(0, str.LastIndexOf('.'))).CreateInstance(typeName, false, System.Reflection.BindingFlags.CreateInstance, null, p, null, null);
            }
            catch (Exception exception)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——数据类工厂错误！", exception);
                return new object();
            }
        }
        public static object GetDalHisDB(string classname, string dbparamter)
        {
            try
            {
                string str = ZhiFang.Common.Public.ConfigHelper.GetConfigString("HisDBSourceType");
                string typeName = str.Substring(0, str.LastIndexOf('.')) + "." + classname;
                string[] p = { dbparamter };
                return Assembly.Load(str.Substring(0, str.LastIndexOf('.'))).CreateInstance(typeName, false, System.Reflection.BindingFlags.CreateInstance, null, p, null, null);
            }
            catch (Exception exception)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——数据类工厂错误！", exception);
                return new object();
            }
        }
    }
}
