
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
	public class StatusTypeDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDStatusTypeDao StatusTypeDao;
        public StatusTypeDaoTest()
        {
            StatusTypeDao = context.GetObject("StatusTypeDao") as IDStatusTypeDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	StatusType entity = new StatusType();
        	entity.Id = longGUID; 
entity.CName = "测试"; 
entity.StatusDesc = "测试"; 
entity.StatusColor = "测试"; 
			bool b = StatusTypeDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	StatusType entity = StatusTypeDao.Get(longGUID);
        	
        	bool b = StatusTypeDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	StatusType entity = StatusTypeDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = StatusTypeDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}