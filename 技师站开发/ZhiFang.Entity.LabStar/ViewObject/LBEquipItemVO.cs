using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBEquipItemVO

    /// <summary>
    /// LBEquipItem object for NHibernate mapped table 'LB_EquipItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBEquipItem", ShortCode = "LBEquipItem", Desc = "")]
    public class LBEquipItemVO : BaseEntity
    {
        #region Member Variables

        protected LBEquipItem _lBEquipItem;
        protected LBEquip _lBEquip;
        protected LBItem _lBItem;

        #endregion

        #region Constructors

        public LBEquipItemVO() { }

        public LBEquipItemVO(LBEquipItem lBEquipItem, LBEquip lBEquip, LBItem lBItem)
        {
            this._lBEquipItem = lBEquipItem;
            this._lBEquip = lBEquip;
            this._lBItem = lBItem;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBEquipItem", Desc = "")]
        public virtual LBEquipItem LBEquipItem
        {
            get { return _lBEquipItem; }
            set { _lBEquipItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBEquip", Desc = "")]
        public virtual LBEquip LBEquip
        {
            get { return _lBEquip; }
            set { _lBEquip = value; }
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
}