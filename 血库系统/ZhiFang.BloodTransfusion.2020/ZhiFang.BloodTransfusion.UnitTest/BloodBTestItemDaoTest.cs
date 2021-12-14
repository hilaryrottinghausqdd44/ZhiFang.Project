
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
	public class BloodBTestItemDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBTestItemDao BloodBTestItemDao;
        public BloodBTestItemDaoTest()
        {
            BloodBTestItemDao = context.GetObject("BloodBTestItemDao") as IDBloodBTestItemDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBTestItem entity = new BloodBTestItem();
        	entity.Id = longGUID; 
entity.BTestItemName = "测试"; 
entity.EName = "测试"; 
entity.SName = "测试"; 
entity.PinYinZiTou = "测试"; 
entity.ShortCode = "测试"; 
entity.IsGroup = true; 
entity.HisOrderCode = "测试"; 
entity.Reference = "测试"; 
entity.IsResultItem = true; 
entity.IsPreTransEvalItem = true; 
entity.IsUse = true; 
entity.DispOrder = 1; 
			bool b = BloodBTestItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBTestItem entity = BloodBTestItemDao.Get(longGUID);
        	
        	bool b = BloodBTestItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBTestItem entity = BloodBTestItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBTestItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}