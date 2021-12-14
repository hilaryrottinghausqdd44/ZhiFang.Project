using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Common.Public;
using NHibernate;
using NHibernate.Criterion;
using ZhiFang.DAO.NHB.Base;

namespace ZhiFang.DAO.NHB.RBAC
{
    public class RBACRoleDao : Base.BaseDaoNHBService<RBACRole, long>, IDRBACRoleDao
    {
        #region IDRBACRoleDao 成员

        public IList<RBACRole> SearchRoleByHREmpID(long longHREmpID)
        {
            List<RBACRole> rbacrolelist = new List<RBACRole>();
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsPlatform") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIPUrl") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsPlatform") == "0")//判断是否是集成平台
            {
                string platformrbacurl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIPUrl") + "/ServerWCF/RBACService.svc";
                string param = "/RBAC_UDTO_GetRBACRolesListByEmpId?id="+ longHREmpID;
                string tmpjson = ProjectProgressMonitorManage.Common.RestfullHelper.InvkerRestServiceByGet(platformrbacurl, param);
                if (tmpjson != null && tmpjson.Trim().Length > 0)
                {
                    Entity.Base.BaseResultDataValue resultvalue = JsonSerializer.JsonDotNetDeserializeObject<ZhiFang.Entity.Base.BaseResultDataValue>(tmpjson);
                    if (resultvalue.success)
                    {
                        if (resultvalue.ResultDataValue != null && resultvalue.ResultDataValue.Trim() != "")
                        {
                            rbacrolelist = JsonSerializer.JsonDotNetDeserializeObject<List<RBACRole>>(resultvalue.ResultDataValue);

                            return rbacrolelist;
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".SearchRoleByHREmpID;resultvalue.ResultDataValue返回为空！platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                            return null;
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".SearchRoleByHREmpID;resultvalue.Success=" + resultvalue.success + ";resultvalue.ErrorInfo=" + resultvalue.ErrorInfo + ";platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                        return null;
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".SearchRoleByHREmpID;tmpjson返回为空！platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                    return null;
                }
            }
            else
            {
                Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
                dic.Add("RBACRole", null);
                dic.Add("RBACEmpRoleList", null);
                dic.Add("HREmployee", new List<ICriterion>() { Restrictions.Eq("Id", longHREmpID) });
                DaoNHBCriteriaAction<List<RBACRole>, RBACRole> action = new DaoNHBCriteriaAction<List<RBACRole>, RBACRole>(dic);
                rbacrolelist = base.HibernateTemplate.Execute<List<RBACRole>>(action);               
            }
            return rbacrolelist;
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