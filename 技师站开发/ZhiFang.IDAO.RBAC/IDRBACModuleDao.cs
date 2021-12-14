using System.Collections.Generic;
using ZhiFang.Entity.RBAC;


namespace ZhiFang.IDAO.RBAC
{
    public interface IDRBACModuleDao : ZhiFang.IDAO.Base.IDBaseDao<RBACModule, long>
    {
        /// <summary>
        /// 根据模块操作ID查询其所属模块
        /// </summary>
        /// <param name="longModuleOperID">模块操作ID</param>
        /// <returns>IList&lt;RBACModule&gt;</returns>
        IList<RBACModule> SearchModuleByModuleOperID(long longModuleOperID);

        /// <summary>
        /// 根据角色ID查询该角色拥有权限的模块列表
        /// </summary>
        /// <param name="longRoleID">角色ID</param>
        /// <returns>IList&lt;RBACModule&gt;</returns>
        IList<RBACModule> SearchModuleByRoleID(long longRoleID);

        /// <summary>
        /// 根据员工ID查询该人员所具有权限的模块列表
        /// </summary>
        /// <param name="longHREmpID">员工ID</param>
        /// <returns>IList&lt;RBACModule&gt;</returns>
        IList<RBACModule> SearchModuleByHREmpID(long longHREmpID);

        /// <summary>
        /// 根据员工ID、模块ID查询该人员所具有权限的模块列表
        /// </summary>
        /// <param name="longHREmpID">员工ID</param>
        /// <param name="longModuleID">模块ID</param>
        /// <returns>IList&lt;RBACModule&gt;</returns>
        IList<RBACModule> SearchModuleByHREmpIDAndModuleID(long longHREmpID, long longModuleID);

        /// <summary>
        /// 根据人员编码查询该人员所具有权限的模块列表
        /// </summary>
        /// <param name="strUserCode">人员编码</param>
        /// <returns>IList&lt;RBACModule&gt;</returns>
        IList<RBACModule> SearchModuleByUserCode(string strUserCode);
        /// <summary>
        /// 根据模块用户使用编码查询模块
        /// </summary>
        /// <param name="longHREmpID">用户使用编码</param>
        /// <returns>IList&lt;RBACModule&gt;</returns>
        RBACModule SearchModuleByUseCode(string UseCode);
    }
}