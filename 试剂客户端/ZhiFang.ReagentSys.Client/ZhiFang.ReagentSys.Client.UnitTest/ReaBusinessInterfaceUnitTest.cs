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
    public class ReaBusinessInterfaceUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaBusinessInterfaceDao ReaBusinessInterfaceDao;

        public ReaBusinessInterfaceUnitTest()
        {
            ReaBusinessInterfaceDao = context.GetObject("ReaBusinessInterfaceDao") as IDReaBusinessInterfaceDao;
        }
        //新增
        [TestMethod]
        public void ReaBusinessInterfaceTestAdd()
        {
            SysPublicSet.IsSetLicense = true;
            ReaBusinessInterface entity = new ReaBusinessInterface();
            entity.Id = longGUID;
            entity.CName = "10001DD2QQ1";
            entity.InterfaceType = "1";
            entity.DataUpdateTime = DateTime.Now;
            //entity.GoodsID = 4623513945469361870;
            entity.ZX1 = "测试";
            bool b = ReaBusinessInterfaceDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void ReaBusinessInterfaceTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            ReaBusinessInterface entity = ReaBusinessInterfaceDao.Get(longGUID);
            entity.CName = "10001W2QQW11";
            entity.ZX1 = "测试DDD";
            entity.DataUpdateTime = DateTime.Now;

            bool b = ReaBusinessInterfaceDao.Update(entity);
            Assert.AreEqual(true, b);
            //int b = ReaBusinessInterfaceDao.UpdateByHql("update ReaBusinessInterface  set CName='测试2' where Id=" + longGUID);
            //Assert.AreEqual(1, b);
        }
        //查
        [TestMethod]
        public void ReaBusinessInterfaceTestGet()
        {
            SysPublicSet.IsSetLicense = true;
            ReaBusinessInterface entity = ReaBusinessInterfaceDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        [TestMethod]
        public void ReaBusinessInterfaceTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = ReaBusinessInterfaceDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
    }
}
