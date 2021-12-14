using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodUseDesc

    /// <summary>
    /// BloodUseDesc object for NHibernate mapped table 'BloodUseDesc'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodUseDesc", ShortCode = "BloodUseDesc", Desc = "")]
    public class BloodUseDesc : BaseEntityService
    {
        #region Member Variables

        protected string _versionNo;
        protected string _contents;
        protected int _dispOrder;
        protected bool _visible;


        #endregion

        #region Constructors

        public BloodUseDesc() { }

        public BloodUseDesc(string _versionNo, string _contents, int dispOrder, bool visible, long labID, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._versionNo = _versionNo;
            this._contents = _contents;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "VersionNo", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string VersionNo
        {
            get { return _versionNo; }
            set
            {
                _versionNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Contents", Desc = "", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Contents
        {
            get { return _contents; }
            set
            {
                _contents = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set
            {
                _dispOrder = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
            }
        }

        #endregion
    }
    #endregion
}