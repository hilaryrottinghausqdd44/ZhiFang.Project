
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
    public class BloodDocGradeDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodDocGradeDao BloodDocGradeDao;
        public BloodDocGradeDaoTest()
        {
            BloodDocGradeDao = context.GetObject("BloodDocGradeDao") as IDBloodDocGradeDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodDocGrade entity = new BloodDocGrade();
            entity.Id = longGUID;
            entity.CName = "测试";
            entity.IsUse = true;
            entity.DispOrder = 1;
            bool b = BloodDocGradeDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodDocGrade entity = BloodDocGradeDao.Get(longGUID);

            bool b = BloodDocGradeDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodDocGrade entity = BloodDocGradeDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodDocGradeDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}