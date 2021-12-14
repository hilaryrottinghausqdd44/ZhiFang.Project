
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.WebAssist.UnitTest
{
    [TestClass]
    public class SCRecordTypeDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDSCRecordTypeDao SCRecordTypeDao;
        public SCRecordTypeDaoTest()
        {
            SCRecordTypeDao = context.GetObject("SCRecordTypeDao") as IDSCRecordTypeDao;
        }

        [TestMethod]
        public void TestSave()
        {
            SCRecordType entity = new SCRecordType();
            entity.Id = longGUID;
            entity.ContentTypeID = 1;
            entity.CName = "测试";
            entity.TypeCode = "测试";
            entity.SName = "测试";
            entity.ShortCode = "测试";
            entity.PinYinZiTou = "测试";
            entity.DispOrder = 1;
            entity.Description = "测试";
            entity.Memo = "测试";
            entity.IsUse = true;
            bool b = SCRecordTypeDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            SCRecordType entity = SCRecordTypeDao.Get(longGUID);

            bool b = SCRecordTypeDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            SCRecordType entity = SCRecordTypeDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = SCRecordTypeDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}