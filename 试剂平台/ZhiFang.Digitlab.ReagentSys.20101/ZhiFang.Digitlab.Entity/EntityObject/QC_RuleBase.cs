using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region QCRuleBase

	/// <summary>
	/// QCRuleBase object for NHibernate mapped table 'QC_RuleBase'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "基本质控规则", ClassCName = "", ShortCode = "JBZKGZ", Desc = "基本质控规则")]
    public class QCRuleBase : BaseEntity
	{
		#region Member Variables
		
		
		protected int _countN;
		protected int _countM;
		protected double _multX;
		protected double _multY;
		protected int _ruleType;
		protected string _useCode;
		protected string _standCode;
		protected string _deveCode;
		protected string _cName;
		protected string _eName;
		protected string _sName;
		protected string _shortcode;
		protected string _pinYinZiTou;
		protected string _comment;
		protected bool _isUse;
		protected int _dispOrder;
        protected int _rulePara1;
        protected int _rulePara2;
		protected DateTime? _dataUpdateTime;
		protected IList<QCRulesCon> _qCRulesCons;

		#endregion

		#region Constructors

		public QCRuleBase() { }

		public QCRuleBase( long labID, int countN, int countM, double multX, double multY, int ruleType, string useCode, string standCode, string deveCode, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._countN = countN;
			this._countM = countM;
			this._multX = multX;
			this._multY = multY;
			this._ruleType = ruleType;
			this._useCode = useCode;
			this._standCode = standCode;
			this._deveCode = deveCode;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
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
        [DataDesc(CName = "CountN", ShortCode = "CountN", Desc = "CountN", ContextType = SysDic.Number, Length = 4)]
		public virtual int CountN
		{
			get { return _countN; }
			set { _countN = value; }
		}

        [DataMember]
        [DataDesc(CName = "CountM", ShortCode = "CountM", Desc = "CountM", ContextType = SysDic.Number, Length = 4)]
		public virtual int CountM
		{
			get { return _countM; }
			set { _countM = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "MultX", ShortCode = "MultX", Desc = "MultX", ContextType = SysDic.Number, Length = 8)]
		public virtual double MultX
		{
			get { return _multX; }
			set { _multX = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "MultY", ShortCode = "MultY", Desc = "MultY", ContextType = SysDic.Number, Length = 8)]
		public virtual double MultY
		{
			get { return _multY; }
			set { _multY = value; }
		}

        [DataMember]
        [DataDesc(CName = "规则类型", ShortCode = "GZLX", Desc = "规则类型", ContextType = SysDic.Number, Length = 4)]
		public virtual int RuleType
		{
			get { return _ruleType; }
			set { _ruleType = value; }
		}

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "DM", Desc = "代码", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "标准代码", ShortCode = "BZDM", Desc = "标准代码", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "开发商标准代码", ShortCode = "KFSBZDM", Desc = "开发商标准代码", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "名称", ShortCode = "MC", Desc = "名称", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "英文名称", ShortCode = "YWMC", Desc = "英文名称", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "简称", ShortCode = "JC", Desc = "简称", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "快捷码", ShortCode = "KJM", Desc = "快捷码", ContextType = SysDic.NText, Length = 20)]
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
        [DataDesc(CName = "汉字拼音字头", ShortCode = "HZPYZT", Desc = "汉字拼音字头", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "描述", ShortCode = "MS", Desc = "描述", ContextType = SysDic.NText)]
		public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "SFSY", Desc = "是否使用", ContextType = SysDic.All)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "XSCX", Desc = "显示次序", ContextType = SysDic.Number, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "规则参数1", ShortCode = "GZCS1", Desc = "规则参数1；‘R-nS’规则时的值说明：0：不需要判断，1：‘需要判断同浓度相邻批次两个值的结果是否失控’其他规则暂时还没有用到", ContextType = SysDic.Number, Length = 4)]
        public virtual int RulePara1
        {
            get { return _rulePara1; }
            set { _rulePara1 = value; }
        }

        [DataMember]
        [DataDesc(CName = "规则参数2", ShortCode = "GZCS2", Desc = "规则参数2；‘R-nS’规则时的值说明：0：不需要判断，1：‘需要判定每个点的值都超过n/2’其他规则暂时还没有用到", ContextType = SysDic.Number, Length = 4)]
        public virtual int RulePara2
        {
            get { return _rulePara2; }
            set { _rulePara2 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
        [DataMember]
        [DataDesc(CName = "规则关系列表", ShortCode = "GZGXLB", Desc = "规则关系列表", ContextType = SysDic.List)]
		public virtual IList<QCRulesCon> QCRulesConList
		{
			get
			{
				if (_qCRulesCons==null)
				{
                    _qCRulesCons = new List<QCRulesCon>();
				}
				return _qCRulesCons;
			}
			set { _qCRulesCons = value; }
		}

		#endregion
	}
	#endregion
}