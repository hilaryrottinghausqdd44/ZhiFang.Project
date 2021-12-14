using ZhiFang.IDAO.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.BloodTransfusion
{
	public interface IDBloodBOutItemDao : IDBaseDao<BloodBOutItem, string>
	{
        /// <summary>
        /// (血制品交接登记)根据申请单信息获取未交接登记完成的发血明细信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBOutItem> SearchBloodBOutItemOfHandoverByBReqVOHQL(string strHqlWhere, string sort, int page, int limit);

        /// <summary>
        /// (血制品交接登记)根据血袋号获取未交接登记完成的发血明细信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBOutItem> SearchBloodBOutItemOfHandoverByBBagCodeHQL(string strHqlWhere, string sort, int page, int limit);

        /// <summary>
        /// (血袋回收登记)获取待进行血袋回收登记的血袋信息(HQL)
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBOutItem> SearchBloodBOutItemOfRecycleByHQL(string strHqlWhere, string sort, int page, int limit);

        /// <summary>
        /// (血袋回收登记)按血袋号获取待进行血袋回收登记的血袋信息(HQL)
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBOutItem> SearchBloodBOutItemOfRecycleByBBagCodeHQL(string strHqlWhere, string sort, int page, int limit);
    } 
}