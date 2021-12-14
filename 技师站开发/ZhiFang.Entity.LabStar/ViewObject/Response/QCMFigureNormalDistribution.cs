using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class QCMFigureNormalDistribution
    {
        [DataMember]
        public QCMFigureNormalDistributionY X { get; set; }
        [DataMember]
        public QCMFigureNormalDistributionY Y { get; set; }
        [DataMember]
        public List<QCMFigureNormalDistribution_Data> ReferenceLine { get; set; }
        [DataMember]
        public List<QCMFigureNormalDistribution_QCItemData> Data { get; set; }
    }

    [DataContract]
    public class QCMFigureNormalDistributionY
    {
        [DataMember]
        public double Min { get; set; }
        [DataMember]
        public double Max { get; set; }
        [DataMember]
        public List<QCMFigureNormalDistributionY_Data> Data { get; set; }
    }
    [DataContract]
    public class QCMFigureNormalDistributionY_Data
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string SDValue { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
    [DataContract]
    public class QCMFigureNormalDistribution_Data
    {
        [DataMember]
        public double YValue { get; set; }
        [DataMember]
        public double XValue { get; set; }
    }

    [DataContract]
    public class QCMFigureNormalDistribution_QCItemData
    {
        [DataMember]
        public string QCMName { get; set; }
        [DataMember]
        public List<QCMFigureNormalDistribution_Data> Data { get; set; }
    }
}
