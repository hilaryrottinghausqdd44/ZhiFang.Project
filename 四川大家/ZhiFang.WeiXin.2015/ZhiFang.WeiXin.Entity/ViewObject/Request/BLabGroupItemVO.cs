using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.WeiXin.Entity.ViewObject.Request
{
    [DataContract]
    public class BLabGroupItemVO : BLabGroupItem
    {
        /// <summary>
        /// 对此业务实体操作时的描述
        /// </summary>
        [DataMember]
        public virtual BLabTestItemVO BLabTestItemVO { get; set; }
    }
}
