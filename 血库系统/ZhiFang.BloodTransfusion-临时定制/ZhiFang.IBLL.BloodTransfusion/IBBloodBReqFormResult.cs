

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBloodBReqFormResult : IBGenericManager<BloodBReqFormResult>
    {
        IList<BloodBReqFormResult> SearchBloodBReqFormResultListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit);
        EntityList<BloodBReqFormResult> SearchBloodBReqFormResultEntityListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit);
        /// <summary>
        /// 新增或编辑医嘱申请时,按传入的病人信息(病历号+姓名=日期范围)获取病人的检验结果(HQL)
        /// </summary>
        /// <param name="vlisresultHql">LIS检验结果视图查询条件</param>
        /// <param name="reqresulthql">医嘱申请的LIS检验结果查询条件</param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBReqFormResult> GetBloodBReqFormResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, string sort, int page, int limit);
        /// <summary>
        /// 新增或编辑医嘱申请时,按传入的病人信息(病历号+姓名=日期范围)获取病人的检验结果(HQL)
        /// </summary>
        /// <param name="vlisresultHql">LIS检验结果视图查询条件</param>
        /// <param name="reqresulthql">医嘱申请的LIS检验结果查询条件</param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBReqFormResult> SelectBloodBReqFormResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, string sort, int page, int limit);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="addResultList"></param>
        /// <returns></returns>
        BaseResultDataValue AddBReqFormResultList(BloodBReqForm entity, IList<BloodBReqFormResult> addResultList);
        BaseResultDataValue EditBReqFormResultList(BloodBReqForm reqForm, IList<BloodBReqFormResult> editResultList);
    }
}