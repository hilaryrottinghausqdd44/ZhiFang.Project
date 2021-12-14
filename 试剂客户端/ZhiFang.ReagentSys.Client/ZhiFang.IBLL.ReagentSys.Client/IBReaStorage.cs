using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaStorage : IBGenericManager<ReaStorage>
    {
        /// <summary>
        /// 获取库房信息
        /// </summary>
        /// <param name="employeeID">员工ID</param>
        /// <param name="isEmpPermission">是否根据库房员工权限获取</param>
        /// <returns></returns>
        BaseResultTree GetReaStorageTree(long employeeID, bool isEmpPermission, string operType);
        IList<ReaStorage> SearchReaStorageListByStorageAndLinHQL(string storageHql, string linkHql, string operType, string sort, int page, int count);
        /// <summary>
        /// 根据员工权限获取库房信息
        /// </summary>
        /// <param name="storageHql"></param>
        /// <param name="linkHql"></param>
        /// <param name="operType">关系类型</param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<ReaStorage> SearchListByStorageAndLinHQL(string storageHql, string linkHql, string operType, string sort, int page, int count);

        BaseResultData AddReaStorageSyncByInterface(string syncField, string syncFieldValue, Dictionary<string, object> dicFieldAndValue);
    }
}