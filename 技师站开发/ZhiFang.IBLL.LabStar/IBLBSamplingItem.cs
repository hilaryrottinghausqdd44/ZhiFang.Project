using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBSamplingItem : IBGenericManager<LBSamplingItem>
    {
        EntityList<LBSamplingItem> QueryLBSamplingItemByFetch(string strHqlWhere, string Order, int start, int count);

        EntityList<LBSamplingGroup> QuerySamplingGroupIsMultiItem(string strHqlWhere, bool isMulti);

        EntityList<LBItem> QueryItemIsMultiSamplingGroup(string strHqlWhere, string strSectionID, bool isMulti);

        EntityList<LBItem> QueryItemNoSamplingGroup(string strHqlWhere, string strSectionID);

        EntityList<LBSamplingItemVO> SearchLBSamplingItemBandItemName(string where, string sort, int page, int limit);
        BaseResultDataValue LS_UDTO_UpdateSamplingItemIsDefault(long? Id, long? ItemId, bool IsDefault);
    }
}