
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
    public class MEMicroAppraisalValueDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDMEMicroAppraisalValueDao MEMicroAppraisalValueDao;
        public MEMicroAppraisalValueDaoTest()
        {
            MEMicroAppraisalValueDao = context.GetObject("MEMicroAppraisalValueDao") as IDMEMicroAppraisalValueDao;
        }

        [TestMethod]
        public void TestSave()
        {
            MEMicroAppraisalValue entity = new MEMicroAppraisalValue();
            entity.Id = longGUID;
            entity.IMType = "测试";
            entity.Id = longGUID;
            entity.ResistancePhenotypeName = "测试";
            entity.EquipResistancePhenotype = "测试";
            entity.AppraisalResult = "测试";
            entity.LaboratoryEvaluation = "测试";
            entity.Zdy1 = "测试";
            entity.Zdy2 = "测试";
            entity.Zdy3 = "测试";
            entity.Zdy4 = "测试";
            entity.Zdy5 = "测试";
            entity.EquipNo = 1;
            entity.Id = longGUID;
            entity.ResultComment = "测试";
            entity.Id = longGUID;
            entity.MicroPositive = "测试";
            entity.IsCopy = 1;
            bool b = MEMicroAppraisalValueDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            MEMicroAppraisalValue entity = MEMicroAppraisalValueDao.Get(longGUID);

            bool b = MEMicroAppraisalValueDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            MEMicroAppraisalValue entity = MEMicroAppraisalValueDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = MEMicroAppraisalValueDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}