using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBReportDateItem : BaseBLL<LBReportDateItem>, ZhiFang.IBLL.LabStar.IBLBReportDateItem
    {
        public EntityList<LBReportDateItem> QueryLBReportDateItemByFetch(string strHqlWhere, string Order, int start, int count)
        {
            return (this.DBDao as IDLBReportDateItemDao).QueryLBReportDateItemByFetchDao(strHqlWhere, Order, start, count);
        }
    }
}