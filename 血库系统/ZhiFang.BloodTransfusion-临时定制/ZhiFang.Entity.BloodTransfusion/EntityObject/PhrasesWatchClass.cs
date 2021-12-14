using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region PhrasesWatchClass

    /// <summary>
    /// PhrasesWatchClass object for NHibernate mapped table 'PhrasesWatchClass'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "PhrasesWatchClass", ShortCode = "PhrasesWatchClass", Desc = "")]
    public class PhrasesWatchClass : BaseEntityService
    {
        #region Member Variables

        protected long _qIndicatorTypeId;
        protected string _qIndicatorTypeCName;
        protected long _parentID;
        protected string _cName;
        protected string _eName;
        protected string _shortCode;
        protected int _visible;
        protected string _dispOrder;
        protected string _memo;


        #endregion

        #region Constructors

        public PhrasesWatchClass() { }

        public PhrasesWatchClass(long parentID, string cName, string eName, string shortCode, int visible, string dispOrder, string memo)
        {
            this._parentID = parentID;
            this._cName = cName;
            this._eName = eName;
            this._shortCode = shortCode;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._memo = memo;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "QIndicatorTypeId", ShortCode = "QIndicatorTypeId", Desc = "QIndicatorTypeId", ContextType = SysDic.All, Length = 8)]
        public virtual long QIndicatorTypeId
        {
            get { return _qIndicatorTypeId; }
            set { _qIndicatorTypeId = value; }
        }

        [DataMember]
        [DataDesc(CName = "QIndicatorTypeCName", ShortCode = "QIndicatorTypeCName", Desc = "QIndicatorTypeCName", ContextType = SysDic.All, Length = 80)]
        public virtual string QIndicatorTypeCName
        {
            get { return _qIndicatorTypeCName; }
            set
            {
                if (value != null && value.Length > 80)
                    throw new ArgumentOutOfRangeException("Invalid value for QIndicatorTypeCName", value, value.ToString());
                _qIndicatorTypeCName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ParentID", ShortCode = "ParentID", Desc = "ParentID", ContextType = SysDic.All, Length = 8)]
        public virtual long ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
                _eName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
                _shortCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string DispOrder
        {
            get { return _dispOrder; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DispOrder", value, value.ToString());
                _dispOrder = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
            }
        }


        #endregion
    }
    #endregion
}