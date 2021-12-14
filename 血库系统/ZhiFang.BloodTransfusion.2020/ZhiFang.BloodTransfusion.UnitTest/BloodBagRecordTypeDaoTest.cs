
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
    public class BloodBagRecordTypeDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBagRecordTypeDao BloodBagRecordTypeDao;
        public BloodBagRecordTypeDaoTest()
        {
            BloodBagRecordTypeDao = context.GetObject("BloodBagRecordTypeDao") as IDBloodBagRecordTypeDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodBagRecordType entity = new BloodBagRecordType();
            entity.Id = longGUID;
            entity.ContentTypeID = 1;
            entity.CName = "测试";
            entity.TypeCode = "测试";
            entity.DispOrder = 1;
            entity.Memo = "测试";
            entity.IsUse = true;
            bool b = BloodBagRecordTypeDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodBagRecordType entity = BloodBagRecordTypeDao.Get(longGUID);

            bool b = BloodBagRecordTypeDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodBagRecordType entity = BloodBagRecordTypeDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodBagRecordTypeDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}