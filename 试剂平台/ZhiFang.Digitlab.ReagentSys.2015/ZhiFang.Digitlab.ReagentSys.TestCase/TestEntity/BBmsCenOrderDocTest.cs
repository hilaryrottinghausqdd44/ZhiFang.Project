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
    public class BBmsCenOrderDocTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBBmsCenOrderDoc IBBmsCenOrderDoc;

        public BBmsCenOrderDocTest()
        {
            IBBmsCenOrderDoc = context.GetObject("BBmsCenOrderDoc") as IBBmsCenOrderDoc;
        }

        [TestMethod]
        public void TestSave()
        {
            //var list = IBBmsCenOrderDoc.EditBmsCenOrderDoc();
            BmsCenOrderDoc aa = new BmsCenOrderDoc();
            aa = null;
        }


    }
}