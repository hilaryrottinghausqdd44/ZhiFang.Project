using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BDictType

    /// <summary>
    /// BDictType object for NHibernate mapped table 'B_DictType'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "字典类别", ClassCName = "BDictType", ShortCode = "BDictType", Desc = "字典类别")]
    public class BDictType : BaseEntity
    {
        #region Member Variables

        protected string _cname;       
        protected int _dispOrder;
        protected string _memo;
        protected bool _isUse;
        protected IList<BDict> _bDictList;
        protected string _dictTypeCode;

        #endregion

        #region Constructors

        public BDictType() { }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 40)]
        public virtual string CName
        {
            get { return _cname; }
            set
            {
                _cname = value;
            }
        }        

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典", ShortCode = "BDictList", Desc = "字典")]
        public virtual IList<BDict> BDictList
        {
            get
            {
                if (_bDictList == null)
                {
                    _bDictList = new List<BDict>();
                }
                return _bDictList;
            }
            set { _bDictList = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典类型编号", ShortCode = "DictTypeCode", Desc = "字典类型编号", ContextType = SysDic.All, Length = 100)]
        public virtual string DictTypeCode
        {
            get { return _dictTypeCode; }
            set { _dictTypeCode = value; }
        }

        #endregion
    }
    #endregion
}