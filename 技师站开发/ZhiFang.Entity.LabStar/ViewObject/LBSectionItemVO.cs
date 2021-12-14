using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBSectionItemVO

    /// <summary>
    /// LBSectionItem object for NHibernate mapped table 'LB_SectionItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBSectionItem", ShortCode = "LBSectionItem", Desc = "")]
    public class LBSectionItemVO : BaseEntity
    {
        #region Member Variables

        protected LBSectionItem _lBSectionItem;
        protected LBSection _lBSection;
        protected LBItem _lBItem;

        #endregion

        #region Constructors

        public LBSectionItemVO() { }

        public LBSectionItemVO(LBSectionItem lBSectionItem, LBSection lBSection, LBItem lBItem)
        {
            this._lBSectionItem = lBSectionItem;
            this._lBSection = lBSection;
            this._lBItem = lBItem;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBSection", Desc = "")]
        public virtual LBSectionItem LBSectionItem
        {
            get { return _lBSectionItem; }
            set { _lBSectionItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBSection", Desc = "")]
        public virtual LBSection LBSection
        {
            get { return _lBSection; }
            set { _lBSection = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBItem", Desc = "")]
        public virtual LBItem LBItem
        {
            get { return _lBItem; }
            set { _lBItem = value; }
        }

        #endregion
    }
    #endregion


    #region LBSectionSingleItemVO

    [DataContract]
    [DataDesc(CName = "小组项目单项VO", ClassCName = "LBSectionItem", ShortCode = "LBSectionItem", Desc = "")]
    public class LBSectionSingleItemVO : LBSectionItemVO
    {
        #region Member Variables

        protected LBItem _lBParItem;

        #endregion

        #region Constructors

        public LBSectionSingleItemVO() { }

        public LBSectionSingleItemVO(LBItem lBParItem)
        {
            this._lBParItem = lBParItem;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBItem", Desc = "")]
        public virtual LBItem LBParItem
        {
            get { return _lBParItem; }
            set { _lBParItem = value; }
        }

        #endregion
    }
    #endregion
}