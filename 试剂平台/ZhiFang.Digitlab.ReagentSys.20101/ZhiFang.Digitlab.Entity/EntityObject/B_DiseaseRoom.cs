using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BDiseaseRoom

	/// <summary>
	/// BDiseaseRoom object for NHibernate mapped table 'B_DiseaseRoom'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "病房", ClassCName = "BDiseaseRoom", ShortCode = "BDiseaseRoom", Desc = "病房")]
	public class BDiseaseRoom : BaseEntity
	{
		#region Member Variables
		
        protected long? _parentID;
        protected int _levelNum;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected string _tel;
        protected string _fax;
        protected string _zipCode;
        protected string _address;
        protected string _contact;
        protected string _parentOrg;
        protected string _orgType;
        protected string _orgCode;
        protected DateTime? _dataUpdateTime;
		protected IList<MEPTOrderForm> _mEPTOrderFormList; 

		#endregion

		#region Constructors

		public BDiseaseRoom() { }

		public BDiseaseRoom( long labID, long parentID, int levelNum, string useCode, string standCode, string deveCode, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, int dispOrder, string tel, string fax, string zipCode, string address, string contact, string parentOrg, string orgType, string orgCode, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._parentID = parentID;
			this._levelNum = levelNum;
			this._useCode = useCode;
			this._standCode = standCode;
			this._deveCode = deveCode;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._tel = tel;
			this._fax = fax;
			this._zipCode = zipCode;
			this._address = address;
			this._contact = contact;
			this._parentOrg = parentOrg;
			this._orgType = orgType;
			this._orgCode = orgCode;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "树形结构父级ID", ShortCode = "ParentID", Desc = "树形结构父级ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ParentID
		{
			get { return _parentID; }
			set { _parentID = value; }
		}

        [DataMember]
        [DataDesc(CName = "树形结构层级", ShortCode = "LevelNum", Desc = "树形结构层级", ContextType = SysDic.All, Length = 4)]
        public virtual int LevelNum
		{
			get { return _levelNum; }
			set { _levelNum = value; }
		}

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "开发商标准代码", ShortCode = "DeveCode", Desc = "开发商标准代码", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "部门电话", ShortCode = "Tel", Desc = "部门电话", ContextType = SysDic.All, Length = 50)]
        public virtual string Tel
		{
			get { return _tel; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Tel", value, value.ToString());
				_tel = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "部门传真", ShortCode = "Fax", Desc = "部门传真", ContextType = SysDic.All, Length = 50)]
        public virtual string Fax
		{
			get { return _fax; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Fax", value, value.ToString());
				_fax = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "邮编", ShortCode = "ZipCode", Desc = "邮编", ContextType = SysDic.All, Length = 50)]
        public virtual string ZipCode
		{
			get { return _zipCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZipCode", value, value.ToString());
				_zipCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "地址", ShortCode = "Address", Desc = "地址", ContextType = SysDic.All, Length = 100)]
        public virtual string Address
		{
			get { return _address; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Address", value, value.ToString());
				_address = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "部门联系人", ShortCode = "Contact", Desc = "部门联系人", ContextType = SysDic.All, Length = 50)]
        public virtual string Contact
		{
			get { return _contact; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Contact", value, value.ToString());
				_contact = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "上级机构", ShortCode = "ParentOrg", Desc = "上级机构", ContextType = SysDic.All, Length = 44)]
        public virtual string ParentOrg
		{
			get { return _parentOrg; }
			set
			{
				if ( value != null && value.Length > 44)
					throw new ArgumentOutOfRangeException("Invalid value for ParentOrg", value, value.ToString());
				_parentOrg = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "机构类型", ShortCode = "OrgType", Desc = "机构类型", ContextType = SysDic.All, Length = 12)]
        public virtual string OrgType
		{
			get { return _orgType; }
			set
			{
				if ( value != null && value.Length > 12)
					throw new ArgumentOutOfRangeException("Invalid value for OrgType", value, value.ToString());
				_orgType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "机构编码", ShortCode = "OrgCode", Desc = "机构编码", ContextType = SysDic.All, Length = 12)]
        public virtual string OrgCode
		{
			get { return _orgCode; }
			set
			{
				if ( value != null && value.Length > 12)
					throw new ArgumentOutOfRangeException("Invalid value for OrgCode", value, value.ToString());
				_orgCode = value;
			}
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
        [DataDesc(CName = "医嘱单", ShortCode = "MEPTOrderFormList", Desc = "医嘱单")]
		public virtual IList<MEPTOrderForm> MEPTOrderFormList
		{
			get
			{
				if (_mEPTOrderFormList==null)
				{
					_mEPTOrderFormList = new List<MEPTOrderForm>();
				}
				return _mEPTOrderFormList;
			}
			set { _mEPTOrderFormList = value; }
		}

        
		#endregion
	}
	#endregion
}