
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
	public class BloodPatinfoDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodPatinfoDao BloodPatinfoDao;
        public BloodPatinfoDaoTest()
        {
            BloodPatinfoDao = context.GetObject("BloodPatinfoDao") as IDBloodPatinfoDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodPatinfo entity = new BloodPatinfo();
        	entity.Id = longGUID; 
entity.HisPatID = "测试"; 
entity.VisitID = 1; 
entity.HisWardNo = "测试"; 
entity.HisDeptNo = "测试"; 
entity.AdmID = "测试"; 
entity.PatNo = "测试"; 
entity.Bed = "测试"; 
entity.WristBandNo = "测试"; 
entity.CName = "测试"; 
entity.Sex = "测试"; 
entity.Age = 1; 
entity.AgeuNit = "测试"; 
entity.AgeALL = "测试"; 
entity.PatIdentity = "测试"; 
entity.CardId = "测试"; 
entity.IsAgree = true; 
entity.HisABOCode = "测试"; 
entity.HisRhCode = "测试"; 
entity.Gravida = true; 
entity.Birth = true; 
entity.BeforUse = true; 
entity.AddressType = "测试"; 
entity.Diag = "测试"; 
entity.IsAnesth = true; 
entity.HasAllergy = true; 
entity.Costtype = "测试"; 
entity.HasTransplant = true; 
entity.DonorABORH = "测试"; 
			bool b = BloodPatinfoDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodPatinfo entity = BloodPatinfoDao.Get(longGUID);
        	
        	bool b = BloodPatinfoDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodPatinfo entity = BloodPatinfoDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodPatinfoDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}