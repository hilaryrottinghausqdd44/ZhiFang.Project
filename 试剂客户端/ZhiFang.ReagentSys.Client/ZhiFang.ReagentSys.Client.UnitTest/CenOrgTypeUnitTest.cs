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
    public class CenOrgTypeUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDCenOrgTypeDao CenOrgTypeDao;

        public CenOrgTypeUnitTest()
        {
            CenOrgTypeDao = context.GetObject("CenOrgTypeDao") as IDCenOrgTypeDao;
        }
        //新增
        [TestMethod]
        public void CenOrgTypeTestAdd()
        {
            SysPublicSet.IsSetLicense = true;
            CenOrgType entity = new CenOrgType();
            entity.Id = longGUID;
            entity.CName = "10001DD2QQ1";
            entity.EName = "DDDD";
            entity.DataUpdateTime = DateTime.Now;
            //entity.GoodsID = 4623513945469361870;
            entity.Memo = "测试";
            bool b = CenOrgTypeDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void CenOrgTypeTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            CenOrgType entity = CenOrgTypeDao.Get(longGUID);
            entity.EName = "100012QQW11";
            entity.Memo = "测试DDD";
            entity.DataUpdateTime = DateTime.Now;

            bool b = CenOrgTypeDao.Update(entity);
            Assert.AreEqual(true, b);
            //int b = CenOrgTypeDao.UpdateByHql("update CenOrgType  set CName='测试2' where Id=" + longGUID);
            //Assert.AreEqual(1, b);
        }
        //查
        [TestMethod]
        public void CenOrgTypeTestGet()
        {
            SysPublicSet.IsSetLicense = true;
            CenOrgType entity = CenOrgTypeDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        [TestMethod]
        public void CenOrgTypeTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = CenOrgTypeDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
    }
}
