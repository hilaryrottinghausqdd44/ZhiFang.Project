using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaStoreIn;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using ZhiFang.Digitlab.IBLL.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaGoodsScanCode;
using System.Text;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsInDtl : BaseBLL<ReaBmsInDtl>, ZhiFang.Digitlab.IBLL.ReagentSys.IBReaBmsInDtl
    {
        IDBmsCenSaleDtlConfirmDao IDBmsCenSaleDtlConfirmDao { get; set; }
        IBReaCenBarCodeFormat IBReaCenBarCodeFormat { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        IBReaBmsQtyDtl IBReaBmsQtyDtl { get; set; }

        #region 客户端入库
        /// <summary>
        /// 入库明细的保存验证判断
        /// </summary>
        /// <param name="dtAddList"></param>
        /// <param name="codeScanningMode">扫码模式:严格模式-strict;混合模式-mixing</param>
        /// <returns></returns>
        public BaseResultBool AddReaBmsInDtlValid(IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            IList<BmsCenSaleDtlConfirm> listDtlConfirm = new List<BmsCenSaleDtlConfirm>();
            foreach (var vo in dtAddList)
            {
                var model = vo.ReaBmsInDtl;
                listDtlConfirm.Clear();

                #region 验证判断处理
                if (model.GoodsQty <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "试剂名称为【" + model.GoodsCName + "】的本次入库数为零，不能入库！";
                }
                else if (!model.ReaCompanyID.HasValue)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "试剂名称为【" + model.GoodsCName + "】的供应商ID为空，不能入库！";
                }
                else if (string.IsNullOrEmpty(model.LotNo))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "试剂名称为【" + model.GoodsCName + "】的批号信息为空，不能入库！";
                }
                else if (!model.PlaceID.HasValue)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "试剂名称为【" + model.GoodsCName + "】的存储库房ID为空，不能入库！";
                }
                else if (!model.StorageID.HasValue)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "试剂名称为【" + model.GoodsCName + "】的货位ID为空，不能入库！";
                }
                else if (model.ReaGoods == null)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "试剂名称为【" + model.GoodsCName + "】的货品信息为空，不能入库！";
                }
                else if (!model.SaleDtlConfirmID.HasValue)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "试剂名称为【" + model.GoodsCName + "】的验收明细ID为空，不能入库！";
                }
                else if (model.SaleDtlConfirmID.HasValue)
                {
                    listDtlConfirm = IDBmsCenSaleDtlConfirmDao.GetListByHQL(string.Format("bmscensaledtlconfirm.Id={0}", model.SaleDtlConfirmID.Value));
                    //验收明细的本次入库数>验收数量
                    if (model.GoodsQty > listDtlConfirm[0].AcceptCount)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = string.Format("试剂名称为【" + model.GoodsCName + "】的本次入库数{0}大于验收数量{1}，不能入库！", model.GoodsQty, listDtlConfirm[0].AcceptCount);
                    }
                    else
                    {
                        IList<ReaBmsInDtl> listInDtl = this.SearchListByHQL(string.Format("reabmsindtl.SaleDtlConfirmID={0}", model.SaleDtlConfirmID.Value));
                        double inCount = 0;
                        if (listInDtl.Count > 0) inCount = listInDtl.Sum(p => p.GoodsQty);
                        //验收明细的本次入库数+入库总数>验收数量
                        if ((inCount + model.GoodsQty) > listDtlConfirm[0].AcceptCount)
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = string.Format("试剂名称为【" + model.GoodsCName + "】的本次入库数为{0}+已入库总数{1}等于{2},大于验收数量{3}，不能入库！", model.GoodsQty, inCount, (inCount + model.GoodsQty), listDtlConfirm[0].AcceptCount);
                        }
                    }
                }
                #endregion

                #region 盒条码处理
                else if (vo.BarCodeMgr == int.Parse(ReaGoodsBarCodeMgr.盒条码.Key))
                {
                    if (model.GoodsQty < vo.ReaBmsInDtlLinkList.Count)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = string.Format("试剂【" + model.GoodsCName + "】的本次入库数为{0}小于本次扫码记录数{1},不能入库！", model.GoodsQty, vo.ReaBmsInDtlLinkList.Count);
                    }
                    else if (codeScanningMode == "strict" && vo.ReaBmsInDtlLinkList == null)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = string.Format("试剂【" + model.GoodsCName + "】的扫码操作记录为空,在严格扫码模式下不能入库！");
                    }
                    else
                    {
                        //验证本次入库扫码操作记录是否已入库
                        foreach (ReaGoodsBarcodeOperation operation in vo.ReaBmsInDtlLinkList)
                        {
                            IList<ReaGoodsBarcodeOperation> dtlBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(string.Format("reagoodsbarcodeoperation.UsePackSerial='{0}' and reagoodsbarcodeoperation.OperTypeID={1}", operation.UsePackSerial, ReaGoodsBarcodeOperType.验货入库.Key));
                            if (dtlBarCodeList.Count > 0)
                            {
                                tempBaseResultBool.success = false;
                                tempBaseResultBool.ErrorInfo = string.Format("试剂名称为【" + model.GoodsCName + "】的入库条码值为{0}已被扫码入库，不能重复扫码入库！", operation.UsePackSerial);
                                break;
                            }
                        }
                    }
                }
                #endregion
                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 新增入库明细
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtAddList"></param>
        /// <returns></returns>
        public BaseResultBool AddReaBmsInDtl(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtAddList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //tempBaseResultBool = AddReaBmsInDtlValid(dtAddList);
            //if (tempBaseResultBool.success == false) return tempBaseResultBool;
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (var vo in dtAddList)
            {
                var model = vo.ReaBmsInDtl;
                model.InDocNo = entity.InDocNo;
                model.InDtlNo = GetInDtlNo();
                model.CreaterID = empID;
                model.CreaterName = empName;
                model.DataUpdateTime = DateTime.Now;
                if (model.ReaBmsInDoc == null)
                    model.ReaBmsInDoc = entity;
                if (model.ReaBmsInDoc.DataTimeStamp == null)
                    model.ReaBmsInDoc.DataTimeStamp = dataTimeStamp;
                if (model.ReaGoods.DataTimeStamp == null)
                    model.ReaGoods.DataTimeStamp = dataTimeStamp;
                this.Entity = model;
                tempBaseResultBool.BoolFlag = this.Add();
                if (tempBaseResultBool.BoolFlag == false)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "保存入库明细信息失败!";
                }
                if (tempBaseResultBool.success == false) break;

                //条码扫码扣件记录保存
                if (vo.ReaBmsInDtlLinkList != null)
                    tempBaseResultBool = AddBarcodeOperationOfList(model, vo.ReaBmsInDtlLinkList, empID, empName);
                //库存处理

                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        public BaseResultBool AddBarcodeOperationOfList(ReaBmsInDtl model, IList<ReaGoodsBarcodeOperation> dtAddList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtAddList == null || dtAddList.Count <= 0) return tempBaseResultBool;

            foreach (ReaGoodsBarcodeOperation operation in dtAddList)
            {
                operation.BDocID = model.ReaBmsInDoc.Id;
                operation.BDocNo = model.InDocNo;
                operation.BDtlID = model.Id;
                operation.ReaCompanyID = model.ReaCompanyID;
                operation.CompanyName = model.CompanyName;
                operation.LotNo = model.LotNo;
                operation.GoodsID = model.ReaGoods.Id;
                operation.GoodsCName = model.GoodsCName;
                operation.GoodsUnit = model.GoodsUnit;
                operation.OperTypeID = long.Parse(ReaGoodsBarcodeOperType.验货入库.Key);
                operation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[operation.OperTypeID.ToString()].Name;
            }
            tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperationOfList(dtAddList, 0, empID, empName);
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

        #endregion
    }
}