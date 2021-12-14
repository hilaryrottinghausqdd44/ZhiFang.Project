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
    [DataDesc(CName = "采样管颜色", ClassCName = "ItemColorDict", ShortCode = "ItemColorDict", Desc = "采样管颜色")]
    public class ItemColorDict : BaseEntity
    {
        #region Member Variables

        protected string _ColorName;
        protected string _ColorValue;
        #endregion

        #region Constructors

        public ItemColorDict() { }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "采样管颜色名称", ShortCode = "ColorName", Desc = "采样管颜色名称", ContextType = SysDic.All, Length = 20)]
        public virtual string ColorName
        {
            get { return _ColorName; }
            set
            {
                _ColorName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "采样管颜色值", ShortCode = "ColorValue", Desc = "采样管颜色值", ContextType = SysDic.All, Length = 20)]
        public virtual string ColorValue
        {
            get { return _ColorValue; }
            set
            {
                _ColorValue = value;
            }
        }
        #endregion
    }
    #endregion
}