using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region RBACRoleRight

	/// <summary>
	/// RBACRoleRight object for NHibernate mapped table 'RBAC_RoleRight'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "角色权限", ClassCName = "RBACRoleRight", ShortCode = "RBACRoleRight", Desc = "角色权限")]
	public class RBACRoleRight : BaseEntityService
    {
		#region Member Variables
		
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected RBACModuleOper _rBACModuleOper;
		protected RBACRole _rBACRole;
		protected RBACRowFilter _rBACRowFilter;
        protected RBACPreconditions _rBACPreconditions;
        #endregion

        #region Constructors

        public RBACRoleRight() { }

		public RBACRoleRight( long labID, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, RBACModuleOper rBACModuleOper, RBACRole rBACRole, RBACRowFilter rBACRowFilter )
		{
			this._labID = labID;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._rBACModuleOper = rBACModuleOper;
			this._rBACRole = rBACRole;
			this._rBACRowFilter = rBACRowFilter;
		}

        #endregion

        #region Public Properties

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
        [DataDesc(CName = "模块操作", ShortCode = "RBACModuleOper", Desc = "模块操作")]
		public virtual RBACModuleOper RBACModuleOper
		{
			get { return _rBACModuleOper; }
			set { _rBACModuleOper = value; }
		}

        [DataMember]
        [DataDesc(CName = "角色", ShortCode = "RBACRole", Desc = "角色")]
		public virtual RBACRole RBACRole
		{
			get { return _rBACRole; }
			set { _rBACRole = value; }
		}

        [DataMember]
        [DataDesc(CName = "行过滤", ShortCode = "RBACRowFilter", Desc = "行过滤")]
		public virtual RBACRowFilter RBACRowFilter
		{
			get { return _rBACRowFilter; }
			set { _rBACRowFilter = value; }
		}
        [DataMember]
        [DataDesc(CName = "预置条件", ShortCode = "RBACPreconditions", Desc = "预置条件")]
        public virtual RBACPreconditions RBACPreconditions
        {
            get { return _rBACPreconditions; }
            set { _rBACPreconditions = value; }
        }
        #endregion
    }
	#endregion
}