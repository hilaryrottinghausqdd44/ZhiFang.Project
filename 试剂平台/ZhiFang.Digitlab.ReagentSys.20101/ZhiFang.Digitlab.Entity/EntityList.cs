using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Digitlab.Entity
{
    [DataContract]
    public class EntityList<T>
    {
        [DataMember]
        public virtual int count { get; set; }
        [DataMember]
        public virtual IList<T> list { get; set; }
    }
}
