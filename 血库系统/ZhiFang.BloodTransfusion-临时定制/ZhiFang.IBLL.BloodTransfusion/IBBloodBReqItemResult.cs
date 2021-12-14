

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBloodBReqItemResult : IBGenericManager<BloodBReqItemResult>
    {
        IList<BloodBReqItemResult> SearchBloodBReqItemResultListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit);
        EntityList<BloodBReqItemResult> SearchBloodBReqItemResultEntityListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit);
        /// <summary>
        /// 新增医嘱申请时同时保存病人对应时间范围内的检验结果
        /// </summary>
        /// <param name="reqForm"></param>
        /// <returns></returns>
        BaseResultDataValue AddBReqItemResultOfReqForm(BloodBReqForm reqForm);
        /// <summary>
        /// 按传入的病人信息(病历号+姓名=日期范围)获取病人的检验结果(HQL)
        /// </summary>
        /// <param name="vlisresultHql">LIS检验结果视图查询条件</param>
        /// <param name="reqresulthql">医嘱申请的LIS检验结果查询条件</param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBReqItemResult> GetBloodBReqItemResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, string sort, int page, int limit);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="addResultList"></param>
        /// <returns></returns>
        BaseResultDataValue AddBReqItemResultList(BloodBReqForm entity, IList<BloodBReqItemResult> addResultList);
    }
}