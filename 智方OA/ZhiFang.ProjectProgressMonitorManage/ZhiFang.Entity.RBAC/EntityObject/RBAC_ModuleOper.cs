using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region RBACModuleOper

	/// <summary>
	/// RBACModuleOper object for NHibernate mapped table 'RBAC_ModuleOper'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "模块操作", ClassCName = "RBACModuleOper", ShortCode = "RBACModuleOper", Desc = "模块操作")]
	public class RBACModuleOper : BaseEntityService
    {
		#region Member Variables
		
        protected int _invisibleOrDisable;
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
        protected bool _defaultChecked;
        protected string _operateURL;
        protected string _rowFilterURL;
        protected string _rowFilterBase;
        protected string _filterCondition;
        protected string _colFilterURL;
        protected string _colFilterBase;
        protected string _colFilterDesc;
        protected DateTime? _dataUpdateTime;
        protected bool _useRowFilter;
        protected string _predefinedField;
        protected RBACRowFilter _rBACRowFilter;
		protected BTDAppComponentsOperate _bTDAppComponentsOperate;
		protected RBACModule _rBACModule;
		protected IList<RBACRoleRight> _rBACRoleRightList;

        protected string _serviceURLEName;
        protected string _rowFilterBaseCName;
        #endregion

        #region Constructors

        public RBACModuleOper() { }

		public RBACModuleOper( long labID, int invisibleOrDisable, string useCode, string standCode, string deveCode, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, int dispOrder, bool defaultChecked, string operateURL, string rowFilterURL, string rowFilterBase, string filterCondition, string colFilterURL, string colFilterBase, string colFilterDesc, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BTDAppComponentsOperate bTDAppComponentsOperate, RBACModule rBACModule )
		{
			this._labID = labID;
			this._invisibleOrDisable = invisibleOrDisable;
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
			this._defaultChecked = defaultChecked;
			this._operateURL = operateURL;
			this._rowFilterURL = rowFilterURL;
			this._rowFilterBase = rowFilterBase;
			this._filterCondition = filterCondition;
			this._colFilterURL = colFilterURL;
			this._colFilterBase = colFilterBase;
			this._colFilterDesc = colFilterDesc;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bTDAppComponentsOperate = bTDAppComponentsOperate;
			this._rBACModule = rBACModule;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "显示方式", ShortCode = "InvisibleOrDisable", Desc = "显示方式", ContextType = SysDic.All, Length = 4)]
        public virtual int InvisibleOrDisable
		{
			get { return _invisibleOrDisable; }
			set { _invisibleOrDisable = value; }
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
        [DataDesc(CName = "默认状态", ShortCode = "DefaultChecked", Desc = "默认状态", ContextType = SysDic.All, Length = 1)]
        public virtual bool DefaultChecked
		{
			get { return _defaultChecked; }
			set { _defaultChecked = value; }
		}

        [DataMember]
        [DataDesc(CName = "操作服务地址", ShortCode = "OperateURL", Desc = "操作服务地址", ContextType = SysDic.All, Length = 200)]
        public virtual string OperateURL
		{
			get { return _operateURL; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for OperateURL", value, value.ToString());
				_operateURL = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "行过滤服务URL", ShortCode = "RowFilterURL", Desc = "行过滤服务URL", ContextType = SysDic.All, Length = 200)]
        public virtual string RowFilterURL
		{
			get { return _rowFilterURL; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for RowFilterURL", value, value.ToString());
				_rowFilterURL = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "行过滤依据对象", ShortCode = "RowFilterBase", Desc = "行过滤依据对象", ContextType = SysDic.All, Length = 500)]
        public virtual string RowFilterBase
		{
			get { return _rowFilterBase; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for RowFilterBase", value, value.ToString());
				_rowFilterBase = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "行过滤条件", ShortCode = "FilterCondition", Desc = "行过滤条件", ContextType = SysDic.All, Length = 500)]
        public virtual string FilterCondition
		{
			get { return _filterCondition; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for FilterCondition", value, value.ToString());
				_filterCondition = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "列过滤服务URL", ShortCode = "ColFilterURL", Desc = "列过滤服务URL", ContextType = SysDic.All, Length = 200)]
        public virtual string ColFilterURL
		{
			get { return _colFilterURL; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ColFilterURL", value, value.ToString());
				_colFilterURL = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "列过滤依据对象", ShortCode = "ColFilterBase", Desc = "列过滤依据对象", ContextType = SysDic.All, Length = 500)]
        public virtual string ColFilterBase
		{
			get { return _colFilterBase; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ColFilterBase", value, value.ToString());
				_colFilterBase = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "列过滤说明", ShortCode = "ColFilterDesc", Desc = "列过滤说明", ContextType = SysDic.All, Length = 50)]
        public virtual string ColFilterDesc
		{
			get { return _colFilterDesc; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ColFilterDesc", value, value.ToString());
				_colFilterDesc = value;
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
        [DataDesc(CName = "是否开启数据权限", ShortCode = "UseRowFilter", Desc = "是否开启数据权限", ContextType = SysDic.All, Length = 1)]
        public virtual bool UseRowFilter
        {
            get { return _useRowFilter; }
            set { _useRowFilter = value; }
        }

        [DataMember]
        [DataDesc(CName = "预定义属性", ShortCode = "PredefinedField", Desc = "预定义属性", ContextType = SysDic.All, Length = 50000)]
        public virtual string PredefinedField
        {
            get { return _predefinedField; }
            set { _predefinedField = value; }
        }

        [DataMember]
        [DataDesc(CName = "服务URL名称", ShortCode = "ServiceURLEName", Desc = "服务URL名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ServiceURLEName
        {
            get { return _serviceURLEName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ServiceURLEName", value, value.ToString());
                _serviceURLEName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "行过滤依据对象中文名称", ShortCode = "RowFilterBaseCName", Desc = "行过滤依据对象中文名称", ContextType = SysDic.All, Length = 100)]
        public virtual string RowFilterBaseCName
        {
            get { return _rowFilterBaseCName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for RowFilterBaseCName", value, value.ToString());
                _rowFilterBaseCName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "程序操作包括按钮及非按钮操作（如下拉列表等）", ShortCode = "BTDAppComponentsOperate", Desc = "程序操作包括按钮及非按钮操作（如下拉列表等）")]
		public virtual BTDAppComponentsOperate BTDAppComponentsOperate
		{
			get { return _bTDAppComponentsOperate; }
			set { _bTDAppComponentsOperate = value; }
		}

        [DataMember]
        [DataDesc(CName = "模块", ShortCode = "RBACModule", Desc = "模块")]
		public virtual RBACModule RBACModule
		{
			get { return _rBACModule; }
			set { _rBACModule = value; }
		}

        [DataMember]
        [DataDesc(CName = "默认行过滤", ShortCode = "RBACRowFilter", Desc = "默认行过滤")]
        public virtual RBACRowFilter RBACRowFilter
        {
            get { return _rBACRowFilter; }
            set { _rBACRowFilter = value; }
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