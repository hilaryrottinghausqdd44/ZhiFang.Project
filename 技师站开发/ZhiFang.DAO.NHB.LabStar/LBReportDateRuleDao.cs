using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBReportDateRuleDao : BaseDaoNHB<LBReportDateRule, long>, IDLBReportDateRuleDao
    {
        public EntityList<LBReportDateRule> QueryLBReportDateRuleByFetchDao(string strHqlWhere, string Order, int start, int count)
        {
            string strHQL = " select lbreportdaterule from LBReportDateRule lbreportdaterule " +
                " left join fetch lbreportdaterule.LBReportDate lbreportdate ";
            string strHQLCount = " select count(*) from LBReportDateRule lbreportdaterule " +
                " left join lbreportdaterule.LBReportDate lbreportdate ";
            return this.GetListByHQL(strHqlWhere, Order, start, count, strHQL, strHQLCount);
        }
    }
}