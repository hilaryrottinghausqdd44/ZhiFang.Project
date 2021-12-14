
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
    public class BloodstyleDaoTest
    {
        static int longGUID = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodstyleDao BloodstyleDao;
        public BloodstyleDaoTest()
        {
            BloodstyleDao = context.GetObject("BloodstyleDao") as IDBloodstyleDao;
        }

        [TestMethod]
        public void TestSave()
        {
            Bloodstyle entity = new Bloodstyle();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.IsMakeBlood = "测试";
            entity.HisDispCode = "测试";
            entity.BisDispCode = "测试";
            entity.StoreDays = "测试";
            entity.ShortCode = "测试";
            entity.ShortName = "测试";
            entity.DispOrder = 1;
            entity.Visible = "测试";
           // entity.Bloodclass = "测试";
            entity.WarnDays = "测试";
            entity.WarnUnit = "测试";
            entity.StoreUnit = "测试";
            entity.RegAllow = "测试";
           // entity.BloodUnitNo = "测试";
            entity.StoreCondNo = 1;
            entity.Ahead = 1;
            entity.AheadUnit = "测试";
            entity.HemolysisTime = "测试";
            entity.HemolysisUnit = "测试";
           // entity.BCNo = "测试";
            entity.Isprocess = "测试";
            bool b = BloodstyleDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            Bloodstyle entity = BloodstyleDao.Get(longGUID);

            bool b = BloodstyleDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            Bloodstyle entity = BloodstyleDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodstyleDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}