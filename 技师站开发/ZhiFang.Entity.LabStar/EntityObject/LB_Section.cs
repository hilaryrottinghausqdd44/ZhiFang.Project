using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBSection

    /// <summary>
    /// LBSection object for NHibernate mapped table 'LB_Section'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "检验小组", ClassCName = "LBSection", ShortCode = "LBSection", Desc = "检验小组")]
    public class LBSection : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected long _execDeptID;
        protected long _superSectionID;
        protected bool _isVirtualGroup;
        protected string _sectionFun;
        protected long? _sectionTypeID;
        protected string _sectionType;
        protected string _proDLL;
        protected bool _isImage;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected int _sectionNo;
        protected LBSpecialty _lBSpecialty;

        #endregion

        #region Constructors

        public LBSection() { }

        public LBSection(string cName, string eName, string sName, long execDeptID, long superSectionID, bool isVirtualGroup, string sectionFun, long sectionTypeID, string sectionType, string proDLL, bool isImage, string useCode, string standCode, string deveCode, string shortcode, string pinYinZiTou, string comment, bool isUse, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, int sectionNo, LBSpecialty lBSpecialty)
        {
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
            this._execDeptID = execDeptID;
            this._superSectionID = superSectionID;
            this._isVirtualGroup = isVirtualGroup;
            this._sectionFun = sectionFun;
            this._sectionTypeID = sectionTypeID;
            this._sectionType = sectionType;
            this._proDLL = proDLL;
            this._isImage = isImage;
            this._useCode = useCode;
            this._standCode = standCode;
            this._deveCode = deveCode;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._comment = comment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._sectionNo = sectionNo;
            this._lBSpecialty = lBSpecialty;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
        {
            get { return _eName; }
            set { _eName = value; }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
        {
            get { return _sName; }
            set { _sName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验执行科室ID", ShortCode = "ExecDeptID", Desc = "检验执行科室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ExecDeptID
        {
            get { return _execDeptID; }
            set { _execDeptID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验大组ID", ShortCode = "SuperSectionID", Desc = "检验大组ID", ContextType = SysDic.All, Length = 8)]
        public virtual long SuperSectionID
        {
            get { return _superSectionID; }
            set { _superSectionID = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否虚拟小组", ShortCode = "IsVirtualGroup", Desc = "是否虚拟小组", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsVirtualGroup
        {
            get { return _isVirtualGroup; }
            set { _isVirtualGroup = value; }
        }

        [DataMember]
        [DataDesc(CName = "专业功能类型", ShortCode = "SectionFun", Desc = "专业功能类型：通用， 微生物，细胞学，病理，酶免", ContextType = SysDic.All, Length = 50)]
        public virtual string SectionFun
        {
            get { return _sectionFun; }
            set { _sectionFun = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组类型ID", ShortCode = "SectionTypeID", Desc = "小组类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SectionTypeID
        {
            get { return _sectionTypeID; }
            set { _sectionTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组类型名称", ShortCode = "SectionType", Desc = "小组类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string SectionType
        {
            get { return _sectionType; }
            set { _sectionType = value; }
        }

        [DataMember]
        [DataDesc(CName = "专业编辑", ShortCode = "ProDLL", Desc = "专业编辑", ContextType = SysDic.All, Length = 200)]
        public virtual string ProDLL
        {
            get { return _proDLL; }
            set { _proDLL = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否图形结果", ShortCode = "IsImage", Desc = "是否图形结果", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsImage
        {
            get { return _isImage; }
            set { _isImage = value; }
        }

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
        {
            get { return _useCode; }
            set { _useCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
        {
            get { return _standCode; }
            set { _standCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "开发商标准代码", ShortCode = "DeveCode", Desc = "开发商标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string DeveCode
        {
            get { return _deveCode; }
            set { _deveCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 50)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set { _pinYinZiTou = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
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
        [DataDesc(CName = "小组编码", ShortCode = "SectionNo", Desc = "小组编码", ContextType = SysDic.All, Length = 4)]
        public virtual int SectionNo
        {
            get { return _sectionNo; }
            set { _sectionNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBSpecialty", Desc = "")]
        public virtual LBSpecialty LBSpecialty
        {
            get { return _lBSpecialty; }
            set { _lBSpecialty = value; }
        }

        #endregion
    }
    #endregion
}