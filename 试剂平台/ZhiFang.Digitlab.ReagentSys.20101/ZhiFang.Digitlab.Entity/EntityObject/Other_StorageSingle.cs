using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherStorageSingle

	/// <summary>
	/// OtherStorageSingle object for NHibernate mapped table 'Other_StorageSingle'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "入库单", ClassCName = "OtherStorageSingle", ShortCode = "OtherStorageSingle", Desc = "入库单")]
    public class OtherStorageSingle : BaseEntity
	{
		#region Member Variables
		
        
        protected DateTime? _storageTime;
        protected string _comment;
        protected string _operator;
        protected string _handlingMan;
        protected string _checker;
        protected long? _operatorID;
		protected long? _handlingManID;
		protected long? _checkerID;
        protected DateTime? _dataUpdateTime;
		protected BCheckStatus _bCheckStatus;
		protected BStorehouse _bStorehouse;
		protected BSupplier _bSupplier;
		protected BStorageType _bStorageType;
		protected IList<OtherStorageItem> _otherStorageItemList;

		#endregion

		#region Constructors

		public OtherStorageSingle() { }

		public OtherStorageSingle( long labID, DateTime storageTime, string comment, string Operator, string handlingMan, string checker, byte[] dataTimeStamp, long operatorID, long handlingManID, long checkerID, BCheckStatus bCheckStatus, BStorehouse bStorehouse, BSupplier bSupplier, BStorageType bStorageType )
		{
            this._labID = labID;
			this._storageTime = storageTime;
			this._comment = comment;
			this._operator = Operator;
			this._handlingMan = handlingMan;
			this._checker = checker;
			this._dataTimeStamp = dataTimeStamp;
			this._operatorID = operatorID;
			this._handlingManID = handlingManID;
			this._checkerID = checkerID;
			this._bCheckStatus = bCheckStatus;
			this._bStorehouse = bStorehouse;
			this._bSupplier = bSupplier;
			this._bStorageType = bStorageType;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "入库日期", ShortCode = "StorageTime", Desc = "入库日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StorageTime
		{
			get { return _storageTime; }
			set { _storageTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
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
        [DataDesc(CName = "操作人", ShortCode = "Operator", Desc = "操作人", ContextType = SysDic.All, Length = 40)]
        public virtual string Operator
		{
			get { return _operator; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Operator", value, value.ToString());
				_operator = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "经手人", ShortCode = "HandlingMan", Desc = "经手人", ContextType = SysDic.All, Length = 40)]
        public virtual string HandlingMan
		{
			get { return _handlingMan; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for HandlingMan", value, value.ToString());
				_handlingMan = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "审核人", ShortCode = "Checker", Desc = "审核人", ContextType = SysDic.All, Length = 40)]
        public virtual string Checker
		{
			get { return _checker; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Checker", value, value.ToString());
				_checker = value;
			}
		}

        

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作人ID", ShortCode = "OperatorID", Desc = "操作人ID", ContextType = SysDic.All)]
		public virtual long? OperatorID
		{
			get { return _operatorID; }
			set { _operatorID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "经手人ID", ShortCode = "HandlingManID", Desc = "经手人ID", ContextType = SysDic.All)]
		public virtual long? HandlingManID
		{
			get { return _handlingManID; }
            set { _handlingManID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核人ID", ShortCode = "CheckerID", Desc = "审核人ID", ContextType = SysDic.All)]
        public virtual long? CheckerID
		{
			get { return _checkerID; }
            set { _checkerID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核状态", ShortCode = "BCheckStatus", Desc = "审核状态", ContextType = SysDic.All)]
		public virtual BCheckStatus BCheckStatus
		{
			get { return _bCheckStatus; }
			set { _bCheckStatus = value; }
		}

        [DataMember]
        [DataDesc(CName = "仓库", ShortCode = "BStorehouse", Desc = "仓库", ContextType = SysDic.All)]
		public virtual BStorehouse BStorehouse
		{
			get { return _bStorehouse; }
			set { _bStorehouse = value; }
		}

        [DataMember]
        [DataDesc(CName = "供应商", ShortCode = "BSupplier", Desc = "供应商", ContextType = SysDic.All)]
		public virtual BSupplier BSupplier
		{
			get { return _bSupplier; }
			set { _bSupplier = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库类型", ShortCode = "BStorageType", Desc = "入库类型", ContextType = SysDic.All)]
		public virtual BStorageType BStorageType
		{
			get { return _bStorageType; }
			set { _bStorageType = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库明细", ShortCode = "OtherStorageItemList", Desc = "入库明细", ContextType = SysDic.All)]
		public virtual IList<OtherStorageItem> OtherStorageItemList
		{
			get
			{
				if (_otherStorageItemList==null)
				{
					_otherStorageItemList = new List<OtherStorageItem>();
				}
				return _otherStorageItemList;
			}
			set { _otherStorageItemList = value; }
		}

		#endregion
	}
	#endregion
}