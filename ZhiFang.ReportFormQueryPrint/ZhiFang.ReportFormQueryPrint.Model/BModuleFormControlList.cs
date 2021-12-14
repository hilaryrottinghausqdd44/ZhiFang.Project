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
    /// 列表控件集合
    /// </summary>
    [Serializable]
    [DataContract]
    public class BModuleFormControlList
    {
        #region Member Variables
        private long _labID;
        private string _labNO;
        private long _FormControlID;//表单设置控件集合ID
        private long _FormID;//表单ID
        private string _FormCode;//表单代码
        private string _MapField;//对应属性
        private string _DefaultValue;//默认值
        private string _TextField;//显示属性
        private string _ValueField;//值属性
        private long _TypeID;//控件类型id
        private string _TypeName;//控件类型名称
        private string _ClassName;//样式名称
        private string _StyleContent;//样式内容

        private string _Label;
        private string _CallBackFunc;//回调函数
        private long _Cols;//所占栅格
        
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
        private bool _isReadOnly;
        private bool _isDisplay;
        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long LabID { get => _labID; set => _labID = value; }
        [DataMember]
        public string LabNO { get => _labNO; set => _labNO = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long FormControlID { get => _FormControlID; set => _FormControlID = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long FormID { get => _FormID; set => _FormID = value; }
        [DataMember]
        public string FormCode { get => _FormCode; set => _FormCode = value; }
        [DataMember]
        public string MapField { get => _MapField; set => _MapField = value; }
        [DataMember]
        public string DefaultValue { get => _DefaultValue; set => _DefaultValue = value; }
        [DataMember]
        public string TextField { get => _TextField; set => _TextField = value; }
        [DataMember]
        public string ValueField { get => _ValueField; set => _ValueField = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long TypeID { get => _TypeID; set => _TypeID = value; }
        [DataMember]
        public string TypeName { get => _TypeName; set => _TypeName = value; }
        [DataMember]
        public string ClassName { get => _ClassName; set => _ClassName = value; }
        [DataMember]
        public string StyleContent { get => _StyleContent; set => _StyleContent = value; }
        [DataMember]
        public string Label { get => _Label; set => _Label = value; }
        [DataMember]
        public string CallBackFunc { get => _CallBackFunc; set => _CallBackFunc = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long Cols { get => _Cols; set => _Cols = value; }
        [DataMember]
        public string URL { get => _URL; set => _URL = value; }
        [DataMember]
        public string DataJSON { get => _DataJSON; set => _DataJSON = value; }
        [DataMember]
        public bool IsHasNull { get => _IsHasNull; set => _IsHasNull = value; }
        [DataMember]
        public string CName { get => _CName; set => _CName = value; }
        [DataMember]
        public string ShortName { get => _ShortName; set => _ShortName = value; }
        [DataMember]
        public string ShortCode { get => _ShortCode; set => _ShortCode = value; }
        [DataMember]
        public string StandCode { get => _StandCode; set => _StandCode = value; }
        [DataMember]
        public string ZFStandCode { get => _ZFStandCode; set => _ZFStandCode = value; }
        [DataMember]
        public string PinYinZiTou { get => _PinYinZiTou; set => _PinYinZiTou = value; }
        [DataMember]
        public int DispOrder { get => _DispOrder; set => _DispOrder = value; }
        [DataMember]
        public bool IsUse { get => _IsUse; set => _IsUse = value; }
        [DataMember]
        public DateTime DataAddTime { get => _DataAddTime; set => _DataAddTime = value; }
        [DataMember]
        public DateTime? DataUpdateTime { get => _DataUpdateTime; set => _DataUpdateTime = value; }
        [DataMember]
        public byte[] DataTimeStamp { get => _dataTimeStamp; set => _dataTimeStamp = value; }
        [DataMember]
        public bool IsReadOnly { get => _isReadOnly; set => _isReadOnly = value; }
        [DataMember]
        public bool IsDisplay { get => _isDisplay; set => _isDisplay = value; }

        #endregion
    }
}
