
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
	public class BloodInCheckedDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodInCheckedDao BloodInCheckedDao;
        public BloodInCheckedDaoTest()
        {
            BloodInCheckedDao = context.GetObject("BloodInCheckedDao") as IDBloodInCheckedDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodInChecked entity = new BloodInChecked();
        	entity.Id = longGUID; 
entity.InCheckedNo = "测试"; 
entity.Checker = "测试"; 
entity.CheckFlag = 1; 
entity.DispOrder = 1; 
entity.Visible = true; 
			bool b = BloodInCheckedDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodInChecked entity = BloodInCheckedDao.Get(longGUID);
        	
        	bool b = BloodInCheckedDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodInChecked entity = BloodInCheckedDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodInCheckedDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}