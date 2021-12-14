using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZhiFang.BLL.LabStar;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BLisCommonTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLisCommon IBLisCommon;
        IBLBSectionItem IBLBSectionItem;

        public BLisCommonTest()
        {
            IBLisCommon = context.GetObject("BLisCommon") as IBLisCommon;
            IBLBSectionItem = context.GetObject("BLBSectionItem") as IBLBSectionItem;
        }

        [TestMethod()]
        public void AddItemTest()
        {
            //IBLisCommon.AddCommonBaseRelationEntity(IBLBSectionItem,null ,false, null, "");
        }

        [TestMethod()]
        public void ExecSQLTest()
        {
            IBLisCommon.ExecSQL("delete from LB_AgeUnit where AgeUnitID=3");
        }

        [TestMethod()]
        public void QuerySQLTest()
        {
            IBLisCommon.QuerySQL("select * from LB_AgeUnit");
        }

        [TestMethod()]
        public void CalcAgeTest()
        {
            string str = LisCommonMethod.GetAge("2018-10-29", DateTime.Now.ToString("yyyy-MM-dd"));
        }
    }
}