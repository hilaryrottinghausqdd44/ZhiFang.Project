
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.ReagentSys.Client.UnitTest
{
    [TestClass]
    public class ReaAlertInfoSettingsDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaAlertInfoSettingsDao ReaAlertInfoSettingsDao;
        public ReaAlertInfoSettingsDaoTest()
        {
            ReaAlertInfoSettingsDao = context.GetObject("ReaAlertInfoSettingsDao") as IDReaAlertInfoSettingsDao;
        }

        [TestMethod]
        public void TestSave()
        {
            ReaAlertInfoSettings entity = new ReaAlertInfoSettings();
            entity.Id = longGUID;
            entity.AlertTypeId = 1;
            entity.AlertTypeCName = "测试";
            entity.StoreUpper =1;
            entity.DispOrder = 1;
            entity.Visible = 1;
            bool b = ReaAlertInfoSettingsDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaAlertInfoSettings entity = ReaAlertInfoSettingsDao.Get(longGUID);

            bool b = ReaAlertInfoSettingsDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaAlertInfoSettings entity = ReaAlertInfoSettingsDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaAlertInfoSettingsDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}