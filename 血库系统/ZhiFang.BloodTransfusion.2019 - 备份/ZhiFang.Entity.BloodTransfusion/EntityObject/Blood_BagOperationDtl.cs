using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBagOperationDtl

	/// <summary>
	/// BloodBagOperationDtl object for NHibernate mapped table 'Blood_BagOperationDtl'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBagOperationDtl", ShortCode = "BloodBagOperationDtl", Desc = "")]
	public class BloodBagOperationDtl : BaseEntity
	{
		#region Member Variables
		
        protected string _bagOperResult;
        protected DateTime? _dataUpdateTime;
		protected BDict _bDict;
		protected BloodBagOperation _bloodBagOperation;

		#endregion

		#region Constructors

		public BloodBagOperationDtl() { }

		public BloodBagOperationDtl( long labID, string bagOperResult, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BDict bDict, BloodBagOperation bloodBagOperation )
		{
			this._labID = labID;
			this._bagOperResult = bagOperResult;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bDict = bDict;
			this._bloodBagOperation = bloodBagOperation;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "BagOperResult", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string BagOperResult
		{
			get { return _bagOperResult; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for BagOperResult", value, value.ToString());
				_bagOperResult = value;
			}
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
        [DataDesc(CName = "BDict", ShortCode = "BDict", Desc = "BDict")]
		public virtual BDict BDict
        {
			get { return _bDict; }
			set { _bDict = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodBagOperation", Desc = "")]
		public virtual BloodBagOperation BloodBagOperation
		{
			get { return _bloodBagOperation; }
			set { _bloodBagOperation = value; }
		}

        
		#endregion
	}
	#endregion
}