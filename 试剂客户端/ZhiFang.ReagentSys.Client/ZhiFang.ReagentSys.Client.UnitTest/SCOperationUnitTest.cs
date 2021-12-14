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
    public class SCOperationUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDSCOperationDao SCOperationDao;

        public SCOperationUnitTest()
        {
            SCOperationDao = context.GetObject("SCOperationDao") as IDSCOperationDao;
        }
        //新增
        [TestMethod]
        public void SCOperationTestAdd()
        {
            SysPublicSet.IsSetLicense = true;
            SCOperation entity = new SCOperation();
            entity.Id = longGUID;
            entity.TypeName = "10001DD2QQ1";
            entity.DataUpdateTime = DateTime.Now;
            //entity.GoodsID = 4623513945469361870;
            entity.Memo = "测试";
            bool b = SCOperationDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void SCOperationTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            SCOperation entity = SCOperationDao.Get(longGUID);
            entity.Memo = "测试DDD";
            entity.DataUpdateTime = DateTime.Now;

            bool b = SCOperationDao.Update(entity);
            Assert.AreEqual(true, b);
            //int b = SCOperationDao.UpdateByHql("update SCOperation  set CName='测试2' where Id=" + longGUID);
            //Assert.AreEqual(1, b);
        }
        //查
        [TestMethod]
        public void SCOperationTestGet()
        {
            SysPublicSet.IsSetLicense = true;
            SCOperation entity = SCOperationDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        [TestMethod]
        public void SCOperationTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = SCOperationDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
    }
}
