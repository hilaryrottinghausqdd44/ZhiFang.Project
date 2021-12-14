using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LisEquipForm

    /// <summary>
    /// LisEquipForm object for NHibernate mapped table 'Lis_EquipForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "仪器样本单", ClassCName = "LisEquipForm", ShortCode = "LisEquipForm", Desc = "仪器样本单")]
    public class LisEquipForm : BaseEntity
    {
        #region Member Variables

        protected DateTime? _eTestDate;
        protected string _eSampleNo;
        protected string _eSampleNoForOrder;
        protected string _eBarCode;
        protected string _equipModuleCode;
        protected string _eRack;
        protected int _ePosition;
        protected int _iExamine;
        protected bool _isExamined;
        protected int _eFinishCode;
        protected string _eFinishInfo;
        protected string _eResultComment;
        protected string _eSend;
        protected DateTime? _dataUpdateTime;
        protected LBEquip _lBEquip;
        protected LisTestForm _lisTestForm;


        #endregion

        #region Constructors

        public LisEquipForm() { }

        public LisEquipForm(long labID, DateTime eTestDate, string eSampleNo, string eSampleNoForOrder, string eBarCode, string equipModuleCode, string eRack, int ePosition, int iExamine, bool isExamined, int eFinishCode, string eFinishInfo, string eResultComment, string eSend, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBEquip lBEquip, LisTestForm lisTestForm)
        {
            this._labID = labID;
            this._eTestDate = eTestDate;
            this._eSampleNo = eSampleNo;
            this._eSampleNoForOrder = eSampleNoForOrder;
            this._eBarCode = eBarCode;
            this._equipModuleCode = equipModuleCode;
            this._eRack = eRack;
            this._ePosition = ePosition;
            this._iExamine = iExamine;
            this._isExamined = isExamined;
            this._eFinishCode = eFinishCode;
            this._eFinishInfo = eFinishInfo;
            this._eResultComment = eResultComment;
            this._eSend = eSend;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBEquip = lBEquip;
            this._lisTestForm = lisTestForm;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器检验日期", ShortCode = "ETestDate", Desc = "仪器检验日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ETestDate
        {
            get { return _eTestDate; }
            set { _eTestDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器样本号", ShortCode = "ESampleNo", Desc = "仪器样本号", ContextType = SysDic.All, Length = 20)]
        public virtual string ESampleNo
        {
            get { return _eSampleNo; }
            set { _eSampleNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本号排序字段", ShortCode = "ESampleNoForOrder", Desc = "样本号排序字段", ContextType = SysDic.All, Length = 50)]
        public virtual string ESampleNoForOrder
        {
            get { return _eSampleNoForOrder; }
            set { _eSampleNoForOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "EBarCode", Desc = "条码号", ContextType = SysDic.All, Length = 20)]
        public virtual string EBarCode
        {
            get { return _eBarCode; }
            set { _eBarCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "模块Code", ShortCode = "EquipModuleCode", Desc = "模块Code", ContextType = SysDic.All, Length = 20)]
        public virtual string EquipModuleCode
        {
            get { return _equipModuleCode; }
            set { _equipModuleCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器架位号", ShortCode = "ERack", Desc = "仪器架位号", ContextType = SysDic.All, Length = 20)]
        public virtual string ERack
        {
            get { return _eRack; }
            set { _eRack = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器位置号", ShortCode = "EPosition", Desc = "仪器位置号", ContextType = SysDic.All, Length = 4)]
        public virtual int EPosition
        {
            get { return _ePosition; }
            set { _ePosition = value; }
        }

        [DataMember]
        [DataDesc(CName = "检查次数", ShortCode = "IExamine", Desc = "检查次数", ContextType = SysDic.All, Length = 4)]
        public virtual int IExamine
        {
            get { return _iExamine; }
            set { _iExamine = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否多次检验", ShortCode = "IsExamined", Desc = "是否多次检验", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsExamined
        {
            get { return _isExamined; }
            set { _isExamined = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器检测完结标志", ShortCode = "EFinishCode", Desc = "仪器检测完结标志", ContextType = SysDic.All, Length = 4)]
        public virtual int EFinishCode
        {
            get { return _eFinishCode; }
            set { _eFinishCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "检测完结说明", ShortCode = "EFinishInfo", Desc = "检测完结说明", ContextType = SysDic.All, Length = 50)]
        public virtual string EFinishInfo
        {
            get { return _eFinishInfo; }
            set { _eFinishInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器结果备注", ShortCode = "EResultComment", Desc = "仪器结果备注", ContextType = SysDic.All, Length = 16)]
        public virtual string EResultComment
        {
            get { return _eResultComment; }
            set { _eResultComment = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器审核状态", ShortCode = "ESend", Desc = "仪器审核状态", ContextType = SysDic.All, Length = 100)]
        public virtual string ESend
        {
            get { return _eSend; }
            set { _eSend = value; }
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
        [DataDesc(CName = "", ShortCode = "LBEquip", Desc = "")]
        public virtual LBEquip LBEquip
        {
            get { return _lBEquip; }
            set { _lBEquip = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验单", ShortCode = "LisTestForm", Desc = "检验单")]
        public virtual LisTestForm LisTestForm
        {
            get { return _lisTestForm; }
            set { _lisTestForm = value; }
        }

        #endregion
    }
    #endregion
}