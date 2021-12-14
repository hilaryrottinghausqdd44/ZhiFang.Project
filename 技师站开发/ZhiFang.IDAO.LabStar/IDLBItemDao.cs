using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBItemDao : IDBaseDao<LBItem, long>
    {
        void TestDao(long id, string strHqlWhere, string Order, int limit);

        /// <summary>
        /// 按传入的检验小组Id从小组项目里获取检验项目信息(传入的检验小组Id值为空时从检验项目获取信息)
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<LBItem> SearchLBItemEntityListByLBSectionItemHQL(string strHqlWhere, string Order, int start, int count);

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

    }
}