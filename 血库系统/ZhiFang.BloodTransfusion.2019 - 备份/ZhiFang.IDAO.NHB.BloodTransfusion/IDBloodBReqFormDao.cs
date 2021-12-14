using ZhiFang.IDAO.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.Base;
using System.Collections.Generic;

namespace ZhiFang.IDAO.NHB.BloodTransfusion
{
    public interface IDBloodBReqFormDao : IDBaseDao<BloodBReqForm, string>
    {
        #region 医生站
        IList<BloodBReqForm> SearchBloodBReqFormListByJoinHql(string where, string sort, int page, int limit);
        EntityList<BloodBReqForm> SearchBloodBReqFormEntityListByJoinHql(string where, string sort, int page, int limit);
        /// <summary>
        /// 更新用血申请的打印总数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        bool UpdateBloodBReqFormPrintTotalById(string id);
        #endregion

        #region 护士站
        /// <summary>
        /// 通过血袋号)获取输血申请综合查询的BloodBReqForm(HQL)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="bbagCode"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBReqForm> SearchBloodBReqFormListByBBagCodeAndHql(string where, string scanCodeField, string bbagCode, string sort, int page, int limit);

        #endregion

        /// <summary>
        /// 按申请信息建立病区与科室关系
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<WarpAndDeptVO> GetWarpAndDeptList(string strHqlWhere, string sort, int page, int limit);
    }
}