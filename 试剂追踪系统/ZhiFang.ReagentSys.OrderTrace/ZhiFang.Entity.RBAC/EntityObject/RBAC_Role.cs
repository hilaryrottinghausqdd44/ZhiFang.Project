using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region RBACRole

	/// <summary>
	/// RBACRole object for NHibernate mapped table 'RBAC_Role'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "角色", ClassCName = "RBACRole", ShortCode = "RBACRole", Desc = "角色")]
	public class RBACRole : BaseEntity//BaseEntityService
    {
		#region Member Variables
		
        protected long? _parentID;
        protected int _levelNum;
        protected int _treeCatalog;
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
		protected IList<RBACEmpRoles> _rBACEmpRolesList; 
		protected IList<RBACRoleModule> _rBACRoleModuleList; 
		protected IList<RBACRoleRight> _rBACRoleRightList; 

		#endregion

		#region Constructors

		public RBACRole() { }

		public RBACRole( long labID, long parentID, int levelNum, int treeCatalog, string useCode, string standCode, string deveCode, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._parentID = parentID;
			this._levelNum = levelNum;
			this._treeCatalog = treeCatalog;
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
        [DataDesc(CName = "树目录", ShortCode = "TreeCatalog", Desc = "树目录", ContextType = SysDic.All, Length = 4)]
        public virtual int TreeCatalog
		{
			get { return _treeCatalog; }
			set { _treeCatalog = value; }
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
        [DataDesc(CName = "员工角色", ShortCode = "RBACEmpRolesList", Desc = "员工角色")]
		public virtual IList<RBACEmpRoles> RBACEmpRoleList
		{
			get
			{
				if (_rBACEmpRolesList==null)
				{
					_rBACEmpRolesList = new List<RBACEmpRoles>();
				}
				return _rBACEmpRolesList;
			}
			set { _rBACEmpRolesList = value; }
		}

        [DataMember]
        [DataDesc(CName = "角色模块访问权限", ShortCode = "RBACRoleModuleList", Desc = "角色模块访问权限")]
		public virtual IList<RBACRoleModule> RBACRoleModuleList
		{
			get
			{
				if (_rBACRoleModuleList==null)
				{
					_rBACRoleModuleList = new List<RBACRoleModule>();
				}
				return _rBACRoleModuleList;
			}
			set { _rBACRoleModuleList = value; }
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

        
		#endregion
	}
	#endregion
}