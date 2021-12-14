using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region BModuleFormControlList

	/// <summary>
	/// BModuleFormControlList object for NHibernate mapped table 'B_Module_FormControlList'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BModuleFormControlList", ShortCode = "BModuleFormControlList", Desc = "")]
	public class BModuleFormControlList : BaseEntity
	{
		#region Member Variables

		protected string _labNo;
		protected long _formID;
		protected string _formCode;
		protected string _defaultValue;
		protected string _mapField;
		protected string _textField;
		protected string _valueField;
		protected long _typeID;
		protected string _typeName;
		protected string _className;
		protected string _styleContent;
		protected string _label;
		protected string _callBackFunc;
		protected long _cols;
		protected string _uRL;
		protected string _dataJSON;
		protected bool _isHasNull;
		protected string _cName;
		protected string _shortName;
		protected string _shortCode;
		protected string _standCode;
		protected string _zFStandCode;
		protected string _pinYinZiTou;
		protected int _dispOrder;
		protected bool _isUse;
		protected DateTime? _dataUpdateTime;
		protected bool _IsDisplay;
		protected bool _IsReadOnly;


		#endregion

		#region Constructors

		public BModuleFormControlList() { }

		public BModuleFormControlList(long labID, string labNo, long formID, string formCode, string defaultValue, string mapField, string textField, string valueField, long typeID, string typeName, string className, string styleContent, string label, string callBackFunc, long cols, string uRL, string dataJSON, bool isHasNull, string cName, string shortName, string shortCode, string standCode, string zFStandCode, string pinYinZiTou, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
		{
			this._labID = labID;
			this._labNo = labNo;
			this._formID = formID;
			this._formCode = formCode;
			this._defaultValue = defaultValue;
			this._mapField = mapField;
			this._textField = textField;
			this._valueField = valueField;
			this._typeID = typeID;
			this._typeName = typeName;
			this._className = className;
			this._styleContent = styleContent;
			this._label = label;
			this._callBackFunc = callBackFunc;
			this._cols = cols;
			this._uRL = uRL;
			this._dataJSON = dataJSON;
			this._isHasNull = isHasNull;
			this._cName = cName;
			this._shortName = shortName;
			this._shortCode = shortCode;
			this._standCode = standCode;
			this._zFStandCode = zFStandCode;
			this._pinYinZiTou = pinYinZiTou;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "", ShortCode = "LabNo", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual bool IsDisplay
		{
			get { return _IsDisplay; }
			set
			{
				_IsDisplay = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "LabNo", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual bool IsReadOnly
		{
			get { return _IsReadOnly; }
			set
			{
				_IsReadOnly = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "LabNo", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string LabNo
		{
			get { return _labNo; }
			set
			{
				_labNo = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "FormID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long FormID
		{
			get { return _formID; }
			set { _formID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "FormCode", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string FormCode
		{
			get { return _formCode; }
			set
			{
				_formCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DefaultValue", Desc = "", ContextType = SysDic.All, Length = 500)]
		public virtual string DefaultValue
		{
			get { return _defaultValue; }
			set
			{
				_defaultValue = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "MapField", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string MapField
		{
			get { return _mapField; }
			set
			{
				_mapField = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "TextField", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string TextField
		{
			get { return _textField; }
			set
			{
				_textField = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ValueField", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string ValueField
		{
			get { return _valueField; }
			set
			{
				_valueField = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "TypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long TypeID
		{
			get { return _typeID; }
			set { _typeID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "TypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string TypeName
		{
			get { return _typeName; }
			set
			{
				_typeName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ClassName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string ClassName
		{
			get { return _className; }
			set
			{
				_className = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "StyleContent", Desc = "", ContextType = SysDic.All, Length = 1000)]
		public virtual string StyleContent
		{
			get { return _styleContent; }
			set
			{
				_styleContent = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Label", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string Label
		{
			get { return _label; }
			set
			{
				_label = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "CallBackFunc", Desc = "", ContextType = SysDic.All, Length = 16)]
		public virtual string CallBackFunc
		{
			get { return _callBackFunc; }
			set
			{
				_callBackFunc = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "Cols", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long Cols
		{
			get { return _cols; }
			set { _cols = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "URL", Desc = "", ContextType = SysDic.All, Length = 1000)]
		public virtual string URL
		{
			get { return _uRL; }
			set
			{
				_uRL = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DataJSON", Desc = "", ContextType = SysDic.All, Length = 16)]
		public virtual string DataJSON
		{
			get { return _dataJSON; }
			set
			{
				_dataJSON = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsHasNull", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsHasNull
		{
			get { return _isHasNull; }
			set { _isHasNull = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string CName
		{
			get { return _cName; }
			set
			{
				_cName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ShortName", Desc = "", ContextType = SysDic.All, Length = 10)]
		public virtual string ShortName
		{
			get { return _shortName; }
			set
			{
				_shortName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 10)]
		public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				_shortCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "StandCode", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string StandCode
		{
			get { return _standCode; }
			set
			{
				_standCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ZFStandCode", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string ZFStandCode
		{
			get { return _zFStandCode; }
			set
			{
				_zFStandCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "PinYinZiTou", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				_pinYinZiTou = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}


		#endregion
	}
	#endregion
}