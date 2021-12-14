using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBItemTimeW : IBGenericManager<LBItemTimeW>
    {
        BaseResultDataValue QueryLisTestItemOverTime(LisTestForm testForm, IList<LisTestItem> listTestItem);
    }
}