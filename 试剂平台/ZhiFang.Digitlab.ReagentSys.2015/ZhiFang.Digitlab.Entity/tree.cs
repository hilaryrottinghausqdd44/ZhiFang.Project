using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Digitlab.Entity
{
     //前台特殊要求，树的属性名必须小写。
    [DataContract]
    public class TreeLeaf
    {
        [DataMember]
        public virtual string text { get; set; }
        [DataMember]
        public virtual bool expanded { get; set; }
        [DataMember]
        public virtual bool leaf { get; set; }
        [DataMember]
        public virtual string icon { get; set; }
        [DataMember]
        public virtual string iconCls { get; set; }
        [DataMember]
        public virtual string url { get; set; }
        [DataMember]
        public virtual string tid { get; set; }
        [DataMember]
        public virtual string pid { get; set; }
        [DataMember]
        public virtual string objectType { get; set; }
        [DataMember]
        public virtual object value { get; set; }
        [DataMember]
        public virtual object Para { get; set; }
    }
    [DataContract]
    public class TreeLeaf<T>
    {
        [DataMember]
        public virtual string text { get; set; }
        [DataMember]
        public virtual bool expanded { get; set; }
        [DataMember]
        public virtual bool leaf { get; set; }
        [DataMember]
        public virtual string icon { get; set; }
        [DataMember]
        public virtual string iconCls { get; set; }
        [DataMember]
        public virtual string url { get; set; }
        [DataMember]
        public virtual string tid { get; set; }
        [DataMember]
        public virtual string pid { get; set; }
        [DataMember]
        public virtual string objectType { get; set; }
        [DataMember]
        public virtual object Para { get; set; }
        [DataMember]
        public virtual T value { get; set; }
    }
    [DataContract]
    public class tree : TreeLeaf
    {
        [DataMember]
        public virtual List<tree> Tree { get; set; }
    }
    [DataContract]
    public class tree<T> : TreeLeaf<T>
    {        
        [DataMember]
        public virtual tree<T>[] Tree { get; set; }
    }
}
