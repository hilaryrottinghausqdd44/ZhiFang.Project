using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using System.Text;
using System.Collections.Generic;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn;
using System;
using System.Linq;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.ServiceCommon.RBAC;
using System.IO;
using ZhiFang.ReagentSys.Client.Common;
using System.Data;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using Newtonsoft.Json.Linq;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class BReaBmsInDtl : BaseBLL<ReaBmsInDtl>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsInDtl
    {
        IDCenOrgDao IDCenOrgDao { get; set; }
        IDReaCenOrgDao IDReaCenOrgDao { get; set; }
        IDReaStorageDao IDReaStorageDao { get; set; }
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IDReaBmsCenSaleDtlConfirmDao IDReaBmsCenSaleDtlConfirmDao { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        IBReaBmsQtyDtl IBReaBmsQtyDtl { get; set; }
        IBReaGoodsLot IBReaGoodsLot { get; set; }
        IBReaGoods IBReaGoods { get; set; }
        IBBDict IBBDict { get; set; }

        public BaseResultBool EditReaBmsInDtlListVOOfValid(IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            foreach (var vo in dtAddList)
            {
                var model = vo.ReaBmsInDtl;
                tempBaseResultBool = EditReaBmsInDtlValid(model, vo.ReaBmsInDtlLinkList, vo.EditReaBmsInDtlLinkList, codeScanningMode);
                if (tempBaseResultBool.success == false)
                {
                    ZhiFang.Common.Log.Log.Warn(tempBaseResultBool.ErrorInfo);
                    break;
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditReaBmsInDtlListOfValid(IList<ReaBmsInDtl> dtEditList, string codeScanningMode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            foreach (var model in dtEditList)
            {
                IList<ReaGoodsBarcodeOperation> editInDtlLinkList = IBReaGoodsBarcodeOperation.SearchListByHQL(string.Format(" reagoodsbarcodeoperation.BDtlID={0} and reagoodsbarcodeoperation.OperTypeID in({1},{2})", model.Id, int.Parse(ReaGoodsBarcodeOperType.验货入库.Key), int.Parse(ReaGoodsBarcodeOperType.库存初始化.Key)));
                tempBaseResultBool = EditReaBmsInDtlValid(model, null, editInDtlLinkList, codeScanningMode);
                if (tempBaseResultBool.success == false)
                {
                    ZhiFang.Common.Log.Log.Warn(tempBaseResultBool.ErrorInfo);
                    break;
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditsInDtlBasicValid(ReaBmsInDtl model)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;
            if (model.ReaGoods == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("货品信息为空，不能入库");
                return tempBaseResultBool;
            }
            if (!model.GoodsQty.HasValue || model.GoodsQty.Value < 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的本次入库数为空或小于零，不能入库", model.GoodsCName);
                return tempBaseResultBool;
            }
            if (!model.ReaCompanyID.HasValue)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的供应商ID为空，不能入库", model.GoodsCName);
                return tempBaseResultBool;
            }
            if (!model.StorageID.HasValue)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的存储库房ID为空，不能入库", model.GoodsCName);
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(model.ReaGoodsNo))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的产品编码为空，不能入库", model.GoodsCName);
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(model.LotNo))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的货品批号为空，不能入库", model.GoodsCName);
                return tempBaseResultBool;
            }
            if (!model.InvalidDate.HasValue)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的有效期至为空，不能入库", model.GoodsCName);
                return tempBaseResultBool;
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditReaBmsInDtlValid(ReaBmsInDtl model, IList<ReaGoodsBarcodeOperation> addInDtlLinkList, IList<ReaGoodsBarcodeOperation> editInDtlLinkList, string codeScanningMode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            #region 验证判断处理
            tempBaseResultBool = EditsInDtlBasicValid(model);
            if (tempBaseResultBool.success == false) return tempBaseResultBool;

            #region 验收入库
            if (model.SaleDtlConfirmID.HasValue)
            {
                IList<ReaBmsCenSaleDtlConfirm> listDtlConfirm = IDReaBmsCenSaleDtlConfirmDao.GetListByHQL(string.Format("reabmscensaledtlconfirm.Id={0}", model.SaleDtlConfirmID.Value));
                //验收明细的本次入库数>验收数量
                if (model.GoodsQty > listDtlConfirm[0].AcceptCount)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的本次入库数{1}大于验收数量{2}，不能入库！", model.GoodsCName, model.GoodsQty, listDtlConfirm[0].AcceptCount);
                    return tempBaseResultBool;
                }
                else
                {
                    IList<ReaBmsInDtl> listInDtl = this.SearchListByHQL(string.Format("reabmsindtl.SaleDtlConfirmID={0}", model.SaleDtlConfirmID.Value));
                    double inCount = 0;
                    if (listInDtl.Count > 0) inCount = listInDtl.Sum(p => p.GoodsQty.Value);
                    //验收明细的本次入库数+入库总数>验收数量
                    if ((inCount + model.GoodsQty) > listDtlConfirm[0].AcceptCount)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的本次入库数为{1}+已入库总数{2}等于{3},大于验收数量{4}，不能入库！", model.GoodsCName, model.GoodsQty, inCount, (inCount + model.GoodsQty), listDtlConfirm[0].AcceptCount);
                        return tempBaseResultBool;
                    }
                }
            }
            #endregion

            #region 盒条码处理
            if (tempBaseResultBool.success == true && model.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key))
            {
                int count = 0;
                if (addInDtlLinkList != null) count = addInDtlLinkList.Count;
                if (editInDtlLinkList != null) count = count + editInDtlLinkList.Count;

                if (model.GoodsQty < count)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的入库数为{1}小于扫码记录数{2},不能入库！", model.GoodsCName, model.GoodsQty, addInDtlLinkList.Count);
                    return tempBaseResultBool;
                }
                if (codeScanningMode == "strict" && addInDtlLinkList == null && editInDtlLinkList == null)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的扫码操作记录为空,在严格扫码模式下不能入库！", model.GoodsCName);
                    return tempBaseResultBool;
                }
                //验证本次入库新增的扫码操作记录是否已入库(验货入库,库存初始化)
                if (tempBaseResultBool.success == true && addInDtlLinkList != null)
                {
                    string hql = "";
                    string hql2 = string.Format("reagoodsbarcodeoperation.OperTypeID in ({0},{1})", ReaGoodsBarcodeOperType.验货入库.Key, ReaGoodsBarcodeOperType.库存初始化.Key);
                    foreach (ReaGoodsBarcodeOperation operation in addInDtlLinkList)
                    {
                        hql = "";
                        if (!string.IsNullOrEmpty(operation.UsePackSerial))
                        {
                            hql = string.Format("({0} and reagoodsbarcodeoperation.UsePackSerial='{1}')", hql2, operation.UsePackSerial);
                        }
                        if (!string.IsNullOrEmpty(operation.UsePackQRCode))
                        {
                            string hql3 = string.Format("({0} and reagoodsbarcodeoperation.UsePackQRCode='{1}')", hql2, operation.UsePackQRCode);
                            if (string.IsNullOrEmpty(hql))
                            {
                                hql = hql3;
                            }
                            else
                            {
                                hql = hql + " or " + hql3;
                            }
                        }
                        hql = "(" + hql + ")";
                        IList<ReaGoodsBarcodeOperation> dtlBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(hql);
                        if (dtlBarCodeList.Count > 0)
                        {
                            tempBaseResultBool.success = false;
                            string serialNo = "";
                            if (!string.IsNullOrEmpty(operation.UsePackSerial))
                            {
                                serialNo = operation.UsePackSerial;
                            }
                            else
                            {
                                serialNo = operation.UsePackQRCode;
                            }
                            tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的入库条码值为{1},已被扫码入库，不能重复扫码入库！", model.GoodsCName, serialNo);
                            break;
                        }
                    }
                    if (tempBaseResultBool.success == false) return tempBaseResultBool;
                }
            }
            #endregion
            #endregion
            return tempBaseResultBool;
        }
        public BaseResultDataValue AddReaBmsInDtlList(ReaBmsInDoc inDoc, IList<ReaBmsInDtl> dtAddList, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };

            IList<CenOrg> cenOrgList = IDCenOrgDao.GetListByHQL("cenorg.LabID=" + inDoc.LabID.ToString());
            foreach (var model in dtAddList)
            {
                model.InDocNo = inDoc.InDocNo;
                model.InDocID = inDoc.Id;
                model.Visible = true;
                if (string.IsNullOrEmpty(model.InDtlNo))
                    model.InDtlNo = GetInDtlNo();
                model.CreaterID = empID;
                model.CreaterName = empName;
                model.DataUpdateTime = DateTime.Now;
                if (model.ReaGoods.DataTimeStamp == null)
                    model.ReaGoods.DataTimeStamp = dataTimeStamp;
                if ((string.IsNullOrEmpty(model.ProdGoodsNo) || string.IsNullOrEmpty(model.CenOrgGoodsNo)) && model.CompGoodsLinkID.HasValue)
                {
                    ReaGoodsOrgLink goodsOrgLink = IDReaGoodsOrgLinkDao.Get(model.CompGoodsLinkID.Value);
                    if (goodsOrgLink != null)
                    {
                        model.CenOrgGoodsNo = goodsOrgLink.CenOrgGoodsNo;
                        model.ProdGoodsNo = goodsOrgLink.ReaGoods.ProdGoodsNo;
                        model.GoodsNo = goodsOrgLink.ReaGoods.GoodsNo;
                        if (string.IsNullOrEmpty(model.UnitMemo))
                            model.UnitMemo = goodsOrgLink.ReaGoods.UnitMemo;
                    }
                }
                if (string.IsNullOrEmpty(model.UnitMemo))
                {
                    model.UnitMemo = IBReaGoods.Get(model.ReaGoods.Id).UnitMemo;
                }
                if (model.SumTotal <= 0) model.SumTotal = model.Price * model.GoodsQty;
                this.Entity = model;
                ReaGoodsLot reaGoodsLot = null;
                AddReaGoodsLot(ref reaGoodsLot, empID, empName);
                if (reaGoodsLot != null)
                    this.Entity.GoodsLotID = reaGoodsLot.Id;
                baseResultDataValue.success = this.Add();
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "保存入库明细信息失败!";
                }
                if (baseResultDataValue.success == false) break;

                //库存处理(入库单状态为暂存时不新增库存信息)
                if (inDoc.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
                {
                    long operTypeID = GetOperTypeID(inDoc.InType.Value);
                    ReaBmsQtyDtl addQtyDtl = new ReaBmsQtyDtl();
                    BaseResultBool tempBaseResultBool = IBReaBmsQtyDtl.AddReaBmsQtyDtl(model, cenOrgList[0], operTypeID, empID, empName, ref addQtyDtl);
                    baseResultDataValue.success = tempBaseResultBool.success;
                    baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                }

                if (baseResultDataValue.success == false) break;
            }
            return baseResultDataValue;
        }
        public BaseResultBool AddReaBmsInDtlOfVO(ReaBmsInDoc inDoc, IList<ReaBmsInDtlVO> dtAddList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };

            IList<CenOrg> cenOrgList = IDCenOrgDao.GetListByHQL("cenorg.LabID=" + inDoc.LabID.ToString());
            foreach (var vo in dtAddList)
            {
                var model = vo.ReaBmsInDtl;
                model.InDocNo = inDoc.InDocNo;
                model.InDocID = inDoc.Id;
                model.InDtlNo = GetInDtlNo();
                model.Visible = true;
                model.CreaterID = empID;
                model.CreaterName = empName;
                model.DataUpdateTime = DateTime.Now;

                if (model.ReaGoods.DataTimeStamp == null)
                    model.ReaGoods.DataTimeStamp = dataTimeStamp;

                if ((string.IsNullOrEmpty(model.ProdGoodsNo) || string.IsNullOrEmpty(model.CenOrgGoodsNo)) && model.CompGoodsLinkID.HasValue)
                {
                    ReaGoodsOrgLink goodsOrgLink = IDReaGoodsOrgLinkDao.Get(model.CompGoodsLinkID.Value);
                    if (goodsOrgLink != null)
                    {
                        model.CenOrgGoodsNo = goodsOrgLink.CenOrgGoodsNo;
                        model.ProdGoodsNo = goodsOrgLink.ReaGoods.ProdGoodsNo;
                        model.GoodsNo = goodsOrgLink.ReaGoods.GoodsNo;
                        if (string.IsNullOrEmpty(model.UnitMemo))
                            model.UnitMemo = goodsOrgLink.ReaGoods.UnitMemo;
                    }
                }

                if (string.IsNullOrEmpty(model.UnitMemo) && model.ReaGoods != null)
                {
                    ReaGoods reaGoods = IBReaGoods.Get(model.ReaGoods.Id);
                    if (reaGoods != null)
                        model.UnitMemo = IBReaGoods.Get(reaGoods.Id).UnitMemo;
                }
                if (model.SumTotal <= 0) model.SumTotal = model.Price * model.GoodsQty;
                this.Entity = model;
                ReaGoodsLot reaGoodsLot = null;
                AddReaGoodsLot(ref reaGoodsLot, empID, empName);
                if (reaGoodsLot != null)
                    this.Entity.GoodsLotID = reaGoodsLot.Id;
                tempBaseResultBool.success = this.Add();
                if (tempBaseResultBool.success == false)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "保存入库明细信息失败!";
                }
                if (tempBaseResultBool.success == false) break;

                //库存处理(入库单状态为暂存时不新增库存信息)
                if (inDoc.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
                {
                    long operTypeID = GetOperTypeID(inDoc.InType.Value);
                    ReaBmsQtyDtl addQtyDtl = new ReaBmsQtyDtl();
                    tempBaseResultBool = IBReaBmsQtyDtl.AddReaBmsQtyDtl(model, cenOrgList[0], operTypeID, empID, empName, ref addQtyDtl);
                    if (vo.ReaBmsInDtlLinkList != null)
                    {
                        long operTypeID1 = GetBarcodeOperTypeID(inDoc.InType.Value);
                        tempBaseResultBool = AddBarcodeOperationOfList(model, addQtyDtl, vo.ReaBmsInDtlLinkList, operTypeID1, empID, empName);
                    }
                }
                else
                {
                    //新增的条码扫码操作记录保存
                    if (vo.ReaBmsInDtlLinkList != null)
                    {
                        long operTypeID1 = GetBarcodeOperTypeID(inDoc.InType.Value);
                        tempBaseResultBool = AddBarcodeOperationOfList(model, null, vo.ReaBmsInDtlLinkList, operTypeID1, empID, empName);
                    }
                }

                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        public BaseResultBool AddReaBmsInDtlOfVOByInterface(ReaBmsInDoc inDoc, IList<ReaBmsInDtlVO> dtAddList, string iSNeedCreateBarCode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };

            IList<CenOrg> cenOrgList = IDCenOrgDao.GetListByHQL("cenorg.LabID=" + inDoc.LabID.ToString());
            foreach (var vo in dtAddList)
            {
                var model = vo.ReaBmsInDtl;
                model.InDocNo = inDoc.InDocNo;
                model.InDocID = inDoc.Id;
                model.InDtlNo = GetInDtlNo();
                model.Visible = true;
                model.CreaterID = empID;
                model.CreaterName = empName;
                model.DataUpdateTime = DateTime.Now;

                if (model.ReaGoods.DataTimeStamp == null)
                    model.ReaGoods.DataTimeStamp = dataTimeStamp;

                if ((string.IsNullOrEmpty(model.ProdGoodsNo) || string.IsNullOrEmpty(model.CenOrgGoodsNo)) && model.CompGoodsLinkID.HasValue)
                {
                    ReaGoodsOrgLink goodsOrgLink = IDReaGoodsOrgLinkDao.Get(model.CompGoodsLinkID.Value);
                    if (goodsOrgLink != null)
                    {
                        model.CenOrgGoodsNo = goodsOrgLink.CenOrgGoodsNo;
                        model.ProdGoodsNo = goodsOrgLink.ReaGoods.ProdGoodsNo;
                        model.GoodsNo = goodsOrgLink.ReaGoods.GoodsNo;
                        if (string.IsNullOrEmpty(model.UnitMemo))
                            model.UnitMemo = goodsOrgLink.ReaGoods.UnitMemo;
                    }
                }

                if (string.IsNullOrEmpty(model.UnitMemo) && model.ReaGoods != null)
                {
                    ReaGoods reaGoods = IBReaGoods.Get(model.ReaGoods.Id);
                    if (reaGoods != null)
                        model.UnitMemo = IBReaGoods.Get(reaGoods.Id).UnitMemo;
                }
                if (model.SumTotal <= 0) model.SumTotal = model.Price * model.GoodsQty;
                this.Entity = model;
                ReaGoodsLot reaGoodsLot = null;
                AddReaGoodsLot(ref reaGoodsLot, empID, empName);
                if (reaGoodsLot != null)
                    this.Entity.GoodsLotID = reaGoodsLot.Id;
                tempBaseResultBool.success = this.Add();
                if (tempBaseResultBool.success == false)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "保存入库明细信息失败!";
                }
                if (tempBaseResultBool.success == false) break;

                //库存处理(入库单状态为暂存时不新增库存信息)
                if (inDoc.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
                {
                    long operTypeID = GetOperTypeID(inDoc.InType.Value);
                    ReaBmsQtyDtl addQtyDtl = new ReaBmsQtyDtl();
                    //tempBaseResultBool = IBReaBmsQtyDtl.AddReaBmsQtyDtl(model, cenOrgList[0], operTypeID, empID, empName, ref addQtyDtl);
                    tempBaseResultBool = IBReaBmsQtyDtl.AddReaBmsQtyDtlByInterface(inDoc, model, cenOrgList[0], iSNeedCreateBarCode, operTypeID, empID, empName, ref addQtyDtl);
                    if (vo.ReaBmsInDtlLinkList != null)
                    {
                        long operTypeID1 = GetBarcodeOperTypeID(inDoc.InType.Value);
                        tempBaseResultBool = AddBarcodeOperationOfList(model, addQtyDtl, vo.ReaBmsInDtlLinkList, operTypeID1, empID, empName);
                    }
                }
                else
                {
                    //新增的条码扫码操作记录保存
                    if (vo.ReaBmsInDtlLinkList != null)
                    {
                        long operTypeID1 = GetBarcodeOperTypeID(inDoc.InType.Value);
                        tempBaseResultBool = AddBarcodeOperationOfList(model, null, vo.ReaBmsInDtlLinkList, operTypeID1, empID, empName);
                    }
                }

                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        private long GetOperTypeID(long inType)
        {
            long operTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.库存初始化.Key);
            if (inType == int.Parse(ReaBmsInDocInType.验货入库.Key))
            {
                operTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.验货入库.Key);
            }
            else if (inType == int.Parse(ReaBmsInDocInType.盘盈入库.Key))
            {
                operTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.盘盈入库.Key);
            }
            else if (inType == int.Parse(ReaBmsInDocInType.退库入库.Key))
            {
                operTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.退库入库.Key);
            }
            else if (inType == int.Parse(ReaBmsInDocInType.借调入库.Key))
            {
                operTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.借调入库.Key);
            }
            return operTypeID;
        }
        private long GetBarcodeOperTypeID(long inType)
        {
            long operTypeID = long.Parse(ReaGoodsBarcodeOperType.库存初始化.Key);
            if (inType == int.Parse(ReaBmsInDocInType.验货入库.Key))
            {
                operTypeID = long.Parse(ReaGoodsBarcodeOperType.验货入库.Key);
            }
            else if (inType == int.Parse(ReaBmsInDocInType.盘盈入库.Key))
            {
                operTypeID = long.Parse(ReaGoodsBarcodeOperType.盘盈入库.Key);
            }
            else if (inType == int.Parse(ReaBmsInDocInType.退库入库.Key))
            {
                operTypeID = long.Parse(ReaGoodsBarcodeOperType.退库入库.Key);
            }
            else if (inType == int.Parse(ReaBmsInDocInType.借调入库.Key))
            {
                operTypeID = long.Parse(ReaGoodsBarcodeOperType.借调入库.Key);
            }
            return operTypeID;
        }
        public BaseResultBool EditReaBmsInDtlOfVO(ReaBmsInDoc inDoc, IList<ReaBmsInDtlVO> dtAddList, IList<ReaBmsInDtlVO> dtEditList, string fieldsDtl, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;

            if (dtAddList != null && dtAddList.Count > 0)
                tempBaseResultBool = AddReaBmsInDtlOfVO(inDoc, dtAddList, empID, empName);
            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;
            if (dtEditList == null || dtEditList.Count <= 0)
                return tempBaseResultBool;

            IList<CenOrg> cenOrgList = IDCenOrgDao.GetListByHQL("cenorg.LabID=" + inDoc.LabID.ToString());

            foreach (var vo in dtEditList)
            {
                var model = vo.ReaBmsInDtl;
                try
                {
                    model.DataUpdateTime = DateTime.Now;
                    model.InDocID = inDoc.Id;
                    model.Visible = true;
                    this.Entity = model;
                    string[] tempDtlArray = CommonServiceMethod.GetUpdateFieldValueStr(this.Entity, fieldsDtl);
                    tempBaseResultBool.success = this.Update(tempDtlArray);
                    //tempBaseResultBool.success = this.Edit();
                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "更新库存初始化信息错误!";
                        break;
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.ErrorInfo = "更新库存初始化信息错误!";
                    ZhiFang.Common.Log.Log.Error("更新库存初始化信息错误!" + ex.Message);
                }

                //库存处理(入库单状态为暂存时不新增库存信息)
                if (inDoc.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
                {
                    long operTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.验货入库.Key);
                    if (inDoc.InType.Value == int.Parse(ReaBmsInDocInType.库存初始化.Key))
                        operTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.库存初始化.Key);
                    ReaBmsQtyDtl addQtyDtl = new ReaBmsQtyDtl();
                    tempBaseResultBool = IBReaBmsQtyDtl.AddReaBmsQtyDtl(model, cenOrgList[0], operTypeID, empID, empName, ref addQtyDtl);
                    if (vo.ReaBmsInDtlLinkList != null && vo.ReaBmsInDtlLinkList.Count > 0)
                    {
                        long operTypeID1 = long.Parse(ReaGoodsBarcodeOperType.验货入库.Key);
                        if (inDoc.InType.Value == int.Parse(ReaBmsInDocInType.库存初始化.Key))
                            operTypeID1 = long.Parse(ReaGoodsBarcodeOperType.库存初始化.Key);
                        tempBaseResultBool = AddBarcodeOperationOfList(model, addQtyDtl, vo.ReaBmsInDtlLinkList, operTypeID1, empID, empName);
                    }
                }
                else
                {
                    //新增的条码扫码操作记录保存
                    if (vo.ReaBmsInDtlLinkList != null && vo.ReaBmsInDtlLinkList.Count > 0)
                    {
                        long operTypeID1 = long.Parse(ReaGoodsBarcodeOperType.验货入库.Key);
                        if (inDoc.InType.Value == int.Parse(ReaBmsInDocInType.库存初始化.Key))
                            operTypeID1 = long.Parse(ReaGoodsBarcodeOperType.库存初始化.Key);
                        tempBaseResultBool = AddBarcodeOperationOfList(model, null, vo.ReaBmsInDtlLinkList, operTypeID1, empID, empName);
                    }
                }
                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditReaBmsInDtlOfVOByInterface(ReaBmsInDoc inDoc, IList<ReaBmsInDtlVO> dtEditList, string iSNeedCreateBarCode, string fieldsDtl, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;

            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;
            if (dtEditList == null || dtEditList.Count <= 0)
                return tempBaseResultBool;
            IList<CenOrg> cenOrgList = IDCenOrgDao.GetListByHQL("cenorg.LabID=" + inDoc.LabID.ToString());
            foreach (var vo in dtEditList)
            {
                var model = vo.ReaBmsInDtl;
                try
                {
                    model.DataUpdateTime = DateTime.Now;
                    model.InDocID = inDoc.Id;
                    if (string.IsNullOrEmpty(model.InDocNo))
                    {
                        model.InDocNo = inDoc.InDocNo;
                    }
                    model.Visible = true;
                    this.Entity = model;
                    string[] tempDtlArray = CommonServiceMethod.GetUpdateFieldValueStr(this.Entity, fieldsDtl);
                    tempBaseResultBool.success = this.Update(tempDtlArray);
                    //tempBaseResultBool.success = this.Edit();
                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "更新入库信息错误!";
                        break;
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.ErrorInfo = "更新入库信息错误!";
                    ZhiFang.Common.Log.Log.Error("更新入库信息错误!" + ex.Message);
                }
                //库存处理(入库单状态为暂存时不新增库存信息)
                if (inDoc.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
                {
                    long operTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.验货入库.Key);
                    ReaBmsQtyDtl addQtyDtl = new ReaBmsQtyDtl();
                    tempBaseResultBool = IBReaBmsQtyDtl.AddReaBmsQtyDtlByInterface(inDoc, model, cenOrgList[0], iSNeedCreateBarCode, operTypeID, empID, empName, ref addQtyDtl);
                    if (vo.ReaBmsInDtlLinkList != null && vo.ReaBmsInDtlLinkList.Count > 0)
                    {
                        long operTypeID1 = long.Parse(ReaGoodsBarcodeOperType.验货入库.Key);
                        tempBaseResultBool = AddBarcodeOperationOfList(model, addQtyDtl, vo.ReaBmsInDtlLinkList, operTypeID1, empID, empName);
                    }
                }
                else
                {
                    //新增的条码扫码操作记录保存
                    if (vo.ReaBmsInDtlLinkList != null && vo.ReaBmsInDtlLinkList.Count > 0)
                    {
                        long operTypeID1 = long.Parse(ReaGoodsBarcodeOperType.验货入库.Key);
                        tempBaseResultBool = AddBarcodeOperationOfList(model, null, vo.ReaBmsInDtlLinkList, operTypeID1, empID, empName);
                    }
                }
                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditReaBmsInDtl(ReaBmsInDoc inDoc, IList<ReaBmsInDtl> dtEditList, bool isEditDtl, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;

            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;

            if (dtEditList == null || dtEditList.Count <= 0)
                return tempBaseResultBool;

            IList<CenOrg> cenOrgList = IDCenOrgDao.GetListByHQL("cenorg.LabID=" + inDoc.LabID.ToString());
            foreach (var model in dtEditList)
            {
                model.DataUpdateTime = DateTime.Now;
                this.Entity = model;
                if (isEditDtl == true)
                    tempBaseResultBool.success = this.Edit();
                if (tempBaseResultBool.success == false) break;

                //库存处理(入库单状态为暂存时不新增库存信息)
                if (inDoc.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
                {
                    long operTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.验货入库.Key);
                    if (inDoc.InType.Value == int.Parse(ReaBmsInDocInType.库存初始化.Key))
                        operTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.库存初始化.Key);
                    ReaBmsQtyDtl addQtyDtl = new ReaBmsQtyDtl();
                    tempBaseResultBool = IBReaBmsQtyDtl.AddReaBmsQtyDtl(model, cenOrgList[0], operTypeID, empID, empName, ref addQtyDtl);
                }
                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditReaBmsInDtlByInterface(ReaBmsInDoc inDoc, IList<ReaBmsInDtl> dtEditList, bool isEditDtl, string iSNeedCreateBarCode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;

            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;
            if (dtEditList == null || dtEditList.Count <= 0)
                return tempBaseResultBool;
            IList<CenOrg> cenOrgList = IDCenOrgDao.GetListByHQL("cenorg.LabID=" + inDoc.LabID.ToString());
            foreach (var model in dtEditList)
            {
                model.DataUpdateTime = DateTime.Now;
                this.Entity = model;
                if (isEditDtl == true)
                    tempBaseResultBool.success = this.Edit();
                if (tempBaseResultBool.success == false) break;

                //库存处理(入库单状态为暂存时不新增库存信息)
                if (inDoc.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
                {
                    long operTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.验货入库.Key);
                    if (inDoc.InType.Value == int.Parse(ReaBmsInDocInType.库存初始化.Key))
                        operTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.库存初始化.Key);
                    ReaBmsQtyDtl addQtyDtl = new ReaBmsQtyDtl();
                    tempBaseResultBool = IBReaBmsQtyDtl.AddReaBmsQtyDtlByInterface(inDoc, model, cenOrgList[0], iSNeedCreateBarCode, operTypeID, empID, empName, ref addQtyDtl);
                }
                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        public BaseResultBool AddBarcodeOperationOfList(ReaBmsInDtl inDtl, ReaBmsQtyDtl addQtyDtl, IList<ReaGoodsBarcodeOperation> dtAddList, long operTypeID, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtAddList == null || dtAddList.Count <= 0) return tempBaseResultBool;

            foreach (ReaGoodsBarcodeOperation barcode in dtAddList)
            {
                barcode.LabID = inDtl.LabID;
                barcode.BDocID = inDtl.InDocID;
                barcode.BDocNo = inDtl.InDocNo;
                barcode.BDtlID = inDtl.Id;
                barcode.ReaCompanyID = inDtl.ReaCompanyID;
                barcode.CompanyName = inDtl.CompanyName;
                barcode.ReaCompCode = inDtl.ReaCompCode;
                barcode.GoodsLotID = inDtl.GoodsLotID;
                barcode.LotNo = inDtl.LotNo;
                barcode.OperTypeID = operTypeID;
                barcode.ReaCompCode = inDtl.ReaCompCode;
                barcode.CompGoodsLinkID = inDtl.CompGoodsLinkID;
                barcode.BarCodeType = (int)inDtl.BarCodeType;
                barcode.StorageID = inDtl.StorageID;
                barcode.PlaceID = inDtl.PlaceID;

                barcode.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
                barcode.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[barcode.OperTypeID.ToString()].Name;
                barcode.GoodsID = inDtl.ReaGoods.Id;
                //barcode.ScanCodeGoodsID = inDtl.GoodsID;
                if (!barcode.ScanCodeGoodsID.HasValue)
                    barcode.ScanCodeGoodsID = inDtl.ReaGoods.Id;
                barcode.GoodsCName = inDtl.GoodsCName;
                barcode.GoodsUnit = inDtl.GoodsUnit;
                barcode.GoodsSort = inDtl.GoodsSort;
                barcode.UnitMemo = inDtl.UnitMemo;
                barcode.ReaGoodsNo = inDtl.ReaGoodsNo;
                barcode.ProdGoodsNo = inDtl.ProdGoodsNo;
                barcode.CenOrgGoodsNo = inDtl.CenOrgGoodsNo;
                barcode.GoodsNo = inDtl.GoodsNo;
                if (string.IsNullOrEmpty(barcode.UsePackQRCode))
                    barcode.UsePackQRCode = barcode.UsePackSerial;
                if (addQtyDtl != null)
                {
                    barcode.QtyDtlID = addQtyDtl.Id;
                }
                if (!barcode.GoodsQty.HasValue)
                    barcode.GoodsQty = inDtl.GoodsQty;
                if (!barcode.MinBarCodeQty.HasValue)
                {
                    ReaGoods reaGoods = IBReaGoods.Get(inDtl.ReaGoods.Id);
                    double gonvertQty = 1;
                    if (reaGoods != null) gonvertQty = reaGoods.GonvertQty;
                    if (gonvertQty <= 0)
                    {
                        ZhiFang.Common.Log.Log.Warn("货品编码为:" + barcode.ReaGoodsNo + ",货品名称为:" + barcode.GoodsCName + ",货品包装单位的换算系数值为" + gonvertQty + ",维护不合理!");
                        gonvertQty = 1;
                    }
                    barcode.MinBarCodeQty = gonvertQty;
                }
                if (barcode.MinBarCodeQty <= 0) barcode.MinBarCodeQty = 1;
                barcode.OverageQty = barcode.MinBarCodeQty;
                if (!barcode.ScanCodeQty.HasValue)
                    barcode.ScanCodeQty = 1;
                if (string.IsNullOrEmpty(barcode.ScanCodeGoodsUnit))
                    barcode.ScanCodeGoodsUnit = barcode.GoodsUnit;
                if (string.IsNullOrEmpty(barcode.PUsePackSerial))
                    barcode.PUsePackSerial = barcode.Id.ToString();

            }
            tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperationOfList(dtAddList, 0, empID, empName, inDtl.LabID);
            return tempBaseResultBool;
        }
        /// <summary>
        /// 入库明细单号
        /// </summary>
        /// <returns></returns>
        private string GetInDtlNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        public EntityList<ReaBmsInDtlVO> SearchReaBmsInDtlVOByHQL(string strHqlWhere, string order, int page, int limit)
        {
            EntityList<ReaBmsInDtlVO> entityVOList = new EntityList<ReaBmsInDtlVO>();
            entityVOList.list = new List<ReaBmsInDtlVO>();

            EntityList<ReaBmsInDtl> el = this.SearchListByHQL(strHqlWhere, order, page, limit);

            IList<ReaBmsInDtlVO> tempDtlConfirmVOList = new List<ReaBmsInDtlVO>();
            foreach (var model in el.list)
            {
                ReaBmsInDtlVO vo = new ReaBmsInDtlVO();
                vo.ReaBmsInDtl = model;
                vo.BarCodeType = (int)vo.ReaBmsInDtl.BarCodeType;
                if (model.BarCodeType == 1)
                {
                    //当前入库明细的所有入库扫码记录集合(验货入库,库存初始化)
                    IList<ReaGoodsBarcodeOperation> dtlBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(string.Format("reagoodsbarcodeoperation.BDtlID={0} and reagoodsbarcodeoperation.OperTypeID in({1},{2})", vo.ReaBmsInDtl.Id, int.Parse(ReaGoodsBarcodeOperType.验货入库.Key), int.Parse(ReaGoodsBarcodeOperType.库存初始化.Key)));
                    ParseObjectProperty tempParseObjectProperty2 = new ParseObjectProperty();
                    vo.CurReaBmsInDtlLinkListStr = tempParseObjectProperty2.GetObjectPropertyNoPlanish(dtlBarCodeList);
                }
                tempDtlConfirmVOList.Add(vo);
            }
            entityVOList.count = tempDtlConfirmVOList.Count;
            entityVOList.list = tempDtlConfirmVOList;
            return entityVOList;
        }
        public void AddReaGoodsLot(ref ReaGoodsLot reaGoodsLot, long empID, string empName)
        {
            if (this.Entity == null) return;
            if (string.IsNullOrEmpty(this.Entity.LotNo)) return;
            if (this.Entity.ReaGoods == null) return;
            if (!this.Entity.InvalidDate.HasValue) return;

            ReaGoodsLot lot = new ReaGoodsLot();
            lot.LabID = this.Entity.LabID;
            lot.Visible = true;
            lot.GoodsID = this.Entity.ReaGoods.Id;
            lot.ReaGoodsNo = this.Entity.ReaGoodsNo;
            lot.LotNo = this.Entity.LotNo;
            lot.ProdDate = this.Entity.ProdDate;
            lot.GoodsCName = this.Entity.GoodsCName;
            lot.InvalidDate = this.Entity.InvalidDate;
            lot.CreaterID = empID;
            lot.CreaterName = empName;
            IBReaGoodsLot.Entity = lot;
            BaseResultBool baseResultBool = IBReaGoodsLot.AddAndValid(ref reaGoodsLot);
            //if (!baseResultBool.success)
            //ZhiFang.Common.Log.Log.Error("货品入库保存货品批号信息结果:" + baseResultBool.ErrorInfo);
        }
        /// <summary>
        /// 通过货品条码从入库明细表获取对应的入库货品ID
        /// 每次整表扫描会影响效率
        /// </summary>
        /// <param name="serialNo">货品条码</param>
        /// <returns></returns>
        public IList<string> SearchGoodsIDListByGoodsSerialNo(string serialNo)
        {
            IList<string> listGoodsID = new List<string>();
            if (!string.IsNullOrEmpty(serialNo))
            {
                string strWhere = " reabmsindtl.GoodsQty>0" + " and reabmsindtl.GoodsSerial=\'" + serialNo + "\'";
                IList<ReaBmsInDtl> listReaBmsInDtl = this.SearchListByHQL(strWhere);
                if (listReaBmsInDtl != null && listReaBmsInDtl.Count > 0)
                {
                    listGoodsID = listReaBmsInDtl.GroupBy(p => new
                    {
                        p.ReaGoods.Id,
                        p.GoodsSerial,
                    }).Select(g => g.Key.Id.ToString()).ToList();
                }
            }
            return listGoodsID;
        }
        public ReaBmsInDtl SearchReaBmsInDtlByReaBmsOutDtl(ReaBmsInDoc inDoc, ReaBmsOutDtl outDtl, double goodsQty, long empID, string empName)
        {
            ReaBmsInDtl inDtl = new ReaBmsInDtl();
            inDtl.InDtlNo = this.GetInDtlNo();
            inDtl.InDocID = inDoc.Id;
            inDtl.InDocNo = inDoc.InDocNo;
            inDtl.ReaGoods = IBReaGoods.Get((long)outDtl.GoodsID);
            inDtl.GoodsCName = outDtl.GoodsCName;
            inDtl.GoodsUnitID = outDtl.GoodsUnitID;
            inDtl.GoodsUnit = outDtl.GoodsUnit;
            inDtl.UnitMemo = outDtl.UnitMemo;
            inDtl.GoodsQty = goodsQty;
            inDtl.Price = outDtl.Price;
            inDtl.SumTotal = outDtl.GoodsQty * outDtl.Price;
            inDtl.TaxRate = outDtl.TaxRate;
            inDtl.GoodsLotID = outDtl.GoodsLotID;
            inDtl.LotNo = outDtl.LotNo;
            inDtl.StorageID = outDtl.StorageID;
            inDtl.PlaceID = outDtl.PlaceID;
            inDtl.Visible = true;
            inDtl.CreaterID = empID;
            inDtl.CreaterName = empName;
            inDtl.DataAddTime = DateTime.Now;
            inDtl.DataUpdateTime = DateTime.Now;
            inDtl.StorageName = outDtl.StorageName;
            inDtl.PlaceName = outDtl.PlaceName;
            inDtl.ReaCompanyID = outDtl.ReaCompanyID;
            inDtl.CompanyName = outDtl.CompanyName;
            inDtl.ProdDate = outDtl.ProdDate;
            inDtl.InvalidDate = outDtl.InvalidDate;
            inDtl.RegisterInvalidDate = inDtl.ReaGoods.RegistNoInvalidDate;
            //inDtl.BiddingNo
            inDtl.ApproveDocNo = inDtl.ReaGoods.ApproveDocNo;
            inDtl.GoodsSerial = outDtl.GoodsSerial;
            inDtl.LotSerial = outDtl.LotSerial;
            inDtl.RegisterNo = inDtl.ReaGoods.RegistNo;
            inDtl.SysLotSerial = outDtl.SysLotSerial;
            inDtl.CompGoodsLinkID = outDtl.CompGoodsLinkID;
            inDtl.ReaServerCompCode = outDtl.ReaServerCompCode;
            inDtl.BarCodeType = (int)outDtl.BarCodeType;
            inDtl.ReaGoodsNo = outDtl.ReaGoodsNo;
            inDtl.ProdGoodsNo = outDtl.ProdGoodsNo;
            inDtl.CenOrgGoodsNo = outDtl.CenOrgGoodsNo;
            inDtl.GoodsNo = outDtl.GoodsNo;
            inDtl.ReaCompCode = outDtl.ReaCompCode;
            inDtl.GoodsSort = outDtl.GoodsSort;
            return inDtl;
        }

        #region 入库统计及报表
        public EntityList<ReaBmsInDtl> SearchReaBmsInDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit, bool isOfColdInfoMerge, bool isPage)
        {
            EntityList<ReaBmsInDtl> entityList = new EntityList<ReaBmsInDtl>();
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                return entityList;

            IList<ReaBmsInDtl> dtlList = new List<ReaBmsInDtl>();
            dtlList = ((IDReaBmsInDtlDao)base.DBDao).SearchReaBmsInDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, order, -1, -1);

            if (groupType == int.Parse(ReaBmsInDtlStatisticalType.按货品规格.Key))
            {
                dtlList = SearchReaBmsInDtlSummaryOfGroupBy1(dtlList);
            }
            else if (groupType == int.Parse(ReaBmsInDtlStatisticalType.按供应商加批号及货品.Key))
            {
                dtlList = SearchReaBmsInDtlSummaryOfGroupBy2(dtlList);
            }
            else if (groupType == int.Parse(ReaBmsInDtlStatisticalType.按入库明细常规合并.Key))
            {
                if (isOfColdInfoMerge)
                    dtlList = SearchReaBmsOutDtlListOfGroupByOfColdInfoMerge(dtlList);
                else
                    dtlList = SearchReaBmsInDtlSummaryOfGroupBy3(dtlList);
            }
            else if (groupType == int.Parse(ReaBmsInDtlStatisticalType.按入库总单号汇总.Key))
            {
                dtlList = SearchReaBmsInDtlSummaryOfGroupBy4(dtlList);
            }
            //if (isOfColdInfoMerge)
            //    dtlList = SearchReaBmsOutDtlListOfGroupByOfColdInfoMerge(dtlList);
            //else
            //    dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            entityList.count = dtlList.Count;
            if (isPage)
            {
                //分页处理
                if (limit > 0 && limit < dtlList.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = dtlList.Skip(startIndex).Take(endIndex);
                    if (list != null)
                        dtlList = list.ToList();
                }
            }
            entityList.list = dtlList;
            return entityList;
        }

        #region 入库明细汇总，4个（1 按货品规格、2 按供应商加批号及货品、3 按入库明细常规合并、4 按入库总单号汇总）统计

        /// <summary>
        /// 按货品规格
        /// </summary>
        /// <param name="dtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsInDtl> SearchReaBmsInDtlSummaryOfGroupBy1(IList<ReaBmsInDtl> dtlList)
        {
            return dtlList.GroupBy(p => new
            {
                //p.ReaCompanyID,
                p.ReaGoodsNo,
                p.GoodsUnit
                //p.UnitMemo,
                //p.LotNo,
                //p.InvalidDate
            }).Select(g => new ReaBmsInDtl
            {
                ReaGoodsNo = g.Key.ReaGoodsNo,
                ProdGoodsNo = g.ElementAt(0).ProdGoodsNo,
                GoodsNo = g.ElementAt(0).GoodsNo,
                GoodsID = g.ElementAt(0).GoodsID,
                GoodsCName = g.ElementAt(0).GoodsCName,
                GoodsUnit = g.ElementAt(0).GoodsUnit,
                UnitMemo = g.ElementAt(0).UnitMemo,
                BarCodeType = g.ElementAt(0).BarCodeType,
                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.GoodsQty) > 0 ? g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty) : 0,
                Memo = g.ElementAt(0).Memo,
                ProdOrgName = g.ElementAt(0).ProdOrgName,
                GoodSName = g.ElementAt(0).GoodSName
            }).ToList();
        }

        /// <summary>
        /// 按供应商加批号及货品
        /// </summary>
        /// <param name="dtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsInDtl> SearchReaBmsInDtlSummaryOfGroupBy2(IList<ReaBmsInDtl> dtlList)
        {
            return dtlList.GroupBy(p => new
            {
                p.ReaCompanyID,
                p.ReaGoodsNo,
                p.GoodsUnit,
                p.UnitMemo,
                p.LotNo,
                p.InvalidDate
            }).Select(g => new ReaBmsInDtl
            {
                ReaCompanyID = g.Key.ReaCompanyID,
                CompanyName = g.ElementAt(0).CompanyName,
                ReaGoodsNo = g.Key.ReaGoodsNo,
                GoodsID = g.ElementAt(0).GoodsID,
                GoodsCName = g.ElementAt(0).GoodsCName,
                GoodsUnit = g.ElementAt(0).GoodsUnit,

                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty),//g.ElementAt(0).Price,
                TaxRate = g.ElementAt(0).TaxRate,
                UnitMemo = g.ElementAt(0).UnitMemo,
                GoodsLotID = g.ElementAt(0).GoodsLotID,
                LotNo = g.Key.LotNo,

                InvalidDate = g.ElementAt(0).InvalidDate,
                StorageID = g.ElementAt(0).StorageID,
                PlaceID = g.ElementAt(0).PlaceID,
                StorageName = g.ElementAt(0).StorageName,
                PlaceName = g.ElementAt(0).PlaceName,

                ProdDate = g.ElementAt(0).ProdDate,
                RegisterInvalidDate = g.ElementAt(0).RegisterInvalidDate,
                BiddingNo = g.ElementAt(0).BiddingNo,
                GoodsNo = g.ElementAt(0).GoodsNo,
                ReaServerCompCode = g.ElementAt(0).ReaServerCompCode,

                BarCodeType = g.ElementAt(0).BarCodeType,
                ProdGoodsNo = g.ElementAt(0).ProdGoodsNo,
                CenOrgGoodsNo = g.ElementAt(0).CenOrgGoodsNo,
                ReaCompCode = g.ElementAt(0).ReaCompCode,
                GoodsSort = g.ElementAt(0).GoodsSort,

                CreaterID = g.ElementAt(0).CreaterID,
                CreaterName = g.ElementAt(0).CreaterName,
                Memo = g.ElementAt(0).Memo,
                ProdOrgName = g.ElementAt(0).ProdOrgName,
                GoodSName = g.ElementAt(0).GoodSName
            }).ToList();
        }


        /// <summary>
        /// 按入库明细常规合并
        /// </summary>
        /// <param name="dtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsInDtl> SearchReaBmsInDtlSummaryOfGroupBy3(IList<ReaBmsInDtl> dtlList)
        {
            return dtlList.GroupBy(p => new
            {
                p.ReaCompanyID,
                p.ReaGoodsNo,
                p.GoodsUnit,
                p.UnitMemo,
                p.LotNo,
                p.InvalidDate,
                p.TransportNo,
                p.DataAddTime,
                p.InDocNo,
                p.InvoiceNo,
                p.NetGoodsNo,
                p.RegisterNo,
                p.OrderDocNo
            }).Select(g => new ReaBmsInDtl
            {
                ReaCompanyID = g.Key.ReaCompanyID,
                CompanyName = g.ElementAt(0).CompanyName,
                ReaGoodsNo = g.Key.ReaGoodsNo,
                GoodsID = g.ElementAt(0).GoodsID,
                GoodsCName = g.ElementAt(0).GoodsCName,
                GoodsUnit = g.ElementAt(0).GoodsUnit,

                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty),//g.ElementAt(0).Price,
                TaxRate = g.ElementAt(0).TaxRate,
                UnitMemo = g.ElementAt(0).UnitMemo,
                GoodsLotID = g.ElementAt(0).GoodsLotID,
                LotNo = g.Key.LotNo,

                InvalidDate = g.ElementAt(0).InvalidDate,
                StorageID = g.ElementAt(0).StorageID,
                PlaceID = g.ElementAt(0).PlaceID,
                StorageName = g.ElementAt(0).StorageName,
                PlaceName = g.ElementAt(0).PlaceName,

                ProdDate = g.ElementAt(0).ProdDate,
                RegisterInvalidDate = g.ElementAt(0).RegisterInvalidDate,
                BiddingNo = g.ElementAt(0).BiddingNo,
                GoodsNo = g.ElementAt(0).GoodsNo,
                ReaServerCompCode = g.ElementAt(0).ReaServerCompCode,

                BarCodeType = g.ElementAt(0).BarCodeType,
                ProdGoodsNo = g.ElementAt(0).ProdGoodsNo,
                CenOrgGoodsNo = g.ElementAt(0).CenOrgGoodsNo,
                ReaCompCode = g.ElementAt(0).ReaCompCode,
                GoodsSort = g.ElementAt(0).GoodsSort,

                CreaterID = g.ElementAt(0).CreaterID,
                CreaterName = g.ElementAt(0).CreaterName,
                DataAddTime = g.ElementAt(0).DataAddTime,
                Memo = g.ElementAt(0).Memo,

                TransportNo = g.ElementAt(0).TransportNo, //新加货运单号

                InDocNo = g.ElementAt(0).InDocNo,
                InvoiceNo = g.ElementAt(0).InvoiceNo,
                OrderDocNo = g.ElementAt(0).OrderDocNo,
                NetGoodsNo = g.ElementAt(0).NetGoodsNo,
                RegisterNo = g.ElementAt(0).RegisterNo,
                ProdOrgName = g.ElementAt(0).ProdOrgName,
                GoodSName = g.ElementAt(0).GoodSName
            }).ToList();
        }

        /// <summary>
        /// 按入库总单号汇总
        /// </summary>
        /// <param name="dtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsInDtl> SearchReaBmsInDtlSummaryOfGroupBy4(IList<ReaBmsInDtl> dtlList)
        {
            return dtlList.GroupBy(p => new
            {
                p.ReaCompanyID,
                p.InDocNo,
                p.InvoiceNo,
                p.DataAddTime
            }).Select(g => new ReaBmsInDtl
            {
                ReaCompanyID = g.Key.ReaCompanyID,
                CompanyName = g.ElementAt(0).CompanyName,
                InDocNo = g.ElementAt(0).InDocNo,
                InvoiceNo = g.ElementAt(0).InvoiceNo,
                DataAddTime = g.ElementAt(0).DataAddTime,
                
                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.GoodsQty) > 0 ? g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty) : 0,
                Memo = g.ElementAt(0).Memo
            }).ToList();
        }

        #endregion

        /// <summary>
        /// 合并条件:供应商ID+货品产品编码+包装单位+规格+批号+效期
        /// </summary>
        /// <param name="dtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsInDtl> SearchReaBmsOutDtlListOfGroupBy1(IList<ReaBmsInDtl> dtlList)
        {
            return dtlList.GroupBy(p => new
            {
                p.ReaCompanyID,
                p.ReaGoodsNo,
                p.GoodsUnit,
                p.UnitMemo,
                p.LotNo,
                p.InvalidDate
            }).Select(g => new ReaBmsInDtl
            {
                ReaCompanyID = g.Key.ReaCompanyID,
                CompanyName = g.ElementAt(0).CompanyName,
                ReaGoodsNo = g.Key.ReaGoodsNo,
                GoodsID = g.ElementAt(0).GoodsID,
                GoodsCName = g.ElementAt(0).GoodsCName,
                GoodsUnit = g.ElementAt(0).GoodsUnit,

                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty),//g.ElementAt(0).Price,
                TaxRate = g.ElementAt(0).TaxRate,
                UnitMemo = g.ElementAt(0).UnitMemo,
                GoodsLotID = g.ElementAt(0).GoodsLotID,
                LotNo = g.Key.LotNo,

                InvalidDate = g.ElementAt(0).InvalidDate,
                StorageID = g.ElementAt(0).StorageID,
                PlaceID = g.ElementAt(0).PlaceID,
                StorageName = g.ElementAt(0).StorageName,
                PlaceName = g.ElementAt(0).PlaceName,

                ProdDate = g.ElementAt(0).ProdDate,
                RegisterInvalidDate = g.ElementAt(0).RegisterInvalidDate,
                BiddingNo = g.ElementAt(0).BiddingNo,
                GoodsNo = g.ElementAt(0).GoodsNo,
                ReaServerCompCode = g.ElementAt(0).ReaServerCompCode,

                BarCodeType = g.ElementAt(0).BarCodeType,
                ProdGoodsNo = g.ElementAt(0).ProdGoodsNo,
                CenOrgGoodsNo = g.ElementAt(0).CenOrgGoodsNo,
                ReaCompCode = g.ElementAt(0).ReaCompCode,
                GoodsSort = g.ElementAt(0).GoodsSort,

                CreaterID = g.ElementAt(0).CreaterID,
                CreaterName = g.ElementAt(0).CreaterName,
                DataAddTime = g.ElementAt(0).DataAddTime,
                Memo = g.ElementAt(0).Memo,

                TransportNo = g.ElementAt(0).TransportNo //新加货运单号
            }).ToList();
        }
        /// <summary>
        /// 合并条件:供应商ID+货品产品编码+包装单位+规格+批号+效期+冷链信息项(厂家出库温度+到货温度+外观验收)
        /// </summary>
        /// <param name="dtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsInDtl> SearchReaBmsOutDtlListOfGroupByOfColdInfoMerge(IList<ReaBmsInDtl> dtlList)
        {
            return dtlList.GroupBy(p => new
            {
                p.ReaCompanyID,
                p.ReaGoodsNo,
                p.GoodsUnit,
                p.UnitMemo,
                p.LotNo,
                p.InvalidDate,
                p.TransportNo,
                p.OrderDocNo,
                p.DataAddTime,
                p.InDocNo,
                p.InvoiceNo,
                p.NetGoodsNo,
                p.RegisterNo,
                p.FactoryOutTemperature,
                p.ArrivalTemperature,
                p.AppearanceAcceptance
            }).Select(g => new ReaBmsInDtl
            {
                ReaCompanyID = g.Key.ReaCompanyID,
                CompanyName = g.ElementAt(0).CompanyName,
                ReaGoodsNo = g.Key.ReaGoodsNo,
                GoodsID = g.ElementAt(0).GoodsID,
                GoodsCName = g.ElementAt(0).GoodsCName,
                GoodsUnit = g.ElementAt(0).GoodsUnit,

                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty),//g.ElementAt(0).Price,
                TaxRate = g.ElementAt(0).TaxRate,
                UnitMemo = g.ElementAt(0).UnitMemo,
                GoodsLotID = g.ElementAt(0).GoodsLotID,
                LotNo = g.Key.LotNo,

                InvalidDate = g.ElementAt(0).InvalidDate,
                StorageID = g.ElementAt(0).StorageID,
                PlaceID = g.ElementAt(0).PlaceID,
                StorageName = g.ElementAt(0).StorageName,
                PlaceName = g.ElementAt(0).PlaceName,

                ProdDate = g.ElementAt(0).ProdDate,
                RegisterInvalidDate = g.ElementAt(0).RegisterInvalidDate,
                BiddingNo = g.ElementAt(0).BiddingNo,
                GoodsNo = g.ElementAt(0).GoodsNo,
                ReaServerCompCode = g.ElementAt(0).ReaServerCompCode,

                BarCodeType = g.ElementAt(0).BarCodeType,
                ProdGoodsNo = g.ElementAt(0).ProdGoodsNo,
                CenOrgGoodsNo = g.ElementAt(0).CenOrgGoodsNo,
                ReaCompCode = g.ElementAt(0).ReaCompCode,
                GoodsSort = g.ElementAt(0).GoodsSort,

                CreaterID = g.ElementAt(0).CreaterID,
                CreaterName = g.ElementAt(0).CreaterName,
                DataAddTime = g.ElementAt(0).DataAddTime,
                Memo = g.ElementAt(0).Memo,
                FactoryOutTemperature = g.ElementAt(0).FactoryOutTemperature,
                ArrivalTemperature = g.ElementAt(0).ArrivalTemperature,
                AppearanceAcceptance = g.ElementAt(0).AppearanceAcceptance,

                TransportNo = g.ElementAt(0).TransportNo, //新加货运单号

                InDocNo = g.ElementAt(0).InDocNo,
                InvoiceNo = g.ElementAt(0).InvoiceNo,
                OrderDocNo = g.ElementAt(0).OrderDocNo,
                NetGoodsNo = g.ElementAt(0).NetGoodsNo,
                RegisterNo = g.ElementAt(0).RegisterNo,
                ProdOrgName = g.ElementAt(0).ProdOrgName,
                GoodSName = g.ElementAt(0).GoodSName
            }).ToList();
        }
        public Stream SearchBusinessSummaryReportOfExcelByHql(long labID, string labCName, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string fileName, string startDate, string endDate, bool isOfColdInfoMerge)
        {
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            Stream stream = null;
            //IList<ReaBmsInDtl> dtlList = new List<ReaBmsInDtl>();
            //dtlList = ((IDReaBmsInDtlDao)base.DBDao).SearchReaBmsInDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, sort, -1, -1);

            //if (dtlList == null || dtlList.Count <= 0)
            //{
            //    return ResponseResultStream.GetErrMemoryStreamInfo("获取入库汇总明细信息为空!");
            //}
            //if (isOfColdInfoMerge)
            //    dtlList = SearchReaBmsOutDtlListOfGroupByOfColdInfoMerge(dtlList);
            //else
            //    dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);

            IList<ReaBmsInDtl> dtlList = SearchReaBmsInDtlSummaryByHQL(groupType, docHql, dtlHql, reaGoodsHql, sort, -1, -1, isOfColdInfoMerge, false).list;
            if (dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取入库汇总明细信息为空!");
            }

            //获取入库汇总模板
            if (string.IsNullOrEmpty(frx))
                frx = "入库汇总.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));

            string fileNameTemp = frx.Substring(0, frx.LastIndexOf("."));
            if (fileNameTemp.Trim() != "入库汇总")
            {
                fileNameTemp = "入库汇总信息-" + fileNameTemp + fileExt;
            }
            else
            {
                fileNameTemp = "入库汇总信息" + fileExt;
            }
            fileName = fileNameTemp;

            //string excelFile = frx;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsInDoc, ReaBmsInDtl>(null, dtlList, excelCommand, breportType, labID, frx, fileName, ref saveFullPath);
            //fileName = "入库汇总信息" + fileExt;
            return stream;
        }
        public Stream SearchBusinessSummaryReportOfPdfByHql(string reaReportClass, long labId, string labCName, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string pdfFileName, string startDate, string endDate, bool isOfColdInfoMerge)
        {
            Stream stream = null;
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            IList<ReaBmsInDtl> dtlList = SearchReaBmsInDtlSummaryByHQL(groupType, docHql, dtlHql, reaGoodsHql, sort, -1, -1, isOfColdInfoMerge, false).list;
            if (dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取入库汇总明细信息为空!");
            }
            //IList<ReaBmsInDtl> dtlList = ((IDReaBmsInDtlDao)base.DBDao).SearchReaBmsInDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, sort, -1, -1);

            //if (dtlList == null || dtlList.Count <= 0)
            //{
            //    return ResponseResultStream.GetErrMemoryStreamInfo("获取移库汇总明细信息为空!");
            //}
            //if (isOfColdInfoMerge)
            //    dtlList = SearchReaBmsOutDtlListOfGroupByOfColdInfoMerge(dtlList);
            //else
            //    dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            //pdfFileName = "入库汇总.pdf";
            pdfFileName = "入库汇总_" + DateTime.Now.ToString("yyMMddHHmmss") + ".pdf";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = SearchPdfReportOfFrxByHql(labId, labCName, pdfFileName, dtlList, frx, startDate, endDate);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                string excelFileFullDir = "";
                if (string.IsNullOrEmpty(frx))
                    frx = "入库汇总.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = fileExt;
                Stream stream2 = null;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
                stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsInDtl, ReaBmsInDtl>(null, dtlList, excelCommand, breportType, labId, frx, excelFile, ref excelFileFullDir);
                stream2.Close();
                string pdfFullDir = "";
                bool result = ExcelToPdfReportHelp.ExcelToPDF(excelFileFullDir, breportType, labId, pdfFileName, ref pdfFullDir);
                if (result)
                {
                    stream = PdfReportHelp.GetReportPDF(pdfFullDir);
                }
            }
            return stream;
        }
        private Stream SearchPdfReportOfFrxByHql(long labId, string labCName, string pdfFileName, IList<ReaBmsInDtl> dtlList, string frx, string startDate, string endDate)
        {
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";
            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsInDtl>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "Rea_BmsInDtl";
                dataSet.Tables.Add(dtDtl);
            }
            if (string.IsNullOrEmpty(frx))
                frx = "入库汇总.frx";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, labId, pdfFileName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.入库汇总.Key].Name, frx, false);
            return stream;
        }
        public BaseResultDataValue SearchStackInEChartsVOListByHql(int statisticType, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<ReaBmsInDtl> dtlList = ((IDReaBmsInDtlDao)base.DBDao).SearchReaBmsInDtlListByJoinHql(docHql, dtlHql, deptGoodsHql, reaGoodsHql, -1, -1, "");
            string goodsClassHql = "reagoods.Visible=1";
            IList<ReaGoodsClassVO> goodsclassList = IBReaGoods.SearchGoodsClassListByClassTypeAndHQL("goodsclass", true, goodsClassHql, "", -1, -1);
            IList<ReaGoodsClassVO> goodsClassTypeList = IBReaGoods.SearchGoodsClassListByClassTypeAndHQL("goodsclasstype", true, goodsClassHql, "", -1, -1);
            var groupByGoodsClass = goodsclassList.GroupBy(p => new
            {
                p.CName
            });
            var groupByGoodsClassType = goodsClassTypeList.GroupBy(p => new
            {
                p.CName
            });
            JObject jresult = new JObject();
            JObject jAxis = new JObject();
            JArray axisData = new JArray();
            JObject jLegend = new JObject();
            JArray legendData = new JArray();
            JArray seriesData = new JArray();
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            jresult.Add("allSumTotal", ConvertQtyHelp.ConvertQty(allSumTotal.Value, 2));

            bool isHasNull = false;
            foreach (var vo in groupByGoodsClass)
            {
                if (!string.IsNullOrEmpty(vo.Key.CName))
                {
                    axisData.Add(vo.Key.CName);
                }
                else if (isHasNull == false)
                {
                    isHasNull = true;
                    axisData.Add("未知");
                }
            }
            jAxis.Add("data", axisData);
            jresult.Add("axis", jAxis);

            isHasNull = false;
            foreach (var vo in groupByGoodsClassType)
            {
                if (!string.IsNullOrEmpty(vo.Key.CName))
                {
                    legendData.Add(vo.Key.CName);
                }
                else if (isHasNull == false)
                {
                    isHasNull = true;
                    legendData.Add("未知");
                }
            }
            jLegend.Add("data", legendData);
            jresult.Add("legend", jLegend);

            switch (statisticType)
            {
                case 1:
                    //一级分类金额及金额占比处理
                    JArray goodsClassList = new JArray();
                    var groupBy = dtlList.GroupBy(p => new
                    {
                        p.ReaGoods.GoodsClass
                    });
                    if (groupBy != null && groupBy.Count() > 0)
                    {
                        foreach (var g in groupBy)
                        {
                            JObject jseries = new JObject();
                            jseries.Add("GoodsClass", !string.IsNullOrEmpty(g.Key.GoodsClass) == true ? g.Key.GoodsClass : "未知");
                            jseries.Add("SumTotal", Math.Round(g.Sum(k => k.SumTotal).Value, 2));
                            jseries.Add("SumTotalPercent", (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0));
                            goodsClassList.Add(jseries);
                        }
                        jresult.Add("goodsClassList", goodsClassList);
                    }

                    //按货品一级分类(堆叠为二级分类)
                    foreach (var voGoodsClassType in groupByGoodsClassType)
                    {
                        JObject jseries = new JObject();
                        jseries.Add("name", !string.IsNullOrEmpty(voGoodsClassType.Key.CName) == true ? voGoodsClassType.Key.CName : "未知");
                        jseries.Add("type", "bar");
                        jseries.Add("stack", "总量");
                        JArray seriesData2 = new JArray();
                        foreach (var voGoodsclass in groupByGoodsClass)
                        {
                            double? sumTotal = 0;
                            var tempList = dtlList.Where(p => p.ReaGoods.GoodsClass == voGoodsclass.Key.CName && p.ReaGoods.GoodsClassType == voGoodsClassType.Key.CName);
                            if (tempList != null) sumTotal = tempList.Sum(k => k.SumTotal);
                            seriesData2.Add(Math.Round(sumTotal.Value, 2));
                        }
                        jseries.Add("data", seriesData2);
                        seriesData.Add(jseries);
                    }
                    jresult.Add("series", seriesData);
                    break;
                default:

                    break;
            }
            //ZhiFang.Common.Log.Log.Debug("jresult:" + jresult.ToString());
            baseResultDataValue.ResultDataValue = jresult.ToString();
            return baseResultDataValue;
        }
        #endregion
        #region EChart图表统计
        public EntityList<EChartsVO> SearchInEChartsVOListByHql(int statisticType, bool showZero, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, string sort)
        {
            EntityList<EChartsVO> entityList = new EntityList<EChartsVO>();
            entityList.list = new List<EChartsVO>();
            IList<ReaBmsInDtl> dtlList = ((IDReaBmsInDtlDao)base.DBDao).SearchReaBmsInDtlListByJoinHql(docHql, dtlHql, deptGoodsHql, reaGoodsHql, -1, -1, sort);
            switch (statisticType)
            {
                case 1://按库房
                    entityList.list = SearchInEChartsVOListOfGroupByStorage(dtlList, showZero);
                    break;
                case 2://按供货商
                    entityList.list = SearchInEChartsVOListOfGroupByComp(dtlList, showZero);
                    break;
                case 3://按品牌
                    entityList.list = SearchInEChartsVOListOfGroupByProdOrg(dtlList, showZero);
                    break;
                case 4://按货品一级分类
                    entityList.list = SearchInEChartsVOListOfGroupByGoodsClass(dtlList, showZero);
                    break;
                case 5://按货品二级分类
                    entityList.list = SearchInEChartsVOListOfGroupByGoodsClassType(dtlList, showZero);
                    break;
                default:

                    break;
            }
            if (entityList.list != null && entityList.list.Count > 0) entityList.list = entityList.list.OrderByDescending(p => p.SumTotal).ToList();
            entityList.count = entityList.list.Count();
            return entityList;
        }
        private IList<EChartsVO> SearchInEChartsVOListOfGroupByProdOrg(IList<ReaBmsInDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.ReaGoods.ProdOrgName
            });
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            bool hasNull = false;
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    ProdOrgName = !string.IsNullOrEmpty(g.Key.ProdOrgName) == true ? g.Key.ProdOrgName : "未知",
                    SumTotal = Math.Round(g.Sum(k => k.SumTotal).Value, 2),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    if (!string.IsNullOrEmpty(item.Key.ProdOrgName))
                    {
                        hqlStr.Append("'" + item.Key.ProdOrgName + "'");
                        hqlStr.Append(",");
                    }
                    else if (hasNull == false)
                    {
                        hasNull = true;
                    }
                }
            }
            if (!showZero) return voList;

            //没有入库信息的库房处理
            string hql2 = "bdict.IsUse=1 and bdict.BDictType.DictTypeCode='ProdOrg'";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and bdict.CName not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<BDict> entityList = IBBDict.SearchListByHQL(hql2);
            foreach (BDict entity in entityList)
            {
                EChartsVO vo = new EChartsVO();
                vo.ProdOrgName = entity.CName;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }
        private IList<EChartsVO> SearchInEChartsVOListOfGroupByGoodsClass(IList<ReaBmsInDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.ReaGoods.GoodsClass
            });
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            bool hasNull = false;
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    GoodsClass = !string.IsNullOrEmpty(g.Key.GoodsClass) == true ? g.Key.GoodsClass : "未知",
                    SumTotal = Math.Round(g.Sum(k => k.SumTotal).Value, 2),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    if (!string.IsNullOrEmpty(item.Key.GoodsClass))
                    {
                        hqlStr.Append("'" + item.Key.GoodsClass + "'");
                        hqlStr.Append(",");
                    }
                    else if (hasNull == false)
                    {
                        hasNull = true;
                    }
                }
            }
            if (!showZero) return voList;

            //没有入库信息的一级分类处理
            string hql2 = "reagoods.Visible=1";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and reagoods.GoodsClass not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<ReaGoodsClassVO> goodsclassList = IBReaGoods.SearchGoodsClassListByClassTypeAndHQL("goodsclass", false, hql2, "", -1, -1);
            foreach (ReaGoodsClassVO entity in goodsclassList)
            {
                EChartsVO vo = new EChartsVO();
                vo.GoodsClass = entity.CName;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }
        private IList<EChartsVO> SearchInEChartsVOListOfGroupByGoodsClassType(IList<ReaBmsInDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.ReaGoods.GoodsClassType
            });
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            bool hasNull = false;
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    GoodsClassType = !string.IsNullOrEmpty(g.Key.GoodsClassType) == true ? g.Key.GoodsClassType : "未知",
                    SumTotal = Math.Round(g.Sum(k => k.SumTotal).Value, 2),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    if (!string.IsNullOrEmpty(item.Key.GoodsClassType))
                    {
                        hqlStr.Append("'" + item.Key.GoodsClassType + "'");
                        hqlStr.Append(",");
                    }
                    else if (hasNull == false)
                    {
                        hasNull = true;
                    }
                }
            }
            if (!showZero) return voList;

            //没有入库信息的二级分类处理
            string hql2 = "reagoods.Visible=1";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and reagoods.GoodsClassType not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<ReaGoodsClassVO> coodsClassTypeList = IBReaGoods.SearchGoodsClassListByClassTypeAndHQL("goodsclasstype", false, hql2, "", -1, -1);
            foreach (ReaGoodsClassVO entity in coodsClassTypeList)
            {
                EChartsVO vo = new EChartsVO();
                vo.GoodsClassType = entity.CName;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }
        private IList<EChartsVO> SearchInEChartsVOListOfGroupByStorage(IList<ReaBmsInDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.StorageID
            });
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    StorageID = g.Key.StorageID.ToString(),
                    StorageName = g.ElementAt(0).StorageName,
                    SumTotal = Math.Round(g.Sum(k => k.SumTotal).Value, 2),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    hqlStr.Append(item.Key.StorageID);
                    hqlStr.Append(",");
                }
            }
            if (!showZero) return voList;

            //没有入库信息的库房处理
            string hql2 = "reastorage.Visible=1";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and reastorage.Id not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<ReaStorage> cenOrgList = IDReaStorageDao.GetListByHQL(hql2);
            foreach (ReaStorage entity in cenOrgList)
            {
                EChartsVO vo = new EChartsVO();
                vo.StorageID = entity.Id.ToString();
                vo.StorageName = entity.CName;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }
        private IList<EChartsVO> SearchInEChartsVOListOfGroupByComp(IList<ReaBmsInDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.ReaCompanyID
            });
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    ReaCompanyID = g.Key.ReaCompanyID.ToString(),
                    ReaCompCode = g.ElementAt(0).ReaCompCode,
                    ReaCompanyName = g.ElementAt(0).CompanyName,
                    SumTotal = Math.Round(g.Sum(k => k.SumTotal).Value, 2),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    hqlStr.Append(item.Key.ReaCompanyID);
                    hqlStr.Append(",");
                }
            }
            if (!showZero) return voList;

            //没有入库信息的供货商处理
            string hql2 = "reacenorg.Visible=1 and reacenorg.OrgType=" + ReaCenOrgType.供货方.Key;
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and reacenorg.Id not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<ReaCenOrg> cenOrgList = IDReaCenOrgDao.GetListByHQL(hql2);
            foreach (ReaCenOrg cenOrg in cenOrgList)
            {
                EChartsVO vo = new EChartsVO();
                vo.ReaCompanyID = cenOrg.Id.ToString();
                vo.ReaCompCode = cenOrg.OrgNo.ToString();
                vo.ReaCompanyName = cenOrg.CName;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }

        #endregion

        /// <summary>
        /// 关联货品表查询入库明细
        /// </summary>
        public EntityList<ReaBmsInDtl> GetReaBmsInDtlListByHql(string strHqlWhere, string sort, int page, int limit)
        {
            return ((IDReaBmsInDtlDao)base.DBDao).GetReaBmsInDtlListByHql(strHqlWhere, sort, page, limit);
        }

    }
}