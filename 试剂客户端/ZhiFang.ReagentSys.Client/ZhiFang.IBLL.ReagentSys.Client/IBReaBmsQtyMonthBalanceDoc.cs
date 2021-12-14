

using System.Collections.Generic;
using System.IO;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsQtyMonthBalanceDoc : IBGenericManager<ReaBmsQtyMonthBalanceDoc>
    {
        /// <summary>
        /// 按库存结转单生成结转报表
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue SaveOfQtyBalanceDtlList(ReaBmsQtyMonthBalanceDoc entity, string labCName, long empID, string empName);
        /// <summary>
        /// 按库存变化操作记录新增结转报表生成结转报表
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue SaveQtyBalanceReportOfQtyDtlOperList(ReaBmsQtyMonthBalanceDoc entity, string labCName, long empID, string empName);
        /// <summary>
        /// 取消结转报表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResultBool UpdateCancelReaBmsQtyMonthBalanceDocById(long id, long empID, string empName);
        /// <summary>
        /// 依结转报表ID获取月结统计明细数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tempBaseResultDataValue"></param>
        /// <returns></returns>
        EntityList<ReaBmsQtyMonthBalanceDtl> SearchQtyMonthBalanceDtlListById(long id, int page, int limit, ref BaseResultDataValue tempBaseResultDataValue);
        /// <summary>
        /// 依结转报表ID获取生成PDF格式的结转报表文件
        /// </summary>
        /// <param name="id">结转报表Id</param>
        /// <param name="frx">结转报表模板名称</param>
        /// <returns></returns>
        Stream GetQtyMonthBalanceAndDtlOfPdf(long id, string frx, string labCName);
    }
}