using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZhiFang.IDAO.RBAC
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
        private static readonly string AssemblyPath = "ZhiFang.DAO.SQL.WebAssist";
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

        #region RBAC
        /// <summary>
        /// 创建SServiceClientDao_SQL数据层接口
        /// </summary>
        public static IDAO.RBAC.IDSServiceClientDao_SQL CreateSServiceClientDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".SServiceClientDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDAO.RBAC.IDSServiceClientDao_SQL)objType;
        }
        /// <summary>
		/// 创建HRDeptDao_SQL数据层接口
		/// </summary>
		public static IDAO.RBAC.IDHRDeptDao_SQL CreateHRDeptDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".HRDeptDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDAO.RBAC.IDHRDeptDao_SQL)objType;
        }
        /// <summary>
		/// 创建HREmployeeDao_SQL数据层接口
		/// </summary>
		public static IDAO.RBAC.IDHREmployeeDao_SQL CreateHREmployeeDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".HREmployeeDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDAO.RBAC.IDHREmployeeDao_SQL)objType;
        }
        /// <summary>
		/// 创建RBACUserDao_SQL数据层接口
		/// </summary>
		public static IDAO.RBAC.IDRBACUserDao_SQL CreateRBACUserDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".RBACUserDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDAO.RBAC.IDRBACUserDao_SQL)objType;
        }
        /// <summary>
		/// 创建RBACRoleDao_SQL数据层接口
		/// </summary>
		public static IDAO.RBAC.IDRBACRoleDao_SQL CreateRBACRoleDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".RBACRoleDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDAO.RBAC.IDRBACRoleDao_SQL)objType;
        }
        /// <summary>
		/// 创建RBACRoleModuleDao_SQL数据层接口
		/// </summary>
		public static IDAO.RBAC.IDRBACRoleModuleDao_SQL CreateRBACRoleModuleDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".RBACRoleModuleDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDAO.RBAC.IDRBACRoleModuleDao_SQL)objType;
        }
        /// <summary>
		/// 创建RBACEmpRolesDao_SQL数据层接口
		/// </summary>
		public static IDAO.RBAC.IDRBACEmpRolesDao_SQL CreateRBACEmpRolesDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".RBACEmpRolesDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDAO.RBAC.IDRBACEmpRolesDao_SQL)objType;
        }
        /// <summary>
        /// 创建RBACModuleDao_SQL数据层接口
        /// </summary>
        public static IDAO.RBAC.IDRBACModuleDao_SQL CreateRBACModuleDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".RBACModuleDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDAO.RBAC.IDRBACModuleDao_SQL)objType;
        }
        #endregion

    }
}
