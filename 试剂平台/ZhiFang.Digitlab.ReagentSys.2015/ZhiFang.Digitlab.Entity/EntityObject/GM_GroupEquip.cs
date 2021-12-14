using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region GMGroupEquip

	/// <summary>
	/// GMGroupEquip object for NHibernate mapped table 'GM_GroupEquip'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "小组仪器", ClassCName = "GMGroupEquip", ShortCode = "GMGroupEquip", Desc = "小组仪器")]
	public class GMGroupEquip : BaseEntity
	{
		#region Member Variables
		
        protected DateTime? _dataUpdateTime;
		protected EPBEquip _ePBEquip;
		protected GMGroup _gMGroup;

		#endregion

		#region Constructors

		public GMGroupEquip() { }

		public GMGroupEquip( long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, EPBEquip ePBEquip, GMGroup gMGroup )
		{
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._ePBEquip = ePBEquip;
			this._gMGroup = gMGroup;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器表", ShortCode = "EPBEquip", Desc = "仪器表")]
		public virtual EPBEquip EPBEquip
		{
			get { return _ePBEquip; }
			set { _ePBEquip = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组表", ShortCode = "GMGroup", Desc = "小组表")]
		public virtual GMGroup GMGroup
		{
			get { return _gMGroup; }
			set { _gMGroup = value; }
		}

        
		#endregion
	}
	#endregion
}