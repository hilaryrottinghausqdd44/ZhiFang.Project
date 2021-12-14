using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar
{
    [TestClass()]
    public class BBParaItemTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBBParaItem IBBParaItem;
        IBBPara IBBPara;

        public BBParaItemTest()
        {
            IBBParaItem = context.GetObject("BBParaItem") as IBBParaItem;
            IBBPara = context.GetObject("BBPara") as IBBPara;
        }

        [TestMethod()]
        public void AddAndEditParaItemTest()
        {
            string strJson = "[" +
                             "{\"ObjectID\":\"1\",\"ObjectName\":\"12356\"}," +
                             "{\"ObjectID\":\"2\",\"ObjectName\":\"12356787\"}," +
                             "{\"ObjectID\":\"3\",\"ObjectName\":\"566612387\"}" +
                             "]";
            IList<BParaItem> ParaItemList = new List<BParaItem>()
           {
            new BParaItem() { ParaNo = "Com_0006", ObjectID = 1, ObjectName = "ObjectName1",  ParaValue = "3", DispOrder = 1 },
            new BParaItem() { ParaNo = "Com_0007", ObjectID = 1, ObjectName = "ObjectName2",  ParaValue = "4", DispOrder = 1 },
            new BParaItem() { ParaNo = "Com_0008", ObjectID = 1, ObjectName = "ObjectName3",  ParaValue = "5", DispOrder = 1 }

            };
            ParaItemList[0].BPara = IBBPara.Get(5655295498975101340);
            ParaItemList[1].BPara = IBBPara.Get(5506597760437083773);
            ParaItemList[2].BPara = IBBPara.Get(5247761990112602936);
            IBBParaItem.AddAndEditParaItem(strJson, ParaItemList, "35699", "empName");
        }

        [TestMethod()]
        public void AddCopyParaItemByObjectIDTest()
        {
            IBBParaItem.AddCopyParaItemByObjectID("1", "11", "112233", "5566", "emp");
        }

        [TestMethod()]
        public void DeleteParaItemByObjectIDTest()
        {
            string strJson = "[{\"ObjectID\":\"1\",\"ObjectName\":\"测试1\"},{\"ObjectID\":\"2\",\"ObjectName\":\"测试2\"},]";

            IBBParaItem.DeleteParaItemByObjectID(strJson);
        }

        [TestMethod()]
        public void QueryParaSystemTypeInfoTest()
        {
            IBBParaItem.QueryParaSystemTypeInfo("2", "QC_Z_Para");
            //IBBParaItem.QuerySystemParaItem(" bparaitem.ObjectID=1", "2", "Com_Type3_Para");
        }

        [TestMethod()]
        public void QueryParaValueByParaNoTest()
        {
            BPara para = IBBParaItem.QueryParaValueByParaNo("QC_Z_0006", "1");
        }
    }
}