using System.Collections.Generic;

namespace ZhiFang.Entity.LabStar
{
    //此文件定义质控枚举类
    public static class QCRule
    {
        public static KeyValuePair<string, BaseClassDicEntity> N_xS = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "N-xS", Code = "N-xS[连续N个结果超过x倍标准差]", FontColor = "#ffffff", BGColor = "", Memo = "N个连续的质控测定结果同时超过(平均数)±(x倍标准差),为违背此规则" });
        public static KeyValuePair<string, BaseClassDicEntity> R_xS = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "R-xS", Code = "R-xS[极差超过x倍标准差]", FontColor = "#ffffff", BGColor = "", Memo = "同批两个质控结果之差值超过(x倍标准差),为违背此规则,表示存在随机误差" });
        public static KeyValuePair<string, BaseClassDicEntity> N_T = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "N-T", Code = "N-T[连续N个结果上升或下降趋势]", FontColor = "#ffffff", BGColor = "", Memo = "N个连续的质控结果呈现出向上或向下的趋势,为违背此规则" });
        public static KeyValuePair<string, BaseClassDicEntity> N_X = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "N-X", Code = "N-X[连续N个记过在平均数一侧]", FontColor = "#ffffff", BGColor = "", Memo = "N个连续的质控结果落在平均数的一侧,为违背此规则,表示存在系统误差" });
        public static KeyValuePair<string, BaseClassDicEntity> M_Of_NxS = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "(M Of N)xS", Code = "(M Of N)xS[连续N结果中,有M个结果超过x倍标准差]", FontColor = "#ffffff", BGColor = "", Memo = "连续的N个质控结果中,有M个质控结果超过(平均数)±(x倍标准差)控制限" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { N_xS.Key, N_xS.Value },
                { R_xS.Key, R_xS.Value },
                { N_T.Key, N_T.Value },
                { N_X.Key, N_X.Value },
                { M_Of_NxS.Key, M_Of_NxS.Value }
            };
            return dic;
        }
    }
    public static class QC_PrintType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 日质控 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "日质控", Code = "", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 仪器日质控 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "仪器日质控 ", Code = "", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 月质控 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "月质控 ", Code = "", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 多浓度质控 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "多浓度质控 ", Code = "", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 日质控.Key, 日质控.Value },
                { 仪器日质控.Key, 仪器日质控.Value },
                { 多浓度质控.Key, 多浓度质控.Value },
                { 月质控.Key, 月质控.Value }
            };
            return dic;
        }
    }

    public static class QC_Para_ZImage
    {

    }
}
