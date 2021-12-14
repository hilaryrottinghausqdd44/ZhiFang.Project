using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region SCBarCodeRules

	/// <summary>
	/// SCBarCodeRules object for NHibernate mapped table 'SC_BarCodeRules'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "一维条码序号信息", ClassCName = "SCBarCodeRules", ShortCode = "SCBarCodeRules", Desc = "一维条码序号信息")]
	public class SCBarCodeRules : BaseEntity
	{
		#region Member Variables
		
        protected string _bmsType;
        protected long _curBarCode;
        protected DateTime? _dataUpdateTime;

		#endregion

		#region Constructors

		public SCBarCodeRules() { }

		public SCBarCodeRules( long labID, string bmsType, int curBarCode, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._bmsType = bmsType;
			this._curBarCode = curBarCode;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "条码类型", ShortCode = "BmsType", Desc = "条码类型", ContextType = SysDic.All, Length = 60)]
        public virtual string BmsType
		{
			get { return _bmsType; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for BmsType", value, value.ToString());
				_bmsType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "当前条码值", ShortCode = "CurBarCode", Desc = "当前条码值", ContextType = SysDic.All, Length = 8)]
        public virtual long CurBarCode
		{
			get { return _curBarCode; }
			set { _curBarCode = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "最后更新时间", ShortCode = "DataUpdateTime", Desc = "最后更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
		#endregion
	}
	#endregion
}