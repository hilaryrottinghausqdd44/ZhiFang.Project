
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
    public class GKSampleRequestFormDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDGKSampleRequestFormDao GKSampleRequestFormDao;
        public GKSampleRequestFormDaoTest()
        {
            GKSampleRequestFormDao = context.GetObject("GKSampleRequestFormDao") as IDGKSampleRequestFormDao;
        }

        [TestMethod]
        public void TestSave()
        {
            GKSampleRequestForm entity = new GKSampleRequestForm();
            entity.Id = longGUID;
            entity.ReqDocNo = "测试";
            entity.Sampler = "测试";
            entity.StatusID = 1;
            entity.BarCode = "测试";
            entity.PrintCount = 1;
            entity.ReceiveFlag = false;
            entity.ResultFlag = false;
            entity.CreatorName = "测试";
            entity.TesterName = "测试";
            entity.TestResult = "测试";
            entity.Evaluators = "测试";
            entity.Judgment = "测试";
            entity.Visible = true;
            entity.Memo = "测试";
            entity.DispOrder = 1;
            bool b = GKSampleRequestFormDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            GKSampleRequestForm entity = GKSampleRequestFormDao.Get(longGUID);

            bool b = GKSampleRequestFormDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            GKSampleRequestForm entity = GKSampleRequestFormDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = GKSampleRequestFormDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}