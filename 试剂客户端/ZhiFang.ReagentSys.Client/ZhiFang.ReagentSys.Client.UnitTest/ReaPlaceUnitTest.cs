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
    public class ReaPlaceUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaPlaceDao ReaPlaceDao;

        public ReaPlaceUnitTest()
        {
            ReaPlaceDao = context.GetObject("ReaPlaceDao") as IDReaPlaceDao;
        }
        //新增
        [TestMethod]
        public void ReaPlaceTestAdd()
        {
            SysPublicSet.IsSetLicense = true;
            ReaPlace entity = new ReaPlace();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.ShortCode = "测试";
            entity.Memo = "123";
            entity.DataUpdateTime = DateTime.Now;
            bool b = ReaPlaceDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void ReaPlaceTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            ReaPlace entity = ReaPlaceDao.Get(longGUID);
            entity.CName = "测试2";
            entity.ShortCode = "Q22";
            entity.Memo = "3333";
            bool b = ReaPlaceDao.Update(entity);
            Assert.AreEqual(true, b);
            //int b = ReaPlaceDao.UpdateByHql("update ReaPlace  set CName='测试2' where Id=" + longGUID);
            //Assert.AreEqual(1, b);
        }
        //查
        [TestMethod]
        public void ReaPlaceTestGet()
        {
            SysPublicSet.IsSetLicense = true;
            ReaPlace entity = ReaPlaceDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        [TestMethod]
        public void ReaPlaceTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = ReaPlaceDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
    }
}
