using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.OA;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
	/// <summary>
	///
	/// </summary>
	public  class BPRepayment : BaseBLL<PRepayment>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPRepayment
    {
        public IBSCOperation IBSCOperation { get; set; }
        public IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public IDAO.RBAC.IDHRDeptDao IDHRDeptDao { set; get; }
        public IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { set; get; }
        public IDAO.ProjectProgressMonitorManage.IDPEmpFinanceAccountDao IDPEmpFinanceAccountDao { set; get; }

        public BaseResultDataValue PRepaymentAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction)
        {
            BaseResultDataValue brb = new BaseResultDataValue();
            if (base.Add())
            {
                if (this.Entity.DeptID.HasValue)
                {
                    var tmp = IDHRDeptDao.Get(this.Entity.DeptID.Value);
                    if (tmp != null)
                    {
                        this.Entity.DeptName = tmp.CName;
                    }
                }
                if (this.Entity.Status.ToString() == PRepaymentStatus.还款确认.Key)
                {
                    IDPEmpFinanceAccountDao.PExpenseAccount(this.Entity.PRepaymentAmount, this.Entity.ApplyManID.Value);
                }
                SaveSCOperation(this.Entity);
                PLoanRepaymentStatusMessagePush(pushWeiXinMessageAction, this.Entity.Id, this.Entity.Status.ToString(), this.Entity);
                brb.success= true;
            }
            else
            {
                brb.success= false;
            }
            return brb;
        }
        public BaseResultBool PRepaymentStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long EmpID, string EmpName)
        {
            BaseResultBool brb = new BaseResultBool();
            PRepayment entity = this.Entity;
            var tmpa = tempArray.ToList();
            PRepayment tmpPRepayment = new PRepayment();
            tmpPRepayment = DBDao.Get(entity.Id);
            
            if (tmpPRepayment == null)
            {
                brb.ErrorInfo = "还款单ID：" + entity.Id + "为空！";
                brb.success = false;
                return brb;
            }

            if (!PRepaymentStatusUpdateCheck(entity, tmpPRepayment, EmpID, EmpName, tmpa))
            {
                return new BaseResultBool() { ErrorInfo = "还款单ID：" + entity.Id + "的状态为：" + PRepaymentStatus.GetStatusDic()[tmpPRepayment.Status.ToString()].Name + "！", success = false };
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                if (entity.Status.ToString() == PRepaymentStatus.还款确认.Key)
                {
                    IDPEmpFinanceAccountDao.PExpenseAccount(tmpPRepayment.PRepaymentAmount, tmpPRepayment.ApplyManID.Value);
                }
                SaveSCOperation(entity);

                PLoanRepaymentStatusMessagePush(pushWeiXinMessageAction, entity.Id, entity.Status.ToString(), null);
                brb.success = true;
                return brb;
            }
            else
            {
                brb.ErrorInfo = "PRepaymentStatusUpdate.Update错误！";
                brb.success = false;
                return brb;
            }
        }
        bool PRepaymentStatusUpdateCheck(PRepayment entity, PRepayment tmpPRepayment, long EmpID, string EmpName, List<string> tmpa)
        {
            #region 暂存
            if (entity.Status.ToString() == PRepaymentStatus.暂存.Key)
            {
                if (tmpPRepayment.Status.ToString() != PRepaymentStatus.暂存.Key && tmpPRepayment.Status.ToString() != PRepaymentStatus.申请.Key && tmpPRepayment.Status.ToString() != PRepaymentStatus.打回.Key)
                {
                    return false;
                }
            }
            #endregion

            #region  申请
            if (entity.Status.ToString() == PRepaymentStatus.申请.Key)
            {
                if (tmpPRepayment.Status.ToString() != PRepaymentStatus.暂存.Key && tmpPRepayment.Status.ToString() != PRepaymentStatus.打回.Key)
                {
                    return false;
                }
            }
            #endregion

            #region 还款确认
            if (entity.Status.ToString() == PRepaymentStatus.还款确认.Key)
            {
                if ( tmpPRepayment.Status.ToString() != PRepaymentStatus.申请.Key )
                {
                    return false;
                }
            }
            #endregion

            #region 打回
            if (entity.Status.ToString() == PRepaymentStatus.打回.Key)
            {
                if (tmpPRepayment.Status.ToString() != PRepaymentStatus.申请.Key)
                {
                    return false;
                }
            }
            #endregion
            return true;
        }
        /// <summary>
        /// 操作记录登记
        /// </summary>
        private void SaveSCOperation(PRepayment entity)
        {
            SCOperation sco = new SCOperation();
            sco.BobjectID = entity.Id;
            string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (empid != null && empid.Trim() != "")
                sco.CreatorID = long.Parse(empid);
            if (empname != null && empname.Trim() != "")
                sco.CreatorName = empname;
            sco.BusinessModuleCode = "PRepayment";
            sco.Memo = entity.OperationMemo;

            sco.Type = entity.Status;
            sco.TypeName = PRepaymentStatus.GetStatusDic()[Entity.Status.ToString()].Name;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }

        private void PLoanRepaymentStatusMessagePush(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long id, string StatusId, PRepayment entity)
        {
            List<long> receiveidlist = new List<long>();
            string message = "";
            string sendmen = "";
            PRepayment prepayment = entity;
            if (prepayment == null)
                prepayment = DBDao.Get(id);
            //string url = "WeiXin/WeiXinMainRouter.aspx?operate=TASKINFO&id=" + PTaskId + "&IsSingle=1" + "&name=" + ptask.CName;
            #region 申请
            if (StatusId.Trim() == PRepaymentStatus.暂存.Key)
            {

            }
            if (StatusId.Trim() == PRepaymentStatus.申请.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.出纳.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }
                message = "您收到待一审的还款申请（所属单位：" + prepayment.DeptName + "，金额：" + prepayment.PRepaymentAmount + "），申请人：" + prepayment.ApplyMan + "。";
            }
            #endregion
            #region 还款确认
            if (StatusId.Trim() == PRepaymentStatus.还款确认.Key)
            {
                receiveidlist.Add(prepayment.ApplyManID.Value);
                message = "您的还款申请（所属单位：" + prepayment.DeptName + "，金额：" + prepayment.PRepaymentAmount + "），申请人：" + prepayment.ApplyMan + ",已被" + prepayment.ReviewMan + "定为'还款确认'状态。";
            }
            if (StatusId.Trim() == PRepaymentStatus.打回.Key)
            {
                receiveidlist.Add(prepayment.ApplyManID.Value);
                message = "您的还款申请（所属单位：" + prepayment.DeptName + "，金额：" + prepayment.PRepaymentAmount + "），申请人：" + prepayment.ApplyMan + ",已被" + prepayment.ReviewMan + "定为'打回'状态。";
            }
            #endregion

            if (receiveidlist.Count > 0)
            {
                
                Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                string urgencycolor = "#11cd6e";
                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你收到还款管理模块信息" });
                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = message });
                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = "OA系统：还款管理" });
                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = prepayment.ApplyMan });
                string tmpdatetime = (prepayment.DataAddTime.HasValue) ? prepayment.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = tmpdatetime });
                data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "请登录OA查看" });                
                IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, receiveidlist, data, "PRepayment", "");
            }
        }
        
    }
}