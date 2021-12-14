using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.Base;
using System.IO;
using ZhiFang.ProjectProgressMonitorManage.Common;
using ZhiFang.IBLL.OA;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPLoanBill : BaseBLL<PLoanBill>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPLoanBill
    {
        public IBSCOperation IBSCOperation { get; set; }
        public IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public IDAO.RBAC.IDHRDeptDao IDHRDeptDao { set; get; }
        public IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { set; get; }
        public IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { set; get; }
        public IDAO.ProjectProgressMonitorManage.IDPEmpFinanceAccountDao IDPEmpFinanceAccountDao { set; get; }
        public IBBParameter IBBParameter { get; set; }



        public BaseResultDataValue PLoanBillAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //借款单号的生成处理:年月日时分秒毫秒
            this.Entity.LoanBillNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            if (this.Entity.DeptID.HasValue)
            {
                var tmp = IDHRDeptDao.Get(this.Entity.DeptID.Value);//.GetListByHQL(" HREmployee.Id = " + this.Entity.ApplyManID);
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
            if (!this.Entity.ReviewManID.HasValue)
            {
                brdv.ErrorInfo = "未找到部门经理和直属领导！";
                brdv.success = false;
                return brdv;
            }
            if (base.Add())
            {
                SaveSCOperation(this.Entity);
                PLoanBillStatusMessagePush(pushWeiXinMessageAction, this.Entity.Id, this.Entity.Status.ToString(), this.Entity);
                brdv.success = true;
            }
            else
            {
                brdv.ErrorInfo = "PLoanBillAdd.Add错误！";
                brdv.success = false;
            }
            return brdv;
        }

        public BaseResultBool PLoanBillStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long EmpID, string EmpName)
        {
            BaseResultBool brb = new BaseResultBool();
            PLoanBill entity = this.Entity;
            var tmpa = tempArray.ToList();
            PLoanBill tmpPLoanBill = new PLoanBill();
            tmpPLoanBill = DBDao.Get(entity.Id);
            if (tmpPLoanBill == null)
            {
                brb.ErrorInfo = "借款单ID：" + entity.Id + "为空！";
                brb.success = false;
                return brb;
            }

            if (!PLoanBillStatusUpdateCheck(entity, tmpPLoanBill, EmpID, EmpName, tmpa))
            {               
                return new BaseResultBool() { ErrorInfo = "借款单ID：" + entity.Id + "的状态为：" + PLoanBillStatus.GetStatusDic()[tmpPLoanBill.Status.ToString()].Name + "！", success = false };
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                if (entity.Status.ToString() == PLoanBillStatus.打款.Key)
                {
                    IDPEmpFinanceAccountDao.PLoanBill(tmpPLoanBill.LoanBillAmount, tmpPLoanBill.ApplyManID.Value);
                }
                SaveSCOperation(entity);

                PLoanBillStatusMessagePush(pushWeiXinMessageAction, entity.Id, entity.Status.ToString(), null);
                brb.success= true;
            }
            else
            {
                brb.ErrorInfo = "PLoanBillStatusUpdate.Update错误！";
                brb.success = false;
            }
            return brb;
        }

        bool PLoanBillStatusUpdateCheck(PLoanBill entity, PLoanBill tmpPLoanBill, long EmpID, string EmpName, List<string> tmpa)
        {
            #region 暂存
            if (entity.Status.ToString() == PLoanBillStatus.暂存.Key)
            {
                if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.暂存.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.申请.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.一审退回.Key)
                {                   
                    return false;
                }
            }
            #endregion

            #region 申请
            if (entity.Status.ToString() == PLoanBillStatus.申请.Key)
            {
                if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.暂存.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.一审退回.Key)
                {
                    return false;
                }

                tmpa.Add("ApplyManID=" + EmpID + " ");
                tmpa.Add("ApplyMan='" + EmpName + "'");
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
            if (entity.Status.ToString() == PLoanBillStatus.一审通过.Key)
            {
                if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.申请.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.二审退回.Key)
                {
                    return false;
                }
                tmpa.Add("ReviewManID=" + EmpID + " ");
                tmpa.Add("ReviewMan='" + EmpName + "'");
                tmpa.Add("ReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ReviewInfo='" + entity.ReviewInfo + "'");

                tmpa.Add("TwoReviewManID=null");
                tmpa.Add("TwoReviewMan=null");
                tmpa.Add("TwoReviewDate=null");
                tmpa.Add("TwoReviewInfo=null");

            }
            #endregion

            #region 一审退回
            if (entity.Status.ToString() == PLoanBillStatus.一审退回.Key)
            {
                if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.申请.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.二审退回.Key)
                {
                    return false;
                }
                tmpa.Add("ReviewManID=" + EmpID + " ");
                tmpa.Add("ReviewMan='" + EmpName + "'");
                tmpa.Add("ReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ReviewInfo='" + entity.ReviewInfo + "'");

            }
            #endregion

            #region 二审通过
            if (entity.Status.ToString() == PLoanBillStatus.二审通过.Key)
            {
                if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.一审通过.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.三审退回.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.四审退回.Key)
                {
                    return false;
                }
                tmpa.Add("TwoReviewManID=" + EmpID + " ");
                tmpa.Add("TwoReviewMan='" + EmpName + "'");
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
            if (entity.Status.ToString() == PLoanBillStatus.二审退回.Key)
            {
                if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.一审通过.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.三审退回.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.四审退回.Key)
                {
                    return false;
                }
                tmpa.Add("TwoReviewManID=" + EmpID + " ");
                tmpa.Add("TwoReviewMan='" + EmpName + "'");
                tmpa.Add("TwoReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("TwoReviewInfo='" + entity.TwoReviewInfo + "'");

            }
            #endregion

            #region 三审通过
            if (entity.Status.ToString() == PLoanBillStatus.三审通过.Key)
            {
                if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.二审通过.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.四审退回.Key)
                {
                    return false;
                }
                tmpa.Add("ThreeReviewManID=" + EmpID + " ");
                tmpa.Add("ThreeReviewMan='" + EmpName + "'");
                tmpa.Add("ThreeReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ThreeReviewInfo='" + entity.ThreeReviewInfo + "'");

                tmpa.Add("FourReviewManID=null");
                tmpa.Add("FourReviewMan=null");
                tmpa.Add("FourReviewDate=null");
                tmpa.Add("FourReviewInfo=null");

            }
            #endregion

            #region 三审退回
            if (entity.Status.ToString() == PLoanBillStatus.三审退回.Key)
            {
                if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.二审通过.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.四审退回.Key)
                {
                    return false;
                }
                tmpa.Add("ThreeReviewManID=" + EmpID + " ");
                tmpa.Add("ThreeReviewMan='" + EmpName + "'");
                tmpa.Add("ThreeReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ThreeReviewInfo='" + entity.ThreeReviewInfo + "'");

            }
            #endregion

            #region 四审通过
            if (entity.Status.ToString() == PLoanBillStatus.四审通过.Key)
            {
                if (tmpPLoanBill.IsSpecially)
                {
                    if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.三审通过.Key)
                    {
                        return false;
                    }
                }
                else
                {
                    if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.三审通过.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.二审通过.Key)
                    {
                        return false;
                    }
                }

                tmpa.Add("FourReviewManID=" + EmpID + " ");
                tmpa.Add("FourReviewMan='" + EmpName + "'");
                tmpa.Add("FourReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("FourReviewInfo='" + entity.FourReviewInfo + "'");

            }
            #endregion

            #region 四审退回
            if (entity.Status.ToString() == PLoanBillStatus.四审退回.Key)
            {
                if (tmpPLoanBill.IsSpecially)
                {
                    if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.三审通过.Key)
                    {
                        return false;
                    }
                }
                else
                {
                    if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.三审通过.Key && tmpPLoanBill.Status.ToString() != PLoanBillStatus.二审通过.Key)
                    {
                        return false;
                    }
                }
                tmpa.Add("FourReviewManID=" + EmpID + " ");
                tmpa.Add("FourReviewMan='" + EmpName + "'");
                tmpa.Add("FourReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("FourReviewInfo='" + entity.FourReviewInfo + "'");

            }
            #endregion

            #region 打款
            if (entity.Status.ToString() == PLoanBillStatus.打款.Key)
            {
                if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.四审通过.Key)
                {
                    return false;
                }

                tmpa.Add("PayManID=" + EmpID + " ");
                tmpa.Add("PayManName='" + EmpName + "'");
                //tmpa.Add("PayDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("PayDateInfo='" + entity.PayDateInfo + "'");

            }
            #endregion

            #region 领款确认
            if (entity.Status.ToString() == PLoanBillStatus.领款确认.Key)
            {
                if (tmpPLoanBill.Status.ToString() != PLoanBillStatus.打款.Key)
                {
                    return false;
                }
                tmpa.Add("ReceiveManID=" + EmpID + " ");
                tmpa.Add("ReceiveManName='" + EmpName + "'");
                tmpa.Add("ReceiveDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ReceiveManInfo='" + entity.ReceiveManInfo + "'");

            }
            #endregion   
            return true;
        }

        /// <summary>
        /// 操作记录登记
        /// </summary>
        /// <param name="ffile"></param>
        /// <param name="type"></param>
        private void SaveSCOperation(PLoanBill entity)
        {
            SCOperation sco = new SCOperation();
            sco.BobjectID = entity.Id;
            string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (empid != null && empid.Trim() != "")
                sco.CreatorID = long.Parse(empid);
            if (empname != null && empname.Trim() != "")
                sco.CreatorName = empname;
            sco.BusinessModuleCode = "PLoanBill";
            sco.Memo = entity.OperationMemo;

            sco.Type = entity.Status;
            sco.TypeName = PLoanBillStatus.GetStatusDic()[Entity.Status.ToString()].Name;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }

        private void PLoanBillStatusMessagePush(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long id, string StatusId, PLoanBill entity)
        {
            List<long> receiveidlist = new List<long>();
            string message = "";
            string sendmen = "";
            PLoanBill ploanbill = entity;
            if (ploanbill == null)
                ploanbill = DBDao.Get(id);
            //string url = "WeiXin/WeiXinMainRouter.aspx?operate=TASKINFO&id=" + PTaskId + "&IsSingle=1" + "&name=" + ptask.CName;
            #region 申请
            if (StatusId.Trim() == PLoanBillStatus.暂存.Key)
            {

            }
            if (StatusId.Trim() == PLoanBillStatus.申请.Key)
            {
                receiveidlist.Add(ploanbill.ReviewManID.Value);
                message = "您收到待一审的借款申请（所属单位：" + ploanbill.DeptName + "，金额：" + ploanbill.LoanBillAmount + "），申请人：" + ploanbill.ApplyMan + "。";
            }
            #endregion
            #region 一审
            if (StatusId.Trim() == PLoanBillStatus.一审通过.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.商务助理.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                    message = "您收到待二审的借款申请（所属单位：" + ploanbill.DeptName + "，金额：" + ploanbill.LoanBillAmount + "），申请人：" + ploanbill.ApplyMan + ",已被" + ploanbill.ReviewMan + "定为'一审通过'状态。";
                }
            }
            if (StatusId.Trim() == PLoanBillStatus.一审退回.Key)
            {
                if (ploanbill.ApplyManID.HasValue)
                {
                    receiveidlist.Add(ploanbill.ApplyManID.Value);
                    message = "您借款申请（所属单位：" + ploanbill.DeptName + "，金额：" + ploanbill.LoanBillAmount + "）,已被" + ploanbill.ReviewMan + "定为'一审退回'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion
            #region 二审
            if (StatusId.Trim() == PLoanBillStatus.二审通过.Key)
            {
                if (ploanbill.IsSpecially)
                {
                    if (ploanbill.LoanBillAmount > 20000)
                    {
                        IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.总经理.Key);
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
                        IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.副总经理.Key);
                        if (rbacerlist != null && rbacerlist.Count() > 0)
                        {
                            foreach (RBACEmpRoles rbacer in rbacerlist)
                            {
                                receiveidlist.Add(rbacer.HREmployee.Id);
                            }
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
                message = "您收到待审核的借款申请（所属单位：" + ploanbill.DeptName + "，金额：" + ploanbill.LoanBillAmount + "），申请人：" + ploanbill.ApplyMan + ",已被" + ploanbill.TwoReviewMan + "定为'二审通过'状态。";
            }
            if (StatusId.Trim() == PLoanBillStatus.二审退回.Key)
            {
                if (ploanbill.ReviewManID.HasValue)
                {
                    receiveidlist.Add(ploanbill.ReviewManID.Value);
                    message = "您一审的借款申请（所属单位：" + ploanbill.DeptName + "，金额：" + ploanbill.LoanBillAmount + "），申请人：" + ploanbill.ApplyMan + ",已被" + ploanbill.TwoReviewMan + "定为'二审退回'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion
            #region 三审
            if (StatusId.Trim() == PLoanBillStatus.三审通过.Key)
            {

                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.出纳.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }

                message = "您收到待审核的借款申请（所属单位：" + ploanbill.DeptName + "，金额：" + ploanbill.LoanBillAmount + "），申请人：" + ploanbill.ApplyMan + ",已被" + ploanbill.ThreeReviewMan + "定为'三审通过'状态。";
            }
            if (StatusId.Trim() == PLoanBillStatus.三审退回.Key)
            {
                if (ploanbill.TwoReviewManID.HasValue)
                {
                    receiveidlist.Add(ploanbill.TwoReviewManID.Value);
                    message = "您审核的借款申请（所属单位：" + ploanbill.DeptName + "，金额：" + ploanbill.LoanBillAmount + "），申请人：" + ploanbill.ApplyMan + ",已被" + ploanbill.ThreeReviewMan + "定为'三审退回'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion
            #region 四审
            if (StatusId.Trim() == PLoanBillStatus.四审通过.Key)
            {

                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.出纳.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }

                message = "您收到待审核的借款申请（所属单位：" + ploanbill.DeptName + "，金额：" + ploanbill.LoanBillAmount + "），申请人：" + ploanbill.ApplyMan + ",已被" + ploanbill.FourReviewMan + "定为'四审通过'状态。";
            }
            if (StatusId.Trim() == PLoanBillStatus.四审退回.Key)
            {
                if (ploanbill.ThreeReviewManID.HasValue)
                {
                    receiveidlist.Add(ploanbill.ThreeReviewManID.Value);
                    message = "您审核的借款申请（所属单位：" + ploanbill.DeptName + "，金额：" + ploanbill.LoanBillAmount + "），申请人：" + ploanbill.ApplyMan + ",已被" + ploanbill.FourReviewMan + "定为'四审退回'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion
            #region 打款
            if (StatusId.Trim() == PLoanBillStatus.打款.Key)
            {
                if (ploanbill.ApplyManID.HasValue)
                {
                    receiveidlist.Add(ploanbill.ApplyManID.Value);
                }

                message = "您的借款申请（所属单位：" + ploanbill.DeptName + "，金额：" + ploanbill.LoanBillAmount + "），申请人：" + ploanbill.ApplyMan + ",已被" + ploanbill.PayManName + "定为'打款'状态。";
            }
            #endregion
            #region 已签收
            if (StatusId.Trim() == PLoanBillStatus.领款确认.Key)
            {
                if (ploanbill.PayManID.HasValue)
                {
                    receiveidlist.Add(ploanbill.PayManID.Value);
                    message = "您已打款的借款申请的那（所属单位：" + ploanbill.DeptName + "，金额：" + ploanbill.LoanBillAmount + "），申请人：" + ploanbill.ApplyMan + ",已被" + ploanbill.ApplyMan + "定为'领款确认'状态。";
                }
            }
            #endregion
            if (receiveidlist.Count > 0)
            {
               
                Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                string urgencycolor = "#11cd6e";
                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你收到借款管理模块信息" });
                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = message });
                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = "OA系统：借款管理" });
                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = ploanbill.ApplyMan });
                string tmpdatetime = (ploanbill.DataAddTime.HasValue) ? ploanbill.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = tmpdatetime });
                data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "请登录OA查看" });
                IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, receiveidlist, data, "ploanbill", "");
    }
        }

        public override PLoanBill Get(long longID)
        {
            return SetStatus(base.Get(longID));
        }
        public override IList<PLoanBill> Search()
        {
            var list = base.Search();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = SetStatus(list[i]);
                }
            }
            return list;
        }
        public override EntityList<PLoanBill> SearchListByHQL(string strHqlWhere, string Order, int page, int count)
        {
            var list = base.SearchListByHQL(strHqlWhere, Order, page, count);
            if (list != null && list.list.Count > 0)
            {
                for (int i = 0; i < list.list.Count; i++)
                {
                    list.list[i] = SetStatus(list.list[i]);
                }
            }
            return list;
        }
        public override EntityList<PLoanBill> SearchListByHQL(string strHqlWhere, int page, int count)
        {
            var list = base.SearchListByHQL(strHqlWhere, page, count);
            if (list != null && list.list.Count > 0)
            {
                for (int i = 0; i < list.list.Count; i++)
                {
                    list.list[i] = SetStatus(list.list[i]);
                }
            }
            return list;
        }
        public PLoanBill SetStatus(PLoanBill p)
        {
            if (p == null)
            {
                return null;
            }
            if (p.Status > 0)
            {
                var statuslist = PLoanBillStatus.GetStatusDic();
                if (statuslist.Keys.Contains(p.Status.ToString()))
                {
                    p.StatusName = statuslist[p.Status.ToString()].Name;
                }
            }
            return p;
        }

        public EntityList<PLoanBill> SearchPLoanBillByExportType(long ExportType, long empid, string where, string sort, int Page, int Limit)
        {
            ZhiFang.Common.Log.Log.Debug("SearchPLoanBillByExportType.where:" + where);
            string hql = " 1=1 ";
            #region 浏览查看类型。-1：全部、1：我申请、2：我一审、3：我二审、4：我三审、5：我四审、6：我打款、7：我领款

            if (ExportType == 1)
            {
                hql += " and ApplyManID =" + empid + " ";
            }
            if (ExportType == 2)
            {
                string tmphql = " ReviewManID =" + empid + " ";
                hql += " and (" + tmphql + ") ";
                ZhiFang.Common.Log.Log.Debug("Review.hql:" + hql);
            }
            if (ExportType == 3)
            {
                string tmphql = " TwoReviewManID =" + empid + " or TwoReviewManID=null ";
                hql += " and (" + tmphql + ") ";
                ZhiFang.Common.Log.Log.Debug("TwoReview.hql:" + hql);
            }
            if (ExportType == 4)
            {
                string tmphql = " ThreeReviewID =" + empid + " or （ThreeReviewID=null and IsSpecially=true and Status=5)";
                hql += " and (" + tmphql + ") ";
                ZhiFang.Common.Log.Log.Debug("ThreeReview.hql:" + hql);

            }
            if (ExportType == 5)
            {
                string tmphql = " FourReviewID =" + empid + " or (FourReviewID=null and IsSpecially=true and Status=7) or (FourReviewID=null and IsSpecially=false and Status=5)";
                hql += " and (" + tmphql + ") ";
                ZhiFang.Common.Log.Log.Debug("FourReview.hql:" + hql);

            }
            if (ExportType == 6)
            {
                string tmphql = " PayManID =" + empid + " or (PayManID=null and Status=9)";
                hql += " and (" + tmphql + ") ";
                ZhiFang.Common.Log.Log.Debug("PayMan.hql:" + hql);

            }
            if (ExportType == 7)
            {
                hql += " and ReceiveManID =" + empid + " ";
            }

            if (ExportType == -1)
            {
                hql += " and (ApplyManID =" + empid + " or ReviewManID =" + empid + " or TwoReviewManID =" + empid + " or InvoiceManID =" + empid + ") ";
            }
            if (where != null && where.Trim() != "")
            {
                hql += " and " + where;
            }
            ZhiFang.Common.Log.Log.Debug("SearchListByEntity.hql:" + hql);
            return base.SearchListByHQL(hql, sort, Page, Limit);
            #endregion
        }

        public EntityList<PLoanBill> SearchPLoanBillByExportType(long ExportType, long empid, string where, int page, int limit)
        {
            return SearchPLoanBillByExportType(ExportType, empid, where, null, page, limit);
        }

        #region 借款单打印
        public BaseResultDataValue ExcelToPdfFile(long id, bool isPreview, string templetName, ref string fileName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            PLoanBill entity = this.Get(id);
            if (String.IsNullOrEmpty(templetName))
                templetName = "借款单带时间模板.xlsx";
            baseResultDataValue = FillMaintenanceDataToExcel(entity, id, templetName);
            if (entity != null)//  
                fileName = entity.ApplyMan + "借款单.pdf";

            if (baseResultDataValue.success)
            {
                try
                {
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNo.ExcelExportSavePath.ToString());

                    parentPath = parentPath + "\\PLoanBill\\" + entity.LabID.ToString();
                    if (isPreview)
                        parentPath = parentPath + "\\TempPDFFile\\"+ entity.ApplyMan;
                    //+"\\"+ DateTime.Parse(entity.ApplyDate.ToString()).ToString("yyyyMMdd")
                    else
                        parentPath = parentPath + "\\ExcelFile";

                    string pdfFile = parentPath + "\\" + id + ".pdf";
                    //ZhiFang.Common.Log.Log.Info("TempPDFFile：" + pdfFile);
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    baseResultDataValue.success = ExcelHelp.ExcelToPDF(baseResultDataValue.ResultDataValue, pdfFile);
                    if (baseResultDataValue.success)
                        baseResultDataValue.ResultDataValue = pdfFile;
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ResultDataValue = "";
                    baseResultDataValue.ErrorInfo = ex.Message;
                    ZhiFang.Common.Log.Log.Error("ExcelToPdfFile：" + ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue FillMaintenanceDataToExcel(PLoanBill entity, long id, string templetName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取借款单信息";
                ZhiFang.Common.Log.Log.Info("无法获取借款单信息,ID：" + id.ToString());
                return baseResultDataValue;
            }
            //获取借款单的模板信息
            string parentPath = (string)IBBParameter.GetCache(BParameterParaNo.ExcelExportSavePath.ToString());
            parentPath = parentPath + "\\PLoanBill\\";
            string templetPath = parentPath + "Templet\\" + templetName;

            string extName = Path.GetExtension(templetPath);

            string dicFile = parentPath + entity.LabID+ "\\TempExcelFile";
            if (!String.IsNullOrEmpty(entity.ApplyMan))
                parentPath = parentPath + "\\" + entity.ApplyMan;

            string excelFile = dicFile + "\\" + entity.Id + extName;
            ZhiFang.Common.Log.Log.Info("excelFile：" + excelFile);
            if (!Directory.Exists(dicFile))
            {
                Directory.CreateDirectory(dicFile);
            }
            Dictionary<string, string> dicCellValue = null;
            IList<PLoanBill> listData = new List<PLoanBill>();
            listData.Add(entity);

            if ((listData != null) && (listData.Count > 0))
                dicCellValue = _FillExcelCell(entity);

            baseResultDataValue.success = MyNPOIHelper.FillExcelMoudleSheet(templetPath, excelFile, dicCellValue, false);

            if (baseResultDataValue.success)
                baseResultDataValue.ResultDataValue = excelFile;
            return baseResultDataValue;
        }
        private Dictionary<string, string> _FillExcelCell(PLoanBill entity)
        {
            Dictionary<string, string> dicCellValue = new Dictionary<string, string>();

            #region 电子签名路径
            //上传电子签名保存路径
            string parentEmpSignPath = (string)IBBParameter.GetCache(BParameterParaNo.UploadEmpSignPath.ToString());
            string applyManPath = "", reviewManPath = "", twoReviewManPath = "", threeReviewManPath = "", fourReviewManPath = "", payManPath = "", receiveManPath = "";
            if (entity.ApplyManID.HasValue)
                applyManPath = Path.Combine(parentEmpSignPath, entity.ApplyManID + ".png");
            if (entity.ReviewManID.HasValue)
                reviewManPath = Path.Combine(parentEmpSignPath, entity.ReviewManID + ".png");
            if (entity.TwoReviewManID.HasValue)
                twoReviewManPath = Path.Combine(parentEmpSignPath, entity.TwoReviewManID + ".png");
            if (entity.ThreeReviewManID.HasValue)
                threeReviewManPath = Path.Combine(parentEmpSignPath, entity.ThreeReviewManID + ".png");
            if (entity.FourReviewManID.HasValue)
                fourReviewManPath = Path.Combine(parentEmpSignPath, entity.FourReviewManID + ".png");
            if (entity.PayManID.HasValue)
                payManPath = Path.Combine(parentEmpSignPath, entity.PayManID + ".png");
            if (entity.ReceiveManID.HasValue)
                receiveManPath = Path.Combine(parentEmpSignPath, entity.ReceiveManID + ".png");
            #endregion

            //填充第一行,年月日
            if (entity.ApplyDate.HasValue)
                dicCellValue.Add("1,0", entity.ApplyDate.Value.ToString("yyyy年MM月dd日"));
            string replaceOld = @"<br />", replaceNew = "\r\n";
            
            #region 填充第二行
            //借款单位（公章）
            dicCellValue.Add("2,1", entity.ComponeName);
            //借款部门
            dicCellValue.Add("2,4", entity.DeptName);

            //借款人
            if (File.Exists(applyManPath))
            {
                //ZhiFang.Common.Log.Log.Info("借款人电子签名路径：" + applyManPath);
                dicCellValue.Add("2,8,P", applyManPath);
            }
            else {
                dicCellValue.Add("2,8", entity.ApplyMan);
            }
            //借款时间
            if (entity.ApplyDate.HasValue)
                dicCellValue.Add("2,9", entity.ApplyDate.Value.ToString("yyyy年MM月dd日"));
            #endregion

            //填充第三行
            //借款事由
            if (!String.IsNullOrEmpty(entity.LoanBillMemo))
                dicCellValue.Add("3,1", entity.LoanBillMemo.Replace(replaceOld, replaceNew));

            #region 填充第四行
            //借款金额
            if (!String.IsNullOrEmpty(entity.LoanBillAmount.ToString()))
            {
                string cmycurD = Rmb.CmycurD(entity.LoanBillAmount.ToString());
                cmycurD = cmycurD + " ￥" + entity.LoanBillAmount;
                dicCellValue.Add("4,1", cmycurD);
                //ZhiFang.Common.Log.Log.Info("借款金额大写：" + cmycurD);
            }
            //dicCellValue.Add("4,8", entity.LoanBillAmount.ToString());
            //支付方式□√☑
            dicCellValue.Add("4,9", "☑" + entity.ReceiveTypeName);
            #endregion

            #region 填充第五行
            //审核人
            if (File.Exists(reviewManPath))
            {
                //ZhiFang.Common.Log.Log.Info("审核人电子签名路径：" + reviewManPath);
                dicCellValue.Add("5,1,P", reviewManPath);
            }
            else
            {
                dicCellValue.Add("5,1", entity.ReviewMan);
            }
            //审核时间
            if (entity.ReviewDate.HasValue)
                dicCellValue.Add("5,2", entity.ReviewDate.Value.ToString("yyyy.MM.dd"));
            //审核意见(不显示)entity.ReviewInfo.Replace(replaceOld, replaceNew)
            if (!String.IsNullOrEmpty(entity.ReviewInfo))
                dicCellValue.Add("5,4", "");

            //审批人
            if (File.Exists(twoReviewManPath))
            {
                //ZhiFang.Common.Log.Log.Info("审批人电子签名路径：" + twoReviewManPath);
                dicCellValue.Add("5,7,P", twoReviewManPath);
            }
            else
            {
                dicCellValue.Add("5,7", entity.TwoReviewMan);
            }
            
            //审批时间
            if (entity.TwoReviewDate.HasValue)
                dicCellValue.Add("5,8", entity.TwoReviewDate.Value.ToString("yyyy.MM.dd"));
            //审批意见
            if (!String.IsNullOrEmpty(entity.TwoReviewInfo))
                dicCellValue.Add("5,10", entity.TwoReviewInfo.Replace(replaceOld, replaceNew));
            #endregion

            #region 填充第六行
            //商务核对人
            if (File.Exists(threeReviewManPath))
            {
                //ZhiFang.Common.Log.Log.Info("商务核对人电子签名路径：" + threeReviewManPath);
                dicCellValue.Add("6,1,P", threeReviewManPath);
            }
            else
            {
                dicCellValue.Add("6,1", entity.ThreeReviewMan);
            }
            
            //商务核对时间
            if (entity.ThreeReviewDate.HasValue)
                dicCellValue.Add("6,2", entity.ThreeReviewDate.Value.ToString("yyyy.MM.dd"));
            //商务核对意见
            if (!String.IsNullOrEmpty(entity.ThreeReviewInfo))
                dicCellValue.Add("6,4", entity.ThreeReviewInfo.Replace(replaceOld, replaceNew));

            //财务复核人
            if (File.Exists(fourReviewManPath))
            {
                //ZhiFang.Common.Log.Log.Info("财务复核人电子签名路径：" + fourReviewManPath);
                dicCellValue.Add("6,7,P", fourReviewManPath);
            }
            else
            {
                dicCellValue.Add("6,7", entity.FourReviewMan);
            }
           
            //财务复核时间
            if (entity.FourReviewDate.HasValue)
                dicCellValue.Add("6,8", entity.FourReviewDate.Value.ToString("yyyy.MM.dd"));
            //财务复核意见
            if (!String.IsNullOrEmpty(entity.FourReviewInfo))
                dicCellValue.Add("6,10", entity.FourReviewInfo.Replace(replaceOld, replaceNew));
            #endregion

            #region 填充第七行
            //出纳检查打款人
            if (File.Exists(payManPath))
            {
                //ZhiFang.Common.Log.Log.Info("出纳检查打款人电子签名路径：" + payManPath);
                dicCellValue.Add("7,1,P", payManPath);
            }
            else
            {
                dicCellValue.Add("7,1", entity.PayManName);
            }
            
            //出纳检查打款时间
            if (entity.PayDate.HasValue)
                dicCellValue.Add("7,2", entity.PayDate.Value.ToString("yyyy.MM.dd"));
            //出纳检查打款意见
            if (!String.IsNullOrEmpty(entity.PayDateInfo))
                dicCellValue.Add("7,4", entity.PayDateInfo.Replace(replaceOld, replaceNew));

            //领款人
            if (File.Exists(receiveManPath))
            {
                //ZhiFang.Common.Log.Log.Info("领款人电子签名路径：" + receiveManPath);
                dicCellValue.Add("7,7,P", receiveManPath);
            }
            else
            {
                dicCellValue.Add("7,7", entity.ReceiveManName);
            }
            
            //领款时间
            if (entity.ReceiveDate.HasValue)
                dicCellValue.Add("7,8", entity.ReceiveDate.Value.ToString("yyyy.MM.dd"));
            //领款意见
            if (!String.IsNullOrEmpty(entity.ReceiveManInfo))
                dicCellValue.Add("7,10", entity.ReceiveManInfo.Replace(replaceOld, replaceNew));
            #endregion
            return dicCellValue;
        }
        #endregion
    }
}