
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
	public class BloodInCheckedItemDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodInCheckedItemDao BloodInCheckedItemDao;
        public BloodInCheckedItemDaoTest()
        {
            BloodInCheckedItemDao = context.GetObject("BloodInCheckedItemDao") as IDBloodInCheckedItemDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodInCheckedItem entity = new BloodInCheckedItem();
        	entity.Id = longGUID; 
entity.IsBloodIn = 1; 
entity.B3Code = "测试"; 
entity.BBagCode = "测试"; 
entity.PCode = "测试"; 
entity.SCanUser = "测试"; 
entity.ABOType = "测试"; 
entity.RhType = "测试"; 
entity.InvalidCode = "测试"; 
entity.AboRhCode = "测试"; 
entity.DispOrder = 1; 
entity.Visible = true; 
			bool b = BloodInCheckedItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodInCheckedItem entity = BloodInCheckedItemDao.Get(longGUID);
        	
        	bool b = BloodInCheckedItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodInCheckedItem entity = BloodInCheckedItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodInCheckedItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}