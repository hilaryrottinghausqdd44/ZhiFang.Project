using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.ReagentSys.Client.UnitTest
{
    [TestClass]
    public class CenOrgUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDCenOrgDao CenOrgDao;
        IDCenOrgTypeDao CenOrgTypeDao;
        public CenOrgUnitTest()
        {
            CenOrgDao = context.GetObject("CenOrgDao") as IDCenOrgDao;
            CenOrgTypeDao = context.GetObject("CenOrgTypeDao") as IDCenOrgTypeDao;
        }
        string OrgTypeID = "";
        bool IsSuccess = true;
        //新增
        [TestMethod]
        public void CenOrgTestAdd()
        {

            //找到其中一个类型
            IList<CenOrgType> listCenOrgType = CenOrgTypeDao.LoadAll();
            for (int i = 0; i < listCenOrgType.Count; i++)
            {
                if (!String.IsNullOrEmpty(OrgTypeID)) break;
                if (String.IsNullOrEmpty(OrgTypeID)) OrgTypeID = listCenOrgType[i].Id.ToString();
            }
            if (!String.IsNullOrEmpty(OrgTypeID))
            {
                SysPublicSet.IsSetLicense = true;
                CenOrg entity = new CenOrg();
                entity.Id = longGUID;
                entity.CName = "10001DD2QQ1";
                entity.EName = "DDDD";
                entity.OrgNo = 1282617210;
                entity.OrgTypeID = long.Parse(OrgTypeID);
                entity.DataUpdateTime = DateTime.Now;
                //entity.GoodsID = 4623513945469361870;
                entity.Memo = "测试";
                bool b = CenOrgDao.Save(entity);
                IsSuccess = b;
                Assert.AreEqual(true, b);
            }

        }
        //修改
        [TestMethod]
        public void CenOrgTestUpadte()
        {
            if (IsSuccess)
            {
                SysPublicSet.IsSetLicense = true;
                CenOrg entity = CenOrgDao.Get(longGUID);
                entity.CName = "EEQ";
                entity.Memo = "测试DDD";
                entity.DataUpdateTime = DateTime.Now;

                bool b = CenOrgDao.Update(entity);
                Assert.AreEqual(true, b);
            }
           
            //int b = CenOrgDao.UpdateByHql("update CenOrg  set CName='测试2' where Id=" + longGUID);
            //Assert.AreEqual(1, b);
        }
        //查
        [TestMethod]
        public void CenOrgTestGet()
        {
            if (IsSuccess)
            {
                SysPublicSet.IsSetLicense = true;
                CenOrg entity = CenOrgDao.Get(longGUID);
                Assert.AreEqual(entity.Id, longGUID);
            }
        }
        [TestMethod]
        public void CenOrgTestDel()
        {
            if (IsSuccess)
            {
                SysPublicSet.IsSetLicense = true;
                bool b = CenOrgDao.Delete(longGUID);
                Assert.AreEqual(true, b);
            }
        }
    }
}
