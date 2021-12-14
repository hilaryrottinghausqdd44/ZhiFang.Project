using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBReqItemResult

    /// <summary>
    /// BloodBReqItemResult object for NHibernate mapped table 'Blood_BReqItemResult'.
    /// BReqFormID与BTestItemNo作为联合主键
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBReqItemResult", ShortCode = "BloodBReqItemResult", Desc = "")]
    public class BloodBReqItemResult : BaseEntityService
    {
        #region Member Variables

        protected string _bReqFormID;
        protected string _bTestItemNo;
        protected string _patNo;
        protected string _patID;
        protected string _barcode;
        protected string _itemResult;
        protected string _itemUnit;
        protected DateTime? _bReqTime;
        protected int _dispOrder;
        protected DateTime? _bTestTime;
        //protected long _bReqItemResultID;
        protected bool _visible;


        #endregion

        #region Constructors

        public BloodBReqItemResult() { }

        public BloodBReqItemResult(string bReqFormID, string bTestItemNo, string patNo, string patID, string barcode, string itemResult, string itemUnit, DateTime bReqTime, int dispOrder, DateTime bTestTime, long labID, DateTime dataAddTime, byte[] dataTimeStamp, bool visible)
        {
            this._bReqFormID = bReqFormID;
            this._bTestItemNo = bTestItemNo;
            this._patNo = patNo;
            this._patID = patID;
            this._barcode = barcode;
            this._itemResult = itemResult;
            this._itemUnit = itemUnit;
            this._bReqTime = bReqTime;
            this._dispOrder = dispOrder;
            this._bTestTime = bTestTime;
            //this._bReqItemResultID = bReqItemResultID;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "CS原BReqFormID(联合主键1)", ShortCode = "BReqFormID", Desc = "BReqFormID", ContextType = SysDic.All, Length = 40)]
        public virtual string BReqFormID
        {
            get { return _bReqFormID; }
            set
            {
                _bReqFormID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "CS原联合主键2", ShortCode = "BTestItemNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BTestItemNo
        {
            get { return _bTestItemNo; }
            set
            {
                _bTestItemNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PatNo
        {
            get { return _patNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PatNo", value, value.ToString());
                _patNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PatID
        {
            get { return _patID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PatID", value, value.ToString());
                _patID = value;
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
        [DataDesc(CName = "", ShortCode = "ItemResult", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemResult
        {
            get { return _itemResult; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ItemResult", value, value.ToString());
                _itemResult = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemUnit", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemUnit
        {
            get { return _itemUnit; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ItemUnit", value, value.ToString());
                _itemUnit = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BReqTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BReqTime
        {
            get { return _bReqTime; }
            set { _bReqTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
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
        private void _setBloodBReqItemResult(BloodBReqItemResult bloodbreqitemresult)
        {
            this._id = bloodbreqitemresult.Id;
            this._bReqFormID = bloodbreqitemresult.BReqFormID;
            this._bTestItemNo = bloodbreqitemresult.BTestItemNo;
            this._patNo = bloodbreqitemresult.PatNo;
            this._patID = bloodbreqitemresult.PatID;
            this._barcode = bloodbreqitemresult.Barcode;
            this._itemResult = bloodbreqitemresult.ItemResult;
            this._itemUnit = bloodbreqitemresult.ItemUnit;
            this._bReqTime = bloodbreqitemresult.BReqTime;
            this._dispOrder = bloodbreqitemresult.DispOrder;
            this._bTestTime = bloodbreqitemresult.BTestTime;
            this._labID = bloodbreqitemresult.LabID;
            this._dataAddTime = bloodbreqitemresult.DataAddTime;
            this._dataTimeStamp = bloodbreqitemresult.DataTimeStamp;
            this._visible = bloodbreqitemresult.Visible;
        }

        public BloodBReqItemResult(BloodBReqItemResult bloodbreqitemresult, BloodBTestItem bloodbtestitem)
        {
            _setBloodBReqItemResult(bloodbreqitemresult);

            this._bTestItemCName = bloodbtestitem.CName;
        }
        #endregion
    }
    #endregion
}