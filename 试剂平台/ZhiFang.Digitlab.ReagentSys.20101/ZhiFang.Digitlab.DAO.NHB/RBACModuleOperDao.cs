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
    public class RBACModuleOperDao : BaseDaoNHB<RBACModuleOper, long>, IDRBACModuleOperDao
    {
        #region IDRBACModuleOperDao 成员

        public IList<RBACModuleOper> SearchModuleOperIDByModuleID(long longModuleID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACModuleOper", null);
            dic.Add("RBACModule", new List<ICriterion>() { Restrictions.Eq("Id", longModuleID) });

            DaoNHBCriteriaAction<List<RBACModuleOper>, RBACModuleOper> action = new DaoNHBCriteriaAction<List<RBACModuleOper>, RBACModuleOper>(dic);

            List<RBACModuleOper> l = base.HibernateTemplate.Execute<List<RBACModuleOper>>(action);
            return l;
        }

        public IList<RBACModuleOper> SearchModuleOperByRoleID(long longRoleID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACModuleOper", null);
            dic.Add("RBACRoleRightList", null);
            dic.Add("RBACRole", new List<ICriterion>() { Restrictions.Eq("Id", longRoleID) });

            DaoNHBCriteriaAction<List<RBACModuleOper>, RBACModuleOper> action = new DaoNHBCriteriaAction<List<RBACModuleOper>, RBACModuleOper>(dic);

            List<RBACModuleOper> l = base.HibernateTemplate.Execute<List<RBACModuleOper>>(action);
            return l;
        }

        public IList<RBACModuleOper> SearchModuleOperByHREmpID(long longHREmpID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACModuleOper", null);
            dic.Add("RBACRoleRightList", null);
            dic.Add("RBACRole", null);
            dic.Add("RBACEmpRoleList", null);
            dic.Add("HREmployee", new List<ICriterion>() { Restrictions.Eq("Id", longHREmpID) });

            DaoNHBCriteriaAction<List<RBACModuleOper>, RBACModuleOper> action = new DaoNHBCriteriaAction<List<RBACModuleOper>, RBACModuleOper>(dic);

            List<RBACModuleOper> l = base.HibernateTemplate.Execute<List<RBACModuleOper>>(action);
            return l;
        }

        public IList<RBACModuleOper> SearchModuleOperByUserCode(string strUserCode)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACModuleOper", null);
            dic.Add("RBACRoleRightList", null);
            dic.Add("RBACRole", null);
            dic.Add("RBACEmpRoleList", null);
            dic.Add("HREmployee", null);
            dic.Add("RBACUserList", new List<ICriterion>() { Restrictions.Eq("UseCode", strUserCode) });

            DaoNHBCriteriaAction<List<RBACModuleOper>, RBACModuleOper> action = new DaoNHBCriteriaAction<List<RBACModuleOper>, RBACModuleOper>(dic);

            List<RBACModuleOper> l = base.HibernateTemplate.Execute<List<RBACModuleOper>>(action);
            return l;
        }

        #endregion
    }
}