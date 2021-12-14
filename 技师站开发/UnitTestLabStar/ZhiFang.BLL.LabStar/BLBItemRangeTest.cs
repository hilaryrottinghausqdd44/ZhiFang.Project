using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BLBItemRangeTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLBItemRange IBLBItemRange;

        public BLBItemRangeTest()
        {
            IBLBItemRange = context.GetObject("BLBItemRange") as IBLBItemRange;
        }

        [TestMethod()]
        public void Test()
        {

        }



    }
}