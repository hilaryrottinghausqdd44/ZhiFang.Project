using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BLisTestItemTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLisTestItem IBLisTestItem;

        public BLisTestItemTest()
        {
            IBLisTestItem = context.GetObject("BLisTestItem") as IBLisTestItem;
        }

        [TestMethod()]
        public void QueryLisTestItemTest()
        {
            IBLisTestItem.QueryLisTestItem("listestform.CName=\'001\'", "", 0, 0);
            IBLisTestItem.QueryLisTestItem("listestitem.LisTestForm.Id=1001", "", 0, 0);
            IBLisTestItem.QueryLisTestItem("listestform.Id=1001", "", 0, 0);
            IBLisTestItem.QueryLisTestItem("listestitem.LBItem.Id=100123", "", 0, 0);
            IBLisTestItem.QueryLisTestItem("lbitem.Id=100123", "", 0, 0);
        }

        [TestMethod()]
        public void EditLisTestItemResultByDilutionTest()
        {
            IBLisTestItem.EditLisTestItemResultByDilution("100102,100103", 100);
        }

        [TestMethod()]
        public void SearchLisTestItemTest()
        {
            LisTestItem aa = IBLisTestItem.Get(51);
            IList<LisTestItem> list = IBLisTestItem.LoadAll();
            EntityList<LisTestItem> entityList = IBLisTestItem.SearchListByHQL("listestitem.LisTestForm.CName=\'001\'", "", 0, 0);
            entityList = IBLisTestItem.SearchListByHQL("listestitem.LisTestForm.Id=1001", "", 0, 0);
        }

        [TestMethod()]
        public void QueryCommonItemByTestFormIDTest()
        {
            IBLisTestItem.QueryCommonItemByTestFormID("1,2,3");
        }

        [TestMethod()]
        public void EditLisTestItemResultByOffsetTest()
        {
            string str = "[{\"LisTestItemID\":\"1\",\"LBItemID\":\"12356\",\"Coefficient\":\"3\",\"AddValue\":\"2\"}," +
            "{\"LisTestItemID\":\"2\",\"LBItemID\":\"12356787\",\"Coefficient\":\"31\",\"AddValue\":\"22\"}," +
            "{\"LisTestItemID\":\"3\",\"LBItemID\":\"566612387\",\"Coefficient\":\"2\",\"AddValue\":\"21\"}" +
            "]";
            IBLisTestItem.EditLisTestItemResultByOffset(str);
        }
    }
}