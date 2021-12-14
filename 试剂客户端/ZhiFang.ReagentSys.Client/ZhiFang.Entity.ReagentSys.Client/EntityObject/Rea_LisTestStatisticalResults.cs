using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaLisTestStatisticalResults

    /// <summary>
    /// ReaLisTestStatisticalResults object for NHibernate mapped table 'Rea_LisTestStatisticalResults'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaLisTestStatisticalResults", ShortCode = "ReaLisTestStatisticalResults", Desc = "")]
    public class ReaLisTestStatisticalResults : BaseEntity
    {
        #region Member Variables

        protected DateTime? _testDate;
        protected long? _testEquipID;
        protected string _testEquipCode;
        protected string _testEquipName;
        protected string _testEquipTypeCode;
        protected string _testEquipTypeName;
        protected string _testType;
        protected long? _testItemID;
        protected string _testItemCode;
        protected string _testItemCName;
        protected string _testItemSName;
        protected string _testItemEName;
        protected int _testCount;
        protected double _price;
        protected double _sumTotal;


        #endregion

        #region Constructors

        public ReaLisTestStatisticalResults() { }

        public ReaLisTestStatisticalResults(long labID, DateTime testDate, long testEquipID, string testEquipCode, string testEquipName, string testEquipTypeCode, string testEquipTypeName, string testType, long testItemID, string testItemCode, string testItemCName, string testItemSName, string testItemEName, int testCount, double price, double sumTotal, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._testDate = testDate;
            this._testEquipID = testEquipID;
            this._testEquipCode = testEquipCode;
            this._testEquipName = testEquipName;
            this._testEquipTypeCode = testEquipTypeCode;
            this._testEquipTypeName = testEquipTypeName;
            this._testType = testType;
            this._testItemID = testItemID;
            this._testItemCode = testItemCode;
            this._testItemCName = testItemCName;
            this._testItemSName = testItemSName;
            this._testItemEName = testItemEName;
            this._testCount = testCount;
            this._price = price;
            this._sumTotal = sumTotal;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检测日期", ShortCode = "TestDate", Desc = "检测日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TestDate
        {
            get { return _testDate; }
            set { _testDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "TestEquipID", ShortCode = "TestEquipID", Desc = "仪器Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? TestEquipID
        {
            get { return _testEquipID; }
            set { _testEquipID = value; }
        }

        [DataMember]
        [DataDesc(CName = "TestEquipCode", ShortCode = "TestEquipCode", Desc = "LIS仪器对照码", ContextType = SysDic.All, Length = 60)]
        public virtual string TestEquipCode
        {
            get { return _testEquipCode; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for TestEquipCode", value, value.ToString());
                _testEquipCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestEquipName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string TestEquipName
        {
            get { return _testEquipName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for TestEquipName", value, value.ToString());
                _testEquipName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestEquipTypeCode", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string TestEquipTypeCode
        {
            get { return _testEquipTypeCode; }
            set { _testEquipTypeCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestEquipTypeName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string TestEquipTypeName
        {
            get { return _testEquipTypeName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for TestEquipTypeName", value, value.ToString());
                _testEquipTypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "检验类型代码", ShortCode = "TestType", Desc = "检验类型代码", ContextType = SysDic.All, Length = 60)]
        public virtual string TestType
        {
            get { return _testType; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for TestType", value, value.ToString());
                _testType = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TestItemID", Desc = "检验项目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TestItemID
        {
            get { return _testItemID; }
            set { _testItemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验项目LIS对照码", ShortCode = "TestItemCode", Desc = "检验项目LIS对照码", ContextType = SysDic.All, Length = 60)]
        public virtual string TestItemCode
        {
            get { return _testItemCode; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for TestItemCode", value, value.ToString());
                _testItemCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestItemCName", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual string TestItemCName
        {
            get { return _testItemCName; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for TestItemCName", value, value.ToString());
                _testItemCName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestItemSName", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string TestItemSName
        {
            get { return _testItemSName; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for TestItemSName", value, value.ToString());
                _testItemSName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestItemEName", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string TestItemEName
        {
            get { return _testItemEName; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for TestItemEName", value, value.ToString());
                _testItemEName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int TestCount
        {
            get { return _testCount; }
            set { _testCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Price", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SumTotal", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double SumTotal
        {
            get { return _sumTotal; }
            set { _sumTotal = value; }
        }


        #endregion

        #region 自定义属性

        protected string _testTypeName;
        [DataMember]
        [DataDesc(CName = "LIS检测类型", ShortCode = "TestTypeName", Desc = "LIS检测类型", ContextType = SysDic.All, Length = 60)]
        public virtual string TestTypeName
        {
            get { return _testTypeName; }
            set
            {
                _testTypeName = value;
            }
        }
        protected string _startDate;
        [DataMember]
        [DataDesc(CName = "开始日期", ShortCode = "StartDate", Desc = "开始日期", ContextType = SysDic.All, Length = 20)]
        public virtual string StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
            }
        }
        protected string _endDate;
        [DataMember]
        [DataDesc(CName = "结束日期", ShortCode = "EndDate", Desc = "结束日期", ContextType = SysDic.All, Length = 20)]
        public virtual string EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
            }
        }
        #endregion
    }
    #endregion
}