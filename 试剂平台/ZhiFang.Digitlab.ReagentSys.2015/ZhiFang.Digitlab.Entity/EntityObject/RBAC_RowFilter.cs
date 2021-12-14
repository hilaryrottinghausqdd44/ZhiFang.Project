using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region RBACRowFilter

	/// <summary>
	/// RBACRowFilter object for NHibernate mapped table 'RBAC_RowFilter'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "行过滤", ClassCName = "RBACRowFilter", ShortCode = "RBACRowFilter", Desc = "行过滤")]
	public class RBACRowFilter : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _rowFilterCondition;
        protected string _standCode;
        protected string _deveCode;
        protected string _pinYinZiTou;
        protected string _shortcode;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected IList<RBACRoleRight> _rBACRoleRightList;
        protected string _rowFilterConstruction;

		#endregion

		#region Constructors

		public RBACRowFilter() { }

		public RBACRowFilter( long labID, string cName, string eName, string sName, string rowFilterCondition, string standCode, string deveCode, string pinYinZiTou, string shortcode, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._rowFilterCondition = rowFilterCondition;
			this._standCode = standCode;
			this._deveCode = deveCode;
			this._pinYinZiTou = pinYinZiTou;
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
        [DataDesc(CName = "过滤条件", ShortCode = "RowFilterCondition", Desc = "过滤条件", ContextType = SysDic.All, Length = 8000)]
        public virtual string RowFilterCondition
		{
			get { return _rowFilterCondition; }
			set
			{
                //if ( value != null && value.Length > 8000)
                //    throw new ArgumentOutOfRangeException("Invalid value for RowFilterCondition", value, value.ToString());
				_rowFilterCondition = value;
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
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 8000)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 8000)
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "角色权限", ShortCode = "RBACRoleRightList", Desc = "角色权限")]
		public virtual IList<RBACRoleRight> RBACRoleRightList
		{
			get
			{
				if (_rBACRoleRightList==null)
				{
					_rBACRoleRightList = new List<RBACRoleRight>();
				}
				return _rBACRoleRightList;
			}
			set { _rBACRoleRightList = value; }
		}

        [DataMember]
        [DataDesc(CName = "过滤结构", ShortCode = "RowFilterConstruction", Desc = "过滤结构", ContextType = SysDic.All, Length = 8000)]
        public virtual string RowFilterConstruction
        {
            get { return _rowFilterConstruction; }
            set
            {
                if (value != null && value.Length > 8000)
                    throw new ArgumentOutOfRangeException("Invalid value for RowFilterConstruction", value, value.ToString());
                _rowFilterConstruction = value;
            }
        }
		#endregion
	}
	#endregion
}