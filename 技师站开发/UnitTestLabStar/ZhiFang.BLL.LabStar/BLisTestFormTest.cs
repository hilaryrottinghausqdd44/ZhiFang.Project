using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;
using ZhiFang.Common.Public;


namespace UnitTestLabStar
{
    [TestClass()]
    public class BLisTestFormTest
    {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLisTestForm IBLisTestForm;
        IBLBSection IBLBSection;
        IBLisTestItem IBLisTestItem;

        public BLisTestFormTest()
        {
            IBLisTestForm = context.GetObject("BLisTestForm") as IBLisTestForm;
            IBLBSection = context.GetObject("BLBSection") as IBLBSection;
            IBLisTestItem = context.GetObject("BLisTestItem") as IBLisTestItem;
        }

        [TestMethod()]
        public void AddCalcBItemTest()
        {
            IBLisTestForm.GetLisTestFormCalcItem(1);
        }

        [TestMethod()]
        public void EditLisTestFormInfoMergeTest()
        {
            IBLisTestForm.EditLisTestFormInfoMerge(1, new LisTestForm(), "7,9", 2, true, true, false, false);
        }

        [TestMethod()]
        public void QueryItemMergeFormInfoTest()
        {
            IBLisTestForm.QueryItemMergeFormInfo(5170768280308239606, "2019-11-01", "2019-11-30");
        }

        [TestMethod()]
        public void QueryItemMergeItemInfoTest()
        {
            IBLisTestForm.QueryItemMergeItemInfo(1001, "345", "测试人员", "2019-11-01", "2019-11-30", "");
        }

        [TestMethod()]
        public void EditMergeItemInfoTest()
        {
            IBLisTestForm.EditMergeItemInfo(10, "1001,100101,100102,100103,100104");
        }

        [TestMethod()]
        public void QueryLisTestFormTest()
        {
            string strWhere = " listestform.CName=\'111\' and listestform.LBSection.Id=123 and listestform.LisOrderForm.LisPatient.CName=\'Test\'";
            IBLisTestForm.QueryLisTestForm(strWhere, "", 1, 50, "LisTestForm_Id,LisTestForm_CName," +
                "LisTestForm_LBSection_Id,LisTestForm_LBSection_CName,LisTestForm_LBSection_EName," +
                "LisTestForm_LisOrderForm_LisPatient_CName,LisTestForm_LisOrderForm_LisPatient_Age");
        }

        [TestMethod()]
        public void CSharpAndOrTest()
        {
            int i = 3;  //0000 0011
            int h = 16; //0001 0000
            int k = i | h;
            int l = i & h;
            long Two25 = (long)System.Math.Pow(2, 25);

            long aa = (1 | Two25);
        }

        [TestMethod()]
        public void AddBatchLisTestFormTest()
        {
            string sampleInfo = "{\"GSampleTypeID\":\"1\",\"GSampleInfo\":\"SampleInfo Test\"" +
                ",\"FormMemo\":\"FormMemo Test\",\"SampleSpecialDesc\":\"SampleSpecialDesc Test\"" +
                ",\"TestComment\":\"TestComment Test\",\"TestInfo\":\"TestInfo Test\"" +
                "}";
            IBLisTestForm.AddBatchLisTestForm(sampleInfo, "2019-12-30", 4681502312058204030, "100", 5);
        }

        [TestMethod()]
        public void DeleteBatchLisTestFormTest()
        {
            IBLisTestForm.DeleteBatchLisTestForm("1,2", true, true);
        }

        [TestMethod()]
        public void AddLisItemResultByEquipResultTest()
        {
            //IBLisTestForm.AddLisItemResultByEquipResult(20200427001, 20200427001, "", true, true, true, 110420, "");
        }

        [TestMethod()]
        public void LisTestFormTest()
        {
            IBLisTestForm.EditLisTestFormZFSysCheckStatus(3, 0, "");
            IBLisTestForm.EditLisTestFormConfirmByTestFormID(3, 1, "test", null, "", "memo", true);
            IBLisTestForm.EditLisTestFormConfirmCancelByTestFormID(3, "memo");
            IBLisTestForm.EditLisTestFormCheckByTestFormID(3, 1, "test", "memo");
            IBLisTestForm.EditLisTestFormCheckCancelByTestFormID(3, 0, "", "memo");
        }

        [TestMethod()]
        public void CreateNewSampleNoByOldSampleNoTest()
        {
            IBLisTestForm.CreateNewSampleNoByOldSampleNo(4681502312058204030, "2020-08-17", "1007");
        }

        [TestMethod()]
        public void EditLisTestItemReCheckCancelTest()
        {
            IBLisTestForm.EditLisTestItemReCheckCancel(4740808086336216118, "5504385593691664920", true, true, "");
        }


        [TestMethod()]
        public void EditLisTestItemReCheckTest()
        {
            IList<LisTestItem> listTestItem = IBLisTestItem.SearchListByHQL(" listestitem.Id=4943812602822542558 or listestitem.Id=4953805233273304669");
            IBLisTestForm.EditLisTestItemReCheck(4740808086336216118, listTestItem, "");
        }

        [TestMethod()]
        public void EditLisTestFormReCheckCancelTest()
        {
            IBLisTestForm.EditLisTestFormReCheckCancel(4740808086336216118, true, true, "");
        }

        [TestMethod()]
        public void AddSingleLisTestFormTest()
        {
            LisTestForm aa = new LisTestForm();
            aa.GTestDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            aa.GSampleNo = DateTime.Now.ToString("ddHHmmss");
            aa.LBSection = IBLBSection.Get(5227174306717441870);
            aa.LisPatient = new LisPatient();
            aa.LisPatient.CName = DateTime.Now.ToString("HHmmss");
            IBLisTestForm.AddSingleLisTestForm(aa, null, false);
        }

        [TestMethod()]
        public void PanicValueMsgVOTest()
        {
            PanicValueMsgVO tempVO = new PanicValueMsgVO()
            {
                MSGCONTENT = new MSGCONTENT()
                {
                    MSGBODY = new MSGBODY()
                    {
                        //MSG = new MSG(),
                        MSG = new List<MSG>(),
                    },
                    MSGKEY = new MSGKEY(),
                },
            };

            string aa = JsonSerializer.JsonDotNetSerializer(tempVO);
        }

        [TestMethod()]
        public void DisposeLisTestItemPanicValueTest()
        {
            LisTestForm testForm = IBLisTestForm.Get(5229950080558769423);
            IList<LisTestItem> listTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=5229950080558769423");
            //IBLisTestForm.DisposeLisTestItemPanicValue(testForm, listTestItem);
        }
        

    }
}