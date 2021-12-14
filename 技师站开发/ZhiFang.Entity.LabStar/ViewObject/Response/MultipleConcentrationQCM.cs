using System;
using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class MultipleConcentrationQCM : LBQCItem
    {
        [DataMember]
        public virtual DateTime StartDate { get; set; }

        [DataMember]
        public virtual DateTime? EndDate { get; set; }

        [DataMember]
        public LBQCItemTime LBQCItemTime { get; set; }
        [DataMember]
        public double Target { get; set; }
        [DataMember]
        public double SD { get; set; }
        [DataMember]
        public double CCV { get; set; }
        [DataMember]
        public double CalculationTarget { get; set; }
        [DataMember]
        public double CalculationSD { get; set; }
        [DataMember]
        public string CalculationCCV { get; set; }
        [DataMember]
        public string LotNo { get; set; }

        [DataMember]
        public string Manu { get; set; }

    }
}
