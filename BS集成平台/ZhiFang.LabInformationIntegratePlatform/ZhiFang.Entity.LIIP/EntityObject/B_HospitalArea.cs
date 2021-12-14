using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region BHospitalArea

	/// <summary>
	/// BHospitalArea object for NHibernate mapped table 'B_HospitalArea'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "医院区域", ClassCName = "BHospitalArea", ShortCode = "BHospitalArea", Desc = "医院区域")]
	public class BHospitalArea : BaseEntityService//BaseEntity
    {
		#region Member Variables
		
        protected long? _pHospitalAreaID;
        protected string _PHospitalAreaName;
        protected string _PHospitalAreaCode;
        protected string _name;
        protected string _code;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected long? _centerHospitalID;
        protected string _centerHospitalName;
        protected string _comment;
        protected bool _isUse;
        protected int _DispOrder;
        private long? _AreaTypeID;
        private string _AreaTypeName;
        private string _HospitalAreaLevelName;
        private long? _DeptID;
        private string _DeptName;

        #endregion

        #region Constructors

        public BHospitalArea() { }
        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 30)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
                _name = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Code
        {
            get { return _code; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Code", value, value.ToString());
                _code = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "父区域ID", ShortCode = "PHospitalAreaID", Desc = "父区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PHospitalAreaID
		{
			get { return _pHospitalAreaID; }
			set { _pHospitalAreaID = value; }
		}

        [DataMember]
        [DataDesc(CName = "父区域名称", ShortCode = "PHospitalAreaName", Desc = "父区域名称", ContextType = SysDic.All, Length = 8)]
        public virtual string PHospitalAreaName
        {
            get { return _PHospitalAreaName; }
            set { _PHospitalAreaName = value; }
        }

        [DataMember]
        [DataDesc(CName = "父区域编码", ShortCode = "PHospitalAreaCode", Desc = "父区域编码", ContextType = SysDic.All, Length = 8)]
        public virtual string PHospitalAreaCode
        {
            get { return _PHospitalAreaCode; }
            set { _PHospitalAreaCode = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域中心医院ID", ShortCode = "CenterHospitalID", Desc = "区域中心医院ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CenterHospitalID
        {
            get { return _centerHospitalID; }
            set { _centerHospitalID = value; }
        }

        [DataMember]
        [DataDesc(CName = "区域中心医院名称", ShortCode = "CenterHospitalName", Desc = "区域中心医院名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CenterHospitalName
        {
            get { return _centerHospitalName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CenterHospitalName", value, value.ToString());
                _centerHospitalName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域类型ID", ShortCode = "AreaTypeID", Desc = "区域类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? AreaTypeID
        {
            get { return _AreaTypeID; }
            set { _AreaTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "区域类型名称", ShortCode = "AreaTypeName", Desc = "区域中心医院名称", ContextType = SysDic.All, Length = 50)]
        public virtual string AreaTypeName
        {
            get { return _AreaTypeName; }
            set
            {
                _AreaTypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 30)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 30)
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
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 300)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
                _comment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "排序", ShortCode = "DispOrder", Desc = "排序", ContextType = SysDic.All, Length = 50)]
        public virtual int DispOrder
        {
            get { return _DispOrder; }
            set
            {
                _DispOrder = value;
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
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "医院区域级别名称", ShortCode = "HospitalAreaLevelName", Desc = "医院区域级别名称", ContextType = SysDic.All, Length = 300)]
        public virtual string HospitalAreaLevelName
        {
            get { return _HospitalAreaLevelName; }
            set
            {
                _HospitalAreaLevelName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "部门ID", ShortCode = "DeptID", Desc = "部门ID", ContextType = SysDic.All, Length = 19)]
        public virtual long? DeptID
        {
            get { return _DeptID; }
            set { _DeptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "部门名称", ShortCode = "DeptName", Desc = "部门名称", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptName
        {
            get { return _DeptName; }
            set
            {
                _DeptName = value;
            }
        }

        #endregion
    }
	#endregion
}