using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ZhiFang.Common.Public;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZhiFang.Digitlab.Entity
{

    [DataContract]
    [DataDesc(CName = "自定义质控数据", ClassCName = "QCDataCustom", ShortCode = "QCDataCustom", Desc = "自定义质控数据")]
    public class QCDataCustom : BaseEntity
    {
        public QCDataCustom() { }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public override long Id  { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器ID", ShortCode = "ZKYIId", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long EquipID { get; set; }
        [DataMember]
        [DataDesc(CName = "仪器名称", ShortCode = "ZKYIMC", Desc = "仪器名称", ContextType = SysDic.All, Length = 40)]
        public virtual string EquipName { get; set; }
        [DataMember]
        [DataDesc(CName = "仪器简称", ShortCode = "EquipSName", Desc = "仪器简称", ContextType = SysDic.All, Length = 40)]
        public virtual string EquipSName { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验项目ID", ShortCode = "ItemID", Desc = "检验项目ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long ItemID { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "质控项目ID", ShortCode = "ZKXMId", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long QCItemID { get; set; }
        [DataMember]
        [DataDesc(CName = "质控项目名称", ShortCode = "ZKXMMC", Desc = "质控项目名称", ContextType = SysDic.All, Length = 40)]
        public virtual string QCItemName { get; set; }
        [DataMember]
        [DataDesc(CName = "质控项目简称", ShortCode = "ZKXMJC", Desc = "质控项目简称", ContextType = SysDic.All, Length = 40)]
        public virtual string QCItemSName { get; set; }
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataDesc(CName = "质控项目类型", ShortCode = "ZKXMLX", Desc = "质控项目类型", ContextType = SysDic.Number, Length = 4)]
        public virtual QCValueTypeEnum? QCItemValueType { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "质控项目时间戳", ShortCode = "ZKXMSJC", Desc = "质控项目时间戳", ContextType = SysDic.All, Length = 8)]
        public virtual byte[] QCItemDataTimeStamp { get; set; }
        [DataMember]
        [DataDesc(CName = "质控项目显示次序", ShortCode = "QCItemDispOrder", Desc = "质控项目显示次序", ContextType = SysDic.Number, Length = 4)]
        public virtual int QCItemDispOrder { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "质控物ID", ShortCode = "ZKWId", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long QCMatID { get; set; }
        [DataMember]
        [DataDesc(CName = "质控物名称", ShortCode = "ZKWMC", Desc = "质控物名称", ContextType = SysDic.All, Length = 40)]
        public virtual string QCMatName { get; set; }
        [DataMember]
        [DataDesc(CName = "质控物简称", ShortCode = "QCMatSName", Desc = "质控物简称", ContextType = SysDic.All, Length = 40)]
        public virtual string QCMatSName { get; set; }
        [DataMember]
        [DataDesc(CName = "质控物显示次序", ShortCode = "QCMatDispOrder", Desc = "质控物显示次序", ContextType = SysDic.Number, Length = 4)]
        public virtual int QCMatDispOrder { get; set; }

        [DataMember]
        [DataDesc(CName = "质控结果", ShortCode = "ZKJG", Desc = "质控结果", ContextType = SysDic.All, Length = 40)]
        public virtual string ReportValue { get; set; }

        [DataMember]
        [DataDesc(CName = "仪器原始数值", ShortCode = "OriglValue", Desc = "仪器原始数值", ContextType = SysDic.All, Length = 8)]
        public virtual string OriglValue { get; set; }

        [DataMember]
        [DataDesc(CName = "是否仪器结果", ShortCode = "IsEquipResult", Desc = "是否仪器结果", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsEquipResult { get; set; }

        [DataMember]  //计算方法=绝对值（用户值-靶值）/靶值 * 100 
        [DataDesc(CName = "变异系数", ShortCode = "JSBYXS", Desc = "变异系数", ContextType = SysDic.Number, Length = 8)]
        public virtual string CVValue { get; set; }

        [DataMember]
        [DataDesc(CName = "质控批次", ShortCode = "ZKPC", Desc = "质控批次", ContextType = SysDic.Number, Length = 4)]
        public virtual int QCDataLotNo { get; set; }

        [DataMember]
        [DataDesc(CName = "质控总批次", ShortCode = "QCDataTotalBatch", Desc = "质控总批次", ContextType = SysDic.Number, Length = 4)]
        public virtual int QCDataTotalBatch { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "质控时间", ShortCode = "ZKSJ", Desc = "质控时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? ReceiveTime { get; set; }

        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataDesc(CName = "是否失控", ShortCode = "SFSK", Desc = "是否失控", ContextType = SysDic.Number, Length = 4)]
        public virtual QCValueIsControlEnum? IsControl { get; set; }

        [DataMember]
        [DataDesc(CName = "失控规则", ShortCode = "SKGZ", Desc = "失控规则", ContextType = SysDic.All, Length = 40)]
        public virtual string QCControlInfo { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验人ID", ShortCode = "JYRID", Desc = "检验人ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long OperatorID { get; set; }
        [DataMember]
        [DataDesc(CName = "检验人", ShortCode = "JYR", Desc = "检验人", ContextType = SysDic.All, Length = 40)]
        public virtual string Operator { get; set; }

        [DataMember]
        [DataDesc(CName = "靶值", ShortCode = "BZ", Desc = "靶值", ContextType = SysDic.Number, Length = 8)]
        public virtual string Target { get; set; }

        [DataMember]
        [DataDesc(CName = "标准差", ShortCode = "BZC", Desc = "标准差", ContextType = SysDic.Number, Length = 8)]
        public virtual string SD { get; set; }

        [DataMember]
        [DataDesc(CName = "变异系数", ShortCode = "BYXS", Desc = "变异系数", ContextType = SysDic.Number, Length = 8)]
        public virtual string CV { get; set; }

        [DataMember]
        [DataDesc(CName = "计算靶值", ShortCode = "JSBZ", Desc = "靶值", ContextType = SysDic.Number, Length = 8)]
        public virtual string CalcTarget { get; set; }

        [DataMember]
        [DataDesc(CName = "计算标准差", ShortCode = "JSBZC", Desc = "标准差", ContextType = SysDic.Number, Length = 8)]
        public virtual string CalcSD { get; set; }

        [DataMember]
        [DataDesc(CName = "计算变异系数", ShortCode = "JSBYXS", Desc = "变异系数", ContextType = SysDic.Number, Length = 8)]
        public virtual string CalcCV { get; set; }

        [DataMember]
        [DataDesc(CName = "质控物厂家", ShortCode = "CJ", Desc = "质控物厂家", ContextType = SysDic.NText, Length = 40)]
        public virtual string Manu { get; set; }

        [DataMember]
        [DataDesc(CName = "浓度水平", ShortCode = "NDSP", Desc = "质控物浓度水平", ContextType = SysDic.NText, Length = 20)]
        public virtual string ConcLevel { get; set; }

        [DataMember]
        [DataDesc(CName = "质控物批号", ShortCode = "QCMatLotNo", Desc = "质控物批号", ContextType = SysDic.NText, Length = 40)]
        public virtual string QCMatLotNo { get; set; }

        [DataMember]
        [DataDesc(CName = "质控数据备注", ShortCode = "QCComment ", Desc = "质控数据备注", ContextType = SysDic.All, Length = 200)]
        public virtual string QCComment { get; set; }

        [DataMember]
        [DataDesc(CName = "时效描述(质控物时效描述+质控项目时效描述)", ShortCode = "QCMatAndItemDesc ", Desc = "时效描述(质控物时效描述+质控项目时效描述)", ContextType = SysDic.All, Length = 200)]
        public virtual string QCMatAndItemDesc { get; set; }

        [DataMember]
        [DataDesc(CName = "质控项目时效开始日期", ShortCode = "QCItemBeginDate ", Desc = "质控项目时效开始日期", ContextType = SysDic.All, Length = 200)]
        public virtual string QCItemTimeBeginDate { get; set; }

        [DataMember]
        [DataDesc(CName = "质控项目时效结束日期", ShortCode = "QCItemEndDate ", Desc = "质控项目时效结束日期", ContextType = SysDic.All, Length = 200)]
        public virtual string QCItemTimeEndDate { get; set; }

        [DataMember]
        [DataDesc(CName = "质控数据是否使用", ShortCode = "IsUse", Desc = "质控数据是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool QCDValueIsUse { get; set; }

    }
}
