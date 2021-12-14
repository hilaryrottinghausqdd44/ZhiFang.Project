
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
    public class BloodChargeItemDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodChargeItemDao BloodChargeItemDao;
        public BloodChargeItemDaoTest()
        {
            BloodChargeItemDao = context.GetObject("BloodChargeItemDao") as IDBloodChargeItemDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodChargeItem entity = new BloodChargeItem();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.SName = "测试";
            entity.ShortCode = "测试";
            entity.PinYinZiTou = "测试";
            entity.IsGroup = true;
            entity.HisOrderCode = "测试";
            entity.HisChargeItemUnits = "测试";
            entity.HisPriceDemo = "测试";
            entity.ChargeItemSpec = "测试";
            entity.DispOrder = 1;
            entity.IsUse = true;
            bool b = BloodChargeItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodChargeItem entity = BloodChargeItemDao.Get(longGUID);

            bool b = BloodChargeItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodChargeItem entity = BloodChargeItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodChargeItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}