
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
    public class ReaStorageDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaStorageDao ReaStorageDao;
        public ReaStorageDaoTest()
        {
            ReaStorageDao = context.GetObject("ReaStorageDao") as IDReaStorageDao;
        }

        [TestMethod]
        public void TestSave()
        {
            ReaStorage entity = new ReaStorage();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.ShortCode = "测试";
            entity.Memo = "测试";
            entity.ZX1 = "测试";
            entity.ZX2 = "测试";
            entity.ZX3 = "测试";
            entity.DispOrder = 1;
            entity.Visible = true;
            entity.CreaterName = "测试";
            entity.IsMainStorage = true;
            bool b = ReaStorageDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaStorage entity = ReaStorageDao.Get(longGUID);

            bool b = ReaStorageDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaStorage entity = ReaStorageDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaStorageDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}