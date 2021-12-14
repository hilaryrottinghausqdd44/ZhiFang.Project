
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
	public class BloodBReqItemDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBReqItemDao BloodBReqItemDao;
        public BloodBReqItemDaoTest()
        {
            BloodBReqItemDao = context.GetObject("BloodBReqItemDao") as IDBloodBReqItemDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBReqItem entity = new BloodBReqItem();
        	entity.BReqItemNo = "测试"; 
entity.OtherItemNo = "测试"; 
entity.Memo = "测试"; 
entity.DispOrder = 1; 
entity.Visible = true; 
			bool b = BloodBReqItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBReqItem entity = BloodBReqItemDao.Get(longGUID);
        	
        	bool b = BloodBReqItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBReqItem entity = BloodBReqItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBReqItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}