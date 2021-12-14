

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
    public interface IBBmsAccountInput : IBGenericManager<BmsAccountInput>
    {
        /// <summary>
        /// 新增入帐及入帐明细,同时更新供货单的入帐标志
        /// </summary>
        /// <param name="saleDocIDStr">待入帐的供货单Id(如123,22323,34445432)</param>
        /// <returns></returns>
        BaseResultDataValue AddBmsAccountInputAndDtList(string saleDocIDStr);
        /// <summary>
        /// 删除入帐及入帐明细,同时更新供货单的入帐标志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResultBool DeleteBmsAccountInputAndDtList(long id);
    }
}