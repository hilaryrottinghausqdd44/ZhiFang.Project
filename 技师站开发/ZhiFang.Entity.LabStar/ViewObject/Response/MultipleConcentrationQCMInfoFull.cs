using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class MultipleConcentrationQCMInfoFull : LisQCData
    {
        [DataMember]
        public LBQCItemTime lBQCItemTime { get; set; }
    }
}
