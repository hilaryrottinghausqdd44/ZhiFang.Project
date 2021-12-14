using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BLBEquipItemTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLBEquipItem IBLBEquipItem;

        public BLBEquipItemTest()
        {
            IBLBEquipItem = context.GetObject("BLBEquipItem") as IBLBEquipItem;
        }

        [TestMethod()]
        public void AddDelLBEquipItemTest()
        {
            //IBLBEquipItem.QueryIsExistSectionItem(4681502312058204030, 5508383914625398533);
            IBLBEquipItem.AddDelLBEquipItem(null, true, "2");
        }

        [TestMethod()]
        public void DeleteLBEquipItemTest()
        {
            IBLBEquipItem.DeleteLBEquipItem("1");
        }

    }
}