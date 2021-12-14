using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Model
{
    //下载报告参数
    [DataContract]
   public class DownloadReportParam
    {
        [DataMember]
        public string reportformtitle { get; set; }
        [DataMember]
        public string reportformIds { get; set; }
    }
}
