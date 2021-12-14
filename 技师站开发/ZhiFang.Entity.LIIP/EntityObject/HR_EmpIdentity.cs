using Newtonsoft.Json;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
    #region HREmpIdentity

    /// <summary>
    /// HREmpIdentity object for NHibernate mapped table 'HR_EmpIdentity'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "员工身份", ClassCName = "", ShortCode = "YGSF", Desc = "员工身份")]
    public class HREmpIdentity
    {
        #region Member Variables
        protected long _tSysID;
        protected bool _isUse;
        private string _CName;
        private string _StandCode;
        private string _DeveCode;
        private long _Id;
        private long _LabID;
        private string _SystemName;
        private string _TSysName;
        #endregion

        #region Constructors

        public HREmpIdentity() { }

        public HREmpIdentity(long tSysID, bool isUse, string cName, string standCode, string deveCode, long id, long labID, string systemName, string tSysName)
        {
            _tSysID = tSysID;
            _isUse = isUse;
            _CName = cName;
            _StandCode = standCode;
            _DeveCode = deveCode;
            _Id = id;
            _LabID = labID;
            _SystemName = systemName;
            _TSysName = tSysName;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "员工身份类型ID", ShortCode = "YGSFLXID", Desc = "员工身份类型ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long TSysID
        {
            get { return _tSysID; }
            set { _tSysID = value; }
        }


        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "SFSY", Desc = "是否使用", ContextType = SysDic.All)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }
        [DataMember]
        public string CName { get => _CName; set => _CName = value; }
        [DataMember]
        public string StandCode { get => _StandCode; set => _StandCode = value; }
        [DataMember]
        public string DeveCode { get => _DeveCode; set => _DeveCode = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long Id { get => _Id; set => _Id = value; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long LabID { get => _LabID; set => _LabID = value; }

        [DataMember]
        public string SystemName { get => _SystemName; set => _SystemName = value; }
        [DataMember]
        public string TSysName { get => _TSysName; set => _TSysName = value; }
        #endregion
    }
    #endregion
}