using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class QCMFigureZ
    {
        [DataMember]
        public ZX X { get; set; }
        [DataMember]
        public ZY Y { get; set; }
        [DataMember]
        public List<ZY> ItemTimes { get; set; }
        [DataMember]
        public List<List<Z_Data>> Data { get; set; }
        [DataMember]
        public List<List<Z_Data>> UnData { get; set; }
    }
    [DataContract]
    public class ZX
    {
        [DataMember]
        public List<int> Batch { get; set; }
        [DataMember]
        public List<DateTime> Date { get; set; }
    }
    [DataContract]
    public class ZY
    {
        [DataMember]
        public string LBQCMaterialCName { get; set; }

        [DataMember]
        public double Min { get; set; }
        [DataMember]
        public double Max { get; set; }
        [DataMember]
        public int Batch { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }


        [DataMember]
        public List<ZY_Data> Data { get; set; }
    }
    [DataContract]
    public class ZY_Data
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string SDValue { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
    [DataContract]
    public class Z_Data
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
        public string loseRule { get; set; }
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
