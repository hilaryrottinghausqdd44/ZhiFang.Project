using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;


namespace ZhiFang.Digitlab.IDAO
{	
	public interface IDRBACModuleOperDao : IDBaseDao<ZhiFang.Digitlab.Entity.RBACModuleOper, long>
	{
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

	} 
}