using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.NHB.RBAC
{
    public class RBACUserDao : Base.BaseDaoNHB<RBACUser, long>, IDRBACUserDao
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

            List<RBACUser> listRBACUser = base.HibernateTemplate.Execute<List<RBACUser>>(action);
            return listRBACUser;
        }

    }
}