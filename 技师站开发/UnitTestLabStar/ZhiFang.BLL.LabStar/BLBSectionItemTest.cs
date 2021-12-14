using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.IBLL.LabStar;


namespace UnitTestLabStar
{
    [TestClass()]
    public class BLBSectionItemTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLBSectionItem IBLBSectionItem;

        public BLBSectionItemTest()
        {
            IBLBSectionItem = context.GetObject("BLBSectionItem") as IBLBSectionItem;
        }

        [TestMethod()]
        public void AddDelLBSectionItemTest()
        {
            IBLBSectionItem.AddDelLBSectionItem(null, true, "");
        }

        [TestMethod()]
        public void DeleteLBSectionItemTest()
        {
            IBLBSectionItem.DeleteLBSectionItem("1");
        }
    }
}