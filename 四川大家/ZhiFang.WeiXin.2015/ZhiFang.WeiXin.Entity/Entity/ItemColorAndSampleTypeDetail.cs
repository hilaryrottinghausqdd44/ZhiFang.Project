using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
    #region ItemColorDict

    /// <summary>
    /// BAccountHospitalSearchContext object for NHibernate mapped table 'B_AccountHospitalSearchContext'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "采样管颜色样本类型关系", ClassCName = "ItemColorAndSampleTypeDetail", ShortCode = "ItemColorAndSampleTypeDetail", Desc = "采样管颜色样本类型关系")]
    public class ItemColorAndSampleTypeDetail : BaseEntity
    {
        #region Member Variables

        protected long? _ColorId;
        protected long? _SampleTypeNo;
        #endregion

        #region Constructors

        public ItemColorAndSampleTypeDetail() { }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "采样管颜色ID", ShortCode = "ColorId", Desc = "采样管颜色ID", ContextType = SysDic.All, Length = 20)]
        public virtual long? ColorId
        {
            get { return _ColorId; }
            set
            {
                _ColorId = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "样本类型号", ShortCode = "SampleTypeNo", Desc = "样本类型号", ContextType = SysDic.All, Length = 20)]
        public virtual long? SampleTypeNo
        {
            get { return _SampleTypeNo; }
            set
            {
                _SampleTypeNo = value;
            }
        }
        #endregion
    }
    #endregion
}