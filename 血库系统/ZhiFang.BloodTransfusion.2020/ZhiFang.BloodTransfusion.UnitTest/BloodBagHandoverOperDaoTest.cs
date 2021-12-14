
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
	public class BloodBagHandoverOperDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBagHandoverOperDao BloodBagHandoverOperDao;
        public BloodBagHandoverOperDaoTest()
        {
            BloodBagHandoverOperDao = context.GetObject("BloodBagHandoverOperDao") as IDBloodBagHandoverOperDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBagHandoverOper entity = new BloodBagHandoverOper();
        	entity.Id = longGUID; 
entity.BagOperationNo = "测试"; 
entity.DeptCName = "测试"; 
entity.BagOper = "测试"; 
entity.Carrier = "测试"; 
entity.Visible = true; 
			bool b = BloodBagHandoverOperDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBagHandoverOper entity = BloodBagHandoverOperDao.Get(longGUID);
        	
        	bool b = BloodBagHandoverOperDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBagHandoverOper entity = BloodBagHandoverOperDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBagHandoverOperDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}