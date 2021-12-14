using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Policy;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace ZhiFang.Entity.BloodTransfusion
{
    /// <summary>
    /// 系统运行参数静态类
    /// </summary>
    public static class RunParams
    {
        /// <summary>
        /// 系统运行参数集合信息
        /// </summary>
        public static JObject JObjectList;
    }

    public enum CodeValue
    {
        无 = 0,
        无法从Session中获取用户ID和名称 = 1001
    }
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
    /// 系统参数编码
    /// </summary>
    public static class SYSParaNo
    {
        public static KeyValuePair<string, BaseClassDicEntity> 数据库版本号 = new KeyValuePair<string, BaseClassDicEntity>("SYS_DBVersion", new BaseClassDicEntity() { Id = "SYS_DBVersion", Name = "数据库版本号", Code = "SYS_DBVersion", FontColor = "#ffffff", BGColor = "#f4c600", DefaultValue = "1.0.0.0", Memo = "", SName = "系统", DataType = "String" });
        public static KeyValuePair<string, BaseClassDicEntity> 登录后升级数据库 = new KeyValuePair<string, BaseClassDicEntity>("BL-SYSE-UDAL-0001", new BaseClassDicEntity() { Id = "BL-SYSE-UDAL-0001", Name = "登录后升级数据库", Code = "ISUpgradeDatabaseAfterLogin", FontColor = "#ffffff", BGColor = "#f4c600", DefaultValue = "1", Memo = "是否(0:否;1:是)", SName = "系统", DataType = "Boolean" });
        public static KeyValuePair<string, BaseClassDicEntity> 集成平台服务访问URL = new KeyValuePair<string, BaseClassDicEntity>("BL-SYSE-LURL-0002", new BaseClassDicEntity() { Id = "BL-SYSE-LURL-0002", Name = "集成平台服务访问URL", Code = "IPlatformServiceAccessURL", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "", Memo = "", SName = "集成平台", DataType = "String" });
        // public static KeyValuePair<string, BaseClassDicEntity> 实验室数据升级版本 = new KeyValuePair<string, BaseClassDicEntity>("BL-ULAB-DATA-0007", new BaseClassDicEntity() { Id = "BL-ULAB-DATA-0007", Name = "实验室数据升级版本", Code = "LAB_DBVersion", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "1.0.0.1", Memo = "实验室数据升级版本", SName = "系统", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> CS服务访问URL = new KeyValuePair<string, BaseClassDicEntity>("BL-SYSE-CSRL-0011", new BaseClassDicEntity() { Id = "BL-SYSE-CSRL-0011", Name = "CS服务访问URL", Code = "CSServiceAccessURL", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "http://localhost", Memo = "", SName = "CS接口", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> 列表默认分页记录数 = new KeyValuePair<string, BaseClassDicEntity>("BL-LRMP-UIPA-0007", new BaseClassDicEntity() { Id = "BL-LRMP-UIPA-0007", Name = "列表默认分页记录数", Code = "BLTFUIDefaultPageSize", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "50", Memo = "系统默认列表的分页数为50条记录每页,用户可自行设置,设置保存后需要重新登录才生效", SName = "UI", DataType = "String" });
        public static KeyValuePair<string, BaseClassDicEntity> 启用用户UI配置 = new KeyValuePair<string, BaseClassDicEntity>("BL-EUSE-UICF-0008", new BaseClassDicEntity() { Id = "BL-EUSE-UICF-0008", Name = "启用用户UI配置", Code = "EnableUserUIConfig", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "0", Memo = "1:是;0:否;", SName = "UI", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> 血袋接收是否需要护工完成取血确认 = new KeyValuePair<string, BaseClassDicEntity>("BL-BBAG-ISNC-0010", new BaseClassDicEntity() { Id = "BL-BBAG-ISNC-0010", Name = "血袋接收是否需要护工完成取血确认", Code = "BloodBagISNeedConfirmation", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "0", Memo = "1:是;0:否;", SName = "护士站", DataType = "String" });
        public static KeyValuePair<string, BaseClassDicEntity> 是否输血过程记录登记后才能血袋回收登记 = new KeyValuePair<string, BaseClassDicEntity>("BL-BBBO-BBRR-0012", new BaseClassDicEntity() { Id = "BL-BBBO-BBRR-0012", Name = "是否输血过程记录登记后才能血袋回收登记", Code = "RecycleIsHasTrans", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "1", Memo = "1:是;0:否;", SName = "护士站", DataType = "String" });
        public static KeyValuePair<string, BaseClassDicEntity> 更新输血过程登记时是否添加操作记录 = new KeyValuePair<string, BaseClassDicEntity>("BL-BBBO-BBTF-0013", new BaseClassDicEntity() { Id = "BL-BL-BBBO-BBTF-0013", Name = "更新输血过程登记时是否添加操作记录", Code = "UpdateTransIsAddOper", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "1", Memo = "1:是;0:否;", SName = "护士站", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> 输血申请单当前流水号 = new KeyValuePair<string, BaseClassDicEntity>("BL-BRQF-CURN-0009", new BaseClassDicEntity() { Id = "BL-BRQF-CURN-0009", Name = "输血申请单当前流水号", Code = "BloodBReqFormCurNo", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "0", Memo = "输血申请单当前流水号", SName = "医生站", DataType = "String" });
        public static KeyValuePair<string, BaseClassDicEntity> 获取几天内的LIS检验结果 = new KeyValuePair<string, BaseClassDicEntity>("BL-LISG-DAYS-0015", new BaseClassDicEntity() { Id = "BL-LISG-DAYS-0015", Name = "获取几天内的LIS检验结果", Code = "GET_LISRESULT_DAYS", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "7", Memo = "用血申请获取患者的多少天内LIS检验结果的配置", SName = "医生站", DataType = "String" });
        public static KeyValuePair<string, BaseClassDicEntity> LIS结果为空时默认值 = new KeyValuePair<string, BaseClassDicEntity>("BL-LISR-DEVL-0016", new BaseClassDicEntity() { Id = "BL-LISR-DEVL-0016", Name = "LIS结果为空时默认值", Code = "LIS_DEFAULT_ITEMSRESULT", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "检查中", Memo = "新增用血申请时,检验项目LIS结果为空时,设置的默认值", SName = "医生站", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> 用血申请传入的患者参数非空字段 = new KeyValuePair<string, BaseClassDicEntity>("BL-HISN-FIED-0017", new BaseClassDicEntity() { Id = "BL-HISN-FIED-0017", Name = "用血申请传入的患者参数非空字段", Code = "NONEMPTYFIELD", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "admId", Memo = "HIS调用时,用血申请传入的患者参数非空字段", SName = "HIS", DataType = "String" });
        public static KeyValuePair<string, BaseClassDicEntity> HIS调用时按传入信息自动建立科室人员关系 = new KeyValuePair<string, BaseClassDicEntity>("BL-ISAT-ADDDU-0014", new BaseClassDicEntity() { Id = "BL-ISAT-ADDDU-0014", Name = "HIS调用时按传入信息自动建立科室人员关系", Code = "IsAutoAddDepartmentUser", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "1", Memo = "1:是;0:否;", SName = "HIS", DataType = "String" });
        public static KeyValuePair<string, BaseClassDicEntity> 用血申请审核完成后是否返回给HIS = new KeyValuePair<string, BaseClassDicEntity>("BL-ISTO-HISR-0018", new BaseClassDicEntity() { Id = "BL-ISTO-HISR-0018", Name = "用血申请审核完成后是否返回给HIS", Code = "ISTOHISDATA", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "true", Memo = "用血申请审核完成后是否返回给HIS", SName = "HIS", DataType = "String" });
        public static KeyValuePair<string, BaseClassDicEntity> 申请作废时是否调用HIS作废接口 = new KeyValuePair<string, BaseClassDicEntity>("BL-ISTO-HISO-0019", new BaseClassDicEntity() { Id = "BL-ISTO-HISO-0019", Name = "申请作废时是否调用HIS作废接口", Code = "ISTOHISOBSOLETE", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "true", Memo = "申请作废时是否调用HIS作废接口", SName = "HIS", DataType = "String" });
        public static KeyValuePair<string, BaseClassDicEntity> 紧急用血是否在用血申请确认提交时上传HIS = new KeyValuePair<string, BaseClassDicEntity>("BL-ISTO-HISJ-0020", new BaseClassDicEntity() { Id = "BL-ISTO-HISJ-0020", Name = "紧急用血是否在用血申请确认提交时上传HIS", Code = "ISBUSETIMETYPEIDAUTOUPLOADADD", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "true", Memo = "紧急用血是否在用血申请确认提交时上传HIS", SName = "HIS", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> 是否允许手工选择患者ABO及患者Rh = new KeyValuePair<string, BaseClassDicEntity>("BL-NULL-ISBH-0021", new BaseClassDicEntity() { Id = "BL-NULL-ISBH-0021", Name = "是否允许手工选择患者ABO及患者Rh", Code = "ISALLOWPATABOANDRHOPT", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "false", Memo = "是否允许手工选择患者ABO及患者Rh", SName = "医生站", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> 血袋扫码识别字段 = new KeyValuePair<string, BaseClassDicEntity>("BL-BBSC-IDFT-0022", new BaseClassDicEntity() { Id = "BL-BBSC-IDFT-0022", Name = "血袋扫码识别字段", Code = "BloodBagScanCodeIDField", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "false", Memo = "血袋扫码识别字段", SName = "系统", DataType = "String" });

        public static KeyValuePair<string, BaseClassDicEntity> 输血登记是否需要交接登记完成后 = new KeyValuePair<string, BaseClassDicEntity>("BL-BLTF-ISNB-0023", new BaseClassDicEntity() { Id = "BL-BLTF-ISNB-0023", Name = "输血登记是否需要交接登记完成后", Code = "BloodTransISNeedBloodBag", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "1", Memo = "1:是;0:否;", SName = "护士站", DataType = "String" });

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

            dic.Add(SYSParaNo.血袋扫码识别字段.Key, SYSParaNo.血袋扫码识别字段.Value);
            dic.Add(SYSParaNo.血袋接收是否需要护工完成取血确认.Key, SYSParaNo.血袋接收是否需要护工完成取血确认.Value);
            dic.Add(SYSParaNo.是否输血过程记录登记后才能血袋回收登记.Key, SYSParaNo.是否输血过程记录登记后才能血袋回收登记.Value);
            dic.Add(SYSParaNo.更新输血过程登记时是否添加操作记录.Key, SYSParaNo.更新输血过程登记时是否添加操作记录.Value);

            dic.Add(SYSParaNo.输血申请单当前流水号.Key, SYSParaNo.输血申请单当前流水号.Value);
            dic.Add(SYSParaNo.获取几天内的LIS检验结果.Key, SYSParaNo.获取几天内的LIS检验结果.Value);
            dic.Add(SYSParaNo.LIS结果为空时默认值.Key, SYSParaNo.LIS结果为空时默认值.Value);
            dic.Add(SYSParaNo.是否允许手工选择患者ABO及患者Rh.Key, SYSParaNo.是否允许手工选择患者ABO及患者Rh.Value);

            dic.Add(SYSParaNo.HIS调用时按传入信息自动建立科室人员关系.Key, SYSParaNo.HIS调用时按传入信息自动建立科室人员关系.Value);
            dic.Add(SYSParaNo.用血申请传入的患者参数非空字段.Key, SYSParaNo.用血申请传入的患者参数非空字段.Value);
            dic.Add(SYSParaNo.用血申请审核完成后是否返回给HIS.Key, SYSParaNo.用血申请审核完成后是否返回给HIS.Value);
            dic.Add(SYSParaNo.申请作废时是否调用HIS作废接口.Key, SYSParaNo.申请作废时是否调用HIS作废接口.Value);
            dic.Add(SYSParaNo.紧急用血是否在用血申请确认提交时上传HIS.Key, SYSParaNo.紧急用血是否在用血申请确认提交时上传HIS.Value);
            dic.Add(SYSParaNo.输血登记是否需要交接登记完成后.Key, SYSParaNo.输血登记是否需要交接登记完成后.Value);

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
        public static KeyValuePair<string, BaseClassDicEntity> 血型复核 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "血型复核", Code = "Review", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本接收 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "样本接收", Code = "SampleAcceptance", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 交叉配血 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "交叉配血", Code = "Crossmatch", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static KeyValuePair<string, BaseClassDicEntity> 加工处理 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "加工处理", Code = "Crossmatch", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 发血清单 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "发血清单", Code = "Bleeding", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 领用清单 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "领用清单", Code = "Consuming", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 血袋接收 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "血袋接收", Code = "BloodBagReception", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static KeyValuePair<string, BaseClassDicEntity> 输血过程 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "输血过程", Code = "BloodTrans", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 血袋回收 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "血袋回收", Code = "BloodRecovery", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 血袋销毁 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "血袋销毁", Code = "BloodTrans", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static KeyValuePair<string, BaseClassDicEntity> 输血综合查询 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "输血综合查询", Code = "BloodTrans", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BTemplateType.医嘱申请.Key, BTemplateType.医嘱申请.Value);

            dic.Add(BTemplateType.入库清单.Key, BTemplateType.入库清单.Value);
            dic.Add(BTemplateType.血型复核.Key, BTemplateType.血型复核.Value);
            dic.Add(BTemplateType.样本接收.Key, BTemplateType.样本接收.Value);
            dic.Add(BTemplateType.交叉配血.Key, BTemplateType.交叉配血.Value);

            dic.Add(BTemplateType.加工处理.Key, BTemplateType.加工处理.Value);
            dic.Add(BTemplateType.发血清单.Key, BTemplateType.发血清单.Value);
            dic.Add(BTemplateType.领用清单.Key, BTemplateType.领用清单.Value);
            dic.Add(BTemplateType.血袋接收.Key, BTemplateType.血袋接收.Value);
            dic.Add(BTemplateType.输血过程.Key, BTemplateType.输血过程.Value);

            dic.Add(BTemplateType.血袋回收.Key, BTemplateType.血袋回收.Value);
            dic.Add(BTemplateType.血袋销毁.Key, BTemplateType.血袋销毁.Value);
            dic.Add(BTemplateType.输血综合查询.Key, BTemplateType.输血综合查询.Value);
            return dic;
        }
    }

    #endregion

    #region 统计分析

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

    #endregion\ 
    #endregion

    #region 血库系统字典

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
    /// 修改记录操作类型
    /// </summary>
    public static class UpdateOperationType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 人员修改记录 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "人员修改记录", Code = "EditPUser", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 医生修改记录 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "医生修改记录", Code = "EditDoctor", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 科室修改记录 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "科室修改记录", Code = "EditDepartment", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity>输血过程记录基本信息 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "输血过程记录基本信息", Code = "EditBloodTransForm", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 病人体征记录项 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "病人体征记录项", Code = "EditTransfusionAntries", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 临床处理结果记录项 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "临床处理结果记录项", Code = "EditClinicalResults", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 临床处理结果描述记录项 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "临床处理结果描述记录项", Code = "EditClinicalResultsDesc", FontColor = "#ffffff", BGColor = "#1c8f36" });
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
            return dic;
        }
    }
    #endregion

    #region 医生站字典
    /// <summary>
    /// 医生等级,对应Blood_docGrade
    /// </summary>
    public static class BloodDocGrade
    {
        public static KeyValuePair<string, BaseClassDicEntity> 申请医生 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "申请医生", Code = "apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 主治医师 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "主治医师", Code = "senior", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 科主任 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "科主任", Code = "director", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 医务科 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "医务科", Code = "medical", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BloodDocGrade.申请医生.Key, BloodDocGrade.申请医生.Value);
            dic.Add(BloodDocGrade.主治医师.Key, BloodDocGrade.主治医师.Value);
            dic.Add(BloodDocGrade.科主任.Key, BloodDocGrade.科主任.Value);
            dic.Add(BloodDocGrade.医务科.Key, BloodDocGrade.医务科.Value);
            return dic;
        }
    }

    /// <summary>
    /// 申请ABO血型
    /// </summary>
    public static class HisABOCode
    {
        public static KeyValuePair<string, BaseClassDicEntity> A = new KeyValuePair<string, BaseClassDicEntity>("A", new BaseClassDicEntity() { Id = "A", Name = "A", Code = "A", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> B = new KeyValuePair<string, BaseClassDicEntity>("B", new BaseClassDicEntity() { Id = "B", Name = "B", Code = "B", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> AB = new KeyValuePair<string, BaseClassDicEntity>("AB", new BaseClassDicEntity() { Id = "AB", Name = "AB", Code = "AB", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> O = new KeyValuePair<string, BaseClassDicEntity>("O", new BaseClassDicEntity() { Id = "O", Name = "O", Code = "O", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(HisABOCode.A.Key, HisABOCode.A.Value);
            dic.Add(HisABOCode.B.Key, HisABOCode.B.Value);
            dic.Add(HisABOCode.AB.Key, HisABOCode.AB.Value);
            dic.Add(HisABOCode.O.Key, HisABOCode.O.Value);
            return dic;
        }
    }
    /// <summary>
    /// 申请的RhD血型
    /// </summary>
    public static class HisRhCode
    {
        public static KeyValuePair<string, BaseClassDicEntity> 阳性 = new KeyValuePair<string, BaseClassDicEntity>("阳性", new BaseClassDicEntity() { Id = "阳性", Name = "阳性", Code = "Positive", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 阴性 = new KeyValuePair<string, BaseClassDicEntity>("阴性", new BaseClassDicEntity() { Id = "阴性", Name = "阴性", Code = "Negative", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(HisRhCode.阳性.Key, HisRhCode.阳性.Value);
            dic.Add(HisRhCode.阴性.Key, HisRhCode.阴性.Value);
            return dic;
        }
    }

    /// <summary>
    /// 医嘱申请单状态
    /// </summary>
    public static class BreqFormStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 医嘱暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "医嘱暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 提交申请 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "提交申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 上级审核通过 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "上级审核通过", Code = "SeniorConfirm", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 上级审核退回 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "上级审核退回", Code = "SeniorCancel", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 科主任审核通过 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "科主任审核通过", Code = "DirectorConfirm", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 科主任审核退回 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "科主任审核退回", Code = "DirectorCancel", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 医务处审批通过 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "医务处审批通过", Code = "Approval", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 医务处审批退回 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "医务处审批退回", Code = "UnApproval", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 审批完成 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "审批完成", Code = "ApprovalCompleted", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 医嘱作废 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "医嘱作废", Code = "Obsolete", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BreqFormStatus.医嘱暂存.Key, BreqFormStatus.医嘱暂存.Value);
            dic.Add(BreqFormStatus.提交申请.Key, BreqFormStatus.提交申请.Value);
            dic.Add(BreqFormStatus.上级审核通过.Key, BreqFormStatus.上级审核通过.Value);
            dic.Add(BreqFormStatus.上级审核退回.Key, BreqFormStatus.上级审核退回.Value);
            dic.Add(BreqFormStatus.医务处审批通过.Key, BreqFormStatus.医务处审批通过.Value);
            dic.Add(BreqFormStatus.医务处审批退回.Key, BreqFormStatus.医务处审批退回.Value);
            dic.Add(BreqFormStatus.科主任审核通过.Key, BreqFormStatus.科主任审核通过.Value);
            dic.Add(BreqFormStatus.科主任审核退回.Key, BreqFormStatus.科主任审核退回.Value);
            dic.Add(BreqFormStatus.审批完成.Key, BreqFormStatus.审批完成.Value);
            dic.Add(BreqFormStatus.医嘱作废.Key, BreqFormStatus.医嘱作废.Value);
            return dic;
        }
    }
    /// <summary>
    /// 医嘱申请的项目结果对照码-停用
    /// </summary>
    public static class BreqFormReqCode
    {
        public static KeyValuePair<string, BaseClassDicEntity> 白蛋白 = new KeyValuePair<string, BaseClassDicEntity>("LisALB", new BaseClassDicEntity() { Id = "LisALB", DispOrder = "1", Name = "白蛋白", Code = "LisALB", FontColor = "#ffffff", BGColor = "#8B4513" });
        public static KeyValuePair<string, BaseClassDicEntity> 丙氨酸氨基转移酶 = new KeyValuePair<string, BaseClassDicEntity>("LisALT", new BaseClassDicEntity() { Id = "LisALT", DispOrder = "2", Name = "丙氨酸氨基转移酶", Code = "LisALT", FontColor = "#ffffff", BGColor = "#A0522D" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分凝血酶原时间 = new KeyValuePair<string, BaseClassDicEntity>("LisAPTT", new BaseClassDicEntity() { Id = "LisAPTT", DispOrder = "3", Name = "部分凝血酶原时间", Code = "LisAPTT", FontColor = "#ffffff", BGColor = "#FFA07A" });
        public static KeyValuePair<string, BaseClassDicEntity> 纤维蛋白原 = new KeyValuePair<string, BaseClassDicEntity>("LisFbg", new BaseClassDicEntity() { Id = "LisFbg", DispOrder = "4", Name = "纤维蛋白原", Code = "LisFbg", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 乙肝抗体c = new KeyValuePair<string, BaseClassDicEntity>("LisHBc", new BaseClassDicEntity() { Id = "LisHBc", DispOrder = "5", Name = "乙肝抗体c", Code = "LisHBc", FontColor = "#ffffff", BGColor = "#DEB887" });

        public static KeyValuePair<string, BaseClassDicEntity> 乙肝抗体e = new KeyValuePair<string, BaseClassDicEntity>("LisHBe", new BaseClassDicEntity() { Id = "LisHBe", DispOrder = "6", Name = "乙肝抗体e", Code = "LisHBe", FontColor = "#ffffff", BGColor = "#CD853F" });
        public static KeyValuePair<string, BaseClassDicEntity> 乙肝抗原 = new KeyValuePair<string, BaseClassDicEntity>("LisHBeAg", new BaseClassDicEntity() { Id = "LisHBeAg", DispOrder = "7", Name = "乙肝抗原", Code = "LisHBeAg", FontColor = "#ffffff", BGColor = "#F4A460" });
        public static KeyValuePair<string, BaseClassDicEntity> 乙肝抗体s = new KeyValuePair<string, BaseClassDicEntity>("LisHBs", new BaseClassDicEntity() { Id = "LisHBs", DispOrder = "8", Name = "乙肝抗体s", Code = "LisHBs", FontColor = "#ffffff", BGColor = "#D2691E" });
        public static KeyValuePair<string, BaseClassDicEntity> 乙肝表面抗原 = new KeyValuePair<string, BaseClassDicEntity>("LisHBsAg", new BaseClassDicEntity() { Id = "LisHBsAg", DispOrder = "9", Name = "乙肝表面抗原", Code = "LisHBsAg", FontColor = "#ffffff", BGColor = "#E9967A" });
        public static KeyValuePair<string, BaseClassDicEntity> 红细胞压积 = new KeyValuePair<string, BaseClassDicEntity>("LisHCT", new BaseClassDicEntity() { Id = "LisHCT", DispOrder = "10", Name = "红细胞压积", Code = "LisHCT", FontColor = "#ffffff", BGColor = "#BC8F8F" });

        public static KeyValuePair<string, BaseClassDicEntity> 丙型肝炎病毒抗体 = new KeyValuePair<string, BaseClassDicEntity>("LisHCV", new BaseClassDicEntity() { Id = "LisHCV", DispOrder = "11", Name = "丙型肝炎病毒抗体", Code = "LisHCV", FontColor = "#ffffff", BGColor = "#FF7F50" });
        public static KeyValuePair<string, BaseClassDicEntity> 血红蛋白浓度 = new KeyValuePair<string, BaseClassDicEntity>("LisHGB", new BaseClassDicEntity() { Id = "LisHGB", DispOrder = "12", Name = "血红蛋白浓度", Code = "LisHGB", FontColor = "#ffffff", BGColor = "#CD5C5C" });
        public static KeyValuePair<string, BaseClassDicEntity> HIV = new KeyValuePair<string, BaseClassDicEntity>("LisHIV", new BaseClassDicEntity() { Id = "LisHIV", DispOrder = "13", Name = "HIV", Code = "LisHIV", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 血小板 = new KeyValuePair<string, BaseClassDicEntity>("LisPLT", new BaseClassDicEntity() { Id = "LisPLT", DispOrder = "14", Name = "血小板", Code = "LisPLT", FontColor = "#ffffff", BGColor = "#FF4500" });
        public static KeyValuePair<string, BaseClassDicEntity> 凝血酶原时间 = new KeyValuePair<string, BaseClassDicEntity>("LisPT", new BaseClassDicEntity() { Id = "LisPT", DispOrder = "15", Name = "凝血酶原时间", Code = "LisPT", FontColor = "#ffffff", BGColor = "#FF4500" });

        public static KeyValuePair<string, BaseClassDicEntity> 红细胞 = new KeyValuePair<string, BaseClassDicEntity>("LisRBC", new BaseClassDicEntity() { Id = "LisRBC", DispOrder = "16", Name = "红细胞", Code = "LisRBC", FontColor = "#ffffff", BGColor = "#CD5C5C" });
        public static KeyValuePair<string, BaseClassDicEntity> 梅毒 = new KeyValuePair<string, BaseClassDicEntity>("LisRPR", new BaseClassDicEntity() { Id = "LisRPR", DispOrder = "17", Name = "梅毒", Code = "LisRPR", FontColor = "#ffffff", BGColor = "#D2B48C" });
        public static KeyValuePair<string, BaseClassDicEntity> 凝血酶时间 = new KeyValuePair<string, BaseClassDicEntity>("LisTT", new BaseClassDicEntity() { Id = "LisTT", DispOrder = "18", Name = "凝血酶时间", Code = "LisTT", FontColor = "#ffffff", BGColor = "#FF4500" });
        public static KeyValuePair<string, BaseClassDicEntity> 白细胞 = new KeyValuePair<string, BaseClassDicEntity>("LisWBC", new BaseClassDicEntity() { Id = "LisWBC", DispOrder = "19", Name = "白细胞", Code = "LisWBC", FontColor = "#ffffff", BGColor = "#FF4500" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BreqFormReqCode.白蛋白.Key, BreqFormReqCode.白蛋白.Value);
            dic.Add(BreqFormReqCode.丙氨酸氨基转移酶.Key, BreqFormReqCode.丙氨酸氨基转移酶.Value);
            dic.Add(BreqFormReqCode.部分凝血酶原时间.Key, BreqFormReqCode.部分凝血酶原时间.Value);
            dic.Add(BreqFormReqCode.纤维蛋白原.Key, BreqFormReqCode.纤维蛋白原.Value);
            dic.Add(BreqFormReqCode.乙肝抗体c.Key, BreqFormReqCode.乙肝抗体c.Value);

            dic.Add(BreqFormReqCode.乙肝抗体e.Key, BreqFormReqCode.乙肝抗体e.Value);
            dic.Add(BreqFormReqCode.乙肝抗原.Key, BreqFormReqCode.乙肝抗原.Value);
            dic.Add(BreqFormReqCode.乙肝抗体s.Key, BreqFormReqCode.乙肝抗体s.Value);
            dic.Add(BreqFormReqCode.乙肝表面抗原.Key, BreqFormReqCode.乙肝表面抗原.Value);
            dic.Add(BreqFormReqCode.红细胞压积.Key, BreqFormReqCode.红细胞压积.Value);

            dic.Add(BreqFormReqCode.丙型肝炎病毒抗体.Key, BreqFormReqCode.丙型肝炎病毒抗体.Value);
            dic.Add(BreqFormReqCode.血红蛋白浓度.Key, BreqFormReqCode.血红蛋白浓度.Value);
            dic.Add(BreqFormReqCode.HIV.Key, BreqFormReqCode.HIV.Value);
            dic.Add(BreqFormReqCode.血小板.Key, BreqFormReqCode.血小板.Value);
            dic.Add(BreqFormReqCode.凝血酶原时间.Key, BreqFormReqCode.凝血酶原时间.Value);

            dic.Add(BreqFormReqCode.红细胞.Key, BreqFormReqCode.红细胞.Value);
            dic.Add(BreqFormReqCode.梅毒.Key, BreqFormReqCode.梅毒.Value);
            dic.Add(BreqFormReqCode.凝血酶时间.Key, BreqFormReqCode.凝血酶时间.Value);
            dic.Add(BreqFormReqCode.白细胞.Key, BreqFormReqCode.白细胞.Value);

            return dic;
        }
    }
    /// <summary>
    /// 医嘱申请HIS数据标志
    /// </summary>
    public static class BreqFormReqToHisFlag
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未处理 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "未处理", Code = "0", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 上传成功 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "上传成功", Code = "1", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 上传失败 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "上传失败", Code = "2", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BreqFormReqToHisFlag.未处理.Key, BreqFormReqToHisFlag.未处理.Value);
            dic.Add(BreqFormReqToHisFlag.上传成功.Key, BreqFormReqToHisFlag.上传成功.Value);
            dic.Add(BreqFormReqToHisFlag.上传失败.Key, BreqFormReqToHisFlag.上传失败.Value);
            return dic;
        }
    }
    /// <summary>
    /// 医嘱申请输血科审核标志
    /// </summary>
    public static class BReqFormFlag
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未受理 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "未受理", Code = "0", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 受理通过 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "受理通过", Code = "1", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 受理不通过 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "受理不通过", Code = "2", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BReqFormFlag.未受理.Key, BReqFormFlag.未受理.Value);
            dic.Add(BReqFormFlag.受理通过.Key, BReqFormFlag.受理通过.Value);
            dic.Add(BReqFormFlag.受理不通过.Key, BReqFormFlag.受理不通过.Value);
            return dic;
        }
    }

    #endregion

    #region 护士站字典
    /// <summary>
    /// 血袋登记操作类型
    /// </summary>
    public static class BloodBagOperationType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 领用确认登记 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "领用确认登记", Code = "AcceptanceConfirmation", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 交接登记 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "交接登记", Code = "HandoverRegistration", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 回收登记 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "回收登记", Code = "RecyclingRegistration", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BloodBagOperationType.领用确认登记.Key, BloodBagOperationType.领用确认登记.Value);
            dic.Add(BloodBagOperationType.交接登记.Key, BloodBagOperationType.交接登记.Value);
            dic.Add(BloodBagOperationType.回收登记.Key, BloodBagOperationType.回收登记.Value);
            return dic;
        }
    }
    /// <summary>
    /// 血袋登记交接类型
    /// </summary>
    public static class BloodBagOperResultType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 确认接收 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "确认接收", Code = "ConfirmReceipt", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 血袋退回 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "血袋退回", Code = "BloodBagReturn", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BloodBagOperResultType.确认接收.Key, BloodBagOperResultType.确认接收.Value);
            dic.Add(BloodBagOperResultType.血袋退回.Key, BloodBagOperResultType.血袋退回.Value);
            return dic;
        }
    }
    /// <summary>
    /// 输血过程内容分类
    /// </summary>
    public static class ClassificationOfTransfusionContent
    {
        public static KeyValuePair<string, BaseClassDicEntity> 输血记录项 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "输血记录项", Code = "TransfusionAntries", FontColor = "#ffffff", BGColor = "green" });
        public static KeyValuePair<string, BaseClassDicEntity> 不良反应分类 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "不良反应分类", Code = "AdverseReactions", FontColor = "#ffffff", BGColor = "orange" });
        public static KeyValuePair<string, BaseClassDicEntity> 临床处理措施 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "临床处理措施", Code = "ClinicalMeasures", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static KeyValuePair<string, BaseClassDicEntity> 不良反应选择项 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "不良反应选择项", Code = "AdverseReactionOptions", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static KeyValuePair<string, BaseClassDicEntity> 临床处理结果 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "临床处理结果", Code = "ClinicalResults", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static KeyValuePair<string, BaseClassDicEntity> 临床处理结果描述 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "临床处理结果描述", Code = "ClinicalResultsDesc ", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ClassificationOfTransfusionContent.输血记录项.Key, ClassificationOfTransfusionContent.输血记录项.Value);
            dic.Add(ClassificationOfTransfusionContent.不良反应分类.Key, ClassificationOfTransfusionContent.不良反应分类.Value);
            dic.Add(ClassificationOfTransfusionContent.临床处理措施.Key, ClassificationOfTransfusionContent.临床处理措施.Value);

            dic.Add(ClassificationOfTransfusionContent.不良反应选择项.Key, ClassificationOfTransfusionContent.不良反应选择项.Value);
            dic.Add(ClassificationOfTransfusionContent.临床处理结果.Key, ClassificationOfTransfusionContent.临床处理结果.Value);
            dic.Add(ClassificationOfTransfusionContent.临床处理结果描述.Key, ClassificationOfTransfusionContent.临床处理结果描述.Value);

            return dic;
        }
    }
    /// <summary>
    /// 领用确认完成度
    /// </summary>
    public static class ConfirmCompletion
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未领用 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "未领用", Code = "NotUsed", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分领用 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "部分领用", Code = "PartiallyUsed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 全部领用 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "全部领用", Code = "TakeAll", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ConfirmCompletion.未领用.Key, ConfirmCompletion.未领用.Value);
            dic.Add(ConfirmCompletion.部分领用.Key, ConfirmCompletion.部分领用.Value);
            dic.Add(ConfirmCompletion.全部领用.Key, ConfirmCompletion.全部领用.Value);
            return dic;
        }
    }
    /// <summary>
    /// 交接登记完成度
    /// </summary>
    public static class HandoverCompletion
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未交接 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "未交接", Code = "NotHandedOver", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分交接 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "部分交接", Code = "PartialTransfer", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 交接完成 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "交接完成", Code = "TransferCompleted", FontColor = "#ffffff", BGColor = "#00BFFF" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(HandoverCompletion.未交接.Key, HandoverCompletion.未交接.Value);
            dic.Add(HandoverCompletion.部分交接.Key, HandoverCompletion.部分交接.Value);
            dic.Add(HandoverCompletion.交接完成.Key, HandoverCompletion.交接完成.Value);
            return dic;
        }
    }
    /// <summary>
    /// 输血过程登记完成度
    /// 已登记:表示发血血袋或发血主单已进行过输血过程登记,但是否完成输血过程登记未清楚
    /// </summary>
    public static class CourseCompletion
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未登记 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "未登记", Code = "NotRegistration", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已登记 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已登记", Code = "PartialRegistration", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 登记完成 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "登记完成", Code = "RegistrationCompleted", FontColor = "#ffffff", BGColor = "#00BFFF" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(CourseCompletion.未登记.Key, CourseCompletion.未登记.Value);
            dic.Add(CourseCompletion.已登记.Key, CourseCompletion.已登记.Value);
            dic.Add(CourseCompletion.登记完成.Key, CourseCompletion.登记完成.Value);
            return dic;
        }
    }
    /// <summary>
    /// 回收登记完成度
    /// </summary>
    public static class RecoverCompletion
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未回收 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "未回收", Code = "NotRecycled", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分回收 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "部分回收", Code = "PartialRecycling", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 回收完成 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "回收完成", Code = "RecycleAll", FontColor = "#ffffff", BGColor = "#00BFFF" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(RecoverCompletion.未回收.Key, RecoverCompletion.未回收.Value);
            dic.Add(RecoverCompletion.部分回收.Key, RecoverCompletion.部分回收.Value);
            dic.Add(RecoverCompletion.回收完成.Key, RecoverCompletion.回收完成.Value);
            return dic;
        }
    }
    #endregion

    /// <summary>
    /// 血袋跟踪类型
    /// </summary>
    public static class BloodBagTrackingType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 入库 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "入库", Code = "In", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 血型复核 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "血型复核", Code = "Review", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 申请 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "申请", Code = "Req", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本接收 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "样本接收", Code = "SampleAcceptance", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 交叉配血 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "交叉配血", Code = "Crossmatch", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static KeyValuePair<string, BaseClassDicEntity> 加工处理 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "加工处理", Code = "Crossmatch", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 发血 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "发血", Code = "Bleeding", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 领用 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "领用", Code = "Consuming", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 血袋接收 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "血袋接收", Code = "BloodBagReception", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static KeyValuePair<string, BaseClassDicEntity> 输血过程 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "输血过程", Code = "BloodTrans", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 血袋回收 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "血袋回收", Code = "BloodRecovery", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 血袋销毁 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "血袋销毁", Code = "BloodTrans", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BloodBagTrackingType.入库.Key, BloodBagTrackingType.入库.Value);
            dic.Add(BloodBagTrackingType.血型复核.Key, BloodBagTrackingType.血型复核.Value);
            dic.Add(BloodBagTrackingType.申请.Key, BloodBagTrackingType.申请.Value);
            dic.Add(BloodBagTrackingType.样本接收.Key, BloodBagTrackingType.样本接收.Value);
            dic.Add(BloodBagTrackingType.交叉配血.Key, BloodBagTrackingType.交叉配血.Value);

            dic.Add(BloodBagTrackingType.加工处理.Key, BloodBagTrackingType.加工处理.Value);
            dic.Add(BloodBagTrackingType.发血.Key, BloodBagTrackingType.发血.Value);
            dic.Add(BloodBagTrackingType.领用.Key, BloodBagTrackingType.领用.Value);
            dic.Add(BloodBagTrackingType.血袋接收.Key, BloodBagTrackingType.血袋接收.Value);
            dic.Add(BloodBagTrackingType.输血过程.Key, BloodBagTrackingType.输血过程.Value);

            dic.Add(BloodBagTrackingType.输血过程.Key, BloodBagTrackingType.输血过程.Value);
            dic.Add(BloodBagTrackingType.血袋回收.Key, BloodBagTrackingType.血袋回收.Value);
            dic.Add(BloodBagTrackingType.血袋销毁.Key, BloodBagTrackingType.血袋销毁.Value);
            return dic;
        }
    }

    /// <summary>
    /// 发库明细的OutFlag
    /// </summary>
    public static class BloodBOutItemOutFlag
    {
        public static KeyValuePair<string, BaseClassDicEntity> 退库 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "退库", Code = "In", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "出库", Code = "Out", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BloodBOutItemOutFlag.退库.Key, BloodBOutItemOutFlag.退库.Value);
            dic.Add(BloodBOutItemOutFlag.出库.Key, BloodBOutItemOutFlag.出库.Value);
            return dic;
        }
    }
}