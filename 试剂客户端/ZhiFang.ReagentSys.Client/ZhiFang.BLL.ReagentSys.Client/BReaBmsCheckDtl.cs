
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsCheckDtl : BaseBLL<ReaBmsCheckDtl>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsCheckDtl
    {
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IBReaGoodsLot IBReaGoodsLot { get; set; }
        public override bool Add()
        {
            if (this.Entity.CompGoodsLinkID.HasValue)
            {
                ReaGoodsOrgLink goodsOrgLink = IDReaGoodsOrgLinkDao.Get(this.Entity.CompGoodsLinkID.Value);
                if (goodsOrgLink != null)
                {
                    this.Entity.GoodsSort = goodsOrgLink.ReaGoods.GoodsSort;
                    this.Entity.ProdGoodsNo = goodsOrgLink.ReaGoods.ProdGoodsNo;
                    this.Entity.CenOrgGoodsNo = goodsOrgLink.CenOrgGoodsNo;
                    this.Entity.ReaCompCode = goodsOrgLink.CenOrg.OrgNo.ToString();
                    this.Entity.ReaServerCompCode = goodsOrgLink.CenOrg.PlatformOrgNo.ToString();
                    if (!this.Entity.Price.HasValue || this.Entity.Price <= 0)
                    {
                        this.Entity.Price = goodsOrgLink.Price;
                        this.Entity.SumTotal = this.Entity.Price * this.Entity.GoodsQty;
                    }
                }
            }
            bool a = DBDao.Save(this.Entity);
            return a;
        }
        public BaseResultBool AddCheckDtlOfList(ReaBmsCheckDoc doc, IList<ReaBmsCheckDtl> dtAddList, long empID, string empName, bool isTakenFromQty)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            if (dtAddList == null || dtAddList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("盘库明细信息为空!");
                return tempBaseResultBool;
            }
            foreach (var model in dtAddList)
            {
                if (model.CheckQty == model.GoodsQty)
                {
                    model.IsException = 0;
                    model.IsHandleException = 1;
                }
                model.CheckDocID = doc.Id;
                model.DataUpdateTime = DateTime.Now;
                model.Visible = true;
                if (!model.Price.HasValue && model.GoodsQty.HasValue && model.GoodsQty.Value > 0)
                    model.Price = model.SumTotal / model.GoodsQty;
                else
                {
                    model.Price = 0;
                    model.SumTotal = 0;
                }
                if (isTakenFromQty==true) {
                    model.CheckQty = model.GoodsQty;
                    model.IsException =0;
                    model.IsHandleException = 0;
                }
                this.Entity = model;
                ReaGoodsLot reaGoodsLot = null;
                AddReaGoodsLot(ref reaGoodsLot, empID, empName);
                if (reaGoodsLot != null)
                    this.Entity.GoodsLotID = reaGoodsLot.Id;
                tempBaseResultBool.success = this.Add();
                if (tempBaseResultBool.success == false)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "保存盘库明细信息失败!";
                }
                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        private void AddReaGoodsLot(ref ReaGoodsLot reaGoodsLot, long empID, string empName)
        {
            if (this.Entity == null) return;
            if (string.IsNullOrEmpty(this.Entity.LotNo)) return;
            if (this.Entity.GoodsID == null) return;
            if (!this.Entity.InvalidDate.HasValue) return;

            ReaGoodsLot lot = new ReaGoodsLot();
            lot.LabID = this.Entity.LabID;
            lot.Visible = true;
            lot.GoodsID = this.Entity.GoodsID.Value;
            lot.ReaGoodsNo = this.Entity.ReaGoodsNo;
            lot.LotNo = this.Entity.LotNo;
            lot.ProdDate = this.Entity.ProdDate;
            lot.GoodsCName = this.Entity.GoodsName;
            lot.InvalidDate = this.Entity.InvalidDate;
            lot.CreaterID = empID;
            lot.CreaterName = empName;
            IBReaGoodsLot.Entity = lot;
            BaseResultBool baseResultBool = IBReaGoodsLot.AddAndValid(ref reaGoodsLot);
        }
        public BaseResultBool EditCheckDtlOfList(ReaBmsCheckDoc doc, IList<ReaBmsCheckDtl> dtEditList, string fieldsDtl, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtEditList == null || dtEditList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("盘库明细信息为空!");
                return tempBaseResultBool;
            }
            foreach (ReaBmsCheckDtl entity in dtEditList)
            {
                entity.DataUpdateTime = DateTime.Now;
                if (entity.CheckQty == entity.GoodsQty)
                {
                    entity.IsException = 0;
                    entity.IsHandleException = 1;
                }
                this.Entity = entity;
                string[] tempDtlArray = ServiceCommon.RBAC.CommonServiceMethod.GetUpdateFieldValueStr(this.Entity, fieldsDtl);
                tempBaseResultBool.success = this.Update(tempDtlArray);
                if (tempBaseResultBool.success == false)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "保存盘库明细信息失败!";
                }
                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        public IList<ReaBmsCheckDtl> SearchReaBmsCheckDtlListByJoinHQL(string checkHql, string checkDtlHql, string reaGoodHql, string sort, int page, int limit)
        {
            return ((IDReaBmsCheckDtlDao)base.DBDao).SearchReaBmsCheckDtlListByJoinHQL(checkHql, checkDtlHql, reaGoodHql, sort, page, limit);
        }
        public EntityList<ReaBmsCheckDtl> SearchReaBmsCheckDtlEntityListByJoinHQL(string checkHql, string checkDtlHql, string reaGoodHql, string sort, int page, int limit)
        {
            return ((IDReaBmsCheckDtlDao)base.DBDao).SearchReaBmsCheckDtlEntityListByJoinHQL(checkHql, checkDtlHql, reaGoodHql, sort, page, limit);
        }
    }
}