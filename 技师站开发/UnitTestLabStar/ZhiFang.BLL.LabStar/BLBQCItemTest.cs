using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Testing.Microsoft;
using System;
using System.Collections.Generic;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;

namespace UnitTestLabStar.ZhiFang.BLL.LabStar
{
    /// <summary>
    /// BLBQCItem 的摘要说明
    /// </summary>
    [TestClass]
    public class BLBQCItemTest : AbstractDependencyInjectionSpringContextTests
    {

        public BLBQCItemTest()
        {

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

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            IBLBQCItem bLBQCItem = (IBLBQCItem)applicationContext.GetObject("BLBQCItem");
            bLBQCItem.FindTemplate(1, new LBQCItem()
            {
                LBQCMaterial = new LBQCMaterial()
                {
                    LBEquip = new LBEquip() { Id = 11 },
                    EquipModule = ""
                },
            }, "1", "1");

        }

        [TestMethod]
        public void TestMethod2()
        {
            IBLBQCItem bLBQCItem = (IBLBQCItem)applicationContext.GetObject("BLBQCItem");
            bLBQCItem.QCMReportFormPrint(
                new Dictionary<string, string>()
                {
                    { "EquipId","5696592994759264433"},
                    { "ItemId","5135547738192029872"},
                    { "EquipGroup",""},
                    { "EquipModel",""},
                },
                DateTime.Parse("2020-01-01"),
                DateTime.Parse("2020-01-31"),
                "3",
                "多浓度",
                new SortedList<string, System.IO.Stream>()
                );

        }

        [TestMethod]
        public void QueryLBQCItemTest()
        {
            IBLBQCItem bLBQCItem = (IBLBQCItem)applicationContext.GetObject("BLBQCItem");
            bLBQCItem.QueryLBQCItem(" lbequip.Computer=\'3\' ", "", 0, 0);
        }
    }
}
