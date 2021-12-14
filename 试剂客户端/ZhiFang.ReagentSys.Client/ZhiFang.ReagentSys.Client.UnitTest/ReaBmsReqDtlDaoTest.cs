
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
    public class ReaBmsReqDtlDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaBmsReqDtlDao ReaBmsReqDtlDao;
        public ReaBmsReqDtlDaoTest()
        {
            ReaBmsReqDtlDao = context.GetObject("ReaBmsReqDtlDao") as IDReaBmsReqDtlDao;
        }

        [TestMethod]
        public void TestSave()
        {
            ReaBmsReqDtl entity = new ReaBmsReqDtl();
            entity.Id = longGUID;
            entity.ReqDtlNo = "测试";
            entity.ReqDocNo = "测试";
            entity.GoodsCName = "测试";
            entity.GoodsUnit = "测试";
            entity.OrderFlag = 1;
            entity.OrderDtlNo = "测试";
            entity.ZX1 = "测试";
            entity.ZX2 = "测试";
            entity.ZX3 = "测试";
            entity.OrgName = "测试";
            entity.DispOrder = 1;
            entity.Memo = "测试";
            entity.Visible = true;
            entity.CreaterName = "测试";
            entity.OrderCheckMemo = "测试";
            entity.ReaGoodsNo = "测试";
            entity.CenOrgGoodsNo = "测试";
            entity.ProdOrgName = "测试";
            entity.UnitMemo = "测试";
            bool b = ReaBmsReqDtlDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaBmsReqDtl entity = ReaBmsReqDtlDao.Get(longGUID);

            bool b = ReaBmsReqDtlDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaBmsReqDtl entity = ReaBmsReqDtlDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaBmsReqDtlDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}