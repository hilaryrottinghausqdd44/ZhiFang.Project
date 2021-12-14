using System.Collections.Generic;

namespace ZhiFang.Entity.LabStar
{
    //此文件定义前处理枚举类

    /// <summary>
    /// 操作类型
    /// </summary>
    public static class OrderFromOperateType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 保存医嘱单 = new KeyValuePair<string, BaseClassDicEntity>("100001", new BaseClassDicEntity() { Id = "100001", Name = "保存医嘱单", Code = "BCYZD", FontColor = "#ffffff", BGColor = "", Memo = "医嘱单-保存医嘱单" });
        public static KeyValuePair<string, BaseClassDicEntity> 修改医嘱单 = new KeyValuePair<string, BaseClassDicEntity>("100002", new BaseClassDicEntity() { Id = "100002", Name = "修改医嘱单", Code = "XGYZD", FontColor = "#ffffff", BGColor = "", Memo = "医嘱单-修改医嘱单" });
        public static KeyValuePair<string, BaseClassDicEntity> 删除医嘱单 = new KeyValuePair<string, BaseClassDicEntity>("100003", new BaseClassDicEntity() { Id = "100003", Name = "删除医嘱单", Code = "SCYZD", FontColor = "#ffffff", BGColor = "", Memo = "医嘱单-删除医嘱单" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核医嘱单 = new KeyValuePair<string, BaseClassDicEntity>("100004", new BaseClassDicEntity() { Id = "100004", Name = "审核医嘱单", Code = "SHYZD", FontColor = "#ffffff", BGColor = "", Memo = "医嘱单-审核医嘱单" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消审核医嘱单 = new KeyValuePair<string, BaseClassDicEntity>("100005", new BaseClassDicEntity() { Id = "100005", Name = "取消审核医嘱单", Code = "QXSHYZD", FontColor = "#ffffff", BGColor = "", Memo = "医嘱单-取消审核医嘱单" });
        public static KeyValuePair<string, BaseClassDicEntity> 保存采样样本单 = new KeyValuePair<string, BaseClassDicEntity>("100006", new BaseClassDicEntity() { Id = "100006", Name = "保存采样样本单", Code = "BCCYYBD", FontColor = "#ffffff", BGColor = "", Memo = "样本单-保存采样样本单" });
        public static KeyValuePair<string, BaseClassDicEntity> 保存采样项目 = new KeyValuePair<string, BaseClassDicEntity>("100007", new BaseClassDicEntity() { Id = "100007", Name = "保存采样项目", Code = "BCCYXM", FontColor = "#ffffff", BGColor = "", Memo = "样本单-保存采样采样项目" });
        public static KeyValuePair<string, BaseClassDicEntity> 条码作废 = new KeyValuePair<string, BaseClassDicEntity>("100009", new BaseClassDicEntity() { Id = "100009", Name = "条码作废", Code = "TMZF", FontColor = "#ffffff", BGColor = "", Memo = "样本单-条码作废" });
        public static KeyValuePair<string, BaseClassDicEntity> 撤销采集 = new KeyValuePair<string, BaseClassDicEntity>("100028", new BaseClassDicEntity() { Id = "100028", Name = "撤销采集", Code = "CXCJ", FontColor = "#ffffff", BGColor = "", Memo = "样本单-撤销采集" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消签收 = new KeyValuePair<string, BaseClassDicEntity>("100029", new BaseClassDicEntity() { Id = "100029", Name = "取消签收", Code = "QXQS", FontColor = "#ffffff", BGColor = "", Memo = "样本单-取消签收" });
        public static KeyValuePair<string, BaseClassDicEntity> 撤销送检 = new KeyValuePair<string, BaseClassDicEntity>("100030", new BaseClassDicEntity() { Id = "100030", Name = "撤销送检", Code = "CXSJ", FontColor = "#ffffff", BGColor = "", Memo = "样本单-撤销送检" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消分发 = new KeyValuePair<string, BaseClassDicEntity>("100031", new BaseClassDicEntity() { Id = "100031", Name = "取消分发", Code = "QXFF", FontColor = "#ffffff", BGColor = "", Memo = "样本单-取消分发" });
        public static KeyValuePair<string, BaseClassDicEntity> 撤销确认 = new KeyValuePair<string, BaseClassDicEntity>("100032", new BaseClassDicEntity() { Id = "100032", Name = "撤销确认", Code = "CXQR", FontColor = "#ffffff", BGColor = "", Memo = "样本单-撤销确认" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 保存医嘱单.Key, 保存医嘱单.Value },
                { 修改医嘱单.Key, 修改医嘱单.Value },
                { 删除医嘱单.Key, 删除医嘱单.Value },
                { 审核医嘱单.Key, 审核医嘱单.Value },
                { 取消审核医嘱单.Key, 取消审核医嘱单.Value },
                { 保存采样样本单.Key, 保存采样样本单.Value },
                { 保存采样项目.Key, 保存采样项目.Value },
                { 条码作废.Key, 条码作废.Value },
                { 撤销采集.Key, 撤销采集.Value },
                { 取消签收.Key, 取消签收.Value },
                { 撤销送检.Key, 撤销送检.Value },
                { 取消分发.Key, 取消分发.Value },
                { 撤销确认.Key, 撤销确认.Value },
            };
            return dic;
        }
    }

    /// <summary>
    /// 业务流水号表业务类型
    /// </summary>
    public static class LBTGetMaxNOBmsTypes
    {
        public static KeyValuePair<string, BaseClassDicEntity> 医嘱单号流水号 = new KeyValuePair<string, BaseClassDicEntity>("100", new BaseClassDicEntity() { Id = "100", Name = "医嘱单号", Code = "OrderFormNoNum", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 条码号全局流水号 = new KeyValuePair<string, BaseClassDicEntity>("101", new BaseClassDicEntity() { Id = "101", Name = "条码号全局流水号", Code = "BarCodeGlobalNum", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 条码号当日流水号 = new KeyValuePair<string, BaseClassDicEntity>("102", new BaseClassDicEntity() { Id = "102", Name = "条码号当日流水号", Code = "BarCodeDayNum", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本采集打包号流水号 = new KeyValuePair<string, BaseClassDicEntity>("103", new BaseClassDicEntity() { Id = "103", Name = "样本采集打包号流水号", Code = "SampleGetherCollectPackNoNum", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本送检打包号流水号 = new KeyValuePair<string, BaseClassDicEntity>("104", new BaseClassDicEntity() { Id = "104", Name = "样本送检打包号流水号", Code = "SampleExchangeInspectCollectPackNoNum", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });

        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号1 = new KeyValuePair<string, BaseClassDicEntity>("105", new BaseClassDicEntity() { Id = "105", Name = "叫号系统排队号预留流水号1", Code = "A101", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号2 = new KeyValuePair<string, BaseClassDicEntity>("106", new BaseClassDicEntity() { Id = "106", Name = "叫号系统排队号预留流水号2", Code = "A102", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号3 = new KeyValuePair<string, BaseClassDicEntity>("107", new BaseClassDicEntity() { Id = "107", Name = "叫号系统排队号预留流水号3", Code = "A103", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号4 = new KeyValuePair<string, BaseClassDicEntity>("108", new BaseClassDicEntity() { Id = "108", Name = "叫号系统排队号预留流水号4", Code = "A104", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号5 = new KeyValuePair<string, BaseClassDicEntity>("109", new BaseClassDicEntity() { Id = "109", Name = "叫号系统排队号预留流水号5", Code = "A105", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号6 = new KeyValuePair<string, BaseClassDicEntity>("110", new BaseClassDicEntity() { Id = "110", Name = "叫号系统排队号预留流水号6", Code = "A106", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号7 = new KeyValuePair<string, BaseClassDicEntity>("111", new BaseClassDicEntity() { Id = "111", Name = "叫号系统排队号预留流水号7", Code = "A107", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号8 = new KeyValuePair<string, BaseClassDicEntity>("112", new BaseClassDicEntity() { Id = "112", Name = "叫号系统排队号预留流水号8", Code = "A108", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号9 = new KeyValuePair<string, BaseClassDicEntity>("113", new BaseClassDicEntity() { Id = "113", Name = "叫号系统排队号预留流水号9", Code = "A109", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号10 = new KeyValuePair<string, BaseClassDicEntity>("114", new BaseClassDicEntity() { Id = "114", Name = "叫号系统排队号预留流水号10", Code = "V110", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号11 = new KeyValuePair<string, BaseClassDicEntity>("115", new BaseClassDicEntity() { Id = "115", Name = "叫号系统排队号预留流水号11", Code = "V111", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号12 = new KeyValuePair<string, BaseClassDicEntity>("116", new BaseClassDicEntity() { Id = "116", Name = "叫号系统排队号预留流水号12", Code = "V112", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号13 = new KeyValuePair<string, BaseClassDicEntity>("117", new BaseClassDicEntity() { Id = "117", Name = "叫号系统排队号预留流水号13", Code = "V113", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号14 = new KeyValuePair<string, BaseClassDicEntity>("118", new BaseClassDicEntity() { Id = "118", Name = "叫号系统排队号预留流水号14", Code = "V114", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号15 = new KeyValuePair<string, BaseClassDicEntity>("119", new BaseClassDicEntity() { Id = "119", Name = "叫号系统排队号预留流水号15", Code = "V115", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号16 = new KeyValuePair<string, BaseClassDicEntity>("120", new BaseClassDicEntity() { Id = "120", Name = "叫号系统排队号预留流水号16", Code = "V116", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号17 = new KeyValuePair<string, BaseClassDicEntity>("121", new BaseClassDicEntity() { Id = "121", Name = "叫号系统排队号预留流水号17", Code = "V117", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号18 = new KeyValuePair<string, BaseClassDicEntity>("122", new BaseClassDicEntity() { Id = "122", Name = "叫号系统排队号预留流水号18", Code = "V118", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号19 = new KeyValuePair<string, BaseClassDicEntity>("123", new BaseClassDicEntity() { Id = "123", Name = "叫号系统排队号预留流水号19", Code = "V119", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });
        public static KeyValuePair<string, BaseClassDicEntity> 叫号系统排队号预留流水号20 = new KeyValuePair<string, BaseClassDicEntity>("124", new BaseClassDicEntity() { Id = "124", Name = "叫号系统排队号预留流水号20", Code = "V120", FontColor = "", BGColor = "", Memo = "数据库类型名称取当前Code" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 医嘱单号流水号.Key, 医嘱单号流水号.Value },
                { 条码号全局流水号.Key, 条码号全局流水号.Value },
                { 条码号当日流水号.Key, 条码号当日流水号.Value },
                { 样本采集打包号流水号.Key, 样本采集打包号流水号.Value },
                { 样本送检打包号流水号.Key, 样本送检打包号流水号.Value },
                { 叫号系统排队号预留流水号1.Key, 叫号系统排队号预留流水号1.Value },
                { 叫号系统排队号预留流水号2.Key, 叫号系统排队号预留流水号2.Value },
                { 叫号系统排队号预留流水号3.Key, 叫号系统排队号预留流水号3.Value },
                { 叫号系统排队号预留流水号4.Key, 叫号系统排队号预留流水号4.Value },
                { 叫号系统排队号预留流水号5.Key, 叫号系统排队号预留流水号5.Value },
                { 叫号系统排队号预留流水号6.Key, 叫号系统排队号预留流水号6.Value },
                { 叫号系统排队号预留流水号7.Key, 叫号系统排队号预留流水号7.Value },
                { 叫号系统排队号预留流水号8.Key, 叫号系统排队号预留流水号8.Value },
                { 叫号系统排队号预留流水号9.Key, 叫号系统排队号预留流水号9.Value },
                { 叫号系统排队号预留流水号10.Key, 叫号系统排队号预留流水号10.Value },
                { 叫号系统排队号预留流水号11.Key, 叫号系统排队号预留流水号11.Value },
                { 叫号系统排队号预留流水号12.Key, 叫号系统排队号预留流水号12.Value },
                { 叫号系统排队号预留流水号13.Key, 叫号系统排队号预留流水号13.Value },
                { 叫号系统排队号预留流水号14.Key, 叫号系统排队号预留流水号14.Value },
                { 叫号系统排队号预留流水号15.Key, 叫号系统排队号预留流水号15.Value },
                { 叫号系统排队号预留流水号16.Key, 叫号系统排队号预留流水号16.Value },
                { 叫号系统排队号预留流水号17.Key, 叫号系统排队号预留流水号17.Value },
                { 叫号系统排队号预留流水号18.Key, 叫号系统排队号预留流水号18.Value },
                { 叫号系统排队号预留流水号19.Key, 叫号系统排队号预留流水号19.Value },
                { 叫号系统排队号预留流水号20.Key, 叫号系统排队号预留流水号20.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 分管类型
    /// </summary>
    public static class SampleGroupingType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 分管成功 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "分管成功", Code = "GroupingSuccess", FontColor = "", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 无采样组 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "无采样组", Code = "NoSamplingGroup", FontColor = "", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 无默认采样组 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "无默认采样组", Code = "NoDefaultSamplingGroup", FontColor = "", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 不参与分管 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "不参与分管", Code = "NoParticipationSamplingGroup", FontColor = "", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 分管成功.Key, 分管成功.Value },
                { 无采样组.Key, 无采样组.Value },
                { 无默认采样组.Key, 无默认采样组.Value },
                { 不参与分管.Key, 不参与分管.Value },
            };
            return dic;
        }
    }
    /// <summary>
    /// 条码样本状态,以及样本操作类型
    /// </summary>
    public static class BarCodeStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 生成条码 = new KeyValuePair<string, BaseClassDicEntity>("100010", new BaseClassDicEntity() { Id = "100010", Name = "生成条码", Code = "1", DefaultValue="1",FontColor = "", BGColor = "", Memo = "样本单-生成条码" });
        public static KeyValuePair<string, BaseClassDicEntity> 条码确认 = new KeyValuePair<string, BaseClassDicEntity>("100008", new BaseClassDicEntity() { Id = "100008", Name = "条码确认", Code = "2", DefaultValue = "2", FontColor = "", BGColor = "", Memo = "样本单-条码确认" });
        public static KeyValuePair<string, BaseClassDicEntity> 条码打印 = new KeyValuePair<string, BaseClassDicEntity>("100011", new BaseClassDicEntity() { Id = "100011", Name = "条码打印", Code = "3", DefaultValue = "4",FontColor = "", BGColor = "", Memo = "样本单-条码打印" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本采集 = new KeyValuePair<string, BaseClassDicEntity>("100012", new BaseClassDicEntity() { Id = "100012", Name = "样本采集", Code = "4", DefaultValue = "8",FontColor = "", BGColor = "", Memo = "样本单-样本采集" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本送检 = new KeyValuePair<string, BaseClassDicEntity>("100013", new BaseClassDicEntity() { Id = "100013", Name = "样本送检", Code = "5", DefaultValue = "16",FontColor = "", BGColor = "", Memo = "样本单-样本送检" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本送达 = new KeyValuePair<string, BaseClassDicEntity>("100014", new BaseClassDicEntity() { Id = "100014", Name = "样本送达", Code = "6", DefaultValue = "32",FontColor = "", BGColor = "", Memo = "样本单-样本送达" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本签收 = new KeyValuePair<string, BaseClassDicEntity>("100015", new BaseClassDicEntity() { Id = "100015", Name = "样本签收", Code = "7", DefaultValue = "64", FontColor = "", BGColor = "", Memo = "样本单-样本签收" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本组内签收 = new KeyValuePair<string, BaseClassDicEntity>("100016", new BaseClassDicEntity() { Id = "100016", Name = "样本组内签收", Code = "8", DefaultValue = "128", FontColor = "", BGColor = "", Memo = "样本单-样本组内签收" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本拒收 = new KeyValuePair<string, BaseClassDicEntity>("100017", new BaseClassDicEntity() { Id = "100017", Name = "样本拒收", Code = "9", DefaultValue = "256", FontColor = "", BGColor = "", Memo = "样本单-样本拒收" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本让步 = new KeyValuePair<string, BaseClassDicEntity>("100018", new BaseClassDicEntity() { Id = "100018", Name = "样本让步", Code = "10", DefaultValue = "512", FontColor = "", BGColor = "", Memo = "样本单-样本让步" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本核收 = new KeyValuePair<string, BaseClassDicEntity>("100019", new BaseClassDicEntity() { Id = "100019", Name = "样本核收", Code = "11", DefaultValue = "1024", FontColor = "", BGColor = "", Memo = "样本单-样本核收" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本分发 = new KeyValuePair<string, BaseClassDicEntity>("100020", new BaseClassDicEntity() { Id = "100020", Name = "样本分发", Code = "12", DefaultValue = "2048", FontColor = "", BGColor = "", Memo = "样本单-样本分发" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告初审 = new KeyValuePair<string, BaseClassDicEntity>("100021", new BaseClassDicEntity() { Id = "100021", Name = "报告初审", Code = "13", DefaultValue = "4096", FontColor = "", BGColor = "", Memo = "样本单-报告初审" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告终审 = new KeyValuePair<string, BaseClassDicEntity>("100022", new BaseClassDicEntity() { Id = "100022", Name = "报告终审", Code = "14", DefaultValue = "8192", FontColor = "", BGColor = "", Memo = "样本单-报告终审" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告发布 = new KeyValuePair<string, BaseClassDicEntity>("100023", new BaseClassDicEntity() { Id = "100023", Name = "报告发布", Code = "15", DefaultValue = "116384", FontColor = "", BGColor = "", Memo = "样本单-报告发布" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告打印_技师站 = new KeyValuePair<string, BaseClassDicEntity>("100024", new BaseClassDicEntity() { Id = "100024", Name = "报告打印_技师站", Code = "16", DefaultValue = "32768", FontColor = "", BGColor = "", Memo = "样本单-报告打印_技师站" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告打印_自助站 = new KeyValuePair<string, BaseClassDicEntity>("100025", new BaseClassDicEntity() { Id = "100025", Name = "报告打印_自助站", Code = "17", DefaultValue = "65536", FontColor = "", BGColor = "", Memo = "样本单-报告打印_自助站" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告打印_临床站 = new KeyValuePair<string, BaseClassDicEntity>("100026", new BaseClassDicEntity() { Id = "100026", Name = "报告打印_临床站", Code = "18", DefaultValue = "131072", FontColor = "", BGColor = "", Memo = "样本单-报告打印_临床站" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验终止 = new KeyValuePair<string, BaseClassDicEntity>("100027", new BaseClassDicEntity() { Id = "100027", Name = "检验终止", Code = "19", DefaultValue = "262144", FontColor = "", BGColor = "", Memo = "样本单-检验终止" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 生成条码.Key, 生成条码.Value },
                { 条码确认.Key, 条码确认.Value },
                { 条码打印.Key, 条码打印.Value },
                { 样本采集.Key, 样本采集.Value },
                { 样本送检.Key, 样本送检.Value },
                { 样本送达.Key, 样本送达.Value },
                { 样本签收.Key, 样本签收.Value },
                { 样本组内签收.Key, 样本组内签收.Value },
                { 样本拒收.Key, 样本拒收.Value },
                { 样本让步.Key, 样本让步.Value },
                { 样本核收.Key, 样本核收.Value },
                { 样本分发.Key, 样本分发.Value },
                { 报告初审.Key, 报告初审.Value },
                { 报告终审.Key, 报告终审.Value },
                { 报告发布.Key, 报告发布.Value },
                { 报告打印_技师站.Key, 报告打印_技师站.Value },
                { 报告打印_自助站.Key, 报告打印_自助站.Value },
                { 报告打印_临床站.Key, 报告打印_临床站.Value },
                { 检验终止.Key, 检验终止.Value },
            };
            return dic;
        }
    }
}
