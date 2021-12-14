
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.WebAssist.UnitTest
{
    [TestClass]
    public class SamplingGroupDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDSamplingGroupDao SamplingGroupDao;
        public SamplingGroupDaoTest()
        {
            SamplingGroupDao = context.GetObject("SamplingGroupDao") as IDSamplingGroupDao;
        }

        [TestMethod]
        public void TestSave()
        {
            SamplingGroup entity = new SamplingGroup();
            entity.SamplingGroupName = "测试";
            entity.SampleTypeNo = 1;
            entity.CubeType = 1;
            entity.CubeColor = "测试";
            entity.SpecialtyType = 1;
            entity.Shortname = "测试";
            entity.Shortcode = "测试";
            entity.Destination = "测试";
            entity.Unit = "测试";
            entity.Disporder = 1;
            entity.Synopsis = "测试";
            entity.PrinterName = "测试";
            entity.ShortCode2 = "测试";
            entity.PrintCount = 1;
            entity.ChargeMode = 1;
            entity.ChargeID1 = 1;
            entity.ChargeID2 = 1;
            entity.ChargeID3 = 1;
            entity.AffixTubeFlag = "测试";
            entity.VirtualNo = 1;
            bool b = SamplingGroupDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            SamplingGroup entity = SamplingGroupDao.Get(longGUID);

            bool b = SamplingGroupDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            SamplingGroup entity = SamplingGroupDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = SamplingGroupDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}