using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodSetQtyAlertInfo

	/// <summary>
	/// BloodSetQtyAlertInfo object for NHibernate mapped table 'Blood_SetQtyAlertInfo'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "库存预警明细信息表", ClassCName = "BloodSetQtyAlertInfo", ShortCode = "BloodSetQtyAlertInfo", Desc = "库存预警明细信息表")]
	public class BloodSetQtyAlertInfo : BaseEntity
	{
		#region Member Variables
		
        protected double _storeLower;
        protected double _storeUpper;
        protected double _beforeWarningDay;
        protected int _dispOrder;
        protected bool _visible;
		protected BloodABO _bloodABO;
		protected BloodStyle _bloodStyle;
		//protected BloodUnit _warnUnit;

		#endregion

		#region Constructors

		public BloodSetQtyAlertInfo() { }

		public BloodSetQtyAlertInfo( long labID, double storeLower, double storeUpper, double beforeWarningDay, int dispOrder, bool visible, DateTime dataAddTime, byte[] dataTimeStamp, BloodABO aboNo, BloodStyle bloodStyle)//, BloodUnit warnUnit
		{
			this._labID = labID;
			this._storeLower = storeLower;
			this._storeUpper = storeUpper;
			this._beforeWarningDay = beforeWarningDay;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodABO = aboNo;
			this._bloodStyle = bloodStyle;
			//this._warnUnit = warnUnit;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存预警下限值", ShortCode = "StoreLower", Desc = "库存预警下限值", ContextType = SysDic.All, Length = 8)]
        public virtual double StoreLower
		{
			get { return _storeLower; }
			set { _storeLower = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存预警上限值", ShortCode = "StoreUpper", Desc = "库存预警上限值", ContextType = SysDic.All, Length = 8)]
        public virtual double StoreUpper
		{
			get { return _storeUpper; }
			set { _storeUpper = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "有效期报警天数", ShortCode = "BeforeWarningDay", Desc = "有效期报警天数", ContextType = SysDic.All, Length = 8)]
        public virtual double BeforeWarningDay
		{
			get { return _beforeWarningDay; }
			set { _beforeWarningDay = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "血型表", ShortCode = "AboNo", Desc = "血型表")]
		public virtual BloodABO BloodABO
		{
			get { return _bloodABO; }
			set { _bloodABO = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品字典", ShortCode = "BloodStyle", Desc = "血制品字典")]
		public virtual BloodStyle BloodStyle
		{
			get { return _bloodStyle; }
			set { _bloodStyle = value; }
		}

  //      [DataMember]
  //      [DataDesc(CName = "血制品单位", ShortCode = "WarnUnit", Desc = "血制品单位")]
		//public virtual BloodUnit WarnUnit
		//{
		//	get { return _warnUnit; }
		//	set { _warnUnit = value; }
		//}

        
		#endregion
	}
	#endregion
}