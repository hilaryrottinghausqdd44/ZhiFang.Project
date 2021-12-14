
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
    public class BLodopTempletDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBLodopTempletDao BLodopTempletDao;
        public BLodopTempletDaoTest()
        {
            BLodopTempletDao = context.GetObject("BLodopTempletDao") as IDBLodopTempletDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BLodopTemplet entity = new BLodopTemplet();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.TypeCode = "测试";
            entity.TypeCName = "测试";
            entity.PaperType = "测试";
            entity.PrintingDirection = "测试";
            entity.PaperUnit = "测试";
            entity.TemplateCode = "测试";
            entity.DataTestValue = "测试";
            entity.DispOrder = 1;
            entity.Memo = "测试";
            entity.IsUse = true;
            bool b = BLodopTempletDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BLodopTemplet entity = BLodopTempletDao.Get(longGUID);

            bool b = BLodopTempletDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BLodopTemplet entity = BLodopTempletDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BLodopTempletDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}