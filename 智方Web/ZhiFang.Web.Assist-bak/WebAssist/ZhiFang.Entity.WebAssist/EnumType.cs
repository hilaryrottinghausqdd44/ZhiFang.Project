using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Policy;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace ZhiFang.Entity.WebAssist
{

    public class BaseClassDicEntity
    {
        public string Id { get; set; }
        public string ParentID;
        public string DataType;
        public string Name { get; set; }
        public string Code { get; set; }
        public string FontColor { get; set; }
        public string BGColor { get; set; }
        public string SName { get; set; }
        public string DefaultValue { get; set; }
        public string DispOrder { get; set; }
        public string Memo { get; set; }
    }

    #region 公共字典
    /// <summary>
    /// 系统参数类型
    /// </summary>
    public static class SYSParaType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 全系统 = new KeyValuePair<string, BaseClassDicEntity>("SYS", new BaseClassDicEntity() { Id = "SYS", Name = "全系统", Code = "SYS", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 可配置参数 = new KeyValuePair<string, BaseClassDicEntity>("CONFIG", new BaseClassDicEntity() { Id = "CONFIG", Name = "可配置参数", Code = "CONFIG", FontColor = "#ffffff", BGColor = "#aad08f" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SYSParaType.全系统.Key, SYSParaType.全系统.Value);
            dic.Add(SYSParaType.可配置参数.Key, SYSParaType.可配置参数.Value);
            return dic;
        }
    }

    /// <summary>
    /// 业务接口字典对照
    /// 如果key为"BDict"，取MapingBDictType对应的BDictType的ID的值作为数据过滤
    /// </summary>
    public static class InterfaceMapingBDict
    {
        public static KeyValuePair<string, BaseClassDicEntity> 公共字典 = new KeyValuePair<string, BaseClassDicEntity>("BDict", new BaseClassDicEntity() { Id = "1", ParentID = "0", DispOrder = "1", Name = "公共字典", Code = "BDict", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验项目 = new KeyValuePair<string, BaseClassDicEntity>("BloodBTestItem", new BaseClassDicEntity() { Id = "3", ParentID = "0", DispOrder = "12", Name = "检验项目", Code = "BloodBTestItem", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 人员字典对照 = new KeyValuePair<string, BaseClassDicEntity>("PUser", new BaseClassDicEntity() { Id = "7", ParentID = "0", DispOrder = "11", Name = "人员字典对照", Code = "PUser", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室字典对照 = new KeyValuePair<string, BaseClassDicEntity>("Department", new BaseClassDicEntity() { Id = "8", ParentID = "0", DispOrder = "11", Name = "科室字典对照", Code = "Department", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 费用项目对照 = new KeyValuePair<string, BaseClassDicEntity>("BloodChargeItem", new BaseClassDicEntity() { Id = "9", ParentID = "0", DispOrder = "11", Name = "费用项目对照", Code = "BloodChargeItem", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(InterfaceMapingBDict.公共字典.Key, InterfaceMapingBDict.公共字典.Value);
            dic.Add(InterfaceMapingBDict.检验项目.Key, InterfaceMapingBDict.检验项目.Value);
            dic.Add(InterfaceMapingBDict.人员字典对照.Key, InterfaceMapingBDict.人员字典对照.Value);
            dic.Add(InterfaceMapingBDict.科室字典对照.Key, InterfaceMapingBDict.科室字典对照.Value);
            dic.Add(InterfaceMapingBDict.费用项目对照.Key, InterfaceMapingBDict.费用项目对照.Value);
            return dic;
        }
    }

    /// <summary>
    /// 人员身份
    /// </summary>
    public static class EmpSystemType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 检验技师 = new KeyValuePair<string, BaseClassDicEntity>("1001001", new BaseClassDicEntity() { Id = "1001001", Name = "检验技师", Code = "1001001", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 护士 = new KeyValuePair<string, BaseClassDicEntity>("1001002", new BaseClassDicEntity() { Id = "1001002", Name = "护士", Code = "1001002", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 医生 = new KeyValuePair<string, BaseClassDicEntity>("1001003", new BaseClassDicEntity() { Id = "1001003", Name = "医生", Code = "1001003", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 护工 = new KeyValuePair<string, BaseClassDicEntity>("1001004", new BaseClassDicEntity() { Id = "1001004", Name = "护工", Code = "1001004", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 医务科人员 = new KeyValuePair<string, BaseClassDicEntity>("1001005", new BaseClassDicEntity() { Id = "1001005", Name = "医务科人员", Code = "1001005", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 财务人员 = new KeyValuePair<string, BaseClassDicEntity>("1001006", new BaseClassDicEntity() { Id = "1001006", Name = "财务人员", Code = "1001006", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 检验技师.Key, 检验技师.Value },
                { 护士.Key, 护士.Value },
                { 医生.Key, 医生.Value },
                { 护工.Key, 护工.Value },
                { 医务科人员.Key, 医务科人员.Value },
                { 财务人员.Key, 财务人员.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 机构类型
    /// </summary>
    public static class OrgType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 供应商 = new KeyValuePair<string, BaseClassDicEntity>("5068778579665143965", new BaseClassDicEntity() { Id = "5068778579665143965", Name = "供应商", Code = "Comp", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 实验室 = new KeyValuePair<string, BaseClassDicEntity>("5070458560961965845", new BaseClassDicEntity() { Id = "5070458560961965845", Name = "实验室", Code = "Lab", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 厂商 = new KeyValuePair<string, BaseClassDicEntity>("5095132324043854891", new BaseClassDicEntity() { Id = "5095132324043854891", Name = "厂商", Code = "Fact", FontColor = "#ffffff", BGColor = "#1195db" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(OrgType.供应商.Key, OrgType.供应商.Value);
            dic.Add(OrgType.实验室.Key, OrgType.实验室.Value);
            dic.Add(OrgType.厂商.Key, OrgType.厂商.Value);
            return dic;
        }
    }

    /// <summary>
    /// 系统参数编码
    /// </summary>
    public static class SYSParaNo
    {
        public static KeyValuePair<string, BaseClassDicEntity> 数据库版本号 = new KeyValuePair<string, BaseClassDicEntity>("SYS_DBVersion", new BaseClassDicEntity() { Id = "SYS_DBVersion", Name = "数据库版本号", Code = "SYS_DBVersion", FontColor = "#ffffff", BGColor = "#f4c600", DefaultValue = "1.0.0.0", Memo = "", SName = "系统", DataType = "String" });
        public static KeyValuePair<string, BaseClassDicEntity> 登录后升级数据库 = new KeyValuePair<string, BaseClassDicEntity>("BL-SYSE-UDAL-0001", new BaseClassDicEntity() { Id = "BL-SYSE-UDAL-0001", Name = "登录后升级数据库", Code = "ISUpgradeDatabaseAfterLogin", FontColor = "#ffffff", BGColor = "#f4c600", DefaultValue = "1", Memo = "是否(0:否;1:是)", SName = "系统", DataType = "Boolean" });
        public static KeyValuePair<string, BaseClassDicEntity> 集成平台服务访问URL = new KeyValuePair<string, BaseClassDicEntity>("BL-SYSE-LURL-0002", new BaseClassDicEntity() { Id = "BL-SYSE-LURL-0002", Name = "集成平台服务访问URL", Code = "IPlatformServiceAccessURL", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "", Memo = "", SName = "集成平台", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> CS服务访问URL = new KeyValuePair<string, BaseClassDicEntity>("BL-SYSE-CSRL-0011", new BaseClassDicEntity() { Id = "BL-SYSE-CSRL-0011", Name = "CS服务访问URL", Code = "CSServiceAccessURL", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "http://localhost", Memo = "", SName = "CS接口", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> 列表默认分页记录数 = new KeyValuePair<string, BaseClassDicEntity>("BL-LRMP-UIPA-0007", new BaseClassDicEntity() { Id = "BL-LRMP-UIPA-0007", Name = "列表默认分页记录数", Code = "BLTFUIDefaultPageSize", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "50", Memo = "系统默认列表的分页数为50条记录每页,用户可自行设置,设置保存后需要重新登录才生效", SName = "UI", DataType = "String" });
        public static KeyValuePair<string, BaseClassDicEntity> 启用用户UI配置 = new KeyValuePair<string, BaseClassDicEntity>("BL-EUSE-UICF-0008", new BaseClassDicEntity() { Id = "BL-EUSE-UICF-0008", Name = "启用用户UI配置", Code = "EnableUserUIConfig", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "0", Memo = "1:是;0:否;", SName = "UI", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> 完成评估后超过天数未归档 = new KeyValuePair<string, BaseClassDicEntity>("GK-CPEV-DAYS-0009", new BaseClassDicEntity() { Id = "GK-CPEV-DAYS-0009", Name = "完成评估后超过天数未归档", Code = "", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "30", Memo = "完成评估后超过天数未归档", SName = "院感", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> 指定自动核收检验小组 = new KeyValuePair<string, BaseClassDicEntity>("GK-AUTO-TEAM-0010", new BaseClassDicEntity() { Id = "GK-AUTO-TEAM-0010", Name = "指定自动核收检验小组", Code = "AutomaticTeam", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "30", Memo = "指定自动核收检验小组", SName = "院感", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> 指定自动核收默认检验者编码 = new KeyValuePair<string, BaseClassDicEntity>("GK-AUTO-ACCE-0011", new BaseClassDicEntity() { Id = "GK-AUTO-ACCE-0011", Name = "指定自动核收默认检验者编码", Code = "AutomaticAcceptance", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "", Memo = "指定自动核收默认检验者编码", SName = "院感", DataType = "String" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SYSParaNo.数据库版本号.Key, SYSParaNo.数据库版本号.Value);
            dic.Add(SYSParaNo.登录后升级数据库.Key, SYSParaNo.登录后升级数据库.Value);
            //dic.Add(SYSParaNo.实验室数据升级版本.Key, SYSParaNo.实验室数据升级版本.Value);

            dic.Add(SYSParaNo.CS服务访问URL.Key, SYSParaNo.CS服务访问URL.Value);
            dic.Add(SYSParaNo.集成平台服务访问URL.Key, SYSParaNo.集成平台服务访问URL.Value);

            dic.Add(SYSParaNo.启用用户UI配置.Key, SYSParaNo.启用用户UI配置.Value);
            dic.Add(SYSParaNo.列表默认分页记录数.Key, SYSParaNo.列表默认分页记录数.Value);

            dic.Add(SYSParaNo.完成评估后超过天数未归档.Key, SYSParaNo.完成评估后超过天数未归档.Value);
            dic.Add(SYSParaNo.指定自动核收检验小组.Key, SYSParaNo.指定自动核收检验小组.Value);
            dic.Add(SYSParaNo.指定自动核收默认检验者编码.Key, SYSParaNo.指定自动核收默认检验者编码.Value);
            return dic;
        }
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
    /// 模板/报表类型
    /// </summary>
    public static class ReaReportClass
    {
        public static KeyValuePair<string, BaseClassDicEntity> Frx = new KeyValuePair<string, BaseClassDicEntity>("Frx", new BaseClassDicEntity() { Id = "Frx", Name = "Frx", Code = "Frx", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> Excel = new KeyValuePair<string, BaseClassDicEntity>("Excel", new BaseClassDicEntity() { Id = "Excel", Name = "Excel", Code = "Excel", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaReportClass.Frx.Key, ReaReportClass.Frx.Value);
            dic.Add(ReaReportClass.Excel.Key, ReaReportClass.Excel.Value);
            return dic;
        }
    }

    /// <summary>
    /// 报表模板类型信息
    /// </summary>
    public static class BTemplateType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 医嘱申请 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "医嘱申请", Code = "BloodBReqForm", FontColor = "#ffffff", BGColor = "#1c8f36" });

        public static KeyValuePair<string, BaseClassDicEntity> 入库清单 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "入库清单", Code = "In", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 院感登记 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "院感登记", Code = "GKBarcode", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本接收 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "样本接收", Code = "SampleAcceptance", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 领用清单 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "领用清单", Code = "Consuming", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BTemplateType.医嘱申请.Key, BTemplateType.医嘱申请.Value);

            dic.Add(BTemplateType.入库清单.Key, BTemplateType.入库清单.Value);
            dic.Add(BTemplateType.院感登记.Key, BTemplateType.院感登记.Value);
            dic.Add(BTemplateType.样本接收.Key, BTemplateType.样本接收.Value);
            dic.Add(BTemplateType.领用清单.Key, BTemplateType.领用清单.Value);
            return dic;
        }
    }

    /// <summary>
    /// 身份类型,对应PUser表的usertype
    /// </summary>
    public static class BloodIdentityType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 检验技师 = new KeyValuePair<string, BaseClassDicEntity>("检验技师", new BaseClassDicEntity() { Id = "检验技师", Name = "检验技师", Code = "InspectionTechnician", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 医生 = new KeyValuePair<string, BaseClassDicEntity>("医生", new BaseClassDicEntity() { Id = "医生", Name = "医生", Code = "Doctors", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 护士 = new KeyValuePair<string, BaseClassDicEntity>("护士", new BaseClassDicEntity() { Id = "护士", Name = "护士", Code = "Nurse", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 护工 = new KeyValuePair<string, BaseClassDicEntity>("护工", new BaseClassDicEntity() { Id = "护工", Name = "护工", Code = "CareWorkers", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BloodIdentityType.检验技师.Key, BloodIdentityType.检验技师.Value);
            dic.Add(BloodIdentityType.医生.Key, BloodIdentityType.医生.Value);
            dic.Add(BloodIdentityType.护士.Key, BloodIdentityType.护士.Value);
            dic.Add(BloodIdentityType.护工.Key, BloodIdentityType.护工.Value);
            return dic;
        }
    }

    /// <summary>
    /// 院感登记报表类型
    /// </summary>
    public static class GKBarcodeReportType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 院感登记清单 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "院感登记清单", Code = "InspectionTechnician", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 院感标本送检清单 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "院感标本送检清单", Code = "Doctors", FontColor = "#ffffff", BGColor = "#f4c600" });

        public static KeyValuePair<string, BaseClassDicEntity> 环境卫生学监测报告 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "环境卫生学监测报告", Code = "Doctors", FontColor = "#ffffff", BGColor = "#f4c600" });

        public static KeyValuePair<string, BaseClassDicEntity> 按科室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "按科室", Code = "Nurse", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 按季度 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "按季度", Code = "CareWorkers", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 评价报告表 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "评价报告表", Code = "CareWorkers", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static KeyValuePair<string, BaseClassDicEntity> 环境卫生学监测报告按监测类型分组 = new KeyValuePair<string, BaseClassDicEntity>("30", new BaseClassDicEntity() { Id = "30", Name = "环境卫生学监测报告按监测类型分组", Code = "Doctors", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(GKBarcodeReportType.院感登记清单.Key, GKBarcodeReportType.院感登记清单.Value);
            dic.Add(GKBarcodeReportType.院感标本送检清单.Key, GKBarcodeReportType.院感标本送检清单.Value);
            dic.Add(GKBarcodeReportType.环境卫生学监测报告.Key, GKBarcodeReportType.环境卫生学监测报告.Value);
            dic.Add(GKBarcodeReportType.环境卫生学监测报告按监测类型分组.Key, GKBarcodeReportType.环境卫生学监测报告按监测类型分组.Value);

            dic.Add(GKBarcodeReportType.按科室.Key, GKBarcodeReportType.按科室.Value);
            dic.Add(GKBarcodeReportType.按季度.Key, GKBarcodeReportType.按季度.Value);
            dic.Add(GKBarcodeReportType.评价报告表.Key, GKBarcodeReportType.评价报告表.Value);
            return dic;
        }
    }

    /// <summary>
    /// 修改记录操作类型
    /// </summary>
    public static class UpdateOperationType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 人员修改记录 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "人员修改记录", Code = "EditPUser", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 医生修改记录 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "医生修改记录", Code = "EditDoctor", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室修改记录 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "科室修改记录", Code = "EditDepartment", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 输血过程记录基本信息 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "输血过程记录基本信息", Code = "EditBloodTransForm", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 病人体征记录项 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "病人体征记录项", Code = "EditTransfusionAntries", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 临床处理结果记录项 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "临床处理结果记录项", Code = "EditClinicalResults", FontColor = "#ffffff", BGColor = "#1c8f36" });

        public static KeyValuePair<string, BaseClassDicEntity> 临床处理结果描述记录项 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "临床处理结果描述记录项", Code = "EditClinicalResultsDesc", FontColor = "#ffffff", BGColor = "#1c8f36" });

        public static KeyValuePair<string, BaseClassDicEntity> 公共记录项类型 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "公共记录项类型", Code = "EditSCRecordType", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 公共记录项字典 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "公共记录项字典", Code = "EditSCRecordTypeItem", FontColor = "#ffffff", BGColor = "#1c8f36" });

        public static KeyValuePair<string, BaseClassDicEntity> 公共记录项短语 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "公共记录项短语", Code = "EditSCRecordPhrase", FontColor = "#ffffff", BGColor = "#1c8f36" });

        public static KeyValuePair<string, BaseClassDicEntity> 字典对照记录 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "字典对照记录", Code = "BloodInterfaceMaping", FontColor = "#ffffff", BGColor = "#1c8f36" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(UpdateOperationType.人员修改记录.Key, UpdateOperationType.人员修改记录.Value);
            dic.Add(UpdateOperationType.医生修改记录.Key, UpdateOperationType.医生修改记录.Value);
            dic.Add(UpdateOperationType.科室修改记录.Key, UpdateOperationType.科室修改记录.Value);
            dic.Add(UpdateOperationType.输血过程记录基本信息.Key, UpdateOperationType.输血过程记录基本信息.Value);
            dic.Add(UpdateOperationType.病人体征记录项.Key, UpdateOperationType.病人体征记录项.Value);
            dic.Add(UpdateOperationType.临床处理结果记录项.Key, UpdateOperationType.临床处理结果记录项.Value);
            dic.Add(UpdateOperationType.临床处理结果描述记录项.Key, UpdateOperationType.临床处理结果描述记录项.Value);

            dic.Add(UpdateOperationType.公共记录项类型.Key, UpdateOperationType.公共记录项类型.Value);
            dic.Add(UpdateOperationType.公共记录项字典.Key, UpdateOperationType.公共记录项字典.Value);
            dic.Add(UpdateOperationType.公共记录项短语.Key, UpdateOperationType.公共记录项短语.Value);
            dic.Add(UpdateOperationType.字典对照记录.Key, UpdateOperationType.字典对照记录.Value);
            return dic;
        }
    }

    /// <summary>
    /// 条码生成类型
    /// </summary>
    public static class SCBarCodeRulesType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 供货条码 = new KeyValuePair<string, BaseClassDicEntity>("CenSaleDtlBarCode", new BaseClassDicEntity() { Id = "CenSaleDtlBarCode", Name = "供货条码", Code = "CenSaleDtlBarCode", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存条码 = new KeyValuePair<string, BaseClassDicEntity>("QtyDtlBarCode", new BaseClassDicEntity() { Id = "QtyDtlBarCode", Name = "库存条码", Code = "QtyDtlBarCode", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 院感登记 = new KeyValuePair<string, BaseClassDicEntity>("GKSampleFormBarCode", new BaseClassDicEntity() { Id = "GKSampleFormBarCode", Name = "院感登记", Code = "GKSampleFormBarCode", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SCBarCodeRulesType.供货条码.Key, SCBarCodeRulesType.供货条码.Value);
            dic.Add(SCBarCodeRulesType.库存条码.Key, SCBarCodeRulesType.库存条码.Value);
            dic.Add(SCBarCodeRulesType.院感登记.Key, SCBarCodeRulesType.院感登记.Value);
            return dic;
        }
    }

    /// <summary>
    /// 院感登记样本状态
    /// </summary>
    public static class GKSampleFormStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已提交 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "已提交", Code = "Submitted", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 已核收 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已核收", Code = "Approved", FontColor = "#ffffff", BGColor = "#C0C000" });
        public static KeyValuePair<string, BaseClassDicEntity> 已检验 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "已检验", Code = "Inspected", FontColor = "#ffffff", BGColor = "#00C0C0" });
        public static KeyValuePair<string, BaseClassDicEntity> 已返结果 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "已返结果", Code = "ResultsReturned", FontColor = "#ffffff", BGColor = "#1c8f36" });

        public static KeyValuePair<string, BaseClassDicEntity> 已评价 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "已评价", Code = "Evaluated", FontColor = "#ffffff", BGColor = "#009688" });
        public static KeyValuePair<string, BaseClassDicEntity> 已归档 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "已归档", Code = "Archived", FontColor = "#ffffff", BGColor = "#1c8f36" });

        public static KeyValuePair<string, BaseClassDicEntity> 作废 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "作废", Code = "ApplyVoid", FontColor = "#ffffff", BGColor = "red" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(GKSampleFormStatus.暂存.Key, GKSampleFormStatus.暂存.Value);
            dic.Add(GKSampleFormStatus.已提交.Key, GKSampleFormStatus.已提交.Value);
            dic.Add(GKSampleFormStatus.已核收.Key, GKSampleFormStatus.已核收.Value);
            dic.Add(GKSampleFormStatus.已检验.Key, GKSampleFormStatus.已检验.Value);
            dic.Add(GKSampleFormStatus.已返结果.Key, GKSampleFormStatus.已返结果.Value);
            dic.Add(GKSampleFormStatus.已评价.Key, GKSampleFormStatus.已评价.Value);
            dic.Add(GKSampleFormStatus.已归档.Key, GKSampleFormStatus.已归档.Value);
            dic.Add(GKSampleFormStatus.作废.Key, GKSampleFormStatus.作废.Value);
            return dic;
        }
    }

    /// <summary>
    /// 院感登记评估结果
    /// </summary>
    public static class GKSampleFormJudgment
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未评估 = new KeyValuePair<string, BaseClassDicEntity>("-1", new BaseClassDicEntity() { Id = "-1", Name = "未评估", Code = "未评估", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 合格 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "合格", Code = "合格", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 不合格 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "不合格", Code = "不合格", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(GKSampleFormJudgment.未评估.Key, GKSampleFormJudgment.未评估.Value);
            dic.Add(GKSampleFormJudgment.合格.Key, GKSampleFormJudgment.合格.Value);
            dic.Add(GKSampleFormJudgment.不合格.Key, GKSampleFormJudgment.不合格.Value);
            return dic;
        }
    }
    /// <summary>
    /// 记录项类型所属类别
    /// </summary>
    public static class SCRecordTypeContentType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 院感登记 = new KeyValuePair<string, BaseClassDicEntity>("10000", new BaseClassDicEntity() { Id = "10000", Name = "院感登记", Code = "TransfusionAntries", FontColor = "#ffffff", BGColor = "green" });
        public static KeyValuePair<string, BaseClassDicEntity> 输血过程登记 = new KeyValuePair<string, BaseClassDicEntity>("20000", new BaseClassDicEntity() { Id = "20000", Name = "输血过程登记", Code = "AdverseReactions", FontColor = "#ffffff", BGColor = "orange" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SCRecordTypeContentType.院感登记.Key, SCRecordTypeContentType.院感登记.Value);
            dic.Add(SCRecordTypeContentType.输血过程登记.Key, SCRecordTypeContentType.输血过程登记.Value);

            return dic;
        }
    }
    /// <summary>
    /// 记录项类型所属类别
    /// </summary>
    public static class PhraseType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 公共 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "公共", Code = "Common", FontColor = "#ffffff", BGColor = "green" });
        public static KeyValuePair<string, BaseClassDicEntity> 按科室 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "按科室", Code = "OfDept", FontColor = "#ffffff", BGColor = "orange" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(PhraseType.公共.Key, PhraseType.公共.Value);
            dic.Add(PhraseType.按科室.Key, PhraseType.按科室.Value);

            return dic;
        }
    }
    /// <summary>
    /// 感控监测类型：1:感控监测;0:科室监测;
    /// </summary>
    public static class MonitorType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 感控监测 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "感控监测", Code = "OfMonitor", FontColor = "#ffffff", BGColor = "green" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室监测 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "科室监测", Code = "OfDept", FontColor = "#ffffff", BGColor = "orange" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(MonitorType.感控监测.Key, MonitorType.感控监测.Value);
            dic.Add(MonitorType.科室监测.Key, MonitorType.科室监测.Value);

            return dic;
        }
    }
    /// <summary>
    /// 系统配置类型
    /// </summary>
    public static class SysConfig
    {
        public static KeyValuePair<string, BaseClassDicEntity> HISSYS = new KeyValuePair<string, BaseClassDicEntity>("HISSYS", new BaseClassDicEntity() { Id = "HISSYS", Name = "HISSYS", Code = "HISSYS", FontColor = "#ffffff", BGColor = "green" });

        public static KeyValuePair<string, BaseClassDicEntity> LISSYS = new KeyValuePair<string, BaseClassDicEntity>("LISSYS", new BaseClassDicEntity() { Id = "LISSYS", Name = "LISSYS", Code = "LISSYS", FontColor = "#ffffff", BGColor = "green" });
        public static KeyValuePair<string, BaseClassDicEntity> GKSYS = new KeyValuePair<string, BaseClassDicEntity>("GKSYS", new BaseClassDicEntity() { Id = "GKSYS", Name = "GKSYS", Code = "GKSYS", FontColor = "#ffffff", BGColor = "orange" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SysConfig.HISSYS.Key, SysConfig.HISSYS.Value);
            dic.Add(SysConfig.LISSYS.Key, SysConfig.LISSYS.Value);
            dic.Add(SysConfig.GKSYS.Key, SysConfig.GKSYS.Value);

            return dic;
        }
    }

    /// <summary>
    /// LIS同步HIS数据的配置类型
    /// </summary>
    public static class LisSyncHisType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 百色市人民医院 = new KeyValuePair<string, BaseClassDicEntity>("BSSYMYY", new BaseClassDicEntity() { Id = "BSSYMYY", Name = "百色市人民医院", Code = "BSSYMYY", FontColor = "#ffffff", BGColor = "green" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(LisSyncHisType.百色市人民医院.Key, LisSyncHisType.百色市人民医院.Value);

            return dic;
        }
    }

    #endregion

}