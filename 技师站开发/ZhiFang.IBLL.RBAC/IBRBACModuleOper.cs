using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
namespace ZhiFang.IBLL.RBAC
{
    public interface IBRBACModuleOper : ZhiFang.IBLL.Base.IBGenericManager<ZhiFang.Entity.RBAC.RBACModuleOper>
    {
        //bool Add();
        //bool Edit();
        //bool Remove();

        /// <summary>
        /// 根据模块ID查询其包含的模块操作列表
        /// </summary>
        /// <param name="longModuleID">模块ID</param>
        /// <returns>IList&lt;RBACModuleOper&gt;</returns>
        IList<RBACModuleOper> SearchModuleOperIDByModuleID(long longModuleID);

        /// <summary>
        /// 根据角色ID查询该角色拥有权限的模块操作列表
        /// </summary>
        /// <param name="longRoleID">角色ID</param>
        /// <returns>IList&lt;RBACModuleOper&gt;</returns>
        IList<RBACModuleOper> SearchModuleOperByRoleID(long longRoleID);

        /// <summary>
        /// 根据员工ID查询该人员所具有权限的模块操作列表
        /// </summary>
        /// <param name="longHREmpID">员工ID</param>
        /// <returns>IList&lt;RBACModuleOper&gt;</returns>
        IList<RBACModuleOper> SearchModuleOperByHREmpID(long longHREmpID);

        /// <summary>
        /// 根据用户编码查询该人员所具有权限的模块操作列表
        /// </summary>
        /// <param name="strUserCode">用户编码</param>
        /// <returns>IList&lt;RBACModuleOper&gt;</returns>
        IList<RBACModuleOper> SearchModuleOperByUserCode(string strUserCode);

        /// <summary>
        /// 模块操作权限判定服务
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="longModuleOperID"></param>
        /// <returns></returns>
        bool JudgeModuleOperByHREmpIDAndModuleOperID(long longHREmpID, long longModuleOperID);

        /// <summary>
        /// 根据Session中的人员ID返回新增的模块列表
        /// </summary>
        /// <returns></returns>
        IList<RBACModuleOper> RBAC_BA_GetNewModuleListBySessionHREmpID(long longHREmpID);

        /// <summary>
        /// 根据模块ID删除模块操作
        /// </summary>
        /// <returns></returns>
        bool DeleteByRBACModuleId(long RBACModuleID);
        /// <summary>
        /// 获取系统缓存模块服务信息
        /// </summary>
        /// <returns></returns>
        IList<SYSCacheModuleOper> GetModuleOperCacheList();
        /// <summary>
        /// 将选择的模块服务新增复制到指定的模块中
        /// </summary>
        /// <param name="moduleId">待复制的模块ID</param>
        /// <param name="copyModuleOpeIdStr">选择需要复制的模块服务Id字符串(123,222)</param>
        /// <returns></returns>
        BaseResultBool CopyRBACModuleOperOfModule(long moduleId, string copyModuleOpeIdStr);

    }
}