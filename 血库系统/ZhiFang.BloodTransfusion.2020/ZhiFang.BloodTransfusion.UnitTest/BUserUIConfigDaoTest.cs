
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
	public class BUserUIConfigDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBUserUIConfigDao BUserUIConfigDao;
        public BUserUIConfigDaoTest()
        {
            BUserUIConfigDao = context.GetObject("BUserUIConfigDao") as IDBUserUIConfigDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BUserUIConfig entity = new BUserUIConfig();
        	entity.Id = longGUID; 
entity.UserUIKey = "测试"; 
entity.UserUIName = "测试"; 
entity.TemplateTypeCName = "测试"; 
entity.UITypeName = "测试"; 
entity.IsDefault = true; 
entity.Comment = "测试"; 
entity.DispOrder = 1; 
entity.IsUse = true; 
			bool b = BUserUIConfigDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BUserUIConfig entity = BUserUIConfigDao.Get(longGUID);
        	
        	bool b = BUserUIConfigDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BUserUIConfig entity = BUserUIConfigDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BUserUIConfigDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}