using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.NHB.RBAC
{
    public class RBACRoleRightDao : Base.BaseDaoNHBService<RBACRoleRight, long>, IDRBACRoleRightDao
    {
        public IList<RBACRoleRight> GetRBACRoleRightByRBACUserCodeAndModuleOperID(string strUserCode, long longModuleOperID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACRoleRight", new List<ICriterion>() { Restrictions.Eq("Id", longModuleOperID) });
            dic.Add("RBACModuleOper", null);
            dic.Add("RBACRole", null);
            dic.Add("RBACEmpRoleList", null);
            dic.Add("HREmployee", null);
            dic.Add("RBACUserList", new List<ICriterion>() { Restrictions.Eq("UseCode", strUserCode) });

            DaoNHBCriteriaAction<List<RBACRoleRight>, RBACRoleRight> action = new DaoNHBCriteriaAction<List<RBACRoleRight>, RBACRoleRight>(dic);

            List<RBACRoleRight> l = base.HibernateTemplate.Execute<List<RBACRoleRight>>(action);
            return l;
        }

        #region IDRBACRoleRightDao 成员


        public IList<RBACRoleRight> SearchRBACRoleRightByRoleIDAndModuleOperID(long longRoleID, long longModuleOperID)
        {
            string strHQL = "from RBACRoleRight rbacroleright where rbacroleright.RBACRole.Id=:RoleID and rbacroleright.RBACModuleOper.Id=:ModuleOperID";
            return base.HibernateTemplate.FindByNamedParam<RBACRoleRight>(strHQL, new string[] { "RoleID", "ModuleOperID" }, new object[] { longRoleID, longModuleOperID }).ToList();
        }

        #endregion
    }
}