using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region HRDept

	/// <summary>
	/// HRDept object for NHibernate mapped table 'HR_Dept'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "部门", ClassCName = "", ShortCode = "BM", Desc = "部门")]
    public class HRDept : BaseEntity
	{
		#region Member Variables
		
		
		protected long _parentID;
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
		protected DateTime? _dataUpdateTime;
		protected string _tel;
		protected string _fax;
		protected string _zipCode;
		protected string _address;
		protected string _contact;
		protected string _parentOrg;
		protected string _orgType;
		protected string _orgCode;
        protected long? _ManagerID;
        protected string _ManagerName;
        protected string _matchCode;
        protected IList<HRDeptEmp> _hRDeptEmpList;
		protected IList<HRDeptIdentity> _hRDeptIdentitieList;
		protected IList<HREmployee> _hREmployeeList;

		#endregion

		#region Constructors

		public HRDept() { }

		public HRDept( long labID, long parentID, int levelNum, string useCode, string standCode, string deveCode, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string tel, string fax, string zipCode, string address, string contact, string parentOrg, string orgType, string orgCode,long? ManagerID,string ManagerName)
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
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._tel = tel;
			this._fax = fax;
			this._zipCode = zipCode;
			this._address = address;
			this._contact = contact;
			this._parentOrg = parentOrg;
			this._orgType = orgType;
			this._orgCode = orgCode;
            this._ManagerID = ManagerID;
            this._ManagerName = ManagerName;
        }

		#endregion

		#region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "树形结构父级ID", ShortCode = "SXJGFJID", Desc = "树形结构父级ID", ContextType = SysDic.Number, Length = 4)]
		public virtual long ParentID
		{
			get { return _parentID; }
			set { _parentID = value; }
		}

        [DataMember]
        [DataDesc(CName = "树形结构层级", ShortCode = "SXJGCJ", Desc = "树形结构层级", ContextType = SysDic.Number, Length = 4)]
		public virtual int LevelNum
		{
			get { return _levelNum; }
			set { _levelNum = value; }
		}

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "DM", Desc = "代码", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "标准代码", ShortCode = "BZDM", Desc = "标准代码", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "开发商标准代码", ShortCode = "KFSBZDM", Desc = "开发商标准代码", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "部门名称", ShortCode = "BMMC", Desc = "部门名称", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "部门英文名称", ShortCode = "BMYWMC", Desc = "部门英文名称", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "简称", ShortCode = "JC", Desc = "简称", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "快捷码", ShortCode = "KJM", Desc = "快捷码", ContextType = SysDic.NText, Length = 20)]
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
        [DataDesc(CName = "汉字拼音字头", ShortCode = "HZPYZT", Desc = "汉字拼音字头", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "描述", ShortCode = "MS", Desc = "描述", ContextType = SysDic.NText)]
		public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 1000000)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "SFSY", Desc = "是否使用", ContextType = SysDic.All)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "XSCX", Desc = "显示次序", ContextType = SysDic.Number, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
        [DataMember]
        [DataDesc(CName = "电话", ShortCode = "DH", Desc = "电话", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "传真", ShortCode = "CZ", Desc = "传真", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "邮编", ShortCode = "YB", Desc = "邮编", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "地址", ShortCode = "DZ", Desc = "地址", ContextType = SysDic.NText, Length = 100)]
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
        [DataDesc(CName = "联系人", ShortCode = "LXR", Desc = "联系人", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "上级机构", ShortCode = "SJJG", Desc = "上级机构", ContextType = SysDic.NText, Length = 44)]
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
        [DataDesc(CName = "机构类型", ShortCode = "JGLX", Desc = "机构类型", ContextType = SysDic.NText, Length = 12)]
		public virtual string OrgType
		{
			get 
            { 
                if (string.IsNullOrEmpty(_orgType))
                    return "1"; 
                else
                    return _orgType; 
            }
			set
			{
				if ( value != null && value.Length > 12)
					throw new ArgumentOutOfRangeException("Invalid value for OrgType", value, value.ToString());
				_orgType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "部门管理者ID", ShortCode = "BMGLZID", Desc = "部门管理者ID", ContextType = SysDic.NText, Length = 12)]
		public virtual long? ManagerID
        {
			get { return _ManagerID; }
			set
			{
                _ManagerID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "部门管理者姓名", ShortCode = "BMGLZXM", Desc = "部门管理者姓名", ContextType = SysDic.NText, Length = 12)]
        public virtual string ManagerName
        {
            get
            {
                return _ManagerName;
            }
            set
            {
                _ManagerName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "机构编码", ShortCode = "JGBM", Desc = "机构编码", ContextType = SysDic.NText, Length = 12)]
        public virtual string OrgCode
        {
            get { return _orgCode; }
            set
            {
                if (value != null && value.Length > 12)
                    throw new ArgumentOutOfRangeException("Invalid value for OrgCode", value, value.ToString());
                _orgCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "接口对照码", ShortCode = "MatchCode", Desc = "物资接口对照码", ContextType = SysDic.All, Length = 100)]
        public virtual string MatchCode
        {
            get { return _matchCode; }
            set { _matchCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "部门员工关系列表", ShortCode = "BMYGGXLB", Desc = "部门员工关系列表", ContextType = SysDic.List)]
		public virtual IList<HRDeptEmp> HRDeptEmpList
		{
			get
			{
				if (_hRDeptEmpList==null)
				{
                    _hRDeptEmpList = new List<HRDeptEmp>();
				}
				return _hRDeptEmpList;
			}
			set { _hRDeptEmpList = value; }
		}

        [DataMember]
        [DataDesc(CName = "部门身份列表", ShortCode = "BMSFLB", Desc = "部门身份列表", ContextType = SysDic.List)]
        public virtual IList<HRDeptIdentity> HRDeptIdentityList
		{
			get
			{
				if (_hRDeptIdentitieList==null)
				{
					_hRDeptIdentitieList = new List<HRDeptIdentity>();
				}
				return _hRDeptIdentitieList;
			}
			set { _hRDeptIdentitieList = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工列表", ShortCode = "YGLB", Desc = "员工列表", ContextType = SysDic.List)]
        public virtual IList<HREmployee> HREmployeeList
		{
			get
			{
				if (_hREmployeeList==null)
				{
                    _hREmployeeList = new List<HREmployee>();
				}
				return _hREmployeeList;
			}
			set { _hREmployeeList = value; }
		}

        #endregion

        #region 自定义属性
        protected string _code1;
        protected string _code2;
        protected string _code3;
        protected string _code4;
        protected string _code5;

        [DataMember]
        [DataDesc(CName = "住院编码", ShortCode = "Code1", Desc = "住院编码", ContextType = SysDic.All, Length = 50)]
        public virtual string Code1
        {
            get { return _code1; }
            set
            {
                if (value != null && value.Length > 50)
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
                if (value != null && value.Length > 50)
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
                if (value != null && value.Length > 50)
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
                if (value != null && value.Length > 50)
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code5", value, value.ToString());
                _code5 = value;
            }
        }
        #endregion
    }
    #endregion
}
