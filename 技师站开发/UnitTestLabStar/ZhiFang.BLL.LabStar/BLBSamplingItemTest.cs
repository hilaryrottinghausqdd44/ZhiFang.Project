using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.IBLL.LabStar;


namespace UnitTestLabStar
{
    [TestClass()]
    public class BLBSamplingItemTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLBSamplingItem IBLBSamplingItem;

        public BLBSamplingItemTest()
        {
            IBLBSamplingItem = context.GetObject("BLBSamplingItem") as IBLBSamplingItem;
        }

        [TestMethod()]
        public void QuerySamplingGroupMultiItemTest()
        {
            IBLBSamplingItem.QuerySamplingGroupIsMultiItem("", true);
        }

        [TestMethod()]
        public void QueryItemMultiSamplingGroupTest()
        {
            IBLBSamplingItem.QueryItemIsMultiSamplingGroup("lbitem.CName=\'123\'", "12356,78987", true);
        }

        [TestMethod()]
        public void QueryItemNoSamplingGroupTest()
        {
            IBLBSamplingItem.QueryItemNoSamplingGroup("lbitem.CName=\'123\'", "12356,78987");
        }
    }
}