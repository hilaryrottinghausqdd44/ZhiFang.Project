using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBReportDate : IBGenericManager<LBReportDate>
    {
        BaseResultDataValue DeleteLBReportDateByID(long id);

    }
}