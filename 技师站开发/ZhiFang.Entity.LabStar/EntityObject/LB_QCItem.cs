using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LBQCItem

	/// <summary>
	/// LBQCItem object for NHibernate mapped table 'LB_QCItem'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "LB_QCItem质控项目", ClassCName = "LBQCItem", ShortCode = "LBQCItem", Desc = "LB_QCItem质控项目")]
	public class LBQCItem : BaseEntity
	{
		#region Member Variables

		protected int _qCItemNo;
		protected long? _antiID;
		protected bool _isLog;
		protected double _sDCV;
		protected int _qCDataType;
		protected string _qCDataTypeName;
		protected double _testDateInterval;
		protected int _iLostTimes;
		protected string _diagMethod;
		protected string _unit;
		protected string _testDesc;
		protected string _comment;
		protected bool _isUse;
		protected long? _userID;
		protected DateTime? _dataUpdateTime;
		protected string _itemName;
		protected int _prec;
		protected string _qCMatName;
		protected string _equipName;
		protected LBEquip _lBEquip;
		protected LBItem _lBItem;
		protected LBQCMaterial _lBQCMaterial;

		#endregion

		#region Constructors

		public LBQCItem() { }

		public LBQCItem(long labID, int qCItemNo, long antiID, bool isLog, double sDCV, int qCDataType, string qCDataTypeName, double testDateInterval, int iLostTimes, string diagMethod, string unit, string testDesc, string comment, bool isUse, long userID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string itemName, int prec, string qCMatName, string equipName, LBEquip lBEquip, LBItem lBItem, LBQCMaterial lBQCMaterial)
		{
			this._labID = labID;
			this._qCItemNo = qCItemNo;
			this._antiID = antiID;
			this._isLog = isLog;
			this._sDCV = sDCV;
			this._qCDataType = qCDataType;
			this._qCDataTypeName = qCDataTypeName;
			this._testDateInterval = testDateInterval;
			this._iLostTimes = iLostTimes;
			this._diagMethod = diagMethod;
			this._unit = unit;
			this._testDesc = testDesc;
			this._comment = comment;
			this._isUse = isUse;
			this._userID = userID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemName = itemName;
			this._prec = prec;
			this._qCMatName = qCMatName;
			this._equipName = equipName;
			this._lBEquip = lBEquip;
			this._lBItem = lBItem;
			this._lBQCMaterial = lBQCMaterial;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "质控项目编码", ShortCode = "QCItemNo", Desc = "质控项目编码", ContextType = SysDic.All, Length = 4)]
		public virtual int QCItemNo
		{
			get { return _qCItemNo; }
			set { _qCItemNo = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "抗生素ID", ShortCode = "AntiID", Desc = "抗生素ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? AntiID
		{
			get { return _antiID; }
			set { _antiID = value; }
		}

		[DataMember]
		[DataDesc(CName = "是否对数值", ShortCode = "IsLog", Desc = "是否对数值", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsLog
		{
			get { return _isLog; }
			set { _isLog = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "设定CV", ShortCode = "SDCV", Desc = "设定CV", ContextType = SysDic.All, Length = 8)]
		public virtual double SDCV
		{
			get { return _sDCV; }
			set { _sDCV = value; }
		}

		[DataMember]
		[DataDesc(CName = "0：靶值标准差， 1：定性 2：值范围", ShortCode = "QCDataType", Desc = "0：靶值标准差， 1：定性 2：值范围", ContextType = SysDic.All, Length = 4)]
		public virtual int QCDataType
		{
			get { return _qCDataType; }
			set { _qCDataType = value; }
		}

		[DataMember]
		[DataDesc(CName = "质控类型名称", ShortCode = "QCDataTypeName", Desc = "质控类型名称", ContextType = SysDic.All, Length = 20)]
		public virtual string QCDataTypeName
		{
			get { return _qCDataTypeName; }
			set { _qCDataTypeName = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "质控日期间隔", ShortCode = "TestDateInterval", Desc = "质控日期间隔", ContextType = SysDic.All, Length = 8)]
		public virtual double TestDateInterval
		{
			get { return _testDateInterval; }
			set { _testDateInterval = value; }
		}

		[DataMember]
		[DataDesc(CName = "多次失控处理", ShortCode = "ILostTimes", Desc = "多次失控处理", ContextType = SysDic.All, Length = 4)]
		public virtual int ILostTimes
		{
			get { return _iLostTimes; }
			set { _iLostTimes = value; }
		}

		[DataMember]
		[DataDesc(CName = "检验方法", ShortCode = "DiagMethod", Desc = "检验方法", ContextType = SysDic.All, Length = 50)]
		public virtual string DiagMethod
		{
			get { return _diagMethod; }
			set { _diagMethod = value; }
		}

		[DataMember]
		[DataDesc(CName = "单位", ShortCode = "Unit", Desc = "单位", ContextType = SysDic.All, Length = 50)]
		public virtual string Unit
		{
			get { return _unit; }
			set { _unit = value; }
		}

		[DataMember]
		[DataDesc(CName = "检验说明", ShortCode = "TestDesc", Desc = "检验说明", ContextType = SysDic.All, Length = 50)]
		public virtual string TestDesc
		{
			get { return _testDesc; }
			set { _testDesc = value; }
		}

		[DataMember]
		[DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 500)]
		public virtual string Comment
		{
			get { return _comment; }
			set { _comment = value; }
		}

		[DataMember]
		[DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "操作者", ShortCode = "UserID", Desc = "操作者", ContextType = SysDic.All, Length = 8)]
		public virtual long? UserID
		{
			get { return _userID; }
			set { _userID = value; }
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
		[DataDesc(CName = "", ShortCode = "ItemName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string ItemName
		{
			get { return _itemName; }
			set { _itemName = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Prec", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int Prec
		{
			get { return _prec; }
			set { _prec = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "QCMatName", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string QCMatName
		{
			get { return _qCMatName; }
			set { _qCMatName = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "EquipName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string EquipName
		{
			get { return _equipName; }
			set { _equipName = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "LBEquip", Desc = "")]
		public virtual LBEquip LBEquip
		{
			get { return _lBEquip; }
			set { _lBEquip = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "LBItem", Desc = "")]
		public virtual LBItem LBItem
		{
			get { return _lBItem; }
			set { _lBItem = value; }
		}

		[DataMember]
		[DataDesc(CName = "LB_QCMaterial 仪器质控物   仪器与质控物的关系   同时，指明质控物在仪器中的一些信息。   例如：   仪器质控物名称，   仪器模块，   仪器标识，   是否使用，   显示次序等", ShortCode = "LBQCMaterial", Desc = "LB_QCMaterial 仪器质控物   仪器与质控物的关系   同时，指明质控物在仪器中的一些信息。   例如：   仪器质控物名称，   仪器模块，   仪器标识，   是否使用，   显示次序等")]
		public virtual LBQCMaterial LBQCMaterial
		{
			get { return _lBQCMaterial; }
			set { _lBQCMaterial = value; }
		}
	
		#endregion
	}
	#endregion
}