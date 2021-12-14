
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBmsCenOrderDoc : IBGenericManager<BmsCenOrderDoc>
	{
        BaseResultDataValue EditBmsCenOrderDoc(BmsCenOrderDoc bmsCenOrderDoc, string mainFields, IList<BmsCenOrderDtl> listAddBmsCenOrderDtl, IList<BmsCenOrderDtl> listUpdateBmsCenOrderDtl, string childFields, string delBmsCenOrderDtlID, int IsAutoCreateBmsCenSaleDoc);

        BaseResultDataValue AddBmsCenOrderDoc(BmsCenOrderDoc entity);

        string GetBaronOrderJson(string fileTime, string userNo, string logName, string goodsJson, string orderMemo);

        string GetBaronOrderJson(string fileTime, string OrderNo, string goodsJson);

        string GetBaronGoodsJson(IList<BmsCenOrderDtl> listBmsCenOrderDtl, string userNo);

        BaseResultDataValue EditCheckBmsCenOrderDocByID(long id);

        BaseResultDataValue EditAutoCheckBmsCenOrderDocByID(long id);

        BaseResultDataValue EditBmsCenOrderDocTotalPrice(long docID);

        BaseResultDataValue EditBmsCenOrderDocThirdFlag(long id);

        BaseResultDataValue EditOrderDocDeleteFlagByID(string idList, int deleteFlag);

        BaseResultDataValue BmsCenOrderDocAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessage, long Id);

        BaseResultDataValue BmsCenOrderDocConfirmAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, long Id);

        BaseResultDataValue BmsCenOrderDocReviewAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, long Id);

        BaseResultDataValue BmsCenOrderDocCheckedAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, long Id);
    }
}