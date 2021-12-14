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
    public class ReaTestEquipLabUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaTestEquipLabDao ReaTestEquipLabDao;

        public ReaTestEquipLabUnitTest()
        {
            ReaTestEquipLabDao = context.GetObject("ReaTestEquipLabDao") as IDReaTestEquipLabDao;
        }
        //新增
        [TestMethod]
        public void ReaTestEquipLabTestAdd()
        {
            SysPublicSet.IsSetLicense = true;
            ReaTestEquipLab entity = new ReaTestEquipLab();
            entity.Id = longGUID;
            entity.CName = "10001DD2QQ1";
            entity.EName = "DDDD";
            entity.DataUpdateTime = DateTime.Now;
            //entity.GoodsID = 4623513945469361870;
            entity.Memo = "测试";
            bool b = ReaTestEquipLabDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void ReaTestEquipLabTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            ReaTestEquipLab entity = ReaTestEquipLabDao.Get(longGUID);
            entity.CName = "10001444DD2QQ1";
            entity.EName = "DDDD";
            entity.Memo = "DDeeeeeeDD";
            entity.DataUpdateTime = DateTime.Now;

            bool b = ReaTestEquipLabDao.Update(entity);
            Assert.AreEqual(true, b);
            //int b = ReaTestEquipLabDao.UpdateByHql("update ReaTestEquipLab  set CName='测试2' where Id=" + longGUID);
            //Assert.AreEqual(1, b);
        }
        //查
        [TestMethod]
        public void ReaTestEquipLabTestGet()
        {
            SysPublicSet.IsSetLicense = true;
            ReaTestEquipLab entity = ReaTestEquipLabDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        [TestMethod]
        public void ReaTestEquipLabTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = ReaTestEquipLabDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
    }
}
