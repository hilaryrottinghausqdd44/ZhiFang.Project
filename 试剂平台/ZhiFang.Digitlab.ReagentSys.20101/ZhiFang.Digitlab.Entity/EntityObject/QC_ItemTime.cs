using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region QCItemTime

	/// <summary>
	/// QCItemTime object for NHibernate mapped table 'QC_ItemTime'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "质控项目时效", ClassCName = "", ShortCode = "ZKXMSX", Desc = "质控项目时效")]
    public class QCItemTime : BaseEntity
	{
		#region Member Variables
		
		
		protected DateTime _beginDate;
		protected DateTime? _endDate;
		protected double? _target;
		protected double? _sD;
		protected double? _cV;
        protected double? _caclTarget;
        protected double? _caclSD;
		protected string _qcitemdesc;
		protected double? _warningLineH;
		protected double? _warningLineL;
		protected double? _maxErrorH;
		protected double? _maxErrorL;
		protected double? _mCCV;
		protected string _descTarget;
		protected string _descRange;
		protected string _descAll;
		protected bool _isUse;
		protected DateTime? _dataUpdateTime;
		protected QCItem _qCItem;
		protected QCMatTime _qCMatTime;

		#endregion

		#region Constructors

		public QCItemTime() { }

		public QCItemTime( long labID, DateTime beginDate, DateTime endDate, double target, double sD, double cV, string qcitemdesc, double warningLineH, double warningLineL, double maxErrorH, double maxErrorL, double mCCV, string descTarget, string descRange, string descAll, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, QCItem qCItem, QCMatTime qCMatTime )
		{
			this._labID = labID;
			this._beginDate = beginDate;
			this._endDate = endDate;
			this._target = target;
			this._sD = sD;
			this._cV = cV;
			this._qcitemdesc = qcitemdesc;
			this._warningLineH = warningLineH;
			this._warningLineL = warningLineL;
			this._maxErrorH = maxErrorH;
			this._maxErrorL = maxErrorL;
			this._mCCV = mCCV;
			this._descTarget = descTarget;
			this._descRange = descRange;
			this._descAll = descAll;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._qCItem = qCItem;
			this._qCMatTime = qCMatTime;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "启用日期", ShortCode = "QYRQ", Desc = "启用日期", ContextType = SysDic.DateTime)]
		public virtual DateTime BeginDate
		{
			get { return _beginDate; }
			set { _beginDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "终止日期", ShortCode = "ZZRQ", Desc = "终止日期", ContextType = SysDic.DateTime)]
		public virtual DateTime? EndDate
		{
			get { return _endDate; }
			set { _endDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "靶值", ShortCode = "BZ", Desc = "靶值", ContextType = SysDic.Number, Length = 8)]
		public virtual double? Target
		{
			get { return _target; }
			set { _target = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "标准差", ShortCode = "BZC", Desc = "标准差", ContextType = SysDic.Number, Length = 8)]
		public virtual double? SD
		{
			get { return _sD; }
			set { _sD = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "变异系数", ShortCode = "BYXS", Desc = "变异系数", ContextType = SysDic.Number, Length = 8)]
		public virtual double? CV
		{
			get { return _cV; }
			set { _cV = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计算靶值", ShortCode = "CaclTarget", Desc = "计算靶值", ContextType = SysDic.Number, Length = 8)]
        public virtual double? CaclTarget
        {
            get { return _caclTarget; }
            set { _caclTarget = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计算标准差", ShortCode = "CaclSD", Desc = "计算标准差", ContextType = SysDic.Number, Length = 8)]
        public virtual double? CaclSD
        {
            get { return _caclSD; }
            set { _caclSD = value; }
        }

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "MS", Desc = "描述", ContextType = SysDic.NText, Length = 200)]
		public virtual string QCItemDesc
		{
			get { return _qcitemdesc; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Qcitemdesc", value, value.ToString());
				_qcitemdesc = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "警告线H", ShortCode = "JGXH", Desc = "警告线H", ContextType = SysDic.Number, Length = 8)]
		public virtual double? WarningLineH
		{
			get { return _warningLineH; }
			set { _warningLineH = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "警告线L", ShortCode = "JGXL", Desc = "警告线L", ContextType = SysDic.Number, Length = 8)]
		public virtual double? WarningLineL
		{
			get { return _warningLineL; }
			set { _warningLineL = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "最大允许值线H", ShortCode = "ZDYXZXH", Desc = "最大允许值线H", ContextType = SysDic.Number, Length = 8)]
		public virtual double? MaxErrorH
		{
			get { return _maxErrorH; }
			set { _maxErrorH = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "最大允许值线L", ShortCode = "ZDYXZXL", Desc = "最大允许值线L", ContextType = SysDic.Number, Length = 8)]
		public virtual double? MaxErrorL
		{
			get { return _maxErrorL; }
			set { _maxErrorL = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "Monica变异系数", ShortCode = "MonicaBYXS", Desc = "Monica变异系数", ContextType = SysDic.Number, Length = 8)]
		public virtual double? MCCV
		{
			get { return _mCCV; }
			set { _mCCV = value; }
		}

        [DataMember]
        [DataDesc(CName = "定性靶值", ShortCode = "DXBZ", Desc = "定性靶值", ContextType = SysDic.NText, Length = 50)]
		public virtual string DescTarget
		{
			get { return _descTarget; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DescTarget", value, value.ToString());
				_descTarget = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定性在控范围", ShortCode = "DXZKFW", Desc = "定性在控范围", ContextType = SysDic.NText, Length = 50)]
		public virtual string DescRange
		{
			get { return _descRange; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DescRange", value, value.ToString());
				_descRange = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定性可描述范围", ShortCode = "DXKMSFW", Desc = "定性可描述范围", ContextType = SysDic.NText, Length = 50)]
		public virtual string DescAll
		{
			get { return _descAll; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DescAll", value, value.ToString());
				_descAll = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
        [DataMember]
        [DataDesc(CName = "质控项目", ShortCode = "ZKXM", Desc = "质控项目")]
		public virtual QCItem QCItem
		{
			get { return _qCItem; }
			set { _qCItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "质控物时效", ShortCode = "ZKWSX", Desc = "质控物时效")]
		public virtual QCMatTime QCMatTime
		{
			get { return _qCMatTime; }
			set { _qCMatTime = value; }
		}

		#endregion
	}
	#endregion
}