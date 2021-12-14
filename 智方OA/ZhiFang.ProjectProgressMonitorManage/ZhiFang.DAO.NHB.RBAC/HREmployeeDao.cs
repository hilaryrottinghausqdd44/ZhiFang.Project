using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;
using NHibernate;
using NHibernate.Criterion;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Common.Public;

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

        public IList<HREmployee> GetAllList()
        {
            List<HREmployee> emplist = new List<HREmployee>();
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsPlatform") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIPUrl") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsPlatform") == "0")//判断是否是集成平台
            {
                string platformrbacurl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIPUrl")+ "/ServerWCF/RBACService.svc";
                string param = "/RBAC_UDTO_GetHREmployeeAllList";
                string tmpjson = ProjectProgressMonitorManage.Common.RestfullHelper.InvkerRestServiceByGet(platformrbacurl, param);
                if (tmpjson != null && tmpjson.Trim().Length > 0)
                {
                    Entity.Base.BaseResultDataValue resultvalue = JsonSerializer.JsonDotNetDeserializeObject<ZhiFang.Entity.Base.BaseResultDataValue>(tmpjson);
                    if (resultvalue.success)
                    {
                        if (resultvalue.ResultDataValue != null && resultvalue.ResultDataValue.Trim() != "")
                        {
                            emplist = JsonSerializer.JsonDotNetDeserializeObject<List<HREmployee>>(resultvalue.ResultDataValue);

                            return emplist;
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetAllList;resultvalue.ResultDataValue返回为空！platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                            return null;
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetAllList;resultvalue.Success=" + resultvalue.success + ";resultvalue.ErrorInfo=" + resultvalue.ErrorInfo + ";platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                        return null;
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetAllList;tmpjson返回为空！platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                    return null;
                }
            }
            else
            {
                return  this.GetListByHQL(" 1=1 ").ToList();
            }
        }

        public IList<HREmployee> GetListByDeptIdList(List<long> deptidlist)
        {
            List<HREmployee> emplist = new List<HREmployee>();
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsPlatform") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIPUrl") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsPlatform") == "0")//判断是否是集成平台
            {
                string platformrbacurl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIPUrl");
                string param = "/RBAC_UDTO_GetHREmployeeListByDeptIdList?deptidlist=" + string.Join(",", deptidlist.ToArray()) ;
                string tmpjson = ProjectProgressMonitorManage.Common.RestfullHelper.InvkerRestServiceByGet(platformrbacurl, param);
                if (tmpjson != null && tmpjson.Trim().Length > 0)
                {
                    Entity.Base.BaseResultDataValue resultvalue = JsonSerializer.JsonDotNetDeserializeObject<ZhiFang.Entity.Base.BaseResultDataValue>(tmpjson);
                    if (resultvalue.success)
                    {
                        if (resultvalue.ResultDataValue != null && resultvalue.ResultDataValue.Trim() != "")
                        {
                            emplist = JsonSerializer.JsonDotNetDeserializeObject<List<HREmployee>>(resultvalue.ResultDataValue);

                            return emplist;
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetListByDeptIdList;resultvalue.ResultDataValue返回为空！platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                            return null;
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetListByDeptIdList;resultvalue.Success=" + resultvalue.success + ";resultvalue.ErrorInfo=" + resultvalue.ErrorInfo + ";platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                        return null;
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetListByDeptIdList;tmpjson返回为空！platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                    return null;
                }
            }
            else
            {
                return this.GetListByHQL(" HRDept.Id in (" + string.Join(",", deptidlist.ToArray()) + ")").ToList();
            }
        }


        public override HREmployee Get(long id)
        {
            HREmployee tmpemp = new HREmployee();
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsPlatform") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIPUrl") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsPlatform") == "0")//判断是否是集成平台
            {
                string platformrbacurl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIPUrl") + "/ServerWCF/RBACService.svc";
                string param = "/RBAC_UDTO_GetHREmployeeById?id=" + id;
                string tmpjson = ProjectProgressMonitorManage.Common.RestfullHelper.InvkerRestServiceByGet(platformrbacurl, param);
                if (tmpjson != null && tmpjson.Trim().Length > 0)
                {
                    Entity.Base.BaseResultDataValue resultvalue = JsonSerializer.JsonDotNetDeserializeObject<ZhiFang.Entity.Base.BaseResultDataValue>(tmpjson);
                    if (resultvalue.success)
                    {
                        if (resultvalue.ResultDataValue != null && resultvalue.ResultDataValue.Trim() != "")
                        {
                            tmpemp = JsonSerializer.JsonDotNetDeserializeObject<HREmployee>(resultvalue.ResultDataValue);

                            return tmpemp;
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".Get;resultvalue.ResultDataValue返回为空！platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                            return null;
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".Get;resultvalue.Success=" + resultvalue.success + ";resultvalue.ErrorInfo=" + resultvalue.ErrorInfo + ";platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                        return null;
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".Get;tmpjson返回为空！platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                    return null;
                }
            }
            else
            {
                return base.Get(id);
            }
        }

        #endregion
    }
}