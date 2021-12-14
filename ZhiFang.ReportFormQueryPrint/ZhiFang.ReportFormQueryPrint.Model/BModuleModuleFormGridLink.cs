using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.ReportFormQueryPrint.Common;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    /// <summary>
    /// 模块表单列表关系
    /// </summary>
    [DataContract]
    public class BModuleModuleFormGridLink
    {
		#region Member Variables
		private long _labID;
		private string _labNo;
		private long _ModuleFormGridLinkID;
		private long _FormID;//表单ID
        private long _GridID;//列表ID
        private long _ModuleID;
		private long _ChartID;//图表ID
        private string _FormCode;//表单Code
        private string _GridCode;//列表Code
        private string _ChartCode;//图表代码
        private string _CName;//表单或列表名称
        private long _TypeID;
		private string _TypeName;
		private int _DispOrder;
		private bool _IsUse;
		private DateTime _DataAddTime;
		private DateTime? _DataUpdateTime;
		private byte[] _dataTimeStamp;
        private string _ShortCode;
        private string _ShortName;
		private string _StandCode;


		#endregion
		public BModuleModuleFormGridLink()
        {
            
        }

        public BModuleModuleFormGridLink(long labID, string labNo, long moduleFormGridLinkID, long formID, long gridID, long moduleID, long chartID, string formCode, string gridCode, string chartCode, string cName, long typeID, string typeName, int dispOrder, bool isUse, DateTime dataAddTime, DateTime? dataUpdateTime, byte[] dataTimeStamp, string shortName, string shortCode, string standCode)
        {
            LabID = labID;
            LabNo = labNo;
            ModuleFormGridLinkID = moduleFormGridLinkID;
            FormID = formID;
            GridID = gridID;
            ModuleID = moduleID;
            ChartID = chartID;
            FormCode = formCode;
            GridCode = gridCode;
            ChartCode = chartCode;
            CName = cName;
            TypeID = typeID;
            TypeName = typeName;
            DispOrder = dispOrder;
            IsUse = isUse;
            DataAddTime = dataAddTime;
            DataUpdateTime = dataUpdateTime;
            DataTimeStamp = dataTimeStamp;
            ShortCode = shortCode;
            ShortName = shortName;
            StandCode = standCode;
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long LabID { get => _labID; set => _labID = value; }
        [DataMember]
        public string LabNo { get => _labNo; set => _labNo = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long ModuleFormGridLinkID { get => _ModuleFormGridLinkID; set => _ModuleFormGridLinkID = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long FormID { get => _FormID; set => _FormID = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long GridID { get => _GridID; set => _GridID = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long ModuleID { get => _ModuleID; set => _ModuleID = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long ChartID { get => _ChartID; set => _ChartID = value; }
        [DataMember]
        public string FormCode { get => _FormCode; set => _FormCode = value; }
        [DataMember]
        public string GridCode { get => _GridCode; set => _GridCode = value; }
        [DataMember]
        public string ChartCode { get => _ChartCode; set => _ChartCode = value; }
        [DataMember]
        public string CName { get => _CName; set => _CName = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long TypeID { get => _TypeID; set => _TypeID = value; }
        [DataMember]
        public string TypeName { get => _TypeName; set => _TypeName = value; }
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
        public string ShortName { get => _ShortName; set => _ShortName = value; }
        [DataMember]
        public string ShortCode { get => _ShortCode; set => _ShortCode = value; }
        [DataMember]
        public string StandCode { get => _StandCode; set => _StandCode = value; }
    }
}
