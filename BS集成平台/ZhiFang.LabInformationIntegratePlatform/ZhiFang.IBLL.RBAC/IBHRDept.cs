using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.Base;
using System.IO;
using System.Data;

namespace ZhiFang.IBLL.RBAC
{
    public interface IBHRDept : ZhiFang.IBLL.Base.IBGenericManager<HRDept>
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

        /// <summary>
        /// 获取到某一部门的所有下级部门Id信息
        /// </summary>
        /// <param name="id">当前部门信息</param>
        /// <returns></returns>
        IList<long> GetSubDeptIdListByDeptId(long id);

        /// <summary>
        /// 获取到某一部门的所有父级部门Id(ParentID=0结束)信息
        /// </summary>
        /// <param name="id">当前部门信息</param>
        /// <returns></returns>
        IList<long> GetParentDeptIdListByDeptId(long id);
        BaseResultDataValue TransformDeptByExcel(DataTable tmpdt);
    }
}
