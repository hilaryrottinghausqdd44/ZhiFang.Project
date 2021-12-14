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
using System.IO;
using ZhiFang.ProjectProgressMonitorManage.Common;
using ZhiFang.IBLL.ProjectProgressMonitorManage;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPInvoice : BaseBLL<PInvoice>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPInvoice
    {
        public IDSCOperationDao IDSCOperationDao { get; set; }
        public IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }
        public IDAO.ProjectProgressMonitorManage.IDPContractDao IDPContractDao { get; set; }
        public IBBParameter IBBParameter { get; set; }

        public override bool Add()
        {
            if (Entity.Status.ToString() == PInvoiceStatus.暂存.Key || Entity.Status.ToString() == PInvoiceStatus.申请.Key)
            {
                if (base.Add())
                {
                    SaveSCOperation(this.Entity);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public BaseResultDataValue PInvoiceAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction)
        {
            BaseResultDataValue brb = new BaseResultDataValue();
            if (Entity.Status.ToString() == PInvoiceStatus.暂存.Key || Entity.Status.ToString() == PInvoiceStatus.申请.Key)
            {
                if (base.Add())
                {
                    SaveSCOperation(this.Entity);
                    if (Entity.Status.ToString() == PInvoiceStatus.申请.Key)
                        PInvoiceStatusMessagePush(pushWeiXinMessageAction, this.Entity.Id, this.Entity.Status.ToString(), this.Entity);
                    return brb;
                }
                else
                {
                    brb.ErrorInfo = "PInvoiceAdd.Add错误！";
                    brb.success = false;
                }
            }
            else
            {
                brb.ErrorInfo = "PInvoiceAdd.Add错误！状态：" + Entity.Status.ToString();
                brb.success = false;
            }
            return brb;
        }

        public BaseResultBool PInvoiceStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long EmpID, string EmpName)
        {
            BaseResultBool brb = new BaseResultBool();
            PInvoice entity = this.Entity;
            var tmpa = tempArray.ToList();
            PInvoice tmpPInvoice = new PInvoice();
            tmpPInvoice = DBDao.Get(entity.Id);
            if (tmpPInvoice == null)
            {
                brb.ErrorInfo = "发票申请ID：" + entity.Id + "为空！";
                brb.success = false;
                return brb;
            }
            if (!PInvoiceStatusUpdateCheck(entity, tmpPInvoice, EmpID, EmpName, tmpa))
            {
                return new BaseResultBool() { ErrorInfo = "发票申请ID：" + entity.Id + "的状态为：" + PInvoiceStatus.GetStatusDic()[tmpPInvoice.Status.ToString()].Name + "！", success = false };
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                if (entity.Status.ToString() == PInvoiceStatus.已开票.Key)
                {
                    PContract contract = IDPContractDao.Get(tmpPInvoice.ContractID);
                    if (contract != null)
                    {
                        if (contract.InvoiceMoney > 0)
                        {
                            IDPContractDao.UpdateByHql("update PContract as pcontract set pcontract.InvoiceMoney=" + contract.InvoiceMoney + tmpPInvoice.InvoiceAmount + " where   pcontract.Id=" + entity.ContractID + "");
                        }
                        else
                        {
                            IDPContractDao.UpdateByHql("update PContract as pcontract set pcontract.InvoiceMoney=" + tmpPInvoice.InvoiceAmount + " where   pcontract.Id=" + entity.ContractID + "");
                        }
                    }
                }
                SaveSCOperation(entity);
                PInvoiceStatusMessagePush(pushWeiXinMessageAction, entity.Id, entity.Status.ToString(), null);
                brb.success = true;
            }
            else
            {
                brb.ErrorInfo = "PInvoiceStatusUpdate.Update错误！";
                brb.success = false;
            }
            return brb;
        }

        bool PInvoiceStatusUpdateCheck(PInvoice entity, PInvoice tmpPInvoice, long EmpID, string EmpName, List<string> tmpa)
        {
            #region 暂存
            if (entity.Status.ToString() == PInvoiceStatus.暂存.Key)
            {
                if (tmpPInvoice.Status.ToString() != PInvoiceStatus.暂存.Key && tmpPInvoice.Status.ToString() != PInvoiceStatus.申请.Key && tmpPInvoice.Status.ToString() != PInvoiceStatus.一审退回.Key)
                {
                    return false;
                }
            }
            #endregion

            #region 申请
            if (entity.Status.ToString() == PInvoiceStatus.申请.Key)
            {
                if (tmpPInvoice.Status.ToString() != PInvoiceStatus.暂存.Key && tmpPInvoice.Status.ToString() != PInvoiceStatus.一审退回.Key)
                {
                    return false;
                }

                tmpa.Add("ApplyManID=" + EmpID + " ");
                tmpa.Add("ApplyMan='" + EmpName + "'");
                tmpa.Add("ApplyDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.Add("ReviewManID=null");
                tmpa.Add("ReviewMan=null");
                tmpa.Add("ReviewDate=null");
                tmpa.Add("ReviewInfo=null");

                tmpa.Add("TwoReviewManID=null");
                tmpa.Add("TwoReviewMan=null");
                tmpa.Add("TwoReviewDate=null");
                tmpa.Add("TwoReviewInfo=null");
            }
            #endregion

            #region 一审通过
            if (entity.Status.ToString() == PInvoiceStatus.一审通过.Key)
            {
                if (tmpPInvoice.Status.ToString() != PInvoiceStatus.申请.Key && tmpPInvoice.Status.ToString() != PInvoiceStatus.二审退回.Key)
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
            if (entity.Status.ToString() == PInvoiceStatus.一审退回.Key)
            {
                if (tmpPInvoice.Status.ToString() != PInvoiceStatus.申请.Key && tmpPInvoice.Status.ToString() != PInvoiceStatus.二审退回.Key)
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
            if (entity.Status.ToString() == PInvoiceStatus.二审通过.Key)
            {
                if (tmpPInvoice.Status.ToString() != PInvoiceStatus.一审通过.Key)
                {
                    return false;
                }
                tmpa.Add("TwoReviewManID=" + EmpID + " ");
                tmpa.Add("TwoReviewMan='" + EmpName + "'");
                tmpa.Add("TwoReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("TwoReviewInfo='" + entity.TwoReviewInfo + "'");
            }
            #endregion

            #region 二审退回
            if (entity.Status.ToString() == PInvoiceStatus.二审退回.Key)
            {
                if (tmpPInvoice.Status.ToString() != PInvoiceStatus.一审通过.Key)
                {
                    return false;
                }
                tmpa.Add("TwoReviewManID=" + EmpID + " ");
                tmpa.Add("TwoReviewMan='" + EmpName + "'");
                tmpa.Add("TwoReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("TwoReviewInfo='" + entity.TwoReviewInfo + "'");
            }
            #endregion

            #region 已开票
            if (entity.Status.ToString() == PInvoiceStatus.已开票.Key)
            {
                if (tmpPInvoice.Status.ToString() != PInvoiceStatus.二审通过.Key)
                {
                    return false;
                }

                tmpa.Add("InvoiceManID=" + EmpID + " ");
                tmpa.Add("InvoiceManName='" + EmpName + "'");
                tmpa.Add("InvoiceDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            #endregion

            #region 已邮寄
            if (entity.Status.ToString() == PInvoiceStatus.已邮寄.Key)
            {
                if (tmpPInvoice.Status.ToString() != PInvoiceStatus.已开票.Key)
                {
                    return false;
                }
            }
            #endregion

            #region 已签收
            if (entity.Status.ToString() == PInvoiceStatus.已签收.Key)
            {
            }
            #endregion
            return true;
        }

        private void PInvoiceStatusMessagePush(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long PInvoiceId, string StatusId, PInvoice entity)
        {
            List<long> receiveidlist = new List<long>();
            string message = "";
            string sendmen = "";
            PInvoice pinvoice = entity;
            if (pinvoice == null)
            {
                pinvoice = DBDao.Get(PInvoiceId);
            }
            //string url = "WeiXin/WeiXinMainRouter.aspx?operate=TASKINFO&id=" + PTaskId + "&IsSingle=1" + "&name=" + ptask.CName;
            #region 申请
            if (StatusId.Trim() == PInvoiceStatus.暂存.Key)
            {

            }
            if (StatusId.Trim() == PInvoiceStatus.申请.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.商务助理.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                    message = "您收到待一审的发票（付款单位：" + pinvoice.PayOrgName + "，金额：" + pinvoice.InvoiceAmount + "），申请人：" + pinvoice.ApplyMan + "。";
                }
            }
            #endregion
            #region 一审
            if (StatusId.Trim() == PInvoiceStatus.一审通过.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.商务经理.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                    message = "您收到待二审的发票（付款单位：" + pinvoice.PayOrgName + "，金额：" + pinvoice.InvoiceAmount + "），申请人：" + pinvoice.ApplyMan + ",已被" + pinvoice.ReviewMan + "定为'一审通过'状态。";
                }
            }
            if (StatusId.Trim() == PInvoiceStatus.一审退回.Key)
            {
                if (pinvoice.ApplyManID.HasValue)
                {
                    receiveidlist.Add(pinvoice.ApplyManID.Value);
                    message = "您申请的发票（付款单位：" + pinvoice.PayOrgName + "，金额：" + pinvoice.InvoiceAmount + "）,已被" + pinvoice.ReviewMan + "定为'一审退回'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion
            #region 二审
            if (StatusId.Trim() == PInvoiceStatus.二审通过.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id= " + RoleList.出纳.Key);
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                    message = "您收到带开据的发票（付款单位：" + pinvoice.PayOrgName + "，金额：" + pinvoice.InvoiceAmount + "），申请人：" + pinvoice.ApplyMan + ",已被" + pinvoice.TwoReviewMan + "定为'二审通过'状态。";
                    //url += "&ExportType=0";
                }
            }
            if (StatusId.Trim() == PInvoiceStatus.二审退回.Key)
            {
                if (pinvoice.ReviewManID.HasValue)
                {
                    receiveidlist.Add(pinvoice.ReviewManID.Value);
                    message = "您申请的发票（付款单位：" + pinvoice.PayOrgName + "，金额：" + pinvoice.InvoiceAmount + "）,已被" + pinvoice.TwoReviewMan + "定为'二审退回'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion
            #region 已开票
            if (StatusId.Trim() == PInvoiceStatus.已开票.Key)
            {
                if (pinvoice.ApplyManID.HasValue)
                {
                    receiveidlist.Add(pinvoice.ApplyManID.Value);
                    message = "您申请的发票（付款单位：" + pinvoice.PayOrgName + "，金额：" + pinvoice.InvoiceAmount + "）,已被" + pinvoice.InvoiceManName + "定为'已开票'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion
            #region 邮寄
            if (StatusId.Trim() == PInvoiceStatus.已邮寄.Key)
            {
                if (pinvoice.ApplyManID.HasValue)
                {
                    receiveidlist.Add(pinvoice.ApplyManID.Value);
                    message = "您申请的发票（付款单位：" + pinvoice.PayOrgName + "，金额：" + pinvoice.InvoiceAmount + "）,已被" + pinvoice.ReviewMan + "定为'已邮寄'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion
            #region 已签收
            if (StatusId.Trim() == PInvoiceStatus.已签收.Key)
            {
                if (pinvoice.ReviewManID.HasValue)
                {
                    receiveidlist.Add(pinvoice.ReviewManID.Value);
                    message = "您邮寄的发票（付款单位：" + pinvoice.PayOrgName + "，金额：" + pinvoice.InvoiceAmount + "，快递公司：" + pinvoice.FreightName + ",快递单号：" + pinvoice.FreightOddNumbers + "）,已被" + pinvoice.ApplyMan + "定为'已签收'状态。";
                    //url += "&ExportType=0";
                }
            }
            #endregion

            if (receiveidlist.Count > 0)
            {
                Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                string urgencycolor = "#11cd6e";
                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你收到发票管理模块信息" });
                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = message });
                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = "OA系统：发票管理" });
                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = pinvoice.ApplyMan });
                string tmpdatetime = (pinvoice.DataAddTime.HasValue) ? pinvoice.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = tmpdatetime });
                data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "请登录OA查看" });
                IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, receiveidlist, data, "pinvoice", "");
            }
        }
        private void SaveSCOperation(PInvoice entity)
        {
            SCOperation sco = new SCOperation();
            sco.BobjectID = entity.Id;
            string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (empid != null && empid.Trim() != "")
                sco.CreatorID = long.Parse(empid);
            if (empname != null && empname.Trim() != "")
                sco.CreatorName = empname;
            sco.BusinessModuleCode = "PInvoice";
            sco.Memo = entity.OperationMemo;

            sco.Type = entity.Status;
            sco.TypeName = PInvoiceStatus.GetStatusDic()[entity.Status.ToString()].Name;
            IDSCOperationDao.Save(sco);
        }

        public override PInvoice Get(long longID)
        {
            return SetStatus(base.Get(longID));
        }
        public override IList<PInvoice> Search()
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
        public override EntityList<PInvoice> SearchListByHQL(string strHqlWhere, string Order, int page, int count)
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
        public override EntityList<PInvoice> SearchListByHQL(string strHqlWhere, int page, int count)
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
        public PInvoice SetStatus(PInvoice p)
        {
            if (p == null)
            {
                return null;
            }
            if (p.Status > 0)
            {
                var statuslist = PInvoiceStatus.GetStatusDic();
                if (statuslist.Keys.Contains(p.Status.ToString()))
                {
                    p.StatusName = statuslist[p.Status.ToString()].Name;
                }
            }
            return p;
        }

        public EntityList<PInvoice> SearchPInvoiceByExportType(long ExportType, long empid, string where, string sort, int Page, int Limit)
        {
            ZhiFang.Common.Log.Log.Debug("SearchPInvoiceByExportType.where:" + where);
            string hql = " 1=1 ";
            #region 浏览查看类型。-1：全部、0：我申请、1：我一审、2：我二审、3：我开票、4：我邮寄

            if (ExportType == 0)
            {
                hql += " and ApplyManID =" + empid + " ";
            }
            if (ExportType == 1)
            {
                string tmphql = " ReviewManID =" + empid + " or ReviewManID=null ";
                hql += " and (" + tmphql + ") ";
                ZhiFang.Common.Log.Log.Debug("Review.hql:" + hql);
            }
            if (ExportType == 2)
            {
                string tmphql = " TwoReviewManID =" + empid + " or TwoReviewManID=null ";
                hql += " and (" + tmphql + ") ";
                ZhiFang.Common.Log.Log.Debug("TwoReview.hql:" + hql);
            }
            if (ExportType == 3)
            {
                string tmphql = " InvoiceManID =" + empid + " or InvoiceManID=null ";
                hql += " and (" + tmphql + ") ";
                ZhiFang.Common.Log.Log.Debug("Invoice.hql:" + hql);

            }
            if (ExportType == 4)
            {
                hql += " and ApplyManID =" + empid + " ";
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

        public EntityList<PInvoice> SearchPInvoiceByExportType(long ExportType, long empid, string where, int page, int limit)
        {
            return SearchPInvoiceByExportType(ExportType, empid, where, null, page, limit);
        }
        #region 发票打印
        public BaseResultDataValue ExcelToPdfFile(long id, bool isPreview, string templetName, ref string fileName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            PInvoice entity = this.Get(id);
            if (String.IsNullOrEmpty(templetName))
                templetName = "发票开具申请表固定模板.xlsx";
            baseResultDataValue = FillMaintenanceDataToExcel(entity, id, templetName);

            if (entity != null)
            {
                if (entity.ApplyDate.HasValue)
                    fileName = entity.ApplyDate.Value.ToString("yyyy年MM月dd日");
                fileName = fileName + entity.ApplyMan + "发票.pdf";
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.ErrorInfo = "entity为空";
                ZhiFang.Common.Log.Log.Error("发票打印" + baseResultDataValue.ErrorInfo);
            }
            if (baseResultDataValue.success)
            {
                try
                {
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNo.ExcelExportSavePath.ToString());

                    parentPath = parentPath + "\\PInvoice\\" + entity.LabID;
                    if (isPreview)
                        parentPath = parentPath + "\\TempPDFFile\\" + entity.ApplyMan;
                    // + "\\" + DateTime.Parse(entity.ApplyDate.ToString()).ToString("yyyyMMdd")
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
        public BaseResultDataValue FillMaintenanceDataToExcel(PInvoice entity, long id, string templetName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取发票信息";
                ZhiFang.Common.Log.Log.Info("无法获取发票信息,ID：" + id.ToString());
                return baseResultDataValue;
            }
            //获取发票的模板信息
            string parentPath = (string)IBBParameter.GetCache(BParameterParaNo.ExcelExportSavePath.ToString());
            parentPath = parentPath + "\\PInvoice\\";
            string templetPath = parentPath + "Templet\\" + templetName;

            string extName = Path.GetExtension(templetPath);

            string dicFile = parentPath + entity.LabID.ToString() + "\\TempExcelFile";
            if (!String.IsNullOrEmpty(entity.ApplyMan))
                parentPath = parentPath + "\\" + entity.ApplyMan;

            string excelFile = dicFile + "\\" + entity.Id + extName;
            ZhiFang.Common.Log.Log.Info("excelFile：" + excelFile);
            if (!Directory.Exists(dicFile))
            {
                Directory.CreateDirectory(dicFile);
            }
            Dictionary<string, string> dicCellValue = null;
            IList<PInvoice> listData = new List<PInvoice>();
            listData.Add(entity);

            if ((listData != null) && (listData.Count > 0))
                dicCellValue = _FillExcelCell(entity);

            baseResultDataValue.success = MyNPOIHelper.FillExcelMoudleSheet(templetPath, excelFile, dicCellValue, false);

            if (baseResultDataValue.success)
                baseResultDataValue.ResultDataValue = excelFile;
            return baseResultDataValue;
        }
        private Dictionary<string, string> _FillExcelCell(PInvoice entity)
        {
            Dictionary<string, string> dicCellValue = new Dictionary<string, string>();

            #region 电子签名路径
            //上传电子签名保存路径
            string parentEmpSignPath = (string)IBBParameter.GetCache(BParameterParaNo.UploadEmpSignPath.ToString());
            string applyManPath = "", reviewManPath = "", twoReviewManPath = "";
            if (entity.ApplyManID.HasValue)
                applyManPath = Path.Combine(parentEmpSignPath, entity.ApplyManID + ".png");
            if (entity.ReviewManID.HasValue)
                reviewManPath = Path.Combine(parentEmpSignPath, entity.ReviewManID + ".png");
            if (entity.TwoReviewManID.HasValue)
                twoReviewManPath = Path.Combine(parentEmpSignPath, entity.TwoReviewManID + ".png");

            #endregion

            //填充第一行,标题
            if (!String.IsNullOrEmpty(entity.ComponeName))
                dicCellValue.Add("0,0", entity.ComponeName + "发票开具申请表");
            string replaceOld = @"<br />", replaceNew = "\r\n";

            #region 填充第二行
            //部门
            dicCellValue.Add("1,1", entity.DeptName);
            //申请人
            if (File.Exists(applyManPath))
            {
                //ZhiFang.Common.Log.Log.Info("申请人电子签名路径：" + applyManPath);
                dicCellValue.Add("1,5,P", applyManPath);
            }
            else
            {
                dicCellValue.Add("1,5", entity.ApplyMan);
            }
            //申请时间
            if (entity.ApplyDate.HasValue)
                dicCellValue.Add("1,9", entity.ApplyDate.Value.ToString("yyyy.MM.dd"));
            #endregion

            //填充第三行:项目类别
            dicCellValue.Add("2,1", entity.ProjectTypeName);

            //填充第四行:用户名称
            dicCellValue.Add("3,3", entity.ClientName);

            //填充第五行:新联机名称及SQH
            dicCellValue.Add("4,6", "");

            //填充第六行:其他项目类别
            dicCellValue.Add("5,6", "");

            #region 填充第七行
            //实施人员
            dicCellValue.Add("6,2", "");
            //是否实施
            dicCellValue.Add("6,4", "□已实施  □未实施");
            //计划回款时间
            if (entity.ExpectReceiveDate.HasValue)
                dicCellValue.Add("6,9", entity.ExpectReceiveDate.Value.ToString("yyyy.MM.dd"));
            //支付方式□√☑
            #endregion

            #region 发票开具
            //填充第八行:发票类型
            dicCellValue.Add("7,3", entity.InvoiceTypeName);
            //填充第九行:单位名称
            dicCellValue.Add("8,3", entity.PayOrgName);
            //填充第十行:增值税税号
            dicCellValue.Add("9,3", entity.VATNumber);
            //填充第十一行:增值税开户行
            dicCellValue.Add("10,3", entity.VATBank);
            //填充第十二行:增值税帐号
            dicCellValue.Add("11,3", entity.VATAccount);
            //增值税专用发票:
            dicCellValue.Add("12,4", entity.ClientAddress);
            dicCellValue.Add("13,4", entity.ClientPhone);
            //开票内容
            dicCellValue.Add("14,3", entity.InvoiceContentTypeName);

            //开票总金额
            dicCellValue.Add("15,2", entity.InvoiceAmount.ToString());
            //软件金额
            if (entity.SoftwareAmount.HasValue)
                dicCellValue.Add("16,2", entity.SoftwareAmount.Value.ToString());
            else
            {
                dicCellValue.Add("16,2", "");
            }
            //本次开票百分比
            dicCellValue.Add("16,3", entity.PercentAmount);
            //软件套数
            if (entity.SoftwareCount.HasValue)
                dicCellValue.Add("16,5", entity.SoftwareCount.Value.ToString());
            else
            {
                dicCellValue.Add("16,5", "");
            }
            //硬件数量
            if (entity.HardwareCount.HasValue)
                dicCellValue.Add("16,7", entity.HardwareCount.Value.ToString());
            else
            {
                dicCellValue.Add("16,7", "");
            }
            //开票金额
            dicCellValue.Add("16,9", entity.InvoiceAmount.ToString());
            //硬件金额
            if (entity.HardwareAmount.HasValue)
                dicCellValue.Add("17,2", entity.HardwareAmount.Value.ToString());
            else
            {
                dicCellValue.Add("17,2", "");
            }
            //服务费
            if (entity.ServerAmount.HasValue)
                dicCellValue.Add("18,2", entity.ServerAmount.Value.ToString());
            else
            {
                dicCellValue.Add("18,2", "");
            }
            #endregion

            #region 发票邮寄信息
            //收件人
            dicCellValue.Add("19,3", entity.ReceiveInvoiceName);
            //联系电话
            dicCellValue.Add("19,7", entity.ReceiveInvoicePhoneNumbers);
            //单位名称
            dicCellValue.Add("20,3", entity.FreightName);
            //邮寄地址
            dicCellValue.Add("21,3", entity.ReceiveInvoiceAddress);
            #endregion

            //特殊情况说明
            dicCellValue.Add("22,1", entity.InvoiceMemo);

            #region 填充审核信息
            //审核人
            if (File.Exists(reviewManPath))
            {
                ZhiFang.Common.Log.Log.Info("审核人电子签名路径：" + reviewManPath);
                dicCellValue.Add("23,1,P", reviewManPath);
            }
            else
            {
                dicCellValue.Add("23,1", entity.ReviewMan);
            }

            //审批人
            if (File.Exists(twoReviewManPath))
            {
                ZhiFang.Common.Log.Log.Info("审批人电子签名路径：" + twoReviewManPath);
                dicCellValue.Add("23,5,P", twoReviewManPath);
            }
            else
            {
                dicCellValue.Add("23,5", entity.TwoReviewMan);
            }

            //审批时间
            if (entity.TwoReviewDate.HasValue)
                dicCellValue.Add("23,9", entity.TwoReviewDate.Value.ToString("yyyy.MM.dd"));
            #endregion

            return dicCellValue;
        }
        #endregion
    }
}