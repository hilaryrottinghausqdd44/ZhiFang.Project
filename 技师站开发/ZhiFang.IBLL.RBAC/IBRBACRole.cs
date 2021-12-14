using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.IBLL.RBAC
{
    public interface IBRBACRole : ZhiFang.IBLL.Base.IBGenericManager<ZhiFang.Entity.RBAC.RBACRole>
    {
        //bool Add();
        //bool Edit();
        //bool Remove();

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
        /// 根据模块ID查询拥有该模块权限的角列表
        /// </summary>
        /// <param name="longModuleID">模块ID</param>
        /// <returns>IList&lt;RBACRole&gt;</returns>
        IList<RBACRole> SearchRoleByModuleID(long longModuleID);

        /// <summary>
        /// 根据模块ID查询拥有该模块权限的角色模块操作列表
        /// </summary>
        /// <param name="longModuleID">模块ID</param>
        /// <returns>IList&lt;RBACRole&gt;</returns>
        string SearchRoleModuleOperByModuleID(long longModuleID);

        /// <summary>
        /// 根据模块操作ID查询拥有该权限的角色列表
        /// </summary>
        /// <param name="longModuleOperID">模块操作ID</param>
        /// <returns>IList&lt;RBACRole&gt;</returns>
        IList<RBACRole> SearchRoleByModuleOperID(long longModuleOperID);
        /// <summary>
        /// 根据部门ID查询该角色列表树
        /// </summary>
        /// <param name="longHRDeptID">角色ID</param>
        /// 角色ID等于0时 查询所有角色
        /// <returns>BaseResultTree</returns>
        BaseResultTree<RBACRole> SearchRBACRoleListTree(long longRBACRoleID);

        /// <summary>
        /// 根据部门ID查询该角色单列树
        /// </summary>
        /// <param name="longHRDeptID">角色ID</param>
        /// 角色ID等于0时 查询所有角色
        /// <returns>BaseResultTree</returns>
        BaseResultTree SearchRBACRoleTree(long longRBACRoleID);
    }
}