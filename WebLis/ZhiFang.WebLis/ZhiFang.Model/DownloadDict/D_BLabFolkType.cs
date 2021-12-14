using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Model.DownloadDict
{
    [DataContract]
    public class D_BLabFolkType : DownloadDictBase
    {
        public D_BLabFolkType()
        { }
        [DataMember]
        public string FolkID { get; set; }
        [DataMember]
        public string LabFolkNo { get; set; }
    }
}
