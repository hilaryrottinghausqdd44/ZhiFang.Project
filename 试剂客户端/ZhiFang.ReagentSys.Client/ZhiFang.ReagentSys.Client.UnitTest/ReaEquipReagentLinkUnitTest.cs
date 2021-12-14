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
    public class ReaEquipReagentLinkUnitTest
    {
        static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDReaEquipReagentLinkDao ReaEquipReagentLinkDao;
        IDReaTestEquipLabDao ReaTestEquipLabDao;
        IDReaGoodsDao ReaGoodsDao;


        public ReaEquipReagentLinkUnitTest()
        {
            ReaEquipReagentLinkDao = context.GetObject("ReaEquipReagentLinkDao") as IDReaEquipReagentLinkDao;
            ReaTestEquipLabDao = context.GetObject("ReaTestEquipLabDao") as IDReaTestEquipLabDao;
            ReaGoodsDao = context.GetObject("ReaGoodsDao") as IDReaGoodsDao; 
        }
    
        //新增
        [TestMethod]
        public void ReaEquipReagentLinkTestAdd()
        {
            ////找到仪器数据
            ////ReaTestEquipLabDao.LoadAll();
            //IList<ReaTestEquipLab> listReaTestEquipLab = ReaTestEquipLabDao.LoadAll();
            //for (int i = 0; i < listReaTestEquipLab.Count; i++)
            //{
            //    if (!String.IsNullOrEmpty(ReaTestEquipLabID)) break;
            //    if (String.IsNullOrEmpty(ReaTestEquipLabID)) ReaTestEquipLabID = listReaTestEquipLab[i].Id.ToString();                
            //}


            ////找到货品数据
            ////ReaTestEquipLabDao.LoadAll();
            //IList<ReaGoods> listReaGoodsLab = ReaGoodsDao.LoadAll();
            //for (int i = 0; i < listReaGoodsLab.Count; i++)
            //{
            //    if (!String.IsNullOrEmpty(ReaGoodsID)) break;
            //    if (String.IsNullOrEmpty(ReaGoodsID)) ReaGoodsID = listReaGoodsLab[i].Id.ToString();
            //}

            SysPublicSet.IsSetLicense = true;
            ReaEquipReagentLink entity = new ReaEquipReagentLink();
            entity.Id = longGUID;
            // entity.TestEquipID = "10001DD2QQ1";
            //entity.GoodsID =long.Parse(ReaGoodsID);
            //entity.TestEquipID = long.Parse(ReaTestEquipLabID);
            entity.DataUpdateTime = DateTime.Now;
            entity.Memo = "测试wwwwwwwwwwwwww";
            bool b = ReaEquipReagentLinkDao.Save(entity);
            Assert.AreEqual(true, b);

        }
        //修改
        [TestMethod]
        public void ReaEquipReagentLinkTestUpadte()
        {
            SysPublicSet.IsSetLicense = true;
            ReaEquipReagentLink entity = ReaEquipReagentLinkDao.Get(longGUID);
            //entity.LotNo = "100012QQW11";
            entity.Memo = "测试DDD";
            entity.DataUpdateTime = DateTime.Now;

            bool b = ReaEquipReagentLinkDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        //查
        [TestMethod]
        public void ReaEquipReagentLinkTestGet()
        {
            SysPublicSet.IsSetLicense = true;
            ReaEquipReagentLink entity = ReaEquipReagentLinkDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        [TestMethod]
        public void ReaEquipReagentLinkTestDel()
        {
            SysPublicSet.IsSetLicense = true;
            bool b = ReaEquipReagentLinkDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
    }
}
