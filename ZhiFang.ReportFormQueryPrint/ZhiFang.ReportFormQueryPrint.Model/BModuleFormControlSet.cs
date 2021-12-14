using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Common;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    /// <summary>
    /// 模块表单配置
    /// </summary>
    [Serializable]
    [DataContract]
    public class BModuleFormControlSet
    {
        #region Member Variables
        private long _labID;
        private string _labNO;
        private long _FormControlSetID;//表单设置ID
        private long _FormControlID;//表单设置控件集合ID
        private long _QFuncID;//快捷功能ID
        private string _FormCode;//表单代码
        private bool _IsReadOnly;//是否只读
        private bool _IsDisplay;//是否显示
        private string _DefaultValue;//默认值
        private string _Label;
        private string _URL;//服务地址
        private string _DataJSON;//数据集JSON
        private bool _IsHasNull;//是否存在空值
        private string _CName;
        private string _ShortName;
        private string _ShortCode;
        private string _StandCode;
        private string _ZFStandCode;
        private string _PinYinZiTou;
        private int _DispOrder;
        private bool _IsUse;
        private DateTime _DataAddTime;
        private DateTime? _DataUpdateTime;
        private byte[] _dataTimeStamp;
        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long LabID { get => _labID; set => _labID = value; }
        public string LabNO { get => _labNO; set => _labNO = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long FormControlSetID { get => _FormControlSetID; set => _FormControlSetID = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long FormControlID { get => _FormControlID; set => _FormControlID = value; }
        [DataMember]
        public long QFuncID { get => _QFuncID; set => _QFuncID = value; }
        [DataMember]
        public string FormCode { get => _FormCode; set => _FormCode = value; }
        [DataMember]
        public bool IsReadOnly { get => _IsReadOnly; set => _IsReadOnly = value; }
        [DataMember]
        public bool IsDisplay { get => _IsDisplay; set => _IsDisplay = value; }
        [DataMember]
        public string DefaultValue { get => _DefaultValue; set => _DefaultValue = value; }
        [DataMember]
        public string Label { get => _Label; set => _Label = value; }
        public string URL { get => _URL; set => _URL = value; }
        public string DataJSON { get => _DataJSON; set => _DataJSON = value; }
        public bool IsHasNull { get => _IsHasNull; set => _IsHasNull = value; }
        public string CName { get => _CName; set => _CName = value; }
        public string ShortName { get => _ShortName; set => _ShortName = value; }
        public string ShortCode { get => _ShortCode; set => _ShortCode = value; }
        public string StandCode { get => _StandCode; set => _StandCode = value; }
        public string ZFStandCode { get => _ZFStandCode; set => _ZFStandCode = value; }
        public string PinYinZiTou { get => _PinYinZiTou; set => _PinYinZiTou = value; }
        [DataMember]
        public int DispOrder { get => _DispOrder; set => _DispOrder = value; }
        [DataMember]
        public bool IsUse { get => _IsUse; set => _IsUse = value; }
        public DateTime DataAddTime { get => _DataAddTime; set => _DataAddTime = value; }
        public DateTime? DataUpdateTime { get => _DataUpdateTime; set => _DataUpdateTime = value; }
        public byte[] DataTimeStamp { get => _dataTimeStamp; set => _dataTimeStamp = value; }
        #endregion
    }
}
