using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.Base;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.IBLL
{
    public interface IBBRuleNumber : ZhiFang.IBLL.Base.IBGenericManager<BRuleNumber>
    {
        /// <summary>
        /// 退费单编号
        /// </summary>
        /// <returns></returns>
        string GetMRefundFormCode();
        /// <summary>
        /// 消费单编号
        /// </summary>
        /// <returns></returns>
        string GetOSUserConsumerFormCode();
    }
}
