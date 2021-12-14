
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
	public class BloodLisResultDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodLisResultDao BloodLisResultDao;
        public BloodLisResultDaoTest()
        {
            BloodLisResultDao = context.GetObject("BloodLisResultDao") as IDBloodLisResultDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodLisResult entity = new BloodLisResult();
        	entity.Id = longGUID; 
entity.BarCode = "测试"; 
entity.Receiver = "测试"; 
entity.TestOperCName = "测试"; 
entity.Checker = "测试"; 
entity.ItemLisResult = "测试"; 
entity.ItemResult = "测试"; 
entity.ItemUnit = "测试"; 
entity.Reference = "测试"; 
entity.ResultHint = "测试"; 
entity.Visible = true; 
entity.DispOrder = 1; 
			bool b = BloodLisResultDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodLisResult entity = BloodLisResultDao.Get(longGUID);
        	
        	bool b = BloodLisResultDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodLisResult entity = BloodLisResultDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodLisResultDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}