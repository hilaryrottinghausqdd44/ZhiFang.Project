using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBReqFormResult

	/// <summary>
	/// BloodBReqFormResult object for NHibernate mapped table 'Blood_BReqFormResult'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "申请相关检验结果表", ClassCName = "BloodBReqFormResult", ShortCode = "BloodBReqFormResult", Desc = "申请相关检验结果表")]
	public class BloodBReqFormResult : BaseEntity
	{
		#region Member Variables
		
        protected DateTime? _bTestTime;
        protected string _barcode;
        protected string _itemResult;
        protected string _itemUnit;
        protected string _itemLisResult;
        protected bool _visible;
        protected int _dispOrder;
		protected BloodBReqForm _bloodBReqForm;
		protected BloodBTestItem _bloodBTestItem;

		#endregion

		#region Constructors

		public BloodBReqFormResult() { }

		public BloodBReqFormResult( long labID, DateTime bTestTime, string barcode, string itemResult, string itemUnit, string itemLisResult, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodBReqForm bloodBReqForm, BloodBTestItem bloodBTestItem )
		{
			this._labID = labID;
			this._bTestTime = bTestTime;
			this._barcode = barcode;
			this._itemResult = itemResult;
			this._itemUnit = itemUnit;
			this._itemLisResult = itemLisResult;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBReqForm = bloodBReqForm;
			this._bloodBTestItem = bloodBTestItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验时间", ShortCode = "BTestTime", Desc = "检验时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BTestTime
		{
			get { return _bTestTime; }
			set { _bTestTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "Barcode", Desc = "条码号", ContextType = SysDic.All, Length = 20)]
        public virtual string Barcode
		{
			get { return _barcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Barcode", value, value.ToString());
				_barcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "检验结果", ShortCode = "ItemResult", Desc = "检验结果", ContextType = SysDic.All, Length = 500)]
        public virtual string ItemResult
		{
			get { return _itemResult; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ItemResult", value, value.ToString());
				_itemResult = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "ItemUnit", Desc = "单位", ContextType = SysDic.All, Length = 500)]
        public virtual string ItemUnit
		{
			get { return _itemUnit; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ItemUnit", value, value.ToString());
				_itemUnit = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "Lis原始结果", ShortCode = "ItemLisResult", Desc = "Lis原始结果", ContextType = SysDic.All, Length = 500)]
        public virtual string ItemLisResult
		{
			get { return _itemLisResult; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ItemLisResult", value, value.ToString());
				_itemLisResult = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血申请主单表", ShortCode = "BloodBReqForm", Desc = "用血申请主单表")]
		public virtual BloodBReqForm BloodBReqForm
		{
			get { return _bloodBReqForm; }
			set { _bloodBReqForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "检验项目表 ", ShortCode = "BloodBTestItem", Desc = "检验项目表 ")]
		public virtual BloodBTestItem BloodBTestItem
		{
			get { return _bloodBTestItem; }
			set { _bloodBTestItem = value; }
		}

        
		#endregion
	}
	#endregion
}