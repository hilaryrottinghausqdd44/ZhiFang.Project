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
    public class ReaDeptGoodsLinkUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaDeptGoodsDao ReaDeptGoodsDao;



        public ReaDeptGoodsLinkUnitTest()
        {
            ReaDeptGoodsDao = context.GetObject("ReaDeptGoodsDao") as IDReaDeptGoodsDao;
        }

        //新增
        [TestMethod]
        public void ReaDeptGoodsTestAdd()
        {


            SysPublicSet.IsSetLicense = true;
            ReaDeptGoods entity = new ReaDeptGoods();
            entity.Id = longGUID;
            //entity.GoodsCName = "SSSSAAAAA";
            // entity.TestEquipID = "10001DD2QQ1";
            //entity.GoodsID =long.Parse(ReaGoodsID);
            //entity.TestEquipID = long.Parse(ReaTestEquipLabID);
            entity.Memo = "测试wwwwwwwwwwwwww";
            bool b = ReaDeptGoodsDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void ReaDeptGoodsTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            ReaDeptGoods entity = ReaDeptGoodsDao.Get(longGUID);
            //entity.LotNo = "100012QQW11";
            entity.Memo = "测试DDD";

            bool b = ReaDeptGoodsDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        //查
        [TestMethod]
        public void ReaDeptGoodsTestGet()
        {
            SysPublicSet.IsSetLicense = true;
            ReaDeptGoods entity = ReaDeptGoodsDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        [TestMethod]
        public void ReaDeptGoodsTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = ReaDeptGoodsDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
    }
}
