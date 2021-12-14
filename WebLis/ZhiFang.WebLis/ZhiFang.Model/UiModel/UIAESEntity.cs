using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Model.UiModel
{
    public class UIAESEntity
    {
        public UIAESEntity() { }

        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string UserCName { get; set; }
        [DataMember]
        public string UserPwd { get; set; }

        [DataMember]
        public string LabCode { get; set; }
        [DataMember]
        public string LabCName { get; set; }
        [DataMember]
        public string Version { get; set; }
        [DataMember]
        public string Validity { get; set; }

        [DataMember]
        public string BaseUrl { get; set; }
        [DataMember]
        public string Port { get; set; }
        
        [DataMember]
        public string DownloadDictName { get; set; }

        [DataMember]
        public string NrequestFormAddOrUpdateName { get; set; }
    }
}
