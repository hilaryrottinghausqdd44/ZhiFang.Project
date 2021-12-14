using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class LisQCDataMonthVO : LisQCData
    {
        [DataMember]
        public override long Id
        {
            get
            {
                return _id;
            }
            set { _id = value; }
        }
        [DataMember]
        public int Prec { get; set; } //精度
        [DataMember]
        public LBQCItemTime lBQCItemTime { get; set; }
    }
}
