
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
    public class BloodStyleDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodStyleDao BloodStyleDao;
        public BloodStyleDaoTest()
        {
            BloodStyleDao = context.GetObject("BloodStyleDao") as IDBloodStyleDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodStyle entity = new BloodStyle();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.SName = "测试";
            entity.ShortCode = "测试";
            entity.PinYinZiTou = "测试";
            entity.IsMakeBlood = true;
            entity.HisDispCode = "测试";
            entity.BisDispCode = "测试";
            entity.HisOrderCode = "测试";
            entity.DispOrder = 1;
            entity.IsUse = true;
            bool b = BloodStyleDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodStyle entity = BloodStyleDao.Get(longGUID);

            bool b = BloodStyleDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodStyle entity = BloodStyleDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodStyleDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}