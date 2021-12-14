using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.WeiXin.Entity.ViewObject.Request
{
    [DataContract]
    public class GroupItemVO : GroupItem
    {
        /// <summary>
        /// 对此业务实体操作时的描述
        /// </summary>
        [DataMember]
        public virtual TestItemVO TestItemVO { get; set; }
    }
}
