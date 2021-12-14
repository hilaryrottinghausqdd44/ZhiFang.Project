using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LisOperateASection

    /// <summary>
    /// LisOperateASection object for NHibernate mapped table 'Lis_OperateASection'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LisOperateASection", ShortCode = "LisOperateASection", Desc = "")]
    public class LisOperateASection : BaseEntity
    {
        #region Member Variables

        protected LisOperateAuthorize _lisOperateAuthorize;
        protected LBSection _lBSection;


        #endregion

        #region Constructors

        public LisOperateASection() { }

        public LisOperateASection(DateTime dataAddTime, LisOperateAuthorize lisOperateAuthorize, LBSection lBSection)
        {
            this._dataAddTime = dataAddTime;
            this._lisOperateAuthorize = lisOperateAuthorize;
            this._lBSection = lBSection;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisOperateAuthorize", Desc = "")]
        public virtual LisOperateAuthorize LisOperateAuthorize
        {
            get { return _lisOperateAuthorize; }
            set { _lisOperateAuthorize = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBSection", Desc = "")]
        public virtual LBSection LBSection
        {
            get { return _lBSection; }
            set { _lBSection = value; }
        }


        #endregion
    }
    #endregion
}