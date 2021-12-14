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
    public class RBACRoleDao : BaseDaoNHBService<RBACRole, long>, IDRBACRoleDao
    {
        #region IDRBACRoleDao 成员

        public IList<RBACRole> SearchRoleByHREmpID(long longHREmpID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACRole", null);
            dic.Add("RBACEmpRoleList", null);
            dic.Add("PUser", new List<ICriterion>() { Restrictions.Eq("Id", longHREmpID) });//HREmployee
            DaoNHBCriteriaAction<List<RBACRole>, RBACRole> action = new DaoNHBCriteriaAction<List<RBACRole>, RBACRole>(dic);
            List<RBACRole> l = base.HibernateTemplate.Execute<List<RBACRole>>(action);
            return l;
        }

        public IList<RBACRole> SearchRoleByUserCode(string strUserCode)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACRole", null);
            dic.Add("RBACEmpRoleList", null);
            dic.Add("HREmployee", null);
            dic.Add("RBACUserList", new List<ICriterion>() { Restrictions.Eq("UseCode", strUserCode) });

            DaoNHBCriteriaAction<List<RBACRole>, RBACRole> action = new DaoNHBCriteriaAction<List<RBACRole>, RBACRole>(dic);

            List<RBACRole> l = base.HibernateTemplate.Execute<List<RBACRole>>(action);
            return l;
        }

        public IList<RBACRole> SearchRoleByModuleID(long longModuleID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACRole", null);
            dic.Add("RBACRoleModuleList", null);
            dic.Add("RBACModule", new List<ICriterion>() { Restrictions.Eq("Id", longModuleID) });

            DaoNHBCriteriaAction<List<RBACRole>, RBACRole> action = new DaoNHBCriteriaAction<List<RBACRole>, RBACRole>(dic);

            List<RBACRole> l = base.HibernateTemplate.Execute<List<RBACRole>>(action);
            return l;
        }

        public IList<RBACRole> SearchRoleByModuleOperID(long longModuleOperID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACRole", null);
            dic.Add("RBACRoleRightList", null);
            dic.Add("RBACModuleOper", new List<ICriterion>() { Restrictions.Eq("Id", longModuleOperID) });

            DaoNHBCriteriaAction<List<RBACRole>, RBACRole> action = new DaoNHBCriteriaAction<List<RBACRole>, RBACRole>(dic);

            List<RBACRole> l = base.HibernateTemplate.Execute<List<RBACRole>>(action);
            return l;
        }

        #endregion
    }
}