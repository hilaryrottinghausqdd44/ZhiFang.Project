

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBloodBOutItem : IBGenericManager<BloodBOutItem, string>
    {
        /// <summary>
        /// (血制品交接登记)根据申请单信息获取未交接登记完成的出库血袋信息
        /// </summary>
        /// <param name="bReqVO"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBOutItem> SearchBloodBOutItemOfHandoverByBReqVOHQL(BloodBReqFormVO bReqVO,string where, string sort, int page, int limit);

        /// <summary>
        /// (血制品交接登记)根据血袋号获取未交接登记完成的出库血袋信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="scanCodeField">血袋扫码识别字段</param>
        /// <param name="bagCode">条码值</param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tempBaseResultDataValue"></param>
        /// <returns></returns>
        EntityList<BloodBOutItem> SearchBloodBOutItemOfHandoverByBBagCodeHQL(string where, string scanCodeField, string bagCode, string sort, int page, int limit, ref BaseResultDataValue tempBaseResultDataValue);

        /// <summary>
        /// (血制品交接登记)根据发血单号获取发血血袋信息(包含血袋外观及完整性信息)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="outId"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tempBaseResultDataValue"></param>
        /// <returns></returns>
        EntityList<BloodBOutItem> SearchBloodBOutItemAndBagOperDtlOfHandoverByHQL(string where,string sort, int page, int limit, ref BaseResultDataValue tempBaseResultDataValue);

        /// <summary>
        /// (血袋回收登记)获取待进行血袋回收登记的出库血袋信息(HQL)
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBOutItem> SearchBloodBOutItemOfRecycleByHQL(string strHqlWhere, string sort, int page, int limit);

        /// <summary>
        /// (血袋回收登记)按血袋号获取待进行血袋回收登记的出库血袋信息(HQL)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="scanCodeField">血袋扫码识别字段</param>
        /// <param name="bagCode"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tempBaseResultDataValue"></param>
        /// <returns></returns>
        EntityList<BloodBOutItem> SearchBloodBOutItemOfRecycleByBBagCodeHQL(string where, string scanCodeField, string bagCode, string sort, int page, int limit, ref BaseResultDataValue tempBaseResultDataValue);

        /// <summary>
        /// (输血过程记录登记)获取待进行输血过程记录登记的出库血袋信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBOutItem> SearchBloodBOutItemOfBloodTransByHQL(string strHqlWhere, string sort, int page, int limit);

        #region 
        /// <summary>
        /// (输血申请综合查询)按申请单号获取发血血袋信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="reqFormId"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBOutItem> SearchBloodBOutItemByBReqFormIDAndHQL(string strHqlWhere, string reqFormId, string sort, int page, int limit);

        #endregion
    }
}