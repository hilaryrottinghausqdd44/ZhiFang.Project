using System.Collections.Generic;
using ZhiFang.Entity.RBAC;


namespace ZhiFang.IDAO.RBAC
{
    public interface IDHRDeptDao : ZhiFang.IDAO.Base.IDBaseDao<HRDept, long>
    {
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
        List<long> GetSubDeptIdListByDeptId(long id);
        /// <summary>
        /// 获取到某一部门的所有父级部门Id(ParentID=0结束)信息
        /// </summary>
        /// <param name="id">当前部门信息</param>
        /// <returns></returns>
        List<long> GetParentDeptIdListByDeptId(long id);
    }
}
