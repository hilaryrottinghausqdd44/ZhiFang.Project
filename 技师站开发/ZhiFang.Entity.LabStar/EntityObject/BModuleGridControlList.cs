using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region BModuleGridControlList

	/// <summary>
	/// BModuleGridControlList object for NHibernate mapped table 'B_Module_GridControlList'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BModuleGridControlList", ShortCode = "BModuleGridControlList", Desc = "")]
	public class BModuleGridControlList : BaseEntity
	{
		#region Member Variables

		protected string _labNo;
		protected long _gridID;
		protected string _gridCode;
		protected string _mapField;
		protected string _textField;
		protected string _valueField;
		protected long _typeID;
		protected string _typeName;
		protected string _className;
		protected string _styleContent;
		protected string _colName;
		protected string _orderType;
		protected bool _isOrder;
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
		protected string _minWidth;
		protected string _edit;
		protected string _toolbar;
		protected string _align;
		protected string _fixed;
		protected string _sourceCode;


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
		[DataDesc(CName = "", ShortCode = "GridID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long GridID
		{
			get { return _gridID; }
			set { _gridID = value; }
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
		[DataDesc(CName = "", ShortCode = "IsOrder", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsOrder
		{
			get { return _isOrder; }
			set { _isOrder = value; }
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

		[DataMember]
		[DataDesc(CName = "", ShortCode = "MinWidth", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string MinWidth
		{
			get { return _minWidth; }
			set
			{
				_minWidth = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Edit", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string Edit
		{
			get { return _edit; }
			set
			{
				_edit = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Toolbar", Desc = "", ContextType = SysDic.All, Length = 500)]
		public virtual string Toolbar
		{
			get { return _toolbar; }
			set
			{
				_toolbar = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Align", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string Align
		{
			get { return _align; }
			set
			{
				_align = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Fixed", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string Fixed
		{
			get { return _fixed; }
			set
			{
				_fixed = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SourceCode", Desc = "", ContextType = SysDic.All, Length = 16)]
		public virtual string SourceCode
		{
			get { return _sourceCode; }
			set
			{
				_sourceCode = value;
			}
		}


		#endregion
	}
	#endregion
}