
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
    public class BloodLargeUseFormDaoTest
    {
        static string longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodLargeUseFormDao BloodLargeUseFormDao;
        public BloodLargeUseFormDaoTest()
        {
            BloodLargeUseFormDao = context.GetObject("BloodLargeUseFormDao") as IDBloodLargeUseFormDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodLargeUseForm entity = new BloodLargeUseForm();
            entity.Id = longGUID;
            entity.LogID = "测试";
            entity.LUFMemo = "测试";
            entity.DispOrder = 1;
            entity.Visible = true;
            bool b = BloodLargeUseFormDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodLargeUseForm entity = BloodLargeUseFormDao.Get(longGUID);

            bool b = BloodLargeUseFormDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodLargeUseForm entity = BloodLargeUseFormDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodLargeUseFormDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}