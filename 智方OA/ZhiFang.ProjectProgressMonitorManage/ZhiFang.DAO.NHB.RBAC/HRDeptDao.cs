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
    public class HRDeptDao : Base.BaseDaoNHB<HRDept, long>, IDHRDeptDao
    {
        #region IDHRDeptDao 成员

        public IList<HRDept> SearchHRDeptByHREmpID(long longHREmpID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("HRDept", null);
            dic.Add("HRDeptEmpList", null);
            dic.Add("HREmployee", new List<ICriterion>() { Restrictions.Eq("Id", longHREmpID) });

            DaoNHBCriteriaAction<List<HRDept>, HRDept> action = new DaoNHBCriteriaAction<List<HRDept>, HRDept>(dic);

            List<HRDept> l = base.HibernateTemplate.Execute<List<HRDept>>(action);
            return l;
        }

        public IList<HRDept> SearchHRDeptByHRDeptIdentity(long longHRDeptIdentity)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("HRDept", null);
            dic.Add("HRDeptIdentityList", new List<ICriterion>() { Restrictions.Eq("Id", longHRDeptIdentity) });

            DaoNHBCriteriaAction<List<HRDept>, HRDept> action = new DaoNHBCriteriaAction<List<HRDept>, HRDept>(dic);

            List<HRDept> l = base.HibernateTemplate.Execute<List<HRDept>>(action);
            return l;
        }
        /// <summary>
        /// 获取到某一部门的所有子级部门Id信息
        /// </summary>
        /// <param name="id">当前部门信息</param>
        /// <returns></returns>
        public List<long> GetSubDeptIdListByDeptId(long id)
        {
            List<long> deptidlist = new List<long>();
            long count = base.GetListCountByHQL(" IsUse=true and ParentID= " + id);
            if (count > 0)
            {
                IList<HRDept> deptlist = base.GetListByHQL(" IsUse=true and ParentID= " + id);
                if (deptlist != null && deptlist.Count > 0)
                {
                    foreach (HRDept dept in deptlist)
                    {
                        deptidlist.Add(dept.Id);
                        List<long> tmpdeptidlist = GetSubDeptIdListByDeptId(dept.Id);
                        if (tmpdeptidlist.Count > 0)
                        {
                            deptidlist.AddRange(tmpdeptidlist);
                        }
                    }
                }
            }
            return deptidlist;
        }
        /// <summary>
        /// 获取到某一部门的所有父级部门Id(ParentID=0结束)信息
        /// </summary>
        /// <param name="id">当前部门信息</param>
        /// <returns></returns>
        public List<long> GetParentDeptIdListByDeptId(long id)
        {
            List<long> deptidlist = new List<long>();
            if (id == 0)
            {
                return deptidlist;
            }

            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsPlatform") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIPUrl") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsPlatform") == "0")//判断是否是集成平台
            {
                string platformrbacurl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIIPUrl") + "/ServerWCF/RBACService.svc";
                string param = "/RBAC_RJ_GetParentDeptIdListByDeptId?id=" + id;
                string tmpjson= ProjectProgressMonitorManage.Common.RestfullHelper.InvkerRestServiceByGet(platformrbacurl, param);
                if (tmpjson != null && tmpjson.Trim().Length > 0)
                {
                    Entity.Base.BaseResultDataValue resultvalue = JsonSerializer.JsonDotNetDeserializeObject<ZhiFang.Entity.Base.BaseResultDataValue>(tmpjson);
                    if (resultvalue.success)
                    {
                        if (resultvalue.ResultDataValue != null && resultvalue.ResultDataValue.Trim() != "")
                        {
                            long[] arrdeptid = JsonSerializer.JsonDotNetDeserializeObject<long[]>(resultvalue.ResultDataValue);
                            if (arrdeptid.Length <= 0)
                            {
                                ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetParentDeptIdListByDeptId;返回数组为空！resultvalue.ResultDataValue:" + resultvalue.ResultDataValue + ";platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                                return deptidlist;
                            }

                            return arrdeptid.ToList<long>();
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetParentDeptIdListByDeptId;resultvalue.ResultDataValue返回为空！platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                            return deptidlist;
                        }                    
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetParentDeptIdListByDeptId;resultvalue.Success="+ resultvalue.success + ";resultvalue.ErrorInfo=" + resultvalue.ErrorInfo + ";platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                        return deptidlist;
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetParentDeptIdListByDeptId;tmpjson返回为空！platformrbacurl='" + platformrbacurl + "';param='" + param + "'");
                    return deptidlist;
                }
            }
            else
            {                
              
                long count = base.GetListCountByHQL(" IsUse=true and Id= " + id);
                if (count > 0)
                {
                    IList<HRDept> deptlist = base.GetListByHQL(" IsUse=true and Id= " + id);
                    if (deptlist != null && deptlist.Count > 0)
                    {
                        foreach (HRDept dept in deptlist)
                        {
                            if (!deptidlist.Contains(dept.Id))
                            {
                                deptidlist.Add(dept.Id);
                                List<long> tmpdeptidlist = GetParentDeptIdListByDeptId(dept.ParentID);
                                if (tmpdeptidlist.Count > 0)
                                {
                                    deptidlist.AddRange(tmpdeptidlist);
                                }
                            }
                        }
                    }
                }
            }
            return deptidlist;
        }
        #endregion
    }
}
