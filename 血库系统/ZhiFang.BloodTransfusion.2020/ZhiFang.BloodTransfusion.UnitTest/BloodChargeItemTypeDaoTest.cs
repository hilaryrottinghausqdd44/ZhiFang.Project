
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
	public class BloodChargeItemTypeDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodChargeItemTypeDao BloodChargeItemTypeDao;
        public BloodChargeItemTypeDaoTest()
        {
            BloodChargeItemTypeDao = context.GetObject("BloodChargeItemTypeDao") as IDBloodChargeItemTypeDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodChargeItemType entity = new BloodChargeItemType();
        	entity.Id = longGUID; 
entity.CName = "测试"; 
entity.SName = "测试"; 
entity.PinYinZiTou = "测试"; 
entity.ShortCode = "测试"; 
entity.IsUse = true; 
entity.DispOrder = 1; 
			bool b = BloodChargeItemTypeDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodChargeItemType entity = BloodChargeItemTypeDao.Get(longGUID);
        	
        	bool b = BloodChargeItemTypeDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodChargeItemType entity = BloodChargeItemTypeDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodChargeItemTypeDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}