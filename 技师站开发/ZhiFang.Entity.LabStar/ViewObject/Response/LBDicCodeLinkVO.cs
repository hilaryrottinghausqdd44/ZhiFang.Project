using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class LBDicCodeLinkVO : LBDicCodeLink
    {
        [DataMember]
        public override long Id { get; set; } //ID

        [DataMember]
        public string DicType { get; set; } //字典类型id

    }
}
