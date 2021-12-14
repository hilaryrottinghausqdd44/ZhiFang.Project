
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.WebAssist.UnitTest
{
    [TestClass]
    public class ChargeTypeDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDChargeTypeDao ChargeTypeDao;
        public ChargeTypeDaoTest()
        {
            ChargeTypeDao = context.GetObject("ChargeTypeDao") as IDChargeTypeDao;
        }

        [TestMethod]
        public void TestSave()
        {
            ChargeType entity = new ChargeType();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.ChargeTypeDesc = "测试";
            entity.ShortCode = "测试";
            entity.Visible = 1;
            entity.DispOrder = 1;
            entity.HisOrderCode = "测试";
            entity.Code1 = "测试";
            entity.Code2 = "测试";
            entity.Code3 = "测试";
            entity.Code4 = "测试";
            entity.Code5 = "测试";
            entity.Code6 = "测试";
            entity.Code7 = "测试";
            entity.Code8 = "测试";
            bool b = ChargeTypeDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ChargeType entity = ChargeTypeDao.Get(longGUID);

            bool b = ChargeTypeDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ChargeType entity = ChargeTypeDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ChargeTypeDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}