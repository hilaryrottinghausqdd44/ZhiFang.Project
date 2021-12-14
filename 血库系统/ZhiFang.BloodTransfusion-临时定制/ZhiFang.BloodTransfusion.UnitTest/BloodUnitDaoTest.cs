
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
    public class BloodUnitDaoTest
    {
        static int longGUID = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodUnitDao BloodUnitDao;
        public BloodUnitDaoTest()
        {
            BloodUnitDao = context.GetObject("BloodUnitDao") as IDBloodUnitDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodUnit entity = new BloodUnit();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.HisDispCode = "测试";
            entity.ShortCode = "测试";
            entity.DispOrder = "测试";
            entity.Visible = true;
            entity.EName = "测试";
            entity.BloodScaleUnit = "测试";
            bool b = BloodUnitDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodUnit entity = BloodUnitDao.Get(longGUID);

            bool b = BloodUnitDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodUnit entity = BloodUnitDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodUnitDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}