using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BMicroBloodCultureBottleManufacturer

	/// <summary>
	/// BMicroBloodCultureBottleManufacturer object for NHibernate mapped table 'B_MicroBloodCultureBottleManufacturer'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物血培养瓶厂商", ClassCName = "BMicroBloodCultureBottleManufacturer", ShortCode = "BMicroBloodCultureBottleManufacturer", Desc = "微生物血培养瓶厂商")]
	public class BMicroBloodCultureBottleManufacturer : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _contactPerson;
        protected string _telephone;
        protected string _cellPhoneNumber;
        protected string _manufacturerAddress;
        protected string _zipCode;
        protected string _eMail;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected IList<BMicroBCBottleTypeOfManufacturer> _bMicroBCBottleTypeOfManufacturerList; 
		protected IList<MEMicroBCBottleManageInfo> _mEMicroBCBottleManageInfoList; 

		#endregion

		#region Constructors

		public BMicroBloodCultureBottleManufacturer() { }

		public BMicroBloodCultureBottleManufacturer( long labID, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string contactPerson, string telephone, string cellPhoneNumber, string manufacturerAddress, string zipCode, string eMail, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._contactPerson = contactPerson;
			this._telephone = telephone;
			this._cellPhoneNumber = cellPhoneNumber;
			this._manufacturerAddress = manufacturerAddress;
			this._zipCode = zipCode;
			this._eMail = eMail;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "联系人", ShortCode = "ContactPerson", Desc = "联系人", ContextType = SysDic.All, Length = 50)]
        public virtual string ContactPerson
		{
			get { return _contactPerson; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ContactPerson", value, value.ToString());
				_contactPerson = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "固定电话", ShortCode = "Telephone", Desc = "固定电话", ContextType = SysDic.All, Length = 100)]
        public virtual string Telephone
		{
			get { return _telephone; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Telephone", value, value.ToString());
				_telephone = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "手机号", ShortCode = "CellPhoneNumber", Desc = "手机号", ContextType = SysDic.All, Length = 100)]
        public virtual string CellPhoneNumber
		{
			get { return _cellPhoneNumber; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for CellPhoneNumber", value, value.ToString());
				_cellPhoneNumber = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "地址", ShortCode = "ManufacturerAddress", Desc = "地址", ContextType = SysDic.All, Length = 200)]
        public virtual string ManufacturerAddress
		{
			get { return _manufacturerAddress; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ManufacturerAddress", value, value.ToString());
				_manufacturerAddress = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "邮编", ShortCode = "ZipCode", Desc = "邮编", ContextType = SysDic.All, Length = 20)]
        public virtual string ZipCode
		{
			get { return _zipCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ZipCode", value, value.ToString());
				_zipCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "电子信箱", ShortCode = "EMail", Desc = "电子信箱", ContextType = SysDic.All, Length = 200)]
        public virtual string EMail
		{
			get { return _eMail; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for EMail", value, value.ToString());
				_eMail = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物血培养瓶类型与厂商对应关系", ShortCode = "BMicroBCBottleTypeOfManufacturerList", Desc = "微生物血培养瓶类型与厂商对应关系")]
		public virtual IList<BMicroBCBottleTypeOfManufacturer> BMicroBCBottleTypeOfManufacturerList
		{
			get
			{
				if (_bMicroBCBottleTypeOfManufacturerList==null)
				{
					_bMicroBCBottleTypeOfManufacturerList = new List<BMicroBCBottleTypeOfManufacturer>();
				}
				return _bMicroBCBottleTypeOfManufacturerList;
			}
			set { _bMicroBCBottleTypeOfManufacturerList = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物血培养瓶管理信息表", ShortCode = "MEMicroBCBottleManageInfoList", Desc = "微生物血培养瓶管理信息表")]
		public virtual IList<MEMicroBCBottleManageInfo> MEMicroBCBottleManageInfoList
		{
			get
			{
				if (_mEMicroBCBottleManageInfoList==null)
				{
					_mEMicroBCBottleManageInfoList = new List<MEMicroBCBottleManageInfo>();
				}
				return _mEMicroBCBottleManageInfoList;
			}
			set { _mEMicroBCBottleManageInfoList = value; }
		}

        
		#endregion
	}
	#endregion
}