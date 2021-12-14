
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
    public class BloodUseTypeDaoTest
    {
        static string longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodUseTypeDao BloodUseTypeDao;
        public BloodUseTypeDaoTest()
        {
            BloodUseTypeDao = context.GetObject("BloodUseTypeDao") as IDBloodUseTypeDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodUseType entity = new BloodUseType();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.BeforUnit = "测试";
            entity.DispOrder = 1;
            entity.ShortCode = "测试";
            entity.Visible = true;
            bool b = BloodUseTypeDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodUseType entity = BloodUseTypeDao.Get(longGUID);

            bool b = BloodUseTypeDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodUseType entity = BloodUseTypeDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodUseTypeDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}