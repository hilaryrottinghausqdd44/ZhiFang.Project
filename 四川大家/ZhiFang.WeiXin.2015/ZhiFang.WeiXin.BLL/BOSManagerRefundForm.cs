using ZhiFang.BLL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.Entity.Base;
using System.IO;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.WeiXin.Common;
using System.Data;
using System.Reflection;
using ZhiFang.WeiXin.Entity.ViewObject.Request;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BOSManagerRefundForm : BaseBLL<OSManagerRefundForm>, ZhiFang.WeiXin.IBLL.IBOSManagerRefundForm
    {
        //    public ZhiFang.IDAO.RBAC.IDHRDeptDao IDHRDeptDao { set; get; }
        //    public ZhiFang.IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { set; get; }
        //    public ZhiFang.IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { set; get; }
        public IDAO.IDOSUserOrderFormDao IDOSUserOrderFormDao { set; get; }
        public IDOSManagerRefundFormOperationDao IDOSManagerRefundFormOperationDao { get; set; }

        public IBBParameter IBBParameter { get; set; }

        public SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction { get; set; }
        public IBBWeiXinAccount IBBWeiXinAccount { get; set; }

        public BaseResultBool OSManagerRefundFormOneReview(string refundFormCode, string reason, bool result, string EmpId, string EmpName)
        {
            var tmplist = DBDao.GetListByHQL(" MRefundFormCode='" + refundFormCode + "' ");
            if (tmplist != null && tmplist.Count > 0)
            {
                return OneReview(tmplist[0], reason, result, EmpId, EmpName);
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("OSManagerRefundFormOneReview.未找到退费单为：" + refundFormCode + "的退费单！");
                return new BaseResultBool() { success = false, BoolFlag = false, ErrorInfo = "退款单编码错误！" };
            }
        }

        public BaseResultBool OSManagerRefundFormOneReview(long refundFormId, string reason, bool result, string EmpId, string EmpName)
        {
            var tmpentity = DBDao.Get(refundFormId);
            if (tmpentity != null)
            {
                return OneReview(tmpentity, reason, result, EmpId, EmpName);
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("OSManagerRefundFormOneReview.未找到退费ID单为：" + refundFormId + "的退费单！");
                return new BaseResultBool() { success = false, BoolFlag = false, ErrorInfo = "退款单Id错误！" };
            }
        }
        public BaseResultBool OSManagerRefundFormTwoReview(long refundFormId, string reason, bool result, string EmpId, string EmpName)
        {
            var tmpentity = DBDao.Get(refundFormId);
            if (tmpentity != null)
            {
                return TwoReview(tmpentity, reason, result, EmpId, EmpName);
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("OSManagerRefundFormTwoReview.未找到退费ID单为：" + refundFormId + "的退费单！");
                return new BaseResultBool() { success = false, BoolFlag = false, ErrorInfo = "退款单Id错误！" };
            }
        }
        public BaseResultBool OSManagerRefundFormThreeReview(SysWeiXinPayBack.PayBack paybackfunc, RefundFormThreeReviewVO refundformthreereviewvo, string EmpId, string EmpName)
        {
            var tmpentity = DBDao.Get(refundformthreereviewvo.Id);
            if (tmpentity != null)
            {
                return ThreeReview(paybackfunc,tmpentity, refundformthreereviewvo, EmpId, EmpName);
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("OSManagerRefundFormThreeReview.未找到退费ID单为：" + refundformthreereviewvo.Id + "的退费单！");
                return new BaseResultBool() { success = false, BoolFlag = false, ErrorInfo = "退款单Id错误！" };
            }
        }

        BaseResultBool OneReview(OSManagerRefundForm tmpentity, string reason, bool result, string EmpId, string EmpName)
        {
            BaseResultBool brb = new BaseResultBool();
            if (tmpentity == null)
            {
                brb.success = false;
                brb.BoolFlag = false;
                brb.ErrorInfo = "退费单为空！";
                ZhiFang.Common.Log.Log.Error("OneReview.退费单为空！");
                return brb;
            }
            if (tmpentity.Status != long.Parse(RefundFormStatus.申请.Key) && tmpentity.Status != long.Parse(RefundFormStatus.一审退回.Key))
            {
                brb.success = false;
                brb.BoolFlag = false;
                brb.ErrorInfo = "退费单不匹配！";
                ZhiFang.Common.Log.Log.Error("OneReview.退费单不匹配！ID:" + tmpentity.Id);
                return brb;
            }
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            string status = (result) ? RefundFormStatus.一审通过.Key : RefundFormStatus.一审退回.Key;
            string statuso = (result) ? UserOrderFormStatus.退款申请处理中.Key : UserOrderFormStatus.退款申请被打回.Key;
            if (DBDao.UpdateByHql(" update OSManagerRefundForm  set Status=" + status + " ,RefundOneReviewStartTime='" + datetime + "' ,RefundOneReviewFinishTime='" + datetime + "' ,RefundOneReviewReason='" + reason + "',RefundOneReviewManName='" + EmpName + "',RefundOneReviewManID=" + EmpId + "  where Id=" + tmpentity.Id) > 0)
            {
                if (tmpentity.UOFID.HasValue)
                    IDOSUserOrderFormDao.UpdateByHql(" update OSUserOrderForm  set Status=" + statuso + " ,RefundOneReviewStartTime='" + datetime + "' ,RefundOneReviewFinishTime='" + datetime + "' ,RefundOneReviewReason='" + reason + "',RefundOneReviewManName='" + EmpName + "',RefundOneReviewManID=" + EmpId + "  where Id=" + tmpentity.UOFID.Value);

                OSManagerRefundFormOperation osmrfo = new OSManagerRefundFormOperation();
                osmrfo.BobjectID = tmpentity.Id;
                osmrfo.BusinessModuleCode = "OSUserOrderForm";
                osmrfo.CreatorID = long.Parse(EmpId);
                osmrfo.CreatorName = EmpName;
                osmrfo.Type = long.Parse(status);
                osmrfo.TypeName = RefundFormStatus.GetStatusDic()[status].Name;
                osmrfo.Memo = reason;
                osmrfo.IsUse = true;
                IDOSManagerRefundFormOperationDao.Save(osmrfo);

                brb.success = true;
                brb.BoolFlag = true;
            }
            else
            {
                brb.success = false;
                brb.BoolFlag = false;
                brb.ErrorInfo = "更新状态失败！";
                ZhiFang.Common.Log.Log.Error("OneReview.更新状态失败！ID:" + tmpentity.Id);
            }
            return brb;
        }

        BaseResultBool TwoReview(OSManagerRefundForm tmpentity, string reason, bool result, string EmpId, string EmpName)
        {
            BaseResultBool brb = new BaseResultBool();
            if (tmpentity == null)
            {
                brb.success = false;
                brb.BoolFlag = false;
                brb.ErrorInfo = "TwoReview.退费单为空！";
                ZhiFang.Common.Log.Log.Error("TwoReview.退费单为空！");
                return brb;
            }
            if (tmpentity.Status != long.Parse(RefundFormStatus.一审通过.Key) && tmpentity.Status != long.Parse(RefundFormStatus.财务退回.Key))
            {
                brb.success = false;
                brb.BoolFlag = false;
                brb.ErrorInfo = "退费单状态不匹配！";
                ZhiFang.Common.Log.Log.Error("TwoReview.退费单状态不匹配！ID:" + tmpentity.Id);
                return brb;
            }
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            string status = (result) ? RefundFormStatus.二审通过.Key : RefundFormStatus.二审退回.Key;
            if (DBDao.UpdateByHql(" update OSManagerRefundForm  set Status=" + status + " ,RefundTwoReviewStartTime='" + datetime + "' ,RefundTwoReviewFinishTime='" + datetime + "' ,RefundTwoReviewReason='" + reason + "',RefundTwoReviewManName='" + EmpName + "',RefundTwoReviewManID=" + EmpId + "  where Id=" + tmpentity.Id) > 0)
            {
                if (tmpentity.UOFID.HasValue)
                    IDOSUserOrderFormDao.UpdateByHql(" update OSUserOrderForm  set RefundTwoReviewStartTime='" + datetime + "' ,RefundTwoReviewFinishTime='" + datetime + "' ,RefundTwoReviewReason='" + reason + "',RefundTwoReviewManName='" + EmpName + "',RefundTwoReviewManID=" + EmpId + "  where Id=" + tmpentity.UOFID.Value);

                OSManagerRefundFormOperation osmrfo = new OSManagerRefundFormOperation();
                osmrfo.BobjectID = tmpentity.Id;
                osmrfo.BusinessModuleCode = "OSUserOrderForm";
                osmrfo.CreatorID = long.Parse(EmpId);
                osmrfo.CreatorName = EmpName;
                osmrfo.Type = long.Parse(status);
                osmrfo.TypeName = RefundFormStatus.GetStatusDic()[status].Name;
                osmrfo.Memo = reason;
                osmrfo.IsUse = true;
                IDOSManagerRefundFormOperationDao.Save(osmrfo);

                brb.success = true;
                brb.BoolFlag = true;
            }
            else
            {
                brb.success = false;
                brb.BoolFlag = false;
                brb.ErrorInfo = "更新状态失败！";
                ZhiFang.Common.Log.Log.Error("TwoReview.更新状态失败！ID:" + tmpentity.Id);
            }
            return brb;
        }

        BaseResultBool ThreeReview(SysWeiXinPayBack.PayBack paybackfunc, OSManagerRefundForm tmpentity, RefundFormThreeReviewVO refundformthreereviewvo, string EmpId, string EmpName)
        {
            BaseResultBool brb = new BaseResultBool();
            if (tmpentity == null)
            {
                brb.success = false;
                brb.BoolFlag = false;
                brb.ErrorInfo = "退费单为空！";
                ZhiFang.Common.Log.Log.Error("ThreeReview.退费单为空！");
                return brb;
            }
            if (tmpentity.Status != long.Parse(RefundFormStatus.二审通过.Key) && tmpentity.Status != long.Parse(RefundFormStatus.退款异常.Key))
            {
                brb.success = false;
                brb.BoolFlag = false;
                brb.ErrorInfo = "退费单状态不匹配！";
                ZhiFang.Common.Log.Log.Error("ThreeReview.退费单状态不匹配！ID:" + tmpentity.Id);
                return brb;
            }
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            string status = (refundformthreereviewvo.Result) ? RefundFormStatus.退款完成.Key : RefundFormStatus.退款异常.Key;
            string statuso = (refundformthreereviewvo.Result) ? UserOrderFormStatus.退款完成.Key : UserOrderFormStatus.退款申请被打回.Key;
            string TransactionId = tmpentity.TransactionId;
            string RefundId = tmpentity.RefundId;
            long RefundType = refundformthreereviewvo.RefundType;
            string Reason = refundformthreereviewvo.Reason != null ? refundformthreereviewvo.Reason : "";

            string BankAccount = refundformthreereviewvo.BankAccount != null ? refundformthreereviewvo.BankAccount : "";
            string BankTransFormCode = refundformthreereviewvo.BankTransFormCode != null ? refundformthreereviewvo.BankTransFormCode : "";
            string HQLBankID = refundformthreereviewvo.BankID > 0 ? " ,BankID=" + refundformthreereviewvo.BankID.ToString() : "";

            if (refundformthreereviewvo.Result)
            {
                if (refundformthreereviewvo.BankID > 0)
                {
                    //银行退款
                }
                else
                {
                    //微信退款
                    SortedDictionary<string, object> refundresult =paybackfunc(tmpentity.TransactionId, tmpentity.UOFID.ToString(), (tmpentity.Price*100).ToString(), (tmpentity.RefundPrice * 100).ToString(), tmpentity.MRefundFormCode);
                    if (refundresult != null && refundresult["result_code"] != null && refundresult["result_code"].ToString().Trim() == "SUCCESS" && refundresult["return_code"] != null && refundresult["return_code"].ToString().Trim() == "SUCCESS")
                    {
                        RefundId = refundresult["refund_id"].ToString();
                    }

//                        appid = wx401c52ad6d6741db
//cash_fee = 3
//cash_refund_fee = 1
//coupon_refund_count = 0
//coupon_refund_fee = 0
//mch_id = 1409524002
//nonce_str = KQd8ZdbbPzSLQlPG
//out_refund_no = 20170626005
//out_trade_no = 5002018960262644955
//refund_channel =
//refund_fee = 1
//refund_id = 50000403242017062601296912913
//result_code = SUCCESS
//return_code = SUCCESS
//return_msg = OK
//sign = F7A6820CEA531975552B699BCAEABCF9
//total_fee = 3
//transaction_id = 4006352001201706267572833380
//WeiXinPayBack(paybackfunc,);
                }
            }
            if (DBDao.UpdateByHql(" update OSManagerRefundForm  set Status=" + status + " ,RefundThreeReviewStartTime='" + datetime + "' ,RefundThreeReviewFinishTime='" + datetime + "' ,RefundThreeReviewReason='" + Reason + "',RefundThreeReviewManName='" + EmpName + "',RefundThreeReviewManID=" + EmpId + " ,RefundType=" + RefundType + HQLBankID + " ,BankAccount='" + BankAccount + "' ,BankTransFormCode='" + BankTransFormCode + "',RefundId='"+ RefundId + "'  where Id=" + tmpentity.Id) > 0)
            {
                if (tmpentity.UOFID.HasValue)
                    IDOSUserOrderFormDao.UpdateByHql(" update OSUserOrderForm  set Status=" + statuso + " ,RefundThreeReviewStartTime='" + datetime + "' ,RefundThreeReviewFinishTime='" + datetime + "' ,RefundThreeReviewReason='" + Reason + "',RefundThreeReviewManName='" + EmpName + "',RefundThreeReviewManID=" + EmpId + "  where Id=" + tmpentity.UOFID.Value);

                RefundFormStatusMessagePushToUser(this.pushWeiXinMessageAction, tmpentity.Id);
                OSManagerRefundFormOperation osmrfo = new OSManagerRefundFormOperation();
                osmrfo.BobjectID = tmpentity.Id;
                osmrfo.BusinessModuleCode = "OSUserOrderForm";
                osmrfo.CreatorID = long.Parse(EmpId);
                osmrfo.CreatorName = EmpName;
                osmrfo.Type = long.Parse(status);
                osmrfo.TypeName = RefundFormStatus.GetStatusDic()[status].Name;
                osmrfo.Memo = Reason;
                osmrfo.IsUse = true;
                IDOSManagerRefundFormOperationDao.Save(osmrfo);

                brb.success = true;
                brb.BoolFlag = true;
            }
            else
            {
                brb.success = false;
                brb.BoolFlag = false;
                brb.ErrorInfo = "更新状态失败！";
                ZhiFang.Common.Log.Log.Error("ThreeReview.更新状态失败！ID:" + tmpentity.Id);
            }
            return brb;
        }

        //private void WeiXinPayBack()
        //{
        //    BaseResultBool brb = new BaseResultBool();
        //    ZhiFang.Common.Log.Log.Info("WeiXinPayBack.RefundExcAction.");
        //    RefundTest.RunRefundApply(transaction_id.Text, out_trade_no.Text, total_fee.Text, refund_fee.Text, out_refund_no.Text);
        //    WxPayData data = new WxPayData();
        //    if (!string.IsNullOrEmpty(transaction_id))//微信订单号存在的条件下，则已微信订单号为准
        //    {
        //        data.SetValue("transaction_id", transaction_id);
        //    }
        //    else//微信订单号不存在，才根据商户订单号去退款
        //    {
        //        data.SetValue("out_trade_no", out_trade_no);
        //    }

        //    data.SetValue("total_fee", int.Parse(total_fee));//订单总金额
        //    data.SetValue("refund_fee", int.Parse(refund_fee));//退款金额
        //    data.SetValue("out_refund_no", WxPayApi.GenerateOutTradeNo());//随机生成商户退款单号
        //    data.SetValue("op_user_id", WxPayConfig.MCHID);//操作员，默认为商户号

        //    WxPayData result = WxPayApi.Refund(data);//提交退款申请给API，接收返回数据

        //    Log.Info("Refund", "Refund process complete, result : " + result.ToXml());
        //    return result.ToPrintStr();
        //    return brb;
        //}
        private void RefundFormStatusMessagePushToUser(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long OSManagerRefundFormId)
        {
            if (pushWeiXinMessageAction == null)
            {
                ZhiFang.Common.Log.Log.Error("pushWeiXinMessageAction或entity为空！");
                return;
            }
            var entity = DBDao.Get(OSManagerRefundFormId);
            if (entity.Status.ToString() == RefundFormStatus.退款完成.Key)
            {
                if (entity.UserOpenID != null && entity.UserOpenID.Trim() != "")
                {

                    Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                    string urgencycolor = "#11cd6e";
                    data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你收到新的退费通知，订单号：" + entity.UOFCode });
                    data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = entity.RefundPrice.ToString() });
                    data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = entity.RefundThreeReviewFinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss") });
                    string tmprefundtype = "微信";
                    if (entity.BankID != null)
                    {
                        tmprefundtype = "银行转账，帐号：" + entity.BankAccount;
                    }
                    data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "请查收。【" + tmprefundtype + "】" });
                    IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, new List<string>() { entity.UserOpenID }, data, "refunformsingle", "");
                }
            }
            if (entity.Status.ToString() == RefundFormStatus.一审退回.Key || entity.Status.ToString() == RefundFormStatus.退款异常.Key)
            {
                if (entity.UserOpenID != null && entity.UserOpenID.Trim() != "")
                {

                    Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                    string urgencycolor = "#11cd6e";

                    string reason = "";
                    string result = "";
                    if (entity.Status.ToString() == RefundFormStatus.一审退回.Key && entity.RefundOneReviewReason != null)
                    {
                        reason = entity.RefundOneReviewReason;
                        result = "被打回。";
                    }
                    if (entity.Status.ToString() == RefundFormStatus.退款异常.Key && entity.RefundOneReviewReason != null)
                    {
                        reason = entity.RefundOneReviewReason;
                        result = "退款异常。";
                    }

                    data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你的订单（订单号：" + entity.UOFCode + "）," + result });
                    data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = entity.RefundPrice.ToString() });
                    data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = entity.RefundThreeReviewFinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss") });

                    data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "原因：" + reason });
                    IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, new List<string>() { entity.UserOpenID }, data, "refunformsingle", "");
                }
            }
        }

        public List<RFVO> SearchRefundFormInfoByUOFCode(string uOFCode, string weiXinUserID)
        {
            List<RFVO> r = new List<RFVO>();
            var tmplist = DBDao.GetListByHQL(" UOFCode='" + uOFCode + "'  and UserAccountID=" + weiXinUserID);
            if (tmplist != null && tmplist.Count > 0)
            {
                foreach (var rf in tmplist)
                {
                    RFVO rfvo = new RFVO();
                    rfvo.BA = rf.BankAccount;
                    rfvo.BID = rf.BankID;
                    rfvo.BTFCode = rf.BankTransFormCode;
                    rfvo.DataAddTime = rf.DataAddTime;
                    rfvo.DispOrder = rf.DispOrder;
                    rfvo.DN = rf.DoctorName;
                    rfvo.DOpenID = rf.DoctorOpenID;
                    rfvo.DUT = rf.DataUpdateTime;
                    rfvo.Id = rf.Id;
                    rfvo.IU = rf.IsUse;
                    rfvo.LabID = rf.LabID;
                    rfvo.Memo = rf.Memo;
                    rfvo.MRFCode = rf.MRefundFormCode;
                    rfvo.Pe = rf.Price;
                    rfvo.PT = rf.PayTime;
                    rfvo.RAManID = rf.RefundApplyManID;
                    rfvo.RAManName = rf.RefundApplyManName;
                    rfvo.RAT = rf.RefundApplyTime;
                    rfvo.RId = rf.RefundId;
                    rfvo.ROneReFT = rf.RefundOneReviewFinishTime;
                    //rfvo.ROneReManID = rf.RefundOneReviewManID;
                    //rfvo.ROneReManName = rf.RefundOneReviewManName;
                    //rfvo.ROneReR = rf.RefundOneReviewReason;
                    //rfvo.ROneReST = rf.RefundOneReviewStartTime;
                    rfvo.RP = rf.RefundPrice;
                    rfvo.RR = rf.RefundReason;
                    rfvo.RThreeReFT = rf.RefundThreeReviewFinishTime;
                    //rfvo.RThreeReManID = rf.RefundThreeReviewManID;
                    //rfvo.RThreeReManName = rf.RefundThreeReviewManName;
                    rfvo.RThreeReR = rf.RefundThreeReviewReason;
                    //rfvo.RThreeReST = rf.RefundThreeReviewStartTime;
                    rfvo.RTwoReFT = rf.RefundTwoReviewFinishTime;
                    //rfvo.RTwoReManID = rf.RefundTwoReviewManID;
                    //rfvo.RTwoReManName = rf.RefundTwoReviewManName;
                    rfvo.RTwoReR = rf.RefundTwoReviewReason;
                    //rfvo.RTwoReST = rf.RefundTwoReviewStartTime;
                    rfvo.RType = rf.RefundType;
                    rfvo.Status = rf.Status;
                    rfvo.UAID = rf.UserAccountID;
                    rfvo.UCFCode = rf.OSUserConsumerFormCode;
                    rfvo.UCFID = rf.OSUserConsumerFormID;
                    rfvo.UN = rf.UserName;
                    rfvo.UOFCode = rf.UOFCode;
                    rfvo.UOFID = rf.UOFID;
                    rfvo.UOpenID = rf.UserOpenID;
                    r.Add(rfvo);
                }
            }
            return r;
        }

        #region PDF预览及Excel导出
        /// <summary>
        /// PDF预览
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isPreview"></param>
        /// <param name="templetName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public BaseResultDataValue ExcelToPdfFile(long id, bool isPreview, string templetName, ref string fileName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            OSManagerRefundForm entity = this.Get(id);
            if (String.IsNullOrEmpty(templetName))
                templetName = "退款申请单模板.xlsx";
            //baseResultDataValue = FillMaintenanceDataToExcel(entity, id, templetName);
            if (entity != null)//  
                fileName = entity.RefundApplyManName + "退款申请单.pdf";

            if (baseResultDataValue.success)
            {
                try
                {
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNoClass.ExcelExportSavePath.Key.ToString());

                    parentPath = parentPath + "\\OSManagerRefundForm\\" + entity.LabID.ToString();
                    if (isPreview)
                        parentPath = parentPath + "\\TempPDFFile\\" + entity.RefundApplyManName;
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
        /// <summary>
        /// 退款申请清单Excel导出
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileStream GetExportExcelOSManagerRefundFormDetail(string where, ref string fileName)
        {
            FileStream fileStream = null;
            //退款申请清单信息
            IList<OSManagerRefundForm> atemCount = new List<OSManagerRefundForm>();
            EntityList<OSManagerRefundForm> tempEntityList = new EntityList<OSManagerRefundForm>();

            tempEntityList = DBDao.GetListByHQL(where, " DataAddTime ", 0, 0);
            if (tempEntityList != null)
            {
                atemCount = tempEntityList.list;
            }
            if (atemCount != null && atemCount.Count > 0)
            {
                DataTable dtSource = null;
                dtSource = this.ExportExcelOSManagerRefundFormToDataTable<OSManagerRefundForm>(atemCount);
                string strHeaderText = "退款申请清单信息";
                fileName = "退款申请清单.xlsx";

                string filePath = "", basePath = "";
                //一级保存路径
                basePath = (string)IBBParameter.GetCache(BParameterParaNoClass.ExcelExportSavePath.Key.ToString());
                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = "ExcelExport";
                }
                //ATEmpSignInfoDetail为二级保存路径,作分类用
                basePath = basePath + "\\" + "ExportExcelOSManagerRefundForm\\";
                filePath = basePath + DateTime.Now.ToString("yyMMddhhmmss") + fileName;
                try
                {
                    if (!Directory.Exists(basePath))
                        Directory.CreateDirectory(basePath);
                    //单元格字体颜色的处理
                    Dictionary<string, short> cellFontStyleList = new Dictionary<string, short>();
                    //cellFontStyleList.Add("", NPOI.HSSF.Util.HSSFColor.Red.Index);

                    fileStream = ExportDTtoExcelHelp.ExportDTtoExcellHelp(dtSource, strHeaderText, filePath, cellFontStyleList);
                    if (fileStream != null)
                    {
                        fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    }
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            }
            return fileStream;
        }

        private DataTable ExportExcelOSManagerRefundFormToDataTable<T>(IList<T> list)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> removeList = new List<string>();
            foreach (PropertyInfo prop in props)
            {
                Type t = ExportDTtoExcelHelp.GetCoreType(prop.PropertyType);
                string columnName = prop.Name;
                #region DataTable的列转换为导出的中文显示名称
                switch (prop.Name)
                {
                    case "MRefundFormCode":
                        columnName = "退费单编号";
                        break;
                    case "UOFCode":
                        columnName = "用户订单编号";
                        break;
                    case "OS_UserConsumerFormCode":
                        columnName = "消费单编号";
                        break;
                    case "DoctorName":
                        columnName = "医生姓名";
                        break;
                    case "UserName":
                        columnName = "用户姓名";
                        break;
                    case "PayCode":
                        columnName = "消费码";
                        break;
                    case "Memo":
                        columnName = "备注";
                        break;
                    case "DiscountPrice":
                        columnName = "折扣价格";
                        break;
                    case "Discount":
                        columnName = "折扣率";
                        break;
                    case "Price":
                        columnName = "实际金额";
                        break;
                    case "RefundPrice":
                        columnName = "退费金额";
                        break;
                    case "PayTime":
                        columnName = "缴费时间";
                        break;
                    case "RefundApplyManName":
                        columnName = "退费申请人";
                        break;
                    case "RefundApplyTime":
                        columnName = "退费申请时间";
                        break;
                    case "RefundOneReviewManName":
                        columnName = "退款处理人";
                        break;
                    case "RefundOneReviewStartTime":
                        columnName = "退款处理开始时间";
                        break;
                    case "RefundOneReviewFinishTime":
                        columnName = "退款处理完成时间";
                        break;
                    case "RefundTwoReviewManName":
                        columnName = "退款审批人";
                        break;
                    case "RefundTwoReviewStartTime":
                        columnName = "退款审批开始时间";
                        break;
                    case "RefundTwoReviewFinishTime":
                        columnName = "退款审批完成时间";
                        break;
                    case "RefundThreeReviewManName":
                        columnName = "退款发放人";
                        break;
                    case "RefundThreeReviewStartTime":
                        columnName = "退款发放开始时间";
                        break;
                    case "RefundThreeReviewFinishTime":
                        columnName = "退款发放完成时间";
                        break;
                    case "RefundReason":
                        columnName = "退费原因";
                        break;
                    case "TransactionId":
                        columnName = "微信订单号";
                        break;
                    case "RefundId":
                        columnName = "微信退款单号";
                        break;
                    case "BankAccount":
                        columnName = "银行帐号";
                        break;
                    case "BankTransFormCode":
                        columnName = "银行转账单号";
                        break;
                    default:
                        removeList.Add(columnName);
                        break;
                }
                #endregion
                if (!String.IsNullOrEmpty(columnName))
                    tb.Columns.Add(columnName, t);
            }
            foreach (T item in list)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    //if(tb.Columns.Contains(props[i].Name))
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            foreach (var columnName in removeList)
            {
                if (tb.Columns.Contains(columnName))
                {
                    tb.Columns.Remove(columnName);
                }
            }
            if (tb != null)
            {
                tb.Columns["用户姓名"].SetOrdinal(0);
                tb.Columns["医生姓名"].SetOrdinal(1);

                tb.Columns["退费申请人"].SetOrdinal(2);
                tb.Columns["退费申请时间"].SetOrdinal(3);
                tb.Columns["退费金额"].SetOrdinal(4);
                tb.Columns["实际金额"].SetOrdinal(5);

                tb.Columns["折扣价格"].SetOrdinal(6);
                tb.Columns["折扣率"].SetOrdinal(7);
                tb.Columns["消费单编号"].SetOrdinal(8);
                tb.Columns["消费码"].SetOrdinal(9);

                tb.Columns["退款处理人"].SetOrdinal(10);
                tb.Columns["退款处理开始时间"].SetOrdinal(11);
                tb.Columns["退款处理完成时间"].SetOrdinal(12);

                tb.Columns["退款审批人"].SetOrdinal(13);
                tb.Columns["退款审批开始时间"].SetOrdinal(14);
                tb.Columns["退款审批完成时间"].SetOrdinal(15);

                tb.Columns["退款发放人"].SetOrdinal(16);
                tb.Columns["退款发放开始时间"].SetOrdinal(17);
                tb.Columns["退款发放完成时间"].SetOrdinal(18);

                tb.Columns["退费原因"].SetOrdinal(18);
                //排序
                tb.DefaultView.Sort = "用户姓名 asc,医生姓名 asc";
                tb = tb.DefaultView.ToTable();

            }
            return tb;
        }

        #endregion
    }
}