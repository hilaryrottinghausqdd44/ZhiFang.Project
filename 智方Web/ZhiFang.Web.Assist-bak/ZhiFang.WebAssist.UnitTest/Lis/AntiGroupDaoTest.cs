
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
    public class AntiGroupDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDAntiGroupDao AntiGroupDao;
        public AntiGroupDaoTest()
        {
            AntiGroupDao = context.GetObject("AntiGroupDao") as IDAntiGroupDao;
        }

        [TestMethod]
        public void TestSave()
        {
            AntiGroup entity = new AntiGroup();
            entity.FamilyNo = 1;
            entity.UseTypeNo = 1;
            entity.AntiNo = 1;
            entity.RangeNo = 1;
            entity.AntiUnit = "测试";
            entity.Range = "测试";
            entity.DispOrder = 1;
            entity.IsUse = 1;
            entity.SusCept = "测试";
            entity.MicroNo = 1;
            entity.SusDesc = "测试";
            entity.Anticode = "测试";
            entity.Antigrouptype = "测试";
            entity.OftenUse = 1;
            bool b = AntiGroupDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            AntiGroup entity = AntiGroupDao.Get(longGUID);

            bool b = AntiGroupDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            AntiGroup entity = AntiGroupDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = AntiGroupDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}