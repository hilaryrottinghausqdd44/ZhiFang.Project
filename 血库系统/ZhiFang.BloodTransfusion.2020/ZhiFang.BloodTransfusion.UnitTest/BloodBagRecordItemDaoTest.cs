
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
    public class BloodBagRecordItemDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBagRecordItemDao BloodBagRecordItemDao;
        public BloodBagRecordItemDaoTest()
        {
            BloodBagRecordItemDao = context.GetObject("BloodBagRecordItemDao") as IDBloodBagRecordItemDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodBagRecordItem entity = new BloodBagRecordItem();
            entity.Id = longGUID;
            entity.ItemCode = "测试";
            entity.CName = "测试";
            entity.SName = "测试";
            entity.ShortCode = "测试";
            entity.PinYinZiTou = "测试";
            entity.ItemEditInfo = "测试";
            entity.DispOrder = 1;
            entity.IsUse = true;
            bool b = BloodBagRecordItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodBagRecordItem entity = BloodBagRecordItemDao.Get(longGUID);

            bool b = BloodBagRecordItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodBagRecordItem entity = BloodBagRecordItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodBagRecordItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}