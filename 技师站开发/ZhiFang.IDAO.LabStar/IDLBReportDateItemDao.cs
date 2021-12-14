using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBReportDateItemDao : IDBaseDao<LBReportDateItem, long>
    {
        EntityList<LBReportDateItem> QueryLBReportDateItemByFetchDao(string strHqlWhere, string Order, int start, int count);
    }
}