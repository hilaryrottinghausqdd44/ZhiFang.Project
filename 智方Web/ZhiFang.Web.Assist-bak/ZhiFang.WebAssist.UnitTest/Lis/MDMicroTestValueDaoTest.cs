
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
    public class MDMicroTestValueDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDMDMicroTestValueDao MDMicroTestValueDao;
        public MDMicroTestValueDaoTest()
        {
            MDMicroTestValueDao = context.GetObject("MDMicroTestValueDao") as IDMDMicroTestValueDao;
        }

        [TestMethod]
        public void TestSave()
        {
            MDMicroTestValue entity = new MDMicroTestValue();
            entity.Id = longGUID;
            entity.Id = longGUID;
            entity.FName = "测试";
            entity.TestValue = "测试";
            entity.UseCode = "测试";
            entity.BPrint = true;
            entity.AlarmLevel = 1;
            entity.ZX1 = "测试";
            entity.ZX2 = "测试";
            entity.ZX3 = "测试";
            entity.Id = longGUID;
            entity.DispOrder = 1;
            entity.Groups = "测试";
            bool b = MDMicroTestValueDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            MDMicroTestValue entity = MDMicroTestValueDao.Get(longGUID);

            bool b = MDMicroTestValueDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            MDMicroTestValue entity = MDMicroTestValueDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = MDMicroTestValueDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}