using System.Collections.Generic;

namespace ZhiFang.Entity.LabStar
{
    //此文件定义前处理基础枚举类

    /// <summary>
    /// 分发规则_复位周期（类型）
    /// </summary>
    public static class TranRuleResetType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 日 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "日", Code = "Day", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 月 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "月", Code = "Month", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 周 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "周", Code = "Week", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 季度 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "季度", Code = "Quarter", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 年 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "年", Code = "Year", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 日.Key, 日.Value },
                { 月.Key, 月.Value },
                { 周.Key, 周.Value },
                { 季度.Key, 季度.Value },
                { 年.Key, 年.Value }
            };
            return dic;
        }
    }
}
