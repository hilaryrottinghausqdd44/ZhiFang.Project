using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.Common.Public;
using ZhiFang.Entity.RBAC;
using ZhiFang.ReagentSys.Client.Common;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ZhiFang.BLL.ReagentSys.Client
{
    public class BReaBmsOutDoc : BaseBLL<ReaBmsOutDoc>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsOutDoc
    {
        IBReaBmsOutDtl IBReaBmsOutDtl { get; set; }
        IBReaBmsQtyDtl IBReaBmsQtyDtl { get; set; }
        IBReaBmsQtyDtlOperation IBReaBmsQtyDtlOperation { get; set; }
        IDReaBmsCheckDocDao IDReaBmsCheckDocDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IBReaUserStorageLink IBReaUserStorageLink { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        IBLL.RBAC.IBHRDept IBHRDept { get; set; }
        IBSCOperation IBSCOperation { get; set; }
        IDReaGoodsLotDao IDReaGoodsLotDao { get; set; }
<<<<<<< .mine
        IBReaOpenBottleOperDoc IBReaOpenBottleOperDoc { get; set; }
||||||| .r2673
=======
        IDReaCenOrgDao IDReaCenOrgDao { get; set; }
>>>>>>> .r2783
        public override bool Add()
        {
            this.Entity.DataAddTime = DateTime.Now;
            this.Entity.DataUpdateTime = DateTime.Now;
            this.Entity.OperDate = DateTime.Now;
            bool a = DBDao.Save(this.Entity);
            return a;
        }
        public EntityList<ReaBmsOutDoc> SearchListByHQL(string where, string order, int page, int count, string isUseEmpOut, string type, long empId)
        {
            EntityList<ReaBmsOutDoc> entityList = new EntityList<ReaBmsOutDoc>();
            entityList.list = new List<ReaBmsOutDoc>();
            //是否按出库人权限出库 1:是;2:否;
            if (!string.IsNullOrEmpty(isUseEmpOut) && isUseEmpOut == "1")
            {
                StringBuilder idStr = new StringBuilder();
                string outHql = "";
                if (type == "1")
                {
                    //出库申请,只能查看登录人=申请人的出库信息
                    outHql = " reabmsoutdoc.CreaterID=" + empId;
                }
                else
                {
                    IList<long> storageIDList = IBReaUserStorageLink.SearchStorageIDListByHQL(" reauserstoragelink.OperType=" + ReaUserStorageLinkOperType.库房管理权限.Key + " and reauserstoragelink.OperID=" + empId, "", -1, -1);
                    if (storageIDList == null || storageIDList.Count <= 0)
                    {
                        ZhiFang.Common.Log.Log.Error("按出库人权限出库:获取当前员工ID为:" + empId + ",获取到的库房人员权限信息为空!不能获取出库信息");
                        return entityList;
                    }
                    foreach (var storageID in storageIDList)
                    {
                        idStr.Append(storageID);
                        idStr.Append(",");
                    }
                    if (idStr.Length > 0)
                    {
                        switch (type)
                        {
                            case "1"://出库申请
                                break;
                            case "2"://直接出库管理
                                outHql = " reabmsoutdoc.StorageID in (" + idStr.ToString().TrimEnd(',') + ") ";
                                break;
                            case "3"://出库管理(申请)
                                outHql = " reabmsoutdoc.StorageID in (" + idStr.ToString().TrimEnd(',') + ") ";
                                break;
                            case "4"://出库管理(全部)
                                outHql = " reabmsoutdoc.StorageID in (" + idStr.ToString().TrimEnd(',') + ") ";
                                break;
                            default:
                                break;
                        }
                    }
                }
                //兼容在2018-11-21号之前,出库总单没有库房数据项的出库数据处理
                if (!string.IsNullOrEmpty(where))
                {
                    outHql = " (" + outHql + " or (reabmsoutdoc.StorageID is null or reabmsoutdoc.StorageID=''))";
                }
                else
                {
                    outHql = " (reabmsoutdoc.StorageID is null or reabmsoutdoc.StorageID='')";
                }
                if (!string.IsNullOrEmpty(outHql))
                {
                    if (!string.IsNullOrEmpty(where))
                    {
                        where = where + " and " + outHql;
                    }
                    else
                    {
                        where = outHql;
                    }
                }

            }
            entityList = this.SearchListByHQL(where, order, page, count);
            return entityList;
        }

        #region 直接出库完成处理
        /// <summary>
        /// 直接新增出库并修改库存(带库存条码扫码操作记录处理)
        /// </summary>
        /// <param name="outDoc">出库总单</param>
        /// <param name="dtlAddList">出库明细单</param>
        /// <param name="isEmpOut">是否按出库人权限出库</param>
        /// <param name="isNeedPerformanceTest">库存货品是否需要性能验证后才能使用出库</param>
        /// <returns></returns>
        public BaseResultDataValue AddOutDocAndOutDtlListOfComp(ref ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> dtlAddList, bool isEmpOut, bool isNeedPerformanceTest, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (outDoc == null || dtlAddList == null || dtlAddList.Count == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "出库主单或出库单明细信息不能为空！";
                return brdv;
            }
            //出库货品的货品批号是否需要性能验证后才能使用出库处理
            if (isNeedPerformanceTest == true && outDoc.OutType == long.Parse(ReaBmsOutDocOutType.使用出库.Key))
                brdv = AddValidNeedPerformanceTest(dtlAddList, empID, empName);
            //出库权限
            brdv = AddValidDocAndDtl(dtlAddList, isEmpOut, empID, empName);

            if (!brdv.success)
                return brdv;
            var sumTotal = dtlAddList.Sum(p => p.SumTotal);
            if (outDoc.TotalPrice <= 0 || outDoc.TotalPrice < sumTotal)
                outDoc.TotalPrice = sumTotal;
            if (!outDoc.ConfirmTime.HasValue)
                outDoc.ConfirmTime = DateTime.Now;
            if (!outDoc.ConfirmId.HasValue)
                outDoc.ConfirmId = empID;
            if (string.IsNullOrEmpty(outDoc.ConfirmName))
                outDoc.ConfirmName = empName;
            AddReaBmsOutDoc(ref outDoc, empID, empName, ref brdv);
            if (!brdv.success)
                return brdv;

            foreach (var item in dtlAddList)
            {
                if (brdv.success == false)
                    break;
                var outDtl = item;
                EditReaBmsOutDtl(outDoc, ref outDtl, item.ReaBmsOutDtlLinkList, empID, empName, ref brdv);
                if (brdv.success)
                {
                    outDtl.SumTotal = outDtl.GoodsQty * outDtl.Price;
                    outDtl.OutDocID = outDoc.Id;
                    IBReaBmsOutDtl.Entity = outDtl;
                    ZhiFang.Common.Log.Log.Debug("AddOutDocAndOutDtlListOfComp.outDtl.Id:" + outDtl.Id + ",outDoc.Id:" + outDoc.Id + ",outDtl.GoodsQty:" + outDtl.GoodsQty + ",outDtl.Price:" + outDtl.Price + ",outDtl.SumTotal:" + outDtl.SumTotal);
                    if (!IBReaBmsOutDtl.Add())
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "货品出库失败：出库明细单保存失败！";
                        throw new Exception(brdv.ErrorInfo);
                    }
                }
            }
            ZhiFang.Common.Log.Log.Debug("outDoc.IsNeedBOpen:" + outDoc.IsNeedBOpen);

            if (brdv.success) AddSCOperation(this.Entity, empID, empName);
            if (!brdv.success)
                throw new Exception(brdv.ErrorInfo);
            return brdv;
        }
        /// <summary>
        /// 新增出库主单
        /// </summary>
        /// <param name="outDoc"></param>
        /// <param name="brdv"></param>
        private void AddReaBmsOutDoc(ref ReaBmsOutDoc outDoc, long empID, string empName, ref BaseResultDataValue brdv)
        {
            if (outDoc.OutType <= 0)
                outDoc.OutType = int.Parse(ReaBmsOutDocOutType.使用出库.Key);
            outDoc.OutTypeName = ReaBmsOutDocOutType.GetStatusDic()[outDoc.OutType.ToString()].Name;

            if (string.IsNullOrEmpty(outDoc.OutDocNo))
                outDoc.OutDocNo = GetOutDocNo();
            outDoc.Visible = true;
            outDoc.OperDate = DateTime.Now;
            outDoc.DataAddTime = DateTime.Now;
            outDoc.DataUpdateTime = DateTime.Now;
            if (!outDoc.OutBoundID.HasValue)
            {
                outDoc.OutBoundID = empID;
                outDoc.OutBoundName = empName;
            }
            if (!outDoc.CreaterID.HasValue)
            {
                outDoc.CreaterID = empID;
                outDoc.CreaterName = empName;
            }
            if (outDoc.Status <= 0)
                outDoc.Status = int.Parse(ReaBmsOutDocStatus.出库完成.Key);
            outDoc.StatusName = ReaBmsOutDocStatus.GetStatusDic()[outDoc.Status.ToString()].Name;

            if (outDoc.DeptID <= 0)
            {
                outDoc.DeptID = string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.HRDeptID)) ? 0 : long.Parse(SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));
                if (string.IsNullOrEmpty(outDoc.DeptName))
                    outDoc.DeptName = SessionHelper.GetSessionValue(DicCookieSession.HRDeptName);
            }
            this.Entity = outDoc;
            brdv.success = this.Add();
            if (!brdv.success)
            {
                brdv.success = false;
                brdv.ErrorInfo = "新增出库单保存失败！";
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
            }
        }
        /// <summary>
        /// 当前出库人是否有对某库房及货架的出库权限
        /// </summary>
        /// <param name="userStorageLinkList">当前出库人的出库权限集合</param>
        /// <param name="outDtl">某一出库明细</param>
        /// <param name="dicRight">库房ID+货架ID临时集合</param>
        /// <param name="brdv"></param>
        private static void AddOutDtlIsEmpOut(IList<ReaUserStorageLink> userStorageLinkList, ReaBmsOutDtl outDtl, string empName, ref Dictionary<string, bool> dicRight, ref BaseResultDataValue brdv)
        {
            if (!dicRight.Keys.Contains(outDtl.StorageID.ToString()))
            {
                IList<ReaUserStorageLink> listReaUserStorage = userStorageLinkList.Where(p => p.StorageID == outDtl.StorageID && p.OperType == long.Parse(ReaUserStorageLinkOperType.库房管理权限.Key)).ToList();
                dicRight.Add(outDtl.StorageID.ToString(), listReaUserStorage != null && listReaUserStorage.Count > 0);
            }
            if (dicRight.Keys.Contains(outDtl.StorageID.ToString()))
            {
                if (!(dicRight[outDtl.StorageID.ToString()]))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("货品出库失败：出库人【{0}】没有库房【{1}】出库权限！", empName, outDtl.StorageName);
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    //throw new Exception(brdv.ErrorInfo);
                    return;
                }
            }

        }
        private string GetQytHql(ReaBmsOutDtl outDtl)
        {
            StringBuilder qtyHql = new StringBuilder();
            qtyHql.Append(" reabmsqtydtl.Visible=1 and reabmsqtydtl.GoodsQty>0");
            qtyHql.Append(" and reabmsqtydtl.BarCodeType=" + outDtl.BarCodeType);
            if (outDtl.StorageID > 0)
            {
                qtyHql.Append(" and reabmsqtydtl.StorageID=" + outDtl.StorageID);
            }
            if (outDtl.PlaceID.HasValue)
            {
                qtyHql.Append(" and reabmsqtydtl.PlaceID=" + outDtl.PlaceID.Value);
            }
            if (!string.IsNullOrEmpty(outDtl.ReaGoodsNo))
            {
                qtyHql.Append(" and reabmsqtydtl.ReaGoodsNo='" + outDtl.ReaGoodsNo + "'");
            }
            if (outDtl.ReaCompanyID.HasValue)
            {
                qtyHql.Append(" and reabmsqtydtl.ReaCompanyID=" + outDtl.ReaCompanyID.Value);
            }
            if (!string.IsNullOrEmpty(outDtl.LotNo))
            {
                qtyHql.Append(" and reabmsqtydtl.LotNo='" + outDtl.LotNo + "'");
            }
            return qtyHql.ToString();
        }
        /// <summary>
        /// 保存出库明细及出库扫码记录信息
        /// </summary>
        /// <param name="outDoc">出库主单信息</param>
        /// <param name="outDtl">出库明细</param>
        /// <param name="outBarcodeList">出库明细当次的条码扫码记录</param>
        /// <param name="brdv"></param>
        private void EditReaBmsOutDtl(ReaBmsOutDoc outDoc, ref ReaBmsOutDtl outDtl, IList<ReaGoodsBarcodeOperation> outBarcodeList,long empID, string empName, ref BaseResultDataValue brdv)
        {
            outDtl.Visible = true;
            outDtl.DataUpdateTime = DateTime.Now;
            //if (outDtl.SumTotal <= 0)
            outDtl.SumTotal = outDtl.GoodsQty * outDtl.Price;
            if (outDoc.CreaterID.HasValue)
            {
                outDtl.CreaterID = outDoc.CreaterID.Value;
                outDtl.CreaterName = outDoc.CreaterName;
            }
            ReaGoods outReaGoods = IDReaGoodsDao.Get(outDtl.GoodsID.Value);
            outDtl.IsNeedBOpen = outReaGoods.IsNeedBOpen;
            if (outDtl.IsNeedBOpen == true)
            {
                outDoc.IsNeedBOpen = outDtl.IsNeedBOpen;
                IBReaOpenBottleOperDoc.AddReaOpenBottleOperDoc(outDtl, outDtl.Id, empID, empName);
            }

            if (string.IsNullOrEmpty(outDtl.ProdGoodsNo))
                outDtl.ProdGoodsNo = outReaGoods.ProdGoodsNo;
            if (string.IsNullOrEmpty(outDtl.ReaGoodsNo))
                outDtl.ReaGoodsNo = outReaGoods.ReaGoodsNo;
            if (string.IsNullOrEmpty(outDtl.GoodsNo))
                outDtl.GoodsNo = outReaGoods.GoodsNo;
            //出库明细出库总数(转换为最小包装单位)
            double outDtlGoodsQty = outDtl.GoodsQty;
            double goodsPrice = outDtl.Price;
            ZhiFang.Common.Log.Log.Info(string.Format("出库数量开始单位换算：原数量{0}，原价格{1}", outDtlGoodsQty, goodsPrice));
            BaseResultDataValue tempResult = GetReaGoodsMinUnitCount(outDtl.GoodsID, outDtl.ReaCompanyID, ref outDtlGoodsQty, ref goodsPrice);
            ZhiFang.Common.Log.Log.Info(string.Format("出库数量结束单位换算：转换后数量{0}，转换后价格{1}", outDtlGoodsQty, goodsPrice));
            if (!tempResult.success)
            {
                brdv = tempResult;
                return;
            }
            outDtl.OutDocID = outDoc.Id;
            if (outDtl.GoodsQty <= 0)
            {
                brdv.success = true;
                ZhiFang.Common.Log.Log.Info(string.Format("出库明细ID:{0},出库货品名称:{1},实际出库数:{2}", outDtl.Id, outDtl.GoodsCName, outDtl.GoodsQty));
                return;
            }
            //获取出库明细选择出库货品当前库存货品记录信息
            IList<ReaBmsQtyDtl> listReaBmsQtyDtl = IBReaBmsQtyDtl.SearchListByHQL(GetQytHql(outDtl)).OrderBy(p => p.DataAddTime).ToList();
            #region 获取出库明细选择对应的库存货品记录的总库存数
            var groupByQtyList = listReaBmsQtyDtl.GroupBy(p => p.GoodsID);
            double allOverageQty = 0;
            double qtygoodsPrice = 0;
            if (groupByQtyList != null && groupByQtyList.Count() > 0)
            {
                foreach (var groupBy in groupByQtyList)
                {
                    double allOverageQty2 = groupBy.Where(p => p.GoodsQty.HasValue == true).Sum(g => g.GoodsQty.Value);
                    ZhiFang.Common.Log.Log.Info(string.Format("QtyDtl.GoodsID:" + groupBy.Key + ",出库的库存货品总库存数开始单位换算：原数量{0}，原价格{1}", allOverageQty2, qtygoodsPrice));
                    tempResult = GetReaGoodsMinUnitCount(groupBy.Key, outDtl.ReaCompanyID, ref allOverageQty2, ref qtygoodsPrice);
                    ZhiFang.Common.Log.Log.Info(string.Format("QtyDtl.GoodsID:" + groupBy.Key + ",出库的库存货品总库存数结束单位换算：转换后数量{0}，转换后价格{1}", allOverageQty2, qtygoodsPrice));
                    if (!tempResult.success)
                    {
                        brdv = tempResult;
                        return;
                    }
                    allOverageQty = allOverageQty + allOverageQty2;
                }
            }
            //剩余库存数保留两位小数点
            allOverageQty = ConvertQtyHelp.ConvertQty(allOverageQty, 2);
            if (outDtl.IsAllOut == true)
            {
                string info = string.Format("货品【{0}】完全出库,出库数为:{1},剩余库存数为:{2}", outDtl.GoodsCName, outDtlGoodsQty, allOverageQty);
                ZhiFang.Common.Log.Log.Warn(info);
                outDtlGoodsQty = allOverageQty;
            }
            //出库数量大于库存数量
            if (outDtlGoodsQty > allOverageQty)
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("货品【{0}】出库失败：库存数量不足！", outDtl.GoodsCName);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                return;
            }
            #endregion

            #region 出库明细货品的扫码库存货品优先出库
            //当次的扫码条码集合优先出库
            if (outBarcodeList != null && outBarcodeList.Count > 0)
                EditReaBmsOutDtlOfoOutBarcode(outDoc, listReaBmsQtyDtl, outReaGoods, ref outDtl, ref outBarcodeList, ref outDtlGoodsQty, ref brdv);
            #endregion

            #region 出库剩余出库数的库存数处理
            if (outDtlGoodsQty > 0)
            {
                ZhiFang.Common.Log.Log.Info(string.Format("出库明细ID：{0}，非货品扫码出库.出库剩余出库数的库存数处理开始", outDtl.Id));
                for (int i = 0; i < listReaBmsQtyDtl.Count; i++)
                {
                    if (outDtlGoodsQty <= 0)
                    {
                        break;
                    }
                    if (listReaBmsQtyDtl[i].GoodsQty <= 0) continue;

                    ReaBmsQtyDtl sQtyDtl = listReaBmsQtyDtl[i];
                    //库存货品的当前剩余库存数
                    double qtyCurOverage = (double)(sQtyDtl.GoodsQty != null ? sQtyDtl.GoodsQty : 0);
                    //当前出库的库存货品库存数(转换为小包装单位)
                    double qtygoodsPrice2 = (double)(sQtyDtl.Price != null ? sQtyDtl.Price : 0);
                    ZhiFang.Common.Log.Log.Info(string.Format("出库的库存货品开始单位换算：原数量{0}，原价格{1}", qtyCurOverage, qtygoodsPrice2));
                    tempResult = GetReaGoodsMinUnitCount(listReaBmsQtyDtl[i].GoodsID, sQtyDtl.ReaCompanyID, ref qtyCurOverage, ref qtygoodsPrice2);
                    ZhiFang.Common.Log.Log.Info(string.Format("出库的库存货品结束单位换算：转换后数量{0}，转换后价格{1}", qtyCurOverage, qtygoodsPrice2));
                    if (!tempResult.success)
                    {
                        brdv = tempResult;
                        return;
                    }

                    //剩余库存数保留两位小数点
                    qtyCurOverage = ConvertQtyHelp.ConvertQty(qtyCurOverage, 2);
                    double curOutDtlQty = outDtlGoodsQty;
                    if (curOutDtlQty >= qtyCurOverage) curOutDtlQty = qtyCurOverage;
                    EditQtyDtlOfOutDtl(outDoc, ref outDtl, ref sQtyDtl, qtyCurOverage, curOutDtlQty, ref outDtlGoodsQty, ref brdv);
                    //更新sQtyDtl在listReaBmsQtyDtl的库存数
                    listReaBmsQtyDtl[i].GoodsQty = sQtyDtl.GoodsQty;
                    //如果出库对应库存记录的库存数等于0,需要将库存记录对应的货品条码的剩余扫码数也设置为0
                    if (sQtyDtl.GoodsQty <= 0)
                        AddOutBarcodeOfOutOtherQty(outDoc, sQtyDtl, outReaGoods, ref outDtl, ref outDtlGoodsQty, ref brdv);
                    if (brdv.success == false)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = string.Format("货品【{0}】货品出库失败：处理出库的库存信息失败！", outDtl.GoodsCName);
                        ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                        throw new Exception(brdv.ErrorInfo);
                        //return;
                    }
                }
            }
            else
            {
                listReaBmsQtyDtl.Clear();
            }
            #endregion

            if (!string.IsNullOrEmpty(outDtl.QtyDtlID))
                outDtl.QtyDtlID = outDtl.QtyDtlID.TrimEnd(',');
            if (!outDtl.ReqGoodsQty.HasValue || outDtl.ReqGoodsQty.Value <= 0)
                outDtl.ReqGoodsQty = outDtl.GoodsQty;
        }
        private void AddOutBarcodeOfOutOtherQty(ReaBmsOutDoc outDoc, ReaBmsQtyDtl sQtyDtl, ReaGoods outReaGoods, ref ReaBmsOutDtl outDtl, ref double outDtlGoodsQty, ref BaseResultDataValue brdv)
        {
            string allBarCodeHql = string.Format(" (reagoodsbarcodeoperation.OverageQty>0 or reagoodsbarcodeoperation.OverageQty is null) and reagoodsbarcodeoperation.QtyDtlID={0}", sQtyDtl.Id);
            if (sQtyDtl.StorageID.HasValue)
                allBarCodeHql = allBarCodeHql + " and reagoodsbarcodeoperation.StorageID=" + sQtyDtl.StorageID.Value;
            if (sQtyDtl.PlaceID.HasValue)
                allBarCodeHql = allBarCodeHql + " and reagoodsbarcodeoperation.PlaceID=" + sQtyDtl.PlaceID.Value;
            IList<ReaGoodsBarcodeOperation> barcodeAllList = IBReaGoodsBarcodeOperation.GetListByHQL(allBarCodeHql);
            foreach (var barcode in barcodeAllList)
            {
                ReaGoodsBarcodeOperation addOperation = ClassMapperHelp.GetMapper<ReaGoodsBarcodeOperation, ReaGoodsBarcodeOperation>(barcode);
                long operTypeID = long.Parse(ReaGoodsBarcodeOperType.使用出库.Key);
                if (outDoc.OutType.ToString() == ReaBmsOutDocOutType.报损出库.Key)
                {
                    operTypeID = long.Parse(ReaGoodsBarcodeOperType.报损出库.Key);
                }
                else if (outDoc.OutType.ToString() == ReaBmsOutDocOutType.退供应商.Key)
                {
                    operTypeID = long.Parse(ReaGoodsBarcodeOperType.退供应商.Key);
                }
                else if (outDoc.OutType.ToString() == ReaBmsOutDocOutType.退库入库.Key)
                {
                    operTypeID = long.Parse(ReaGoodsBarcodeOperType.退库入库.Key);
                }
                else if (outDoc.OutType.ToString() == ReaBmsOutDocOutType.盘亏出库.Key)
                {
                    operTypeID = long.Parse(ReaGoodsBarcodeOperType.盘亏出库.Key);
                }
                addOperation.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                addOperation.LabID = outDtl.LabID;
                addOperation.BDocID = outDtl.OutDocID;
                addOperation.BDocNo = outDoc.OutDocNo;
                addOperation.BDtlID = outDtl.Id;
                addOperation.QtyDtlID = sQtyDtl.Id;
                addOperation.OperTypeID = operTypeID;
                addOperation.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
                addOperation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[addOperation.OperTypeID.ToString()].Name;
                addOperation.ScanCodeGoodsUnit = outDtl.GoodsUnit;
                addOperation.ScanCodeGoodsID = outDtl.GoodsID;
                addOperation.Memo = "非直接出库扫码;库存货品库存数为0,对应的货品条码全部标志为出库!";
                if (!addOperation.MinBarCodeQty.HasValue)
                {
                    ReaGoods reaGoods = IDReaGoodsDao.Get(outDtl.GoodsID.Value);
                    double gonvertQty = 1;
                    if (reaGoods != null) gonvertQty = reaGoods.GonvertQty;
                    if (gonvertQty <= 0)
                    {
                        ZhiFang.Common.Log.Log.Warn("货品编码为:" + addOperation.ReaGoodsNo + ",货品名称为:" + addOperation.GoodsCName + ",货品包装单位的换算系数值为" + gonvertQty + ",维护不合理!");
                        gonvertQty = 1;
                    }
                    addOperation.MinBarCodeQty = gonvertQty;
                }
                if (addOperation.MinBarCodeQty <= 0) addOperation.MinBarCodeQty = 1;
                if (!addOperation.MinBarCodeQty.HasValue)
                    addOperation.MinBarCodeQty = 1;
                if (addOperation.MinBarCodeQty.Value == 1)
                {
                    addOperation.ScanCodeQty = 1;
                    addOperation.OverageQty = 0;
                }
                else if (addOperation.MinBarCodeQty.Value > 1)
                {
                    //获取剩余的扫码条码数(最小包装单位)
                    string barCodeHql = string.Format(" (reagoodsbarcodeoperation.UsePackSerial='{0}' or reagoodsbarcodeoperation.UsePackQRCode='{1}')", barcode.UsePackSerial, barcode.UsePackQRCode);
                    if (sQtyDtl.StorageID.HasValue)
                        barCodeHql = barCodeHql + " and reagoodsbarcodeoperation.StorageID=" + sQtyDtl.StorageID.Value;
                    if (sQtyDtl.PlaceID.HasValue)
                        barCodeHql = barCodeHql + " and reagoodsbarcodeoperation.PlaceID=" + sQtyDtl.PlaceID.Value;
                    IList<ReaGoodsBarcodeOperation> barcodeAllList2 = IBReaGoodsBarcodeOperation.SearchListByHQL(barCodeHql);
                    double overageQty = IBReaGoodsBarcodeOperation.SearchOverageQty(addOperation.UsePackQRCode, barcodeAllList2);
                    if (overageQty <= 0) overageQty = 0;
                    if (overageQty > addOperation.MinBarCodeQty.Value) overageQty = addOperation.MinBarCodeQty.Value;
                    addOperation.ScanCodeQty = overageQty;
                    addOperation.OverageQty = overageQty - addOperation.ScanCodeQty;
                }
                IBReaGoodsBarcodeOperation.AddBarcodeOperation(addOperation, 0, outDoc.CreaterID.Value, outDoc.CreaterName, outDtl.LabID);
            }
        }
        /// <summary>
        /// 当次出库的扫码条码集合优先出库
        /// </summary>
        /// <param name="outDoc"></param>
        /// <param name="listReaBmsQtyDtl">符合出库明细的库存货品</param>
        /// <param name="outReaGoods">出库明细的机构货品信息</param>
        /// <param name="outDtl">出库明细</param>
        /// <param name="outBarcodeList">出库扫码条码集合</param>
        /// <param name="outDtlGoodsQty">出库明细的出库总数(转换为最小包装单位)</param>
        /// <param name="brdv"></param>
        private void EditReaBmsOutDtlOfoOutBarcode(ReaBmsOutDoc outDoc, IList<ReaBmsQtyDtl> listReaBmsQtyDtl, ReaGoods outReaGoods, ref ReaBmsOutDtl outDtl, ref IList<ReaGoodsBarcodeOperation> outBarcodeList, ref double outDtlGoodsQty, ref BaseResultDataValue brdv)
        {
            //当前出库扫码条码对应的大包装单位条码集合(出库扫码的条码为小包装条码时)
            IList<ReaGoodsBarcodeOperation> addMaxBarCodeList = AddGetMaxBarCodeList(outDoc, outReaGoods, ref outDtl, ref outBarcodeList);
            #region foreach outBarcodeList
            double outGonvertQty = outReaGoods.GonvertQty;
            if (outGonvertQty <= 0)
                outGonvertQty = 1;
            ZhiFang.Common.Log.Log.Info(string.Format("出库明细ID:{0}，出库扫码的记录数为:{1}", outDtl.Id, outBarcodeList.Count));
            foreach (ReaGoodsBarcodeOperation outBarcode in outBarcodeList)
            {
                //当前出库扫码对应的大包装单位条码记录
                ReaGoodsBarcodeOperation pOutBarcode = null;
                //出库扫码货品的存储库房ID
                string storageId = "";
                if (outBarcode.StorageID.HasValue)
                    storageId = outBarcode.StorageID.Value.ToString();
                //出库扫码货品的存储货架ID
                string placeId = "";
                if (outBarcode.PlaceID.HasValue)
                    placeId = outBarcode.PlaceID.Value.ToString();

                //本次出库扫码次数
                double scanCodeQty = 0;
                if (outBarcode.ScanCodeQty.HasValue)
                    scanCodeQty = outBarcode.ScanCodeQty.Value;
                ZhiFang.Common.Log.Log.Info(string.Format("出库扫码的一维条码为:{0}，二维条码为:{1}，本次出库扫码次数为:{2}", outBarcode.UsePackSerial, outBarcode.UsePackQRCode, scanCodeQty));
                //出库扫码次数不能大于出库条码的最小包装单位扫码次数
                if (outBarcode.MinBarCodeQty.HasValue && outBarcode.MinBarCodeQty.Value > 0 && scanCodeQty > outBarcode.MinBarCodeQty.Value)
                {
                    ZhiFang.Common.Log.Log.Info(string.Format("出库扫码的一维条码为:{0}，二维条码为:{1}，最小包装单位扫码次数为:{2},本次出库扫码次数最大值只能为:{3}", outBarcode.UsePackSerial, outBarcode.UsePackQRCode, outBarcode.MinBarCodeQty.Value, outBarcode.MinBarCodeQty.Value));
                    scanCodeQty = outBarcode.MinBarCodeQty.Value;
                }
                //条码的当次扫码次数不能大于条码货品对应的转换系数
                if (scanCodeQty <= 0)
                    continue;

                //本次出库扫码的出库使用的库存数(最小包装单位)
                double scanCodeQty2 = scanCodeQty * outGonvertQty;
                IList<ReaBmsQtyDtl> sListReaBmsQtyDtl = null;
                ReaBmsQtyDtl sQtyDtl = null;
                long qtyDtlID = 0;
                if (outBarcode.QtyDtlID.HasValue)
                {
                    qtyDtlID = outBarcode.QtyDtlID.Value;
                    sListReaBmsQtyDtl = listReaBmsQtyDtl.Where(p => p.Id == qtyDtlID && p.GoodsQty > 0).ToList();
                }
                else
                {
                    sListReaBmsQtyDtl = listReaBmsQtyDtl.Where(p => p.GoodsQty > 0).ToList();
                }
                if (sListReaBmsQtyDtl != null && sListReaBmsQtyDtl.Count > 0)
                    sQtyDtl = sListReaBmsQtyDtl[0];
                if (sQtyDtl != null)
                {
                    if (!outDtl.GoodsLotID.HasValue)
                        outDtl.GoodsLotID = sQtyDtl.GoodsLotID;
                    if (!outBarcode.GoodsLotID.HasValue)
                        outBarcode.GoodsLotID = sQtyDtl.GoodsLotID;
                    int qtyIndexOf = listReaBmsQtyDtl.IndexOf(sQtyDtl);
                    //当前扫码条码剩余的总库存数
                    double qtyCurOverage = (double)(sQtyDtl.GoodsQty != null ? sQtyDtl.GoodsQty : 0);
                    //当前出库库存货品的库存数(转换为小包装单位)
                    double qtygoodsPrice2 = (double)(sQtyDtl.Price != null ? sQtyDtl.Price : 0);
                    ZhiFang.Common.Log.Log.Info(string.Format("出库的库存货品开始单位换算：原库存数{0}，原库存价格{1}", qtyCurOverage, qtygoodsPrice2));
                    brdv = GetReaGoodsMinUnitCount(sQtyDtl.GoodsID, sQtyDtl.ReaCompanyID, ref qtyCurOverage, ref qtygoodsPrice2);
                    ZhiFang.Common.Log.Log.Info(string.Format("出库的库存货品结束单位换算：转换后库存数{0}，转换后库存价格{1}", qtyCurOverage, qtygoodsPrice2));
                    //剩余库存数保留两位小数点
                    qtyCurOverage = ConvertQtyHelp.ConvertQty(qtyCurOverage, 2);

                    string allBarCodeHql = string.Format(" (reagoodsbarcodeoperation.OverageQty>0 or reagoodsbarcodeoperation.OverageQty is null) and (reagoodsbarcodeoperation.UsePackSerial='{0}' or reagoodsbarcodeoperation.UsePackQRCode='{1}')", outBarcode.UsePackSerial, outBarcode.UsePackSerial);
                    if (!string.IsNullOrEmpty(storageId))
                        allBarCodeHql = allBarCodeHql + " and reagoodsbarcodeoperation.StorageID=" + storageId;
                    if (!string.IsNullOrEmpty(placeId))
                        allBarCodeHql = allBarCodeHql + " and reagoodsbarcodeoperation.PlaceID=" + placeId;
                    IList<ReaGoodsBarcodeOperation> barcodeAllList = IBReaGoodsBarcodeOperation.GetListByHQL(allBarCodeHql);
                    //当前出库扫码剩余的库存数
                    double barcodeCurOverage = IBReaGoodsBarcodeOperation.SearchOverageQty(outBarcode.UsePackSerial, barcodeAllList);
                    ZhiFang.Common.Log.Log.Info(string.Format("出库扫码的一维条码为:{0}，二维条码为:{1}，当前出库扫码剩余的库存数:{2}", outBarcode.UsePackSerial, outBarcode.UsePackQRCode, barcodeCurOverage));
                    //剩余库存数保留两位小数点
                    barcodeCurOverage = ConvertQtyHelp.ConvertQty(barcodeCurOverage, 2);

                    if (barcodeCurOverage > qtyCurOverage)
                    {
                        barcodeCurOverage = qtyCurOverage;
                    }
                    if (barcodeCurOverage <= 0)
                    {
                        barcodeCurOverage = 0;
                        ZhiFang.Common.Log.Log.Info(string.Format("出库扫码的一维条码为:{0}，二维条码为:{1}，当前出库扫码剩余的库存数:{2},条码的存储库房为:{3},存储货架为:{4},当前出库库房为:{5},出库货架为:{6}", outBarcode.UsePackSerial, outBarcode.UsePackQRCode, barcodeCurOverage, outBarcode.StorageID, outBarcode.PlaceID, sQtyDtl.StorageID, sQtyDtl.PlaceID));
                        if (outGonvertQty == 1)
                        {
                            //出库条码的库房及货架更新为出库
                            outBarcode.StorageID = sQtyDtl.StorageID;
                            outBarcode.PlaceID = sQtyDtl.PlaceID;
                            outBarcode.OverageQty = 0;
                            outBarcode.Memo = string.Format("出库货品扫码操作,出库库房为:{0},货架为:{1},当前剩余库存数为0", outDtl.StorageName, outDtl.PlaceName) + outBarcode.Memo;
                            AddCurBarcodeOperation(outDoc, outDtl, sQtyDtl, outBarcode, pOutBarcode, outDoc.CreaterID.Value, outDoc.CreaterName);
                        }
                        continue;
                    }

                    if (scanCodeQty2 > barcodeCurOverage)
                    {
                        scanCodeQty2 = barcodeCurOverage;
                    }
                    //出库条码的库房及货架更新为出库
                    outBarcode.StorageID = sQtyDtl.StorageID;
                    outBarcode.PlaceID = sQtyDtl.PlaceID;
                    //条码剩余的扫码次数(最小包装单位)=当前出库扫码的库存货品剩余库存数-本次出库扫码的库存数
                    double overageQty1 = barcodeCurOverage - scanCodeQty2;
                    //出库明细的某一条码的当次出库数(最小包装单位)
                    double curOutDtlQty = scanCodeQty2;
                    //出库扫码的出库数只能小于或等于原库存的库存数
                    if (curOutDtlQty > barcodeCurOverage) curOutDtlQty = barcodeCurOverage;
                    //库存出库数只能小于等于某一出库明细剩下的出库总数
                    if (curOutDtlQty >= outDtlGoodsQty) curOutDtlQty = outDtlGoodsQty;
                    EditQtyDtlOfOutDtl(outDoc, ref outDtl, ref sQtyDtl, qtyCurOverage, curOutDtlQty, ref outDtlGoodsQty, ref brdv);
                    if (brdv.success == false)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = string.Format("货品【{0}】货品出库失败：处理出库扫码的库存信息失败！", outDtl.GoodsCName);
                        ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                        throw new Exception(brdv.ErrorInfo);
                        //return;
                    }
                    //更新sQtyDtl在listReaBmsQtyDtl的库存数
                    listReaBmsQtyDtl[qtyIndexOf].GoodsQty = sQtyDtl.GoodsQty;
                    outBarcode.OverageQty = overageQty1;
                    if (outBarcode.OverageQty < 0)
                        outBarcode.OverageQty = 0;
                    #region 当前出库扫码对应的大包装条码的剩余扫码数处理
                    if (!string.IsNullOrEmpty(outBarcode.PUsePackSerial) && addMaxBarCodeList.Count > 0)
                    {
                        for (int i = 0; i < addMaxBarCodeList.Count; i++)
                        {
                            if (addMaxBarCodeList[i].PUsePackSerial == outBarcode.PUsePackSerial)
                            {
                                ZhiFang.Common.Log.Log.Error("出库扫码条码(对应的大包装单位条码).ScanCodeQty:" + addMaxBarCodeList[i].ScanCodeQty + ",本次出库扫码次数:" + scanCodeQty);
                                addMaxBarCodeList[i].ScanCodeQty = addMaxBarCodeList[i].ScanCodeQty + scanCodeQty;
                                addMaxBarCodeList[i].OverageQty = addMaxBarCodeList[i].OverageQty - scanCodeQty2;
                                if (addMaxBarCodeList[i].OverageQty < 0)
                                    addMaxBarCodeList[i].OverageQty = 0;
                                pOutBarcode = addMaxBarCodeList[i];
                                //如果当前扫码条码为大包装条码,需要在addMaxBarCodeList里移除相应的条码信息
                                if (addMaxBarCodeList[i].UsePackSerial == outBarcode.UsePackSerial || addMaxBarCodeList[i].UsePackQRCode == outBarcode.UsePackQRCode)
                                {
                                    addMaxBarCodeList.RemoveAt(i);
                                    ZhiFang.Common.Log.Log.Error("当前出库扫码条码为大包装条码.UsePackSerial:" + outBarcode.UsePackSerial + ",UsePackQRCode:" + outBarcode.UsePackQRCode);
                                }
                                ZhiFang.Common.Log.Log.Error("出库扫码条码(对应的大包装单位条码).PUsePackSerial:" + outBarcode.PUsePackSerial + ",本次出库扫码次数:" + scanCodeQty2);
                            }
                        }
                    }
                    #endregion
                    //当前出库扫码的条码操作记录
                    BaseResultBool tempBaseResultBool = AddCurBarcodeOperation(outDoc, outDtl, sQtyDtl, outBarcode, pOutBarcode, outDoc.CreaterID.Value, outDoc.CreaterName);
                    if (tempBaseResultBool.success == false)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = string.Format("货品【{0}】货品出库失败：处理保存出库扫码信息失败！", outDtl.GoodsCName);
                        ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                        throw new Exception(brdv.ErrorInfo);
                        //return;
                    }
                }
            }
            #endregion

            if (addMaxBarCodeList.Count > 0)
            {
                try
                {
                    foreach (var maxBarCode in addMaxBarCodeList)
                    {
                        ZhiFang.Common.Log.Log.Error("出库扫码条码(对应的大包装单位条码).PUsePackSerial:" + maxBarCode.PUsePackSerial + ",OverageQty:" + maxBarCode.OverageQty);
                        maxBarCode.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                        maxBarCode.Memo = "非直接出库扫码;小包装货品条码出库扫码,更新对应大包装货品条码的可扫码次数!";
                        IBReaGoodsBarcodeOperation.Entity = maxBarCode;
                        IBReaGoodsBarcodeOperation.Add();
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("货品【{0}】货品出库失败：出库扫码条码(对应的大包装单位条码)操作记录登记错误:！", outDtl.GoodsCName);
                    ZhiFang.Common.Log.Log.Error("出库扫码条码(对应的大包装单位条码)操作记录登记错误:" + ex.StackTrace);
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 出库明细的库存处理
        /// </summary>
        /// <param name="outDoc"></param>
        /// <param name="outDtl"></param>
        /// <param name="sQtyDtl">当次出库对应的库存记录</param>
        /// <param name="qtyCurOverage">出库明细对应库存的剩余库存数</param>
        /// <param name="curOutDtlQty">当次出库数(最小包装单位)</param>
        /// <param name="outDtlGoodsQty">某一出库明细的出库总数(转换为最小包装单位)</param>
        /// <param name="brdv"></param>
        private void EditQtyDtlOfOutDtl(ReaBmsOutDoc outDoc, ref ReaBmsOutDtl outDtl, ref ReaBmsQtyDtl sQtyDtl, double qtyCurOverage, double curOutDtlQty, ref double outDtlGoodsQty, ref BaseResultDataValue brdv)
        {
            if (outDtlGoodsQty <= 0) return;
            double qtyGonvertQty = 1;
            double qtyOverage1 = 0;
            if (!outDtl.GoodsLotID.HasValue)
                outDtl.GoodsLotID = sQtyDtl.GoodsLotID;
            //剩余库存数保留两位小数点
            qtyCurOverage = ConvertQtyHelp.ConvertQty(qtyCurOverage, 2);
            if (qtyCurOverage >= curOutDtlQty)
            {
                //库存货品出库后剩余的库存数(最小包装单位)
                qtyOverage1 = qtyCurOverage - curOutDtlQty;
                //剩余的库存数(已转换为最小包装单位)转换为当前库存货品的包装单位的库存数
                if (qtyOverage1 > 0)
                {
                    ReaGoods qtyReaGoods = IDReaGoodsDao.Get(sQtyDtl.GoodsID.Value);
                    string resultInfo = "";
                    ReaGoodsOrgLink reaGoodsOrgLink = IDReaGoodsOrgLinkDao.GetReaGoodsMinUnit(long.Parse(ReaCenOrgType.供货方.Key), sQtyDtl.ReaCompanyID.Value, qtyReaGoods.Id, qtyReaGoods, ref resultInfo);
                    qtyGonvertQty = qtyReaGoods.GonvertQty;
                    if (qtyGonvertQty <= 0)
                        qtyGonvertQty = 1;
                    if (reaGoodsOrgLink != null && qtyReaGoods.GonvertQty > 0)
                    {
                        qtyOverage1 = qtyOverage1 / qtyReaGoods.GonvertQty;
                        ZhiFang.Common.Log.Log.Info(string.Format("出库的库存货品剩余的库存数单位换算：转换后数量{0}", qtyOverage1));
                    }
                }

                sQtyDtl.GoodsQty = qtyOverage1;//更新库存数量
                sQtyDtl.SumTotal = qtyOverage1 * sQtyDtl.Price;//更新库存总价
                sQtyDtl.OutFlag = 1;//出库标志
                IBReaBmsQtyDtl.Entity = sQtyDtl;
                if (!IBReaBmsQtyDtl.Edit())
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "货品出库失败：库存更新失败！";
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    throw new Exception(brdv.ErrorInfo);
                }
            }
            else
            {
                sQtyDtl.GoodsQty = 0;
                sQtyDtl.SumTotal = 0;
                sQtyDtl.OutFlag = 1;
                IBReaBmsQtyDtl.Entity = sQtyDtl;
                if (!IBReaBmsQtyDtl.Edit())
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "货品出库失败：库存更新失败！";
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    throw new Exception(brdv.ErrorInfo);
                }
            }
            ZhiFang.Common.Log.Log.Info(string.Format("出库的库存货品原剩余库存数为:{0}，当次出库数为:{1},出库后剩余库存数:{2}", qtyCurOverage, curOutDtlQty, qtyOverage1));
            double curTransferQty2 = curOutDtlQty;
            if (qtyGonvertQty > 1)
            {
                curTransferQty2 = outDtlGoodsQty / qtyGonvertQty;
            }
            IBReaBmsQtyDtlOperation.AddReaBmsQtyDtlOutOperation(outDoc, outDtl, IBReaBmsQtyDtl.Entity, curTransferQty2);
            //剩下的出库明细的出库数=出库明细的出库总数-当次出库数
            outDtlGoodsQty = outDtlGoodsQty - curOutDtlQty;
            if (string.IsNullOrEmpty(outDtl.QtyDtlID))
            {
                outDtl.QtyDtlID = sQtyDtl.Id + ",";
            }
            else if (!outDtl.QtyDtlID.Contains(sQtyDtl.Id.ToString()))
            {
                outDtl.QtyDtlID = outDtl.QtyDtlID.TrimEnd(',') + "," + sQtyDtl.Id + ",";
            }
        }
        public BaseResultDataValue GetReaGoodsMinUnitCount(long? reaGoodsID, long? reaCompanyID, ref double goodsQry, ref double goodsPrice)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ReaGoods reaGoods = IDReaGoodsDao.Get((long)reaGoodsID);
            double gonvertQty = reaGoods.GonvertQty;
            if (gonvertQty <= 0)
                gonvertQty = 1;
            string resultInfo = "";
            ReaGoodsOrgLink reaGoodsOrgLink = IDReaGoodsOrgLinkDao.GetReaGoodsMinUnit(long.Parse(ReaCenOrgType.供货方.Key), reaCompanyID.Value, reaGoods.Id,
                 reaGoods, ref resultInfo);
            if (reaGoodsOrgLink != null)
            {
                reaGoods = reaGoodsOrgLink.ReaGoods;
                brdv.ResultDataValue = reaGoods.Id.ToString();
                goodsQry = goodsQry * gonvertQty;
                goodsPrice = goodsPrice / gonvertQty;
            }
            else
            {
                brdv.ResultDataValue = reaGoods.Id.ToString();
            }
            return brdv;
        }
        /// <summary>
        /// 当前出库扫码条码对应的大包装单位条码集合(出库扫码的条码为小包装条码时)
        /// </summary>
        /// <param name="outDoc"></param>
        /// <param name="outReaGoods"></param>
        /// <param name="outDtl"></param>
        /// <param name="outBarcodeList"></param>
        /// <returns></returns>
        private IList<ReaGoodsBarcodeOperation> AddGetMaxBarCodeList(ReaBmsOutDoc outDoc, ReaGoods outReaGoods, ref ReaBmsOutDtl outDtl, ref IList<ReaGoodsBarcodeOperation> outBarcodeList)
        {
            IList<ReaGoodsBarcodeOperation> maxBarCodeList = new List<ReaGoodsBarcodeOperation>();

            long operTypeID = long.Parse(ReaGoodsBarcodeOperType.使用出库.Key);
            if (outDoc.OutType.ToString() == ReaBmsOutDocOutType.报损出库.Key)
            {
                operTypeID = long.Parse(ReaGoodsBarcodeOperType.报损出库.Key);
            }
            else if (outDoc.OutType.ToString() == ReaBmsOutDocOutType.退供应商.Key)
            {
                operTypeID = long.Parse(ReaGoodsBarcodeOperType.退供应商.Key);
            }
            else if (outDoc.OutType.ToString() == ReaBmsOutDocOutType.退库入库.Key)
            {
                operTypeID = long.Parse(ReaGoodsBarcodeOperType.退库入库.Key);
            }
            else if (outDoc.OutType.ToString() == ReaBmsOutDocOutType.盘亏出库.Key)
            {
                operTypeID = long.Parse(ReaGoodsBarcodeOperType.盘亏出库.Key);
            }
            //按条码的所属父条码进行分组
            var maxGroupByList = outBarcodeList.Where(p => string.IsNullOrEmpty(p.PUsePackSerial) == false).GroupBy(p => p.PUsePackSerial);
            foreach (var maxGroupBy in maxGroupByList)
            {
                if (string.IsNullOrEmpty(maxGroupBy.Key))
                    continue;

                ReaGoodsBarcodeOperation maxOperation = maxGroupBy.ElementAt(0);
                string storageId = "";
                if (maxOperation.StorageID.HasValue)
                    storageId = maxOperation.StorageID.Value.ToString();
                string placeId = "";
                if (maxOperation.PlaceID.HasValue)
                    placeId = maxOperation.PlaceID.Value.ToString();
                //当前扫码条码的原始父条码
                string maxBarCodeHql = " reagoodsbarcodeoperation.Id='" + maxOperation.PUsePackSerial + "' and reagoodsbarcodeoperation.PUsePackSerial='" + maxOperation.PUsePackSerial + "'";
                if (!string.IsNullOrEmpty(storageId))
                    maxBarCodeHql = maxBarCodeHql + " and reagoodsbarcodeoperation.StorageID=" + storageId;
                if (!string.IsNullOrEmpty(placeId))
                    maxBarCodeHql = maxBarCodeHql + " and reagoodsbarcodeoperation.PlaceID=" + placeId;
                IList<ReaGoodsBarcodeOperation> maxBarCodeList2 = IBReaGoodsBarcodeOperation.SearchListByHQL(maxBarCodeHql);//GetListByHQL
                ZhiFang.Common.Log.Log.Info(string.Format("出库扫码.maxBarCodeHql:{0},当前出库扫码货品对应的大包装单位条码集合：{1}", maxBarCodeHql, maxBarCodeList2.Count));
                if (maxBarCodeList2 == null || maxBarCodeList2.Count <= 0)
                    continue;

                //当前条码的原始父条码记录
                ReaGoodsBarcodeOperation pMaxBarCode = maxBarCodeList2.OrderByDescending(p => p.DataAddTime).ElementAt(0);
                //当前扫码条码的所有父条码记录
                maxBarCodeHql = " reagoodsbarcodeoperation.PUsePackSerial='" + pMaxBarCode.PUsePackSerial + "' and (reagoodsbarcodeoperation.UsePackSerial='" + pMaxBarCode.UsePackSerial + "' and reagoodsbarcodeoperation.UsePackQRCode='" + pMaxBarCode.UsePackQRCode + "')";
                if (!string.IsNullOrEmpty(storageId))
                    maxBarCodeHql = maxBarCodeHql + " and reagoodsbarcodeoperation.StorageID=" + storageId;
                if (!string.IsNullOrEmpty(placeId))
                    maxBarCodeHql = maxBarCodeHql + " and reagoodsbarcodeoperation.PlaceID=" + placeId;
                maxBarCodeList2 = IBReaGoodsBarcodeOperation.SearchListByHQL(maxBarCodeHql);
                ZhiFang.Common.Log.Log.Info(string.Format("出库扫码.maxBarCodeHql:{0},当前出库扫码货品对应的大包装单位条码集合：{1}", maxBarCodeHql, maxBarCodeList2.Count));
                if (maxBarCodeList2 == null || maxBarCodeList2.Count <= 0)
                    continue;

                ReaGoodsBarcodeOperation maxBarCode1 = maxBarCodeList2.OrderByDescending(p => p.DataAddTime).ElementAt(0);
                ReaGoodsBarcodeOperation addMaxOperation = ClassMapperHelp.GetMapper<ReaGoodsBarcodeOperation, ReaGoodsBarcodeOperation>(maxBarCode1);
                //addMaxOperation.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                addMaxOperation.LabID = outDtl.LabID;
                addMaxOperation.BDocID = outDtl.OutDocID;
                addMaxOperation.BDocNo = outDoc.OutDocNo;
                addMaxOperation.BDtlID = outDtl.Id;
                //addMaxOperation.QtyDtlID = sQtyDtl.Id;
                addMaxOperation.OperTypeID = operTypeID;
                addMaxOperation.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
                addMaxOperation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[addMaxOperation.OperTypeID.ToString()].Name;
                addMaxOperation.ScanCodeGoodsID = outDtl.GoodsID;
                addMaxOperation.ScanCodeGoodsUnit = outDtl.GoodsUnit;
                ZhiFang.Common.Log.Log.Info(string.Format("出库扫码.PUsePackSerial:{0},当前OverageQty：{1}", maxOperation.PUsePackSerial, addMaxOperation.OverageQty));
                //本次扫码次数先重置为0
                addMaxOperation.ScanCodeQty = 0;
                if (!addMaxOperation.OverageQty.HasValue)
                    addMaxOperation.OverageQty = addMaxOperation.MinBarCodeQty;
                if (!maxBarCodeList.Contains(addMaxOperation))
                    maxBarCodeList.Add(addMaxOperation);
            }
            return maxBarCodeList;
        }
        /// <summary>
        /// 出库明细的某一出库扫码条码处理
        /// </summary>
        /// <param name="outDoc"></param>
        /// <param name="outDtl"></param>
        /// <param name="sQtyDtl"></param>
        /// <param name="addOperation"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        private BaseResultBool AddCurBarcodeOperation(ReaBmsOutDoc outDoc, ReaBmsOutDtl outDtl, ReaBmsQtyDtl sQtyDtl, ReaGoodsBarcodeOperation addOperation, ReaGoodsBarcodeOperation pOutBarcode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            long operTypeID = long.Parse(ReaGoodsBarcodeOperType.使用出库.Key);
            if (outDoc.OutType.ToString() == ReaBmsOutDocOutType.报损出库.Key)
            {
                operTypeID = long.Parse(ReaGoodsBarcodeOperType.报损出库.Key);
            }
            else if (outDoc.OutType.ToString() == ReaBmsOutDocOutType.退供应商.Key)
            {
                operTypeID = long.Parse(ReaGoodsBarcodeOperType.退供应商.Key);
            }
            else if (outDoc.OutType.ToString() == ReaBmsOutDocOutType.退库入库.Key)
            {
                operTypeID = long.Parse(ReaGoodsBarcodeOperType.退库入库.Key);
            }
            else if (outDoc.OutType.ToString() == ReaBmsOutDocOutType.盘亏出库.Key)
            {
                operTypeID = long.Parse(ReaGoodsBarcodeOperType.盘亏出库.Key);
            }
            addOperation.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
            addOperation.LabID = outDtl.LabID;
            addOperation.BDocID = outDtl.OutDocID;
            addOperation.BDocNo = outDoc.OutDocNo;
            addOperation.BDtlID = outDtl.Id;
            addOperation.QtyDtlID = sQtyDtl.Id;
            addOperation.OperTypeID = operTypeID;
            addOperation.ReaCompanyID = outDtl.ReaCompanyID;
            addOperation.CompanyName = outDtl.CompanyName;
            addOperation.ReaCompCode = outDtl.ReaCompCode;
            addOperation.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
            addOperation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[addOperation.OperTypeID.ToString()].Name;
            if (!addOperation.GoodsLotID.HasValue)
                addOperation.GoodsLotID = sQtyDtl.GoodsLotID;
            addOperation.LotNo = outDtl.LotNo;
            addOperation.CompGoodsLinkID = outDtl.CompGoodsLinkID;
            addOperation.StorageID = outDtl.StorageID;
            addOperation.PlaceID = outDtl.PlaceID;
            addOperation.GoodsQty = outDtl.GoodsQty;
            if (!addOperation.GoodsID.HasValue)
                addOperation.GoodsID = outDtl.GoodsID;
            if (string.IsNullOrEmpty(addOperation.GoodsCName))
                addOperation.GoodsCName = outDtl.GoodsCName;
            if (string.IsNullOrEmpty(addOperation.GoodsUnit))
                addOperation.GoodsUnit = outDtl.GoodsUnit;
            if (string.IsNullOrEmpty(addOperation.ReaGoodsNo))
                addOperation.ReaGoodsNo = outDtl.ReaGoodsNo;
            if (string.IsNullOrEmpty(addOperation.ProdGoodsNo))
                addOperation.ProdGoodsNo = outDtl.ProdGoodsNo;
            if (string.IsNullOrEmpty(addOperation.CenOrgGoodsNo))
                addOperation.CenOrgGoodsNo = outDtl.CenOrgGoodsNo;
            if (string.IsNullOrEmpty(addOperation.GoodsNo))
                addOperation.GoodsNo = outDtl.GoodsNo;
            if (addOperation.GoodsSort <= 0)
                addOperation.GoodsSort = outDtl.GoodsSort;
            if (string.IsNullOrEmpty(addOperation.UnitMemo))
                addOperation.UnitMemo = outDtl.UnitMemo;
            if (!addOperation.ScanCodeQty.HasValue)
                addOperation.ScanCodeQty = 1;
            if (string.IsNullOrEmpty(addOperation.ScanCodeGoodsUnit))
                addOperation.ScanCodeGoodsUnit = outDtl.GoodsUnit;
            if (!addOperation.ScanCodeGoodsID.HasValue)
                addOperation.ScanCodeGoodsID = outDtl.GoodsID;
            addOperation.BarCodeType = int.Parse(outDtl.BarCodeType.ToString());
            if (!addOperation.MinBarCodeQty.HasValue)
            {
                ReaGoods reaGoods = IDReaGoodsDao.Get(outDtl.GoodsID.Value);
                double gonvertQty = 1;
                if (reaGoods != null) gonvertQty = reaGoods.GonvertQty;
                if (gonvertQty <= 0)
                {
                    ZhiFang.Common.Log.Log.Warn("货品编码为:" + addOperation.ReaGoodsNo + ",货品名称为:" + addOperation.GoodsCName + ",货品包装单位的换算系数值为" + gonvertQty + ",维护不合理!");
                    gonvertQty = 1;
                }
                addOperation.MinBarCodeQty = gonvertQty;
            }
            if (!addOperation.ScanCodeQty.HasValue)
                addOperation.ScanCodeQty = 1;
            tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperation(addOperation, 0, empID, empName, outDtl.LabID);

            #region 当前出库扫码(为大包装单位)对应的小包装单位条码
            ZhiFang.Common.Log.Log.Info(string.Format("出库扫码.addOperation.OverageQty:{0},addOperation.PUsePackSerial：{1},pOutBarcode is null：{2}", addOperation.OverageQty, addOperation.PUsePackSerial, pOutBarcode == null ? true : false));
            if (addOperation.OverageQty <= 0 && !string.IsNullOrEmpty(addOperation.PUsePackSerial) && pOutBarcode != null)
            {
                if (pOutBarcode.PUsePackSerial == addOperation.PUsePackSerial && (pOutBarcode.UsePackSerial == addOperation.UsePackSerial || pOutBarcode.UsePackQRCode == addOperation.UsePackQRCode))
                {
                    IList<ReaGoodsBarcodeOperation> minBarCodeList = new List<ReaGoodsBarcodeOperation>();
                    string storageId = "";
                    if (addOperation.StorageID.HasValue)
                        storageId = addOperation.StorageID.Value.ToString();
                    string placeId = "";
                    if (addOperation.PlaceID.HasValue)
                        placeId = addOperation.PlaceID.Value.ToString();
                    string minBarCodeHql = " (reagoodsbarcodeoperation.OverageQty>0 or reagoodsbarcodeoperation.OverageQty is null) and reagoodsbarcodeoperation.GoodsUnit!='" + addOperation.GoodsUnit + "' and reagoodsbarcodeoperation.PUsePackSerial='" + addOperation.PUsePackSerial + "' and (reagoodsbarcodeoperation.UsePackSerial!='" + addOperation.UsePackSerial + "' and reagoodsbarcodeoperation.UsePackQRCode!='" + addOperation.UsePackQRCode + "') and (reagoodsbarcodeoperation.UsePackSerial!='" + pOutBarcode.UsePackSerial + "' and reagoodsbarcodeoperation.UsePackQRCode!='" + pOutBarcode.UsePackQRCode + "')";
                    if (!string.IsNullOrEmpty(storageId))
                        minBarCodeHql = minBarCodeHql + " and reagoodsbarcodeoperation.StorageID=" + storageId;
                    if (!string.IsNullOrEmpty(placeId))
                        minBarCodeHql = minBarCodeHql + " and reagoodsbarcodeoperation.PlaceID=" + placeId;
                    minBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(minBarCodeHql);
                    ZhiFang.Common.Log.Log.Info(string.Format("出库扫码.minBarCodeHql:{0},当前出库扫码货品对应的小包装单位条码集合：{1}", minBarCodeHql, minBarCodeList.Count));
                    if (minBarCodeList != null && minBarCodeList.Count > 0)
                    {
                        ReaGoods addMinReaGoods = IDReaGoodsDao.Get(minBarCodeList[0].GoodsID.Value);
                        foreach (ReaGoodsBarcodeOperation minBarCode in minBarCodeList)
                        {
                            ReaGoodsBarcodeOperation addMinBarCode = ClassMapperHelp.GetMapper<ReaGoodsBarcodeOperation, ReaGoodsBarcodeOperation>(minBarCode);
                            addMinBarCode.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                            addMinBarCode.LabID = addOperation.LabID;
                            addMinBarCode.BDocID = addOperation.BDocID;
                            addMinBarCode.BDocNo = addOperation.BDocNo;
                            addMinBarCode.BDtlID = addOperation.Id;
                            addMinBarCode.QtyDtlID = addOperation.QtyDtlID;
                            addMinBarCode.PUsePackSerial = addOperation.PUsePackSerial;
                            addMinBarCode.OperTypeID = operTypeID;
                            addMinBarCode.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
                            addMinBarCode.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[addMinBarCode.OperTypeID.ToString()].Name;
                            if (!addMinBarCode.ScanCodeGoodsID.HasValue)
                                addMinBarCode.ScanCodeGoodsID = addMinReaGoods.Id;
                            if (string.IsNullOrEmpty(addMinBarCode.ScanCodeGoodsUnit))
                                addMinBarCode.ScanCodeGoodsUnit = addMinReaGoods.UnitName;
                            if (!addMinBarCode.ScanCodeQty.HasValue)
                                addMinBarCode.ScanCodeQty = 1;
                            if (!addMinBarCode.MinBarCodeQty.HasValue)
                                addMinBarCode.MinBarCodeQty = addMinReaGoods.GonvertQty;
                            addMinBarCode.OverageQty = 0;
                            addMinBarCode.Memo = "非直接出库扫码;大包装货品条码出库扫码,同时更新对应小包装货品条码为出库!";
                            tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperation(addMinBarCode, 0, empID, empName, outDtl.LabID);
                        }
                    }
                }
            }
            #endregion
            return tempBaseResultBool;
        }
        #endregion

        #region 出库申请/审核
        public EntityList<ReaBmsOutDoc> SearchReaBmsOutDocByReqDeptHQL(long deptId, string strHqlWhere, string order, int page, int count)
        {
            EntityList<ReaBmsOutDoc> entityList = new EntityList<ReaBmsOutDoc>();
            if (deptId >= 0)
            {
                //申请人所属部门及其所属部门的所有子部门信息
                string deptIdStr = IBHRDept.SearchHRDeptIdListByHRDeptId(deptId);
                if (string.IsNullOrEmpty(strHqlWhere))
                    strHqlWhere = " 1=1 ";
                if (!string.IsNullOrEmpty(deptIdStr))
                {
                    strHqlWhere = strHqlWhere + " and reabmsoutdoc.DeptID in(" + deptIdStr.TrimEnd(',') + ")";
                }
            }
            entityList = this.SearchListByHQL(strHqlWhere, order, page, count);
            return entityList;
        }
        public BaseResultDataValue SearchSumReqGoodsQtyAndCurrentQtyByHQL(string qtyHql, string dtlHql, string goodsId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ////当前剩余总库存
            double sumCurrentQty = 0;
            //已申请总数
            double sumDtlGoodsQty = 0;
            ReaGoods dtlGooods = null;
            if (!string.IsNullOrEmpty(goodsId))
            {
                dtlGooods = IDReaGoodsDao.Get(long.Parse(goodsId), false);
            }
            if (dtlGooods != null && !string.IsNullOrEmpty(qtyHql))
            {
                IList<ReaBmsQtyDtl> qtyList = IBReaBmsQtyDtl.SearchListByHQL(qtyHql);
                if (qtyList != null)
                {
                    var groupByList = qtyList.GroupBy(p => p.GoodsID.Value);
                    foreach (var groupBy in groupByList)
                    {
                        ReaGoods qtyGooods = IDReaGoodsDao.Get(groupBy.Key, false);
                        if (qtyGooods == null) continue;

                        if (qtyGooods.Id == dtlGooods.Id)
                        {
                            sumCurrentQty = sumCurrentQty + groupBy.Where(p => p.GoodsQty.HasValue == true).Sum(p => p.GoodsQty.Value);
                        }
                        else
                        {
                            //需要将库存货品数按dtlGooods的包装单位进行转换
                            double goodsQty = groupBy.Where(p => p.GoodsQty.HasValue == true).Sum(p => p.GoodsQty.Value);
                            if (dtlGooods.GonvertQty > qtyGooods.GonvertQty)
                            {
                                sumCurrentQty = sumCurrentQty + (goodsQty / dtlGooods.GonvertQty);
                            }
                            else if (dtlGooods.GonvertQty < qtyGooods.GonvertQty)
                            {
                                sumCurrentQty = sumCurrentQty + (goodsQty * dtlGooods.GonvertQty);
                            }
                        }
                    }
                }
            }
            if (dtlGooods != null && !string.IsNullOrEmpty(dtlHql))
            {
                string docHql = string.Format(" reabmsoutdoc.Visible=1 and reabmsoutdoc.Status!={0} and reabmsoutdoc.Status!={1}", ReaBmsOutDocStatus.申请作废.Key, ReaBmsOutDocStatus.出库完成.Key);
                IList<ReaBmsOutDtl> dtlList = IBReaBmsOutDtl.SearchReaBmsOutDtlSummaryListByHQL(docHql, dtlHql, "", "", -1, -1);
                if (dtlList != null)
                {
                    var groupByList = dtlList.GroupBy(p => p.GoodsID.Value);
                    foreach (var groupBy in groupByList)
                    {
                        ReaGoods oldDtlGooods = IDReaGoodsDao.Get(groupBy.Key, false);
                        if (oldDtlGooods == null) continue;

                        if (oldDtlGooods.Id == dtlGooods.Id)
                        {
                            sumDtlGoodsQty = sumDtlGoodsQty + groupBy.Where(p => p.GoodsQty > 0).Sum(p => p.GoodsQty);
                        }
                        else
                        {
                            //需要将库存货品数按dtlGooods的包装单位进行转换
                            double goodsQty = groupBy.Sum(p => p.GoodsQty);
                            if (dtlGooods.GonvertQty > oldDtlGooods.GonvertQty)
                            {
                                sumDtlGoodsQty = sumDtlGoodsQty + (goodsQty / dtlGooods.GonvertQty);
                            }
                            else if (dtlGooods.GonvertQty < oldDtlGooods.GonvertQty)
                            {
                                sumDtlGoodsQty = sumDtlGoodsQty + (goodsQty * dtlGooods.GonvertQty);
                            }
                        }
                    }
                }
            }
            JObject result = new JObject();
            sumCurrentQty = ConvertQtyHelp.ConvertQty(sumCurrentQty, 2);
            sumDtlGoodsQty = ConvertQtyHelp.ConvertQty(sumDtlGoodsQty, 2);
            result.Add("SumCurrentQty", sumCurrentQty);
            result.Add("SumDtlGoodsQty", sumDtlGoodsQty);
            baseResultDataValue.ResultDataValue = result.ToString();
            return baseResultDataValue;
        }
        public BaseResultDataValue AddReaBmsOutDocAndDtlOfApply(ReaBmsOutDoc entity, IList<ReaBmsOutDtl> dtAddList, bool isEmpOut, bool isNeedPerformanceTest, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "不能新增出库保存,传入参数entity为空!";
                return brdv;
            }
            if (dtAddList == null || dtAddList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "不能新增出库保存,传入参数dtAddList为空!";
                return brdv;
            }
            //出库货品的货品批号是否需要性能验证后才能使用出库处理
            if (isNeedPerformanceTest == true && entity.OutType == long.Parse(ReaBmsOutDocOutType.使用出库.Key))
                brdv = AddValidNeedPerformanceTest(dtAddList, empID, empName);

            //检查出库选择的库存货品是否被盘库锁定
            brdv = AddValidDocAndDtl(dtAddList, isEmpOut, empID, empName);
            if (!brdv.success)
                return brdv;

            if (entity.TotalPrice <= 0)
                entity.TotalPrice = dtAddList.Sum(p => p.SumTotal);
            AddReaBmsOutDoc(ref entity, empID, empName, ref brdv);
            if (brdv.success)
            {
                brdv = IBReaBmsOutDtl.AddOutDtlList(entity, dtAddList, empID, empName);
                if (brdv.success) AddSCOperation(entity, empID, empName);
            }
            else
            {
                brdv.ErrorInfo = "新增出库保存失败!";
            }

            return brdv;
        }
        public BaseResultBool UpdateReaBmsOutDocAndDtl(ReaBmsOutDoc entity, IList<ReaBmsOutDtl> dtlAddList, IList<ReaBmsOutDtl> dtlEditList, bool isEmpOut, bool isNeedPerformanceTest, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();
            if (entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "不能新增出库保存,传入参数entity为空!";
                return brdv;
            }
            if ((dtlAddList == null || dtlAddList.Count <= 0) && (dtlEditList == null || dtlEditList.Count <= 0))
            {
                brdv.success = false;
                brdv.ErrorInfo = "不能新增出库保存,传入参数dtAddList及dtEditList都为空!";
                return brdv;
            }
            ReaBmsOutDoc serverEntity = this.Get(entity.Id);
            if (serverEntity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取出库单ID为:" + entity.Id + ",出库信息为空!";
                return brdv;
            }
            //出库货品的货品批号是否需要性能验证后才能使用出库处理
            if (dtlAddList != null && dtlAddList.Count > 0 && isNeedPerformanceTest == true && entity.OutType == long.Parse(ReaBmsOutDocOutType.使用出库.Key))
            {
                BaseResultDataValue brdv2 = AddValidNeedPerformanceTest(dtlAddList, empID, empName);
                brdv.success = brdv2.success;
                brdv.ErrorInfo = brdv2.ErrorInfo;
                if (!brdv.success)
                    return brdv;
            }
            if (dtlEditList != null && dtlEditList.Count > 0 && isNeedPerformanceTest == true && entity.OutType == long.Parse(ReaBmsOutDocOutType.使用出库.Key))
            {
                BaseResultDataValue brdv2 = AddValidNeedPerformanceTest(dtlEditList, empID, empName);
                brdv.success = brdv2.success;
                brdv.ErrorInfo = brdv2.ErrorInfo;
                if (!brdv.success)
                    return brdv;
            }
            #region 检查是否有出库权限及被盘库锁定
            if (dtlAddList != null && dtlAddList.Count > 0)
            {
                BaseResultDataValue brdv2 = AddValidDocAndDtl(dtlAddList, isEmpOut, empID, empName);
                brdv.success = brdv2.success;
                brdv.ErrorInfo = brdv2.ErrorInfo;
                if (!brdv.success)
                    return brdv;
            }
            else if (isEmpOut)
            {
                IList<ReaUserStorageLink> userStorageLinkList = null;
                userStorageLinkList = IBReaUserStorageLink.SearchListByHQL(" reauserstoragelink.OperID=" + empID + " and reauserstoragelink.OperType=" + ReaUserStorageLinkOperType.库房管理权限.Key);
                if (userStorageLinkList == null || userStorageLinkList.Count == 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "当前用户没有相应的出库权限！";
                    return brdv;
                }
            }
            if (dtlEditList != null && dtlEditList.Count > 0)
            {
                foreach (var outDtl in dtlEditList)
                {
                    brdv = IDReaBmsCheckDocDao.EditValidIsLock(outDtl.ReaCompanyID, outDtl.CompanyName, outDtl.StorageID, outDtl.StorageName, outDtl.PlaceID, outDtl.PlaceName, outDtl.GoodsID);
                    if (!brdv.success)
                        return brdv;
                }
            }
            #endregion
            List<string> tmpa = new List<string>();
            if (!EditReaBmsOutDocStatusCheck(entity, serverEntity, tmpa, empID, empName))
            {
                brdv.success = false;
                if (serverEntity.Visible == false)
                    brdv.ErrorInfo = "出库单ID：" + entity.Id + "已作废,不能再操作！";
                else
                    brdv.ErrorInfo = "出库单ID：" + entity.Id + "的状态为：" + ReaBmsOutDocStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return brdv;
            }
            if (dtlAddList != null && dtlAddList.Count > 0)
            {
                BaseResultDataValue brdv2 = IBReaBmsOutDtl.AddOutDtlList(entity, dtlAddList, empID, empName);
                brdv.success = brdv2.success;
                brdv.ErrorInfo = brdv2.ErrorInfo;
            }
            if (!brdv.success) return brdv;

            if (dtlEditList != null && dtlEditList.Count > 0)
            {
                brdv = IBReaBmsOutDtl.UpdateOutDtlList(entity, dtlEditList);
            }
            if (!brdv.success) return brdv;

            if (dtlAddList != null)
                entity.TotalPrice = dtlAddList.Sum(p => p.SumTotal);
            if (dtlEditList != null)
                entity.TotalPrice = entity.TotalPrice + dtlEditList.Sum(p => p.SumTotal);
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            entity.DataUpdateTime = DateTime.Now;
            entity.DataTimeStamp = serverEntity.DataTimeStamp;
            this.Entity = entity;

            if (this.Edit())
            {
                AddSCOperation(entity, empID, empName);
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "编辑保存出库主单信息失败！";
            }
            return brdv;
        }
        public BaseResultBool UpdateReaBmsOutDocByCheck(ReaBmsOutDoc entity, string[] tempArray, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();
            if (entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数entity为空!";
                return brdv;
            }
            ReaBmsOutDoc serverEntity = this.Get(entity.Id);
            if (serverEntity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取出库单ID为:" + entity.Id + ",出库信息为空!";
                return brdv;
            }
            List<string> tmpa = tempArray.ToList();// new List<string>();
            if (!EditReaBmsOutDocStatusCheck(entity, serverEntity, tmpa, empID, empName))
            {
                brdv.success = false;
                if (serverEntity.Visible == false)
                    brdv.ErrorInfo = "出库单ID：" + entity.Id + "已作废,不能再操作！";
                else
                    brdv.ErrorInfo = "出库单ID：" + entity.Id + "的状态为：" + ReaBmsOutDocStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return brdv;
            }
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            entity.DataUpdateTime = DateTime.Now;
            entity.DataTimeStamp = serverEntity.DataTimeStamp;
            this.Entity = entity;
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            //if (this.Edit())
            {
                AddSCOperation(entity, empID, empName);
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "保存出库申请单信息失败！";
            }
            return brdv;
        }
        public BaseResultBool UpdateReaBmsOutDocAndDtlOfComp(ReaBmsOutDoc entity, IList<ReaBmsOutDtl> dtlAddList, IList<ReaBmsOutDtl> dtlEditList, bool isEmpOut, bool isNeedPerformanceTest, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();
            if (entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "不能确认出库,传入参数entity为空!";
                return brdv;
            }
            if ((dtlAddList == null || dtlAddList.Count <= 0) && (dtlEditList == null || dtlEditList.Count <= 0))
            {
                brdv.success = false;
                brdv.ErrorInfo = "不能确认出库,传入参数dtAddList及dtEditList都为空!";
                return brdv;
            }
            ReaBmsOutDoc serverEntity = this.Get(entity.Id);
            if (serverEntity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取出库单ID为:" + entity.Id + ",出库信息为空!";
                return brdv;
            }
            if (!entity.CreaterID.HasValue)
            {
                entity.CreaterID = empID;
                entity.CreaterName = empName;
            }
            //出库货品的货品批号是否需要性能验证后才能使用出库处理
            if (dtlAddList != null && dtlAddList.Count > 0 && isNeedPerformanceTest == true && entity.OutType == long.Parse(ReaBmsOutDocOutType.使用出库.Key))
            {
                BaseResultDataValue brdv2 = AddValidNeedPerformanceTest(dtlAddList, empID, empName);
                brdv.success = brdv2.success;
                brdv.ErrorInfo = brdv2.ErrorInfo;
                if (!brdv.success)
                    return brdv;
            }
            if (dtlEditList != null && dtlEditList.Count > 0 && isNeedPerformanceTest == true && entity.OutType == long.Parse(ReaBmsOutDocOutType.使用出库.Key))
            {
                BaseResultDataValue brdv2 = AddValidNeedPerformanceTest(dtlEditList, empID, empName);
                brdv.success = brdv2.success;
                brdv.ErrorInfo = brdv2.ErrorInfo;
                if (!brdv.success)
                    return brdv;
            }
            #region 检查是否有出库权限及被盘库锁定
            if (dtlAddList != null && dtlAddList.Count > 0)
            {
                BaseResultDataValue brdv2 = AddValidDocAndDtl(dtlAddList, isEmpOut, empID, empName);
                brdv.success = brdv2.success;
                brdv.ErrorInfo = brdv2.ErrorInfo;
                if (!brdv.success)
                    return brdv;
            }
            else if (isEmpOut)
            {
                IList<ReaUserStorageLink> userStorageLinkList = null;
                userStorageLinkList = IBReaUserStorageLink.SearchListByHQL(" reauserstoragelink.OperID=" + empID + " and reauserstoragelink.OperType=" + ReaUserStorageLinkOperType.库房管理权限.Key);
                if (userStorageLinkList == null || userStorageLinkList.Count == 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "当前用户没有相应的出库权限！";
                    return brdv;
                }
            }
            if (dtlEditList != null && dtlEditList.Count > 0)
            {
                foreach (var outDtl in dtlEditList)
                {
                    brdv = IDReaBmsCheckDocDao.EditValidIsLock(outDtl.ReaCompanyID, outDtl.CompanyName, outDtl.StorageID, outDtl.StorageName, outDtl.PlaceID, outDtl.PlaceName, outDtl.GoodsID);
                    if (!brdv.success)
                        return brdv;
                }
            }
            #endregion
            List<string> tmpa = new List<string>();
            if (!EditReaBmsOutDocStatusCheck(entity, serverEntity, tmpa, empID, empName))
            {
                brdv.success = false;
                if (serverEntity.Visible == false)
                    brdv.ErrorInfo = "出库单ID：" + entity.Id + "已作废,不能再操作！";
                else
                    brdv.ErrorInfo = "出库单ID：" + entity.Id + "的状态为：" + ReaBmsOutDocStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return brdv;
            }

            if (dtlAddList != null && dtlAddList.Count > 0)
            {
                BaseResultDataValue brdv2 = new BaseResultDataValue();
                foreach (var outDtl1 in dtlAddList)
                {
                    var outDtl = outDtl1;
                    outDtl.OutDocID = entity.Id;
                    EditReaBmsOutDtl(entity, ref outDtl, outDtl.ReaBmsOutDtlLinkList, empID, empName, ref brdv2);
                    brdv.success = brdv2.success;
                    brdv.ErrorInfo = brdv2.ErrorInfo;
                    if (brdv.success)
                    {
                        outDtl.SumTotal = outDtl.GoodsQty * outDtl.Price;
                        IBReaBmsOutDtl.Entity = outDtl;
                        ZhiFang.Common.Log.Log.Debug("UpdateReaBmsOutDocAndDtlOfComp.outDtl.Id:" + outDtl.Id + ",outDoc.Id:" + entity.Id + ",outDtl.GoodsQty:" + outDtl.GoodsQty + ",outDtl.Price:" + outDtl.Price + ",outDtl.SumTotal:" + outDtl.SumTotal);
                        if (!IBReaBmsOutDtl.Add())
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "货品出库失败：出库明细单保存失败！";
                        }
                    }
                    if (brdv.success == false)
                        break;
                }
            }
            if (!brdv.success) return brdv;

            if (dtlEditList != null && dtlEditList.Count > 0)
            {
                BaseResultDataValue brdv2 = new BaseResultDataValue();
                foreach (var outDtl1 in dtlEditList)
                {
                    var outDtl = outDtl1;
                    outDtl1.DataUpdateTime = DateTime.Now;
                    outDtl.OutDocID = entity.Id;
                    EditReaBmsOutDtl(entity, ref outDtl, outDtl.ReaBmsOutDtlLinkList, empID, empName, ref brdv2);
                    brdv.success = brdv2.success;
                    brdv.ErrorInfo = brdv2.ErrorInfo;
                    if (brdv.success)
                    {
                        outDtl.DataTimeStamp = IBReaBmsOutDtl.Get(outDtl.Id).DataTimeStamp;
                        outDtl.SumTotal = outDtl.GoodsQty * outDtl.Price;
                        ZhiFang.Common.Log.Log.Debug("UpdateReaBmsOutDocAndDtlOfComp.outDtl.Id:" + outDtl.Id + ",outDoc.Id:" + entity.Id + ",outDtl.GoodsQty:" + outDtl.GoodsQty + ",outDtl.Price:" + outDtl.Price + ",outDtl.SumTotal:" + outDtl.SumTotal);
                        IBReaBmsOutDtl.Entity = outDtl;
                        if (!IBReaBmsOutDtl.Edit())
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "货品出库失败：出库明细单保存失败！";
                        }
                    }

                    if (brdv.success == false)
                        break;
                }
            }
            if (!brdv.success) return brdv;

            if (dtlAddList != null)
                entity.TotalPrice = dtlAddList.Sum(p => p.SumTotal);
            if (dtlEditList != null)
                entity.TotalPrice = entity.TotalPrice + dtlEditList.Sum(p => p.SumTotal);
            entity.DataTimeStamp = serverEntity.DataTimeStamp;
            entity.DataUpdateTime = DateTime.Now;
            if (entity.Status.ToString() == ReaBmsOutDocStatus.出库完成.Key)
            {
                if (!entity.DataAddTime.HasValue)
                {
                    entity.DataAddTime = serverEntity.DataAddTime;
                }
                if (!entity.TakerID.HasValue)
                {
                    entity.TakerID = serverEntity.TakerID;
                    entity.TakerName = serverEntity.TakerName;

                }
                if (!entity.OutBoundID.HasValue)
                {
                    entity.OutBoundID = serverEntity.OutBoundID;
                    entity.OutBoundName = serverEntity.OutBoundName;
                }
                if (!entity.CheckID.HasValue)
                {
                    entity.IsHasCheck = serverEntity.IsHasCheck;
                    entity.CheckID = serverEntity.CheckID;
                    entity.CheckName = serverEntity.CheckName;
                    entity.CheckTime = serverEntity.CheckTime;
                    entity.CheckMemo = serverEntity.CheckMemo;
                }
                if (!entity.ApprovalId.HasValue)
                {
                    entity.IsHasApproval = serverEntity.IsHasApproval;
                    entity.ApprovalId = serverEntity.ApprovalId;
                    entity.ApprovalCName = serverEntity.ApprovalCName;
                    entity.ApprovalTime = serverEntity.ApprovalTime;
                    entity.ApprovalMemo = serverEntity.ApprovalMemo;
                }
                entity.ZX1 = serverEntity.ZX1;
                entity.ZX2 = serverEntity.ZX2;
                entity.ZX3 = serverEntity.ZX3;
                entity.Memo = serverEntity.Memo;
            }
            this.Entity = entity;
            if (this.Edit())
            {
                AddSCOperation(entity, empID, empName);
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "编辑保存出库主单信息失败！";
            }
            return brdv;
        }
        bool EditReaBmsOutDocStatusCheck(ReaBmsOutDoc entity, ReaBmsOutDoc serverEntity, List<string> tmpa, long empID, string empName)
        {
            if (serverEntity.Visible == false)
            {
                return false;
            }
            if (entity.Status.ToString() == ReaBmsOutDocStatus.暂存.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOutDocStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsOutDocStatus.审核退回.Key)
                {
                    return false;
                }
            }
            else if (entity.Status.ToString() == ReaBmsOutDocStatus.已申请.Key)
            {
                //&& serverEntity.Status.ToString() != ReaBmsOutDocStatus.已申请.Key
                if (serverEntity.Status.ToString() != ReaBmsOutDocStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsOutDocStatus.审核退回.Key)
                {
                    return false;
                }
                if (!entity.CreaterID.HasValue)
                {
                    entity.CreaterID = empID;
                    if (string.IsNullOrEmpty(entity.CreaterName)) entity.CreaterName = empName;
                    tmpa.Add("CreaterID=" + entity.CreaterID + " ");
                    tmpa.Add("CreaterName='" + entity.CreaterName + "'");
                }
                if (!entity.OperDate.HasValue) tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            else if (entity.Status.ToString() == ReaBmsOutDocStatus.申请作废.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOutDocStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsOutDocStatus.审核退回.Key)
                {
                    return false;
                }
                entity.Visible = false;
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            else if (entity.Status.ToString() == ReaBmsOutDocStatus.审核通过.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOutDocStatus.已申请.Key && serverEntity.Status.ToString() != ReaBmsOutDocStatus.审批退回.Key)
                {
                    return false;
                }
                entity.IsHasCheck = true;
                entity.CheckID = empID;
                entity.CheckName = empName;
                entity.CheckTime = DateTime.Now;
                tmpa.Add("IsHasCheck=1 ");
                tmpa.Add("CheckID=" + entity.CheckID + " ");
                tmpa.Add("CheckName='" + entity.CheckName + "'");
                tmpa.Add("CheckTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("CheckMemo='" + entity.CheckMemo + "'");
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            else if (entity.Status.ToString() == ReaBmsOutDocStatus.审核退回.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOutDocStatus.已申请.Key && serverEntity.Status.ToString() != ReaBmsOutDocStatus.审批退回.Key)
                {
                    return false;
                }
                entity.IsHasCheck = true;
                entity.CheckID = empID;
                entity.CheckName = empName;
                entity.CheckTime = DateTime.Now;
                tmpa.Add("IsHasCheck=1 ");
                tmpa.Add("CheckID=" + entity.CheckID + " ");
                tmpa.Add("CheckName='" + entity.CheckName + "'");
                tmpa.Add("CheckTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("CheckMemo='" + entity.CheckMemo + "'");
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            else if (entity.Status.ToString() == ReaBmsOutDocStatus.审批通过.Key)
            {
                //申请+:申请+操作时,出库单为"已申请"或"审核退回"状态时,该出库单自动进行审批通过;
                if (serverEntity.Status.ToString() != ReaBmsOutDocStatus.已申请.Key && serverEntity.Status.ToString() != ReaBmsOutDocStatus.审核退回.Key && serverEntity.Status.ToString() != ReaBmsOutDocStatus.审核通过.Key)
                {
                    return false;
                }
                //出库申请单为申请+操作时
                if (!serverEntity.CheckID.HasValue)
                {
                    entity.IsHasCheck = true;
                    entity.CheckID = empID;
                    entity.CheckName = empName;
                    entity.CheckTime = DateTime.Now;
                    tmpa.Add("IsHasCheck=1 ");
                    tmpa.Add("CheckID=" + entity.CheckID + " ");
                    tmpa.Add("CheckName='" + entity.CheckName + "'");
                    tmpa.Add("CheckTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    tmpa.Add("CheckMemo='" + entity.CheckMemo + "'");
                }
                entity.IsHasApproval = true;
                entity.ApprovalId = empID;
                entity.ApprovalCName = empName;
                entity.ApprovalTime = DateTime.Now;
                tmpa.Add("IsHasApproval=1 ");
                tmpa.Add("ApprovalId=" + entity.ApprovalId + " ");
                tmpa.Add("ApprovalCName='" + entity.ApprovalCName + "'");
                tmpa.Add("ApprovalTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ApprovalMemo='" + entity.ApprovalMemo + "'");
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            else if (entity.Status.ToString() == ReaBmsOutDocStatus.审批退回.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOutDocStatus.审核通过.Key)
                {
                    return false;
                }
                entity.IsHasApproval = true;
                entity.ApprovalId = empID;
                entity.ApprovalCName = empName;
                entity.ApprovalTime = DateTime.Now;
                tmpa.Add("IsHasApproval=1 ");
                tmpa.Add("ApprovalId=" + entity.ApprovalId + " ");
                tmpa.Add("ApprovalCName='" + entity.ApprovalCName + "'");
                tmpa.Add("ApprovalTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ApprovalMemo='" + entity.ApprovalMemo + "'");
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            else if (entity.Status.ToString() == ReaBmsOutDocStatus.出库完成.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOutDocStatus.审批通过.Key)
                {
                    return false;
                }
                if (!entity.ConfirmId.HasValue)
                {
                    entity.ConfirmId = empID;
                    entity.ConfirmName = empName;
                }
                entity.ConfirmTime = DateTime.Now;
                tmpa.Add("ConfirmId=" + entity.ConfirmId + " ");
                tmpa.Add("ConfirmName='" + entity.ConfirmName + "'");
                tmpa.Add("ConfirmTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            return true;
        }
        private BaseResultDataValue AddValidDocAndDtl(IList<ReaBmsOutDtl> outDtlList, bool isEmpOut, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<ReaUserStorageLink> userStorageLinkList = null;
            if (isEmpOut)
            {
                userStorageLinkList = IBReaUserStorageLink.SearchListByHQL(" reauserstoragelink.OperID=" + empID + " and reauserstoragelink.OperType=" + ReaUserStorageLinkOperType.库房管理权限.Key);
                if (userStorageLinkList == null || userStorageLinkList.Count == 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "当前用户没有相应的出库权限！";
                    return brdv;
                }
            }
            Dictionary<string, bool> dicRight = new Dictionary<string, bool>();
            foreach (var outDtl in outDtlList)
            {
                if (isEmpOut && brdv.success)
                    AddOutDtlIsEmpOut(userStorageLinkList, outDtl, empName, ref dicRight, ref brdv);
                if (brdv.success)
                {
                    BaseResultBool brb = IDReaBmsCheckDocDao.EditValidIsLock(outDtl.ReaCompanyID, outDtl.CompanyName, outDtl.StorageID, outDtl.StorageName, outDtl.PlaceID, outDtl.PlaceName, outDtl.GoodsID);
                    brdv.success = brb.success;
                    brdv.ErrorInfo = brb.ErrorInfo;
                }
                if (!brdv.success)
                    return brdv;
            }
            return brdv;
        }
        private BaseResultDataValue AddValidNeedPerformanceTest(IList<ReaBmsOutDtl> outDtlList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;
            foreach (var outDtl in outDtlList)
            {
                if (string.IsNullOrEmpty(outDtl.ReaGoodsNo) || string.IsNullOrEmpty(outDtl.ReaGoodsNo))
                    continue;

                IList<ReaGoodsLot> tempList = IDReaGoodsLotDao.GetListByHQL(string.Format(" reagoodslot.Visible=1 and reagoodslot.ReaGoodsNo='{0}' reagoodslot.LotNo='{1}'", outDtl.ReaGoodsNo, outDtl.LotNo)).OrderByDescending(p => p.DataAddTime).ToList();
                if (tempList.Count > 0 && !tempList[0].IsNeedPerformanceTest.HasValue || tempList[0].IsNeedPerformanceTest.Value == true)
                {
                    if (!tempList[0].VerificationStatus.HasValue || tempList[0].VerificationStatus.Value == long.Parse(ReaGoodsLotVerificationStatus.未验证.Key))
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = string.Format("库存货品为:{0},货品批号为:{1},需要完成货品批号性能验证后才能使用出库!", outDtl.GoodsCName, outDtl.LotNo);
                    }
                }
                if (!brdv.success)
                    return brdv;
            }
            return brdv;
        }
        /// <summary>
        /// 添加出库操作记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        public void AddSCOperation(ReaBmsOutDoc entity, long empID, string empName)
        {
            if (entity.Status.ToString() == ReaBmsOutDocStatus.暂存.Key) return;

            SCOperation sco = new SCOperation();
            sco.LabID = entity.LabID;
            sco.BobjectID = entity.Id;
            sco.CreatorID = empID;
            if (empName != null && empName.Trim() != "")
                sco.CreatorName = empName;
            sco.BusinessModuleCode = "ReaBmsOutDoc";
            if (!string.IsNullOrEmpty(entity.CheckMemo))
                sco.Memo = entity.CheckMemo;
            sco.DataUpdateTime = DateTime.Now;
            sco.IsUse = true;
            sco.Type = entity.Status;
            sco.TypeName = ReaBmsOutDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }
        /// <summary>
        /// 获取出库总单号
        /// </summary>
        /// <returns></returns>
        private string GetOutDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        #endregion

        #region PDF清单及统计
        public Stream SearchPdfReportOfTypeById(string reaReportClass, long id, long labID, string labCName, string breportType, string frx, ref string pdfFileName)
        {
            Stream stream = null;
            ReaBmsOutDoc outDoc = this.Get(id);
            if (outDoc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取出库单PDF清单数据信息为空!");
            }
            IList<ReaBmsOutDtl> dtlList = IBReaBmsOutDtl.SearchListByHQL("reabmsoutdtl.OutDocID=" + id);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取出库单PDF清单明细信息为空!");
            }
            SearchReaGoodsOfDtl(dtlList);

            pdfFileName = outDoc.OutDocNo + ".pdf";
            //string milliseconds = "";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = SearchPdfReportOfFrxById(outDoc, dtlList, frx);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                string excelFileFullDir = "";
                //获取出库单模板
                if (string.IsNullOrEmpty(frx))
                    frx = "出库清单.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = outDoc.OutDocNo.ToString() + fileExt;
                Stream stream2 = null;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
                stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsOutDoc, ReaBmsOutDtl>(outDoc, dtlList, excelCommand, breportType, outDoc.LabID, frx, excelFile, ref excelFileFullDir);
                stream2.Close();
                string pdfFullDir = "";

                bool result = ExcelToPdfReportHelp.ExcelToPDF(excelFileFullDir, breportType, outDoc.LabID, pdfFileName, ref pdfFullDir);
                if (result)
                {
                    stream = PdfReportHelp.GetReportPDF(pdfFullDir);
                }
            }

            return stream;
        }
        private Stream SearchPdfReportOfFrxById(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> dtlList, string frx)
        {
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";

            List<ReaBmsOutDoc> docList = new List<ReaBmsOutDoc>();
            docList.Add(outDoc);
            DataTable docDt = ReportBTemplateHelp.ToDataTable<ReaBmsOutDoc>(docList, null);
            docDt.TableName = "Rea_BmsOutDoc";
            dataSet.Tables.Add(docDt);

            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsOutDtl>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "Rea_BmsOutDtl";
                dataSet.Tables.Add(dtDtl);
            }
            //获取出库单Frx模板
            //string parentPath = ReportBTemplateHelp.GetSaveBTemplatePath(this.Entity.LabID, "出库清单");
            string pdfName = outDoc.OutDocNo.ToString() + ".pdf";
            //如果当前实验室还没有维护出库单报表模板,默认使用公共的出库单模板
            if (string.IsNullOrEmpty(frx))
                frx = "出库清单.frx";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, outDoc.LabID, pdfName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.出库清单.Key].Name, frx, false);

            return stream;
        }
        public Stream SearchExcelReportOfExcelById(long id, long labID, string labCName, string breportType, string frx, ref string fileName)
        {
            Stream stream = null;
            ReaBmsOutDoc orderDoc = this.Get(id);
            if (orderDoc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取出库单数据信息为空!");
            }
            IList<ReaBmsOutDtl> dtlList = IBReaBmsOutDtl.SearchListByHQL("reabmsoutdtl.OutDocID=" + id);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取出库单明细信息为空!");
            }
            SearchReaGoodsOfDtl(dtlList);
            //获取出库单模板
            if (string.IsNullOrEmpty(frx))
                frx = "出库清单.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = orderDoc.OutDocNo.ToString() + fileExt;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsOutDoc, ReaBmsOutDtl>(orderDoc, dtlList, excelCommand, breportType, orderDoc.LabID, frx, excelFile, ref saveFullPath);
            fileName = orderDoc.DeptName + "出库清单信息" + fileExt;
            return stream;
        }
        /// <summary>
        /// 添加出库明细货品的基本信息
        /// </summary>
        /// <param name="dtlList"></param>
        private void SearchReaGoodsOfDtl(IList<ReaBmsOutDtl> dtlList)
        {
            for (int i = 0; i < dtlList.Count; i++)
            {
                //批号是否改变赋值
                if (!string.IsNullOrWhiteSpace(dtlList[i].LastLotNo) && ("^" + dtlList[i].LastLotNo + "^").IndexOf("^" + dtlList[i].LotNo + "^") >= 0)
                {
                    dtlList[i].LotNoIsChange = "否";
                }
                else
                {
                    dtlList[i].LotNoIsChange = "是";
                }
                //货运单是否改变赋值
                if (!string.IsNullOrWhiteSpace(dtlList[i].LastTransportNo) && ("^" + dtlList[i].LastTransportNo + "^").IndexOf("^" + dtlList[i].TransportNo + "^") >= 0)
                {
                    dtlList[i].TransportNoIsChange = "否";
                }
                else
                {
                    dtlList[i].TransportNoIsChange = "是";
                }

                ReaGoods reaGoods = IDReaGoodsDao.Get(dtlList[i].GoodsID.Value);

                dtlList[i].TestCount = reaGoods.TestCount;
                dtlList[i].RegistNo = reaGoods.RegistNo;
                dtlList[i].ProdOrgName = reaGoods.ProdOrgName;
                dtlList[i].GoodsClass = reaGoods.GoodsClass;
                dtlList[i].GoodsClassType = reaGoods.GoodsClassType;
                dtlList[i].NetGoodsNo = reaGoods.NetGoodsNo;

                dtlList[i].StorageType = reaGoods.StorageType;
                dtlList[i].Purpose = reaGoods.Purpose;
                dtlList[i].EName = reaGoods.EName;
                dtlList[i].SName = reaGoods.SName;
                if (reaGoods.TestCount > 0)
                    dtlList[i].TotalAveragePrice = dtlList[i].Price / reaGoods.TestCount;
            }
        }
        #endregion

        #region 出库单上传至智方试剂平台--不在同一数据库--客户端部分
        public BaseResultDataValue GetUploadPlatformOutDocAndDtl(long outDocId, string reaServerLabcCode, ref JObject jPostData, ref ReaBmsOutDoc outDoc)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            jPostData = new JObject();
            outDoc = this.Get(outDocId);
            //出库单是否已经上传判断
            if (outDoc.IOFlag == int.Parse(ReaBmsOutDocIOFlag.已上传.Key))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "出库单号为:" + outDoc.OutDocNo + ",已经上传!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }

            ReaBmsOutDoc outDoc2 = ClassMapperHelp.GetMapper<ReaBmsOutDoc, ReaBmsOutDoc>(outDoc);
            outDoc2.LabOutDocID = outDoc2.Id;
            IList<ReaBmsOutDtl> outDtlList = IBReaBmsOutDtl.SearchListByHQL("OutDocID=" + outDocId);

            outDoc2.DataTimeStamp = null;
            if (string.IsNullOrEmpty(outDoc2.ReaServerLabcCode))
            {
                outDoc2.ReaServerLabcCode = reaServerLabcCode;
            }
            string outDocStr = JsonHelper.ObjectToJson(outDoc2);
            ZhiFang.Common.Log.Log.Debug("outDocStr:" + outDocStr);
            //ReaBmsCenOrderDoc orderDoc3 = JsonHelper.JsonDeserializeObject<ReaBmsCenOrderDoc>(orderDocStr);

            JObject joutDoc = JsonHelper.GetPropertyInfo<ReaBmsOutDoc>(outDoc2);
            jPostData.Add(ZFPlatformHelp.出库总单.Key, joutDoc);

            ////出库明细货品平台编码+供应商货品编码验证
            JArray jdtlList = new JArray();
            foreach (ReaBmsOutDtl outDtl in outDtlList)
            {
                //判断是否存在供货商货品编码
                if (string.IsNullOrEmpty(outDtl.CenOrgGoodsNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "出库明细的货品为:" + outDtl.GoodsCName + ",供货商货品编码为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                ReaBmsOutDtl outDtl2 = ClassMapperHelp.GetMapper<ReaBmsOutDtl, ReaBmsOutDtl>(outDtl);
                outDtl2.DataTimeStamp = null;
                //判断是否存在平台货品编码
                if (string.IsNullOrEmpty(outDtl2.GoodsNo))
                {
                    outDtl2.GoodsNo = outDtl2.CenOrgGoodsNo;
                }
                JObject joutDtl = JsonHelper.GetPropertyInfo<ReaBmsOutDtl>(outDtl2);
                jdtlList.Add(joutDtl);
            }
            jPostData.Add(ZFPlatformHelp.出库明细单.Key, jdtlList);

            return baseresultdata;
        }

        #endregion

        #region 出库单上传至智方试剂平台--不在同一数据库--平台部分
        public BaseResultDataValue SaveClientOutDocAndDtl(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> outDtlList, long empID, string empName)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            
            outDoc.IOFlag = int.Parse(ReaBmsOutDocIOFlag.已上传.Key);
            outDoc.Status = int.Parse(ReaBmsOutDocStatus.出库单上传平台.Key);
            outDoc.StatusName = ReaBmsOutDocStatus.GetStatusDic()[outDoc.Status.ToString()].Name;

            string outDocNo = outDoc.OutDocNo;
            //客户端平台编码
            string reaServerLabcCode = outDoc.ReaServerLabcCode;

            //出库单是否已经上传过
            IList<ReaBmsOutDoc> tempDocList = this.SearchListByHQL("(reabmsoutdoc.OutDocNo='" + outDocNo + "' and reabmsoutdoc.ReaServerLabcCode ='" + reaServerLabcCode + "')");
            if (tempDocList.Count > 0)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "出库单号为:" + outDocNo + ",客户端平台编码为:" + reaServerLabcCode + ",已上传过,请不要重复上传!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }

            //保存到平台库
            outDoc.DataAddTime = DateTime.Now;
            outDoc.DataUpdateTime = DateTime.Now;
            this.Entity = outDoc;
            baseresultdata.success= this.Add();
            if (baseresultdata.success)
            {
                foreach (var dtl in outDtlList)
                {
                    dtl.DataAddTime = DateTime.Now;
                    dtl.DataUpdateTime = DateTime.Now;
                    IBReaBmsOutDtl.Entity = dtl;
                    baseresultdata.success = IBReaBmsOutDtl.Add();
                    if (baseresultdata.success == false)
                    {
                        baseresultdata.ErrorInfo = "货品为:" + dtl.GoodsCName + ",保存失败!";
                        break;
                    }
                }
            }
          
            return baseresultdata;
        }
        #endregion

        /// <summary>
        /// 更新出库单同步到第三方系统标志，1为已经同步到第三方
        /// </summary>
        public BaseResultDataValue EditReaBmsOutDocThirdFlag(long id, int isThirdFlag)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ReaBmsOutDoc outDoc = this.Get(id);
            outDoc.IsThirdFlag = isThirdFlag;
            this.Entity = outDoc;
            this.Edit();
            return brdv;
        }

        /// <summary>
        /// 智方试剂平台使用
        /// 查询 状态=出库单上传平台 且 订货方类型=调拨 的出库单
        /// </summary>
        public EntityList<ReaBmsOutDoc> GetPlatformOutDocListByDBClient(string strHqlWhere, string sort, int page, int limit)
        {
            return ((IDReaBmsOutDocDao)base.DBDao).GetPlatformOutDocListByDBClient(strHqlWhere, sort, page, limit);
        }
    }
}