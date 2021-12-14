using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region RBACRoleModule

	/// <summary>
	/// RBACRoleModule object for NHibernate mapped table 'RBAC_RoleModule'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "角色模块访问权限", ClassCName = "RBACRoleModule", ShortCode = "RBACRoleModule", Desc = "角色模块访问权限")]
	public class RBACRoleModule : BaseEntity
	{
		#region Member Variables
		
        protected bool _isUse;
        protected int _dispOrder;
        protected int _isOftenUse;
        protected int _isDefaultOpen;
        protected DateTime? _dataUpdateTime;
		protected RBACModule _rBACModule;
		protected RBACRole _rBACRole;

		#endregion

		#region Constructors

		public RBACRoleModule() { }

		public RBACRoleModule( long labID, bool isUse, int dispOrder, int isOftenUse, int isDefaultOpen, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, RBACModule rBACModule, RBACRole rBACRole )
		{
			this._labID = labID;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._isOftenUse = isOftenUse;
			this._isDefaultOpen = isDefaultOpen;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._rBACModule = rBACModule;
			this._rBACRole = rBACRole;
		}

		#endregion

		#region Public Properties


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
        [DataDesc(CName = "是否常用模块", ShortCode = "IsOftenUse", Desc = "是否常用模块", ContextType = SysDic.All, Length = 4)]
        public virtual int IsOftenUse
		{
			get { return _isOftenUse; }
			set { _isOftenUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否默认开启", ShortCode = "IsDefaultOpen", Desc = "是否默认开启", ContextType = SysDic.All, Length = 4)]
        public virtual int IsDefaultOpen
		{
			get { return _isDefaultOpen; }
			set { _isDefaultOpen = value; }
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
        [DataDesc(CName = "模块", ShortCode = "RBACModule", Desc = "模块")]
		public virtual RBACModule RBACModule
		{
			get { return _rBACModule; }
			set { _rBACModule = value; }
		}

        [DataMember]
        [DataDesc(CName = "角色", ShortCode = "RBACRole", Desc = "角色")]
		public virtual RBACRole RBACRole
		{
			get { return _rBACRole; }
			set { _rBACRole = value; }
		}

        
		#endregion
	}
	#endregion
}