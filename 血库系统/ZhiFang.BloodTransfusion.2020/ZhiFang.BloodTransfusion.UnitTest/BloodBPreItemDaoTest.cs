
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
	public class BloodBPreItemDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBPreItemDao BloodBPreItemDao;
        public BloodBPreItemDaoTest()
        {
            BloodBPreItemDao = context.GetObject("BloodBPreItemDao") as IDBloodBPreItemDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBPreItem entity = new BloodBPreItem();
        	entity.Id = longGUID; 
entity.BPreItemNo = "测试"; 
entity.MsResult1 = "测试"; 
entity.MsResult2 = "测试"; 
entity.MsResult3 = "测试"; 
entity.Msresult4 = "测试"; 
entity.SsResult1 = "测试"; 
entity.SsResult2 = "测试"; 
entity.SsResult3 = "测试"; 
entity.Ssresult4 = "测试"; 
entity.BPreSame = "测试"; 
entity.BPreItemCheckFlag = 1; 
entity.Gbloodscan = "测试"; 
entity.Bagprocess = "测试"; 
entity.Memo = "测试"; 
entity.DispOrder = 1; 
entity.Visible = true; 
			bool b = BloodBPreItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBPreItem entity = BloodBPreItemDao.Get(longGUID);
        	
        	bool b = BloodBPreItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBPreItem entity = BloodBPreItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBPreItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}