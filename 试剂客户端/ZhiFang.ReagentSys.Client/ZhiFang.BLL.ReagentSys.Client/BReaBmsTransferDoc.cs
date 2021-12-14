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
using System.IO;
using ZhiFang.ReagentSys.Client.Common;
using Newtonsoft.Json.Linq;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsTransferDoc : BaseBLL<ReaBmsTransferDoc>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsTransferDoc
    {
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IDReaBmsCheckDocDao IDReaBmsCheckDocDao { get; set; }
        IBReaBmsTransferDtl IBReaBmsTransferDtl { get; set; }
        IBReaBmsQtyDtl IBReaBmsQtyDtl { get; set; }
        IBReaBmsQtyDtlOperation IBReaBmsQtyDtlOperation { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        IBLL.RBAC.IBHRDept IBHRDept { get; set; }
        IBSCOperation IBSCOperation { get; set; }
        IBReaUserStorageLink IBReaUserStorageLink { get; set; }
        IDReaBmsInDtlDao IDReaBmsInDtlDao { get; set; }
        IDReaBmsInDocDao IDReaBmsInDocDao { get; set; }

        public EntityList<ReaBmsTransferDoc> SearchListByHQL(string where, string order, int page, int count, string isUseEmpOut, string type, long empId)
        {
            EntityList<ReaBmsTransferDoc> entityList = new EntityList<ReaBmsTransferDoc>();
            //是否按移库人权限移库 1:是;2:否;
            if (!string.IsNullOrEmpty(isUseEmpOut) && isUseEmpOut == "1")
            {
                StringBuilder idStr = new StringBuilder();
                string outHql = "";
                if (type == "1")
                {
                    //移库申请,只能查看登录人=申请人的移库信息
                    outHql = " reabmstransferdoc.CreaterID=" + empId;
                }
                else
                {
                    IList<long> storageIDList = IBReaUserStorageLink.SearchStorageIDListByHQL(" reauserstoragelink.OperType=" + ReaUserStorageLinkOperType.库房管理权限.Key + " and reauserstoragelink.OperID=" + empId, "", -1, -1);
                    if (storageIDList == null || storageIDList.Count <= 0)
                    {
                        ZhiFang.Common.Log.Log.Error("按移库人权限移库:获取当前员工ID为:" + empId + ",获取到的库房人员权限信息为空!不能获取移库信息");
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
                            case "1"://移库申请
                                break;
                            case "2"://直接移库管理
                                outHql = " reabmstransferdoc.SStorageID in (" + idStr.ToString().TrimEnd(',') + ") ";
                                break;
                            case "3"://移库管理(申请)
                                outHql = " reabmstransferdoc.SStorageID in (" + idStr.ToString().TrimEnd(',') + ") ";
                                break;
                            case "4"://移库管理(全部)
                                outHql = " reabmstransferdoc.SStorageID in (" + idStr.ToString().TrimEnd(',') + ") ";
                                break;
                            default:
                                break;
                        }
                    }
                }
                //兼容在2018-11-21号之前,移库总单没有源库房数据项的移库数据处理
                if (!string.IsNullOrEmpty(outHql))
                {
                    outHql = " (" + outHql + " or (reabmstransferdoc.SStorageID is null or reabmstransferdoc.SStorageID='')) ";
                }
                else
                {
                    outHql = " (reabmstransferdoc.SStorageID is null or reabmstransferdoc.SStorageID='') ";
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
        #region 直接移库完成
        /// <summary>
        /// 新增直接移库完成并修改移库对应的库存记录
        /// </summary>
        /// <param name="entity">移库总单</param>
        /// <param name="dtlAddList">移库明细单</param>
        /// <param name="isEmpTransfer">是否按移库人权限移库</param>
        /// <returns></returns>
        public BaseResultDataValue AddTransferDocAndDtlListOfComp(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtlAddList, bool isEmpTransfer, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "移库主单信息为空！";
                return brdv;
            }
            if (dtlAddList == null || dtlAddList.Count == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "移库明细信息不能为空！";
                return brdv;
            }
            brdv = AddValidDocAndDtl(dtlAddList, isEmpTransfer, empID, empName);
            if (brdv.success != true)
                return brdv;

            //移库保存前，对每个货品进行校验，有一个失败就不能做任何保存，直接返回失败信息。
            EditTransferDtlAndQtyDtl_SaveBeforeCheck(entity, dtlAddList, ref brdv);
            if (brdv.success != true)
                return brdv;

            entity.TotalPrice = dtlAddList.Sum(p => p.SumTotal);
            AddReaBmsTransferDoc(ref entity, empID, empName, ref brdv);
            if (brdv.success == true)
            {
                for (int j = 0; j < dtlAddList.Count; j++)
                {
                    if (brdv.success != true)
                        return brdv;

                    ReaBmsTransferDtl transferDtl = dtlAddList[j];
                    if (brdv.success == true)
                        EditTransferDtlAndQtyDtl(entity, ref transferDtl, ref brdv);
                    if (brdv.success != true)
                        return brdv;

                    transferDtl.SumTotal = transferDtl.GoodsQty * transferDtl.Price;
                    IBReaBmsTransferDtl.Entity = transferDtl;
                    brdv.success = IBReaBmsTransferDtl.Add();
                    if (brdv.success != true)
                    {
                        brdv.ErrorInfo = "货品为：" + transferDtl.GoodsCName + "移库失败,移库明细单保存失败！";
                        ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    }
                }
            }
            ZhiFang.Common.Log.Log.Debug("新增移库保存结束，移库申请单号为:" + entity.TransferDocNo);
            if (brdv.success == true) AddSCOperation(entity, empID, empName);
            return brdv;
        }
        /// <summary>
        /// 移库完成保存前移库明细验证
        /// </summary>
        /// <param name="transferDtlList"></param>
        /// <param name="isEmpTransfer"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        private BaseResultDataValue AddValidDocAndDtl(IList<ReaBmsTransferDtl> transferDtlList, bool isEmpTransfer, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (transferDtlList == null || transferDtlList.Count <= 0)
            {
                brdv.success = true;
                return brdv;
            }

            IList<ReaUserStorageLink> userStorageLinkList = null;
            if (isEmpTransfer)
            {
                userStorageLinkList = IBReaUserStorageLink.SearchListByHQL(" reauserstoragelink.OperID=" + empID + " and reauserstoragelink.OperType=" + ReaUserStorageLinkOperType.库房管理权限.Key);
                if (userStorageLinkList == null || userStorageLinkList.Count == 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "当前用户没有相应的移库权限！";
                    return brdv;
                }
            }
            //检查是否有移库权限及被盘库锁定
            Dictionary<string, bool> dicRight = new Dictionary<string, bool>();
            foreach (ReaBmsTransferDtl transferDtl in transferDtlList)
            {
                if (isEmpTransfer && brdv.success)
                    AddTransferDtlIsEmpTransfer(userStorageLinkList, transferDtl, empName, ref dicRight, ref brdv);
                if (brdv.success)
                {
                    BaseResultBool brb = IDReaBmsCheckDocDao.EditValidIsLock(transferDtl.ReaCompanyID, transferDtl.ReaCompanyName, transferDtl.SStorageID, transferDtl.SStorageName, transferDtl.SPlaceID, transferDtl.SStorageName, transferDtl.GoodsID);
                    brdv.success = brb.success;
                    brdv.ErrorInfo = brb.ErrorInfo;
                }
                if (!brdv.success)
                    return brdv;
            }
            return brdv;
        }
        /// <summary>
        /// 新增保存移库主单
        /// </summary>
        /// <param name="transferDoc"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="brdv"></param>
        private void AddReaBmsTransferDoc(ref ReaBmsTransferDoc transferDoc, long empID, string empName, ref BaseResultDataValue brdv)
        {
            if (string.IsNullOrEmpty(transferDoc.TransferDocNo))
                transferDoc.TransferDocNo = this.GetTransferDocNo();
            transferDoc.Visible = true;
            transferDoc.OperDate = DateTime.Now;
            transferDoc.DataAddTime = DateTime.Now;
            transferDoc.DataUpdateTime = DateTime.Now;
            if (!transferDoc.CreaterID.HasValue)
            {
                transferDoc.CreaterID = empID;
                transferDoc.CreaterName = empName;
            }
            if (transferDoc.Status <= 0)
                transferDoc.Status = int.Parse(ReaBmsTransferDocStatus.移库完成.Key);
            transferDoc.StatusName = ReaBmsTransferDocStatus.GetStatusDic()[transferDoc.Status.ToString()].Name;
            this.Entity = transferDoc;
            ZhiFang.Common.Log.Log.Debug("新增移库保存开始，移库申请单号为:" + transferDoc.TransferDocNo);
            brdv.success = this.Add();
            if (!brdv.success)
            {
                brdv.success = false;
                brdv.ErrorInfo = "新增移库保存失败！";
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
            }
        }
        /// <summary>
        /// 当前移库人是否有对某库房及货架的移库权限
        /// </summary>
        /// <param name="userStorageLinkList">当前出库人的移库权限集合</param>
        /// <param name="transferDtl">某一移库明细</param>
        /// <param name="dicRight">库房ID+货架ID临时集合</param>
        /// <param name="brdv"></param>
        private static void AddTransferDtlIsEmpTransfer(IList<ReaUserStorageLink> userStorageLinkList, ReaBmsTransferDtl transferDtl, string empName, ref Dictionary<string, bool> dicRight, ref BaseResultDataValue brdv)
        {
            if (!dicRight.Keys.Contains(transferDtl.SStorageID.ToString()))
            {
                IList<ReaUserStorageLink> listReaUserStorage = userStorageLinkList.Where(p => p.StorageID == transferDtl.SStorageID && p.OperType == long.Parse(ReaUserStorageLinkOperType.库房管理权限.Key)).ToList();
                dicRight.Add(transferDtl.SStorageID.ToString(), listReaUserStorage != null && listReaUserStorage.Count > 0);
            }
            if (dicRight.Keys.Contains(transferDtl.SStorageID.ToString()))
            {
                if (!(dicRight[transferDtl.SStorageID.ToString()]))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("货品移库失败：出库人【{0}】没有库房【{1}】移库权限！", empName, transferDtl.SStorageName);
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    throw new Exception(brdv.ErrorInfo);
                    //return;
                }
            }
        }
        /// <summary>
        /// 封装获取移库明细的库存货品查询条件
        /// </summary>
        /// <param name="transferDtl"></param>
        /// <returns></returns>
        private string GetQtyHql(ReaBmsTransferDtl transferDtl)
        {
            StringBuilder qtyHql = new StringBuilder();
            qtyHql.Append(" reabmsqtydtl.Visible=1 and reabmsqtydtl.GoodsQty>0");
            qtyHql.Append(" and reabmsqtydtl.BarCodeType=" + transferDtl.BarCodeType);
            if (transferDtl.SStorageID.HasValue)
            {
                qtyHql.Append(" and reabmsqtydtl.StorageID=" + transferDtl.SStorageID.Value);
            }
            if (transferDtl.SPlaceID.HasValue)
            {
                qtyHql.Append(" and reabmsqtydtl.PlaceID=" + transferDtl.SPlaceID.Value);
            }
            if (!string.IsNullOrEmpty(transferDtl.ReaGoodsNo))
            {
                qtyHql.Append(" and reabmsqtydtl.ReaGoodsNo='" + transferDtl.ReaGoodsNo + "'");
            }
            if (transferDtl.ReaCompanyID.HasValue)
            {
                qtyHql.Append(" and reabmsqtydtl.ReaCompanyID=" + transferDtl.ReaCompanyID.Value);
            }
            if (!string.IsNullOrEmpty(transferDtl.LotNo))
            {
                qtyHql.Append(" and reabmsqtydtl.LotNo='" + transferDtl.LotNo + "'");
            }
            return qtyHql.ToString();
        }
        /// <summary>
        /// 获取移库明细保存信息
        /// </summary>
        /// <param name="transferDoc"></param>
        /// <param name="reaGoods">移库明细的机构货品信息</param>
        /// <param name="listReaBmsQtyDtl">当前移库明细对应的库存记录</param>
        /// <param name="transferDtl">当前移库明细</param>
        private void EditGetReaBmsTransferDtl(ReaBmsTransferDoc transferDoc, ReaGoods reaGoods, IList<ReaBmsQtyDtl> listReaBmsQtyDtl, ref ReaBmsTransferDtl transferDtl)
        {
            transferDtl.TransferDocID = transferDoc.Id;
            transferDtl.Visible = true;
            transferDtl.DataAddTime = DateTime.Now;
            transferDtl.DataUpdateTime = DateTime.Now;
            if (transferDtl.SumTotal <= 0)
                transferDtl.SumTotal = transferDtl.GoodsQty * transferDtl.Price;
            if (transferDoc.CreaterID.HasValue)
            {
                transferDtl.CreaterID = transferDoc.CreaterID.Value;
                transferDtl.CreaterName = transferDoc.CreaterName;
            }
            if (listReaBmsQtyDtl.Count > 0)
            {
                if (string.IsNullOrEmpty(transferDtl.LotSerial))
                    transferDtl.LotSerial = listReaBmsQtyDtl[0].LotSerial;
                if (string.IsNullOrEmpty(transferDtl.LotQRCode))
                    transferDtl.LotQRCode = listReaBmsQtyDtl[0].LotQRCode;
                if (string.IsNullOrEmpty(transferDtl.SysLotSerial))
                    transferDtl.SysLotSerial = listReaBmsQtyDtl[0].SysLotSerial;
                if (!transferDtl.ProdDate.HasValue)
                    transferDtl.ProdDate = listReaBmsQtyDtl[0].ProdDate;
                if (string.IsNullOrEmpty(transferDtl.ReaCompCode))
                    transferDtl.ReaCompCode = listReaBmsQtyDtl[0].ReaCompCode;
                if (string.IsNullOrEmpty(transferDtl.ReaServerCompCode))
                    transferDtl.ReaServerCompCode = listReaBmsQtyDtl[0].ReaServerCompCode;
                if (!transferDtl.GoodsLotID.HasValue)
                    transferDtl.GoodsLotID = listReaBmsQtyDtl[0].GoodsLotID;
            }
        }

        /// <summary>
        /// 移库确认前的检查，对要移库的货品进行校验，有一个货品验证失败，直接返回失败，且不能保存
        /// </summary>
        /// <returns></returns>
        private void EditTransferDtlAndQtyDtl_SaveBeforeCheck(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtlAddList, ref BaseResultDataValue brdv)
        {
            for (int j = 0; j < dtlAddList.Count; j++)
            {
                ReaBmsTransferDtl transferDtl = dtlAddList[j];

                //获取当前移库明细对应的库存货品信息(库房ID+货架ID+货品ID+供应商ID+批号)
                IList<ReaBmsQtyDtl> listReaBmsQtyDtl = IBReaBmsQtyDtl.SearchListByHQL(GetQtyHql(transferDtl)).OrderBy(p => p.DataAddTime).ToList();
                if (listReaBmsQtyDtl == null || listReaBmsQtyDtl.Count <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("货品【{0}】移库失败：库存数量不足！", transferDtl.GoodsCName);
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    break;
                }
                //移库货品信息
                ReaGoods transferReaGoods = IDReaGoodsDao.Get(transferDtl.GoodsID.Value);
                EditGetReaBmsTransferDtl(entity, transferReaGoods, listReaBmsQtyDtl, ref transferDtl);
                //移库明细的移库总数(转换为最小包装单位)
                double outDtlGoodsQty = transferDtl.GoodsQty;
                double goodsPrice = transferDtl.Price;
                ZhiFang.Common.Log.Log.Info(string.Format("移库明细的移库总数(转换为最小包装单位).transferDtl.GoodsID:" + transferDtl.GoodsID + ",移库数量开始单位换算：原数量{0}，原价格{1}", outDtlGoodsQty, goodsPrice));
                BaseResultDataValue tempResult = SearchReaGoodsMinUnitCount(transferDtl.GoodsID, transferDtl.ReaCompanyID, ref outDtlGoodsQty, ref goodsPrice);
                ZhiFang.Common.Log.Log.Info(string.Format("移库明细的移库总数(转换为最小包装单位).transferDtl.GoodsID:" + transferDtl.GoodsID + ",移库数量结束单位换算：转换后数量{0}，转换后价格{1}", outDtlGoodsQty, goodsPrice));
                if (!tempResult.success)
                {
                    brdv = tempResult;
                    break;
                }
                #region 计算移库货品的当前总剩余库存数(转换为最小包装单位)
                var groupByQtyList = listReaBmsQtyDtl.GroupBy(p => p.GoodsID);
                double allOverageQty = 0;
                double qtygoodsPrice = 0;
                if (groupByQtyList != null && groupByQtyList.Count() > 0)
                {
                    foreach (var groupBy in groupByQtyList)
                    {
                        double curOverageQty2 = groupBy.Where(p => p.GoodsQty.HasValue == true).Sum(g => g.GoodsQty.Value);
                        ZhiFang.Common.Log.Log.Info(string.Format("QtyDtl.GoodsID:" + groupBy.Key + ",移库的库存货品总库存数开始单位换算：原数量{0}，原价格{1}", curOverageQty2, qtygoodsPrice));
                        tempResult = SearchReaGoodsMinUnitCount(groupBy.Key, transferDtl.ReaCompanyID, ref curOverageQty2, ref qtygoodsPrice);
                        ZhiFang.Common.Log.Log.Info(string.Format("QtyDtl.GoodsID:" + groupBy.Key + ",移库的库存货品总库存数结束单位换算：转换后数量{0}，转换后价格{1}", curOverageQty2, qtygoodsPrice));
                        if (!tempResult.success)
                        {
                            brdv = tempResult;
                            return;
                        }
                        allOverageQty = allOverageQty + curOverageQty2;
                    }
                }
                //剩余库存数保留两位小数点
                allOverageQty = ConvertQtyHelp.ConvertQty(allOverageQty, 2);
                //移库数量大于当前库存总数，不能移库
                if (outDtlGoodsQty > allOverageQty)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("货品【{0}】移库失败：本次移库数为:{1},剩余库存数为:{2},库存数量不足！", transferDtl.GoodsCName, outDtlGoodsQty, allOverageQty);
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    break;
                }
                #endregion
            }
        }

        /// <summary>
        /// 保存移库明细及移库库存货品的处理
        /// </summary>
        /// <param name="transferDoc"></param>
        /// <param name="transferDtl"></param>
        /// <param name="brdv"></param>
        private void EditTransferDtlAndQtyDtl(ReaBmsTransferDoc transferDoc, ref ReaBmsTransferDtl transferDtl, ref BaseResultDataValue brdv)
        {
            //获取当前移库明细对应的库存货品信息(库房ID+货架ID+货品ID+供应商ID+批号)
            IList<ReaBmsQtyDtl> listReaBmsQtyDtl = IBReaBmsQtyDtl.SearchListByHQL(GetQtyHql(transferDtl)).OrderBy(p => p.DataAddTime).ToList();
            if (listReaBmsQtyDtl == null || listReaBmsQtyDtl.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("货品【{0}】移库失败：库存数量不足！", transferDtl.GoodsCName);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                throw new Exception(brdv.ErrorInfo);
            }
            //移库货品信息
            ReaGoods transferReaGoods = IDReaGoodsDao.Get(transferDtl.GoodsID.Value);
            EditGetReaBmsTransferDtl(transferDoc, transferReaGoods, listReaBmsQtyDtl, ref transferDtl);
            //移库明细的移库总数(转换为最小包装单位)
            double outDtlGoodsQty = transferDtl.GoodsQty;
            double goodsPrice = transferDtl.Price;
            ZhiFang.Common.Log.Log.Info(string.Format("移库明细的移库总数(转换为最小包装单位).transferDtl.GoodsID:" + transferDtl.GoodsID + ",移库数量开始单位换算：原数量{0}，原价格{1}", outDtlGoodsQty, goodsPrice));
            BaseResultDataValue tempResult = SearchReaGoodsMinUnitCount(transferDtl.GoodsID, transferDtl.ReaCompanyID, ref outDtlGoodsQty, ref goodsPrice);
            ZhiFang.Common.Log.Log.Info(string.Format("移库明细的移库总数(转换为最小包装单位).transferDtl.GoodsID:" + transferDtl.GoodsID + ",移库数量结束单位换算：转换后数量{0}，转换后价格{1}", outDtlGoodsQty, goodsPrice));
            if (!tempResult.success)
            {
                brdv = tempResult;
                throw new Exception(brdv.ErrorInfo);
            }
            #region 计算移库货品的当前总剩余库存数(转换为最小包装单位)---在保存之前遍历所有货品一一验证，有一个货品验证失败就都不能保存，所以此处注释
            //var groupByQtyList = listReaBmsQtyDtl.GroupBy(p => p.GoodsID);
            //double allOverageQty = 0;
            //double qtygoodsPrice = 0;
            //if (groupByQtyList != null && groupByQtyList.Count() > 0)
            //{
            //    foreach (var groupBy in groupByQtyList)
            //    {
            //        double curOverageQty2 = groupBy.Where(p => p.GoodsQty.HasValue == true).Sum(g => g.GoodsQty.Value);
            //        ZhiFang.Common.Log.Log.Info(string.Format("QtyDtl.GoodsID:" + groupBy.Key + ",移库的库存货品总库存数开始单位换算：原数量{0}，原价格{1}", curOverageQty2, qtygoodsPrice));
            //        tempResult = SearchReaGoodsMinUnitCount(groupBy.Key, transferDtl.ReaCompanyID, ref curOverageQty2, ref qtygoodsPrice);
            //        ZhiFang.Common.Log.Log.Info(string.Format("QtyDtl.GoodsID:" + groupBy.Key + ",移库的库存货品总库存数结束单位换算：转换后数量{0}，转换后价格{1}", curOverageQty2, qtygoodsPrice));
            //        if (!tempResult.success)
            //        {
            //            brdv = tempResult;
            //            return;
            //        }
            //        allOverageQty = allOverageQty + curOverageQty2;
            //    }
            //}
            ////剩余库存数保留两位小数点
            //allOverageQty = ConvertQtyHelp.ConvertQty(allOverageQty, 2);
            ////移库数量大于当前库存总数，不能移库
            //if (outDtlGoodsQty > allOverageQty)
            //{
            //    brdv.success = false;
            //    brdv.ErrorInfo = string.Format("货品【{0}】移库失败：本次移库数为:{1},剩余库存数为:{2},库存数量不足！", transferDtl.GoodsCName, outDtlGoodsQty, allOverageQty);
            //    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
            //    return;
            //}
            #endregion
            //移库明细的扫码货品及对应库存货品优先移库
            if (transferDtl.ReaBmsTransferDtlLinkList != null && transferDtl.ReaBmsTransferDtlLinkList.Count > 0)
            {
                EditTransferDtlAndQtyDtlOfBarcode(transferDoc, transferReaGoods, ref transferDtl, ref listReaBmsQtyDtl, ref outDtlGoodsQty, ref brdv);
            }
            else
            {
                //,本次移库不存在移库扫码,按移库明细的(源库房+源货架+货品ID+包装单位+货品批号+供应商)取库存货品进行移库
                long? sStorageID = transferDtl.SStorageID;
                string goodsUnit = transferDtl.GoodsUnit;
                long? sPlaceID = transferDtl.SPlaceID;
                long? goodsID = transferDtl.GoodsID;
                string lotNo = transferDtl.LotNo;
                long? reaCompanyID = transferDtl.ReaCompanyID;
                var tempQtyList = listReaBmsQtyDtl.Where(p => (p.StorageID == sStorageID && p.PlaceID == sPlaceID && p.GoodsID == goodsID && p.GoodsUnit == goodsUnit && p.LotNo == lotNo && p.ReaCompanyID == reaCompanyID));
                if (tempQtyList != null && tempQtyList.Count() > 0)
                    EditTransferDtlAndQtyDtlOfAllOther(transferDoc, tempQtyList.ToList(), ref listReaBmsQtyDtl, ref transferDtl, ref outDtlGoodsQty, ref brdv);
                else
                    EditTransferDtlAndQtyDtlOfAllOther(transferDoc, listReaBmsQtyDtl, ref listReaBmsQtyDtl, ref transferDtl, ref outDtlGoodsQty, ref brdv);
            }
            //剩余的移库数处理
            if (outDtlGoodsQty > 0)
            {
                EditTransferDtlAndQtyDtlOfAllOther(transferDoc, listReaBmsQtyDtl, ref listReaBmsQtyDtl, ref transferDtl, ref outDtlGoodsQty, ref brdv);
            }
            else
            {
                listReaBmsQtyDtl.Clear();
            }
            //移库明细对应移库领用的库存记录ID及移库入库的库存记录ID处理
            if (!string.IsNullOrEmpty(transferDtl.SQtyDtlID))
                transferDtl.SQtyDtlID = transferDtl.SQtyDtlID.TrimEnd(',');
            if (!string.IsNullOrEmpty(transferDtl.QtyDtlID))
                transferDtl.QtyDtlID = transferDtl.QtyDtlID.TrimEnd(',');
            if (!transferDtl.ReqGoodsQty.HasValue || transferDtl.ReqGoodsQty.Value <= 0)
                transferDtl.ReqGoodsQty = transferDtl.GoodsQty;
        }
        public BaseResultDataValue SearchReaGoodsMinUnitCount(long? reaGoodsID, long? reaCompanyID, ref double goodsQry, ref double goodsPrice)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ReaGoods reaGoods = IDReaGoodsDao.Get((long)reaGoodsID);
            double gonvertQty = reaGoods.GonvertQty;
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
        /// 移库明细货品的扫码货品对应的库存优先移库
        /// </summary>
        /// <param name="transferDoc"></param>
        /// <param name="transferReaGoods">移库明细的机构货品信息</param>
        /// <param name="listReaBmsQtyDtl">移库明细对应的库存集合</param>
        /// <param name="transferDtl">移库明细</param>
        /// <param name="outDtlGoodsQty">移库明细的移库总数</param>
        /// <param name="brdv"></param>
        private void EditTransferDtlAndQtyDtlOfBarcode(ReaBmsTransferDoc transferDoc, ReaGoods transferReaGoods, ref ReaBmsTransferDtl transferDtl, ref IList<ReaBmsQtyDtl> listReaBmsQtyDtl, ref double outDtlGoodsQty, ref BaseResultDataValue brdv)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            long empID = string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)) ? 0 : long.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            string empName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            //当前移库扫码条码(扫码条码为小包装条码时)对应的大包装单位条码集合
            IList<ReaGoodsBarcodeOperation> addMaxBarCodeList = new List<ReaGoodsBarcodeOperation>();
            addMaxBarCodeList = AddGetMaxBarCodeList(transferDoc, transferReaGoods, ref transferDtl, transferDtl.ReaBmsTransferDtlLinkList);

            //当前移库扫码(扫码条码为大包装条码时)对应的小包装单位条码集合
            IList<ReaGoodsBarcodeOperation> minBarCodeList = new List<ReaGoodsBarcodeOperation>();
            double transferGonvertQty = transferReaGoods.GonvertQty;
            if (transferGonvertQty <= 0)
                transferGonvertQty = 1;
            #region foreach ReaBmsTransferDtlLinkList
            foreach (ReaGoodsBarcodeOperation outBarcode in transferDtl.ReaBmsTransferDtlLinkList)
            {
                //当前移库扫码对应的大包装单位条码记录
                ReaGoodsBarcodeOperation pOutBarcode = null;
                minBarCodeList.Clear();
                //移库扫码货品的存储库房ID
                string storageId = "";
                if (outBarcode.StorageID.HasValue)
                    storageId = outBarcode.StorageID.Value.ToString();
                string placeId = "";
                if (outBarcode.PlaceID.HasValue)
                    placeId = outBarcode.PlaceID.Value.ToString();

                //本次移库扫码次数
                double scanCodeQty = 0;
                if (outBarcode.ScanCodeQty.HasValue)
                    scanCodeQty = outBarcode.ScanCodeQty.Value;
                ZhiFang.Common.Log.Log.Info(string.Format("移库扫码一维条码为:{0}，二维条码为:{1}，本次移库扫码次数为:{2}", outBarcode.UsePackSerial, outBarcode.UsePackQRCode, scanCodeQty));
                //移库扫码次数不能大于移库条码的最小包装单位扫码次数
                if (outBarcode.MinBarCodeQty.HasValue && outBarcode.MinBarCodeQty.Value > 0 && scanCodeQty > outBarcode.MinBarCodeQty.Value)
                {
                    ZhiFang.Common.Log.Log.Info(string.Format("移库扫码的一维条码为:{0}，二维条码为:{1}，最小包装单位扫码次数为:{2},本次移库扫码次数最大值只能为:{3}", outBarcode.UsePackSerial, outBarcode.UsePackQRCode, outBarcode.MinBarCodeQty.Value, outBarcode.MinBarCodeQty.Value));
                    scanCodeQty = outBarcode.MinBarCodeQty.Value;
                }
                //条码的当次扫码次数不能大于条码货品对应的转换系数

                if (scanCodeQty <= 0)
                    continue;

                //本次移库扫码的移库领用/移库入库的库存数(最小包装单位)
                double scanCodeQty2 = scanCodeQty * transferGonvertQty;
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
                    if (!transferDtl.GoodsLotID.HasValue)
                        transferDtl.GoodsLotID = sQtyDtl.GoodsLotID;
                    if (!outBarcode.GoodsLotID.HasValue)
                        outBarcode.GoodsLotID = sQtyDtl.GoodsLotID;
                    int qtyIndexOf = listReaBmsQtyDtl.IndexOf(sQtyDtl);
                    ReaGoods qtyReaGoods = IDReaGoodsDao.Get(sQtyDtl.GoodsID.Value);
                    //当前扫码条码剩余的总库存数(转换为小包装单位的库存数)
                    double qtyCurOverage = (double)(sQtyDtl.GoodsQty != null ? sQtyDtl.GoodsQty : 0);
                    double qtygoodsPrice2 = (double)(sQtyDtl.Price != null ? sQtyDtl.Price : 0);
                    ZhiFang.Common.Log.Log.Info(string.Format("移库库存货品开始单位换算：原数量{0}，原价格{1}", qtyCurOverage, qtygoodsPrice2));
                    brdv = SearchReaGoodsMinUnitCount(sQtyDtl.GoodsID, sQtyDtl.ReaCompanyID, ref qtyCurOverage, ref qtygoodsPrice2);
                    ZhiFang.Common.Log.Log.Info(string.Format("移库库存货品结束单位换算：转换后数量{0}，转换后价格{1}", qtyCurOverage, qtygoodsPrice2));
                    //获取当前移库扫码的库存货品剩余库存数
                    string allBarCodeHql = string.Format(" (reagoodsbarcodeoperation.OverageQty>0 or reagoodsbarcodeoperation.OverageQty is null) and (reagoodsbarcodeoperation.UsePackSerial='{0}' or reagoodsbarcodeoperation.UsePackQRCode='{1}')", outBarcode.UsePackSerial, outBarcode.UsePackSerial);
                    if (!string.IsNullOrEmpty(storageId))
                        allBarCodeHql = allBarCodeHql + " and reagoodsbarcodeoperation.StorageID=" + storageId;
                    if (!string.IsNullOrEmpty(placeId))
                        allBarCodeHql = allBarCodeHql + " and reagoodsbarcodeoperation.PlaceID=" + placeId;
                    IList<ReaGoodsBarcodeOperation> allBarcodeList = IBReaGoodsBarcodeOperation.GetListByHQL(allBarCodeHql);
                    double barcodeCurOverage = IBReaGoodsBarcodeOperation.SearchOverageQty(outBarcode.UsePackSerial, allBarcodeList);
                    ZhiFang.Common.Log.Log.Info(string.Format("移库扫码一维条码为:{0}，二维条码为:{1}，当前移库扫码库存货品剩余库存数:{2}", outBarcode.UsePackSerial, outBarcode.UsePackQRCode, barcodeCurOverage));
                    if (barcodeCurOverage > qtyCurOverage)
                    {
                        ZhiFang.Common.Log.Log.Info(string.Format("移库扫码一维条码为:{0}，二维条码为:{1}，当前移库扫码库存货品剩余库存数:{2},大于库存记录的剩余库存数:{3}", outBarcode.UsePackSerial, outBarcode.UsePackQRCode, barcodeCurOverage, qtyCurOverage));
                        barcodeCurOverage = qtyCurOverage;
                    }
                    //剩余库存数保留两位小数点
                    barcodeCurOverage = ConvertQtyHelp.ConvertQty(barcodeCurOverage, 2);

                    if (barcodeCurOverage <= 0)
                    {
                        barcodeCurOverage = 0;
                        outBarcode.OverageQty = 0;
                        continue;
                    }
                    if (scanCodeQty2 > barcodeCurOverage)
                    {
                        scanCodeQty2 = barcodeCurOverage;
                    }
                    #region 当前移库扫码对应的大包装条码的剩余扫码数处理
                    if (!string.IsNullOrEmpty(outBarcode.PUsePackSerial) && addMaxBarCodeList.Count > 0)
                    {
                        for (int i = 0; i < addMaxBarCodeList.Count; i++)
                        {
                            if (addMaxBarCodeList[i].PUsePackSerial == outBarcode.PUsePackSerial)
                            {
                                addMaxBarCodeList[i].ScanCodeQty = addMaxBarCodeList[i].ScanCodeQty + scanCodeQty;
                                addMaxBarCodeList[i].OverageQty = addMaxBarCodeList[i].OverageQty - scanCodeQty2;
                                if (addMaxBarCodeList[i].OverageQty < 0)
                                    addMaxBarCodeList[i].OverageQty = 0;
                                pOutBarcode = addMaxBarCodeList[i];
                                //如果当前扫码条码为大包装条码,需要在addMaxBarCodeList里移除相应的条码信息
                                if (addMaxBarCodeList[i].UsePackSerial == outBarcode.UsePackSerial || addMaxBarCodeList[i].UsePackQRCode == outBarcode.UsePackQRCode)
                                {
                                    addMaxBarCodeList.RemoveAt(i);
                                    ZhiFang.Common.Log.Log.Error("当前移库扫码条码为大包装条码.UsePackSerial:" + outBarcode.UsePackSerial + ",UsePackQRCode:" + outBarcode.UsePackQRCode);
                                }
                                ZhiFang.Common.Log.Log.Error("移库扫码条码(对应的大包装单位条码).PUsePackSerial:" + outBarcode.PUsePackSerial + ",本次移库扫码次数:" + scanCodeQty2);
                            }
                        }
                    }
                    #endregion
                    //扫码条码剩余扫码次数(最小包装单位)=当前移库扫码库存货品剩余库存数-当前条码扫码条码数
                    double overageQty1 = barcodeCurOverage - scanCodeQty2;
                    #region 当前条码对应的小包装单位条码集合(不包含当前条码本身)
                    if (overageQty1 <= 0 && !string.IsNullOrEmpty(outBarcode.PUsePackSerial))
                    {
                        string minBarCodeHql = " (reagoodsbarcodeoperation.OverageQty>0 or reagoodsbarcodeoperation.OverageQty is null) and reagoodsbarcodeoperation.GoodsUnit!='" + outBarcode.GoodsUnit + "' and reagoodsbarcodeoperation.Id!='" + outBarcode.PUsePackSerial + "' and reagoodsbarcodeoperation.PUsePackSerial='" + outBarcode.PUsePackSerial + "' and (reagoodsbarcodeoperation.UsePackSerial!='" + outBarcode.UsePackSerial + "' and reagoodsbarcodeoperation.UsePackQRCode!='" + outBarcode.UsePackQRCode + "')";
                        if (!string.IsNullOrEmpty(storageId))
                            minBarCodeHql = minBarCodeHql + " and reagoodsbarcodeoperation.StorageID=" + storageId;
                        if (!string.IsNullOrEmpty(placeId))
                            minBarCodeHql = minBarCodeHql + " and reagoodsbarcodeoperation.PlaceID=" + placeId;
                        //排除当前条码的父条码
                        if (pOutBarcode != null)
                        {
                            minBarCodeHql = minBarCodeHql + " and (reagoodsbarcodeoperation.UsePackSerial!='" + pOutBarcode.UsePackSerial + "' and reagoodsbarcodeoperation.UsePackQRCode!='" + pOutBarcode.UsePackQRCode + "')";
                        }
                        minBarCodeList = IBReaGoodsBarcodeOperation.GetListByHQL(minBarCodeHql);
                        ZhiFang.Common.Log.Log.Debug("移库扫码.minBarCodeHql:" + minBarCodeHql + ",对应的小包装单位条码集合:" + minBarCodeList.Count());
                    }
                    #endregion
                    #region 移库领用的条码处理
                    double qtyGonvertQty = qtyReaGoods.GonvertQty;
                    if (qtyGonvertQty <= 0)
                        qtyGonvertQty = 1;
                    //库存货品剩余库存数=当前扫码条码剩余总库存数-当前条码扫码条码数
                    double curOverageQty2 = qtyCurOverage - scanCodeQty2;
                    sQtyDtl.GoodsQty = curOverageQty2;
                    sQtyDtl.SumTotal = sQtyDtl.GoodsQty * sQtyDtl.Price;
                    IBReaBmsQtyDtl.Entity = sQtyDtl;
                    if (!IBReaBmsQtyDtl.Edit())
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "货品移库失败：库存更新失败！";
                        ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                        throw new Exception(brdv.ErrorInfo);
                        //return;
                    }
                    //更新sQtyDtl在listReaBmsQtyDtl的库存数
                    listReaBmsQtyDtl[qtyIndexOf].GoodsQty = sQtyDtl.GoodsQty;
                    listReaBmsQtyDtl[qtyIndexOf].SumTotal = listReaBmsQtyDtl[qtyIndexOf].GoodsQty * listReaBmsQtyDtl[qtyIndexOf].Price;
                    //移库领用的库存变化记录数
                    double curTransferQty2 = scanCodeQty2;
                    if (qtyGonvertQty > 1)
                    {
                        curTransferQty2 = scanCodeQty2 / qtyGonvertQty;
                    }
                    IBReaBmsQtyDtlOperation.AddReaBmsQtyDtlTransferOperation(transferDoc, transferDtl, IBReaBmsQtyDtl.Entity, curTransferQty2, long.Parse(ReaBmsQtyDtlOperationOperType.移库出库.Key));

                    tempBaseResultBool = AddBarcodeOperationOfTransferOut(transferDtl, sQtyDtl, outBarcode, minBarCodeList, transferReaGoods, overageQty1, long.Parse(ReaGoodsBarcodeOperType.移库出库.Key), empID, empName);
                    #endregion
                    #region 移库入库的条码处理
                    //移库入库的库存信息
                    ReaBmsQtyDtl addQtyDtl = new ReaBmsQtyDtl();
                    double addGoodsQty = scanCodeQty2;
                    if (addGoodsQty > 0)
                    {
                        if (qtyGonvertQty > 0)
                        {
                            addGoodsQty = addGoodsQty / qtyGonvertQty;
                            ZhiFang.Common.Log.Log.Info(string.Format("移库入库的库存货品的库存数:{0},单位换算系数为：{1},转换后数量:{2}", scanCodeQty2, qtyGonvertQty, addGoodsQty));
                        }
                    }
                    addQtyDtl = EditCopyReaBmsQtyDtl(sQtyDtl, addQtyDtl, transferDtl);
                    addQtyDtl.GoodsQty = addGoodsQty;
                    addQtyDtl.SumTotal = addQtyDtl.GoodsQty * addQtyDtl.Price;
                    IBReaBmsQtyDtl.Entity = addQtyDtl;
                    if (!IBReaBmsQtyDtl.Add())
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "货品移库失败：库存新增失败！";
                        ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                        throw new Exception(brdv.ErrorInfo);
                    }
                    IBReaBmsQtyDtlOperation.AddReaBmsQtyDtlTransferOperation(transferDoc, transferDtl, IBReaBmsQtyDtl.Entity, addQtyDtl.GoodsQty.Value, long.Parse(ReaBmsQtyDtlOperationOperType.移库入库.Key));
                    tempBaseResultBool = AddBarcodeOperationOfTransferIn(transferDoc, transferDtl, addQtyDtl, outBarcode, minBarCodeList, transferReaGoods, long.Parse(ReaGoodsBarcodeOperType.移库入库.Key), empID, empName);
                    //移库明细的剩余移库数=移库明细移库总数-移库入库数
                    outDtlGoodsQty = outDtlGoodsQty - addGoodsQty;
                    #endregion

                    if (string.IsNullOrEmpty(transferDtl.QtyDtlID))
                    {
                        transferDtl.QtyDtlID = addQtyDtl.Id + ",";
                    }
                    else if (!transferDtl.QtyDtlID.Contains(addQtyDtl.Id.ToString()))
                    {
                        transferDtl.QtyDtlID = transferDtl.QtyDtlID.TrimEnd(',') + "," + addQtyDtl.Id + ",";
                    }
                    if (string.IsNullOrEmpty(transferDtl.SQtyDtlID))
                    {
                        transferDtl.SQtyDtlID = sQtyDtl.Id + ",";
                    }
                    else if (!transferDtl.SQtyDtlID.Contains(sQtyDtl.Id.ToString()))
                    {
                        transferDtl.SQtyDtlID = transferDtl.SQtyDtlID.TrimEnd(',') + "," + sQtyDtl.Id + ",";
                    }
                }
            }
            #endregion foreach end
            #region 移库扫码对应的大包装扫码作移库领用登记处理
            if (addMaxBarCodeList.Count > 0)
            {
                try
                {
                    foreach (var maxBarCode in addMaxBarCodeList)
                    {
                        ZhiFang.Common.Log.Log.Error("移库扫码条码(对应的大包装单位条码).PUsePackSerial:" + maxBarCode.PUsePackSerial + ",OverageQty:" + maxBarCode.OverageQty);
                        maxBarCode.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                        maxBarCode.Memo = "非直接移库扫码;小包装货品条码移库扫码,更新对应大包装货品条码的可扫码次数!";
                        IBReaGoodsBarcodeOperation.Entity = maxBarCode;
                        IBReaGoodsBarcodeOperation.Add();
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("货品【{0}】货品出库失败：移库扫码条码(对应的大包装单位条码)操作记录登记错误:！", transferDtl.GoodsCName);
                    ZhiFang.Common.Log.Log.Error("移库扫码条码(对应的大包装单位条码)操作记录登记错误:" + ex.StackTrace);
                    throw ex;
                }
            }
            #endregion
        }
        /// <summary>
        /// 移库明细剩余未扫码的移库货品处理
        /// </summary>
        /// <param name="transferDoc"></param>
        /// <param name="curListReaBmsQtyDtl">移库明细对应的库存集合</param>
        /// <param name="transferDtl">移库明细</param>
        /// <param name="outDtlGoodsQty">某一移库明细的移库总数</param>
        /// <param name="brdv"></param>
        private void EditTransferDtlAndQtyDtlOfAllOther(ReaBmsTransferDoc transferDoc, IList<ReaBmsQtyDtl> curListReaBmsQtyDtl, ref IList<ReaBmsQtyDtl> listReaBmsQtyDtl, ref ReaBmsTransferDtl transferDtl, ref double outDtlGoodsQty, ref BaseResultDataValue brdv)
        {
            for (int i = 0; i < curListReaBmsQtyDtl.Count; i++)
            {
                if (outDtlGoodsQty <= 0)
                {
                    break;
                }
                if (curListReaBmsQtyDtl[i].GoodsQty <= 0) continue;

                ReaBmsQtyDtl sQtyDtl = curListReaBmsQtyDtl[i];
                int qtyIndexOf = listReaBmsQtyDtl.IndexOf(sQtyDtl);
                //当前移库库存货品的库存数(转换为小包装单位)
                double qtyCurOverage = (double)(sQtyDtl.GoodsQty != null ? sQtyDtl.GoodsQty : 0);
                double qtyCurPrice = (double)(sQtyDtl.Price != null ? sQtyDtl.Price : 0);
                ZhiFang.Common.Log.Log.Info(string.Format("移库的库存货品开始单位换算：原数量{0}，原价格{1}", qtyCurOverage, qtyCurPrice));
                BaseResultDataValue tempResult = SearchReaGoodsMinUnitCount(sQtyDtl.GoodsID, sQtyDtl.ReaCompanyID, ref qtyCurOverage, ref qtyCurPrice);
                ZhiFang.Common.Log.Log.Info(string.Format("移库的库存货品结束单位换算：转换后数量{0}，转换后价格{1}", qtyCurOverage, qtyCurPrice));
                if (!tempResult.success)
                {
                    brdv = tempResult;
                    return;
                }
                double curOutDtlQty = outDtlGoodsQty;
                if (curOutDtlQty >= qtyCurOverage) curOutDtlQty = qtyCurOverage;
                EditTransferDtlAndQtyDtlOfOther(transferDoc, ref transferDtl, ref sQtyDtl, qtyCurOverage, curOutDtlQty, ref outDtlGoodsQty, ref brdv);
                if (brdv.success == false)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("货品【{0}】货品移库失败：处理移库的库存信息失败！", transferDtl.GoodsCName);
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    throw new Exception(brdv.ErrorInfo);
                    //return;
                }
                //更新sQtyDtl在listReaBmsQtyDtl的库存数
                listReaBmsQtyDtl[qtyIndexOf].GoodsQty = sQtyDtl.GoodsQty;
                listReaBmsQtyDtl[qtyIndexOf].SumTotal = listReaBmsQtyDtl[qtyIndexOf].GoodsQty * listReaBmsQtyDtl[qtyIndexOf].Price;

                curListReaBmsQtyDtl[i].GoodsQty = sQtyDtl.GoodsQty;
                curListReaBmsQtyDtl[i].SumTotal = curListReaBmsQtyDtl[i].GoodsQty * curListReaBmsQtyDtl[i].Price;
                //如果移库对应库存记录的库存数等于0,需要将库存记录对应的货品条码剩余扫码数也设置为0
                if (sQtyDtl.GoodsQty <= 0)
                    AddOutBarcodeOfOutOtherQty(transferDoc, sQtyDtl, ref transferDtl, ref outDtlGoodsQty, ref brdv);
            }//for 
        }
        private void AddOutBarcodeOfOutOtherQty(ReaBmsTransferDoc transferDoc, ReaBmsQtyDtl sQtyDtl, ref ReaBmsTransferDtl transferDtl, ref double outDtlGoodsQty, ref BaseResultDataValue brdv)
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
                long operTypeID = long.Parse(ReaGoodsBarcodeOperType.移库出库.Key);
                addOperation.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                addOperation.LabID = transferDtl.LabID;
                addOperation.BDocID = transferDtl.TransferDocID;
                addOperation.BDocNo = transferDoc.TransferDocNo;
                addOperation.BDtlID = transferDtl.Id;
                addOperation.QtyDtlID = sQtyDtl.Id;
                addOperation.OperTypeID = operTypeID;
                addOperation.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
                addOperation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[addOperation.OperTypeID.ToString()].Name;
                addOperation.ScanCodeGoodsUnit = transferDtl.GoodsUnit;
                addOperation.ScanCodeGoodsID = transferDtl.GoodsID;
                addOperation.Memo = "非直接移库扫码;库存货品库存数为0,对应的货品条码全部标志为移库领用!";
                if (!addOperation.MinBarCodeQty.HasValue)
                {
                    ReaGoods reaGoods = IDReaGoodsDao.Get(transferDtl.GoodsID.Value);
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
                IBReaGoodsBarcodeOperation.AddBarcodeOperation(addOperation, 0, transferDoc.CreaterID.Value, transferDoc.CreaterName, transferDtl.LabID);
            }
        }
        /// <summary>
        /// 移库明细剩余未扫码的移库货品处理二
        /// </summary>
        /// <param name="transferDoc"></param>
        /// <param name="transferDtl"></param>
        /// <param name="sQtyDtl">当前移库对应的库存记录</param>
        /// <param name="qtyCurOverage">移库货品的当前剩余库存数</param>
        /// <param name="outDtlGoodsQty">某一移库明细的移库总数</param>
        /// <param name="curTransferQty">当前移库数</param>
        /// <param name="brdv"></param>
        private void EditTransferDtlAndQtyDtlOfOther(ReaBmsTransferDoc transferDoc, ref ReaBmsTransferDtl transferDtl, ref ReaBmsQtyDtl sQtyDtl, double qtyCurOverage, double curTransferQty, ref double outDtlGoodsQty, ref BaseResultDataValue brdv)
        {
            if (outDtlGoodsQty <= 0) return;

            if (!transferDtl.GoodsLotID.HasValue)
                transferDtl.GoodsLotID = sQtyDtl.GoodsLotID;
            //移库入库的库存信息
            ReaBmsQtyDtl addQtyDtl = new ReaBmsQtyDtl();
            ReaGoods qtyReaGoods = IDReaGoodsDao.Get(sQtyDtl.GoodsID.Value);
            double gonvertQty = qtyReaGoods.GonvertQty;
            //剩余库存数保留两位小数点
            qtyCurOverage = ConvertQtyHelp.ConvertQty(qtyCurOverage, 2);
            //当前库存数大于当前移库数时处理
            if (qtyCurOverage >= curTransferQty)
            {
                #region 移库领用库存处理                
                string resultInfo = "";
                ReaGoodsOrgLink reaGoodsOrgLink = IDReaGoodsOrgLinkDao.GetReaGoodsMinUnit(long.Parse(ReaCenOrgType.供货方.Key), sQtyDtl.ReaCompanyID.Value, qtyReaGoods.Id, qtyReaGoods, ref resultInfo);
                //剩余的库存数=移库货品的当前库存数-当前移库数
                double curOverageQty1 = qtyCurOverage - curTransferQty;
                if (curOverageQty1 > 0)
                {
                    if (reaGoodsOrgLink != null && qtyReaGoods.GonvertQty > 0)
                    {
                        curOverageQty1 = curOverageQty1 / qtyReaGoods.GonvertQty;
                        ZhiFang.Common.Log.Log.Info(string.Format("移库领用的库存货品剩余的库存数单位换算：转换后数量:{0}", curOverageQty1));
                    }
                }
                //更新库存数量
                sQtyDtl.GoodsQty = curOverageQty1;
                sQtyDtl.SumTotal = sQtyDtl.GoodsQty * sQtyDtl.Price;//更新库存总价
                IBReaBmsQtyDtl.Entity = sQtyDtl;
                if (!IBReaBmsQtyDtl.Edit())
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "货品移库失败：库存更新失败！";
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    throw new Exception(brdv.ErrorInfo);
                    //return;
                }
                //移库领用的库存变化记录数
                double curTransferQty2 = curTransferQty;
                if (gonvertQty > 1)
                {
                    curTransferQty2 = curTransferQty / gonvertQty;
                }
                IBReaBmsQtyDtlOperation.AddReaBmsQtyDtlTransferOperation(transferDoc, transferDtl, IBReaBmsQtyDtl.Entity, curTransferQty2, long.Parse(ReaBmsQtyDtlOperationOperType.移库出库.Key));
                #endregion
                #region 移库入库库存处理
                if (curTransferQty > 0)
                {
                    if (gonvertQty > 0)
                    {
                        curTransferQty = curTransferQty / gonvertQty;
                        ZhiFang.Common.Log.Log.Info(string.Format("移库入库库存的库存货品的库存数单位换算：转换后数量:{0}", curTransferQty));
                    }
                }

                addQtyDtl = EditCopyReaBmsQtyDtl(sQtyDtl, addQtyDtl, transferDtl);
                addQtyDtl.GoodsQty = curTransferQty;
                addQtyDtl.SumTotal = addQtyDtl.GoodsQty * addQtyDtl.Price;
                IBReaBmsQtyDtl.Entity = addQtyDtl;
                if (!IBReaBmsQtyDtl.Add())
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "货品移库失败：库存新增失败！";
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    throw new Exception(brdv.ErrorInfo);
                }
                IBReaBmsQtyDtlOperation.AddReaBmsQtyDtlTransferOperation(transferDoc, transferDtl, IBReaBmsQtyDtl.Entity, addQtyDtl.GoodsQty.Value, long.Parse(ReaBmsQtyDtlOperationOperType.移库入库.Key));
                outDtlGoodsQty = outDtlGoodsQty - qtyCurOverage;
                #endregion
            }
            else if (qtyCurOverage > 0)
            {
                double curTransferQty2 = sQtyDtl.GoodsQty.Value;
                addQtyDtl.GoodsQty = sQtyDtl.GoodsQty;
                addQtyDtl.SumTotal = addQtyDtl.GoodsQty * addQtyDtl.Price;
                #region 移库领用库存处理
                sQtyDtl.GoodsQty = 0;//更新库存数量
                sQtyDtl.SumTotal = 0;
                IBReaBmsQtyDtl.Entity = sQtyDtl;
                if (!IBReaBmsQtyDtl.Edit())
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "货品移库失败：库存更新失败！";
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    throw new Exception(brdv.ErrorInfo);
                    //return;
                }
                IBReaBmsQtyDtlOperation.AddReaBmsQtyDtlTransferOperation(transferDoc, transferDtl, IBReaBmsQtyDtl.Entity, curTransferQty, long.Parse(ReaBmsQtyDtlOperationOperType.移库出库.Key));
                outDtlGoodsQty = outDtlGoodsQty - qtyCurOverage;
                #endregion
                #region 移库入库库存处理
                addQtyDtl = EditCopyReaBmsQtyDtl(sQtyDtl, addQtyDtl, transferDtl);
                addQtyDtl.GoodsQty = curTransferQty2;
                addQtyDtl.SumTotal = addQtyDtl.GoodsQty * addQtyDtl.Price;
                IBReaBmsQtyDtl.Entity = addQtyDtl;
                if (!IBReaBmsQtyDtl.Add())
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "货品移库失败：库存新增失败！";
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    throw new Exception(brdv.ErrorInfo);
                    //return;
                }
                IBReaBmsQtyDtlOperation.AddReaBmsQtyDtlTransferOperation(transferDoc, transferDtl, IBReaBmsQtyDtl.Entity, curTransferQty, long.Parse(ReaBmsQtyDtlOperationOperType.移库入库.Key));
                #endregion
            }
            if (string.IsNullOrEmpty(transferDtl.QtyDtlID))
            {
                transferDtl.QtyDtlID = addQtyDtl.Id + ",";
            }
            else if (!transferDtl.QtyDtlID.Contains(addQtyDtl.Id.ToString()))
            {
                transferDtl.QtyDtlID = transferDtl.QtyDtlID.TrimEnd(',') + "," + addQtyDtl.Id + ",";
            }
            if (string.IsNullOrEmpty(transferDtl.SQtyDtlID))
            {
                transferDtl.SQtyDtlID = sQtyDtl.Id + ",";
            }
            else if (!transferDtl.SQtyDtlID.Contains(sQtyDtl.Id.ToString()))
            {
                transferDtl.SQtyDtlID = transferDtl.SQtyDtlID.TrimEnd(',') + "," + sQtyDtl.Id + ",";
            }
        }
        private ReaBmsQtyDtl EditCopyReaBmsQtyDtl(ReaBmsQtyDtl sQtyDtl, ReaBmsQtyDtl addQtyDtl, ReaBmsTransferDtl transferDtl)
        {
            addQtyDtl = ClassMapperHelp.GetMapper<ReaBmsQtyDtl, ReaBmsQtyDtl>(sQtyDtl);
            addQtyDtl.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
            addQtyDtl.InDtlID = sQtyDtl.InDtlID;
            addQtyDtl.InDocNo = sQtyDtl.InDocNo;
            addQtyDtl.PQtyDtlID = sQtyDtl.Id;
            addQtyDtl.StorageID = transferDtl.DStorageID;
            addQtyDtl.StorageName = transferDtl.DStorageName;
            addQtyDtl.PlaceID = transferDtl.DPlaceID;
            addQtyDtl.PlaceName = transferDtl.DPlaceName;
            addQtyDtl.Price = sQtyDtl.Price;
            addQtyDtl.SumTotal = sQtyDtl.Price * addQtyDtl.GoodsQty;
            addQtyDtl.DataAddTime = DateTime.Now;
            addQtyDtl.DataUpdateTime = DateTime.Now;
            return addQtyDtl;
        }
        /// <summary>
        /// 当次移库领用条码对应的大包装单位条码信息
        /// </summary>
        /// <param name="reaBmsTransferDoc"></param>
        /// <param name="transferReaGoods"></param>
        /// <param name="transferDtl"></param>
        /// <param name="outBarcodeList"></param>
        /// <returns></returns>
        private IList<ReaGoodsBarcodeOperation> AddGetMaxBarCodeList(ReaBmsTransferDoc reaBmsTransferDoc, ReaGoods transferReaGoods, ref ReaBmsTransferDtl transferDtl, IList<ReaGoodsBarcodeOperation> outBarcodeList)
        {
            IList<ReaGoodsBarcodeOperation> maxBarCodeList = new List<ReaGoodsBarcodeOperation>();
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
                IList<ReaGoodsBarcodeOperation> maxBarCodeList2 = IBReaGoodsBarcodeOperation.GetListByHQL(maxBarCodeHql);//GetListByHQL
                ZhiFang.Common.Log.Log.Info(string.Format("移库扫码.maxBarCodeHql:{0},当前移库扫码货品对应的大包装单位条码集合：{1}", maxBarCodeHql, maxBarCodeList2.Count));
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
                ZhiFang.Common.Log.Log.Info(string.Format("移库扫码.maxBarCodeHql:{0},当前移库扫码货品对应的大包装单位条码集合：{1}", maxBarCodeHql, maxBarCodeList2.Count));
                if (maxBarCodeList2 == null || maxBarCodeList2.Count <= 0)
                    continue;

                ReaGoodsBarcodeOperation maxBarCode1 = maxBarCodeList2.OrderByDescending(p => p.DataAddTime).ElementAt(0);
                ReaGoodsBarcodeOperation addMaxOperation = ClassMapperHelp.GetMapper<ReaGoodsBarcodeOperation, ReaGoodsBarcodeOperation>(maxBarCode1);
                addMaxOperation.LabID = transferDtl.LabID;
                addMaxOperation.BDocID = transferDtl.TransferDocID;
                addMaxOperation.BDocNo = reaBmsTransferDoc.TransferDocNo;
                addMaxOperation.BDtlID = transferDtl.Id;
                addMaxOperation.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
                addMaxOperation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[addMaxOperation.OperTypeID.ToString()].Name;
                addMaxOperation.ScanCodeGoodsID = transferDtl.GoodsID;
                addMaxOperation.ScanCodeGoodsUnit = transferDtl.GoodsUnit;
                ZhiFang.Common.Log.Log.Info(string.Format("移库扫码.PUsePackSerial:{0},当前OverageQty：{1}", maxOperation.PUsePackSerial, addMaxOperation.OverageQty));
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
        /// 新增移库领用条码记录
        /// </summary>
        /// <param name="transferDtl"></param>
        /// <param name="sQtyDtl"></param>
        /// <param name="soperation">移库扫码条码信息</param>
        /// <param name="minBarCodeList">当前移库扫码货品对应的小包装单位条码集合</param>
        /// <param name="transferReaGoods"></param>
        /// <param name="overageQty1"></param>
        /// <param name="operTypeID"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        private BaseResultBool AddBarcodeOperationOfTransferOut(ReaBmsTransferDtl transferDtl, ReaBmsQtyDtl sQtyDtl, ReaGoodsBarcodeOperation soperation, IList<ReaGoodsBarcodeOperation> minBarCodeList, ReaGoods transferReaGoods, double overageQty1, long operTypeID, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            ReaBmsInDtl inDtl = IDReaBmsInDtlDao.Get(sQtyDtl.InDtlID.Value);
            ReaGoodsBarcodeOperation addOperation = new ReaGoodsBarcodeOperation();
            addOperation.SysPackSerial = soperation.SysPackSerial;
            addOperation.OtherPackSerial = soperation.OtherPackSerial;
            addOperation.UsePackSerial = soperation.UsePackSerial;
            addOperation.UsePackQRCode = soperation.UsePackQRCode;
            if (string.IsNullOrEmpty(soperation.PUsePackSerial))
                addOperation.PUsePackSerial = addOperation.Id.ToString();
            else
                addOperation.PUsePackSerial = soperation.PUsePackSerial;
            addOperation.LabID = sQtyDtl.LabID;
            addOperation.BDocID = inDtl.InDocID;
            addOperation.BDocNo = sQtyDtl.InDocNo;
            addOperation.BDtlID = sQtyDtl.Id;
            addOperation.QtyDtlID = sQtyDtl.Id;
            addOperation.ReaCompanyID = sQtyDtl.ReaCompanyID;
            addOperation.CompanyName = sQtyDtl.CompanyName;
            addOperation.ReaCompCode = sQtyDtl.ReaCompCode;
            addOperation.CompGoodsLinkID = sQtyDtl.CompGoodsLinkID;
            if (!addOperation.GoodsLotID.HasValue)
                addOperation.GoodsLotID = sQtyDtl.GoodsLotID;
            if (!string.IsNullOrEmpty(soperation.LotNo))
                addOperation.LotNo = soperation.LotNo;
            else
                addOperation.LotNo = sQtyDtl.LotNo;
            addOperation.ReaGoodsNo = soperation.ReaGoodsNo;
            addOperation.ProdGoodsNo = soperation.ProdGoodsNo;
            addOperation.GoodsID = soperation.GoodsID; //inDtl.ReaGoods.Id;
            addOperation.ScanCodeGoodsID = soperation.ScanCodeGoodsID;// inDtl.ReaGoods.Id;
            addOperation.GoodsCName = soperation.GoodsCName;
            addOperation.GoodsUnit = soperation.GoodsUnit;
            addOperation.GoodsSort = soperation.GoodsSort;
            addOperation.UnitMemo = soperation.UnitMemo;
            addOperation.ReaGoodsNo = soperation.ReaGoodsNo;
            addOperation.ProdGoodsNo = soperation.ProdGoodsNo;
            addOperation.CenOrgGoodsNo = soperation.CenOrgGoodsNo;
            addOperation.GoodsNo = inDtl.GoodsNo;
            addOperation.StorageID = inDtl.StorageID;
            addOperation.PlaceID = inDtl.PlaceID;
            addOperation.DispOrder = soperation.DispOrder;
            if (!addOperation.GoodsQty.HasValue)
                addOperation.GoodsQty = 1;
            addOperation.BarCodeType = soperation.BarCodeType;
            addOperation.OperTypeID = operTypeID;
            addOperation.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
            addOperation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[addOperation.OperTypeID.ToString()].Name;
            if (!addOperation.MinBarCodeQty.HasValue)
            {
                addOperation.MinBarCodeQty = transferReaGoods.GonvertQty;
            }
            if (soperation.ScanCodeQty.HasValue)
                addOperation.ScanCodeQty = soperation.ScanCodeQty;
            else
                addOperation.ScanCodeQty = 1;
            if (string.IsNullOrEmpty(addOperation.ScanCodeGoodsUnit))
                addOperation.ScanCodeGoodsUnit = transferDtl.GoodsUnit;
            if (overageQty1 < 0)
                overageQty1 = 0;
            addOperation.OverageQty = overageQty1;
            addOperation.Memo = string.Format("由源库房:{0},源货架:{1}移库到目标库房:{2},目标货架:{3};{4}", transferDtl.SStorageName, transferDtl.SPlaceName, transferDtl.DStorageName, transferDtl.DPlaceName, soperation.Memo);
            tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperation(addOperation, 0, empID, empName, transferDtl.LabID);

            #region 当前条码对应的小包装单位条码信息处理(当前出库货品条码为大包装条码,并且存在小包装货品条码信息)
            var minBarCodeList2 = minBarCodeList.Where(p => (p.UsePackSerial != addOperation.UsePackSerial || p.UsePackQRCode != addOperation.UsePackQRCode)).ToList();
            if (minBarCodeList2 != null && minBarCodeList2.Count > 0)
            {
                ReaGoods addMinReaGoods = IDReaGoodsDao.Get(minBarCodeList2[0].GoodsID.Value);
                foreach (ReaGoodsBarcodeOperation minBarCode in minBarCodeList2)
                {
                    ReaGoodsBarcodeOperation addMinBarCode = ClassMapperHelp.GetMapper<ReaGoodsBarcodeOperation, ReaGoodsBarcodeOperation>(minBarCode);
                    addMinBarCode.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                    addMinBarCode.LabID = addOperation.LabID;
                    addMinBarCode.BDocID = addOperation.BDocID;
                    addMinBarCode.BDocNo = addOperation.BDocNo;
                    addMinBarCode.BDtlID = addOperation.BDtlID;
                    addMinBarCode.QtyDtlID = addOperation.QtyDtlID;
                    if (string.IsNullOrEmpty(addOperation.PUsePackSerial))
                        addMinBarCode.PUsePackSerial = addOperation.Id.ToString();
                    else
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
                    addMinBarCode.Memo = "非直接移库扫码;大包装货品条码移库扫码,同时更新对应小包装货品条码为移库领用!";
                    tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperation(addMinBarCode, 0, empID, empName, transferDtl.LabID);
                }
            }
            #endregion
            return tempBaseResultBool;
        }
        /// <summary>
        /// 新增移库入库条码记录
        /// </summary>
        /// <param name="reaBmsTransferDoc"></param>
        /// <param name="transferDtl"></param>
        /// <param name="addQtyDtl"></param>
        /// <param name="dtAddList"></param>
        /// <param name="operTypeID"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        private BaseResultBool AddBarcodeOperationOfTransferIn(ReaBmsTransferDoc reaBmsTransferDoc, ReaBmsTransferDtl transferDtl, ReaBmsQtyDtl addQtyDtl, ReaGoodsBarcodeOperation soperation, IList<ReaGoodsBarcodeOperation> minBarCodeList, ReaGoods transferReaGoods, long operTypeID, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            ReaGoodsBarcodeOperation addOperation = new ReaGoodsBarcodeOperation();
            addOperation.SysPackSerial = soperation.SysPackSerial;
            addOperation.OtherPackSerial = soperation.OtherPackSerial;
            addOperation.UsePackSerial = soperation.UsePackSerial;
            addOperation.UsePackQRCode = soperation.UsePackQRCode;
            if (string.IsNullOrEmpty(soperation.PUsePackSerial))
                addOperation.PUsePackSerial = addOperation.Id.ToString();
            else
                addOperation.PUsePackSerial = soperation.PUsePackSerial;
            addOperation.LabID = transferDtl.LabID;
            addOperation.BDocID = transferDtl.TransferDocID;
            addOperation.BDocNo = reaBmsTransferDoc.TransferDocNo;
            addOperation.BDtlID = transferDtl.Id;
            addOperation.QtyDtlID = addQtyDtl.Id;
            addOperation.ReaCompanyID = transferDtl.ReaCompanyID;
            addOperation.CompanyName = transferDtl.ReaCompanyName;
            if (!addOperation.GoodsLotID.HasValue)
                addOperation.GoodsLotID = transferDtl.GoodsLotID;
            addOperation.LotNo = transferDtl.LotNo;
            addOperation.GoodsID = transferDtl.GoodsID;
            addOperation.ScanCodeGoodsID = transferDtl.GoodsID;
            addOperation.GoodsCName = transferDtl.GoodsCName;
            addOperation.GoodsUnit = transferDtl.GoodsUnit;
            addOperation.ReaGoodsNo = transferDtl.ReaGoodsNo;
            addOperation.ProdGoodsNo = transferDtl.ProdGoodsNo;
            addOperation.CenOrgGoodsNo = transferDtl.CenOrgGoodsNo;
            addOperation.GoodsNo = transferDtl.GoodsNo;
            addOperation.ReaCompCode = transferDtl.ReaCompCode;
            addOperation.GoodsSort = transferDtl.GoodsSort;
            addOperation.CompGoodsLinkID = transferDtl.CompGoodsLinkID;
            addOperation.StorageID = transferDtl.DStorageID;
            addOperation.PlaceID = transferDtl.DPlaceID;
            addOperation.DispOrder = soperation.DispOrder;
            if (!addOperation.GoodsQty.HasValue)
                addOperation.GoodsQty = 1;
            addOperation.UnitMemo = transferDtl.UnitMemo;
            addOperation.BarCodeType = transferDtl.BarCodeType;
            addOperation.OperTypeID = operTypeID;
            addOperation.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
            addOperation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[addOperation.OperTypeID.ToString()].Name;
            if (!addOperation.MinBarCodeQty.HasValue)
            {
                double gonvertQty = 1;
                if (transferReaGoods != null) gonvertQty = transferReaGoods.GonvertQty;
                if (gonvertQty <= 0)
                {
                    ZhiFang.Common.Log.Log.Warn("货品编码为:" + addOperation.ReaGoodsNo + ",货品名称为:" + addOperation.GoodsCName + ",货品包装单位的换算系数值为" + gonvertQty + ",维护不合理!");
                    gonvertQty = 1;
                }
                addOperation.MinBarCodeQty = gonvertQty;
            }
            if (soperation.ScanCodeQty.HasValue)
                addOperation.ScanCodeQty = soperation.ScanCodeQty;
            else
                addOperation.ScanCodeQty = 1;
            if (string.IsNullOrEmpty(addOperation.ScanCodeGoodsUnit))
                addOperation.ScanCodeGoodsUnit = transferDtl.GoodsUnit;
            addOperation.OverageQty = addQtyDtl.GoodsQty;
            if (!addOperation.DataAddTime.HasValue)
                addOperation.DataAddTime = DateTime.Now;
            //保证移库入库的条码加入时间要比同一时间的移库领用条码记录的登记时间晚
            addOperation.DataAddTime = addOperation.DataAddTime.Value.AddSeconds(3);
            addOperation.Memo = string.Format("由源库房:{0},源货架:{1}移库到目标库房:{2},目标货架:{3};{4}", transferDtl.SStorageName, transferDtl.SPlaceName, transferDtl.DStorageName, transferDtl.DPlaceName, soperation.Memo);
            tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperation(addOperation, 0, empID, empName, transferDtl.LabID);

            #region 当前条码对应的小包装单位条码信息处理(当前出库货品条码为大包装条码,并且存在小包装货品条码信息)
            var minBarCodeList2 = minBarCodeList.Where(p => (p.UsePackSerial != addOperation.UsePackSerial || p.UsePackQRCode != addOperation.UsePackQRCode)).ToList();
            if (minBarCodeList2 != null && minBarCodeList2.Count > 0)
            {
                ReaGoods addMinReaGoods = IDReaGoodsDao.Get(minBarCodeList2[0].GoodsID.Value);
                foreach (ReaGoodsBarcodeOperation minBarCode in minBarCodeList2)
                {
                    ReaGoodsBarcodeOperation addMinBarCode = ClassMapperHelp.GetMapper<ReaGoodsBarcodeOperation, ReaGoodsBarcodeOperation>(minBarCode);
                    addMinBarCode.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                    addMinBarCode.LabID = addOperation.LabID;
                    addMinBarCode.BDocID = addOperation.BDocID;
                    addMinBarCode.BDocNo = addOperation.BDocNo;
                    addMinBarCode.BDtlID = addOperation.BDtlID;
                    addMinBarCode.QtyDtlID = addOperation.QtyDtlID;
                    addMinBarCode.StorageID = transferDtl.DStorageID;
                    addMinBarCode.PlaceID = transferDtl.DPlaceID;
                    addMinBarCode.OperTypeID = operTypeID;
                    addMinBarCode.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
                    addMinBarCode.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[addMinBarCode.OperTypeID.ToString()].Name;
                    if (string.IsNullOrEmpty(addOperation.PUsePackSerial))
                        addMinBarCode.PUsePackSerial = addOperation.Id.ToString();
                    else
                        addMinBarCode.PUsePackSerial = addOperation.PUsePackSerial;
                    if (!addMinBarCode.ScanCodeGoodsID.HasValue)
                        addMinBarCode.ScanCodeGoodsID = addMinReaGoods.Id;
                    if (string.IsNullOrEmpty(addMinBarCode.ScanCodeGoodsUnit))
                        addMinBarCode.ScanCodeGoodsUnit = addMinReaGoods.UnitName;
                    if (!addMinBarCode.ScanCodeQty.HasValue)
                        addMinBarCode.ScanCodeQty = 1;
                    if (!addMinBarCode.MinBarCodeQty.HasValue)
                        addMinBarCode.MinBarCodeQty = addMinReaGoods.GonvertQty;
                    if (!addMinBarCode.OverageQty.HasValue)
                        addMinBarCode.OverageQty = addMinReaGoods.GonvertQty;
                    addMinBarCode.Memo = "非直接移库扫码;大包装货品条码移库扫码,同时更新对应小包装货品条码为移库入库!";
                    if (!addMinBarCode.DataAddTime.HasValue)
                        addMinBarCode.DataAddTime = DateTime.Now;
                    //保证移库入库的条码加入时间要比同一时间的移库领用条码记录的登记时间晚
                    addMinBarCode.DataAddTime = addMinBarCode.DataAddTime.Value.AddSeconds(3);
                    tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperation(addMinBarCode, 0, empID, empName, transferDtl.LabID);
                }
            }
            #endregion

            return tempBaseResultBool;
        }
        #endregion

        #region 移库申请
        public EntityList<ReaBmsTransferDoc> SearchReaBmsTransferDocByReqDeptHQL(long deptId, string strHqlWhere, string order, int page, int count)
        {
            EntityList<ReaBmsTransferDoc> entityList = new EntityList<ReaBmsTransferDoc>();
            if (deptId >= 0)
            {
                //申请人所属部门及其所属部门的所有子部门信息
                string deptIdStr = IBHRDept.SearchHRDeptIdListByHRDeptId(deptId);
                if (string.IsNullOrEmpty(strHqlWhere))
                    strHqlWhere = " 1=1 ";
                if (!string.IsNullOrEmpty(deptIdStr))
                {
                    strHqlWhere = strHqlWhere + " and reabmstransferdoc.DeptID in(" + deptIdStr.TrimEnd(',') + ")";
                }
            }
            entityList = this.SearchListByHQL(strHqlWhere, order, page, count);

            return entityList;
        }
        public BaseResultDataValue SearchSumReqGoodsQtyAndCurrentQtyByHQL(string qtyHql, string dtlHql, string goodsId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            //当前剩余总库存
            double sumCurrentQty = 0;
            //已申请总数
            double sumDtlGoodsQty = 0;
            ReaGoods dtlGooods = null;
            if (!string.IsNullOrEmpty(goodsId))
            {
                dtlGooods = IDReaGoodsDao.Get(long.Parse(goodsId));
            }
            if (!string.IsNullOrEmpty(qtyHql))
            {
                IList<ReaBmsQtyDtl> qtyList = IBReaBmsQtyDtl.SearchListByHQL(qtyHql);
                if (qtyList != null)
                {
                    var groupByList = qtyList.GroupBy(p => p.GoodsID.Value);
                    foreach (var groupBy in groupByList)
                    {
                        ReaGoods qtyGooods = IDReaGoodsDao.Get(groupBy.Key);
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
            if (!string.IsNullOrEmpty(dtlHql))
            {
                string docHql = string.Format(" reabmstransferdoc.Visible=1 and reabmstransferdoc.Status!={0} and reabmstransferdoc.Status!={1}", ReaBmsTransferDocStatus.申请作废.Key, ReaBmsTransferDocStatus.移库完成.Key);
                IList<ReaBmsTransferDtl> dtlList = IBReaBmsTransferDtl.SearchReaBmsTransferDtlSummaryListByHQL(docHql, dtlHql, "", "", -1, -1);
                if (dtlList != null)
                {
                    var groupByList = dtlList.GroupBy(p => p.GoodsID.Value);
                    foreach (var groupBy in groupByList)
                    {
                        ReaGoods oldDtlGooods = IDReaGoodsDao.Get(groupBy.Key);
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
        public BaseResultDataValue AddTransferDocAndDtlOfApply(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtlAddList, bool isEmpTransfer, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数entity为空!";
                return brdv;
            }
            if (dtlAddList == null || dtlAddList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数dtAddList为空!";
                return brdv;
            }
            brdv = IBReaBmsTransferDtl.EditValidTransferDtlList(dtlAddList);
            if (!brdv.success)
                return brdv;

            //当前移库人是否有移库权限及盘库锁定
            brdv = AddValidDocAndDtl(dtlAddList, isEmpTransfer, empID, empName);
            if (!brdv.success)
                return brdv;

            entity.TotalPrice = dtlAddList.Sum(p => p.SumTotal);
            AddReaBmsTransferDoc(ref entity, empID, empName, ref brdv);
            if (brdv.success)
            {
                brdv = IBReaBmsTransferDtl.AddTransferDtlList(entity, dtlAddList, empID, empName);
                if (brdv.success) AddSCOperation(entity, empID, empName);
            }
            else
            {
                brdv.ErrorInfo = "新增移库保存失败!";
            }
            return brdv;
        }
        public BaseResultBool UpdateTransferDocAndDtl(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtlAddList, IList<ReaBmsTransferDtl> dtlEditList, bool isEmpTransfer, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();
            if (entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数entity为空!";
                return brdv;
            }
            if ((dtlAddList == null || dtlAddList.Count <= 0) && (dtlEditList == null || dtlEditList.Count <= 0))
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数dtAddList及dtEditList都为空!";
                return brdv;
            }

            if (dtlAddList != null && dtlAddList.Count > 0)
            {
                BaseResultDataValue brdv1 = IBReaBmsTransferDtl.EditValidTransferDtlList(dtlAddList);
                brdv.success = brdv1.success;
                brdv.ErrorInfo = brdv1.ErrorInfo;
                if (!brdv.success)
                    return brdv;
            }
            if (dtlEditList != null && dtlEditList.Count > 0)
            {
                BaseResultDataValue brdv1 = IBReaBmsTransferDtl.EditValidTransferDtlList(dtlEditList);
                brdv.success = brdv1.success;
                brdv.ErrorInfo = brdv1.ErrorInfo;
                if (!brdv.success)
                    return brdv;
            }

            ReaBmsTransferDoc oldEntity = this.Get(entity.Id);
            if (oldEntity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取移库单ID为:" + entity.Id + ",移库信息为空!";
                return brdv;
            }
            List<string> tmpa = new List<string>();// tempArray.ToList();
            if (!EditReaBmsTransferDocStatusCheck(entity, oldEntity, tmpa, empID, empName))
            {
                brdv.success = false;
                brdv.ErrorInfo = "移库单ID：" + entity.Id + "的状态为：" + ReaBmsTransferDocStatus.GetStatusDic()[oldEntity.Status.ToString()].Name + "！";
                return brdv;
            }
            //重新计算移库总金额
            entity.TotalPrice = 0;
            if (dtlAddList != null && dtlAddList.Count > 0)
                entity.TotalPrice = entity.TotalPrice + dtlAddList.Sum(p => p.SumTotal);
            if (dtlEditList != null && dtlEditList.Count > 0)
                entity.TotalPrice = entity.TotalPrice + dtlEditList.Sum(p => p.SumTotal);

            if (entity.Status.ToString() == ReaBmsTransferDocStatus.移库完成.Key)
            {
                brdv = EditTransferDocAndDtlOfComp(entity, dtlAddList, dtlEditList, isEmpTransfer, empID, empName);
                if (!brdv.success)
                    return brdv;
            }
            else
            {
                #region 检查是否有移库权限及被盘库锁定
                if (dtlAddList != null && dtlAddList.Count > 0)
                {
                    BaseResultDataValue brdv2 = AddValidDocAndDtl(dtlAddList, isEmpTransfer, empID, empName);
                    brdv.success = brdv2.success;
                    brdv.ErrorInfo = brdv2.ErrorInfo;
                    if (!brdv.success)
                        return brdv;
                }

                if (dtlEditList != null && dtlEditList.Count > 0)
                {
                    foreach (var outDtl in dtlEditList)
                    {
                        //源库房及源货架
                        brdv = IDReaBmsCheckDocDao.EditValidIsLock(outDtl.ReaCompanyID, outDtl.ReaCompanyName, outDtl.SStorageID, outDtl.SStorageName, outDtl.SPlaceID, outDtl.SStorageName, outDtl.GoodsID);
                        if (!brdv.success)
                            return brdv;
                    }
                }
                #endregion

                if (dtlAddList != null && dtlAddList.Count > 0)
                {
                    BaseResultDataValue brdv3 = IBReaBmsTransferDtl.AddTransferDtlList(entity, dtlAddList, empID, empName);
                    brdv.success = brdv3.success;
                    brdv.ErrorInfo = brdv3.ErrorInfo;
                }
                if (!brdv.success)
                    return brdv;

                if (dtlEditList != null && dtlEditList.Count > 0)
                    brdv = IBReaBmsTransferDtl.UpdateTransferDtlList(entity, dtlEditList);
            }

            if (!brdv.success)
                return brdv;

            entity.DataTimeStamp = oldEntity.DataTimeStamp;
            this.Entity = entity;
            if (this.Edit())
            {
                AddSCOperation(entity, empID, empName);
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "编辑保存移库主单信息失败！";
            }
            return brdv;
        }
        bool EditReaBmsTransferDocStatusCheck(ReaBmsTransferDoc entity, ReaBmsTransferDoc serverEntity, List<string> tmpa, long empID, string empName)
        {
            if (entity.Status.ToString() == ReaBmsTransferDocStatus.暂存.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsTransferDocStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsTransferDocStatus.审核退回.Key)
                {
                    return false;
                }
            }
            if (entity.Status.ToString() == ReaBmsTransferDocStatus.已申请.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsTransferDocStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsTransferDocStatus.已申请.Key && serverEntity.Status.ToString() != ReaBmsTransferDocStatus.审核退回.Key)
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
                entity.OperID = empID;
                entity.OperName = empName;
                tmpa.Add("OperID=" + entity.OperID + " ");
                tmpa.Add("OperName='" + entity.OperName + "'");
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.Add("CheckID=null");
                tmpa.Add("CheckName=null");
                tmpa.Add("CheckTime=null");
            }
            if (entity.Status.ToString() == ReaBmsTransferDocStatus.申请作废.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsTransferDocStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsTransferDocStatus.已申请.Key && serverEntity.Status.ToString() != ReaBmsTransferDocStatus.审核退回.Key)
                {
                    return false;
                }
                entity.OperID = empID;
                entity.OperName = empName;
                tmpa.Add("OperID=" + entity.OperID + " ");
                tmpa.Add("OperName='" + entity.OperName + "'");
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }

            if (entity.Status.ToString() == ReaBmsTransferDocStatus.审核通过.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsTransferDocStatus.已申请.Key && serverEntity.Status.ToString() != ReaBmsTransferDocStatus.审核退回.Key)
                {
                    return false;
                }
                if (!entity.CheckID.HasValue)
                {
                    entity.CheckID = empID;
                    entity.CheckName = empName;
                    tmpa.Add("CheckID=" + entity.CheckID + " ");
                    tmpa.Add("CheckName='" + entity.CheckName + "'");
                    tmpa.Add("CheckTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
                entity.OperID = empID;
                entity.OperName = empName;
                tmpa.Add("OperID=" + entity.OperID + " ");
                tmpa.Add("OperName='" + entity.OperName + "'");
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }

            if (entity.Status.ToString() == ReaBmsTransferDocStatus.审核退回.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsTransferDocStatus.已申请.Key)
                {
                    return false;
                }
                entity.OperID = empID;
                entity.OperName = empName;
                tmpa.Add("OperID=" + entity.OperID + " ");
                tmpa.Add("OperName='" + entity.OperName + "'");
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            if (entity.Status.ToString() == ReaBmsTransferDocStatus.移库完成.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsTransferDocStatus.已申请.Key && serverEntity.Status.ToString() != ReaBmsTransferDocStatus.审核通过.Key)
                {
                    return false;
                }

                if (!entity.CheckID.HasValue)
                {
                    entity.CheckID = empID;
                    entity.CheckName = empName;
                    tmpa.Add("CheckID=" + entity.CheckID + " ");
                    tmpa.Add("CheckName='" + entity.CheckName + "'");
                    tmpa.Add("CheckTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }

                entity.OperID = empID;
                entity.OperName = empName;
                tmpa.Add("OperID=" + entity.OperID + " ");
                tmpa.Add("OperName='" + entity.OperName + "'");
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            return true;
        }
        /// <summary>
        /// 移库申请单的移库完成处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        private BaseResultBool EditTransferDocAndDtlOfComp(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtlAddList, IList<ReaBmsTransferDtl> dtlEditList, bool isEmpTransfer, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();

            BaseResultDataValue brdv1 = AddValidDocAndDtl(dtlAddList, isEmpTransfer, empID, empName);
            brdv.success = brdv1.success;
            brdv.ErrorInfo = brdv1.ErrorInfo;
            if (!brdv1.success)
                return brdv;

            brdv1 = AddValidDocAndDtl(dtlEditList, isEmpTransfer, empID, empName);
            brdv.success = brdv1.success;
            brdv.ErrorInfo = brdv1.ErrorInfo;
            if (!brdv1.success)
                return brdv;

            //移库申请后的确认移库操作，保存前，对每个货品进行校验，有一个失败就不能做任何保存，直接返回失败信息。
            EditTransferDtlAndQtyDtl_SaveBeforeCheck(entity, dtlAddList, ref brdv1);
            brdv.success = brdv1.success;
            brdv.ErrorInfo = brdv1.ErrorInfo;
            if (!brdv1.success)
                return brdv;
            
            //移库申请后的确认移库操作，保存前，对每个货品进行校验，有一个失败就不能做任何保存，直接返回失败信息。
            EditTransferDtlAndQtyDtl_SaveBeforeCheck(entity, dtlEditList, ref brdv1);
            brdv.success = brdv1.success;
            brdv.ErrorInfo = brdv1.ErrorInfo;
            if (!brdv.success)
                return brdv;

            if (brdv.success && dtlAddList != null)
            {
                for (int j = 0; j < dtlAddList.Count; j++)
                {
                    if (brdv.success != true)
                        return brdv;
                    ReaBmsTransferDtl transferDtl = dtlAddList[j];
                    if (brdv.success)
                        EditTransferDtlAndQtyDtl(entity, ref transferDtl, ref brdv1);
                    brdv.success = brdv1.success;
                    brdv.ErrorInfo = brdv1.ErrorInfo;
                    if (brdv.success != true)
                        return brdv;
                    if (brdv.success)
                    {
                        transferDtl.SumTotal = transferDtl.GoodsQty * transferDtl.Price;
                        IBReaBmsTransferDtl.Entity = transferDtl;
                        brdv.success = IBReaBmsTransferDtl.Add();
                        if (brdv.success != true)
                        {
                            brdv.ErrorInfo = "货品为：" + transferDtl.GoodsCName + "移库失败,移库明细单保存失败！";
                            ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                        }
                    }
                    if (brdv.success != true)
                        return brdv;
                }
            }

            if (brdv.success && dtlEditList != null)
            {
                for (int j = 0; j < dtlEditList.Count; j++)
                {
                    ReaBmsTransferDtl transferDtl = dtlEditList[j];
                    if (brdv.success)
                        EditTransferDtlAndQtyDtl(entity, ref transferDtl, ref brdv1);
                    brdv.success = brdv1.success;
                    brdv.ErrorInfo = brdv1.ErrorInfo;
                    if (brdv.success)
                    {
                        transferDtl.SumTotal = transferDtl.GoodsQty * transferDtl.Price;
                        IBReaBmsTransferDtl.Entity = transferDtl;
                        transferDtl.DataTimeStamp = IBReaBmsTransferDtl.Get(transferDtl.Id).DataTimeStamp;
                        if (!IBReaBmsTransferDtl.Edit())
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "货品为：" + transferDtl.GoodsCName + "移库失败,移库明细单保存失败！";
                            ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                        }
                    }
                    if (!brdv.success)
                        return brdv;
                }
            }
            return brdv;
        }
        /// <summary>
        /// 添加移库操作记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        public void AddSCOperation(ReaBmsTransferDoc entity, long empID, string empName)
        {
            if (entity.Status.ToString() == ReaBmsTransferDocStatus.暂存.Key) return;

            SCOperation sco = new SCOperation();
            sco.LabID = entity.LabID;
            sco.BobjectID = entity.Id;
            sco.CreatorID = empID;
            if (empName != null && empName.Trim() != "")
                sco.CreatorName = empName;
            sco.BusinessModuleCode = "ReaBmsTransferDoc";
            if (!string.IsNullOrEmpty(entity.CheckMemo))
                sco.Memo = entity.CheckMemo;
            sco.IsUse = true;
            sco.Type = entity.Status;
            sco.DataUpdateTime = DateTime.Now;
            sco.TypeName = ReaBmsTransferDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }
        /// <summary>
        /// 获取移库总单号
        /// </summary>
        /// <returns></returns>
        private string GetTransferDocNo()
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
            ReaBmsTransferDoc transferDoc = this.Get(id);
            if (transferDoc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取移库单PDF清单数据信息为空!");
            }
            IList<ReaBmsTransferDtl> dtlList = IBReaBmsTransferDtl.SearchListByHQL("reabmstransferdtl.TransferDocID=" + id);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取移库单PDF清单明细信息为空!");
            }

            pdfFileName = transferDoc.TransferDocNo + ".pdf";
            //string milliseconds = "";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = SearchPdfReportOfFrxById(transferDoc, dtlList, frx);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                string excelFileFullDir = "";
                //获取移库单模板
                if (string.IsNullOrEmpty(frx))
                    frx = "移库清单.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = transferDoc.TransferDocNo.ToString() + fileExt;
                Stream stream2 = null;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
                stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsTransferDoc, ReaBmsTransferDtl>(transferDoc, dtlList, excelCommand, breportType, transferDoc.LabID, frx, excelFile, ref excelFileFullDir);
                stream2.Close();
                string pdfFullDir = "";

                bool result = ExcelToPdfReportHelp.ExcelToPDF(excelFileFullDir, breportType, transferDoc.LabID, pdfFileName, ref pdfFullDir);
                if (result)
                {
                    stream = PdfReportHelp.GetReportPDF(pdfFullDir);
                }
            }

            return stream;
        }
        private Stream SearchPdfReportOfFrxById(ReaBmsTransferDoc transferDoc, IList<ReaBmsTransferDtl> dtlList, string frx)
        {
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";

            List<ReaBmsTransferDoc> docList = new List<ReaBmsTransferDoc>();
            docList.Add(transferDoc);
            DataTable docDt = ReportBTemplateHelp.ToDataTable<ReaBmsTransferDoc>(docList, null);
            docDt.TableName = "Rea_BmsTransferDoc";
            dataSet.Tables.Add(docDt);

            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsTransferDtl>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "Rea_BmsTransferDtl";
                dataSet.Tables.Add(dtDtl);
            }
            //获取移库单Frx模板
            string pdfName = transferDoc.TransferDocNo.ToString() + ".pdf";
            //如果当前实验室还没有维护移库单报表模板,默认使用公共的移库单模板
            if (string.IsNullOrEmpty(frx))
                frx = "移库清单.frx";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, transferDoc.LabID, pdfName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.移库清单.Key].Name, frx, false);

            return stream;
        }
        public Stream SearchExcelReportOfExcelById(long id, long labID, string labCName, string breportType, string frx, ref string fileName)
        {
            Stream stream = null;
            ReaBmsTransferDoc transferDoc = this.Get(id);
            if (transferDoc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取移库单数据信息为空!");
            }
            IList<ReaBmsTransferDtl> dtlList = IBReaBmsTransferDtl.SearchListByHQL("reabmstransferdtl.TransferDocID=" + id);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取移库单明细信息为空!");
            }

            //获取移库单模板
            if (string.IsNullOrEmpty(frx))
                frx = "移库清单.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = transferDoc.TransferDocNo.ToString() + fileExt;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsTransferDoc, ReaBmsTransferDtl>(transferDoc, dtlList, excelCommand, breportType, transferDoc.LabID, frx, excelFile, ref saveFullPath);
            fileName = transferDoc.DeptName + "移库清单信息" + fileExt;
            return stream;
        }

        #endregion

        #region 移库入库
        public BaseResultDataValue AddTransferDocOfInDoc(ReaBmsInDoc inDoc, ReaBmsTransferDoc transferDoc, IList<ReaBmsTransferDtl> transferDtlList, bool isEmpTransfer, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv = AddValidTransferDocOfInDoc(inDoc, transferDoc, transferDtlList);
            if (!brdv.success)
            {
                return brdv;
            }

            //移库主单的源库房处理
            if (!transferDoc.SStorageID.HasValue)
            {
                transferDoc.SStorageID = transferDtlList[0].SStorageID;
                transferDoc.SStorageName = transferDtlList[0].SStorageName;
            }
            transferDoc.TotalPrice = transferDtlList.Sum(p => p.SumTotal);
            AddReaBmsTransferDoc(ref transferDoc, empID, empName, ref brdv);
            if (brdv.success)
            {
                for (int j = 0; j < transferDtlList.Count; j++)
                {
                    ReaBmsTransferDtl transferDtl = transferDtlList[j];
                    if (brdv.success)
                        AddTransferDtlAndQtyDtlOfInDoc(inDoc, transferDoc, transferDtl, empID, empName, ref brdv);
                    if (!brdv.success)
                        return brdv;
                }
            }
            if (brdv.success) AddSCOperation(transferDoc, empID, empName);
            return brdv;
        }
        private BaseResultDataValue AddValidTransferDocOfInDoc(ReaBmsInDoc inDoc, ReaBmsTransferDoc transferDoc, IList<ReaBmsTransferDtl> transferDtlList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (inDoc == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数inDoc为空!";
                return brdv;
            }
            if (transferDtlList == null || transferDtlList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数transferDtlList为空!";
                return brdv;
            }
            ReaBmsInDoc serverInDoc = IDReaBmsInDocDao.Get(inDoc.Id);
            if (serverInDoc == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取入库单信息为空!";
                return brdv;
            }

            //目的库房货架,源库房货架一致,不能移库
            //移库入库的移库货品的源库房必须相同且只能是一个
            string fristSStoragePlace = "";
            for (int i = 0; i < transferDtlList.Count; i++)
            {
                if (transferDtlList[i].GoodsQty <= 0)
                {
                    brdv.ErrorInfo = "货品名称:【" + transferDtlList[i].GoodsCName + "】移库存数小于0,不能移库!";
                    brdv.success = false;
                    break;
                }
                var sStorageID = transferDtlList[i].SStorageID;
                var sPlaceID = transferDtlList[i].SPlaceID;
                var dStorageID = transferDtlList[i].DStorageID;
                var dPlaceID = transferDtlList[i].DPlaceID;
                var dStoragePlace = dStorageID.ToString() + dPlaceID.ToString();
                var sStoragePlace = sStorageID.ToString() + sPlaceID.ToString();
                if (string.IsNullOrEmpty(fristSStoragePlace)) fristSStoragePlace = sStoragePlace;

                if (dStoragePlace == sStoragePlace)
                {
                    brdv.ErrorInfo = "货品名称:【" + transferDtlList[i].GoodsCName + "】的目的库房货架,源库房货架一致,不能移库!";
                    brdv.success = false;
                    break;
                }
                if (fristSStoragePlace != sStoragePlace)
                {
                    brdv.ErrorInfo = "移库入库的移库货品的源库房必须相同且只能是一个,不能移库!";
                    brdv.success = false;
                    break;
                }
            }
            return brdv;
        }
        private void AddTransferDtlAndQtyDtlOfInDoc(ReaBmsInDoc inDoc, ReaBmsTransferDoc transferDoc, ReaBmsTransferDtl transferDtl, long empID, string empName, ref BaseResultDataValue brdv)
        {
            //入库货品的库存信息
            ReaBmsQtyDtl sQtyDtl = IBReaBmsQtyDtl.Get(long.Parse(transferDtl.SQtyDtlID));
            if (sQtyDtl == null && sQtyDtl.GoodsQty <= 0)
            {
                ZhiFang.Common.Log.Log.Error("库存货品为:" + sQtyDtl.GoodsName + ",当前库存数为0,不能进行入库移库!");
                return;
            }
            //入库货品的库存盒条码信息
            IList<ReaGoodsBarcodeOperation> boxBarCodeList = new List<ReaGoodsBarcodeOperation>();
            if (sQtyDtl.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key))
            {
                StringBuilder barCodeHql = new StringBuilder();
                barCodeHql.Append("reagoodsbarcodeoperation.StorageID=" + transferDtl.SStorageID);
                barCodeHql.Append(" and reagoodsbarcodeoperation.GoodsID=" + transferDtl.GoodsID);
                barCodeHql.Append(" and reagoodsbarcodeoperation.LotNo='" + transferDtl.LotNo + "'");
                barCodeHql.Append(" and reagoodsbarcodeoperation.BDocNo='" + inDoc.InDocNo + "'");
                barCodeHql.Append(" and reagoodsbarcodeoperation.BDocID=" + inDoc.Id);
                barCodeHql.Append(" and reagoodsbarcodeoperation.BDtlID=" + sQtyDtl.InDtlID);
                barCodeHql.Append(" and reagoodsbarcodeoperation.QtyDtlID=" + sQtyDtl.Id);
                boxBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(barCodeHql.ToString());
            }
            ReaBmsQtyDtl addQtyDtl = new ReaBmsQtyDtl();
            addQtyDtl = EditCopyReaBmsQtyDtl(sQtyDtl, addQtyDtl, transferDtl);
            transferDtl.TransferDocID = transferDoc.Id;
            transferDtl.QtyDtlID = addQtyDtl.Id.ToString();
            _getAddTransferDtl(sQtyDtl, ref transferDtl);
            IBReaBmsTransferDtl.Entity = transferDtl;
            if (!IBReaBmsTransferDtl.Add())
            {
                brdv.success = false;
                brdv.ErrorInfo = "货品移库失败：移库明细单保存失败！";
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                return;
            }
            #region 库存记录
            //更新入库的库存货品库存数
            double curTransferQty2 = sQtyDtl.GoodsQty.Value;
            sQtyDtl.GoodsQty = 0;
            sQtyDtl.SumTotal = 0;
            IBReaBmsQtyDtl.Entity = sQtyDtl;
            brdv.success = IBReaBmsQtyDtl.Edit();
            if (!brdv.success)
            {
                brdv.success = false;
                brdv.ErrorInfo = "更新移库库存货品的库存数失败！";
                return;
            }
            //新增移库库存记录
            addQtyDtl.SumTotal = addQtyDtl.GoodsQty * addQtyDtl.Price;
            IBReaBmsQtyDtl.Entity = addQtyDtl;
            brdv.success = IBReaBmsQtyDtl.Add();
            if (!brdv.success)
            {
                brdv.success = false;
                brdv.ErrorInfo = "新增移库库存货品保存失败！";
                return;
            }
            #endregion
            //移库出库库存变化操作记录
            brdv = IBReaBmsQtyDtlOperation.AddReaBmsQtyDtlTransferOperation(transferDoc, transferDtl, sQtyDtl, curTransferQty2, long.Parse(ReaBmsQtyDtlOperationOperType.移库出库.Key));
            //移库入库库存变化操作记录
            brdv = IBReaBmsQtyDtlOperation.AddReaBmsQtyDtlTransferOperation(transferDoc, transferDtl, addQtyDtl, addQtyDtl.GoodsQty.Value, long.Parse(ReaBmsQtyDtlOperationOperType.移库入库.Key));
            //库存条码操作记录
            if (sQtyDtl.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key) && boxBarCodeList.Count > 0)
            {
                //移库出库
                AddReaGoodsBarcodeOperationOfInDoc(inDoc, transferDoc, transferDtl, sQtyDtl, boxBarCodeList, long.Parse(ReaGoodsBarcodeOperType.移库出库.Key), empID, empName, ref brdv);
                //移库入库
                AddReaGoodsBarcodeOperationOfInDoc(inDoc, transferDoc, transferDtl, addQtyDtl, boxBarCodeList, long.Parse(ReaGoodsBarcodeOperType.移库入库.Key), empID, empName, ref brdv);
            }
        }
        private void _getAddTransferDtl(ReaBmsQtyDtl sQtyDtl, ref ReaBmsTransferDtl transferDtl)
        {
            transferDtl.GoodsQty = sQtyDtl.GoodsQty.Value;
            transferDtl.GoodsID = sQtyDtl.GoodsID;
            transferDtl.BarCodeType = sQtyDtl.BarCodeType;
            transferDtl.ReaGoodsNo = sQtyDtl.ReaGoodsNo;
            transferDtl.CenOrgGoodsNo = sQtyDtl.CenOrgGoodsNo;
            transferDtl.GoodsUnit = sQtyDtl.GoodsUnit;
            transferDtl.UnitMemo = sQtyDtl.UnitMemo;
            transferDtl.GoodsCName = sQtyDtl.GoodsName;
            transferDtl.Price = sQtyDtl.Price.Value;
            transferDtl.SumTotal = transferDtl.Price * transferDtl.GoodsQty;
            transferDtl.LotNo = sQtyDtl.LotNo;
            transferDtl.ProdDate = sQtyDtl.ProdDate;
            transferDtl.InvalidDate = sQtyDtl.InvalidDate;
            transferDtl.GoodsSerial = sQtyDtl.GoodsSerial;
            transferDtl.LotSerial = sQtyDtl.LotSerial;
            transferDtl.LotQRCode = sQtyDtl.LotQRCode;
            transferDtl.SysLotSerial = sQtyDtl.SysLotSerial;
            transferDtl.GoodsLotID = sQtyDtl.GoodsLotID;
            transferDtl.ReaCompanyID = sQtyDtl.ReaCompanyID;
            transferDtl.CompGoodsLinkID = sQtyDtl.CompGoodsLinkID;
            transferDtl.ReaCompCode = sQtyDtl.ReaCompCode;
            transferDtl.ReaServerCompCode = sQtyDtl.ReaServerCompCode;
            transferDtl.ReaCompanyName = sQtyDtl.CompanyName;
            transferDtl.DataUpdateTime = DateTime.Now;
        }
        private void AddReaGoodsBarcodeOperationOfInDoc(ReaBmsInDoc inDoc, ReaBmsTransferDoc transferDoc, ReaBmsTransferDtl transferDtl, ReaBmsQtyDtl qtyDtl, IList<ReaGoodsBarcodeOperation> boxBarCodeList, long operTypeID, long empID, string empName, ref BaseResultDataValue brdv)
        {
            foreach (var boxBarCode in boxBarCodeList)
            {
                ReaGoodsBarcodeOperation addOperation = ClassMapperHelp.GetMapper<ReaGoodsBarcodeOperation, ReaGoodsBarcodeOperation>(boxBarCode);
                addOperation.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                addOperation.OperTypeID = operTypeID;
                addOperation.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
                addOperation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[addOperation.OperTypeID.ToString()].Name;

                if (operTypeID == long.Parse(ReaGoodsBarcodeOperType.移库入库.Key))
                {
                    addOperation.BDocID = transferDtl.TransferDocID;
                    addOperation.BDocNo = transferDoc.TransferDocNo;
                    addOperation.BDtlID = transferDtl.Id;
                    addOperation.StorageID = transferDtl.DStorageID;
                    addOperation.PlaceID = transferDtl.DPlaceID;
                    addOperation.QtyDtlID = qtyDtl.Id;
                }
                else if (operTypeID == long.Parse(ReaGoodsBarcodeOperType.移库出库.Key))
                {
                    addOperation.BDocID = inDoc.Id;
                    addOperation.BDocNo = inDoc.InDocNo;
                    //addOperation.BDtlID = inDtlQty.Id;
                    addOperation.QtyDtlID = qtyDtl.Id;
                    addOperation.ScanCodeQty = addOperation.OverageQty;
                    addOperation.OverageQty = 0;
                }
                BaseResultBool tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperation(addOperation, operTypeID, empID, empName, transferDtl.LabID);
                brdv.success = tempBaseResultBool.success;
                brdv.ErrorInfo = tempBaseResultBool.ErrorInfo;
                if (!brdv.success) return;
            }
        }
        #endregion

    }
}