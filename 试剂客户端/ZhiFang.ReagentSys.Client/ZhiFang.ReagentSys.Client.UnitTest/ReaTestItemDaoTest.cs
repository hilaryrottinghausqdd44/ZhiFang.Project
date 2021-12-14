
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
    public class ReaTestItemDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaTestItemDao ReaTestItemDao;
        public ReaTestItemDaoTest()
        {
            ReaTestItemDao = context.GetObject("ReaTestItemDao") as IDReaTestItemDao;
        }

        [TestMethod]
        public void TestSave()
        {
            ReaTestItem entity = new ReaTestItem();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.EName = "测试";
            entity.ShortCode = "测试";
            entity.DispOrder = 1;
            entity.Visible = 1;
            entity.LisCode = "测试";
            entity.ZX1 = "测试";
            entity.ZX2 = "测试";
            entity.ZX3 = "测试";
            entity.Memo = "测试";
            bool b = ReaTestItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaTestItem entity = ReaTestItemDao.Get(longGUID);

            bool b = ReaTestItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaTestItem entity = ReaTestItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaTestItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}