using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodInChecked

	/// <summary>
	/// BloodInChecked object for NHibernate mapped table 'Blood_In_Checked'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "盘点主单表", ClassCName = "BloodInChecked", ShortCode = "BloodInChecked", Desc = "盘点主单表")]
	public class BloodInChecked : BaseEntity
	{
		#region Member Variables
		
        protected string _inCheckedNo;
        protected DateTime? _startDate;
        protected DateTime? _endDate;
        protected long? _checkID;
        protected string _checker;
        protected int _checkFlag;
        protected int _dispOrder;
        protected bool _visible;
		protected BloodABO _bloodABO;
		protected BloodClass _bloodClass;
		protected BDict _bloodIceBox;
		protected BloodStyle _bloodStyle;

		#endregion

		#region Constructors

		public BloodInChecked() { }

		public BloodInChecked( long labID, string inCheckedNo, DateTime startDate, DateTime endDate, long checkID, string checker, int checkFlag, int dispOrder, bool visible, DateTime dataAddTime, byte[] dataTimeStamp, BloodABO aboNo, BloodClass bloodClass, BDict iceboxNo, BloodStyle bloodStyle )
		{
			this._labID = labID;
			this._inCheckedNo = inCheckedNo;
			this._startDate = startDate;
			this._endDate = endDate;
			this._checkID = checkID;
			this._checker = checker;
			this._checkFlag = checkFlag;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodABO = aboNo;
			this._bloodClass = bloodClass;
			this._bloodIceBox = iceboxNo;
			this._bloodStyle = bloodStyle;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "盘点主单编号", ShortCode = "InCheckedNo", Desc = "盘点主单编号", ContextType = SysDic.All, Length = 20)]
        public virtual string InCheckedNo
		{
			get { return _inCheckedNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for InCheckedNo", value, value.ToString());
				_inCheckedNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开始时间", ShortCode = "StartDate", Desc = "开始时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? StartDate
		{
			get { return _startDate; }
			set { _startDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结束时间", ShortCode = "EndDate", Desc = "结束时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? EndDate
		{
			get { return _endDate; }
			set { _endDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核者", ShortCode = "CheckID", Desc = "审核者", ContextType = SysDic.All, Length = 8)]
		public virtual long? CheckID
		{
			get { return _checkID; }
			set { _checkID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Checker", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Checker
		{
			get { return _checker; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Checker", value, value.ToString());
				_checker = value;
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
        [DataDesc(CName = "血型表", ShortCode = "BloodABO", Desc = "血型表")]
		public virtual BloodABO BloodABO
		{
			get { return _bloodABO; }
			set { _bloodABO = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋分类", ShortCode = "BloodClass", Desc = "血袋分类")]
		public virtual BloodClass BloodClass
		{
			get { return _bloodClass; }
			set { _bloodClass = value; }
		}

        [DataMember]
        [DataDesc(CName = "冰箱", ShortCode = "BloodIceBox", Desc = "冰箱")]
		public virtual BDict BloodIceBox
		{
			get { return _bloodIceBox; }
			set { _bloodIceBox = value; }
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