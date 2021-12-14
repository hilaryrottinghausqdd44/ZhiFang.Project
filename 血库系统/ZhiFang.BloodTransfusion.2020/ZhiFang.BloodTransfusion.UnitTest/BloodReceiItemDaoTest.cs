
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
	public class BloodReceiItemDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodReceiItemDao BloodReceiItemDao;
        public BloodReceiItemDaoTest()
        {
            BloodReceiItemDao = context.GetObject("BloodReceiItemDao") as IDBloodReceiItemDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodReceiItem entity = new BloodReceiItem();
        	entity.Id = longGUID; 
entity.Visible = true; 
entity.DispOrder = 1; 
			bool b = BloodReceiItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodReceiItem entity = BloodReceiItemDao.Get(longGUID);
        	
        	bool b = BloodReceiItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodReceiItem entity = BloodReceiItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodReceiItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}