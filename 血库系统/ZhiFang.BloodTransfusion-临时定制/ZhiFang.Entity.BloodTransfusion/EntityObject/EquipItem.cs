using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region EquipItem

    /// <summary>
    /// EquipItem object for NHibernate mapped table 'EquipItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "EquipItem", ShortCode = "EquipItem", Desc = "")]
    public class EquipItem : BaseEntityService
    {
        #region Member Variables
        protected int _equipNo;
        protected int _itemNo;
        protected string _itemCode;
        protected int _defaultch;
        protected int _doctorItemNo;
        protected int _sectionNo;
        protected string _commPara;
        protected string _reagentPara;
        protected int _calcType;
        protected string _density;
        protected string _fittingInfo;
        protected string _reagentFactory;
        protected DateTime? _reagentVDate;
        protected double _dilutionMultiple;
        protected int _qCDispOrder;


        #endregion

        #region Constructors
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public EquipItem() { }

        public EquipItem(string itemCode, int defaultch, int doctorItemNo, int sectionNo, string commPara, string reagentPara, int calcType, string density, string fittingInfo, string reagentFactory, DateTime reagentVDate, double dilutionMultiple, int qCDispOrder)
        {
            this._itemCode = itemCode;
            this._defaultch = defaultch;
            this._doctorItemNo = doctorItemNo;
            this._sectionNo = sectionNo;
            this._commPara = commPara;
            this._reagentPara = reagentPara;
            this._calcType = calcType;
            this._density = density;
            this._fittingInfo = fittingInfo;
            this._reagentFactory = reagentFactory;
            this._reagentVDate = reagentVDate;
            this._dilutionMultiple = dilutionMultiple;
            this._qCDispOrder = qCDispOrder;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int EquipNo
        {
            get { return _equipNo; }
            set { _equipNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemNo
        {
            get { return _itemNo; }
            set { _itemNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ItemCode
        {
            get { return _itemCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ItemCode", value, value.ToString());
                _itemCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Defaultch", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Defaultch
        {
            get { return _defaultch; }
            set { _defaultch = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DoctorItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DoctorItemNo
        {
            get { return _doctorItemNo; }
            set { _doctorItemNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectionNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SectionNo
        {
            get { return _sectionNo; }
            set { _sectionNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CommPara", Desc = "", ContextType = SysDic.All, Length = 800)]
        public virtual string CommPara
        {
            get { return _commPara; }
            set
            {
                if (value != null && value.Length > 800)
                    throw new ArgumentOutOfRangeException("Invalid value for CommPara", value, value.ToString());
                _commPara = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReagentPara", Desc = "", ContextType = SysDic.All, Length = 800)]
        public virtual string ReagentPara
        {
            get { return _reagentPara; }
            set
            {
                if (value != null && value.Length > 800)
                    throw new ArgumentOutOfRangeException("Invalid value for ReagentPara", value, value.ToString());
                _reagentPara = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CalcType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CalcType
        {
            get { return _calcType; }
            set { _calcType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Density", Desc = "", ContextType = SysDic.All, Length = 800)]
        public virtual string Density
        {
            get { return _density; }
            set
            {
                if (value != null && value.Length > 800)
                    throw new ArgumentOutOfRangeException("Invalid value for Density", value, value.ToString());
                _density = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FittingInfo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string FittingInfo
        {
            get { return _fittingInfo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for FittingInfo", value, value.ToString());
                _fittingInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReagentFactory", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ReagentFactory
        {
            get { return _reagentFactory; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ReagentFactory", value, value.ToString());
                _reagentFactory = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReagentVDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReagentVDate
        {
            get { return _reagentVDate; }
            set { _reagentVDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DilutionMultiple", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double DilutionMultiple
        {
            get { return _dilutionMultiple; }
            set { _dilutionMultiple = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCDispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int QCDispOrder
        {
            get { return _qCDispOrder; }
            set { _qCDispOrder = value; }
        }


        #endregion
    }
    #endregion
}