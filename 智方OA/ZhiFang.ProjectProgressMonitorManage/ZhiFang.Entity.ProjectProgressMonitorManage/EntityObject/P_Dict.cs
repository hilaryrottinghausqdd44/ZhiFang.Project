using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PDict

	/// <summary>
	/// PDict object for NHibernate mapped table 'P_Dict'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "字典表", ClassCName = "PDict", ShortCode = "PDict", Desc = "字典表")]
	public class PDict : BaseEntityService
    {
		#region Member Variables
		
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _cName;
        protected int _dispOrder;
        protected bool _isUse;
        protected string _memo;
        protected string _standCode;
        protected string _deveCode;
        protected PDictType _pDictType;

		#endregion

		#region Constructors

		public PDict() { }

		public PDict( long labID, string sName, string shortcode, string pinYinZiTou, string cName, int dispOrder, bool isUse, string memo, DateTime dataAddTime, byte[] dataTimeStamp, PDictType pDictType, string standCode, string deveCode)
		{
			this._labID = labID;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._cName = cName;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._memo = memo;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._pDictType = pDictType;
            this._standCode = standCode;
            this._deveCode = deveCode;
        }

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 40)]
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
        [DataDesc(CName = "字典名称", ShortCode = "CName", Desc = "字典名称", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
        {
            get { return _standCode; }
            set
            {
                if (value != null && value.Length > 50)
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DeveCode", value, value.ToString());
                _deveCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "字典类型表", ShortCode = "PDictType", Desc = "字典类型表")]
		public virtual PDictType PDictType
		{
			get { return _pDictType; }
			set { _pDictType = value; }
		}     

        
		#endregion
	}
	#endregion
}