
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBmsCenOrderDoc : IBGenericManager<BmsCenOrderDoc>
    {
        BaseResultDataValue EditBmsCenOrderDoc(BmsCenOrderDoc bmsCenOrderDoc, string mainFields, IList<BmsCenOrderDtl> listAddBmsCenOrderDtl, IList<BmsCenOrderDtl> listUpdateBmsCenOrderDtl, string childFields, string delBmsCenOrderDtlID, int IsAutoCreateBmsCenSaleDoc);

        BaseResultDataValue AddBmsCenOrderDoc(BmsCenOrderDoc entity);

        BaseResultData AddBmsCenOrderDoc(string jsonEntity);

        BaseResultData AddBmsCenOrderDoc(string jsonEntity, string orderDocNo, string labNo, string compNo);

        string GetBaronOrderJson(string fileTime, string userNo, string logName, string goodsJson, string orderMemo);

        string GetBaronOrderJson(string fileTime, string OrderNo, string goodsJson);

        string GetBaronGoodsJson(IList<BmsCenOrderDtl> listBmsCenOrderDtl, string userNo);

        BaseResultDataValue EditCheckBmsCenOrderDocByID(long id);

        BaseResultDataValue EditAutoCheckBmsCenOrderDocByID(long id);

        BaseResultDataValue EditBmsCenOrderDocTotalPrice(long docID);

        BaseResultDataValue EditBmsCenOrderDocThirdFlag(long id, int isThirdFlag, string reason);

        BaseResultDataValue EditOrderDocDeleteFlagByID(string idList, int deleteFlag);

        BmsCenOrderDoc GetOrderDocByOtherOrderDocNo(string otherOrderDocNo);

        EntityList<BmsCenOrderDoc> SearchBmsCenOrderDoc(string orderDocWhere, string orderDtlWhere, string sort, int page, int limit);

        DataSet GetBmsCenOrderDtlInfoByID(string idList, string where, string sort, string xmlPath);

        BaseResultDataValue BmsCenOrderDocAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessage, long Id);

        BaseResultDataValue BmsCenOrderDocConfirmAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, long Id);

        BaseResultDataValue BmsCenOrderDocReviewAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, long Id);

        BaseResultDataValue BmsCenOrderDocCheckedAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, long Id);

        BaseResultDataValue AddOtherOrderDocNoByInterface(long orderDocID, string jsonResult);

        #region 客户端订单处理

        /// <summary>
        /// 客户端物理删除平台订单信息(订单状态都为暂存或已提交才能删除)
        /// </summary>
        /// <param name="labOrgNo">实验室编号</param>
        /// <param name="orderDocNo">订单编号</param>
        /// <returns></returns>
        BaseResultBool DelBmsCenOrderDocByLabOrgNoAndOrderDocNo(string labOrgNo, string orderDocNo);
        /// <summary>
        /// 客户端申请生成订单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtl"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool AddReaBmsCenOrderDocAndDtl(BmsCenOrderDoc entity, Dictionary<string, BmsCenOrderDtl> dtl, long empID, string empName);

        BaseResultDataValue AddReaBmsCenOrderDocAndDt(BmsCenOrderDoc entity, IList<BmsCenOrderDtl> dtAddList, long empID, string empName);

        BaseResultBool EditReaBmsCenOrderDocAndDt(BmsCenOrderDoc entity, string[] tempArray, IList<BmsCenOrderDtl> dtAddList, IList<BmsCenOrderDtl> dtEditList, long empID, string empName);
        #endregion
    }
}