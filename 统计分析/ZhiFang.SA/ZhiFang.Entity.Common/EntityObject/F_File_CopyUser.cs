using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.Common
{
	#region FFileCopyUser

	/// <summary>
	/// FFileCopyUser object for NHibernate mapped table 'F_File_CopyUser'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "文档抄送对象表", ClassCName = "FFileCopyUser", ShortCode = "FFileCopyUser", Desc = "文档抄送对象表")]
	public class FFileCopyUser : BaseEntity
	{
		#region Member Variables
		
        protected long? _type;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
		protected FFile _fFile;
		protected HREmployee _creator;
		protected HRDept _hRDept;
		protected HREmployee _user;
		protected RBACRole _rBACRole;

		#endregion

		#region Constructors

		public FFileCopyUser() { }

		public FFileCopyUser( long labID, long type, string memo, int dispOrder, bool isUse, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, FFile fFile, HREmployee creator, HRDept hRDept, HREmployee user, RBACRole rBACRole )
		{
			this._labID = labID;
			this._type = type;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._creatorName = creatorName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._fFile = fFile;
			this._creator = creator;
			this._hRDept = hRDept;
			this._user = user;
			this._rBACRole = rBACRole;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "对象类型：1全部人员、2按部门、3按角色、4按人员", ShortCode = "Type", Desc = "对象类型：1全部人员、2按部门、3按角色、4按人员", ContextType = SysDic.All, Length = 8)]
		public virtual long? Type
		{
			get { return _type; }
			set { _type = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreatorName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreatorName
		{
			get { return _creatorName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
				_creatorName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据修改时间", ShortCode = "DataUpdateTime", Desc = "数据修改时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "文档表", ShortCode = "FFile", Desc = "文档表")]
		public virtual FFile FFile
		{
			get { return _fFile; }
			set { _fFile = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "Creator", Desc = "创建者")]
		public virtual HREmployee Creator
		{
			get { return _creator; }
			set { _creator = value; }
		}

        [DataMember]
        [DataDesc(CName = "抄送部门", ShortCode = "HRDept", Desc = "抄送部门")]
		public virtual HRDept HRDept
		{
			get { return _hRDept; }
			set { _hRDept = value; }
		}

        [DataMember]
        [DataDesc(CName = "抄送人", ShortCode = "User", Desc = "抄送人")]
		public virtual HREmployee User
		{
			get { return _user; }
			set { _user = value; }
		}

        [DataMember]
        [DataDesc(CName = "抄送角色", ShortCode = "RBACRole", Desc = "抄送角色")]
		public virtual RBACRole RBACRole
		{
			get { return _rBACRole; }
			set { _rBACRole = value; }
		}

        
		#endregion
	}
	#endregion
}