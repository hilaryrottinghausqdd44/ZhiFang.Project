using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Testing.Microsoft;
using System;
using System.Collections.Generic;
using System.Data;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;
using ZhiFang.LabStar.Common;

namespace UnitTestLabStar.ZhiFang.BLL.LabStar
{
    [TestClass()]
    public class QualityControlRuleJudgment : AbstractDependencyInjectionSpringContextTests
    {
        //Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        IBLisQCData IBLisQCData { get; set; }
        IBLisBarCodeForm IBLisBarCodeForm { get; set; }
        public QualityControlRuleJudgment()
        {
            //IBLisQCData = context.GetObject("BLisQCData") as IBLisQCData;
        }

        protected override string[] ConfigLocations
        {
            get
            {
                return new string[] {
                "assembly://ZhiFang.ObjectFactory.LabStar/ZhiFang.ObjectFactory.LabStar.Config/DaoBase.xml",
                "assembly://ZhiFang.ObjectFactory.LabStar/ZhiFang.ObjectFactory.LabStar.Config/Dao.xml",
                "assembly://ZhiFang.ObjectFactory.LabStar/ZhiFang.ObjectFactory.LabStar.Config/ServiceBase.xml",
                "assembly://ZhiFang.ObjectFactory.LabStar/ZhiFang.ObjectFactory.LabStar.Config/Service.xml"
                };
            }
        }

        [TestMethod()]
        public void AddLisQCDataTest()
        {
            IBLisQCData iBLisQCData = (IBLisQCData)applicationContext.GetObject("BLisQCData");
            DataTable dataTable = MyNPOIHelper.ImportExceltoDataTable(@"E:\智方项目\检验之星BS版\ZhiFang.LabStar.TechnicianStation\UnitTestLabStar\QCMData.xls");
            List<LisQCData> lisQCDatas = new List<LisQCData>();
            foreach (DataRow item in dataTable.Rows)
            {
                lisQCDatas.Add(new LisQCData()
                {
                    LBQCItem = new LBQCItem() { Id = long.Parse(item[0].ToString()) },
                    ReportValue = item[1].ToString(),
                    ReceiveTime = DateTime.Parse(item[2].ToString()),
                    BUse = true
                });
            }
            foreach (var item in lisQCDatas)
            {
                iBLisQCData.AddLisQCData(item);
            }
        }

        [TestMethod()]
        public void BarCodeFormTest()
        {
            IBLisBarCodeForm lisBarCodeForm = (IBLisBarCodeForm)applicationContext.GetObject("BLisBarCodeForm");
            List<string> list = new List<string>("1234".Split(','));
            //lisBarCodeForm.EditSampleSignForOrGetListByPara(0, list, "", "", 0, "BarCode", true, false);
        }
    }
}
