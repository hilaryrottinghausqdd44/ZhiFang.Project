
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
    public class ReaBmsOutDocDaoTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaBmsOutDocDao ReaBmsOutDocDao;
        IBReaBmsOutDoc IBReaBmsOutDoc;
        //IBReaBmsOutDtl IBReaBmsOutDtl;
        public ReaBmsOutDocDaoTest()
        {
            ReaBmsOutDocDao = context.GetObject("ReaBmsOutDocDao") as IDReaBmsOutDocDao;
            IBReaBmsOutDoc = context.GetObject("BReaBmsOutDoc") as IBReaBmsOutDoc;
            //IBReaBmsOutDtl = context.GetObject("BReaBmsOutDtl") as IBReaBmsOutDtl;
        }
        /// <summary>
        /// 新增出库申请测试
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtAddList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        [TestMethod]
        public void AddReaBmsOutDocAndDtl()
        {
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            long empID = long.Parse(employeeID);
            long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
            string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
            string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

            ReaBmsOutDoc entity = GetReaBmsOutDoc();
            IList<ReaBmsOutDtl> dtAddList = new List<ReaBmsOutDtl>();

            BaseResultDataValue brdv = IBReaBmsOutDoc.AddReaBmsOutDocAndDtlOfApply(entity, dtAddList, false,false, empID, empName);
            Assert.AreEqual(true, brdv.success);
        }
        [TestMethod]
        public void TestSave()
        {
            ReaBmsOutDoc entity = GetReaBmsOutDoc();
            bool b = ReaBmsOutDocDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        private static ReaBmsOutDoc GetReaBmsOutDoc()
        {
            ReaBmsOutDoc entity = new ReaBmsOutDoc();
            entity.Id = longGUID;
            entity.Status = 1;
            entity.PrintTimes = 1;
            entity.ZX1 = "测试";
            entity.ZX2 = "测试";
            entity.ZX3 = "测试";
            entity.DispOrder = 1;
            entity.Memo = "测试";
            entity.Visible = true;
            entity.CreaterName = "测试";
            entity.DeptName = "测试";
            entity.OutTypeName = "测试";
            entity.StatusName = "测试";
            entity.OperateOutDocNo = "测试";
            entity.OutDocNo = "测试";
            entity.TakerName = "测试";
            entity.CheckName = "测试";
            entity.CheckMemo = "测试";
            return entity;
        }

        [TestMethod]
        public void TestUpdate()
        {
            ReaBmsOutDoc entity = ReaBmsOutDocDao.Get(longGUID);

            bool b = ReaBmsOutDocDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            ReaBmsOutDoc entity = ReaBmsOutDocDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = ReaBmsOutDocDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}