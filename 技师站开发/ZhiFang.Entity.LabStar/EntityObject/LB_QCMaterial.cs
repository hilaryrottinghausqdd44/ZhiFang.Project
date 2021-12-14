using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LBQCMaterial

	/// <summary>
	/// LBQCMaterial object for NHibernate mapped table 'LB_QCMaterial'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "LB_QCMaterial 仪器质控物   仪器与质控物的关系   同时，指明质控物在仪器中的一些信息。   例如：   仪器质控物名称，   仪器模块，   仪器标识，   是否使用，   显示次序等", ClassCName = "LBQCMaterial", ShortCode = "LBQCMaterial", Desc = "LB_QCMaterial 仪器质控物   仪器与质控物的关系   同时，指明质控物在仪器中的一些信息。   例如：   仪器质控物名称，   仪器模块，   仪器标识，   是否使用，   显示次序等")]
	public class LBQCMaterial : BaseEntity
	{
		#region Member Variables

		protected int _qCMatNo;
		protected string _cName;
		protected string _sName;
		protected string _equipModule;
		protected string _concLevel;
		protected string _markID;
		protected int _matType;
		protected long? _microID;
		protected string _qCGroup;
		protected string _qCWithGroup;
		protected bool _isQCWithMain;
		protected string _comment;
		protected bool _isUse;
		protected int _dispOrder;
		protected DateTime? _begindate;
		protected DateTime? _endDate;
		protected DateTime? _dataUpdateTime;
		protected LBEquip _lBEquip;
		protected LBQCPMat _lBQCPMat;

		#endregion

		#region Constructors

		public LBQCMaterial() { }

		public LBQCMaterial(long labID, int qCMatNo, string cName, string sName, string equipModule, string concLevel, string markID, int matType, long microID, string qCGroup, string qCWithGroup, bool isQCWithMain, string comment, bool isUse, int dispOrder, DateTime begindate, DateTime endDate, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBEquip lBEquip, LBQCPMat lBQCPMat)
		{
			this._labID = labID;
			this._qCMatNo = qCMatNo;
			this._cName = cName;
			this._sName = sName;
			this._equipModule = equipModule;
			this._concLevel = concLevel;
			this._markID = markID;
			this._matType = matType;
			this._microID = microID;
			this._qCGroup = qCGroup;
			this._qCWithGroup = qCWithGroup;
			this._isQCWithMain = isQCWithMain;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._begindate = begindate;
			this._endDate = endDate;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._lBEquip = lBEquip;
			this._lBQCPMat = lBQCPMat;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "质控物编码", ShortCode = "QCMatNo", Desc = "质控物编码", ContextType = SysDic.All, Length = 4)]
		public virtual int QCMatNo
		{
			get { return _qCMatNo; }
			set { _qCMatNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
		public virtual string CName
		{
			get { return _cName; }
			set { _cName = value; }
		}

		[DataMember]
		[DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
		public virtual string SName
		{
			get { return _sName; }
			set { _sName = value; }
		}

		[DataMember]
		[DataDesc(CName = "仪器模块", ShortCode = "EquipModule", Desc = "仪器模块", ContextType = SysDic.All, Length = 50)]
		public virtual string EquipModule
		{
			get { return _equipModule; }
			set { _equipModule = value; }
		}

		[DataMember]
		[DataDesc(CName = "浓度水平", ShortCode = "ConcLevel", Desc = "浓度水平", ContextType = SysDic.All, Length = 20)]
		public virtual string ConcLevel
		{
			get { return _concLevel; }
			set { _concLevel = value; }
		}

		[DataMember]
		[DataDesc(CName = "通讯标识", ShortCode = "MarkID", Desc = "通讯标识", ContextType = SysDic.All, Length = 50)]
		public virtual string MarkID
		{
			get { return _markID; }
			set { _markID = value; }
		}

		[DataMember]
		[DataDesc(CName = "质控类型0：常规质控 1：微生物质控 ", ShortCode = "MatType", Desc = "质控类型0：常规质控 1：微生物质控 ", ContextType = SysDic.All, Length = 4)]
		public virtual int MatType
		{
			get { return _matType; }
			set { _matType = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "微生物ID", ShortCode = "MicroID", Desc = "微生物ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? MicroID
		{
			get { return _microID; }
			set { _microID = value; }
		}

		[DataMember]
		[DataDesc(CName = "质控分组", ShortCode = "QCGroup", Desc = "质控分组", ContextType = SysDic.All, Length = 50)]
		public virtual string QCGroup
		{
			get { return _qCGroup; }
			set { _qCGroup = value; }
		}

		[DataMember]
		[DataDesc(CName = "相同的联合组", ShortCode = "QCWithGroup", Desc = "相同的联合组", ContextType = SysDic.All, Length = 50)]
		public virtual string QCWithGroup
		{
			get { return _qCWithGroup; }
			set { _qCWithGroup = value; }
		}

		[DataMember]
		[DataDesc(CName = "联合组主参考", ShortCode = "IsQCWithMain", Desc = "联合组主参考", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsQCWithMain
		{
			get { return _isQCWithMain; }
			set { _isQCWithMain = value; }
		}

		[DataMember]
		[DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
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
		[DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "开始日期", ShortCode = "Begindate", Desc = "开始日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Begindate
		{
			get { return _begindate; }
			set { _begindate = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "截止日期", ShortCode = "EndDate", Desc = "截止日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? EndDate
		{
			get { return _endDate; }
			set { _endDate = value; }
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
		[DataDesc(CName = "", ShortCode = "LBEquip", Desc = "")]
		public virtual LBEquip LBEquip
		{
			get { return _lBEquip; }
			set { _lBEquip = value; }
		}

		[DataMember]
		[DataDesc(CName = "LB_QCPMat质控物   质控物入库管理等，还未分配到检验仪器", ShortCode = "LBQCPMat", Desc = "LB_QCPMat质控物   质控物入库管理等，还未分配到检验仪器")]
		public virtual LBQCPMat LBQCPMat
		{
			get { return _lBQCPMat; }
			set { _lBQCPMat = value; }
		}

		#endregion
	}
	#endregion
}