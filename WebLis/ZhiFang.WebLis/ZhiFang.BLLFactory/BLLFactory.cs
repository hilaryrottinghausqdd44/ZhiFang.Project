using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ZhiFang.Common.Log;
using ZhiFang.Common.AppConfig;

namespace ZhiFang.BLLFactory
{
    /// <summary>
    /// 泛型业务工厂
    /// </summary>
    /// <typeparam name="T">返回的接口类型</typeparam>
    public static class BLLFactory<T>
    {
        public static T obj;
        /// <summary>
        /// 业务工厂
        /// </summary>
        /// <param name="classname">业务类型名</param>
        /// <returns>T类型的接口对象</returns>
        public static T GetBLL(string classname)
        {
            try
            {
                string strModule = typeof(T).Module.Name;
                string strPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString(strModule.Substring(0,strModule.LastIndexOf('.')));
                string strClassName = strPath + "." + classname;
                return (T)Assembly.Load(strPath).CreateInstance(strClassName);
            }
            catch (Exception e)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——业务类工厂错误！", e);
                throw e;
            }
        }
        /// <summary>
        /// 业务工厂
        /// </summary>
        /// <param name="classname">业务类型名</param>
        /// <returns>T类型的接口对象（必须以IB开头）</returns>
        public static T GetBLL()
        {
            try
            {
               string strModule = typeof(T).Module.Name;
               strModule = strModule.Substring(0, strModule.LastIndexOf('.'));
               string strPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString(strModule);
                string strIFullName= typeof(T).FullName;
                string strClassName = strPath + strIFullName.Substring(0, strIFullName.LastIndexOf('.')).Replace(strModule, "") + "." + typeof(T).Name.Replace("IB", "");
                return (T)Assembly.Load(strPath).CreateInstance(strClassName);
            }
            catch (Exception e)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——业务类工厂错误！", e);
                throw e;
            }
        }       
    }

    /// <summary>
    /// 业务工厂
    /// </summary>
    public static class BLLFactory
    {
        /// <summary>
        /// 通用业务工厂
        /// </summary>
        /// <param name="classname">业务类型名</param>
        /// <returns>T类型的接口对象</returns>
        public static object GetBLLInterface(string classname)
        {
            try
            {
                string path = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang.IBLL.Interface");
                string className = path + "." + classname;
                return Assembly.Load(path).CreateInstance(className);

            }
            catch (Exception e)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——业务类工厂错误！", e);
                throw e;
            }
        }
        /// <summary>
        /// 样本跟踪业务工厂
        /// </summary>
        /// <param name="classname">样本跟踪业务对象名</param>
        /// <returns>样本跟踪业务类型的接口对象</returns>
        public static object GetBLLSampleTrack(string classname)
        {
            try
            {
                string path = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang.IBLL.SampleTrack");
                string className = path + "." + classname;
                return Assembly.Load(path).CreateInstance(className);

            }
            catch (Exception e)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——样本跟踪业务类工厂错误！", e);
                throw e;
            }
        }
        /// <summary>
        /// 公共平台层业务工厂
        /// </summary>
        /// <param name="classname">公共平台层业务对象名</param>
        /// <returns>公共平台层业务类型的接口对象</returns>
        public static object GetBLLCommon(string classname)
        {
            try
            {
                string path = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFang.IBLL.Common");
                string className = path + "." + classname;
                return Assembly.Load(path).CreateInstance(className);

            }
            catch (Exception e)
            {
                Log.Error(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "——公共平台层业务类工厂错误！", e);
                throw e;
            }
        }
    }
   
}
