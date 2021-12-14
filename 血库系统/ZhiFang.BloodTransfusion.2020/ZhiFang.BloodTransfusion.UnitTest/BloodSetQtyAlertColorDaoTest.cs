
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
	public class BloodSetQtyAlertColorDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodSetQtyAlertColorDao BloodSetQtyAlertColorDao;
        public BloodSetQtyAlertColorDaoTest()
        {
            BloodSetQtyAlertColorDao = context.GetObject("BloodSetQtyAlertColorDao") as IDBloodSetQtyAlertColorDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodSetQtyAlertColor entity = new BloodSetQtyAlertColor();
        	entity.Id = longGUID; 
entity.AlertName = "测试"; 
entity.AlertTypeId = 1; 
entity.AlertTypeCName = "测试"; 
entity.AlertColor = "测试"; 
entity.Memo = "测试"; 
entity.DispOrder = 1; 
entity.Visible = true; 
			bool b = BloodSetQtyAlertColorDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodSetQtyAlertColor entity = BloodSetQtyAlertColorDao.Get(longGUID);
        	
        	bool b = BloodSetQtyAlertColorDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodSetQtyAlertColor entity = BloodSetQtyAlertColorDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodSetQtyAlertColorDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}