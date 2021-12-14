using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC.ViewObject.Response;

namespace ZhiFang.IBLL.RBAC
{
    public interface IBRBACRoleRight : ZhiFang.IBLL.Base.IBGenericManager<ZhiFang.Entity.RBAC.RBACRoleRight>
    {
        IBRBACRole IBRBACRole { set; get; }

        IList<RBACRoleRight> GetRBACRoleRightByRBACUserCodeAndModuleOperID(string strUserCode, long longModuleOperID);

        /// <summary>
        /// 模块操作权限信息获取服务
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="longModuleOperID"></param>
        /// <returns></returns>
        IList<RBACRoleRight> RBAC_BA_GetRBACRoleRightByHREmpIDAndModuleOperID(long longHREmpID, long longModuleOperID);


        /// <summary>
        /// 模块操作权限信息获取服务
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="longModuleOperID"></param>
        /// <returns></returns>
        bool JudgeRBACRoleRightByHREmpIDAndModuleOperID(long longHREmpID, long longModuleOperID);

        /// <summary>
        /// 获取模块操作的行过滤树
        /// </summary>
        /// <param name="longModuleOperID"></param>
        /// <returns></returns>
        BaseResultTree SearchRBACRowFilterTreeByModuleOperID(long longModuleOperID, bool isPreconditions);

        /// <summary>
        /// 复制角色权限
        /// </summary>
        /// <param name="sourceModuleOperID">源模块操作ID</param>
        /// <param name="targetModuleOperID">目标模块操作ID</param>
        /// <returns>BaseResultDataValue</returns>
        BaseResultDataValue AddRBACRoleRightByModuleOperID(long sourceModuleOperID, long targetModuleOperID);

        /// <summary>
        /// 根据模块操作ID删除模块操作权限
        /// </summary>
        /// <returns></returns>
        bool DeleteByRBACModuleOperIDList(List<long> RBACModuleOperIDList);
        /// <summary>
        /// 清空相关的角色权限的行数据条件Id值
        /// </summary>
        /// <param name="rowFilterId">行数据条件Id值</param>
        /// <param name="moduleOperId">模块操作Id值</param>
        /// <returns></returns>
        bool UpdateRBACRoleRightOfClearRowFilterIdByModuleOperId(long rowFilterId, long moduleOperId);
        /// <summary>
        /// 删除除默认的行数据过滤条件的角色访问权限的数据
        /// </summary>
        /// <param name="rowFilterId"></param>
        /// <param name="moduleOperId"></param>
        /// <returns></returns>
        bool DeleteRBACRoleRightByModuleOperId(long rowFilterId, long moduleOperId);

        /// <summary>
        /// 获取模块操作的行数据条件配置时的待选角色信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <param name="moduleId"></param>
        /// <param name="moduleOperId"></param>
        /// <returns></returns>
        EntityList<RBACRoleVO> SearchRBACRoleRightByModuleIdAndModuleOperID(int page, int limit, string sort, long moduleId, long moduleOperId, string where);

        /// <summary>
        /// 清空相关的角色权限的行数据条件Id值
        /// </summary>
        /// <param name="rowFilterId">行数据条件Id值</param>
        /// <param name="preconditionsId">预置条件Id值</param>
        /// <returns></returns>
        bool UpdateRBACRoleRightOfClearRowFilterIdByPreconditionsId(long rowFilterId, long preconditionsId);
        /// <summary>
        /// 获取预置条件的行过滤树
        /// </summary>
        /// <param name="preconditionsId"></param>
        /// <returns></returns>
        BaseResultTree SearchRBACRowFilterTreeByPreconditionsId(long preconditionsId);
        /// <summary>
        /// 获取预置条件配置时的待选角色信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <param name="moduleId"></param>
        /// <param name="moduleOperId"></param>
        /// <returns></returns>
        EntityList<RBACRoleVO> SearchRBACRoleRightByModuleIdAndPreconditionsId(int page, int limit, string sort, long moduleId, long preconditionsId, string where,string rowFilterId);
        /// <summary>
        /// 删除除默认的行数据过滤条件的角色访问权限的数据
        /// </summary>
        /// <param name="rowFilterId"></param>
        /// <param name="preconditionsId"></param>
        /// <returns></returns>
        bool DeleteRBACRoleRightByPreconditionsId(long rowFilterId, long preconditionsId);

    }
}