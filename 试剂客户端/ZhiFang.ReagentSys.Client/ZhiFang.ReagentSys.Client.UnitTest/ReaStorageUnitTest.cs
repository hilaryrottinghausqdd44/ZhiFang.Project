using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.ReagentSys.Client.UnitTest
{
    [TestClass]
    public class ReaStorageUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaStorageDao ReaStorageDao;

        public ReaStorageUnitTest()
        {
            ReaStorageDao = context.GetObject("ReaStorageDao") as IDReaStorageDao;
        }
        //新增
        [TestMethod]
        public void ReaStorageTestAdd()
        {
            SysPublicSet.IsSetLicense = true;
            ReaStorage entity = new ReaStorage();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.ShortCode = "测试";
            entity.Memo = "123";
            entity.DataUpdateTime = DateTime.Now;
            bool b = ReaStorageDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void ReaStorageTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            ReaStorage entity = ReaStorageDao.Get(longGUID);
            entity.CName = "测试2";
            entity.ShortCode = "Q22";
            entity.Memo = "3333";
            bool b = ReaStorageDao.Update(entity);
            Assert.AreEqual(true, b);
            //int b = ReaStorageDao.UpdateByHql("update ReaStorage  set CName='测试2' where Id=" + longGUID);
            //Assert.AreEqual(1, b);
        }
        //查
        [TestMethod]
        public void ReaStorageTestGet()
        {
            SysPublicSet.IsSetLicense = true;
            ReaStorage entity = ReaStorageDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        [TestMethod]
        public void ReaStorageTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = ReaStorageDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
    }
}
