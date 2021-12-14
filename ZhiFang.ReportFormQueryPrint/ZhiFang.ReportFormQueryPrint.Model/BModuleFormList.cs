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
    /// 表单集合
    /// </summary>
    [Serializable]
    [DataContract]
    public class BModuleFormList
    {
        #region Member Variables
        private long _labID;
        private string _labNO;
        private long _FormID;//表单ID
        private string _FormCode;//表单代码
        private long _TypeID;//类型ID
        private string _TypeName;//类型名称
        private long _ClassID;//样式类型ID
        private string _ClassName;//样式类型名称
        private string _CName;//表单或列表名称
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
        private string _SourceCodeUrl;//源代码url
        private string _SourceCode;//源代码
        private string _Memo;//备注
        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long LabID { get => _labID; set => _labID = value; }
        [DataMember]
        public string LabNO { get => _labNO; set => _labNO = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long FormID { get => _FormID; set => _FormID = value; }
        [DataMember]
        public string FormCode { get => _FormCode; set => _FormCode = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long TypeID { get => _TypeID; set => _TypeID = value; }
        [DataMember]
        public string TypeName { get => _TypeName; set => _TypeName = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long ClassID { get => _ClassID; set => _ClassID = value; }
        [DataMember]
        public string ClassName { get => _ClassName; set => _ClassName = value; }
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
        public string SourceCodeUrl { get => _SourceCodeUrl; set => _SourceCodeUrl = value; }
        [DataMember]
        public string SourceCode { get => _SourceCode; set => _SourceCode = value; }
        [DataMember]
        public string Memo { get => _Memo; set => _Memo = value; }


        #endregion
    }
}
