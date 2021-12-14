using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBItem

    /// <summary>
    /// LBItem object for NHibernate mapped table 'LB_Item'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "检验项目", ClassCName = "LBItem", ShortCode = "LBItem", Desc = "检验项目")]
    public class LBItem : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _diagMethod;
        protected string _unit;
        protected int _valueType;
        protected string _samplingRequire;
        protected string _clinicalInfo;
        protected decimal _itemCharge;
        protected int _prec;
        protected bool _isOrderItem;
        protected int _groupType;
        protected bool _isSampleItem;
        protected bool _isCalcItem;
        protected bool _isChargeItem;
        protected bool _isUnionItem;
        protected bool _isPrint;
        protected bool _isPartItem;
        protected int _secretFlag;
        protected int _hisCompType;
        protected double _hisCompH;
        protected double _hisCompHH;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected long? _specialtyID;
        protected int _hintFlag;
        protected double _fWorkLoad;
        protected string _sectionFun;
        protected int _itemNo;
        protected string _defaultRange;
        protected string _rangeAllInfo;
        protected int _collectSort;
        protected int _specialType;

        #endregion

        #region Constructors

        public LBItem() { }

        public LBItem(string cName, string eName, string sName, string diagMethod, string unit, int valueType, string samplingRequire, string clinicalInfo, decimal itemCharge, int prec, bool isOrderItem, int groupType, bool isSampleItem, bool isCalcItem, bool isChargeItem, bool isUnionItem, bool isPrint, bool isPartItem, int secretFlag, int hisCompType, double hisCompH, double hisCompHH, string useCode, string standCode, string deveCode, string shortcode, string pinYinZiTou, string comment, bool isUse, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long specialtyID, int hintFlag, double fWorkLoad, string sectionFun, int itemNo, string defaultRange, string rangeAllInfo, int collectSort, int specialType)
        {
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
            this._diagMethod = diagMethod;
            this._unit = unit;
            this._valueType = valueType;
            this._samplingRequire = samplingRequire;
            this._clinicalInfo = clinicalInfo;
            this._itemCharge = itemCharge;
            this._prec = prec;
            this._isOrderItem = isOrderItem;
            this._groupType = groupType;
            this._isSampleItem = isSampleItem;
            this._isCalcItem = isCalcItem;
            this._isChargeItem = isChargeItem;
            this._isUnionItem = isUnionItem;
            this._isPrint = isPrint;
            this._isPartItem = isPartItem;
            this._secretFlag = secretFlag;
            this._hisCompType = hisCompType;
            this._hisCompH = hisCompH;
            this._hisCompHH = hisCompHH;
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
            this._specialtyID = specialtyID;
            this._hintFlag = hintFlag;
            this._fWorkLoad = fWorkLoad;
            this._sectionFun = sectionFun;
            this._itemNo = itemNo;
            this._defaultRange = defaultRange;
            this._rangeAllInfo = rangeAllInfo;
            this._collectSort = collectSort;
            this._specialType = specialType;
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
        [DataDesc(CName = "默认检验方法", ShortCode = "DiagMethod", Desc = "默认检验方法", ContextType = SysDic.All, Length = 50)]
        public virtual string DiagMethod
        {
            get { return _diagMethod; }
            set { _diagMethod = value; }
        }

        [DataMember]
        [DataDesc(CName = "结果单位", ShortCode = "Unit", Desc = "结果单位", ContextType = SysDic.All, Length = 50)]
        public virtual string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        [DataMember]
        [DataDesc(CName = "结果类型 定性，定量，描述，图形等", ShortCode = "ValueType", Desc = "结果类型 定性，定量，描述，图形等", ContextType = SysDic.All, Length = 4)]
        public virtual int ValueType
        {
            get { return _valueType; }
            set { _valueType = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样要求", ShortCode = "SamplingRequire", Desc = "采样要求", ContextType = SysDic.All, Length = 16)]
        public virtual string SamplingRequire
        {
            get { return _samplingRequire; }
            set { _samplingRequire = value; }
        }

        [DataMember]
        [DataDesc(CName = "临床意义", ShortCode = "ClinicalInfo", Desc = "临床意义", ContextType = SysDic.All, Length = 16)]
        public virtual string ClinicalInfo
        {
            get { return _clinicalInfo; }
            set { _clinicalInfo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目价格", ShortCode = "ItemCharge", Desc = "项目价格", ContextType = SysDic.All, Length = 9)]
        public virtual decimal ItemCharge
        {
            get { return _itemCharge; }
            set { _itemCharge = value; }
        }

        [DataMember]
        [DataDesc(CName = "精度", ShortCode = "Prec", Desc = "精度", ContextType = SysDic.All, Length = 4)]
        public virtual int Prec
        {
            get { return _prec; }
            set { _prec = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否医嘱项目", ShortCode = "IsOrderItem", Desc = "是否医嘱项目", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsOrderItem
        {
            get { return _isOrderItem; }
            set { _isOrderItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "0=单项  1=组合 2=组套", ShortCode = "GroupType", Desc = "0=单项  1=组合 2=组套", ContextType = SysDic.All, Length = 4)]
        public virtual int GroupType
        {
            get { return _groupType; }
            set { _groupType = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否采样项目", ShortCode = "IsSampleItem", Desc = "是否采样项目", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsSampleItem
        {
            get { return _isSampleItem; }
            set { _isSampleItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否计算项目", ShortCode = "IsCalcItem", Desc = "是否计算项目", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsCalcItem
        {
            get { return _isCalcItem; }
            set { _isCalcItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否收费项目", ShortCode = "IsChargeItem", Desc = "是否收费项目", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsChargeItem
        {
            get { return _isChargeItem; }
            set { _isChargeItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否合并项目", ShortCode = "IsUnionItem", Desc = "是否合并项目", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUnionItem
        {
            get { return _isUnionItem; }
            set { _isUnionItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否报告项目", ShortCode = "IsPrint", Desc = "是否报告项目", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsPrint
        {
            get { return _isPrint; }
            set { _isPrint = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否辅助项目，是否项目部分描述，无结果可删除项目", ShortCode = "IsPartItem", Desc = "是否辅助项目，是否项目部分描述，无结果可删除项目", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsPartItem
        {
            get { return _isPartItem; }
            set { _isPartItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "保密标志", ShortCode = "SecretFlag", Desc = "保密标志", ContextType = SysDic.All, Length = 4)]
        public virtual int SecretFlag
        {
            get { return _secretFlag; }
            set { _secretFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "历史对比类型0：百分比对比，1：数值对比，2：定性对比 3：不对比", ShortCode = "HisCompType", Desc = "历史对比类型0：百分比对比，1：数值对比，2：定性对比 3：不对比", ContextType = SysDic.All, Length = 4)]
        public virtual int HisCompType
        {
            get { return _hisCompType; }
            set { _hisCompType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "历史对比状态警告百分比", ShortCode = "HisCompH", Desc = "历史对比状态警告百分比", ContextType = SysDic.All, Length = 8)]
        public virtual double HisCompH
        {
            get { return _hisCompH; }
            set { _hisCompH = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "历史对比状态严重警告百分比", ShortCode = "HisCompHH", Desc = "历史对比状态严重警告百分比", ContextType = SysDic.All, Length = 8)]
        public virtual double HisCompHH
        {
            get { return _hisCompHH; }
            set { _hisCompHH = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "专业ID", ShortCode = "SpecialtyID", Desc = "专业ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SpecialtyID
        {
            get { return _specialtyID; }
            set { _specialtyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "提示标志 0 普通 1特殊", ShortCode = "HintFlag", Desc = "提示标志 0 普通 1特殊", ContextType = SysDic.All, Length = 4)]
        public virtual int HintFlag
        {
            get { return _hintFlag; }
            set { _hintFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "工作量参数", ShortCode = "FWorkLoad", Desc = "工作量参数", ContextType = SysDic.All, Length = 8)]
        public virtual double FWorkLoad
        {
            get { return _fWorkLoad; }
            set { _fWorkLoad = value; }
        }

        [DataMember]
        [DataDesc(CName = "通用， 微生物，细胞学，病理，酶免", ShortCode = "SectionFun", Desc = "通用， 微生物，细胞学，病理，酶免", ContextType = SysDic.All, Length = 50)]
        public virtual string SectionFun
        {
            get { return _sectionFun; }
            set { _sectionFun = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目编码", ShortCode = "ItemNo", Desc = "项目编码", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemNo
        {
            get { return _itemNo; }
            set { _itemNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "默认参考范围", ShortCode = "SectionFun", Desc = "默认参考范围", ContextType = SysDic.All, Length = 400)]
        public virtual string DefaultRange
        {
            get { return _defaultRange; }
            set { _defaultRange = value; }
        }

        [DataMember]
        [DataDesc(CName = "参考范围详细信息", ShortCode = "SectionFun", Desc = "参考范围详细信息", ContextType = SysDic.All, Length = 1000)]
        public virtual string RangeAllInfo
        {
            get { return _rangeAllInfo; }
            set { _rangeAllInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样排序", ShortCode = "Prec", Desc = "采样排序", ContextType = SysDic.All, Length = 4)]
        public virtual int CollectSort
        {
            get { return _collectSort; }
            set { _collectSort = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目特殊属性", ShortCode = "Prec", Desc = "项目特殊属性 0：常规项目 1：大文本 2：仅大文本", ContextType = SysDic.All, Length = 4)]
        public virtual int SpecialType
        {
            get { return _specialType; }
            set { _specialType = value; }
        }

        #endregion
    }
    #endregion
}