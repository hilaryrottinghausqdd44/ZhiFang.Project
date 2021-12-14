using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Response;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.OA;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPTask : BaseBLL<PTask>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPTask
    {
        public IDAO.ProjectProgressMonitorManage.IDPTaskCopyForDao IDPTaskCopyForDao { set; get; }
        public IDAO.ProjectProgressMonitorManage.IDPTaskTypeEmpLinkDao IDPTaskTypeEmpLinkDao { set; get; }
        public IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public EntityList<PTask> SearchListByEntity(Task_Search entity, string sort, int Page, int Limit)
        {
            string hql = " 1=1 ";
            #region 日期范围
            if (entity.EstiStartTimeB.HasValue)
            {
                hql += " and ptask.EstiStartTime>='" + entity.EstiStartTimeB.Value.ToString("yyyy-MM-dd HH:mm:ss") +"' ";
            }
            if (entity.EstiStartTimeE.HasValue)
            {
                hql += " and ptask.EstiStartTime<='" + entity.EstiStartTimeE.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }

            if (entity.EstiEndTimeB.HasValue)
            {
                hql += " and ptask.EstiEndTime>='" + entity.EstiEndTimeB.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.EstiEndTimeE.HasValue)
            {
                hql += " and ptask.EstiEndTime<='" + entity.EstiEndTimeE.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }

            if (entity.StartTimeB.HasValue)
            {
                hql += " and ptask.StartTime>='" + entity.StartTimeB.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.StartTimeE.HasValue)
            {
                hql += " and ptask.StartTime<='" + entity.StartTimeE.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.EndTimeB.HasValue)
            {
                hql += " and ptask.EndTime>='" + entity.EndTimeB.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.EndTimeE.HasValue)
            {
                hql += " and ptask.EndTime<='" + entity.EndTimeE.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.DataAddTimeB.HasValue)
            {
                hql += " and ptask.DataAddTime>='" + entity.DataAddTimeB.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.DataAddTimeE.HasValue)
            {
                hql += " and ptask.DataAddTime<='" + entity.DataAddTimeE.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            #endregion

            if (entity.ExportType == 0)
            {

            }

            #region 相关人员
            if (entity.CheckerID.HasValue)
            {
                hql += " and ptask.CheckerID =" + entity.CheckerID.Value.ToString() + " ";
            }

            if (entity.ExecutorID.HasValue)
            {
                hql += " and ptask.ExecutorID =" + entity.ExecutorID.Value.ToString() + " ";
            }

            if (entity.PublisherID.HasValue)
            {
                hql += " and ptask.PublisherID =" + entity.PublisherID.Value.ToString() + " ";
            }
            #endregion

            if (entity.CName!=null && entity.CName.Trim()!="")
            {
                hql += " and ptask.CName like '%" + entity.CName.Trim() + "%' ";
            }

            if (entity.PProject.HasValue)
            {
                hql += " and ptask.PProject.Id =" + entity.PProject.Value.ToString() + " ";
            }

            if (entity.PClient.HasValue)
            {
                hql += " and ptask.PClient.Id =" + entity.PClient.Value.ToString() + " ";
            }

            if (entity.Type.HasValue)
            {
                hql += " and ptask.Type.Id =" + entity.Type.Value.ToString() + " ";
            }

            if (entity.ExecutType.HasValue)
            {
                hql += " and ptask.ExecutType.Id =" + entity.ExecutType.Value.ToString() + " ";
            }

            if (entity.Status!=null && entity.Status.Trim()!="")
            {
                hql += " and ptask.Status.Id in (" + entity.Status + ") ";
            }


            if (entity.Pace.HasValue)
            {
                hql += " and ptask.Pace.Id =" + entity.Pace.Value.ToString() + " ";
            }

            if (entity.IsUse.HasValue)
            {
                hql += " and ptask.IsUse =" + entity.IsUse.Value + " ";
            }





            return base.SearchListByHQL(hql,sort, Page, Limit);
        }
        public EntityList<PTask> SearchListByEntity(Task_Search entity,long empid, string sort, int Page, int Limit)
        {
            ZhiFang.Common.Log.Log.Debug("empid:"+ empid);
            string hql = " 1=1 ";
            #region 日期范围
            if (entity.EstiStartTimeB.HasValue)
            {
                hql += " and ptask.EstiStartTime>='" + entity.EstiStartTimeB.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.EstiStartTimeE.HasValue)
            {
                hql += " and ptask.EstiStartTime<='" + entity.EstiStartTimeE.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }

            if (entity.EstiEndTimeB.HasValue)
            {
                hql += " and ptask.EstiEndTime>='" + entity.EstiEndTimeB.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.EstiEndTimeE.HasValue)
            {
                hql += " and ptask.EstiEndTime<='" + entity.EstiEndTimeE.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }

            if (entity.StartTimeB.HasValue)
            {
                hql += " and ptask.StartTime>='" + entity.StartTimeB.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.StartTimeE.HasValue)
            {
                hql += " and ptask.StartTime<='" + entity.StartTimeE.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.EndTimeB.HasValue)
            {
                hql += " and ptask.EndTime>='" + entity.EndTimeB.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.EndTimeE.HasValue)
            {
                hql += " and ptask.EndTime<='" + entity.EndTimeE.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.DataAddTimeB.HasValue)
            {
                hql += " and ptask.DataAddTime>='" + entity.DataAddTimeB.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (entity.DataAddTimeE.HasValue)
            {
                hql += " and ptask.DataAddTime<='" + entity.DataAddTimeE.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            #endregion

            #region 浏览查看类型。-1：全部、0：我申请、1：我一审、2：我二审、3：我分配、4：我执行、5：我检查、6：抄送给我
            if (entity.ExportType.HasValue)
            {
                if (entity.ExportType == 0)
                {
                    hql += " and ptask.ApplyID =" + empid + " ";
                }
                if (entity.ExportType == 1)
                {
                    hql += " and ptask.OneAuditID =" + empid + " ";
                }
                if (entity.ExportType == 2)
                {
                    string tmphql = " ptask.TwoAuditID =" + empid + " ";
                    IList<PTaskTypeEmpLink> pttellist = IDPTaskTypeEmpLinkDao.GetListByHQL(" EmpID=" + empid + " and TwoAudit=true ");
                    if (pttellist != null && pttellist.Count > 0)
                    {
                        string typeid = "";
                        foreach (var a in pttellist)
                        {
                            typeid += a.TaskTypeID + ",";
                        }
                        tmphql += " or (ptask.PTypeID in (" + typeid.Remove(typeid.LastIndexOf(',')) + ") and ptask.TwoAuditID=null )";
                        ZhiFang.Common.Log.Log.Debug("TwoAuditID.tmphql:" + tmphql);
                    }
                    hql += " and (" + tmphql + ") ";
                    ZhiFang.Common.Log.Log.Debug("TwoAuditID.hql:" + hql);
                }
                if (entity.ExportType == 3)
                {
                    string tmphql = " ptask.PublisherID =" + empid + " ";
                    IList<PTaskTypeEmpLink> pttellist = IDPTaskTypeEmpLinkDao.GetListByHQL(" EmpID=" + empid + " and Publish=true ");
                    if (pttellist != null && pttellist.Count > 0)
                    {
                        string typeid = "";
                        foreach (var a in pttellist)
                        {
                            typeid += a.TaskTypeID + ",";
                        }
                        tmphql += " or (ptask.PTypeID in (" + typeid.Remove(typeid.LastIndexOf(',')) + ") and ptask.PublisherID=null )";
                        ZhiFang.Common.Log.Log.Debug("PublisherID.tmphql:" + tmphql);
                    }
                    hql += " and (" + tmphql + ") ";
                    ZhiFang.Common.Log.Log.Debug("PublisherID.hql:" + hql);

                }
                if (entity.ExportType == 4)
                {
                    hql += " and ptask.ExecutorID =" + empid + " ";
                }
                if (entity.ExportType == 5)
                {
                    hql += " and ptask.CheckerID =" + empid + " ";
                }
                if (entity.ExportType == 6)
                {
                    IList<PTaskCopyFor> ptcflist = IDPTaskCopyForDao.GetListByHQL(" CopyForEmpID=" + empid);
                    if (ptcflist != null && ptcflist.Count > 0)
                    {
                        string ptid = "";
                        foreach (var t in ptcflist)
                        {
                            ptid += t.Id + ",";
                        }
                        hql += " and ptask.Id in (" + ptid.Remove(ptid.Length - 1) + ") ";
                    }
                }
                if (entity.ExportType ==-1)
                {
                    hql += " and (ptask.ApplyID =" + empid + " or ptask.OneAuditID =" + empid + " or ptask.TwoAuditID =" + empid + " or ptask.PublisherID =" + empid + " or ptask.ExecutorID =" + empid + " or ptask.CheckerID =" + empid + ") " ;
                }
            }
            #endregion

            #region 相关人员
            if (entity.ApplyID.HasValue)
            {
                hql += " and ptask.ApplyID =" + entity.ApplyID.Value.ToString() + " ";
            }

            if (entity.OneAuditID.HasValue)
            {
                hql += " and ptask.OneAuditID =" + entity.OneAuditID.Value.ToString() + " ";
            }

            if (entity.TwoAuditID.HasValue)
            {
                hql += " and ptask.TwoAuditID =" + entity.TwoAuditID.Value.ToString() + " ";
            }

            if (entity.CheckerID.HasValue)
            {
                hql += " and ptask.CheckerID =" + entity.CheckerID.Value.ToString() + " ";
            }

            if (entity.ExecutorID.HasValue)
            {
                hql += " and ptask.ExecutorID =" + entity.ExecutorID.Value.ToString() + " ";
            }

            if (entity.PublisherID.HasValue)
            {
                hql += " and ptask.PublisherID =" + entity.PublisherID.Value.ToString() + " ";
            }
            #endregion

            if (entity.CName != null && entity.CName.Trim() != "")
            {
                hql += " and ptask.CName like '%" + entity.CName.Trim() + "%' ";
            }

            if (entity.PProject.HasValue)
            {
                hql += " and ptask.PProject.Id =" + entity.PProject.Value.ToString() + " ";
            }

            if (entity.PClient.HasValue)
            {
                hql += " and ptask.PClient.Id =" + entity.PClient.Value.ToString() + " ";
            }

            if (entity.Type.HasValue)
            {
                hql += " and ptask.Type.Id =" + entity.Type.Value.ToString() + " ";
            }

            if (entity.ExecutType.HasValue)
            {
                hql += " and ptask.ExecutType.Id =" + entity.ExecutType.Value.ToString() + " ";
            }

            if (entity.Status!=null && entity.Status.Trim()!="")
            {
                hql += " and ptask.Status.Id in ("+ entity.Status + ") ";
            }

            if (entity.NoStatus != null && entity.NoStatus.Trim() != "")
            {
                hql += " and ptask.Status.Id not in (" + entity.NoStatus + ") ";
            }

            if (entity.Pace.HasValue)
            {
                hql += " and ptask.Pace.Id =" + entity.Pace.Value.ToString() + " ";
            }

            if (entity.IsUse.HasValue)
            {
                hql += " and ptask.IsUse =" + entity.IsUse.Value + " ";
            }
            ZhiFang.Common.Log.Log.Debug("SearchListByEntity.hql:" + hql);
            return base.SearchListByHQL(hql, sort, Page, Limit);
        }
        public EntityList<PTask> SearchListByEntity(Task_Search entity, long empid, int Page, int Limit)
        {
            return SearchListByEntity(entity, empid, " DataAddTime Desc ", Page, Limit);
        }
        public override bool Add()
        {
            Entity.ApplyDataTime = DateTime.Now;
            bool bo = base.Add();
            if (bo && Entity.PTaskID.HasValue) {
                return SubCountReset(Entity.PTaskID.Value, 1);
            }
            return bo;
        }
        public bool Test(SysWeiXinTemplate.PushWeiXinMessage func,string openid)
        {
            func(openid,null,null,null,null);
            return false;
        }
        public override bool Remove(long longID)
        {
            bool bo = base.Remove(longID);

            if (!bo) return bo;

            PTask ptask = DBDao.Get(longID);
            if (ptask.PTaskID.HasValue) {
                return SubCountReset(Entity.PTaskID.Value, -1);
            }

            return bo;
        }
        public bool SubCountReset(long TaskId, int value)
        {
            int subCount = DBDao.GetListCountByHQL("ptask.PTaskID=" + TaskId) + value;
            return DBDao.UpdateByHql("update PTask ptask set ptask.SubCount=" + subCount + " where ptask.Id=" + TaskId) > 0;
        }

        public bool PTaskAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction)
        {
            Entity.ApplyDataTime = DateTime.Now;
            if (Entity.Status!=null && Entity.Status.Id.ToString() == PTaskStatus.一审通过.Id)
            {
                Entity.OneAuditDataTime= DateTime.Now;
            }
            bool bo = base.Add();

            if (bo && Entity.PTaskID.HasValue)
            {
                SubCountReset(Entity.PTaskID.Value, 1);
            }
            if (bo)
            {
                PTaskStatusMessagePush(pushWeiXinMessageAction, Entity.Id, Entity.Status.Id.ToString(), 0, null,this.Entity);
            }


            return bo;
        }

        //public bool Update(string[] strParas,SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction)
        //{
        //    bool flag= DBDao.Update(strParas);
        //    if (flag)
        //    {
        //        string statusnamestr = "";
        //        string ptaskidstr = "0";
        //        foreach (string p in strParas)
        //        {
        //            if (p.Trim().Split('=').Length > 2)
        //            {
        //                if (p.Trim().Split('=')[0].Trim() == "StatusName")
        //                    statusnamestr = p.Trim().Split('=')[1].Trim();

        //                if (p.Trim().IndexOf("Id=") >= 0 && p.Trim().IndexOf(".Id=") < 0)
        //                {
        //                    if (p.Trim().Split('=')[0].Trim() == "Id")
        //                        ptaskidstr = p.Trim().Split('=')[1].Trim();
        //                }
        //            }
        //        }
        //        if (statusnamestr != "" && ptaskidstr != "0")
        //        {
        //            long ptaskid = long.Parse(ptaskidstr);
        //            PTaskStatusMessagePush(pushWeiXinMessageAction, ptaskid, statusnamestr.ToString(), 0, null);
        //            return true;
        //        }
        //    }
        //    return flag;
        //}        

        public bool PTaskStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, PTask entity, string[] tempArray, long EmpID, string EmpName, out string ErrorInfo)
        {
            var tmpa = tempArray.ToList();
            PTask tmpptask = new PTask();
            tmpptask = DBDao.Get(entity.Id);
            ErrorInfo = "";
            if (tmpptask == null)
            {
                ErrorInfo = "任务ID：" + entity.Id + "的任务为空！";
                return false;
            }
            #region 暂存
            if (entity.Status.Id.ToString() == PTaskStatus.暂存.Id)
            {               
               
                if (tmpptask.Status.Id.ToString() != PTaskStatus.暂存.Id && tmpptask.Status.Id.ToString() != PTaskStatus.申请.Id && tmpptask.Status.Id.ToString() != PTaskStatus.一审退回.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为："+ tmpptask.StatusName + "！";
                    return false;
                }
            }
            #endregion
            #region 申请
            if (entity.Status.Id.ToString() == PTaskStatus.申请.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.暂存.Id && tmpptask.Status.Id.ToString() != PTaskStatus.申请.Id && tmpptask.Status.Id.ToString() != PTaskStatus.一审退回.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！PTaskStatus.暂存.Id="+ PTaskStatus.暂存.Id+ "@tmpptask.Status.Id.ToString()="+ tmpptask.Status.Id.ToString();
                    return false;
                }
                tmpa.Add("ApplyDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.Add("OneAuditDataTime=null");

                tmpa.Add("TwoAuditID=null");
                tmpa.Add("TwoAuditName=null");
                tmpa.Add("TwoAuditDataTime=null");

                tmpa.Add("PublisherID=null");
                tmpa.Add("PublisherName=null");
                tmpa.Add("PublisherDataTime=null");

                tmpa.Add("ExecutorID=null");
                tmpa.Add("ExecutorName=null");
                tmpa.Add("EstiStartTime=null");
                tmpa.Add("EstiEndTime=null");
                tmpa.Add("StartTime=null");
                tmpa.Add("EndTime=null");

                tmpa.Add("CheckerDataTime=null");

                tempArray = tmpa.ToArray();
            }
            #endregion
            #region 一审中
            if (entity.Status.Id.ToString() == PTaskStatus.一审中.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.申请.Id && tmpptask.Status.Id.ToString() != PTaskStatus.一审中.Id && tmpptask.Status.Id.ToString() != PTaskStatus.二审退回.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("OneAuditDataTime=null");

                tmpa.Add("TwoAuditID=null");
                tmpa.Add("TwoAuditName=null");
                tmpa.Add("TwoAuditDataTime=null");

                tmpa.Add("PublisherID=null");
                tmpa.Add("PublisherName=null");
                tmpa.Add("PublisherDataTime=null");

                tmpa.Add("ExecutorID=null");
                tmpa.Add("ExecutorName=null");
                tmpa.Add("EstiStartTime=null");
                tmpa.Add("EstiEndTime=null");
                tmpa.Add("StartTime=null");
                tmpa.Add("EndTime=null");

                tmpa.Add("CheckerDataTime=null");

                tempArray = tmpa.ToArray();
            }
            #endregion
            #region 一审通过
            if (entity.Status.Id.ToString() == PTaskStatus.一审通过.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.申请.Id && tmpptask.Status.Id.ToString() != PTaskStatus.暂存.Id && tmpptask.Status.Id.ToString() != PTaskStatus.一审中.Id && tmpptask.Status.Id.ToString() != PTaskStatus.二审退回.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("OneAuditDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.Add("TwoAuditID=null");
                tmpa.Add("TwoAuditName=null");
                tmpa.Add("TwoAuditDataTime=null");

                tmpa.Add("PublisherID=null");
                tmpa.Add("PublisherName=null");
                tmpa.Add("PublisherDataTime=null");

                tmpa.Add("ExecutorID=null");
                tmpa.Add("ExecutorName=null");
                tmpa.Add("EstiStartTime=null");
                tmpa.Add("EstiEndTime=null");
                tmpa.Add("StartTime=null");
                tmpa.Add("EndTime=null");

                tmpa.Add("CheckerDataTime=null");
                tempArray = tmpa.ToArray();
            }
            #endregion
            #region 一审退回
            if (entity.Status.Id.ToString() == PTaskStatus.一审退回.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.申请.Id && tmpptask.Status.Id.ToString() != PTaskStatus.一审中.Id && tmpptask.Status.Id.ToString() != PTaskStatus.二审退回.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("OneAuditDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.Add("TwoAuditID=null");
                tmpa.Add("TwoAuditName=null");
                tmpa.Add("TwoAuditDataTime=null");

                tmpa.Add("PublisherID=null");
                tmpa.Add("PublisherName=null");
                tmpa.Add("PublisherDataTime=null");

                tmpa.Add("ExecutorID=null");
                tmpa.Add("ExecutorName=null");
                tmpa.Add("EstiStartTime=null");
                tmpa.Add("EstiEndTime=null");
                tmpa.Add("StartTime=null");
                tmpa.Add("EndTime=null");

                tmpa.Add("CheckerDataTime=null");
                tempArray = tmpa.ToArray();
            }
            #endregion
            #region 二审中
            if (entity.Status.Id.ToString() == PTaskStatus.二审中.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.一审通过.Id && tmpptask.Status.Id.ToString() != PTaskStatus.二审中.Id && tmpptask.Status.Id.ToString() != PTaskStatus.分配退回.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("TwoAuditDataTime=null");

                tmpa.Add("PublisherID=null");
                tmpa.Add("PublisherName=null");
                tmpa.Add("PublisherDataTime=null");

                tmpa.Add("ExecutorID=null");
                tmpa.Add("ExecutorName=null");
                tmpa.Add("EstiStartTime=null");
                tmpa.Add("EstiEndTime=null");
                tmpa.Add("StartTime=null");
                tmpa.Add("EndTime=null");

                tmpa.Add("CheckerDataTime=null");

                tempArray = tmpa.ToArray();
            }
            #endregion
            #region 二审通过
            if (entity.Status.Id.ToString() == PTaskStatus.二审通过.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.一审通过.Id && tmpptask.Status.Id.ToString() != PTaskStatus.二审中.Id && tmpptask.Status.Id.ToString() != PTaskStatus.分配退回.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("TwoAuditDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.Add("PublisherID=null");
                tmpa.Add("PublisherName=null");
                tmpa.Add("PublisherDataTime=null");

                tmpa.Add("ExecutorID=null");
                tmpa.Add("ExecutorName=null");
                tmpa.Add("EstiStartTime=null");
                tmpa.Add("EstiEndTime=null");
                tmpa.Add("StartTime=null");
                tmpa.Add("EndTime=null");

                tmpa.Add("CheckerDataTime=null");
                tempArray = tmpa.ToArray();

            }
            #endregion
            #region 二审退回
            if (entity.Status.Id.ToString() == PTaskStatus.二审退回.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.一审通过.Id && tmpptask.Status.Id.ToString() != PTaskStatus.二审中.Id && tmpptask.Status.Id.ToString() != PTaskStatus.分配退回.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("TwoAuditDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tempArray = tmpa.ToArray();

            }
            #endregion
            #region 分配中
            if (entity.Status.Id.ToString() == PTaskStatus.分配中.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.二审通过.Id && tmpptask.Status.Id.ToString() != PTaskStatus.分配中.Id && tmpptask.Status.Id.ToString() != PTaskStatus.不执行.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("PublisherDataTime=null");
                tmpa.Add("EstiStartTime=null");
                tmpa.Add("EstiEndTime=null");
                tmpa.Add("StartTime=null");
                tmpa.Add("EndTime=null");

                tmpa.Add("CheckerDataTime=null");
                tempArray = tmpa.ToArray();               
            }
            #endregion
            #region 分配完成
            if (entity.Status.Id.ToString() == PTaskStatus.分配完成.Id)
            {

                if (tmpptask.Status.Id.ToString() != PTaskStatus.二审通过.Id && tmpptask.Status.Id.ToString() != PTaskStatus.分配中.Id && tmpptask.Status.Id.ToString() != PTaskStatus.不执行.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("PublisherDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.Add("EstiStartTime=null");
                tmpa.Add("EstiEndTime=null");
                tmpa.Add("StartTime=null");
                tmpa.Add("EndTime=null");

                tmpa.Add("CheckerDataTime=null");
                tempArray = tmpa.ToArray();

            }
            #endregion
            #region 分配退回
            if (entity.Status.Id.ToString() == PTaskStatus.分配退回.Id)
            {

                if (tmpptask.Status.Id.ToString() != PTaskStatus.二审通过.Id && tmpptask.Status.Id.ToString() != PTaskStatus.分配中.Id && tmpptask.Status.Id.ToString() != PTaskStatus.不执行.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("PublisherDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tempArray = tmpa.ToArray();
            }
            #endregion
            #region 执行中
            if (entity.Status.Id.ToString() == PTaskStatus.执行中.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.分配完成.Id && tmpptask.Status.Id.ToString() != PTaskStatus.执行中.Id && tmpptask.Status.Id.ToString() != PTaskStatus.验收退回.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("CheckerDataTime=null");
                tempArray = tmpa.ToArray();
            }
            #endregion
            #region 执行完成
            if (entity.Status.Id.ToString() == PTaskStatus.执行完成.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.分配完成.Id && tmpptask.Status.Id.ToString() != PTaskStatus.执行中.Id && tmpptask.Status.Id.ToString() != PTaskStatus.验收退回.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("CheckerDataTime=null");
                tempArray = tmpa.ToArray();
            }
            #endregion
            #region 不执行
            if (entity.Status.Id.ToString() == PTaskStatus.不执行.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.分配完成.Id && tmpptask.Status.Id.ToString() != PTaskStatus.执行中.Id && tmpptask.Status.Id.ToString() != PTaskStatus.验收退回.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
            }
            #endregion
            #region 验收中
            if (entity.Status.Id.ToString() == PTaskStatus.验收中.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.执行完成.Id && tmpptask.Status.Id.ToString() != PTaskStatus.验收中.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("CheckerDataTime=null");
                tempArray = tmpa.ToArray();
            }
            #endregion
            #region 已验收
            if (entity.Status.Id.ToString() == PTaskStatus.已验收.Id)
            {
                //if (tmpptask.Status.Id.ToString() != PTaskStatus.执行完成.Id && tmpptask.Status.Id.ToString() != PTaskStatus.验收中.Id )
                //{
                //    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                //    return false;
                //}
                tmpa.Add("CheckerDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tempArray = tmpa.ToArray();
            }
            #endregion
            #region 验收退回
            if (entity.Status.Id.ToString() == PTaskStatus.验收退回.Id)
            {
                if (tmpptask.Status.Id.ToString() != PTaskStatus.执行完成.Id && tmpptask.Status.Id.ToString() != PTaskStatus.验收中.Id)
                {
                    ErrorInfo = "任务ID：" + entity.Id + "的任务状态为：" + tmpptask.StatusName + "！";
                    return false;
                }
                tmpa.Add("CheckerDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tempArray = tmpa.ToArray();
            }
            #endregion
            
            if (this.Update(tempArray))
            {                
                PTaskStatusMessagePush(pushWeiXinMessageAction, entity.Id, entity.Status.Id.ToString(), EmpID, EmpName,null);
                return true;
            }
            return false;
        }
        public void PTaskStatusMessagePush(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long PTaskId, string PTaskStatusId, long EmpID, string EmpName, PTask entity)
        {
            List<long> receiveidlist = new List<long>();
            string message = "";
            PTask ptask = entity;
            if (ptask == null)
            {
                ptask = DBDao.Get(PTaskId);
            }
            string url = "WeiXin/WeiXinMainRouter.aspx?operate=TASKINFO&id=" + PTaskId+ "&IsSingle=1" + "&name="+ptask.CName;
            #region 申请
            if (PTaskStatusId.Trim() == PTaskStatus.暂存.Id)
            {
                if (ptask.ApplyID.HasValue)
                {
                    //receiveidlist.Add(ptask.ApplyID.Value);
                    //message = "您申请的任务：" + ptask.CName + "已被" + EmpName + "定为'暂存'状态。";
                }
            }
            if (PTaskStatusId.Trim() == PTaskStatus.申请.Id)
            {
                if (ptask.OneAuditID.HasValue)
                {
                    receiveidlist.Add(ptask.OneAuditID.Value);
                    string applytime = ptask.ApplyDataTime.HasValue ? ptask.ApplyDataTime.Value.ToString("yyyy-MM-dd") : "";
                    message = "您有一个待一审的任务：" + ptask.CName + ",申请人:" + ptask.ApplyName + ",申请时间:" + applytime + "。";
                    url += "&ExportType=1";
                }
            }
            #endregion
            #region 一审
            if (PTaskStatusId.Trim() == PTaskStatus.一审中.Id)
            {
                if (ptask.ApplyID.HasValue)
                {
                    receiveidlist.Add(ptask.ApplyID.Value);
                    message = "您申请的任务：" + ptask.CName + "已被" + ptask.OneAuditName + "定为'一审中'状态。";
                    url += "&ExportType=0";
                }
            }
            if (PTaskStatusId.Trim() == PTaskStatus.一审退回.Id)
            {
                if (ptask.ApplyID.HasValue)
                {
                    receiveidlist.Add(ptask.ApplyID.Value);
                    message = "您申请的任务：" + ptask.CName + "已被" + ptask.OneAuditName + "定为'一审退回'状态。";
                    url += "&ExportType=0";
                }
            }
            if (PTaskStatusId.Trim() == PTaskStatus.一审通过.Id)
            {
                if (ptask.TwoAuditID.HasValue)
                {
                    receiveidlist.Add(ptask.TwoAuditID.Value);
                    message = "您有一个待二审的任务：" + ptask.CName + "已被" + ptask.OneAuditName + "定为'一审通过'状态。";
                    url += "&ExportType=2";
                }
                else
                {
                    if (ptask.PTypeID.HasValue)
                    {
                        IList<PTaskTypeEmpLink> pttellist = IDPTaskTypeEmpLinkDao.GetListByHQL(" TaskTypeID=" + ptask.PTypeID + " and TwoAudit=true ");
                        if (pttellist != null && pttellist.Count > 0)
                        {
                            foreach (var a in pttellist)
                            {
                                receiveidlist.Add(a.EmpID);
                            }
                        }
                        message = "您有一个待二审的任务：" + ptask.CName + "已被" + ptask.OneAuditName + "定为'一审通过'状态。";
                        url += "&ExportType=2";
                    }
                }
            }
            #endregion
            #region 二审
            if (PTaskStatusId.Trim() == PTaskStatus.二审中.Id)
            {
                if (ptask.OneAuditID.HasValue)
                {
                    receiveidlist.Add(ptask.OneAuditID.Value);
                    message = "您一审通过的任务：" + ptask.CName + "已被" + ptask.TwoAuditName + "定为'二审中'状态。";
                    url += "&ExportType=0";
                }
            }
            if (PTaskStatusId.Trim() == PTaskStatus.二审退回.Id)
            {
                if (ptask.OneAuditID.HasValue)
                {
                    receiveidlist.Add(ptask.OneAuditID.Value);
                    message = "您一审通过的任务：" + ptask.CName + "已被" + ptask.TwoAuditName + "定为'二审退回'状态。";
                    url += "&ExportType=1";
                }
            }
            if (PTaskStatusId.Trim() == PTaskStatus.二审通过.Id)
            {
                if (ptask.PublisherID.HasValue)
                {
                    receiveidlist.Add(ptask.PublisherID.Value);
                    message = "您有一个待分配的任务：" + ptask.CName + "已被" + ptask.TwoAuditName + "定为'二审通过'状态。";
                    url += "&ExportType=3";
                }
                else
                {
                    if (ptask.PTypeID.HasValue)
                    {
                        IList<PTaskTypeEmpLink> pttellist = IDPTaskTypeEmpLinkDao.GetListByHQL(" TaskTypeID=" + ptask.PTypeID + " and Publish=true ");
                        if (pttellist != null && pttellist.Count > 0)
                        {
                            foreach (var a in pttellist)
                            {
                                receiveidlist.Add(a.EmpID);
                            }
                        }
                        message = "您有一个待分配的任务：" + ptask.CName + "已被" + ptask.TwoAuditName + "定为'二审通过'状态。";
                        url += "&ExportType=3";
                    }
                }
            }
            #endregion
            #region 分配
            if (PTaskStatusId.Trim() == PTaskStatus.分配中.Id)
            {
                if (ptask.TwoAuditID.HasValue)
                {
                    receiveidlist.Add(ptask.TwoAuditID.Value);
                    message = "您二审通过的任务：" + ptask.CName + "已被" + ptask.PublisherName + "定为'分配中'状态。";
                    url += "&ExportType=0";
                }
            }
            if (PTaskStatusId.Trim() == PTaskStatus.分配退回.Id)
            {
                if (ptask.TwoAuditID.HasValue)
                {
                    receiveidlist.Add(ptask.TwoAuditID.Value);
                    message = "您二审通过的任务：" + ptask.CName + "已被" + ptask.PublisherName + "定为'分配退回'状态。";
                    url += "&ExportType=2";
                }
            }
            if (PTaskStatusId.Trim() == PTaskStatus.分配完成.Id)
            {
                if (ptask.ExecutorID.HasValue)
                {
                    receiveidlist.Add(ptask.ExecutorID.Value);
                    message = "您有一个待执行的任务：" + ptask.CName + "已被" + ptask.PublisherName + "定为'分配完成'状态。";
                    url += "&ExportType=4";
                }
            }
            #endregion
            #region 执行
            if (PTaskStatusId.Trim() == PTaskStatus.执行中.Id)
            {
                if (ptask.PublisherID.HasValue)
                {
                    receiveidlist.Add(ptask.PublisherID.Value);
                    message = "您分配的任务：" + ptask.CName + "已被" + ptask.ExecutorName + "定为'执行中'状态。";
                    url += "&ExportType=0";
                }
            }
            if (PTaskStatusId.Trim() == PTaskStatus.不执行.Id)
            {
                if (ptask.PublisherID.HasValue)
                {
                    receiveidlist.Add(ptask.PublisherID.Value);
                    message = "您分配的任务：" + ptask.CName + "已被" + ptask.ExecutorName + "定为'不执行'状态。";
                    url += "&ExportType=3";
                }
            }
            if (PTaskStatusId.Trim() == PTaskStatus.执行完成.Id)
            {
                if (ptask.CheckerID.HasValue)
                {
                    receiveidlist.Add(ptask.CheckerID.Value);
                    message = "您有一个待验收的任务：" + ptask.CName + "已被" + ptask.ExecutorName + "定为'执行完成'状态。";
                    url += "&ExportType=5";
                }
            }
            #endregion
            #region 验收
            if (PTaskStatusId.Trim() == PTaskStatus.验收中.Id)
            {
                if (ptask.ExecutorID.HasValue)
                {
                    receiveidlist.Add(ptask.ExecutorID.Value);
                    message = "您执行的任务：" + ptask.CName + "已被" + ptask.CheckerName + "定为'验收中'状态。";
                    url += "&ExportType=0";
                }
            }
            if (PTaskStatusId.Trim() == PTaskStatus.验收退回.Id)
            {
                if (ptask.ExecutorID.HasValue)
                {
                    receiveidlist.Add(ptask.ExecutorID.Value);
                    message = "您执行的任务：" + ptask.CName + "已被" + ptask.CheckerName + "定为'验收退回'状态。";
                    url += "&ExportType=4";
                }
            }
            if (PTaskStatusId.Trim() == PTaskStatus.已验收.Id)
            {
                if (ptask.ApplyID.HasValue)
                {
                    receiveidlist.Add(ptask.ApplyID.Value);
                    message = "您申请的任务：" + ptask.CName + "已被" + ptask.CheckerName + "定为'已验收'状态。";
                    url += "&ExportType=0";
                }
            }
            #endregion
            #region 已终止
            if (PTaskStatusId.Trim() == PTaskStatus.已终止.Id)
            {
                if (ptask.ApplyID.HasValue)
                {
                    receiveidlist.Add(ptask.ApplyID.Value);
                    if (ptask.PublisherName != null)
                    {
                        message = "您申请的任务：" + ptask.CName + "已被" + ptask.PublisherName + "定为'已终止'状态。";
                    }
                    else
                    {
                        message = "您申请的任务：" + ptask.CName + "已被" + ptask.ApplyName + "定为'已终止'状态。";
                    }
                    url += "&ExportType=0";
                }
            }
            #endregion           
            if (receiveidlist.Count > 0)
            {
                Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                string urgencycolor = "#11cd6e";
                if (ptask.UrgencyName != null)
                {
                    if (ptask.UrgencyName.Trim() == "紧急重要")
                    {
                        urgencycolor = "#d81e06";
                    }
                    if (ptask.UrgencyName.Trim() == "紧急不重要")
                    {
                        urgencycolor = "#ea8010";
                    }
                    if (ptask.UrgencyName.Trim() == "不紧急重要")
                    {
                        urgencycolor = "#ea8010";
                    }
                }

                data.Add("first", new TemplateDataObject() { color = urgencycolor, value = "任务名称：" + ptask.CName + "(" + ptask.UrgencyName + ")" });
                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = ptask.Contents.Length <= 15 ? ptask.Contents : ptask.Contents.Substring(0, 15) + "..." });
                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = ptask.Workload.ToString() });
                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = ptask.Memo });
                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = ptask.ReqEndTime.HasValue ? ptask.ReqEndTime.Value.ToString("yyyy-MM-dd") : "" });
                data.Add("keyword5", new TemplateDataObject() { color = "#000000", value = ptask.ApplyName });
                data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = message }); 
                IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, receiveidlist, data, "task", url);
            }
        }
    }
}