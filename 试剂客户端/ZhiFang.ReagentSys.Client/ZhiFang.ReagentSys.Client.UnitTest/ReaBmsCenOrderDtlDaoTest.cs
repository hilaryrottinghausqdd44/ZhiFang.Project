
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
    public class ReaBmsCenOrderDtlDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaBmsCenOrderDtlDao ReaBmsCenOrderDtlDao;
        public ReaBmsCenOrderDtlDaoTest()
        {
            ReaBmsCenOrderDtlDao = context.GetObject("ReaBmsCenOrderDtlDao") as IDReaBmsCenOrderDtlDao;
        }

        [TestMethod]
        public void TestSave()
        {
            ReaBmsCenOrderDtl entity = new ReaBmsCenOrderDtl();
            entity.Id = longGUID;
            entity.OrderDtlNo = "测试";
            entity.OrderDocNo = "测试";
            entity.ProdGoodsNo = "测试";
            entity.ProdOrgName = "测试";
            entity.GoodsName = "测试";
            entity.GoodsUnit = "测试";
            entity.UnitMemo = "测试";
            entity.IOFlag = 1;
            entity.ZX1 = "测试";
            entity.ZX2 = "测试";
            entity.ZX3 = "测试";
            entity.ReaGoodsName = "测试";
            entity.GoodsNo = "测试";
            entity.DeleteFlag = 1;
            entity.Memo = "测试";
            entity.LabMemo = "测试";
            entity.CompMemo = "测试";
            entity.OtherOrderDocNo = "测试";
            entity.ProdOrgNo = "测试";
            entity.ReaGoodsNo = "测试";
            entity.CenOrgGoodsNo = "测试";
            entity.GoodsSort = 1;
            bool b = ReaBmsCenOrderDtlDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaBmsCenOrderDtl entity = ReaBmsCenOrderDtlDao.Get(longGUID);

            bool b = ReaBmsCenOrderDtlDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaBmsCenOrderDtl entity = ReaBmsCenOrderDtlDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaBmsCenOrderDtlDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}