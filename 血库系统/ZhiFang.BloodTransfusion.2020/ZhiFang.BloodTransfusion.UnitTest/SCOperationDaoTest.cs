
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
	public class SCOperationDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDSCOperationDao SCOperationDao;
        public SCOperationDaoTest()
        {
            SCOperationDao = context.GetObject("SCOperationDao") as IDSCOperationDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	SCOperation entity = new SCOperation();
        	entity.Id = longGUID; 
entity.TypeName = "测试"; 
entity.BusinessModuleCode = "测试"; 
entity.Memo = "测试"; 
entity.IsUse = true; 
entity.CreatorName = "测试"; 
entity.DispOrder = 1; 
			bool b = SCOperationDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	SCOperation entity = SCOperationDao.Get(longGUID);
        	
        	bool b = SCOperationDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	SCOperation entity = SCOperationDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = SCOperationDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}