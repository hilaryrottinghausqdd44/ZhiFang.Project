using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSpecialSplit

	/// <summary>
	/// MEPTSpecialSplit object for NHibernate mapped table 'MEPT_SpecialSplit'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "特殊拆分规则", ClassCName = "MEPTSpecialSplit", ShortCode = "MEPTSpecialSplit", Desc = "特殊拆分规则")]
    public class MEPTSpecialSplit : BaseEntity
	{
		#region Member Variables
		
        
        protected string _cName;
        protected int _isSplit;
        protected long _conditionLevel;
        protected string _requestFormField;
        protected string _fieldValue;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;

		#endregion

		#region Constructors

		public MEPTSpecialSplit() { }

		public MEPTSpecialSplit( long labID, string cName, int isSplit, long conditionLevel, string requestFormField, string fieldValue, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._cName = cName;
			this._isSplit = isSplit;
			this._conditionLevel = conditionLevel;
			this._requestFormField = requestFormField;
			this._fieldValue = fieldValue;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [DataDesc(CName = "规则名称", ShortCode = "CName", Desc = "规则名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否需要拆分", ShortCode = "IsSplit", Desc = "是否需要拆分", ContextType = SysDic.All, Length = 4)]
        public virtual int IsSplit
		{
			get { return _isSplit; }
			set { _isSplit = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "条件级别", ShortCode = "ConditionLevel", Desc = "条件级别", ContextType = SysDic.All, Length = 8)]
        public virtual long ConditionLevel
		{
			get { return _conditionLevel; }
			set { _conditionLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "申请单字段", ShortCode = "RequestFormField", Desc = "申请单字段", ContextType = SysDic.All, Length = 40)]
        public virtual string RequestFormField
		{
			get { return _requestFormField; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for RequestFormField", value, value.ToString());
				_requestFormField = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "字段值", ShortCode = "FieldValue", Desc = "字段值", ContextType = SysDic.All, Length = 100)]
        public virtual string FieldValue
		{
			get { return _fieldValue; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for FieldValue", value, value.ToString());
				_fieldValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "说明", ShortCode = "Comment", Desc = "说明", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 8000)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		#endregion
	}
	#endregion
}