
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
	public class SCBagOperationDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDSCBagOperationDao SCBagOperationDao;
        public SCBagOperationDaoTest()
        {
            SCBagOperationDao = context.GetObject("SCBagOperationDao") as IDSCBagOperationDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	SCBagOperation entity = new SCBagOperation();
        	entity.Id = longGUID; 
entity.BusinessModuleCode = "测试"; 
entity.DispOrder = 1; 
entity.Visible = true; 
entity.OperatorName = "测试"; 
entity.Memo = "测试"; 
			bool b = SCBagOperationDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	SCBagOperation entity = SCBagOperationDao.Get(longGUID);
        	
        	bool b = SCBagOperationDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	SCBagOperation entity = SCBagOperationDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = SCBagOperationDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}