using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BLBQCMaterialTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLBQCMaterial IBLBQCMaterial;

        public BLBQCMaterialTest()
        {
            IBLBQCMaterial = context.GetObject("BLBQCMaterial") as IBLBQCMaterial;
        }

        [TestMethod()]
        public void QueryLBQCMaterialTreeTest()
        {
            BaseResultTree aa = IBLBQCMaterial.GetEquipMaterialTree(0, 1);
        }

        [TestMethod()]
        public void AddCopyLBQCItemByMatIDTest()
        {
            BaseResult aa = IBLBQCMaterial.AddCopyLBQCItemByMatID(1, 2);
        }
    }
}