

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsQtyBalanceDoc : IBGenericManager<ReaBmsQtyBalanceDoc>
    {
        /// <summary>
        /// 判断当前月是否已生成过库存结转单
        /// </summary>
        /// <returns></returns>
        BaseResultBool GetJudgeISAddReaBmsQtyBalanceDoc(string beginDate, string endDate);
        /// <summary>
        /// 按库存结转单新增库存结转
        /// </summary>
        /// <param name="isCover">是否覆盖当前月产生的库存结转单</param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsQtyBalanceDocOfQtyBalance(ReaBmsQtyBalanceDoc entity, long empID, string empName, bool isCover, string beginDate, string endDate);
        /// <summary>
        /// 启用/禁用库存结转单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResultBool UpdateVisibleReaBmsQtyBalanceDocById(long id, bool visible, long empID, string empName);
    }
}