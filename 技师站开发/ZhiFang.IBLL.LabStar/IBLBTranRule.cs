using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBTranRule : IBGenericManager<LBTranRule>
    {
        EntityList<LBTranRule> GetLBTranRuleList(string where, string sort,int page,int limit);

        BaseResultDataValue GetLBTranRuleNextSampleNo(int? SampleNoSection, string SampleNoPrefix);

        BaseResultBool DelLBTranRuleAndTranRuleItem(long LBTranRuleTypeID);
    }
}