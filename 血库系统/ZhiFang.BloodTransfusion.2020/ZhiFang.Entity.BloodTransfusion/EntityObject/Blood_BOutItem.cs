using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBOutItem

	/// <summary>
	/// BloodBOutItem object for NHibernate mapped table 'Blood_BOutItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "发血明细表", ClassCName = "BloodBOutItem", ShortCode = "BloodBOutItem", Desc = "发血明细表")]
	public class BloodBOutItem : BaseEntity
	{
		#region Member Variables
		
        protected string _bOutItemNo;
        protected int _checkFlag;
        protected DateTime? _bODate;
        protected int _outFlag;
        protected int _confirmCompletion;
        protected int _handoverCompletion;
        protected int _courseCompletion;
        protected int _recoverCompletion;
        protected int _dispOrder;
        protected bool _visible;
		protected BloodBOutForm _bloodBOutForm;
		protected BloodQtyDtl _bloodQtyDtl;
		protected BloodStyle _bloodStyle;

		#endregion

		#region Constructors

		public BloodBOutItem() { }

		public BloodBOutItem( long labID, string bOutItemNo, int checkFlag, DateTime bODate, int outFlag, int confirmCompletion, int handoverCompletion, int courseCompletion, int recoverCompletion, int dispOrder, bool visible, DateTime dataAddTime, byte[] dataTimeStamp, BloodBOutForm bloodBOutForm, BloodQtyDtl bloodQtyDtl, BloodStyle bloodStyle )
		{
			this._labID = labID;
			this._bOutItemNo = bOutItemNo;
			this._checkFlag = checkFlag;
			this._bODate = bODate;
			this._outFlag = outFlag;
			this._confirmCompletion = confirmCompletion;
			this._handoverCompletion = handoverCompletion;
			this._courseCompletion = courseCompletion;
			this._recoverCompletion = recoverCompletion;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBOutForm = bloodBOutForm;
			this._bloodQtyDtl = bloodQtyDtl;
			this._bloodStyle = bloodStyle;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "发血明细单号", ShortCode = "BOutItemNo", Desc = "发血明细单号", ContextType = SysDic.All, Length = 20)]
        public virtual string BOutItemNo
		{
			get { return _bOutItemNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BOutItemNo", value, value.ToString());
				_bOutItemNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "审核标志", ShortCode = "CheckFlag", Desc = "审核标志", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckFlag
		{
			get { return _checkFlag; }
			set { _checkFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出库时间", ShortCode = "BODate", Desc = "出库时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BODate
		{
			get { return _bODate; }
			set { _bODate = value; }
		}

        [DataMember]
        [DataDesc(CName = "出库标志", ShortCode = "OutFlag", Desc = "出库标志", ContextType = SysDic.All, Length = 4)]
        public virtual int OutFlag
		{
			get { return _outFlag; }
			set { _outFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "领用确认完成度", ShortCode = "ConfirmCompletion", Desc = "领用确认完成度", ContextType = SysDic.All, Length = 4)]
        public virtual int ConfirmCompletion
		{
			get { return _confirmCompletion; }
			set { _confirmCompletion = value; }
		}

        [DataMember]
        [DataDesc(CName = "交接登记完成度", ShortCode = "HandoverCompletion", Desc = "交接登记完成度", ContextType = SysDic.All, Length = 4)]
        public virtual int HandoverCompletion
		{
			get { return _handoverCompletion; }
			set { _handoverCompletion = value; }
		}

        [DataMember]
        [DataDesc(CName = "输血过程登记完成度", ShortCode = "CourseCompletion", Desc = "输血过程登记完成度", ContextType = SysDic.All, Length = 4)]
        public virtual int CourseCompletion
		{
			get { return _courseCompletion; }
			set { _courseCompletion = value; }
		}

        [DataMember]
        [DataDesc(CName = "回收登记完成度", ShortCode = "RecoverCompletion", Desc = "回收登记完成度", ContextType = SysDic.All, Length = 4)]
        public virtual int RecoverCompletion
		{
			get { return _recoverCompletion; }
			set { _recoverCompletion = value; }
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
        [DataDesc(CName = "发血主单表", ShortCode = "BloodBOutForm", Desc = "发血主单表")]
		public virtual BloodBOutForm BloodBOutForm
		{
			get { return _bloodBOutForm; }
			set { _bloodBOutForm = value; }
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