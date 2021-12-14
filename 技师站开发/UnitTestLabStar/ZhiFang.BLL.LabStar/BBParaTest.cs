using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BBParaTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBBPara IBBPara;

        public BBParaTest()
        {
            IBBPara = context.GetObject("BBPara") as IBBPara;
        }

        [TestMethod()]
        public void GetParaDictClassTest()
        {
            IBBPara.QueryFactoryParaInfoByParaName("Com_Type3_Para");
        }

        [TestMethod()]
        public void QueryParaByParaTypeCodeTest()
        {
            IBBPara.GetSystemDefaultPara("Com_Type3_Para", "123", "erwrwer");
        }

        [TestMethod()]
        public void QueryParaInfoByParaNameTest()
        {
            IBBPara.QueryFactoryParaInfoByParaName("质控");
            //IBBPara.QueryParaInfoByParaName("分类");
            //IBBPara.QueryParaInfoByParaName("参数1");

        }
    }
}