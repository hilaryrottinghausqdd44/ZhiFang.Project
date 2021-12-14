using NHibernate.Criterion;
using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.NHB.RBAC
{
    public class HREmployeeDao : Base.BaseDaoNHB<HREmployee, long>, IDHREmployeeDao
    {
        #region IDHREmployeeDao 成员

        public IList<HREmployee> SearchHREmployeeByRoleID(long longRoleID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("HREmployee", null);
            dic.Add("RBACEmpRoleList", null);
            dic.Add("RBACRole", new List<ICriterion>() { Restrictions.Eq("Id", longRoleID) });

            DaoNHBCriteriaAction<List<HREmployee>, HREmployee> action = new DaoNHBCriteriaAction<List<HREmployee>, HREmployee>(dic);

            List<HREmployee> l = base.HibernateTemplate.Execute<List<HREmployee>>(action);
            return l;
        }

        public IList<HREmployee> SearchHREmployeeByHRDeptID(long longHRDeptID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("HREmployee", null);
            dic.Add("HRDeptEmpList", null);
            dic.Add("HRDept", new List<ICriterion>() { Restrictions.Eq("Id", longHRDeptID) });

            DaoNHBCriteriaAction<List<HREmployee>, HREmployee> action = new DaoNHBCriteriaAction<List<HREmployee>, HREmployee>(dic);

            List<HREmployee> l = base.HibernateTemplate.Execute<List<HREmployee>>(action);
            return l;
        }

        public IList<HREmployee> SearchHREmployeeByHRPositionID(long longHRPositionID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("HREmployee", null);
            dic.Add("HRPosition", new List<ICriterion>() { Restrictions.Eq("Id", longHRPositionID) });

            DaoNHBCriteriaAction<List<HREmployee>, HREmployee> action = new DaoNHBCriteriaAction<List<HREmployee>, HREmployee>(dic);

            List<HREmployee> l = base.HibernateTemplate.Execute<List<HREmployee>>(action);
            return l;
        }

        public IList<HREmployee> SearchHREmployeeByHRDeptIdentityID(long longHRDeptIdentityID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("HREmployee", null);
            dic.Add("HRDept", null);
            dic.Add("HRDeptIdentityList", new List<ICriterion>() { Restrictions.Eq("Id", longHRDeptIdentityID) });

            DaoNHBCriteriaAction<List<HREmployee>, HREmployee> action = new DaoNHBCriteriaAction<List<HREmployee>, HREmployee>(dic);

            List<HREmployee> l = base.HibernateTemplate.Execute<List<HREmployee>>(action);
            return l;
        }

        public IList<HREmployee> SearchHREmployeeByHREmpIdentityID(long longHREmpIdentityID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("HREmployee", null);
            dic.Add("HREmpIdentityList", new List<ICriterion>() { Restrictions.Eq("Id", longHREmpIdentityID) });

            DaoNHBCriteriaAction<List<HREmployee>, HREmployee> action = new DaoNHBCriteriaAction<List<HREmployee>, HREmployee>(dic);

            List<HREmployee> l = base.HibernateTemplate.Execute<List<HREmployee>>(action);
            return l;
        }

        public IList<HREmployee> SearchHREmployeeByUserAccount(string strUserAccount)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("HREmployee", null);
            dic.Add("RBACUserList", new List<ICriterion>() { Restrictions.Eq("Account", strUserAccount) });

            DaoNHBCriteriaAction<List<HREmployee>, HREmployee> action = new DaoNHBCriteriaAction<List<HREmployee>, HREmployee>(dic);

            List<HREmployee> l = base.HibernateTemplate.Execute<List<HREmployee>>(action);
            return l;
        }

        public IList<HREmployee> SearchHREmployeeByUserCode(string strUserCode)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("HREmployee", null);
            dic.Add("RBACUserList", new List<ICriterion>() { Restrictions.Eq("UseCode", strUserCode) });

            DaoNHBCriteriaAction<List<HREmployee>, HREmployee> action = new DaoNHBCriteriaAction<List<HREmployee>, HREmployee>(dic);

            List<HREmployee> l = base.HibernateTemplate.Execute<List<HREmployee>>(action);
            return l;
        }

        public IList<RBACModule> SearchModuleByHREmpIDRole(long id, int page, int limit)
        {
            DaoNHBSearchByHqlAction<IList<RBACModule>, RBACModule> action = new DaoNHBSearchByHqlAction<IList<RBACModule>, RBACModule>(" select rbacm from RBACModule  rbacm join rbacm.RBACRoleModuleList  rbacrml join rbacrml.RBACRole  rbacr join rbacr.RBACEmpRoleList rbacerl join rbacerl.HREmployee hremp where hremp.Id = " + id + "  ", page, limit);
            var list = this.HibernateTemplate.Execute<IList<RBACModule>>(action);
            return list;
        }

        #endregion
    }
}