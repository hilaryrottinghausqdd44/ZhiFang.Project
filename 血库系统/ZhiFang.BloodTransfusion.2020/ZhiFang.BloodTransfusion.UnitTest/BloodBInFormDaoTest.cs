
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
    public class BloodBInFormDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBInFormDao BloodBInFormDao;
        public BloodBInFormDaoTest()
        {
            BloodBInFormDao = context.GetObject("BloodBInFormDao") as IDBloodBInFormDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodBInForm entity = new BloodBInForm();
            entity.Id = longGUID;
            entity.BInFormNo = "测试";
            entity.InType = "测试";
            entity.InFileName = "测试";
            entity.Operator = "测试";
            entity.Checker = "测试";
            entity.PrintCount = 1;
            entity.Memo = "测试";
            //entity.yqCode = "测试";
            entity.Visible = true;
            entity.DispOrder = 1;
            bool b = BloodBInFormDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodBInForm entity = BloodBInFormDao.Get(longGUID);

            bool b = BloodBInFormDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodBInForm entity = BloodBInFormDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodBInFormDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}