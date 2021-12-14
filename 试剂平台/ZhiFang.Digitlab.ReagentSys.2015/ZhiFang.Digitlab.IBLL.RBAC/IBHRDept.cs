using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using System.IO;

namespace ZhiFang.Digitlab.IBLL.RBAC
{
    public interface IBHRDept : IBGenericManager<ZhiFang.Digitlab.Entity.HRDept>
    {
        //bool Add();
        //bool Edit();
        //bool Remove();

        /// <summary>
        /// 根据员工ID查询所属部门列表
        /// </summary>
        /// <param name="longHREmpID">员工ID</param>
        /// <returns>IList&lt;HRDept&gt;</returns>
        IList<HRDept> SearchHRDeptByHREmpID(long longHREmpID);

        /// <summary>
        /// 根据部门身份查询部门列表
        /// </summary>
        /// <param name="longHRDeptIdentity">部门身份ID</param>
        /// <returns>IList&lt;HRDept&gt;</returns>
        IList<HRDept> SearchHRDeptByHRDeptIdentity(long longHRDeptIdentity);
        /// <summary>
        /// 根据部门ID查询单列树
        /// </summary>
        /// <param name="longHRDeptID">部门ID</param>
        /// 部门ID等于0时 查询所有部门
        /// <returns>BaseResultTree</returns>
        BaseResultTree SearchHRDeptTree(long longHRDeptID);
        /// <summary>
        /// 根据部门ID查询列表树
        /// </summary>
        /// <param name="longHRDeptID">部门ID</param>
        /// 部门ID等于0时 查询所有部门
        /// <returns>BaseResultTree</returns>
        BaseResultTree<HRDept> SearchHRDeptListTree(long longHRDeptID);

        BaseResultTree GetHRDeptEmployeeFrameTree(long strHRDeptID);
        /// <summary>
        /// 根据部门ID查询部门列表(包含子部门)
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        IList<HRDept> SearchHRDeptListByHRDeptId(long deptId);

        /// <summary>
        /// 根据部门ID查询子部门的ID列表(包含子部门)
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        string SearchHRDeptIdListByHRDeptId(long deptId);
    }
}
