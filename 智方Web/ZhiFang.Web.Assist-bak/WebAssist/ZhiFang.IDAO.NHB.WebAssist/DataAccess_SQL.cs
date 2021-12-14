
using System.Reflection;
using System.Web;

namespace ZhiFang.IDAO.NHB.WebAssist
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
        private static readonly string AssemblyPath = "ZhiFang.DAO.SQL.WebAssist";//DBUtility.PubConstant.GetADODataBaseSettings(Common.Public.ConfigHelper.GetDataBaseSettings("lisdatabaseSettings", "db.provider"));
        public DataAccess_SQL()
        { }

        #region CreateObject 

        #region GKBarcode
        /// <summary>
        /// 创建Department_SQL据层接口
        /// </summary>
        public static IDepartment_SQL CreateDepartment_SQL()
        {
            string ClassNamespace = AssemblyPath + ".Department_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDepartment_SQL)objType;
        }
        /// <summary>
        /// 创建FeeSetUp_SQL据层接口
        /// </summary>
        public static IFeeSetUp_SQL CreateFeeSetUp_SQL()
        {
            string ClassNamespace = AssemblyPath + ".FeeSetUp_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IFeeSetUp_SQL)objType;
        }

        /// <summary>
        /// 创建GKBarRed_SQL据层接口
        /// </summary>
        public static IGKBarRed_SQL CreateGKBarRed_SQL()
        {
            string ClassNamespace = AssemblyPath + ".GKBarRed_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IGKBarRed_SQL)objType;
        }

        /// <summary>
        /// 创建LastBarcodeS_SQL据层接口
        /// </summary>
        public static ILastBarcodeS_SQL CreateLastBarcodeS_SQL()
        {
            string ClassNamespace = AssemblyPath + ".LastBarcodeS_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ILastBarcodeS_SQL)objType;
        }
        /// <summary>
        /// 创建OperateType_SQL据层接口
        /// </summary>
        public static IOperateType_SQL CreateOperateType_SQL()
        {
            string ClassNamespace = AssemblyPath + ".OperateType_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IOperateType_SQL)objType;
        }
        /// <summary>
        /// 创建TestType_Info_SQL据层接口
        /// </summary>
        public static ITestType_Info_SQL CreateTestTypeInfo_SQL()
        {
            string ClassNamespace = AssemblyPath + ".TestType_Info_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ITestType_Info_SQL)objType;
        }
        /// <summary>
        /// 创建TestType_SQL据层接口
        /// </summary>
        public static ITestType_SQL CreateTestType_SQL()
        {
            string ClassNamespace = AssemblyPath + ".TestType_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ITestType_SQL)objType;
        }
        /// <summary>
        /// 创建User_SQL据层接口
        /// </summary>
        public static IUser_SQL CreateUser_SQL()
        {
            string ClassNamespace = AssemblyPath + ".User_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IUser_SQL)objType;
        }
        #endregion

        #region Lis
        /// <summary>
        /// 创建MEGroupSampleFormDao_SQL据层接口
        /// </summary>
        public static IDMEGroupSampleFormDao_SQL CreateMEGroupSampleFormDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".MEGroupSampleFormDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDMEGroupSampleFormDao_SQL)objType;
        }
        /// <summary>
        /// 创建MEGroupSampleItemDao_SQL据层接口
        /// </summary>
        public static IDMEGroupSampleItemDao_SQL CreateMEGroupSampleItemDao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".MEGroupSampleItemDao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDMEGroupSampleItemDao_SQL)objType;
        }
        #endregion

        #region His
        /// <summary>
        /// 创建ME百色市人民医院科室人员关系数据访问类据层接口
        /// </summary>
        public static IDBSDeptUserVODao_SQL CreateBSDeptUserVODao_SQL()
        {
            string ClassNamespace = AssemblyPath + ".BSDeptUserVODao_SQL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDBSDeptUserVODao_SQL)objType;
        }
        #endregion

        #endregion

        #region 缓存
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

    }
}
