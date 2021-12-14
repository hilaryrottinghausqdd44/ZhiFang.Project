using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBReportDateItem : IBGenericManager<LBReportDateItem>
    {
        EntityList<LBReportDateItem> QueryLBReportDateItemByFetch(string strHqlWhere, string Order, int start, int count);
    }
}