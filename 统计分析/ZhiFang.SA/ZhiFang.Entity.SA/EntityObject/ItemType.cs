using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.SA
{
	#region ItemType

	/// <summary>
	/// ItemType object for NHibernate mapped table 'ItemType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ItemType", ShortCode = "ItemType", Desc = "")]
	public class ItemType : BaseEntity
	{
		#region Member Variables
		
        protected double _amount;
        protected string _sampleUnit;
        protected int _isUnite;
		

		#endregion

		#region Constructors

		public ItemType() { }

		public ItemType( double amount, string sampleUnit, int isUnite )
		{
			this._amount = amount;
			this._sampleUnit = sampleUnit;
			this._isUnite = isUnite;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Amount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Amount
		{
			get { return _amount; }
			set { _amount = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleUnit", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string SampleUnit
		{
			get { return _sampleUnit; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for SampleUnit", value, value.ToString());
				_sampleUnit = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUnite", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsUnite
		{
			get { return _isUnite; }
			set { _isUnite = value; }
		}

		
		#endregion
	}
	#endregion
}