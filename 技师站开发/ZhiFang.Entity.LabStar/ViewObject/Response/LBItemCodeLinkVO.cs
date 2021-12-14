using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class LBItemCodeLinkVO : LBItemCodeLink
    {
        [DataMember]
        public override long Id { get; set; } //项目ID
        [DataMember]
        public int GroupType { get; set; }//组合类型
    }
}
