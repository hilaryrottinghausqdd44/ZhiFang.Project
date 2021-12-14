
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
    public class BloodClassDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodClassDao BloodClassDao;
        public BloodClassDaoTest()
        {
            BloodClassDao = context.GetObject("BloodClassDao") as IDBloodClassDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodClass entity = new BloodClass();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.ShortCode = "测试";
            entity.BCCode = "测试";
            entity.SName = "测试";
            entity.PinYinZiTou = "测试";
            entity.IsLargeUse = true;
            entity.IsUse = true;
            entity.DispOrder = 1;
            bool b = BloodClassDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodClass entity = BloodClassDao.Get(longGUID);

            bool b = BloodClassDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodClass entity = BloodClassDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodClassDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}