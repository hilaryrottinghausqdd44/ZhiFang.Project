using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBMergeItemFormVO

    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBMergeItemFormVO", ShortCode = "LBMergeItemFormVO", Desc = "")]
    public class LBMergeItemFormVO : BaseEntity
    {
        #region Member Variables

        protected string _patNo;
        protected string _cName;
        protected long _isMerge;
        protected long _itemCount;

        #endregion

        #region Constructors

        public LBMergeItemFormVO() { }

        public LBMergeItemFormVO(string patNo, string cName, long isMerge, long itemCount)
        {
            this._patNo = patNo;
            this._cName = cName;
            this._isMerge = isMerge;
            this._itemCount = itemCount;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatNo", Desc = "")]
        public virtual string PatNo
        {
            get { return _patNo; }
            set { _patNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "")]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsMerge", Desc = "")]
        public virtual long IsMerge
        {
            get { return _isMerge; }
            set { _isMerge = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemCount", Desc = "")]
        public virtual long ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; }
        }

        #endregion
    }
    #endregion
}