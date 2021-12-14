using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class LBQCItemVO : LBQCItem
    {
        [DataMember]
        public int Prec { get; set; } //精度
        [DataMember]
        public string ItemId { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string ItemSName { get; set; }
        [DataMember]
        public LBQCItemTimeVO lBQCItemTime { get; set; }
        [DataMember]
        public LisQCDataVO lisQCData { get; set; }
        [DataMember]
        public override LBItem LBItem
        {
            get { return null; }
            set { _lBItem = value; }
        }

        [DataMember]
        public override LBQCMaterial LBQCMaterial
        {
            get { return null; }
            set { _lBQCMaterial = value; }
        }
    }

    public class LBQCItemTimeVO : LBQCItemTime
    {
        public override LBQCItem LBQCItem
        {
            get { return null; }
            set { _lBQCItem = value; }
        }

        public override LBQCMatTime LBQCMatTime
        {
            get { return null; }
            set { _lBQCMatTime = value; }
        }
    }

    public class LisQCDataVO : LisQCData
    {
        public override LBQCItem LBQCItem
        {
            get { return null; }
            set { _lBQCItem = value; }
        }
    }
}
