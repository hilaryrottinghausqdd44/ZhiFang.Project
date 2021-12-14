using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
    #region PGroup

    /// <summary>
    /// PGroup object for NHibernate mapped table 'PGroup'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "PGroup", ShortCode = "PGroup", Desc = "")]
    public class PGroup : BaseEntityServiceByInt
    {
        #region Member Variables

        protected int _superGroupNo;
        protected string _cName;
        protected string _shortName;
        protected string _shortCode;
        protected string _sectionDesc;
        protected int _sectionType;
        protected int _visible;
        protected int _dispOrder;
        protected int _onlinetime;
        protected int _keyDispOrder;
        protected int _dummygroup;
        protected int _uniontype;
        protected int _sectorTypeNo;
        protected int _sampleNoType;
        protected int _isSendGroup;
        protected int _reportSection;
        protected int _rePortToDept;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected int _userNo;
        protected DateTime? _dataUpdateTime;
        protected bool _isImage;
        protected string _proDLL;
        protected int _execDeptNo;
        protected int _sectionResultType;
        protected string _printCode;
        protected int _sTestType;
        protected int _clientNo;
        protected string _eName;


        #endregion

        #region Constructors

        public PGroup() { }

        public PGroup(int superGroupNo, string cName, string shortName, string shortCode, string sectionDesc, int sectionType, int visible, int dispOrder, int onlinetime, int keyDispOrder, int dummygroup, int uniontype, int sectorTypeNo, int sampleNoType, int isSendGroup, int reportSection, int rePortToDept, string useCode, string standCode, string deveCode, int userNo, DateTime dataAddTime, DateTime dataUpdateTime, bool isImage, string proDLL, int execDeptNo, int sectionResultType, string printCode, int sTestType, int clientNo, string eName, long labID, byte[] dataTimeStamp)
        {
            this._superGroupNo = superGroupNo;
            this._cName = cName;
            this._shortName = shortName;
            this._shortCode = shortCode;
            this._sectionDesc = sectionDesc;
            this._sectionType = sectionType;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._onlinetime = onlinetime;
            this._keyDispOrder = keyDispOrder;
            this._dummygroup = dummygroup;
            this._uniontype = uniontype;
            this._sectorTypeNo = sectorTypeNo;
            this._sampleNoType = sampleNoType;
            this._isSendGroup = isSendGroup;
            this._reportSection = reportSection;
            this._rePortToDept = rePortToDept;
            this._useCode = useCode;
            this._standCode = standCode;
            this._deveCode = deveCode;
            this._userNo = userNo;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._isImage = isImage;
            this._proDLL = proDLL;
            this._execDeptNo = execDeptNo;
            this._sectionResultType = sectionResultType;
            this._printCode = printCode;
            this._sTestType = sTestType;
            this._clientNo = clientNo;
            this._eName = eName;
            this._labID = labID;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "Ö÷¼üID", ShortCode = "Id", Desc = "Ö÷¼üID", ContextType = SysDic.Number)]
        public override int Id
        {
            get
            {
                if (_id <= 0 && _id != -1)
                    _id = ZhiFang.Common.Public.GUIDHelp.GetGUIDInt();
                return _id;
            }
            set { _id = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SuperGroupNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SuperGroupNo
        {
            get { return _superGroupNo; }
            set { _superGroupNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 40)]
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
        [DataDesc(CName = "", ShortCode = "ShortName", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string ShortName
        {
            get { return _shortName; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortName", value, value.ToString());
                _shortName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
                _shortCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectionDesc", Desc = "", ContextType = SysDic.All, Length = 250)]
        public virtual string SectionDesc
        {
            get { return _sectionDesc; }
            set
            {
                if (value != null && value.Length > 250)
                    throw new ArgumentOutOfRangeException("Invalid value for SectionDesc", value, value.ToString());
                _sectionDesc = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectionType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SectionType
        {
            get { return _sectionType; }
            set { _sectionType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Onlinetime", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Onlinetime
        {
            get { return _onlinetime; }
            set { _onlinetime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "KeyDispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int KeyDispOrder
        {
            get { return _keyDispOrder; }
            set { _keyDispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Dummygroup", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Dummygroup
        {
            get { return _dummygroup; }
            set { _dummygroup = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Uniontype", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Uniontype
        {
            get { return _uniontype; }
            set { _uniontype = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectorTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SectorTypeNo
        {
            get { return _sectorTypeNo; }
            set { _sectorTypeNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleNoType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SampleNoType
        {
            get { return _sampleNoType; }
            set { _sampleNoType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsSendGroup", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsSendGroup
        {
            get { return _isSendGroup; }
            set { _isSendGroup = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportSection", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ReportSection
        {
            get { return _reportSection; }
            set { _reportSection = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RePortToDept", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int RePortToDept
        {
            get { return _rePortToDept; }
            set { _rePortToDept = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UseCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
        {
            get { return _useCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
                _useCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StandCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
        {
            get { return _standCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StandCode", value, value.ToString());
                _standCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeveCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DeveCode
        {
            get { return _deveCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DeveCode", value, value.ToString());
                _deveCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UserNo
        {
            get { return _userNo; }
            set { _userNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsImage", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsImage
        {
            get { return _isImage; }
            set { _isImage = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProDLL", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ProDLL
        {
            get { return _proDLL; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ProDLL", value, value.ToString());
                _proDLL = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ExecDeptNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ExecDeptNo
        {
            get { return _execDeptNo; }
            set { _execDeptNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectionResultType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SectionResultType
        {
            get { return _sectionResultType; }
            set { _sectionResultType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintCode", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string PrintCode
        {
            get { return _printCode; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for PrintCode", value, value.ToString());
                _printCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "STestType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int STestType
        {
            get { return _sTestType; }
            set { _sTestType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ClientNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ClientNo
        {
            get { return _clientNo; }
            set { _clientNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 50)]
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


        #endregion
    }
    #endregion
}