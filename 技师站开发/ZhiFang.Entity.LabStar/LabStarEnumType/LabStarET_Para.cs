using System.Collections.Generic;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    //此文件定义参数相关字典类

    /// <summary>
    /// 系统相关性
    /// </summary>
    public static class Para_SystemType
    {

        public static KeyValuePair<string, BaseClassDicEntity> 无相关性 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "无相关性", SName = "无相关性", Code = "WXGX", FontColor = "#ffffff", BGColor = "", Memo = "无相关性" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验站点相关 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "检验站点相关", SName = "检验站点相关", Code = "JYZDXG", FontColor = "#ffffff", BGColor = "", Memo = "与检验站点相关" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验小组相关 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "检验小组相关", SName = "检验小组相关", Code = "JYXZXG", FontColor = "#ffffff", BGColor = "", Memo = "与检验小组相关" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验仪器相关 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "检验仪器相关", SName = "检验仪器相关", Code = "JYYQXG", FontColor = "#ffffff", BGColor = "", Memo = "与检验仪器相关" });
        public static KeyValuePair<string, BaseClassDicEntity> 就诊类型相关 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "就诊类型相关", SName = "就诊类型相关", Code = "JZLXXG", FontColor = "#ffffff", BGColor = "", Memo = "与就诊类型相关" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型相关 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "样本类型相关", SName = "样本类型相关", Code = "YBLXG", FontColor = "#ffffff", BGColor = "", Memo = "与样本类型相关" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验专业相关 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "检验专业相关", SName = "检验专业相关", Code = "JYZYXG", FontColor = "#ffffff", BGColor = "", Memo = "与检验专业相关" });
        public static KeyValuePair<string, BaseClassDicEntity> 操作者相关 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "操作者相关", SName = "操作者相关", Code = "CZZXG", FontColor = "#ffffff", BGColor = "", Memo = "与操作者相关" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 无相关性.Key, 无相关性.Value },
                { 检验站点相关.Key, 检验站点相关.Value },
                { 检验小组相关.Key, 检验小组相关.Value },
                { 检验仪器相关.Key, 检验仪器相关.Value },
                { 就诊类型相关.Key, 就诊类型相关.Value },
                { 样本类型相关.Key, 样本类型相关.Value },
                { 检验专业相关.Key, 检验专业相关.Value },
                { 操作者相关.Key, 操作者相关.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 参数模块分类
    /// 此分类为第一级分类
    /// </summary>
    [DataDesc(CName = "检验系统参数一级分类", ClassCName = "Para_MoudleType", ShortCode = "", Desc = "按大类分", Length = 1)]
    public static class Para_MoudleType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 常规检验 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "检验中-常规检验", SName = "常规检验", Code = "Para_NTestType", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 微生物检验 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "检验中-微生物检验", SName = "微生物检验", Code = "Para_MicroTestType", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 通讯 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "通讯", SName = "通讯", Code = "Para_ComType", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 质控 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "质控", SName = "质控", Code = "Para_QCType", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            return new Dictionary<string, BaseClassDicEntity>
            {
                { 常规检验.Key, 常规检验.Value },
                { 微生物检验.Key, 微生物检验.Value },
                { 通讯.Key, 通讯.Value },
                { 质控.Key, 质控.Value }
            };
        }
    }

    #region 常规检验相关参数


    /// <summary>
    /// 常规检验参数分类
    /// 此分类为第二级分类
    /// </summary>
    [DataDesc(CName = "常规检验二级分类", ClassCName = "Para_NTestType", ShortCode = "", Desc = "", Length = 2)]
    public static class Para_NTestType
    {
        #region 系统判定设置参数
        public static KeyValuePair<string, BaseClassDicEntity> 项目检验完成校验 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "项目检验完成校验", SName = "项目检验完成校验", Code = "NTestType_SysJudge_TestItem_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验小组相关.Key, BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 申请信息校验 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "申请信息校验", SName = "申请信息校验", Code = "NTestType_SysJudge_OrderInfo_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验小组相关.Key, BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验时间校验 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "检验时间校验", SName = "检验时间校验", Code = "NTestType_SysJudge_TestDate_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验小组相关.Key, BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验结果校验 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "检验结果校验", SName = "检验结果校验", Code = "NTestType_SysJudge_TestResult_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验小组相关.Key, BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 其他校验 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "其他校验", SName = "其他校验", Code = "NTestType_SysJudge_OtherInfo_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验小组相关.Key, BGColor = "", Memo = "" });
        #endregion

        #region 历史对比参数设置
        public static KeyValuePair<string, BaseClassDicEntity> 历史对比设置 = new KeyValuePair<string, BaseClassDicEntity>("60001", new BaseClassDicEntity() { Id = "60001", Name = "历史对比设置", SName = "历史对比设置", Code = "NTestType_ItemResult_HistoryCompare_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验小组相关.Key, BGColor = "", Memo = "" });
        #endregion

        #region 审核参数设置
        public static KeyValuePair<string, BaseClassDicEntity> 审核参数设置 = new KeyValuePair<string, BaseClassDicEntity>("91000", new BaseClassDicEntity() { Id = "91000", Name = "审核参数设置", SName = "审核参数设置", Code = "NTestType_TestFormCheck_Operater_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验小组相关.Key, BGColor = "", Memo = "" });
        #endregion

        #region 危急值参数设置
        public static KeyValuePair<string, BaseClassDicEntity> 危急值参数设置 = new KeyValuePair<string, BaseClassDicEntity>("99000", new BaseClassDicEntity() { Id = "99000", Name = "危急值参数设置", SName = "危急值参数设置", Code = "NTestType_TesItem_PanicValue_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验小组相关.Key, BGColor = "", Memo = "" });
        #endregion

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                #region 系统判定设置参数
                { 项目检验完成校验.Key, 项目检验完成校验.Value },
                { 申请信息校验.Key, 申请信息校验.Value },
                { 检验时间校验.Key, 检验时间校验.Value },
                { 检验结果校验.Key, 检验结果校验.Value },
                { 其他校验.Key, 其他校验.Value },
                #endregion

                #region 历史对比参数设置
                { 历史对比设置.Key, 历史对比设置.Value },                             
                #endregion
                
                #region 审核参数设置
                  { 审核参数设置.Key, 审核参数设置.Value },
                #endregion

                #region 危急值参数设置
                  { 危急值参数设置.Key, 危急值参数设置.Value }
                #endregion
            };
            return dic;
        }
    }
    /// <summary>
    /// 系统判定设置---项目检验完成校验参数
    /// </summary>
    [DataDesc(CName = "项目检验完成校验参数字典类", ClassCName = "NTestType_SysJudge_TestItem_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class NTestType_SysJudge_TestItem_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "NTestType_SysJudge_TestItem_0001", CName = "检验单有检验项目", SName = "", ShortCode = "", PinYinZiTou = "JYDYJYXM", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.项目检验完成校验.Value.Code, ParaGroup = Para_NTestType.项目检验完成校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestItem_0002", CName = "项目检验完成（都有报告值）", SName = "", ShortCode = "", PinYinZiTou = "XMJYWC(DYBGZ)", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.项目检验完成校验.Value.Code, ParaGroup = Para_NTestType.项目检验完成校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestItem_0003", CName = "病人信息存在（病人姓名，性别）", SName = "", ShortCode = "", PinYinZiTou = "BRXXCZ(XMXB)", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.项目检验完成校验.Value.Code, ParaGroup = Para_NTestType.项目检验完成校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestItem_0004", CName = "样本信息存在", SName = "", ShortCode = "", PinYinZiTou = "YBXXCZ", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.项目检验完成校验.Value.Code, ParaGroup = Para_NTestType.项目检验完成校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" },
        };
    }

    /// <summary>
    /// 系统判定设置---申请信息校验参数
    /// </summary>
    [DataDesc(CName = "申请信息校验参数字典类", ClassCName = "NTestType_SysJudge_OrderInfo_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class NTestType_SysJudge_OrderInfo_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "NTestType_SysJudge_OrderInfo_0001", CName = "有对应的申请信息", SName = "", ShortCode = "", PinYinZiTou = "YDYDSQXX", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.申请信息校验.Value.Code, ParaGroup = Para_NTestType.申请信息校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_OrderInfo_0002", CName = "患者信息检查", SName = "", ShortCode = "", PinYinZiTou = "HZXXJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.申请信息校验.Value.Code, ParaGroup = Para_NTestType.申请信息校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
            //文档中此内容已删除//new BPara() { ParaNo = "NTestType_SysJudge_OrderInfo_0003", CName = "样本类型检查", SName = "", ShortCode = "", PinYinZiTou = "YBLXJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.申请信息校验.Value.Code, ParaGroup = Para_NTestType.申请信息校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "", DispOrder = 3, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_OrderInfo_0003", CName = "样本特殊性状检查", SName = "", ShortCode = "", PinYinZiTou = "YBTSXZJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.申请信息校验.Value.Code, ParaGroup = Para_NTestType.申请信息校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_OrderInfo_0004", CName = "检验项目与医嘱项目对比检查", SName = "", ShortCode = "", PinYinZiTou = "JYXMYYZDBJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.申请信息校验.Value.Code, ParaGroup = Para_NTestType.申请信息校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 5, BVisible = true, IsUse = true, ParaVER = "" },
        };
    }

    /// <summary>
    /// 系统判定设置---检验时间校验参数
    /// </summary>
    [DataDesc(CName = "检验时间校验参数字典类", ClassCName = "NTestType_SysJudge_TestDate_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class NTestType_SysJudge_TestDate_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "NTestType_SysJudge_TestDate_0001", CName = "样本采样时间", SName = "", ShortCode = "", PinYinZiTou = "YDYDSQXX", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验时间校验.Value.Code, ParaGroup = Para_NTestType.检验时间校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestDate_0002", CName = "样本检验时间", SName = "", ShortCode = "", PinYinZiTou = "HZXXJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验时间校验.Value.Code, ParaGroup = Para_NTestType.检验时间校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestDate_0003", CName = "样本收样时间", SName = "", ShortCode = "", PinYinZiTou = "YBLXJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验时间校验.Value.Code, ParaGroup = Para_NTestType.检验时间校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 3, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestDate_0004", CName = "样本上机时间", SName = "", ShortCode = "", PinYinZiTou = "YBTSXZJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验时间校验.Value.Code, ParaGroup = Para_NTestType.检验时间校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestDate_0005", CName = "采样到检验超时检查", SName = "", ShortCode = "", PinYinZiTou = "JYXMYYZDBJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验时间校验.Value.Code, ParaGroup = Para_NTestType.检验时间校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 5, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestDate_0006", CName = "各时间节点正确性检查", SName = "", ShortCode = "", PinYinZiTou = "JYXMYYZDBJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验时间校验.Value.Code, ParaGroup = Para_NTestType.检验时间校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 6, BVisible = true, IsUse = true, ParaVER = "" },
        };
    }

    /// <summary>
    /// 系统判定设置---检验结果校验参数
    /// </summary>
    [DataDesc(CName = "检验结果校验参数字典类", ClassCName = "NTestType_SysJudge_TestResult_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class NTestType_SysJudge_TestResult_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "NTestType_SysJudge_TestResult_0001", CName = "结果异常高/异常低状态检查", SName = "", ShortCode = "", PinYinZiTou = "JGYCG/D", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验结果校验.Value.Code, ParaGroup = Para_NTestType.检验结果校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestResult_0002", CName = "结果偏高/偏低状态检查", SName = "", ShortCode = "", PinYinZiTou = "JGPG/D", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验结果校验.Value.Code, ParaGroup = Para_NTestType.检验结果校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestResult_0003", CName = "结果值非负检查", SName = "", ShortCode = "", PinYinZiTou = "JGZFFJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验结果校验.Value.Code, ParaGroup = Para_NTestType.检验结果校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 3, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestResult_0004", CName = "结果阳性检查（包含阳性 + 等）", SName = "", ShortCode = "", PinYinZiTou = "JGZYXJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验结果校验.Value.Code, ParaGroup = Para_NTestType.检验结果校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestResult_0005", CName = "结果弱阳性检查", SName = "", ShortCode = "", PinYinZiTou = "JGZRYXJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验结果校验.Value.Code, ParaGroup = Para_NTestType.检验结果校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 5, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestResult_0006", CName = "结果异常检查", SName = "", ShortCode = "", PinYinZiTou = "JGZYCJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验结果校验.Value.Code, ParaGroup = Para_NTestType.检验结果校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestResult_0007", CName = "结果警告状态", SName = "", ShortCode = "", PinYinZiTou = "JGZJGZT", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验结果校验.Value.Code, ParaGroup = Para_NTestType.检验结果校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestResult_0008", CName = "历史对比差异大", SName = "", ShortCode = "", PinYinZiTou = "LSDBCYD", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验结果校验.Value.Code, ParaGroup = Para_NTestType.检验结果校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 3, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_TestResult_0009", CName = "仪器结果报警", SName = "", ShortCode = "", PinYinZiTou = "YQJGBJ", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验结果校验.Value.Code, ParaGroup = Para_NTestType.检验结果校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" },
            //new BPara() { ParaNo = "NTestType_SysJudge_TestResult_0000", CName = "结果无效检查", SName = "", ShortCode = "", PinYinZiTou = "LSDBCYBD", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.检验结果校验.Value.Code, ParaGroup = Para_NTestType.检验结果校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "结果状态ResultStatusCode=U 界面不出现，必须校验", ParaEditInfo = "", DispOrder = 5, BVisible = true, IsUse = true, ParaVER = "" },
        };
    }

    /// <summary>
    /// 系统判定设置---其他校验参数
    /// </summary>
    [DataDesc(CName = "其他校验参数字典类", ClassCName = "NTestType_SysJudge_OtherInfo_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class NTestType_SysJudge_OtherInfo_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "NTestType_SysJudge_OtherInfo_0001", CName = "仪器报警检查", SName = "", ShortCode = "", PinYinZiTou = "YQBJJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.其他校验.Value.Code, ParaGroup = Para_NTestType.其他校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_OtherInfo_0002", CName = "仪器质控检查", SName = "", ShortCode = "", PinYinZiTou = "YQZKJC", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.其他校验.Value.Code, ParaGroup = Para_NTestType.其他校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_SysJudge_OtherInfo_0003", CName = "专家规则判定", SName = "", ShortCode = "", PinYinZiTou = "ZJGZPD", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.其他校验.Value.Code, ParaGroup = Para_NTestType.其他校验.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 3, BVisible = true, IsUse = true, ParaVER = "" },
         };
    }

    /// <summary>
    /// 审核参数设置---审核人相关参数
    /// </summary>
    [DataDesc(CName = "审核人相关参数类", ClassCName = "NTestType_TestFormCheck_Operater_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class NTestType_TestFormCheck_Operater_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
             new BPara() { ParaNo = "NTestType_TestFormCheck_Operater_0001", CName = "检验者与审核者不同", SName = "", ShortCode = "", PinYinZiTou = "JYZYSHZBT", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.审核参数设置.Value.Code, ParaGroup = Para_NTestType.审核参数设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" },
         };
    }

    /// <summary>
    /// 危急值参数设置
    /// </summary>
    [DataDesc(CName = "危急值参数类", ClassCName = "NTestType_TesItem_PanicValue_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class NTestType_TesItem_PanicValue_Para
    {

        public static IList<BPara> ParaList = new List<BPara>()
        {
             new BPara() { ParaNo = "NTestType_TesItem_PanicValue_0000", CName = "开启危急值判断", SName = "", ShortCode = "", PinYinZiTou = "KQWJZPD", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.危急值参数设置.Value.Code, ParaGroup = Para_NTestType.危急值参数设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 0, BVisible = true, IsUse = true, ParaVER = "" },
             new BPara() { ParaNo = "NTestType_TesItem_PanicValue_0001", CName = "异常结果发送危急值", SName = "", ShortCode = "", PinYinZiTou = "YCJGFSWJZ", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.危急值参数设置.Value.Code, ParaGroup = Para_NTestType.危急值参数设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
             new BPara() { ParaNo = "NTestType_TesItem_PanicValue_0002", CName = "危急值发送时间节点", SName = "", ShortCode = "", PinYinZiTou = "WJZFSSJJD", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.危急值参数设置.Value.Code, ParaGroup = Para_NTestType.危急值参数设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "", ParaDesc = "", ParaEditInfo = "CL|检验单确认||检验单确认#检验单审定/1/1", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
             new BPara() { ParaNo = "NTestType_TesItem_PanicValue_0003", CName = "其他时间是否可以发送危急值", SName = "", ShortCode = "", PinYinZiTou = "QTSJFSWJZ", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.危急值参数设置.Value.Code, ParaGroup = Para_NTestType.危急值参数设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 3, BVisible = true, IsUse = true, ParaVER = "" },
             new BPara() { ParaNo = "NTestType_TesItem_PanicValue_0004", CName = "危急值发送电话通知", SName = "", ShortCode = "", PinYinZiTou = "WJZDHTZ", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.危急值参数设置.Value.Code, ParaGroup = Para_NTestType.危急值参数设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|0", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" },
         };
    }


    /// <summary>
    /// 历史对比参数
    /// </summary>
    [DataDesc(CName = "历史对比设置参数字典类", ClassCName = "NTestType_SysJudge_TestItem_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class NTestType_ItemResult_HistoryCompare_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "NTestType_ItemResult_HistoryCompare_0001", CName = "历史对比是否打开", SName = "", ShortCode = "", PinYinZiTou = "LSDBSFDK", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.历史对比设置.Value.Code, ParaGroup = Para_NTestType.历史对比设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "0", ParaDesc = "0:不打开；1：打开；默认为0", ParaEditInfo = "E|0", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "NTestType_ItemResult_HistoryCompare_0011", CName = "历史对比日期范围", SName = "", ShortCode = "", PinYinZiTou = "LSDBRQFW", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.历史对比设置.Value.Code, ParaGroup = Para_NTestType.历史对比设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "90", ParaDesc = "默认为90天之内的历史数据", ParaEditInfo = "I|90", DispOrder = 11, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_ItemResult_HistoryCompare_0012", CName = "历史对比小组范围", SName = "", ShortCode = "", PinYinZiTou = "LSDBXZFW", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.历史对比设置.Value.Code, ParaGroup = Para_NTestType.历史对比设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "", ParaDesc = "不设置为全部小组", ParaEditInfo = "", DispOrder = 12, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_ItemResult_HistoryCompare_0013", CName = "历史对比区分样本类型", SName = "", ShortCode = "", PinYinZiTou = "LSDBQFYBLX", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.历史对比设置.Value.Code, ParaGroup = Para_NTestType.历史对比设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "0", ParaDesc = "0:不区分；1：区分；默认为0", ParaEditInfo = "E|0", DispOrder = 13, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_ItemResult_HistoryCompare_0014", CName = "历史对比查询字段列表", SName = "", ShortCode = "", PinYinZiTou = "JYDYJYXM", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.历史对比设置.Value.Code, ParaGroup = Para_NTestType.历史对比设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "PatNo", ParaDesc = "不设置默认为病历号PatNo，格式为:PatNo,CName...", ParaEditInfo = "C|PatNo", DispOrder = 14, BVisible = true, IsUse = true, ParaVER = "" },

            new BPara() { ParaNo = "NTestType_ItemResult_HistoryCompare_0021", CName = "历史对比高低百分比参考值", SName = "", ShortCode = "", PinYinZiTou = "LSDBGDBFBCKZ", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.历史对比设置.Value.Code, ParaGroup = Para_NTestType.历史对比设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "30", ParaDesc = "", ParaEditInfo = "I|30", DispOrder = 21, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_ItemResult_HistoryCompare_0022", CName = "历史对比高低百分比符号", SName = "", ShortCode = "", PinYinZiTou = "LSDBGDBFBFH", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.历史对比设置.Value.Code, ParaGroup = Para_NTestType.历史对比设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "↑|↓", ParaDesc = "", ParaEditInfo = "C|↑|↓", DispOrder = 22, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_ItemResult_HistoryCompare_0025", CName = "历史对比超高低百分比参考值", SName = "", ShortCode = "", PinYinZiTou = "LSDBCGDBFBCKZ", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.历史对比设置.Value.Code, ParaGroup = Para_NTestType.历史对比设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "50", ParaDesc = "", ParaEditInfo = "I|50", DispOrder = 25, BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "NTestType_ItemResult_HistoryCompare_0026", CName = "历史对比超高低百分比符号", SName = "", ShortCode = "", PinYinZiTou = "LSDBCGDBFBFH", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.历史对比设置.Value.Code, ParaGroup = Para_NTestType.历史对比设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "▲|▼", ParaDesc = "", ParaEditInfo = "C|▲|▼", DispOrder = 26, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "NTestType_ItemResult_HistoryCompare_0029", CName = "历史对比百分比小数位数", SName = "", ShortCode = "", PinYinZiTou = "LSDBCGDBFBFH", SystemCode = Para_SystemType.检验小组相关.Key, TypeCode = Para_NTestType.历史对比设置.Value.Code, ParaGroup = Para_NTestType.历史对比设置.Value.Name, ParaType = Para_MoudleType.常规检验.Value.Name, ParaValue = "1", ParaDesc = "默认为1位小数", ParaEditInfo = "CL|1||0#1#2#3", DispOrder = 29, BVisible = true, IsUse = true, ParaVER = ""},
        };
    }

    #endregion

    #region 微生物检验相关参数

    /// <summary>
    /// 微生物检验参数分类
    /// 此分类为第二级分类
    /// </summary>
    [DataDesc(CName = "微生物检验二级分类", ClassCName = "Para_MicroTestType", ShortCode = "", Desc = "", Length = 2)]
    public static class Para_MicroTestType
    {

    }

    #endregion

    #region 质控相关参数设置 
    /// <summary>
    /// 质控参数分类
    /// 此分类为第二级分类
    /// </summary>
    [DataDesc(CName = "质控参数二级分类", ClassCName = "Para_QCType", ShortCode = "", Desc = "", Length = 2)]
    public static class Para_QCType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 质控数据精度 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "质控数据精度", SName = "质控数据精度", Code = "QC_Value_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验仪器相关.Key, BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 质控Z分数图 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "质控Z分数图", SName = "质控Z分数图", Code = "QC_Z_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验仪器相关.Key, BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 质控LJ图 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "质控LJ图", SName = "质控LJ图", Code = "QC_LJ_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验仪器相关.Key, BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 质控U顿图 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "质控U顿图", SName = "质控U顿图", Code = "QC_U_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验仪器相关.Key, BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 质控正态分布图 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "质控正态分布图", SName = "质控正态分布图", Code = "QC_ZTFB_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验仪器相关.Key, BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 质控值范围图 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "质控值范围图", SName = "质控值范围图", Code = "QC_ZFW_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验仪器相关.Key, BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 质控Monico图 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "质控Monico图", SName = "质控Monico图", Code = "QC_Monico_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验仪器相关.Key, BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 质控定性质控图 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "质控定性质控图", SName = "质控定性质控图", Code = "QC_DX_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验仪器相关.Key, BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 质控通讯 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "质控通讯", SName = "质控通讯", Code = "QC_Com_Para", FontColor = "#ffffff", DefaultValue = Para_SystemType.检验仪器相关.Key, BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 质控数据精度.Key, 质控数据精度.Value },
                { 质控Z分数图.Key, 质控Z分数图.Value },
                { 质控LJ图.Key, 质控LJ图.Value },
                { 质控U顿图.Key, 质控U顿图.Value },
                { 质控正态分布图.Key, 质控正态分布图.Value },
                { 质控值范围图.Key, 质控值范围图.Value },
                { 质控Monico图.Key, 质控Monico图.Value },
                { 质控定性质控图.Key, 质控定性质控图.Value },
                { 质控通讯.Key, 质控通讯.Value }
            };
            return dic;
        }
    }

    //数据类型|默认值|下拉列表|下拉或展开


    /// <summary>
    /// 质控分类【质控数据精度】参数
    /// </summary>
    [DataDesc(CName = "质控数据精度参数字典类", ClassCName = "QC_Value_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class QC_Value_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "QC_Value_0001", CName = "质控结果精度增强位数", SName = "", ShortCode = "", PinYinZiTou = "ZKJGJD", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控数据精度.Value.Code, ParaGroup = Para_QCType.质控数据精度.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "CL|||0#1#2", DispOrder = 1 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_Value_0002", CName = "靶值，标准差精度增强位数", SName = "", ShortCode = "", PinYinZiTou = "BZBZCJD", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控数据精度.Value.Code, ParaGroup = Para_QCType.质控数据精度.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "CL|||0#1#2", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
        };
    }

    /// <summary>
    /// 质控分类【质控Z分数图】参数
    /// </summary>
    [DataDesc(CName = "质控Z分数图参数字典类", ClassCName = "QC_Z_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class QC_Z_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "QC_Z_0001", CName = "显示方式（X轴）", SName = "", ShortCode = "", PinYinZiTou = "XSFSXZ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|0|批次#日期", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Z_0002", CName = "质控点显示-质控日期", SName = "", ShortCode = "", PinYinZiTou = "XSZKRQ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Z_0003", CName = "质控点显示-质控状态", SName = "", ShortCode = "", PinYinZiTou = "XSZKZT", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 3, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Z_0004", CName = "质控点显示-质控结果", SName = "", ShortCode = "", PinYinZiTou = "XSZKJG", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Z_0005", CName = "质控点显示-操作者", SName = "", ShortCode = "", PinYinZiTou = "XSCZZ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 5, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Z_0006", CName = "显示质控点竖线", SName = "", ShortCode = "", PinYinZiTou = "XSZKDSX", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 6, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Z_0007", CName = "显示网格", SName = "", ShortCode = "", PinYinZiTou = "XSWG", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 7, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Z_0008", CName = "显示质控时效分割线", SName = "", ShortCode = "", PinYinZiTou = "XSZKSXFGX", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 8, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Z_0009", CName = "显示图例", SName = "", ShortCode = "", PinYinZiTou = "XSTL", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 9, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Z_00010", CName = "显示不使用质控点", SName = "", ShortCode = "", PinYinZiTou = "XSBSYZKD", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 10, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Z_00011", CName = "警告线颜色", SName = "", ShortCode = "", PinYinZiTou = "JGXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#FF0000", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 19, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Z_00012", CName = "失控线颜色", SName = "", ShortCode = "", PinYinZiTou = "SKXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#A54AFF", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 20, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Z_00013", CName = "质控数据精度不够时是否补0", SName = "", ShortCode = "", PinYinZiTou = "ZKSJJDBGSSFB", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Z分数图.Value.Code, ParaGroup = Para_QCType.质控Z分数图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 11, BVisible = true, IsUse = true, ParaVER = "" }
        };
    }

    /// <summary>
    /// 质控分类【质控LJ图】参数
    /// </summary>
    [DataDesc(CName = "质控LJ图参数字典类", ClassCName = "QC_LJ_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class QC_LJ_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "QC_LJ_0001", CName = "显示方式（X轴）", SName = "", ShortCode = "", PinYinZiTou = "XSFSXZ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|0|批次#日期", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_LJ_0002", CName = "质控点显示-质控日期", SName = "", ShortCode = "", PinYinZiTou = "XSZKRQ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_LJ_0003", CName = "质控点显示-质控状态", SName = "", ShortCode = "", PinYinZiTou = "XSZKZT", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 3, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_LJ_0004", CName = "质控点显示-质控结果", SName = "", ShortCode = "", PinYinZiTou = "XSZKJG", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_LJ_0005", CName = "质控点显示-操作者", SName = "", ShortCode = "", PinYinZiTou = "XSCZZ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 5, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_LJ_0006", CName = "显示质控点竖线", SName = "", ShortCode = "", PinYinZiTou = "XSZKDSX", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 6, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_LJ_0007", CName = "显示网格", SName = "", ShortCode = "", PinYinZiTou = "XSWG", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 7, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_LJ_0008", CName = "显示质控时效分割线", SName = "", ShortCode = "", PinYinZiTou = "XSZKSXFGX", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 8, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_LJ_0009", CName = "显示图例", SName = "", ShortCode = "", PinYinZiTou = "XSTL", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 9, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_LJ_00010", CName = "显示不使用质控点", SName = "", ShortCode = "", PinYinZiTou = "XSBSYZKD", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 10, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_LJ_00011", CName = "警告线颜色", SName = "", ShortCode = "", PinYinZiTou = "JGXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#FF0000", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 19, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_LJ_00012", CName = "失控线颜色", SName = "", ShortCode = "", PinYinZiTou = "SKXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#A54AFF", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 20, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_LJ_00013", CName = "质控数据精度不够时是否补0", SName = "", ShortCode = "", PinYinZiTou = "ZKSJJDBGSSFB", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控LJ图.Value.Code, ParaGroup = Para_QCType.质控LJ图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 11, BVisible = true, IsUse = true, ParaVER = "" },
        };
    }

    /// <summary>
    /// 质控分类【质控值范围图】参数
    /// </summary>
    [DataDesc(CName = "质控值范围图参数字典类", ClassCName = "QC_ZFW_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class QC_ZFW_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "QC_ZFW_0001", CName = "显示方式（X轴）", SName = "", ShortCode = "", PinYinZiTou = "XSFSXZ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E|0|批次#日期", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZFW_0002", CName = "质控点显示-质控日期", SName = "", ShortCode = "", PinYinZiTou = "XSZKRQ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZFW_0003", CName = "质控点显示-质控状态", SName = "", ShortCode = "", PinYinZiTou = "XSZKZT", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 3, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZFW_0004", CName = "质控点显示-质控结果", SName = "", ShortCode = "", PinYinZiTou = "XSZKJG", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZFW_0005", CName = "质控点显示-操作者", SName = "", ShortCode = "", PinYinZiTou = "XSCZZ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 5, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZFW_0006", CName = "显示质控点竖线", SName = "", ShortCode = "", PinYinZiTou = "XSZKDSX", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 6, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZFW_0007", CName = "显示网格", SName = "", ShortCode = "", PinYinZiTou = "XSWG", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 7, BVisible = true, IsUse = true, ParaVER = "" },
            //-值范围，monica 不用该参数 -//new BPara() { ParaNo = "QC_ZFW_0008", CName = "显示质控时效分割线", SName = "", ShortCode = "", PinYinZiTou = "XSZKSXFGX", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 8, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZFW_0009", CName = "显示图例", SName = "", ShortCode = "", PinYinZiTou = "XSTL", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 9, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZFW_00010", CName = "显示不使用质控点", SName = "", ShortCode = "", PinYinZiTou = "XSBSYZKD", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 10, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZFW_00011", CName = "警告线颜色", SName = "", ShortCode = "", PinYinZiTou = "JGXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#FF0000", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 19, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZFW_00012", CName = "失控线颜色", SName = "", ShortCode = "", PinYinZiTou = "SKXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#A54AFF", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 20, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZFW_00013", CName = "质控数据精度不够时是否补0", SName = "", ShortCode = "", PinYinZiTou = "ZKSJJDBGSSFB", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控值范围图.Value.Code, ParaGroup = Para_QCType.质控值范围图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 11, BVisible = true, IsUse = true, ParaVER = "" },
        };
    }

    /// <summary>
    /// 质控分类【质控Monica图】参数
    /// </summary>
    [DataDesc(CName = "质控Monica图参数字典类", ClassCName = "QC_Monico_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class QC_Monico_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "QC_Monico_0001", CName = "显示方式（X轴）", SName = "", ShortCode = "", PinYinZiTou = "XSFSXZ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E|0|批次#日期", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Monico_0002", CName = "质控点显示-质控日期", SName = "", ShortCode = "", PinYinZiTou = "XSZKRQ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Monico_0003", CName = "质控点显示-质控状态", SName = "", ShortCode = "", PinYinZiTou = "XSZKZT", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 3, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Monico_0004", CName = "质控点显示-质控结果", SName = "", ShortCode = "", PinYinZiTou = "XSZKJG", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Monico_0005", CName = "质控点显示-操作者", SName = "", ShortCode = "", PinYinZiTou = "XSCZZ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 5, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Monico_0006", CName = "显示质控点竖线", SName = "", ShortCode = "", PinYinZiTou = "XSZKDSX", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 6, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Monico_0007", CName = "显示网格", SName = "", ShortCode = "", PinYinZiTou = "XSWG", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 7, BVisible = true, IsUse = true, ParaVER = "" },
            //-值范围，monica 不用该参数 -//new BPara() { ParaNo = "QC_Monico_0008", CName = "显示质控时效分割线", SName = "", ShortCode = "", PinYinZiTou = "XSZKSXFGX", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 8, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Monico_0009", CName = "显示图例", SName = "", ShortCode = "", PinYinZiTou = "XSTL", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 9, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Monico_00010", CName = "显示不使用质控点", SName = "", ShortCode = "", PinYinZiTou = "XSBSYZKD", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 10, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Monico_00011", CName = "警告线颜色", SName = "", ShortCode = "", PinYinZiTou = "JGXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#FF0000", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 19, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Monico_00012", CName = "失控线颜色", SName = "", ShortCode = "", PinYinZiTou = "SKXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#A54AFF", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 20, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_Monico_00013", CName = "质控数据精度不够时是否补0", SName = "", ShortCode = "", PinYinZiTou = "ZKSJJDBGSSFB", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控Monico图.Value.Code, ParaGroup = Para_QCType.质控Monico图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 11, BVisible = true, IsUse = true, ParaVER = "" }
        };
    }

    /// <summary>
    /// 质控分类【质控定性图】参数
    /// </summary>
    [DataDesc(CName = "质控定性图参数字典类", ClassCName = "QC_DX_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class QC_DX_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "QC_DX_0001", CName = "显示方式（X轴）", SName = "", ShortCode = "", PinYinZiTou = "XSFSXZ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E|0|批次#日期", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_DX_0002", CName = "质控点显示-质控日期", SName = "", ShortCode = "", PinYinZiTou = "XSZKRQ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 2, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_DX_0003", CName = "质控点显示-质控状态", SName = "", ShortCode = "", PinYinZiTou = "XSZKZT", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 3, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_DX_0004", CName = "质控点显示-质控结果", SName = "", ShortCode = "", PinYinZiTou = "XSZKJG", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E|1", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_DX_0005", CName = "质控点显示-操作者", SName = "", ShortCode = "", PinYinZiTou = "XSCZZ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 5 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_DX_0006", CName = "显示质控点竖线", SName = "", ShortCode = "", PinYinZiTou = "XSZKDSX", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 6 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_DX_0007", CName = "显示网格", SName = "", ShortCode = "", PinYinZiTou = "XSWG", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 7 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_DX_0008", CName = "显示质控时效分割线", SName = "", ShortCode = "", PinYinZiTou = "XSZKSXFGX", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 8 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_DX_0009", CName = "显示图例", SName = "", ShortCode = "", PinYinZiTou = "XSTL", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 9 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_DX_00010", CName = "显示不使用质控点", SName = "", ShortCode = "", PinYinZiTou = "XSBSYZKD", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 10 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_DX_00011", CName = "警告线颜色", SName = "", ShortCode = "", PinYinZiTou = "JGXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#FF0000", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 19 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_DX_00012", CName = "失控线颜色", SName = "", ShortCode = "", PinYinZiTou = "SKXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#A54AFF", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 20 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_DX_00013", CName = "质控数据精度不够时是否补0", SName = "", ShortCode = "", PinYinZiTou = "ZKSJJDBGSSFB", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控定性质控图.Value.Code, ParaGroup = Para_QCType.质控定性质控图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 11 , BVisible = true, IsUse = true, ParaVER = ""},
        };
    }

    /// <summary>
    /// 质控分类【质控U顿图】参数
    /// </summary>
    [DataDesc(CName = "质控U顿图参数字典类", ClassCName = "QC_U_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class QC_U_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "QC_U_0001", CName = "质控点显示-质控结果", SName = "", ShortCode = "", PinYinZiTou = "XSZKJG", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控U顿图.Value.Code, ParaGroup = Para_QCType.质控U顿图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 1 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_U_0002", CName = "显示网格", SName = "", ShortCode = "", PinYinZiTou = "XSWG", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控U顿图.Value.Code, ParaGroup = Para_QCType.质控U顿图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 2 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_U_0003", CName = "显示图例", SName = "", ShortCode = "", PinYinZiTou = "XSTL", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控U顿图.Value.Code, ParaGroup = Para_QCType.质控U顿图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 3 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_U_0004", CName = "显示批次", SName = "", ShortCode = "", PinYinZiTou = "XSPC", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控U顿图.Value.Code, ParaGroup = Para_QCType.质控U顿图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 4 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_U_0005", CName = "显示日期", SName = "", ShortCode = "", PinYinZiTou = "XSRQ", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控U顿图.Value.Code, ParaGroup = Para_QCType.质控U顿图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 5 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_U_0006", CName = "警告线颜色", SName = "", ShortCode = "", PinYinZiTou = "JGXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控U顿图.Value.Code, ParaGroup = Para_QCType.质控U顿图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#FFA500", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 9 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_U_0007", CName = "质控点颜色", SName = "", ShortCode = "", PinYinZiTou = "ZKDYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控U顿图.Value.Code, ParaGroup = Para_QCType.质控U顿图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#000000", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 10 , BVisible = true, IsUse = true, ParaVER = ""},
             new BPara() { ParaNo = "QC_U_0008", CName = "质控数据精度不够时是否补0", SName = "", ShortCode = "", PinYinZiTou = "ZKSJJDBGSSFB", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控U顿图.Value.Code, ParaGroup = Para_QCType.质控U顿图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 6 , BVisible = true, IsUse = true, ParaVER = ""},
        };
    }

    /// <summary>
    /// 质控分类【质控正态分布图】参数
    /// </summary>
    [DataDesc(CName = "质控正态分布图参数字典类", ClassCName = "QC_ZTFB_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class QC_ZTFB_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "QC_ZTFB_0001", CName = "显示图例", SName = "", ShortCode = "", PinYinZiTou = "XSTL", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控正态分布图.Value.Code, ParaGroup = Para_QCType.质控正态分布图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "1", ParaDesc = "", ParaEditInfo = "E", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZTFB_0002", CName = "参考线颜色", SName = "", ShortCode = "", PinYinZiTou = "CKXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控正态分布图.Value.Code, ParaGroup = Para_QCType.质控正态分布图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#808080", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 2 , BVisible = true, IsUse = true, ParaVER = ""},
            new BPara() { ParaNo = "QC_ZTFB_0003", CName = "警告线颜色", SName = "", ShortCode = "", PinYinZiTou = "JGXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控正态分布图.Value.Code, ParaGroup = Para_QCType.质控正态分布图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#FF0000", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 3, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "QC_ZTFB_0004", CName = "失控线颜色", SName = "", ShortCode = "", PinYinZiTou = "SKXYS", SystemCode = Para_SystemType.检验仪器相关.Key, TypeCode = Para_QCType.质控正态分布图.Value.Code, ParaGroup = Para_QCType.质控正态分布图.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "#A54AFF", ParaDesc = "", ParaEditInfo = "RGB", DispOrder = 4, BVisible = true, IsUse = true, ParaVER = "" }
        };
    }

    #endregion

    #region 通讯类相关参数
    /// <summary>
    /// 通讯参数分类
    /// 此分类为第二级分类
    /// </summary>
    [DataDesc(CName = "质控参数二级分类", ClassCName = "Para_ComType", ShortCode = "", Desc = "", Length = 2)]
    public static class Para_ComType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 通讯参数分类1 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "通讯参数分类1", SName = "通讯参数分类1", Code = "Com_Type1_Para", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 通讯参数分类2 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "通讯参数分类2", SName = "通讯参数分类2", Code = "Com_Type2_Para", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 通讯参数分类3 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "通讯参数分类3", SName = "通讯参数分类3", Code = "Com_Type3_Para", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 通讯参数分类1.Key, 通讯参数分类1.Value },
                { 通讯参数分类2.Key, 通讯参数分类2.Value },
                { 通讯参数分类3.Key, 通讯参数分类3.Value }
            };
            return dic;
        }

    }

    [DataDesc(CName = "通讯参数分类1字典类", ClassCName = "Com_Type1_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class Com_Type1_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "Com_0001", CName = "通讯参数1", SName = "", ShortCode = "", PinYinZiTou = "", SystemCode = Para_SystemType.检验站点相关.Key, TypeCode = Para_ComType.通讯参数分类1.Value.Code, ParaGroup = Para_ComType.通讯参数分类1.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaDesc = "", ParaEditInfo = "E|0|批次#日期", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "Com_0002", CName = "通讯参数2", SName = "", ShortCode = "", PinYinZiTou = "", SystemCode = Para_SystemType.检验站点相关.Key, TypeCode = Para_ComType.通讯参数分类1.Value.Code, ParaGroup = Para_ComType.通讯参数分类1.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" }
        };

    }

    [DataDesc(CName = "通讯参数分类2字典类", ClassCName = "Com_Type2_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class Com_Type2_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "Com_0003", CName = "通讯参数13", SName = "", ShortCode = "", PinYinZiTou = "", SystemCode = Para_SystemType.检验站点相关.Key, TypeCode = Para_ComType.通讯参数分类2.Value.Code, ParaGroup = Para_ComType.通讯参数分类2.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaDesc = "", ParaEditInfo = "E|0|是#否", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "Com_0005", CName = "通讯参数5", SName = "", ShortCode = "", PinYinZiTou = "", SystemCode = Para_SystemType.检验站点相关.Key, TypeCode = Para_ComType.通讯参数分类2.Value.Code, ParaGroup = Para_ComType.通讯参数分类2.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "0", ParaDesc = "", ParaEditInfo = "E", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" }
        };
    }

    [DataDesc(CName = "通讯参数分类3字典类", ClassCName = "Com_Type3_Para", ShortCode = "", Desc = "", Length = 6)]
    public static class Com_Type3_Para
    {
        public static IList<BPara> ParaList = new List<BPara>()
        {
            new BPara() { ParaNo = "Com_0006", CName = "通讯参数6", SName = "", ShortCode = "", PinYinZiTou = "", SystemCode = Para_SystemType.检验站点相关.Key, TypeCode = Para_ComType.通讯参数分类3.Value.Code, ParaGroup = Para_ComType.通讯参数分类3.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "3", ParaDesc = "", ParaEditInfo = "E|0|是#否", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = "" },
            new BPara() { ParaNo = "Com_0007", CName = "通讯参数7", SName = "", ShortCode = "", PinYinZiTou = "", SystemCode = Para_SystemType.检验站点相关.Key, TypeCode = Para_ComType.通讯参数分类3.Value.Code, ParaGroup = Para_ComType.通讯参数分类3.Value.Name, ParaType = Para_MoudleType.质控.Value.Name, ParaValue = "4", ParaDesc = "", ParaEditInfo = "E", DispOrder = 1, BVisible = true, IsUse = true, ParaVER = ""}
         };
    }
    #endregion

}