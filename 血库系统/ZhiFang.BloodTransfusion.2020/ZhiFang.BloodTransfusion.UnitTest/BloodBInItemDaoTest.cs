
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
	public class BloodBInItemDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBInItemDao BloodBInItemDao;
        public BloodBInItemDaoTest()
        {
            BloodBInItemDao = context.GetObject("BloodBInItemDao") as IDBloodBInItemDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBInItem entity = new BloodBInItem();
        	entity.Id = longGUID; 
entity.BInItemNo = "测试"; 
entity.InTypeNo = "测试"; 
entity.BframeNo = "测试"; 
entity.BINo = "测试"; 
entity.BBagCode = "测试"; 
entity.PCode = "测试"; 
entity.ABORHCode = "测试"; 
entity.InvalidCode = "测试"; 
entity.CollectCode = "测试"; 
entity.AllCode = "测试"; 
entity.BBagExCode = "测试"; 
entity.CheckFlag = 1; 
entity.IsScanCheck = 1; 
entity.B3Code = "测试"; 
entity.IsRepeat = 1; 
entity.SUserName = "测试"; 
entity.BCCode = "测试"; 
entity.BankBloodName = "测试"; 
entity.YQCode = "测试"; 
entity.BloodoC = "测试"; 
entity.Memo = "测试"; 
entity.Visible = true; 
entity.DispOrder = 1; 
			bool b = BloodBInItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBInItem entity = BloodBInItemDao.Get(longGUID);
        	
        	bool b = BloodBInItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBInItem entity = BloodBInItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBInItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}