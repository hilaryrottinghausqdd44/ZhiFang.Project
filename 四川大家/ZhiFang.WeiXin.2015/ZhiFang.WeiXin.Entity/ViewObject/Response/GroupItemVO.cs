using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{

    [DataContract]
    public class GroupItemVO:GroupItem
    {
        /// <summary>
        /// 项目
        /// </summary>
        [DataMember]
        public virtual TestItem TestItem { get; set; }
    }
}
