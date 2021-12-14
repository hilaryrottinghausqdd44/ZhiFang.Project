using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.Entity
{
	#region QCItem

	/// <summary>
	/// QCItem object for NHibernate mapped table 'QC_Item'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "质控项目", ClassCName = "", ShortCode = "ZKXM", Desc = "质控项目")]
    public class QCItem : BaseEntity
	{
		#region Member Variables

        protected double? _cCV;
        protected QCValueTypeEnum _valueType;
		protected int _isLog;
		protected string _comment;
		protected bool _isUse;
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;
        protected string _qCComment;
		protected ItemAllItem _itemAllItem;
        protected QCRuleUse _qCRuleUse;
		protected QCMat _qCMat;
        protected BMicAnti _bMicAnti;
		protected IList<QCDValue> _qCDValues;
		protected IList<QCItemTime> _qCItemTimes;

		#endregion

		#region Constructors

		public QCItem() { }

		public QCItem( long labID, int isLog, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem, QCMat qCMat )
		{
			this._labID = labID;
			this._isLog = isLog;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemAllItem = itemAllItem;
			this._qCMat = qCMat;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "设定变异系数", ShortCode = "CCV", Desc = "设定变异系数", ContextType = SysDic.Number, Length = 8)]
        public virtual double? CCV
        {
            get { return _cCV; }
            set { _cCV = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否对数值", ShortCode = "SFDSZ", Desc = "是否对数值", ContextType = SysDic.Number, Length = 4)]
		public virtual int IsLog
		{
			get { return _isLog; }
			set { _isLog = value; }
		}

        [DataMember]
        [DataDesc(CName = "质控类型", ShortCode = "ValueType", Desc = "质控类型", ContextType = SysDic.Number, Length = 4)]
        public virtual QCValueTypeEnum ValueType
		{
            get { return _valueType; }
            set { _valueType = value; }
		}

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "MS", Desc = "描述", ContextType = SysDic.NText)]
		public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 1000000)
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "QCComment ", Desc = "备注", ContextType = SysDic.All, Length = 200)]
        public virtual string QCComment
        {
            get { return _qCComment; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for QCComment", value, value.ToString());
                _qCComment = value;
            }
        }
        
        [DataMember]
        [DataDesc(CName = "项目", ShortCode = "XM", Desc = "项目")]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "质控规则", ShortCode = "QCRuleUse", Desc = "质控规则")]
        public virtual QCRuleUse QCRuleUse
        {
            get { return _qCRuleUse; }
            set { _qCRuleUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "质控物", ShortCode = "ZKW", Desc = "质控物")]
		public virtual QCMat QCMat
		{
			get { return _qCMat; }
			set { _qCMat = value; }
		}

        [DataMember]
        [DataDesc(CName = "抗生素表", ShortCode = "BMicAnti", Desc = "抗生素表")]
        public virtual BMicAnti BMicAnti
        {
            get { return _bMicAnti; }
            set { _bMicAnti = value; }
        }

        [DataMember]
        [DataDesc(CName = "质控数据列表", ShortCode = "ZKSJLB", Desc = "质控数据列表", ContextType = SysDic.List)]
		public virtual IList<QCDValue> QCDValueList
		{
			get
			{
				if (_qCDValues==null)
				{
                    _qCDValues = new List<QCDValue>();
				}
				return _qCDValues;
			}
			set { _qCDValues = value; }
		}

        [DataMember]
        [DataDesc(CName = "质控项目时效列表", ShortCode = "ZKXMSXLB", Desc = "质控项目时效列表", ContextType = SysDic.List)]
		public virtual IList<QCItemTime> QCItemTimeList
		{
			get
			{
				if (_qCItemTimes==null)
				{
                    _qCItemTimes = new List<QCItemTime>();
				}
				return _qCItemTimes;
			}
			set { _qCItemTimes = value; }
		}

		#endregion
	}
	#endregion
}