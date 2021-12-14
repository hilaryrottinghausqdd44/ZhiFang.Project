
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
    public class SectionItemDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDSectionItemDao SectionItemDao;
        public SectionItemDaoTest()
        {
            SectionItemDao = context.GetObject("SectionItemDao") as IDSectionItemDao;
        }

        [TestMethod]
        public void TestSave()
        {
            SectionItem entity = new SectionItem();
            entity.Id = longGUID;
            entity.Id = longGUID;
            entity.IsDefault = 1;
            entity.DefaultValue = "测试";
            entity.WarningTime = "测试";
            entity.BWarningTimeOnlyWorkingDay = 1;
            entity.BResultIsDesc = 1;
            entity.BKXJSF = 1;
            entity.KxjsfType = 1;
            entity.KxjsfDec = 1;
            entity.KxjsfWhen = 1;
            entity.BKxjsfDy = 1;
            entity.KxjsfDyType = 1;
            entity.BKxjsfXy = 1;
            entity.KxjsfXyType = 1;
            entity.DefaultPItemNo = 1;
            entity.DefaultEquipNo = 1;
            entity.UserNo = 1;
            entity.DefaultValue2 = "测试";
            entity.IsTran = true;
            entity.UseReagentCount = 1;
            entity.ReagentCode = "测试";
            entity.Visible = 1;
            entity.DispOrder = 1;
            bool b = SectionItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            SectionItem entity = SectionItemDao.Get(longGUID);

            bool b = SectionItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            SectionItem entity = SectionItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = SectionItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}