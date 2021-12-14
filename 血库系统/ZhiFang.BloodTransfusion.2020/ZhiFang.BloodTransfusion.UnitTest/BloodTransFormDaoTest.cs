
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
	public class BloodTransFormDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodTransFormDao BloodTransFormDao;
        public BloodTransFormDaoTest()
        {
            BloodTransFormDao = context.GetObject("BloodTransFormDao") as IDBloodTransFormDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodTransForm entity = new BloodTransForm();
        	entity.Id = longGUID; 
entity.TransFormNo = "测试"; 
entity.BeforeCheck1 = "测试"; 
entity.BeforeCheck2 = "测试"; 
entity.TransCheck1 = "测试"; 
entity.TransCheck2 = "测试"; 
entity.HasAdverseReactions = true; 
entity.Visible = true; 
entity.DispOrder = 1; 
			bool b = BloodTransFormDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodTransForm entity = BloodTransFormDao.Get(longGUID);
        	
        	bool b = BloodTransFormDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodTransForm entity = BloodTransFormDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodTransFormDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}