
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
    public class SCRecordItemLinkDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDSCRecordItemLinkDao SCRecordItemLinkDao;
        public SCRecordItemLinkDaoTest()
        {
            SCRecordItemLinkDao = context.GetObject("SCRecordItemLinkDao") as IDSCRecordItemLinkDao;
        }

        [TestMethod]
        public void TestSave()
        {
            SCRecordItemLink entity = new SCRecordItemLink();
            entity.Id = longGUID;
            entity.DispOrder = 1;
            entity.IsUse = true;
            bool b = SCRecordItemLinkDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            SCRecordItemLink entity = SCRecordItemLinkDao.Get(longGUID);

            bool b = SCRecordItemLinkDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            SCRecordItemLink entity = SCRecordItemLinkDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = SCRecordItemLinkDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}