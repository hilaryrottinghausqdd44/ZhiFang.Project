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
    public class ReaCenOrgUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaCenOrgDao ReaCenOrgDao;

        public ReaCenOrgUnitTest()
        {
            ReaCenOrgDao = context.GetObject("ReaCenOrgDao") as IDReaCenOrgDao;
        }
        //新增
        [TestMethod]
        public void ReaCenOrgTestAdd()
        {
            SysPublicSet.IsSetLicense = true;
            ReaCenOrg entity = new ReaCenOrg();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.ShortCode = "测试";
            entity.Memo = "123";
            entity.OrgNo = 612231758;
            entity.OrgType = 1;
            entity.Visible = 1;

            bool b = ReaCenOrgDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void ReaCenOrgTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            ReaCenOrg entity = ReaCenOrgDao.Get(longGUID);
            entity.CName = "测试2";
            entity.ShortCode = "Q22";
            entity.Memo = "3333";
            bool b = ReaCenOrgDao.Update(entity);
            Assert.AreEqual(true, b);
            //int b = ReaCenOrgDao.UpdateByHql("update ReaCenOrg  set CName='测试2' where Id=" + longGUID);
            //Assert.AreEqual(1, b);
        }
        //查
        [TestMethod]
        public void ReaCenOrgTestGet()
        {
            SysPublicSet.IsSetLicense = true;
            ReaCenOrg entity = ReaCenOrgDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        [TestMethod]
        public void ReaCenOrgTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = ReaCenOrgDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}