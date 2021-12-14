using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class MultipleConcentrationQCMTree
    {
        [DataMember]
        public virtual long EquipId { get; set; }
        [DataMember]
        public virtual string QCGroup { get; set; }
        [DataMember]
        public virtual string EquipModule { get; set; }
    }
}
