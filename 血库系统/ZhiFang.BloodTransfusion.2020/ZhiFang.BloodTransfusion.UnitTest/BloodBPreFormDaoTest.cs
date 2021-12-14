
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
    public class BloodBPreFormDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBPreFormDao BloodBPreFormDao;
        public BloodBPreFormDaoTest()
        {
            BloodBPreFormDao = context.GetObject("BloodBPreFormDao") as IDBloodBPreFormDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodBPreForm entity = new BloodBPreForm();
            //entity.yqCode = "测试";
            entity.Id = longGUID;
            entity.BPreFormNo = "测试";
            entity.BPreCheckFlag = 1;
            entity.BarCode = "测试";
            entity.IsCharge = 1;
            entity.OutFlag = 1;
            entity.Operator = "测试";
            entity.Checker = "测试";
            entity.BloodOrder = "测试";
            entity.BPreAntiBody = "测试";
            entity.FirstABOWay = "测试";
            entity.FirstABOTest = "测试";
            entity.OverdueCName = "测试";
            entity.BarCodeMemo = "测试";
            entity.FirstRhTest = "测试";
            entity.IsCompleted = 1;
            entity.BPreSendCName = "测试";
            entity.PrintCount = 1;
            entity.Memo = "测试";
            entity.Visible = true;
            entity.DispOrder = 1;
            bool b = BloodBPreFormDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodBPreForm entity = BloodBPreFormDao.Get(longGUID);

            bool b = BloodBPreFormDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodBPreForm entity = BloodBPreFormDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodBPreFormDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}