
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
	public class BloodSetQtyAlertInfoDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodSetQtyAlertInfoDao BloodSetQtyAlertInfoDao;
        public BloodSetQtyAlertInfoDaoTest()
        {
            BloodSetQtyAlertInfoDao = context.GetObject("BloodSetQtyAlertInfoDao") as IDBloodSetQtyAlertInfoDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodSetQtyAlertInfo entity = new BloodSetQtyAlertInfo();
        	entity.Id = longGUID; 
entity.DispOrder = 1; 
entity.Visible = true; 
			bool b = BloodSetQtyAlertInfoDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodSetQtyAlertInfo entity = BloodSetQtyAlertInfoDao.Get(longGUID);
        	
        	bool b = BloodSetQtyAlertInfoDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodSetQtyAlertInfo entity = BloodSetQtyAlertInfoDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodSetQtyAlertInfoDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}