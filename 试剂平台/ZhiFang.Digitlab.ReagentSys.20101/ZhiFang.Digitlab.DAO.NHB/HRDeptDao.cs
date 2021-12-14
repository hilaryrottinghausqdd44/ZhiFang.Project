using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using NHibernate;
using NHibernate.Criterion;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class HRDeptDao : BaseDaoNHB<HRDept, long>, IDHRDeptDao
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

        #endregion
    }
}
