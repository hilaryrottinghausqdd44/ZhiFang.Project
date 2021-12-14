using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BLBQCRuleTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLBQCRule IBLBQCRule;

        public BLBQCRuleTest()
        {
            IBLBQCRule = context.GetObject("BLBQCRule") as IBLBQCRule;
        }

        [TestMethod()]
        public void DeleteInvalidLBQCRuleTest()
        {
            IBLBQCRule.DeleteInvalidLBQCRule();
        }
    }
}