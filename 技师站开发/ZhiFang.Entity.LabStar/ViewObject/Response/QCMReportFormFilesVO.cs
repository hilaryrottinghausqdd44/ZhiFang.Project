using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class QCMReportFormFilesVO
    {
        [DataMember]
        public string PDFPath { get; set; }

        [DataMember]
        public List<string> JpgPath { get; set; }

        [DataMember]
        public string PageName { get; set; }

        [DataMember]
        public string PageCount { get; set; }
    }
}
