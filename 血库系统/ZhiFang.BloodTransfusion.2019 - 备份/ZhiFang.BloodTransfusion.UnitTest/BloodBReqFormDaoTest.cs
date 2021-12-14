
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
        static string longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
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
            entity.PatID = "测试";
            entity.PatNo = "测试";
            entity.CName = "测试";
            entity.Sex = "测试";
            entity.Age = 1;
            entity.AgeALL = "测试";
            entity.AgeUnit = "测试";
            entity.DeptNo = 1;
            entity.DoctorNo = 1;
            entity.Diag = "测试";
            entity.BeforUse = "测试";
            entity.Gravida = "测试";
            entity.Harm = "测试";
            entity.Help = "测试";
            entity.Drag = "测试";
            entity.AddressType = "测试";
            entity.BPatStatusID = 1;
            entity.UseTypeID = 1;
            entity.BUseTimeTypeID = 1;
            entity.UsePurpose = "测试";
            entity.BloodABONo = 1;
            entity.ReqBloodABONo = 1;
            entity.BPresOutFlag = 1;
            entity.TestFlag = 1;
            entity.ImmFlag = 1;
            entity.Newbornflag = 1;
            entity.Bed = "测试";
            entity.BarCode = "测试";
            entity.SfzNO = "测试";
            entity.ReqDoctor = "测试";
            entity.PatientCount = "测试";
            entity.BloodOrderID = "测试";
            entity.BReqTypeID = 1;
            entity.UsePlaceID = "测试";
            entity.DiscordNo = "测试";
            entity.Zx1 = "测试";
            entity.Zx2 = "测试";
            entity.Zx3 = "测试";
            entity.Memo = "测试";
            entity.Birth = "测试";
            entity.Postflag = 1;
            entity.WristBandNo = "测试";
            entity.EmergentFlag = "测试";
            entity.HisABOCode = "测试";
            entity.HisrhCode = "测试";
            entity.VisitID = 1;
            entity.CostType = "测试";
            entity.BReqFormFlag = 1;
            entity.BPreMemo = "测试";
            entity.BPreMemoEditID = "测试";
            entity.BdeptCheckID = "测试";
            entity.BPreMemoNo = "测试";
            entity.ReCheckBloodABONo = 1;
            entity.ReChecker = "测试";
            entity.IsSame = "测试";
            entity.ABORhDesc = "测试";
            entity.ReviewABORhdesc = "测试";
            entity.GetbloodHint = "测试";
            entity.Evaluation = "测试";
            entity.Pataddress = "测试";
            //entity.CheckdoctorNo = 1;
            entity.AggluName = "测试";
            entity.AggluMemo = "测试";
            entity.PatIdentity = "测试";
            entity.BarCodeMemo = "测试";
            entity.Transplant = "测试";
            entity.DonorABOrh = "测试";
            entity.AssessFormID = "测试";
            entity.BreqMainNo = "测试";
            entity.Docphone = "测试";
            entity.YqCode = "测试";
            entity.Bprotect = "测试";
            entity.Cardio = "测试";
            entity.Decom = "测试";
            entity.Around = "测试";
            entity.Icdcode = "测试";
            entity.Lostbcount = "测试";
            entity.Lostspeed = "测试";
            entity.Bodytpe = "测试";
            entity.Bpress = "测试";
            entity.Breathe = "测试";
            entity.Pulse = "测试";
            entity.Heartrate = "测试";
            entity.Urine = "测试";
            entity.Anesth = "测试";
            entity.DispOrder = 1;
            entity.Visible = true;
            entity.HisDeptID = "测试";
            entity.HisDoctorId = "测试";
            entity.CheckCompleteFlag = true;
            entity.ApplyName = "测试";
            entity.ApplyMemo = "测试";
            entity.SeniorName = "测试";
            entity.SeniorMemo = "测试";
            entity.DirectorName = "测试";
            entity.DirectorMemo = "测试";
            entity.MedicalName = "测试";
            entity.MedicalMemo = "测试";
            entity.BreqStatusName = "测试";
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