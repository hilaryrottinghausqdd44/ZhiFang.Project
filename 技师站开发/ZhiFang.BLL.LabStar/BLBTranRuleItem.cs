using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBTranRuleItem : BaseBLL<LBTranRuleItem>, ZhiFang.IBLL.LabStar.IBLBTranRuleItem
    {
        public BaseResultBool DelLBTranRuleItemByHQL(long TranRuleid)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            int result = this.DeleteByHql("From LBTranRuleItem lbtranruleitem where lbtranruleitem.LBTranRule.Id=" + TranRuleid);
            baseResultBool.success = true;
            return baseResultBool;
        }
    }
}