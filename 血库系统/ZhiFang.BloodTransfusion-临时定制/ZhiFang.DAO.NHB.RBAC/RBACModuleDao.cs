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
    public class RBACModuleDao : Base.BaseDaoNHBService<RBACModule, long>, IDRBACModuleDao
    {
        #region IDRBACModuleDao 成员

        public IList<RBACModule> SearchModuleByModuleOperID(long longModuleOperID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACModule", null);
            dic.Add("RBACModuleOperList", new List<ICriterion>() { Restrictions.Eq("Id", longModuleOperID) });

            DaoNHBCriteriaAction<List<RBACModule>, RBACModule> action = new DaoNHBCriteriaAction<List<RBACModule>, RBACModule>(dic);

            List<RBACModule> l = base.HibernateTemplate.Execute<List<RBACModule>>(action);
            return l;
        }

        public IList<RBACModule> SearchModuleByRoleID(long longRoleID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACModule", null);
            dic.Add("RBACRoleModuleList", null);
            dic.Add("RBACRole", new List<ICriterion>() { Restrictions.Eq("Id", longRoleID) });

            DaoNHBCriteriaAction<List<RBACModule>, RBACModule> action = new DaoNHBCriteriaAction<List<RBACModule>, RBACModule>(dic);

            List<RBACModule> l = base.HibernateTemplate.Execute<List<RBACModule>>(action);
            return l;
        }

        public IList<RBACModule> SearchModuleByHREmpID(long longHREmpID)
        {
            var pl = this.HibernateTemplate.Find<RBACModule>(" select distinct rbacm from RBACModule  rbacm join rbacm.RBACRoleModuleList  rrm join rrm.RBACRole  rr join rr.RBACEmpRoleList rer join rer.PUser hre where hre.Id = " + longHREmpID + " and rbacm.IsUse=true order by  rbacm.DispOrder,rbacm.DataAddTime ");
            return pl;
        }

        public IList<RBACModule> SearchModuleByUserCode(string strUserCode)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACModule", null);
            dic.Add("RBACRoleModuleList", null);
            dic.Add("RBACRole", null);
            dic.Add("RBACEmpRoleList", null);
            dic.Add("PUser", null);//HREmployee
            dic.Add("RBACUserList", new List<ICriterion>() { Restrictions.Eq("UseCode", strUserCode) });

            DaoNHBCriteriaAction<List<RBACModule>, RBACModule> action = new DaoNHBCriteriaAction<List<RBACModule>, RBACModule>(dic);

            List<RBACModule> l = base.HibernateTemplate.Execute<List<RBACModule>>(action);
            return l;
        }

        public IList<RBACModule> SearchModuleByHREmpIDAndModuleID(long longHREmpID, long longModuleID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACModule", new List<ICriterion>() { Restrictions.Eq("Id", longModuleID) });
            dic.Add("RBACRoleModuleList", null);
            dic.Add("RBACRole", null);
            dic.Add("RBACEmpRoleList", null);
            dic.Add("PUser", new List<ICriterion>() { Restrictions.Eq("Id", longHREmpID) });//HREmployee

            DaoNHBCriteriaAction<List<RBACModule>, RBACModule> action = new DaoNHBCriteriaAction<List<RBACModule>, RBACModule>(dic);

            List<RBACModule> l = base.HibernateTemplate.Execute<List<RBACModule>>(action);
            return l;
        }

        public RBACModule SearchModuleByUseCode(string UseCode)
        {
            IList<RBACModule> l = base.HibernateTemplate.Find<RBACModule>(" from RBACModule rbacmodule where rbacmodule.UseCode='" + UseCode + "' and IsUse=true ");
            if (l != null&& l.Count>0)
            {
                return l[0];
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}