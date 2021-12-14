
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
	public class BloodClassUnitLinkDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodClassUnitLinkDao BloodClassUnitLinkDao;
        public BloodClassUnitLinkDaoTest()
        {
            BloodClassUnitLinkDao = context.GetObject("BloodClassUnitLinkDao") as IDBloodClassUnitLinkDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodClassUnitLink entity = new BloodClassUnitLink();
        	entity.Id = longGUID; 
entity.IsCalcUnit = true; 
entity.IsUse = true; 
entity.DispOrder = 1; 
entity.Memo = "测试"; 
			bool b = BloodClassUnitLinkDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodClassUnitLink entity = BloodClassUnitLinkDao.Get(longGUID);
        	
        	bool b = BloodClassUnitLinkDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodClassUnitLink entity = BloodClassUnitLinkDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodClassUnitLinkDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}