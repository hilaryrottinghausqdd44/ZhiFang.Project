
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
    public class BloodChargeMoneyDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodChargeMoneyDao BloodChargeMoneyDao;
        public BloodChargeMoneyDaoTest()
        {
            BloodChargeMoneyDao = context.GetObject("BloodChargeMoneyDao") as IDBloodChargeMoneyDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodChargeMoney entity = new BloodChargeMoney();
            entity.Id = longGUID;
            entity.CMNo = "测试";
            entity.HisChargeItemNo = "测试";
            entity.OrdItemId = "测试";
            entity.IsCharge = 1;
            entity.HisisCharge = 1;
            //entity.YqCode = "测试";
            entity.OperateName = "测试";
           
            entity.Visible = true;
            entity.DispOrder = 1;
            bool b = BloodChargeMoneyDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodChargeMoney entity = BloodChargeMoneyDao.Get(longGUID);

            bool b = BloodChargeMoneyDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodChargeMoney entity = BloodChargeMoneyDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodChargeMoneyDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}