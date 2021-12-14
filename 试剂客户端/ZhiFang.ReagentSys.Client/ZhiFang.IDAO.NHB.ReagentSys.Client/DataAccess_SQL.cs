using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    /// <summary>
    /// 缓存操作类
    /// </summary>
    public class DataCache
    {
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }

    }
    public sealed class DataAccess_SQL
    {
        private static readonly string AssemblyPath = "ZhiFang.DAO.SQL.ReagentSys.Client";//DBUtility.PubConstant.GetADODataBaseSettings(Common.Public.ConfigHelper.GetDataBaseSettings("lisdatabaseSettings", "db.provider"));
        public DataAccess_SQL()
        { }

        #region CreateObject 

        //不使用缓存
        private static object CreateObjectNoCache(string AssemblyPath, string classNamespace)
        {
            try
            {
                object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                return objType;
            }
            catch//(System.Exception ex)
            {
                //string str=ex.Message;// 记录错误日志
                return null;
            }

        }
        //使用缓存
        private static object CreateObject(string AssemblyPath, string classNamespace)
        {
            object objType = DataCache.GetCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                    DataCache.SetCache(classNamespace, objType);// 写入缓存
                }
                catch//(System.Exception ex)
                {
                    //string str=ex.Message;// 记录错误日志
                }
            }
            return objType;
        }
        #endregion

        /// <summary>
        /// 创建ReaTestEquipLabDao_SQL数据层接口
        /// </summary>
        public static IDReaTestEquipLabDao_SQL CreateReaTestEquipLabDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".ReaTestEquipLabDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDReaTestEquipLabDao_SQL)objType;
        }
        /// <summary>
        /// 创建ReaTestItemDao_SQL数据层接口
        /// </summary>
        public static IDReaTestItemDao_SQL CreateReaTestItemDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".ReaTestItemDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDReaTestItemDao_SQL)objType;
        }
        /// <summary>
        /// 创建ReaTestEquipItemDao_SQL数据层接口
        /// </summary>
        public static IDReaTestEquipItemDao_SQL CreateReaTestEquipItemDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".ReaTestEquipItemDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDReaTestEquipItemDao_SQL)objType;
        }
        /// <summary>
        /// 创建ReaLisTestStatisticalResultsDao数据层接口
        /// </summary>
        public static IDReaLisTestStatisticalResultsDao_SQL CreateReaLisTestStatisticalResultsDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".ReaLisTestStatisticalResultsDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDReaLisTestStatisticalResultsDao_SQL)objType;
        }
        /// <summary>
        /// 创建CSUpdateToBSDao_SQL数据层接口
        /// </summary>
        public static IDCSUpdateToBSDao_SQL CreateCSUpdateToBSDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".CSUpdateToBSDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDCSUpdateToBSDao_SQL)objType;
        }
    }
}
