using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BLisOrderFormTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLisOrderForm IBLisOrderForm;
        IBLBItem IBLBItem;
        IBLisPatient IBLisPatient;
        public BLisOrderFormTest()
        {
            IBLisOrderForm = context.GetObject("BLisOrderForm") as IBLisOrderForm;
            IBLBItem = context.GetObject("BLBItem") as IBLBItem;
            IBLisPatient = context.GetObject("BLisPatient") as IBLisPatient;
        }

        [TestMethod()]
        public void AddOrder()
        {
            var p = new LisPatient() { LabID = 1, CName = "测试用例病人信息" };
            var f = new LisOrderForm() { LabID = 1, OrderFormNo = "测试用例医嘱单" };
            var i = new List<LisOrderItem>() {
                new LisOrderItem(){LabID=1, OrderFormNo="测试用例医嘱单项目",PartitionDate=DateTime.Parse("2020-01-01")},
                new LisOrderItem(){LabID=1,OrderFormNo="测试用例医嘱单项目",PartitionDate=DateTime.Parse("2020-12-12")}
            };
            IBLisOrderForm.Edit_AddOrder(p, f, i,"","");
        }

    }
}