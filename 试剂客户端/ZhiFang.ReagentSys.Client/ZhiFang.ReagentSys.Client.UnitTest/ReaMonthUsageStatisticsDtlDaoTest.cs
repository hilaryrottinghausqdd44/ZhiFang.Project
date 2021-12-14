
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
    public class ReaMonthUsageStatisticsDtlDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaMonthUsageStatisticsDtlDao ReaMonthUsageStatisticsDtlDao;
        public ReaMonthUsageStatisticsDtlDaoTest()
        {
            ReaMonthUsageStatisticsDtlDao = context.GetObject("ReaMonthUsageStatisticsDtlDao") as IDReaMonthUsageStatisticsDtlDao;
        }

        [TestMethod]
        public void TestSave()
        {
            ReaMonthUsageStatisticsDtl entity = new ReaMonthUsageStatisticsDtl();
            entity.Id = longGUID;
            entity.GoodsName = "测试";
            entity.GoodsUnit = "测试";
            entity.ProdGoodsNo = "测试";
            entity.ReaGoodsNo = "测试";
            entity.CenOrgGoodsNo = "测试";
            entity.UnitMemo = "测试";
            entity.DispOrder = 1;
            entity.Visible = true;
            entity.DeptName = "测试";
            bool b = ReaMonthUsageStatisticsDtlDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaMonthUsageStatisticsDtl entity = ReaMonthUsageStatisticsDtlDao.Get(longGUID);

            bool b = ReaMonthUsageStatisticsDtlDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaMonthUsageStatisticsDtl entity = ReaMonthUsageStatisticsDtlDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaMonthUsageStatisticsDtlDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}