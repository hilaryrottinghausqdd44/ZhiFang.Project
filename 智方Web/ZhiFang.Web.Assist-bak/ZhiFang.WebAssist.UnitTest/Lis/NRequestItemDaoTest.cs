
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.WebAssist.UnitTest
{
    [TestClass]
    public class NRequestItemDaoTest
    {
        static string longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDNRequestItemDao NRequestItemDao;
        public NRequestItemDaoTest()
        {
            NRequestItemDao = context.GetObject("NRequestItemDao") as IDNRequestItemDao;
        }

        [TestMethod]
        public void TestSave()
        {
            NRequestItem entity = new NRequestItem();
            entity.Id = longGUID;
            entity.Id = longGUID;
            entity.OrderNo = "测试";
            entity.SampleTypeNo = "测试";
            entity.Id = longGUID;
            entity.Id = longGUID;
            entity.ItemNo = 1;
            entity.Id = longGUID;
            entity.Zdy1 = "测试";
            entity.Zdy2 = "测试";
            entity.Zdy3 = "测试";
            entity.Zdy4 = "测试";
            entity.Zdy5 = "测试";
            entity.SamplingGroupNo = 1;
            entity.Id = longGUID;
            entity.UniteName = "测试";
            entity.UniteItemNo = "测试";
            entity.IsCheckFee = 1;
            entity.CountNodesItemSource = "测试";
            entity.StateFlag = 1;
            entity.ItemDispenseFlag = 1;
            entity.PItemCName = "测试";
            entity.PItemNo = 1;
            entity.IsNurseDo = 1;
            entity.PrepaymentFlag = 1;
            entity.SerialNoParent = "测试";
            entity.ChargeFlag = 1;
            entity.Sampleno = "测试";
            entity.Mergeno = "测试";
            entity.OldParItemno = "测试";
            entity.SectionNo = 1;
            entity.TestTypeNo = 1;
            entity.NRequestItemNo = "测试";
            entity.OldNRequestItemNo = "测试";
            entity.CheckFlag = 1;
            entity.ReportDateMemo = "测试";
            entity.Id = longGUID;
            entity.ReportDateGroup = "测试";
            entity.GroupItemList = "测试";
            entity.ItemGroupNo = 1;
            //entity.iAutoUnion = 1;
            entity.AutoUnionSNo = "测试";
            entity.DispOrder = 1;
            //entity.iNeedUnionCount = 1;
            entity.NPItemNo = 1;
            entity.Number = 1;
            entity.CancelReason = "测试";
            bool b = NRequestItemDao.Save(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestUpdate()
        {
            NRequestItem entity = NRequestItemDao.Get(longGUID);

            bool b = NRequestItemDao.Update(entity);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestGet()
        {
            NRequestItem entity = NRequestItemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }

        [TestMethod]
        public void TestDel()
        {
            bool b = NRequestItemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }

    }
}