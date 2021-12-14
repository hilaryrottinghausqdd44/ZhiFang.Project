using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    public  class ClientEleAreaVO : ClientEleArea
    {
        [DataMember]
        public string clienteleName { get; set; }
        [DataMember]
        public long clienteleId { get; set; }
    }
}
