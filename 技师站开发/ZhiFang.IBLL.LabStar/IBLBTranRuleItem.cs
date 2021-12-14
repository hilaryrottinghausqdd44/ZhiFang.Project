using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBTranRuleItem : IBGenericManager<LBTranRuleItem>
    {
        BaseResultBool DelLBTranRuleItemByHQL(long id);
    }
}