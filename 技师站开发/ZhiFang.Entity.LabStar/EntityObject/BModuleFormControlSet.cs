using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region BModuleFormControlSet

	/// <summary>
	/// BModuleFormControlSet object for NHibernate mapped table 'B_Module_FormControlSet'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BModuleFormControlSet", ShortCode = "BModuleFormControlSet", Desc = "")]
	public class BModuleFormControlSet : BaseEntity
	{
		#region Member Variables

		protected string _labNo;
		protected long _formControlID;
		protected long _qFuncID;
		protected string _formCode;
		protected bool _isReadOnly;
		protected bool _isDisplay;
		protected string _defaultValue;
		protected string _label;
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


		#endregion

		#region Constructors

		public BModuleFormControlSet() { }

		public BModuleFormControlSet(long labID, string labNo, long formControlID, long qFuncID, string formCode, bool isReadOnly, bool isDisplay, string defaultValue, string label, string uRL, string dataJSON, bool isHasNull, string cName, string shortName, string shortCode, string standCode, string zFStandCode, string pinYinZiTou, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
		{
			this._labID = labID;
			this._labNo = labNo;
			this._formControlID = formControlID;
			this._qFuncID = qFuncID;
			this._formCode = formCode;
			this._isReadOnly = isReadOnly;
			this._isDisplay = isDisplay;
			this._defaultValue = defaultValue;
			this._label = label;
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
		[DataDesc(CName = "", ShortCode = "FormControlID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long FormControlID
		{
			get { return _formControlID; }
			set { _formControlID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "QFuncID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long QFuncID
		{
			get { return _qFuncID; }
			set { _qFuncID = value; }
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
		[DataDesc(CName = "", ShortCode = "IsReadOnly", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsReadOnly
		{
			get { return _isReadOnly; }
			set { _isReadOnly = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsDisplay", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsDisplay
		{
			get { return _isDisplay; }
			set { _isDisplay = value; }
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