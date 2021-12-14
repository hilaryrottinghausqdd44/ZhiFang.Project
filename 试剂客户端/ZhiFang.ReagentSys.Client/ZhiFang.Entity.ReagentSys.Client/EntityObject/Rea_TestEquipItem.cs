using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaTestEquipItem

    /// <summary>
    /// ReaTestEquipItem object for NHibernate mapped table 'Rea_TestEquipItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaTestEquipItem", ShortCode = "ReaTestEquipItem", Desc = "")]
    public class ReaTestEquipItem : BaseEntity
    {
        #region Member Variables

        protected long _testEquipID;
        protected long _testItemID;
        protected int _dispOrder;
        protected int _visible;
        protected string _zX1;
        protected string _zX2;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public ReaTestEquipItem() { }

        public ReaTestEquipItem(long labID, long testEquipID, long testItemID, int dispOrder, int visible, string zX1, string zX2, DateTime dataUpdateTime, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._testEquipID = testEquipID;
            this._testItemID = testItemID;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._dataUpdateTime = dataUpdateTime;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TestEquipID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long TestEquipID
        {
            get { return _testEquipID; }
            set { _testEquipID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TestItemID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long TestItemID
        {
            get { return _testItemID; }
            set { _testItemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX1", value, value.ToString());
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX2", value, value.ToString());
                _zX2 = value;
            }
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

        #region 自定义
        [DataMember]
        [DataDesc(CName = "仪器信息", ShortCode = "ReaTestEquipLab", Desc = "仪器信息")]
        public virtual ReaTestEquipLab ReaTestEquipLab { get; set; }
        [DataMember]
        [DataDesc(CName = "项目信息", ShortCode = "ReaTestItem", Desc = "项目信息")]
        public virtual ReaTestItem ReaTestItem { get; set; }
        public ReaTestEquipItem(ReaTestEquipItem reatestequipitem, ReaTestEquipLab reatestequiplab, ReaTestItem reatestitem)
        {
            this._id = reatestequipitem.Id;
            this._labID = reatestequipitem.LabID;
            this._testEquipID = reatestequipitem.TestEquipID;
            this._testItemID = reatestequipitem.TestItemID;
            this._dispOrder = reatestequipitem.DispOrder;
            this._visible = reatestequipitem.Visible;
            this._zX1 = reatestequipitem.ZX1;
            this._zX2 = reatestequipitem.ZX2;
            this._dataUpdateTime = reatestequipitem.DataUpdateTime;
            this._dataAddTime = reatestequipitem.DataAddTime;
            this._dataTimeStamp = reatestequipitem.DataTimeStamp;
            this.ReaTestEquipLab = reatestequiplab;
            this.ReaTestItem = reatestitem;
        }
        #endregion
    }
    #endregion
}