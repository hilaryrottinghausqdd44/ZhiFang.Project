using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;


namespace ZhiFang.IDAO.RBAC
{	
	public interface IDHREmployeeDao : ZhiFang.IDAO.Base.IDBaseDao<HREmployee, long>
	{
        /// <summary>
        /// 获取员工对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="hasLabId">是否按机构LabId过滤</param>
        /// <returns></returns>
        HREmployee Get(long id, bool hasLabId);

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
        IList<RBACModule> SearchModuleByHREmpIDRole(long id, int page, int limit);
    } 
}