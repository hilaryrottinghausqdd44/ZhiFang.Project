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
    [DataContract]
    public class BModuleGridControlList
    {
        #region Member Variables
        private long _labID;
        private string _labNO;
        private long _GridControlID;//列表设置控件集合ID
        private long _GridID;//列表ID
        private string _GridCode;//列表Code
        private string _MapField;//对应属性
        private string _TextField;//显示属性
        private string _ValueField;//值属性
        private long _TypeID;//控件类型id
        private string _TypeName;//控件类型名称
        private string _ClassName;//样式名称
        private string _StyleContent;//样式内容
        private string _ColName;//列头名称
        private string _OrderType;//排序类型
        private bool _IsOrder;//是否排序
        private string _ColData;//列数据扩展
        private string _URL;//服务地址
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
        private bool _IsHide;//是否隐藏
        private string _Width;//宽度
        private string _MinWidth;//最小宽度
        private string _Edit;//单元格编辑类型
        private string _Toolbar;//工具条模板
        private string _Align;//单元格排序方式
        private string _Fixed;//固定列
        private string _SourceCode;//源代码


        #endregion

        #region Constructors
        public BModuleGridControlList()
        {
        }

        public BModuleGridControlList(long labID, string labNO, long gridControlID, long gridID, string gridCode, string mapField, string textField, string valueField, long typeID, string typeName, string className, string styleContent, string colName, string orderType, bool isOrder, string colData, string uRL, string cName, string shortName, string shortCode, string standCode, string zFStandCode, string pinYinZiTou, int dispOrder, bool isUse, DateTime dataAddTime, DateTime? dataUpdateTime, byte[] dataTimeStamp, bool isHide, string width, string minWidth, string edit, string toolbar, string align, string @fixed, string sourceCode)
        {
            _labID = labID;
            _labNO = labNO;
            _GridControlID = gridControlID;
            _GridID = gridID;
            _GridCode = gridCode;
            _MapField = mapField;
            _TextField = textField;
            _ValueField = valueField;
            _TypeID = typeID;
            _TypeName = typeName;
            _ClassName = className;
            _StyleContent = styleContent;
            _ColName = colName;
            _OrderType = orderType;
            _IsOrder = isOrder;
            _ColData = colData;
            _URL = uRL;
            _CName = cName;
            _ShortName = shortName;
            _ShortCode = shortCode;
            _StandCode = standCode;
            _ZFStandCode = zFStandCode;
            _PinYinZiTou = pinYinZiTou;
            _DispOrder = dispOrder;
            _IsUse = isUse;
            _DataAddTime = dataAddTime;
            _DataUpdateTime = dataUpdateTime;
            _dataTimeStamp = dataTimeStamp;
            _IsHide = isHide;
            _Width = width;
            _MinWidth = minWidth;
            _Edit = edit;
            _Toolbar = toolbar;
            _Align = align;
            _Fixed = @fixed;
            _SourceCode = sourceCode;
        }
        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long LabID { get => _labID; set => _labID = value; }
        [DataMember]
        public string LabNO { get => _labNO; set => _labNO = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long GridControlID { get => _GridControlID; set => _GridControlID = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long GridID { get => _GridID; set => _GridID = value; }
        [DataMember]
        public string GridCode { get => _GridCode; set => _GridCode = value; }
        [DataMember]
        public string MapField { get => _MapField; set => _MapField = value; }
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
        public string ColName { get => _ColName; set => _ColName = value; }
        [DataMember]
        public string OrderType { get => _OrderType; set => _OrderType = value; }
        [DataMember]
        public bool IsOrder { get => _IsOrder; set => _IsOrder = value; }
        [DataMember]
        public string ColData { get => _ColData; set => _ColData = value; }
        [DataMember]
        public string URL { get => _URL; set => _URL = value; }
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
        public bool IsHide { get => _IsHide; set => _IsHide = value; }
        [DataMember]
        public string Width { get => _Width; set => _Width = value; }
        [DataMember]
        public string MinWidth { get => _MinWidth; set => _MinWidth = value; }
        [DataMember]
        public string Edit { get => _Edit; set => _Edit = value; }
        [DataMember]
        public string Toolbar { get => _Toolbar; set => _Toolbar = value; }
        [DataMember]
        public string Align { get => _Align; set => _Align = value; }
        [DataMember]
        public string Fixed { get => _Fixed; set => _Fixed = value; }
        [DataMember]
        public string SourceCode { get => _SourceCode; set => _SourceCode = value; }
        #endregion
    }
}
