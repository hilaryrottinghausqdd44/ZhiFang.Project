
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
	public class BloodInterfaceMapingDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodInterfaceMapingDao BloodInterfaceMapingDao;
        public BloodInterfaceMapingDaoTest()
        {
            BloodInterfaceMapingDao = context.GetObject("BloodInterfaceMapingDao") as IDBloodInterfaceMapingDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodInterfaceMaping entity = new BloodInterfaceMaping();
        	entity.Id = longGUID; 
entity.MapingCode = "测试"; 
entity.IsUse = true; 
entity.DispOrder = 1; 
			bool b = BloodInterfaceMapingDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodInterfaceMaping entity = BloodInterfaceMapingDao.Get(longGUID);
        	
        	bool b = BloodInterfaceMapingDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodInterfaceMaping entity = BloodInterfaceMapingDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodInterfaceMapingDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}