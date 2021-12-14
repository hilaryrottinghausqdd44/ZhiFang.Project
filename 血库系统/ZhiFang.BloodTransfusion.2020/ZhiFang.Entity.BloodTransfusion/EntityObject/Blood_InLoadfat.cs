using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodInLoadfat

	/// <summary>
	/// BloodInLoadfat object for NHibernate mapped table 'Blood_InLoadfat'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "入库中间表", ClassCName = "BloodInLoadfat", ShortCode = "BloodInLoadfat", Desc = "入库中间表")]
	public class BloodInLoadfat : BaseEntity
	{
		#region Member Variables
		
        protected string _inFileName;
        protected string _bLOODBILLNO;
        protected string _bBagCode;
        protected string _bloodName;
        protected string _pcode;
        protected decimal _bcount;
        protected string _bUnit;
        protected string _aBO;
        protected string _rhd;
        protected DateTime? _invalidDate;
        protected DateTime? _collectDate;
        protected DateTime? _sendDate;
        protected decimal _price;
        protected DateTime? _lfdate;
        protected string _invalidCode;
        protected string _b3Code;
        protected int _lFflag;
        protected int _hflag;
        protected string _yqcode;
        protected bool _visible;
        protected int _dispOrder;
		protected BDict _bloodBank;

		#endregion

		#region Constructors

		public BloodInLoadfat() { }

		public BloodInLoadfat( long labID, string inFileName, string bLOODBILLNO, string bBagCode, string bloodName, string pcode, decimal bcount, string bUnit, string aBO, string rhd, DateTime invalidDate, DateTime collectDate, DateTime sendDate, decimal price, DateTime lfdate, string invalidCode, string b3Code, int lFflag, int hflag, string yqcode, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BDict bBankNo )
		{
			this._labID = labID;
			this._inFileName = inFileName;
			this._bLOODBILLNO = bLOODBILLNO;
			this._bBagCode = bBagCode;
			this._bloodName = bloodName;
			this._pcode = pcode;
			this._bcount = bcount;
			this._bUnit = bUnit;
			this._aBO = aBO;
			this._rhd = rhd;
			this._invalidDate = invalidDate;
			this._collectDate = collectDate;
			this._sendDate = sendDate;
			this._price = price;
			this._lfdate = lfdate;
			this._invalidCode = invalidCode;
			this._b3Code = b3Code;
			this._lFflag = lFflag;
			this._hflag = hflag;
			this._yqcode = yqcode;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBank = bBankNo;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "文件名", ShortCode = "InFileName", Desc = "文件名", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "血站单号", ShortCode = "BLOODBILLNO", Desc = "血站单号", ContextType = SysDic.All, Length = 50)]
        public virtual string BLOODBILLNO
		{
			get { return _bLOODBILLNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BLOODBILLNO", value, value.ToString());
				_bLOODBILLNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "血袋码", ShortCode = "BBagCode", Desc = "血袋码", ContextType = SysDic.All, Length = 50)]
        public virtual string BBagCode
		{
			get { return _bBagCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BBagCode", value, value.ToString());
				_bBagCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "血制品名称", ShortCode = "BloodName", Desc = "血制品名称", ContextType = SysDic.All, Length = 50)]
        public virtual string BloodName
		{
			get { return _bloodName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BloodName", value, value.ToString());
				_bloodName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "产品码", ShortCode = "Pcode", Desc = "产品码", ContextType = SysDic.All, Length = 20)]
        public virtual string Pcode
		{
			get { return _pcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Pcode", value, value.ToString());
				_pcode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "规格", ShortCode = "Bcount", Desc = "规格", ContextType = SysDic.All, Length = 9)]
        public virtual decimal Bcount
		{
			get { return _bcount; }
			set { _bcount = value; }
		}

        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "BUnit", Desc = "单位", ContextType = SysDic.All, Length = 20)]
        public virtual string BUnit
		{
			get { return _bUnit; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BUnit", value, value.ToString());
				_bUnit = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "ABO血型", ShortCode = "ABO", Desc = "ABO血型", ContextType = SysDic.All, Length = 10)]
        public virtual string ABO
		{
			get { return _aBO; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ABO", value, value.ToString());
				_aBO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "RH血型", ShortCode = "Rhd", Desc = "RH血型", ContextType = SysDic.All, Length = 10)]
        public virtual string Rhd
		{
			get { return _rhd; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Rhd", value, value.ToString());
				_rhd = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失效时间", ShortCode = "InvalidDate", Desc = "失效时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InvalidDate
		{
			get { return _invalidDate; }
			set { _invalidDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采集时间", ShortCode = "CollectDate", Desc = "采集时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectDate
		{
			get { return _collectDate; }
			set { _collectDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "血站送血时间", ShortCode = "SendDate", Desc = "血站送血时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? SendDate
		{
			get { return _sendDate; }
			set { _sendDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "价格", ShortCode = "Price", Desc = "价格", ContextType = SysDic.All, Length = 9)]
        public virtual decimal Price
		{
			get { return _price; }
			set { _price = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Lfdate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Lfdate
		{
			get { return _lfdate; }
			set { _lfdate = value; }
		}

        [DataMember]
        [DataDesc(CName = "失效日期码", ShortCode = "InvalidCode", Desc = "失效日期码", ContextType = SysDic.All, Length = 50)]
        public virtual string InvalidCode
		{
			get { return _invalidCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for InvalidCode", value, value.ToString());
				_invalidCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "唯一码", ShortCode = "B3Code", Desc = "唯一码", ContextType = SysDic.All, Length = 50)]
        public virtual string B3Code
		{
			get { return _b3Code; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for B3Code", value, value.ToString());
				_b3Code = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "导入标志", ShortCode = "LFflag", Desc = "导入标志", ContextType = SysDic.All, Length = 4)]
        public virtual int LFflag
		{
			get { return _lFflag; }
			set { _lFflag = value; }
		}

        [DataMember]
        [DataDesc(CName = "His标志", ShortCode = "Hflag", Desc = "His标志", ContextType = SysDic.All, Length = 4)]
        public virtual int Hflag
		{
			get { return _hflag; }
			set { _hflag = value; }
		}

        [DataMember]
        [DataDesc(CName = "院区编号", ShortCode = "Yqcode", Desc = "院区编号", ContextType = SysDic.All, Length = 50)]
        public virtual string Yqcode
		{
			get { return _yqcode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Yqcode", value, value.ToString());
				_yqcode = value;
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
        [DataDesc(CName = "血站信息", ShortCode = "BBankNo", Desc = "血站信息")]
		public virtual BDict BloodBank
		{
			get { return _bloodBank; }
			set { _bloodBank = value; }
		}

        
		#endregion
	}
	#endregion
}