using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Digitlab.Entity
{
    public static class SYSDataRowRoleCacheBase
    {
        public static string ModuleOperCacheKey = "SYSCacheModuleOper";
        public static string SYSCacheRowFilterKey = "SYSCacheRowFilter";
        /// <summary>
        /// 是否启用角色权限的行数据条件
        /// </summary>
        public static readonly bool IsUseDataRowRoleFilter = false;
        /// <summary>
        /// 是否重新刷新缓存模块服务信息
        /// false
        /// </summary>
        public static bool IsRefreshModuleOperCache
        {
            get
            {
                return isRefreshModuleOperCache;
            }
            set
            {
                isRefreshModuleOperCache = value;
            }
        }
        private static bool isRefreshRowFilterCache = true;
        /// <summary>
        /// 是否重新刷新缓存的行数据条件信息
        /// </summary>
        public static bool IsRefreshRowFilterCache
        {
            get
            {
                return isRefreshRowFilterCache;
            }
            set
            {
                isRefreshRowFilterCache = value;
            }
        }
        /// <summary>
        /// 缓存的模块服务在IIS进程中的内存大小
        /// </summary>
        public static string ModuleOperCacheSize { get; set; }
        private static bool isRefreshModuleOperCache = true;
        public static string GetSizeStr(double objSize)
        {
            string[] units = new String[] { "B", "KB", "MB", "GB", "TB", "PB" };
            double mod = 1024.0;
            int j = 0;
            while (objSize >= mod)
            {
                objSize /= mod;
                j++;
            }
            string sizeStr = Math.Round(objSize) + units[j];
            return sizeStr;
        }
    }
}
