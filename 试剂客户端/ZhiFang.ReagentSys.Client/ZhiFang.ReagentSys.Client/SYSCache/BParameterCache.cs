using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;

namespace ZhiFang.ReagentSys.Client
{
    public static class BParameterCache
    {
        private static HttpApplication _applicationCache;
        /// <summary>
        /// 系统参数缓存
        /// </summary>
        public static HttpApplication ApplicationCache
        {
            get
            {
                if (_applicationCache == null)
                {
                    if (ZhiFang.Entity.ReagentSys.Client.BParameterCache.ApplicationCache != null)
                        _applicationCache = ZhiFang.Entity.ReagentSys.Client.BParameterCache.ApplicationCache;
                    if (_applicationCache == null)
                        _applicationCache = new HttpApplication();
                }
                return _applicationCache;
            }
            set { _applicationCache = value; }
        }

        /// <summary>
        /// 清除单一键缓存
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveOneCache(string CacheKey)
        {
            BParameterCache.ApplicationCache.Application.Remove(CacheKey);
        }
        /// <summary>
        /// 清除所有缓存WS
        /// </summary>
        public static void RemoveAllCache()
        {
            if (BParameterCache.ApplicationCache.Application.Count > 0)
            {
                BParameterCache.ApplicationCache.Application.RemoveAll();
            }
        }
        /// <summary>
        /// 添加或者更新指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string cacheKey, object objObject)
        {
            string objValue = "";
            if (objObject != null && objObject.GetType().ToString() == "System.String")
            {
                objValue = objObject.ToString().Replace("&#92", @"\");
                //ZhiFang.Common.Log.Log.Debug("cacheValue:" + objValue);
            }

            if (BParameterCache.ApplicationCache.Application.AllKeys.Contains(cacheKey))
            {
                BParameterCache.ApplicationCache.Application.Set(cacheKey, String.IsNullOrEmpty(objValue) ? objValue : objValue);
            }
            else
            {
                BParameterCache.ApplicationCache.Application.Add(cacheKey, String.IsNullOrEmpty(objValue) ? objValue : objValue);
            }
        }
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static string GetParaValue(string cacheKey)
        {
            return GetCache(cacheKey);
        }
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static string GetCache(string cacheKey)
        {
            string cacheValue = "";
            if (BParameterCache.ApplicationCache != null)
                cacheValue = (string)BParameterCache.ApplicationCache.Application.Get(cacheKey);
            //ZhiFang.Common.Log.Log.Error("缓存值:" + cacheValue);
            //如果为空,从数据库读取
            if (String.IsNullOrEmpty(cacheValue))
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBBParameter ibBParameter = (IBBParameter)context.GetObject("BBParameter");
                BParameter bparameter = ibBParameter.GetParameterByParaNo(cacheKey);
                if (bparameter != null)
                {
                    cacheValue = bparameter.ParaValue;
                    //ZhiFang.Common.Log.Log.Error("系统参数值:" + cacheValue);
                    //设置系统参数缓存
                    if (!String.IsNullOrEmpty(cacheValue))
                        cacheValue = cacheValue.ToString().Replace("&#92", @"\");
                }
                else
                {
                    //ZhiFang.Common.Log.Log.Error("web.config读取值:" + cacheValue);
                }
                SetCache(cacheKey, cacheValue);
            }
            return cacheValue;
        }
    }

}