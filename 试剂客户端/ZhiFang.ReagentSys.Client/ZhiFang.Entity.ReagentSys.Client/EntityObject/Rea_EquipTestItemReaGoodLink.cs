using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaEquipTestItemReaGoodLink

    /// <summary>
    /// ReaEquipTestItemReaGoodLink object for NHibernate mapped table 'Rea_EquipTestItemReaGoodLink'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaEquipTestItemReaGoodLink", ShortCode = "ReaEquipTestItemReaGoodLink", Desc = "")]
    public class ReaEquipTestItemReaGoodLink : BaseEntity
    {
        #region Member Variables

        protected long _testEquipID;
        protected long _testItemID;
        protected long? _goodsID;
        protected int _testCount;
        protected int _dispOrder;
        protected int _visible;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public ReaEquipTestItemReaGoodLink() { }

        public ReaEquipTestItemReaGoodLink(long labID, long testEquipID, long testItemID, long goodsID, int testCount, int dispOrder, int visible, string zX1, string zX2, string zX3, DateTime dataUpdateTime, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._testEquipID = testEquipID;
            this._testItemID = testItemID;
            this._goodsID = goodsID;
            this._testCount = testCount;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GoodsID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
        }

        [DataMember]
        [DataDesc(CName = "单位包装检测量", ShortCode = "TestCount", Desc = "单位包装检测量", ContextType = SysDic.All, Length = 4)]
        public virtual int TestCount
        {
            get { return _testCount; }
            set { _testCount = value; }
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
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX3", value, value.ToString());
                _zX3 = value;
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

        #region 添加自定义属性
        protected string _equipCode;
        protected string _equipCName;

        protected string _reaGoodsNo;
        protected string _goodsCName;
        protected string _goodsSName;
        protected string _unitName;
        protected string _unitMemo;

        protected string _testItemCode;
        protected string _testItemSName;
        protected string _testItemCName;

        [DataMember]
        [DataDesc(CName = "LIS项目编码", ShortCode = "TestItemCode", Desc = "LIS项目编码", ContextType = SysDic.All, Length = 60)]
        public virtual string TestItemCode
        {
            get { return _testItemCode; }
            set
            {
                _testItemCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestItemSName", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string TestItemSName
        {
            get { return _testItemSName; }
            set
            {
                _testItemSName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestItemCName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string TestItemCName
        {
            get { return _testItemCName; }
            set
            {
                _testItemCName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "LIS仪器编码", ShortCode = "EquipCode", Desc = "LIS仪器编码", ContextType = SysDic.All, Length = 60)]
        public virtual string EquipCode
        {
            get { return _equipCode; }
            set
            {
                _equipCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipCName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string EquipCName
        {
            get { return _equipCName; }
            set
            {
                _equipCName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaGoodsNo", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string ReaGoodsNo
        {
            get { return _reaGoodsNo; }
            set
            {
                _reaGoodsNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsCName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsCName
        {
            get { return _goodsCName; }
            set
            {
                _goodsCName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsSName", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string GoodsSName
        {
            get { return _goodsSName; }
            set
            {
                _goodsSName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "UnitName", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string UnitName
        {
            get { return _unitName; }
            set
            {
                _unitName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UnitMemo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string UnitMemo
        {
            get { return _unitMemo; }
            set
            {
                _unitMemo = value;
            }
        }
        public ReaEquipTestItemReaGoodLink(ReaEquipTestItemReaGoodLink reaequiptestitemreagoodlink, string equipCode, string equipCName, string testItemCode, string testItemSName, string testItemCName, string reaGoodsNo, string goodsCName, string goodsSName, string unitName, string unitMemo)
        {
            this._id = reaequiptestitemreagoodlink.Id;
            this._labID = reaequiptestitemreagoodlink.LabID;
            this._testEquipID = reaequiptestitemreagoodlink.TestEquipID;
            this._testItemID = reaequiptestitemreagoodlink.TestItemID;
            this._goodsID = reaequiptestitemreagoodlink.GoodsID;
            this._testCount = reaequiptestitemreagoodlink._testCount;
            this._dispOrder = reaequiptestitemreagoodlink.DispOrder;
            this._visible = reaequiptestitemreagoodlink.Visible;
            this._zX1 = reaequiptestitemreagoodlink.ZX1;
            this._zX2 = reaequiptestitemreagoodlink.ZX2;
            this._zX3 = reaequiptestitemreagoodlink.ZX3;
            this._dataUpdateTime = reaequiptestitemreagoodlink.DataUpdateTime;
            this._dataAddTime = reaequiptestitemreagoodlink.DataAddTime;
            this._dataTimeStamp = reaequiptestitemreagoodlink.DataTimeStamp;

            this._equipCode = equipCode;
            this._equipCName = equipCName;
            this._testItemCode = testItemCode;
            this._testItemSName = testItemSName;
            this._testItemCName = testItemCName;

            this._reaGoodsNo = reaGoodsNo;
            this._goodsCName = goodsCName;
            this._goodsSName = goodsSName;
            this._unitName = unitName;
            this._unitMemo = unitMemo;
        }
        #endregion
    }
    #endregion
}