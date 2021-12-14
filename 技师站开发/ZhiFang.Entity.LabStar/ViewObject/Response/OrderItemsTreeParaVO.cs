using System.Collections.Generic;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    public class OrderItemsTreeParaVO
    {
        public LBItemTreePara LBItem { get; set; }
        public List<LBSampleItemTreePara> LBSampleType { get; set; }
    }
    public class LBItemTreePara
    {
        public string ItemNo { get; set; }
        public string ItemCharge { get; set; }
    }
    public class LBSampleItemTreePara
    {
        public string SampleTypeID { get; set; }
        public string CName { get; set; }
    }
}
