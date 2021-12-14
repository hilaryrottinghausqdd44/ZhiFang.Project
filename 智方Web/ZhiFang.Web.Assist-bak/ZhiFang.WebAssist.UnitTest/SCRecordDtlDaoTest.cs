
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
    public class SCRecordDtlDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDSCRecordDtlDao SCRecordDtlDao;
        public SCRecordDtlDaoTest()
        {
            SCRecordDtlDao = context.GetObject("SCRecordDtlDao") as IDSCRecordDtlDao;
        }

        [TestMethod]
        public void TestSave()
        {
            SCRecordDtl entity = new SCRecordDtl();
            entity.Id = longGUID;
            entity.RecordDtlNo = "测试";
            entity.ItemResult = "测试";
            entity.Visible = true;
            entity.Memo = "测试";
            entity.DispOrder = 1;
            bool b = SCRecordDtlDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            SCRecordDtl entity = SCRecordDtlDao.Get(longGUID);

            bool b = SCRecordDtlDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            SCRecordDtl entity = SCRecordDtlDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = SCRecordDtlDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}