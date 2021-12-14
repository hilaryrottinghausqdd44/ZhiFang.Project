
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
	public class BloodBagProcessDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBagProcessDao BloodBagProcessDao;
        public BloodBagProcessDaoTest()
        {
            BloodBagProcessDao = context.GetObject("BloodBagProcessDao") as IDBloodBagProcessDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBagProcess entity = new BloodBagProcess();
        	entity.Id = longGUID; 
entity.BPflag = 1; 
entity.Visible = true; 
entity.DispOrder = 1; 
			bool b = BloodBagProcessDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBagProcess entity = BloodBagProcessDao.Get(longGUID);
        	
        	bool b = BloodBagProcessDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBagProcess entity = BloodBagProcessDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBagProcessDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}