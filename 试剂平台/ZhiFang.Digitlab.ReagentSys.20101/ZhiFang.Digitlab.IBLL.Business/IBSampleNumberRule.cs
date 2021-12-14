using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
    /// <summary>
    /// 样本号取号规则
    /// </summary>
    public interface IBSampleNumberRule
    {
        /// <summary>
        /// 号码生成规则
        /// </summary>
        string NumberRuleContent { get; set; }
        /// <summary>
        /// 规则号码，示例：固定字符|日期格式|流水号最大号,T|0928|1
        /// </summary>
        string RuleNumber { get; set; }
        /// <summary>
        /// 规则因子在规则字段里的索引位置
        /// </summary>
        int SubRuleIndex { get; set; }

        /// <summary>
        /// 获取下一个样本号
        /// </summary>
        /// <param name="strSampleNumber">返回生成的下一个样本号</param>
        /// <param name="endNum">顺序号区间结束值</param>
        /// <param name="isMaxNum">获取生成的样本号是否已经达到最大样本号</param>
        /// <returns>BaseResultBool</returns>
        BaseResultBool GetSampleNumber(out string strSampleNumber, out int endNum, out bool isMaxNum);
    }
}
