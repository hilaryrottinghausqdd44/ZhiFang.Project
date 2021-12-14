

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBloodBOutForm : IBGenericManager<BloodBOutForm, string>
    {
        /// <summary>
        /// (血袋交接登记)获取已发血但是未交接完成的病人清单
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBReqFormVO> SearchBloodBReqFormVOOfHandoverByHQL(string strHqlWhere, string sort, int page, int limit);

        /// <summary>
        /// (血制品交接登记)根据申请信息获取未交接登记完成的出库主单信息
        /// </summary>
        /// <param name="bReqVO"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBOutForm> SearchBloodBOutFormOfHandoverByBReqVOHQL(BloodBReqFormVO bReqVO, string where, string sort, int page, int limit);

        /// <summary>
        /// (输血过程记录登记)获取待进行输血过程记录登记的出库主单信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBOutForm> SearchBloodBOutFormOfBloodTransByHQL(string strHqlWhere, string sort, int page, int limit);
        
        /// <summary>
        /// 按发血单ID手工标记发血主单及明细的输血登记完成度
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateValue"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>

        BaseResultBool EditBOutCourseCompletionByOutId(string id, string updateValue, long empID, string empName);

        /// <summary>
        /// 对发血主单进行终止输血操作更新
        /// </summary>
        /// <param name="updateEntity"></param>
        /// <param name="updateValue"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>

        BaseResultBool EditBOutCourseCompletionByEndBloodOper(BloodBOutForm updateEntity, string updateValue, long empID, string empName);
    }
}