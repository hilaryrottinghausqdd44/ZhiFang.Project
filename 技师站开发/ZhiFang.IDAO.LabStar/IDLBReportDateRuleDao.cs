using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBReportDateRuleDao : IDBaseDao<LBReportDateRule, long>
    {
        EntityList<LBReportDateRule> QueryLBReportDateRuleByFetchDao(string strHqlWhere, string Order, int start, int count);

    }
}