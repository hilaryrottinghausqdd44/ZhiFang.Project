
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
	public class SCBarCodeRulesDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDSCBarCodeRulesDao SCBarCodeRulesDao;
        public SCBarCodeRulesDaoTest()
        {
            SCBarCodeRulesDao = context.GetObject("SCBarCodeRulesDao") as IDSCBarCodeRulesDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	SCBarCodeRules entity = new SCBarCodeRules();
        	entity.Id = longGUID; 
entity.BmsType = "测试"; 
entity.CurBarCode = 1; 
			bool b = SCBarCodeRulesDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	SCBarCodeRules entity = SCBarCodeRulesDao.Get(longGUID);
        	
        	bool b = SCBarCodeRulesDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	SCBarCodeRules entity = SCBarCodeRulesDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = SCBarCodeRulesDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}