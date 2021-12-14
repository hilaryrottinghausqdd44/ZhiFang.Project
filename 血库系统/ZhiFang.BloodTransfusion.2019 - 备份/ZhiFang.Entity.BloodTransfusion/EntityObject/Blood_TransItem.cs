using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodTransItem

    /// <summary>
    /// BloodTransItem object for NHibernate mapped table 'Blood_TransItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodTransItem", ShortCode = "BloodTransItem", Desc = "")]
    public class BloodTransItem : BaseEntity
    {
        #region Member Variables

        protected int _contentTypeID;
        protected string _transItemResult;
        protected double? _numberItemResult;
        protected bool _visible;
        protected int _dispOrder;
        protected BloodTransForm _bloodTransForm;
        protected BloodTransRecordTypeItem _bloodTransRecordTypeItem;
        protected BloodTransRecordType _bloodTransRecordType;
        #endregion

        #region Constructors

        public BloodTransItem() { }

        public BloodTransItem(long labID, int contentTypeID, string transItemResult, double numberItemResult, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodTransForm bloodTransForm, BloodTransRecordTypeItem bloodTransRecordTypeItem, BloodTransRecordType bloodTransRecordType)
        {
            this._labID = labID;
            this._contentTypeID = contentTypeID;
            this._transItemResult = transItemResult;
            this._numberItemResult = numberItemResult;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bloodTransForm = bloodTransForm;
            this._bloodTransRecordTypeItem = bloodTransRecordTypeItem;
            this._bloodTransRecordType = bloodTransRecordType;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "��Ѫ��¼�����", ShortCode = "BloodTransRecordType", Desc = "��Ѫ��¼�����")]
        public virtual BloodTransRecordType BloodTransRecordType
        {
            get { return _bloodTransRecordType; }
            set { _bloodTransRecordType = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�����ͽ��ֵ", ShortCode = "NumberItemResult", Desc = "�����ͽ��ֵ", ContextType = SysDic.All)]
        public virtual double? NumberItemResult
        {
            get { return _numberItemResult; }
            set { _numberItemResult = value; }
        }

        [DataMember]
        [DataDesc(CName = "���ݷ���", ShortCode = "ContentTypeID", Desc = "���ݷ���", ContextType = SysDic.All, Length = 4)]
        public virtual int ContentTypeID
        {
            get { return _contentTypeID; }
            set { _contentTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "��¼����", ShortCode = "TransItemResult", Desc = "��¼����", ContextType = SysDic.All, Length = 200)]
        public virtual string TransItemResult
        {
            get { return _transItemResult; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for TransItemResult", value, value.ToString());
                _transItemResult = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "�Ƿ�ʹ��", ShortCode = "Visible", Desc = "�Ƿ�ʹ��", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "��ʾ����", ShortCode = "DispOrder", Desc = "��ʾ����", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "���̼�¼������Ϣ", ShortCode = "BloodTransForm", Desc = "���̼�¼������Ϣ")]
        public virtual BloodTransForm BloodTransForm
        {
            get { return _bloodTransForm; }
            set { _bloodTransForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "���̼�¼������", ShortCode = "BloodTransRecordTypeItem", Desc = "���̼�¼������")]
        public virtual BloodTransRecordTypeItem BloodTransRecordTypeItem
        {
            get { return _bloodTransRecordTypeItem; }
            set { _bloodTransRecordTypeItem = value; }
        }

        #endregion

        #region �Զ�������

        protected int _batchSign;
        /// <summary>
        /// �����޸�¼������ݱ�־
        /// 1:��ʾ��ǰѡ��ķ�ѪѪ����Ӧ�ļ�¼����ȫδ�����Ǽ�,�Ľ��ֵȫ��Ϊ��;
        /// 2:��ʾ��ǰѡ��ķ�ѪѪ����Ӧ�ļ�¼��Ľ��ֵ������ͬ,���ֲ���ͬ;
        /// 3:��ʾ��ǰѡ��ķ�ѪѪ����Ӧ�ļ�¼��Ľ��ֵ��ȫһ��;
        /// </summary>
        [DataMember]
        [DataDesc(CName = "�����޸�¼������ݱ�־", ShortCode = "BatchSign", Desc = "�����޸�¼������ݱ�־", ContextType = SysDic.All, Length = 4)]
        public virtual int BatchSign
        {
            get { return _batchSign; }
            set { _batchSign = value; }
        }

        #endregion
    }
    #endregion
}