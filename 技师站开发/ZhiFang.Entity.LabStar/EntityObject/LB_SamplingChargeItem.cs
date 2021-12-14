using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBSamplingChargeItem

    /// <summary>
    /// LBSamplingChargeItem object for NHibernate mapped table 'LB_SamplingChargeItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBSamplingChargeItem", ShortCode = "LBSamplingChargeItem", Desc = "")]
    public class LBSamplingChargeItem : BaseEntity
    {
        #region Member Variables

        protected long? _samplingGroupID;
        protected long? _itemID;
        protected int _chargeTimes;
        protected bool _isBatchCharge;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public LBSamplingChargeItem() { }

        public LBSamplingChargeItem(long samplingGroupID, long itemID, int chargeTimes, bool isBatchCharge, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._samplingGroupID = samplingGroupID;
            this._itemID = itemID;
            this._chargeTimes = chargeTimes;
            this._isBatchCharge = isBatchCharge;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SamplingGroupID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SamplingGroupID
        {
            get { return _samplingGroupID; }
            set { _samplingGroupID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ItemID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChargeTimes", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ChargeTimes
        {
            get { return _chargeTimes; }
            set { _chargeTimes = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsBatchCharge", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsBatchCharge
        {
            get { return _isBatchCharge; }
            set { _isBatchCharge = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}