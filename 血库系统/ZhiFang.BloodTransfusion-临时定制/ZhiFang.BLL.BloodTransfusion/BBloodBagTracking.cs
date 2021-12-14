
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    /// 血袋跟踪统计
    /// </summary>
    public class BBloodBagTracking : BaseBLL<BloodBagTrackingVO>, ZhiFang.IBLL.BloodTransfusion.IBBloodBagTracking
    {
        IBBloodBReqItem IBBloodBReqItem { get; set; }
        IBBloodBInItem IBBloodBInItem { get; set; }
        IBBloodBOutItem IBBloodBOutItem { get; set; }
        IBBloodBPreForm IBBloodBPreForm { get; set; }
        IBBloodBPreItem IBBloodBPreItem { get; set; }
        IBBloodRecei IBBloodRecei { get; set; }
        IBBloodBagOperation IBBloodBagOperation { get; set; }
        IBBloodBagOperationDtl IBBloodBagOperationDtl { get; set; }
        IBBloodTransForm IBBloodTransForm { get; set; }
        IBBloodstyle IBBloodstyle { get; set; }

        public BaseResultDataValue GetBReqComplexOfMergeVO(string reqId)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(reqId))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入申请单号为空!";
                return tempBaseResultDataValue;
            }
            //申请血制品信息
            IList<BloodBReqItem> reqItemList = IBBloodBReqItem.SearchListByHQL("bloodbreqitem.BReqFormID='" + reqId + "'");

            //交叉配血血袋信息
            EntityList<BReqComplexOfInInfoVO> preItemList = IBBloodBPreForm.SearchBReqComplexOfInInfoVOByBReqFormID(reqId, "", -1, -1);

            //发血血袋信息
            IList<BloodBOutItem> outItemList = IBBloodBOutItem.SearchListByHQL("bloodboutitem.BloodBReqItem.BReqFormID='" + reqId + "'");

            //血袋接收
            IList<BloodBagOperation> receptionList = IBBloodBagOperation.SearchListByHQL("bloodbagoperation.BloodBReqForm.Id='" + reqId + "'");

            //血袋回收
            IList<BloodBagOperation> bloodRecoveryList = IBBloodBagOperation.SearchListByHQL("bloodbagoperation.BloodBReqForm.Id='" + reqId + "'");

            return tempBaseResultDataValue;
        }
    }
}