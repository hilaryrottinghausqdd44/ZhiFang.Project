
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.BloodTransfusion.UnitTest
{
    [TestClass]
    public class BloodBReqEditItemDaoTest
    {
        static string longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBReqEditItemDao BloodBReqEditItemDao;
        public BloodBReqEditItemDaoTest()
        {
            BloodBReqEditItemDao = context.GetObject("BloodBReqEditItemDao") as IDBloodBReqEditItemDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodBReqEditItem entity = new BloodBReqEditItem();
            entity.Id = longGUID;
            entity.BtestItemName = "测试";
            entity.BEIName = "测试";
            entity.DispOrder = 1;
            entity.Visible = true;
            bool b = BloodBReqEditItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodBReqEditItem entity = BloodBReqEditItemDao.Get(longGUID);

            bool b = BloodBReqEditItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodBReqEditItem entity = BloodBReqEditItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodBReqEditItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}