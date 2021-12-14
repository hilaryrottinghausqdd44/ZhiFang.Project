using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
	#region ReaCenBarCodeFormat

	/// <summary>
	/// ReaCenBarCodeFormat object for NHibernate mapped table 'Rea_CenBarCodeFormat'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "供应商条码格式表", ClassCName = "ReaCenBarCodeFormat", ShortCode = "ReaCenBarCodeFormat", Desc = "供应商条码格式表")]
	public class ReaCenBarCodeFormat :BaseEntityService
	{
		#region Member Variables
		
        protected int? _platformOrgNo;
        protected string _cName;
        protected string _barCodeFormatExample;
        protected string _regularExpression;
        protected int? _splitCount;
        protected string _sName;
        protected string _shortCode;
        protected string _pinyinzitou;
        protected int _dispOrder;
        protected bool _isUse;
        protected string _memo;
        protected long? _type;
        protected int? _barCodeType;
        #endregion

        #region Constructors

        public ReaCenBarCodeFormat() { }

		public ReaCenBarCodeFormat( long labID, int platformOrgNo, string cName, string barCodeFormatExample, string regularExpression, int splitCount, string sName, string shortCode, string pinyinzitou, int dispOrder, bool isUse, string memo, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._platformOrgNo = platformOrgNo;
			this._cName = cName;
			this._barCodeFormatExample = barCodeFormatExample;
			this._regularExpression = regularExpression;
			this._splitCount = splitCount;
			this._sName = sName;
			this._shortCode = shortCode;
			this._pinyinzitou = pinyinzitou;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._memo = memo;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "平台机构编码", ShortCode = "PlatformOrgNo", Desc = "平台机构编码", ContextType = SysDic.All, Length = 4)]
        public virtual int? PlatformOrgNo
		{
			get { return _platformOrgNo; }
			set { _platformOrgNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "条码格式名称", ShortCode = "CName", Desc = "条码格式名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				_cName = value;
			}
		}
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "条码类型(1:一维条码;2:二维条码)", ShortCode = "BarCodeType", Desc = "条码类型(1:一维条码;2:二维条码)", ContextType = SysDic.All, Length = 4)]
        public virtual int? BarCodeType
        {
            get { return _barCodeType; }
            set { _barCodeType = value; }
        }
        [DataMember]
        [DataDesc(CName = "条码格式样例", ShortCode = "BarCodeFormatExample", Desc = "条码格式样例", ContextType = SysDic.All, Length = 100)]
        public virtual string BarCodeFormatExample
		{
			get { return _barCodeFormatExample; }
			set
			{
				_barCodeFormatExample = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "正则表达式", ShortCode = "RegularExpression", Desc = "正则表达式", ContextType = SysDic.All, Length = 1000)]
        public virtual string RegularExpression
		{
			get { return _regularExpression; }
			set
			{
				_regularExpression = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "分隔符数量", ShortCode = "SplitCount", Desc = "分隔符数量", ContextType = SysDic.All, Length = 4)]
        public virtual int? SplitCount
		{
			get { return _splitCount; }
			set { _splitCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "条码规则前缀", ShortCode = "SName", Desc = "条码规则前缀", ContextType = SysDic.All, Length = 40)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "条码规则分割符", ShortCode = "ShortCode", Desc = "条码规则分割符", ContextType = SysDic.All, Length = 20)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "Pinyinzitou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string Pinyinzitou
		{
			get { return _pinyinzitou; }
			set
			{
				_pinyinzitou = value;
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
        [DataDesc(CName = "是否启用", ShortCode = "IsUse", Desc = "是否启用", ContextType = SysDic.All, Length = 1)]
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
				_memo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "条码分类", ShortCode = "Type", Desc = "条码分类:1,按公共;2,按供应商", ContextType = SysDic.All, Length = 8)]
        public virtual long? Type
        {
            get { return _type; }
            set { _type = value; }
        }

        #endregion
    }
	#endregion
}