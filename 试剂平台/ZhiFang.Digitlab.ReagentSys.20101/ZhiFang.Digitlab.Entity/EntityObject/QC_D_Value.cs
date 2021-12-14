using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.Entity
{
	#region QCDValue

	/// <summary>
	/// QCDValue object for NHibernate mapped table 'QC_D_Value'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "质控数据", ClassCName = "QCDValue", ShortCode = "QCDValue", Desc = "质控数据")]
	public class QCDValue : BaseEntity
	{
		#region Member Variables
		
        protected DateTime? _receiveTime;
        protected int _qCDataLotNo;
        protected string _origlValue;
        protected string _reportValue;
        protected double? _quanValue;
        protected double? _quanValue2;
        protected string _cVValue;
        protected int _resultStatus;
        protected QCValueIsControlEnum _isControl;
        protected string _qCControlInfo;
        protected bool _isUse;
        protected bool _isEquipResult;
        protected DateTime? _dataUpdateTime;
        protected string _qCComment;
		protected HREmployee _operator;
		protected MEEquipSampleItem _mEEquipSampleItem;
		protected QCItem _qCItem;
        //protected IList<QCDLoseValue> _qCDLoseValueList;
        protected QCDLoseValue _qCDLoseValue;
        private double? unifystandard;
        private TargetModel targetMode;

        public virtual TargetModel TargetMode
        {
            get { return targetMode; }
            set { targetMode = value; }
        }

		#endregion

		#region Constructors

		public QCDValue() { }

        public QCDValue(long labID, DateTime receiveTime, int qCDataLotNo, string origlValue, string reportValue, double quanValue, double quanValue2, string cVValue, int resultStatus, QCValueIsControlEnum? isControl, string qCControlInfo, bool isUse, bool isEquipResult, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, MEEquipSampleItem mEEquipSampleItem, QCItem qCItem)
		{
			this._labID = labID;
			this._receiveTime = receiveTime;
			this._qCDataLotNo = qCDataLotNo;
			this._origlValue = origlValue;
			this._reportValue = reportValue;
			this._quanValue = quanValue;
			this._quanValue2 = quanValue2;
			this._cVValue = cVValue;
			this._resultStatus = resultStatus;
			//this._isControl = isControl;
			this._qCControlInfo = qCControlInfo;
			this._isUse = isUse;
            this._isEquipResult = isEquipResult;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._mEEquipSampleItem = mEEquipSampleItem;
			this._qCItem = qCItem;
		}

        public QCDValue(long Id, DateTime? receiveTime, int qCDataLotNo, string reportValue, string qcComment, int isControl, string qcControlInfo, QCItem qcitem, double? target, double? sd)
        {
            this.Id = Id;
            this.ReceiveTime = receiveTime;
            this.QCDataLotNo = qCDataLotNo;
            this.ReportValue = reportValue;
            this.QCComment = qcComment;
            this.IsControl = (QCValueIsControlEnum)isControl;
            this.QCControlInfo = qcControlInfo;
            this.QCComment = qcComment;
            this.QCItem = qcitem;
            this.Target = target;
            this.SD = sd;
        }

        public QCDValue(long Id, DateTime? receiveTime, int qCDataLotNo, string reportValue)
        {
            this.Id = Id;
            this.ReceiveTime = receiveTime;
            this.QCDataLotNo = qCDataLotNo;
            this.ReportValue = reportValue;
        }

		#endregion

		#region Public Properties

        [DataMember]
        public virtual double? Target { get; set; }

        [DataMember]
        public virtual double? SD { get; set; }

        /// <summary>
        /// 归一处理之后的值
        /// </summary>
        public virtual double? UnifyStandard
        {
            get { return unifystandard; }
            set { unifystandard = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "质控时间", ShortCode = "ReceiveTime", Desc = "质控时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReceiveTime
		{
			get { return _receiveTime; }
			set { _receiveTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "日序号", ShortCode = "QCDataLotNo", Desc = "日序号", ContextType = SysDic.All, Length = 4)]
        public virtual int QCDataLotNo
		{
			get { return _qCDataLotNo; }
			set { _qCDataLotNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器原始数值", ShortCode = "OriglValue", Desc = "仪器原始数值", ContextType = SysDic.All, Length = 8)]
        public virtual string OriglValue
		{
			get { return _origlValue; }
			set { _origlValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "用户报告结果", ShortCode = "ReportValue", Desc = "用户报告结果", ContextType = SysDic.All, Length = 8)]
        public virtual string ReportValue
		{
			get { return _reportValue; }
			set { _reportValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "定量结果", ShortCode = "QuanValue", Desc = "定量结果", ContextType = SysDic.All, Length = 8)]
        public virtual double? QuanValue
		{
			get { return _quanValue; }
			set { _quanValue = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定量计算结果", ShortCode = "QuanValue2", Desc = "定量计算结果", ContextType = SysDic.All, Length = 8)]
        public virtual double? QuanValue2
		{
			get { return _quanValue2; }
			set { _quanValue2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "变异系数", ShortCode = "CVValue", Desc = "变异系数", ContextType = SysDic.All, Length = 20)]
        public virtual string CVValue
		{
			get { return _cVValue; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CVValue", value, value.ToString());
				_cVValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定性结果状态", ShortCode = "ResultStatus", Desc = "定性结果状态", ContextType = SysDic.All, Length = 4)]
        public virtual int ResultStatus
		{
			get { return _resultStatus; }
			set { _resultStatus = value; }
		}

        [DataMember]
        [DataDesc(CName = "在控类别", ShortCode = "IsControl", Desc = "在控类别", ContextType = SysDic.All, Length = 4)]
        public virtual QCValueIsControlEnum IsControl
		{
			get { return _isControl; }
			set { _isControl = value; }
		}

        [DataMember]
        [DataDesc(CName = "失控规则", ShortCode = "QCControlInfo", Desc = "失控规则", ContextType = SysDic.All, Length = 50)]
        public virtual string QCControlInfo
		{
			get { return _qCControlInfo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for QCControlInfo", value, value.ToString());
				_qCControlInfo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否仪器结果", ShortCode = "IsEquipResult", Desc = "是否仪器结果", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsEquipResult
        {
            get { return _isEquipResult; }
            set { _isEquipResult = value; }
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
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
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
        [DataDesc(CName = "员工", ShortCode = "Operator", Desc = "员工")]
		public virtual HREmployee Operator
		{
			get { return _operator; }
			set { _operator = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器样本项目", ShortCode = "MEEquipSampleItem", Desc = "仪器样本项目")]
		public virtual MEEquipSampleItem MEEquipSampleItem
		{
			get { return _mEEquipSampleItem; }
			set { _mEEquipSampleItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "质控项目", ShortCode = "QCItem", Desc = "质控项目")]
		public virtual QCItem QCItem
		{
			get { return _qCItem; }
			set { _qCItem = value; }
		}

        //[DataMember]
        //[DataDesc(CName = "质控失控处理", ShortCode = "QCDLoseValueList", Desc = "质控失控处理")]
        //public virtual IList<QCDLoseValue> QCDLoseValueList
        //{
        //    get
        //    {
        //        if (_qCDLoseValueList==null)
        //        {
        //            _qCDLoseValueList = new List<QCDLoseValue>();
        //        }
        //        return _qCDLoseValueList;
        //    }
        //    set { _qCDLoseValueList = value; }
        //}
        [DataMember]
        [DataDesc(CName = "质控失控处理", ShortCode = "QCDLoseValue", Desc = "质控失控处理")]
        public virtual QCDLoseValue QCDLoseValue
        {
            get { return _qCDLoseValue; }
            set { _qCDLoseValue = value; }
        }

        
		#endregion
	}
	#endregion
}