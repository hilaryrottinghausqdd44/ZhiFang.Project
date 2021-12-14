using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBReportDate : BaseBLL<LBReportDate>, ZhiFang.IBLL.LabStar.IBLBReportDate
    {
        ZhiFang.IBLL.LabStar.IBLBReportDateItem IBLBReportDateItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBReportDateRule IBLBReportDateRule { get; set; }

        public BaseResultDataValue DeleteLBReportDateByID(long id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            int intDelCount = IBLBReportDateItem.DeleteByHql(" from LBReportDateItem lbreportdateitem where lbreportdateitem.LBReportDate.Id=" + id.ToString());
            intDelCount = IBLBReportDateRule.DeleteByHql(" from LBReportDateRule lbreportdaterule where lbreportdaterule.LBReportDate.Id=" + id.ToString());
            intDelCount = this.DeleteByHql(" from LBReportDate lbreportdate where lbreportdate.Id=" + id.ToString());
            return baseResultDataValue;
        }
    }
}