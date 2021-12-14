
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
    public class BloodBReqItemDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBReqItemDao BloodBReqItemDao;
        public BloodBReqItemDaoTest()
        {
            BloodBReqItemDao = context.GetObject("BloodBReqItemDao") as IDBloodBReqItemDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodBReqItem entity = new BloodBReqItem();
            entity.Id = longGUID;
            entity.Id = longGUID;
            entity.BloodUnitNo = 1;
            // entity.BloodOrder = 1;
            entity.BPresOutFlag = 1;
            entity.Memo = "测试";
            entity.ZX1 = "测试";
            entity.ZX2 = "测试";
            entity.ZX3 = "测试";
            entity.Auditflag = 1;
            entity.Barcode = "测试";
            entity.B3Code = "测试";
            entity.MainSignID = "测试";
            entity.DeptSignID = "测试";
            entity.BReqTypeItemID = 1;
            entity.BCCode = "测试";
            entity.BCNo = "测试";
            entity.BPreCheckFlag = 1;
            entity.Zlxmbm = "测试";
            bool b = BloodBReqItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodBReqItem entity = BloodBReqItemDao.Get(longGUID);

            bool b = BloodBReqItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodBReqItem entity = BloodBReqItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodBReqItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}