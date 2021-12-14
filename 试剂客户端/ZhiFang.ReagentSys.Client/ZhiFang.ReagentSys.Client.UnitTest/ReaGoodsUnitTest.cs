using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.ReagentSys.Client.Common;
using ZhiFang.Entity.RBAC;
using ZhiFang.Common.Public;
using ZhiFang.DAO.NHB.RBAC;

namespace ZhiFang.ReagentSys.Client.UnitTest
{
    [TestClass]
    public class ReaGoodsUnitTest
    {

        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaGoodsDao ReaGoodsDao;

        public ReaGoodsUnitTest()
        {
            ReaGoodsDao = context.GetObject("ReaGoodsDao") as IDReaGoodsDao;
        }
        //新增
        [TestMethod] 
        public void ReaGoodsTestAdd()
        {
            SysPublicSet.IsSetLicense = true;
            ReaGoods entity = new ReaGoods();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.SName = "测试";
            entity.ReaGoodsNo = "123";
            entity.GoodsNo = "123";
            entity.Visible = 1;

            bool b = ReaGoodsDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void ReaGoodsTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            ReaGoods entity = ReaGoodsDao.Get(longGUID);
            entity.CName = "测试2";
            entity.SName = "测试2";
            bool b = ReaGoodsDao.Update(entity);
            Assert.AreEqual(true, b);
            //int b = ReaGoodsDao.UpdateByHql("update ReaGoods  set CName='测试2' where Id=" + longGUID);
            //Assert.AreEqual(1, b);
        }
        //查
        [TestMethod]
        public void ReaGoodsTestGet()
        {
            SysPublicSet.IsSetLicense = true;
            ReaGoods entity = ReaGoodsDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        [TestMethod]
        public void ReaGoodsTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = ReaGoodsDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
      
    }
}
