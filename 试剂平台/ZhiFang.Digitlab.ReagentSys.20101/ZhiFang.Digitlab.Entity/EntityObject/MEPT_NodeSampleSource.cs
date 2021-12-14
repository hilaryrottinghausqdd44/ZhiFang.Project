using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTNodeSampleSource

	/// <summary>
	/// MEPTNodeSampleSource object for NHibernate mapped table 'MEPT_NodeSampleSource'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "站点样本来源表", ClassCName = "MEPTNodeSampleSource", ShortCode = "MEPTNodeSampleSource", Desc = "站点样本来源表")]
	public class MEPTNodeSampleSource : BaseEntity
	{
		#region Member Variables
		
        
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected BNodeTable _bNodeTable;
		protected MEPTBSampleSource _mEPTBSampleSource;

		#endregion

		#region Constructors

		public MEPTNodeSampleSource() { }

		public MEPTNodeSampleSource( long labID, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BNodeTable mEPTBNodeTable, MEPTBSampleSource mEPTBSampleSource )
		{
			this._labID = labID;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bNodeTable = mEPTBNodeTable;
			this._mEPTBSampleSource = mEPTBSampleSource;
		}

		#endregion

		#region Public Properties




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
        [DataDesc(CName = "站点表", ShortCode = "BNodeTable", Desc = "站点表", ContextType = SysDic.All)]
        public virtual BNodeTable BNodeTable
		{
			get { return _bNodeTable; }
			set { _bNodeTable = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本来源", ShortCode = "MEPTBSampleSource", Desc = "样本来源", ContextType = SysDic.All)]
		public virtual MEPTBSampleSource MEPTBSampleSource
		{
			get { return _mEPTBSampleSource; }
			set { _mEPTBSampleSource = value; }
		}

		#endregion
	}
	#endregion
}