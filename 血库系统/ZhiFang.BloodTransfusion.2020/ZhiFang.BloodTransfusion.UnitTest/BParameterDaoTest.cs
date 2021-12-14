
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
    public class BParameterDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBParameterDao BParameterDao;
        public BParameterDaoTest()
        {
            BParameterDao = context.GetObject("BParameterDao") as IDBParameterDao;
        }

        [TestMethod]
        public void TestSave()
        {
            BParameter entity = new BParameter();
            entity.Id = longGUID;
            entity.Name = "测试";
            entity.SName = "测试";
            entity.ParaType = "测试";
            entity.ParaNo = "测试";
            entity.ParaValue = "测试";
            entity.ParaDesc = "测试";
            entity.Shortcode = "测试";
            entity.PinYinZiTou = "测试";
            entity.ItemEditInfo = "测试";
            entity.DispOrder = 1;
            entity.IsUse = true;
            //entity.IsUserSet = true;
            bool b = BParameterDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            BParameter entity = BParameterDao.Get(longGUID);

            bool b = BParameterDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            BParameter entity = BParameterDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = BParameterDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}