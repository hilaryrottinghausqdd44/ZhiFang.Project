
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
	public class BloodInterfaceTransportDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodInterfaceTransportDao BloodInterfaceTransportDao;
        public BloodInterfaceTransportDaoTest()
        {
            BloodInterfaceTransportDao = context.GetObject("BloodInterfaceTransportDao") as IDBloodInterfaceTransportDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodInterfaceTransport entity = new BloodInterfaceTransport();
        	entity.Id = longGUID; 
entity.TypeName = "测试"; 
entity.DockingParty = "测试"; 
entity.OperatId = "测试"; 
entity.OperatName = "测试"; 
entity.TableName = "测试"; 
entity.PrimaryKey = "测试"; 
entity.TransportContent = "测试"; 
entity.ReturnResult = "测试"; 
entity.Status = "测试"; 
entity.Description = "测试"; 
entity.SingleNumber = "测试"; 
entity.DispOrder = 1; 
entity.Visible = true; 
			bool b = BloodInterfaceTransportDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodInterfaceTransport entity = BloodInterfaceTransportDao.Get(longGUID);
        	
        	bool b = BloodInterfaceTransportDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodInterfaceTransport entity = BloodInterfaceTransportDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodInterfaceTransportDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}