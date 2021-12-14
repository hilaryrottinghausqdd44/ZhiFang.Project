using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BMicroBCBottleTypeOfManufacturer

	/// <summary>
	/// BMicroBCBottleTypeOfManufacturer object for NHibernate mapped table 'B_MicroBCBottleTypeOfManufacturer'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物血培养瓶类型与厂商对应关系", ClassCName = "BMicroBCBottleTypeOfManufacturer", ShortCode = "BMicroBCBottleTypeOfManufacturer", Desc = "微生物血培养瓶类型与厂商对应关系")]
	public class BMicroBCBottleTypeOfManufacturer : BaseEntity
	{
		#region Member Variables
		
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BMicroBloodCultureBottleManufacturer _bMicroBloodCultureBottleManufacturer;
		protected BMicroBloodCultureBottleType _bMicroBloodCultureBottleType;

		#endregion

		#region Constructors

		public BMicroBCBottleTypeOfManufacturer() { }

		public BMicroBCBottleTypeOfManufacturer( long labID, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicroBloodCultureBottleManufacturer bMicroBloodCultureBottleManufacturer, BMicroBloodCultureBottleType bMicroBloodCultureBottleType )
		{
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicroBloodCultureBottleManufacturer = bMicroBloodCultureBottleManufacturer;
			this._bMicroBloodCultureBottleType = bMicroBloodCultureBottleType;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物血培养瓶厂商", ShortCode = "BMicroBloodCultureBottleManufacturer", Desc = "微生物血培养瓶厂商")]
		public virtual BMicroBloodCultureBottleManufacturer BMicroBloodCultureBottleManufacturer
		{
			get { return _bMicroBloodCultureBottleManufacturer; }
			set { _bMicroBloodCultureBottleManufacturer = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物血培养瓶类型", ShortCode = "BMicroBloodCultureBottleType", Desc = "微生物血培养瓶类型")]
		public virtual BMicroBloodCultureBottleType BMicroBloodCultureBottleType
		{
			get { return _bMicroBloodCultureBottleType; }
			set { _bMicroBloodCultureBottleType = value; }
		}

        
		#endregion
	}
	#endregion
}