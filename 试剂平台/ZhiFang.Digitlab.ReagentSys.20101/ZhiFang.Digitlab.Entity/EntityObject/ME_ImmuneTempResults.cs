using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEImmuneTempResults

    /// <summary>
    /// MEImmuneTempResults object for NHibernate mapped table 'ME_ImmuneTempResults'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "存储酶免板定量计算后所用到的临时结果", ClassCName = "MEImmuneTempResults", ShortCode = "MEImmuneTempResults", Desc = "存储酶免板定量计算后所用到的临时结果")]
    public class MEImmuneTempResults : BaseEntity
    {
        #region Member Variables

        protected string _pointSets;
        protected string _niheInfo;
        protected DateTime? _dataUpdateTime;
        protected EPEquipItem _ePEquipItem;
        protected ItemAllItem _itemAllItem;
        protected MEImmunePlate _mEImmunePlate;

        #endregion

        #region Constructors

        public MEImmuneTempResults() { }

        public MEImmuneTempResults(long labID, string pointSets, string niheInfo, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, EPEquipItem ePEquipItem, ItemAllItem itemAllItem, MEImmunePlate mEImmunePlate)
        {
            this._labID = labID;
            this._pointSets = pointSets;
            this._niheInfo = niheInfo;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._ePEquipItem = ePEquipItem;
            this._itemAllItem = itemAllItem;
            this._mEImmunePlate = mEImmunePlate;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "拟合曲线点集", ShortCode = "PointSets", Desc = "拟合曲线点集", ContextType = SysDic.All, Length = 2000)]
        public virtual string PointSets
        {
            get { return _pointSets; }
            set
            {
                if (value != null && value.Length > 2000)
                    throw new ArgumentOutOfRangeException("Invalid value for PointSets", value, value.ToString());
                _pointSets = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "拟合公式，仅用于在客户端显示", ShortCode = "NiheInfo", Desc = "拟合公式，仅用于在客户端显示", ContextType = SysDic.All, Length = 500)]
        public virtual string NiheInfo
        {
            get { return _niheInfo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for NiheInfo", value, value.ToString());
                _niheInfo = value;
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
        [DataDesc(CName = "仪器项目关系表", ShortCode = "EPEquipItem", Desc = "仪器项目关系表")]
        public virtual EPEquipItem EPEquipItem
        {
            get { return _ePEquipItem; }
            set { _ePEquipItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目")]
        public virtual ItemAllItem ItemAllItem
        {
            get { return _itemAllItem; }
            set { _itemAllItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "存储酶免板的板信息：", ShortCode = "MEImmunePlate", Desc = "存储酶免板的板信息：")]
        public virtual MEImmunePlate MEImmunePlate
        {
            get { return _mEImmunePlate; }
            set { _mEImmunePlate = value; }
        }
        #endregion
    }
    #endregion
}