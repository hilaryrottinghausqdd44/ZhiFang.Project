using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using AopAlliance.Intercept;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.Common.Public;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.ProjectProgressMonitorManage.Interceptor
{
    public class DataRowRoleHQLAdvice : IMethodInterceptor
    {
        ZhiFang.IBLL.RBAC.IBRBACModule IBRBACModule { get; set; }
        ZhiFang.IBLL.RBAC.IBRBACEmpRoles IBRBACEmpRoles { get; set; }
        ZhiFang.IBLL.RBAC.IBRBACRoleRight IBRBACRoleRight { get; set; }
        ZhiFang.IBLL.RBAC.IBRBACModuleOper IBRBACModuleOper { get; set; }
        ZhiFang.IBLL.RBAC.IBRBACRowFilter IBRBACRowFilter { get; set; }
        public object Invoke(IMethodInvocation invocation)
        {
            if (SYSDataRowRoleCacheBase.IsUseDataRowRoleFilter)
            {
                RefreshSYSDataRowRoleCache();
                SetCacheRowFilterHQL(invocation);
            }
            return invocation.Proceed();

            #region 模块访问权限判断 
            #endregion
        }

        #region 功能权限及缓存数据处理
        /// <summary>
        /// 模块服务的基本数据信息系统缓存处理
        /// </summary>
        public void RefreshSYSDataRowRoleCache()
        {
            RefreshSYSModuleOperCache();
            //RefreshSYSRowFilterCache();
        }
        /// <summary>
        /// 依员工所属的角色及模块服务ID获取员工角色权限的行数据条件信息
        /// </summary>
        /// <param name="empRolesIdStr"></param>
        /// <param name="moduleOperId"></param>
        /// <returns></returns>
        public void SetDataRowRoleHQLByCurEmpId(long empId, long moduleOperId)
        {
            Dictionary<string, string> dicHQL = new Dictionary<string, string>();
            IApplicationContext context = ContextRegistry.GetContext();
            IBRBACRoleRight = (ZhiFang.IBLL.RBAC.IBRBACRoleRight)context.GetObject("BRBACRoleRight");

            IList<RBACRoleRight> tempResultList = new List<RBACRoleRight>();
            IList<RBACRowFilter> tempRowFilterList = new List<RBACRowFilter>();
            IList<long> tempRowIdList = new List<long>();
            string strEmployeeName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            IList<RBACRoleRight> tempList = IBRBACRoleRight.RBAC_BA_GetRBACRoleRightByHREmpIDAndModuleOperID(empId, moduleOperId);
            //找出员工的所有角色不为空且不重复的行数据信息
            foreach (var item in tempList)
            {
                var rowFilter = item.RBACRowFilter;
                if (item.RBACRole != null && !tempRowIdList.Contains(rowFilter.Id))
                {
                    tempRowIdList.Add(rowFilter.Id);
                    tempResultList.Add(item);
                }
            }
            if (tempResultList.Count > 0)
            {
                //预置条件的行数据,按角色分组
                var tempGroupBy = tempResultList.Where(p => (p.RBACRowFilter != null && p.RBACRowFilter.IsPreconditions == true)).GroupBy(p => p.RBACRole);

                foreach (var roleRightList in tempGroupBy)
                {
                    //同一角色下的预置条件行数据条件
                    for (int i = 0; i < roleRightList.Count(); i++)
                    {
                        var hqlStr = roleRightList.ElementAt(i).RBACRowFilter.RowFilterCondition;
                        var entityCode = roleRightList.ElementAt(i).RBACRowFilter.EntityCode;
                        if (!string.IsNullOrEmpty(hqlStr)) hqlStr = hqlStr.Trim();
                        ZhiFang.Common.Log.Log.Debug("员工为:" + strEmployeeName + ",所属角色为:" + roleRightList.ElementAt(i).RBACRole.CName + ",EntityCode为:" + entityCode + ",预置条件的行数据条件HQL:" + hqlStr);
                        if (dicHQL.ContainsKey(entityCode) && !string.IsNullOrEmpty(hqlStr))
                        {
                            var hqlOldStr = dicHQL[entityCode];

                            if (hqlStr.TrimStart().IndexOf('(') == 0 && hqlStr.TrimEnd().LastIndexOf(')') == hqlStr.Length - 1)
                                hqlStr = hqlOldStr + " or " + hqlStr + " ";
                            else
                                hqlStr = hqlOldStr + " or (" + hqlStr + ")";
                            dicHQL[entityCode] = hqlStr;
                        }
                        else if (!dicHQL.ContainsKey(entityCode) && !string.IsNullOrEmpty(hqlStr))
                        {
                            if (hqlStr.TrimStart().IndexOf('(') == 0 && hqlStr.TrimEnd().LastIndexOf(')') == hqlStr.Length - 1)
                                hqlStr = " " + hqlStr + " ";
                            else
                                hqlStr = "(" + hqlStr + ")";
                            dicHQL.Add(entityCode, hqlStr);
                        }
                    }
                }
                try
                {
                    String[] keyStr = dicHQL.Keys.ToArray<String>();
                    for (int i = 0; i < keyStr.Count(); i++)
                    {
                        var hqlStr = dicHQL[keyStr[i]];
                        if (!string.IsNullOrEmpty(hqlStr))
                        {
                            if (hqlStr.TrimStart().IndexOf('(') == 0 && hqlStr.TrimEnd().LastIndexOf(')') == hqlStr.Length - 1)
                                hqlStr = hqlStr.Trim();
                            else
                                hqlStr = "(" + hqlStr + ")";
                            dicHQL[keyStr[i]] = hqlStr;
                        }
                        //ZhiFang.Common.Log.Log.Debug("EntityCode为:" + keyStr[i] + ",预置条件的行数据条件HQL:" + hqlStr);
                    }
                }
                catch (Exception ee)
                {
                    ZhiFang.Common.Log.Log.Debug("预置条件的行数据条件HQL处理错误:" + ee.Message);
                }

                //单表的行数据
                var tempGroupBy2 = tempResultList.Where(p => (p.RBACRowFilter != null && p.RBACRowFilter.RBACPreconditions == null)).GroupBy(p => p.RBACRole);
                foreach (var roleRightList in tempGroupBy2)
                {
                    //同一角色下的单表行数据条件
                    for (int i = 0; i < roleRightList.Count(); i++)
                    {
                        var hqlStr = roleRightList.ElementAt(i).RBACRowFilter.RowFilterCondition;
                        var entityCode = roleRightList.ElementAt(i).RBACRowFilter.EntityCode;
                        if (!string.IsNullOrEmpty(hqlStr)) hqlStr = hqlStr.Trim();
                        ZhiFang.Common.Log.Log.Debug("员工为:" + strEmployeeName + ",所属角色为:" + roleRightList.ElementAt(i).RBACRole.CName + ",EntityCode为:" + entityCode + ",单表的行数据条件HQL:" + hqlStr);
                        if (dicHQL.ContainsKey(entityCode) && !string.IsNullOrEmpty(hqlStr))
                        {
                            var hqlOldStr = dicHQL[entityCode];
                            if (hqlStr.TrimStart().IndexOf('(') == 0 && hqlStr.TrimEnd().LastIndexOf(')') == hqlStr.Length - 1)
                                hqlStr = hqlOldStr + " or " + hqlStr + " ";
                            else
                                hqlStr = hqlOldStr + " or (" + hqlStr + ")";
                            dicHQL[entityCode] = hqlStr;
                        }
                        else if (!dicHQL.ContainsKey(entityCode) && !string.IsNullOrEmpty(hqlStr))
                        {
                            if (hqlStr.TrimStart().IndexOf('(') == 0 && hqlStr.TrimEnd().LastIndexOf(')') == hqlStr.Length - 1)
                                hqlStr = " " + hqlStr + " ";
                            else
                                hqlStr = "(" + hqlStr + ")";
                            dicHQL.Add(entityCode, hqlStr);
                        }
                    }
                }
            }
            context = ContextRegistry.GetContext();
            object bslog = context.GetObject("DataRowRoleHQL");
            ((DataRowRoleHQL)bslog).Hql = "";
            ((DataRowRoleHQL)bslog).DicPreconditionsHQL = dicHQL;
        }
        /// <summary>
        /// 角色权限的行数据条件合并处理
        /// 处理当前模块所请求的模块服务所有的行数据条件并缓存
        /// </summary>
        /// <param name="invocation"></param>
        public void SetCacheRowFilterHQL(IMethodInvocation invocation)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            object drrh = context.GetObject("DataRowRoleHQL");
            ((DataRowRoleHQL)drrh).DicPreconditionsHQL = null;

            string strEmployeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string strCurModuleID = Cookie.CookieHelper.Read(DicCookieSession.CurModuleID);     //新请求的模块ID
            string strUserAccount = SessionHelper.GetSessionValue(DicCookieSession.UserAccount);
            string methodName = invocation.Method.Name;
            if (methodName.IndexOf("/") > 0)
                methodName = methodName.Split('/')[methodName.Split('/').Length - 1];
            //Common.Log.Log.Debug("methodName:" + methodName);
            Common.Log.Log.Debug("DataRowRoleHQLAdvice行权限拦截.调用模块服务方法名为:" + methodName + "的行数据条件合并处理开始");
            //当前调用的模块服务方法的模块服务系统缓存基本信息
            SYSCacheModuleOper cacheModuleOper = null;
            long moduleID = 0;
            long? moduleOperId = null;
            bool isExec = true;
            if (!string.IsNullOrEmpty(strUserAccount) && !string.IsNullOrEmpty(strEmployeeID) && strUserAccount != DicCookieSession.SuperUser)
            {
                isExec = true;
            }
            else
            {
                isExec = false;
                Common.Log.Log.Debug("UserAccount为:" + strUserAccount + "," + "EmployeeID为:" + strEmployeeID);
            }
            if (isExec == true)
            {
                isExec = long.TryParse(strCurModuleID, out moduleID);
                if (isExec == false)
                    Common.Log.Log.Error("模块ID为:" + strCurModuleID + ",转换出现异常!");
                cacheModuleOper = GetSYSCacheModuleOperByModuleIdAndUrl(moduleID, methodName);
            }
            if (isExec == true && cacheModuleOper != null)
            {
                moduleOperId = cacheModuleOper.ModuleOperId;
                isExec = true;
            }
            else if (isExec == true && cacheModuleOper == null)
            {
                isExec = false;
                Common.Log.Log.Debug("模块ID为:" + strCurModuleID + ",模块服务方法为:" + methodName + "不存在系统缓存中!");
            }
            if (isExec == true)
            {
                //找出该员工在该模块服务中的所具有的全部角色权限的行数据条件
                SetDataRowRoleHQLByCurEmpId(long.Parse(strEmployeeID), moduleOperId.Value);
            }
        }

        /// <summary>
        /// 刷新模块服务缓存,并返回模块服务缓存数据
        /// </summary>
        public IList<SYSCacheModuleOper> RefreshSYSModuleOperCache()
        {
            IList<SYSCacheModuleOper> list = new List<SYSCacheModuleOper>();
            if (SYSDataRowRoleCacheBase.IsRefreshModuleOperCache == true)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBRBACModuleOper = (ZhiFang.IBLL.RBAC.IBRBACModuleOper)context.GetObject("BRBACModuleOper");
                list = IBRBACModuleOper.GetModuleOperCacheList();
                if (BParameterCache.ApplicationCache.Application.AllKeys.Contains(SYSDataRowRoleCacheBase.ModuleOperCacheKey))
                {
                    BParameterCache.ApplicationCache.Application.Set(SYSDataRowRoleCacheBase.ModuleOperCacheKey, list);
                }
                else
                {
                    BParameterCache.ApplicationCache.Application.Add(SYSDataRowRoleCacheBase.ModuleOperCacheKey, list);
                }
            }
            else
            {
                if (BParameterCache.ApplicationCache != null)
                {
                    list = (List<SYSCacheModuleOper>)BParameterCache.ApplicationCache.Application.Get(SYSDataRowRoleCacheBase.ModuleOperCacheKey);
                }
            }
            return list;
        }
        /// <summary>
        /// 刷新行数据条件的系统缓存,并返回行数据条件的系统缓存数据(暂时不用)
        /// </summary>
        public IList<SYSCacheRowFilter> RefreshSYSRowFilterCache()
        {
            IList<SYSCacheRowFilter> list = new List<SYSCacheRowFilter>();
            if (SYSDataRowRoleCacheBase.IsRefreshRowFilterCache == true)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBRBACRowFilter = (ZhiFang.IBLL.RBAC.IBRBACRowFilter)context.GetObject("BRBACRowFilter");
                list = IBRBACRowFilter.GetRowFilterCacheList();
                if (BParameterCache.ApplicationCache.Application.AllKeys.Contains(SYSDataRowRoleCacheBase.SYSCacheRowFilterKey))
                {
                    BParameterCache.ApplicationCache.Application.Set(SYSDataRowRoleCacheBase.SYSCacheRowFilterKey, list);
                }
                else
                {
                    BParameterCache.ApplicationCache.Application.Add(SYSDataRowRoleCacheBase.SYSCacheRowFilterKey, list);
                }
            }
            else
            {
                if (BParameterCache.ApplicationCache != null)
                {
                    list = (List<SYSCacheRowFilter>)BParameterCache.ApplicationCache.Application.Get(SYSDataRowRoleCacheBase.SYSCacheRowFilterKey);
                }
            }
            return list;
        }
        /// <summary>
        /// 依模块Id及模块服务名获取模块服务基本信息
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public SYSCacheModuleOper GetSYSCacheModuleOperByModuleIdAndUrl(long moduleId, string url)
        {
            SYSCacheModuleOper cacheModuleOper = null;
            IList<SYSCacheModuleOper> list = RefreshSYSModuleOperCache();
            if (moduleId > 0 && !String.IsNullOrEmpty(url))
            {
                var tempList = new List<SYSCacheModuleOper>();
                if (list != null && list.Count() > 0)
                {
                    tempList = list.Where(s => s.ModuleId == moduleId && s.ServiceURLEName == @"" + url + "").ToList();
                }
                if (tempList != null && tempList.Count() > 0)
                {
                    cacheModuleOper = tempList[0];
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("模块ID为:" + moduleId + ",模块服务URL为:" + url + "不存在系统缓存中!");
                }
            }
            return cacheModuleOper;
        }
        #endregion
    }
}
