using System.Collections.Generic;
using ZhiFang.Entity.RBAC;


namespace ZhiFang.IDAO.RBAC
{
    public interface IDRBACRoleDao : ZhiFang.IDAO.Base.IDBaseDao<RBACRole, long>
    {
        /// <summary>
        /// 根据员工ID查询角色列表
        /// </summary>
        /// <param name="longHREmpID">员工ID</param>
        /// <returns>IList&lt;RBACRole&gt;</returns>
        IList<RBACRole> SearchRoleByHREmpID(long longHREmpID);

        /// <summary>
        /// 根据用户编码查询器角色列表
        /// </summary>
        /// <param name="strUserCode">用户编码</param>
        /// <returns>IList&lt;RBACRole&gt;</returns>
        IList<RBACRole> SearchRoleByUserCode(string strUserCode);

        /// <summary>
        /// 根据模块ID查询拥有该模块权限的角色列表
        /// </summary>
        /// <param name="longModuleID">模块ID</param>
        /// <returns>IList&lt;RBACRole&gt;</returns>
        IList<RBACRole> SearchRoleByModuleID(long longModuleID);

        /// <summary>
        /// 根据模块操作ID查询拥有该权限的角色列表
        /// </summary>
        /// <param name="longModuleOperID">模块操作ID</param>
        /// <returns>IList&lt;RBACRole&gt;</returns>
        IList<RBACRole> SearchRoleByModuleOperID(long longModuleOperID);
    }
}