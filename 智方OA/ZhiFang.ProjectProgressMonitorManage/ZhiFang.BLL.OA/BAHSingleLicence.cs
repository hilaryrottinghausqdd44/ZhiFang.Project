using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.OA;
using ZhiFang.Entity.OA;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.IBLL.OA;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;
using ZhiFang.IDAO.ProjectProgressMonitorManage;

namespace ZhiFang.BLL.OA
{
    /// <summary>
    ///
    /// </summary>
    public class BAHSingleLicence : BaseBLL<AHSingleLicence>, ZhiFang.IBLL.OA.IBAHSingleLicence
    {
        public IBAHOperation IBAHOperation { get; set; }

        public IDPClientDao IDPClientDao { get; set; }
        // public IBBParameter IBBParameter { get; set; }
        public IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }
        public BaseResultDataValue AHSingleLicenceAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;
            if (this.Entity.StartDate.HasValue && this.Entity.EndDate.HasValue)
            {
                DateTime startDate = DateTime.Parse(this.Entity.StartDate.Value.ToString("yyyy-MM-dd"));
                DateTime endDate = DateTime.Parse(this.Entity.EndDate.Value.ToString("yyyy-MM-dd"));
                if (endDate.CompareTo(startDate) < 0)
                {
                    brdv.ErrorInfo = "授权结束时间小于开始时间！";
                    brdv.success = false;
                }

            }
            if (brdv.success)
            {
                if (base.Add())
                {
                    SaveAHOperation(this.Entity);
                    if (Entity.Status.ToString() == LicenceStatus.申请.Key)
                        AHSingleLicenceStatusMessagePush(pushWeiXinMessageAction, this.Entity.Id, this.Entity.Status.ToString(), this.Entity);
                }
                else
                {
                    brdv.ErrorInfo = "AHSingleLicenceAdd.Add错误！";
                    brdv.success = false;
                }
            }

            return brdv;
        }
        public BaseResultBool AHSingleLicenceStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long EmpID, string EmpName)
        {
            BaseResultBool brb = new BaseResultBool();
            AHSingleLicence entity = this.Entity;
            var tmpa = tempArray.ToList();
            AHSingleLicence tmpServerEntity = new AHSingleLicence();
            tmpServerEntity = DBDao.Get(entity.Id);
            if (tmpServerEntity == null)
            {
                brb.ErrorInfo = "单站点授权ID：" + entity.Id + "为空！";
                brb.success = false;
                return brb;
            }

            if (!AHSingleLicenceStatusUpdateCheck(entity, tmpServerEntity, EmpID, EmpName, tmpa))
            {
                return new BaseResultBool() { ErrorInfo = "单站点授权ID：" + entity.Id + "的状态为：" + LicenceStatus.GetStatusDic()[tmpServerEntity.Status.ToString()].Name + "！", success = false };
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                SaveAHOperation(entity);

                AHSingleLicenceStatusMessagePush(pushWeiXinMessageAction, entity.Id, entity.Status.ToString(), null);
                brb.success = true;
            }
            else
            {
                brb.ErrorInfo = "AHSingleLicenceStatusUpdate.Update错误！";
                brb.success = false;
            }
            return brb;
        }
        bool AHSingleLicenceStatusUpdateCheck(AHSingleLicence updateEntity, AHSingleLicence tmpServerEntity, long EmpID, string EmpName, List<string> tmpa)
        {
            //暂丰及一审时可以修改
            if (updateEntity.Status.ToString() != LicenceStatus.暂存.Key && updateEntity.Status.ToString() != LicenceStatus.商务授权通过.Key)
            {
                updateEntity.PClientID = tmpServerEntity.PClientID;
                updateEntity.PClientName = tmpServerEntity.PClientName;
                updateEntity.ProgramID = tmpServerEntity.ProgramID;
                updateEntity.ProgramName = tmpServerEntity.ProgramName;
                updateEntity.EquipID = tmpServerEntity.EquipID;
                updateEntity.EquipName = tmpServerEntity.EquipName;
                updateEntity.SQH = tmpServerEntity.SQH;
                updateEntity.StartDate = tmpServerEntity.StartDate;
                updateEntity.EndDate = tmpServerEntity.EndDate;
                updateEntity.LicenceTypeId = tmpServerEntity.LicenceTypeId;
            }
            //IsCharLicenceByMAC在一审时赋值
            if (updateEntity.Status.ToString() != LicenceStatus.商务授权通过.Key)
            {
                updateEntity.IsCharLicenceByMAC = tmpServerEntity.IsCharLicenceByMAC;
            }

            #region 暂存
            if (updateEntity.Status.ToString() == LicenceStatus.暂存.Key)
            {
                if (tmpServerEntity.Status.ToString() != LicenceStatus.暂存.Key && tmpServerEntity.Status.ToString() != LicenceStatus.申请.Key && tmpServerEntity.Status.ToString() != LicenceStatus.商务授权退回.Key)
                {
                    return false;
                }
            }
            #endregion
            #region 申请
            if (updateEntity.Status.ToString() == LicenceStatus.申请.Key)
            {
                if (tmpServerEntity.Status.ToString() != LicenceStatus.暂存.Key && tmpServerEntity.Status.ToString() != LicenceStatus.商务授权退回.Key)
                {
                    return false;
                }

                tmpa.Add("ApplyID=" + EmpID + " ");
                tmpa.Add("ApplyName='" + EmpName + "'");
                tmpa.Add("ApplyDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("OneAuditID=null");
                tmpa.Add("OneAuditName=null");
                tmpa.Add("OneAuditDataTime=null");
                tmpa.Add("OneAuditInfo=null");

                tmpa.Add("TwoAuditID=null");
                tmpa.Add("TwoAuditName=null");
                tmpa.Add("TwoAuditDataTime=null");
                tmpa.Add("TwoAuditInfo=null");
                tmpa.Add("GenDateTime=null");
                tmpa.Add("LicenceKey=null");
            }
            #endregion

            #region 商务授权通过
            if (updateEntity.Status.ToString() == LicenceStatus.商务授权通过.Key)
            {
                if (tmpServerEntity.Status.ToString() != LicenceStatus.申请.Key && tmpServerEntity.Status.ToString() != LicenceStatus.特批授权退回.Key)
                {
                    return false;
                }
                tmpa.Add("OneAuditID=" + EmpID + " ");
                tmpa.Add("OneAuditName='" + EmpName + "'");
                tmpa.Add("OneAuditDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("OneAuditInfo='" + updateEntity.OneAuditInfo + "'");

                tmpa.Add("TwoAuditID=null");
                tmpa.Add("TwoAuditName=null");
                tmpa.Add("TwoAuditDataTime=null");
                tmpa.Add("TwoAuditInfo=null");

                tmpa.Add("IsCharLicenceByMAC=" + updateEntity.IsCharLicenceByMAC);

                if (!updateEntity.LicenceTypeId.HasValue)
                {
                    updateEntity.LicenceTypeId = tmpServerEntity.LicenceTypeId;
                }
                //判断是否需要特批(暂时只判断授权类型为临时),如果不需要特批,直接根据SQH和网卡号生成授权码
                bool isSpecialApproval = CheckIsSpecialApproval(updateEntity);
                if (isSpecialApproval == false)
                {
                    string licenceKey = GetCreateLicenceKey(updateEntity);
                    tmpa.Add("GenDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    tmpa.Add("LicenceKey='" + licenceKey + "'");
                }
                else
                {
                    tmpa.Add("GenDateTime=null");
                    tmpa.Add("LicenceKey=null");
                }
            }
            #endregion

            #region 商务授权退回
            if (updateEntity.Status.ToString() == LicenceStatus.商务授权退回.Key)
            {
                if (tmpServerEntity.Status.ToString() != LicenceStatus.申请.Key && tmpServerEntity.Status.ToString() != LicenceStatus.特批授权退回.Key)
                {
                    return false;
                }
                tmpa.Add("OneAuditID=" + EmpID + " ");
                tmpa.Add("OneAuditName='" + EmpName + "'");
                tmpa.Add("OneAuditDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("OneAuditInfo='" + updateEntity.OneAuditInfo + "'");
                tmpa.Add("GenDateTime=null");
                tmpa.Add("LicenceKey=null");
            }
            #endregion

            #region 特批授权通过
            if (updateEntity.Status.ToString() == LicenceStatus.特批授权通过.Key)
            {
                if (tmpServerEntity.Status.ToString() != LicenceStatus.商务授权通过.Key)
                {
                    return false;
                }
                tmpa.Add("TwoAuditID=" + EmpID + " ");
                tmpa.Add("TwoAuditName='" + EmpName + "'");
                tmpa.Add("TwoAuditDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("TwoAuditInfo='" + updateEntity.TwoAuditInfo + "'");
                tmpa.Add("GenDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                //根据SQH和网卡号生成授权码
                string licenceKey = GetCreateLicenceKey(updateEntity);
                tmpa.Add("LicenceKey='" + licenceKey + "'");
            }
            #endregion
            #region 特批授权退回
            if (updateEntity.Status.ToString() == LicenceStatus.特批授权退回.Key)
            {
                if (tmpServerEntity.Status.ToString() != LicenceStatus.商务授权通过.Key)
                {
                    return false;
                }
                tmpa.Add("TwoAuditID=" + EmpID + " ");
                tmpa.Add("TwoAuditName='" + EmpName + "'");
                tmpa.Add("TwoAuditDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("TwoAuditInfo='" + updateEntity.TwoAuditInfo + "'");
                tmpa.Add("GenDateTime=null");
                tmpa.Add("LicenceKey=null");

            }
            #endregion

            return true;
        }

        private void AHSingleLicenceStatusMessagePush(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long id, string StatusId, AHSingleLicence entity)
        {
            List<long> receiveidlist = new List<long>();
            string message = "";
            string sendmen = "";
            AHSingleLicence ahsinglelicence = entity;
            if (ahsinglelicence == null)
                ahsinglelicence = DBDao.Get(id);
            //string url = "WeiXin/WeiXinMainRouter.aspx?operate=TASKINFO&id=" + PTaskId + "&IsSingle=1" + "&name=" + ptask.CName;
            string tmpstr = "";
            if (ahsinglelicence.ProgramName != null && ahsinglelicence.ProgramName.Trim() != "")
            {
                tmpstr = ahsinglelicence.ProgramName;
            }
            else
            {
                tmpstr = ahsinglelicence.EquipName;
            }
            ahsinglelicence.LicenceTypeName = LicenceType.GetStatusDic()[ahsinglelicence.LicenceTypeId.Value.ToString()].Name;
            #region 申请
            if (StatusId.Trim() == LicenceStatus.暂存.Key)
            {

            }
            if (StatusId.Trim() == LicenceStatus.申请.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id in( " + RoleList.授权审核.Key + ") ");
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }
                message = "您收到待审核的授权申请（客户名称：" + ahsinglelicence.PClientName + "，程序、仪器名称：" + tmpstr + "，授权类型：" + ahsinglelicence.LicenceTypeName + "），申请人：" + ahsinglelicence.ApplyName + "。";
            }
            #endregion

            #region 商务授权通过
            if (StatusId.Trim() == LicenceStatus.商务授权通过.Key)
            {
                receiveidlist.Add(ahsinglelicence.ApplyID.Value);

                if (ahsinglelicence.LicenceKey != null && ahsinglelicence.LicenceKey.Trim() != "")
                {
                    message = "您的授权申请（客户名称：" + ahsinglelicence.PClientName + "，程序、仪器名称：" + tmpstr + "，授权类型：" + ahsinglelicence.LicenceTypeName + "，授权码：" + ahsinglelicence.LicenceKey + "）,已被" + ahsinglelicence.OneAuditName + "定为'商务授权通过'状态。";
                }
                else
                {
                    message = "您的授权申请（客户名称：" + ahsinglelicence.PClientName + "，程序、仪器名称：" + tmpstr + "，授权类型：" + ahsinglelicence.LicenceTypeName + "）,已被" + ahsinglelicence.OneAuditName + "定为'商务授权通过'状态。";
                }
            }
            #endregion

            #region 商务授权退回
            if (StatusId.Trim() == LicenceStatus.商务授权退回.Key)
            {
                receiveidlist.Add(ahsinglelicence.ApplyID.Value);
                message = "您的授权申请（客户名称：" + ahsinglelicence.PClientName + "，程序、仪器名称：" + tmpstr + "，授权类型：" + ahsinglelicence.LicenceTypeName + "）,已被" + ahsinglelicence.OneAuditName + "定为'商务授权退回'状态。";
            }
            #endregion

            #region 特批授权通过
            if (StatusId.Trim() == LicenceStatus.特批授权通过.Key)
            {
                receiveidlist.Add(ahsinglelicence.ApplyID.Value);
                if (ahsinglelicence.LicenceKey != null && ahsinglelicence.LicenceKey.Trim() != "")
                {
                    message = "您的授权申请（客户名称：" + ahsinglelicence.PClientName + "，程序、仪器名称：" + tmpstr + "，授权类型：" + ahsinglelicence.LicenceTypeName + "，授权码：" + ahsinglelicence.LicenceKey + "）,已被" + ahsinglelicence.TwoAuditName + "定为'商务授权通过'状态。";
                }
                else
                {
                    message = "您的授权申请（客户名称：" + ahsinglelicence.PClientName + "，程序、仪器名称：" + tmpstr + "，授权类型：" + ahsinglelicence.LicenceTypeName + "）,已被" + ahsinglelicence.TwoAuditName + "定为'商务授权通过'状态。";
                }
            }
            #endregion

            #region 特批授权退回
            if (StatusId.Trim() == LicenceStatus.特批授权退回.Key)
            {
                receiveidlist.Add(ahsinglelicence.OneAuditID.Value);
                message = "您的授权申请（客户名称：" + ahsinglelicence.PClientName + "，程序、仪器名称：" + tmpstr + "，授权类型：" + ahsinglelicence.LicenceTypeName + "）,已被" + ahsinglelicence.TwoAuditName + "定为'特批授权退回'状态。";
            }
            #endregion

            #region 授权完成
            if (StatusId.Trim() == LicenceStatus.授权完成.Key)
            {
                receiveidlist.Add(ahsinglelicence.ApplyID.Value);
                message = "您的授权申请（客户名称：" + ahsinglelicence.PClientName + "，程序、仪器名称：" + tmpstr + "，授权类型：" + ahsinglelicence.LicenceTypeName + "，授权码：" + ahsinglelicence.LicenceKey + "）,已被" + ahsinglelicence.OneAuditName + "定为'商务授权通过'状态。";
            }
            #endregion
            if (receiveidlist.Count > 0)
            {
                ZhiFang.Common.Log.Log.Debug("PContractStatusMessagePush.receiveidlist.Count:" + receiveidlist.Count);
                Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                string urgencycolor = "#11cd6e";
                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你收到授权申请信息" });
                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = message });
                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = "OA系统：授权申请" });
                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = ahsinglelicence.ApplyName });
                string tmpdatetime = (ahsinglelicence.DataAddTime.HasValue) ? ahsinglelicence.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = tmpdatetime });
                data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "请登录OA查看" });
                //ZhiFang.Common.Log.Log.Debug("PContractStatusMessagePush.receiveidlist.Count:@" + receiveidlist.Count);
                IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, receiveidlist, data, "licence", "");
                //ZhiFang.Common.Log.Log.Debug("PContractStatusMessagePush.receiveidlist.Count:@@" + receiveidlist.Count);
            }
        }
        public override AHSingleLicence Get(long longID)
        {
            return SetStatusAndLicenceStatus(base.Get(longID));
        }
        public override EntityList<AHSingleLicence> SearchListByHQL(string strHqlWhere, int page, int count)
        {
            var list = base.SearchListByHQL(strHqlWhere, page, count);
            if (list != null && list.list.Count > 0)
            {
                for (int i = 0; i < list.list.Count; i++)
                {
                    list.list[i] = SetStatusAndLicenceStatus(list.list[i]);
                }
            }
            return list;
        }
        public override EntityList<AHSingleLicence> SearchListByHQL(string strHqlWhere, string sort, int page, int count)
        {
            var list = base.SearchListByHQL(strHqlWhere, sort, page, count);
            if (list != null && list.list.Count > 0)
            {
                for (int i = 0; i < list.list.Count; i++)
                {
                    list.list[i] = SetStatusAndLicenceStatus(list.list[i]);
                }
            }
            return list;
        }
        public AHSingleLicence SetStatus(AHSingleLicence p)
        {
            if (p == null)
            {
                return null;
            }
            if (p.Status > 0)
            {
                var statuslist = LicenceStatus.GetStatusDic();
                if (statuslist.Keys.Contains(p.Status.ToString()))
                {
                    p.StatusName = statuslist[p.Status.ToString()].Name;
                    p.TwoStatusName = p.StatusName;
                }

                if (p.Status.ToString() == LicenceStatus.申请.Key)
                {
                    p.TwoStatusName = statuslist[LicenceStatus.授权中.Key.ToString()].Name;
                }
                else if (p.Status.ToString() == LicenceStatus.商务授权退回.Key)
                {
                    p.TwoStatusName = statuslist[LicenceStatus.授权中.Key.ToString()].Name;
                }
                else if (p.Status.ToString() == LicenceStatus.特批授权退回.Key)
                {
                    p.TwoStatusName = statuslist[LicenceStatus.授权中.Key.ToString()].Name;
                }
                else if (p.Status.ToString() == LicenceStatus.商务授权通过.Key)
                {
                    bool isSpecialApproval = CheckIsSpecialApproval(p);
                    if (isSpecialApproval)
                    {
                        p.TwoStatusName = statuslist[LicenceStatus.特批授权中.Key.ToString()].Name;
                    }
                    else
                    {
                        p.TwoStatusName = statuslist[(LicenceStatus.授权完成.Key.ToString())].Name;
                    }
                }
                else if (p.Status.ToString() == LicenceStatus.特批授权通过.Key)
                {
                    p.TwoStatusName = statuslist[LicenceStatus.授权完成.Key.ToString()].Name;
                }
                else
                {
                    p.TwoStatusName = p.StatusName;
                }
            }
            return p;
        }
        /// <summary>
        /// 有效期状态处理
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private AHSingleLicence SetStatusAndLicenceStatus(AHSingleLicence entity)
        {
            entity = SetStatus(entity);
            if (entity.LicenceTypeId.ToString() != LicenceType.商业.Key)
            {
                //是否到期提醒处理 授权状态：1有效、2警告（10天内到期）、3失效
                string startDate = "", endDate = "";
                if (entity.StartDate.HasValue && entity.EndDate.HasValue)
                {
                    //startDate = entity.StartDate.Value.ToString("yyyy-MM-dd");
                    startDate = DateTime.Now.ToString("yyyy-MM-dd");
                    endDate = entity.EndDate.Value.ToString("yyyy-MM-dd");
                    TimeSpan d1 = DateTime.Parse(endDate).Subtract(DateTime.Parse(startDate));
                    entity.CalcDays = d1.Days;
                    //if (d1.Days >= 10)
                    //{
                    //    entity.LicenceStatusId = 1;
                    //    entity.LicenceStatusName = "有效";
                    //}
                    //else if (d1.Days > 0 && d1.Days < 10)
                    //{
                    //    entity.LicenceStatusId = 2;
                    //    entity.LicenceStatusName = "10天内到期";
                    //}
                    //else
                    //{
                    //    entity.LicenceStatusId = 3;
                    //    entity.LicenceStatusName = "失效";
                    //}
                }
            }
            else
            {
                entity.LicenceStatusId = 1;
                entity.LicenceStatusName = "有效";
            }
            return entity;
        }
        /// <summary>
        /// 获取单站点需要特批的数据
        /// 同一用户,同一网卡.对同一程序或仪器,它临时授权天数(包括当前)累计超过110天,需要特批
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public EntityList<AHSingleLicence> SearchSpecialApprovalAHSingleLicenceByHQL(string where, int page, int limit, string sort)
        {
            EntityList<AHSingleLicence> tempEntityList = new EntityList<AHSingleLicence>();
            EntityList<AHSingleLicence> resultEntityList = new EntityList<AHSingleLicence>();
            resultEntityList.list = new List<AHSingleLicence>();
            resultEntityList.count = 0;
            //单站点需要特批的默认条件处理
            string hqlWhere = "";

            if (String.IsNullOrEmpty(hqlWhere))
            {
                hqlWhere = where;
            }
            else if (!String.IsNullOrEmpty(where) && !String.IsNullOrEmpty(hqlWhere))
            {
                hqlWhere = hqlWhere + " and " + where;
            }
            resultEntityList = ((IDAHSingleLicenceDao)base.DBDao).SearchSpecialApprovalAHSingleLicenceByHQL(hqlWhere, page, limit, sort);
            #region 2017-3-23
            //tempEntityList = ((IDAHSingleLicenceDao)base.DBDao).SearchSpecialApprovalAHSingleLicenceByHQL(hqlWhere, 0, 0, sort);
            //bool isSpecialApproval = false;
            //if (tempEntityList.list != null && tempEntityList.count > 0)
            //{
            //    foreach (AHSingleLicence entity in tempEntityList.list)
            //    {
            //        //if (entity.LicenceTypeId.ToString() == LicenceType.临时.Key)
            //        // {
            //        isSpecialApproval = CheckIsSpecialApproval(entity);
            //        if (isSpecialApproval)
            //        {
            //            AHSingleLicence tempEntity = SetStatusAndLicenceStatus(entity);
            //            resultEntityList.list.Add(tempEntity);
            //        }
            //        //}
            //    }
            //    #region 分页处理
            //    if (resultEntityList.list != null)
            //    {
            //        resultEntityList.count = resultEntityList.list.Count;
            //        if (limit < resultEntityList.list.Count)
            //        {
            //            int startIndex = limit * (page - 1);
            //            int endIndex = limit;
            //            var list = resultEntityList.list.Skip(startIndex).Take(endIndex);
            //            if (list != null)
            //            {
            //                resultEntityList.list = list.ToList();
            //            }
            //        }
            //    }
            //    #endregion
            //} 
            #endregion
            return resultEntityList;
        }
        /// <summary>
        /// 同一用户,同一网卡.对同一程序或仪器,它临时授权天数(包括当前)累计超过110天,需要特批
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool CheckIsSpecialApproval(AHSingleLicence entity)
        {
            if (entity.LicenceTypeId.ToString() == LicenceType.商业.Key)
            {
                return false;
            }
            bool isSpecialApproval = false;

            int days = 0;
            string startDate = "", endDate = "";

            if (entity.StartDate.HasValue && entity.EndDate.HasValue)
            {
                startDate = entity.StartDate.Value.ToString("yyyy-MM-dd");
                endDate = entity.EndDate.Value.ToString("yyyy-MM-dd");
                TimeSpan d1 = DateTime.Parse(endDate).Subtract(DateTime.Parse(startDate));
                days = days + d1.Days;
                //ZhiFang.Common.Log.Log.Debug("临时授权天数一共计有:" + days);
            }
            //如果当前申请的授权天数已经超过110天
            if (days >= 110)
            {
                isSpecialApproval = true;
            }
            else
            {
                StringBuilder strb = new StringBuilder();
                strb.Append(" Id!=" + entity.Id);

                strb.Append((String.IsNullOrEmpty(entity.SQH) ? " and SQH is null" : " and SQH='" + entity.SQH + "'"));
                strb.Append((entity.PClientID.HasValue ? " and PClientID=" + entity.PClientID : " and PClientID is null"));
                strb.Append((String.IsNullOrEmpty(entity.MacAddress) ? " and MacAddress is null" : " and MacAddress='" + entity.MacAddress + "'"));
                strb.Append((entity.EquipID.HasValue ? " and EquipID=" + entity.EquipID : " and EquipID is null"));
                strb.Append((entity.ProgramID.HasValue ? " and ProgramID=" + entity.ProgramID : " and ProgramID is null"));
                //授权类型ahsinglelicence.
                strb.Append(" and LicenceTypeId=" + LicenceType.临时.Key);

                strb.Append(" and (LicenceKey is not null or LicenceKey!=null)");
                //strb.Append(" and ((Status=" + LicenceStatus.授权完成.Key+ ") or (Status=" + LicenceStatus.商务授权通过.Key+ " and LicenceKey is not null) or (Status=" + LicenceStatus.特批授权通过.Key + " and LicenceKey is not null))");
                IList<AHSingleLicence> tempEntityList = ((IDAHSingleLicenceDao)base.DBDao).GetListByHQL(strb.ToString());
                if (tempEntityList.Count > 0)
                {
                    foreach (AHSingleLicence item in tempEntityList)
                    {
                        if (isSpecialApproval == false && item.StartDate.HasValue && item.EndDate.HasValue)
                        {
                            startDate = item.StartDate.Value.ToString("yyyy-MM-dd");
                            endDate = item.EndDate.Value.ToString("yyyy-MM-dd");
                            TimeSpan d3 = DateTime.Parse(endDate).Subtract(DateTime.Parse(startDate));
                            days = days + d3.Days;
                            if (days >= 110)
                                isSpecialApproval = true;
                        }
                    }
                }
                //ZhiFang.Common.Log.Log.Debug("临时授权天数共计有:" + days);
                if (days >= 110)
                    isSpecialApproval = true;
            }
            return isSpecialApproval;
        }
        /// <summary>
        /// 授权操作记录登记
        /// </summary>
        /// <param name="entityLicence"></param>
        private void SaveAHOperation(AHSingleLicence entityLicence)
        {
            AHSingleLicence entity = this.Entity;
            if (entityLicence != null)
            {
                entity = entityLicence;
            }
            if (entity.Status.ToString() != LicenceStatus.暂存.Key)
            {
                AHOperation sco = new AHOperation();
                sco.BobjectID = entity.Id;
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid != null && empid.Trim() != "")
                    sco.CreatorID = long.Parse(empid);
                if (empname != null && empname.Trim() != "")
                    sco.CreatorName = empname;
                sco.BusinessModuleCode = "AHSingleLicence";
                sco.Memo = entity.OperationMemo;

                sco.Type = entity.Status;
                sco.TypeName = LicenceStatus.GetStatusDic()[entity.Status.ToString()].Name;
                IBAHOperation.Entity = sco;
                IBAHOperation.Add();
            }
        }
        /// <summary>
        /// 根据SQH和网卡号生成授权码
        /// </summary>
        /// <param name="entity"></param>
        public string GetCreateLicenceKey(AHSingleLicence entity)
        {
            try
            {
                AHSingleLicence tmpentity = DBDao.Get(entity.Id);
                long typeid = 2;
                DateTime dtstart = DateTime.Now;
                DateTime dtend = DateTime.Now;
                OALSR.OALClient tmp = new OALSR.OALClient();

                //暂定是全数字授权码
                if (tmpentity.LicenceTypeId.HasValue)
                {
                    typeid = tmpentity.LicenceTypeId.Value;
                    if (typeid == 1)
                    {

                    }
                    else
                    {
                        if (tmpentity.StartDate.HasValue)
                        {
                            dtstart = tmpentity.StartDate.Value;
                        }
                        if (tmpentity.EndDate.HasValue)
                        {
                            dtend = tmpentity.EndDate.Value;
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug("GetCreateLicenceKey错误,EndDate为空！");
                            return "";
                        }
                    }
                    ZhiFang.Common.Log.Log.Debug("GetCreateLicenceKey参数：typeid=" + typeid.ToString() + ";tmpentity.MacAddress=" + tmpentity.MacAddress.Replace("-", "") + ";tmpentity.SQH=" + tmpentity.SQH + ";dtstart=" + dtstart.ToString("yyyy-MM-dd") + ";dtend=" + dtend.ToString("yyyy-MM-dd"));
                    //string licenceKey = "TEST100011";
                    string licenceKey = "";
                    //使用新版本授权:GetNumLicenceByMAC;使用老版本授权:GetCharLicenceByMAC
                    if (entity.IsCharLicenceByMAC == true)
                    {
                        licenceKey = tmp.GetCharLicenceByMAC(typeid.ToString(), tmpentity.MacAddress.Replace("-", ""), tmpentity.SQH, dtstart.ToString("yyyy-MM-dd"), dtend.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        licenceKey = tmp.GetNumLicenceByMAC(typeid.ToString(), tmpentity.MacAddress.Replace("-", ""), tmpentity.SQH, dtstart.ToString("yyyy-MM-dd"), dtend.ToString("yyyy-MM-dd"));
                    }
                    switch (licenceKey.Trim())
                    {
                        case "-1": ZhiFang.Common.Log.Log.Debug("GetCreateLicenceKey错误:" + licenceKey); return "";
                        case "-2": ZhiFang.Common.Log.Log.Debug("GetCreateLicenceKey错误:" + licenceKey); return "";
                        case "-3": ZhiFang.Common.Log.Log.Debug("GetCreateLicenceKey错误:" + licenceKey); return "";
                    }
                    return licenceKey;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("GetCreateLicenceKey错误,LicenceTypeId为空！");
                    return "";
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(" GetCreateLicenceKey异常：" + e.ToString());
                throw e;
            }
        }
        /// <summary>
        /// 单站点新申请时,授权类型为临时时获取开始日期值处理
        /// </summary>
        /// <param name="macAddress">网卡号</param>
        /// <param name="sqh">程序或仪器的SQH</param>
        /// <returns></returns>
        public BaseResultDataValue GetStartDateValueOfApply(string macAddress, string sqh)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            DateTime? StartDate = DateTime.Now;
            if (!String.IsNullOrEmpty(macAddress) && !String.IsNullOrEmpty(sqh))
            {
                macAddress = macAddress.Trim().Replace("'", "");
                sqh = sqh.Trim().Replace("'", "");
                IList<AHSingleLicence> tempList = this.SearchListByHQL("(Status=" + LicenceStatus.特批授权通过.Key + " or (Status=" + LicenceStatus.商务授权通过.Key + " and LicenceKey is not null))" + " and LicenceTypeId=" + LicenceType.临时.Key.ToString() + " and MacAddress='" + macAddress + "' and SQH='" + sqh + "' order by DataAddTime DESC");
                if (tempList != null && tempList.Count > 0)
                {
                    brdv.success = true;
                    StartDate = tempList[0].EndDate;
                    //如果上次临时授权截止日期小于当前日期
                    //if (StartDate.Value.CompareTo(DateTime.Now) <= 0)
                    //    StartDate = DateTime.Now;
                }
                else
                {
                    brdv.ErrorInfo = "没有临时授权信息！";
                }
            }
            else
            {
                brdv.ErrorInfo = "网卡号或SQH值为空！";
                brdv.success = false;
            }
            brdv.ResultDataValue = "{StartDate:" + "\"" + StartDate.Value.ToString("yyyy-MM-dd") + "\"" + "}";
            return brdv;
        }

        public EntityList<AHSingleLicence> SearchListByHQL_LicenceInfo(string where, string returnSortStr, int page, int limit)
        {
            EntityList<AHSingleLicence> list = base.SearchListByHQL(where, returnSortStr, page, limit);
            if (list != null && list.list != null && list.list.Count() > 0)
            {
                SetLicenceInfo(list);
            }
            return list;
        }

        public EntityList<AHSingleLicence> SearchListByHQL_LicenceInfo(string where, int page, int limit)
        {
            EntityList<AHSingleLicence> list = base.SearchListByHQL(where, page, limit);
            if (list != null && list.list != null && list.list.Count() > 0)
            {
                SetLicenceInfo(list);
            }
            return list;
        }

        private EntityList<AHSingleLicence> SetLicenceInfo(EntityList<AHSingleLicence> list)
        {
            List<string> clientno = new List<string>();
            foreach (var c in list.list)
            {
                if (c.PClientID > 0)
                    clientno.Add(c.PClientID.ToString());
            }
            var clist = IDPClientDao.GetListByHQL(" id in (" + string.Join(",", clientno) + ") ");
            if (clist != null && clist.Count > 0)
            {
                foreach (var c in list.list)
                {
                    if (clist.Count(a => a.Id == c.PClientID) > 0)
                    {
                        c.LicenceClientName = clist.Where(a => a.Id == c.PClientID).ElementAt(0).LicenceClientName;
                        c.LicenceCode = clist.Where(a => a.Id == c.PClientID).ElementAt(0).LicenceCode;
                    }
                }
            }
            return list;
        }
    }
}