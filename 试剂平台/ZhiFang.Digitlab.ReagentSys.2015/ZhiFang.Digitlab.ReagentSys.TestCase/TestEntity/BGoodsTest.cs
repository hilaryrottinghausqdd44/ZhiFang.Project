using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.Digitlab.IBLL;
using ZhiFang.Digitlab.IBLL.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys;


namespace ZhiFang.Digitlab.ReagentSys.TestCase
{
    [TestClass]
    public class BGoodsTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBGoods IBGoods;

        public BGoodsTest()
        {
            IBGoods = context.GetObject("BGoods") as IBGoods;
        }

        [TestMethod]
        public void TestSave()
        {
            var list = IBGoods.AddGoodsDataFormExcel("4617565891398391990", "4617565891398391990", "4617565891398391990", @"D:\测试数据西门子导入.xls", @"D:\NewStart\99-其他类\ZhiFang.Digitlab.ReagentSys\ZhiFang.Digitlab.ReagentSys");
        }

        public void Test()
        {
            var list = IBGoods.CheckGoodsExcelFormat(@"D:\a1122.xlsx", @"D:\NewStart\99-其他类\ZhiFang.Digitlab.ReagentSys\ZhiFang.Digitlab.ReagentSys");
        }

        public void Test11()
        {
            IBGoods.SearchGoodsByCompID("5646270513436139226", "5762022216103561415", "1=1", "", 0, 0);
        }
    }
}