
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
    public class BTemplateDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBTemplateDao BTemplateDao;
        public BTemplateDaoTest()
        {
            BTemplateDao = context.GetObject("BTemplateDao") as IDBTemplateDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BTemplate entity = new BTemplate();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.SName = "测试";
            //entity.ShortCode = "测试";
            entity.PinYinZiTou = "测试";
            entity.TypeName = "测试";
            entity.TemplateType = "测试";
            entity.FilePath = "测试";
            entity.FileName = "测试";
            entity.FileExt = "测试";
            entity.ContentType = "测试";
            entity.Comment = "测试";
            entity.ExcelRuleInfo = "测试";
            entity.DispOrder = 1;
            entity.IsUse = true;
            entity.IsDefault = true;
            bool b = BTemplateDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BTemplate entity = BTemplateDao.Get(longGUID);

            bool b = BTemplateDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BTemplate entity = BTemplateDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BTemplateDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}