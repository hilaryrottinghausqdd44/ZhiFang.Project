
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
    public class BloodBagProcessTypeQryDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBagProcessTypeQryDao BloodBagProcessTypeQryDao;
        public BloodBagProcessTypeQryDaoTest()
        {
            BloodBagProcessTypeQryDao = context.GetObject("BloodBagProcessTypeQryDao") as IDBloodBagProcessTypeQryDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BloodBagProcessTypeQry entity = new BloodBagProcessTypeQry();
            //entity.CName = "测试";
            //entity.SName = "测试";
            //entity.ShortCode = "测试";
            //entity.PinYinZiTou = "测试";
            entity.IsUse = true;
            entity.DispOrder = 1;
            bool b = BloodBagProcessTypeQryDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BloodBagProcessTypeQry entity = BloodBagProcessTypeQryDao.Get(longGUID);

            bool b = BloodBagProcessTypeQryDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BloodBagProcessTypeQry entity = BloodBagProcessTypeQryDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BloodBagProcessTypeQryDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}