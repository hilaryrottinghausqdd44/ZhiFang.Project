
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.BloodTransfusion.UnitTest
{
    [TestClass]
    public class SCRecordTypeItemDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDSCRecordTypeItemDao SCRecordTypeItemDao;
        public SCRecordTypeItemDaoTest()
        {
            SCRecordTypeItemDao = context.GetObject("SCRecordTypeItemDao") as IDSCRecordTypeItemDao;
        }

        [TestMethod]
        public void TestSave()
        {
            SCRecordTypeItem entity = new SCRecordTypeItem();
            entity.Id = longGUID;
            entity.ItemCode = "测试";
            entity.CName = "测试";
            entity.SName = "测试";
            entity.ShortCode = "测试";
            entity.PinYinZiTou = "测试";
            entity.ItemEditInfo = "测试";
            entity.Description = "测试";
            entity.Memo = "测试";
            entity.DispOrder = 1;
            entity.IsUse = true;
            bool b = SCRecordTypeItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            SCRecordTypeItem entity = SCRecordTypeItemDao.Get(longGUID);

            bool b = SCRecordTypeItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            SCRecordTypeItem entity = SCRecordTypeItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = SCRecordTypeItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}