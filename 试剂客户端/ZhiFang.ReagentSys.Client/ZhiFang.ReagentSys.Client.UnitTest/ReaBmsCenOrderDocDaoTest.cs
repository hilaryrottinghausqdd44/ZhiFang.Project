
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
    public class ReaBmsCenOrderDocDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaBmsCenOrderDocDao ReaBmsCenOrderDocDao;
        public ReaBmsCenOrderDocDaoTest()
        {
            ReaBmsCenOrderDocDao = context.GetObject("ReaBmsCenOrderDocDao") as IDReaBmsCenOrderDocDao;
        }

        [TestMethod]
        public void TestSave()
        {
            ReaBmsCenOrderDoc entity = new ReaBmsCenOrderDoc();
            entity.LabcName = "测试";
            entity.Id = longGUID;
            entity.OrderDocNo = "测试";
            entity.StID = 1;
            entity.StName = "测试";
            entity.CompanyName = "测试";
            entity.ReaCompanyName = "测试";
            entity.ReaServerCompCode = "测试";
            entity.UserName = "测试";
            entity.UrgentFlag = 1;
            entity.UrgentFlagName = "测试";
            entity.Status = 1;
            entity.StatusName = "测试";
            entity.PrintTimes = 1;
            entity.IOFlag = 1;
            entity.Memo = "测试";
            entity.LabMemo = "测试";
            entity.CompMemo = "测试";
            entity.Confirm = "测试";
            entity.Checker = "测试";
            entity.IsThirdFlag = 1;
            entity.Sender = "测试";
            entity.DeleteFlag = 1;
            entity.CheckMemo = "测试";
            entity.DeptName = "测试";
            entity.ReaServerLabcCode = "测试";
            entity.IsVerifyProdGoodsNo = true;
            entity.ReaCompCode = "测试";
            bool b = ReaBmsCenOrderDocDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaBmsCenOrderDoc entity = ReaBmsCenOrderDocDao.Get(longGUID);

            bool b = ReaBmsCenOrderDocDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaBmsCenOrderDoc entity = ReaBmsCenOrderDocDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaBmsCenOrderDocDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}