
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
	public class BloodLargeUseItemDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodLargeUseItemDao BloodLargeUseItemDao;
        public BloodLargeUseItemDaoTest()
        {
            BloodLargeUseItemDao = context.GetObject("BloodLargeUseItemDao") as IDBloodLargeUseItemDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodLargeUseItem entity = new BloodLargeUseItem();
        	entity.LUIMemo = "测试"; 
entity.Visible = true; 
entity.DispOrder = 1; 
			bool b = BloodLargeUseItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodLargeUseItem entity = BloodLargeUseItemDao.Get(longGUID);
        	
        	bool b = BloodLargeUseItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodLargeUseItem entity = BloodLargeUseItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodLargeUseItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}