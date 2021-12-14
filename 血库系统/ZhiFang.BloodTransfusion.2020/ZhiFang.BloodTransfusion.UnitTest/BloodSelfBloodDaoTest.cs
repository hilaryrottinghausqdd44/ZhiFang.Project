
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
	public class BloodSelfBloodDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodSelfBloodDao BloodSelfBloodDao;
        public BloodSelfBloodDaoTest()
        {
            BloodSelfBloodDao = context.GetObject("BloodSelfBloodDao") as IDBloodSelfBloodDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodSelfBlood entity = new BloodSelfBlood();
        	entity.Id = longGUID; 
entity.ZdyBBagCode = "测试"; 
entity.CName = "测试"; 
entity.Sex = "测试"; 
entity.BCount = "测试"; 
entity.CollectDate = "测试"; 
entity.BBagCode = "测试"; 
entity.PCode = "测试"; 
entity.InvalidCode = "测试"; 
entity.ABORHCode = "测试"; 
entity.NurseName = "测试"; 
entity.TechnicianName = "测试"; 
entity.PrintIName = "测试"; 
entity.PrintDate = "测试"; 
entity.PrintFlag = true; 
entity.VoidName = "测试"; 
entity.VoidFlag = true; 
entity.WarehousingName = "测试"; 
entity.WarehousingFlag = true; 
entity.Visible = true; 
entity.DispOrder = 1; 
			bool b = BloodSelfBloodDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodSelfBlood entity = BloodSelfBloodDao.Get(longGUID);
        	
        	bool b = BloodSelfBloodDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodSelfBlood entity = BloodSelfBloodDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodSelfBloodDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}