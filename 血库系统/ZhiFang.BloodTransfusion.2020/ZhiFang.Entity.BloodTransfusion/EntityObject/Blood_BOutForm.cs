using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBOutForm

	/// <summary>
	/// BloodBOutForm object for NHibernate mapped table 'Blood_BOutForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "发血主单表", ClassCName = "BloodBOutForm", ShortCode = "BloodBOutForm", Desc = "发血主单表")]
	public class BloodBOutForm : BaseEntity
	{
		#region Member Variables
		
        protected string _bOutFormNo;
        protected int _outType;
        protected long? _operatorID;
        protected string _operatorName;
        protected DateTime? _operTime;
        protected long? _checkID;
        protected string _checkCName;
        protected DateTime? _checkTime;
        protected int _checkFlag;
        protected int _confirmCompletion;
        protected int _handoverCompletion;
        protected int _courseCompletion;
        protected int _recoverCompletion;
        protected DateTime? _printTime;
        protected int _printCount;
        protected string _memo;
        protected bool _visible;
        protected int _dispOrder;
		protected string _endBloodOperName;
		protected long? _endBloodOperId;
		protected DateTime? _endBloodOperTime;
		protected string _endBloodReason;
		protected BDict _bDEndBReason;

		protected BloodBPreForm _bloodBPreForm;
		protected BloodBReqForm _bloodBReqForm;
		protected BloodBReqItem _bloodBReqItem;
		protected BloodPatinfo _bloodPatinfo;
		#endregion

		#region Constructors

		public BloodBOutForm() { }

		public BloodBOutForm( long labID, string bOutFormNo, int outType, long operatorID, string operatorName, DateTime operTime, long checkID, string checkCName, DateTime checkTime, int checkFlag, int confirmCompletion, int handoverCompletion, int courseCompletion, int recoverCompletion, DateTime printTime, int printCount, string memo, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodBPreForm bloodBPreForm, BloodBReqForm bloodBReqForm, BloodBReqItem bloodBReqItem, BloodPatinfo bloodPatinfo )
		{
			this._labID = labID;
			this._bOutFormNo = bOutFormNo;
			this._outType = outType;
			this._operatorID = operatorID;
			this._operatorName = operatorName;
			this._operTime = operTime;
			this._checkID = checkID;
			this._checkCName = checkCName;
			this._checkTime = checkTime;
			this._checkFlag = checkFlag;
			this._confirmCompletion = confirmCompletion;
			this._handoverCompletion = handoverCompletion;
			this._courseCompletion = courseCompletion;
			this._recoverCompletion = recoverCompletion;
			this._printTime = printTime;
			this._printCount = printCount;
			this._memo = memo;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBPreForm = bloodBPreForm;
			this._bloodBReqForm = bloodBReqForm;
			this._bloodBReqItem = bloodBReqItem;
			this._bloodPatinfo = bloodPatinfo;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "发血单号", ShortCode = "BOutFormNo", Desc = "发血单号", ContextType = SysDic.All, Length = 20)]
        public virtual string BOutFormNo
		{
			get { return _bOutFormNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BOutFormNo", value, value.ToString());
				_bOutFormNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "出库类型", ShortCode = "OutType", Desc = "出库类型", ContextType = SysDic.All, Length = 4)]
        public virtual int OutType
		{
			get { return _outType; }
			set { _outType = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作人编号", ShortCode = "OperatorID", Desc = "操作人编号", ContextType = SysDic.All, Length = 8)]
		public virtual long? OperatorID
		{
			get { return _operatorID; }
			set { _operatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "操作人姓名", ShortCode = "OperatorName", Desc = "操作人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string OperatorName
		{
			get { return _operatorName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for OperatorName", value, value.ToString());
				_operatorName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作时间", ShortCode = "OperTime", Desc = "操作时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? OperTime
		{
			get { return _operTime; }
			set { _operTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核者编号", ShortCode = "CheckID", Desc = "审核者编号", ContextType = SysDic.All, Length = 8)]
		public virtual long? CheckID
		{
			get { return _checkID; }
			set { _checkID = value; }
		}

        [DataMember]
        [DataDesc(CName = "审核者姓名", ShortCode = "CheckCName", Desc = "审核者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CheckCName
		{
			get { return _checkCName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CheckCName", value, value.ToString());
				_checkCName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核时间", ShortCode = "CheckTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CheckTime
		{
			get { return _checkTime; }
			set { _checkTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "审核标志", ShortCode = "CheckFlag", Desc = "审核标志", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckFlag
		{
			get { return _checkFlag; }
			set { _checkFlag = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "打印时间", ShortCode = "PrintTime", Desc = "打印时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PrintTime
		{
			get { return _printTime; }
			set { _printTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "打印次数", ShortCode = "PrintCount", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
		{
			get { return _printCount; }
			set { _printCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
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
        [DataDesc(CName = "配血主单", ShortCode = "BloodBPreForm", Desc = "配血主单")]
		public virtual BloodBPreForm BloodBPreForm
		{
			get { return _bloodBPreForm; }
			set { _bloodBPreForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血申请主单表", ShortCode = "BloodBReqForm", Desc = "用血申请主单表")]
		public virtual BloodBReqForm BloodBReqForm
		{
			get { return _bloodBReqForm; }
			set { _bloodBReqForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血申请明细信息表", ShortCode = "BloodBReqItem", Desc = "用血申请明细信息表")]
		public virtual BloodBReqItem BloodBReqItem
		{
			get { return _bloodBReqItem; }
			set { _bloodBReqItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "病人就诊记录信息表", ShortCode = "BloodPatinfo", Desc = "病人就诊记录信息表")]
		public virtual BloodPatinfo BloodPatinfo
		{
			get { return _bloodPatinfo; }
			set { _bloodPatinfo = value; }
		}
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "终止输血操作人Id", ShortCode = "EndBloodOperId", Desc = "终止输血操作人Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? EndBloodOperId
		{
			get { return _endBloodOperId; }
			set { _endBloodOperId = value; }
		}
		[DataMember]
		[DataDesc(CName = "终止输血操作人", ShortCode = "EndBloodOperName", Desc = "终止输血操作人", ContextType = SysDic.All, Length = 50)]
		public virtual string EndBloodOperName
		{
			get { return _endBloodOperName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EndBloodOperName", value, value.ToString());
				_endBloodOperName = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "终止输血操作时间", ShortCode = "EndBloodOperTime", Desc = "终止输血操作时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? EndBloodOperTime
		{
			get { return _endBloodOperTime; }
			set { _endBloodOperTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "终止输血原因", ShortCode = "EndBloodReason", Desc = "终止输血原因", ContextType = SysDic.All, Length = 500)]
		public virtual string EndBloodReason
		{
			get { return _endBloodReason; }
			set
			{
				if (value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for EndBloodReason", value, value.ToString());
				_endBloodReason = value;
			}
		}
		[DataMember]
		[DataDesc(CName = "终止输血原因字典", ShortCode = "BDEndBReason", Desc = "终止输血原因字典")]
		public virtual BDict BDEndBReason
		{
			get { return _bDEndBReason; }
			set { _bDEndBReason = value; }
		}
		#endregion
	}
	#endregion
}