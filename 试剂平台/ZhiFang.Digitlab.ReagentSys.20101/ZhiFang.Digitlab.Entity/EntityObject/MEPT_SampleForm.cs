using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEPTSampleForm

    /// <summary>
    /// MEPTSampleForm object for NHibernate mapped table 'MEPT_SampleForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "样本单", ClassCName = "MEPTSampleForm", ShortCode = "MEPTSampleForm", Desc = "样本单")]
    public class MEPTSampleForm : BaseEntity
    {
        #region Member Variables

        protected string _barCode;
        protected int _isSpiltItem;
        protected int _isSendOut;
        protected long? _sendOutLabID;
        protected double _sampleCap;
        protected int _sampleCount;
        protected string _collectPart;
        protected DateTime? _barCodeOpTime;
        protected string _serialScanTime;
        protected int _barCodeSource;
        protected int _printCount;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected DateTime? _dataUpdateTime;
        protected string _amount;
        protected string _sampleCharacterName;
        protected long? _sampleCharacterID;
        protected BSampleStatus _bSampleStatus;
        protected MEPTOrderForm _mEPTOrderForm;
        protected BSampleType _bSampleType;
        protected MEPTSamplingGroup _mEPTSamplingGroup;
        protected IList<MEGroupSampleForm> _mEGroupSampleFormList;
        protected IList<MEMicroInoculant> _mEMicroInoculantList;
        protected IList<MEPTSampleDeliveryConditon> _mEPTSampleDeliveryConditonList;
        protected IList<MEPTSampleInceptConditon> _mEPTSampleInceptConditonList;
        protected IList<MEPTSampleItem> _mEPTSampleItemList;
        protected IList<MEPTSampleSendConditon> _mEPTSampleSendConditonList;

        #endregion

        #region Constructors

        public MEPTSampleForm() { }

        public MEPTSampleForm(long labID, string barCode, int isSpiltItem, int isSendOut, long sendOutLabID, double sampleCap, int sampleCount, string collectPart, DateTime barCodeOpTime, string serialScanTime, int barCodeSource, int printCount, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string amount, string sampleCharacterName, long sampleCharacterID, BSampleStatus bSampleStatus, MEPTOrderForm mEPTOrderForm, BSampleType bSampleType, MEPTSamplingGroup mEPTSamplingGroup)
        {
            this._labID = labID;
            this._barCode = barCode;
            this._isSpiltItem = isSpiltItem;
            this._isSendOut = isSendOut;
            this._sendOutLabID = sendOutLabID;
            this._sampleCap = sampleCap;
            this._sampleCount = sampleCount;
            this._collectPart = collectPart;
            this._barCodeOpTime = barCodeOpTime;
            this._serialScanTime = serialScanTime;
            this._barCodeSource = barCodeSource;
            this._printCount = printCount;
            this._zDY1 = zDY1;
            this._zDY2 = zDY2;
            this._zDY3 = zDY3;
            this._zDY4 = zDY4;
            this._zDY5 = zDY5;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._amount = amount;
            this._sampleCharacterName = sampleCharacterName;
            this._sampleCharacterID = sampleCharacterID;
            this._bSampleStatus = bSampleStatus;
            this._mEPTOrderForm = mEPTOrderForm;
            this._bSampleType = bSampleType;
            this._mEPTSamplingGroup = mEPTSamplingGroup;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "BarCode", Desc = "条码号", ContextType = SysDic.All, Length = 30)]
        public virtual string BarCode
        {
            get { return _barCode; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
                _barCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否拆分项目", ShortCode = "IsSpiltItem", Desc = "是否拆分项目", ContextType = SysDic.All, Length = 4)]
        public virtual int IsSpiltItem
        {
            get { return _isSpiltItem; }
            set { _isSpiltItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否外送样本", ShortCode = "IsSendOut", Desc = "是否外送样本", ContextType = SysDic.All, Length = 4)]
        public virtual int IsSendOut
        {
            get { return _isSendOut; }
            set { _isSendOut = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "外送实验室", ShortCode = "SendOutLabID", Desc = "外送实验室", ContextType = SysDic.All, Length = 8)]
        public virtual long? SendOutLabID
        {
            get { return _sendOutLabID; }
            set { _sendOutLabID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采样量", ShortCode = "SampleCap", Desc = "采样量", ContextType = SysDic.All, Length = 8)]
        public virtual double SampleCap
        {
            get { return _sampleCap; }
            set { _sampleCap = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样次数", ShortCode = "SampleCount", Desc = "采样次数", ContextType = SysDic.All, Length = 4)]
        public virtual int SampleCount
        {
            get { return _sampleCount; }
            set { _sampleCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样部位", ShortCode = "CollectPart", Desc = "采样部位", ContextType = SysDic.All, Length = 200)]
        public virtual string CollectPart
        {
            get { return _collectPart; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for CollectPart", value, value.ToString());
                _collectPart = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "条码生成时间", ShortCode = "BarCodeOpTime", Desc = "条码生成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BarCodeOpTime
        {
            get { return _barCodeOpTime; }
            set { _barCodeOpTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "条码扫描时间", ShortCode = "SerialScanTime", Desc = "条码扫描时间", ContextType = SysDic.All, Length = 30)]
        public virtual string SerialScanTime
        {
            get { return _serialScanTime; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for SerialScanTime", value, value.ToString());
                _serialScanTime = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "条码来源", ShortCode = "BarCodeSource", Desc = "条码来源", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeSource
        {
            get { return _barCodeSource; }
            set { _barCodeSource = value; }
        }

        [DataMember]
        [DataDesc(CName = "打印次数", ShortCode = "PrintCount", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
        {
            get { return _printCount; }
            set { _printCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "自定义1", ShortCode = "ZDY1", Desc = "自定义1", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY1
        {
            get { return _zDY1; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY1", value, value.ToString());
                _zDY1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义2", ShortCode = "ZDY2", Desc = "自定义2", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY2
        {
            get { return _zDY2; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY2", value, value.ToString());
                _zDY2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义3", ShortCode = "ZDY3", Desc = "自定义3", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY3
        {
            get { return _zDY3; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY3", value, value.ToString());
                _zDY3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义4", ShortCode = "ZDY4", Desc = "自定义4", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY4
        {
            get { return _zDY4; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY4", value, value.ToString());
                _zDY4 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义5", ShortCode = "ZDY5", Desc = "自定义5", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY5
        {
            get { return _zDY5; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY5", value, value.ToString());
                _zDY5 = value;
            }
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
        [DataDesc(CName = "签收时样本量", ShortCode = "Amount", Desc = "签收时样本量", ContextType = SysDic.All, Length = 50)]
        public virtual string Amount
        {
            get { return _amount; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Amount", value, value.ToString());
                _amount = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "签收时标本性状", ShortCode = "SampleCharacterName", Desc = "签收时标本性状", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleCharacterName
        {
            get { return _sampleCharacterName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SampleCharacterName", value, value.ToString());
                _sampleCharacterName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签收时标本性状ID", ShortCode = "SampleCharacterID", Desc = "签收时标本性状ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SampleCharacterID
        {
            get { return _sampleCharacterID; }
            set { _sampleCharacterID = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本状态", ShortCode = "BSampleStatus", Desc = "样本状态")]
        public virtual BSampleStatus BSampleStatus
        {
            get { return _bSampleStatus; }
            set { _bSampleStatus = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱单", ShortCode = "MEPTOrderForm", Desc = "医嘱单")]
        public virtual MEPTOrderForm MEPTOrderForm
        {
            get { return _mEPTOrderForm; }
            set { _mEPTOrderForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型")]
        public virtual BSampleType BSampleType
        {
            get { return _bSampleType; }
            set { _bSampleType = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样组设置", ShortCode = "MEPTSamplingGroup", Desc = "采样组设置")]
        public virtual MEPTSamplingGroup MEPTSamplingGroup
        {
            get { return _mEPTSamplingGroup; }
            set { _mEPTSamplingGroup = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本单", ShortCode = "MEGroupSampleFormList", Desc = "小组样本单")]
        public virtual IList<MEGroupSampleForm> MEGroupSampleFormList
        {
            get
            {
                if (_mEGroupSampleFormList == null)
                {
                    _mEGroupSampleFormList = new List<MEGroupSampleForm>();
                }
                return _mEGroupSampleFormList;
            }
            set { _mEGroupSampleFormList = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物接种记录", ShortCode = "MEMicroInoculantList", Desc = "微生物接种记录")]
        public virtual IList<MEMicroInoculant> MEMicroInoculantList
        {
            get
            {
                if (_mEMicroInoculantList == null)
                {
                    _mEMicroInoculantList = new List<MEMicroInoculant>();
                }
                return _mEMicroInoculantList;
            }
            set { _mEMicroInoculantList = value; }
        }

        [DataMember]
        [DataDesc(CName = "外送清单表", ShortCode = "MEPTSampleDeliveryConditonList", Desc = "外送清单表")]
        public virtual IList<MEPTSampleDeliveryConditon> MEPTSampleDeliveryConditonList
        {
            get
            {
                if (_mEPTSampleDeliveryConditonList == null)
                {
                    _mEPTSampleDeliveryConditonList = new List<MEPTSampleDeliveryConditon>();
                }
                return _mEPTSampleDeliveryConditonList;
            }
            set { _mEPTSampleDeliveryConditonList = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本签收关系表", ShortCode = "MEPTSampleInceptConditonList", Desc = "样本签收关系表")]
        public virtual IList<MEPTSampleInceptConditon> MEPTSampleInceptConditonList
        {
            get
            {
                if (_mEPTSampleInceptConditonList == null)
                {
                    _mEPTSampleInceptConditonList = new List<MEPTSampleInceptConditon>();
                }
                return _mEPTSampleInceptConditonList;
            }
            set { _mEPTSampleInceptConditonList = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本单项目", ShortCode = "MEPTSampleItemList", Desc = "样本单项目")]
        public virtual IList<MEPTSampleItem> MEPTSampleItemList
        {
            get
            {
                if (_mEPTSampleItemList == null)
                {
                    _mEPTSampleItemList = new List<MEPTSampleItem>();
                }
                return _mEPTSampleItemList;
            }
            set { _mEPTSampleItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本送检关系表", ShortCode = "MEPTSampleSendConditonList", Desc = "样本送检关系表")]
        public virtual IList<MEPTSampleSendConditon> MEPTSampleSendConditonList
        {
            get
            {
                if (_mEPTSampleSendConditonList == null)
                {
                    _mEPTSampleSendConditonList = new List<MEPTSampleSendConditon>();
                }
                return _mEPTSampleSendConditonList;
            }
            set { _mEPTSampleSendConditonList = value; }
        }


        #endregion
    }
    #endregion
}