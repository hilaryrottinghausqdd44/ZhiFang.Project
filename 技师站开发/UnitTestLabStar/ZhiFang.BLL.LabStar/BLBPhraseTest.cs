using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BLBPhraseTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLBPhrase IBLBPhrase;

        public BLBPhraseTest()
        {
            IBLBPhrase = context.GetObject("BLBPhrase") as IBLBPhrase;
        }

        [TestMethod()]
        public void QueryLBPhraseVOTest()
        {
            EntityList<LBPhraseVO> aa = IBLBPhrase.QueryLBPhraseVO("", "", "", null, "");
        }
    }
}