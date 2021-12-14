using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BLisOperateTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLisOperate IBLisOperate;

        public BLisOperateTest()
        {
            IBLisOperate = context.GetObject("BLisOperate") as IBLisOperate;
        }

        [TestMethod()]
        public void AddLisOperateTest()
        {
            LisTestForm aaa = new LisTestForm();
            IBLisOperate.AddLisOperate(aaa, TestFormOperateType.上机检验.Value, null, null);
        }

    }
}