using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region EPBEquipCommInfo

	/// <summary>
	/// EPBEquipCommInfo object for NHibernate mapped table 'EP_B_EquipCommInfo'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "仪器配置表", ClassCName = "EPBEquipCommInfo", ShortCode = "EPBEquipCommInfo", Desc = "仪器配置表")]
	public class EPBEquipCommInfo : BaseEntity
	{
		#region Member Variables
		
        protected long _equipID;
        protected string _commType;
        protected byte[] _commPara;
        protected DateTime? _dataUpdateTime;

		#endregion

		#region Constructors

		public EPBEquipCommInfo() { }

		public EPBEquipCommInfo( long labID, long equipID, string commType, byte[] commPara, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._equipID = equipID;
			this._commType = commType;
			this._commPara = commPara;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器ID", ShortCode = "EquipID", Desc = "仪器ID", ContextType = SysDic.All, Length = 8)]
        public virtual long EquipID
		{
			get { return _equipID; }
			set { _equipID = value; }
		}

        [DataMember]
        [DataDesc(CName = "参数类型", ShortCode = "CommType", Desc = "参数类型", ContextType = SysDic.All, Length = 50)]
        public virtual string CommType
		{
			get { return _commType; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CommType", value, value.ToString());
				_commType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "通讯参数", ShortCode = "CommPara", Desc = "通讯参数", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] CommPara
		{
			get { return _commPara; }
			set { _commPara = value; }
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