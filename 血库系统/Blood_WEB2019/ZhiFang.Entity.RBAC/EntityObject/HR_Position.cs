using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region HRPosition

	/// <summary>
	/// HRPosition object for NHibernate mapped table 'HR_Position'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "职位", ClassCName = "", ShortCode = "ZW", Desc = "职位")]
    public class HRPosition : BaseEntity
	{
		#region Member Variables
		
		
		protected int _grade;
		protected string _useCode;
		protected string _standCode;
		protected string _deveCode;
		protected string _cName;
		protected string _eName;
		protected string _sName;
		protected string _shortcode;
		protected string _comment;
		protected bool _isUse;
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;
        protected IList<HREmployee> _hREmployeeList;

		#endregion

		#region Constructors

		public HRPosition() { }

		public HRPosition( long labID, int grade, string useCode, string standCode, string deveCode, string cName, string eName, string sName, string shortcode, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._grade = grade;
			this._useCode = useCode;
			this._standCode = standCode;
			this._deveCode = deveCode;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "职位等级", ShortCode = "ZWDJ", Desc = "职位等级", ContextType = SysDic.Number, Length = 4)]
		public virtual int Grade
		{
			get { return _grade; }
			set { _grade = value; }
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
        [DataDesc(CName = "名称", ShortCode = "MC", Desc = "名称", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "英文名称", ShortCode = "YWMC", Desc = "英文名称", ContextType = SysDic.NText, Length = 50)]
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
	}
	#endregion
}