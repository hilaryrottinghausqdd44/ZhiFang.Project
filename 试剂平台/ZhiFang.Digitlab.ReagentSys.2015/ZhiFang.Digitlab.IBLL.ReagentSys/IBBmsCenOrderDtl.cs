

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaConfirm;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBmsCenOrderDtl : IBGenericManager<BmsCenOrderDtl>
    {
        BaseResultBool AddDtList(IList<BmsCenOrderDtl> dtAddList, BmsCenOrderDoc reaBmsOrderDoc, long empID, string empName);
        BaseResultBool EditDtList(IList<BmsCenOrderDtl> dtEditList, BmsCenOrderDoc reaBmsOrderDoc);
        /// <summary>
        /// 定制客户端订单验收获取某一订单的订单明细集合信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<ReaOrderDtlVO> SearchReaOrderDtlVOListByHQL(string strHqlWhere, string Order, int page, int count);
        /// <summary>
        /// 定制客户端订单验收--获取某一订单明细VO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ReaOrderDtlVO GetReaOrderDtlVO(long id);
    }
}