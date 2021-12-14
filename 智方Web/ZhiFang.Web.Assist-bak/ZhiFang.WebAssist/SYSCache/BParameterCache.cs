using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.Entity.WebAssist;

namespace ZhiFang.WebAssist
{
    public static class BParameterCache
    {
        private static HttpApplication _ApplicationCache;
        /// <summary>
        /// 系统参数缓存
        /// </summary>
        public static HttpApplication ApplicationCache
        {
            get
            {
                if (_ApplicationCache == null)
                {
                    _ApplicationCache = new HttpApplication();
                }
                return _ApplicationCache;
            }
            set { _ApplicationCache = value; }
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
                IBBParameter IBBParameter = (IBBParameter)context.GetObject("BBParameter");
                BParameter bparameter = IBBParameter.GetParameterByParaNo(cacheKey);
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