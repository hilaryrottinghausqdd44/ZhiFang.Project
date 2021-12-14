using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodTransForm

	/// <summary>
	/// BloodTransForm object for NHibernate mapped table 'Blood_TransForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "输血记录主单表", ClassCName = "BloodTransForm", ShortCode = "BloodTransForm", Desc = "输血记录主单表")]
	public class BloodTransForm : BaseEntity
	{
		#region Member Variables
		
        protected string _transFormNo;
        protected long? _beforeCheckID1;
        protected string _beforeCheck1;
        protected long? _beforeCheckID2;
        protected string _beforeCheck2;
        protected long? _transCheckID1;
        protected string _transCheck1;
        protected long? _transCheckID2;
        protected string _transCheck2;
        protected DateTime? _transBeginTime;
        protected DateTime? _transEndTime;
        protected bool _hasAdverseReactions;
        protected DateTime? _adverseReactionsTime;
        protected double _adverseReactionsHP;
        protected bool _visible;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BloodBOutForm _bloodBOutForm;
		protected BloodBOutItem _bloodBOutItem;
		protected BloodBReqForm _bloodBReqForm;
		protected BloodPatinfo _bloodPatinfo;
		protected BloodQtyDtl _bloodQtyDtl;
		protected BloodStyle _bloodStyle;
		
		#endregion

		#region Constructors

		public BloodTransForm() { }

		public BloodTransForm( long labID, string transFormNo, long beforeCheckID1, string beforeCheck1, long beforeCheckID2, string beforeCheck2, long transCheckID1, string transCheck1, long transCheckID2, string transCheck2, DateTime transBeginTime, DateTime transEndTime, bool hasAdverseReactions, DateTime adverseReactionsTime, double adverseReactionsHP, bool visible, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BloodBOutForm bloodBOutForm, BloodBOutItem bloodBOutItem, BloodBReqForm bloodBReqForm, BloodPatinfo bloodPatinfo, BloodQtyDtl bloodQtyDtl, BloodStyle bloodStyle )
		{
			this._labID = labID;
			this._transFormNo = transFormNo;
			this._beforeCheckID1 = beforeCheckID1;
			this._beforeCheck1 = beforeCheck1;
			this._beforeCheckID2 = beforeCheckID2;
			this._beforeCheck2 = beforeCheck2;
			this._transCheckID1 = transCheckID1;
			this._transCheck1 = transCheck1;
			this._transCheckID2 = transCheckID2;
			this._transCheck2 = transCheck2;
			this._transBeginTime = transBeginTime;
			this._transEndTime = transEndTime;
			this._hasAdverseReactions = hasAdverseReactions;
			this._adverseReactionsTime = adverseReactionsTime;
			this._adverseReactionsHP = adverseReactionsHP;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBOutForm = bloodBOutForm;
			this._bloodBOutItem = bloodBOutItem;
			this._bloodBReqForm = bloodBReqForm;
			this._bloodPatinfo = bloodPatinfo;
			this._bloodQtyDtl = bloodQtyDtl;
			this._bloodStyle = bloodStyle;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "输血记录单号", ShortCode = "TransFormNo", Desc = "输血记录单号", ContextType = SysDic.All, Length = 20)]
        public virtual string TransFormNo
		{
			get { return _transFormNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for TransFormNo", value, value.ToString());
				_transFormNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "输血前核对人ID1", ShortCode = "BeforeCheckID1", Desc = "输血前核对人ID1", ContextType = SysDic.All, Length = 8)]
		public virtual long? BeforeCheckID1
		{
			get { return _beforeCheckID1; }
			set { _beforeCheckID1 = value; }
		}

        [DataMember]
        [DataDesc(CName = "输血前核对人姓名1", ShortCode = "BeforeCheck1", Desc = "输血前核对人姓名1", ContextType = SysDic.All, Length = 50)]
        public virtual string BeforeCheck1
		{
			get { return _beforeCheck1; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BeforeCheck1", value, value.ToString());
				_beforeCheck1 = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "输血前核对人ID2", ShortCode = "BeforeCheckID2", Desc = "输血前核对人ID2", ContextType = SysDic.All, Length = 8)]
		public virtual long? BeforeCheckID2
		{
			get { return _beforeCheckID2; }
			set { _beforeCheckID2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "输血前核对人姓名2", ShortCode = "BeforeCheck2", Desc = "输血前核对人姓名2", ContextType = SysDic.All, Length = 50)]
        public virtual string BeforeCheck2
		{
			get { return _beforeCheck2; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BeforeCheck2", value, value.ToString());
				_beforeCheck2 = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "输血时核对人ID1", ShortCode = "TransCheckID1", Desc = "输血时核对人ID1", ContextType = SysDic.All, Length = 8)]
		public virtual long? TransCheckID1
		{
			get { return _transCheckID1; }
			set { _transCheckID1 = value; }
		}

        [DataMember]
        [DataDesc(CName = "输血时核对人姓名1", ShortCode = "TransCheck1", Desc = "输血时核对人姓名1", ContextType = SysDic.All, Length = 50)]
        public virtual string TransCheck1
		{
			get { return _transCheck1; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TransCheck1", value, value.ToString());
				_transCheck1 = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "输血时核对人ID2", ShortCode = "TransCheckID2", Desc = "输血时核对人ID2", ContextType = SysDic.All, Length = 8)]
		public virtual long? TransCheckID2
		{
			get { return _transCheckID2; }
			set { _transCheckID2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "输血时核对人姓名2", ShortCode = "TransCheck2", Desc = "输血时核对人姓名2", ContextType = SysDic.All, Length = 50)]
        public virtual string TransCheck2
		{
			get { return _transCheck2; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TransCheck2", value, value.ToString());
				_transCheck2 = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "输血开始时间", ShortCode = "TransBeginTime", Desc = "输血开始时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? TransBeginTime
		{
			get { return _transBeginTime; }
			set { _transBeginTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "输血结束时间", ShortCode = "TransEndTime", Desc = "输血结束时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? TransEndTime
		{
			get { return _transEndTime; }
			set { _transEndTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否有不良反应", ShortCode = "HasAdverseReactions", Desc = "是否有不良反应", ContextType = SysDic.All, Length = 1)]
        public virtual bool HasAdverseReactions
		{
			get { return _hasAdverseReactions; }
			set { _hasAdverseReactions = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "不良反应时间", ShortCode = "AdverseReactionsTime", Desc = "不良反应时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? AdverseReactionsTime
		{
			get { return _adverseReactionsTime; }
			set { _adverseReactionsTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "不良反应剩余血量", ShortCode = "AdverseReactionsHP", Desc = "不良反应剩余血量", ContextType = SysDic.All, Length = 8)]
        public virtual double AdverseReactionsHP
		{
			get { return _adverseReactionsHP; }
			set { _adverseReactionsHP = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据数据修改时间", ShortCode = "DataUpdateTime", Desc = "数据数据修改时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "发血主单表", ShortCode = "BloodBOutForm", Desc = "发血主单表")]
		public virtual BloodBOutForm BloodBOutForm
		{
			get { return _bloodBOutForm; }
			set { _bloodBOutForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "发血明细表", ShortCode = "BloodBOutItem", Desc = "发血明细表")]
		public virtual BloodBOutItem BloodBOutItem
		{
			get { return _bloodBOutItem; }
			set { _bloodBOutItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血申请主单表", ShortCode = "BloodBReqForm", Desc = "用血申请主单表")]
		public virtual BloodBReqForm BloodBReqForm
		{
			get { return _bloodBReqForm; }
			set { _bloodBReqForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "病人就诊记录信息表", ShortCode = "BloodPatinfo", Desc = "病人就诊记录信息表")]
		public virtual BloodPatinfo BloodPatinfo
		{
			get { return _bloodPatinfo; }
			set { _bloodPatinfo = value; }
		}

        [DataMember]
        [DataDesc(CName = "库存表", ShortCode = "BloodQtyDtl", Desc = "库存表")]
		public virtual BloodQtyDtl BloodQtyDtl
		{
			get { return _bloodQtyDtl; }
			set { _bloodQtyDtl = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品字典", ShortCode = "BloodStyle", Desc = "血制品字典")]
		public virtual BloodStyle BloodStyle
		{
			get { return _bloodStyle; }
			set { _bloodStyle = value; }
		}

		#endregion
	}
	#endregion
}