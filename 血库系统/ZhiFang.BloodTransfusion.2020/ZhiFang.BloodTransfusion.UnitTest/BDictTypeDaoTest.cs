
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
    public class BDictTypeDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBDictTypeDao BDictTypeDao;
        public BDictTypeDaoTest()
        {
            BDictTypeDao = context.GetObject("BDictTypeDao") as IDBDictTypeDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BDictType entity = new BDictType();
            entity.Id = longGUID;
            entity.DictTypeCode = "测试";
            entity.CName = "测试";
            entity.SName = "测试";
            entity.ShortCode = "测试";
            entity.PinYinZiTou = "测试";
            entity.IsUse = true;
            entity.DispOrder = 1;
            entity.Memo = "测试";
            bool b = BDictTypeDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BDictType entity = BDictTypeDao.Get(longGUID);

            bool b = BDictTypeDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BDictType entity = BDictTypeDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BDictTypeDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}