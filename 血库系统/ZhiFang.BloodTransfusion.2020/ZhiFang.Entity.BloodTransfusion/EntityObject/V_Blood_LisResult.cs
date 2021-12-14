using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region VBloodLisResult

    /// <summary>
    /// VBloodLisResult object for NHibernate mapped table 'VBloodLisResult'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "获取LIS检验结果", ClassCName = "VBloodLisResult", ShortCode = "VBloodLisResult", Desc = "获取LIS检验结果")]
    public class VBloodLisResult : BaseEntityServiceByString
    {
        #region Member Variables
        protected DateTime? _ReceiveDate;
        protected string _BarCode;
        protected string _ParItemNo;
        protected string _ReportDesc;
        protected string _B3code;
        protected string _CheckDateTime;
        protected string _Checker;

        protected string _PatName;
        protected string _SampleNo;
        protected string _SectionNo;
        protected string _TestTypeNo;
        protected string _PatNo;

        protected string _Itemtesttime;
        protected string _ItemResult;
        protected string _ItemNo;
        protected string _ItemName;
        protected string _EName;

        protected string _ItemUnit;
        #endregion

        #region 多主键
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Constructors

        public VBloodLisResult() { }

        public VBloodLisResult(string id, DateTime? ReceiveDate, string _ParItemNo, string ReportDesc, string B3code, string _CheckDateTime, string _Checker, string _PatName, string _SampleNo, string _SectionNo, string _TestTypeNo, string _PatNo, string _Itemtesttime, string _ItemResult, string _ItemNo, string _ItemName, string _EName, string _ItemUnit)
        {
            this._id = id;
            this._ReceiveDate = ReceiveDate;
            this._ParItemNo = _ParItemNo;
            this._ReportDesc = ReportDesc;
            this._B3code = B3code;
            this._Checker = _Checker;

            this._PatName = _PatName;
            this._SampleNo = _SampleNo;
            this._SectionNo = _SectionNo;
            this._TestTypeNo = _TestTypeNo;
            this._PatNo = _PatNo;

            this._Itemtesttime = _Itemtesttime;
            this._ItemResult = _ItemResult;
            this._ItemNo = _ItemNo;
            this._ItemName = _ItemName;
            this._EName = _EName;

            this._ItemUnit = _ItemUnit;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "核收日期", ShortCode = "ReceiveDate", Desc = "核收日期", ContextType = SysDic.DateTime)]
        public virtual DateTime? ReceiveDate
        {
            get { return _ReceiveDate; }
            set { _ReceiveDate = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ParItemNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ParItemNo
        {
            get { return _ParItemNo; }
            set
            {
                _ParItemNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "BarCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BarCode
        {
            get { return _BarCode; }
            set
            {
                _BarCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportDesc", Desc = "", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ReportDesc
        {
            get { return _ReportDesc; }
            set
            {
                _ReportDesc = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "B3code", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string B3code
        {
            get { return _B3code; }
            set
            {
                _B3code = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckDateTime", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CheckDateTime
        {
            get { return _CheckDateTime; }
            set
            {
                _CheckDateTime = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "Checker", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Checker
        {
            get { return _Checker; }
            set
            {
                _Checker = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PatName
        {
            get { return _PatName; }
            set
            {
                _PatName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleNo
        {
            get { return _SampleNo; }
            set
            {
                _SampleNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectionNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SectionNo
        {
            get { return _SectionNo; }
            set
            {
                _SectionNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestTypeNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TestTypeNo
        {
            get { return _TestTypeNo; }
            set
            {
                _TestTypeNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PatNo
        {
            get { return _PatNo; }
            set
            {
                _PatNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Itemtesttime", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Itemtesttime
        {
            get { return _Itemtesttime; }
            set
            {
                _Itemtesttime = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemResult", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ItemResult
        {
            get { return _ItemResult; }
            set
            {
                _ItemResult = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemNo
        {
            get { return _ItemNo; }
            set
            {
                _ItemNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemName", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual string ItemName
        {
            get { return _ItemName; }
            set
            {
                _ItemName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
        {
            get { return _EName; }
            set
            {
                _EName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemUnit", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ItemUnit
        {
            get { return _ItemUnit; }
            set
            {
                _ItemUnit = value;
            }
        }

        #endregion
    }
    #endregion
}