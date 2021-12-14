using ZhiFang.BLL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.WeiXin.IBLL;
using System.Data;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BOSUserOrderForm : BaseBLL<OSUserOrderForm>, ZhiFang.WeiXin.IBLL.IBOSUserOrderForm
    {
        IDAO.IDOSManagerRefundFormDao IDOSManagerRefundFormDao { get; set; }
        IDAO.IDOSDoctorOrderFormDao IDOSDoctorOrderFormDao { get; set; }
        IDAO.IDOSDoctorOrderItemDao IDOSDoctorOrderItemDao { get; set; }
        IDAO.IDOSUserOrderItemDao IDOSUserOrderItemDao { get; set; }
        IDAO.IDBDoctorAccountDao IDBDoctorAccountDao { get; set; }
        IBBParameter IBBParameter { get; set; }

        IBLL.IBBRuleNumber IBBRuleNumber { get; set; }
        IDAO.IDBWeiXinAccountDao IDBWeiXinAccountDao { get; set; }

        public BaseResultBool OSUserOrderFormRefundApplyByUser(string UOFCode, string RefundReason, string UserName, string UserOpenID)
        {
            BaseResultBool brb = new BaseResultBool();
            OSUserOrderForm tmpOSUserOrderForm = new OSUserOrderForm();
            //var tmpOSUserOrderFormList = DBDao.GetListByHQL(" UOFCode='" + UOFCode + "' and UserName='" + UserName + "' and UserOpenID='" + UserOpenID + "' ");
            var tmpOSUserOrderFormList = DBDao.GetListByHQL(" UOFCode='" + UOFCode + "' and UserOpenID='" + UserOpenID + "' ");
            if (tmpOSUserOrderFormList == null || tmpOSUserOrderFormList.Count <= 0)
            {
                brb.ErrorInfo = "不存在用户订单，订单号：'" + UOFCode + "'！";
                brb.success = false;
                return brb;
            }
            tmpOSUserOrderForm = tmpOSUserOrderFormList[0];
            if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.已交费.Key)
            {
                brb.ErrorInfo = "用户订单，订单号：'" + UOFCode + "'！状态错误！";
                ZhiFang.Common.Log.Log.Error("用户订单，订单号：'" + UOFCode + "'！状态错误,当前状态：" + UserOrderFormStatus.GetStatusDic()[tmpOSUserOrderForm.Status.ToString()].Name);
                brb.success = false;
                return brb;
            }
            brb.success = RefundApplyAction(tmpOSUserOrderForm, RefundReason, tmpOSUserOrderForm.Price, 0, null, "1");
            return brb;
        }
        public BaseResultBool OSUserOrderFormRefundApplyByEmp(long UserOrderFormID, long EmpID, string EmpName, string RefundReason, Double RefundPrice)
        {
            BaseResultBool brb = new BaseResultBool();
            OSUserOrderForm tmpOSUserOrderForm = new OSUserOrderForm();
            tmpOSUserOrderForm = DBDao.Get(UserOrderFormID);
            if (tmpOSUserOrderForm == null)
            {
                brb.ErrorInfo = "不存在用户订单，订单ID：'" + UserOrderFormID + "'！";
                brb.success = false;
                return brb;
            }
            //2018-01-08 longfc添加对订单状态为完全使用的支持
            if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.已交费.Key && tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.完全使用.Key)
            {
                brb.ErrorInfo = "用户订单，订单ID：'" + UserOrderFormID + "'！状态错误,当前状态：" + UserOrderFormStatus.GetStatusDic()[tmpOSUserOrderForm.Status.ToString()].Name;
                ZhiFang.Common.Log.Log.Error(brb.ErrorInfo);
                brb.success = false;
                return brb;
            }
            brb.success = RefundApplyAction(tmpOSUserOrderForm, RefundReason, RefundPrice, EmpID, EmpName, "3");
            return brb;
        }
        bool RefundApplyAction(OSUserOrderForm osuof, string RefundReason, Double RefundPrice, long EmpID, string EmpName, string RefundType)
        {

            OSManagerRefundForm tmprefundform = new OSManagerRefundForm();
            //tmprefundform.MRefundFormCode = ZhiFang.WeiXin.Common.GUIDHelp.GetGUIDLong().ToString();
            tmprefundform.MRefundFormCode = IBBRuleNumber.GetMRefundFormCode();

            tmprefundform.UOFID = osuof.Id;
            tmprefundform.UOFCode = osuof.UOFCode;
            tmprefundform.DOFID = osuof.DOFID;
            tmprefundform.DoctorAccountID = osuof.DoctorAccountID;
            tmprefundform.OSUserConsumerFormID = osuof.OSUserConsumerFormID;
            tmprefundform.OSUserConsumerFormCode = osuof.OSUserConsumerFormCode;
            tmprefundform.WeiXinUserID = osuof.WeiXinUserID;
            tmprefundform.DoctorOpenID = osuof.DoctorOpenID;
            tmprefundform.DoctorName = osuof.DoctorName;
            tmprefundform.UserAccountID = osuof.UserAccountID;
            tmprefundform.UserWeiXinUserID = osuof.UserWeiXinUserID;
            tmprefundform.UserName = osuof.UserName;
            tmprefundform.UserOpenID = osuof.UserOpenID;
            tmprefundform.Status = long.Parse(RefundFormStatus.申请.Key);
            tmprefundform.PayCode = osuof.PayCode;
            tmprefundform.Memo = osuof.Memo;
            tmprefundform.Price = osuof.Price + osuof.CollectionPrice;
            tmprefundform.PayTime = osuof.PayTime;
            tmprefundform.RefundApplyTime = DateTime.Now;
            tmprefundform.TransactionId = osuof.TransactionId;
            tmprefundform.RefundPrice = RefundPrice;
            tmprefundform.RefundReason = RefundReason;
            tmprefundform.IsUse = true;
            tmprefundform.CollectionFlag = osuof.CollectionFlag;
            tmprefundform.CollectionPrice = osuof.CollectionPrice;

            if (EmpID > 0)
            {
                tmprefundform.RefundApplyManID = EmpID;
                tmprefundform.RefundApplyManName = EmpName;
            }
            if (IDOSManagerRefundFormDao.Save(tmprefundform))
            {
                if (DBDao.UpdateByHql(" update OSUserOrderForm  set Status=" + UserOrderFormStatus.退款申请.Key + " ,RefundApplyTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ,RefundReason='" + RefundReason + "' ,RefundPrice=" + RefundPrice + "    where Id=" + osuof.Id) > 0)
                {
                    return true;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("RefundApplyAction.未能在用户订单更新退款申请状态！退款申请单ID:" + tmprefundform.Id + "@订单ID:" + osuof.Id + "@订单编码:" + osuof.UOFCode);
                    return false;
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("RefundApplyAction.未能新增退款申请单！");
                return false;
            }
        }


        //public BaseResultBool OSUserOrderFormStatusUpdate(string[] tempArray, long empID, string empName)
        //{
        //    BaseResultBool brb = new BaseResultBool();
        //    OSUserOrderForm entity = this.Entity;
        //    var tmpa = tempArray.ToList();
        //    OSUserOrderForm tmpOSUserOrderForm = new OSUserOrderForm();
        //    tmpOSUserOrderForm = DBDao.Get(entity.Id);
        //    if (tmpOSUserOrderForm == null)
        //    {
        //        brb.ErrorInfo = "用户订单ID：" + entity.Id + "为空！";
        //        brb.success = false;
        //        return brb;
        //    }

        //    if (!OSUserOrderFormStatusUpdateCheck(entity, tmpOSUserOrderForm, empID, empName, tmpa))
        //    {
        //        return new BaseResultBool() { ErrorInfo = "用户订单ID：" + entity.Id + "的状态为：" + UserOrderFormStatus.GetStatusDic()[tmpOSUserOrderForm.Status.ToString()].Name + "！", success = false };
        //    }
        //    tempArray = tmpa.ToArray();
        //    if (this.Update(tempArray))
        //    {
        //        if (entity.Status.ToString() == UserOrderFormStatus.退款完成.Key)
        //        {
        //            //IDPEmpFinanceAccountDao.PLoanBill(tmpOSUserOrderForm.LoanBillAmount, tmpOSUserOrderForm.ApplyManID.Value);
        //        }
        //        //SaveSCOperation(entity);

        //        // UserOrderFormStatusMessagePush(pushWeiXinMessageAction, entity.Id, entity.Status.ToString(), null);
        //        brb.success = true;
        //    }
        //    else
        //    {
        //        brb.ErrorInfo = "UserOrderFormStatusUpdate.Update错误！";
        //        brb.success = false;
        //    }
        //    return brb;
        //}
        //bool OSUserOrderFormStatusUpdateCheck(OSUserOrderForm entity, OSUserOrderForm tmpOSUserOrderForm, long EmpID, string EmpName, List<string> tmpa)
        //{
        //    #region 已交费
        //    if (entity.Status.ToString() == UserOrderFormStatus.已交费.Key)
        //    {
        //        if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.待缴费.Key)
        //        {
        //            return false;
        //        }

        //        string PayCode = GetPayCode();
        //        tmpa.Add("PayCode=" + PayCode + " ");
        //        tmpa.Add("ApplyMan='" + EmpName + "'");
        //        tmpa.Add("ApplyDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        //        tmpa.Add("ReviewDate=null");
        //        tmpa.Add("ReviewInfo=null");

        //        tmpa.Add("TwoReviewManID=null");
        //        tmpa.Add("TwoReviewMan=null");
        //        tmpa.Add("TwoReviewDate=null");
        //        tmpa.Add("TwoReviewInfo=null");
        //    }
        //    #endregion

        //    #region 申请
        //    if (entity.Status.ToString() == UserOrderFormStatus.完全使用.Key)
        //    {
        //        if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.已交费.Key)
        //        {
        //            return false;
        //        }
        //        tmpa.Add("PayTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        //        tmpa.Add("ReviewDate=null");
        //        tmpa.Add("ReviewInfo=null");

        //        tmpa.Add("TwoReviewManID=null");
        //        tmpa.Add("TwoReviewMan=null");
        //        tmpa.Add("TwoReviewDate=null");
        //        tmpa.Add("TwoReviewInfo=null");

        //    }
        //    #endregion

        //    //#region 一审通过
        //    //if (entity.Status.ToString() == UserOrderFormStatus.一审通过.Key)
        //    //{
        //    //    if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.申请.Key && tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.二审退回.Key)
        //    //    {
        //    //        return false;
        //    //    }
        //    //    tmpa.Add("ReviewManID=" + EmpID + " ");
        //    //    tmpa.Add("ReviewMan='" + EmpName + "'");
        //    //    tmpa.Add("ReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        //    //    tmpa.Add("ReviewInfo='" + entity.ReviewInfo + "'");

        //    //    tmpa.Add("TwoReviewManID=null");
        //    //    tmpa.Add("TwoReviewMan=null");
        //    //    tmpa.Add("TwoReviewDate=null");
        //    //    tmpa.Add("TwoReviewInfo=null");

        //    //}
        //    //#endregion

        //    //#region 一审退回
        //    //if (entity.Status.ToString() == UserOrderFormStatus.一审退回.Key)
        //    //{
        //    //    if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.申请.Key && tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.二审退回.Key)
        //    //    {
        //    //        return false;
        //    //    }
        //    //    tmpa.Add("ReviewManID=" + EmpID + " ");
        //    //    tmpa.Add("ReviewMan='" + EmpName + "'");
        //    //    tmpa.Add("ReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        //    //    tmpa.Add("ReviewInfo='" + entity.ReviewInfo + "'");

        //    //}
        //    //#endregion

        //    //#region 二审通过
        //    //if (entity.Status.ToString() == UserOrderFormStatus.二审通过.Key)
        //    //{
        //    //    if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.一审通过.Key && tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.三审退回.Key && tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.四审退回.Key)
        //    //    {
        //    //        return false;
        //    //    }
        //    //    tmpa.Add("TwoReviewManID=" + EmpID + " ");
        //    //    tmpa.Add("TwoReviewMan='" + EmpName + "'");
        //    //    tmpa.Add("TwoReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        //    //    tmpa.Add("TwoReviewInfo='" + entity.TwoReviewInfo + "'");

        //    //    tmpa.Add("ThreeReviewManID=null");
        //    //    tmpa.Add("ThreeReviewMan=null");
        //    //    tmpa.Add("ThreeReviewDate=null");
        //    //    tmpa.Add("ThreeReviewInfo=null");
        //    //    tmpa.Add("FourReviewManID=null");
        //    //    tmpa.Add("FourReviewMan=null");
        //    //    tmpa.Add("FourReviewDate=null");
        //    //    tmpa.Add("FourReviewInfo=null");

        //    //}
        //    //#endregion

        //    //#region 二审退回
        //    //if (entity.Status.ToString() == UserOrderFormStatus.二审退回.Key)
        //    //{
        //    //    if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.一审通过.Key && tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.三审退回.Key && tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.四审退回.Key)
        //    //    {
        //    //        return false;
        //    //    }
        //    //    tmpa.Add("TwoReviewManID=" + EmpID + " ");
        //    //    tmpa.Add("TwoReviewMan='" + EmpName + "'");
        //    //    tmpa.Add("TwoReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        //    //    tmpa.Add("TwoReviewInfo='" + entity.TwoReviewInfo + "'");

        //    //}
        //    //#endregion

        //    //#region 三审通过
        //    //if (entity.Status.ToString() == UserOrderFormStatus.三审通过.Key)
        //    //{
        //    //    if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.二审通过.Key && tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.四审退回.Key)
        //    //    {
        //    //        return false;
        //    //    }
        //    //    tmpa.Add("ThreeReviewManID=" + EmpID + " ");
        //    //    tmpa.Add("ThreeReviewMan='" + EmpName + "'");
        //    //    tmpa.Add("ThreeReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        //    //    tmpa.Add("ThreeReviewInfo='" + entity.ThreeReviewInfo + "'");

        //    //    tmpa.Add("FourReviewManID=null");
        //    //    tmpa.Add("FourReviewMan=null");
        //    //    tmpa.Add("FourReviewDate=null");
        //    //    tmpa.Add("FourReviewInfo=null");

        //    //}
        //    //#endregion

        //    //#region 三审退回
        //    //if (entity.Status.ToString() == UserOrderFormStatus.三审退回.Key)
        //    //{
        //    //    if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.二审通过.Key && tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.四审退回.Key)
        //    //    {
        //    //        return false;
        //    //    }
        //    //    tmpa.Add("ThreeReviewManID=" + EmpID + " ");
        //    //    tmpa.Add("ThreeReviewMan='" + EmpName + "'");
        //    //    tmpa.Add("ThreeReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        //    //    tmpa.Add("ThreeReviewInfo='" + entity.ThreeReviewInfo + "'");

        //    //}
        //    //#endregion

        //    //#region 四审通过
        //    //if (entity.Status.ToString() == UserOrderFormStatus.四审通过.Key)
        //    //{
        //    //    if (tmpOSUserOrderForm.IsSpecially)
        //    //    {
        //    //        if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.三审通过.Key)
        //    //        {
        //    //            return false;
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.三审通过.Key && tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.二审通过.Key)
        //    //        {
        //    //            return false;
        //    //        }
        //    //    }

        //    //    tmpa.Add("FourReviewManID=" + EmpID + " ");
        //    //    tmpa.Add("FourReviewMan='" + EmpName + "'");
        //    //    tmpa.Add("FourReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        //    //    tmpa.Add("FourReviewInfo='" + entity.FourReviewInfo + "'");

        //    //}
        //    //#endregion

        //    //#region 四审退回
        //    //if (entity.Status.ToString() == UserOrderFormStatus.四审退回.Key)
        //    //{
        //    //    if (tmpOSUserOrderForm.IsSpecially)
        //    //    {
        //    //        if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.三审通过.Key)
        //    //        {
        //    //            return false;
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.三审通过.Key && tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.二审通过.Key)
        //    //        {
        //    //            return false;
        //    //        }
        //    //    }
        //    //    tmpa.Add("FourReviewManID=" + EmpID + " ");
        //    //    tmpa.Add("FourReviewMan='" + EmpName + "'");
        //    //    tmpa.Add("FourReviewDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        //    //    tmpa.Add("FourReviewInfo='" + entity.FourReviewInfo + "'");

        //    //}
        //    //#endregion

        //    //#region 打款
        //    //if (entity.Status.ToString() == UserOrderFormStatus.打款.Key)
        //    //{
        //    //    if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.四审通过.Key)
        //    //    {
        //    //        return false;
        //    //    }

        //    //    tmpa.Add("PayManID=" + EmpID + " ");
        //    //    tmpa.Add("PayManName='" + EmpName + "'");
        //    //    //tmpa.Add("PayDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        //    //    tmpa.Add("PayDateInfo='" + entity.PayDateInfo + "'");

        //    //}
        //    //#endregion

        //    //#region 领款确认
        //    //if (entity.Status.ToString() == UserOrderFormStatus.领款确认.Key)
        //    //{
        //    //    if (tmpOSUserOrderForm.Status.ToString() != UserOrderFormStatus.打款.Key)
        //    //    {
        //    //        return false;
        //    //    }
        //    //    tmpa.Add("ReceiveManID=" + EmpID + " ");
        //    //    tmpa.Add("ReceiveManName='" + EmpName + "'");
        //    //    tmpa.Add("ReceiveDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        //    //    tmpa.Add("ReceiveManInfo='" + entity.ReceiveManInfo + "'");

        //    //}
        //    //#endregion   
        //    return true;
        //}
        //public string GetPayCode()
        //{
        //    return ZhiFang.WeiXin.Common.GUIDHelp.GetGUIDString() ;
        //}

        public EntityList<OSUserOrderFormVO> VO_OSUserOrderFormList(IList<OSUserOrderForm> listOSUserOrderForm)
        {
            EntityList<OSUserOrderFormVO> listVOEntity = new EntityList<OSUserOrderFormVO>();
            if (listOSUserOrderForm != null && listOSUserOrderForm.Count > 0)
            {
                IList<OSUserOrderFormVO> listVO = new List<OSUserOrderFormVO>();
                foreach (OSUserOrderForm osUserOrderForm in listOSUserOrderForm)
                {
                    listVO.Add(VO_OSUserOrderForm(osUserOrderForm));
                }
                listVOEntity.count = listVO.Count;
                listVOEntity.list = listVO;
            }
            return listVOEntity;
        }

        public OSUserOrderFormVO VO_OSUserOrderForm(OSUserOrderForm osUserOrderForm)
        {
            OSUserOrderFormVO voEntity = new OSUserOrderFormVO();
            if (osUserOrderForm != null)
            {
                #region VO赋值
                voEntity.Id = osUserOrderForm.Id;
                voEntity.LabID = osUserOrderForm.LabID;
                voEntity.AD = osUserOrderForm.AreaID;
                voEntity.HD = osUserOrderForm.HospitalID;
                voEntity.UFC = osUserOrderForm.UOFCode;
                voEntity.DFD = osUserOrderForm.DOFID;
                voEntity.DAD = osUserOrderForm.DoctorAccountID;
                voEntity.OCD = osUserOrderForm.OSUserConsumerFormID;
                voEntity.OCC = osUserOrderForm.OSUserConsumerFormCode;
                voEntity.WXD = osUserOrderForm.WeiXinUserID;
                voEntity.DOD = osUserOrderForm.DoctorOpenID;
                voEntity.DN = osUserOrderForm.DoctorName;
                voEntity.UID = osUserOrderForm.UserAccountID;
                voEntity.UWD = osUserOrderForm.UserWeiXinUserID;
                voEntity.UN = osUserOrderForm.UserName;
                voEntity.UOD = osUserOrderForm.UserOpenID;
                voEntity.SS = osUserOrderForm.Status;
                voEntity.PC = osUserOrderForm.PayCode;
                voEntity.MM = osUserOrderForm.Memo;
                voEntity.DO = osUserOrderForm.DispOrder;
                voEntity.IU = osUserOrderForm.IsUse;
                voEntity.DUT = osUserOrderForm.DataUpdateTime;
                voEntity.MP = osUserOrderForm.MarketPrice;
                voEntity.GMP = osUserOrderForm.GreatMasterPrice;
                voEntity.DP = osUserOrderForm.DiscountPrice;
                voEntity.DT = osUserOrderForm.Discount;
                voEntity.PE = osUserOrderForm.Price;
                voEntity.AP = osUserOrderForm.AdvicePrice;
                voEntity.RP = osUserOrderForm.RefundPrice;
                voEntity.PT = osUserOrderForm.PayTime;
                voEntity.CAT = osUserOrderForm.CancelApplyTime;
                voEntity.CDT = osUserOrderForm.CancelFinishedTime;
                voEntity.CST = osUserOrderForm.ConsumerStartTime;
                voEntity.CFT = osUserOrderForm.ConsumerFinishedTime;
                voEntity.RAT = osUserOrderForm.RefundApplyTime;
                voEntity.RRM = osUserOrderForm.RefundOneReviewManName;
                voEntity.RRD = osUserOrderForm.RefundOneReviewManID;
                voEntity.RRT = osUserOrderForm.RefundOneReviewStartTime;
                voEntity.RFT = osUserOrderForm.RefundOneReviewFinishTime;
                voEntity.RTM = osUserOrderForm.RefundTwoReviewManName;
                voEntity.RTD = osUserOrderForm.RefundTwoReviewManID;
                voEntity.RTS = osUserOrderForm.RefundTwoReviewStartTime;
                voEntity.RRF = osUserOrderForm.RefundTwoReviewFinishTime;
                voEntity.TRM = osUserOrderForm.RefundThreeReviewManName;
                voEntity.TRD = osUserOrderForm.RefundThreeReviewManID;
                voEntity.TRS = osUserOrderForm.RefundThreeReviewStartTime;
                voEntity.TRF = osUserOrderForm.RefundThreeReviewFinishTime;
                voEntity.RR = osUserOrderForm.RefundReason;
                voEntity.RRR = osUserOrderForm.RefundOneReviewReason;
                voEntity.RTR = osUserOrderForm.RefundTwoReviewReason;
                voEntity.RERR = osUserOrderForm.RefundThreeReviewReason;
                voEntity.IPP = osUserOrderForm.IsPrePay;
                voEntity.PPD = osUserOrderForm.PrePayId;
                voEntity.PPT = osUserOrderForm.PrePayTime;
                voEntity.PRC = osUserOrderForm.PrePayReturnCode;
                voEntity.PPM = osUserOrderForm.PrePayReturnMsg;
                voEntity.PEC = osUserOrderForm.PrePayErrCode;
                voEntity.PPE = osUserOrderForm.PrePayErrName;
                voEntity.TD = osUserOrderForm.TransactionId;
                voEntity.DataAddTime = osUserOrderForm.DataAddTime;
                voEntity.CF = osUserOrderForm.CollectionFlag;
                voEntity.CP = osUserOrderForm.CollectionPrice;
                voEntity.TI = osUserOrderForm.TypeID;
                voEntity.TN = osUserOrderForm.TypeName;
                #endregion
            }
            return voEntity;
        }

        public BaseResultDataValue AddUserOrderFormConfirmByOrderFormId(string UserOpenID, long id)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            var form = IDOSDoctorOrderFormDao.Get(id);
            if (form == null)
            {
                throw new Exception("UserOrderFormConfirmByOrderFormId.未能查到医嘱单，医嘱单Id：" + id);
            }
            if (form.UserOpenID != UserOpenID)
            {
                throw new Exception("UserOrderFormConfirmByOrderFormId.未能确认医嘱单，非法的确认操作！医嘱单Id：" + id + "@UserOpenID:" + UserOpenID + "@form.UserOpenID:" + form.UserOpenID);
            }
            if (form.Status != long.Parse(DoctorOrderFormStatus.下医嘱单.Key) && form.Status != long.Parse(DoctorOrderFormStatus.患者已接收.Key))
            {
                throw new Exception("UserOrderFormConfirmByOrderFormId.未能确认医嘱单,医嘱单Id：" + id + "，状态为：" + DoctorOrderFormStatus.GetStatusDic()[form.Status.ToString()].Name);
            }

            var item = IDOSDoctorOrderItemDao.GetListByHQL(" DOFID=" + id);
            if (item == null || item.Count <= 0)
            {
                throw new Exception("UserOrderFormConfirmByOrderFormId.未能查到医嘱项目，医嘱单Id：" + id);
            }
            var tmpuoflist = DBDao.GetListByHQL(" DOFID=" + id);
            if (tmpuoflist != null && tmpuoflist.Count > 0)
            {
                throw new Exception("UserOrderFormConfirmByOrderFormId.未能确认医嘱单，医嘱单Id：" + id + ",已经被确认过！");
            }
            var doct = form.DoctorAccountID.HasValue? IDBDoctorAccountDao.Get(form.DoctorAccountID.Value):null;
            if (doct == null)
            {
                //throw new Exception("UserOrderFormConfirmByOrderFormId.未能确认医生账号，医生账号DoctorAccountID：" + form.DoctorAccountID + "！");
               ZhiFang.Common.Log.Log.Error("UserOrderFormConfirmByOrderFormId.未能确认医生账号，医生账号DoctorAccountID：" + form.DoctorAccountID + ",DoctorOrderForm:"+ id + "！");
            }

            OSUserOrderForm osuof = new OSUserOrderForm();
            osuof.Id = WeiXin.Common.GUIDHelp.GetGUIDLong();
            osuof.HospitalID = form.HospitalID;
            osuof.AreaID = form.AreaID;
            //osuof.UOFCode = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            osuof.UOFCode = Common.NextRuleNumber.GetUOFCode();
            osuof.DOFID = id;
            osuof.DoctorAccountID = form.DoctorAccountID;
            osuof.DoctorName = form.DoctorName;
            osuof.DoctorOpenID = form.DoctorOpenID;
            osuof.WeiXinUserID = form.DoctorWeiXinUserID;
            osuof.UserAccountID = form.UserAccountID;
            osuof.UserName = form.UserName;
            osuof.UserOpenID = form.UserOpenID;
            osuof.UserWeiXinUserID = form.UserWeiXinUserID;
            osuof.Status = long.Parse(UserOrderFormStatus.待缴费.Key);
            osuof.Memo = form.Memo;
            osuof.IsUse = true;
            osuof.DiscountPrice = item.Sum(a => a.DiscountPrice);
            osuof.MarketPrice = item.Sum(a => a.MarketPrice);
            osuof.GreatMasterPrice = item.Sum(a => a.GreatMasterPrice);
            osuof.Price = osuof.DiscountPrice;
            osuof.CollectionFlag = form.CollectionFlag;
            osuof.CollectionPrice = form.CollectionPrice;
            osuof.TypeID = form.TypeID; //暂时先设计成一致
            osuof.TypeName = form.TypeName;//暂时先设计成一致
            osuof.DoctMobileCode = form.DoctMobileCode;
            osuof.DoctMobileCode = form.DoctMobileCode;

            double tmpd = 0;
            if (doct != null)
            {
                if (doct.BonusPercent.HasValue && doct.BonusPercent.Value > 0)
                {
                    tmpd = doct.BonusPercent.Value;
                }
                else
                {
                    if (IBBParameter.SearchListByParaNo(BParameterParaNoClass.BonusPercent.Key.ToString()) != null && IBBParameter.SearchListByParaNo(BParameterParaNoClass.BonusPercent.Key.ToString()).Count>0 && IBBParameter.SearchListByParaNo(BParameterParaNoClass.BonusPercent.Key.ToString()).First().ParaValue.ToString().Trim() != "")
                    {
                        tmpd = int.Parse(IBBParameter.SearchListByParaNo(BParameterParaNoClass.BonusPercent.Key.ToString()).First().ParaValue.ToString().Trim());
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("AddUserOrderFormConfirmByOrderFormId,系统参数,医生的咨询费比率未配置！");
                        throw new Exception("系统参数,医生的咨询费比率未配置！");
                    }
                }
            }
            for (int i = 0; i < item.Count; i++)
            {
                if (item[i].ItemBonusPrice.HasValue && item[i].ItemBonusPrice.Value > 0)
                {
                    osuof.AdvicePrice = osuof.AdvicePrice + item[i].ItemBonusPrice.Value;
                }
                else
                {
                    osuof.AdvicePrice = osuof.AdvicePrice + item[i].DiscountPrice * tmpd * 0.01;
                }
            }
            osuof.AdvicePrice = Math.Round(osuof.AdvicePrice, 2);
            if (osuof.AdvicePrice <= 0.01)
            {
                osuof.AdvicePrice = 0;
            }

            //if (doct.BonusPercent.HasValue && doct.BonusPercent.Value > 0)
            //{
            //    osuof.AdvicePrice = osuof.DiscountPrice * doct.BonusPercent.Value * 0.01;
            //    osuof.AdvicePrice = Math.Round(osuof.AdvicePrice, 2);
            //    if (osuof.AdvicePrice <= 0.01)
            //    {
            //        osuof.AdvicePrice = 0;
            //    }

            //}
            //else
            //{
            //    if (IBBParameter.GetCache(BParameterParaNoClass.BonusPercent.Key.ToString()) != null && IBBParameter.GetCache(BParameterParaNoClass.BonusPercent.Key.ToString()).ToString().Trim() != "")
            //    {
            //        int tmpb = int.Parse(IBBParameter.GetCache(BParameterParaNoClass.BonusPercent.Key.ToString()).ToString());
            //        osuof.AdvicePrice = osuof.DiscountPrice * tmpb * 0.01;
            //        osuof.AdvicePrice = Math.Round(osuof.AdvicePrice, 2);
            //        if (osuof.AdvicePrice <= 0.01)
            //        {
            //            osuof.AdvicePrice = 0;
            //        }
            //    }
            //    else
            //    {
            //        ZhiFang.Common.Log.Log.Debug("AddUserOrderFormConfirmByOrderFormId,系统参数,医生的咨询费比率未配置！");
            //    }
            //}

            if (DBDao.Save(osuof))
            {
                brdv.success = true;
                brdv.ResultDataValue = osuof.Id.ToString();
                foreach (var i in item)
                {
                    OSUserOrderItem tmp = new OSUserOrderItem();
                    tmp.AreaID = i.AreaID;
                    tmp.HospitalID = i.HospitalID;
                    tmp.DiscountPrice = i.DiscountPrice;
                    tmp.MarketPrice = i.MarketPrice;
                    tmp.GreatMasterPrice = i.GreatMasterPrice;
                    tmp.ItemID = i.ItemID;
                    tmp.ItemNo = i.ItemNo;
                    tmp.ItemCName = i.ItemCName;
                    tmp.RecommendationItemProductID = i.RecommendationItemProductID;
                    tmp.DOIID = i.Id;
                    tmp.UOFID = osuof.Id;
                    tmp.IsUse = true;
                    if (!IDOSUserOrderItemDao.Save(tmp))
                    {
                        throw new Exception("UserOrderFormConfirmByOrderFormId.未能新增用户订单项目，医嘱单Id：" + id + "@医嘱单项目Id：" + i.Id);
                    }
                }
                IDOSDoctorOrderFormDao.UpdateByHql(" update OSDoctorOrderForm  set Status=" + DoctorOrderFormStatus.患者已下单.Key + " where Id=" + id);
            }
            else
            {
                throw new Exception("UserOrderFormConfirmByOrderFormId.未能新增用户订单，医嘱单Id：" + id);
            }
            return brdv;
        }

        public void UpdateUnifiedOrder(long UOFID, string PrePayId, string PrePayReturnCode, string PrePayReturnMsg)
        {
            string HQL = " update OSUserOrderForm set IsPrePay=1 ";
            HQL += ", PrePayId='" + PrePayId + "'";
            HQL += ", PrePayTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            HQL += ", PrePayReturnCode='" + PrePayReturnCode + "'";
            HQL += ", PrePayReturnMsg='" + PrePayReturnMsg + "'";
            DBDao.UpdateByHql(HQL + " where Id=" + UOFID);
        }
        public void UpdateUnifiedOrderError(long UOFID, string PrePayReturnCode, string PrePayReturnMsg, string PrePayErrCode, string PrePayErrName)
        {
            string HQL = " update OSUserOrderForm set IsPrePay=0 ";
            HQL += ", PrePayReturnCode='" + PrePayReturnCode + "'";
            HQL += ", PrePayReturnMsg='" + PrePayReturnMsg + "'";
            HQL += (PrePayErrCode != null && PrePayErrCode.Trim() != "") ? ", PrePayErrCode='" + PrePayErrCode + "'" : "";
            HQL += (PrePayErrName != null && PrePayErrName.Trim() != "") ? ", PrePayErrName='" + PrePayErrName + "'" : "";
            DBDao.UpdateByHql(HQL + " where Id=" + UOFID);
        }

        public void UpdatePayStatus(long UOFID, string TransactionId, string PayTime)
        {
            string tmp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (PayTime != null && PayTime.Trim() != "" && PayTime.Trim().Length == 14)
            {
                tmp = PayTime.Substring(0, 4) + "-" + PayTime.Substring(4, 2) + "-" + PayTime.Substring(6, 2) + " " + PayTime.Substring(8, 2) + ":" + PayTime.Substring(10, 2) + ":" + PayTime.Substring(12, 2);
            }
            string PayCode = ZhiFang.WeiXin.Common.GUIDHelp.GetGUIDLong().ToString();//消费码
            string HQL = " update OSUserOrderForm set Status=" + UserOrderFormStatus.已交费.Key + ",PayTime='" + tmp + "',PayCode='" + PayCode + "',TransactionId='" + TransactionId + "'";
            DBDao.UpdateByHql(HQL + " where Id=" + UOFID);
        }
        /// <summary>
        /// 更新用户订单的状态为取消订单
        /// </summary>
        /// <param name="id"></param>
        public BaseResultBool UpdateOSUserOrderFormStatusOfCancelOrder(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            string tmp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string HQL = " update OSUserOrderForm set Status=" + UserOrderFormStatus.取消订单.Key;
            HQL += ",CancelApplyTime='" + tmp + "'";
            int result = DBDao.UpdateByHql(HQL + " where Id=" + id);
            bool boolFlag = true;
            if (result > 0)
            {
                boolFlag = true;
                tempBaseResultBool.BoolInfo = "取消订单操作成功";
            }
            else
            {
                boolFlag = false;
                tempBaseResultBool.BoolInfo = "取消订单操作失败";
                tempBaseResultBool.ErrorInfo = "取消订单操作失败";
            }
            tempBaseResultBool.success = boolFlag;
            tempBaseResultBool.BoolFlag = boolFlag;
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_UnLockOSUserOrderFormById(long oSUserOrderFormId, long EmployeeID, string EmployeeName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var osuof=DBDao.Get(oSUserOrderFormId);
            if (osuof.Status !=long.Parse( UserOrderFormStatus.使用中.Key))
            {
                brdv.success = false;
                brdv.ErrorInfo = "订单状态错误！订单状态："+ UserOrderFormStatus.GetStatusDic()[osuof.Status.ToString()].Name;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_UnLockOSUserOrderFormById.订单状态错误！订单状态：" + UserOrderFormStatus.GetStatusDic()[osuof.Status.ToString()].Name+ "@订单ID："+ osuof.Id);
                return brdv;
            }
            DBDao.UpdateByHql("update OSUserOrderForm set Status=" + UserOrderFormStatus.已交费.Key + "  where Id =" + osuof.Id) ;
            brdv.success = true;
            return brdv;
        }

        public int UpdateOSUserOrderFormStatusOfUseing(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisSourceOrgID, string WeblisSourceOrgName, string ConsumerAreaID)
        {

            if (PayCode.ToLower().IndexOf("del") < 0 && PayCode.ToLower().IndexOf("drop") < 0 && PayCode.ToLower().IndexOf("update") < 0)
            {
                string HQL = " update OSUserOrderForm set Status=" + UserOrderFormStatus.使用中.Key + ",ConsumerStartTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "',WeblisSourceOrgID=" + WeblisSourceOrgID + ",WeblisSourceOrgName=" + WeblisSourceOrgName + ",EmpID=" + ZhiFangUserID + ",EmpAccount='" + EmpAccount + "',ConsumerAreaID=" + ConsumerAreaID + "  where PayCode=" + PayCode + " and Status=" + UserOrderFormStatus.已交费.Key + " and AreaID= " + ConsumerAreaID;
                return  DBDao.UpdateByHql(HQL);
                
            }
            else {
                return 0;
            }
            
        }

        public int UnLockOSUserOrderFormByPayCode(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisSourceOrgID, string WeblisSourceOrgName, string ConsumerAreaID)
        {            
              string HQL = " update OSUserOrderForm set Status=" + UserOrderFormStatus.已交费.Key + ",ConsumerStartTime=null where PayCode='" + PayCode + "' and Status=" + UserOrderFormStatus.使用中.Key + " and WeblisSourceOrgName='" + WeblisSourceOrgName + "' and EmpID=" + ZhiFangUserID + ",EmpAccount='" + EmpAccount + "'";
              return DBDao.UpdateByHql(HQL);           
        }

        public BaseResultDataValue OSConsumerUserOrderForm(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisSourceOrgID, string WeblisSourceOrgName, string ConsumerAreaID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<OSUserOrderForm> dtuof = new EntityList<OSUserOrderForm>();
            EntityList<OSDoctorOrderForm> dtdof = new EntityList<OSDoctorOrderForm>();
            EntityList<BDoctorAccount> dtDoctAccount = new EntityList<BDoctorAccount>();
            EntityList<BWeiXinAccount> dtUserAccount = new EntityList<BWeiXinAccount>();
            EntityList<OSUserOrderItem> dtuoi = new EntityList<OSUserOrderItem>();
            UserOrderFormVO uof = new UserOrderFormVO();
            //锁定（部分消费3）
            if (UpdateOSUserOrderFormStatusOfUseing(PayCode, EmpAccount, ZhiFangUserID, WeblisSourceOrgID, WeblisSourceOrgName, ConsumerAreaID) <= 0)
            {
                #region 检查状态
                EntityList<OSUserOrderForm> entityList = DBDao.GetListByHQL("osuserorderform.PayCode=" + PayCode , 1, 999999);
                if (entityList.count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("OSConsumerUserOrderForm.错误的消费码！PayCode:" + PayCode);
                    baseResultDataValue.ErrorInfo = "错误的消费码！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (entityList.list[0].AreaID.ToString().Trim() != ConsumerAreaID)
                {
                    ZhiFang.Common.Log.Log.Debug("OSConsumerUserOrderForm.该消费码不能在当前区域内消费！PayCode:" + PayCode);
                    baseResultDataValue.ErrorInfo = "该消费码不能在当前区域内消费！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                string status = entityList.list[0].Status.ToString();
                if (status != UserOrderFormStatus.已交费.Key)
                {
                    ZhiFang.Common.Log.Log.Debug("OSConsumerUserOrderForm.状态错误！PayCode:" + PayCode + ",status:" + UserOrderFormStatus.GetStatusDic()[status].Name);
                    if (status == UserOrderFormStatus.待缴费.Key)
                    {
                        baseResultDataValue.ErrorInfo = "错误的消费码:订单未缴费！";
                    }

                    if (status == UserOrderFormStatus.使用中.Key)
                    {
                        baseResultDataValue.ErrorInfo = "错误的消费码:订单正在使用中！";
                    }
                    if (status == UserOrderFormStatus.部分使用.Key || status == UserOrderFormStatus.完全使用.Key)
                    {
                        baseResultDataValue.ErrorInfo = "错误的消费码:订单已使用！";
                    }
                    if (status == UserOrderFormStatus.取消处理中.Key || status == UserOrderFormStatus.取消成功.Key || status == UserOrderFormStatus.取消订单.Key)
                    {
                        baseResultDataValue.ErrorInfo = "错误的消费码:订单取消中！";
                    }
                    if (status == UserOrderFormStatus.退款中.Key || status == UserOrderFormStatus.退款完成.Key || status == UserOrderFormStatus.退款申请.Key || status == UserOrderFormStatus.退款申请处理中.Key || status == UserOrderFormStatus.退款申请.Key || status == UserOrderFormStatus.退款申请被打回.Key)
                    {
                        baseResultDataValue.ErrorInfo = "错误的消费码:订单退款中！";
                    }
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                #endregion
            }
            else
            {
                dtuof = DBDao.GetListByHQL("osuserorderform.PayCode="+ PayCode , 1, 999);

                dtdof = IDOSDoctorOrderFormDao.GetListByHQL("osdoctororderform.Id=" + dtuof.list[0].DOFID, 1, 999);
                if (dtdof.count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到相关医嘱单！DOFID:" + dtuof.list[0].DOFID);
                    baseResultDataValue.ErrorInfo = "未找到相关医嘱单！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                dtDoctAccount = IDBDoctorAccountDao.GetListByHQL("bdoctoraccount.Id=" + dtuof.list[0].DoctorAccountID, 1, 999);
                if (dtDoctAccount.count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到相关医生账户！DoctorAccountID:" + dtuof.list[0].DoctorAccountID.ToString());
                    baseResultDataValue.ErrorInfo = "未找到相关医生账户！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                dtUserAccount = IDBWeiXinAccountDao.GetListByHQL("bweixinaccount.Id=" + dtuof.list[0].UserAccountID, 1, 99999);
                if (dtUserAccount.count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到相关用户账户！UserAccountID:" + dtuof.list[0].UserAccountID.ToString());
                    baseResultDataValue.ErrorInfo = "未找到相关用户账户！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                dtuoi = IDOSUserOrderItemDao.GetListByHQL("osuserorderitem.UOFID=" + dtuof.list[0].Id, 1, 99999);
                if (dtuoi.count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到订单项目！UOFID:" + dtuof.list[0].Id.ToString());
                    baseResultDataValue.ErrorInfo = "未找到订单项目！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
            }

            uof.Name = dtdof.list[0].UserName;
            uof.DeptName = dtDoctAccount.list[0].HospitalDeptName;
            uof.Age = int.Parse(dtdof.list[0].Age.ToString());
            uof.DoctorMemo = dtdof.list[0].Memo;
            uof.DoctorName = dtDoctAccount.list[0].Name;
            if (dtdof.list[0].TypeID.ToString().Trim() == "2")
            {
                uof.DeptName = "";
                uof.DoctorName = "";
            }
            uof.PatNo = (dtdof.list[0].PatNo != null) ? dtdof.list[0].PatNo : "";
            uof.Price = dtuof.list[0].Price;
            uof.SexName = dtdof.list[0].SexName;
            uof.AreaID = dtdof.list[0].AreaID.ToString();
            uof.UserOrderItem = new List<UserOrderItemVO>();
            for (int i = 0; i < dtuoi.list.Count; i++)
            {
                UserOrderItemVO uoi = new UserOrderItemVO();
                uoi.ItemID = long.Parse(dtuoi.list[i].ItemID.ToString());
                uoi.ItemNo = dtuoi.list[i].ItemNo;
                uoi.Name = dtuoi.list[i].ItemCName;
                uof.UserOrderItem.Add(uoi);
            }
            baseResultDataValue.ResultDataValue = Common.JsonSerializer.JsonDotNetSerializer(uof);
            baseResultDataValue.success = true;
            return baseResultDataValue;
        }

        public BaseResultDataValue CheckPayCodeIsUseing(string PayCode, string EmpAccount, string ZhiFangUserID, string WeblisOrgID, string WeblisOrgName)
        {
            EntityList<OSUserOrderForm> dtuof = new EntityList<OSUserOrderForm>();
            BaseResultDataValue brdv = new BaseResultDataValue();
            dtuof = DBDao.GetListByHQL("osuserorderform.PayCode=" + PayCode, 1, 999999); 
            if (dtuof.count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("CheckPayCodeIsUseing.错误的消费码！PayCode:" + PayCode);
                brdv.ErrorInfo = "错误的消费码！";
                brdv.success = false;
                return brdv;
            }
            string status = dtuof.list[0].Status.ToString();
            if (status != UserOrderFormStatus.使用中.Key)
            {
                ZhiFang.Common.Log.Log.Debug("CheckPayCodeIsUseing.状态错误！PayCode:" + PayCode + ",status:" + UserOrderFormStatus.GetStatusDic()[status].Name);
                if (status == UserOrderFormStatus.待缴费.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单未缴费！";
                }
                if (status == UserOrderFormStatus.已交费.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单未被锁定！";
                }
                if (status == UserOrderFormStatus.部分使用.Key || status == UserOrderFormStatus.完全使用.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单已使用！";
                }
                if (status == UserOrderFormStatus.取消处理中.Key || status == UserOrderFormStatus.取消成功.Key || status == UserOrderFormStatus.取消订单.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单取消中！";
                }
                if (status == UserOrderFormStatus.退款中.Key || status == UserOrderFormStatus.退款完成.Key || status == UserOrderFormStatus.退款申请.Key || status == UserOrderFormStatus.退款申请处理中.Key || status == UserOrderFormStatus.退款申请.Key || status == UserOrderFormStatus.退款申请被打回.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单退款中！";
                }
                brdv.success = false;
                return brdv;
            }
            else
            {
                if (EmpAccount != dtuof.list[0].EmpAccount.ToString() || ZhiFangUserID != dtuof.list[0].EmpID.ToString())
                {
                    brdv.ErrorInfo = "订单已被其它采样人员使用！";
                    brdv.success = false;
                    return brdv;
                }

                if (WeblisOrgID !=  dtuof.list[0].WeblisSourceOrgID.ToString() || WeblisOrgName !=  dtuof.list[0].WeblisSourceOrgName.ToString())
                {
                    brdv.ErrorInfo = "订单已被其它采血单位使用！";
                    brdv.success = false;
                    return brdv;
                }
            }
            brdv.success = true;
            return brdv;
        }
        public BaseResultDataValue CheckUserOrderForm(string PayCode, out EntityList<BWeiXinAccount> dtUserAccount, out EntityList<BDoctorAccount> dtDoctAccount, out EntityList<OSDoctorOrderForm> dtdof, out EntityList<OSUserOrderForm> dtuof, out EntityList<OSUserOrderItem> dtuoi)
        {
            dtuof = new EntityList<OSUserOrderForm>();
            dtdof = new EntityList<OSDoctorOrderForm>();
            dtDoctAccount = new EntityList<BDoctorAccount>();
            dtUserAccount = new EntityList<BWeiXinAccount>();
            dtuoi = new EntityList<OSUserOrderItem>();

            BaseResultDataValue brdv = new BaseResultDataValue();
            #region 检查状态
            dtuof = DBDao.GetListByHQL("osuserorderform.PayCode=" + PayCode,1,9999); 
            if (dtuof == null || dtuof.count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.错误的消费码！PayCode:" + PayCode);
                brdv.ErrorInfo = "错误的消费码！";
                brdv.success = false;
                return brdv;
            }
            string status = dtuof.list[0].Status.ToString();
            if (status != UserOrderFormStatus.已交费.Key && status != UserOrderFormStatus.使用中.Key)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.状态错误！PayCode:" + PayCode + ",status:" + UserOrderFormStatus.GetStatusDic()[status].Name);
                if (status == UserOrderFormStatus.待缴费.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单未缴费！";
                }
                if (status == UserOrderFormStatus.部分使用.Key || status == UserOrderFormStatus.完全使用.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单已使用！";
                }
                if (status == UserOrderFormStatus.取消处理中.Key || status == UserOrderFormStatus.取消成功.Key || status == UserOrderFormStatus.取消订单.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单取消中！";
                }
                if (status == UserOrderFormStatus.退款中.Key || status == UserOrderFormStatus.退款完成.Key || status == UserOrderFormStatus.退款申请.Key || status == UserOrderFormStatus.退款申请处理中.Key || status == UserOrderFormStatus.退款申请.Key || status == UserOrderFormStatus.退款申请被打回.Key)
                {
                    brdv.ErrorInfo = "错误的消费码:订单退款中！";
                }
                brdv.success = false;
                return brdv;
            }

            if (dtuof.list[0].DOFID == null || dtuof.list[0].DOFID.ToString() == "")
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.订单的医嘱单ID为空！PayCode:" + PayCode + ",UOFID:" + dtuof.list[0].DOFID);
                brdv.ErrorInfo = "错误的消费码！";
                brdv.success = false;
                return brdv;
            }
            dtdof = IDOSDoctorOrderFormDao.GetListByHQL("osdoctororderform.Id=" + dtuof.list[0].DOFID, 1, 999);
            if (dtdof == null || dtdof.count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到相关医嘱单！DOFID:" + dtuof.list[0].DOFID.ToString());
                brdv.ErrorInfo = "未找到相关医嘱单！";
                brdv.success = false;
                return brdv;
            }
            dtDoctAccount = IDBDoctorAccountDao.GetListByHQL("bdoctoraccount.Id=" + dtuof.list[0].DoctorAccountID, 1, 999);
            if (dtDoctAccount == null || dtDoctAccount.count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到相关医生账户！DoctorAccountID:" + dtuof.list[0].DoctorAccountID.ToString());
                brdv.ErrorInfo = "未找到相关医生账户！";
                brdv.success = false;
                return brdv;
            }
            dtUserAccount = IDBWeiXinAccountDao.GetListByHQL("bweixinaccount.Id=" + dtuof.list[0].UserAccountID, 1, 99999);
            if (dtUserAccount == null || dtUserAccount.count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到相关用户账户！UserAccountID:" + dtuof.list[0].UserAccountID.ToString());
                brdv.ErrorInfo = "未找到相关用户账户！";
                brdv.success = false;
                return brdv;
            }
            dtuoi = IDOSUserOrderItemDao.GetListByHQL("osuserorderitem.UOFID=" + dtuof.list[0].Id, 1, 99999);
            if (dtuoi == null || dtuoi.count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ConsumerUserOrderForm.未找到订单项目！UOFID:" + dtuof.list[0].Id.ToString());
                brdv.ErrorInfo = "未找到订单项目！";
                brdv.success = false;
                return brdv;
            }
            #endregion
            brdv.success = true;
            return brdv;
        }
    }
}