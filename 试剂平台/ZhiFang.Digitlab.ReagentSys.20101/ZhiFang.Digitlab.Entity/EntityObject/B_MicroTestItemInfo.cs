using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BMicroTestItemInfo

	/// <summary>
	/// BMicroTestItemInfo object for NHibernate mapped table 'B_MicroTestItemInfo'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物检验记录项字典表", ClassCName = "BMicroTestItemInfo", ShortCode = "BMicroTestItemInfo", Desc = "微生物检验记录项字典表")]
	public class BMicroTestItemInfo : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected int _itemType;
        protected string _deveCode;
		protected IList<BMicroTestItemUseValue> _bMicroTestItemUseValueList; 
		protected IList<BMicroTestItemAndSample> _bMicroTestItemAndSampleList; 
		protected IList<MEMicroThreeReportDetail> _mEMicroThreeReportDetailList; 

		#endregion

		#region Constructors

		public BMicroTestItemInfo() { }

		public BMicroTestItemInfo( long labID, string cName, string eName, string sName, string shortcode, string pinYinZiTou, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, int itemType, string deveCode )
		{
			this._labID = labID;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemType = itemType;
			this._deveCode = deveCode;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 250)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 50)
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
        [DataDesc(CName = "", ShortCode = "ItemType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemType
		{
			get { return _itemType; }
			set { _itemType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeveCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DeveCode
		{
			get { return _deveCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DeveCode", value, value.ToString());
				_deveCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "微生物检验记录项常用值", ShortCode = "BMicroTestItemUseValueList", Desc = "微生物检验记录项常用值")]
		public virtual IList<BMicroTestItemUseValue> BMicroTestItemUseValueList
		{
			get
			{
				if (_bMicroTestItemUseValueList==null)
				{
					_bMicroTestItemUseValueList = new List<BMicroTestItemUseValue>();
				}
				return _bMicroTestItemUseValueList;
			}
			set { _bMicroTestItemUseValueList = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物检验记录项与样本类型、检验项目关系字典表", ShortCode = "BMicroTestItemAndSampleList", Desc = "微生物检验记录项与样本类型、检验项目关系字典表")]
		public virtual IList<BMicroTestItemAndSample> BMicroTestItemAndSampleList
		{
			get
			{
				if (_bMicroTestItemAndSampleList==null)
				{
					_bMicroTestItemAndSampleList = new List<BMicroTestItemAndSample>();
				}
				return _bMicroTestItemAndSampleList;
			}
			set { _bMicroTestItemAndSampleList = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物三级报告细表", ShortCode = "MEMicroThreeReportDetailList", Desc = "微生物三级报告细表")]
		public virtual IList<MEMicroThreeReportDetail> MEMicroThreeReportDetailList
		{
			get
			{
				if (_mEMicroThreeReportDetailList==null)
				{
					_mEMicroThreeReportDetailList = new List<MEMicroThreeReportDetail>();
				}
				return _mEMicroThreeReportDetailList;
			}
			set { _mEMicroThreeReportDetailList = value; }
		}

        
		#endregion
	}
	#endregion
}
