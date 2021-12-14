using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBReqFormResult

    /// <summary>
    /// BloodBReqFormResult object for NHibernate mapped table 'Blood_BReqItemResult'.
    /// BReqFormID与BTestItemNo作为联合主键
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBReqFormResult", ShortCode = "BloodBReqFormResult", Desc = "")]
    public class BloodBReqFormResult : BaseEntityService
    {
        #region Member Variables

        protected string _bReqFormID;
        protected string _bTestItemNo;
        protected string _bTestItemEName;
        protected string _barcode;
        protected string _itemResult;
        protected string _itemLisResult;
        protected string _itemUnit;
        protected int? _dispOrder;
        protected DateTime? _bTestTime;
        protected bool _visible;
        protected bool _isPreTrransfusionEvaluationItem;

        #endregion

        #region Constructors

        public BloodBReqFormResult() { }

        public BloodBReqFormResult(string bReqFormID, string bTestItemNo, string barcode, string itemResult, string itemLisResult, string itemUnit, int dispOrder, DateTime bTestTime, long labID, DateTime dataAddTime, byte[] dataTimeStamp, bool visible,bool isPreTrransfusionEvaluationItem)
        {
            this._bReqFormID = bReqFormID;
            this._bTestItemNo = bTestItemNo;
            this._barcode = barcode;
            this._itemResult = itemResult;
            this._itemLisResult = itemLisResult;
            this._itemUnit = itemUnit;
            this._dispOrder = dispOrder;
            this._bTestTime = bTestTime;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
            this._isPreTrransfusionEvaluationItem = isPreTrransfusionEvaluationItem;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "BReqFormID", ShortCode = "BReqFormID", Desc = "BReqFormID", ContextType = SysDic.All, Length = 40)]
        public virtual string BReqFormID
        {
            get { return _bReqFormID; }
            set
            {
                _bReqFormID = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "BTestItemNo", ShortCode = "BTestItemNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BTestItemNo
        {
            get { return _bTestItemNo; }
            set
            {
                _bTestItemNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "BTestItemEName", ShortCode = "BTestItemEName", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual string BTestItemEName
        {
            get { return _bTestItemEName; }
            set
            {
                _bTestItemEName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Barcode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Barcode
        {
            get { return _barcode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Barcode", value, value.ToString());
                _barcode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "医嘱检验结果", ShortCode = "ItemResult", Desc = "医嘱检验结果", ContextType = SysDic.All, Length = 500)]
        public virtual string ItemResult
        {
            get { return _itemResult; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ItemResult", value, value.ToString());
                _itemResult = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "LIS原始结果", ShortCode = "ItemLisResult", Desc = "LIS原始结果", ContextType = SysDic.All, Length = 500)]
        public virtual string ItemLisResult
        {
            get { return _itemLisResult; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ItemLisResult", value, value.ToString());
                _itemLisResult = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemUnit", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ItemUnit
        {
            get { return _itemUnit; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ItemUnit", value, value.ToString());
                _itemUnit = value;
            }
        }


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验时间", ShortCode = "BTestTime", Desc = "检验时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BTestTime
        {
            get { return _bTestTime; }
            set { _bTestTime = value; }
        }


        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        [DataMember]
        [DataDesc(CName = "是否为输血前评估项", ShortCode = "IsPreTrransfusionEvaluationItem", Desc = "是否为输血前评估项", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsPreTrransfusionEvaluationItem
        {
            get { return _isPreTrransfusionEvaluationItem; }
            set { _isPreTrransfusionEvaluationItem = value; }
        }

        #endregion

        #region 自定义属性
        protected string _bTestItemCName;

        [DataMember]
        [DataDesc(CName = "检验项目", ShortCode = "BTestItemCName", Desc = "检验项目", ContextType = SysDic.All, Length = 50)]
        public virtual string BTestItemCName
        {
            get { return _bTestItemCName; }
            set
            {
                _bTestItemCName = value;
            }
        }
        private void _setBloodBReqFormResult(BloodBReqFormResult BloodBReqFormResult)
        {
            this._id = BloodBReqFormResult.Id;
            this._bReqFormID = BloodBReqFormResult.BReqFormID;
            this._bTestItemNo = BloodBReqFormResult.BTestItemNo;
            this._bTestItemEName = BloodBReqFormResult.BTestItemEName;
            this._barcode = BloodBReqFormResult.Barcode;
            this._itemResult = BloodBReqFormResult.ItemResult;
            this._itemUnit = BloodBReqFormResult.ItemUnit;
            this._dispOrder = BloodBReqFormResult.DispOrder;
            this._bTestTime = BloodBReqFormResult.BTestTime;
            this._labID = BloodBReqFormResult.LabID;
            this._dataAddTime = BloodBReqFormResult.DataAddTime;
            this._dataTimeStamp = BloodBReqFormResult.DataTimeStamp;
            this._visible = BloodBReqFormResult.Visible;
            this._isPreTrransfusionEvaluationItem = BloodBReqFormResult.IsPreTrransfusionEvaluationItem;
        }

        public BloodBReqFormResult(BloodBReqFormResult BloodBReqFormResult, BloodBTestItem bloodbtestitem)
        {
            _setBloodBReqFormResult(BloodBReqFormResult);

            this._bTestItemCName = bloodbtestitem.CName;
            this._isPreTrransfusionEvaluationItem = bloodbtestitem.IsPreTrransfusionEvaluationItem;
        }
        #endregion
    }
    #endregion
}