
using System;
using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsReqDtl : IBGenericManager<ReaBmsReqDtl>
    {
        /// <summary>
        /// 保存前验证判断申请明细的货品是否存在供应商货品关系表里
        /// 申请的货品可能从申请明细模板里选择,但货品已经被禁用或已删除的货品不能保存
        /// </summary>
        /// <param name="dtEditList"></param>
        /// <returns></returns>
        BaseResultBool EditDtlListCheck(IList<ReaBmsReqDtl> dtEditList);
        BaseResultBool AddDtList(IList<ReaBmsReqDtl> dtAddList, ReaBmsReqDoc reaBmsReqDoc, long empID, string empName);
        BaseResultBool EditDtList(IList<ReaBmsReqDtl> dtEditList, ReaBmsReqDoc reaBmsReqDoc);

        /// <summary>
        /// 智能采购：计算某个货品的平均使用量和建议采购量
        /// </summary>
        /// <param name="goodsId">货品ID</param>
        /// <param name="m">前m个月</param>
        /// <param name="k">采购系数</param>
        /// <returns></returns>
        ReaGoods CalcAvgUsedAndSuggestPurchaseQty(long goodsId, string m, string k);

        EntityList<ReaBmsReqDtl> GetReaBmsReqDtlListByHQL(string strHqlWhere, string Order, int start, int count);

    }
}