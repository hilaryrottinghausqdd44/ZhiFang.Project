using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class QCMFigureCumulativeSumGraph
    {
        [DataMember]
        public QCMFigureCumulativeSumGraphX X { get; set; }
        [DataMember]
        public QCMFigureCumulativeSumGraphY Y { get; set; }
        [DataMember]
        public List<QCMFigureCumulativeSumGraphY> ItemTimes { get; set; }
        [DataMember]
        public List<QCMFigureCumulativeSumGraph_Data> Data { get; set; }
    }
    [DataContract]
    public class QCMFigureCumulativeSumGraphX
    {
        [DataMember]
        public List<int> Batch { get; set; }
        [DataMember]
        public List<DateTime> Date { get; set; }
    }
    [DataContract]
    public class QCMFigureCumulativeSumGraphY
    {
        [DataMember]
        public double Min { get; set; }
        [DataMember]
        public double Max { get; set; }
        [DataMember]
        public int Batch { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }

        [DataMember]
        public List<QCMFigureCumulativeSumGraphY_Data> Data { get; set; }
    }
    [DataContract]
    public class QCMFigureCumulativeSumGraphY_Data
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string SDValue { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
    [DataContract]
    public class QCMFigureCumulativeSumGraph_Data
    {
        [DataMember]
        public string QcDataId { get; set; }
        [DataMember]
        public string ReportValue { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public int Batch { get; set; }
        [DataMember]
        public double YValue { get; set; }
        [DataMember]
        public string rolseType { get; set; }
        [DataMember]
        public string People { get; set; }
        [DataMember]
        public string Equip { get; set; }
        [DataMember]
        public string QCMaterial { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string SD { get; set; }
        [DataMember]
        public string Target { get; set; }

        [DataMember]
        public string EValue { get; set; }
        [DataMember]
        public bool BUse { get; set; }
    }
}
