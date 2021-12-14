
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.WebAssist.UnitTest
{
    [TestClass]
    public class MDMicroTestDevelopDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDMDMicroTestDevelopDao MDMicroTestDevelopDao;
        public MDMicroTestDevelopDaoTest()
        {
            MDMicroTestDevelopDao = context.GetObject("MDMicroTestDevelopDao") as IDMDMicroTestDevelopDao;
        }

        [TestMethod]
        public void TestSave()
        {
            MDMicroTestDevelop entity = new MDMicroTestDevelop();
            entity.Id = longGUID;
            entity.Id = longGUID;
            entity.BPrint = true;
            entity.UserNo = 1;
            entity.Id = longGUID;
            entity.Operator = "测试";
            entity.UserNo2 = 1;
            entity.Checker = "测试";
            entity.AlarmLevel = 1;
            entity.ReportValue = "测试";
            entity.ResultStatus = "测试";
            entity.ResultDesc = "测试";
            entity.ZDY1 = "测试";
            entity.ZDY2 = "测试";
            entity.ZDY3 = "测试";
            entity.EquipSampleNo = "测试";
            entity.LotNo = "测试";
            entity.MessageSend = 1;
            entity.Id = longGUID;
            bool b = MDMicroTestDevelopDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            MDMicroTestDevelop entity = MDMicroTestDevelopDao.Get(longGUID);

            bool b = MDMicroTestDevelopDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            MDMicroTestDevelop entity = MDMicroTestDevelopDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = MDMicroTestDevelopDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}