using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodTransItem

	/// <summary>
	/// BloodTransItem object for NHibernate mapped table 'Blood_TransItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "输血过程记录明细表", ClassCName = "BloodTransItem", ShortCode = "BloodTransItem", Desc = "输血过程记录明细表")]
	public class BloodTransItem : BaseEntity
	{
		#region Member Variables
		
        protected int _contentTypeID;
        protected string _transItemResult;
        protected double _numberItemResult;
        protected bool _visible;
        protected int _dispOrder;
		protected BloodBagRecordType _bloodBagRecordType;
		protected BloodBagRecordItem _bloodBagRecordItem;
		protected BloodTransForm _bloodTransForm;

		#endregion

		#region Constructors

		public BloodTransItem() { }

		public BloodTransItem( long labID, int contentTypeID, string transItemResult, double numberItemResult, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodBagRecordType bloodBagRecordType, BloodBagRecordItem bloodBagRecordItem, BloodTransForm bloodTransForm )
		{
			this._labID = labID;
			this._contentTypeID = contentTypeID;
			this._transItemResult = transItemResult;
			this._numberItemResult = numberItemResult;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBagRecordType = bloodBagRecordType;
			this._bloodBagRecordItem = bloodBagRecordItem;
			this._bloodTransForm = bloodTransForm;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "内容分类ID", ShortCode = "ContentTypeID", Desc = "内容分类ID", ContextType = SysDic.All, Length = 4)]
        public virtual int ContentTypeID
		{
			get { return _contentTypeID; }
			set { _contentTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "结果值", ShortCode = "TransItemResult", Desc = "结果值", ContextType = SysDic.All, Length = 200)]
        public virtual string TransItemResult
		{
			get { return _transItemResult; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for TransItemResult", value, value.ToString());
				_transItemResult = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数字型结果值", ShortCode = "NumberItemResult", Desc = "数字型结果值", ContextType = SysDic.All, Length = 8)]
        public virtual double NumberItemResult
		{
			get { return _numberItemResult; }
			set { _numberItemResult = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否可见", ShortCode = "Visible", Desc = "是否可见", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示序号", ShortCode = "DispOrder", Desc = "显示序号", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋记录类型字典表", ShortCode = "BloodBagRecordType", Desc = "血袋记录类型字典表")]
		public virtual BloodBagRecordType BloodBagRecordType
		{
			get { return _bloodBagRecordType; }
			set { _bloodBagRecordType = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋记录明细字典表", ShortCode = "BloodBagRecordItem", Desc = "血袋记录明细字典表")]
		public virtual BloodBagRecordItem BloodBagRecordItem
		{
			get { return _bloodBagRecordItem; }
			set { _bloodBagRecordItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "输血记录主单表", ShortCode = "BloodTransForm", Desc = "输血记录主单表")]
		public virtual BloodTransForm BloodTransForm
		{
			get { return _bloodTransForm; }
			set { _bloodTransForm = value; }
		}

        
		#endregion
	}
	#endregion
}