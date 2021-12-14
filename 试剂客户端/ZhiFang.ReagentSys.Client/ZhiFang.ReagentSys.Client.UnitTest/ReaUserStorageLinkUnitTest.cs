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
    public class ReaUserStorageLinkUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaUserStorageLinkDao ReaUserStorageLinkDao;

        public ReaUserStorageLinkUnitTest()
        {
            ReaUserStorageLinkDao = context.GetObject("ReaUserStorageLinkDao") as IDReaUserStorageLinkDao;
        }
        //新增
        [TestMethod]
        public void ReaUserStorageLinkTestAdd()
        {
            SysPublicSet.IsSetLicense = true;
            ReaUserStorageLink entity = new ReaUserStorageLink();
            entity.Id = longGUID;
            entity.PlaceName = "10001DD2QQ1";
            entity.DataUpdateTime = DateTime.Now;
            //entity.GoodsID = 4623513945469361870;
            entity.Memo = "测试";
            bool b = ReaUserStorageLinkDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void ReaUserStorageLinkTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            ReaUserStorageLink entity = ReaUserStorageLinkDao.Get(longGUID);
            entity.Memo = "测试DDD";
            entity.DataUpdateTime = DateTime.Now;

            bool b = ReaUserStorageLinkDao.Update(entity);
            Assert.AreEqual(true, b);
            //int b = ReaUserStorageLinkDao.UpdateByHql("update ReaUserStorageLink  set CName='测试2' where Id=" + longGUID);
            //Assert.AreEqual(1, b);
        }
        //查
        [TestMethod]
        public void ReaUserStorageLinkTestGet()
        {
            SysPublicSet.IsSetLicense = true;
            ReaUserStorageLink entity = ReaUserStorageLinkDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        [TestMethod]
        public void ReaUserStorageLinkTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = ReaUserStorageLinkDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
    }
}
