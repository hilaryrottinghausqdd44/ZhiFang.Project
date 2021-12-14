using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region RBACModule

	/// <summary>
	/// RBACModule object for NHibernate mapped table 'RBAC_Module'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "模块", ClassCName = "RBACModule", ShortCode = "RBACModule", Desc = "模块")]
	public class RBACModule : BaseEntity
	{
		#region Member Variables
		
        protected long? _parentID;
        protected int _levelNum;
        protected int _treeCatalog;
        protected bool _isLeaf;
        protected int _moduleType;
        protected string _picFile;
        protected string _uRL;
        protected string _para;
        protected string _owner;
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
        protected BTDAppComponents _BTDAppComponents;
		protected IList<RBACEmpOptions> _rBACEmpOptionsList; 
		protected IList<RBACModuleOper> _rBACModuleOperList; 
		protected IList<RBACRoleModule> _rBACRoleModuleList; 

		#endregion

		#region Constructors

		public RBACModule() { }

		public RBACModule( long labID, long parentID, int levelNum, int treeCatalog, bool isLeaf, int moduleType, string picFile, string uRL, string para, string owner, string useCode, string standCode, string deveCode, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._parentID = parentID;
			this._levelNum = levelNum;
			this._treeCatalog = treeCatalog;
			this._isLeaf = isLeaf;
			this._moduleType = moduleType;
			this._picFile = picFile;
			this._uRL = uRL;
			this._para = para;
			this._owner = owner;
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
        [DataDesc(CName = "是否是叶节点", ShortCode = "IsLeaf", Desc = "是否是叶节点", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsLeaf
		{
			get { return _isLeaf; }
			set { _isLeaf = value; }
		}

        [DataMember]
        [DataDesc(CName = "模块类型", ShortCode = "ModuleType", Desc = "模块类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ModuleType
		{
			get { return _moduleType; }
			set { _moduleType = value; }
		}

        [DataMember]
        [DataDesc(CName = "模块图片文件", ShortCode = "PicFile", Desc = "模块图片文件", ContextType = SysDic.All, Length = 200)]
        public virtual string PicFile
		{
			get { return _picFile; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PicFile", value, value.ToString());
				_picFile = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "模块入口地址", ShortCode = "URL", Desc = "模块入口地址", ContextType = SysDic.All, Length = 150)]
        public virtual string Url
		{
			get { return _uRL; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for URL", value, value.ToString());
				_uRL = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "模块入口参数", ShortCode = "Para", Desc = "模块入口参数", ContextType = SysDic.All, Length = 500)]
        public virtual string Para
		{
			get { return _para; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Para", value, value.ToString());
				_para = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "所有者", ShortCode = "Owner", Desc = "所有者", ContextType = SysDic.All, Length = 50)]
        public virtual string Owner
		{
			get { return _owner; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Owner", value, value.ToString());
				_owner = value;
			}
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
        [DataDesc(CName = "关联的应用", ShortCode = "BTDAppComponents", Desc = "关联的应用", ContextType = SysDic.All, Length = 8)]
        public virtual BTDAppComponents BTDAppComponents
        {
            get { return _BTDAppComponents; }
            set { _BTDAppComponents = value; }
        }

        [DataMember]
        [DataDesc(CName = "员工设置", ShortCode = "RBACEmpOptionsList", Desc = "员工设置")]
		public virtual IList<RBACEmpOptions> RBACEmpOptionsList
		{
			get
			{
                if (_rBACEmpOptionsList == null)
				{
                    _rBACEmpOptionsList = new List<RBACEmpOptions>();
				}
                return _rBACEmpOptionsList;
			}
            set { _rBACEmpOptionsList = value; }
		}

        [DataMember]
        [DataDesc(CName = "模块操作", ShortCode = "RBACModuleOperList", Desc = "模块操作")]
		public virtual IList<RBACModuleOper> RBACModuleOperList
		{
			get
			{
				if (_rBACModuleOperList==null)
				{
					_rBACModuleOperList = new List<RBACModuleOper>();
				}
				return _rBACModuleOperList;
			}
			set { _rBACModuleOperList = value; }
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

        
		#endregion
	}
	#endregion
}
