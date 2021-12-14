
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.BloodTransfusion.UnitTest
{
    [TestClass]
    public class BloodABODaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodABODao BloodABODao;
        public BloodABODaoTest()
        {
            BloodABODao = context.GetObject("BloodABODao") as IDBloodABODao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodABO entity = new BloodABO();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.RhEName = "测试";
            entity.ABOCode = "测试";
            entity.ABOType = "测试";
            entity.RHType = "测试";
            entity.ShortCode = "测试";
            entity.HisOrderCode = "测试";
            entity.Memo = "测试";
            entity.DispOrder = 1;
            entity.IsUse = true;
            bool b = BloodABODao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodABO entity = BloodABODao.Get(longGUID);

            bool b = BloodABODao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodABO entity = BloodABODao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodABODao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}