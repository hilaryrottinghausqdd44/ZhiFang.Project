using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Digitlab.IBLL.Business
{
    /// <summary>
    /// 用于解析各种号码生成规则
    /// </summary>
    public interface IBNumberRule
    {
        /// <summary>
        /// 当前号码规则
        /// </summary>
        string NumberRuleContent { get; set; }
        
        /// <summary>
        /// 当前号码规则的最大顺序号
        /// </summary>
        string CurrentMaxNo { get; set; }

        string StartNumber { get; set; }

        DateTime? StartDate { get; set; }

        /// <summary>
        /// 获取下一个号码
        /// </summary>
        /// <returns></returns>
        string GetNextNumber();
    }
}
