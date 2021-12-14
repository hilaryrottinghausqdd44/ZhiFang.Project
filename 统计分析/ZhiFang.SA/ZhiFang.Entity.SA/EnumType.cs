using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Policy;
using System.Collections;

namespace ZhiFang.Entity.SA
{
    public enum CodeValue
    {
        无 = 0,
        无法从Session中获取用户ID和名称 = 1001
    }
    public class BaseClassDicEntity
    {
        public string Id { get; set; }
        public string ParentID;
        public string Name { get; set; }
        public string Code { get; set; }
        public string FontColor { get; set; }
        public string BGColor { get; set; }
        public string SName { get; set; }
        public string DefaultValue { get; set; }
        public string DispOrder { get; set; }
        public string Memo { get; set; }
    }

    /// <summary>
    /// 用户UI配置的各UI类型
    /// </summary>
    public static class UserUIConfigUIType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 列表配置 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", DispOrder = "1", Name = "列表配置", Code = "DefaultGridPanel", FontColor = "#ffffff", BGColor = "#5cb85c" });//包括列表默认分页数,列配置,列排序
        public static KeyValuePair<string, BaseClassDicEntity> 列表默认分页数 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", DispOrder = "1", Name = "列表默认分页数", Code = "DefaultPageSize", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 列表列配置 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", DispOrder = "1", Name = "列表列配置", Code = "ColumnsConfig", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 列表列排序 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", DispOrder = "1", Name = "列表列排序", Code = "DefaultOrderBy", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(UserUIConfigUIType.列表配置.Key, UserUIConfigUIType.列表配置.Value);
            dic.Add(UserUIConfigUIType.列表默认分页数.Key, UserUIConfigUIType.列表默认分页数.Value);
            dic.Add(UserUIConfigUIType.列表列配置.Key, UserUIConfigUIType.列表列配置.Value);
            dic.Add(UserUIConfigUIType.列表列排序.Key, UserUIConfigUIType.列表列排序.Value);
            return dic;
        }
    }

    /// <summary>
    /// 统计结果分类
    /// </summary>
    public static class LStatTotalClassification
    {
        public static KeyValuePair<string, BaseClassDicEntity> 质量指标类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", DispOrder = "1", Name = "质量指标类型", Code = "QualityIndicatorType", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(LStatTotalClassification.质量指标类型.Key, LStatTotalClassification.质量指标类型.Value);
            return dic;
        }
    }
    /// <summary>
    /// 统计日期类型
    /// </summary>
    public static class LStatTotalStatDateType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 天 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", DispOrder = "1", Name = "天", Code = "Day", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 月 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", DispOrder = "2", Name = "月", Code = "Month", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 年 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", DispOrder = "4", Name = "年", Code = "Year", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 季度 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", DispOrder = "4", Name = "季度", Code = "Quarter", FontColor = "#ffffff", BGColor = "#1195db" });

        public static KeyValuePair<string, BaseClassDicEntity> 本月 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", DispOrder = "5", Name = "本月", Code = "CurMonth", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 本年 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", DispOrder = "6", Name = "本年", Code = "CurYear", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 日期范围 = new KeyValuePair<string, BaseClassDicEntity>("100", new BaseClassDicEntity() { Id = "100", DispOrder = "100", Name = "日期范围", Code = "DateRange", FontColor = "#ffffff", BGColor = "#8B4513" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(LStatTotalStatDateType.天.Key, LStatTotalStatDateType.天.Value);
            dic.Add(LStatTotalStatDateType.月.Key, LStatTotalStatDateType.月.Value);
            dic.Add(LStatTotalStatDateType.季度.Key, LStatTotalStatDateType.季度.Value);
            dic.Add(LStatTotalStatDateType.年.Key, LStatTotalStatDateType.年.Value);

            dic.Add(LStatTotalStatDateType.本月.Key, LStatTotalStatDateType.本月.Value);
            dic.Add(LStatTotalStatDateType.本年.Key, LStatTotalStatDateType.本年.Value);
            dic.Add(LStatTotalStatDateType.日期范围.Key, LStatTotalStatDateType.日期范围.Value);
            return dic;
        }
    }
    #region 质量指标
    /// <summary>
    /// 质量指标-质量指标分类类型
    /// </summary>
    public static class QualityIndicatorType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 标本类型错误率 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", DispOrder = "1", Name = "标本类型错误率", Code = "SampleTypeSADimension", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 标本容器错误率 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", DispOrder = "2", Name = "标本容器错误率", Code = "STContainerErrorSADimension", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 标本采集量错误率 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", DispOrder = "3", Name = "标本采集量错误率", Code = "STCollectionErrorSADimension", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 血培养污染统计 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", DispOrder = "4", Name = "血培养污染统计", Code = "BloodCulturePollutionSADimension", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 抗凝标本凝集 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", DispOrder = "5", Name = "抗凝标本凝集", Code = "ASpecimenAgglutinationSADimension", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 标本标识错误 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", DispOrder = "6", Name = "标本标识错误", Code = "SIdentificationErrorSADimension", FontColor = "#ffffff", BGColor = "#8B4513" });
        public static KeyValuePair<string, BaseClassDicEntity> 标本检验前储存不适当 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", DispOrder = "7", Name = "标本检验前储存不适当", Code = "SStorageIsErrorSADimension", FontColor = "#ffffff", BGColor = "#A0522D" });

        public static KeyValuePair<string, BaseClassDicEntity> 标本运输途中损坏 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", DispOrder = "8", Name = "标本运输途中损坏", Code = "STransportationDamageDuring", FontColor = "#ffffff", BGColor = "#FFA07A" });
        public static KeyValuePair<string, BaseClassDicEntity> 标本运输温度不适当 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", DispOrder = "9", Name = "标本运输温度不适当", Code = "STransportTemperatureImproperRate", FontColor = "#ffffff", BGColor = "#E9967A" });
        public static KeyValuePair<string, BaseClassDicEntity> 标本运输时间过长 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", DispOrder = "10", Name = "标本运输时间过长", Code = "SpecimenTransportTimeIsLong", FontColor = "#ffffff", BGColor = "#BC8F8F" });
        public static KeyValuePair<string, BaseClassDicEntity> 标本采集时机不正确 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", DispOrder = "11", Name = "标本采集时机不正确", Code = "SpecimenCollectionTimingErrorRate", FontColor = "#ffffff", BGColor = "#FF7F50" });
        public static KeyValuePair<string, BaseClassDicEntity> 微生物标本污染 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", DispOrder = "12", Name = "微生物标本污染", Code = "MicroSpecimenContamination", FontColor = "#ffffff", BGColor = "#CD5C5C" });
        public static KeyValuePair<string, BaseClassDicEntity> 其他类型 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", DispOrder = "13", Name = "其他类型", Code = "QIndicatorTypeOtherTypes", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 标本运输丢失 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", DispOrder = "14", Name = "标本运输丢失", Code = "SpecimenTransportLossRate", FontColor = "#ffffff", BGColor = "#FF4500" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(QualityIndicatorType.标本类型错误率.Key, QualityIndicatorType.标本类型错误率.Value);
            dic.Add(QualityIndicatorType.标本容器错误率.Key, QualityIndicatorType.标本容器错误率.Value);
            dic.Add(QualityIndicatorType.标本采集量错误率.Key, QualityIndicatorType.标本采集量错误率.Value);
            dic.Add(QualityIndicatorType.血培养污染统计.Key, QualityIndicatorType.血培养污染统计.Value);
            dic.Add(QualityIndicatorType.抗凝标本凝集.Key, QualityIndicatorType.抗凝标本凝集.Value);
            dic.Add(QualityIndicatorType.标本标识错误.Key, QualityIndicatorType.标本标识错误.Value);
            dic.Add(QualityIndicatorType.标本检验前储存不适当.Key, QualityIndicatorType.标本检验前储存不适当.Value);
            dic.Add(QualityIndicatorType.标本运输途中损坏.Key, QualityIndicatorType.标本运输途中损坏.Value);
            dic.Add(QualityIndicatorType.标本采集时机不正确.Key, QualityIndicatorType.标本采集时机不正确.Value);
            dic.Add(QualityIndicatorType.标本运输丢失.Key, QualityIndicatorType.标本运输丢失.Value);
            dic.Add(QualityIndicatorType.标本运输温度不适当.Key, QualityIndicatorType.标本运输温度不适当.Value);
            dic.Add(QualityIndicatorType.标本运输时间过长.Key, QualityIndicatorType.标本运输时间过长.Value);
            dic.Add(QualityIndicatorType.微生物标本污染.Key, QualityIndicatorType.微生物标本污染.Value);

            dic.Add(QualityIndicatorType.其他类型.Key, QualityIndicatorType.其他类型.Value);
            return dic;
        }
    }
    /// <summary>
    /// 1:质量指标-标本类型错误率统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class SampleTypeSADimension
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "1", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "11", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型医生 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "12", Name = "就诊类型医生", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室医生 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "4", DispOrder = "11", Name = "科室医生", Code = "DeptAndDoctor", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SampleTypeSADimension.就诊类型.Key, SampleTypeSADimension.就诊类型.Value);
            dic.Add(SampleTypeSADimension.就诊类型科室.Key, SampleTypeSADimension.就诊类型科室.Value);
            dic.Add(SampleTypeSADimension.就诊类型医生.Key, SampleTypeSADimension.就诊类型医生.Value);
            dic.Add(SampleTypeSADimension.科室.Key, SampleTypeSADimension.科室.Value);
            dic.Add(SampleTypeSADimension.科室医生.Key, SampleTypeSADimension.科室医生.Value);

            dic.Add(SampleTypeSADimension.按月份.Key, SampleTypeSADimension.按月份.Value);
            dic.Add(SampleTypeSADimension.按季度.Key, SampleTypeSADimension.按季度.Value);
            dic.Add(SampleTypeSADimension.按年份.Key, SampleTypeSADimension.按年份.Value);
            return dic;
        }
    }
    /// <summary>
    /// 2:质量指标-标本容器错误统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class STContainerErrorSADimension
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "1", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "2", Name = "就诊类型采样人", Code = "SickTypeAndSampler", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "2", Name = "就诊类型样本类型", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "1", DispOrder = "2", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#CD853F" });

        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "0", DispOrder = "1", Name = "样本类型", Code = "SampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型科室 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "4", DispOrder = "2", Name = "样本类型科室", Code = "SampleTypeAndDept", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "4", DispOrder = "2", Name = "样本类型采样人", Code = "SampleTypeAndSampler", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室样本类型 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", ParentID = "8", DispOrder = "2", Name = "科室样本类型", Code = "DeptAndSampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室采样人 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", ParentID = "8", DispOrder = "2", Name = "科室采样人", Code = "DeptAndCollecter", FontColor = "#ffffff", BGColor = "#F4A460" });

        public static KeyValuePair<string, BaseClassDicEntity> 采样人 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", ParentID = "0", DispOrder = "1", Name = "采样人", Code = "Collecter", FontColor = "#ffffff", BGColor = "#8B4513" });
        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(STContainerErrorSADimension.就诊类型.Key, STContainerErrorSADimension.就诊类型.Value);
            dic.Add(STContainerErrorSADimension.就诊类型采样人.Key, STContainerErrorSADimension.就诊类型采样人.Value);
            dic.Add(STContainerErrorSADimension.就诊类型样本类型.Key, STContainerErrorSADimension.就诊类型样本类型.Value);
            dic.Add(STContainerErrorSADimension.就诊类型科室.Key, STContainerErrorSADimension.就诊类型科室.Value);
            dic.Add(STContainerErrorSADimension.样本类型.Key, STContainerErrorSADimension.样本类型.Value);
            dic.Add(STContainerErrorSADimension.样本类型采样人.Key, STContainerErrorSADimension.样本类型采样人.Value);
            dic.Add(STContainerErrorSADimension.样本类型科室.Key, STContainerErrorSADimension.样本类型科室.Value);

            dic.Add(STContainerErrorSADimension.科室.Key, STContainerErrorSADimension.科室.Value);
            dic.Add(STContainerErrorSADimension.科室样本类型.Key, STContainerErrorSADimension.科室样本类型.Value);
            dic.Add(STContainerErrorSADimension.科室采样人.Key, STContainerErrorSADimension.科室采样人.Value);
            dic.Add(STContainerErrorSADimension.采样人.Key, STContainerErrorSADimension.采样人.Value);

            dic.Add(STContainerErrorSADimension.按月份.Key, STContainerErrorSADimension.按月份.Value);
            dic.Add(STContainerErrorSADimension.按季度.Key, STContainerErrorSADimension.按季度.Value);
            dic.Add(STContainerErrorSADimension.按年份.Key, STContainerErrorSADimension.按年份.Value);
            return dic;
        }
    }
    /// <summary>
    /// 3:质量指标-标本采集量错误统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class STCollectionErrorSADimension
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型采样人", Code = "SickTypeAndSampler", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型样本类型", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "1", DispOrder = "1", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#CD853F" });

        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "0", DispOrder = "0", Name = "样本类型", Code = "SampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型科室 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "6", DispOrder = "2", Name = "样本类型科室", Code = "SampleTypeAndDept", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "6", DispOrder = "2", Name = "样本类型采样人", Code = "SampleTypeAndSampler", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室样本类型 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", ParentID = "8", DispOrder = "2", Name = "科室样本类型", Code = "DeptAndSampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室采样人 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", ParentID = "8", DispOrder = "2", Name = "科室采样人", Code = "DeptAndCollecter", FontColor = "#ffffff", BGColor = "#F4A460" });

        public static KeyValuePair<string, BaseClassDicEntity> 采样人 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", ParentID = "0", DispOrder = "1", Name = "采样人", Code = "Collecter", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(STCollectionErrorSADimension.就诊类型.Key, STCollectionErrorSADimension.就诊类型.Value);
            dic.Add(STCollectionErrorSADimension.就诊类型采样人.Key, STCollectionErrorSADimension.就诊类型采样人.Value);
            dic.Add(STCollectionErrorSADimension.就诊类型样本类型.Key, STCollectionErrorSADimension.就诊类型样本类型.Value);
            dic.Add(STCollectionErrorSADimension.就诊类型科室.Key, STCollectionErrorSADimension.就诊类型科室.Value);

            dic.Add(STCollectionErrorSADimension.样本类型.Key, STCollectionErrorSADimension.样本类型.Value);
            dic.Add(STCollectionErrorSADimension.样本类型科室.Key, STCollectionErrorSADimension.样本类型科室.Value);
            dic.Add(STCollectionErrorSADimension.样本类型采样人.Key, STCollectionErrorSADimension.样本类型采样人.Value);

            dic.Add(STCollectionErrorSADimension.科室.Key, STCollectionErrorSADimension.科室.Value);
            dic.Add(STCollectionErrorSADimension.科室样本类型.Key, STCollectionErrorSADimension.科室样本类型.Value);
            dic.Add(STCollectionErrorSADimension.科室采样人.Key, STCollectionErrorSADimension.科室采样人.Value);
            dic.Add(STCollectionErrorSADimension.采样人.Key, STCollectionErrorSADimension.采样人.Value);

            dic.Add(STCollectionErrorSADimension.按月份.Key, STCollectionErrorSADimension.按月份.Value);
            dic.Add(STCollectionErrorSADimension.按季度.Key, STCollectionErrorSADimension.按季度.Value);
            dic.Add(STCollectionErrorSADimension.按年份.Key, STCollectionErrorSADimension.按年份.Value);
            return dic;
        }
    }
    /// <summary>
    /// 4:质量指标-血培养污染统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class BloodCulturePollutionSADimension
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型采样人", Code = "SickTypeAndCollecter", FontColor = "#ffffff", BGColor = "#DEB887" });

        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室采样人 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "4", DispOrder = "2", Name = "科室采样人", Code = "DeptAndCollecter", FontColor = "#ffffff", BGColor = "#F4A460" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BloodCulturePollutionSADimension.就诊类型.Key, BloodCulturePollutionSADimension.就诊类型.Value);
            dic.Add(BloodCulturePollutionSADimension.就诊类型科室.Key, BloodCulturePollutionSADimension.就诊类型科室.Value);
            dic.Add(BloodCulturePollutionSADimension.就诊类型采样人.Key, BloodCulturePollutionSADimension.就诊类型采样人.Value);

            dic.Add(BloodCulturePollutionSADimension.科室.Key, BloodCulturePollutionSADimension.科室.Value);
            dic.Add(BloodCulturePollutionSADimension.科室采样人.Key, BloodCulturePollutionSADimension.科室采样人.Value);
            dic.Add(BloodCulturePollutionSADimension.按月份.Key, BloodCulturePollutionSADimension.按月份.Value);
            dic.Add(BloodCulturePollutionSADimension.按季度.Key, BloodCulturePollutionSADimension.按季度.Value);
            dic.Add(BloodCulturePollutionSADimension.按年份.Key, BloodCulturePollutionSADimension.按年份.Value);
            return dic;
        }
    }
    /// <summary>
    /// 5:质量指标-抗凝标本凝集统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class ASpecimenAgglutinationSADimension
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型采样人", Code = "SickTypeAndCollecter", FontColor = "#ffffff", BGColor = "#DEB887" });

        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室采样人 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "4", DispOrder = "2", Name = "科室采样人", Code = "DeptAndCollecter", FontColor = "#ffffff", BGColor = "#F4A460" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ASpecimenAgglutinationSADimension.就诊类型.Key, ASpecimenAgglutinationSADimension.就诊类型.Value);
            dic.Add(ASpecimenAgglutinationSADimension.就诊类型科室.Key, ASpecimenAgglutinationSADimension.就诊类型科室.Value);
            dic.Add(ASpecimenAgglutinationSADimension.就诊类型采样人.Key, ASpecimenAgglutinationSADimension.就诊类型采样人.Value);

            dic.Add(ASpecimenAgglutinationSADimension.科室.Key, ASpecimenAgglutinationSADimension.科室.Value);
            dic.Add(ASpecimenAgglutinationSADimension.科室采样人.Key, ASpecimenAgglutinationSADimension.科室采样人.Value);
            dic.Add(ASpecimenAgglutinationSADimension.按月份.Key, ASpecimenAgglutinationSADimension.按月份.Value);
            dic.Add(ASpecimenAgglutinationSADimension.按季度.Key, ASpecimenAgglutinationSADimension.按季度.Value);
            dic.Add(ASpecimenAgglutinationSADimension.按年份.Key, ASpecimenAgglutinationSADimension.按年份.Value);
            return dic;
        }
    }
    /// <summary>
    /// 6:质量指标-标本标识错误统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class SIdentificationErrorSADimension
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型采样人", Code = "SickTypeAndCollecter", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型样本类型", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "1", DispOrder = "1", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#CD853F" });

        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "0", DispOrder = "0", Name = "样本类型", Code = "SampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型科室 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "5", DispOrder = "2", Name = "样本类型科室", Code = "SampleTypeAndDept", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "5", DispOrder = "1", Name = "样本类型采样人", Code = "SampleTypeAndCollecter", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室样本类型 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", ParentID = "8", DispOrder = "2", Name = "科室样本类型", Code = "DeptAndSampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室采样人 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", ParentID = "8", DispOrder = "2", Name = "科室采样人", Code = "DeptAndCollecter", FontColor = "#ffffff", BGColor = "#F4A460" });

        public static KeyValuePair<string, BaseClassDicEntity> 采样人 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", ParentID = "0", DispOrder = "1", Name = "采样人", Code = "Collecter", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SIdentificationErrorSADimension.就诊类型.Key, SIdentificationErrorSADimension.就诊类型.Value);
            dic.Add(SIdentificationErrorSADimension.就诊类型采样人.Key, SIdentificationErrorSADimension.就诊类型采样人.Value);
            dic.Add(SIdentificationErrorSADimension.就诊类型样本类型.Key, SIdentificationErrorSADimension.就诊类型样本类型.Value);
            dic.Add(SIdentificationErrorSADimension.就诊类型科室.Key, SIdentificationErrorSADimension.就诊类型科室.Value);

            dic.Add(SIdentificationErrorSADimension.样本类型.Key, SIdentificationErrorSADimension.样本类型.Value);
            dic.Add(SIdentificationErrorSADimension.样本类型采样人.Key, SIdentificationErrorSADimension.样本类型采样人.Value);
            dic.Add(SIdentificationErrorSADimension.样本类型科室.Key, SIdentificationErrorSADimension.样本类型科室.Value);

            dic.Add(SIdentificationErrorSADimension.科室.Key, SIdentificationErrorSADimension.科室.Value);
            dic.Add(SIdentificationErrorSADimension.科室样本类型.Key, SIdentificationErrorSADimension.科室样本类型.Value);
            dic.Add(SIdentificationErrorSADimension.科室采样人.Key, SIdentificationErrorSADimension.科室采样人.Value);
            dic.Add(SIdentificationErrorSADimension.采样人.Key, SIdentificationErrorSADimension.采样人.Value);

            dic.Add(SIdentificationErrorSADimension.按月份.Key, SIdentificationErrorSADimension.按月份.Value);
            dic.Add(SIdentificationErrorSADimension.按季度.Key, SIdentificationErrorSADimension.按季度.Value);
            dic.Add(SIdentificationErrorSADimension.按年份.Key, SIdentificationErrorSADimension.按年份.Value);
            return dic;
        }
    }
    /// <summary>
    ///7:质量指标-标本检验前储存不适当统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class SStorageIsErrorSADimension
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型采样人", Code = "SickTypeAndCollecter", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型样本类型", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "1", DispOrder = "1", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#CD853F" });

        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "0", DispOrder = "0", Name = "样本类型", Code = "SampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型科室 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "5", DispOrder = "2", Name = "样本类型科室", Code = "SampleTypeAndDept", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "5", DispOrder = "1", Name = "样本类型采样人", Code = "SampleTypeAndCollecter", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室样本类型 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", ParentID = "8", DispOrder = "2", Name = "科室样本类型", Code = "DeptAndSampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室采样人 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", ParentID = "8", DispOrder = "2", Name = "科室采样人", Code = "DeptAndCollecter", FontColor = "#ffffff", BGColor = "#F4A460" });

        public static KeyValuePair<string, BaseClassDicEntity> 采样人 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", ParentID = "0", DispOrder = "1", Name = "采样人", Code = "Collecter", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SStorageIsErrorSADimension.就诊类型.Key, SStorageIsErrorSADimension.就诊类型.Value);
            dic.Add(SStorageIsErrorSADimension.就诊类型采样人.Key, SStorageIsErrorSADimension.就诊类型采样人.Value);
            dic.Add(SStorageIsErrorSADimension.就诊类型样本类型.Key, SStorageIsErrorSADimension.就诊类型样本类型.Value);
            dic.Add(SStorageIsErrorSADimension.就诊类型科室.Key, SStorageIsErrorSADimension.就诊类型科室.Value);

            dic.Add(SStorageIsErrorSADimension.样本类型.Key, SStorageIsErrorSADimension.样本类型.Value);
            dic.Add(SStorageIsErrorSADimension.样本类型科室.Key, SStorageIsErrorSADimension.样本类型科室.Value);
            dic.Add(SStorageIsErrorSADimension.样本类型采样人.Key, SStorageIsErrorSADimension.样本类型采样人.Value);

            dic.Add(SStorageIsErrorSADimension.科室.Key, SStorageIsErrorSADimension.科室.Value);
            dic.Add(SStorageIsErrorSADimension.科室样本类型.Key, SStorageIsErrorSADimension.科室样本类型.Value);
            dic.Add(SStorageIsErrorSADimension.科室采样人.Key, SStorageIsErrorSADimension.科室采样人.Value);
            dic.Add(SStorageIsErrorSADimension.采样人.Key, SStorageIsErrorSADimension.采样人.Value);

            dic.Add(SStorageIsErrorSADimension.按月份.Key, SStorageIsErrorSADimension.按月份.Value);
            dic.Add(SStorageIsErrorSADimension.按季度.Key, SStorageIsErrorSADimension.按季度.Value);
            dic.Add(SStorageIsErrorSADimension.按年份.Key, SStorageIsErrorSADimension.按年份.Value);
            return dic;
        }

    }
    /// <summary>
    /// 8:质量指标-标本运输途中损坏统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class STransportationDamageDuring
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型送检人 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型送检人", Code = "SickTypeAndInspector", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型样本类型", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "1", DispOrder = "1", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#CD853F" });

        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "0", DispOrder = "0", Name = "样本类型", Code = "SampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型科室 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "5", DispOrder = "2", Name = "样本类型科室", Code = "SampleTypeAndDept", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型送检人 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "5", DispOrder = "1", Name = "样本类型送检人", Code = "SampleTypeAndInspector", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室样本类型 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", ParentID = "8", DispOrder = "2", Name = "科室样本类型", Code = "DeptAndSampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室送检人 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", ParentID = "8", DispOrder = "2", Name = "科室送检人", Code = "DeptAndInspector", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 送检人 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", ParentID = "8", DispOrder = "1", Name = "送检人", Code = "Inspector", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 采样人 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", ParentID = "0", DispOrder = "1", Name = "采样人", Code = "Collecter", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("15", new BaseClassDicEntity() { Id = "15", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(STransportationDamageDuring.就诊类型.Key, STransportationDamageDuring.就诊类型.Value);
            dic.Add(STransportationDamageDuring.就诊类型送检人.Key, STransportationDamageDuring.就诊类型送检人.Value);
            dic.Add(STransportationDamageDuring.就诊类型样本类型.Key, STransportationDamageDuring.就诊类型样本类型.Value);
            dic.Add(STransportationDamageDuring.就诊类型科室.Key, STransportationDamageDuring.就诊类型科室.Value);

            dic.Add(STransportationDamageDuring.样本类型.Key, STransportationDamageDuring.样本类型.Value);
            dic.Add(STransportationDamageDuring.样本类型科室.Key, STransportationDamageDuring.样本类型科室.Value);
            dic.Add(STransportationDamageDuring.样本类型送检人.Key, STransportationDamageDuring.样本类型送检人.Value);

            dic.Add(STransportationDamageDuring.科室.Key, STransportationDamageDuring.科室.Value);
            dic.Add(STransportationDamageDuring.科室样本类型.Key, STransportationDamageDuring.科室样本类型.Value);
            dic.Add(STransportationDamageDuring.科室送检人.Key, STransportationDamageDuring.科室送检人.Value);
            dic.Add(STransportationDamageDuring.送检人.Key, STransportationDamageDuring.送检人.Value);

            dic.Add(STransportationDamageDuring.采样人.Key, STransportationDamageDuring.采样人.Value);
            dic.Add(STransportationDamageDuring.按月份.Key, STransportationDamageDuring.按月份.Value);
            dic.Add(STransportationDamageDuring.按季度.Key, STransportationDamageDuring.按季度.Value);
            dic.Add(STransportationDamageDuring.按年份.Key, STransportationDamageDuring.按年份.Value);

            return dic;
        }
    }
    /// <summary>
    /// 9:质量指标-标本运输温度不当率统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class STransportTemperatureImproperRate
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型送检人 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型送检人", Code = "SickTypeAndInspector", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型样本类型", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "1", DispOrder = "1", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#CD853F" });

        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "0", DispOrder = "0", Name = "样本类型", Code = "SampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型科室 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "5", DispOrder = "2", Name = "样本类型科室", Code = "SampleTypeAndDept", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型送检人 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "5", DispOrder = "1", Name = "样本类型送检人", Code = "SampleTypeAndInspector", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室样本类型 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", ParentID = "8", DispOrder = "2", Name = "科室样本类型", Code = "DeptAndSampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室送检人 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", ParentID = "8", DispOrder = "2", Name = "科室送检人", Code = "DeptAndInspector", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 送检人 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", ParentID = "8", DispOrder = "1", Name = "送检人", Code = "Inspector", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 采样人 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", ParentID = "0", DispOrder = "1", Name = "采样人", Code = "Sampler", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("15", new BaseClassDicEntity() { Id = "15", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(STransportTemperatureImproperRate.就诊类型.Key, STransportTemperatureImproperRate.就诊类型.Value);
            dic.Add(STransportTemperatureImproperRate.就诊类型送检人.Key, STransportTemperatureImproperRate.就诊类型送检人.Value);
            dic.Add(STransportTemperatureImproperRate.就诊类型样本类型.Key, STransportTemperatureImproperRate.就诊类型样本类型.Value);
            dic.Add(STransportTemperatureImproperRate.就诊类型科室.Key, STransportTemperatureImproperRate.就诊类型科室.Value);

            dic.Add(STransportTemperatureImproperRate.样本类型.Key, STransportTemperatureImproperRate.样本类型.Value);
            dic.Add(STransportTemperatureImproperRate.样本类型科室.Key, STransportTemperatureImproperRate.样本类型科室.Value);
            dic.Add(STransportTemperatureImproperRate.样本类型送检人.Key, STransportTemperatureImproperRate.样本类型送检人.Value);

            dic.Add(STransportTemperatureImproperRate.科室.Key, STransportTemperatureImproperRate.科室.Value);
            dic.Add(STransportTemperatureImproperRate.科室样本类型.Key, STransportTemperatureImproperRate.科室样本类型.Value);
            dic.Add(STransportTemperatureImproperRate.科室送检人.Key, STransportTemperatureImproperRate.科室送检人.Value);
            dic.Add(STransportTemperatureImproperRate.送检人.Key, STransportTemperatureImproperRate.送检人.Value);

            dic.Add(STransportTemperatureImproperRate.采样人.Key, STransportTemperatureImproperRate.采样人.Value);
            dic.Add(STransportTemperatureImproperRate.按月份.Key, STransportTemperatureImproperRate.按月份.Value);
            dic.Add(STransportTemperatureImproperRate.按季度.Key, STransportTemperatureImproperRate.按季度.Value);
            dic.Add(STransportTemperatureImproperRate.按年份.Key, STransportTemperatureImproperRate.按年份.Value);
            return dic;
        }
    }
    /// <summary>
    /// 10:质量指标-标本运输时间过长统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class SpecimenTransportTimeIsLong
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型送检人 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型送检人", Code = "SickTypeAndInspector", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型样本类型", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "1", DispOrder = "1", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#CD853F" });

        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "0", DispOrder = "0", Name = "样本类型", Code = "SampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型科室 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "5", DispOrder = "2", Name = "样本类型科室", Code = "SampleTypeAndDept", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型送检人 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "5", DispOrder = "1", Name = "样本类型送检人", Code = "SampleTypeAndInspector", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室样本类型 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", ParentID = "8", DispOrder = "2", Name = "科室样本类型", Code = "DeptAndSampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室送检人 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", ParentID = "8", DispOrder = "2", Name = "科室送检人", Code = "DeptAndInspector", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 送检人 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", ParentID = "8", DispOrder = "1", Name = "送检人", Code = "Inspector", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 采样人 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", ParentID = "0", DispOrder = "1", Name = "采样人", Code = "Collecter", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("15", new BaseClassDicEntity() { Id = "15", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SpecimenTransportTimeIsLong.就诊类型.Key, SpecimenTransportTimeIsLong.就诊类型.Value);
            dic.Add(SpecimenTransportTimeIsLong.就诊类型送检人.Key, SpecimenTransportTimeIsLong.就诊类型送检人.Value);
            dic.Add(SpecimenTransportTimeIsLong.就诊类型样本类型.Key, SpecimenTransportTimeIsLong.就诊类型样本类型.Value);
            dic.Add(SpecimenTransportTimeIsLong.就诊类型科室.Key, SpecimenTransportTimeIsLong.就诊类型科室.Value);

            dic.Add(SpecimenTransportTimeIsLong.样本类型.Key, SpecimenTransportTimeIsLong.样本类型.Value);
            dic.Add(SpecimenTransportTimeIsLong.样本类型科室.Key, SpecimenTransportTimeIsLong.样本类型科室.Value);
            dic.Add(SpecimenTransportTimeIsLong.样本类型送检人.Key, SpecimenTransportTimeIsLong.样本类型送检人.Value);

            dic.Add(SpecimenTransportTimeIsLong.科室.Key, SpecimenTransportTimeIsLong.科室.Value);
            dic.Add(SpecimenTransportTimeIsLong.科室样本类型.Key, SpecimenTransportTimeIsLong.科室样本类型.Value);
            dic.Add(SpecimenTransportTimeIsLong.科室送检人.Key, SpecimenTransportTimeIsLong.科室送检人.Value);
            dic.Add(SpecimenTransportTimeIsLong.送检人.Key, SpecimenTransportTimeIsLong.送检人.Value);

            dic.Add(SpecimenTransportTimeIsLong.采样人.Key, SpecimenTransportTimeIsLong.采样人.Value);
            dic.Add(SpecimenTransportTimeIsLong.按月份.Key, SpecimenTransportTimeIsLong.按月份.Value);
            dic.Add(SpecimenTransportTimeIsLong.按季度.Key, SpecimenTransportTimeIsLong.按季度.Value);
            dic.Add(SpecimenTransportTimeIsLong.按年份.Key, SpecimenTransportTimeIsLong.按年份.Value);
            return dic;
        }
    }
    /// <summary>
    /// 11:质量指标-标本采集时机不正确率统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class SpecimenCollectionTimingErrorRate
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型采样人", Code = "SickTypeAndCollecter", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型样本类型", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "1", DispOrder = "1", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#CD853F" });

        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "0", DispOrder = "0", Name = "样本类型", Code = "SampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型科室 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "5", DispOrder = "2", Name = "样本类型科室", Code = "SampleTypeAndDept", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "5", DispOrder = "1", Name = "样本类型采样人", Code = "SampleTypeAndCollecter", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室样本类型 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", ParentID = "8", DispOrder = "2", Name = "科室样本类型", Code = "DeptAndSampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室采样人 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", ParentID = "8", DispOrder = "2", Name = "科室采样人", Code = "DeptAndCollecter", FontColor = "#ffffff", BGColor = "#F4A460" });

        public static KeyValuePair<string, BaseClassDicEntity> 采样人 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", ParentID = "0", DispOrder = "1", Name = "采样人", Code = "Collecter", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SpecimenCollectionTimingErrorRate.就诊类型.Key, SpecimenCollectionTimingErrorRate.就诊类型.Value);
            dic.Add(SpecimenCollectionTimingErrorRate.就诊类型采样人.Key, SpecimenCollectionTimingErrorRate.就诊类型采样人.Value);
            dic.Add(SpecimenCollectionTimingErrorRate.就诊类型样本类型.Key, SpecimenCollectionTimingErrorRate.就诊类型样本类型.Value);
            dic.Add(SpecimenCollectionTimingErrorRate.就诊类型科室.Key, SpecimenCollectionTimingErrorRate.就诊类型科室.Value);

            dic.Add(SpecimenCollectionTimingErrorRate.样本类型.Key, SpecimenCollectionTimingErrorRate.样本类型.Value);
            dic.Add(SpecimenCollectionTimingErrorRate.样本类型科室.Key, SpecimenCollectionTimingErrorRate.样本类型科室.Value);
            dic.Add(SpecimenCollectionTimingErrorRate.样本类型采样人.Key, SpecimenCollectionTimingErrorRate.样本类型采样人.Value);

            dic.Add(SpecimenCollectionTimingErrorRate.科室.Key, SpecimenCollectionTimingErrorRate.科室.Value);
            dic.Add(SpecimenCollectionTimingErrorRate.科室样本类型.Key, SpecimenCollectionTimingErrorRate.科室样本类型.Value);
            dic.Add(SpecimenCollectionTimingErrorRate.科室采样人.Key, SpecimenCollectionTimingErrorRate.科室采样人.Value);
            dic.Add(SpecimenCollectionTimingErrorRate.采样人.Key, SpecimenCollectionTimingErrorRate.采样人.Value);

            dic.Add(SpecimenCollectionTimingErrorRate.按月份.Key, SpecimenCollectionTimingErrorRate.按月份.Value);
            dic.Add(SpecimenCollectionTimingErrorRate.按季度.Key, SpecimenCollectionTimingErrorRate.按季度.Value);
            dic.Add(SpecimenCollectionTimingErrorRate.按年份.Key, SpecimenCollectionTimingErrorRate.按年份.Value);
            return dic;
        }
    }
    /// <summary>
    /// 12:质量指标-微生物标本污染统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class MicroSpecimenContamination
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型采样人", Code = "SickTypeAndCollecter", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型样本类型", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "1", DispOrder = "1", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#CD853F" });

        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "0", DispOrder = "0", Name = "样本类型", Code = "SampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型科室 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "5", DispOrder = "2", Name = "样本类型科室", Code = "SampleTypeAndDept", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "5", DispOrder = "1", Name = "样本类型采样人", Code = "SampleTypeAndCollecter", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室样本类型 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", ParentID = "8", DispOrder = "2", Name = "科室样本类型", Code = "DeptAndSampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室采样人 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", ParentID = "8", DispOrder = "2", Name = "科室采样人", Code = "DeptAndCollecter", FontColor = "#ffffff", BGColor = "#F4A460" });

        public static KeyValuePair<string, BaseClassDicEntity> 采样人 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", ParentID = "0", DispOrder = "1", Name = "采样人", Code = "Collecter", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(MicroSpecimenContamination.就诊类型.Key, MicroSpecimenContamination.就诊类型.Value);
            dic.Add(MicroSpecimenContamination.就诊类型采样人.Key, MicroSpecimenContamination.就诊类型采样人.Value);
            dic.Add(MicroSpecimenContamination.就诊类型样本类型.Key, MicroSpecimenContamination.就诊类型样本类型.Value);
            dic.Add(MicroSpecimenContamination.就诊类型科室.Key, MicroSpecimenContamination.就诊类型科室.Value);

            dic.Add(MicroSpecimenContamination.样本类型.Key, MicroSpecimenContamination.样本类型.Value);
            dic.Add(MicroSpecimenContamination.样本类型科室.Key, MicroSpecimenContamination.样本类型科室.Value);
            dic.Add(MicroSpecimenContamination.样本类型采样人.Key, MicroSpecimenContamination.样本类型采样人.Value);

            dic.Add(MicroSpecimenContamination.科室.Key, MicroSpecimenContamination.科室.Value);
            dic.Add(MicroSpecimenContamination.科室样本类型.Key, MicroSpecimenContamination.科室样本类型.Value);
            dic.Add(MicroSpecimenContamination.科室采样人.Key, MicroSpecimenContamination.科室采样人.Value);
            dic.Add(MicroSpecimenContamination.采样人.Key, MicroSpecimenContamination.采样人.Value);

            dic.Add(MicroSpecimenContamination.按月份.Key, MicroSpecimenContamination.按月份.Value);
            dic.Add(MicroSpecimenContamination.按季度.Key, MicroSpecimenContamination.按季度.Value);
            dic.Add(MicroSpecimenContamination.按年份.Key, MicroSpecimenContamination.按年份.Value);
            return dic;
        }
    }
    /// <summary>
    /// 13:质量指标-其他类型统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class QIndicatorTypeOtherTypes
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型采样人", Code = "SickTypeAndCollecter", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型样本类型", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "1", DispOrder = "1", Name = "就诊类型科室", Code = "SickTypeAndDept", FontColor = "#ffffff", BGColor = "#CD853F" });

        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "0", DispOrder = "0", Name = "样本类型", Code = "SampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型科室 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "5", DispOrder = "2", Name = "样本类型科室", Code = "SampleTypeAndDept", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "5", DispOrder = "1", Name = "样本类型采样人", Code = "SampleTypeAndCollecter", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 科室 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "科室", Code = "Dept", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室样本类型 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", ParentID = "8", DispOrder = "2", Name = "科室样本类型", Code = "DeptAndSampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室采样人 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", ParentID = "8", DispOrder = "2", Name = "科室采样人", Code = "DeptAndCollecter", FontColor = "#ffffff", BGColor = "#F4A460" });

        public static KeyValuePair<string, BaseClassDicEntity> 采样人 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", ParentID = "0", DispOrder = "1", Name = "采样人", Code = "Collecter", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(QIndicatorTypeOtherTypes.就诊类型.Key, QIndicatorTypeOtherTypes.就诊类型.Value);
            dic.Add(QIndicatorTypeOtherTypes.就诊类型采样人.Key, QIndicatorTypeOtherTypes.就诊类型采样人.Value);
            dic.Add(QIndicatorTypeOtherTypes.就诊类型样本类型.Key, QIndicatorTypeOtherTypes.就诊类型样本类型.Value);
            dic.Add(QIndicatorTypeOtherTypes.就诊类型科室.Key, QIndicatorTypeOtherTypes.就诊类型科室.Value);

            dic.Add(QIndicatorTypeOtherTypes.样本类型.Key, QIndicatorTypeOtherTypes.样本类型.Value);
            dic.Add(QIndicatorTypeOtherTypes.样本类型科室.Key, QIndicatorTypeOtherTypes.样本类型科室.Value);
            dic.Add(QIndicatorTypeOtherTypes.样本类型采样人.Key, QIndicatorTypeOtherTypes.样本类型采样人.Value);

            dic.Add(QIndicatorTypeOtherTypes.科室.Key, QIndicatorTypeOtherTypes.科室.Value);
            dic.Add(QIndicatorTypeOtherTypes.科室样本类型.Key, QIndicatorTypeOtherTypes.科室样本类型.Value);
            dic.Add(QIndicatorTypeOtherTypes.科室采样人.Key, QIndicatorTypeOtherTypes.科室采样人.Value);
            dic.Add(QIndicatorTypeOtherTypes.采样人.Key, QIndicatorTypeOtherTypes.采样人.Value);

            dic.Add(QIndicatorTypeOtherTypes.按月份.Key, QIndicatorTypeOtherTypes.按月份.Value);
            dic.Add(QIndicatorTypeOtherTypes.按季度.Key, QIndicatorTypeOtherTypes.按季度.Value);
            dic.Add(QIndicatorTypeOtherTypes.按年份.Key, QIndicatorTypeOtherTypes.按年份.Value);
            return dic;
        }
    }
    /// <summary>
    /// 质量指标-标本标签不合格率统计维度(不用)
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class SpecimenLabelFailureRate
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型采样人", Code = "SickTypeAndCollecter", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型样本类型", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });

        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "0", DispOrder = "0", Name = "样本类型", Code = "SampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "4", DispOrder = "1", Name = "样本类型采样人", Code = "SampleTypeAndCollecter", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 采样人 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "0", DispOrder = "1", Name = "采样人", Code = "Collecter", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SpecimenLabelFailureRate.就诊类型.Key, SpecimenLabelFailureRate.就诊类型.Value);
            dic.Add(SpecimenLabelFailureRate.就诊类型采样人.Key, SpecimenLabelFailureRate.就诊类型采样人.Value);
            dic.Add(SpecimenLabelFailureRate.就诊类型样本类型.Key, SpecimenLabelFailureRate.就诊类型样本类型.Value);
            dic.Add(SpecimenLabelFailureRate.样本类型.Key, SpecimenLabelFailureRate.样本类型.Value);
            dic.Add(SpecimenLabelFailureRate.样本类型采样人.Key, SpecimenLabelFailureRate.样本类型采样人.Value);

            dic.Add(SpecimenLabelFailureRate.采样人.Key, SpecimenLabelFailureRate.采样人.Value);
            dic.Add(SpecimenLabelFailureRate.按月份.Key, SpecimenLabelFailureRate.按月份.Value);
            dic.Add(SpecimenLabelFailureRate.按季度.Key, SpecimenLabelFailureRate.按季度.Value);
            dic.Add(SpecimenLabelFailureRate.按年份.Key, SpecimenLabelFailureRate.按年份.Value);
            return dic;
        }
    }

    /// <summary>
    /// 质量指标-标本运输丢失率统计维度
    /// DispOrder:为统计维度钻取级别
    /// </summary>
    public static class SpecimenTransportLossRate
    {
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "0", Name = "就诊类型", Code = "SickType", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", ParentID = "1", DispOrder = "1", Name = "就诊类型采样人", Code = "SickTypeAndCollecter", FontColor = "#ffffff", BGColor = "#DEB887" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", ParentID = "1", DispOrder = "1", Name = "就诊类型样本类型", Code = "SickTypeAndDoctor", FontColor = "#ffffff", BGColor = "#CD853F" });

        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", ParentID = "0", DispOrder = "0", Name = "样本类型", Code = "SampleType", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型采样人 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", ParentID = "4", DispOrder = "1", Name = "样本类型采样人", Code = "SampleTypeAndCollecter", FontColor = "#ffffff", BGColor = "#D2691E" });

        public static KeyValuePair<string, BaseClassDicEntity> 采样人 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", ParentID = "0", DispOrder = "1", Name = "采样人", Code = "Collecter", FontColor = "#ffffff", BGColor = "#8B4513" });

        public static KeyValuePair<string, BaseClassDicEntity> 按月份 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", ParentID = "0", DispOrder = "1", Name = "按月份", Code = "ByMonth", FontColor = "#ffffff", BGColor = "#D8BFD8" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "1", Name = "按季度", Code = "ByQuarter", FontColor = "#ffffff", BGColor = "#DB7093" });
        public static KeyValuePair<string, BaseClassDicEntity> 按年份 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", ParentID = "0", DispOrder = "1", Name = "按年份", Code = "ByYear", FontColor = "#ffffff", BGColor = "#9370DB" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SpecimenTransportLossRate.就诊类型.Key, SpecimenTransportLossRate.就诊类型.Value);
            dic.Add(SpecimenTransportLossRate.就诊类型采样人.Key, SpecimenTransportLossRate.就诊类型采样人.Value);
            dic.Add(SpecimenTransportLossRate.就诊类型样本类型.Key, SpecimenTransportLossRate.就诊类型样本类型.Value);
            dic.Add(SpecimenTransportLossRate.样本类型.Key, SpecimenTransportLossRate.样本类型.Value);
            dic.Add(SpecimenTransportLossRate.样本类型采样人.Key, SpecimenTransportLossRate.样本类型采样人.Value);

            dic.Add(SpecimenTransportLossRate.采样人.Key, SpecimenTransportLossRate.采样人.Value);
            dic.Add(SpecimenTransportLossRate.按月份.Key, SpecimenTransportLossRate.按月份.Value);
            dic.Add(SpecimenTransportLossRate.按季度.Key, SpecimenTransportLossRate.按季度.Value);
            dic.Add(SpecimenTransportLossRate.按年份.Key, SpecimenTransportLossRate.按年份.Value);
            return dic;
        }
    }

    #endregion

}