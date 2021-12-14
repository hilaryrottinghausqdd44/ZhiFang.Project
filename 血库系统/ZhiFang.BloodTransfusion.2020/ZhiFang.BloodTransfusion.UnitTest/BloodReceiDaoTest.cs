
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
	public class BloodReceiDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDBloodReceiDao BloodReceiDao;
        public BloodReceiDaoTest()
        {
            BloodReceiDao = context.GetObject("BloodReceiDao") as IDBloodReceiDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	BloodRecei entity = new BloodRecei();
        	entity.Id = longGUID; 
entity.BSourceId = "测试"; 
entity.BarCode = "测试"; 
entity.BisbarCode = "测试"; 
entity.IsCharge = 1; 
entity.BrSampleNo = "测试"; 
entity.FrameNo = "测试"; 
entity.BitNo = "测试"; 
entity.InvalidFlag = true; 
entity.Printflag = 1; 
entity.StandOperatorName = "测试"; 
entity.CollectName = "测试"; 
entity.NurseSender = "测试"; 
entity.DeliveryrName = "测试"; 
entity.Deliveryflag = 1; 
entity.Scanflag = 1; 
entity.SUserName = "测试"; 
entity.Incepter = "测试"; 
entity.ReViewReportType = "测试"; 
entity.AboComparedFlag = "测试"; 
entity.LisAboResult = "测试"; 
entity.LisReViewAboResult = "测试"; 
entity.AboReportDesc = "测试"; 
entity.RhReportDesc = "测试"; 
entity.ReViewAboReportDesc = "测试"; 
entity.ReViewRHReportDesc = "测试"; 
entity.AboReportDescDemoNoList = "测试"; 
entity.AboReportDescDemoNameList = "测试"; 
entity.RhReportDescDemoNoList = "测试"; 
entity.RhReportDescDemoNameList = "测试"; 
entity.SrResult1DemoNoList = "测试"; 
entity.SrResult1DemoNameList = "测试"; 
entity.SrResult2DemoNoList = "测试"; 
entity.SrResult2DemoNameList = "测试"; 
entity.ReViewAboReportDescDemo = "测试"; 
entity.ReViewRhReportDescDemo = "测试"; 
entity.Visible = true; 
entity.DispOrder = 1; 
			bool b = BloodReceiDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	BloodRecei entity = BloodReceiDao.Get(longGUID);
        	
        	bool b = BloodReceiDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	BloodRecei entity = BloodReceiDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = BloodReceiDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}