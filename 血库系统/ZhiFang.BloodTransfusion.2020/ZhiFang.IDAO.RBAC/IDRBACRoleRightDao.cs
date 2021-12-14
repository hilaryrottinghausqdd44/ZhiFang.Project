using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;


namespace ZhiFang.IDAO.RBAC
{	
	public interface IDRBACRoleRightDao : ZhiFang.IDAO.Base.IDBaseDao<RBACRoleRight, long>
	{
       IList<RBACRoleRight> GetRBACRoleRightByRBACUserCodeAndModuleOperID(string strUserCode, long longModuleOperID);

       /// <summary>
       /// 根据角色ID和模块操作ID获取角色权限列表
       /// </summary>
       /// <param name="longRoleID">角色ID</param>
       /// <param name="longModuleOperID">模块操作ID</param>
       /// <returns>IList&lt;RBACRoleRight&gt;</returns>
       IList<RBACRoleRight> SearchRBACRoleRightByRoleIDAndModuleOperID(long longRoleID, long longModuleOperID);

	} 
}