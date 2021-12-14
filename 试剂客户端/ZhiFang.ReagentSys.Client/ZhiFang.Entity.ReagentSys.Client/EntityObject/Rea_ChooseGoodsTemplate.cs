using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
	#region ReaChooseGoodsTemplate

	/// <summary>
	/// ReaChooseGoodsTemplate object for NHibernate mapped table 'Rea_ChooseGoodsTemplate'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaChooseGoodsTemplate", ShortCode = "ReaChooseGoodsTemplate", Desc = "")]
	public class ReaChooseGoodsTemplate : BaseEntity
	{
		#region Member Variables
		
        protected long? _orgID;
        protected string _orgName;
        protected long? _deptID;
        protected string _deptName;
        protected string _cName;
        protected string _site;
        protected string _contextJson;
        protected string _sName;
        protected string _shortCode;
        protected string _pinyinzitou;
        protected int _dispOrder;
        protected bool _isUse;
        protected string _memo;
        protected long? _createrID;
        protected string _creatName;
		

		#endregion

		#region Constructors

		public ReaChooseGoodsTemplate() { }

		public ReaChooseGoodsTemplate( long labID, long orgID, string orgName, long deptID, string deptName, string cName, string site, string contextJson, string sName, string shortCode, string pinyinzitou, int dispOrder, bool isUse, string memo, long createrID, string creatName, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._orgID = orgID;
			this._orgName = orgName;
			this._deptID = deptID;
			this._deptName = deptName;
			this._cName = cName;
			this._site = site;
			this._contextJson = contextJson;
			this._sName = sName;
			this._shortCode = shortCode;
			this._pinyinzitou = pinyinzitou;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._memo = memo;
			this._createrID = createrID;
			this._creatName = creatName;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrgID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrgID
		{
			get { return _orgID; }
			set { _orgID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OrgName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string OrgName
		{
			get { return _orgName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for OrgName", value, value.ToString());
				_orgName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DeptID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string DeptName
		{
			get { return _deptName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value.ToString());
				_deptName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Site", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Site
		{
			get { return _site; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Site", value, value.ToString());
				_site = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "ContextJson", ShortCode = "ContextJson", Desc = "ContextJson", ContextType = SysDic.All, Length = 214748364)]
        public virtual string ContextJson
		{
			get { return _contextJson; }
			set
			{
				_contextJson = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "模板类型(申请明细或订单明细)", ShortCode = "SName", Desc = "模板类型(申请明细或订单明细)", ContextType = SysDic.All, Length = 40)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Pinyinzitou", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Pinyinzitou
		{
			get { return _pinyinzitou; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Pinyinzitou", value, value.ToString());
				_pinyinzitou = value;
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
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 500)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CreaterID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreaterID
		{
			get { return _createrID; }
			set { _createrID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CreatName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CreatName
		{
			get { return _creatName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CreatName", value, value.ToString());
				_creatName = value;
			}
		}

		
		#endregion
	}
	#endregion
}