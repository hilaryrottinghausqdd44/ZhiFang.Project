
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
	public class BloodBOutItemDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBOutItemDao BloodBOutItemDao;
        public BloodBOutItemDaoTest()
        {
            BloodBOutItemDao = context.GetObject("BloodBOutItemDao") as IDBloodBOutItemDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBOutItem entity = new BloodBOutItem();
        	entity.Id = longGUID; 
entity.BOutItemNo = "测试"; 
entity.CheckFlag = 1; 
entity.OutFlag = 1; 
entity.ConfirmCompletion = 1; 
entity.HandoverCompletion = 1; 
entity.CourseCompletion = 1; 
entity.RecoverCompletion = 1; 
entity.DispOrder = 1; 
entity.Visible = true; 
			bool b = BloodBOutItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBOutItem entity = BloodBOutItemDao.Get(longGUID);
        	
        	bool b = BloodBOutItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBOutItem entity = BloodBOutItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBOutItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}