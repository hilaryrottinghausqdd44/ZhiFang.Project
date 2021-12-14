using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.SA
{
    #region TestEquipItem

    /// <summary>
    /// TestEquipItem object for NHibernate mapped table 'TestEquipItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "TestEquipItem", ShortCode = "TestEquipItem", Desc = "")]
    public class TestEquipItem : BaseEntity
    {
        #region Member Variables

        protected long _testEquipID;
        protected long _testItemID;

        #endregion

        #region Constructors

        public TestEquipItem() { }

        public TestEquipItem(long labID, long testEquipID, long testItemID)
        {
            this._labID = labID;
            this._testEquipID = testEquipID;
            this._testItemID = testItemID;
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


        #endregion
    }
    #endregion
}