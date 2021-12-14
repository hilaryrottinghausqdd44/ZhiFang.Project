using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Request
{
    [DataContract]
    public class TestItemVO : TestItem
    {
        //[DataMember]
        //public virtual TestItem TestItem { get; set; }
        #region
        /// <summary>
        /// 细项
        /// </summary>
        [DataMember]
        public virtual List<GroupItemVO> SubTestItemVO { get; set; }

        #endregion
        public TestItem TransVO(TestItemVO VO)
        {
            TestItem ti = new TestItem();
            ti.Actualprice = VO.Actualprice;
            ti.BonusPercent = VO.BonusPercent;
            ti.CName = VO.CName;
            ti.Color = VO.Color;
            ti.CostPrice = VO.CostPrice;
            ti.Cuegrade = VO.Cuegrade;
            ti.DataAddTime = VO.DataAddTime;
            ti.DiagMethod = VO.DiagMethod;
            ti.DispOrder = VO.DispOrder;
            ti.EName = VO.EName;
            ti.FWorkLoad = VO.FWorkLoad;
            ti.HisDispOrder = VO.HisDispOrder;
            ti.Id = VO.Id;
            ti.InspectionPrice = VO.InspectionPrice;
            ti.IsCalc = VO.IsCalc;
            ti.IschargeItem = VO.IschargeItem;
            ti.IsCombiItem = VO.IsCombiItem;
            ti.IsDoctorItem = VO.IsDoctorItem;
            ti.IsHistoryItem = VO.IsHistoryItem;
            ti.IsProfile = VO.IsProfile;
            ti.ItemCode = VO.ItemCode;
            ti.ItemDesc = VO.ItemDesc;
            ti.ItemGUID = VO.ItemGUID;
            ti.LabIsReply = VO.LabIsReply;
            ti.LabStatusFlag = VO.LabStatusFlag;
            ti.LabUploadDate = VO.LabUploadDate;
            ti.LabUploadFlag = VO.LabUploadFlag;
            ti.Lowprice = VO.Lowprice;
            ti.MiniChargeUnit = VO.MiniChargeUnit;
            ti.OrderNo = VO.OrderNo;
            ti.Prec = VO.Prec;
            ti.Price = VO.Price;
            ti.Secretgrade = VO.Secretgrade;
            ti.ShortCode = VO.ShortCode;
            ti.ShortName = VO.ShortName;
            ti.SpecialSection = VO.SpecialSection;
            ti.SpecialType = VO.SpecialType;
            ti.SpecTypeNo = VO.SpecTypeNo;
            ti.StandardCode = VO.StandardCode;
            ti.SuperGroupNo = VO.SuperGroupNo;
            ti.Theoryprice = VO.Theoryprice;
            ti.Tstamp = VO.Tstamp;
            ti.Unit = VO.Unit;
            ti.Visible = VO.Visible;
            return ti;
        }
    }
    
}
