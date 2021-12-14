using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region QCRulesCon

	/// <summary>
	/// QCRulesCon object for NHibernate mapped table 'QC_RulesCon'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "质控规则关系", ClassCName = "", ShortCode = "GZGX", Desc = "质控规则关系")]
    public class QCRulesCon : BaseEntity
	{
		#region Member Variables

		protected int _judgeIndex;
		protected int _loseType;
        protected DateTime? _dataUpdateTime;
		protected QCRuleBase _qCRuleBase;
		protected QCRuleUse _qCRuleUse;

		#endregion

		#region Constructors

		public QCRulesCon() { }

		public QCRulesCon( long labID, int judgeIndex, int loseType, QCRuleBase qCRuleBase, QCRuleUse qCRuleUse )
		{
			this._labID = labID;
			this._judgeIndex = judgeIndex;
			this._loseType = loseType;
			this._qCRuleBase = qCRuleBase;
			this._qCRuleUse = qCRuleUse;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "判定次序", ShortCode = "PDCX", Desc = "判定次序", ContextType = SysDic.Number, Length = 4)]
		public virtual int JudgeIndex
		{
			get { return _judgeIndex; }
			set { _judgeIndex = value; }
		}

        [DataMember]
        [DataDesc(CName = "判定级别", ShortCode = "PDJB", Desc = "判定级别", ContextType = SysDic.Number, Length = 4)]
		public virtual int LoseType
		{
			get { return _loseType; }
			set { _loseType = value; }
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
        [DataDesc(CName = "基本质控规则", ShortCode = "JBZKGZ", Desc = "基本质控规则")]
		public virtual QCRuleBase QCRuleBase
		{
			get { return _qCRuleBase; }
			set { _qCRuleBase = value; }
		}

        [DataMember]
        [DataDesc(CName = "质控规则", ShortCode = "ZKGZ", Desc = "质控规则")]
		public virtual QCRuleUse QCRuleUse
		{
			get { return _qCRuleUse; }
			set { _qCRuleUse = value; }
		}

		#endregion
	}
	#endregion
}