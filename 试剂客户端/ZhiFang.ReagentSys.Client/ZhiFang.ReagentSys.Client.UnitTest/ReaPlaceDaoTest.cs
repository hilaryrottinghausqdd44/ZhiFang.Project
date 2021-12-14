
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
    public class ReaPlaceDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaPlaceDao ReaPlaceDao;
        public ReaPlaceDaoTest()
        {
            ReaPlaceDao = context.GetObject("ReaPlaceDao") as IDReaPlaceDao;
        }

        [TestMethod]
        public void TestSave()
        {
            ReaPlace entity = new ReaPlace();
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
            bool b = ReaPlaceDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaPlace entity = ReaPlaceDao.Get(longGUID);

            bool b = ReaPlaceDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaPlace entity = ReaPlaceDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaPlaceDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}