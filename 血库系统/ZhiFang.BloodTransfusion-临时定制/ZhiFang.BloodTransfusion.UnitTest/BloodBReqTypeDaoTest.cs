
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
    public class BloodBReqTypeDaoTest
    {
        static int longGUID = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBReqTypeDao BloodBReqTypeDao;
        public BloodBReqTypeDaoTest()
        {
            BloodBReqTypeDao = context.GetObject("BloodBReqTypeDao") as IDBloodBReqTypeDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodBReqType entity = new BloodBReqType();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.Shortcode = "测试";
            entity.DispOrder = 1;
            bool b = BloodBReqTypeDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodBReqType entity = BloodBReqTypeDao.Get(longGUID);

            bool b = BloodBReqTypeDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodBReqType entity = BloodBReqTypeDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodBReqTypeDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}