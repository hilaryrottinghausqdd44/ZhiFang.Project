
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
    public class ReaBmsReqDocDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaBmsReqDocDao ReaBmsReqDocDao;
        public ReaBmsReqDocDaoTest()
        {
            ReaBmsReqDocDao = context.GetObject("ReaBmsReqDocDao") as IDReaBmsReqDocDao;
        }

        [TestMethod]
        public void TestSave()
        {
            ReaBmsReqDoc entity = new ReaBmsReqDoc();
            entity.Id = longGUID;
            entity.ReqDocNo = "测试";
            entity.UrgentFlag = 1;
            entity.Status = 1;
            entity.PrintTimes = 1;
            entity.ZX1 = "测试";
            entity.ZX2 = "测试";
            entity.ZX3 = "测试";
            entity.DispOrder = 1;
            entity.Memo = "测试";
            entity.Visible = true;
            entity.ApplyName = "测试";
            entity.ReviewManName = "测试";
            entity.DeptName = "测试";
            entity.ReviewMemo = "测试";
            entity.LabcName = "测试";
            entity.ReaServerLabcCode = "测试";
            bool b = ReaBmsReqDocDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaBmsReqDoc entity = ReaBmsReqDocDao.Get(longGUID);

            bool b = ReaBmsReqDocDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaBmsReqDoc entity = ReaBmsReqDocDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaBmsReqDocDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}