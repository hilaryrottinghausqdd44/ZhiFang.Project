using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region BMicroGroupAntiClass

    /// <summary>
    /// BMicroGroupAntiClass object for NHibernate mapped table 'B_MicroGroupAntiClass'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "微生物分组与抗生素类型关系", ClassCName = "BMicroGroupAntiClass", ShortCode = "BMicroGroupAntiClass", Desc = "微生物分组与抗生素类型关系")]
    public class BMicroGroupAntiClass : BaseEntity
    {
        #region Member Variables

        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected BAnti _bAnti;
        protected BAntiClassType _bAntiClassType;
        protected BMicro _bMicro;
        protected BMicroAntiClass _bMicroAntiClass;

        #endregion

        #region Constructors

        public BMicroGroupAntiClass() { }

        public BMicroGroupAntiClass(long labID, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BAnti bAnti, BAntiClassType bAntiClassType, BMicro bMicro, BMicroAntiClass bMicroAntiClass)
        {
            this._labID = labID;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bAnti = bAnti;
            this._bAntiClassType = bAntiClassType;
            this._bMicro = bMicro;
            this._bMicroAntiClass = bMicroAntiClass;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
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
        [DataDesc(CName = "抗生素", ShortCode = "BAnti", Desc = "抗生素")]
        public virtual BAnti BAnti
        {
            get { return _bAnti; }
            set { _bAnti = value; }
        }

        [DataMember]
        [DataDesc(CName = "抗生素分类类型", ShortCode = "BAntiClassType", Desc = "抗生素分类类型")]
        public virtual BAntiClassType BAntiClassType
        {
            get { return _bAntiClassType; }
            set { _bAntiClassType = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物", ShortCode = "BMicro", Desc = "微生物")]
        public virtual BMicro BMicro
        {
            get { return _bMicro; }
            set { _bMicro = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物抗生素检测类型", ShortCode = "BMicroAntiClass", Desc = "微生物抗生素检测类型")]
        public virtual BMicroAntiClass BMicroAntiClass
        {
            get { return _bMicroAntiClass; }
            set { _bMicroAntiClass = value; }
        }


        #endregion
    }
    #endregion
}