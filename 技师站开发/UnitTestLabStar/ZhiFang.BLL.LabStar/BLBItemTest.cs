using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BLBItemTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLBItem IBLBItem;

        public BLBItemTest()
        {
            IBLBItem = context.GetObject("BLBItem") as IBLBItem;
        }

        [TestMethod()]
        public void QueryLBItemTest()
        {
            IBLBItem.SearchListByHQL(" lbitem.Id not in (select a.LBItem.Id from LBSectionItem a where a.LBSection.Id=4681502312058204030)");
        }

        [TestMethod()]
        public void Test()
        {
            IBLBItem.Test(5006610962819504972, "", " lbitem.CName asc, lbitem.DispOrder desc ", 2);
        }


        [TestMethod()]
        public void TestAnWeiYu()//测试按位与
        {
            IBLBItem.TestAnWeiYu();
        }

    }
}