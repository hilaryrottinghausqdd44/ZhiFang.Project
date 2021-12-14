using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBReportDateRule : IBGenericManager<LBReportDateRule>
    {
        EntityList<LBReportDateRule> QueryLBReportDateRuleByFetch(string strHqlWhere, string Order, int start, int count);
    }
}