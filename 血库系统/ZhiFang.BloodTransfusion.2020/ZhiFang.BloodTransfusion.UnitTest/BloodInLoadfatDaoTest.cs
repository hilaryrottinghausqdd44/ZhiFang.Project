
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
    public class BloodInLoadfatDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodInLoadfatDao BloodInLoadfatDao;
        public BloodInLoadfatDaoTest()
        {
            BloodInLoadfatDao = context.GetObject("BloodInLoadfatDao") as IDBloodInLoadfatDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodInLoadfat entity = new BloodInLoadfat();
            entity.Id = longGUID;
            entity.InFileName = "测试";
            entity.BLOODBILLNO = "测试";
            entity.BBagCode = "测试";
            entity.BloodName = "测试";
            entity.Pcode = "测试";
            entity.BUnit = "测试";
            entity.ABO = "测试";
            //entity.rhd = "测试";
            entity.InvalidCode = "测试";
            entity.B3Code = "测试";
            entity.LFflag = 1;
            entity.Hflag = 1;
            //entity.yqcode = "测试";
            entity.Visible = true;
            entity.DispOrder = 1;
            bool b = BloodInLoadfatDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodInLoadfat entity = BloodInLoadfatDao.Get(longGUID);

            bool b = BloodInLoadfatDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodInLoadfat entity = BloodInLoadfatDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodInLoadfatDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}