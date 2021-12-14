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
    public class ReaGoodsRegisterUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaGoodsRegisterDao ReaGoodsRegisterDao;
        IDReaCenOrgDao ReaCenOrgDao;
        public ReaGoodsRegisterUnitTest()
        {
            ReaGoodsRegisterDao = context.GetObject("ReaGoodsRegisterDao") as IDReaGoodsRegisterDao;
            ReaCenOrgDao = context.GetObject("ReaCenOrgDao") as IDReaCenOrgDao;
        }
        string ReaCenOrgID = "";
        bool IsSuccess = true;
        //新增
        [TestMethod]
        public void ReaGoodsRegisterTestAdd()
        {
            //找到供应商数据
            //ReaTestEquipLabDao.LoadAll();
            IList<ReaCenOrg> listReaCenOrg = ReaCenOrgDao.LoadAll();
            for (int i = 0; i < listReaCenOrg.Count; i++)
            {
                if (!String.IsNullOrEmpty(ReaCenOrgID)) break;
                if (String.IsNullOrEmpty(ReaCenOrgID)) ReaCenOrgID = listReaCenOrg[i].Id.ToString();
            }
            if (!String.IsNullOrEmpty(ReaCenOrgID))
            {
                SysPublicSet.IsSetLicense = true;
                ReaGoodsRegister entity = new ReaGoodsRegister();
                entity.Id = longGUID;
                entity.FactoryID =long.Parse(ReaCenOrgID);
                entity.CompID = long.Parse(ReaCenOrgID);
                // entity.TestEquipID = "10001DD2QQ1";
                //entity.GoodsID =long.Parse(ReaGoodsID);
                //entity.TestEquipID = long.Parse(ReaTestEquipLabID);
                entity.ZX2 = "测试wwwwwwwwwwwwww";
                bool b = ReaGoodsRegisterDao.Save(entity);
                IsSuccess = b;
                Assert.AreEqual(true, b);
            }

        }
        //修改
        [TestMethod]
        public void ReaGoodsRegisterTestUpadte()
        {
            if (!IsSuccess)
            {
                SysPublicSet.IsSetLicense = true;
                ReaGoodsRegister entity = ReaGoodsRegisterDao.Get(longGUID);
                //entity.LotNo = "100012QQW11";
                entity.ZX1 = "测试DDD";

                bool b = ReaGoodsRegisterDao.Update(entity);
                Assert.AreEqual(true, b);
            }
            
        }
        //查
        [TestMethod]
        public void ReaGoodsRegisterTestGet()
        {
            if (!IsSuccess)
            {
                SysPublicSet.IsSetLicense = true;
                ReaGoodsRegister entity = ReaGoodsRegisterDao.Get(longGUID);
                Assert.AreEqual(entity.Id, longGUID);
            }
        }
        [TestMethod]
        public void ReaGoodsRegisterTestDel()
        {
            if (!IsSuccess)
            {
                SysPublicSet.IsSetLicense = true;
                bool b = ReaGoodsRegisterDao.Delete(longGUID);
                Assert.AreEqual(true, b);
            }
        }
    }
}
