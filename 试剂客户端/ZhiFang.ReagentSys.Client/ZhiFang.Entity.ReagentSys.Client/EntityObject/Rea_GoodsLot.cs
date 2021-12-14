using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaGoodsLot

    /// <summary>
    /// ReaGoodsLot object for NHibernate mapped table 'Rea_GoodsLot'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "货品批号表", ClassCName = "ReaGoodsLot", ShortCode = "ReaGoodsLot", Desc = "货品批号表")]
    public class ReaGoodsLot : BaseEntity
    {
        #region Member Variables
        protected string _reaGoodsNo;
        protected string _goodsCName;
        protected string _lotNo;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime? _dataUpdateTime;
        protected long? _goodsID;
        //protected ReaGoods _reaGoods;
        protected long? _FactoryID;
        protected string _FactoryName;
        protected bool? _isNeedPerformanceTest;
        protected long? _verificationStatus;
        protected long? _verificationUserId;
        protected string _verificationUserName;
        protected DateTime? _verificationTime;
        protected string _verificationContent;

        protected string _increaseAppearance;
        protected string _sterilityTest;
        protected string _parallelTest;
        protected string _growthTest;
        protected string _comparisonTest;
        protected string _verificationMemo;
        #endregion

        #region Constructors

        public ReaGoodsLot() { }

        public ReaGoodsLot(long labID, string goodsCName, string lotNo, DateTime prodDate, DateTime invalidDate, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long GoodsID, string increaseAppearance, string sterilityTest, string parallelTest, string comparisonTest, string growthTest)
        {
            this._labID = labID;
            this._goodsCName = goodsCName;
            this._lotNo = lotNo;
            this._prodDate = prodDate;
            this._invalidDate = invalidDate;
            this._dispOrder = dispOrder;
            this._memo = memo;
            this._visible = visible;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._goodsID = GoodsID;

            this._increaseAppearance = increaseAppearance;
            this._sterilityTest = sterilityTest;
            this._parallelTest = parallelTest;
            this._growthTest = growthTest;
            this._comparisonTest = comparisonTest;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "ComparisonTest", ShortCode = "ComparisonTest", Desc = "ComparisonTest", ContextType = SysDic.All, Length = 50)]
        public virtual string ComparisonTest
        {
            get { return _comparisonTest; }
            set { _comparisonTest = value; }
        }
        [DataMember]
        [DataDesc(CName = "IncreaseAppearance", ShortCode = "IncreaseAppearance", Desc = "IncreaseAppearance", ContextType = SysDic.All, Length = 50)]
        public virtual string IncreaseAppearance
        {
            get { return _increaseAppearance; }
            set { _increaseAppearance = value; }
        }
        [DataMember]
        [DataDesc(CName = "SterilityTest", ShortCode = "SterilityTest", Desc = "SterilityTest", ContextType = SysDic.All, Length = 50)]
        public virtual string SterilityTest
        {
            get { return _sterilityTest; }
            set { _sterilityTest = value; }
        }
        [DataMember]
        [DataDesc(CName = "ParallelTest", ShortCode = "ParallelTest", Desc = "ParallelTest", ContextType = SysDic.All, Length = 50)]
        public virtual string ParallelTest
        {
            get { return _parallelTest; }
            set { _parallelTest = value; }
        }
        [DataMember]
        [DataDesc(CName = "GrowthTest", ShortCode = "GrowthTest", Desc = "GrowthTest", ContextType = SysDic.All, Length = 50)]
        public virtual string GrowthTest
        {
            get { return _growthTest; }
            set { _growthTest = value; }
        }
        [DataMember]
        [DataDesc(CName = "货品内部编码", ShortCode = "ReaGoodsNo", Desc = "货品内部编码", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsNo
        {
            get { return _reaGoodsNo; }
            set { _reaGoodsNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "产品中文名", ShortCode = "GoodsCName", Desc = "产品中文名", ContextType = SysDic.All, Length = 200)]
        public virtual string GoodsCName
        {
            get { return _goodsCName; }
            set { _goodsCName = value; }
        }

        [DataMember]
        [DataDesc(CName = "批号", ShortCode = "LotNo", Desc = "批号", ContextType = SysDic.All, Length = 20)]
        public virtual string LotNo
        {
            get { return _lotNo; }
            set { _lotNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "生产日期", ShortCode = "ProdDate", Desc = "生产日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ProdDate
        {
            get { return _prodDate; }
            set { _prodDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "有效期", ShortCode = "InvalidDate", Desc = "有效期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InvalidDate
        {
            get { return _invalidDate; }
            set { _invalidDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者ID", ShortCode = "CreaterID", Desc = "创建者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreaterID
        {
            get { return _createrID; }
            set { _createrID = value; }
        }

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreaterName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
        {
            get { return _createrName; }
            set { _createrName = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品ID", ShortCode = "GoodsID", Desc = "货品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "厂家ID", ShortCode = "FactoryID", Desc = "厂家ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? FactoryID
        {
            get { return _FactoryID; }
            set { _FactoryID = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂家名称", ShortCode = "FactoryName", Desc = "厂家名称", ContextType = SysDic.All, Length = 50)]
        public virtual string FactoryName
        {
            get { return _FactoryName; }
            set { _FactoryName = value; }
        }
        [DataMember]
        [DataDesc(CName = "是否需要性能验证", ShortCode = "IsNeedPerformanceTest", Desc = "是否需要性能验证", ContextType = SysDic.All, Length = 1)]
        public virtual bool? IsNeedPerformanceTest
        {
            get { return _isNeedPerformanceTest; }
            set { _isNeedPerformanceTest = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品批号验证状态", ShortCode = "货品批号验证状态", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? VerificationStatus
        {
            get { return _verificationStatus; }
            set { _verificationStatus = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "验证人ID", ShortCode = "VerificationUserId", Desc = "验证人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? VerificationUserId
        {
            get { return _verificationUserId; }
            set { _verificationUserId = value; }
        }

        [DataMember]
        [DataDesc(CName = "验证人姓名", ShortCode = "VerificationUserName", Desc = "验证人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string VerificationUserName
        {
            get { return _verificationUserName; }
            set { _verificationUserName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "验证时间", ShortCode = "VerificationTime", Desc = "验证时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? VerificationTime
        {
            get { return _verificationTime; }
            set { _verificationTime = value; }
        }
        [DataMember]
        [DataDesc(CName = "验证内容", ShortCode = "VerificationContent", Desc = "验证内容", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string VerificationContent
        {
            get { return _verificationContent; }
            set { _verificationContent = value; }
        }
        #endregion

        #region 自定义属性
        protected string _verificationStatusCName;
        [DataMember]
        [DataDesc(CName = "验证状态名称", ShortCode = "VerificationStatusCName", Desc = "验证状态名称", ContextType = SysDic.All, Length = 20)]
        public virtual string VerificationStatusCName
        {
            get { return _verificationStatusCName; }
            set { _verificationStatusCName = value; }
        }
        [DataMember]
        [DataDesc(CName = "验证说明", ShortCode = "VerificationMemo", Desc = "验证说明", ContextType = SysDic.All, Length = 5000)]
        public virtual string VerificationMemo
        {
            get { return _verificationMemo; }
            set { _verificationMemo = value; }
        }
        #endregion
    }
    #endregion
}