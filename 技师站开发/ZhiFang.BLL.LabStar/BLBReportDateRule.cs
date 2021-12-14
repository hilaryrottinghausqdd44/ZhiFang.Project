using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBReportDateRule : BaseBLL<LBReportDateRule>, ZhiFang.IBLL.LabStar.IBLBReportDateRule
    {
        public EntityList<LBReportDateRule> QueryLBReportDateRuleByFetch(string strHqlWhere, string Order, int start, int count)
        {
            return (this.DBDao as IDLBReportDateRuleDao).QueryLBReportDateRuleByFetchDao(strHqlWhere, Order, start, count);
        }
    }
}