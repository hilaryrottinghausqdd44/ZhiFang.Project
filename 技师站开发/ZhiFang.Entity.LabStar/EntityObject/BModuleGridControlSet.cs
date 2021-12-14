using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region BModuleGridControlSet

	/// <summary>
	/// BModuleGridControlSet object for NHibernate mapped table 'B_Module_GridControlSet'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BModuleGridControlSet", ShortCode = "BModuleGridControlSet", Desc = "")]
	public class BModuleGridControlSet : BaseEntity
	{
		#region Member Variables

		protected string _labNo;
		protected long _gridControlID;
		protected long _qFuncID;
		protected string _gridCode;
		protected string _mapField;
		protected string _textField;
		protected string _valueField;
		protected long _typeID;
		protected string _typeName;
		protected string _className;
		protected string _styleContent;
		protected string _colName;
		protected bool _isOrder;
		protected string _orderType;
		protected string _colData;
		protected string _uRL;
		protected string _cName;
		protected string _shortName;
		protected string _shortCode;
		protected string _standCode;
		protected string _zFStandCode;
		protected string _pinYinZiTou;
		protected int _dispOrder;
		protected bool _isUse;
		protected DateTime? _dataUpdateTime;
		protected bool _isHide;
		protected string _width;


		#endregion

		#region Constructors

		public BModuleGridControlSet() { }

		public BModuleGridControlSet(long labID, string labNo, long gridControlID, long qFuncID, string gridCode, string mapField, string textField, string valueField, long typeID, string typeName, string className, string styleContent, string colName, bool isOrder, string orderType, string colData, string uRL, string cName, string shortName, string shortCode, string standCode, string zFStandCode, string pinYinZiTou, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool isHide, string width)
		{
			this._labID = labID;
			this._labNo = labNo;
			this._gridControlID = gridControlID;
			this._qFuncID = qFuncID;
			this._gridCode = gridCode;
			this._mapField = mapField;
			this._textField = textField;
			this._valueField = valueField;
			this._typeID = typeID;
			this._typeName = typeName;
			this._className = className;
			this._styleContent = styleContent;
			this._colName = colName;
			this._isOrder = isOrder;
			this._orderType = orderType;
			this._colData = colData;
			this._uRL = uRL;
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
			this._isHide = isHide;
			this._width = width;
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
		[DataDesc(CName = "", ShortCode = "GridControlID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long GridControlID
		{
			get { return _gridControlID; }
			set { _gridControlID = value; }
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
		[DataDesc(CName = "", ShortCode = "GridCode", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string GridCode
		{
			get { return _gridCode; }
			set
			{
				_gridCode = value;
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
		[DataDesc(CName = "", ShortCode = "ColName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string ColName
		{
			get { return _colName; }
			set
			{
				_colName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsOrder", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsOrder
		{
			get { return _isOrder; }
			set { _isOrder = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "OrderType", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string OrderType
		{
			get { return _orderType; }
			set
			{
				_orderType = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ColData", Desc = "", ContextType = SysDic.All, Length = 16)]
		public virtual string ColData
		{
			get { return _colData; }
			set
			{
				_colData = value;
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

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsHide", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsHide
		{
			get { return _isHide; }
			set { _isHide = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Width", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string Width
		{
			get { return _width; }
			set
			{
				_width = value;
			}
		}


		#endregion
	}
	#endregion
}