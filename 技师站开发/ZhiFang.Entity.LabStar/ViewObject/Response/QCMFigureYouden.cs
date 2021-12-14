using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class QCMFigureYouden
    {
        [DataMember]
        public QCMFigureYoudenY X { get; set; }
        [DataMember]
        public QCMFigureYoudenY Y { get; set; }
        [DataMember]
        public List<QCMFigureYouden_Data> Data { get; set; }
    }

    [DataContract]
    public class QCMFigureYoudenY
    {
        [DataMember]
        public string QCMName { get; set; }
        [DataMember]
        public double Min { get; set; }
        [DataMember]
        public double Max { get; set; }
        [DataMember]
        public List<QCMFigureYoudenY_Data> Data { get; set; }
    }
    [DataContract]
    public class QCMFigureYoudenY_Data
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string SDValue { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
    [DataContract]
    public class QCMFigureYouden_Data
    {
        [DataMember]
        public string XReportValue { get; set; }
        [DataMember]
        public string YReportValue { get; set; }
        [DataMember]
        public double YValue { get; set; }
        [DataMember]
        public double XValue { get; set; }
        [DataMember]
        public int Batch { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }


        [DataMember]
        public string rolseType { get; set; }
        //[DataMember]
        //public string People { get; set; }
        //[DataMember]
        //public string Equip { get; set; }
        //[DataMember]
        //public string QCMaterial { get; set; }
        //[DataMember]
        //public string ItemName { get; set; }
        //[DataMember]
        //public string SD { get; set; }
        //[DataMember]
        //public string Target { get; set; }
        //[DataMember]
        //public string EValue { get; set; }
    }
}
