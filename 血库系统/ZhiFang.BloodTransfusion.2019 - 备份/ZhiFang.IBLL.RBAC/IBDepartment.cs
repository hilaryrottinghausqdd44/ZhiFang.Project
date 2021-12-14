using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.RBAC
{
    /// <summary>
    ///
    /// </summary>
    public interface IBDepartment : IBGenericManager<Department, int>
    {
        /// <summary>
        /// 获取6.6数据库的科室同步信息
        /// </summary>
        /// <returns></returns>
        IList<JObject> GetSyncDeptList();

        ///// <summary>
        ///// 根据员工ID查询所属部门列表
        ///// </summary>
        ///// <param name="longHREmpID">员工ID</param>
        ///// <returns>IList&lt;Department&gt;</returns>
        //IList<Department> SearchDepartmentByHREmpID(long longHREmpID);
        ///// <summary>
        ///// 根据部门身份查询部门列表
        ///// </summary>
        ///// <param name="longDepartmentIdentity">部门身份ID</param>
        ///// <returns>IList&lt;Department&gt;</returns>
        //IList<Department> SearchDepartmentByDepartmentIdentity(long longDepartmentIdentity);

        /// <summary>
        /// 根据部门ID查询单列树
        /// </summary>
        /// <param name="longDepartmentID">部门ID</param>
        /// 部门ID等于0时 查询所有部门
        /// <returns>BaseResultTree</returns>
        BaseResultTree SearchDepartmentTree(int longDepartmentID);
        /// <summary>
        /// 根据部门ID查询列表树
        /// </summary>
        /// <param name="longDepartmentID">部门ID</param>
        /// 部门ID等于0时 查询所有部门
        /// <returns>BaseResultTree</returns>
        BaseResultTree<Department> SearchDepartmentListTree(int longDepartmentID);

        BaseResultTree GetDepartmentEmployeeFrameTree(int strDepartmentID);

        /// <summary>
        /// 根据部门ID查询部门列表(包含子部门)
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        IList<Department> SearchDepartmentListByDepartmentId(int deptId);

        /// <summary>
        /// 根据部门ID查询子部门的ID列表(包含子部门)
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        string SearchDepIdStrByDeptId(int deptId);

        IList<Department> GetChildList(int ParentID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverPUser"></param>
        /// <param name="arrFields"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        void AddSCOperation(Department serverPUser, string[] arrFields, long empID, string empName);


    }
}