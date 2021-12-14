
using System;
using System.Collections.Generic;

using System.Data;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsCenOrderDocOfService : IBGenericManager<ReaBmsCenOrderDoc>
    {
        BaseResultBool EditReaBmsCenOrderDocAndDt(ReaBmsCenOrderDoc entity, string[] tempArray, IList<ReaBmsCenOrderDtl> dtEditList, long empID, string empName, bool isUpdate);
        /// <summary>
        /// 供应商(供应商确认/取消确认)操作订单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsCenOrderDocOfComp(ReaBmsCenOrderDoc entity, string[] tempArray, long labId, long empID, string empName);
        void AddReaReqOperation(ReaBmsCenOrderDoc entity, long empID, string empName);
    }
}