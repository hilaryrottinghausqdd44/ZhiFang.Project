using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Model
{
    //[Serializable]
    [DataContract]
    public partial class LocationbarCodePrintPamater
    {
        public LocationbarCodePrintPamater()
        { }
        #region Model
        private long _id;
        private string _accountid;
        private string _parameter;
        private DateTime? _createdatetime;
        private DateTime? _updatedatetime;
        private DateTime _timestamp;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string AccountId
        {
            set { _accountid = value; }
            get { return _accountid; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ParaMeter
        {
            set { _parameter = value; }
            get { return _parameter; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime? CreateDateTime
        {
            set { _createdatetime = value; }
            get { return _createdatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime? UpdateDateTime
        {
            set { _updatedatetime = value; }
            get { return _updatedatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime TimeStamp
        {
            set { _timestamp = value; }
            get { return _timestamp; }
        }
        #endregion Model
    }
}
