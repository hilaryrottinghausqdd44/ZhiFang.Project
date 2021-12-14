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
    public class RBACRoleModuleDao : BaseDaoNHBService<RBACRoleModule, long>, IDRBACRoleModuleDao
    {
        //返回用户角色与模块关系表
        public IList<RBACRoleModule> GetRBACRoleModuleByRBACUserCodeAndModuleID(string strUserCode, long longModuleID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("RBACRoleModule", null);
            dic.Add("RBACModule", new List<ICriterion>() { Restrictions.Eq("Id", longModuleID) });
            dic.Add("RBACRole", null);
            dic.Add("RBACEmpRoleList", null);
            dic.Add("PUser", null);//HREmployee
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
        public override int DeleteByHql(string hql)
        {
            if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID) != null)
            {
                ZhiFang.Common.Log.Log.Debug("角色模块删除动作(DeleteByHql)：操作人ID=" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID) + "@操作人姓名=" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName) + "@hql=" + hql);
            }
            return base.DeleteByHql(hql);
        }

        public override bool Delete(long id)
        {
            if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID) != null)
            {
                ZhiFang.Common.Log.Log.Debug("角色模块删除动作(Delete)：操作人ID=" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID) + "@操作人姓名=" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName) + "@id=" + id);
            }
            return base.Delete(id);
        }

        public override bool Delete(RBACRoleModule entity)
        {
            if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID) != null)
            {
                ZhiFang.Common.Log.Log.Debug("角色模块删除动作(Deleteentity)：操作人ID=" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID) + "@操作人姓名=" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName) + "@entityid=" + entity.Id);
            }
            return base.Delete(entity);
        }
    }
}