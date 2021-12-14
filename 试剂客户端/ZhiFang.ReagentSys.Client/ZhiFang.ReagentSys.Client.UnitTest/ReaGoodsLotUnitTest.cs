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
    public class ReaGoodsLotUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaGoodsLotDao ReaGoodsLotDao;

        public ReaGoodsLotUnitTest()
        {
            ReaGoodsLotDao = context.GetObject("ReaGoodsLotDao") as IDReaGoodsLotDao;
        }
        //新增
        [TestMethod]
        public void ReaGoodsLotTestAdd()
        {
            SysPublicSet.IsSetLicense = true;
            ReaGoodsLot entity = new ReaGoodsLot();
            entity.Id = longGUID;
            entity.LotNo = "10001DD2QQ1";
            entity.GoodsCName = "DDDD";
            entity.DataUpdateTime= DateTime.Now;
            //entity.GoodsID = 4623513945469361870;
            entity.Memo = "测试";
            bool b = ReaGoodsLotDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void ReaGoodsLotTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            ReaGoodsLot entity = ReaGoodsLotDao.Get(longGUID);
            entity.LotNo = "100012QQW11";
            entity.Memo = "测试DDD";
            entity.DataUpdateTime = DateTime.Now;

            bool b = ReaGoodsLotDao.Update(entity);
            Assert.AreEqual(true, b);
            //int b = ReaGoodsLotDao.UpdateByHql("update ReaGoodsLot  set CName='测试2' where Id=" + longGUID);
            //Assert.AreEqual(1, b);
        }
        //查
        [TestMethod]
        public void ReaGoodsLotTestGet()
        {
            SysPublicSet.IsSetLicense = true;
            ReaGoodsLot entity = ReaGoodsLotDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        [TestMethod]
        public void ReaGoodsLotTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = ReaGoodsLotDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
    }
}
