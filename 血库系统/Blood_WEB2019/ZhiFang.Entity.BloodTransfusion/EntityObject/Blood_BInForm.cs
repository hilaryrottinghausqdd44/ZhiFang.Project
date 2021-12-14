using System;
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
    [DataDesc(CName = "", ClassCName = "BloodBInForm", ShortCode = "BloodBInForm", Desc = "")]
	public class BloodBInForm : BaseEntityServiceByString
    {
		#region Member Variables
		
        protected DateTime _operTime;
        protected DateTime? _checkTime;
        protected int _operatorID;
        protected int _checker;
        protected int _checkFlag;
        protected int _bSourceID;
        protected string _inType;
        protected string _inFileName;
        protected DateTime? _printTime;
        protected int _printCount;
        protected string _memo;
        protected string _zx1;
        protected string _zx2;
        protected string _zx3;
        protected string _yqCode;
        protected int _dispOrder;
        protected bool _visible;

		#endregion

		#region Constructors

		public BloodBInForm() { }

		public BloodBInForm( DateTime operTime, DateTime checkTime, int operatorID, int checker, int checkFlag, int bSourceID, string inType, string inFileName, DateTime printTime, int printCount, string memo, string zx1, string zx2, string zx3, string yqCode, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible )
		{
			this._operTime = operTime;
			this._checkTime = checkTime;
			this._operatorID = operatorID;
			this._checker = checker;
			this._checkFlag = checkFlag;
			this._bSourceID = bSourceID;
			this._inType = inType;
			this._inFileName = inFileName;
			this._printTime = printTime;
			this._printCount = printCount;
			this._memo = memo;
			this._zx1 = zx1;
			this._zx2 = zx2;
			this._zx3 = zx3;
			this._yqCode = yqCode;
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._visible = visible;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime OperTime
		{
			get { return _operTime; }
			set { _operTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CheckTime
		{
			get { return _checkTime; }
			set { _checkTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperatorID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int OperatorID
		{
			get { return _operatorID; }
			set { _operatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Checker", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Checker
		{
			get { return _checker; }
			set { _checker = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckFlag
		{
			get { return _checkFlag; }
			set { _checkFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BSourceID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BSourceID
		{
			get { return _bSourceID; }
			set { _bSourceID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InType", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "InFileName", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "PrintTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PrintTime
		{
			get { return _printTime; }
			set { _printTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
		{
			get { return _printCount; }
			set { _printCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 500)]
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
        [DataDesc(CName = "", ShortCode = "Zx1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zx1
		{
			get { return _zx1; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Zx1", value, value.ToString());
				_zx1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zx2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zx2
		{
			get { return _zx2; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Zx2", value, value.ToString());
				_zx2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zx3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zx3
		{
			get { return _zx3; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Zx3", value, value.ToString());
				_zx3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "YqCode", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        
		#endregion
	}
	#endregion
}