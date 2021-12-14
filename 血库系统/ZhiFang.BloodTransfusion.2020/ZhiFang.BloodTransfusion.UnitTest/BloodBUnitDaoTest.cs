
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
    public class BloodBUnitDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBUnitDao BloodBUnitDao;
        public BloodBUnitDaoTest()
        {
            BloodBUnitDao = context.GetObject("BloodBUnitDao") as IDBloodBUnitDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodBUnit entity = new BloodBUnit();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.SName = "测试";
            entity.EName = "测试";
            entity.ShortCode = "测试";
            entity.PinYinZiTou = "测试";
            entity.DispOrder = 1;
            entity.IsUse = true;
            bool b = BloodBUnitDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodBUnit entity = BloodBUnitDao.Get(longGUID);

            bool b = BloodBUnitDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodBUnit entity = BloodBUnitDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodBUnitDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}