
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
    public class MEMicroDSTValueDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDMEMicroDSTValueDao MEMicroDSTValueDao;
        public MEMicroDSTValueDaoTest()
        {
            MEMicroDSTValueDao = context.GetObject("MEMicroDSTValueDao") as IDMEMicroDSTValueDao;
        }

        [TestMethod]
        public void TestSave()
        {
            MEMicroDSTValue entity = new MEMicroDSTValue();
            entity.Id = longGUID;
            entity.DSTType = "测试";
            entity.Concentration = "测试";
            entity.UseSDD = true;
            entity.ReportGroup = "测试";
            entity.ReportValue = "测试";
            entity.ResultStatus = "测试";
            entity.Units = "测试";
            entity.RefRange = "测试";
            entity.TestMethod = "测试";
            entity.AlarmLevel = 1;
            entity.ResultComment = "测试";
            entity.IsUse = true;
            entity.DispOrder = 1;
            entity.IsReportPublication = true;
            entity.UserNo = 1;
            entity.Id = longGUID;
            entity.EmpName = "测试";
            entity.DeleteFlag = true;
            entity.Id = longGUID;
            entity.EReportValue = "测试";
            entity.OtherDispOrder = 1;
            entity.IsPrint = true;
            entity.AlarmInfo = "测试";
            entity.PreReportValue = "测试";
            entity.PreResultStatus = "测试";
            entity.PreCompStatus = "测试";
            entity.EResultStatus = "测试";
            entity.BNaturalR = true;
            entity.IsGroupAnti = true;
            entity.AntiNote = "测试";
            bool b = MEMicroDSTValueDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            MEMicroDSTValue entity = MEMicroDSTValueDao.Get(longGUID);

            bool b = MEMicroDSTValueDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            MEMicroDSTValue entity = MEMicroDSTValueDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = MEMicroDSTValueDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}