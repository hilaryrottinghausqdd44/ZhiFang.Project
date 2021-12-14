using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBSectionPrint

    /// <summary>
    /// LBSectionPrint object for NHibernate mapped table 'LB_SectionPrint'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBSectionPrint", ShortCode = "LBSectionPrint", Desc = "")]
    public class LBSectionPrint : BaseEntity
    {
        #region Member Variables

        protected long? _sampleTypeID;
        protected long? _sickTypeID;
        protected bool _bDefPrint;
        protected string _printFormat;
        protected string _printProgram;
        protected string _defPrinter;
        protected string _formatPara;
        protected long? _clientID;
        protected int _itemCountMax;
        protected int _itemCountMin;
        protected int _printOrder;
        protected string _nodename;
        protected string _microattribute;
        protected DateTime? _dataUpdateTime;
        protected LBItem _lBItem;
        protected LBSection _lBSection;


        #endregion

        #region Constructors

        public LBSectionPrint() { }

        public LBSectionPrint(long labID, long sampleTypeID, long sickTypeID, bool bDefPrint, string printFormat, string printProgram, string defPrinter, string formatPara, long clientID, int itemCountMax, int itemCountMin, int printOrder, string nodename, string microattribute, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBItem lBItem, LBSection lBSection)
        {
            this._labID = labID;
            this._sampleTypeID = sampleTypeID;
            this._sickTypeID = sickTypeID;
            this._bDefPrint = bDefPrint;
            this._printFormat = printFormat;
            this._printProgram = printProgram;
            this._defPrinter = defPrinter;
            this._formatPara = formatPara;
            this._clientID = clientID;
            this._itemCountMax = itemCountMax;
            this._itemCountMin = itemCountMin;
            this._printOrder = printOrder;
            this._nodename = nodename;
            this._microattribute = microattribute;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBItem = lBItem;
            this._lBSection = lBSection;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SampleTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SampleTypeID
        {
            get { return _sampleTypeID; }
            set { _sampleTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SickTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SickTypeID
        {
            get { return _sickTypeID; }
            set { _sickTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BDefPrint", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool BDefPrint
        {
            get { return _bDefPrint; }
            set { _bDefPrint = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintFormat", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PrintFormat
        {
            get { return _printFormat; }
            set { _printFormat = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintProgram", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PrintProgram
        {
            get { return _printProgram; }
            set { _printProgram = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DefPrinter", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string DefPrinter
        {
            get { return _defPrinter; }
            set { _defPrinter = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FormatPara", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string FormatPara
        {
            get { return _formatPara; }
            set { _formatPara = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ClientID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ClientID
        {
            get { return _clientID; }
            set { _clientID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemCountMax", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemCountMax
        {
            get { return _itemCountMax; }
            set { _itemCountMax = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemCountMin", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemCountMin
        {
            get { return _itemCountMin; }
            set { _itemCountMin = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintOrder
        {
            get { return _printOrder; }
            set { _printOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Nodename", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Nodename
        {
            get { return _nodename; }
            set { _nodename = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Microattribute", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Microattribute
        {
            get { return _microattribute; }
            set { _microattribute = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBItem", Desc = "")]
        public virtual LBItem LBItem
        {
            get { return _lBItem; }
            set { _lBItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBSection", Desc = "")]
        public virtual LBSection LBSection
        {
            get { return _lBSection; }
            set { _lBSection = value; }
        }


        #endregion
    }
    #endregion
}