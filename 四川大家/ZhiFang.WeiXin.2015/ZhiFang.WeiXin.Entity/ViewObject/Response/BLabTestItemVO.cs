using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "医生开单的组套项目（区域项目、实验室项目）", ClassCName = "BLabTestItemVO", ShortCode = "BLabTestItemVO", Desc = "医生开单的组套项目（区域项目、实验室项目）")]
    public class BLabTestItemVO :BLabTestItem
    {
        public BLabTestItemVO TransVO(BLabTestItem entity)
        {
            ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO bltivo = new ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO();
            bltivo.Id = entity.Id;
            bltivo.DataAddTime = entity.DataAddTime;
            bltivo.CName = entity.CName;
            bltivo.DiagMethod = entity.DiagMethod;
            bltivo.DispOrder = entity.DispOrder;
            bltivo.EName = entity.EName;
            bltivo.IschargeItem = entity.IschargeItem;
            bltivo.IsCombiItem = entity.IsCombiItem;
            bltivo.IsDoctorItem = entity.IsDoctorItem;
            bltivo.IsProfile = entity.IsProfile;
            bltivo.ItemDesc = entity.ItemDesc;
            bltivo.ItemNo = entity.ItemNo;
            bltivo.LabCode = entity.LabCode;
            bltivo.LabSuperGroupNo = entity.LabSuperGroupNo;
            bltivo.Price = entity.Price;
            bltivo.MarketPrice = entity.MarketPrice;
            bltivo.GreatMasterPrice = entity.GreatMasterPrice;
            bltivo.ShortCode = entity.ShortCode;
            bltivo.ShortName = entity.ShortName;
            bltivo.Unit = entity.Unit;
            bltivo.UseFlag = entity.UseFlag;
            bltivo.Visible = entity.Visible;
            bltivo.Pic = entity.Pic;
            bltivo.BonusPercent = entity.BonusPercent;
            bltivo.CostPrice = entity.CostPrice;
            bltivo.InspectionPrice = entity.InspectionPrice;
            bltivo.IsMappingFlag = entity.IsMappingFlag;
            return bltivo;
        }
    }
}
