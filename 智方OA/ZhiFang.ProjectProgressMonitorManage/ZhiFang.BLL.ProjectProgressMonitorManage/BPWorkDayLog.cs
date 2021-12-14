using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Response;
using ZhiFang.ProjectProgressMonitorManage.Common;
using ZhiFang.IBLL.OA;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPWorkDayLog : BaseBLL<PWorkDayLog>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPWorkDayLog
    {
        IDPWorkLogCopyForDao IDPWorkLogCopyForDao { get; set; }
        IDPWorkWeekLogDao IDPWorkWeekLogDao { get; set; }
        IDPWorkMonthLogDao IDPWorkMonthLogDao { get; set; }
        IDPWorkLogInteractionDao IDPWorkLogInteractionDao { get; set; }
        IDPProjectAttachmentDao IDPProjectAttachmentDao { get; set; }
        ZhiFang.IDAO.RBAC.IDHRDeptDao IDHRDeptDao { get; set; }
        ZhiFang.IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { get; set; }
        public IDAO.ProjectProgressMonitorManage.IDPTaskDao IDPTaskDao { get; set; }
        public IBATHolidaySetting IBATHolidaySetting { set; get; }
        public override bool Add()
        {
            if (base.Add())
            {
                if (Entity.PTask != null)
                    IDPTaskDao.UpdateByHql("update PTask set WorkLogCount = (WorkLogCount + 1) where Id = " + Entity.PTask.Id);
                return true;
            }
            return false;
        }

        public bool AddPWorkDayLogByWeiXin(List<string> AttachmentUrlList)
        {
            if (this.Add())
            {
                if (this.Entity.CopyForEmpIdList.Count > 0 && this.Entity.CopyForEmpNameList.Count > 0 && this.Entity.CopyForEmpIdList.Count == this.Entity.CopyForEmpNameList.Count)
                {
                    for (int i = 0; i < this.Entity.CopyForEmpIdList.Count; i++)
                    {
                        PWorkLogCopyFor pwlcf = new PWorkLogCopyFor();
                        pwlcf.LogID = this.Entity.Id;
                        pwlcf.PublishEmpID = this.Entity.EmpID;
                        pwlcf.PublishEmpName = this.Entity.EmpName;
                        pwlcf.ReceiveEmpID = this.Entity.CopyForEmpIdList[i];
                        pwlcf.ReceiveEmpName = this.Entity.CopyForEmpNameList[i];
                        pwlcf.LogType = WorkLogType.WorkLogDay.ToString();
                        IDPWorkLogCopyForDao.Save(pwlcf);
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        public bool UpdatePWorkDayLogByField(PWorkDayLog entity, string[] tempArray)
        {
            bool result = true;
            if (this.Update(tempArray))
            {
                if (entity.CopyForEmpIdList.Count > 0 && entity.CopyForEmpNameList.Count > 0 && entity.CopyForEmpIdList.Count == entity.CopyForEmpNameList.Count)
                {
                    //先删除原来的抄送人信息
                    result = IDPWorkLogCopyForDao.DeleteByLogId(entity.Id);
                    for (int i = 0; i < entity.CopyForEmpIdList.Count; i++)
                    {
                        PWorkLogCopyFor pwlcf = new PWorkLogCopyFor();
                        pwlcf.LogID = entity.Id;
                        pwlcf.PublishEmpID = entity.EmpID;
                        pwlcf.PublishEmpName = entity.EmpName;
                        pwlcf.ReceiveEmpID = entity.CopyForEmpIdList[i];
                        pwlcf.ReceiveEmpName = entity.CopyForEmpNameList[i];
                        pwlcf.LogType = WorkLogType.WorkLogDay.ToString();
                        IDPWorkLogCopyForDao.Save(pwlcf);
                    }
                }
            }
            else
            {
                result = false;
                return result;
            }
            return result;
        }

        public IList<WorkLogVO> SearchPWorkDayLogBySendTypeAndWorkLogType(string sd, string ed, int page, int limit, string sendtype, string worklogtype, string sort, long empid, long ownempid)
        {
            string hql = " 1=2 ";
            string deptempido = "";
            string copyworklogid = "";
            string deptempid = "";
            List<long> tmpdeptemp = new List<long>();
            List<long> tmpworklog = new List<long>();
            Entity.Base.EntityList<PWorkDayLog> pworkdayloglist = new Entity.Base.EntityList<PWorkDayLog>();
            Entity.Base.EntityList<PWorkWeekLog> pworkweekloglist = new Entity.Base.EntityList<PWorkWeekLog>();
            Entity.Base.EntityList<PWorkMonthLog> pworkmonthloglist = new Entity.Base.EntityList<PWorkMonthLog>();
            List<WorkLogVO> worklogvolist = new List<WorkLogVO>();
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType.sendtype.Trim().ToUpper():" + sendtype.Trim().ToUpper());
            #region 日志范围
            switch (sendtype.Trim().ToUpper())
            {
                case "ALL":
                    tmpdeptemp = GetManagerDeptEmpID(ownempid);
                    if (tmpdeptemp != null && tmpdeptemp.Count > 0)
                    {
                        deptempido = string.Join(",", tmpdeptemp.ToArray());
                        hql += " or EmpID in (" + deptempido + ") ";
                    }
                    tmpworklog = GetCopyWorkLogID(ownempid, worklogtype);
                    if (tmpworklog != null && tmpworklog.Count > 0)
                    {
                        copyworklogid = string.Join(",", tmpworklog.ToArray());
                        hql += " or Id in (" + copyworklogid + ") ";
                    }
                    hql += " or EmpID = " + ownempid;
                    hql += " or WorkLogExportLevel = " + ((int)WorkLogExportLevel.全公司可见).ToString() + " ";//全公司可见
                    var tmpdeptempid = GetDeptEmpID(ownempid);
                    if (tmpdeptempid != null && tmpdeptempid.Count > 0)
                    {
                        deptempid = string.Join(",", tmpdeptempid.ToArray());
                        hql += " or (1=1 and WorkLogExportLevel = " + ((int)WorkLogExportLevel.所属部门可见).ToString() + " and EmpID in(" + deptempid + ")) ";//部门可见
                    }
                    break;
                case "COPYFORME"://@我
                    tmpworklog = GetCopyWorkLogID(ownempid, worklogtype);
                    if (tmpworklog != null && tmpworklog.Count > 0)
                    {
                        copyworklogid = string.Join(",", tmpworklog.ToArray());
                        hql += " or Id in (" + copyworklogid + ") ";
                    }
                    break;
                case "SENDFORME"://下属
                    tmpdeptemp = GetManagerDeptEmpID(ownempid);
                    if (tmpdeptemp != null && tmpdeptemp.Count > 0)
                    {
                        deptempido = string.Join(",", tmpdeptemp.ToArray());
                        hql += " or EmpID in (" + deptempido + ") ";
                    }
                    break;
                case "MEOWN"://我自己
                    hql += " or EmpID = " + ownempid;
                    break;
            }

            #endregion
            hql = "(" + hql + ")";
            if (empid > 0 && empid != ownempid)
            {
                hql += " and EmpID=" + empid;
            }
            if (sd != null && sd.Trim() != "")
            {
                hql += " and DataAddTime>='" + sd + "' ";
            }
            if (ed != null && ed.Trim() != "")
            {
                hql += " and DataAddTime<='" + ed + " 23:59:59'";
            }
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType.HQL:" + hql);
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType.worklogtype:" + worklogtype);
            if (worklogtype.ToUpper().Trim() == WorkLogType.WorkLogDay.ToString().ToUpper())
            {
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType.day.HQL:" + hql);
                pworkdayloglist = DBDao.GetListByHQL(hql, " DataAddTime desc ", page, limit);
                foreach (var log in pworkdayloglist.list)
                {
                    worklogvolist.Add(TransVO(log, WorkLogType.WorkLogDay));
                }
            }
            if (worklogtype.ToUpper().Trim() == WorkLogType.WorkLogWeek.ToString().ToUpper())
            {
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType.week.HQL:" + hql);
                pworkweekloglist = IDPWorkWeekLogDao.GetListByHQL(hql, " DataAddTime desc ", page, limit);
                foreach (var log in pworkweekloglist.list)
                {
                    worklogvolist.Add(TransVO(log, WorkLogType.WorkLogWeek));
                }
            }
            if (worklogtype.ToUpper().Trim() == WorkLogType.WorkLogMonth.ToString().ToUpper())
            {
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType.month.HQL:" + hql);
                pworkmonthloglist = IDPWorkMonthLogDao.GetListByHQL(hql, " DataAddTime desc ", page, limit);
                foreach (var log in pworkmonthloglist.list)
                {
                    worklogvolist.Add(TransVO(log, WorkLogType.WorkLogMonth));
                }
            }
            return worklogvolist;
        }
        /// <summary>
        /// 获取管理部门内的员工ID
        /// </summary>
        /// <param name="deptmanagerempid"></param>
        /// <returns></returns>
        private List<long> GetManagerDeptEmpID(long deptmanagerempid)
        {
            List<long> empidlist = new List<long>();
            long tmpid = deptmanagerempid;
            if (tmpid <= 0)
            {
                return null;
            }
            IList<ZhiFang.Entity.RBAC.HRDept> hrdeptlist = IDHRDeptDao.GetListByHQL(" ManagerID=" + deptmanagerempid);
            if (hrdeptlist.Count > 0)
            {
                foreach (Entity.RBAC.HRDept dept in hrdeptlist)
                {
                    List<long> hrdeptidlist = IDHRDeptDao.GetSubDeptIdListByDeptId(dept.Id);
                    hrdeptidlist.Add(dept.Id);
                    IList<ZhiFang.Entity.RBAC.HREmployee> allemplist = IDHREmployeeDao.GetListByHQL(" IsUse=true and HRDept.Id in (" + string.Join(",", hrdeptidlist.ToArray()) + ") ");
                    //if (dept.HREmployeeList!=null &&dept.HREmployeeList.Count > 0)
                    //{
                    foreach (Entity.RBAC.HREmployee emp in allemplist)
                    {
                        empidlist.Add(emp.Id);
                    }
                    //}
                }
            }
            return empidlist;
        }
        /// <summary>
        /// 获取抄送日志的ID
        /// </summary>
        /// <param name="ownempid"></param>
        /// <param name="worklogtype"></param>
        /// <returns></returns>
        private List<long> GetCopyWorkLogID(long ownempid, string worklogtype)
        {
            List<long> worklogidlist = new List<long>();
            long tmpid = ownempid;
            if (tmpid <= 0)
            {
                return null;
            }
            IList<PWorkLogCopyFor> worklogcopyforlist = IDPWorkLogCopyForDao.GetListByHQL(" ReceiveEmpID=" + ownempid + " and LogType='" + worklogtype + "'");
            if (worklogcopyforlist.Count > 0)
            {
                foreach (PWorkLogCopyFor worklogcopyfor in worklogcopyforlist)
                {
                    if (worklogcopyfor.LogID.HasValue && worklogcopyfor.LogID > 0)
                    {
                        worklogidlist.Add(worklogcopyfor.LogID.Value);
                    }
                }
            }
            return worklogidlist;
        }
        /// <summary>
        /// 获取所在部门的员工ID
        /// </summary>
        /// <param name="empid"></param>
        /// <returns></returns>
        private List<long> GetDeptEmpID(long empid)
        {
            List<long> empidlist = new List<long>();
            long tmpid = empid;
            if (tmpid <= 0)
            {
                return null;
            }
            var tmpemp = IDHREmployeeDao.Get(empid);
            if (tmpemp == null || tmpemp.HRDept == null)
            {
                return null;
            }
            IList<ZhiFang.Entity.RBAC.HRDept> hrdeptlist = IDHRDeptDao.GetListByHQL(" Id=" + tmpemp.HRDept.Id);
            if (hrdeptlist.Count > 0)
            {
                if (hrdeptlist[0].HREmployeeList != null && hrdeptlist[0].HREmployeeList.Count > 0)
                {
                    foreach (Entity.RBAC.HREmployee emp in hrdeptlist[0].HREmployeeList)
                    {
                        empidlist.Add(emp.Id);
                    }
                }
            }
            return empidlist;
        }

        private WorkLogVO TransVO(PWorkLogBase pwdl, WorkLogType wlt)
        {
            WorkLogVO wlv = new WorkLogVO();
            if (pwdl != null)
            {
                wlv.EmpId = pwdl.EmpID.ToString();
                wlv.EmpName = pwdl.EmpName.ToString();
                //wlv.HeadImgUrl = pwdl.HeadImgUrl.ToString();
                wlv.DataAddTime = (pwdl.DataAddTime.HasValue) ? pwdl.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                wlv.DataUpdateTime = (pwdl.DataUpdateTime.HasValue) ? pwdl.DataUpdateTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                wlv.DateCode = pwdl.DateCode;
                wlv.WorkLogExportLevel = pwdl.WorkLogExportLevel;
                wlv.Id = pwdl.Id.ToString();
                //wlv.IsUse = pwdl.IsUse;
                wlv.Status = pwdl.Status;
                wlv.Image1 = pwdl.Image1 != null ? pwdl.Image1.Replace(AppDomain.CurrentDomain.BaseDirectory, "") : "";
                wlv.Image2 = pwdl.Image2 != null ? pwdl.Image2.Replace(AppDomain.CurrentDomain.BaseDirectory, "") : "";
                wlv.Image3 = pwdl.Image3 != null ? pwdl.Image3.Replace(AppDomain.CurrentDomain.BaseDirectory, "") : "";
                wlv.Image4 = pwdl.Image4 != null ? pwdl.Image4.Replace(AppDomain.CurrentDomain.BaseDirectory, "") : "";
                wlv.Image5 = pwdl.Image5 != null ? pwdl.Image5.Replace(AppDomain.CurrentDomain.BaseDirectory, "") : "";
                wlv.LikeCount = pwdl.LikeCount.HasValue ? pwdl.LikeCount.Value : 0;
                wlv.ToDayContent = pwdl.ToDayContent;
                wlv.NextDayContent = pwdl.NextDayContent;
                wlv.WorkLogType = wlt;
                if (pwdl.PTask != null)
                {
                    wlv.PTaskID = pwdl.PTask.Id;
                }
                #region 抄送
                IList<PWorkLogCopyFor> pwlcflist = IDPWorkLogCopyForDao.GetListByHQL(" LogID=" + pwdl.Id + " ");
                if (pwlcflist != null && pwlcflist.Count > 0)
                {
                    wlv.CopyForEmpIdList = new List<long>();
                    wlv.CopyForEmpNameList = new List<string>();
                    foreach (var pwlcf in pwlcflist)
                    {
                        long receiveempid = (pwlcf.ReceiveEmpID.HasValue) ? pwlcf.ReceiveEmpID.Value : -1;
                        wlv.CopyForEmpIdList.Add(receiveempid);
                        wlv.CopyForEmpNameList.Add(pwlcf.ReceiveEmpName);
                    }
                }
                #endregion

                #region 互动评论
                string hql = "";
                switch (wlt)
                {
                    case WorkLogType.WorkLogDay: hql = " WorkDayLogID=" + pwdl.Id + " "; break;
                    case WorkLogType.WorkLogWeek: hql = " WorkWeekLogID=" + pwdl.Id + " "; break;
                    case WorkLogType.WorkLogMonth: hql = " WorkMonthLogID=" + pwdl.Id + " "; break;
                }
                if (hql.Trim() != "")
                {
                    IList<PWorkLogInteraction> pwlilist = IDPWorkLogInteractionDao.GetListByHQL(hql);
                    if (pwlilist != null && pwlilist.Count > 0)
                    {
                        wlv.InteractionCount = pwlilist.Count;
                    }
                }
                #endregion
                //wlv.PProjectAttachmentList = pwdl.PProjectAttachmentList;
                //wlv.PTask = pwdl.PTask;
                //wlv.PWorkTaskLogList = pwdl.PWorkTaskLogList;
                return wlv;
            }
            return null;
        }

        public IList<WorkLogVO> SearchTaskWorkDayLogTaskId(string sd, string ed, int page, int limit, string sort, long empid, long ownempid, long taskid)
        {
            string hql = " 1=1 ";
            Entity.Base.EntityList<PWorkDayLog> pworkdayloglist = new Entity.Base.EntityList<PWorkDayLog>();
            List<WorkLogVO> worklogvolist = new List<WorkLogVO>();
            if (taskid > 0)
            {
                hql += " and PTask.Id=" + taskid;
            }
            if (empid > 0 && empid != ownempid)
            {
                hql += " and EmpID=" + empid;
            }
            if (sd != null && sd.Trim() != "")
            {
                hql += " and DataAddTime>='" + sd + "' ";
            }
            if (ed != null && ed.Trim() != "")
            {
                hql += " and DataAddTime<='" + ed + " 23:59:59'";
            }
            ZhiFang.Common.Log.Log.Debug("SearchTaskWorkDayLogTaskId.HQL:" + hql);
            pworkdayloglist = DBDao.GetListByHQL(hql, " DataAddTime desc ", page, limit);
            foreach (var log in pworkdayloglist.list)
            {
                worklogvolist.Add(TransVO(log, WorkLogType.WorkLogDay));
            }
            return worklogvolist;
        }

        public WorkLogVO ST_UDTO_SearchWorkDayLogByIdAndWorkLogType(long id, string worklogtype)
        {
            IList<PWorkDayLog> pworkdayloglist = new List<PWorkDayLog>();
            IList<PWorkWeekLog> pworkweekloglist = new List<PWorkWeekLog>();
            IList<PWorkMonthLog> pworkmonthloglist = new List<PWorkMonthLog>();
            if (worklogtype.ToUpper().Trim() == WorkLogType.WorkLogDay.ToString().ToUpper())
            {
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchWorkDayLogByIdAndWorkLogType.day.id:" + id);
                pworkdayloglist = DBDao.GetListByHQL(" Id= " + id);
                if (pworkdayloglist.Count > 0)
                {
                    return TransVO(pworkdayloglist[0], WorkLogType.WorkLogDay);
                }
            }
            if (worklogtype.ToUpper().Trim() == WorkLogType.WorkLogWeek.ToString().ToUpper())
            {
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchWorkDayLogByIdAndWorkLogType.week.id:" + id);
                pworkweekloglist = IDPWorkWeekLogDao.GetListByHQL(" Id= " + id);
                if (pworkweekloglist.Count > 0)
                {
                    return TransVO(pworkweekloglist[0], WorkLogType.WorkLogWeek);
                }
            }
            if (worklogtype.ToUpper().Trim() == WorkLogType.WorkLogMonth.ToString().ToUpper())
            {
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchWorkDayLogByIdAndWorkLogType.month.id:" + id);
                pworkmonthloglist = IDPWorkMonthLogDao.GetListByHQL(" Id= " + id);
                if (pworkmonthloglist.Count > 0)
                {
                    return TransVO(pworkmonthloglist[0], WorkLogType.WorkLogMonth);
                }
            }
            return null;
        }

        public int WorkDayAddLikeCountLogByIdAndWorkLogType(long id, string worklogtype)
        {
            IList<PWorkDayLog> pworkdayloglist = new List<PWorkDayLog>();
            IList<PWorkWeekLog> pworkweekloglist = new List<PWorkWeekLog>();
            IList<PWorkMonthLog> pworkmonthloglist = new List<PWorkMonthLog>();
            if (worklogtype.ToUpper().Trim() == WorkLogType.WorkLogDay.ToString().ToUpper())
            {
                return DBDao.UpdateByHql("update PWorkDayLog set LikeCount=LikeCount+1 where Id=" + id);
            }
            if (worklogtype.ToUpper().Trim() == WorkLogType.WorkLogWeek.ToString().ToUpper())
            {
                return IDPWorkWeekLogDao.UpdateByHql("update PWorkWeekLog set LikeCount=LikeCount+1 where Id=" + id);
            }
            if (worklogtype.ToUpper().Trim() == WorkLogType.WorkLogMonth.ToString().ToUpper())
            {
                return IDPWorkMonthLogDao.UpdateByHql("update PWorkMonthLog set LikeCount=LikeCount+1 where Id=" + id);
            }
            return 0;
        }

        public IList<WorkLogVO> SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType(string sd, string ed, int page, int limit, long deptid, string worklogtype, string sort, long empid, long ownempid, bool isincludesubdept)
        {
            string hql = " 1=1 ";
            Entity.Base.EntityList<PWorkDayLog> pworkdayloglist = new Entity.Base.EntityList<PWorkDayLog>();
            Entity.Base.EntityList<PWorkWeekLog> pworkweekloglist = new Entity.Base.EntityList<PWorkWeekLog>();
            Entity.Base.EntityList<PWorkMonthLog> pworkmonthloglist = new Entity.Base.EntityList<PWorkMonthLog>();
            List<WorkLogVO> worklogvolist = new List<WorkLogVO>();

            if (deptid > 0)
            {
                if (isincludesubdept)
                {
                    var deptidlist = IDHRDeptDao.GetSubDeptIdListByDeptId(deptid);
                    var emplist = IDHREmployeeDao.GetListByHQL(" IsUse=true and HRDept.Id in ( " + string.Join(",", deptidlist.ToArray()) + " )");
                    List<long> empidlist = new List<long>();
                    foreach (Entity.RBAC.HREmployee emp in emplist)
                    {
                        empidlist.Add(emp.Id);
                    }
                    if (emplist.Count > 0)
                    {
                        hql += " and EmpID in (" + string.Join(",", empidlist.ToArray()) + ") ";
                    }
                    else
                    {
                        hql += " and 1=2 ";
                    }
                }
                else
                {
                    var emplist = IDHREmployeeDao.GetListByHQL(" IsUse=true and HRDept.Id = " + deptid + " ");
                    List<long> empidlist = new List<long>();
                    foreach (Entity.RBAC.HREmployee emp in emplist)
                    {
                        empidlist.Add(emp.Id);
                    }
                    if (emplist.Count > 0)
                    {
                        hql += " and EmpID in (" + string.Join(",", empidlist.ToArray()) + ") ";
                    }
                    else
                    {
                        hql += " and 1=2 ";
                    }
                }
            }
            else
            {
                if (empid > 0 && empid != ownempid)
                {
                    hql += " and EmpID=" + empid;
                }
            }
            if (sd != null && sd.Trim() != "")
            {
                hql += " and DataAddTime>='" + sd + "' ";
            }
            if (ed != null && ed.Trim() != "")
            {
                hql += " and DataAddTime<='" + ed + " 23:59:59'";
            }
            ZhiFang.Common.Log.Log.Debug("SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType.HQL:" + hql);
            ZhiFang.Common.Log.Log.Debug("SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType.worklogtype:" + worklogtype);
            if (worklogtype.ToUpper().Trim() == WorkLogType.WorkLogDay.ToString().ToUpper())
            {
                ZhiFang.Common.Log.Log.Debug("SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType.day.HQL:" + hql);
                pworkdayloglist = DBDao.GetListByHQL(hql, " DataAddTime desc ", page, limit);
                foreach (var log in pworkdayloglist.list)
                {
                    worklogvolist.Add(TransVO(log, WorkLogType.WorkLogDay));
                }
            }
            if (worklogtype.ToUpper().Trim() == WorkLogType.WorkLogWeek.ToString().ToUpper())
            {
                ZhiFang.Common.Log.Log.Debug("SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType.week.HQL:" + hql);
                pworkweekloglist = IDPWorkWeekLogDao.GetListByHQL(hql, " DataAddTime desc ", page, limit);
                foreach (var log in pworkweekloglist.list)
                {
                    worklogvolist.Add(TransVO(log, WorkLogType.WorkLogWeek));
                }
            }
            if (worklogtype.ToUpper().Trim() == WorkLogType.WorkLogMonth.ToString().ToUpper())
            {
                ZhiFang.Common.Log.Log.Debug("SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogTypte.month.HQL:" + hql);
                pworkmonthloglist = IDPWorkMonthLogDao.GetListByHQL(hql, " DataAddTime desc ", page, limit);
                foreach (var log in pworkmonthloglist.list)
                {
                    worklogvolist.Add(TransVO(log, WorkLogType.WorkLogMonth));
                }
            }
            return worklogvolist;
        }

        public IList<WorkLogVO> SearchPWorkDayLogByEmpId(string monthday, long empid, bool isincludesubdept)
        {
            List<WorkLogVO> worklogvolist = new List<WorkLogVO>();
            DateTime tmpdatetime = Convert.ToDateTime(monthday);
            int daysinmonth = DateTime.DaysInMonth(tmpdatetime.Year, tmpdatetime.Month);
            string sd = tmpdatetime.ToString("yyyy-MM") + "-1";
            string ed = tmpdatetime.ToString("yyyy-MM") + "-" + daysinmonth;
            string hql = " 1=1 ";
            if (empid > 0)
            {
                hql += " and EmpID=" + empid;
            }
            if (sd != null && sd.Trim() != "")
            {
                hql += " and DataAddTime>='" + sd + "' ";
            }
            if (ed != null && ed.Trim() != "")
            {
                hql += " and DataAddTime<='" + ed + " 23:59:59'";
            }
            //ZhiFang.Common.Log.Log.Debug("SearchPWorkDayLogByDeptIdOrEmpId.HQL:" + hql);
            var workdayloglist = DBDao.GetListByHQL(hql, " DataAddTime desc ", 1, 100);
            //获取该月份的所有工作日期的集合
            Dictionary<string, DayOfWeek> allWorkDays = IBATHolidaySetting.GetAllWorkDaysOfOneMonth(tmpdatetime.Year, tmpdatetime.Month);

            for (int i = 0; i < daysinmonth; i++)
            {
                WorkLogVO wlv = new WorkLogVO();
                string curDate = Convert.ToDateTime(sd).AddDays(i).ToString("yyyy-MM-dd");
                if (allWorkDays.ContainsKey(curDate))
                    wlv.IsWorkDay = true;
                else
                    wlv.IsWorkDay = false;

                var wdllist=workdayloglist.list.Where(a => a.DateCode == curDate);
                if (wdllist.Count() > 0)
                {
                    var wdl = wdllist.ElementAt(0);
                    wlv.EmpId = wdl.EmpID.ToString();
                    wlv.EmpName = wdl.EmpName.ToString();
                    //wlv.HeadImgUrl = wdl.HeadImgUrl.ToString();
                    wlv.DataAddTime = (wdl.DataAddTime.HasValue) ? wdl.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                    wlv.DataUpdateTime = (wdl.DataUpdateTime.HasValue) ? wdl.DataUpdateTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                    wlv.DateCode = wdl.DateCode;
                    wlv.WorkLogExportLevel = wdl.WorkLogExportLevel;
                    wlv.Id = wdl.Id.ToString();
                    //wlv.IsUse = pwdl.IsUse;
                    wlv.Status = wdl.Status;
                    wlv.Image1 = wdl.Image1 != null ? wdl.Image1.Replace(AppDomain.CurrentDomain.BaseDirectory, "") : "";
                    wlv.Image2 = wdl.Image2 != null ? wdl.Image2.Replace(AppDomain.CurrentDomain.BaseDirectory, "") : "";
                    wlv.Image3 = wdl.Image3 != null ? wdl.Image3.Replace(AppDomain.CurrentDomain.BaseDirectory, "") : "";
                    wlv.Image4 = wdl.Image4 != null ? wdl.Image4.Replace(AppDomain.CurrentDomain.BaseDirectory, "") : "";
                    wlv.Image5 = wdl.Image5 != null ? wdl.Image5.Replace(AppDomain.CurrentDomain.BaseDirectory, "") : "";
                    wlv.LikeCount = wdl.LikeCount.HasValue ? wdl.LikeCount.Value : 0;
                    wlv.ToDayContent = wdl.ToDayContent;
                    wlv.NextDayContent = wdl.NextDayContent;
                    if (wdl.PTask != null)
                    {
                        wlv.PTaskID = wdl.PTask.Id;
                    }
                }
                else
                {
                    wlv.DateCode = Convert.ToDateTime(sd).AddDays(i).ToString("yyyy-MM-dd");
                }
                wlv.WeekInfo = DateTimeHelp.GetDateWeek(Convert.ToDateTime(wlv.DateCode));
                worklogvolist.Add(wlv);
            }
            return worklogvolist;
        }
    }
}