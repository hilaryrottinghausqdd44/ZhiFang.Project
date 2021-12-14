
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
    public class BloodBReqItemResultDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBReqItemResultDao BloodBReqItemResultDao;
        public BloodBReqItemResultDaoTest()
        {
            BloodBReqItemResultDao = context.GetObject("BloodBReqItemResultDao") as IDBloodBReqItemResultDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodBReqItemResult entity = new BloodBReqItemResult();
            entity.Id = longGUID;
            entity.Id = longGUID;
            entity.PatNo = "测试";
            entity.PatID = "测试";
            entity.Barcode = "测试";
            entity.ItemResult = "测试";
            entity.ItemUnit = "测试";
            entity.DispOrder = 1;
            entity.Visible = true;
            bool b = BloodBReqItemResultDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodBReqItemResult entity = BloodBReqItemResultDao.Get(longGUID);

            bool b = BloodBReqItemResultDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodBReqItemResult entity = BloodBReqItemResultDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodBReqItemResultDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}