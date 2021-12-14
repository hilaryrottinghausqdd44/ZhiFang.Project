
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
	public class BDictDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBDictDao BDictDao;
        public BDictDaoTest()
        {
            BDictDao = context.GetObject("BDictDao") as IDBDictDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BDict entity = new BDict();
        	entity.Id = longGUID; 
entity.CName = "测试"; 
entity.SName = "测试"; 
entity.ShortCode = "测试"; 
entity.PinYinZiTou = "测试"; 
entity.HisOrderCode = "测试"; 
entity.IsUse = true; 
entity.DispOrder = 1; 
entity.Memo = "测试"; 
			bool b = BDictDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BDict entity = BDictDao.Get(longGUID);
        	
        	bool b = BDictDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BDict entity = BDictDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BDictDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}