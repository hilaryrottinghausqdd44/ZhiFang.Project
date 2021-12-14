
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.BloodTransfusion.UnitTest
{
    [TestClass]
    public class BlooddocGradeDaoTest
    {
        static string longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBlooddocGradeDao BlooddocGradeDao;
        public BlooddocGradeDaoTest()
        {
            BlooddocGradeDao = context.GetObject("BlooddocGradeDao") as IDBlooddocGradeDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BlooddocGrade entity = new BlooddocGrade();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.DispOrder = 1;
            bool b = BlooddocGradeDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BlooddocGrade entity = BlooddocGradeDao.Get(longGUID);

            bool b = BlooddocGradeDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BlooddocGrade entity = BlooddocGradeDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BlooddocGradeDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}