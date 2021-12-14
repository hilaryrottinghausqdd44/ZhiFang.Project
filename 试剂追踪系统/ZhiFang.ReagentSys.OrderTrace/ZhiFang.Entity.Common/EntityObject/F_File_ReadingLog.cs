using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.Common
{
	#region FFileReadingLog

	/// <summary>
	/// FFileReadingLog object for NHibernate mapped table 'F_File_ReadingLog'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "文档阅读记录表", ClassCName = "FFileReadingLog", ShortCode = "FFileReadingLog", Desc = "文档阅读记录表")]
	public class FFileReadingLog : BaseEntity
	{
		#region Member Variables
		
        protected string _readerName;
        protected int _readTimes;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
		protected FFile _fFile;
		protected HREmployee _creator;
		protected HREmployee _reader;

		#endregion

		#region Constructors

		public FFileReadingLog() { }

		public FFileReadingLog( long labID, string readerName, int readTimes, string memo, int dispOrder, bool isUse, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, FFile fFile, HREmployee creator, HREmployee reader )
		{
			this._labID = labID;
			this._readerName = readerName;
			this._readTimes = readTimes;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._creatorName = creatorName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._fFile = fFile;
			this._creator = creator;
			this._reader = reader;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "阅读者姓名", ShortCode = "ReaderName", Desc = "阅读者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReaderName
		{
			get { return _readerName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReaderName", value, value.ToString());
				_readerName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "阅读时长，毫秒数", ShortCode = "ReadTimes", Desc = "阅读时长，毫秒数", ContextType = SysDic.All, Length = 4)]
        public virtual int ReadTimes
		{
			get { return _readTimes; }
			set { _readTimes = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
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
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreatorName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreatorName
		{
			get { return _creatorName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
				_creatorName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据修改时间", ShortCode = "DataUpdateTime", Desc = "数据修改时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "文档表", ShortCode = "FFile", Desc = "文档表")]
		public virtual FFile FFile
		{
			get { return _fFile; }
			set { _fFile = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建人", ShortCode = "Creator", Desc = "创建人")]
		public virtual HREmployee Creator
		{
			get { return _creator; }
			set { _creator = value; }
		}

        [DataMember]
        [DataDesc(CName = "阅读人", ShortCode = "Reader", Desc = "阅读人")]
		public virtual HREmployee Reader
		{
			get { return _reader; }
			set { _reader = value; }
		}

        
		#endregion
	}
	#endregion
}