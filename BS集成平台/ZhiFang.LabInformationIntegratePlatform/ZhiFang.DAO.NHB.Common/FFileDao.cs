using System;
using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.Common;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.Common;
using ZhiFang.IDAO.LIIP;
using ZhiFang.IDAO.RBAC;
using DicCookieSession = ZhiFang.Entity.RBAC.DicCookieSession;

namespace ZhiFang.DAO.NHB.Common
{
    public class FFileDao : BaseDaoNHB<FFile, long>, IDFFileDao
    {
        public IDHRDeptDao IDHRDeptDao { get; set; }
        public IDRBACRoleDao IDRBACRoleDao { get; set; }
        public IDHREmployeeDao IDHREmployeeDao { get; set; }
        IDBHospitalEmpLinkDao IDBHospitalEmpLinkDao { get; set; }
        IDBHospitalAreaDao IDBHospitalAreaDao { get; set; }
        IDBHospitalDao IDBHospitalDao { get; set; }
        /// <summary>
        /// 更新文档的总阅读次数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool UpdateFFileCountsById(long id)
        {
            bool result = true;
            if (!String.IsNullOrEmpty(id.ToString()))
            {
                string hql = "update FFile ffile set ffile.Counts=(ffile.Counts+1) where ffile.Id=" + id;
                int counts = this.UpdateByHql(hql);
                if (counts > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;

        }

        /// <summary>
        /// 删除/作废文档信息
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="isUse"></param>
        /// <returns></returns>
        public bool UpdateFFileIsUseByIds(string strIds, bool isUse)
        {
            bool result = true;
            if (!String.IsNullOrEmpty(strIds))
            {
                string hql = "update FFile ffile set ffile.IsUse=" + (isUse == true ? 1 : 0) + " where ffile.Id in (" + strIds + ")";// + ",ffile.Status=" + (int)FFileStatus.作废
                int counts = this.UpdateByHql(hql);
                if (counts > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 置顶/撤消置顶文档信息
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="IsTop"></param>
        /// <returns></returns>
        public bool UpdateFFileIsTopByIds(string strIds, bool IsTop)
        {
            bool result = true;
            if (!String.IsNullOrEmpty(strIds))
            {
                string hql = "update FFile ffile set ffile.IsTop=" + (IsTop == true ? 1 : 0) + " where ffile.Id in (" + strIds + ")";
                int counts = this.UpdateByHql(hql);
                if (counts > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 获取登录者的抄送文档信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<FFile> SearchFFileCopyUserListByHQLAndEmployeeIDCookie(string strHqlWhere, int page, int count)
        {
            return SearchFFileCopyUserListByHQLAndEmployeeIDCookie(strHqlWhere, "", page, count);
            #region
            //EntityList<FFile> list = new EntityList<FFile>();
            //string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            //string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            ////如果没有登录,返回空数据
            //if (String.IsNullOrEmpty(employeeID) && String.IsNullOrEmpty(employeeName))
            //{
            //    return list;
            //}
            //string strHQL = "from FFile ffile left join ffile.FFileCopyUserList ffilecopyuser ";
            //StringBuilder strlistHQL = new StringBuilder();
            //string hqlTemp = GetHqlWhereByEmployeeID("ffilecopyuser", employeeID, employeeName);

            //if (String.IsNullOrEmpty(hqlTemp))
            //{
            //    hqlTemp = strHqlWhere;
            //}
            //else if (!String.IsNullOrEmpty(strHqlWhere) && !String.IsNullOrEmpty(hqlTemp))
            //{
            //    hqlTemp = hqlTemp + " and " + strHqlWhere;
            //}
            //string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysDicCookieSession.LabID);
            //if (labid != null && labid.Trim() != "")
            //{
            //    if (!String.IsNullOrEmpty(hqlTemp))
            //    {
            //        hqlTemp = hqlTemp + " and (ffile.LabID= " + labid + ")";
            //    }
            //    else
            //    {
            //        hqlTemp = "(ffile.LabID= " + labid + ")";
            //    }
            //}
            ////获取抄送文档信息
            //strlistHQL.Append("select distinct ffile " + strHQL);
            //if (!string.IsNullOrEmpty(hqlTemp))
            //{
            //    strlistHQL.Append(" where " + hqlTemp);
            //}
            ////获取抄送文档总数
            //StringBuilder strCountHQL = new StringBuilder();
            //strCountHQL.Append("select count(distinct ffile.Id) " + strHQL);
            //if (!string.IsNullOrEmpty(hqlTemp))
            //{
            //    strCountHQL.Append(" where " + hqlTemp);
            //}

            //DaoNHBSearchByHqlAction<List<FFile>, FFile> action = new DaoNHBSearchByHqlAction<List<FFile>, FFile>(strlistHQL.ToString(), page, count);
            //DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strCountHQL.ToString());

            //list.list = this.HibernateTemplate.Execute<List<FFile>>(action);
            //list.count = this.HibernateTemplate.Execute<int>(actionCount);
            //return list;
            #endregion
        }
        /// <summary>
        /// 获取登录者的抄送文档信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<FFile> SearchFFileCopyUserListByHQLAndEmployeeIDCookie(string strHqlWhere, string order, int page, int count)
        {
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            return SearchFFileCopyUserListByHQLAndEmployeeID(strHqlWhere, order, page, count, employeeID, employeeName);            
        }
        public EntityList<FFile> SearchFFileCopyUserListByHQLAndEmployeeID(string strHqlWhere, string order, int page, int count,string employeeID,string employeeName)
        {
            EntityList<FFile> list = new EntityList<FFile>();
            //如果没有登录,返回空数据
            if (String.IsNullOrEmpty(employeeID) && String.IsNullOrEmpty(employeeName))
            {
                return list;
            }
            string strHQL = "from FFile ffile left join ffile.FFileCopyUserList ffilecopyuser ";
            string hqlTemp = GetHqlWhereByEmployeeID("ffilecopyuser", employeeID, employeeName);
            StringBuilder strlistHQL = new StringBuilder();
            if (String.IsNullOrEmpty(hqlTemp))
            {
                hqlTemp = strHqlWhere;
            }
            else if (!String.IsNullOrEmpty(strHqlWhere) && !String.IsNullOrEmpty(hqlTemp))
            {
                hqlTemp = hqlTemp + " and " + strHqlWhere;
            }
            string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
            if (labid != null && labid.Trim() != "")
            {
                if (!String.IsNullOrEmpty(hqlTemp))
                {
                    hqlTemp = hqlTemp + " and (ffile.LabID= " + labid + ")";
                }
                else
                {
                    hqlTemp = "(ffile.LabID= " + labid + ")";
                }
            }
            //获取抄送文档信息
            strlistHQL.Append("select distinct ffile " + strHQL);
            if (!string.IsNullOrEmpty(hqlTemp))
            {
                strlistHQL.Append(" where " + hqlTemp);
            }
            if (order != null && order.Trim().Length > 0)
            {
                strlistHQL.Append(" order by " + order);
            }
            else
            {
                strlistHQL.Append(" order by ffile.IsTop DESC,ffile.PublisherDateTime DESC,ffile.BDictTree.Id ASC,ffile.Title ASC ");
            }

            //获取抄送文档信息总数
            StringBuilder strCountHQL = new StringBuilder();
            strCountHQL.Append("select count(distinct ffile.Id) " + strHQL);
            if (!string.IsNullOrEmpty(hqlTemp))
            {
                strCountHQL.Append(" where " + hqlTemp);
            }

            DaoNHBSearchByHqlAction<List<FFile>, FFile> action = new DaoNHBSearchByHqlAction<List<FFile>, FFile>(strlistHQL.ToString(), page, count);
            DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strCountHQL.ToString());

            list.list = this.HibernateTemplate.Execute<List<FFile>>(action);
            list.count = this.HibernateTemplate.Execute<int>(actionCount);
            return list;
        }
        /// <summary>
        /// 获取登录者的阅读文档信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeIDCookie(string strHqlWhere, int page, int count)
        {
            return SearchFFileReadingUserListByHQLAndEmployeeIDCookie(strHqlWhere, null, page, count);
            #region
            //EntityList<FFile> list = new EntityList<FFile>();
            //string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            //string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            ////如果没有登录,返回空数据
            //if (String.IsNullOrEmpty(employeeID) && String.IsNullOrEmpty(employeeName))
            //{
            //    return list;
            //}
            //string strHQL = "from FFile ffile left join ffile.FFileReadingUserList ffilereadinguser ";
            //StringBuilder strlistHQL = new StringBuilder();
            //string hqlTemp = GetHqlWhereByEmployeeID("ffilereadinguser");
            //if (String.IsNullOrEmpty(hqlTemp))
            //{
            //    hqlTemp = strHqlWhere;
            //}
            //else if (!String.IsNullOrEmpty(strHqlWhere) && !String.IsNullOrEmpty(hqlTemp))
            //{
            //    hqlTemp = hqlTemp + " and " + strHqlWhere;
            //}
            //string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysDicCookieSession.LabID);
            //if (labid != null && labid.Trim() != "")
            //{
            //    if (!String.IsNullOrEmpty(hqlTemp)) {
            //        hqlTemp = hqlTemp + " and (ffile.LabID= " + labid+")";
            //    }
            //    else
            //    {
            //        hqlTemp ="(ffile.LabID= " + labid + ")";
            //    }
            //}

            ////获取阅读文档信息
            //strlistHQL.Append("select distinct ffile " + strHQL);
            //if (!string.IsNullOrEmpty(hqlTemp))
            //{
            //    strlistHQL.Append(" where " + hqlTemp);
            //}
            ////获取阅读文档总数
            //StringBuilder strCountHQL = new StringBuilder();
            //strCountHQL.Append("select count(distinct ffile.Id) " + strHQL);
            //if (!string.IsNullOrEmpty(hqlTemp))
            //{
            //    strCountHQL.Append(" where " + hqlTemp);
            //}

            //DaoNHBSearchByHqlAction<List<FFile>, FFile> action = new DaoNHBSearchByHqlAction<List<FFile>, FFile>(strlistHQL.ToString(), page, count);
            //DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strCountHQL.ToString());

            //list.list = this.HibernateTemplate.Execute<List<FFile>>(action);
            //list.count = this.HibernateTemplate.Execute<int>(actionCount);
            //return list;
            #endregion
        }
        /// <summary>
        /// 获取登录者的阅读文档信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeIDCookie(string strHqlWhere, string order, int page, int count)
        {  
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            return SearchFFileReadingUserListByHQLAndEmployeeID(strHqlWhere, order, page, count, employeeID, employeeName); 
        }

        public EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeID(string strHqlWhere, string order, int page, int count,string employeeID,string employeeName)
        {
            EntityList<FFile> list = new EntityList<FFile>();
            //如果没有登录,返回空数据
            if (String.IsNullOrEmpty(employeeID) && String.IsNullOrEmpty(employeeName))
            {
                return list;
            }
            string strHQL = "from FFile ffile left join ffile.FFileReadingUserList ffilereadinguser ";
            string hqlTemp = GetHqlWhereByEmployeeID("ffilereadinguser", employeeID, employeeName);
            StringBuilder strlistHQL = new StringBuilder();
            if (String.IsNullOrEmpty(hqlTemp))
            {
                hqlTemp = strHqlWhere;
            }
            else if (!String.IsNullOrEmpty(strHqlWhere) && !String.IsNullOrEmpty(hqlTemp))
            {
                hqlTemp = hqlTemp + " and " + strHqlWhere;
            }

            string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
            if (labid != null && labid.Trim() != "")
            {
                if (!String.IsNullOrEmpty(hqlTemp))
                {
                    hqlTemp = hqlTemp + " and (ffile.LabID= " + labid + ")";
                }
                else
                {
                    hqlTemp = "(ffile.LabID= " + labid + ")";
                }
            }
            
            //获取阅读文档信息
            strlistHQL.Append("select distinct ffile " + strHQL);
            if (!string.IsNullOrEmpty(hqlTemp))
            {
                strlistHQL.Append(" where " + hqlTemp);
            }

            if (order != null && order.Trim().Length > 0)
            {
                strlistHQL.Append(" order by " + order);
            }
            else
            {
                strlistHQL.Append(" order by ffile.IsTop DESC,ffile.PublisherDateTime DESC,ffile.BDictTree.Id ASC,ffile.Title ASC ");
            }

            //获取阅读文档信息总数
            StringBuilder strCountHQL = new StringBuilder();
            strCountHQL.Append("select count(distinct ffile.Id) " + strHQL);
            if (!string.IsNullOrEmpty(hqlTemp))
            {
                strCountHQL.Append(" where " + hqlTemp);
            }
            //ZhiFang.Common.Log.Log.Debug("SearchFFileReadingUserListByHQLAndEmployeeID.strlistHQL:"+ strlistHQL.ToString());
            DaoNHBSearchByHqlAction<List<FFile>, FFile> action = new DaoNHBSearchByHqlAction<List<FFile>, FFile>(strlistHQL.ToString(), page, count);
            DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strCountHQL.ToString());

            list.list = this.HibernateTemplate.Execute<List<FFile>>(action);
            list.count = this.HibernateTemplate.Execute<int>(actionCount);
            return list;
        }
        /// <summary>
        /// 获取登录者的抄送/阅读文档信息的查询条件
        /// 查询条件为((ffilecopyuser.Type=1) or (ffilecopyuser.HRDept.Id in(员工所属部门)) or(ffilecopyuser.RBACRole.Id in(员工所属角色)) or (ffilecopyuser.User.Id=员工帐号Id))
        /// </summary>
        /// <returns></returns>
        private string GetHqlWhereByEmployeeID(string obj,string employeeID,string employeeName)
        {
            StringBuilder strTemp = new StringBuilder();
            //超级管理员
            if ((employeeID == "" || employeeID == "-1") && employeeName.ToLower() == "superuser")
            {
                return "";
            }
            else if (!String.IsNullOrEmpty(employeeID) && employeeID != "-1")
            {
                //查询符合全体人员的文档
                strTemp.Append("(" + obj + ".Type=1)");
                //查询符合当前登录者所属科室
                if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
                {
                    long hrdeptID = 0;
                    if (ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID) != null && ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID).Trim() != "")
                    {
                        hrdeptID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID).Trim());
                    }
                    else
                    {
                        hrdeptID = IDHREmployeeDao.Get(long.Parse(employeeID)).HRDept.Id;
                    }
                    List<long> hrdeptList = IDHRDeptDao.GetParentDeptIdListByDeptId(hrdeptID);
                    string tempIds = hrdeptList.ToString();
                    if (hrdeptList != null && hrdeptList.Count > 0)
                    {
                        strTemp.Append(" or ");
                        string tempId = "";
                        foreach (long id in hrdeptList)
                        {
                            tempId += id + ",";
                        }
                        strTemp.Append("(" + obj + ".HRDept.Id in(" + tempId.TrimEnd(',') + "))");
                    }
                    //查询符合当前登录者所属角色
                    IList<RBACRole> rbacroleList = IDRBACRoleDao.SearchRoleByHREmpID(long.Parse(employeeID));
                    if (rbacroleList != null && rbacroleList.Count > 0)
                    {
                        strTemp.Append(" or ");
                        string tempId = "";
                        foreach (RBACRole model in rbacroleList)
                        {
                            tempId += model.Id + ",";
                        }
                        strTemp.Append("(" + obj + ".RBACRole.Id in(" + tempId.TrimEnd(',') + "))");
                    }
                    //查询符合当前登录者的的文档
                    strTemp.Append(" or ");
                    strTemp.Append("(" + obj + ".User.Id=" + employeeID + ")");
                }

                //2020 查询符合当前登录者所属区域的
                var hospitalemplink = IDBHospitalEmpLinkDao.GetListByHQL("EmpID=" + employeeID); //所属医院
                List<long> areaids = new List<long>();
                if (hospitalemplink.Count > 0)
                {
                    //所属区域
                    List<long> areas = new List<long>();
                    foreach (var item in hospitalemplink)
                    {
                        areas.Add(item.HospitalID.Value);
                    }
                    var hosptals = IDBHospitalDao.GetListByHQL("Id in (" + string.Join(",",areas)+ ")");
                    List<long> areaIds = new List<long>();
                    foreach (var item in hosptals)
                    {
                        if (item.AreaID.HasValue)
                        {
                            areaIds.Add(item.AreaID.Value);
                        }
                    }
                    ZhiFang.Common.Log.Log.Debug("FFileDao.GetHqlWhereByEmployeeID方法:人员所属医院数量："+areaIds.Count);
                    if (areaIds.Count>0)
                    {
                        var hospitals = IDBHospitalDao.GetListByHQL("AreaID in (" + string.Join(",", areaIds) + ")");
                        foreach (var item in hospitals)
                        {
                            if (item.AreaID.HasValue)
                            {
                                areaids.Add(item.AreaID.Value);
                            }
                        }
                    }
                    ZhiFang.Common.Log.Log.Debug("FFileDao.GetHqlWhereByEmployeeID方法:所属医院区域下所有医院的区域：" + areaids.Count);
                    if (areaids.Count>0)
                    {
                        var hparea = IDBHospitalAreaDao.GetListByHQL("Id in (" + string.Join(",", areaids) + ")");
                        foreach (var item in hparea)
                        {
                            if (item.CenterHospitalID.HasValue)
                            {
                                areaids.Add(item.CenterHospitalID.Value);
                            }
                        }
                    }
                    ZhiFang.Common.Log.Log.Debug("FFileDao.GetHqlWhereByEmployeeID方法:所属医院区域下所有医院的区域的中心医院：" + areaids.Count);

                }
                if (areaids.Count>0)
                {
                    strTemp.Append(" or ");
                    strTemp.Append("(" + obj + ".AreaId in (" + string.Join(",", areaids) + "))");
                }

                strTemp.Insert(0, "(");
                strTemp.Append(")");
            }
            return strTemp.ToString();

        }
        public EntityList<FFile> SearchListByHQL(string where, int page, int count, string sort)
        {
            EntityList<FFile> list = new EntityList<FFile>();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            //如果没有登录,返回空数据
            if (String.IsNullOrEmpty(employeeID) && String.IsNullOrEmpty(employeeName))
            {
                return list;
            }
            string strHQL = "from FFile ffile left join ffile.FFileOperationList ffileoperationlist ";
            string hqlTemp = "";
            StringBuilder strlistHQL = new StringBuilder();
            if (String.IsNullOrEmpty(hqlTemp))
            {
                hqlTemp = where;
            }
            else if (!String.IsNullOrEmpty(where) && !String.IsNullOrEmpty(hqlTemp))
            {
                hqlTemp = hqlTemp + " and " + where;
            }
            string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
            if (labid != null && labid.Trim() != "")
            {
                if (!String.IsNullOrEmpty(hqlTemp))
                {
                    hqlTemp = hqlTemp + " and (ffile.LabID= " + labid + ")";
                }
                else
                {
                    hqlTemp = "(ffile.LabID= " + labid + ")";
                }
            }
            //获取阅读文档信息
            strlistHQL.Append("select distinct ffile " + strHQL);
            if (!string.IsNullOrEmpty(hqlTemp))
            {
                strlistHQL.Append(" where " + hqlTemp);
            }
            //获取阅读文档信息总数
            StringBuilder strCountHQL = new StringBuilder();
            strCountHQL.Append("select count(distinct ffile.Id) " + strHQL);
            if (!string.IsNullOrEmpty(hqlTemp))
            {
                strCountHQL.Append(" where " + hqlTemp);
            }

            DaoNHBSearchByHqlAction<List<FFile>, FFile> action = new DaoNHBSearchByHqlAction<List<FFile>, FFile>(strlistHQL.ToString(), page, count);
            DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strCountHQL.ToString());
            EntityList<FFile> tempList = new EntityList<FFile>();
            tempList.list = this.HibernateTemplate.Execute<List<FFile>>(action);
            foreach (FFile ffile in tempList.list)
            {
                var operationLis = ffile.FFileOperationList;
                foreach (FFileOperation item in operationLis)
                {
                    switch (item.Type)
                    {
                        case (int)FFileOperationType.审核:
                            ffile.CheckerDateTime = item.DataAddTime;
                            break;
                        case (int)FFileOperationType.批准:
                            ffile.ApprovalDateTime = item.DataAddTime;
                            break;
                        case (int)FFileOperationType.发布:
                            ffile.PublisherDateTime = item.DataAddTime;
                            break;
                        default:
                            break;
                    }
                }
                list.list.Add(ffile);
            }
            list.count = this.HibernateTemplate.Execute<int>(actionCount);
            return list;

        }
    }
}