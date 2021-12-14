using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
    #region BDictType

    /// <summary>
    /// PDictType object for NHibernate mapped table 'P_DictType'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "字典类型表", ClassCName = "BDictType", ShortCode = "BDictType", Desc = "字典类型表")]
	public class BDictType : BaseEntity
	{
		#region Member Variables
		
        protected string _dictTypeCode;
        protected string _cName;
        protected int _dispOrder;
        protected bool _isUse;
        protected string _memo;
		protected IList<BDict> _bDictList; 

		#endregion

		#region Constructors

		public BDictType() { }

		public BDictType( long labID, string dictTypeCode, string cName, int dispOrder, DateTime dataAddTime, bool isUse, string memo, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._dictTypeCode = dictTypeCode;
			this._cName = cName;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._isUse = isUse;
			this._memo = memo;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "字典类型编号", ShortCode = "DictTypeCode", Desc = "字典类型编号", ContextType = SysDic.All, Length = 100)]
        public virtual string DictTypeCode
		{
			get { return _dictTypeCode; }
			set { _dictTypeCode = value; }
		}

        [DataMember]
        [DataDesc(CName = "字典类型名称", ShortCode = "CName", Desc = "字典类型名称", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
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
        [DataDesc(CName = "字典表", ShortCode = "BDictList", Desc = "字典表")]
		public virtual IList<BDict> BDictList
		{
			get
			{
				if (_bDictList==null)
				{
					_bDictList = new List<BDict>();
				}
				return _bDictList;
			}
			set { _bDictList = value; }
		}

        
		#endregion
	}
	#endregion
}