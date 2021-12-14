
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
	public class BloodBReqFormDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodBReqFormDao BloodBReqFormDao;
        public BloodBReqFormDaoTest()
        {
            BloodBReqFormDao = context.GetObject("BloodBReqFormDao") as IDBloodBReqFormDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodBReqForm entity = new BloodBReqForm();
        	entity.Id = longGUID; 
entity.ReqFormNo = "测试"; 
entity.BloodOrderID = "测试"; 
entity.HisOrderID = "测试"; 
entity.HisWardNo = "测试"; 
entity.WardCName = "测试"; 
entity.HisDeptNo = "测试"; 
entity.DeptCName = "测试"; 
entity.BeforUse = true; 
entity.BarCode = "测试"; 
entity.HisABOCode = "测试"; 
entity.PatABO = "测试"; 
entity.HisrhCode = "测试"; 
entity.PatRh = "测试"; 
entity.UseWay = "测试"; 
entity.Evaluation = "测试"; 
entity.HisDoctorId = "测试"; 
entity.ApplyName = "测试"; 
entity.ApplyMemo = "测试"; 
entity.SeniorName = "测试"; 
entity.SeniorMemo = "测试"; 
entity.DirectorName = "测试"; 
entity.DirectorMemo = "测试"; 
entity.MedicalName = "测试"; 
entity.MedicalMemo = "测试"; 
entity.ObsoleteName = "测试"; 
entity.ObsoleteMemo = "测试"; 
entity.CheckCompleteFlag = true; 
entity.BTransReviewName = "测试"; 
entity.BTransReviewFlag = 1; 
entity.BreqStatusName = "测试"; 
entity.ToHisFlag = 1; 
entity.BLPreEvaluation = "测试"; 
entity.PrintTotal = 1; 
entity.YizhuCode = "测试"; 
entity.Memo = "测试"; 
entity.Visible = true; 
entity.DispOrder = 1; 
			bool b = BloodBReqFormDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodBReqForm entity = BloodBReqFormDao.Get(longGUID);
        	
        	bool b = BloodBReqFormDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodBReqForm entity = BloodBReqFormDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodBReqFormDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}