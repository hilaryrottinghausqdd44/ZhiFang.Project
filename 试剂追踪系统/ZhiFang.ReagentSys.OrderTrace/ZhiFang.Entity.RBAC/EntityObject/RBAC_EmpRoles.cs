using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region RBACEmpRoles

	/// <summary>
	/// RBACEmpRoles object for NHibernate mapped table 'RBAC_EmpRoles'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "员工角色", ClassCName = "RBACEmpRoles", ShortCode = "RBACEmpRoles", Desc = "员工角色")]
	public class RBACEmpRoles : BaseEntity
	{
		#region Member Variables
		
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected string _validity;
		protected HREmployee _hREmployee;
		protected RBACRole _rBACRole;

		#endregion

		#region Constructors

		public RBACEmpRoles() { }

		public RBACEmpRoles( long labID, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string validity, HREmployee hREmployee, RBACRole rBACRole )
		{
			this._labID = labID;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._validity = validity;
			this._hREmployee = hREmployee;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "有效期", ShortCode = "Validity", Desc = "有效期", ContextType = SysDic.All, Length = 500)]
        public virtual string Validity
		{
			get { return _validity; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Validity", value, value.ToString());
				_validity = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
		public virtual HREmployee HREmployee
		{
			get { return _hREmployee; }
			set { _hREmployee = value; }
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