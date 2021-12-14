
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
	public class BloodBagChargeLinkDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBagChargeLinkDao BloodBagChargeLinkDao;
        public BloodBagChargeLinkDaoTest()
        {
            BloodBagChargeLinkDao = context.GetObject("BloodBagChargeLinkDao") as IDBloodBagChargeLinkDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBagChargeLink entity = new BloodBagChargeLink();
        	entity.Id = longGUID; 
entity.IsUse = true; 
entity.DispOrder = 1; 
			bool b = BloodBagChargeLinkDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBagChargeLink entity = BloodBagChargeLinkDao.Get(longGUID);
        	
        	bool b = BloodBagChargeLinkDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBagChargeLink entity = BloodBagChargeLinkDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBagChargeLinkDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}