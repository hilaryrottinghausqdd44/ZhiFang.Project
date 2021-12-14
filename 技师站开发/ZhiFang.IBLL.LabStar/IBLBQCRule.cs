using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBQCRule : IBGenericManager<LBQCRule>
    {
        BaseResultDataValue DeleteInvalidLBQCRule();

        BaseResultDataValue AddLBQCItemRuleByItemList(IList<LBQCItem> listLBQCItem, IList<LBQCRule> listLBQCRule);

    }
}