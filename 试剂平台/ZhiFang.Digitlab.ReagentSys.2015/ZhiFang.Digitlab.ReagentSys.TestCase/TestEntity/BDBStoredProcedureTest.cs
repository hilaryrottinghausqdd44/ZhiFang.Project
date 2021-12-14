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
    public class BDBStoredProcedureTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBDBStoredProcedure BDBStoredProcedure;

        public BDBStoredProcedureTest()
        {
            BDBStoredProcedure = context.GetObject("BDBStoredProcedure") as IBDBStoredProcedure;
        }

        [TestMethod]
        public void Test()
        {
            //var list = BDBStoredProcedure.MigrationCenQtyDtlTemp(13);
            var list = BDBStoredProcedure.StatReagentConsume(",,,", 0);
        }

    }
}