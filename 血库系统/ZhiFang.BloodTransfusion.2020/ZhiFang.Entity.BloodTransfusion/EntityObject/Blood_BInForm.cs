using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBInForm

	/// <summary>
	/// BloodBInForm object for NHibernate mapped table 'Blood_BInForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "入库主单", ClassCName = "BloodBInForm", ShortCode = "BloodBInForm", Desc = "入库主单")]
	public class BloodBInForm : BaseEntity
	{
		#region Member Variables
		
        protected string _bInFormNo;
        protected long? _bSourceID;
        protected string _inType;
        protected string _inFileName;
        protected DateTime _operTime;
        protected long _operatorID;
        protected string _operator;
        protected long? _checkerId;
        protected string _checker;
        protected DateTime? _checkTime;
        protected long? _checkFlag;
        protected DateTime? _printTime;
        protected int _printCount;
        protected string _memo;
        protected string _yqCode;
        protected bool _visible;
        protected int _dispOrder;
		protected IList<BloodBInItem> _bloodBInItemList; 

		#endregion

		#region Constructors

		public BloodBInForm() { }

		public BloodBInForm( long labID, string bInFormNo, long bSourceID, string inType, string inFileName, DateTime operTime, long operatorID, string operator1, long checkerId, string checker, DateTime checkTime, long checkFlag, DateTime printTime, int printCount, string memo, string yqCode, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._bInFormNo = bInFormNo;
			this._bSourceID = bSourceID;
			this._inType = inType;
			this._inFileName = inFileName;
			this._operTime = operTime;
			this._operatorID = operatorID;
			this._operator = operator1;
			this._checkerId = checkerId;
			this._checker = checker;
			this._checkTime = checkTime;
			this._checkFlag = checkFlag;
			this._printTime = printTime;
			this._printCount = printCount;
			this._memo = memo;
			this._yqCode = yqCode;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "入库记录主单号", ShortCode = "BInFormNo", Desc = "入库记录主单号", ContextType = SysDic.All, Length = 20)]
        public virtual string BInFormNo
		{
			get { return _bInFormNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BInFormNo", value, value.ToString());
				_bInFormNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "血液来源ID", ShortCode = "BSourceID", Desc = "血液来源ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BSourceID
		{
			get { return _bSourceID; }
			set { _bSourceID = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库类型", ShortCode = "InType", Desc = "入库类型", ContextType = SysDic.All, Length = 20)]
        public virtual string InType
		{
			get { return _inType; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for InType", value, value.ToString());
				_inType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "入库文件", ShortCode = "InFileName", Desc = "入库文件", ContextType = SysDic.All, Length = 50)]
        public virtual string InFileName
		{
			get { return _inFileName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for InFileName", value, value.ToString());
				_inFileName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核时间", ShortCode = "OperTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime OperTime
		{
			get { return _operTime; }
			set { _operTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作者ID", ShortCode = "OperatorID", Desc = "操作者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long OperatorID
		{
			get { return _operatorID; }
			set { _operatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "操作者ID", ShortCode = "Operator", Desc = "操作者ID", ContextType = SysDic.All, Length = 20)]
        public virtual string Operator
		{
			get { return _operator; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Operator", value, value.ToString());
				_operator = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核人Id", ShortCode = "CheckerId", Desc = "审核人Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? CheckerId
		{
			get { return _checkerId; }
			set { _checkerId = value; }
		}

        [DataMember]
        [DataDesc(CName = "审核人", ShortCode = "Checker", Desc = "审核人", ContextType = SysDic.All, Length = 20)]
        public virtual string Checker
		{
			get { return _checker; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Checker", value, value.ToString());
				_checker = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核标志", ShortCode = "CheckFlag", Desc = "审核标志", ContextType = SysDic.All, Length = 8)]
		public virtual long? CheckFlag
		{
			get { return _checkFlag; }
			set { _checkFlag = value; }
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
        [DataDesc(CName = "院区", ShortCode = "YqCode", Desc = "院区", ContextType = SysDic.All, Length = 20)]
        public virtual string YqCode
		{
			get { return _yqCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for YqCode", value, value.ToString());
				_yqCode = value;
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
        [DataDesc(CName = "入库明细", ShortCode = "BloodBInItemList", Desc = "入库明细")]
		public virtual IList<BloodBInItem> BloodBInItemList
		{
			get
			{
				if (_bloodBInItemList==null)
				{
					_bloodBInItemList = new List<BloodBInItem>();
				}
				return _bloodBInItemList;
			}
			set { _bloodBInItemList = value; }
		}

        
		#endregion
	}
	#endregion
}