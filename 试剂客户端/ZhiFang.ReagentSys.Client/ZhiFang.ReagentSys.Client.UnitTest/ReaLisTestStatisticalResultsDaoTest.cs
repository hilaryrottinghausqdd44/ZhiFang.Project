
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.ReagentSys.Client.UnitTest
{
    [TestClass]
    public class ReaLisTestStatisticalResultsDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaLisTestStatisticalResultsDao ReaLisTestStatisticalResultsDao;
        public ReaLisTestStatisticalResultsDaoTest()
        {
            ReaLisTestStatisticalResultsDao = context.GetObject("ReaLisTestStatisticalResultsDao") as IDReaLisTestStatisticalResultsDao;
        }

        [TestMethod]
        public void TestSave()
        {
            ReaLisTestStatisticalResults entity = new ReaLisTestStatisticalResults();
            entity.Id = longGUID;
            entity.TestEquipCode = "测试";
            entity.TestEquipName = "测试";
            entity.TestEquipTypeCode = "测试";
            entity.TestEquipTypeName = "测试";
            entity.TestType = "测试";
            entity.TestItemCode = "测试";
            entity.TestItemCName = "测试";
            entity.TestItemSName = "测试";
            entity.TestItemEName = "测试";
            entity.TestCount = 1;
            bool b = ReaLisTestStatisticalResultsDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaLisTestStatisticalResults entity = ReaLisTestStatisticalResultsDao.Get(longGUID);

            bool b = ReaLisTestStatisticalResultsDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaLisTestStatisticalResults entity = ReaLisTestStatisticalResultsDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaLisTestStatisticalResultsDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}