

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBMaxNumberToRule : IBGenericManager<BMaxNumberToRule>
	{
           /// <summary>
        /// 根据规则ID获取规则最大号
        /// </summary>
        /// <param name="longNumberRuleID">规则ID</param>
        /// <returns>BMaxNumberToRule</returns>
        BMaxNumberToRule SearchBMaxNumberToRuleByNumberRuleID(long longNumberRuleID);

        BMaxNumberToRule AddBMaxNumberToRule(BNumberBuildRule bNumberBuildRule);
	}
}