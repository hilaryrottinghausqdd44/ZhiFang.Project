
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
    public class BloodAggluItemDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodAggluItemDao BloodAggluItemDao;
        public BloodAggluItemDaoTest()
        {
            BloodAggluItemDao = context.GetObject("BloodAggluItemDao") as IDBloodAggluItemDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodAggluItem entity = new BloodAggluItem();
            entity.Id = longGUID;
            entity.AggluItemName = "测试";
            entity.CName = "测试";
            entity.SName = "测试";
            entity.PinYinZiTou = "测试";
            entity.ShortCode = "测试";
            entity.RhPriority = "测试";
            entity.IsUse = true;
            entity.DispOrder = 1;
            bool b = BloodAggluItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodAggluItem entity = BloodAggluItemDao.Get(longGUID);

            bool b = BloodAggluItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodAggluItem entity = BloodAggluItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodAggluItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}