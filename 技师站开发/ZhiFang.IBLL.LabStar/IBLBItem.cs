using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBItem : IBGenericManager<LBItem>
    {

        void Test(long id, string strHqlWhere, string Order, int limit);

        void TestAnWeiYu();

        /// <summary>
        /// 更新检验项目的默认参考值范围
        /// </summary>
        /// <param name="itemID">检验项目ID</param>
        /// <returns></returns>
        BaseResultDataValue EditLBItemDefaultRange(long itemID);

        /// <summary>
        /// 按传入的检验小组Id从小组项目里获取检验项目信息(传入的检验小组Id值为空时从检验项目获取信息)
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="lbsectionId">检验小组Id值</param>
        /// <returns></returns>
        EntityList<LBItem> SearchLBItemEntityListByLBSectionItemHQL(string strHqlWhere, string order, int page, int count, string lbsectionId);

        /// <summary>
        /// 获取未进行组合项目拆分的组合项目信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<LBItem> SearchNotLBParItemSplitPLBItemListByHQL(string strHqlWhere, string order, int page, int count);

        /// <summary>
        /// 从已进行组合项目拆分的关系表获取到组合项目集合信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<LBItem> SearchAlreadyLBParItemSplitPLBItemListByHQL(string strHqlWhere, string order, int page, int count);
        List<tree> GetItemModelTree();

        List<tree> GetItemTreeByItemIDList(string strItemID);

        EntityList<LBItem> LS_UDTO_SearchLBItemBySamplingGroupID(long SamplingGroupID, long SectionID, int page, int limit, string where, string sort);
        EntityList<LBItem> LS_UDTO_SearchLBItemByReportDateID(long ReportDateID, long SectionID, int page, int limit, string where, string sort);
        List<tree> GetModelItemsTree(string tid,List<LBItem> lBItems);
    }
}