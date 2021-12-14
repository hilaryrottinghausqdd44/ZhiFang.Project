using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.Response
{
    /// <summary>
    /// 机构货品分类VO
    /// </summary>
    [DataContract]
    public class ReaGoodsClassVO
    {
        [DataMember]
        public virtual string Id { get; set; }
        [DataMember]
        public virtual string CName { get; set; }
        public ReaGoodsClassVO() { }
        public ReaGoodsClassVO(string id,string cname) {
            this.Id = id;
            this.CName = cname;
        }
    }
}
