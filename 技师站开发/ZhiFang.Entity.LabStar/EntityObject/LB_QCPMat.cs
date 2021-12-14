using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LBQCPMat

	/// <summary>
	/// LBQCPMat object for NHibernate mapped table 'LB_QCPMat'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "LB_QCPMat质控物   质控物入库管理等，还未分配到检验仪器", ClassCName = "LBQCPMat", ShortCode = "LBQCPMat", Desc = "LB_QCPMat质控物   质控物入库管理等，还未分配到检验仪器")]
	public class LBQCPMat : BaseEntity
	{
		#region Member Variables

		protected string _manu;
		protected string _cName;
		protected string _eName;
		protected string _sName;
		protected string _concLevel;
		protected string _useCode;
		protected string _standCode;
		protected string _deveCode;
		protected string _shortcode;
		protected string _pinYinZiTou;
		protected string _manuQCStoreInfo;
		protected string _manuQCInfo;
		protected string _comment;
		protected bool _isUse;
		protected int _dispOrder;
		protected DateTime? _endDate;
		protected DateTime? _dataUpdateTime;
		protected bool _bLotNo;
		protected long? _specialtyID;
		protected int _iEQAType;
		protected long? _planID;

		#endregion

		#region Constructors

		public LBQCPMat() { }

		public LBQCPMat(long labID, string manu, string cName, string eName, string sName, string concLevel, string useCode, string standCode, string deveCode, string shortcode, string pinYinZiTou, string manuQCStoreInfo, string manuQCInfo, string comment, bool isUse, int dispOrder, DateTime endDate, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool bLotNo, long specialtyID, int iEQAType, long planID)
		{
			this._labID = labID;
			this._manu = manu;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._concLevel = concLevel;
			this._useCode = useCode;
			this._standCode = standCode;
			this._deveCode = deveCode;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._manuQCStoreInfo = manuQCStoreInfo;
			this._manuQCInfo = manuQCInfo;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._endDate = endDate;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bLotNo = bLotNo;
			this._specialtyID = specialtyID;
			this._iEQAType = iEQAType;
			this._planID = planID;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "厂家", ShortCode = "Manu", Desc = "厂家", ContextType = SysDic.All, Length = 50)]
		public virtual string Manu
		{
			get { return _manu; }
			set { _manu = value; }
		}

		[DataMember]
		[DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
		public virtual string CName
		{
			get { return _cName; }
			set { _cName = value; }
		}

		[DataMember]
		[DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
		public virtual string EName
		{
			get { return _eName; }
			set { _eName = value; }
		}

		[DataMember]
		[DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
		public virtual string SName
		{
			get { return _sName; }
			set { _sName = value; }
		}

		[DataMember]
		[DataDesc(CName = "浓度水平", ShortCode = "ConcLevel", Desc = "浓度水平", ContextType = SysDic.All, Length = 20)]
		public virtual string ConcLevel
		{
			get { return _concLevel; }
			set { _concLevel = value; }
		}

		[DataMember]
		[DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
		public virtual string UseCode
		{
			get { return _useCode; }
			set { _useCode = value; }
		}

		[DataMember]
		[DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
		public virtual string StandCode
		{
			get { return _standCode; }
			set { _standCode = value; }
		}

		[DataMember]
		[DataDesc(CName = "开发商标准代码", ShortCode = "DeveCode", Desc = "开发商标准代码", ContextType = SysDic.All, Length = 50)]
		public virtual string DeveCode
		{
			get { return _deveCode; }
			set { _deveCode = value; }
		}

		[DataMember]
		[DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 50)]
		public virtual string Shortcode
		{
			get { return _shortcode; }
			set { _shortcode = value; }
		}

		[DataMember]
		[DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
		public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set { _pinYinZiTou = value; }
		}

		[DataMember]
		[DataDesc(CName = "存储说明", ShortCode = "ManuQCStoreInfo", Desc = "存储说明", ContextType = SysDic.All, Length = 500)]
		public virtual string ManuQCStoreInfo
		{
			get { return _manuQCStoreInfo; }
			set { _manuQCStoreInfo = value; }
		}

		[DataMember]
		[DataDesc(CName = "厂家质控物描述", ShortCode = "ManuQCInfo", Desc = "厂家质控物描述", ContextType = SysDic.All, Length = 200)]
		public virtual string ManuQCInfo
		{
			get { return _manuQCInfo; }
			set { _manuQCInfo = value; }
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
		[DataDesc(CName = "", ShortCode = "BLotNo", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool BLotNo
		{
			get { return _bLotNo; }
			set { _bLotNo = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "SpecialtyID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? SpecialtyID
		{
			get { return _specialtyID; }
			set { _specialtyID = value; }
		}

		[DataMember]
		[DataDesc(CName = "质评类型", ShortCode = "IEQAType", Desc = "质评类型", ContextType = SysDic.All, Length = 4)]
		public virtual int IEQAType
		{
			get { return _iEQAType; }
			set { _iEQAType = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "质控计划ID", ShortCode = "PlanID", Desc = "质控计划ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? PlanID
		{
			get { return _planID; }
			set { _planID = value; }
		}

		#endregion
	}
	#endregion
}