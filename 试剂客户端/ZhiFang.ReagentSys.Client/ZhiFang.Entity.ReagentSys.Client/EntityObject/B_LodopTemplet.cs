using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region BLodopTemplet

    /// <summary>
    /// BLodopTemplet object for NHibernate mapped table 'B_LodopTemplet'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "Lodop打印模板维护信息", ClassCName = "BLodopTemplet", ShortCode = "BLodopTemplet", Desc = "Lodop打印模板维护信息")]
    public class BLodopTemplet : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _typeCode;
        protected string _typeCName;
        protected string _paperType;
        protected string _printingDirection;
        protected double _paperWidth;
        protected double _paperHigh;
        protected string _paperUnit;
        protected string _templateCode;
        protected string _dataTestValue;
        protected int _dispOrder;
        protected string _memo;
        protected bool _isUse;


        #endregion

        #region Constructors

        public BLodopTemplet() { }

        public BLodopTemplet(long labID, string cName, string typeCode, string typeCName, string paperType, string printingDirection, double paperWidth, double paperHigh, string paperUnit, string templateCode, string dataTestValue, int dispOrder, string memo, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._cName = cName;
            this._typeCode = typeCode;
            this._typeCName = typeCName;
            this._paperType = paperType;
            this._printingDirection = printingDirection;
            this._paperWidth = paperWidth;
            this._paperHigh = paperHigh;
            this._paperUnit = paperUnit;
            this._templateCode = templateCode;
            this._dataTestValue = dataTestValue;
            this._dispOrder = dispOrder;
            this._memo = memo;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 40)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TypeCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeCode
        {
            get { return _typeCode; }
            set { _typeCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TypeCName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeCName
        {
            get { return _typeCName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for TypeCName", value, value.ToString());
                _typeCName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PaperType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PaperType
        {
            get { return _paperType; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PaperType", value, value.ToString());
                _paperType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintingDirection", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PrintingDirection
        {
            get { return _printingDirection; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PrintingDirection", value, value.ToString());
                _printingDirection = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PaperWidth", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double PaperWidth
        {
            get { return _paperWidth; }
            set { _paperWidth = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PaperHigh", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double PaperHigh
        {
            get { return _paperHigh; }
            set { _paperHigh = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PaperUnit", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PaperUnit
        {
            get { return _paperUnit; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PaperUnit", value, value.ToString());
                _paperUnit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TemplateCode", Desc = "", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string TemplateCode
        {
            get { return _templateCode; }
            set
            {
                _templateCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DataTestValue", Desc = "", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string DataTestValue
        {
            get { return _dataTestValue; }
            set
            {
                _dataTestValue = value;
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
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }


        #endregion
    }
    #endregion
}