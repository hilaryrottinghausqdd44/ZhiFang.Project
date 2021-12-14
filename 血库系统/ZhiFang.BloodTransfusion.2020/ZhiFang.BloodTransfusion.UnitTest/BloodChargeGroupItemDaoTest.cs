
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
	public class BloodChargeGroupItemDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodChargeGroupItemDao BloodChargeGroupItemDao;
        public BloodChargeGroupItemDaoTest()
        {
            BloodChargeGroupItemDao = context.GetObject("BloodChargeGroupItemDao") as IDBloodChargeGroupItemDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodChargeGroupItem entity = new BloodChargeGroupItem();
        	entity.Id = longGUID; 
entity.DispOrder = 1; 
entity.IsUse = true; 
			bool b = BloodChargeGroupItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodChargeGroupItem entity = BloodChargeGroupItemDao.Get(longGUID);
        	
        	bool b = BloodChargeGroupItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodChargeGroupItem entity = BloodChargeGroupItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodChargeGroupItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}