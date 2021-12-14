
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
	public class BloodBagRecordDtlDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBagRecordDtlDao BloodBagRecordDtlDao;
        public BloodBagRecordDtlDaoTest()
        {
            BloodBagRecordDtlDao = context.GetObject("BloodBagRecordDtlDao") as IDBloodBagRecordDtlDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBagRecordDtl entity = new BloodBagRecordDtl();
        	entity.Id = longGUID; 
entity.ItemResult = "测试"; 
entity.CreatorName = "测试"; 
entity.Visible = true; 
entity.DispOrder = 1; 
entity.Memo = "测试"; 
			bool b = BloodBagRecordDtlDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBagRecordDtl entity = BloodBagRecordDtlDao.Get(longGUID);
        	
        	bool b = BloodBagRecordDtlDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBagRecordDtl entity = BloodBagRecordDtlDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBagRecordDtlDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}