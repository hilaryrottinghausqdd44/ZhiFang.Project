using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region QCRuleUse

	/// <summary>
	/// QCRuleUse object for NHibernate mapped table 'QC_RuleUse'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "质控规则", ClassCName = "", ShortCode = "ZKGZ", Desc = "质控规则")]
    public class QCRuleUse : BaseEntity
	{
		#region Member Variables

		protected string _qCRuleUseName;
		protected string _qCRuleUseEName;
		protected string _qCRuleUseShortName;
		protected string _ruleDesc;
		protected bool _isDefault;
		protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected IList<QCItem> _qCItemList; 
		protected IList<QCRulesCon> _qCRulesCons;

		#endregion

		#region Constructors

		public QCRuleUse() { }

		public QCRuleUse( long labID, string qCRuleUseName, string qCRuleUseEName, string qCRuleUseShortName, string ruleDesc, bool isDefault, int dispOrder )
		{
			this._labID = labID;
			this._qCRuleUseName = qCRuleUseName;
			this._qCRuleUseEName = qCRuleUseEName;
			this._qCRuleUseShortName = qCRuleUseShortName;
			this._ruleDesc = ruleDesc;
			this._isDefault = isDefault;
			this._dispOrder = dispOrder;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "质控规则名称", ShortCode = "ZKGZMC", Desc = "质控规则名称", ContextType = SysDic.NText, Length = 50)]
		public virtual string QCRuleUseName
		{
			get { return _qCRuleUseName; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for QCRuleUseName", value, value.ToString());
				_qCRuleUseName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "质控规则英文名称", ShortCode = "ZKGZYWMC", Desc = "质控规则英文名称", ContextType = SysDic.NText, Length = 50)]
		public virtual string QCRuleUseEName
		{
			get { return _qCRuleUseEName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for QCRuleUseEName", value, value.ToString());
				_qCRuleUseEName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "质控规则简称", ShortCode = "ZKGZJC", Desc = "质控规则简称", ContextType = SysDic.NText, Length = 50)]
		public virtual string QCRuleUseShortName
		{
			get { return _qCRuleUseShortName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for QCRuleUseShortName", value, value.ToString());
				_qCRuleUseShortName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "质控规则描述", ShortCode = "ZKGZMS", Desc = "质控规则描述", ContextType = SysDic.NText, Length = 500)]
		public virtual string RuleDesc
		{
			get { return _ruleDesc; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for RuleDesc", value, value.ToString());
				_ruleDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否缺省质控规则", ShortCode = "SFQSZKGZ", Desc = "是否缺省质控规则", ContextType = SysDic.NText, Length = 10)]
		public virtual bool IsDefault
		{
			get { return _isDefault; }
            set { _isDefault = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "XSCX", Desc = "显示次序", ContextType = SysDic.Number, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
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
        [DataDesc(CName = "质控项目", ShortCode = "QCItemList", Desc = "质控项目")]
        public virtual IList<QCItem> QCItemList
        {
            get
            {
                if (_qCItemList == null)
                {
                    _qCItemList = new List<QCItem>();
                }
                return _qCItemList;
            }
            set { _qCItemList = value; }
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