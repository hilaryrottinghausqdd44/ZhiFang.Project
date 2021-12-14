using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;


namespace UnitTestLabStar
{
    [TestClass()]
    public class BLBSamplingGroupTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLBSamplingGroup IBLBSamplingGroup;

        public BLBSamplingGroupTest()
        {
            IBLBSamplingGroup = context.GetObject("BLBSamplingGroup") as IBLBSamplingGroup;
        }

        [TestMethod()]
        public void QuerySamplingGroupMultiItemTest()
        {
            EntityList<LBSamplingGroup> aa = IBLBSamplingGroup.QueryLBSamplingGroupByFetch("", "", 0, 0);
            LBTcuvete lbtcuvete = aa.list[0].LBTcuvete;
            EntityList<LBSamplingGroup> bb = IBLBSamplingGroup.SearchListByHQL("", 0, 0);
            lbtcuvete = bb.list[0].LBTcuvete;
        }
    }
}