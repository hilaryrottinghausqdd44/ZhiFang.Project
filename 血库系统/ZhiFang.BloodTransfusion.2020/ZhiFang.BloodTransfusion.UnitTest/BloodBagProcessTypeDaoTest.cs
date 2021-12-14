
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
	public class BloodBagProcessTypeDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBagProcessTypeDao BloodBagProcessTypeDao;
        public BloodBagProcessTypeDaoTest()
        {
            BloodBagProcessTypeDao = context.GetObject("BloodBagProcessTypeDao") as IDBloodBagProcessTypeDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBagProcessType entity = new BloodBagProcessType();
        	entity.Id = longGUID; 
entity.CName = "测试"; 
entity.SName = "测试"; 
entity.PinYinZiTou = "测试"; 
entity.ShortCode = "测试"; 
entity.HisOrderCode = "测试"; 
entity.IsUse = true; 
entity.DispOrder = 1; 
			bool b = BloodBagProcessTypeDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBagProcessType entity = BloodBagProcessTypeDao.Get(longGUID);
        	
        	bool b = BloodBagProcessTypeDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBagProcessType entity = BloodBagProcessTypeDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBagProcessTypeDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}