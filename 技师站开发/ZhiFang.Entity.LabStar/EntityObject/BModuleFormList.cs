using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region BModuleFormList

	/// <summary>
	/// BModuleFormList object for NHibernate mapped table 'B_Module_FormList'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BModuleFormList", ShortCode = "BModuleFormList", Desc = "")]
	public class BModuleFormList : BaseEntity
	{
		#region Member Variables

		protected string _labNo;
		protected string _formCode;
		protected long _typeID;
		protected string _typeName;
		protected long _classID;
		protected string _className;
		protected string _cName;
		protected string _shortName;
		protected string _shortCode;
		protected string _standCode;
		protected string _zFStandCode;
		protected string _pinYinZiTou;
		protected int _dispOrder;
		protected bool _isUse;
		protected DateTime? _dataUpdateTime;
		protected string _sourceCodeUrl;
		protected string _sourceCode;
		protected string _memo;


		#endregion

		#region Constructors

		public BModuleFormList() { }

		public BModuleFormList(long labID, string labNo, string formCode, long typeID, string typeName, long classID, string className, string cName, string shortName, string shortCode, string standCode, string zFStandCode, string pinYinZiTou, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string sourceCodeUrl, string sourceCode, string memo)
		{
			this._labID = labID;
			this._labNo = labNo;
			this._formCode = formCode;
			this._typeID = typeID;
			this._typeName = typeName;
			this._classID = classID;
			this._className = className;
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
			this._sourceCodeUrl = sourceCodeUrl;
			this._sourceCode = sourceCode;
			this._memo = memo;
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
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "ClassID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long ClassID
		{
			get { return _classID; }
			set { _classID = value; }
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
		[DataDesc(CName = "", ShortCode = "SourceCodeUrl", Desc = "", ContextType = SysDic.All, Length = 500)]
		public virtual string SourceCodeUrl
		{
			get { return _sourceCodeUrl; }
			set
			{
				_sourceCodeUrl = value;
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

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 16)]
		public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
			}
		}


		#endregion
	}
	#endregion
}