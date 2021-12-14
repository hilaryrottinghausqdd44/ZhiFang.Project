
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
    public class BloodBReqTypeItemDaoTest
    {
        static string longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBReqTypeItemDao BloodBReqTypeItemDao;
        public BloodBReqTypeItemDaoTest()
        {
            BloodBReqTypeItemDao = context.GetObject("BloodBReqTypeItemDao") as IDBloodBReqTypeItemDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodBReqTypeItem entity = new BloodBReqTypeItem();
            entity.Id = longGUID;
            entity.BReqTypeItem = "测试";
            entity.DispOrder = 1;
            bool b = BloodBReqTypeItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodBReqTypeItem entity = BloodBReqTypeItemDao.Get(longGUID);

            bool b = BloodBReqTypeItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodBReqTypeItem entity = BloodBReqTypeItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodBReqTypeItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}