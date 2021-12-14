using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Digitlab.Entity
{
    [DataContract]
    public class Point<T>
    {
        [DataMember]
        public virtual string name { get; set; }
        [DataMember]
        public virtual double XValue { get; set; }
        [DataMember]
        public virtual double YValue { get; set; }
        [DataMember]
        public virtual string Info { get; set; }
        [DataMember]
        public virtual bool IsUse { get; set; }
        [DataMember]
        public virtual T BInfo { get; set; }
    }
}
