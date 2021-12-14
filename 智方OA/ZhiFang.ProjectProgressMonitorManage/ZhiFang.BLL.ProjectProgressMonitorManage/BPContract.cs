using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.OA;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;
using System.IO;
using ZhiFang.ProjectProgressMonitorManage.Common;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPContract : BaseBLL<PContract>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPContract
    {
        public IBSCOperation IBSCOperation { get; set; }
        public IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }
        public IBBParameter IBBParameter { get; set; }
        public IDPReceivePlanDao IDPReceivePlanDao { get; set; }
        public IDPClientDao IDPClientDao { get; set; }

        public BaseResultDataValue BPContractAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction)
        {
            BaseResultDataValue brb = new BaseResultDataValue();
            if (Entity.ContractStatus == PContractStatus.暂存.Key || Entity.ContractStatus == PContractStatus.申请.Key)
            {
                if (Entity.ContractNumber != null && Entity.ContractNumber.Trim() != "")
                {
                    var tmplist = DBDao.GetListByHQL(" ContractNumber='" + Entity.ContractNumber.Trim() + "' ");
                    if (tmplist != null && tmplist.Count > 0)
                    {
                        brb.ErrorInfo = "BPContractAdd.Add错误!合同编号ContractNumber:" + Entity.ContractNumber.Trim() + "已存在！";
                        brb.success = false;
                        return brb;
                    }
                }
                Entity.ApplyDate = Entity.DataAddTime;
                Entity.LeftMoney = Entity.Amount - Entity.PayedMoney;
                if (base.Add())
                {
                    IBSCOperation.AddOperationEntityStatus(this.Entity);
                    if (Entity.ContractStatus.ToString() == PContractStatus.申请.Key)
                        PContractStatusMessagePush(pushWeiXinMessageAction, this.Entity.Id, this.Entity.ContractStatus.ToString(), this.Entity);
                    return brb;
                }
                else
                {
                    brb.ErrorInfo = "BPContractAdd.Add错误！";
                    brb.success = false;
                }

                //else
                //{
                //    brb.ErrorInfo = "BPContractAdd.Add错误!合同编号ContractNumber为空！";
                //    brb.success = false;
                //}
            }
            else
            {
                brb.ErrorInfo = "BPContractAdd.Add错误！状态：" + Entity.ContractStatus;
                brb.success = false;
            }
            return brb;
        }

        private void PContractStatusMessagePush(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long id, string StatusId, PContract entity)
        {
            List<long> receiveidlist = new List<long>();
            string message = "";
            string sendmen = "";
            PContract pcontract = entity;
            if (pcontract == null)
                pcontract = DBDao.Get(id);
            //string url = "WeiXin/WeiXinMainRouter.aspx?operate=TASKINFO&id=" + PTaskId + "&IsSingle=1" + "&name=" + ptask.CName;
            #region 申请
            if (StatusId.Trim() == PContractStatus.暂存.Key)
            {

            }
            if (StatusId.Trim() == PContractStatus.申请.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id in( " + RoleList.合同商务评审.Key + ", " + RoleList.合同技术评审.Key + ") ");
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }
                message = "您收到待评审的合同申请（合同名称：" + pcontract.Name + "，所属客户：" + pcontract.PClientName + "，金额：" + pcontract.Amount + "），申请人：" + pcontract.ApplyMan + "。";
            }
            #endregion
            #region 技术已评
            if (StatusId.Trim() == PContractStatus.技术已评.Key)
            {
                receiveidlist.Add(pcontract.ApplyManID.Value);
                message = "您的合同申请（合同名称：" + pcontract.Name + "，所属客户：" + pcontract.PClientName + "，金额：" + pcontract.Amount + "）,已被" + pcontract.TechReviewMan + "定为'技术已评'状态。";
            }
            #endregion
            #region 商务已评
            if (StatusId.Trim() == PContractStatus.商务已评.Key)
            {
                receiveidlist.Add(pcontract.ApplyManID.Value);
                message = "您的合同申请（合同名称：" + pcontract.Name + "，所属客户：" + pcontract.PClientName + "，金额：" + pcontract.Amount + "）,已被" + pcontract.TechReviewMan + "定为'商务已评'状态。";
            }
            #endregion
            #region 评审未通过
            if (StatusId.Trim() == PContractStatus.评审未通过.Key)
            {
                //ZhiFang.Common.Log.Log.Debug("PContractStatusMessagePush.ApplyManID:" + pcontract.ApplyManID.Value);
                receiveidlist.Add(pcontract.ApplyManID.Value);
                string tmpman = "";
                if (pcontract.ReviewManID.HasValue && pcontract.ReviewManID.Value > 0)
                {
                    //ZhiFang.Common.Log.Log.Debug("PContractStatusMessagePush.ReviewManID:" + pcontract.ReviewManID.Value);
                    tmpman = pcontract.ReviewMan;
                }
                if (pcontract.TechReviewManID.HasValue && pcontract.TechReviewManID.Value > 0)
                {
                    //ZhiFang.Common.Log.Log.Debug("PContractStatusMessagePush.ApplyManID:" + pcontract.TechReviewManID.Value);
                    tmpman = pcontract.TechReviewMan;
                }
                message = "您的合同申请（合同名称：" + pcontract.Name + "，所属客户：" + pcontract.PClientName + "，金额：" + pcontract.Amount + "）,已被：" + tmpman + "定为'评审未通过'状态。";
            }
            #endregion
            #region 评审通过
            if (StatusId.Trim() == PContractStatus.评审通过.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id in( " + RoleList.商务助理.Key + ") ");
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }
                message = "您收到审核通过的合同（合同名称：" + pcontract.Name + "，所属客户：" + pcontract.PClientName + "，金额：" + pcontract.Amount + "）,已被技术评审人：" + pcontract.TechReviewMan + "，商务评审人：" + pcontract.ReviewMan + "定为'评审通过'状态。";
            }
            #endregion
            #region 正式签署
            if (StatusId.Trim() == PContractStatus.正式签署.Key)
            {
                receiveidlist.Add(pcontract.ApplyManID.Value);
                message = "您的合同申请（合同名称：" + pcontract.Name + "，所属客户：" + pcontract.PClientName + "，金额：" + pcontract.Amount + "）,已被" + pcontract.SignMan + "定为'正式签署'状态。";
            }
            #endregion
            #region 已验收
            if (StatusId.Trim() == PContractStatus.已验收.Key)
            {
                receiveidlist.Add(pcontract.ApplyManID.Value);
                message = "您的合同申请（合同名称：" + pcontract.Name + "，所属客户：" + pcontract.PClientName + "，金额：" + pcontract.Amount + "）,已为'已验收'状态。"; ;
            }
            #endregion
            if (receiveidlist.Count > 0)
            {
                ZhiFang.Common.Log.Log.Debug("PContractStatusMessagePush.receiveidlist.Count:" + receiveidlist.Count);
                Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                string urgencycolor = "#11cd6e";
                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你收到合同管理模块信息" });
                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = message });
                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = "OA系统：合同管理" });
                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = pcontract.ApplyMan });
                string tmpdatetime = (pcontract.DataAddTime.HasValue) ? pcontract.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = tmpdatetime });
                data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "请登录OA查看" });
                //ZhiFang.Common.Log.Log.Debug("PContractStatusMessagePush.receiveidlist.Count:@" + receiveidlist.Count);
                IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, receiveidlist, data, "pcontract", "");
                //ZhiFang.Common.Log.Log.Debug("PContractStatusMessagePush.receiveidlist.Count:@@" + receiveidlist.Count);
            }
        }

        public BaseResultBool UpdatePContractStatus(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long EmpID, string EmpName)
        {
            BaseResultBool brb = new BaseResultBool();
            PContract entity = this.Entity;
            var tmpa = tempArray.ToList();
            PContract tmpPContract = new PContract();
            tmpPContract = DBDao.Get(entity.Id);
            if (tmpPContract == null)
            {
                brb.ErrorInfo = "合同ID：" + entity.Id + "为空！";
                brb.success = false;
                return brb;
            }
            if (entity.ContractStatus.ToString() == PContractStatus.商务已评.Key)
            {
                if ((entity.ContractNumber == null || entity.ContractNumber.Trim() == "") && (tmpPContract.ContractNumber == null || tmpPContract.ContractNumber.Trim() == ""))
                {
                    return new BaseResultBool() { ErrorInfo = "合同ID：" + entity.Id + "的合同编号为空！", success = false };
                }
                else
                {
                    if (entity.ContractNumber != null && entity.ContractNumber.Trim() != "")
                    {
                        if (entity.ContractNumber != tmpPContract.ContractNumber)
                        {
                            var tmplist = DBDao.GetListByHQL(" ContractNumber='" + entity.ContractNumber.Trim() + "' ");
                            if (tmplist != null && tmplist.Count > 0)
                            {
                                brb.ErrorInfo = "BPContractAdd.Add错误!合同编号ContractNumber:" + Entity.ContractNumber.Trim() + "已存在！";
                                brb.success = false;
                                return brb;
                            }
                        }
                    }
                }

            }

            if (!PContractStatusUpdateCheck(entity, tmpPContract, EmpID, EmpName, tmpa))
            {
                return new BaseResultBool() { ErrorInfo = "合同ID：" + entity.Id + "的状态为：" + PContractStatus.GetStatusDic()[tmpPContract.ContractStatus.ToString()].Name + "！", success = false };
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                IBSCOperation.AddOperationEntityStatus(this.Entity);
                if ((entity.ContractStatus.ToString() == PContractStatus.技术已评.Key && tmpPContract.ContractStatus.ToString() == PContractStatus.商务已评.Key) || (entity.ContractStatus.ToString() == PContractStatus.商务已评.Key && tmpPContract.ContractStatus.ToString() == PContractStatus.技术已评.Key))
                {
                    DBDao.UpdateByHql(" update PContract set  ContractStatus='" + PContractStatus.评审通过.Key + "' where Id=" + entity.Id);
                }
                PContractStatusMessagePush(pushWeiXinMessageAction, entity.Id, entity.ContractStatus.ToString(), null);
                brb.success = true;
            }
            else
            {
                brb.ErrorInfo = "UpdatePContractStatus.Add错误！";
                brb.success = false;
            }
            return brb;
        }

        bool PContractStatusUpdateCheck(PContract entity, PContract tmpPContract, long EmpID, string EmpName, List<string> tmpa)
        {
            #region 暂存
            if (entity.ContractStatus.ToString() == PContractStatus.暂存.Key)
            {
                if (tmpPContract.ContractStatus.ToString() != PContractStatus.暂存.Key && tmpPContract.ContractStatus.ToString() != PContractStatus.申请.Key && tmpPContract.ContractStatus.ToString() != PContractStatus.评审未通过.Key)
                {
                    return false;
                }
            }
            #endregion

            #region 申请
            if (entity.ContractStatus.ToString() == PContractStatus.申请.Key)
            {
                if (tmpPContract.ContractStatus.ToString() != PContractStatus.暂存.Key && tmpPContract.ContractStatus.ToString() != PContractStatus.评审未通过.Key && tmpPContract.ContractStatus.ToString() != PContractStatus.申请.Key)
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

                tmpa.Add("TechReviewManID=null");
                tmpa.Add("TechReviewMan=null");
                tmpa.Add("TechReviewDate=null");
                tmpa.Add("TechReviewInfo=null");
            }
            #endregion

            #region 商务已评
            if (entity.ContractStatus.ToString() == PContractStatus.商务已评.Key)
            {
                if (tmpPContract.ContractStatus.ToString() != PContractStatus.申请.Key && tmpPContract.ContractStatus.ToString() != PContractStatus.技术已评.Key)
                {
                    return false;
                }


                tmpa.Add("ReviewManID=" + EmpID + " ");
                tmpa.Add("ReviewMan='" + EmpName + "'");
                tmpa.Add("ReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ReviewInfo='" + entity.ReviewInfo + "'");
            }
            #endregion

            #region 技术已评
            if (entity.ContractStatus.ToString() == PContractStatus.技术已评.Key)
            {
                if (tmpPContract.ContractStatus.ToString() != PContractStatus.申请.Key && tmpPContract.ContractStatus.ToString() != PContractStatus.商务已评.Key && tmpPContract.ContractStatus.ToString() != PContractStatus.技术已评.Key)
                {
                    return false;
                }
                tmpa.Add("TechReviewManID=" + EmpID + " ");
                tmpa.Add("TechReviewMan='" + EmpName + "'");
                tmpa.Add("TechReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("TechReviewInfo='" + entity.TechReviewInfo + "'");
            }
            #endregion

            #region 评审未通过
            if (entity.ContractStatus.ToString() == PContractStatus.评审未通过.Key)
            {
                if (tmpPContract.ContractStatus.ToString() != PContractStatus.申请.Key && tmpPContract.ContractStatus.ToString() != PContractStatus.商务已评.Key && tmpPContract.ContractStatus.ToString() != PContractStatus.技术已评.Key)
                {
                    return false;
                }
                if (entity.ReviewManID.HasValue && entity.ReviewManID.Value > 0)
                {
                    tmpa.Add("ReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
                if (entity.TechReviewManID.HasValue && entity.TechReviewManID.Value > 0)
                {
                    tmpa.Add("TechReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            #endregion

            #region 正式签署
            if (entity.ContractStatus.ToString() == PContractStatus.正式签署.Key)
            {
                if (tmpPContract.ContractStatus.ToString() != PContractStatus.评审通过.Key)
                {
                    return false;
                }
                tmpa.Add("SignManID=" + EmpID + " ");
                tmpa.Add("SignMan='" + EmpName + "'");
                tmpa.Add("SignDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            #endregion

            #region 已验收
            if (entity.ContractStatus.ToString() == PContractStatus.已验收.Key)
            {
                if (tmpPContract.ContractStatus.ToString() != PContractStatus.正式签署.Key)
                {
                    return false;
                }

                //tmpa.Add("InvoiceManID=" + EmpID + " ");
                //tmpa.Add("InvoiceManName='" + EmpName + "'");
                //tmpa.Add("InvoiceDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            #endregion


            return true;
        }

        #region 合同评审打印
        public BaseResultDataValue ExcelToPdfFile(long id, bool isPreview, string templetName, ref string fileName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            PContract entity = this.Get(id);
            if (String.IsNullOrEmpty(templetName))
                templetName = "合同评审表固定模板.xlsx";
            baseResultDataValue = FillMaintenanceDataToExcel(entity, id, templetName);
            if (entity != null)
            {
                if (entity.ApplyDate.HasValue)
                    fileName = entity.ApplyDate.Value.ToString("yyyy年MM月dd日");
                fileName = fileName + entity.Name + "合同.pdf";
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.ErrorInfo = "entity为空";
                ZhiFang.Common.Log.Log.Error("合同评审打印" + baseResultDataValue.ErrorInfo);
            }
            if (baseResultDataValue.success)
            {
                try
                {
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNo.ExcelExportSavePath.ToString());

                    parentPath = parentPath + "\\PContract\\" + entity.LabID;
                    if (isPreview)
                    {
                        parentPath = parentPath + "\\TempPDFFile";
                        if (!String.IsNullOrEmpty(entity.ApplyMan))
                            parentPath = parentPath + "\\" + entity.ApplyMan;
                    }
                    else
                        parentPath = parentPath + "\\ExcelFile";

                    string pdfFile = parentPath + "\\" + id + ".pdf";
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
        public BaseResultDataValue FillMaintenanceDataToExcel(PContract entity, long id, string templetName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取合同信息";
                ZhiFang.Common.Log.Log.Info("无法获取合同信息,ID：" + id.ToString());
                return baseResultDataValue;
            }
            //获取合同的模板信息
            string parentPath = (string)IBBParameter.GetCache(BParameterParaNo.ExcelExportSavePath.ToString());
            parentPath = parentPath + "\\PContract\\";
            string templetPath = parentPath + "Templet\\" + templetName;

            string extName = Path.GetExtension(templetPath);

            string dicFile = parentPath + entity.LabID.ToString() + "\\TempExcelFile";
            if (!String.IsNullOrEmpty(entity.ApplyMan))
                dicFile = dicFile + "\\" + entity.ApplyMan;
            // + "\\" + DateTime.Parse(entity.ApplyDate.ToString()).ToString("yyyyMMdd")
            string excelFile = dicFile + "\\" + entity.Id + extName;
            //ZhiFang.Common.Log.Log.Info("excelFile：" + excelFile);
            if (!Directory.Exists(dicFile))
            {
                Directory.CreateDirectory(dicFile);
            }
            Dictionary<string, string> dicCellValue = null;
            IList<PContract> listData = new List<PContract>();
            listData.Add(entity);

            if ((listData != null) && (listData.Count > 0))
                dicCellValue = _FillExcelCell(entity);

            baseResultDataValue.success = MyNPOIHelper.FillExcelMoudleSheet(templetPath, excelFile, dicCellValue, false);

            if (baseResultDataValue.success)
                baseResultDataValue.ResultDataValue = excelFile;
            return baseResultDataValue;
        }
        private Dictionary<string, string> _FillExcelCell(PContract entity)
        {
            Dictionary<string, string> dicCellValue = new Dictionary<string, string>();

            #region 电子签名路径
            //上传电子签名保存路径
            string parentEmpSignPath = (string)IBBParameter.GetCache(BParameterParaNo.UploadEmpSignPath.ToString());
            string principalPath = "", applyManPath = "", reviewManPath = "", signManPath = "";

            if (entity.PrincipalID.HasValue)
                principalPath = Path.Combine(parentEmpSignPath, entity.PrincipalID + ".png");

            if (entity.ApplyManID.HasValue)
                applyManPath = Path.Combine(parentEmpSignPath, entity.ApplyManID + ".png");
            if (entity.ReviewManID.HasValue)
                reviewManPath = Path.Combine(parentEmpSignPath, entity.ReviewManID + ".png");
            if (entity.SignManID.HasValue)
                signManPath = Path.Combine(parentEmpSignPath, entity.SignManID + ".png");
            #endregion

            //填充第一行,标题
            if (!String.IsNullOrEmpty(entity.Compname))
                dicCellValue.Add("0,0", entity.Compname + "有限公司合同评审表");
            else
            {
                dicCellValue.Add("0,0", "合同评审表");
            }
            string replaceOld = @"<br />", replaceNew = "\r\n";

            #region 填充第二行
            //部门
            dicCellValue.Add("1,1", entity.DeptName);
            //销售负责人
            if (File.Exists(principalPath))
            {
                //ZhiFang.Common.Log.Log.Info("申请人电子签名路径：" + principalPath);
                dicCellValue.Add("1,5,P", principalPath);
            }
            else
            {
                dicCellValue.Add("1,5", entity.Principal);
            }
            //申请时间
            if (entity.ApplyDate.HasValue)
                dicCellValue.Add("1,7", entity.ApplyDate.Value.ToString("yyyy.MM.dd"));
            #endregion
            //填充第三行:项目类别
            //□网络项目 □单机升级  □单机服务  □单机项目 □网络升级 □网络服务 □网络新连机\r\n□日立特配 □EQA/IQC   □独立实验室 □前处理仪器□网络增项  □QMS□其它
            dicCellValue.Add("2,1", entity.Content);

            #region 评审内容
            //用户名称
            dicCellValue.Add("3,2", entity.PClientName);
            //付款单位
            dicCellValue.Add("3,6", entity.PayOrg);
            #region 用户联系方式
            if (entity.PClientID.HasValue)
            {
                PClient pclient = IDPClientDao.Get(long.Parse(entity.PClientID.Value.ToString()));
                //联系人
                dicCellValue.Add("4,3", pclient.LinkMan);
                //职务
                dicCellValue.Add("4,6", "");
                //电话
                dicCellValue.Add("4,8", pclient.PhoneNum);
            }
            #endregion
            #region 付款方联系方式
            if (entity.PayOrgID.HasValue)
            {
                PClient pclient = IDPClientDao.Get(long.Parse(entity.PayOrgID.Value.ToString()));
                //联系人
                dicCellValue.Add("5,3", pclient.LinkMan);
                //职务
                dicCellValue.Add("5,6", "");
                //电话
                dicCellValue.Add("5,8", pclient.PhoneNum);
            }
            #endregion

            //合同数量
            dicCellValue.Add("6,2", "");
            //是否邮寄
            dicCellValue.Add("6,6", "□是      □否");
            if (entity.PClientID.HasValue)
            {
                PClient pclient = IDPClientDao.Get(long.Parse(entity.PClientID.Value.ToString()));
                //收件人
                dicCellValue.Add("7,3", pclient.LinkMan);
                //收件电话
                dicCellValue.Add("7,6", pclient.PhoneNum);
                //收件地址
                dicCellValue.Add("8,2", pclient.Address);
            }
            //合同(项目)名称
            dicCellValue.Add("9,2", entity.Name);
            //合同总额
            dicCellValue.Add("10,2", entity.Amount.ToString());
            //软件
            dicCellValue.Add("11,3", entity.Software.ToString());
            //硬件
            dicCellValue.Add("11,5", entity.Hardware.ToString());
            //服务费
            //dicCellValue.Add("11,7", entity.AnnualServiceCharge.ToString());

            //合同服务费用
            dicCellValue.Add("11,7", entity.ContractServiceCharge);
            

            //其他费用
            dicCellValue.Add("12,2", entity.MiddleFee.ToString());
            //联系人
            dicCellValue.Add("12,4", entity.LinkMan);
            //职务
            dicCellValue.Add("12,6", "");
            //电话
            dicCellValue.Add("12,8", entity.LinkPhoneNo);

            #region 硬件及软件采购信息
            //<table data-sort='sortDisabled' bgcolor='#ffffff' border='1' bordercolor='#000000' cellpadding='3' cellspacing='2' height='166' width='586'><tbody><tr class='firstRow'><td rowspan='2' colspan='1'><span style='font-family:黑体size=3'>硬件采购</span></td><td><span style='font-family:黑体size=3'>采购总额</span></td><td><span style='font-family:黑体size=3'>物品名称</span></td><td><span style='font-family:黑体size=3'>单价</span></td><td><span style='font-family:黑体size=3'>数量</span></td></tr><tr><td>11</td><td>22</td><td>33</td><td>44</td></tr><tr><td rowspan='2' colspan='1'><span style='font-family:黑体size=3'>软件采购</span></td><td><span style='font-family:黑体size=3'>采购总额</span></td><td><span style='font-family:黑体size=3'>物品名称</span></td><td><span style='font-family:黑体size=3'>单价</span></td><td><span style='font-family:黑体size=3'>数量</span></td></tr><tr><td>111</td><td>222</td><td>333</td><td>444</td></tr></tbody></table>
            if (!String.IsNullOrEmpty(entity.PurchaseDescHTML))
            {
                Regex reg = new Regex(@"(?is)<tr[^>]*>(.*?)</tr>");
                MatchCollection mc = reg.Matches(entity.PurchaseDescHTML.ToLower());
                if (mc != null && mc.Count == 4)
                {
                    Regex reg2 = new Regex(@"(?is)td[^>]*>(.*?)</td>");
                    //硬件采购
                    Match rowOne = mc[1];
                    MatchCollection mcOne = reg2.Matches(rowOne.ToString());
                    for (int i = 0; i < mcOne.Count; i++)
                    {
                        string result = mcOne[i].ToString().Replace(@"</td>", "");
                        result = result.Replace("&nbsp;", "");
                        result = result.Replace(replaceOld, "");
                        result = result.Replace("<br/>", "");
                        var index = result.IndexOf(">");
                        result = result.Substring(index + 1);
                        //ZhiFang.Common.Log.Log.Info("rowOne-td" + result);
                        switch (i)
                        {
                            case 0:
                                dicCellValue.Add("14,2", result);
                                break;
                            case 1:
                                dicCellValue.Add("14,4", result);
                                break;
                            case 2:
                                dicCellValue.Add("14,6", result);
                                break;
                            case 3:
                                dicCellValue.Add("14,8", result);
                                break;
                            default:
                                break;
                        }
                    }
                    //软件采购
                    Match rowTwo = mc[3];
                    MatchCollection mcTwo = reg2.Matches(rowTwo.ToString());
                    for (int i = 0; i < mcTwo.Count; i++)
                    {
                        string result = mcTwo[i].ToString().Replace(@"</td>", "");
                        result = result.Replace("&nbsp;", "");
                        result = result.Replace(replaceOld, "");
                        result = result.Replace("<br/>", "");
                        var index = result.IndexOf(">");
                        result = result.Substring(index + 1);
                        //ZhiFang.Common.Log.Log.Info("rowTwo-td" + result);
                        switch (i)
                        {
                            case 0:
                                dicCellValue.Add("16,2", result);
                                break;
                            case 1:
                                dicCellValue.Add("16,4", result);
                                break;
                            case 2:
                                dicCellValue.Add("16,6", result);
                                break;
                            case 3:
                                dicCellValue.Add("16,8", result);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            #endregion

            //服务内容
            dicCellValue.Add("17,2", "");

            //实施人员
            dicCellValue.Add("18,2", "");
            //实施时间
            dicCellValue.Add("18,5", "");
            //完成时间
            dicCellValue.Add("18,7", "");

            //其他说明
            dicCellValue.Add("19,2", "");
            #endregion

            #region 付款方式
            //需要从收款计划取出
            IList<PReceivePlan> tempPlanList = IDPReceivePlanDao.GetListByHQL(" PPReceivePlanID=null and PContractID=" + entity.Id);
            StringBuilder strb = new StringBuilder();
            if (tempPlanList != null)
            {
                var receivePlanlist = tempPlanList.OrderBy(a => a.ExpectReceiveDate);
                foreach (var item in receivePlanlist)
                {
                    //ZhiFang.Common.Log.Log.Info(item.ReceiveGradationName + item.ReceiveGradationName.Length);
                    if (item.ReceiveGradationName.Length == 2)
                        item.ReceiveGradationName = item.ReceiveGradationName + "  ";
                    strb.Append(item.ReceiveGradationName + "                       ");
                    string expectReceiveDate = "          ";
                    if (item.ExpectReceiveDate.HasValue)
                    {
                        expectReceiveDate = item.ExpectReceiveDate.Value.ToString("yyyy.MM.dd");
                    }
                    strb.Append(expectReceiveDate + "             ");
                    strb.Append(item.ReceivePlanAmount);
                    strb.Append(replaceNew);
                }
            }
            //预付,二期,尾款等
            dicCellValue.Add("21,1", strb.ToString());
            #endregion

            #region 项目风险预留为空
            dicCellValue.Add("22,1", "");
            #endregion

            //评审意见(审核意见及审批意见合并显示)
            string info = "";
            if (!String.IsNullOrEmpty(entity.ReviewInfo))
                info = "审核意见:" + entity.ReviewInfo;
            if (!String.IsNullOrEmpty(entity.TechReviewInfo))
                info = info + "  评审意见:" + entity.TechReviewInfo;
            dicCellValue.Add("23,1", info);

            #region 填充审核信息
            //审核人
            if (File.Exists(reviewManPath))
            {
                //ZhiFang.Common.Log.Log.Info("审核人电子签名路径：" + reviewManPath);
                dicCellValue.Add("24,1,P", reviewManPath);
            }
            else
            {
                dicCellValue.Add("24,1", entity.ReviewMan);
            }

            //审批人取签署人信息
            if (File.Exists(signManPath))
            {
                //ZhiFang.Common.Log.Log.Info("审批人电子签名路径：" + signManPath);
                dicCellValue.Add("24,4,P", signManPath);
            }
            else
            {
                dicCellValue.Add("24,4", entity.SignMan);
            }
            //签署人
            if (File.Exists(signManPath))
            {
                //ZhiFang.Common.Log.Log.Info("签署人电子签名路径：" + signManPath);
                dicCellValue.Add("24,8,P", signManPath);
            }
            else
            {
                dicCellValue.Add("24,8", entity.SignMan);
            }
            #endregion

            return dicCellValue;
        }
        #endregion

        /// <summary>
        /// 新增或修改合同时获取用户所属省份信息
        /// </summary>
        public void GetEntityProvinceInfo()
        {
            if (this.Entity == null) return;
            if (this.Entity.PClientID.HasValue)
            {
                PClient pclient = IDPClientDao.Get(this.Entity.PClientID.Value);
                this.Entity.ProvinceID = pclient.ProvinceID;
                this.Entity.ProvinceName = pclient.ProvinceName;
                //ZhiFang.Common.Log.Log.Debug("ProvinceName:"+ this.Entity.ProvinceName);
            }
        }

        public BaseResultDataValue SearchListTotalByHQL(string where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            object result=DBDao.GetTotalByHQL(where, fields);
            brdv.ResultDataValue= ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(result);
            return brdv;
        }
    }
}