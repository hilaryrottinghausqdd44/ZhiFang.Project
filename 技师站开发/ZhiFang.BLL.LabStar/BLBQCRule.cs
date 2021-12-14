using System;
using System.Collections.Generic;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBQCRule : BaseBLL<LBQCRule>, ZhiFang.IBLL.LabStar.IBLBQCRule
    {
        ZhiFang.IBLL.LabStar.IBLBQCItemRule IBLBQCItemRule { get; set; }

        ZhiFang.IBLL.LabStar.IBLBQCRulesCon IBLBQCRulesCon { get; set; }

        public BaseResultDataValue DeleteInvalidLBQCRule()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<LBQCRule> listLBQCRule = this.SearchListByHQL(" (lbqcrule.BDefault=false or lbqcrule.BDefault is null) and (lbqcrule.BWestGard=false or lbqcrule.BWestGard is null)");
            if (listLBQCRule != null && listLBQCRule.Count > 0)
            {
                foreach (LBQCRule lbQCRule in listLBQCRule)
                {
                    IList<LBQCItemRule> listLBQCItemRule = IBLBQCItemRule.SearchListByHQL(" lbqcitemrule.LBQCRule.Id=" + lbQCRule.Id);
                    if (listLBQCItemRule == null || listLBQCItemRule.Count == 0)
                    {
                        int intDelCount = IBLBQCRulesCon.DeleteByHql(" from LBQCRulesCon lbqcrulescon where lbqcrulescon.LBQCRule.Id=" + lbQCRule.Id);
                        intDelCount = this.DeleteByHql(" from LBQCRule lbqcrule where lbqcrule.Id=" + lbQCRule.Id);
                    }
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddLBQCItemRuleByItemList(IList<LBQCItem> listLBQCItem, IList<LBQCRule> listLBQCRule)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listLBQCItem == null || listLBQCItem.Count == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "质控项目实体列表不能为空！";
                return baseResultDataValue;
            }
            string itemIDList = "";
            foreach (LBQCItem lbQCItem in listLBQCItem)
            {
                if (itemIDList == "")
                    itemIDList = lbQCItem.Id.ToString();
                else
                    itemIDList += "," + lbQCItem.Id.ToString();
            }
            int intDelCount = IBLBQCItemRule.DeleteByHql(" from LBQCItemRule lbqcitemrule where lbqcitemrule.LBQCItem.Id in (" + itemIDList + ")");
            if (listLBQCRule != null && listLBQCRule.Count > 0)
            {
                foreach (LBQCItem lbQCItem in listLBQCItem)
                {

                    foreach (LBQCRule lbQCRule in listLBQCRule)
                    {
                        LBQCItemRule lbQCItemRule = new LBQCItemRule();
                        lbQCItemRule.LabID = lbQCItem.LabID;
                        lbQCItemRule.LBQCItem = lbQCItem;
                        lbQCItemRule.LBQCRule = lbQCRule;
                        lbQCItemRule.DataUpdateTime = DateTime.Now;
                        IBLBQCItemRule.Entity = lbQCItemRule;
                        IBLBQCItemRule.Add();
                    }
                }
            }
            return baseResultDataValue;
        }

    }
}