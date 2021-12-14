using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    public class BLabGroupItemVO: BLabGroupItem
    {
        /// <summary>
        /// 项目
        /// </summary>
        [DataMember]
        public virtual BLabTestItemVO BLabTestItemVO { get; set; }
    }
}
