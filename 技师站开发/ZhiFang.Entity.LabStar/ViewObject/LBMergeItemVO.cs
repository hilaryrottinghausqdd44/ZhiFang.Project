using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBMergeItemVO

    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBMergeItemVO", ShortCode = "LBMergeItemVO", Desc = "")]
    public class LBMergeItemVO : BaseEntity
    {
        #region Member Variables

        protected long _parItemID;
        protected string _parItemName;
        protected long _changeItemID;
        protected string _changeItemName;
        protected long _isMerge;
        protected LBItem _lbChangeItem;
        protected LisTestItem _lisTestItem;

        #endregion

        #region Constructors

        public LBMergeItemVO() { }

        public LBMergeItemVO(LisTestItem lisTestItem)
        {
            this._lisTestItem = lisTestItem;
        }

        public LBMergeItemVO(long parItemID, string parItemName, long changeItemID, string changeItemName, long isMerge, LisTestItem lisTestItem, LBItem lbChangeItem)
        {
            this._parItemID = parItemID;
            this._parItemName = parItemName;
            this._changeItemID = changeItemID;
            this._changeItemName = changeItemName;
            this._isMerge = isMerge;
            this._lbChangeItem = lbChangeItem;
            this._lisTestItem = lisTestItem;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PItemID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ParItemID
        {
            get { return _parItemID; }
            set { _parItemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PItemName", Desc = "")]
        public virtual string ParItemName
        {
            get { return _parItemName; }
            set { _parItemName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChangeItemID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ChangeItemID
        {
            get { return _changeItemID; }
            set { _changeItemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChangeItemName", Desc = "")]
        public virtual string ChangeItemName
        {
            get { return _changeItemName; }
            set { _changeItemName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChangeItemDispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ChangeItemDispOrder { get; set; }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsMerge", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long IsMerge
        {
            get { return _isMerge; }
            set { _isMerge = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBChangeItem", Desc = "")]
        public virtual LBItem LBChangeItem
        {
            get { return _lbChangeItem; }
            set { _lbChangeItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisTestItem", Desc = "")]
        public virtual LisTestItem LisTestItem
        {
            get { return _lisTestItem; }
            set { _lisTestItem = value; }
        }

        #endregion
    }
    #endregion
}