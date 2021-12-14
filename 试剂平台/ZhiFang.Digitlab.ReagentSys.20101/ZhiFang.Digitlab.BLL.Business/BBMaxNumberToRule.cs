
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.Business
{
	/// <summary>
	///
	/// </summary>
    public class BBMaxNumberToRule : BaseBLL<BMaxNumberToRule>, ZhiFang.Digitlab.IBLL.Business.IBBMaxNumberToRule
	{
        /// <summary>
        /// 根据规则ID获取规则最大号
        /// </summary>
        /// <param name="longNumberRuleID">规则ID</param>
        /// <returns>BMaxNumberToRule</returns>
        public BMaxNumberToRule SearchBMaxNumberToRuleByNumberRuleID(long longNumberRuleID)
        {
            return ((IDAO.IDBMaxNumberToRuleDao)DBDao).SearchBMaxNumberToRuleByNumberRuleID(longNumberRuleID);
        }
        /// <summary>
        /// 新增最大号规则
        /// </summary>
        /// <param name="bNumberBuildRule">号码生成规则</param>
        /// <returns></returns>
        public BMaxNumberToRule AddBMaxNumberToRule(BNumberBuildRule bNumberBuildRule)
        {
            BMaxNumberToRule tempBMaxNumberToRule = new BMaxNumberToRule();
            tempBMaxNumberToRule.BNumberBuildRule = bNumberBuildRule;
            //tempBMaxNumberToRule.RuleNumber = bNumberBuildRule.RuleValue;
            tempBMaxNumberToRule.StartDate = DateTime.Now;
            tempBMaxNumberToRule.StartNumber = "0";
            tempBMaxNumberToRule.MaxNumber = "0";
            tempBMaxNumberToRule.DataAddTime = DateTime.Now;
            this.Entity = tempBMaxNumberToRule;
            if (this.Add())
            {
                //如果此方法是在事务中,则下面代码无法取到实体
                //return this.Get(tempBMaxNumberToRule.Id);
                //返回新建对象
                return tempBMaxNumberToRule;
            }
            else
                return null;
        }
	}
}