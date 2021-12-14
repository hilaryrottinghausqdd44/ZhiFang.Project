
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.WebAssist.UnitTest
{	
	[TestClass]
	public class MEMicroInoculantDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDMEMicroInoculantDao MEMicroInoculantDao;
        public MEMicroInoculantDaoTest()
        {
            MEMicroInoculantDao = context.GetObject("MEMicroInoculantDao") as IDMEMicroInoculantDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	MEMicroInoculant entity = new MEMicroInoculant();
        	entity.Id = longGUID; 
entity.SampleTypeName = "测试"; 
entity.BarCode = "测试"; 
entity.Amount = "测试"; 
entity.SampleCharacterName = "测试"; 
entity.Id = longGUID; 
entity.InoculantType = 1; 
entity.SampleTypeNo = 1; 
entity.CultureMediumName = "测试"; 
entity.CultureMediumNo = "测试"; 
entity.CultivationType = "测试"; 
entity.CultivationConditionName = "测试"; 
entity.Id = longGUID; 
entity.CultureMediumEquipResult = "测试"; 
entity.CultureTotalTime = "测试"; 
entity.WarnPositiveDatetime = "测试"; 
entity.Desc = "测试"; 
entity.EmpUserNo = 1; 
entity.EmpName = "测试"; 
entity.DeleteFlag = true; 
entity.CultureDesc = "测试"; 
entity.EquipOnlineTime = "测试"; 
entity.EquipOfflineTime = "测试"; 
entity.WarnPositiveTimelength = "测试"; 
entity.BottlePosition = "测试"; 
entity.CultureNo = "测试"; 
entity.Id = longGUID; 
entity.AutoAddInfo = "测试"; 
			bool b = MEMicroInoculantDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	MEMicroInoculant entity = MEMicroInoculantDao.Get(longGUID);
        	
        	bool b = MEMicroInoculantDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	MEMicroInoculant entity = MEMicroInoculantDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = MEMicroInoculantDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}