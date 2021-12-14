using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region BEquip

    /// <summary>
    /// BEquip object for NHibernate mapped table 'B_Equip'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "仪器表", ClassCName = "BEquip", ShortCode = "BEquip", Desc = "仪器表")]
    public class BEquip : BusinessBase
    {
        #region Member Variables

        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _useCode;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected string _equipversion;
        protected string _memo;
        protected string _content;
        protected string _fullCName;
        protected PDict _equipFactoryBrand;
        protected PDict _equipType;
        protected IList<PGMProgram> _pGMProgramList;

        #endregion

        #region Constructors

        public BEquip() { }

        public BEquip(long labID, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string useCode, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string equipversion, string memo, string content, string fullCName, PDict equipFactoryBrand, PDict equipType)
        {
            this._labID = labID;
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._useCode = useCode;
            this._comment = comment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._equipversion = equipversion;
            this._memo = memo;
            this._content = content;
            this._fullCName = fullCName;
            this._equipFactoryBrand = equipFactoryBrand;
            this._equipType = equipType;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 500)]
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
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 500)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
                _eName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 500)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
                _sName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "SQH", ShortCode = "Shortcode", Desc = "SQH", ContextType = SysDic.All, Length = 500)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
                _shortcode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 500)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
                _pinYinZiTou = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 500)]
        public virtual string UseCode
        {
            get { return _useCode; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
                _useCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 4000)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                if (value != null && value.Length > 4000)
                    throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
                _comment = value;
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器型号", ShortCode = "Equipversion", Desc = "仪器型号", ContextType = SysDic.All, Length = 500)]
        public virtual string Equipversion
        {
            get { return _equipversion; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Equipversion", value, value.ToString());
                _equipversion = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "概要说明", ShortCode = "Memo", Desc = "概要说明", ContextType = SysDic.All, Length =Int32.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 1002400)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "详细说明", ShortCode = "Content", Desc = "详细说明", ContextType = SysDic.All, Length =Int32.MaxValue)]
        public virtual string Content
        {
            get { return _content; }
            set
            {
                if (value != null && value.Length > 1002400)
                    throw new ArgumentOutOfRangeException("Invalid value for Content", value, value.ToString());
                _content = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "全称", ShortCode = "FullCName", Desc = "全称", ContextType = SysDic.All, Length = 500)]
        public virtual string FullCName
        {
            get { return _fullCName; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for FullCName", value, value.ToString());
                _fullCName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "仪器品牌", ShortCode = "EquipFactoryBrand", Desc = "仪器品牌")]
        public virtual PDict EquipFactoryBrand
        {
            get { return _equipFactoryBrand; }
            set { _equipFactoryBrand = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器分类", ShortCode = "EquipType", Desc = "仪器分类")]
        public virtual PDict EquipType
        {
            get { return _equipType; }
            set { _equipType = value; }
        }

        [DataMember]
        [DataDesc(CName = "程序列表，Status 状态（1、暂存；2，待审核；3、发布）。", ShortCode = "PGMProgramList", Desc = "程序列表，Status 状态（1、暂存；2，待审核；3、发布）。")]
        public virtual IList<PGMProgram> PGMProgramList
        {
            get
            {
                if (_pGMProgramList == null)
                {
                    _pGMProgramList = new List<PGMProgram>();
                }
                return _pGMProgramList;
            }
            set { _pGMProgramList = value; }
        }


        #endregion
    }
    #endregion
}