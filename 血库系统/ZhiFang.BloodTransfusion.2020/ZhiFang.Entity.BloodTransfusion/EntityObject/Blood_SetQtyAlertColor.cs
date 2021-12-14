using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodSetQtyAlertColor

	/// <summary>
	/// BloodSetQtyAlertColor object for NHibernate mapped table 'Blood_SetQtyAlertColor'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分", ClassCName = "BloodSetQtyAlertColor", ShortCode = "BloodSetQtyAlertColor", Desc = "库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分")]
	public class BloodSetQtyAlertColor : BaseEntity
	{
		#region Member Variables
		
        protected string _alertName;
        protected int _alertTypeId;
        protected string _alertTypeCName;
        protected string _alertColor;
        protected double _storeUpper;
        protected double _storeLower;
        protected string _memo;
        protected int _dispOrder;
        protected bool _visible;

		#endregion

		#region Constructors

		public BloodSetQtyAlertColor() { }

		public BloodSetQtyAlertColor( long labID, string alertName, int alertTypeId, string alertTypeCName, string alertColor, double storeUpper, double storeLower, string memo, int dispOrder, bool visible, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._alertName = alertName;
			this._alertTypeId = alertTypeId;
			this._alertTypeCName = alertTypeCName;
			this._alertColor = alertColor;
			this._storeUpper = storeUpper;
			this._storeLower = storeLower;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "库存预警名称", ShortCode = "AlertName", Desc = "库存预警名称", ContextType = SysDic.All, Length = 200)]
        public virtual string AlertName
		{
			get { return _alertName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for AlertName", value, value.ToString());
				_alertName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "预警分类Id", ShortCode = "AlertTypeId", Desc = "预警分类Id", ContextType = SysDic.All, Length = 4)]
        public virtual int AlertTypeId
		{
			get { return _alertTypeId; }
			set { _alertTypeId = value; }
		}

        [DataMember]
        [DataDesc(CName = "预警分类", ShortCode = "AlertTypeCName", Desc = "预警分类", ContextType = SysDic.All, Length = 60)]
        public virtual string AlertTypeCName
		{
			get { return _alertTypeCName; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for AlertTypeCName", value, value.ToString());
				_alertTypeCName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "预警颜色", ShortCode = "AlertColor", Desc = "预警颜色", ContextType = SysDic.All, Length = 60)]
        public virtual string AlertColor
		{
			get { return _alertColor; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for AlertColor", value, value.ToString());
				_alertColor = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "上限值", ShortCode = "StoreUpper", Desc = "上限值", ContextType = SysDic.All, Length = 8)]
        public virtual double StoreUpper
		{
			get { return _storeUpper; }
			set { _storeUpper = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "下限值", ShortCode = "StoreLower", Desc = "下限值", ContextType = SysDic.All, Length = 8)]
        public virtual double StoreLower
		{
			get { return _storeLower; }
			set { _storeLower = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 2000)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 2000)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
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
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

		#endregion
	}
	#endregion
}