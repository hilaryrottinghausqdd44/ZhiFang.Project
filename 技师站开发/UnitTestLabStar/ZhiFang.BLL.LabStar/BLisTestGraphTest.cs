using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BLisTestGraphTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLisTestGraph IBLisTestGraph;

        public BLisTestGraphTest()
        {
            IBLisTestGraph = context.GetObject("BLisTestGraph") as IBLisTestGraph;
        }


        [TestMethod()]
        public void AddLisTestGraphToDataBaseTest()
        {
            IBLisTestGraph.AppendLisTestGraphToDataBase(1, 1, "test1133", "test2233");
        }
    }
}