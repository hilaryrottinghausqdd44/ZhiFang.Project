
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
    public class BReaBmsQtyBalanceDoc : BaseBLL<ReaBmsQtyBalanceDoc>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsQtyBalanceDoc
    {
        IDReaBmsQtyDtlOperationDao IDReaBmsQtyDtlOperationDao { get; set; }
        IBReaBmsQtyBalanceDtl IBReaBmsQtyBalanceDtl { get; set; }
        IBReaBmsQtyDtl IBReaBmsQtyDtl { get; set; }

        #region 按库存结转单新增库存结转
        public BaseResultDataValue AddReaBmsQtyBalanceDocOfQtyBalance(ReaBmsQtyBalanceDoc entity, long empID, string empName, bool isCover, string beginDate, string endDate)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            IList<ReaBmsQtyDtl> tempQtyList = IBReaBmsQtyDtl.SearchListByHQL("reabmsqtydtl.GoodsQty>0");
            if (tempQtyList == null || tempQtyList.Count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "没有符合库存结转的库存信息!";
                return tempBaseResultDataValue;
            }

            //需要覆盖作废当前月已生成的库存结转单
            if (tempBaseResultDataValue.success == true && isCover == true)
                tempBaseResultDataValue = EditReaBmsQtyBalanceDocInvalid(beginDate, endDate, empID, empName);
            if (tempBaseResultDataValue.success == false)
                return tempBaseResultDataValue;

            //获取上次(最近一次有效的库存结转单信息)
            ReaBmsQtyBalanceDoc preBalanceDoc = null;
            string preWhere = string.Format("reabmsqtybalancedoc.Visible=1");
            string preOrder = " reabmsqtybalancedoc.DataUpdateTime desc";
            EntityList<ReaBmsQtyBalanceDoc> preList = this.SearchListByHQL(preWhere, preOrder, 1, 1);
            if (preList.count > 0)
            {
                preBalanceDoc = preList.list[0];
            }
            if (preBalanceDoc != null)
            {
                entity.PreQtyBalanceDocID = preBalanceDoc.Id;
                entity.PreQtyBalanceDocNo = preBalanceDoc.QtyBalanceDocNo;
                entity.PreBalanceDateTime = preBalanceDoc.DataAddTime;
            }
            entity.QtyBalanceDocNo = this.GetQtyBalanceDocNo();
            entity.OperID = empID;
            entity.OperName = empName;
            entity.OperDate = DateTime.Now;
            entity.Visible = true;
            entity.CreaterID = empID;
            entity.CreaterName = empName;
            entity.DataUpdateTime = DateTime.Now;
            this.Entity = entity;
            if (this.Add())
            {
                tempBaseResultDataValue = AddReaBmsQtyMonthBalanceDtl(preBalanceDoc, entity, tempQtyList);
            }
            else
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "新增库存结转的信息失败!";
            }

            return tempBaseResultDataValue;
        }
        public BaseResultDataValue AddReaBmsQtyMonthBalanceDtl(ReaBmsQtyBalanceDoc preBalanceDoc, ReaBmsQtyBalanceDoc doc, IList<ReaBmsQtyDtl> tempQtyList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            IList<ReaBmsQtyBalanceDtl> qtyBalanceDtlList = new List<ReaBmsQtyBalanceDtl>();
            //上次库存数集合
            IList<ReaBmsQtyBalanceDtl> perDtlBalanceList = new List<ReaBmsQtyBalanceDtl>();
            //变化库存数集合
            IList<ReaBmsQtyDtlOperation> perDtlOperationList = new List<ReaBmsQtyDtlOperation>();
            if (preBalanceDoc != null)
            {
                perDtlBalanceList = IBReaBmsQtyBalanceDtl.SearchListByHQL("reabmsqtybalancedtl.QtyBalanceDocID=" + preBalanceDoc.Id);

                string beginDate = preBalanceDoc.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string endDate = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                string hqlWhere = string.Format("reabmsqtydtloperation.DataAddTime>='{0}' and reabmsqtydtloperation.DataAddTime<='{1}'", beginDate, endDate);
                perDtlOperationList = IDReaBmsQtyDtlOperationDao.GetListByHQL(hqlWhere);
            }

            foreach (var qty in tempQtyList)
            {
                ReaBmsQtyBalanceDtl qtyBalanceDtl = new ReaBmsQtyBalanceDtl();
                qtyBalanceDtl.QtyBalanceDocID = doc.Id;
                qtyBalanceDtl.Visible = true;
                qtyBalanceDtl.QtyBalanceDocID = doc.Id;
                qtyBalanceDtl.CreaterID = doc.CreaterID;
                qtyBalanceDtl.CreaterName = doc.CreaterName;
                qtyBalanceDtl.DataUpdateTime = DateTime.Now;

                qtyBalanceDtl.QtyDtlID = qty.Id;
                qtyBalanceDtl.PQtyDtlID = qty.PQtyDtlID;
                qtyBalanceDtl.ReaCompanyID = qty.ReaCompanyID;
                qtyBalanceDtl.CompanyName = qty.CompanyName;
                qtyBalanceDtl.GoodsID = qty.GoodsID;

                qtyBalanceDtl.GoodsName = qty.GoodsName;
                //qtyBalanceDtl.OrgID = qty.OrgID;
                qtyBalanceDtl.GoodsLotID = qty.GoodsLotID;
                qtyBalanceDtl.LotNo = qty.LotNo;
                qtyBalanceDtl.StorageID = qty.StorageID;
                qtyBalanceDtl.PlaceID = qty.PlaceID;

                qtyBalanceDtl.InDtlID = qty.InDtlID;
                qtyBalanceDtl.InDocNo = qty.InDocNo;
                qtyBalanceDtl.StorageName = qty.StorageName;
                qtyBalanceDtl.PlaceName = qty.PlaceName;

                qtyBalanceDtl.PlaceName = qty.PlaceName;
                qtyBalanceDtl.GoodsUnitID = qty.GoodsUnitID;
                qtyBalanceDtl.GoodsUnit = qty.GoodsUnit;
                qtyBalanceDtl.UnitMemo = qty.UnitMemo;
                qtyBalanceDtl.GoodsQty = qty.GoodsQty;

                qtyBalanceDtl.Price = qty.Price;
                qtyBalanceDtl.SumTotal = qty.SumTotal;
                qtyBalanceDtl.TaxRate = qty.TaxRate;
                qtyBalanceDtl.OutFlag = qty.OutFlag;
                qtyBalanceDtl.SumFlag = qty.SumFlag;

                qtyBalanceDtl.IOFlag = qty.IOFlag;
                qtyBalanceDtl.GoodsSerial = qty.GoodsSerial;
                qtyBalanceDtl.LotSerial = qty.LotSerial;
                qtyBalanceDtl.LotQRCode = qty.LotQRCode;
                qtyBalanceDtl.SysLotSerial = qty.SysLotSerial;
                qtyBalanceDtl.ZX1 = qty.ZX1;

                qtyBalanceDtl.ZX2 = qty.ZX2;
                qtyBalanceDtl.ZX3 = qty.ZX3;
                qtyBalanceDtl.Memo = qty.Memo;
                qtyBalanceDtl.GoodsNo = qty.GoodsNo;
                qtyBalanceDtl.CompGoodsLinkID = qty.CompGoodsLinkID;

                qtyBalanceDtl.ProdDate = qty.ProdDate;
                qtyBalanceDtl.InvalidDate = qty.InvalidDate;
                qtyBalanceDtl.InvalidWarningDate = qty.InvalidWarningDate;
                qtyBalanceDtl.ReaServerCompCode = qty.ReaServerCompCode;
                qtyBalanceDtl.RegisterNo = qty.RegisterNo;

                qtyBalanceDtl.BarCodeType = qty.BarCodeType;
                qtyBalanceDtl.ReaGoodsNo = qty.ReaGoodsNo;
                qtyBalanceDtl.ProdGoodsNo = qty.ProdGoodsNo;
                qtyBalanceDtl.CenOrgGoodsNo = qty.CenOrgGoodsNo;
                qtyBalanceDtl.GoodsNo = qty.GoodsNo;

                qtyBalanceDtl.ReaCompCode = qty.ReaCompCode;
                qtyBalanceDtl.GoodsSort = qty.GoodsSort;
                qtyBalanceDtl.CSQtyDtlNo = qty.CSQtyDtlNo;
                qtyBalanceDtl.CSInDtlNo = qty.CSInDtlNo;
                qtyBalanceDtl.InDocNo = qty.InDocNo;

                qtyBalanceDtl.IsNeedPerformanceTest = qty.IsNeedPerformanceTest;
                qtyBalanceDtl.VerificationStatus = qty.VerificationStatus;

                //上次库存数(相同库房及相同货架及相同供货商和相同货品批号的相同货品ID的上次库存数)
                if (perDtlBalanceList != null && perDtlBalanceList.Count > 0)
                {
                    var tempList1 = perDtlBalanceList.Where(p => p.StorageID == qty.StorageID && p.PlaceID == qty.PlaceID && p.ReaCompanyID == qty.ReaCompanyID && p.LotNo == qty.LotNo && p.GoodsID == qty.GoodsID);
                    if (tempList1 != null && tempList1.Count() > 0)
                    {
                        qtyBalanceDtl.PreGoodsQty = tempList1.Sum(p => p.GoodsQty);
                        qtyBalanceDtl.PreSumTotal = tempList1.Sum(p => p.SumTotal);
                    }
                }
                //变化库存数:包括所有种类的入库,出库,也包括移库入库及移库出库
                if (perDtlOperationList != null && perDtlOperationList.Count > 0)
                {
                    //是否需要考虑移库时按大包装移?()
                    qtyBalanceDtl.ChangeGoodsQty = perDtlOperationList.Where(p => p.StorageID == qty.StorageID && p.PlaceID == qty.PlaceID && p.ReaCompanyID == qty.ReaCompanyID && p.LotNo == qty.LotNo && p.GoodsID == qty.GoodsID).Sum(p => p.GoodsQty);
                }
                if (!qtyBalanceDtl.PreGoodsQty.HasValue) qtyBalanceDtl.PreGoodsQty = 0;
                if (!qtyBalanceDtl.ChangeGoodsQty.HasValue) qtyBalanceDtl.ChangeGoodsQty = 0;

                //计算库存数:计算库存数=上次库存数+变化库存数
                qtyBalanceDtl.CalcGoodsQty = qtyBalanceDtl.PreGoodsQty +qtyBalanceDtl.ChangeGoodsQty;

                qtyBalanceDtlList.Add(qtyBalanceDtl);
            }
            if (qtyBalanceDtlList.Count > 0)
                tempBaseResultDataValue = IBReaBmsQtyBalanceDtl.AddReaBmsQtyBalanceDtl(qtyBalanceDtlList, doc.CreaterID, doc.CreaterName);
            return tempBaseResultDataValue;
        }
        private BaseResultDataValue EditReaBmsQtyBalanceDocInvalid(string beginDate, string endDate, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //如果选择日期范围其中一个值为空,日期范围就按当前月第一天+当前日期进行处理
            if (string.IsNullOrEmpty(beginDate) || string.IsNullOrEmpty(endDate))
            {
                //本月第一天
                beginDate = DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToString("yyyy-MM-dd") + " 00:00:00";
                endDate = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            string hqlWhere = string.Format("reabmsqtybalancedoc.Visible=1 and reabmsqtybalancedoc.DataAddTime>='{0}' and reabmsqtybalancedoc.DataAddTime<='{1}'", beginDate, endDate);
            IList<ReaBmsQtyBalanceDoc> tempList = this.SearchListByHQL(hqlWhere);
            foreach (var entity in tempList)
            {
                entity.Visible = false;
                entity.Memo = entity.Memo + " 作废于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                entity.OperID = empID;
                entity.OperName = empName;
                entity.OperDate = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                this.Entity = entity;
                tempBaseResultDataValue.success = this.Edit();
                if (tempBaseResultDataValue.success == false)
                {
                    tempBaseResultDataValue.ErrorInfo = "新增库存结转单时,作废原库存结转单失败,库存结转单Id为:" + entity.Id;
                    break;
                }
            }
            return tempBaseResultDataValue;
        }
        #endregion
        public BaseResultBool GetJudgeISAddReaBmsQtyBalanceDoc(string beginDate, string endDate)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.BoolFlag = false;
            //如果选择日期范围其中一个值为空,日期范围就按当前月第一天+当前日期进行处理
            if (string.IsNullOrEmpty(beginDate) || string.IsNullOrEmpty(endDate))
            {
                //本月第一天
                beginDate = DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToString("yyyy-MM-dd") + " 00:00:00";
                endDate = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            string hqlWhere = string.Format("reabmsqtybalancedoc.Visible=1 and reabmsqtybalancedoc.DataAddTime>='{0}' and reabmsqtybalancedoc.DataAddTime<='{1}'", beginDate, endDate);
            IList<ReaBmsQtyBalanceDoc> tempList = this.SearchListByHQL(hqlWhere);
            if (tempList.Count > 0)
            {
                tempBaseResultBool.BoolFlag = true;
                tempBaseResultBool.ErrorInfo = "当前日期范围内已经进行过库存结转,请不要重复操作!库存结转的选择日期范围不能重复或交叉选择";
            }
            return tempBaseResultBool;
        }
        public BaseResultBool UpdateVisibleReaBmsQtyBalanceDocById(long id, bool visible, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            ReaBmsQtyBalanceDoc entity = this.Get(id);

            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "库存结转单ID为:" + id + ",不存在数据库中!";
                return tempBaseResultBool;
            }

            if (entity.Visible == visible)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("库存结转单ID为:" + id + ",的启用状态为:{0}!", (visible == true ? "启用" : "禁用"));
                return tempBaseResultBool;
            }

            entity.OperID = empID;
            entity.OperName = empName;
            entity.OperDate = DateTime.Now;
            entity.Visible = visible;
            this.Entity = entity;

            IList<string> tmpa = new List<string>();
            tmpa.Add("Id=" + entity.Id);
            tmpa.Add("Visible=" + (visible == true ? 1 : 0));
            tmpa.Add("OperID=" + empID + " ");
            tmpa.Add("OperName='" + empName + "'");
            if (!entity.OperDate.HasValue) tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

            string[] tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                tempBaseResultBool.ErrorInfo = "更新库存结转单成功!";
            }
            else
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "更新库存结转单失败!";
            }
            return tempBaseResultBool;
        }
        private string GetQtyBalanceDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
    }
}