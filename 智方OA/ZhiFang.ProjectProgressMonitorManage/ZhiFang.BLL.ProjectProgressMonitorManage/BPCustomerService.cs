using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.OA;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPCustomerService : BaseBLL<PCustomerService>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPCustomerService
    {
        public IDPCustomerServiceOperationLogDao IDPCustomerServiceOperationLogDao { get; set; }
        public IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }
        public IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { get; set; }
        public bool PCustomerServiceAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction)
        {
            //if (Entity.Status.ToString() == PCustomerServiceStatus.未处理.Key)
            //{
            HREmployee hremployee = IDHREmployeeDao.Get(Entity.ServiceAcceptanceManID.Value);
            if (hremployee != null && hremployee.HRDept != null)
            {
                Entity.DeptID = hremployee.HRDept.Id;
                Entity.DeptName = hremployee.HRDept.CName;
            }
            if (base.Add())
            {
                SavePCustomerServiceOperationLog(this.Entity);
                PCustomerServiceStatusMessagePush(pushWeiXinMessageAction, this.Entity.Id, this.Entity.Status.ToString(), this.Entity);
                return true;
            }
            else
            {
                return false;
            }
            //}
            //else
            //{
            //    return false;
            //}
        }

        private void PCustomerServiceStatusMessagePush(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long id, string StatusId, PCustomerService entity)
        {
            List<long> receiveidlist = new List<long>();
            string message = "";
            string sendmen = "";
            PCustomerService pcs = entity;
            if (pcs == null)
                pcs = DBDao.Get(id);
            //string url = "WeiXin/WeiXinMainRouter.aspx?operate=TASKINFO&id=" + PTaskId + "&IsSingle=1" + "&name=" + ptask.CName;
            #region 未处理
            if (StatusId.Trim() == PCustomerServiceStatus.未处理.Key)
            {

            }
            if (StatusId.Trim() == PCustomerServiceStatus.未处理.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.服务监管.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }
                rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.服务处理.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }

                message = "有新的服务单（用户：" + pcs.ClientName + "，请求人：" + pcs.RequestMan + "，联系电话：" + pcs.RequestManPhone + "），受理人：" + pcs.ServiceAcceptanceMan + "。";
            }
            #endregion
            #region 处理中
            if (StatusId.Trim() == PCustomerServiceStatus.处理中.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.服务监管.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }
                receiveidlist.Add(pcs.ServiceAcceptanceManID.Value);
                message = "服务单（用户：" + pcs.ClientName + "，请求人：" + pcs.RequestMan + "，联系电话：" + pcs.RequestManPhone + ",已被" + pcs.ServiceOperationCompleteMan + "定为'处理中'状态。";

            }
            #endregion
            #region 已完成
            if (StatusId.Trim() == PCustomerServiceStatus.已完成.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.服务监管.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }
                receiveidlist.Add(pcs.ServiceAcceptanceManID.Value);
                message = "服务单（用户：" + pcs.ClientName + "，请求人：" + pcs.RequestMan + "，联系电话：" + pcs.RequestManPhone + ",已被" + pcs.ServiceOperationCompleteMan + "定为'已完成'状态。";
            }

            #endregion
            if (receiveidlist.Count > 0)
            {
                Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                string urgencycolor = "#11cd6e";
                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你收到服务管理模块信息" });
                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = message });
                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = "OA系统：服务管理" });
                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName)
            });
                string tmpdatetime = (pcs.DataAddTime.HasValue) ? pcs.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = tmpdatetime });
                data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "请登录OA查看" });

                IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, receiveidlist, data, "PCustomerService", "");
            }
        }

        private void SavePCustomerServiceOperationLog(PCustomerService Entity)
        {
            PCustomerServiceOperationLog pcsol = new PCustomerServiceOperationLog();
            pcsol.BobjectID = Entity.Id;
            string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (empid != null && empid.Trim() != "")
                pcsol.CreatorID = long.Parse(empid);
            if (empname != null && empname.Trim() != "")
                pcsol.CreatorName = empname;
            pcsol.BusinessModuleCode = "PCustomerService";
            pcsol.Memo = Entity.ProblemMemo;

            pcsol.Type = Entity.Status;
            pcsol.TypeName = Entity.StatusCName;
            IDPCustomerServiceOperationLogDao.Save(pcsol);
        }

        public bool PCustomerServiceStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long empID, string empName)
        {
            var tmpa = tempArray.ToList();
            PCustomerService tmpPCustomerService = new PCustomerService();
            tmpPCustomerService = DBDao.Get(this.Entity.Id);           

            if (this.Update(tempArray))
            {
                SavePCustomerServiceOperationLog(this.Entity);
                PCustomerServiceStatusMessagePush(pushWeiXinMessageAction, this.Entity.Id, this.Entity.Status.ToString(), null);
                return true;
            }
            return false;
        }
    }
}