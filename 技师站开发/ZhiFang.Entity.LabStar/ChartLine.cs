using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar
{
    [DataContract]
    public class Point
    {
        //[DataMember]
        ////[JsonConverter(typeof(JsonConvertClass))]
        //public virtual string PointName { get; set; }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        public virtual string X { get; set; }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        public virtual string Y { get; set; }

        //[DataMember]
        ////[JsonConverter(typeof(JsonConvertClass))]
        //public virtual string PointInfo { get; set; }

        //[DataMember]
        //public virtual bool IsUse { get; set; }

    }

    [DataContract]
    public class Line
    {
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        public virtual string LineName { get; set; }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        public virtual string LineCode { get; set; }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        public virtual string LineInfo { get; set; }

        [DataMember]
        public virtual bool IsUse { get; set; }

        [DataMember]
        public virtual IList<Point> LinePoint { get; set; }

    }
}
