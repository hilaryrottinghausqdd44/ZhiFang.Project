using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region SIDictionaryRelation

    /// <summary>
    /// SIDictionaryRelation object for NHibernate mapped table 'SI_DictionaryRelation'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "字典对照表", ClassCName = "SIDictionaryRelation", ShortCode = "SIDictionaryRelation", Desc = "字典对照表")]
    public class SIDictionaryRelation : BaseEntity
    {
        #region Member Variables

        protected string _dicTableName;
        protected long? _lISCode;
        protected string _contrastCode;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected string _code1;
        protected string _code2;
        protected string _code3;
        protected string _code4;
        protected string _code5;
        protected string _code6;
        protected string _code7;
        protected string _code8;
        protected string _code9;
        protected string _code10;
        protected string _code11;
        protected string _code12;
        protected string _code13;
        protected string _code14;
        protected string _code15;
        protected string _code16;
        protected string _code17;
        protected string _code18;
        protected string _code19;
        protected string _code20;

        #endregion

        #region Constructors

        public SIDictionaryRelation() { }

        public SIDictionaryRelation(long labID, string dicTableName, long lISCode, string contrastCode, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string code1, string code2, string code3, string code4, string code5, string code6, string code7, string code8, string code9, string code10, string code11, string code12, string code13, string code14, string code15, string code16, string code17, string code18, string code19, string code20)
        {
            this._labID = labID;
            this._dicTableName = dicTableName;
            this._lISCode = lISCode;
            this._contrastCode = contrastCode;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._code1 = code1;
            this._code2 = code2;
            this._code3 = code3;
            this._code4 = code4;
            this._code5 = code5;
            this._code6 = code6;
            this._code7 = code7;
            this._code8 = code8;
            this._code9 = code9;
            this._code10 = code10;
            this._code11 = code11;
            this._code12 = code12;
            this._code13 = code13;
            this._code14 = code14;
            this._code15 = code15;
            this._code16 = code16;
            this._code17 = code17;
            this._code18 = code18;
            this._code19 = code19;
            this._code20 = code20;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "字典表名", ShortCode = "DicTableName", Desc = "字典表名", ContextType = SysDic.All, Length = 20)]
        public virtual string DicTableName
        {
            get { return _dicTableName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DicTableName", value, value.ToString());
                _dicTableName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "LIS码ID", ShortCode = "LISCode", Desc = "LIS码ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LISCode
        {
            get { return _lISCode; }
            set { _lISCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "对照码", ShortCode = "ContrastCode", Desc = "对照码", ContextType = SysDic.All, Length = 30)]
        public virtual string ContrastCode
        {
            get { return _contrastCode; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for ContrastCode", value, value.ToString());
                _contrastCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
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
        [DataDesc(CName = "", ShortCode = "Code1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code1
        {
            get { return _code1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code1", value, value.ToString());
                _code1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code2
        {
            get { return _code2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code2", value, value.ToString());
                _code2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code3
        {
            get { return _code3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code3", value, value.ToString());
                _code3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code4", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code4
        {
            get { return _code4; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code4", value, value.ToString());
                _code4 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code5", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code5
        {
            get { return _code5; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code5", value, value.ToString());
                _code5 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code6", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code6
        {
            get { return _code6; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code6", value, value.ToString());
                _code6 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code7", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code7
        {
            get { return _code7; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code7", value, value.ToString());
                _code7 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code8", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code8
        {
            get { return _code8; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code8", value, value.ToString());
                _code8 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code9", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code9
        {
            get { return _code9; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code9", value, value.ToString());
                _code9 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code10", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code10
        {
            get { return _code10; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code10", value, value.ToString());
                _code10 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code11", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code11
        {
            get { return _code11; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code11", value, value.ToString());
                _code11 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code12", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code12
        {
            get { return _code12; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code12", value, value.ToString());
                _code12 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code13", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code13
        {
            get { return _code13; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code13", value, value.ToString());
                _code13 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code14", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code14
        {
            get { return _code14; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code14", value, value.ToString());
                _code14 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code15", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code15
        {
            get { return _code15; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code15", value, value.ToString());
                _code15 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code16", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code16
        {
            get { return _code16; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code16", value, value.ToString());
                _code16 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code17", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code17
        {
            get { return _code17; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code17", value, value.ToString());
                _code17 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code18", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code18
        {
            get { return _code18; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code18", value, value.ToString());
                _code18 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code19", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code19
        {
            get { return _code19; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code19", value, value.ToString());
                _code19 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code20", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code20
        {
            get { return _code20; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code20", value, value.ToString());
                _code20 = value;
            }
        }

        


        #endregion
    }
    #endregion
}