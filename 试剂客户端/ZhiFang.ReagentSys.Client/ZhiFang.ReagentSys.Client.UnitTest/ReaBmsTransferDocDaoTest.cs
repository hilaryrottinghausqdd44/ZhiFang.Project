
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.ReagentSys.Client.UnitTest
{
    [TestClass]
    public class ReaBmsTransferDocDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaBmsTransferDocDao ReaBmsTransferDocDao;
        IBReaBmsTransferDoc IBReaBmsTransferDoc;
        //IBReaBmsTransferDtl IBReaBmsTransferDtl;
        public ReaBmsTransferDocDaoTest()
        {
            ReaBmsTransferDocDao = context.GetObject("ReaBmsTransferDocDao") as IDReaBmsTransferDocDao;
            IBReaBmsTransferDoc = context.GetObject("BReaBmsTransferDoc") as IBReaBmsTransferDoc;
            //IBReaBmsTransferDtl = context.GetObject("BReaBmsTransferDtl") as IBReaBmsTransferDtl;
        }

        /// <summary>
        /// 新增移库申请测试
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtAddList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        [TestMethod]
        public void AddReaBmsTransferDocAndDtl()
        {
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            long empID = long.Parse(employeeID);
            long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
            string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
            string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

            ReaBmsTransferDoc entity = GetReaBmsTransferDoc();
            IList<ReaBmsTransferDtl> dtAddList = new List<ReaBmsTransferDtl>();

            BaseResultDataValue brdv = IBReaBmsTransferDoc.AddTransferDocAndDtlOfApply(entity, dtAddList, false, empID, empName);
            Assert.AreEqual(true, brdv.success);
        }
        [TestMethod]
        public void TestSave()
        {
            ReaBmsTransferDoc entity = GetReaBmsTransferDoc();
            bool b = ReaBmsTransferDocDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        private static ReaBmsTransferDoc GetReaBmsTransferDoc()
        {
            ReaBmsTransferDoc entity = new ReaBmsTransferDoc();
            entity.Id = longGUID;
            entity.DeptName = "测试";
            entity.TransferTypeName = "测试";
            entity.Status = 1;
            entity.StatusName = "测试";
            entity.SStorageName = "测试";
            entity.DStorageName = "测试";
            entity.OperName = "测试";
            entity.PrintTimes = 1;
            entity.ZX1 = "测试";
            entity.ZX2 = "测试";
            entity.ZX3 = "测试";
            entity.DispOrder = 1;
            entity.Memo = "测试";
            entity.Visible = true;
            entity.CreaterName = "测试";
            entity.TransferDocNo = "测试";
            entity.TakerName = "测试";
            entity.CheckName = "测试";
            entity.CheckMemo = "测试";
            return entity;
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaBmsTransferDoc entity = ReaBmsTransferDocDao.Get(longGUID);

            bool b = ReaBmsTransferDocDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaBmsTransferDoc entity = ReaBmsTransferDocDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaBmsTransferDocDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}