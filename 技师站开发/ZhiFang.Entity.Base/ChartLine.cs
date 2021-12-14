using System.Runtime.Serialization;

namespace ZhiFang.Entity.Base
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
