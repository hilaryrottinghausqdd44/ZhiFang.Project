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
    public class ReaDeptGoodsUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaDeptGoodsDao ReaDeptGoodsDao;

        public ReaDeptGoodsUnitTest()
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
            entity.DeptName = "测试";
            //entity.GoodsCName = "测试";
            entity.Memo = "123";
            bool b = ReaDeptGoodsDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void ReaDeptGoodsTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            ReaDeptGoods entity = ReaDeptGoodsDao.Get(longGUID);
            entity.DeptName = "测试";
            //entity.GoodsCName = "测试W";
            entity.Memo = "123WWWW";
            bool b = ReaDeptGoodsDao.Update(entity);
            Assert.AreEqual(true, b);
            //int b = ReaDeptGoodsDao.UpdateByHql("update ReaDeptGoods  set CName='测试2' where Id=" + longGUID);
            //Assert.AreEqual(1, b);
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
        public void  ReaDeptGoodsTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = ReaDeptGoodsDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
    }
}
