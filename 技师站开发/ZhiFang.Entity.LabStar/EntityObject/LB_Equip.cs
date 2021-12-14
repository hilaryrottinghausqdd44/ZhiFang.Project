using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBEquip

    /// <summary>
    /// LBEquip object for NHibernate mapped table 'LB_Equip'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "检验仪器", ClassCName = "LBEquip", ShortCode = "LBEquip", Desc = "检验仪器")]
    public class LBEquip : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected string _computer;
        protected string _comProgram;
        protected int _doubleFlag;
        protected string _equipResultType;
        protected string _commInfo;
        protected string _commPara;
        protected string _licenceKey;
        protected string _licenceType;
        protected string _sQH;
        protected DateTime? _licenceDate;
        protected bool _isUse;
        protected int _dispOrder;
        protected string _sampleNoStart;
        protected string _sampleNoEnd;
        protected DateTime? _dataUpdateTime;
        protected string _commSys;
        protected int _equipNo;
        protected long _SpecialtyID;
        protected LBSection _lBSection;


        #endregion

        #region Constructors

        public LBEquip() { }

        public LBEquip(string cName, string eName, string sName, string useCode, string standCode, string deveCode, string shortcode, string pinYinZiTou, string comment, string computer, string comProgram, int doubleFlag, string equipResultType, string commInfo, string commPara, string licenceKey, string licenceType, string sQH, DateTime licenceDate, bool isUse, int dispOrder, string sampleNoStart, string sampleNoEnd, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string commSys, int equipNo, long specialtyID, LBSection lBSection)
        {
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
            this._useCode = useCode;
            this._standCode = standCode;
            this._deveCode = deveCode;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._comment = comment;
            this._computer = computer;
            this._comProgram = comProgram;
            this._doubleFlag = doubleFlag;
            this._equipResultType = equipResultType;
            this._commInfo = commInfo;
            this._commPara = commPara;
            this._licenceKey = licenceKey;
            this._licenceType = licenceType;
            this._sQH = sQH;
            this._licenceDate = licenceDate;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._sampleNoStart = sampleNoStart;
            this._sampleNoEnd = sampleNoEnd;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._commSys = commSys;
            this._equipNo = equipNo;
            this._lBSection = lBSection;
            this._SpecialtyID = specialtyID;
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
        [DataDesc(CName = "计算机", ShortCode = "Computer", Desc = "计算机", ContextType = SysDic.All, Length = 50)]
        public virtual string Computer
        {
            get { return _computer; }
            set { _computer = value; }
        }

        [DataMember]
        [DataDesc(CName = "程序名", ShortCode = "ComProgram", Desc = "程序名", ContextType = SysDic.All, Length = 50)]
        public virtual string ComProgram
        {
            get { return _comProgram; }
            set { _comProgram = value; }
        }

        [DataMember]
        [DataDesc(CName = "双向通讯0:否 1：双向 2：自动双向", ShortCode = "DoubleFlag", Desc = "双向通讯0:否 1：双向 2：自动双向", ContextType = SysDic.All, Length = 4)]
        public virtual int DoubleFlag
        {
            get { return _doubleFlag; }
            set { _doubleFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "枚举，仪器类型-常规，酶标仪，微生物鉴定，微生物药敏", ShortCode = "EquipResultType", Desc = "枚举，仪器类型-常规，酶标仪，微生物鉴定，微生物药敏", ContextType = SysDic.All, Length = 50)]
        public virtual string EquipResultType
        {
            get { return _equipResultType; }
            set { _equipResultType = value; }
        }

        [DataMember]
        [DataDesc(CName = "通讯设置 串口网口等设置信息，josn字符串", ShortCode = "CommInfo", Desc = "通讯设置 串口网口等设置信息，josn字符串", ContextType = SysDic.All, Length = 16)]
        public virtual string CommInfo
        {
            get { return _commInfo; }
            set { _commInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "通讯控制参数，josn字符串", ShortCode = "CommPara", Desc = "通讯控制参数，josn字符串", ContextType = SysDic.All, Length = 16)]
        public virtual string CommPara
        {
            get { return _commPara; }
            set { _commPara = value; }
        }

        [DataMember]
        [DataDesc(CName = "授权码", ShortCode = "LicenceKey", Desc = "授权码", ContextType = SysDic.All, Length = 50)]
        public virtual string LicenceKey
        {
            get { return _licenceKey; }
            set { _licenceKey = value; }
        }

        [DataMember]
        [DataDesc(CName = "授权类型", ShortCode = "LicenceType", Desc = "授权类型", ContextType = SysDic.All, Length = 50)]
        public virtual string LicenceType
        {
            get { return _licenceType; }
            set { _licenceType = value; }
        }

        [DataMember]
        [DataDesc(CName = "授权号", ShortCode = "SQH", Desc = "授权号", ContextType = SysDic.All, Length = 50)]
        public virtual string SQH
        {
            get { return _sQH; }
            set { _sQH = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "授权日期", ShortCode = "LicenceDate", Desc = "授权日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LicenceDate
        {
            get { return _licenceDate; }
            set { _licenceDate = value; }
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
        [DataDesc(CName = "通讯开始样本号", ShortCode = "SampleNoStart", Desc = "通讯开始样本号", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleNoStart
        {
            get { return _sampleNoStart; }
            set { _sampleNoStart = value; }
        }

        [DataMember]
        [DataDesc(CName = "通讯截止样本号", ShortCode = "SampleNoEnd", Desc = "通讯截止样本号", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleNoEnd
        {
            get { return _sampleNoEnd; }
            set { _sampleNoEnd = value; }
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
        [DataDesc(CName = "通讯解释参数，josn字符串", ShortCode = "CommSys", Desc = "通讯解释参数，josn字符串", ContextType = SysDic.All, Length = 16)]
        public virtual string CommSys
        {
            get { return _commSys; }
            set { _commSys = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器编码", ShortCode = "EquipNo", Desc = "仪器编码", ContextType = SysDic.All, Length = 4)]
        public virtual int EquipNo
        {
            get { return _equipNo; }
            set { _equipNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "专业ID", ShortCode = "SpecialtyID", Desc = "专业", ContextType = SysDic.All, Length = 4)]
        public virtual long SpecialtyID
        {
            get { return _SpecialtyID; }
            set { _SpecialtyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验小组", ShortCode = "LBSection", Desc = "检验小组")]
        public virtual LBSection LBSection
        {
            get { return _lBSection; }
            set { _lBSection = value; }
        }

        #endregion
    }
    #endregion
}