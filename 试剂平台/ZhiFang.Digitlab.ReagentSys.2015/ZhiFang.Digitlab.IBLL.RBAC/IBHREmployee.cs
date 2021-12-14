using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using System.IO;

namespace ZhiFang.Digitlab.IBLL.RBAC
{	
	public interface IBHREmployee : IBGenericManager<ZhiFang.Digitlab.Entity.HREmployee>
	{
        //bool Add();
        //bool Edit();
        //bool Remove();

        /// <summary>
        /// 查询角色下的员工列表
        /// </summary>
        /// <param name="longRoleID">角色ID</param>
        /// <returns>IList&lt;HREmployee&gt;</returns>
        IList<HREmployee> SearchHREmployeeByRoleID(long longRoleID);

        /// <summary>
        /// 查询部门包含的员工列表
        /// </summary>
        /// <param name="longHRDeptID">部门ID</param>
        /// <returns>IList&lt;HREmployee&gt;</returns>
        IList<HREmployee> SearchHREmployeeByHRDeptID(long longHRDeptID);

        /// <summary>
        /// 查询指定部门和角色下的员工列表
        /// </summary>
        /// <param name="longHRDeptID">部门ID</param>
        /// <param name="longRBACRoleID">角色ID</param>
        /// <returns></returns>
        IList<HREmployee> SearchHREmployeeByHRDeptIDAndRBACRoleID(long longHRDeptID, long longRBACRoleID);

        /// <summary>
        /// 查询指定部门的员工列表，并过滤拥有指定角色的员工
        /// </summary>
        /// <param name="longHRDeptID">部门ID</param>
        /// <param name="longRBACRoleID">角色ID</param>
        /// <returns></returns>
        IList<HREmployee> SearchHREmployeeNoRBACRoleIDByHRDeptID(long longHRDeptID, long longRBACRoleID);

        /// <summary>
        /// 查询部门的直属员工列表(包含子部门)
        /// </summary>
        /// <param name="strHRDeptID">部门ID<</param>
        /// <param name="page">页数</param>
        /// <param name="limit">每页记录数</param>
        /// <param name="sort">排序字段</param>
        /// <param name="flagRole"> flagRole为null或０，查找所有员工；为１查找已分配角色的员工；；为２查找未分配角色的员工</param>
        /// <returns></returns>
        IList<HREmployee> SearchHREmployeeByHRDeptID(string where, int page, int limit, string sort, int flagRole);

        /// <summary>
        /// 根据职位ID查询员工列表
        /// </summary>
        /// <param name="longHRPositionID">职位ID</param>
        /// <returns>IList&lt;HREmployee&gt;</returns>
        IList<HREmployee> SearchHREmployeeByHRPositionID(long longHRPositionID);

        /// <summary>
        /// 查询部门身份下的员工列表
        /// </summary>
        /// <param name="longHRDeptIdentityID">部门身份ID</param>
        /// <returns>IList&lt;HREmployee&gt;</returns>
        IList<HREmployee> SearchHREmployeeByHRDeptIdentityID(long longHRDeptIdentityID);

        /// <summary>
        /// 根据员工身份ID查询员工列表
        /// </summary>
        /// <param name="longHREmpIdentityID">员工身份ID</param>
        /// <returns>IList&lt;HREmployee&gt;</returns>
        IList<HREmployee> SearchHREmployeeByHREmpIdentityID(long longHREmpIdentityID);

        /// <summary>
        /// 根据用户帐号查询员工信息
        /// </summary>
        /// <param name="strUserAccount">用户账号</param>
        /// <returns>IList&lt;HREmployee&gt;</returns>
        IList<HREmployee> SearchHREmployeeByUserAccount(string strUserAccount);

        /// <summary>
        /// 根据用户代码查询员工信息
        /// </summary>
        /// <param name="strUserCode">用户代码</param>
        /// <returns>IList&lt;HREmployee&gt;</returns>
        IList<HREmployee> SearchHREmployeeByUserCode(string strUserCode);

        /// <summary>
        /// 根据用户ID查询所属角色所拥有权限的模块
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<RBACModule> RBAC_UDTO_SearchModuleByHREmpIDRole(long id, int page, int limit);
    } 
}