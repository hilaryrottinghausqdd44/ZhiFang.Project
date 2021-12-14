
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
    public class BloodQtyDtlDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodQtyDtlDao BloodQtyDtlDao;
        public BloodQtyDtlDaoTest()
        {
            BloodQtyDtlDao = context.GetObject("BloodQtyDtlDao") as IDBloodQtyDtlDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodQtyDtl entity = new BloodQtyDtl();
            entity.Id = longGUID;
            entity.BframeNo = "测试";
            entity.BINo = "测试";
            entity.BBagCode = "测试";
            entity.PCode = "测试";
            entity.B3Code = "测试";
            entity.BBagExCode = "测试";
            entity.UseFlag = 1;
            entity.Hflag = 1;
            entity.InvFlag = 1;
            entity.BCCode = "测试";
            //entity.yqCode = "测试";
            entity.BloodoC = "测试";
            entity.Ismulflag = 1;
            entity.Preflag = true;
            entity.BagCheckFlag = true;
            entity.Memo = "测试";
            entity.Visible = true;
            entity.DispOrder = 1;
            bool b = BloodQtyDtlDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodQtyDtl entity = BloodQtyDtlDao.Get(longGUID);

            bool b = BloodQtyDtlDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodQtyDtl entity = BloodQtyDtlDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodQtyDtlDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}