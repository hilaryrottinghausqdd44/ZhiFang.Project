
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
	public class BloodChargeItemLinkDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodChargeItemLinkDao BloodChargeItemLinkDao;
        public BloodChargeItemLinkDaoTest()
        {
            BloodChargeItemLinkDao = context.GetObject("BloodChargeItemLinkDao") as IDBloodChargeItemLinkDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodChargeItemLink entity = new BloodChargeItemLink();
        	entity.Id = longGUID; 
entity.DispOrder = 1; 
entity.IsUse = true; 
			bool b = BloodChargeItemLinkDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodChargeItemLink entity = BloodChargeItemLinkDao.Get(longGUID);
        	
        	bool b = BloodChargeItemLinkDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodChargeItemLink entity = BloodChargeItemLinkDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodChargeItemLinkDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}