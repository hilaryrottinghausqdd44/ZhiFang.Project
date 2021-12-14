using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    [DataContract]
    public class HttpPostedFileVO
    {
        public HttpPostedFileVO() { }
        /// <summary>
        /// 附件信息
        /// </summary>
        [DataMember]
        [DataDesc(CName = "HttpPostedFile", ShortCode = "HttpPostedFile", Desc = "HttpPostedFile")]
        public virtual HttpPostedFile PostedFile { get; set; }
    }
}
