
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
	public class BloodBOutFormDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBOutFormDao BloodBOutFormDao;
        public BloodBOutFormDaoTest()
        {
            BloodBOutFormDao = context.GetObject("BloodBOutFormDao") as IDBloodBOutFormDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBOutForm entity = new BloodBOutForm();
        	entity.Id = longGUID; 
entity.BOutFormNo = "测试"; 
entity.OutType = 1; 
entity.OperatorName = "测试"; 
entity.CheckCName = "测试"; 
entity.CheckFlag = 1; 
entity.ConfirmCompletion = 1; 
entity.HandoverCompletion = 1; 
entity.CourseCompletion = 1; 
entity.RecoverCompletion = 1; 
entity.PrintCount = 1; 
entity.Memo = "测试"; 
entity.Visible = true; 
entity.DispOrder = 1; 
			bool b = BloodBOutFormDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBOutForm entity = BloodBOutFormDao.Get(longGUID);
        	
        	bool b = BloodBOutFormDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBOutForm entity = BloodBOutFormDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBOutFormDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}