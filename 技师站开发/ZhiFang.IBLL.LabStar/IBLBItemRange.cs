using System.Collections.Generic;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBItemRange : IBGenericManager<LBItemRange>
    {

        void EditItemResultStatus(LBItemRange itemRange, ref LisTestItem testItem, IList<LBDict> listDict, IList<LBItemRangeExp> listItemRangeExp);

        IList<Line> QueryItemRangeChartLinePoint(LisTestForm testForm, IList<LisTestItem> listLisTestItem, IList<LBItemRange> listItemRange);
    }
}