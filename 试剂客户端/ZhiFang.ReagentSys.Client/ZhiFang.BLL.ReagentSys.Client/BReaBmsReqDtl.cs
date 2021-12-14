
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using System.Collections.Generic;
using System;
using System.Text;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Linq;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsReqDtl : BaseBLL<ReaBmsReqDtl>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsReqDtl
    {
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IDReaBmsQtyDtlDao IDReaBmsQtyDtlDao { get; set; }
        IDReaBmsOutDtlDao IDReaBmsOutDtlDao { get; set; }

        public BaseResultBool EditDtlListCheck(IList<ReaBmsReqDtl> dtEditList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtEditList != null && dtEditList.Count > 0)
            {
                foreach (var entity in dtEditList)
                {
                    if (tempBaseResultBool.success == false) break;

                    ReaGoodsOrgLink reaGoodsOrgLink = IDReaGoodsOrgLinkDao.Get(entity.CompGoodsLinkID.Value);
                    if (reaGoodsOrgLink == null)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "货品为:" + entity.GoodsCName + ",供货商货品关系ID为:" + entity.CompGoodsLinkID.Value + ",已从供货商货品维护里删除,请从申请明细里移除该货品再保存!建议删除原货品申请模板重新维护新的申请货品模板!";
                    }
                    else if (reaGoodsOrgLink.Visible == 0)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "货品为:" + entity.GoodsCName + ",供货商货品关系ID为:" + entity.CompGoodsLinkID.Value + ",已在供货商货品维护里禁用,请从申请明细里移除该货品再保存!建议删除原货品申请模板重新维护新的申请货品模板!";
                    }
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool AddDtList(IList<ReaBmsReqDtl> dtAddList, ReaBmsReqDoc reaBmsReqDoc, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool = EditDtlListCheck(dtAddList);
            if (tempBaseResultBool.success == false) return tempBaseResultBool;

            if (dtAddList != null && dtAddList.Count > 0)
            {
                if (reaBmsReqDoc.DataTimeStamp == null)
                {
                    byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                    reaBmsReqDoc.DataTimeStamp = dataTimeStamp;
                }
                foreach (var entity in dtAddList)
                {
                    if (tempBaseResultBool.success == false) break;

                    entity.ReqDtlNo = this.GetReqDtlNo();
                    entity.ReaBmsReqDoc = reaBmsReqDoc;
                    entity.ReqDocNo = reaBmsReqDoc.ReqDocNo;
                    entity.CreaterID = empID;
                    entity.CreaterName = empName;
                    entity.Visible = true;
                    entity.DataUpdateTime = DateTime.Now;
                    if (!entity.GoodsQty.HasValue)
                        entity.GoodsQty = 0;
                    if (!entity.Price.HasValue)
                        entity.Price = 0;
                    entity.SumTotal = entity.Price * entity.GoodsQty;
                    this.Entity = entity;
                    tempBaseResultBool.success = this.Add();

                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "货品为:" + entity.GoodsCName + ",新增保存失败!";
                    }
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditDtList(IList<ReaBmsReqDtl> dtEditList, ReaBmsReqDoc reaBmsReqDoc)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            tempBaseResultBool = EditDtlListCheck(dtEditList);
            if (tempBaseResultBool.success == false) return tempBaseResultBool;

            if (dtEditList != null && dtEditList.Count > 0)
            {
                List<string> tmpa = new List<string>();
                foreach (var entity in dtEditList)
                {
                    tmpa.Clear();
                    if (tempBaseResultBool.success == false) break;

                    tmpa.Add("Id=" + entity.Id + " ");
                    if (!entity.ReqGoodsQty.HasValue)
                        entity.ReqGoodsQty = 0;
                    tmpa.Add("ReqGoodsQty=" + entity.ReqGoodsQty + "");
                    if (!entity.GoodsQty.HasValue)
                        entity.GoodsQty = 0;
                    tmpa.Add("GoodsQty=" + entity.GoodsQty + "");
                    if (!entity.CurrentQty.HasValue)
                        entity.CurrentQty = 0;
                    tmpa.Add("CurrentQty=" + entity.CurrentQty + "");
                    if (!entity.ExpectedStock.HasValue)
                        entity.ExpectedStock = 0;
                    tmpa.Add("ExpectedStock=" + entity.ExpectedStock + "");
                    if (!entity.Price.HasValue)
                        entity.Price = 0;
                    entity.SumTotal = entity.Price * entity.GoodsQty;
                    tmpa.Add("Price=" + entity.Price + "");
                    tmpa.Add("SumTotal=" + entity.SumTotal + "");

                    if (entity.CompGoodsLinkID.HasValue) tmpa.Add("CompGoodsLinkID=" + entity.CompGoodsLinkID);
                    //if (entity.GoodsUnitID.HasValue) tmpa.Add("GoodsUnitID=" + entity.GoodsUnitID);
                    if (!string.IsNullOrEmpty(entity.GoodsUnit)) tmpa.Add("GoodsUnit='" + entity.GoodsUnit + "'");
                    if (entity.ArrivalTime.HasValue)
                        tmpa.Add("ArrivalTime='" + entity.ArrivalTime + "'");
                    else
                        tmpa.Add("ArrivalTime=null");

                    tmpa.Add("Memo='" + entity.Memo + "'");
                    if (entity.ReaCenOrg != null)
                    {
                        tmpa.Add("ReaCenOrg.Id=" + entity.ReaCenOrg.Id + "");
                        tmpa.Add("OrgName='" + entity.OrgName + "'");
                    }
                    else
                    {
                        tmpa.Add("ReaCenOrg.Id=null");
                        tmpa.Add("OrgName=null");
                    }
                    //this.Entity = item;
                    tempBaseResultBool.success = this.Update(tmpa.ToArray());
                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "货品为:" + entity.GoodsCName + ",编辑保存失败!";
                    }
                }
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 获取申请明细单号
        /// </summary>
        /// <returns></returns>
        private string GetReqDtlNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }

        /// <summary>
        /// 智能采购：计算某个货品的平均使用量和建议采购量
        /// </summary>
        /// <param name="goodsId">货品ID</param>
        /// <param name="m">前m个月</param>
        /// <param name="k">采购系数</param>
        /// <returns></returns>
        public ReaGoods CalcAvgUsedAndSuggestPurchaseQty(long goodsId, string m, string k)
        {
            ReaGoods entityGoods = new ReaGoods();
            //查询库存表，获取当前库存数
            IList<ReaBmsQtyDtl> tempQtyList = IDReaBmsQtyDtlDao.GetListByHQL(string.Format("GoodsID={0}", goodsId));

            //查询出库明细表，获取出库数
            string beginDate = DateTime.Now.AddMonths(-1 * int.Parse(m)).ToString("yyyy-MM-01");
            string endDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 23:59:59");
            string where = string.Format("GoodsID={0} and DataAddTime>='{1}' and DataAddTime<='{2}'", goodsId, beginDate, endDate);
            IList<ReaBmsOutDtl> tempOutDtlList = IDReaBmsOutDtlDao.GetListByHQL(where);
            if (tempOutDtlList.Count > 0)
            {
                //如果实际使用的月数>0，且<参数m，则改写m的值。
                var monthList = tempOutDtlList.Where(p => p.DataAddTime != null).Select(p => p.DataAddTime.Value.ToString("yyyy-MM")).Distinct().ToList();
                if (monthList.Count() > 0 && monthList.Count() < int.Parse(m))
                {
                    m = monthList.Count().ToString();
                }
            }

            //当前库存数
            double currentGoodsQty = tempQtyList.Where(p => p.GoodsQty != null).Sum(p => p.GoodsQty.Value);
            //平均使用量
            double avgUsedQty = (tempOutDtlList.Sum(p => p.GoodsQty)) / (int.Parse(m));
            //建议采购量，建议采购量=平均使用量*k - 当前库存量，最后值四舍五入取整数。
            double suggestPurchaseQty = avgUsedQty * double.Parse(k) - currentGoodsQty;
            if (suggestPurchaseQty <= 0)
            {
                suggestPurchaseQty = 0;
            }

            entityGoods.AvgUsedQty = int.Parse(Math.Round(avgUsedQty).ToString());
            entityGoods.SuggestPurchaseQty = int.Parse(Math.Round(suggestPurchaseQty).ToString());

            return entityGoods;

        }

        public EntityList<ReaBmsReqDtl> GetReaBmsReqDtlListByHQL(string strHqlWhere, string Order, int start, int count)
        {
            return ((IDReaBmsReqDtlDao)base.DBDao).GetReaBmsReqDtlListByHQL(strHqlWhere, Order, start, count);
        }
    }
}