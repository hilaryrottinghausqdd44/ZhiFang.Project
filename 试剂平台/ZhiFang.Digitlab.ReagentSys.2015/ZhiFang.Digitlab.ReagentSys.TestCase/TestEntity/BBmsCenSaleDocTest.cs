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
    public class BBmsCenSaleDocTest
    {  
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBBmsCenSaleDoc IBBmsCenSaleDoc;
        //IBBmsCenOrderDoc IBBmsCenOrderDoc;

        public BBmsCenSaleDocTest()
        {
            IBBmsCenSaleDoc = context.GetObject("BBmsCenSaleDoc") as IBBmsCenSaleDoc;
        }

        [TestMethod]
        public void TestSave()
        {
            var list = IBBmsCenSaleDoc.ReadBmsCenSaleDocDataFormExcel("1", "4617565891398391990", @"D:\供货单模版V1.0.xls", @"D:\NewStart\99-其他类\ZhiFang.Digitlab.ReagentSys\ZhiFang.Digitlab.ReagentSys");
        }

        [TestMethod]
        public void AddTestSave1()
        {
            BmsCenSaleDoc bmsCenSaleDoc = IBBmsCenSaleDoc.Get(5530900281578699671);
            IBBmsCenSaleDoc.Entity = bmsCenSaleDoc;
            IBBmsCenSaleDoc.Edit();
        }

        public void AddTestSave2()
        {
            //IBBmsCenOrderDoc = context.GetObject("BBmsCenOrderDoc") as IBBmsCenOrderDoc;
            //BmsCenOrderDoc bmsCenOrderDoc = IBBmsCenOrderDoc.Get(5509855993549539490);
            //IBBmsCenSaleDoc.AddBmsCenSaleDocByOrderDoc(5509855993549539490);
            //IBBmsCenSaleDoc.StatBmsCenSaleDocTotalPrice("1512291548470146152,1512311737230686916");
            //IBBmsCenSaleDoc.EditBmsCenSaleDocFlag(5047246029437575324, "Test");
            IBBmsCenSaleDoc.StatBmsCenSaleDtlGoodsQty("2017-06-01", "2017-07-1", "", 0, 0, 0, "",0, 1, 50, "");
            //IBBmsCenSaleDoc.AddBmsCenSaleDocDataByMaiKe("", @"D:\NewStart\ZhiFang.Digitlab.ReagentSys\ZhiFang.Digitlab.ReagentSys");
        }
    }
}