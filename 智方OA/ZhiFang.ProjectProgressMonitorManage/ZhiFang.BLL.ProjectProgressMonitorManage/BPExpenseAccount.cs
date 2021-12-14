using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.OA;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPExpenseAccount : BaseBLL<PExpenseAccount>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPExpenseAccount
    {
        public IBSCOperation IBSCOperation { get; set; }
        public IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public IDAO.RBAC.IDHRDeptDao IDHRDeptDao { set; get; }
        public IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { set; get; }
        public IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }
        public IDAO.ProjectProgressMonitorManage.IDPContractDao IDPContractDao { get; set; }
        public IDAO.ProjectProgressMonitorManage.IDPEmpFinanceAccountDao IDPEmpFinanceAccountDao { set; get; }



        public BaseResultDataValue PExpenseAccountAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction)
        {
            BaseResultDataValue bdrv = new BaseResultDataValue();
            if (Entity.Status.ToString() == PExpenseAccountStatus.暂存.Key|| Entity.Status.ToString() == PExpenseAccountStatus.申请.Key)
            {
                if (_getSuperior())
                {
                    if (base.Add())
                        IBSCOperation.AddOperationEntityStatus(Entity);
                    else
                    {
                        bdrv.ErrorInfo = "新增报销单失败！";
                        bdrv.success = false;
                    }
                }
                else
                {
                    bdrv.ErrorInfo = "未找到部门经理和直属领导！";
                    bdrv.success = false;
                }
            }
            else
            {
                bdrv.ErrorInfo = "报销单状态错误，无法新增！状态："+ Entity.Status.ToString();
                bdrv.success = false;
            }
            return bdrv;
        }

        public BaseResultBool PExpenseAccountStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long empID, string empName)
        {
            BaseResultBool brb = new BaseResultBool();
            PExpenseAccount entity = this.Entity;
            var tmpa = tempArray.ToList();
            PExpenseAccount tmpPExpenseAccount = new PExpenseAccount();
            tmpPExpenseAccount = DBDao.Get(entity.Id);
            if (tmpPExpenseAccount == null)
            {
                return new BaseResultBool() { ErrorInfo = "报销单ID：" + entity.Id + "为空！", success = false };
            }
            if (!PExpenseAccountStatusUpdateCheck(entity, tmpPExpenseAccount, empID, empName, tmpa))
            {
                return new BaseResultBool() { ErrorInfo = "报销单ID：" + entity.Id + "的状态为：" + PExpenseAccountStatus.GetStatusDic()[tmpPExpenseAccount.Status.ToString()].Name + "！", success = false };
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                if (entity.Status.ToString() == PExpenseAccountStatus.打款.Key)
                {
                    IDPEmpFinanceAccountDao.PExpenseAccount(entity.LoadAmount, tmpPExpenseAccount.ApplyManID.Value);
                }
                SaveSCOperation(entity);

                PExpenseAccountStatusMessagePush(pushWeiXinMessageAction, entity.Id, entity.Status.ToString(), null);
                brb.success = true;
            }
            else
            {
                brb.ErrorInfo = "PExpenseAccountStatusUpdate.Update错误！";
                brb.success = false;
            }
            return brb;
        }

        private bool PExpenseAccountStatusUpdateCheck(PExpenseAccount entity, PExpenseAccount tmpPExpenseAccount,long empID, string empName,List<string> tmpa)
        {
            #region 暂存
            if (entity.Status.ToString() == PExpenseAccountStatus.暂存.Key)
            {
                if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.暂存.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.申请.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.一审退回.Key)
                {
                    return false;
                }
            }
            #endregion

            #region 申请
            if (entity.Status.ToString() == PExpenseAccountStatus.申请.Key)
            {
                if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.暂存.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.一审退回.Key)
                {
                    return false;
                }

                tmpa.Add("ApplyManID=" + empID + " ");
                tmpa.Add("ApplyMan='" + empName + "'");
                tmpa.Add("ApplyDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ReviewDate=null");
                tmpa.Add("ReviewInfo=null");

                tmpa.Add("TwoReviewManID=null");
                tmpa.Add("TwoReviewMan=null");
                tmpa.Add("TwoReviewDate=null");
                tmpa.Add("TwoReviewInfo=null");
            }
            #endregion

            #region 一审通过
            if (entity.Status.ToString() == PExpenseAccountStatus.一审通过.Key)
            {
                if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.申请.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.二审退回.Key)
                {
                    return false;
                }
                tmpa.Add("ReviewManID=" + empID + " ");
                tmpa.Add("ReviewMan='" + empName + "'");
                tmpa.Add("ReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ReviewInfo='" + entity.ReviewInfo + "'");

                tmpa.Add("TwoReviewManID=null");
                tmpa.Add("TwoReviewMan=null");
                tmpa.Add("TwoReviewDate=null");
                tmpa.Add("TwoReviewInfo=null");
            }
            #endregion

            #region 一审退回
            if (entity.Status.ToString() == PExpenseAccountStatus.一审退回.Key)
            {
                if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.申请.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.二审退回.Key)
                {
                    return false;
                }
                tmpa.Add("ReviewManID=" + empID + " ");
                tmpa.Add("ReviewMan='" + empName + "'");
                tmpa.Add("ReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ReviewInfo='" + entity.ReviewInfo + "'");
            }
            #endregion

            #region 二审通过
            if (entity.Status.ToString() == PExpenseAccountStatus.二审通过.Key)
            {
                if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.一审通过.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.三审退回.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.四审退回.Key)
                {
                    return false;
                }
                tmpa.Add("TwoReviewManID=" + empID + " ");
                tmpa.Add("TwoReviewMan='" + empName + "'");
                tmpa.Add("TwoReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("TwoReviewInfo='" + entity.TwoReviewInfo + "'");

                tmpa.Add("ThreeReviewManID=null");
                tmpa.Add("ThreeReviewMan=null");
                tmpa.Add("ThreeReviewDate=null");
                tmpa.Add("ThreeReviewInfo=null");
                tmpa.Add("FourReviewManID=null");
                tmpa.Add("FourReviewMan=null");
                tmpa.Add("FourReviewDate=null");
                tmpa.Add("FourReviewInfo=null");
            }
            #endregion

            #region 二审退回
            if (entity.Status.ToString() == PExpenseAccountStatus.二审退回.Key)
            {
                if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.一审通过.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.三审退回.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.四审退回.Key)
                {
                    return false;
                }
                tmpa.Add("TwoReviewManID=" + empID + " ");
                tmpa.Add("TwoReviewMan='" + empName + "'");
                tmpa.Add("TwoReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("TwoReviewInfo='" + entity.TwoReviewInfo + "'");
            }
            #endregion

            #region 三审通过
            if (entity.Status.ToString() == PExpenseAccountStatus.三审通过.Key)
            {
                if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.二审通过.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.四审退回.Key)
                {
                    return false;
                }
                tmpa.Add("ThreeReviewManID=" + empID + " ");
                tmpa.Add("ThreeReviewMan='" + empName + "'");
                tmpa.Add("ThreeReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ThreeReviewInfo='" + entity.ThreeReviewInfo + "'");

                tmpa.Add("FourReviewManID=null");
                tmpa.Add("FourReviewMan=null");
                tmpa.Add("FourReviewDate=null");
                tmpa.Add("FourReviewInfo=null");
            }
            #endregion

            #region 三审退回
            if (entity.Status.ToString() == PExpenseAccountStatus.三审退回.Key)
            {
                if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.二审通过.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.四审退回.Key)
                {
                    return false;
                }
                tmpa.Add("ThreeReviewManID=" + empID + " ");
                tmpa.Add("ThreeReviewMan='" + empName + "'");
                tmpa.Add("ThreeReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ThreeReviewInfo='" + entity.ThreeReviewInfo + "'");
            }
            #endregion

            #region 四审通过
            if (entity.Status.ToString() == PExpenseAccountStatus.四审通过.Key)
            {
                if (tmpPExpenseAccount.IsSpecially)
                {
                    if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.三审通过.Key)
                    {
                        return false;
                    }
                }
                else
                {
                    if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.三审通过.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.二审通过.Key)
                    {
                        return false;
                    }
                }

                tmpa.Add("FourReviewManID=" + empID + " ");
                tmpa.Add("FourReviewMan='" + empName + "'");
                tmpa.Add("FourReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("FourReviewInfo='" + entity.FourReviewInfo + "'");
            }
            #endregion

            #region 四审退回
            if (entity.Status.ToString() == PExpenseAccountStatus.四审退回.Key)
            {
                if (tmpPExpenseAccount.IsSpecially)
                {
                    if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.三审通过.Key)
                    {
                        return false;
                    }
                }
                else
                {
                    if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.三审通过.Key && tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.二审通过.Key)
                    {
                        return false;
                    }
                }
                tmpa.Add("FourReviewManID=" + empID + " ");
                tmpa.Add("FourReviewMan='" + empName + "'");
                tmpa.Add("FourReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("FourReviewInfo='" + entity.FourReviewInfo + "'");
            }
            #endregion

            #region 打款
            if (entity.Status.ToString() == PExpenseAccountStatus.打款.Key)
            {
                if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.四审通过.Key)
                {
                    return false;
                }

                tmpa.Add("PayManID=" + empID + " ");
                tmpa.Add("PayManName='" + empName + "'");
                //tmpa.Add("PayDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("PayDateInfo='" + entity.PayDateInfo + "'");
            }
            #endregion

            #region 领款确认
            if (entity.Status.ToString() == PExpenseAccountStatus.领款确认.Key)
            {
                if (tmpPExpenseAccount.Status.ToString() != PExpenseAccountStatus.打款.Key)
                {
                    return false;
                }
                tmpa.Add("ReceiveManID=" + empID + " ");
                tmpa.Add("ReceiveManName='" + empName + "'");
                tmpa.Add("ReceiveDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            #endregion 
            return true;
        }

        private void SaveSCOperation(PExpenseAccount entity)
        {
            SCOperation sco = new SCOperation();
            sco.BobjectID = entity.Id;
            string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (empid != null && empid.Trim() != "")
                sco.CreatorID = long.Parse(empid);
            if (empname != null && empname.Trim() != "")
                sco.CreatorName = empname;
            sco.BusinessModuleCode = "PExpenseAccount";
            sco.Memo = entity.OperationMemo;

            sco.Type = entity.Status;
            sco.TypeName = PExpenseAccountStatus.GetStatusDic()[Entity.Status.ToString()].Name;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }
        private bool _getSuperior()
        {
            if (this.Entity.DeptID.HasValue)
            {
                var tmp = IDHRDeptDao.Get(this.Entity.DeptID.Value);
                if (tmp != null)
                {
                    if (tmp.ManagerID != this.Entity.ApplyManID.Value)
                    {
                        this.Entity.ReviewManID = tmp.ManagerID;
                        this.Entity.ReviewMan = tmp.ManagerName;
                    }
                }
            }
            if (!this.Entity.ReviewManID.HasValue)
            {
                HREmployee tmpemp = IDHREmployeeDao.Get(this.Entity.ApplyManID.Value);
                if (tmpemp != null)
                {
                    this.Entity.ReviewManID = tmpemp.ManagerID;
                    this.Entity.ReviewMan = tmpemp.ManagerName;
                }
            }
            return this.Entity.ReviewManID.HasValue;
        }

        void PExpenseAccountStatusMessagePush(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long id, string StatusId, PExpenseAccount entity)
        {
            List<long> receiveidlist = new List<long>();
            string message = "";
            string sendmen = "";
            PExpenseAccount pexpenseaccount = entity;
            if (pexpenseaccount == null)
                pexpenseaccount = DBDao.Get(id);
            //string url = "WeiXin/WeiXinMainRouter.aspx?operate=TASKINFO&id=" + PTaskId + "&IsSingle=1" + "&name=" + ptask.CName;
            #region 申请
            if (StatusId.Trim() == PExpenseAccountStatus.暂存.Key)
            {

            }
            if (StatusId.Trim() == PExpenseAccountStatus.申请.Key)
            {
                receiveidlist.Add(pexpenseaccount.ReviewManID.Value);
                message = "您收到待一审的报销申请（所属单位：" + pexpenseaccount.DeptName + "，金额：" + pexpenseaccount.PExpenseAccounAmount + "），申请人：" + pexpenseaccount.ApplyMan + "。";
            }
            #endregion
            #region 一审
            if (StatusId.Trim() == PExpenseAccountStatus.一审通过.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.商务助理.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                    message = "您收到待二审的报销申请（所属单位：" + pexpenseaccount.DeptName + "，金额：" + pexpenseaccount.PExpenseAccounAmount + "），申请人：" + pexpenseaccount.ApplyMan + ",已被" + pexpenseaccount.ReviewMan + "定为'一审通过'状态。";
                }
            }
            if (StatusId.Trim() == PExpenseAccountStatus.一审退回.Key)
            {
                if (pexpenseaccount.ApplyManID.HasValue)
                {
                    receiveidlist.Add(pexpenseaccount.ApplyManID.Value);
                    message = "您报销申请（所属单位：" + pexpenseaccount.DeptName + "，金额：" + pexpenseaccount.PExpenseAccounAmount + "）,已被" + pexpenseaccount.ReviewMan + "定为'一审退回'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion
            #region 二审
            if (StatusId.Trim() == PExpenseAccountStatus.二审通过.Key)
            {
                if (pexpenseaccount.IsSpecially)
                {
                    IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.总经理.Key);
                    if (rbacerlist != null && rbacerlist.Count() > 0)
                    {
                        foreach (RBACEmpRoles rbacer in rbacerlist)
                        {
                            receiveidlist.Add(rbacer.HREmployee.Id);
                        }
                    }

                    rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.副总经理.Key);
                    if (rbacerlist != null && rbacerlist.Count() > 0)
                    {
                        foreach (RBACEmpRoles rbacer in rbacerlist)
                        {
                            receiveidlist.Add(rbacer.HREmployee.Id);
                        }
                    }
                }
                else
                {
                    IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.出纳.Key);
                    if (rbacerlist != null && rbacerlist.Count() > 0)
                    {
                        foreach (RBACEmpRoles rbacer in rbacerlist)
                        {
                            receiveidlist.Add(rbacer.HREmployee.Id);
                        }
                    }
                }
                message = "您收到待审核的报销申请（所属单位：" + pexpenseaccount.DeptName + "，金额：" + pexpenseaccount.PExpenseAccounAmount + "），申请人：" + pexpenseaccount.ApplyMan + ",已被" + pexpenseaccount.TwoReviewMan + "定为'二审通过'状态。";
            }
            if (StatusId.Trim() == PExpenseAccountStatus.二审退回.Key)
            {
                if (pexpenseaccount.ReviewManID.HasValue)
                {
                    receiveidlist.Add(pexpenseaccount.ReviewManID.Value);
                    message = "您一审的报销申请（所属单位：" + pexpenseaccount.DeptName + "，金额：" + pexpenseaccount.PExpenseAccounAmount + "），申请人：" + pexpenseaccount.ApplyMan + ",已被" + pexpenseaccount.TwoReviewMan + "定为'二审退回'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion
            #region 三审
            if (StatusId.Trim() == PExpenseAccountStatus.三审通过.Key)
            {

                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.出纳.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }

                message = "您收到待审核的报销申请（所属单位：" + pexpenseaccount.DeptName + "，金额：" + pexpenseaccount.PExpenseAccounAmount + "），申请人：" + pexpenseaccount.ApplyMan + ",已被" + pexpenseaccount.ThreeReviewMan + "定为'三审通过'状态。";
            }
            if (StatusId.Trim() == PExpenseAccountStatus.三审退回.Key)
            {
                if (pexpenseaccount.TwoReviewManID.HasValue)
                {
                    receiveidlist.Add(pexpenseaccount.TwoReviewManID.Value);
                    message = "您审核的报销申请（所属单位：" + pexpenseaccount.DeptName + "，金额：" + pexpenseaccount.PExpenseAccounAmount + "），申请人：" + pexpenseaccount.ApplyMan + ",已被" + pexpenseaccount.ThreeReviewMan + "定为'三审退回'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion
            #region 四审
            if (StatusId.Trim() == PExpenseAccountStatus.四审通过.Key)
            {

                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.出纳.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }

                message = "您收到待审核的报销申请（所属单位：" + pexpenseaccount.DeptName + "，金额：" + pexpenseaccount.PExpenseAccounAmount + "），申请人：" + pexpenseaccount.ApplyMan + ",已被" + pexpenseaccount.FourReviewMan + "定为'四审通过'状态。";
            }
            if (StatusId.Trim() == PExpenseAccountStatus.四审退回.Key)
            {
                if (pexpenseaccount.ThreeReviewManID.HasValue)
                {
                    receiveidlist.Add(pexpenseaccount.ThreeReviewManID.Value);
                    message = "您审核的报销申请（所属单位：" + pexpenseaccount.DeptName + "，金额：" + pexpenseaccount.PExpenseAccounAmount + "），申请人：" + pexpenseaccount.ApplyMan + ",已被" + pexpenseaccount.FourReviewMan + "定为'四审退回'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion
            #region 打款
            if (StatusId.Trim() == PExpenseAccountStatus.打款.Key)
            {
                if (pexpenseaccount.ApplyManID.HasValue)
                {
                    receiveidlist.Add(pexpenseaccount.ApplyManID.Value);
                }

                message = "您的报销申请（所属单位：" + pexpenseaccount.DeptName + "，金额：" + pexpenseaccount.PExpenseAccounAmount + "），申请人：" + pexpenseaccount.ApplyMan + ",已被" + pexpenseaccount.PayManName + "定为'打款'状态。";
            }
            #endregion
            #region 已签收
            if (StatusId.Trim() == PExpenseAccountStatus.领款确认.Key)
            {
                if (pexpenseaccount.PayManID.HasValue)
                {
                    receiveidlist.Add(pexpenseaccount.PayManID.Value);
                    message = "您已打款的报销申请的那（所属单位：" + pexpenseaccount.DeptName + "，金额：" + pexpenseaccount.PExpenseAccounAmount + "），申请人：" + pexpenseaccount.ApplyMan + ",已被" + pexpenseaccount.ApplyMan + "定为'领款确认'状态。";
                }
            }
            #endregion
            if (receiveidlist.Count > 0)
            {

                Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                string urgencycolor = "#11cd6e";
                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你收到报销管理模块信息" });
                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = message });
                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = "OA系统：报销管理" });
                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = pexpenseaccount.ApplyMan });
                string tmpdatetime = (pexpenseaccount.DataAddTime.HasValue) ? pexpenseaccount.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = tmpdatetime });
                data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "请登录OA查看" });
                IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, receiveidlist, data, "pexpenseaccount", "");
            }
        }
    }
}