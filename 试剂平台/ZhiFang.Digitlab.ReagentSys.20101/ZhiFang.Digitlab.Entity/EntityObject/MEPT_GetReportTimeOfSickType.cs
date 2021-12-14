using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTGetReportTimeOfSickType

	/// <summary>
	/// MEPTGetReportTimeOfSickType object for NHibernate mapped table 'MEPT_GetReportTimeOfSickType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "取单时间-就诊类型关系", ClassCName = "MEPTGetReportTimeOfSickType", ShortCode = "MEPTGetReportTimeOfSickType", Desc = "取单时间-就诊类型关系")]
	public class MEPTGetReportTimeOfSickType : BaseEntity
	{
		#region Member Variables
		
        
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected BSickType _bSickType;
		protected MEPTGetReportTime _mEPTGetReportTime;

		#endregion

		#region Constructors

		public MEPTGetReportTimeOfSickType() { }

		public MEPTGetReportTimeOfSickType( long labID, int dispOrder, string comment, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BSickType bSickType, MEPTGetReportTime mEPTGetReportTime )
		{
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bSickType = bSickType;
			this._mEPTGetReportTime = mEPTGetReportTime;
		}

		#endregion

		#region Public Properties




        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        

        [DataMember]
        [DataDesc(CName = "就诊类型", ShortCode = "BSickType", Desc = "就诊类型", ContextType = SysDic.All)]
		public virtual BSickType BSickType
		{
			get { return _bSickType; }
			set { _bSickType = value; }
		}

        [DataMember]
        [DataDesc(CName = "取单时间", ShortCode = "MEPTGetReportTime", Desc = "取单时间", ContextType = SysDic.All)]
		public virtual MEPTGetReportTime MEPTGetReportTime
		{
			get { return _mEPTGetReportTime; }
			set { _mEPTGetReportTime = value; }
		}

		#endregion
	}
	#endregion
}