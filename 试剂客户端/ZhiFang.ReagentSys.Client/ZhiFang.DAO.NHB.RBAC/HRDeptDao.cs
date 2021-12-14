using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;
using NHibernate;
using NHibernate.Criterion;
using ZhiFang.DAO.NHB.Base;

namespace ZhiFang.DAO.NHB.RBAC
{
    public class HRDeptDao : Base.BaseDaoNHB<HRDept, long>, IDHRDeptDao
    {
        #region IDHRDeptDao 成员

        public IList<HRDept> SearchHRDeptByHREmpID(long longHREmpID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("HRDept", null);
            dic.Add("HRDeptEmpList", null);
            dic.Add("HREmployee", new List<ICriterion>() { Restrictions.Eq("Id", longHREmpID) });

            DaoNHBCriteriaAction<List<HRDept>, HRDept> action = new DaoNHBCriteriaAction<List<HRDept>, HRDept>(dic);

            List<HRDept> l = base.HibernateTemplate.Execute<List<HRDept>>(action);
            return l;
        }

        public IList<HRDept> SearchHRDeptByHRDeptIdentity(long longHRDeptIdentity)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("HRDept", null);
            dic.Add("HRDeptIdentityList", new List<ICriterion>() { Restrictions.Eq("Id", longHRDeptIdentity) });

            DaoNHBCriteriaAction<List<HRDept>, HRDept> action = new DaoNHBCriteriaAction<List<HRDept>, HRDept>(dic);

            List<HRDept> l = base.HibernateTemplate.Execute<List<HRDept>>(action);
            return l;
        }

        public List<long> GetSubDeptIdListByDeptId(long id)
        {
            List<long> deptidlist = new List<long>();
            long count = base.GetListCountByHQL(" IsUse=true and ParentID= " + id);
            if (count > 0)
            {
                IList<HRDept> deptlist = base.GetListByHQL(" IsUse=true and ParentID= " + id);
                if (deptlist != null && deptlist.Count > 0)
                {
                    foreach (HRDept dept in deptlist)
                    {
                        deptidlist.Add(dept.Id);
                        List<long> tmpdeptidlist = GetSubDeptIdListByDeptId(dept.Id);
                        if (tmpdeptidlist.Count > 0)
                        {
                            deptidlist.AddRange(tmpdeptidlist);
                        }
                    }
                }
            }
            return deptidlist;
        }
        /// <summary>
        /// 获取到某一部门的所有父级部门Id(ParentID=0结束)信息
        /// </summary>
        /// <param name="id">当前部门信息</param>
        /// <returns></returns>
        public List<long> GetParentDeptIdListByDeptId(long id)
        {
            List<long> deptidlist = new List<long>();
            if (id == 0) {
                return deptidlist;
            }
            long count = base.GetListCountByHQL(" IsUse=true and Id= " + id);
            if (count > 0)
            {
                IList<HRDept> deptlist = base.GetListByHQL(" IsUse=true and Id= " + id);
                if (deptlist != null && deptlist.Count > 0)
                {
                    foreach (HRDept dept in deptlist)
                    {
                        deptidlist.Add(dept.Id);
                        List<long> tmpdeptidlist = GetParentDeptIdListByDeptId(dept.ParentID);
                        if (tmpdeptidlist.Count > 0)
                        {
                            deptidlist.AddRange(tmpdeptidlist);
                        }
                    }
                }
            }
            return deptidlist;
        }

        public List<long> GetParentDeptIdListByDeptId(long id, bool hasLabId)
        {
            List<long> deptidlist = new List<long>();
            if (id == 0)
            {
                return deptidlist;
            }
            long count = base.GetListCountByHQL(" IsUse=true and Id= " + id, hasLabId);
            if (count > 0)
            {
                IList<HRDept> deptlist = base.GetListByHQL(" IsUse=true and Id= " + id, hasLabId);
                if (deptlist != null && deptlist.Count > 0)
                {
                    foreach (HRDept dept in deptlist)
                    {
                        deptidlist.Add(dept.Id);
                        List<long> tmpdeptidlist = GetParentDeptIdListByDeptId(dept.ParentID, hasLabId);
                        if (tmpdeptidlist.Count > 0)
                        {
                            deptidlist.AddRange(tmpdeptidlist);
                        }
                    }
                }
            }
            return deptidlist;
        }

        #endregion
    }
}
