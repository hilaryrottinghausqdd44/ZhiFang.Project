
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
	public class BloodInCheckedCurrentDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodInCheckedCurrentDao BloodInCheckedCurrentDao;
        public BloodInCheckedCurrentDaoTest()
        {
            BloodInCheckedCurrentDao = context.GetObject("BloodInCheckedCurrentDao") as IDBloodInCheckedCurrentDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodInCheckedCurrent entity = new BloodInCheckedCurrent();
        	entity.Id = longGUID; 
entity.InvalidCode = "测试"; 
entity.ABORHCode = "测试"; 
entity.CheckedFlag = true; 
entity.ABOType = "测试"; 
entity.RhType = "测试"; 
entity.DispOrder = 1; 
entity.Visible = true; 
			bool b = BloodInCheckedCurrentDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodInCheckedCurrent entity = BloodInCheckedCurrentDao.Get(longGUID);
        	
        	bool b = BloodInCheckedCurrentDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodInCheckedCurrent entity = BloodInCheckedCurrentDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodInCheckedCurrentDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}