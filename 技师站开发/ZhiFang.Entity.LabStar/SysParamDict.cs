using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ZhiFang.Entity.LabStar
{
    /// <summary>
    /// 系统参数字典类
    /// </summary>
    public class SysParamDict : BaseClassDicEntity
    {
        /// <summary>
        /// 所属系统
        /// </summary>
        public string BeLongSys { get; set; }
        /// <summary>
        /// 所属功能
        /// </summary>
        public string BeLongModuleInfo { get; set; }
        /// <summary>
        /// 所属分类(组)
        /// </summary>
        public BeLongClassification BeLongClassification { get; set; }

        //参数编码,使用父类的Code
        //参数名称,使用父类的Name
        //参数简称,使用父类的SName}
        //默认值,使用父类的DefaultValue

        /// <summary>
        /// 系统参数设置值
        /// </summary>
        public string ParaValue { get; set; }

        /// <summary>
        /// 显示次序
        /// </summary>
        public int DispOrder { get; set; }
        /// <summary>
        /// 参数相关业务字典
        /// </summary>
        public ParamDict ParamDict { get; set; }
        /// <summary>
        /// 配置人员
        /// </summary>
        public BaseClassDicEntity ConfigStaff { get; set; }
        /// <summary>
        /// 使用对象
        /// </summary>
        public BaseClassDicEntity UseObjects { get; set; }
        /// <summary>
        /// 配置控件类型
        /// </summary>
        public BaseClassDicEntity ConfigControlType { get; set; }
        /// <summary>
        /// 值类型
        /// </summary>
        public BaseClassDicEntity DataType { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 值长度
        /// </summary>
        public int DataLength { get; set; }
        /// <summary>
        /// 配置结果样例
        /// </summary>
        public string ConfigSample { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        //描述备注,使用父类的Memo

    }

    #region 系统参数辅助类
    /// <summary>
    /// 参数相关业务字典类
    /// </summary>
    public class ParamDict
    {
        /// <summary>
        /// 字典实体名称
        /// </summary>
        public string DictCName { get; set; }
        /// <summary>
        /// 字典数据源服务名称
        /// </summary>
        public string DictService { get; set; }
        /// <summary>
        /// 字典数据源服务名称
        /// </summary>
        public string DictFields { get; set; }
    }
    /// <summary>
    /// 参数所属分类(组)类
    /// </summary>
    public class BeLongClassification : BaseClassDicEntity
    {
        /// <summary>
        /// 显示次序
        /// </summary>
        public int DispOrder { get; set; }
    }
    #endregion

    #region 系统参数相关枚举字典类
    /// <summary>
    /// 配置人员
    /// </summary>
    public static class ConfigStaff
    {
        public static KeyValuePair<string, BaseClassDicEntity> 工程师 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "工程师", Code = "1", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 用户 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "用户", Code = "2", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(工程师.Key, 工程师.Value);
            dic.Add(用户.Key, 用户.Value);
            return dic;
        }
    }
    /// <summary>
    /// 使用对象
    /// </summary>
    public static class UseObjects
    {
        public static KeyValuePair<string, BaseClassDicEntity> 全局参数 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "全局参数", Code = "1", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 按站点类型 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "按站点类型", Code = "2", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 按检验小组 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "按检验小组", Code = "3", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 按人员 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "按人员", Code = "4", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(全局参数.Key, 全局参数.Value);
            dic.Add(按站点类型.Key, 按站点类型.Value);
            dic.Add(按检验小组.Key, 按检验小组.Value);
            dic.Add(按人员.Key, 按人员.Value);
            return dic;
        }
    }
    /// <summary>
    /// 配置控件类型
    /// </summary>
    public static class ConfigControlType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 枚举下拉单选 = new KeyValuePair<string, BaseClassDicEntity>("enumselectradio", new BaseClassDicEntity() { Id = "enumselectradio", Name = "枚举下拉单选", Code = "enumselectradio", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 枚举下拉多选 = new KeyValuePair<string, BaseClassDicEntity>("enumselectmultiple", new BaseClassDicEntity() { Id = "enumselectmultiple", Name = "枚举下拉多选", Code = "enumselectmultiple", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 字典下拉单选 = new KeyValuePair<string, BaseClassDicEntity>("dictselectradio", new BaseClassDicEntity() { Id = "dictselectradio", Name = "字典下拉单选", Code = "dictselectradio", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 字典下拉多选 = new KeyValuePair<string, BaseClassDicEntity>("dictselectmultiple", new BaseClassDicEntity() { Id = "dictselectmultiple", Name = "字典下拉多选", Code = "dictselectmultiple", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static KeyValuePair<string, BaseClassDicEntity> 文本框控件 = new KeyValuePair<string, BaseClassDicEntity>("text", new BaseClassDicEntity() { Id = "text", Name = "文本框控件", Code = "text", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 文本域控件 = new KeyValuePair<string, BaseClassDicEntity>("textarea", new BaseClassDicEntity() { Id = "textarea", Name = "文本域控件", Code = "textarea", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static KeyValuePair<string, BaseClassDicEntity> 日期框控件 = new KeyValuePair<string, BaseClassDicEntity>("date", new BaseClassDicEntity() { Id = "date", Name = "日期框控件", Code = "date", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 日期范围控件 = new KeyValuePair<string, BaseClassDicEntity>("daterange", new BaseClassDicEntity() { Id = "daterange", Name = "日期范围控件", Code = "daterange", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static KeyValuePair<string, BaseClassDicEntity> 枚举单选框 = new KeyValuePair<string, BaseClassDicEntity>("enumradio", new BaseClassDicEntity() { Id = "enumradio", Name = "枚举单选框", Code = "enumradio", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 开关控件 = new KeyValuePair<string, BaseClassDicEntity>("switch", new BaseClassDicEntity() { Id = "switch", Name = "开关控件", Code = "switch", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(枚举下拉单选.Key, 枚举下拉单选.Value);
            dic.Add(枚举下拉多选.Key, 枚举下拉多选.Value);
            dic.Add(字典下拉单选.Key, 字典下拉单选.Value);
            dic.Add(字典下拉多选.Key, 字典下拉多选.Value);

            dic.Add(文本框控件.Key, 文本框控件.Value);
            dic.Add(文本域控件.Key, 文本域控件.Value);
            dic.Add(日期框控件.Key, 日期框控件.Value);
            dic.Add(日期范围控件.Key, 日期范围控件.Value);

            dic.Add(枚举单选框.Key, 枚举单选框.Value);
            dic.Add(开关控件.Key, 开关控件.Value);
            return dic;
        }
    }
    /// <summary>
    /// 值类型
    /// </summary>
    public static class DataType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 数字类型 = new KeyValuePair<string, BaseClassDicEntity>("number", new BaseClassDicEntity() { Id = "number", Name = "数字类型", Code = "number", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 布尔类型 = new KeyValuePair<string, BaseClassDicEntity>("boolean", new BaseClassDicEntity() { Id = "boolean", Name = "布尔类型", Code = "boolean", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 日期类型 = new KeyValuePair<string, BaseClassDicEntity>("date", new BaseClassDicEntity() { Id = "date", Name = "日期类型", Code = "date", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 字典类型 = new KeyValuePair<string, BaseClassDicEntity>("dict", new BaseClassDicEntity() { Id = "dict", Name = "字典类型", Code = "dict", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static KeyValuePair<string, BaseClassDicEntity> 字符串类型 = new KeyValuePair<string, BaseClassDicEntity>("string", new BaseClassDicEntity() { Id = "text", Name = "字符串类型", Code = "string", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> JSON类型 = new KeyValuePair<string, BaseClassDicEntity>("json", new BaseClassDicEntity() { Id = "json", Name = "JSON类型", Code = "json", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(数字类型.Key, 数字类型.Value);
            dic.Add(布尔类型.Key, 布尔类型.Value);
            dic.Add(日期类型.Key, 日期类型.Value);
            dic.Add(字典类型.Key, 字典类型.Value);
            dic.Add(字符串类型.Key, 字符串类型.Value);
            dic.Add(JSON类型.Key, JSON类型.Value);
            return dic;
        }
    }
    #endregion

    #region 系统参数公共数据源

    public static class CommonDataSource
    {
        /// <summary>
        /// 返回选择值为0及选择值为1的数据源
        /// </summary>
        /// <param name="zeroText">值为0对应的文字描述</param>
        /// <param name="oneText">值为1对应的文字描述</param>
        /// <returns></returns>
        public static JArray GetZeroOne(string zeroText, string oneText)
        {
            JArray jDataSource = new JArray();
            JObject jObject1 = new JObject();
            jObject1.Add("Id", "1");
            jObject1.Add("Text", oneText);
            jDataSource.Add(jObject1);

            JObject jObject2 = new JObject();
            jObject2.Add("Id", "0");
            jObject2.Add("Text", zeroText);
            jDataSource.Add(jObject2);
            return jDataSource;
        }
        ///// <summary>
        ///// 0:不允许；1:允许
        ///// </summary>
        ///// <returns></returns>
        //public static JArray GetYesNo()
        //{
        //    JArray jDataSource = new JArray();
        //    JObject jObject1 = new JObject();
        //    jObject1.Add("Id", "1");
        //    jObject1.Add("CName", "允许");
        //    jDataSource.Add(jObject1);

        //    JObject jObject2 = new JObject();
        //    jObject2.Add("Id", "0");
        //    jObject2.Add("CName", "不允许");
        //    jDataSource.Add(jObject2);
        //    return jDataSource;
        //}
    }

    #endregion

    #region 分析前系统参数
    /// <summary>
    /// 分析前系统参数的所属分类(组)
    /// </summary>
    public static class PreBeLongClassification
    {
        public static KeyValuePair<string, BeLongClassification> 医嘱录入 = new KeyValuePair<string, BeLongClassification>("MedicalOrderEntry", new BeLongClassification() { Id = "MedicalOrderEntry", Name = "医嘱录入", Code = "MedicalOrderEntry", FontColor = "#ffffff", BGColor = "", DispOrder = 1, Memo = "" });
        public static KeyValuePair<string, BeLongClassification> 计费 = new KeyValuePair<string, BeLongClassification>("Billing", new BeLongClassification() { Id = "Billing", Name = "计费", Code = "Billing", FontColor = "#ffffff", BGColor = "", DispOrder = 10, Memo = "" });
        public static KeyValuePair<string, BeLongClassification> 条码打印 = new KeyValuePair<string, BeLongClassification>("BarCodePrint", new BeLongClassification() { Id = "BarCodePrint", Name = "样本采集", Code = "BarCodePrint", FontColor = "#ffffff", BGColor = "", DispOrder = 20, Memo = "" });
        public static KeyValuePair<string, BeLongClassification> 样本采集 = new KeyValuePair<string, BeLongClassification>("SampleCollection", new BeLongClassification() { Id = "SampleCollection", Name = "样本采集", Code = "SampleCollection", FontColor = "#ffffff", BGColor = "", DispOrder = 30, Memo = "" });
        public static KeyValuePair<string, BeLongClassification> 样本送检 = new KeyValuePair<string, BeLongClassification>("SampleInspection", new BeLongClassification() { Id = "SampleInspection", Name = "样本送检", Code = "SampleInspection", FontColor = "#ffffff", BGColor = "", DispOrder = 40, Memo = "" });
        public static KeyValuePair<string, BeLongClassification> 样本外送 = new KeyValuePair<string, BeLongClassification>("SampleDeliveryOfOut", new BeLongClassification() { Id = "SampleDeliveryOfOut", Name = "样本外送", Code = "SampleDeliveryOfOut", FontColor = "#ffffff", BGColor = "", DispOrder = 50, Memo = "" });
        public static KeyValuePair<string, BeLongClassification> 样本送达 = new KeyValuePair<string, BeLongClassification>("SampleDelivery", new BeLongClassification() { Id = "SampleDelivery", Name = "样本送达", Code = "SampleDelivery", FontColor = "#ffffff", BGColor = "", DispOrder = 60, Memo = "" });

        public static KeyValuePair<string, BeLongClassification> 样本签收 = new KeyValuePair<string, BeLongClassification>("SampleReceipt", new BeLongClassification() { Id = "SampleReceipt", Name = "样本签收", Code = "SampleReceipt", FontColor = "#ffffff", BGColor = "", DispOrder = 70, Memo = "" });
        public static KeyValuePair<string, BeLongClassification> 样本拒收 = new KeyValuePair<string, BeLongClassification>("SampleReject", new BeLongClassification() { Id = "SampleReject", Name = "样本拒收", Code = "SampleReject", FontColor = "#ffffff", BGColor = "", DispOrder = 80, Memo = "" });

        public static KeyValuePair<string, BeLongClassification> 样本分发 = new KeyValuePair<string, BeLongClassification>("SampleDistribution", new BeLongClassification() { Id = "SampleDistribution", Name = "样本分发", Code = "SampleDistribution", FontColor = "#ffffff", BGColor = "", DispOrder = 90, Memo = "" });
        public static Dictionary<string, BeLongClassification> GetStatusDic()
        {
            Dictionary<string, BeLongClassification> dic = new Dictionary<string, BeLongClassification>();
            dic.Add(医嘱录入.Key, 医嘱录入.Value);
            dic.Add(计费.Key, 计费.Value);
            dic.Add(条码打印.Key, 条码打印.Value);
            dic.Add(样本采集.Key, 样本采集.Value);
            dic.Add(样本送检.Key, 样本送检.Value);
            dic.Add(样本外送.Key, 样本外送.Value);
            dic.Add(样本送达.Key, 样本送达.Value);

            dic.Add(样本签收.Key, 样本签收.Value);
            dic.Add(样本拒收.Key, 样本拒收.Value);
            dic.Add(样本分发.Key, 样本分发.Value);
            return dic;
        }
    }
    /// <summary>
    /// 分析前系统参数字典类
    /// </summary>
    public static class PreSysParamDict
    {
        #region 各具体系统参数实现
        /// <summary>
        /// 计费方式
        /// </summary>
        /// <returns></returns>
        public static SysParamDict Get50024()
        {
            SysParamDict entity = new SysParamDict();

            entity.BeLongSys = "ZF_PRE";
            entity.BeLongModuleInfo = "计费";
            entity.BeLongClassification = PreBeLongClassification.计费.Value;
            entity.ConfigStaff = ConfigStaff.工程师.Value;
            entity.UseObjects = UseObjects.全局参数.Value;
            entity.ConfigControlType = ConfigControlType.枚举下拉单选.Value;
            entity.DataType = DataType.字符串类型.Value;
            entity.DataSource = CommonDataSource.GetZeroOne("按条码计费", "按项目计费").ToString().Replace(Environment.NewLine, "");

            entity.Id = "50024";
            entity.Code = "50024";
            entity.Name = "计费方式";
            entity.SName = "计费方式";
            entity.DefaultValue = "0";
            entity.DispOrder = 100;
            entity.DataLength = 1;
            entity.ConfigSample = "";
            entity.Icon = "";
            entity.Memo = "0：按条码计费；1：按项目计费";

            return entity;
        }
        /// <summary>
        /// 允许按子项分发
        /// </summary>
        /// <returns></returns>
        public static SysParamDict Get50022()
        {
            SysParamDict entity = new SysParamDict();

            entity.BeLongSys = "ZF_PRE";
            entity.BeLongModuleInfo = "分发";
            entity.BeLongClassification = PreBeLongClassification.样本分发.Value;
            entity.ConfigStaff = ConfigStaff.工程师.Value;
            entity.UseObjects = UseObjects.按站点类型.Value;
            entity.ConfigControlType = ConfigControlType.枚举下拉单选.Value;
            entity.DataType = DataType.字符串类型.Value;
            entity.DataSource = CommonDataSource.GetZeroOne("不允许", "允许").ToString().Replace(Environment.NewLine, "");

            entity.Id = "50022";
            entity.Code = "50022";
            entity.Name = "允许按子项分发";
            entity.SName = "允许按子项分发";
            entity.DefaultValue = "0";
            entity.DispOrder = 1000;
            entity.DataLength = 1;
            entity.ConfigSample = "";
            entity.Icon = "";
            entity.Memo = "0:不允许；1:允许";

            return entity;
        }

        #endregion

        public static KeyValuePair<string, SysParamDict> 计费方式 = new KeyValuePair<string, SysParamDict>(Get50024().Id, Get50024());
        public static KeyValuePair<string, SysParamDict> 允许按子项分发 = new KeyValuePair<string, SysParamDict>(Get50022().Id, Get50022());
        public static Dictionary<string, SysParamDict> GetStatusDic()
        {
            Dictionary<string, SysParamDict> dic = new Dictionary<string, SysParamDict>();

            dic.Add(计费方式.Key, 计费方式.Value);
            dic.Add(允许按子项分发.Key, 允许按子项分发.Value);

            return dic;
        }
    }

    #endregion

    #region 技师站系统参数

    #endregion
}
