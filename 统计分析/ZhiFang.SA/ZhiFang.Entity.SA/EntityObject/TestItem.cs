using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.SA
{
	#region TestItem

	/// <summary>
	/// TestItem object for NHibernate mapped table 'TestItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "TestItem", ShortCode = "TestItem", Desc = "")]
	public class TestItem : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _eName;
        protected string _shortName;
        protected string _shortCode;
        protected string _diagMethod;
        protected string _unit;
        protected int _isCalc;
        protected int _visible;
        protected int _dispOrder;
        protected int _prec;
        protected int _isProfile;
        protected string _orderNo;
        protected string _standardCode;
        protected string _itemDesc;
        protected int _defaultSType;
        protected string _specName;
        protected string _zdy1;
        protected string _zdy2;
        protected double _fWorkLoad;
        protected int _secretgrade;
        protected int _cuegrade;
        protected int _isDoctorItem;
        protected string _isNurseItem;
        protected int _ischargeItem;
        protected int _hisDispOrder;
        protected string _code1;
        protected string _code2;
        protected string _code3;
        protected string _code4;
        protected string _code5;
        protected bool _bItemAbnormal;
        protected string _itemAbnormalInfo;
        protected string _defaultReagent;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected int _userNo;
        protected DateTime? _dataUpdateTime;
        protected string _code6;
        protected string _code7;
        protected string _code8;
        protected string _code9;
        protected string _code10;
        protected int _isUnion;
        protected int _sectorTypeNo;
        protected int _isPrint;
		

		#endregion

		#region Constructors

		public TestItem() { }

		public TestItem( string cName, string eName, string shortName, string shortCode, string diagMethod, string unit, int isCalc, int visible, int dispOrder, int prec, int isProfile, string orderNo, string standardCode, string itemDesc, int defaultSType, string specName, string zdy1, string zdy2, double fWorkLoad, int secretgrade, int cuegrade, int isDoctorItem, string isNurseItem, int ischargeItem, int hisDispOrder, string code1, string code2, string code3, string code4, string code5, bool bItemAbnormal, string itemAbnormalInfo, string defaultReagent, string useCode, string standCode, string deveCode, int userNo, DateTime dataAddTime, DateTime dataUpdateTime, string code6, string code7, string code8, string code9, string code10, int isUnion, int sectorTypeNo, int isPrint )
		{
			this._cName = cName;
			this._eName = eName;
			this._shortName = shortName;
			this._shortCode = shortCode;
			this._diagMethod = diagMethod;
			this._unit = unit;
			this._isCalc = isCalc;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._prec = prec;
			this._isProfile = isProfile;
			this._orderNo = orderNo;
			this._standardCode = standardCode;
			this._itemDesc = itemDesc;
			this._defaultSType = defaultSType;
			this._specName = specName;
			this._zdy1 = zdy1;
			this._zdy2 = zdy2;
			this._fWorkLoad = fWorkLoad;
			this._secretgrade = secretgrade;
			this._cuegrade = cuegrade;
			this._isDoctorItem = isDoctorItem;
			this._isNurseItem = isNurseItem;
			this._ischargeItem = ischargeItem;
			this._hisDispOrder = hisDispOrder;
			this._code1 = code1;
			this._code2 = code2;
			this._code3 = code3;
			this._code4 = code4;
			this._code5 = code5;
			this._bItemAbnormal = bItemAbnormal;
			this._itemAbnormalInfo = itemAbnormalInfo;
			this._defaultReagent = defaultReagent;
			this._useCode = useCode;
			this._standCode = standCode;
			this._deveCode = deveCode;
			this._userNo = userNo;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._code6 = code6;
			this._code7 = code7;
			this._code8 = code8;
			this._code9 = code9;
			this._code10 = code10;
			this._isUnion = isUnion;
			this._sectorTypeNo = sectorTypeNo;
			this._isPrint = isPrint;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ShortName
		{
			get { return _shortName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ShortName", value, value.ToString());
				_shortName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DiagMethod", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string DiagMethod
		{
			get { return _diagMethod; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for DiagMethod", value, value.ToString());
				_diagMethod = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Unit", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Unit
		{
			get { return _unit; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Unit", value, value.ToString());
				_unit = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCalc", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCalc
		{
			get { return _isCalc; }
			set { _isCalc = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Prec", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Prec
		{
			get { return _prec; }
			set { _prec = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsProfile", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsProfile
		{
			get { return _isProfile; }
			set { _isProfile = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OrderNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string OrderNo
		{
			get { return _orderNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for OrderNo", value, value.ToString());
				_orderNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StandardCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string StandardCode
		{
			get { return _standardCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for StandardCode", value, value.ToString());
				_standardCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemDesc", Desc = "", ContextType = SysDic.All, Length = 1024)]
        public virtual string ItemDesc
		{
			get { return _itemDesc; }
			set
			{
				if ( value != null && value.Length > 1024)
					throw new ArgumentOutOfRangeException("Invalid value for ItemDesc", value, value.ToString());
				_itemDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DefaultSType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DefaultSType
		{
			get { return _defaultSType; }
			set { _defaultSType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SpecName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SpecName
		{
			get { return _specName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for SpecName", value, value.ToString());
				_specName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy1", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Zdy1
		{
			get { return _zdy1; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy1", value, value.ToString());
				_zdy1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy2", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Zdy2
		{
			get { return _zdy2; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy2", value, value.ToString());
				_zdy2 = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "FWorkLoad", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double FWorkLoad
		{
			get { return _fWorkLoad; }
			set { _fWorkLoad = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Secretgrade", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Secretgrade
		{
			get { return _secretgrade; }
			set { _secretgrade = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Cuegrade", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Cuegrade
		{
			get { return _cuegrade; }
			set { _cuegrade = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsDoctorItem", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsDoctorItem
		{
			get { return _isDoctorItem; }
			set { _isDoctorItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsNurseItem", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string IsNurseItem
		{
			get { return _isNurseItem; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for IsNurseItem", value, value.ToString());
				_isNurseItem = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IschargeItem", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IschargeItem
		{
			get { return _ischargeItem; }
			set { _ischargeItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisDispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int HisDispOrder
		{
			get { return _hisDispOrder; }
			set { _hisDispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "住院编码", ShortCode = "Code1", Desc = "住院编码", ContextType = SysDic.All, Length = 50)]
        public virtual string Code1
		{
			get { return _code1; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code1", value, value.ToString());
				_code1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "门诊编码", ShortCode = "Code2", Desc = "门诊编码", ContextType = SysDic.All, Length = 50)]
        public virtual string Code2
		{
			get { return _code2; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code2", value, value.ToString());
				_code2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "体检编码", ShortCode = "Code3", Desc = "体检编码", ContextType = SysDic.All, Length = 50)]
        public virtual string Code3
		{
			get { return _code3; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code3", value, value.ToString());
				_code3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "未使用编码", ShortCode = "Code4", Desc = "未使用编码", ContextType = SysDic.All, Length = 50)]
        public virtual string Code4
		{
			get { return _code4; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code4", value, value.ToString());
				_code4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "未使用编码", ShortCode = "Code5", Desc = "未使用编码", ContextType = SysDic.All, Length = 50)]
        public virtual string Code5
		{
			get { return _code5; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code5", value, value.ToString());
				_code5 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BItemAbnormal", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool BItemAbnormal
		{
			get { return _bItemAbnormal; }
			set { _bItemAbnormal = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemAbnormalInfo", Desc = "", ContextType = SysDic.All, Length = 80)]
        public virtual string ItemAbnormalInfo
		{
			get { return _itemAbnormalInfo; }
			set
			{
				if ( value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for ItemAbnormalInfo", value, value.ToString());
				_itemAbnormalInfo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DefaultReagent", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string DefaultReagent
		{
			get { return _defaultReagent; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for DefaultReagent", value, value.ToString());
				_defaultReagent = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UseCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
		{
			get { return _useCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
				_useCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StandCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
		{
			get { return _standCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for StandCode", value, value.ToString());
				_standCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeveCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DeveCode
		{
			get { return _deveCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DeveCode", value, value.ToString());
				_deveCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UserNo
		{
			get { return _userNo; }
			set { _userNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code6", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code6
		{
			get { return _code6; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code6", value, value.ToString());
				_code6 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code7", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code7
		{
			get { return _code7; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code7", value, value.ToString());
				_code7 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code8", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code8
		{
			get { return _code8; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code8", value, value.ToString());
				_code8 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code9", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code9
		{
			get { return _code9; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code9", value, value.ToString());
				_code9 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code10", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code10
		{
			get { return _code10; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code10", value, value.ToString());
				_code10 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUnion", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsUnion
		{
			get { return _isUnion; }
			set { _isUnion = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectorTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SectorTypeNo
		{
			get { return _sectorTypeNo; }
			set { _sectorTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsPrint", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsPrint
		{
			get { return _isPrint; }
			set { _isPrint = value; }
		}

		
		#endregion
	}
	#endregion
}