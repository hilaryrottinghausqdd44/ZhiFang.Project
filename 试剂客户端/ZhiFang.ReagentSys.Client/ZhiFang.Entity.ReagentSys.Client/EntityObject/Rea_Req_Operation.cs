using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
	#region ReaReqOperation

	/// <summary>
	/// ReaReqOperation object for NHibernate mapped table 'Rea_Req_Operation'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "采购申请单、订单操作记录表", ClassCName = "ReaReqOperation", ShortCode = "ReaReqOperation", Desc = "采购申请单、订单操作记录表")]
	public class ReaReqOperation : BaseEntity
	{
		#region Member Variables
		
        protected long _bobjectID;
        protected long _type;
        protected string _typeName;
        protected string _businessModuleCode;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected long _creatorID;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
		

		#endregion

		#region Constructors

		public ReaReqOperation() { }

		public ReaReqOperation( long labID, long bobjectID, long type, string typeName, string businessModuleCode, string memo, int dispOrder, bool isUse, long creatorID, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._bobjectID = bobjectID;
			this._type = type;
			this._typeName = typeName;
			this._businessModuleCode = businessModuleCode;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._creatorID = creatorID;
			this._creatorName = creatorName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "业务对象ID", ShortCode = "BobjectID", Desc = "业务对象ID", ContextType = SysDic.All, Length = 8)]
        public virtual long BobjectID
		{
			get { return _bobjectID; }
			set { _bobjectID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作类型", ShortCode = "Type", Desc = "操作类型", ContextType = SysDic.All, Length = 8)]
        public virtual long Type
		{
			get { return _type; }
			set { _type = value; }
		}

        [DataMember]
        [DataDesc(CName = "操作类型名", ShortCode = "TypeName", Desc = "操作类型名", ContextType = SysDic.All, Length = 20)]
        public virtual string TypeName
		{
			get { return _typeName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for TypeName", value, value.ToString());
				_typeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "业务模块代码", ShortCode = "BusinessModuleCode", Desc = "业务模块代码", ContextType = SysDic.All, Length = 20)]
        public virtual string BusinessModuleCode
		{
			get { return _businessModuleCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BusinessModuleCode", value, value.ToString());
				_businessModuleCode = value;
			}
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者", ShortCode = "CreatorID", Desc = "创建者", ContextType = SysDic.All, Length = 8)]
        public virtual long CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
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

		
		#endregion
	}
	#endregion
}