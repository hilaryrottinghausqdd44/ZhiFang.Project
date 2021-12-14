using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

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


    }
}
