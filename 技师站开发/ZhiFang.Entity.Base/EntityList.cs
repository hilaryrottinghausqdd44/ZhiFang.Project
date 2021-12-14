using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZhiFang.Entity.Base
{
    [DataContract]
    public class EntityList<T>
    {
        [DataMember]
        public virtual int count { get; set; }
        [DataMember]
        public virtual IList<T> list { get; set; }
    }

    public class EntityPageList<T> : EntityList<T>
    {
        [DataMember]
        public virtual int page { get; set; }
    }
}
