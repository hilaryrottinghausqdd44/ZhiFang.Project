using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.IBLL.RBAC
{
    public interface IBRBACModule : ZhiFang.IBLL.Base.IBGenericManager<ZhiFang.Entity.RBAC.RBACModule>
    {
        //bool Add();
        //bool Edit();
        //bool Remove();

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
        /// 根据员工ID查询该人员所具有权限的模块树
        /// </summary>
        /// <param name="longHREmpID">员工ID</param>
        /// <returns>IList&lt;RBACModule&gt;</returns>
        BaseResultTree SearchModuleTreeByHREmpID(long longHREmpID);

        /// <summary>
        /// 根据角色ID查询该角色所具有权限的模块树
        /// </summary>
        /// <param name="longHREmpID">员工ID</param>
        /// <returns>IList&lt;RBACModule&gt;</returns>
        BaseResultTree SearchModuleTreeByRBACRoleID(long longRBACRoleID);

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
        /// 获取目录树或列表
        /// </summary>
        /// <param name="RBACModuleID">根节点ID</param>
        /// <param name="IsListTree">是否是列表树</param>
        /// <returns>BaseResultTree&lt;RBACModule&gt;</returns>
        BaseResultTree<RBACModule> SearchRBACModuleToTree(long RBACModuleID, bool IsListTree);
        /// <summary>
        /// 根据模块用户使用编码查询模块
        /// </summary>
        /// <param name="longHREmpID">用户使用编码</param>
        /// <returns>IList&lt;RBACModule&gt;</returns>
        RBACModule SearchModuleByUseCode(string UseCode);

        bool DeleteModuleByUseCode(string UseCode);
        /// <summary>
        /// 更新简单字段
        /// </summary>
        /// <param name="strParas"></param>
        /// <returns></returns>
        bool UpdateSingleFields(string[] strParas);
        /// <summary>
        /// 获取检测小组和检测仪器目录树
        /// </summary>
        /// <param name="RBACModuleUseCode">模块目录代码</param>
        /// <param name="IsListTree"></param>
        /// <returns></returns>
        #region
        //object SearchTestGroupEquipRBACModuleToTree(string RBACModuleUseCode, bool IsListTree);
        /// <summary>
        /// 获取检测小组和检测仪器目录树
        /// </summary>
        /// <param name="RBACModuleUseCode">模块目录代码</param>
        /// <param name="IsListTree"></param>
        /// <returns></returns>
        // object SearchTestGroupOrEquipRBACModuleToTree(string RBACModuleUseCode, int ModuleType, bool IsListTree);
        /// <summary>
        /// 获取检测小组和检测仪器目录树
        /// </summary>
        /// <param name="HREmpID"></param>
        /// <returns></returns>
        //object SearchTestGroupEquipModuleTreeByHREmpID(long HREmpID);

        /// <summary>
        /// 获取检测小组和检测仪器目录树
        /// </summary>
        /// <param name="HREmpID"></param>
        /// <returns></returns>
        //object SearchTestGroupOrEquipModuleTreeByHREmpID(long longHREmpID, int ModuleType);
        /// <summary>
        /// 根据员工ID获取检测小组
        /// </summary>
        /// <param name="HREmpID"></param>
        /// <returns></returns>
        //object SearchTestGroupModuleByHREmpID(long HREmpID);
        #endregion

    }
}
