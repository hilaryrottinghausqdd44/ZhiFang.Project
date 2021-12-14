using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BMicroBCBottleReceivingPerson

	/// <summary>
	/// BMicroBCBottleReceivingPerson object for NHibernate mapped table 'B_MicroBCBottleReceivingPerson'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物血培养瓶领取人", ClassCName = "BMicroBCBottleReceivingPerson", ShortCode = "BMicroBCBottleReceivingPerson", Desc = "微生物血培养瓶领取人")]
	public class BMicroBCBottleReceivingPerson : BaseEntity
	{
		#region Member Variables
		
        protected string _receivingPerson;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BMedicalDepartment _bMedicalDepartment;

		#endregion

		#region Constructors

		public BMicroBCBottleReceivingPerson() { }

		public BMicroBCBottleReceivingPerson( long labID, string receivingPerson, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMedicalDepartment bMedicalDepartment )
		{
			this._labID = labID;
			this._receivingPerson = receivingPerson;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMedicalDepartment = bMedicalDepartment;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "领取人姓名", ShortCode = "ReceivingPerson", Desc = "领取人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceivingPerson
		{
			get { return _receivingPerson; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReceivingPerson", value, value.ToString());
				_receivingPerson = value;
			}
		}

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
        [DataDesc(CName = "就诊科室", ShortCode = "BMedicalDepartment", Desc = "就诊科室")]
		public virtual BMedicalDepartment BMedicalDepartment
		{
			get { return _bMedicalDepartment; }
			set { _bMedicalDepartment = value; }
		}

        
		#endregion
	}
	#endregion
}