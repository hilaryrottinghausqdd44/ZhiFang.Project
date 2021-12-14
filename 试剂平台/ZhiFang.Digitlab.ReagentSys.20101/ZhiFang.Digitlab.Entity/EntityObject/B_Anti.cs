using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BAnti

	/// <summary>
	/// BAnti object for NHibernate mapped table 'B_Anti'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "抗生素", ClassCName = "BAnti", ShortCode = "BAnti", Desc = "抗生素")]
	public class BAnti : BaseEntity
	{
		#region Member Variables
		
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _concentration;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected IList<BAntiClass> _bAntiClassList; 
		protected IList<BAntiUse> _bAntiUseList; 
		protected IList<BMicroAntiRange> _bMicroAntiRangeList; 
		protected IList<BMicroGroupAntiClass> _bMicroGroupAntiClassList; 
		protected IList<MEMicroDSTValue> _mEMicroDSTValueList; 

		#endregion

		#region Constructors

		public BAnti() { }

		public BAnti( long labID, string useCode, string standCode, string deveCode, string cName, string eName, string sName, string shortcode, string concentration, string pinYinZiTou, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._useCode = useCode;
			this._standCode = standCode;
			this._deveCode = deveCode;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._concentration = concentration;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
		{
			get { return _useCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
				_useCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
		{
			get { return _standCode; }
			set
			{
				if ( value != null && value.Length > 50)
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
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DeveCode", value, value.ToString());
				_deveCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 50)
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
        [DataDesc(CName = "默认纸片浓度", ShortCode = "Concentration", Desc = "默认纸片浓度", ContextType = SysDic.All, Length = 50)]
        public virtual string Concentration
		{
			get { return _concentration; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Concentration", value, value.ToString());
				_concentration = value;
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
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
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
        [DataDesc(CName = "抗生素类型", ShortCode = "BAntiClassList", Desc = "抗生素类型")]
		public virtual IList<BAntiClass> BAntiClassList
		{
			get
			{
				if (_bAntiClassList==null)
				{
					_bAntiClassList = new List<BAntiClass>();
				}
				return _bAntiClassList;
			}
			set { _bAntiClassList = value; }
		}

        [DataMember]
        [DataDesc(CName = "抗生素用药方法", ShortCode = "BAntiUseList", Desc = "抗生素用药方法")]
		public virtual IList<BAntiUse> BAntiUseList
		{
			get
			{
				if (_bAntiUseList==null)
				{
					_bAntiUseList = new List<BAntiUse>();
				}
				return _bAntiUseList;
			}
			set { _bAntiUseList = value; }
		}

        [DataMember]
        [DataDesc(CName = "抗生素参考范围", ShortCode = "BMicroAntiRangeList", Desc = "抗生素参考范围")]
		public virtual IList<BMicroAntiRange> BMicroAntiRangeList
		{
			get
			{
				if (_bMicroAntiRangeList==null)
				{
					_bMicroAntiRangeList = new List<BMicroAntiRange>();
				}
				return _bMicroAntiRangeList;
			}
			set { _bMicroAntiRangeList = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物分组与抗生素类型关系", ShortCode = "BMicroGroupAntiClassList", Desc = "微生物分组与抗生素类型关系")]
		public virtual IList<BMicroGroupAntiClass> BMicroGroupAntiClassList
		{
			get
			{
				if (_bMicroGroupAntiClassList==null)
				{
					_bMicroGroupAntiClassList = new List<BMicroGroupAntiClass>();
				}
				return _bMicroGroupAntiClassList;
			}
			set { _bMicroGroupAntiClassList = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物药敏结果drug sensitive test", ShortCode = "MEMicroDSTValueList", Desc = "微生物药敏结果drug sensitive test")]
		public virtual IList<MEMicroDSTValue> MEMicroDSTValueList
		{
			get
			{
				if (_mEMicroDSTValueList==null)
				{
					_mEMicroDSTValueList = new List<MEMicroDSTValue>();
				}
				return _mEMicroDSTValueList;
			}
			set { _mEMicroDSTValueList = value; }
		}

        
		#endregion
	}
	#endregion
}