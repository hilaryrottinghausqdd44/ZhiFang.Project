using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.Base;

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
        BaseResultTree SearchRBACRowFilterTreeByModuleOperID(long longModuleOperID);

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
	} 
}