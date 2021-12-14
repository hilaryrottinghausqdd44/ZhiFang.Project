
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
	public class BloodBReqFormResultDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBReqFormResultDao BloodBReqFormResultDao;
        public BloodBReqFormResultDaoTest()
        {
            BloodBReqFormResultDao = context.GetObject("BloodBReqFormResultDao") as IDBloodBReqFormResultDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBReqFormResult entity = new BloodBReqFormResult();
        	entity.Id = longGUID; 
entity.Barcode = "测试"; 
entity.ItemResult = "测试"; 
entity.ItemUnit = "测试"; 
entity.ItemLisResult = "测试"; 
entity.Visible = true; 
entity.DispOrder = 1; 
			bool b = BloodBReqFormResultDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBReqFormResult entity = BloodBReqFormResultDao.Get(longGUID);
        	
        	bool b = BloodBReqFormResultDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBReqFormResult entity = BloodBReqFormResultDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBReqFormResultDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}