using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBParItemSplit

    /// <summary>
    /// LBParItemSplit object for NHibernate mapped table 'LB_ParItemSplit'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "组合项目拆分", ClassCName = "LBParItemSplit", ShortCode = "LBParItemSplit", Desc = "组合项目拆分")]
    public class LBParItemSplit : BaseEntity
    {
        #region Member Variables

        protected int _newBarCode;
        protected bool _isAutoUnion;
        protected DateTime? _dataUpdateTime;
        protected LBItem _lBItem;
        protected LBItem _parItem;
        protected LBSamplingGroup _lBSamplingGroup;


        #endregion

        #region Constructors

        public LBParItemSplit() { }

        public LBParItemSplit(int newBarCode, bool isAutoUnion, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBItem lBItem, LBItem parItem, LBSamplingGroup lBSamplingGroup)
        {
            this._newBarCode = newBarCode;
            this._isAutoUnion = isAutoUnion;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBItem = lBItem;
            this._parItem = parItem;
            this._lBSamplingGroup = lBSamplingGroup;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "NewBarCode", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int NewBarCode
        {
            get { return _newBarCode; }
            set { _newBarCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsAutoUnion", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsAutoUnion
        {
            get { return _isAutoUnion; }
            set { _isAutoUnion = value; }
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
        [DataDesc(CName = "", ShortCode = "Par", Desc = "")]
        public virtual LBItem ParItem
        {
            get { return _parItem; }
            set { _parItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样组", ShortCode = "LBSamplingGroup", Desc = "采样组")]
        public virtual LBSamplingGroup LBSamplingGroup
        {
            get { return _lBSamplingGroup; }
            set { _lBSamplingGroup = value; }
        }

        #endregion

        #region 自定义属性
        [DataMember]
        [DataDesc(CName = "新增/编辑组合项目拆分关系时,某一项目在采样组项目关系的所属采样组集合信息(JArray的字符串形式)", ShortCode = "LBSamplingGroupListStr", Desc = "新增/编辑组合项目拆分关系时,某一项目在采样组项目关系的所属采样组集合信息(JArray的字符串形式)")]
        public virtual string LBSamplingGroupListStr { get; set; }
        #endregion

    }
    #endregion
}