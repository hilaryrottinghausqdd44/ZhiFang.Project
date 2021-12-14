using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBagProcess

    /// <summary>
    /// BloodBagProcess object for NHibernate mapped table 'Blood_BagProcess'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBagProcess", ShortCode = "BloodBagProcess", Desc = "")]
    public class BloodBagProcess : BaseEntity
    {
        #region Member Variables

        //protected string _bPreFormID;
        //protected string _bPreItemID;
        protected string _bBagCode;
        protected string _pCode;
        protected string _b3Code;
        //protected string _pTNo;
        protected int _bPflag;
        protected int _dispOrder;

        protected BloodBPreForm _bloodBPreForm;
        protected BloodBPreItem _bloodBPreItem;
        protected BloodBagProcessType _bloodBagProcessType;

        #endregion

        #region Constructors

        public BloodBagProcess() { }

        public BloodBagProcess(BloodBPreForm bloodBPreForm, BloodBPreItem bloodBPreItem, BloodBagProcessType bloodBagProcessType, long labID, string bBagCode, string pCode, string b3Code, int bPflag, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._bloodBPreForm = bloodBPreForm;
            this._bloodBPreItem = bloodBPreItem;
            this._bBagCode = bBagCode;
            this._pCode = pCode;
            this._b3Code = b3Code;
            this._bloodBagProcessType = bloodBagProcessType;
            this._bPflag = bPflag;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodBPreForm", Desc = "")]
        public virtual BloodBPreForm BloodBPreForm
        {
            get { return _bloodBPreForm; }
            set { _bloodBPreForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "BloodBPreItem", ShortCode = "BloodBPreItem", Desc = "BloodBPreItem")]
        public virtual BloodBPreItem BloodBPreItem
        {
            get { return _bloodBPreItem; }
            set { _bloodBPreItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "BloodBagProcessType", ShortCode = "BloodBagProcessType", Desc = "BloodBagProcessType")]
        public virtual BloodBagProcessType BloodBagProcessType
        {
            get { return _bloodBagProcessType; }
            set { _bloodBagProcessType = value; }
        }


        [DataMember]
        [DataDesc(CName = "", ShortCode = "BBagCode", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "PCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PCode
        {
            get { return _pCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PCode", value, value.ToString());
                _pCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "B3Code", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string B3Code
        {
            get { return _b3Code; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for B3Code", value, value.ToString());
                _b3Code = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPflag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BPflag
        {
            get { return _bPflag; }
            set { _bPflag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }


        #endregion
    }
    #endregion
}