using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBagChargeLink

	/// <summary>
	/// BloodBagChargeLink object for NHibernate mapped table 'Blood_ BagChargeLink'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "血袋费用项目关系表(原Blood_Kind)", ClassCName = "BloodBagChargeLink", ShortCode = "BloodBagChargeLink", Desc = "血袋费用项目关系表(原Blood_Kind)")]
	public class BloodBagChargeLink : BaseEntity
	{
		#region Member Variables
		
        protected double _bCount;
        protected bool _isUse;
        protected int _dispOrder;
		protected BloodABO _blood;
		protected BloodChargeItem _bloodChargeItem;
		protected BloodStyle _bloodStyle;
		protected BloodUnit _bloodUnit;

		#endregion

		#region Constructors

		public BloodBagChargeLink() { }

		public BloodBagChargeLink( long labID, double bCount, bool isUse, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodABO blood, BloodChargeItem bloodChargeItem, BloodStyle bloodStyle, BloodUnit bloodUnit )
		{
			this._labID = labID;
			this._bCount = bCount;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._blood = blood;
			this._bloodChargeItem =bloodChargeItem;
			this._bloodStyle = bloodStyle;
			this._bloodUnit = bloodUnit;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "血袋规格", ShortCode = "BCount", Desc = "血袋规格", ContextType = SysDic.All, Length = 8)]
        public virtual double BCount
		{
			get { return _bCount; }
			set { _bCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "血型表", ShortCode = "BloodABO", Desc = "血型表")]
		public virtual BloodABO BloodABO
		{
			get { return _blood; }
			set { _blood = value; }
		}

        [DataMember]
        [DataDesc(CName = "费用项目表", ShortCode = "BloodChargeItem", Desc = "费用项目表")]
		public virtual BloodChargeItem BloodChargeItem
		{
			get { return _bloodChargeItem; }
			set { _bloodChargeItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品字典", ShortCode = "BloodStyle", Desc = "血制品字典")]
		public virtual BloodStyle BloodStyle
		{
			get { return _bloodStyle; }
			set { _bloodStyle = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品单位", ShortCode = "BloodUnit", Desc = "血制品单位")]
		public virtual BloodUnit BloodUnit
		{
			get { return _bloodUnit; }
			set { _bloodUnit = value; }
		}

        
		#endregion
	}
	#endregion
}