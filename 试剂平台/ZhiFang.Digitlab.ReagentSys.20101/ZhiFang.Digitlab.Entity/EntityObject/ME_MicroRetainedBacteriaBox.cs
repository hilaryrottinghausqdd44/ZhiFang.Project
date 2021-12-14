using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroRetainedBacteriaBox

	/// <summary>
	/// MEMicroRetainedBacteriaBox object for NHibernate mapped table 'ME_MicroRetainedBacteriaBox'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物留菌盒记录", ClassCName = "MEMicroRetainedBacteriaBox", ShortCode = "MEMicroRetainedBacteriaBox", Desc = "微生物留菌盒记录")]
	public class MEMicroRetainedBacteriaBox : BaseEntity
	{
		#region Member Variables
		
        protected string _rBBoxNo;
        protected int _unUsedPosition;
        protected bool _isUsed;
        protected long? _refrigerator;
        protected long? _layer;
        protected string _empName;
        protected DateTime? _dataUpdateTime;
		protected BRetainedBacteriaBoxType _bRetainedBacteriaBoxType;
		protected HREmployee _hREmployee;
		protected IList<MEMicroRetainedBacteria> _mEMicroRetainedBacteriaList; 

		#endregion

		#region Constructors

		public MEMicroRetainedBacteriaBox() { }

		public MEMicroRetainedBacteriaBox( long labID, string rBBoxNo, int unUsedPosition, bool isUsed, long refrigerator, long layer, string empName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BRetainedBacteriaBoxType bRetainedBacteriaBoxType, HREmployee hREmployee )
		{
			this._labID = labID;
			this._rBBoxNo = rBBoxNo;
			this._unUsedPosition = unUsedPosition;
			this._isUsed = isUsed;
			this._refrigerator = refrigerator;
			this._layer = layer;
			this._empName = empName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bRetainedBacteriaBoxType = bRetainedBacteriaBoxType;
			this._hREmployee = hREmployee;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "RBBoxNo", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string RBBoxNo
		{
			get { return _rBBoxNo; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for RBBoxNo", value, value.ToString());
				_rBBoxNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "待用位置号", ShortCode = "UnUsedPosition", Desc = "待用位置号", ContextType = SysDic.All, Length = 4)]
        public virtual int UnUsedPosition
		{
			get { return _unUsedPosition; }
			set { _unUsedPosition = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUsed", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUsed
		{
			get { return _isUsed; }
			set { _isUsed = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "冰箱", ShortCode = "Refrigerator", Desc = "冰箱", ContextType = SysDic.All, Length = 8)]
		public virtual long? Refrigerator
		{
			get { return _refrigerator; }
			set { _refrigerator = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "层", ShortCode = "Layer", Desc = "层", ContextType = SysDic.All, Length = 8)]
		public virtual long? Layer
		{
			get { return _layer; }
			set { _layer = value; }
		}

        [DataMember]
        [DataDesc(CName = "记录人姓名", ShortCode = "EmpName", Desc = "记录人姓名", ContextType = SysDic.All, Length = 60)]
        public virtual string EmpName
		{
			get { return _empName; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
				_empName = value;
			}
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
        [DataDesc(CName = "留菌盒类型字典表", ShortCode = "BRetainedBacteriaBoxType", Desc = "留菌盒类型字典表")]
		public virtual BRetainedBacteriaBoxType BRetainedBacteriaBoxType
		{
			get { return _bRetainedBacteriaBoxType; }
			set { _bRetainedBacteriaBoxType = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
		public virtual HREmployee HREmployee
		{
			get { return _hREmployee; }
			set { _hREmployee = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物留菌记录", ShortCode = "MEMicroRetainedBacteriaList", Desc = "微生物留菌记录")]
		public virtual IList<MEMicroRetainedBacteria> MEMicroRetainedBacteriaList
		{
			get
			{
				if (_mEMicroRetainedBacteriaList==null)
				{
					_mEMicroRetainedBacteriaList = new List<MEMicroRetainedBacteria>();
				}
				return _mEMicroRetainedBacteriaList;
			}
			set { _mEMicroRetainedBacteriaList = value; }
		}

        
		#endregion
	}
	#endregion
}