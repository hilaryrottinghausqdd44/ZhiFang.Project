using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
    #region BParameter

    /// <summary>
    /// BParameter object for NHibernate mapped table 'B_Parameter'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "参数表", ClassCName = "BParameter", ShortCode = "BParameter", Desc = "参数表")]
    public class BParameter : BaseEntity
    {
        #region Member Variables

        protected long? _nodeID;
        protected long? _groupNo;
        protected string _name;
        protected string _sName;
        protected string _paraNo;
        protected string _paraType;
        protected string _paraValue;
        protected string _paraDesc;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _itemEditInfo;

        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected int _dispOrder;
        //protected BDict _bDict;

        #endregion

        #region Constructors

        public BParameter() { }

        public BParameter(long labID, long nodeID, long groupNo, string name, string sName, string paraNo, string paraType, string paraValue, string paraDesc, string shortcode, string pinYinZiTou, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, int dispOrder)
        {
            this._labID = labID;
            this._nodeID = nodeID;
            this._groupNo = groupNo;
            this._name = name;
            this._sName = sName;
            this._paraNo = paraNo;
            this._paraType = paraType;
            this._paraValue = paraValue;
            this._paraDesc = paraDesc;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._dispOrder = dispOrder;
            //this._bDict = pDictId;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "辅助录入信息", ShortCode = "ItemEditInfo", Desc = "辅助录入信息", ContextType = SysDic.All, Length = 5000)]
        public virtual string ItemEditInfo
        {
            get { return _itemEditInfo; }
            set
            {
                _itemEditInfo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "站点ID", ShortCode = "NodeID", Desc = "站点ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? NodeID
        {
            get { return _nodeID; }
            set { _nodeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GroupNo", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GroupNo
        {
            get { return _groupNo; }
            set { _groupNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 200)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
                _name = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 40)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                _sName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ParaNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ParaNo
        {
            get { return _paraNo; }
            set
            {
                _paraNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "参数类型", ShortCode = "ParaType", Desc = "参数类型", ContextType = SysDic.All, Length = 100)]
        public virtual string ParaType
        {
            get { return _paraType; }
            set
            {
                _paraType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "参数值", ShortCode = "ParaValue", Desc = "参数值", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ParaValue
        {
            get { return _paraValue; }
            set
            {
                _paraValue = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "参数说明", ShortCode = "ParaDesc", Desc = "参数说明", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ParaDesc
        {
            get { return _paraDesc; }
            set
            {
                _paraDesc = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
                _shortcode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
                _pinYinZiTou = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
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
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        //[DataMember]
        //[DataDesc(CName = "字典", ShortCode = "BDict", Desc = "字典")]
        //public virtual BDict BDict
        //{
        //    get { return _bDict; }
        //    set { _bDict = value; }
        //}


        #endregion
    }
    #endregion
}