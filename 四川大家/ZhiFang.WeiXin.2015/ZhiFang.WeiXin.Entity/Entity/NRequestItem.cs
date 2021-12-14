using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region NRequestItem

	/// <summary>
	/// NRequestItem object for NHibernate mapped table 'NRequestItem'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "NRequestItem", ShortCode = "NRequestItem", Desc = "")]
	public class NRequestItem : BaseEntity
	{
		#region Member Variables

		protected int _itemSource;
		protected long _nRequestFormNo;
		protected long _barCodeFormNo;
		protected int _formNo;
		protected int _tollItemNo;
		protected int _parItemNo;
		protected int _isCheckFee;
		protected int _receiveFlag;
		protected decimal _hISCharge;
		protected decimal _itemCharge;
		protected int _sampleTypeNo;
		protected string _zdy1;
		protected string _zdy2;
		protected string _serialNo;
		protected string _zdy3;
		protected string _zdy4;
		protected string _zdy5;
		protected int _deleteFlag;
		protected string _oldSerialNo;
		protected string _countNodesItemSource;
		protected int _reportFlag;
		protected int _partFlag;
		protected string _webLisOrgID;
		protected string _webLisSourceOrgID;
		protected string _webLisSourceOrgName;
		protected string _clientNo;
		protected string _clientName;
		protected string _lABNREQUESTFORMNO;
		protected int _comboId;
		protected string _comboName;
		protected string _itemPrice;
		protected string _itemAgio;
		protected string _itemAgioPrice;
		protected int _transitFlag;
		protected int _modifyFlag;
		protected DateTime? _modifyDate;
		protected int _errorFlag;
		protected int _isFree;
		protected int _isFreeStatus;
		protected int _isLocked;
		protected DateTime? _isLockedDate;
		protected int _isReportSend;
		protected string _jcfs;
		protected string _itemNamecw;
		protected DateTime? _senderTime1;
		protected string _senderTime1er;
		protected string _senderTime2er;
		protected DateTime? _collectDate;
		protected int _sectionNo1;
		protected string _sectionName;
		protected string _testDater;
		protected DateTime? _testDate;
		protected string _itemType;
		protected string _isLocker;
		protected DateTime? _reportSendDate;
		protected DateTime? _reportPrintDate;
		protected DateTime? _checkDate;
		protected DateTime? _senderTime2;
		protected string _nisampletypename;
		protected string _receiveSectionNo;
		protected string _receiveSectionName;
		protected string _checkDater;
		protected DateTime? _receiveDate;
		protected string _receiveDater;
		protected int _nEquipNo;
		protected string _nEquipName;
		protected string _isFreeType;
		protected string _lABCENTERNO;
		protected string _lABCLIENTNO;
		protected string _lABDONO;
		protected DateTime? _flagDateDelete;
		protected int _combiItemNo;
		protected DateTime? _collectTime;
		protected string _collecter;
		protected string _webLisFlag;
		protected string _parItemName;
		protected string _parItemCode;
		protected string _sampleType;
		protected string _checkType;
		protected string _checkTypeName;
		protected double _price;


		#endregion

		#region Constructors

		public NRequestItem() { }

		public NRequestItem(int itemSource, long nRequestFormNo, long barCodeFormNo, int formNo, int tollItemNo, int parItemNo, int isCheckFee, int receiveFlag, decimal hISCharge, decimal itemCharge, int sampleTypeNo, string zdy1, string zdy2, string serialNo, string zdy3, string zdy4, string zdy5, int deleteFlag, string oldSerialNo, string countNodesItemSource, int reportFlag, int partFlag, string webLisOrgID, string webLisSourceOrgID, string webLisSourceOrgName, string clientNo, string clientName, string lABNREQUESTFORMNO, int comboId, string comboName, string itemPrice, string itemAgio, string itemAgioPrice, int transitFlag, int modifyFlag, DateTime modifyDate, int errorFlag, int isFree, int isFreeStatus, int isLocked, DateTime isLockedDate, int isReportSend, string jcfs, string itemNamecw, DateTime senderTime1, string senderTime1er, string senderTime2er, DateTime collectDate, int sectionNo1, string sectionName, string testDater, DateTime testDate, string itemType, string isLocker, DateTime reportSendDate, DateTime reportPrintDate, DateTime checkDate, DateTime senderTime2, string nisampletypename, string receiveSectionNo, string receiveSectionName, string checkDater, DateTime receiveDate, string receiveDater, int nEquipNo, string nEquipName, string isFreeType, string lABCENTERNO, string lABCLIENTNO, string lABDONO, DateTime flagDateDelete, int combiItemNo, DateTime collectTime, string collecter, string webLisFlag, string parItemName, string parItemCode, string sampleType, string checkType, string checkTypeName, double price)
		{
			this._itemSource = itemSource;
			this._nRequestFormNo = nRequestFormNo;
			this._barCodeFormNo = barCodeFormNo;
			this._formNo = formNo;
			this._tollItemNo = tollItemNo;
			this._parItemNo = parItemNo;
			this._isCheckFee = isCheckFee;
			this._receiveFlag = receiveFlag;
			this._hISCharge = hISCharge;
			this._itemCharge = itemCharge;
			this._sampleTypeNo = sampleTypeNo;
			this._zdy1 = zdy1;
			this._zdy2 = zdy2;
			this._serialNo = serialNo;
			this._zdy3 = zdy3;
			this._zdy4 = zdy4;
			this._zdy5 = zdy5;
			this._deleteFlag = deleteFlag;
			this._oldSerialNo = oldSerialNo;
			this._countNodesItemSource = countNodesItemSource;
			this._reportFlag = reportFlag;
			this._partFlag = partFlag;
			this._webLisOrgID = webLisOrgID;
			this._webLisSourceOrgID = webLisSourceOrgID;
			this._webLisSourceOrgName = webLisSourceOrgName;
			this._clientNo = clientNo;
			this._clientName = clientName;
			this._lABNREQUESTFORMNO = lABNREQUESTFORMNO;
			this._comboId = comboId;
			this._comboName = comboName;
			this._itemPrice = itemPrice;
			this._itemAgio = itemAgio;
			this._itemAgioPrice = itemAgioPrice;
			this._transitFlag = transitFlag;
			this._modifyFlag = modifyFlag;
			this._modifyDate = modifyDate;
			this._errorFlag = errorFlag;
			this._isFree = isFree;
			this._isFreeStatus = isFreeStatus;
			this._isLocked = isLocked;
			this._isLockedDate = isLockedDate;
			this._isReportSend = isReportSend;
			this._jcfs = jcfs;
			this._itemNamecw = itemNamecw;
			this._senderTime1 = senderTime1;
			this._senderTime1er = senderTime1er;
			this._senderTime2er = senderTime2er;
			this._collectDate = collectDate;
			this._sectionNo1 = sectionNo1;
			this._sectionName = sectionName;
			this._testDater = testDater;
			this._testDate = testDate;
			this._itemType = itemType;
			this._isLocker = isLocker;
			this._reportSendDate = reportSendDate;
			this._reportPrintDate = reportPrintDate;
			this._checkDate = checkDate;
			this._senderTime2 = senderTime2;
			this._nisampletypename = nisampletypename;
			this._receiveSectionNo = receiveSectionNo;
			this._receiveSectionName = receiveSectionName;
			this._checkDater = checkDater;
			this._receiveDate = receiveDate;
			this._receiveDater = receiveDater;
			this._nEquipNo = nEquipNo;
			this._nEquipName = nEquipName;
			this._isFreeType = isFreeType;
			this._lABCENTERNO = lABCENTERNO;
			this._lABCLIENTNO = lABCLIENTNO;
			this._lABDONO = lABDONO;
			this._flagDateDelete = flagDateDelete;
			this._combiItemNo = combiItemNo;
			this._collectTime = collectTime;
			this._collecter = collecter;
			this._webLisFlag = webLisFlag;
			this._parItemName = parItemName;
			this._parItemCode = parItemCode;
			this._sampleType = sampleType;
			this._checkType = checkType;
			this._checkTypeName = checkTypeName;
			this._price = price;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "", ShortCode = "ItemSource", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int ItemSource
		{
			get { return _itemSource; }
			set { _itemSource = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "NRequestFormNo", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long NRequestFormNo
		{
			get { return _nRequestFormNo; }
			set { _nRequestFormNo = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "BarCodeFormNo", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long BarCodeFormNo
		{
			get { return _barCodeFormNo; }
			set { _barCodeFormNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "FormNo", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int FormNo
		{
			get { return _formNo; }
			set { _formNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "TollItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int TollItemNo
		{
			get { return _tollItemNo; }
			set { _tollItemNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ParItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int ParItemNo
		{
			get { return _parItemNo; }
			set { _parItemNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsCheckFee", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int IsCheckFee
		{
			get { return _isCheckFee; }
			set { _isCheckFee = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ReceiveFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int ReceiveFlag
		{
			get { return _receiveFlag; }
			set { _receiveFlag = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "HISCharge", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual decimal HISCharge
		{
			get { return _hISCharge; }
			set { _hISCharge = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "ItemCharge", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual decimal ItemCharge
		{
			get { return _itemCharge; }
			set { _itemCharge = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SampleTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int SampleTypeNo
		{
			get { return _sampleTypeNo; }
			set { _sampleTypeNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Zdy1", Desc = "", ContextType = SysDic.All, Length = 80)]
		public virtual string Zdy1
		{
			get { return _zdy1; }
			set
			{
				if (value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy1", value, value.ToString());
				_zdy1 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Zdy2", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string Zdy2
		{
			get { return _zdy2; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy2", value, value.ToString());
				_zdy2 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SerialNo", Desc = "", ContextType = SysDic.All, Length = 30)]
		public virtual string SerialNo
		{
			get { return _serialNo; }
			set
			{
				if (value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for SerialNo", value, value.ToString());
				_serialNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Zdy3", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string Zdy3
		{
			get { return _zdy3; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy3", value, value.ToString());
				_zdy3 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Zdy4", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string Zdy4
		{
			get { return _zdy4; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy4", value, value.ToString());
				_zdy4 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Zdy5", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string Zdy5
		{
			get { return _zdy5; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy5", value, value.ToString());
				_zdy5 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DeleteFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int DeleteFlag
		{
			get { return _deleteFlag; }
			set { _deleteFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "OldSerialNo", Desc = "", ContextType = SysDic.All, Length = 60)]
		public virtual string OldSerialNo
		{
			get { return _oldSerialNo; }
			set
			{
				if (value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for OldSerialNo", value, value.ToString());
				_oldSerialNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "CountNodesItemSource", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual string CountNodesItemSource
		{
			get { return _countNodesItemSource; }
			set
			{
				if (value != null && value.Length > 1)
					throw new ArgumentOutOfRangeException("Invalid value for CountNodesItemSource", value, value.ToString());
				_countNodesItemSource = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ReportFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int ReportFlag
		{
			get { return _reportFlag; }
			set { _reportFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "PartFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int PartFlag
		{
			get { return _partFlag; }
			set { _partFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "WebLisOrgID", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string WebLisOrgID
		{
			get { return _webLisOrgID; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisOrgID", value, value.ToString());
				_webLisOrgID = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "被录入送检单位编号", ShortCode = "WebLisSourceOrgID", Desc = "被录入送检单位编号", ContextType = SysDic.All, Length = 50)]
		public virtual string WebLisSourceOrgID
		{
			get { return _webLisSourceOrgID; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisSourceOrgID", value, value.ToString());
				_webLisSourceOrgID = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "被录入送检单位名称", ShortCode = "WebLisSourceOrgName", Desc = "被录入送检单位名称", ContextType = SysDic.All, Length = 300)]
		public virtual string WebLisSourceOrgName
		{
			get { return _webLisSourceOrgName; }
			set
			{
				if (value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisSourceOrgName", value, value.ToString());
				_webLisSourceOrgName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "录入医疗机构编号", ShortCode = "ClientNo", Desc = "录入医疗机构编号", ContextType = SysDic.All, Length = 50)]
		public virtual string ClientNo
		{
			get { return _clientNo; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ClientNo", value, value.ToString());
				_clientNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "录入医疗机构名称", ShortCode = "ClientName", Desc = "录入医疗机构名称", ContextType = SysDic.All, Length = 300)]
		public virtual string ClientName
		{
			get { return _clientName; }
			set
			{
				if (value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for ClientName", value, value.ToString());
				_clientName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "编号", ShortCode = "LABNREQUESTFORMNO", Desc = "编号", ContextType = SysDic.All, Length = 50)]
		public virtual string LABNREQUESTFORMNO
		{
			get { return _lABNREQUESTFORMNO; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABNREQUESTFORMNO", value, value.ToString());
				_lABNREQUESTFORMNO = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "套餐编号", ShortCode = "ComboId", Desc = "套餐编号", ContextType = SysDic.All, Length = 4)]
		public virtual int ComboId
		{
			get { return _comboId; }
			set { _comboId = value; }
		}

		[DataMember]
		[DataDesc(CName = "套餐名称", ShortCode = "ComboName", Desc = "套餐名称", ContextType = SysDic.All, Length = 50)]
		public virtual string ComboName
		{
			get { return _comboName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ComboName", value, value.ToString());
				_comboName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "项目价格", ShortCode = "ItemPrice", Desc = "项目价格", ContextType = SysDic.All, Length = 50)]
		public virtual string ItemPrice
		{
			get { return _itemPrice; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ItemPrice", value, value.ToString());
				_itemPrice = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "折扣", ShortCode = "ItemAgio", Desc = "折扣", ContextType = SysDic.All, Length = 50)]
		public virtual string ItemAgio
		{
			get { return _itemAgio; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ItemAgio", value, value.ToString());
				_itemAgio = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "折扣价格", ShortCode = "ItemAgioPrice", Desc = "折扣价格", ContextType = SysDic.All, Length = 50)]
		public virtual string ItemAgioPrice
		{
			get { return _itemAgioPrice; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ItemAgioPrice", value, value.ToString());
				_itemAgioPrice = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "迁移标志", ShortCode = "TransitFlag", Desc = "迁移标志", ContextType = SysDic.All, Length = 4)]
		public virtual int TransitFlag
		{
			get { return _transitFlag; }
			set { _transitFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "修改标志", ShortCode = "ModifyFlag", Desc = "修改标志", ContextType = SysDic.All, Length = 4)]
		public virtual int ModifyFlag
		{
			get { return _modifyFlag; }
			set { _modifyFlag = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "修改时间", ShortCode = "ModifyDate", Desc = "修改时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ModifyDate
		{
			get { return _modifyDate; }
			set { _modifyDate = value; }
		}

		[DataMember]
		[DataDesc(CName = "错误标志", ShortCode = "ErrorFlag", Desc = "错误标志", ContextType = SysDic.All, Length = 4)]
		public virtual int ErrorFlag
		{
			get { return _errorFlag; }
			set { _errorFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "免单标志", ShortCode = "IsFree", Desc = "免单标志", ContextType = SysDic.All, Length = 4)]
		public virtual int IsFree
		{
			get { return _isFree; }
			set { _isFree = value; }
		}

		[DataMember]
		[DataDesc(CName = "免单审核标志", ShortCode = "IsFreeStatus", Desc = "免单审核标志", ContextType = SysDic.All, Length = 4)]
		public virtual int IsFreeStatus
		{
			get { return _isFreeStatus; }
			set { _isFreeStatus = value; }
		}

		[DataMember]
		[DataDesc(CName = "锁定标志", ShortCode = "IsLocked", Desc = "锁定标志", ContextType = SysDic.All, Length = 4)]
		public virtual int IsLocked
		{
			get { return _isLocked; }
			set { _isLocked = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "锁定时间", ShortCode = "IsLockedDate", Desc = "锁定时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? IsLockedDate
		{
			get { return _isLockedDate; }
			set { _isLockedDate = value; }
		}

		[DataMember]
		[DataDesc(CName = "报告发送标志", ShortCode = "IsReportSend", Desc = "报告发送标志", ContextType = SysDic.All, Length = 4)]
		public virtual int IsReportSend
		{
			get { return _isReportSend; }
			set { _isReportSend = value; }
		}

		[DataMember]
		[DataDesc(CName = "检测方式", ShortCode = "Jcfs", Desc = "检测方式", ContextType = SysDic.All, Length = 50)]
		public virtual string Jcfs
		{
			get { return _jcfs; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Jcfs", value, value.ToString());
				_jcfs = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "项目名称财务", ShortCode = "ItemNamecw", Desc = "项目名称财务", ContextType = SysDic.All, Length = 50)]
		public virtual string ItemNamecw
		{
			get { return _itemNamecw; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ItemNamecw", value, value.ToString());
				_itemNamecw = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "一审日期", ShortCode = "SenderTime1", Desc = "一审日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? SenderTime1
		{
			get { return _senderTime1; }
			set { _senderTime1 = value; }
		}

		[DataMember]
		[DataDesc(CName = "一审人员", ShortCode = "SenderTime1er", Desc = "一审人员", ContextType = SysDic.All, Length = 50)]
		public virtual string SenderTime1er
		{
			get { return _senderTime1er; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SenderTime1er", value, value.ToString());
				_senderTime1er = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "二审人员", ShortCode = "SenderTime2er", Desc = "二审人员", ContextType = SysDic.All, Length = 50)]
		public virtual string SenderTime2er
		{
			get { return _senderTime2er; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SenderTime2er", value, value.ToString());
				_senderTime2er = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样日期", ShortCode = "CollectDate", Desc = "采样日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectDate
		{
			get { return _collectDate; }
			set { _collectDate = value; }
		}

		[DataMember]
		[DataDesc(CName = "小组号", ShortCode = "SectionNo1", Desc = "小组号", ContextType = SysDic.All, Length = 4)]
		public virtual int SectionNo1
		{
			get { return _sectionNo1; }
			set { _sectionNo1 = value; }
		}

		[DataMember]
		[DataDesc(CName = "小组名称", ShortCode = "SectionName", Desc = "小组名称", ContextType = SysDic.All, Length = 50)]
		public virtual string SectionName
		{
			get { return _sectionName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SectionName", value, value.ToString());
				_sectionName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "检验者", ShortCode = "TestDater", Desc = "检验者", ContextType = SysDic.All, Length = 50)]
		public virtual string TestDater
		{
			get { return _testDater; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TestDater", value, value.ToString());
				_testDater = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "检验时间", ShortCode = "TestDate", Desc = "检验时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? TestDate
		{
			get { return _testDate; }
			set { _testDate = value; }
		}

		[DataMember]
		[DataDesc(CName = "项目类型", ShortCode = "ItemType", Desc = "项目类型", ContextType = SysDic.All, Length = 50)]
		public virtual string ItemType
		{
			get { return _itemType; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ItemType", value, value.ToString());
				_itemType = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "财务锁定人员", ShortCode = "IsLocker", Desc = "财务锁定人员", ContextType = SysDic.All, Length = 50)]
		public virtual string IsLocker
		{
			get { return _isLocker; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for IsLocker", value, value.ToString());
				_isLocker = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "报告发送日期", ShortCode = "ReportSendDate", Desc = "报告发送日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReportSendDate
		{
			get { return _reportSendDate; }
			set { _reportSendDate = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "报告打印日期", ShortCode = "ReportPrintDate", Desc = "报告打印日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReportPrintDate
		{
			get { return _reportPrintDate; }
			set { _reportPrintDate = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "一审日期", ShortCode = "CheckDate", Desc = "一审日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CheckDate
		{
			get { return _checkDate; }
			set { _checkDate = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "二审日期", ShortCode = "SenderTime2", Desc = "二审日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? SenderTime2
		{
			get { return _senderTime2; }
			set { _senderTime2 = value; }
		}

		[DataMember]
		[DataDesc(CName = "标本类型", ShortCode = "Nisampletypename", Desc = "标本类型", ContextType = SysDic.All, Length = 20)]
		public virtual string Nisampletypename
		{
			get { return _nisampletypename; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Nisampletypename", value, value.ToString());
				_nisampletypename = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "小组号", ShortCode = "ReceiveSectionNo", Desc = "小组号", ContextType = SysDic.All, Length = 50)]
		public virtual string ReceiveSectionNo
		{
			get { return _receiveSectionNo; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveSectionNo", value, value.ToString());
				_receiveSectionNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "小组名", ShortCode = "ReceiveSectionName", Desc = "小组名", ContextType = SysDic.All, Length = 50)]
		public virtual string ReceiveSectionName
		{
			get { return _receiveSectionName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveSectionName", value, value.ToString());
				_receiveSectionName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "审核人", ShortCode = "CheckDater", Desc = "审核人", ContextType = SysDic.All, Length = 50)]
		public virtual string CheckDater
		{
			get { return _checkDater; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CheckDater", value, value.ToString());
				_checkDater = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "核收时间", ShortCode = "ReceiveDate", Desc = "核收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReceiveDate
		{
			get { return _receiveDate; }
			set { _receiveDate = value; }
		}

		[DataMember]
		[DataDesc(CName = "核收人", ShortCode = "ReceiveDater", Desc = "核收人", ContextType = SysDic.All, Length = 20)]
		public virtual string ReceiveDater
		{
			get { return _receiveDater; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveDater", value, value.ToString());
				_receiveDater = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "仪器号", ShortCode = "NEquipNo", Desc = "仪器号", ContextType = SysDic.All, Length = 4)]
		public virtual int NEquipNo
		{
			get { return _nEquipNo; }
			set { _nEquipNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "仪器名", ShortCode = "NEquipName", Desc = "仪器名", ContextType = SysDic.All, Length = 50)]
		public virtual string NEquipName
		{
			get { return _nEquipName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NEquipName", value, value.ToString());
				_nEquipName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "免单类型", ShortCode = "IsFreeType", Desc = "免单类型", ContextType = SysDic.All, Length = 20)]
		public virtual string IsFreeType
		{
			get { return _isFreeType; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for IsFreeType", value, value.ToString());
				_isFreeType = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "中心编号", ShortCode = "LABCENTERNO", Desc = "中心编号", ContextType = SysDic.All, Length = 50)]
		public virtual string LABCENTERNO
		{
			get { return _lABCENTERNO; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABCENTERNO", value, value.ToString());
				_lABCENTERNO = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "客户编号", ShortCode = "LABCLIENTNO", Desc = "客户编号", ContextType = SysDic.All, Length = 50)]
		public virtual string LABCLIENTNO
		{
			get { return _lABCLIENTNO; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABCLIENTNO", value, value.ToString());
				_lABCLIENTNO = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "检验编号", ShortCode = "LABDONO", Desc = "检验编号", ContextType = SysDic.All, Length = 50)]
		public virtual string LABDONO
		{
			get { return _lABDONO; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABDONO", value, value.ToString());
				_lABDONO = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "录入时间", ShortCode = "FlagDateDelete", Desc = "录入时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? FlagDateDelete
		{
			get { return _flagDateDelete; }
			set { _flagDateDelete = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "CombiItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int CombiItemNo
		{
			get { return _combiItemNo; }
			set { _combiItemNo = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "CollectTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectTime
		{
			get { return _collectTime; }
			set { _collectTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Collecter", Desc = "", ContextType = SysDic.All, Length = 10)]
		public virtual string Collecter
		{
			get { return _collecter; }
			set
			{
				if (value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Collecter", value, value.ToString());
				_collecter = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "WebLisFlag", Desc = "", ContextType = SysDic.All, Length = 2)]
		public virtual string WebLisFlag
		{
			get { return _webLisFlag; }
			set
			{
				if (value != null && value.Length > 2)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisFlag", value, value.ToString());
				_webLisFlag = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ParItemName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string ParItemName
		{
			get { return _parItemName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ParItemName", value, value.ToString());
				_parItemName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ParItemCode", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string ParItemCode
		{
			get { return _parItemCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ParItemCode", value, value.ToString());
				_parItemCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SampleType", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string SampleType
		{
			get { return _sampleType; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SampleType", value, value.ToString());
				_sampleType = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "CheckType", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string CheckType
		{
			get { return _checkType; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CheckType", value, value.ToString());
				_checkType = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "CheckTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string CheckTypeName
		{
			get { return _checkTypeName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CheckTypeName", value, value.ToString());
				_checkTypeName = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "Price", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual double Price
		{
			get { return _price; }
			set { _price = value; }
		}


		#endregion
	}
	#endregion
}