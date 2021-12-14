
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
    public class ReaBmsTransferDtlDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaBmsTransferDtlDao ReaBmsTransferDtlDao;
        public ReaBmsTransferDtlDaoTest()
        {
            ReaBmsTransferDtlDao = context.GetObject("ReaBmsTransferDtlDao") as IDReaBmsTransferDtlDao;
        }

        [TestMethod]
        public void TestSave()
        {
            ReaBmsTransferDtl entity = new ReaBmsTransferDtl();
            entity.Id = longGUID;
            entity.QtyDtlID = "测试";
            entity.GoodsCName = "测试";
            entity.SerialNo = "测试";
            entity.GoodsUnit = "测试";
            entity.ReaCompanyName = "测试";
            entity.LotNo = "测试";
            entity.SStorageName = "测试";
            entity.SPlaceName = "测试";
            entity.DStorageName = "测试";
            entity.DPlaceName = "测试";
            entity.GoodsSerial = "测试";
            entity.LotSerial = "测试";
            entity.ZX1 = "测试";
            entity.ZX2 = "测试";
            entity.ZX3 = "测试";
            entity.DispOrder = 1;
            entity.Memo = "测试";
            entity.Visible = true;
            entity.CreaterName = "测试";
            entity.GoodsNo = "测试";
            entity.ReaServerCompCode = "测试";
            entity.SysLotSerial = "测试";
            entity.ProdGoodsNo = "测试";
            entity.ReaGoodsNo = "测试";
            entity.CenOrgGoodsNo = "测试";
            entity.LotQRCode = "测试";
            entity.UnitMemo = "测试";
            entity.ReaCompCode = "测试";
            entity.GoodsSort = 1;
            entity.SQtyDtlID = "测试";
            bool b = ReaBmsTransferDtlDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaBmsTransferDtl entity = ReaBmsTransferDtlDao.Get(longGUID);

            bool b = ReaBmsTransferDtlDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaBmsTransferDtl entity = ReaBmsTransferDtlDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaBmsTransferDtlDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}