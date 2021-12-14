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
    public class RBACUserDao : BaseDaoNHB<RBACUser, long>, IDRBACUserDao
    {
        #region IDRBACUserDao 成员

        public IList<RBACUser> GetByHQL(RBACUser entity)
        {
            return base.HibernateTemplate.FindByNamedParam<RBACUser>("from RBACUser user where user.Id=:userID", "userID", entity.Id).ToList();
        }

        #endregion
        public IList<RBACUser> SearchRBACUserByUserAccount(string strUserAccount)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACUser", new List<ICriterion>() { Restrictions.Eq("Account", strUserAccount) });

            DaoNHBCriteriaAction<List<RBACUser>, RBACUser> action = new DaoNHBCriteriaAction<List<RBACUser>, RBACUser>(dic);

            List<RBACUser> l = base.HibernateTemplate.Execute<List<RBACUser>>(action);
            return l;
        }

    }
}