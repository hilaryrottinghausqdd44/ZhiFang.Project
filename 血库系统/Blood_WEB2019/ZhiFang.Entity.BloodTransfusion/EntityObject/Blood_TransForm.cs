using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodTransForm

    /// <summary>
    /// BloodTransForm object for NHibernate mapped table 'Blood_TransForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodTransForm", ShortCode = "BloodTransForm", Desc = "")]
    public class BloodTransForm : BaseEntity
    {
        #region Member Variables

        protected string _transFormNo;
        protected string _bBagCode;
        protected string _pCode;
        protected long? _beforeCheckID1;
        protected string _beforeCheck1;
        protected long? _beforeCheckID2;
        protected string _beforeCheck2;
        protected DateTime? _transBeginTime;
        protected long? _transCheckID1;
        protected string _transCheck1;
        protected long? _transCheckID2;
        protected string _transCheck2;
        protected DateTime? _transEndTime;
        protected bool _visible;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;

        protected bool _hasAdverseReactions;
        protected double? _adverseReactionsHP;
        protected DateTime? _adverseReactionsTime;

        protected BloodBReqForm _bloodBReqForm;
        protected BloodBOutForm _bloodBOutForm;
        protected BloodBOutItem _bloodBOutItem;
        protected Bloodstyle _bloodstyle;

        #endregion

        #region Constructors

        public BloodTransForm() { }

        public BloodTransForm(long labID, string transFormNo, string bBagCode, string pCode, long beforeCheckID1, string beforeCheck1, long beforeCheckID2, string beforeCheck2, DateTime transBeginTime, long transCheckID1, string transCheck1, long transCheckID2, string transCheck2, DateTime transEndTime, bool visible, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BloodBReqForm bloodBReqForm, Bloodstyle bloodstyle, bool hasAdverseReactions, double? adverseReactionsHP, DateTime? adverseReactionsTime, BloodBOutForm bloodBOutForm, BloodBOutItem bloodBOutItem)
        {
            this._labID = labID;
            this._transFormNo = transFormNo;
            this._bloodBOutForm = bloodBOutForm;
            this._bloodBOutItem = bloodBOutItem;

            this._bBagCode = bBagCode;
            this._pCode = pCode;
            this._beforeCheckID1 = beforeCheckID1;
            this._beforeCheck1 = beforeCheck1;
            this._beforeCheckID2 = beforeCheckID2;
            this._beforeCheck2 = beforeCheck2;
            this._transBeginTime = transBeginTime;
            this._transCheckID1 = transCheckID1;
            this._transCheck1 = transCheck1;
            this._transCheckID2 = transCheckID2;
            this._transCheck2 = transCheck2;
            this._transEndTime = transEndTime;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bloodBReqForm = bloodBReqForm;
            this._bloodstyle = bloodstyle;

            this._adverseReactionsHP = adverseReactionsHP;
            this._adverseReactionsTime = adverseReactionsTime;
            this._hasAdverseReactions = hasAdverseReactions;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "��Ѫ����", ShortCode = "BloodBOutForm", Desc = "��Ѫ����")]
        public virtual BloodBOutForm BloodBOutForm
        {
            get { return _bloodBOutForm; }
            set { _bloodBOutForm = value; }
        }
        [DataMember]
        [DataDesc(CName = "��ѪѪ����ϸ", ShortCode = "BloodBOutItem", Desc = "��ѪѪ����ϸ")]
        public virtual BloodBOutItem BloodBOutItem
        {
            get { return _bloodBOutItem; }
            set { _bloodBOutItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "��Ѫ���̼�¼����", ShortCode = "TransFormNo", Desc = "��Ѫ���̼�¼����", ContextType = SysDic.All, Length = 20)]
        public virtual string TransFormNo
        {
            get { return _transFormNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for TransFormNo", value, value.ToString());
                _transFormNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Ѫ����", ShortCode = "BBagCode", Desc = "Ѫ����", ContextType = SysDic.All, Length = 20)]
        public virtual string BBagCode
        {
            get { return _bBagCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BBagCode", value, value.ToString());
                _bBagCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "��Ʒ��", ShortCode = "PCode", Desc = "��Ʒ��", ContextType = SysDic.All, Length = 10)]
        public virtual string PCode
        {
            get { return _pCode; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for PCode", value, value.ToString());
                _pCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��Ѫ��ʼʱ��", ShortCode = "TransBeginTime", Desc = "��Ѫ��ʼʱ��", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TransBeginTime
        {
            get { return _transBeginTime; }
            set { _transBeginTime = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��Ѫ����ʱ��", ShortCode = "TransEndTime", Desc = "��Ѫ����ʱ��", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TransEndTime
        {
            get { return _transEndTime; }
            set { _transEndTime = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��Ѫǰ�˶���ID1", ShortCode = "BeforeCheckID1", Desc = "��Ѫǰ�˶���ID1", ContextType = SysDic.All, Length = 8)]
        public virtual long? BeforeCheckID1
        {
            get { return _beforeCheckID1; }
            set { _beforeCheckID1 = value; }
        }

        [DataMember]
        [DataDesc(CName = "��Ѫǰ�˶�������1", ShortCode = "BeforeCheck1", Desc = "��Ѫǰ�˶�������1", ContextType = SysDic.All, Length = 50)]
        public virtual string BeforeCheck1
        {
            get { return _beforeCheck1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BeforeCheck1", value, value.ToString());
                _beforeCheck1 = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��Ѫǰ�˶���ID2", ShortCode = "BeforeCheckID2", Desc = "��Ѫǰ�˶���ID2", ContextType = SysDic.All, Length = 8)]
        public virtual long? BeforeCheckID2
        {
            get { return _beforeCheckID2; }
            set { _beforeCheckID2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "��Ѫǰ�˶�������2", ShortCode = "BeforeCheck2", Desc = "��Ѫǰ�˶�������2", ContextType = SysDic.All, Length = 50)]
        public virtual string BeforeCheck2
        {
            get { return _beforeCheck2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BeforeCheck2", value, value.ToString());
                _beforeCheck2 = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��Ѫʱ�˶���ID1", ShortCode = "TransCheckID1", Desc = "��Ѫʱ�˶���ID1", ContextType = SysDic.All, Length = 8)]
        public virtual long? TransCheckID1
        {
            get { return _transCheckID1; }
            set { _transCheckID1 = value; }
        }

        [DataMember]
        [DataDesc(CName = "��Ѫʱ�˶�������1", ShortCode = "TransCheck1", Desc = "��Ѫʱ�˶�������1", ContextType = SysDic.All, Length = 50)]
        public virtual string TransCheck1
        {
            get { return _transCheck1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for TransCheck1", value, value.ToString());
                _transCheck1 = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��Ѫʱ�˶���ID2", ShortCode = "TransCheckID2", Desc = "��Ѫʱ�˶���ID2", ContextType = SysDic.All, Length = 8)]
        public virtual long? TransCheckID2
        {
            get { return _transCheckID2; }
            set { _transCheckID2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "��Ѫʱ�˶�������2", ShortCode = "TransCheck2", Desc = "��Ѫʱ�˶�������2", ContextType = SysDic.All, Length = 50)]
        public virtual string TransCheck2
        {
            get { return _transCheck2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for TransCheck2", value, value.ToString());
                _transCheck2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "�Ƿ��в�����Ӧ", ShortCode = "HasAdverseReactions", Desc = "�Ƿ��в�����Ӧ", ContextType = SysDic.All, Length = 1)]
        public virtual bool HasAdverseReactions
        {
            get { return _hasAdverseReactions; }
            set { _hasAdverseReactions = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "������Ӧʣ��Ѫ��", ShortCode = "AdverseReactionsHP", Desc = "������Ӧʣ��Ѫ��", ContextType = SysDic.All, Length = 8)]
        public virtual Double? AdverseReactionsHP
        {
            get { return _adverseReactionsHP; }
            set { _adverseReactionsHP = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "������Ӧʱ��", ShortCode = "AdverseReactionsTime", Desc = "������Ӧʱ��", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? AdverseReactionsTime
        {
            get { return _adverseReactionsTime; }
            set { _adverseReactionsTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "�Ƿ�ɼ�", ShortCode = "Visible", Desc = "�Ƿ�ɼ�", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "��ʾ���", ShortCode = "DispOrder", Desc = "��ʾ���", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�����޸�ʱ��", ShortCode = "DataUpdateTime", Desc = "�����޸�ʱ��", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "BloodBReqForm", ShortCode = "BloodBReqForm", Desc = "���뵥")]
        public virtual BloodBReqForm BloodBReqForm
        {
            get { return _bloodBReqForm; }
            set { _bloodBReqForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "Bloodstyle", ShortCode = "Bloodstyle", Desc = "��ѪѪ��")]
        public virtual Bloodstyle Bloodstyle
        {
            get { return _bloodstyle; }
            set { _bloodstyle = value; }
        }

        #endregion
    }
    #endregion
}