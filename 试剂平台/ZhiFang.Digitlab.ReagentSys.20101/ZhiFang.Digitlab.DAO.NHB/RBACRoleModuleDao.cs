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
    public class RBACRoleModuleDao : BaseDaoNHB<RBACRoleModule, long>, IDRBACRoleModuleDao
    {
        //返回用户角色与模块关系表
        public IList<RBACRoleModule> GetRBACRoleModuleByRBACUserCodeAndModuleID(string strUserCode, long longModuleID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACRoleModule", null);
            dic.Add("RBACModule", new List<ICriterion>() { Restrictions.Eq("Id", longModuleID) });
            dic.Add("RBACRole", null);
            dic.Add("RBACEmpRoleList", null);
            dic.Add("HREmployee", null);
            dic.Add("RBACUserList", new List<ICriterion>() { Restrictions.Eq("UseCode", strUserCode) });

            DaoNHBCriteriaAction<List<RBACRoleModule>, RBACRoleModule> action = new DaoNHBCriteriaAction<List<RBACRoleModule>, RBACRoleModule>(dic);

            List<RBACRoleModule> l = base.HibernateTemplate.Execute<List<RBACRoleModule>>(action);
            return l;
        }

        public bool DeleteByRBACModuleId(long RBACModuleId)
        {
            this.DeleteByHql(" From RBACRoleModule rbacrolemodule where rbacrolemodule.RBACModule.Id=" + RBACModuleId);
            return true;
        }
    }
}