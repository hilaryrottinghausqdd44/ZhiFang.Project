using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region ItemAllItem

    /// <summary>
    /// ItemAllItem object for NHibernate mapped table 'Item_AllItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "所有项目", ClassCName = "ItemAllItem", ShortCode = "ItemAllItem", Desc = "所有项目")]
    public class ItemAllItem : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected int _itemType;
        protected string _unit;
        protected string _refRange;
        protected int _valueType;
        protected string _samplingRequire;
        protected string _clinicalSignificance;
        protected int _chargeType;
        protected decimal _itemCharge;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _precision;
        protected DateTime? _dataUpdateTime;
        protected bool _isHisOrder;
        protected BSpecialty _bSpecialty;
        protected IList<BCalcFormula> _bCalcFormulaList;
        protected IList<BCalcItem> _bCalcItemList;
        protected IList<BChargeItem> _bChargeItemList;
        protected IList<BImmunePlateTemplateDetailInfo> _bImmunePlateTemplateDetailInfoList;

        protected IList<GMGroupItem> _gMGroupItemList;
        protected IList<EPEquipItem> _ePEquipItemList;
        protected IList<ItemRefeRange> _itemRefeRangeList;
        protected IList<ItemResultJudge> _itemResultJudgeList;
        protected IList<MEEquipSampleItem> _mEEquipSampleItemList;
        protected IList<MEGroupSampleItem> _mEGroupSampleItemList;
        protected IList<MEGroupSampleItem> _pMEGroupSampleItemList;
        protected IList<MEGroupSampleReCheckItem> _mEGroupSampleReCheckItemList;
        protected IList<MEImmuneResults> _mEImmuneResultsList;
        protected IList<MEPTDefaultItemModeRelation> _mEPTDefaultItemModeRelationList;
        protected IList<MEPTDistributeGroupItem> _mEPTDistributeGroupItemList;
        protected IList<MEPTSpecialDistributeRule> _mEPTSpecialDistributeRuleList;
        protected IList<MEPTItemSplit> _mEPTItemSplitList;
        private IList<MEPTItemSplit> _pMEPTItemSplitList;
        protected IList<MEPTOrderItem> _mEPTOrderItemList;
        protected IList<MEPTSampleDeliveryItem> _mEPTSampleDeliveryItemList;
        protected IList<MEPTSampleItem> _mEPTSampleItemList;
        protected IList<MEPTSampleTypeItem> _mEPTSampleTypeItemList;
        protected IList<MEPTSamplingItem> _mEPTSamplingItemList;
        protected IList<MEImmuneTempResults> _mEImmuneTempResultsList;
        protected IList<MEPTGetReportTimeOfItem> _mEPTGetReportTimeOfItemList;

        #endregion

        #region Constructors

        public ItemAllItem() { }

        public ItemAllItem(long labID, string cName, string eName, string sName, int itemType, string unit, string refRange, int valueType, string samplingRequire, string clinicalSignificance, int chargeType, decimal itemCharge, string comment, bool isUse, int dispOrder, string useCode, string standCode, string deveCode, string shortcode, string pinYinZiTou, int precision, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool isHisOrder, BSpecialty bSpecialty)
        {
            this._labID = labID;
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
            this._itemType = itemType;
            this._unit = unit;
            this._refRange = refRange;
            this._valueType = valueType;
            this._samplingRequire = samplingRequire;
            this._clinicalSignificance = clinicalSignificance;
            this._chargeType = chargeType;
            this._itemCharge = itemCharge;
            this._comment = comment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._useCode = useCode;
            this._standCode = standCode;
            this._deveCode = deveCode;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._precision = precision;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._isHisOrder = isHisOrder;
            this._bSpecialty = bSpecialty;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 100)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                _eName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 100)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                _sName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "项目类型", ShortCode = "ItemType", Desc = "项目类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        [DataMember]
        [DataDesc(CName = "结果单位", ShortCode = "Unit", Desc = "结果单位", ContextType = SysDic.All, Length = 50)]
        public virtual string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "默认参考范围", ShortCode = "RefRange", Desc = "默认参考范围", ContextType = SysDic.All, Length = 500)]
        public virtual string RefRange
        {
            get { return _refRange; }
            set
            {
                _refRange = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "结果类型", ShortCode = "ValueType", Desc = "结果类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ValueType
        {
            get { return _valueType; }
            set { _valueType = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样要求", ShortCode = "SamplingRequire", Desc = "采样要求", ContextType = SysDic.All, Length = 8000)]
        public virtual string SamplingRequire
        {
            get { return _samplingRequire; }
            set
            {
                _samplingRequire = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "临床意义", ShortCode = "ClinicalSignificance", Desc = "临床意义", ContextType = SysDic.All, Length = 8000)]
        public virtual string ClinicalSignificance
        {
            get { return _clinicalSignificance; }
            set
            {
                _clinicalSignificance = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "收费类型", ShortCode = "ChargeType", Desc = "收费类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ChargeType
        {
            get { return _chargeType; }
            set { _chargeType = value; }
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
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 8000)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
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
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 100)]
        public virtual string UseCode
        {
            get { return _useCode; }
            set
            {
                _useCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 100)]
        public virtual string StandCode
        {
            get { return _standCode; }
            set
            {
                _standCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "开发商标准代码", ShortCode = "DeveCode", Desc = "开发商标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string DeveCode
        {
            get { return _deveCode; }
            set
            {
                _deveCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 100)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set
            {
                _shortcode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set
            {
                _pinYinZiTou = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "精度", ShortCode = "Precision", Desc = "精度", ContextType = SysDic.All, Length = 4)]
        public virtual int Precision
        {
            get { return _precision; }
            set { _precision = value; }
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
        [DataDesc(CName = "是否是医嘱项目（开申请单）", ShortCode = "IsHisOrder", Desc = "是否是医嘱项目（开申请单）", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsHisOrder
        {
            get { return _isHisOrder; }
            set { _isHisOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "专业表", ShortCode = "BSpecialty", Desc = "专业表")]
        public virtual BSpecialty BSpecialty
        {
            get { return _bSpecialty; }
            set { _bSpecialty = value; }
        }

        [DataMember]
        [DataDesc(CName = "计算公式", ShortCode = "BCalcFormulaList", Desc = "计算公式")]
        public virtual IList<BCalcFormula> BCalcFormulaList
        {
            get
            {
                if (_bCalcFormulaList == null)
                {
                    _bCalcFormulaList = new List<BCalcFormula>();
                }
                return _bCalcFormulaList;
            }
            set { _bCalcFormulaList = value; }
        }

        [DataMember]
        [DataDesc(CName = "计算公式中涉及的项目", ShortCode = "BCalcItemList", Desc = "计算公式中涉及的项目")]
        public virtual IList<BCalcItem> BCalcItemList
        {
            get
            {
                if (_bCalcItemList == null)
                {
                    _bCalcItemList = new List<BCalcItem>();
                }
                return _bCalcItemList;
            }
            set { _bCalcItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "计费项目表：0-开单项目（包含医嘱项目、套餐项目）、1-检验耗材", ShortCode = "BChargeItemList", Desc = "计费项目表：0-开单项目（包含医嘱项目、套餐项目）、1-检验耗材")]
        public virtual IList<BChargeItem> BChargeItemList
        {
            get
            {
                if (_bChargeItemList == null)
                {
                    _bChargeItemList = new List<BChargeItem>();
                }
                return _bChargeItemList;
            }
            set { _bChargeItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "用于存储酶免板布局模板的详细布局信息。记录每一个孔位的检验项目、样本类型等信息", ShortCode = "BImmunePlateTemplateDetailInfoList", Desc = "用于存储酶免板布局模板的详细布局信息。记录每一个孔位的检验项目、样本类型等信息")]
        public virtual IList<BImmunePlateTemplateDetailInfo> BImmunePlateTemplateDetailInfoList
        {
            get
            {
                if (_bImmunePlateTemplateDetailInfoList == null)
                {
                    _bImmunePlateTemplateDetailInfoList = new List<BImmunePlateTemplateDetailInfo>();
                }
                return _bImmunePlateTemplateDetailInfoList;
            }
            set { _bImmunePlateTemplateDetailInfoList = value; }
        }
                
        [DataMember]
        [DataDesc(CName = "小组项目", ShortCode = "GMGroupItemList", Desc = "小组项目")]
        public virtual IList<GMGroupItem> GMGroupItemList
        {
            get
            {
                if (_gMGroupItemList == null)
                {
                    _gMGroupItemList = new List<GMGroupItem>();
                }
                return _gMGroupItemList;
            }
            set { _gMGroupItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器项目关系表", ShortCode = "EPEquipItemList", Desc = "仪器项目关系表")]
        public virtual IList<EPEquipItem> EPEquipItemList
        {
            get
            {
                if (_ePEquipItemList == null)
                {
                    _ePEquipItemList = new List<EPEquipItem>();
                }
                return _ePEquipItemList;
            }
            set { _ePEquipItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemRefeRangeList", Desc = "")]
        public virtual IList<ItemRefeRange> ItemRefeRangeList
        {
            get
            {
                if (_itemRefeRangeList == null)
                {
                    _itemRefeRangeList = new List<ItemRefeRange>();
                }
                return _itemRefeRangeList;
            }
            set { _itemRefeRangeList = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemResultJudgeList", Desc = "")]
        public virtual IList<ItemResultJudge> ItemResultJudgeList
        {
            get
            {
                if (_itemResultJudgeList == null)
                {
                    _itemResultJudgeList = new List<ItemResultJudge>();
                }
                return _itemResultJudgeList;
            }
            set { _itemResultJudgeList = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器样本项目", ShortCode = "MEEquipSampleItemList", Desc = "仪器样本项目")]
        public virtual IList<MEEquipSampleItem> MEEquipSampleItemList
        {
            get
            {
                if (_mEEquipSampleItemList == null)
                {
                    _mEEquipSampleItemList = new List<MEEquipSampleItem>();
                }
                return _mEEquipSampleItemList;
            }
            set { _mEEquipSampleItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本项目", ShortCode = "MEGroupSampleItemList", Desc = "小组样本项目")]
        public virtual IList<MEGroupSampleItem> MEGroupSampleItemList
        {
            get
            {
                if (_mEGroupSampleItemList == null)
                {
                    _mEGroupSampleItemList = new List<MEGroupSampleItem>();
                }
                return _mEGroupSampleItemList;
            }
            set { _mEGroupSampleItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本项目组合项目", ShortCode = "PMEGroupSampleItemList", Desc = "小组样本项目组合项目")]
        public virtual IList<MEGroupSampleItem> PMEGroupSampleItemList
        {
            get
            {
                if (_pMEGroupSampleItemList == null)
                {
                    _pMEGroupSampleItemList = new List<MEGroupSampleItem>();
                }
                return _pMEGroupSampleItemList;
            }
            set { _pMEGroupSampleItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "复检项目", ShortCode = "MEGroupSampleReCheckItemList", Desc = "复检项目")]
        public virtual IList<MEGroupSampleReCheckItem> MEGroupSampleReCheckItemList
        {
            get
            {
                if (_mEGroupSampleReCheckItemList == null)
                {
                    _mEGroupSampleReCheckItemList = new List<MEGroupSampleReCheckItem>();
                }
                return _mEGroupSampleReCheckItemList;
            }
            set { _mEGroupSampleReCheckItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "存储酶免板的96个孔的孔号、标本类型、检验项目、检验结果信息", ShortCode = "MEImmuneResultsList", Desc = "存储酶免板的96个孔的孔号、标本类型、检验项目、检验结果信息")]
        public virtual IList<MEImmuneResults> MEImmuneResultsList
        {
            get
            {
                if (_mEImmuneResultsList == null)
                {
                    _mEImmuneResultsList = new List<MEImmuneResults>();
                }
                return _mEImmuneResultsList;
            }
            set { _mEImmuneResultsList = value; }
        }

        [DataMember]
        [DataDesc(CName = "默认模板项目关系", ShortCode = "MEPTDefaultItemModeRelationList", Desc = "默认模板项目关系")]
        public virtual IList<MEPTDefaultItemModeRelation> MEPTDefaultItemModeRelationList
        {
            get
            {
                if (_mEPTDefaultItemModeRelationList == null)
                {
                    _mEPTDefaultItemModeRelationList = new List<MEPTDefaultItemModeRelation>();
                }
                return _mEPTDefaultItemModeRelationList;
            }
            set { _mEPTDefaultItemModeRelationList = value; }
        }

        [DataMember]
        [DataDesc(CName = "分发小组项目", ShortCode = "MEPTDistributeGroupItemList", Desc = "分发小组项目")]
        public virtual IList<MEPTDistributeGroupItem> MEPTDistributeGroupItemList
        {
            get
            {
                if (_mEPTDistributeGroupItemList == null)
                {
                    _mEPTDistributeGroupItemList = new List<MEPTDistributeGroupItem>();
                }
                return _mEPTDistributeGroupItemList;
            }
            set { _mEPTDistributeGroupItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "特殊项目分发规则", ShortCode = "MEPTSpecialDistributeRuleList", Desc = "特殊项目分发规则")]
        public virtual IList<MEPTSpecialDistributeRule> MEPTSpecialDistributeRuleList
        {
            get
            {
                if (_mEPTSpecialDistributeRuleList == null)
                {
                    _mEPTSpecialDistributeRuleList = new List<MEPTSpecialDistributeRule>();
                }
                return _mEPTSpecialDistributeRuleList;
            }
            set { _mEPTSpecialDistributeRuleList = value; }
        }

        [DataMember]
        [DataDesc(CName = "按项目拆分", ShortCode = "MEPTItemSplitList", Desc = "按项目拆分")]
        public virtual IList<MEPTItemSplit> MEPTItemSplitList
        {
            get
            {
                if (_mEPTItemSplitList == null)
                {
                    _mEPTItemSplitList = new List<MEPTItemSplit>();
                }
                return _mEPTItemSplitList;
            }
            set { _mEPTItemSplitList = value; }
        }

        [DataMember]
        [DataDesc(CName = "按项目拆分", ShortCode = "PMEPTItemSplitList", Desc = "按项目拆分")]
        public virtual IList<MEPTItemSplit> PMEPTItemSplitList
        {
            get
            {
                if (_pMEPTItemSplitList == null)
                {
                    _pMEPTItemSplitList = new List<MEPTItemSplit>();
                }
                return _pMEPTItemSplitList;
            }
            set { _pMEPTItemSplitList = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱单项目", ShortCode = "MEPTOrderItemList", Desc = "医嘱单项目")]
        public virtual IList<MEPTOrderItem> MEPTOrderItemList
        {
            get
            {
                if (_mEPTOrderItemList == null)
                {
                    _mEPTOrderItemList = new List<MEPTOrderItem>();
                }
                return _mEPTOrderItemList;
            }
            set { _mEPTOrderItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "外送项目表", ShortCode = "MEPTSampleDeliveryItemList", Desc = "外送项目表")]
        public virtual IList<MEPTSampleDeliveryItem> MEPTSampleDeliveryItemList
        {
            get
            {
                if (_mEPTSampleDeliveryItemList == null)
                {
                    _mEPTSampleDeliveryItemList = new List<MEPTSampleDeliveryItem>();
                }
                return _mEPTSampleDeliveryItemList;
            }
            set { _mEPTSampleDeliveryItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本单项目", ShortCode = "MEPTSampleItemList", Desc = "样本单项目")]
        public virtual IList<MEPTSampleItem> MEPTSampleItemList
        {
            get
            {
                if (_mEPTSampleItemList == null)
                {
                    _mEPTSampleItemList = new List<MEPTSampleItem>();
                }
                return _mEPTSampleItemList;
            }
            set { _mEPTSampleItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本类型-项目", ShortCode = "MEPTSampleTypeItemList", Desc = "样本类型-项目")]
        public virtual IList<MEPTSampleTypeItem> MEPTSampleTypeItemList
        {
            get
            {
                if (_mEPTSampleTypeItemList == null)
                {
                    _mEPTSampleTypeItemList = new List<MEPTSampleTypeItem>();
                }
                return _mEPTSampleTypeItemList;
            }
            set { _mEPTSampleTypeItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样项目维护", ShortCode = "MEPTSamplingItemList", Desc = "采样项目维护")]
        public virtual IList<MEPTSamplingItem> MEPTSamplingItemList
        {
            get
            {
                if (_mEPTSamplingItemList == null)
                {
                    _mEPTSamplingItemList = new List<MEPTSamplingItem>();
                }
                return _mEPTSamplingItemList;
            }
            set { _mEPTSamplingItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "存储酶免板定量计算后所用到的临时结果", ShortCode = "MEImmuneTempResultsList", Desc = "存储酶免板定量计算后所用到的临时结果")]
        public virtual IList<MEImmuneTempResults> MEImmuneTempResultsList
        {
            get
            {
                if (_mEImmuneTempResultsList == null)
                {
                    _mEImmuneTempResultsList = new List<MEImmuneTempResults>();
                }
                return _mEImmuneTempResultsList;
            }
            set { _mEImmuneTempResultsList = value; }
        }

        [DataMember]
        [DataDesc(CName = "取单时间-项目关系表", ShortCode = "MEPTGetReportTimeOfItemList", Desc = "取单时间-项目关系表")]
        public virtual IList<MEPTGetReportTimeOfItem> MEPTGetReportTimeOfItemList
        {
            get
            {
                if (_mEPTGetReportTimeOfItemList == null)
                {
                    _mEPTGetReportTimeOfItemList = new List<MEPTGetReportTimeOfItem>();
                }
                return _mEPTGetReportTimeOfItemList;
            }
            set { _mEPTGetReportTimeOfItemList = value; }
        }


        #endregion
    }
    #endregion
}