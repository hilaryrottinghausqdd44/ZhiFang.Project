using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region BIdentificationMethod

    /// <summary>
    /// BIdentificationMethod object for NHibernate mapped table 'B_IdentificationMethod'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "鉴定方法字典表", ClassCName = "BIdentificationMethod", ShortCode = "BIdentificationMethod", Desc = "鉴定方法字典表")]
    public class BIdentificationMethod : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected bool _hasEquipComm;
        protected string _equipFirstNo;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected int _identificationType;
        protected EPBEquip _ePBEquip;
        protected IList<MEMicroAppraisalValue> _mEMicroAppraisalValueList;

        #endregion

        #region Constructors

        public BIdentificationMethod() { }

        public BIdentificationMethod(long labID, string cName, string eName, string sName, string shortcode, string pinYinZiTou, bool hasEquipComm, string equipFirstNo, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, int identificationType, EPBEquip ePBEquip)
        {
            this._labID = labID;
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._hasEquipComm = hasEquipComm;
            this._equipFirstNo = equipFirstNo;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._identificationType = identificationType;
            this._ePBEquip = ePBEquip;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
                _eName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
                _sName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
                _pinYinZiTou = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "上机药敏：0-否；1-是", ShortCode = "HasEquipComm", Desc = "上机药敏：0-否；1-是", ContextType = SysDic.All, Length = 1)]
        public virtual bool HasEquipComm
        {
            get { return _hasEquipComm; }
            set { _hasEquipComm = value; }
        }

        [DataMember]
        [DataDesc(CName = "上机起始号", ShortCode = "EquipFirstNo", Desc = "上机起始号", ContextType = SysDic.All, Length = 20)]
        public virtual string EquipFirstNo
        {
            get { return _equipFirstNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for EquipFirstNo", value, value.ToString());
                _equipFirstNo = value;
            }
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
        [DataDesc(CName = "鉴定类型", ShortCode = "IdentificationType", Desc = "鉴定类型", ContextType = SysDic.All, Length = 4)]
        public virtual int IdentificationType
        {
            get { return _identificationType; }
            set
            {
                _identificationType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "仪器表", ShortCode = "EPBEquip", Desc = "仪器表")]
        public virtual EPBEquip EPBEquip
        {
            get { return _ePBEquip; }
            set { _ePBEquip = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物鉴定结果", ShortCode = "MEMicroAppraisalValueList", Desc = "微生物鉴定结果")]
        public virtual IList<MEMicroAppraisalValue> MEMicroAppraisalValueList
        {
            get
            {
                if (_mEMicroAppraisalValueList == null)
                {
                    _mEMicroAppraisalValueList = new List<MEMicroAppraisalValue>();
                }
                return _mEMicroAppraisalValueList;
            }
            set { _mEMicroAppraisalValueList = value; }
        }


        #endregion
    }
    #endregion
}