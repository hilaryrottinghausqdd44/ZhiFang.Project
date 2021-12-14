using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBReportDateItemDao : BaseDaoNHB<LBReportDateItem, long>, IDLBReportDateItemDao
    {
        public EntityList<LBReportDateItem> QueryLBReportDateItemByFetchDao(string strHqlWhere, string Order, int start, int count)
        {
            string strHQL = " select lbreportdateitem from LBReportDateItem lbreportdateitem " +
                " left join fetch lbreportdateitem.LBItem lbitem " +
                " left join fetch lbreportdateitem.LBReportDate lbreportdate ";
            string strHQLCount = " select count(*) from LBReportDateItem lbreportdateitem " +
                " left join lbreportdateitem.LBItem lbitem " +
                " left join lbreportdateitem.LBReportDate lbreportdate ";
            return this.GetListByHQL(strHqlWhere, Order, start, count, strHQL, strHQLCount);
        }
    }
}