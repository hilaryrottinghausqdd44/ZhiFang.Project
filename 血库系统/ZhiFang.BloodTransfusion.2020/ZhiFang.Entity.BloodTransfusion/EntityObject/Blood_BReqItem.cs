using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBReqItem

	/// <summary>
	/// BloodBReqItem object for NHibernate mapped table 'Blood_BReqItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "用血申请明细信息表", ClassCName = "BloodBReqItem", ShortCode = "BloodBReqItem", Desc = "用血申请明细信息表")]
	public class BloodBReqItem : BaseEntity
	{
		#region Member Variables
		
        protected string _bReqItemNo;
        protected double _bReqCount;
        protected string _otherItemNo;
        protected string _memo;
        protected int _dispOrder;
        protected bool _visible;
		protected BloodBReqForm _bloodBReqForm;
		protected BloodClass _bloodClass;
		protected BloodStyle _bloodStyle;
		protected BloodUnit _bloodUnit;

		#endregion

		#region Constructors

		public BloodBReqItem() { }

		public BloodBReqItem( long labID, string bReqItemNo, double bReqCount, string otherItemNo, string memo, int dispOrder, bool visible, DateTime dataAddTime, byte[] dataTimeStamp, BloodBReqForm bloodBReqForm, BloodClass bloodClass, BloodStyle bloodStyle, BloodUnit bloodUnit )
		{
			this._labID = labID;
			this._bReqItemNo = bReqItemNo;
			this._bReqCount = bReqCount;
			this._otherItemNo = otherItemNo;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBReqForm = bloodBReqForm;
			this._bloodClass = bloodClass;
			this._bloodStyle = bloodStyle;
			this._bloodUnit = bloodUnit;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "申请明细单号", ShortCode = "BReqItemNo", Desc = "申请明细单号", ContextType = SysDic.All, Length = 20)]
        public virtual string BReqItemNo
		{
			get { return _bReqItemNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BReqItemNo", value, value.ToString());
				_bReqItemNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请血量", ShortCode = "BReqCount", Desc = "申请血量", ContextType = SysDic.All, Length = 8)]
        public virtual double BReqCount
		{
			get { return _bReqCount; }
			set { _bReqCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "第三方项目编码", ShortCode = "OtherItemNo", Desc = "第三方项目编码", ContextType = SysDic.All, Length = 50)]
        public virtual string OtherItemNo
		{
			get { return _otherItemNo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for OtherItemNo", value, value.ToString());
				_otherItemNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "Memo", ShortCode = "Memo", Desc = "Memo", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 500)
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
        [DataDesc(CName = "是否可见", ShortCode = "Visible", Desc = "是否可见", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血申请主单表", ShortCode = "BloodBReqForm", Desc = "用血申请主单表")]
		public virtual BloodBReqForm BloodBReqForm
		{
			get { return _bloodBReqForm; }
			set { _bloodBReqForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋分类", ShortCode = "BloodClass", Desc = "血袋分类")]
		public virtual BloodClass BloodClass
		{
			get { return _bloodClass; }
			set { _bloodClass = value; }
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