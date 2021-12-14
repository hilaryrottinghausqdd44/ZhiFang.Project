using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region RBACEmpOptions

	/// <summary>
	/// RBACEmpOptions object for NHibernate mapped table 'RBAC_EmpOptions'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "员工设置", ClassCName = "RBACEmpOptions", ShortCode = "RBACEmpOptions", Desc = "员工设置")]
	public class RBACEmpOptions : BaseEntityService
    {
		#region Member Variables
		
        protected int _moduleIconSize;
        protected DateTime? _newModuleLookTime;
        protected int _allModuleIconSize;
        protected bool _isLock;
        protected long? _moduleInfoSize;
        protected DateTime? _newInfoModuleLookTime;
        protected DateTime? _dataUpdateTime;
        protected PUser _pUser;
        protected HREmployee _hREmployee;
		protected RBACModule _rbacmodule;
        protected bool _rightFlag;

		#endregion

		#region Constructors

		public RBACEmpOptions() { }

		public RBACEmpOptions( long labID, int moduleIconSize, DateTime newModuleLookTime, int allModuleIconSize, bool isLock, long moduleInfoSize, DateTime newInfoModuleLookTime, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, PUser pUser, HREmployee hREmployee, RBACModule rbacmodule )
		{
			this._labID = labID;
			this._moduleIconSize = moduleIconSize;
			this._newModuleLookTime = newModuleLookTime;
			this._allModuleIconSize = allModuleIconSize;
			this._isLock = isLock;
			this._moduleInfoSize = moduleInfoSize;
			this._newInfoModuleLookTime = newInfoModuleLookTime;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._hREmployee = hREmployee;
            this._rbacmodule = rbacmodule;
            this._pUser = pUser;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "员工2", ShortCode = "PUser", Desc = "员工2")]
        public virtual PUser PUser
        {
            get { return _pUser; }
            set { _pUser = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public override long Id
        {
            get
            {
                if (_id <= 0)
                    _id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                return _id;
            }
            set { _id = value; }
        }
        [DataMember]
        [DataDesc(CName = "常用应用图标大小", ShortCode = "ModuleIconSize", Desc = "常用应用图标大小", ContextType = SysDic.All, Length = 4)]
        public virtual int ModuleIconSize
		{
			get { return _moduleIconSize; }
			set { _moduleIconSize = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "新应用查看时间", ShortCode = "NewModuleLookTime", Desc = "新应用查看时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? NewModuleLookTime
		{
			get { return _newModuleLookTime; }
			set { _newModuleLookTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "所有应用图标大小", ShortCode = "AllModuleIconSize", Desc = "所有应用图标大小", ContextType = SysDic.All, Length = 4)]
        public virtual int AllModuleIconSize
		{
			get { return _allModuleIconSize; }
			set { _allModuleIconSize = value; }
		}

        [DataMember]
        [DataDesc(CName = "功能区锁定", ShortCode = "IsLock", Desc = "功能区锁定", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsLock
		{
			get { return _isLock; }
			set { _isLock = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "关注信息大小", ShortCode = "ModuleInfoSize", Desc = "关注信息大小", ContextType = SysDic.All, Length = 8)]
		public virtual long? ModuleInfoSize
		{
			get { return _moduleInfoSize; }
			set { _moduleInfoSize = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "新关注信息查看时间", ShortCode = "NewInfoModuleLookTime", Desc = "新关注信息查看时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? NewInfoModuleLookTime
		{
			get { return _newInfoModuleLookTime; }
			set { _newInfoModuleLookTime = value; }
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
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
		public virtual HREmployee HREmployee
		{
			get { return _hREmployee; }
			set { _hREmployee = value; }
		}

        [DataMember]
        [DataDesc(CName = "模块", ShortCode = "Default", Desc = "模块")]
		public virtual RBACModule Default
		{
            get { return _rbacmodule; }
            set { _rbacmodule = value; }
		}
        [DataMember]
        [DataDesc(CName = "模块是否有权限", ShortCode = "RightFlag", Desc = "模块是否有权限")]
        public virtual bool RightFlag
        {
            get { return _rightFlag; }
            set { _rightFlag = value; }
        }
        
		#endregion
	}
	#endregion

}