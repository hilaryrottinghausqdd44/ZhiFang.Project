
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
	public class BloodTransItemDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodTransItemDao BloodTransItemDao;
        public BloodTransItemDaoTest()
        {
            BloodTransItemDao = context.GetObject("BloodTransItemDao") as IDBloodTransItemDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodTransItem entity = new BloodTransItem();
        	entity.Id = longGUID; 
entity.ContentTypeID = 1; 
entity.TransItemResult = "测试"; 
entity.Visible = true; 
entity.DispOrder = 1; 
			bool b = BloodTransItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodTransItem entity = BloodTransItemDao.Get(longGUID);
        	
        	bool b = BloodTransItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodTransItem entity = BloodTransItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodTransItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}