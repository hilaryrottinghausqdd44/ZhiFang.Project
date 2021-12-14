namespace ZhiFang.Entity.LabStar
{
    /// <summary>
    /// 检验单历史对比相关参数
    /// </summary>
    public struct ItemHistoryComparePara
    {
        /// <summary>
        /// 是否进行历史对比
        /// </summary>
        public bool IsHistoryCompare;
        /// <summary>
        /// 对比日期范围
        /// </summary>
        public int HistoryCompareDays;

        /// <summary>
        /// 历史对比小组范围
        /// </summary>
        public string HistoryCompareSection;

        /// <summary>
        /// 是否区分样本类型
        /// </summary>
        public bool IsCompareSampleType;

        /// <summary>
        /// 历史对比查询字段列表
        /// </summary>
        public string HistoryCompareField;

        /// <summary>
        /// 需对比的历史结果记录数
        /// </summary>
        public int CompareRecordCount;

        /// <summary>
        /// 历史对比百分比小数位数
        /// </summary>
        public int DecimalBit;

        /// <summary>
        /// 历史对比超高低百分比参考值
        /// </summary>
        public double DiffValueHH;

        /// <summary>
        /// 历史对比高低百分比参考值
        /// </summary>
        public double DiffValueH;

        /// <summary>
        /// 历史对比超高百分比符号
        /// </summary>
        public string DiffValueHHFalg;

        /// <summary>
        /// 历史对比超低百分比符号
        /// </summary>
        public string DiffValueLLFalg;

        /// <summary>
        /// 历史对比高百分比符号
        /// </summary>
        public string DiffValueHFalg;

        /// <summary>
        /// 历史对比低百分比符号
        /// </summary>
        public string DiffValueLFalg;
    }
}
