using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.RBAC
{	
	public interface IBRBACRoleModule : IBGenericManager<ZhiFang.Digitlab.Entity.RBACRoleModule>
	{
        IBRBACRole IBRBACRole { set; get; }

        IList<RBACRoleModule> GetRBACRoleModuleByRBACUserCodeAndModuleID(string strUserCode, long longModuleID);

        /// <summary>
        /// 模块权限信息获取服务
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="longModuleID"></param>
        /// <returns></returns>
        IList<RBACRoleModule> RBAC_BA_GetRBACRoleModuleByHREmpIDAndModuleID(long longHREmpID, long longModuleID);

        /// <summary>
        /// 根据员工ID是否具有该模块的权限
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="longModuleID"></param>
        /// <returns></returns>
        bool RBAC_BA_GetModuleRightByHREmpID(long longHREmpID, long longModuleID);
	} 
}