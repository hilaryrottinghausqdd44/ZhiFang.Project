using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;
using ZhiFang.ReagentSys.Client.Common;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics;
using ZhiFang.BLL.ReagentSys.Client.QtyListGroupBy;
using ZhiFang.Entity.ReagentSys.Client.ViewObject;
using ZhiFang.BLL.ReagentSys.Client.QtyWarningGroupBy;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsQtyDtl : BaseBLL<ReaBmsQtyDtl>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsQtyDtl
    {
        IDRBACModuleDao IDRBACModuleDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaCenOrgDao IDReaCenOrgDao { get; set; }
        IDReaStorageDao IDReaStorageDao { get; set; }
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IDReaBmsOutDtlDao IDReaBmsOutDtlDao { get; set; }
        IBReaBmsQtyDtlOperation IBReaBmsQtyDtlOperation { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        IBBParameter IBBParameter { get; set; }
        IBReaBmsSerial IBReaBmsSerial { get; set; }
        IDReaGoodsRegisterDao IDReaGoodsRegisterDao { get; set; }
        IBReaCenBarCodeFormat IBReaCenBarCodeFormat { get; set; }
        IBBDict IBBDict { get; set; }
        IDReaStorageGoodsLinkDao IDReaStorageGoodsLinkDao { get; set; }
        IDReaGoodsLotDao IDReaGoodsLotDao { get; set; }
        IDReaBmsInDtlDao IDReaBmsInDtlDao { get; set; }
        IDReaOpenBottleOperDocDao IDReaOpenBottleOperDocDao { get; set; }
        public override bool Add()
        {
            ReaGoods reaGoods = IDReaGoodsDao.Get(this.Entity.GoodsID.Value, false);
            if (reaGoods != null)
                this.Entity.IsNeedBOpen = reaGoods.IsNeedBOpen;

            if (this.Entity.InvalidDate.HasValue)
            {
                //效期保存时只取年月日
                this.Entity.InvalidDate = DateTime.Parse(this.Entity.InvalidDate.Value.ToString("yyyy-MM-dd"));
            }
            if (this.Entity.ProdDate.HasValue)
            {
                //生产日期保存时只取年月日
                this.Entity.ProdDate = DateTime.Parse(this.Entity.ProdDate.Value.ToString("yyyy-MM-dd"));
            }
            if (this.Entity.InvalidWarningDate.HasValue)
            {
                this.Entity.InvalidWarningDate = DateTime.Parse(this.Entity.InvalidWarningDate.Value.ToString("yyyy-MM-dd"));
            }
            //当前货品批号的性能验证判断处理
            if (reaGoods != null && !this.Entity.IsNeedPerformanceTest.HasValue)
            {
                this.Entity.IsNeedPerformanceTest = reaGoods.IsNeedPerformanceTest;
            }
            //同步库存货品的货品批号验证状态
            if (this.Entity.GoodsLotID.HasValue && this.Entity.IsNeedPerformanceTest.HasValue && this.Entity.IsNeedPerformanceTest.Value == true)
            {
                ReaGoodsLot reaGoodsLot = null;
                if (this.Entity.GoodsLotID.HasValue)
                    reaGoodsLot = IDReaGoodsLotDao.Get(this.Entity.GoodsLotID.Value);
                if (reaGoodsLot != null)
                {
                    this.Entity.VerificationStatus = reaGoodsLot.VerificationStatus;
                }
            }
            if (!this.Entity.VerificationStatus.HasValue)
                this.Entity.VerificationStatus = long.Parse(ReaGoodsLotVerificationStatus.未验证.Key);
            this.Entity.SumTotal = this.Entity.Price * this.Entity.GoodsQty;

            //获取货运单号
            if (this.Entity.InDtlID != null)
            {
                ReaBmsInDtl inDtl = IDReaBmsInDtlDao.Get(this.Entity.InDtlID.Value);
                if (inDtl != null && inDtl.SaleDtlConfirmID != null)
                {
                    this.Entity.TransportNo = ((IDReaBmsQtyDtlDao)base.DBDao).GetTransportNo(inDtl.SaleDtlConfirmID.Value);
                }
            }

            return base.Add();
        }
        public DateTime? EditCalcInvalidWarningDate(long labID, double? beforeWarningDay, DateTime? InvalidDate)
        {
            DateTime? invalidWarningDate = null;
            if (!beforeWarningDay.HasValue)
            {
                //获取系统运行参数的货品效期预警天数值
                BParameter param = IBBParameter.GetParameterByParaNo(SYSParaNo.货品效期预警天数.Key);
                if (param != null && !string.IsNullOrEmpty(param.ParaValue))
                    beforeWarningDay = double.Parse(param.ParaValue);
                beforeWarningDay = 10;
            }
            if (!InvalidDate.HasValue || !beforeWarningDay.HasValue)
                return DateTime.Now.AddDays(-10);

            if (InvalidDate.HasValue && beforeWarningDay.HasValue)
                invalidWarningDate = InvalidDate.Value.AddDays(-beforeWarningDay.Value);
            return invalidWarningDate;
        }
        public BaseResultBool AddReaBmsQtyDtl(ReaBmsInDtl inDtl, CenOrg cenOrg, long operTypeID, long empID, string empName, ref ReaBmsQtyDtl addQtyDtl)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //存在大小包装单位转换时的大包装单位货品
            ReaGoods pReaGoods = null;
            ReaGoods reaGoods = IDReaGoodsDao.Get(inDtl.ReaGoods.Id, false);
            pReaGoods = reaGoods;
            if (reaGoods == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = " 获取LabID为(" + inDtl.LabID + "),货品ID(" + inDtl.ReaGoods.Id + ")的货品信息为空!";
                throw new Exception(tempBaseResultBool.ErrorInfo);
            }
            double gonvertQty = reaGoods.GonvertQty;
            if (addQtyDtl == null)
                addQtyDtl = new ReaBmsQtyDtl();
            addQtyDtl.LabID = inDtl.LabID;
            addQtyDtl.InDtlID = inDtl.Id;
            addQtyDtl.InDocNo = inDtl.InDocNo;
            addQtyDtl.GoodsQty = inDtl.GoodsQty;
            addQtyDtl.BarCodeType = (int)inDtl.BarCodeType;
            addQtyDtl.Price = inDtl.Price;
            addQtyDtl.SumTotal = addQtyDtl.GoodsQty * addQtyDtl.Price;// inDtl.SumTotal;
            addQtyDtl.GoodsUnit = reaGoods.UnitName;
            addQtyDtl.CompGoodsLinkID = inDtl.CompGoodsLinkID;

            addQtyDtl.ReaGoodsNo = inDtl.ReaGoodsNo;
            addQtyDtl.ProdGoodsNo = inDtl.ProdGoodsNo;
            addQtyDtl.CenOrgGoodsNo = inDtl.CenOrgGoodsNo;
            addQtyDtl.GoodsNo = inDtl.GoodsNo;
            addQtyDtl.GoodsSort = reaGoods.GoodsSort;

            //如果入库货品不是最小包装单位
            #region 入库货品与库存货品进行大小包装单位转换处理
            if (reaGoods.GonvertQty != 1)//IsMinUnit == false
            {
                string resultInfo = "";
                ReaGoodsOrgLink reaGoodsOrgLink = null;
                reaGoodsOrgLink = IDReaGoodsOrgLinkDao.GetReaGoodsMinUnit(long.Parse(ReaCenOrgType.供货方.Key), inDtl.ReaCompanyID.Value, reaGoods.Id, reaGoods, ref resultInfo);

                if (reaGoodsOrgLink != null)
                {
                    reaGoods = reaGoodsOrgLink.ReaGoods;
                    addQtyDtl.GoodsQty = inDtl.GoodsQty * gonvertQty;
                    addQtyDtl.Price = reaGoodsOrgLink.Price;// inDtl.Price / gonvertQty;
                    addQtyDtl.SumTotal = addQtyDtl.GoodsQty * addQtyDtl.SumTotal;// inDtl.GoodsQty * inDtl.Price;
                    addQtyDtl.GoodsUnit = reaGoods.UnitName;
                    addQtyDtl.CompGoodsLinkID = reaGoodsOrgLink.Id;

                    addQtyDtl.ReaGoodsNo = reaGoodsOrgLink.ReaGoods.ReaGoodsNo;
                    addQtyDtl.ProdGoodsNo = reaGoodsOrgLink.ReaGoods.ProdGoodsNo;
                    addQtyDtl.CenOrgGoodsNo = reaGoodsOrgLink.CenOrgGoodsNo;
                    addQtyDtl.GoodsNo = reaGoodsOrgLink.ReaGoods.GoodsNo;
                    addQtyDtl.GoodsSort = reaGoodsOrgLink.ReaGoods.GoodsSort;
                }
                else
                {
                    if (!string.IsNullOrEmpty(resultInfo))
                        ZhiFang.Common.Log.Log.Info("入库货品与库存货品进行大小包装单位转换结果信息:" + resultInfo);
                }
            }
            #endregion

            if (string.IsNullOrEmpty(addQtyDtl.GoodsNo) && addQtyDtl.CompGoodsLinkID.HasValue)
            {
                ReaGoodsOrgLink goodsOrgLink = IDReaGoodsOrgLinkDao.Get(addQtyDtl.CompGoodsLinkID.Value);
                if (goodsOrgLink == null)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "根据货品关系ID[CompGoodsLinkID=" + addQtyDtl.CompGoodsLinkID.Value + "]获取不到货品关系对象";
                    ZhiFang.Common.Log.Log.Info(tempBaseResultBool.ErrorInfo);
                    throw new Exception(tempBaseResultBool.ErrorInfo);
                }
                if (goodsOrgLink.ReaGoods == null)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "根据货品关系ID[CompGoodsLinkID=" + addQtyDtl.CompGoodsLinkID.Value + "]获取不到货品对象";
                    ZhiFang.Common.Log.Log.Info(tempBaseResultBool.ErrorInfo);
                    throw new Exception(tempBaseResultBool.ErrorInfo);
                }
                addQtyDtl.GoodsNo = goodsOrgLink.ReaGoods.GoodsNo;
            }
            addQtyDtl.GoodsLotID = inDtl.GoodsLotID;
            addQtyDtl.LotNo = inDtl.LotNo;
            addQtyDtl.ProdDate = inDtl.ProdDate;
            addQtyDtl.BarCodeType = reaGoods.BarCodeMgr;
            addQtyDtl.GoodsID = reaGoods.Id;
            addQtyDtl.GoodsName = reaGoods.CName;

            addQtyDtl.CreaterID = empID;
            addQtyDtl.CreaterName = empName;
            addQtyDtl.PQtyDtlID = addQtyDtl.Id;
            addQtyDtl.ReaCompanyID = inDtl.ReaCompanyID;
            addQtyDtl.CompanyName = inDtl.CompanyName;
            addQtyDtl.ReaServerCompCode = inDtl.ReaServerCompCode;
            addQtyDtl.ReaCompCode = inDtl.ReaCompCode;

            addQtyDtl.StorageID = inDtl.StorageID;
            addQtyDtl.StorageName = inDtl.StorageName;
            addQtyDtl.PlaceID = inDtl.PlaceID;
            addQtyDtl.PlaceName = inDtl.PlaceName;
            addQtyDtl.TaxRate = inDtl.TaxRate;
            addQtyDtl.GoodsSerial = inDtl.GoodsSerial;

            addQtyDtl.ZX1 = inDtl.ZX1;
            addQtyDtl.ZX2 = inDtl.ZX2;
            addQtyDtl.ZX3 = inDtl.ZX3;
            addQtyDtl.Memo = inDtl.Memo;
            addQtyDtl.Visible = true;
            addQtyDtl.DataUpdateTime = DateTime.Now;

            addQtyDtl.UnitMemo = reaGoods.UnitMemo;
            addQtyDtl.RegisterNo = inDtl.RegisterNo;
            addQtyDtl.InvalidDate = inDtl.InvalidDate;
            addQtyDtl.InvalidWarningDate = EditCalcInvalidWarningDate(inDtl.LabID, reaGoods.BeforeWarningDay, addQtyDtl.InvalidDate);

            //获取货运单号
            if (inDtl.SaleDtlConfirmID != null)
            {
                addQtyDtl.TransportNo = ((IDReaBmsQtyDtlDao)base.DBDao).GetTransportNo(inDtl.SaleDtlConfirmID.Value);
            }

            #region 库存/入库明细的系统内部批条码生成
            #region 生成二维批条码
            if (string.IsNullOrEmpty(addQtyDtl.LotQRCode) && reaGoods.BarCodeMgr == int.Parse(ReaGoodsBarCodeType.批条码.Key) && reaGoods.IsPrintBarCode == 1)
            {
                string lotQRCode = AddGetSysPackSerial(addQtyDtl, reaGoods);
                if (string.IsNullOrEmpty(lotQRCode))
                {
                    ZhiFang.Common.Log.Log.Error("生成货品名称为:【" + reaGoods.CName + "】的系统内部批条码错误!");
                }
                else
                {
                    lotQRCode = lotQRCode + "|1|" + addQtyDtl.GoodsQty;
                    if (!string.IsNullOrEmpty(reaGoods.UnitName))
                        lotQRCode = lotQRCode + "|" + reaGoods.UnitName;
                    addQtyDtl.LotQRCode = lotQRCode;
                    addQtyDtl.SysLotSerial = lotQRCode;

                    inDtl.LotQRCode = lotQRCode;
                    inDtl.SysLotSerial = lotQRCode;
                }
            }
            else
            {
                addQtyDtl.LotQRCode = inDtl.LotQRCode;
                addQtyDtl.SysLotSerial = inDtl.SysLotSerial;
            }
            #endregion

            //生成一维批条码
            if (string.IsNullOrEmpty(addQtyDtl.LotSerial) && reaGoods.BarCodeMgr == int.Parse(ReaGoodsBarCodeType.批条码.Key) && reaGoods.IsPrintBarCode == 1)
            {
                //生成一维批条码
                long nextBarCode = -1;
                addQtyDtl.LotSerial = IBReaBmsSerial.GetNextBarCode(inDtl.LabID, ReaBmsSerialType.库存条码.Key, cenOrg, ref nextBarCode);
                if (string.IsNullOrEmpty(addQtyDtl.LotSerial))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = " Error:获取生成一维条码(LotSerial)值为空,请查看系统日志!";
                    throw new Exception(tempBaseResultBool.ErrorInfo);
                }
                inDtl.LotSerial = addQtyDtl.LotSerial;
            }
            else
            {
                addQtyDtl.LotSerial = inDtl.LotSerial;
            }
            #endregion

            this.Entity = addQtyDtl;
            tempBaseResultBool.success = this.Add();
            if (tempBaseResultBool.success == true)
            {
                //货品是否需要生成打印条码
                if (reaGoods.BarCodeMgr == int.Parse(ReaGoodsBarCodeType.盒条码.Key) && (pReaGoods.IsPrintBarCode == 1 || reaGoods.IsPrintBarCode == 1))
                {
                    tempBaseResultBool = AddUsePackSerialOfBox(cenOrg, inDtl, addQtyDtl, pReaGoods, reaGoods, empID, empName);
                }
                if (tempBaseResultBool.success == true)
                    AddReaBmsQtyDtlOperation(inDtl, addQtyDtl, operTypeID, empID, empName);
            }
            else
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】库存保存失败！", inDtl.GoodsCName);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool AddReaBmsQtyDtlByInterface(ReaBmsInDoc inDoc, ReaBmsInDtl inDtl, CenOrg cenOrg, string iSNeedCreateBarCode, long operTypeID, long empID, string empName, ref ReaBmsQtyDtl addQtyDtl)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            ReaGoods pReaGoods = null;
            ReaGoods reaGoods = IDReaGoodsDao.Get(inDtl.ReaGoods.Id, false);
            pReaGoods = reaGoods;
            if (reaGoods == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = " 获取LabID为(" + inDtl.LabID + "),货品ID(" + inDtl.ReaGoods.Id + ")的货品信息为空!";
                throw new Exception(tempBaseResultBool.ErrorInfo);
            }
            double gonvertQty = reaGoods.GonvertQty;
            if (addQtyDtl == null)
                addQtyDtl = new ReaBmsQtyDtl();
            addQtyDtl.InDtlID = inDtl.Id;
            addQtyDtl.InDocNo = inDtl.InDocNo;
            addQtyDtl.BarCodeType = (int)inDtl.BarCodeType;
            addQtyDtl.Price = inDtl.Price;
            addQtyDtl.GoodsQty = inDtl.GoodsQty;
            addQtyDtl.SumTotal = addQtyDtl.GoodsQty * addQtyDtl.Price;// inDtl.SumTotal;
            addQtyDtl.GoodsUnit = reaGoods.UnitName;
            addQtyDtl.CompGoodsLinkID = inDtl.CompGoodsLinkID;

            addQtyDtl.ReaGoodsNo = inDtl.ReaGoodsNo;
            addQtyDtl.ProdGoodsNo = inDtl.ProdGoodsNo;
            addQtyDtl.CenOrgGoodsNo = inDtl.CenOrgGoodsNo;
            addQtyDtl.GoodsNo = inDtl.GoodsNo;
            addQtyDtl.GoodsSort = reaGoods.GoodsSort;
            addQtyDtl.GoodsLotID = inDtl.GoodsLotID;
            addQtyDtl.LotNo = inDtl.LotNo;
            addQtyDtl.ProdDate = inDtl.ProdDate;
            addQtyDtl.BarCodeType = reaGoods.BarCodeMgr;
            addQtyDtl.GoodsID = reaGoods.Id;
            addQtyDtl.GoodsName = reaGoods.CName;

            addQtyDtl.CreaterID = empID;
            addQtyDtl.CreaterName = empName;
            addQtyDtl.PQtyDtlID = addQtyDtl.Id;
            addQtyDtl.ReaCompanyID = inDtl.ReaCompanyID;
            addQtyDtl.CompanyName = inDtl.CompanyName;
            addQtyDtl.ReaServerCompCode = inDtl.ReaServerCompCode;
            addQtyDtl.ReaCompCode = inDtl.ReaCompCode;

            addQtyDtl.StorageID = inDtl.StorageID;
            addQtyDtl.StorageName = inDtl.StorageName;
            addQtyDtl.PlaceID = inDtl.PlaceID;
            addQtyDtl.PlaceName = inDtl.PlaceName;
            addQtyDtl.TaxRate = inDtl.TaxRate;
            addQtyDtl.GoodsSerial = inDtl.GoodsSerial;

            addQtyDtl.ZX1 = inDtl.ZX1;
            addQtyDtl.ZX2 = inDtl.ZX2;
            addQtyDtl.ZX3 = inDtl.ZX3;
            addQtyDtl.Memo = inDtl.Memo;
            addQtyDtl.Visible = true;
            addQtyDtl.DataUpdateTime = DateTime.Now;

            addQtyDtl.UnitMemo = reaGoods.UnitMemo;
            addQtyDtl.RegisterNo = inDtl.RegisterNo;
            addQtyDtl.InvalidDate = inDtl.InvalidDate;
            addQtyDtl.InvalidWarningDate = EditCalcInvalidWarningDate(inDtl.LabID, reaGoods.BeforeWarningDay, addQtyDtl.InvalidDate);

            addQtyDtl.LotSerial = inDtl.LotSerial;
            addQtyDtl.LotQRCode = inDtl.LotQRCode;
            addQtyDtl.SysLotSerial = inDtl.SysLotSerial;
            this.Entity = addQtyDtl;
            tempBaseResultBool.success = this.Add();
            if (tempBaseResultBool.success == true)
            {
                //货品是否需要生成打印条码
                if (reaGoods.BarCodeMgr == int.Parse(ReaGoodsBarCodeType.盒条码.Key))
                {
                    tempBaseResultBool = AddUsePackSerialOfBox(cenOrg, inDtl, addQtyDtl, pReaGoods, reaGoods, empID, empName);
                }
                //库存货品为盒条码时,获取入库明细对应的(接口)供货明细条码
                if (reaGoods.BarCodeMgr == int.Parse(ReaGoodsBarCodeType.盒条码.Key))
                {
                    //if (inDtl.SaleDtlID.HasValue)
                    //{
                    //    #region 将供货盒条码信息转换为入库库存条码信息
                    //    StringBuilder boxBarCodeHql = new StringBuilder();
                    //    boxBarCodeHql.Append("reagoodsbarcodeoperation.BDtlID=" + inDtl.SaleDtlID.Value);
                    //    char[] trimChars = new char[] { ' ', 'o', 'r' };
                    //    string boxHql = "(" + boxBarCodeHql.ToString().TrimEnd(trimChars) + ") and reagoodsbarcodeoperation.BarcodeCreatType=" + ReaGoodsBarcodeOperationSerialType.条码生成.Key + " and reagoodsbarcodeoperation.OperTypeID=" + ReaGoodsBarcodeOperType.供货.Key;
                    //    IList<ReaGoodsBarcodeOperation> saleDtlBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(boxHql);
                    //    IList<ReaGoodsBarcodeOperation> qtyBarcodeList = new List<ReaGoodsBarcodeOperation>();
                    //    foreach (var saleDtlBarCode in saleDtlBarCodeList)
                    //    {
                    //        ReaGoodsBarcodeOperation barcode = ClassMapperHelp.GetMapper<ReaGoodsBarcodeOperation, ReaGoodsBarcodeOperation>(saleDtlBarCode);
                    //        barcode.LabID = addQtyDtl.LabID;
                    //        barcode.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                    //        barcode.BDocID = inDtl.InDocID;
                    //        barcode.BDocNo = inDtl.InDocNo;
                    //        barcode.BDtlID = inDtl.Id;
                    //        barcode.QtyDtlID = addQtyDtl.Id;
                    //        barcode.StorageID = addQtyDtl.StorageID;
                    //        barcode.PlaceID = addQtyDtl.PlaceID;
                    //        barcode.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key);
                    //        barcode.OperTypeID = long.Parse(ReaGoodsBarcodeOperType.验货入库.Key);
                    //        barcode.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[barcode.OperTypeID.ToString()].Name;
                    //        barcode.Visible = true;
                    //        barcode.CreaterID = empID;

                    //        barcode.CreaterName = empName;
                    //        barcode.DataUpdateTime = DateTime.Now;
                    //        barcode.GoodsID = addQtyDtl.GoodsID;
                    //        barcode.ScanCodeGoodsID = addQtyDtl.GoodsID;
                    //        barcode.GoodsUnitID = addQtyDtl.GoodsUnitID;
                    //        barcode.GoodsCName = addQtyDtl.GoodsName;

                    //        barcode.GoodsUnit = addQtyDtl.GoodsUnit;
                    //        barcode.GoodsLotID = addQtyDtl.GoodsLotID;
                    //        barcode.LotNo = addQtyDtl.LotNo;
                    //        barcode.ReaCompanyID = addQtyDtl.ReaCompanyID;
                    //        barcode.CompanyName = addQtyDtl.CompanyName;
                    //        barcode.GoodsQty = addQtyDtl.GoodsQty;
                    //        barcode.UnitMemo = addQtyDtl.UnitMemo;
                    //        barcode.DispOrder = 1;
                    //        barcode.PUsePackSerial = barcode.Id.ToString();

                    //        barcode.ReaGoodsNo = addQtyDtl.ReaGoodsNo;
                    //        barcode.ProdGoodsNo = addQtyDtl.ProdGoodsNo;
                    //        barcode.CenOrgGoodsNo = addQtyDtl.CenOrgGoodsNo;
                    //        barcode.GoodsNo = addQtyDtl.GoodsNo;
                    //        barcode.ReaCompCode = addQtyDtl.ReaCompCode;

                    //        barcode.GoodsSort = addQtyDtl.GoodsSort;
                    //        barcode.CompGoodsLinkID = addQtyDtl.CompGoodsLinkID;
                    //        barcode.BarCodeType = addQtyDtl.BarCodeType;
                    //        if (!barcode.MinBarCodeQty.HasValue) barcode.MinBarCodeQty = reaGoods.GonvertQty;
                    //        if (barcode.MinBarCodeQty <= 0) barcode.MinBarCodeQty = 1;
                    //        barcode.ScanCodeGoodsID = reaGoods.Id;
                    //        barcode.OverageQty = barcode.MinBarCodeQty;
                    //        qtyBarcodeList.Add(barcode);
                    //    }
                    //    if (qtyBarcodeList.Count > 0)
                    //        tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperationOfList(qtyBarcodeList, 0, empID, empName, inDtl.LabID);
                    //    #endregion
                    //}

                }

                //货品是批条码，且需要打印，按照智方规则生成批条码信息
                if (reaGoods.BarCodeMgr == int.Parse(ReaGoodsBarCodeType.批条码.Key) && reaGoods.IsPrintBarCode == 1)
                {
                    #region 生成二维批条码
                    string lotQRCode = AddGetSysPackSerial(addQtyDtl, reaGoods);
                    if (string.IsNullOrEmpty(lotQRCode))
                    {
                        ZhiFang.Common.Log.Log.Error("生成货品名称为:【" + reaGoods.CName + "】的系统内部批条码错误!");
                    }
                    else
                    {
                        lotQRCode = lotQRCode + "|1|" + addQtyDtl.GoodsQty;
                        if (!string.IsNullOrEmpty(reaGoods.UnitName))
                            lotQRCode = lotQRCode + "|" + reaGoods.UnitName;
                        addQtyDtl.LotQRCode = lotQRCode;
                        addQtyDtl.SysLotSerial = lotQRCode;

                        inDtl.LotQRCode = lotQRCode;
                        inDtl.SysLotSerial = lotQRCode;
                    }
                    #endregion

                    #region 生成一维批条码
                    long nextBarCode = -1;
                    addQtyDtl.LotSerial = IBReaBmsSerial.GetNextBarCode(inDtl.LabID, ReaBmsSerialType.库存条码.Key, cenOrg, ref nextBarCode);
                    if (string.IsNullOrEmpty(addQtyDtl.LotSerial))
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = " Error:获取生成一维条码(LotSerial)值为空,请查看系统日志!";
                        throw new Exception(tempBaseResultBool.ErrorInfo);
                    }
                    inDtl.LotSerial = addQtyDtl.LotSerial;
                    #endregion
                }

                if (tempBaseResultBool.success == true)
                    AddReaBmsQtyDtlOperation(inDtl, addQtyDtl, operTypeID, empID, empName);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool AddReaBmsQtyDtlOperation(ReaBmsInDtl inDtl, ReaBmsQtyDtl qtyDtl, long operTypeID, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            ReaBmsQtyDtlOperation entity = new ReaBmsQtyDtlOperation();
            entity.LabID = qtyDtl.LabID;
            entity.BDocID = inDtl.InDocID;
            entity.BDocNo = inDtl.InDocNo;
            entity.BDtlID = inDtl.Id;
            entity.QtyDtlID = qtyDtl.Id;
            entity.OperTypeID = operTypeID;// long.Parse(ReaBmsQtyDtlOperationOperType.验货入库.Key);
            entity.OperTypeName = ReaBmsQtyDtlOperationOperType.GetStatusDic()[entity.OperTypeID.ToString()].Name;
            entity.GoodsLotID = qtyDtl.GoodsLotID;
            entity.LotNo = qtyDtl.LotNo;
            entity.BarCodeType = qtyDtl.BarCodeType;
            entity.ProdDate = qtyDtl.ProdDate;
            entity.InvalidDate = qtyDtl.InvalidDate;
            entity.CreaterID = empID;

            entity.CreaterName = empName;
            entity.GoodsID = qtyDtl.GoodsID;
            entity.CompGoodsLinkID = qtyDtl.CompGoodsLinkID;
            entity.GoodsName = qtyDtl.GoodsName;
            entity.GoodsUnit = qtyDtl.GoodsUnit;
            entity.UnitMemo = qtyDtl.UnitMemo;

            entity.GoodsQty = qtyDtl.GoodsQty;
            entity.ChangeCount = qtyDtl.GoodsQty;
            entity.Price = qtyDtl.Price;
            entity.SumTotal = entity.ChangeCount * entity.Price;// qtyDtl.SumTotal;
            entity.TaxRate = qtyDtl.TaxRate;

            entity.ReaCompanyID = inDtl.ReaCompanyID;
            entity.CompanyName = inDtl.CompanyName;
            entity.ReaServerCompCode = inDtl.ReaServerCompCode;
            entity.StorageID = inDtl.StorageID;
            entity.StorageName = inDtl.StorageName;

            entity.PlaceID = inDtl.PlaceID;
            entity.PlaceName = inDtl.PlaceName;
            entity.GoodsSerial = inDtl.GoodsSerial;
            entity.LotQRCode = qtyDtl.LotQRCode;
            entity.SysLotSerial = qtyDtl.SysLotSerial;
            entity.SysLotSerial = qtyDtl.SysLotSerial;

            entity.ZX1 = inDtl.ZX1;
            entity.ZX2 = inDtl.ZX2;
            entity.ZX3 = inDtl.ZX3;
            entity.Memo = inDtl.Memo;
            entity.Visible = true;

            entity.DataUpdateTime = DateTime.Now;
            entity.ReaGoodsNo = qtyDtl.ReaGoodsNo;
            entity.ProdGoodsNo = qtyDtl.ProdGoodsNo;
            entity.CenOrgGoodsNo = qtyDtl.CenOrgGoodsNo;
            entity.GoodsNo = qtyDtl.GoodsNo;

            entity.ReaCompCode = qtyDtl.ReaCompCode;
            entity.GoodsSort = qtyDtl.GoodsSort;

            IBReaBmsQtyDtlOperation.Entity = entity;
            tempBaseResultBool.success = IBReaBmsQtyDtlOperation.Add();
            if (tempBaseResultBool.success == false)
                tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】库存操作记录保存失败！", inDtl.GoodsCName);
            return tempBaseResultBool;
        }
        #region 获取采购申请货品当前库存数量描述
        public IList<ReaGoodsCurrentQtyVO> SearchReaGoodsCurrentQtyByGoodIdStr(string idStr, string goodIdStr)
        {
            IList<ReaGoodsCurrentQtyVO> tempList = new List<ReaGoodsCurrentQtyVO>();
            if (string.IsNullOrEmpty(goodIdStr)) return tempList;
            IList<ReaGoods> reaGoodsList = new List<ReaGoods>();
            string qtyHQL = SearchSearchGoodsQtyHQL(goodIdStr, ref reaGoodsList);
            if (string.IsNullOrEmpty(qtyHQL)) return tempList;

            qtyHQL = string.Format("reabmsqtydtl.GoodsQty>0 and reabmsqtydtl.Visible=1 and ({0})", qtyHQL);
            IList<ReaBmsQtyDtl> qtyList = this.SearchListByHQL(qtyHQL);
            if (qtyList == null || qtyList.Count == 0) return tempList;

            string[] goodIdArr = goodIdStr.Split(',');
            for (int i = 0; i < goodIdArr.Length; i++)
            {
                ReaGoods reaGoods = reaGoodsList.Where(p => p.Id == long.Parse(goodIdArr[i])).ElementAt(0);
                ReaGoodsCurrentQtyVO vo = new ReaGoodsCurrentQtyVO();
                vo.CurGoodsId = reaGoods.Id;
                vo.CurrentQty = "";
                vo.GoodsQty = SearchCurrentQty(qtyList, reaGoodsList, reaGoods);
                tempList.Add(vo);
            }
            return tempList;
        }
        public double? SearchCurrentQty(IList<ReaBmsQtyDtl> qtyList, IList<ReaGoods> reaGoodsList, ReaGoods reaGoods)
        {
            double? currentQty = 0;
            //当前机构货品为最小包装单位货品时的当前库存数处理
            if (reaGoods.GonvertQty == 1)
                return currentQty = qtyList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == reaGoods.Id).Sum(p => p.GoodsQty);

            var curReaGoodsList = reaGoodsList.Where(p => p.ReaGoodsNo == reaGoods.ReaGoodsNo);
            //最小包装单位的货品
            ReaGoods minReaGoods = null;
            var minUnit = curReaGoodsList.Where(p => p.GonvertQty == 1);
            if (minUnit != null && minUnit.Count() == 1)
            {
                minReaGoods = minUnit.ElementAt(0);
            }
            if (minReaGoods != null)
            {
                var reaGoodsGroupBy = curReaGoodsList.OrderBy(p => p.GonvertQty).ToList();
                double minTotalGoodsQty = 0;
                //先将各库存货品转换为最小包装单位的库存数
                foreach (var curReaGoods in reaGoodsGroupBy)
                {
                    var sumGoodsQty = qtyList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == curReaGoods.Id).Sum(p => p.GoodsQty) * curReaGoods.GonvertQty;
                    if (sumGoodsQty.HasValue)
                        minTotalGoodsQty = minTotalGoodsQty + sumGoodsQty.Value;
                }
                //再将最小包装单位的库存数按最大包装单位转换系数进行转换
                currentQty = System.Math.Floor(minTotalGoodsQty / reaGoods.GonvertQty);
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("产品编码为:" + reaGoods.ReaGoodsNo + ",没有设置最小包装单位,无法对库存货品进行库存数量转换处理!");
            }
            return currentQty;
        }
        private string SearchSearchGoodsQtyHQL(string goodIdStr, ref IList<ReaGoods> reaGoodsList)
        {
            StringBuilder searchHQL = new StringBuilder();
            reaGoodsList = IDReaGoodsDao.GetListByHQL(string.Format("reagoods.Visible=1 and reagoods.Id in ({0})", goodIdStr));

            StringBuilder reaGoodsNoHql = new StringBuilder();
            foreach (var reaGoods in reaGoodsList)
            {
                //找出相同的ReaGoodsNo
                if (!string.IsNullOrEmpty(reaGoods.ReaGoodsNo))
                {
                    string tempHql = string.Format("(reagoods.Visible=1 and reagoods.ReaGoodsNo='{0}' and reagoods.Id!={1})", reaGoods.ReaGoodsNo, reaGoods.Id);
                    if (string.IsNullOrEmpty(reaGoodsNoHql.ToString()))
                        reaGoodsNoHql.Append(tempHql);
                    else
                    {
                        reaGoodsNoHql.Append(" or ");
                        reaGoodsNoHql.Append(tempHql);
                    }
                }
            }
            //找出相同产品编码的机构货品信息
            if (!string.IsNullOrEmpty(reaGoodsNoHql.ToString()))
            {
                IList<ReaGoods> reaGoodsList2 = IDReaGoodsDao.GetListByHQL(reaGoodsNoHql.ToString());
                //合并且剔除重复项
                reaGoodsList = reaGoodsList.Union(reaGoodsList2).ToList();
            }
            foreach (var reaGoods in reaGoodsList)
            {
                //if (!dictReaGoods.ContainsKey(reaGoods)) dictReaGoods.Add(reaGoods, reaGoods.ReaGoodsNo);
                string tempHql = string.Format("(reabmsqtydtl.GoodsQty>0 and reabmsqtydtl.GoodsID={0})", reaGoods.Id);
                if (string.IsNullOrEmpty(searchHQL.ToString()))
                    searchHQL.Append(tempHql);
                else
                {
                    searchHQL.Append(" or ");
                    searchHQL.Append(tempHql);
                }
            }
            return searchHQL.ToString();
        }
        #endregion
        public IList<ReaBmsQtyDtl> SearchReaBmsQtyDtl(string deptGoodsHql, string reaGoodsHql, string qtyHql, int groupType, bool isMergeInDocNo)
        {
            IList<ReaBmsQtyDtl> listReaBmsQtyDtl = new List<ReaBmsQtyDtl>();
            if (string.IsNullOrEmpty(qtyHql))
                qtyHql = " reabmsqtydtl.Visible=1 and reabmsqtydtl.GoodsQty>0";

            if (string.IsNullOrEmpty(deptGoodsHql))
                deptGoodsHql = "";
            if (string.IsNullOrEmpty(reaGoodsHql))
                reaGoodsHql = "";
            listReaBmsQtyDtl = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlListByHql(qtyHql, deptGoodsHql, reaGoodsHql, "", -1, -1);// this.SearchListByHQL(strWhere);

            if (listReaBmsQtyDtl != null && listReaBmsQtyDtl.Count > 0)
            {
                if (isMergeInDocNo)
                {
                    #region 入库批次合并
                    listReaBmsQtyDtl = listReaBmsQtyDtl.OrderBy(p => p.DataAddTime).GroupBy(p => new
                    {
                        p.StorageID,
                        p.PlaceID,
                        p.ReaCompanyID,
                        p.GoodsID,
                        p.LotNo,
                        p.TransportNo
                    }).Select(g => new ReaBmsQtyDtl
                    {
                        Id = g.ElementAt(0).Id,
                        PQtyDtlID = g.ElementAt(0).PQtyDtlID,
                        ReaCompanyID = g.Key.ReaCompanyID,
                        GoodsID = g.Key.GoodsID,
                        LotNo = g.Key.LotNo,
                        GoodsLotID = g.ElementAt(0).GoodsLotID,
                        StorageID = g.Key.StorageID,
                        PlaceID = g.Key.PlaceID,

                        GoodsQty = g.Sum(k => k.GoodsQty),
                        SumCurrentQty = g.Sum(k => k.GoodsQty),
                        Price = g.ElementAt(0).Price,
                        SumTotal = g.ElementAt(0).Price * g.Sum(k => k.GoodsQty),
                        GoodsName = g.ElementAt(0).GoodsName,
                        UnitMemo = g.ElementAt(0).UnitMemo,

                        RegisterNo = g.ElementAt(0).RegisterNo,
                        InDtlID = g.ElementAt(0).InDtlID,
                        InDocNo = g.ElementAt(0).InDocNo,
                        InvalidWarningDate = g.ElementAt(0).InvalidWarningDate,
                        ProdDate = g.ElementAt(0).ProdDate,
                        InvalidDate = g.ElementAt(0).InvalidDate,

                        CompanyName = g.ElementAt(0).CompanyName,
                        GoodsUnitID = g.ElementAt(0).GoodsUnitID,
                        GoodsUnit = g.ElementAt(0).GoodsUnit,

                        TaxRate = g.ElementAt(0).TaxRate,
                        OutFlag = g.ElementAt(0).OutFlag,
                        SumFlag = g.ElementAt(0).SumFlag,
                        IOFlag = g.ElementAt(0).IOFlag,
                        ZX1 = g.ElementAt(0).ZX1,

                        ZX2 = g.ElementAt(0).ZX2,
                        ZX3 = g.ElementAt(0).ZX3,
                        Memo = g.ElementAt(0).Memo,
                        DispOrder = g.ElementAt(0).DispOrder,
                        Visible = g.ElementAt(0).Visible,

                        CreaterID = g.ElementAt(0).CreaterID,
                        CreaterName = g.ElementAt(0).CreaterName,
                        DataUpdateTime = g.ElementAt(0).DataUpdateTime,
                        StorageName = g.ElementAt(0).StorageName,
                        PlaceName = g.ElementAt(0).PlaceName,

                        GoodsSerial = g.ElementAt(0).GoodsSerial,
                        LotQRCode = g.ElementAt(0).LotQRCode,
                        LotSerial = g.ElementAt(0).LotSerial,
                        SysLotSerial = g.ElementAt(0).SysLotSerial,
                        CompGoodsLinkID = g.ElementAt(0).CompGoodsLinkID,
                        ReaServerCompCode = g.ElementAt(0).ReaServerCompCode,

                        ReaGoodsNo = g.ElementAt(0).ReaGoodsNo,
                        ProdGoodsNo = g.ElementAt(0).ProdGoodsNo,
                        CenOrgGoodsNo = g.ElementAt(0).CenOrgGoodsNo,
                        GoodsNo = g.ElementAt(0).GoodsNo,
                        ReaCompCode = g.ElementAt(0).ReaCompCode,
                        GoodsSort = g.ElementAt(0).GoodsSort,
                        BarCodeType = g.ElementAt(0).BarCodeType,

                        SName = g.ElementAt(0).SName,
                        ProdOrgName = g.ElementAt(0).ProdOrgName,

                        TransportNo = g.ElementAt(0).TransportNo //新增货运单号
                    }).ToList();
                    #endregion
                }
                else
                {
                    #region 入库批次不合并显示
                    listReaBmsQtyDtl = listReaBmsQtyDtl.OrderBy(p => p.DataAddTime).GroupBy(p => new
                    {
                        p.StorageID,
                        p.PlaceID,
                        p.ReaCompanyID,
                        p.GoodsID,
                        p.LotNo,
                        p.InDocNo,
                        p.TransportNo
                    }).Select(g => new ReaBmsQtyDtl
                    {
                        Id = g.ElementAt(0).Id,
                        PQtyDtlID = g.ElementAt(0).PQtyDtlID,
                        ReaCompanyID = g.Key.ReaCompanyID,
                        GoodsID = g.Key.GoodsID,
                        LotNo = g.Key.LotNo,
                        GoodsLotID = g.ElementAt(0).GoodsLotID,
                        StorageID = g.Key.StorageID,
                        PlaceID = g.Key.PlaceID,

                        GoodsQty = g.Sum(k => k.GoodsQty),
                        SumCurrentQty = g.Sum(k => k.GoodsQty),
                        Price = g.ElementAt(0).Price,
                        SumTotal = g.ElementAt(0).Price * g.Sum(k => k.GoodsQty),
                        GoodsName = g.ElementAt(0).GoodsName,
                        UnitMemo = g.ElementAt(0).UnitMemo,

                        RegisterNo = g.ElementAt(0).RegisterNo,
                        InDtlID = g.ElementAt(0).InDtlID,
                        InDocNo = g.ElementAt(0).InDocNo,
                        InvalidWarningDate = g.ElementAt(0).InvalidWarningDate,
                        ProdDate = g.ElementAt(0).ProdDate,
                        InvalidDate = g.ElementAt(0).InvalidDate,

                        CompanyName = g.ElementAt(0).CompanyName,
                        GoodsUnitID = g.ElementAt(0).GoodsUnitID,
                        GoodsUnit = g.ElementAt(0).GoodsUnit,

                        TaxRate = g.ElementAt(0).TaxRate,
                        OutFlag = g.ElementAt(0).OutFlag,
                        SumFlag = g.ElementAt(0).SumFlag,
                        IOFlag = g.ElementAt(0).IOFlag,
                        ZX1 = g.ElementAt(0).ZX1,

                        ZX2 = g.ElementAt(0).ZX2,
                        ZX3 = g.ElementAt(0).ZX3,
                        Memo = g.ElementAt(0).Memo,
                        DispOrder = g.ElementAt(0).DispOrder,
                        Visible = g.ElementAt(0).Visible,

                        CreaterID = g.ElementAt(0).CreaterID,
                        CreaterName = g.ElementAt(0).CreaterName,
                        DataUpdateTime = g.ElementAt(0).DataUpdateTime,
                        StorageName = g.ElementAt(0).StorageName,
                        PlaceName = g.ElementAt(0).PlaceName,

                        GoodsSerial = g.ElementAt(0).GoodsSerial,
                        LotQRCode = g.ElementAt(0).LotQRCode,
                        LotSerial = g.ElementAt(0).LotSerial,
                        SysLotSerial = g.ElementAt(0).SysLotSerial,
                        CompGoodsLinkID = g.ElementAt(0).CompGoodsLinkID,
                        ReaServerCompCode = g.ElementAt(0).ReaServerCompCode,

                        ReaGoodsNo = g.ElementAt(0).ReaGoodsNo,
                        ProdGoodsNo = g.ElementAt(0).ProdGoodsNo,
                        CenOrgGoodsNo = g.ElementAt(0).CenOrgGoodsNo,
                        GoodsNo = g.ElementAt(0).GoodsNo,
                        ReaCompCode = g.ElementAt(0).ReaCompCode,
                        GoodsSort = g.ElementAt(0).GoodsSort,
                        BarCodeType = g.ElementAt(0).BarCodeType,

                        SName = g.ElementAt(0).SName,
                        ProdOrgName = g.ElementAt(0).ProdOrgName,

                        TransportNo = g.ElementAt(0).TransportNo //新增货运单号
                    }).ToList();
                    #endregion
                }

            }
            return listReaBmsQtyDtl;
        }
        public string SearchCurrentQtyInfo(IList<ReaBmsQtyDtl> qtyList, string reaGoodsNo)
        {
            string currentQty = ((IDReaBmsQtyDtlDao)base.DBDao).GetCurrentQtyInfo(qtyList, reaGoodsNo);
            return currentQty;
        }
        #region 入库确认后保存生成系统内部条码
        /// <summary>
        /// 处理入库确认后的库存信息是否需要生成盒条码信息
        /// </summary>
        /// <param name="qtyDtl"></param>
        /// <param name="pReaGoods">大包装单位的货品信息</param>
        /// <param name="reaGoods">小包装单位的货品信息</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        private BaseResultBool AddUsePackSerialOfBox(CenOrg cenOrg, ReaBmsInDtl inDtl, ReaBmsQtyDtl qtyDtl, ReaGoods pReaGoods, ReaGoods reaGoods, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (qtyDtl.GoodsQty <= 0)
            {
                tempBaseResultBool.success = true;
                tempBaseResultBool.ErrorInfo = "货品编码为:" + reaGoods.ReaGoodsNo + ",库存数量小于等于0!";
                return tempBaseResultBool;
            }

            IList<ReaGoodsBarcodeOperation> barcodeList = new List<ReaGoodsBarcodeOperation>();
            long nextBarCode = -1;
            //不存在大小包装单位转换
            if (pReaGoods == reaGoods)
            {
                if (reaGoods.IsPrintBarCode != 1)
                {
                    tempBaseResultBool.success = true;
                    return tempBaseResultBool;
                }
                double barCodeCount = 0;
                if (qtyDtl.GoodsQty.HasValue)
                    barCodeCount = Math.Ceiling(qtyDtl.GoodsQty.Value);
                AddGetBoxBarcodeList(cenOrg, inDtl, qtyDtl, reaGoods, empID, empName, null, barCodeCount, ref nextBarCode, ref barcodeList);
            }
            else
            {
                #region 生成小包装单位的父条码信息
                //大包装货品(入库货品)是否打印条码
                if (pReaGoods.IsPrintBarCode == 1)
                {
                    double pBarCodeCount = 0;
                    if (inDtl.GoodsQty.HasValue)
                        pBarCodeCount = Math.Ceiling(inDtl.GoodsQty.Value);
                    for (int pIndex = 0; pIndex < pBarCodeCount; pIndex++)
                    {
                        //生成库存货品的小包装单位的父条码信息
                        ReaGoodsBarcodeOperation pBbarcode = new ReaGoodsBarcodeOperation();
                        string pUsePackQRCode = AddGetSysPackSerial(qtyDtl, pReaGoods);
                        if (string.IsNullOrEmpty(pUsePackQRCode))
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = "生成货品名称为:【" + pReaGoods.CName + "】的系统内部盒条码错误!";
                            ZhiFang.Common.Log.Log.Error(tempBaseResultBool.ErrorInfo);
                            barcodeList.Clear();
                            return tempBaseResultBool;
                        }
                        pUsePackQRCode = pUsePackQRCode + "|" + (pIndex + 1) + "|" + inDtl.GoodsQty;
                        if (!string.IsNullOrEmpty(pReaGoods.UnitName))
                            pUsePackQRCode = pUsePackQRCode + "|" + pReaGoods.UnitName;

                        //生成一维盒条码
                        string pUsePackSerial = IBReaBmsSerial.GetNextBarCode(inDtl.LabID, ReaBmsSerialType.库存条码.Key, cenOrg, ref nextBarCode);
                        if (string.IsNullOrEmpty(pUsePackSerial))
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = " Error:获取生成一维盒条码(UsePackSerial)值为空,请查看系统日志!";
                            //throw new Exception(tempBaseResultBool.ErrorInfo);
                            barcodeList.Clear();
                            return tempBaseResultBool;
                        }

                        pBbarcode.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key);
                        pBbarcode.OperTypeID = long.Parse(ReaGoodsBarcodeOperType.验货入库.Key);
                        pBbarcode.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[pBbarcode.OperTypeID.ToString()].Name;
                        pBbarcode.LabID = inDtl.LabID;
                        pBbarcode.BDocID = inDtl.InDocID;

                        pBbarcode.BDocNo = inDtl.InDocNo;
                        pBbarcode.BDtlID = inDtl.Id;
                        pBbarcode.QtyDtlID = qtyDtl.Id;
                        pBbarcode.DispOrder = pIndex + 1;
                        pBbarcode.Visible = true;
                        pBbarcode.CreaterID = empID;

                        pBbarcode.CreaterName = empName;
                        pBbarcode.DataUpdateTime = DateTime.Now;
                        pBbarcode.GoodsID = pReaGoods.Id;
                        pBbarcode.GoodsCName = pReaGoods.CName;
                        pBbarcode.GoodsUnit = pReaGoods.UnitName;

                        pBbarcode.LotNo = inDtl.LotNo;
                        pBbarcode.ReaCompanyID = inDtl.ReaCompanyID;
                        pBbarcode.CompanyName = inDtl.CompanyName;
                        pBbarcode.SysPackSerial = pUsePackQRCode;
                        pBbarcode.UsePackQRCode = pUsePackQRCode;

                        pBbarcode.UsePackSerial = pUsePackSerial;
                        pBbarcode.PUsePackSerial = pBbarcode.Id.ToString();
                        pBbarcode.StorageID = inDtl.StorageID;
                        pBbarcode.PlaceID = inDtl.PlaceID;
                        pBbarcode.GoodsQty = inDtl.GoodsQty;
                        pBbarcode.UnitMemo = pReaGoods.UnitMemo;
                        pBbarcode.ReaGoodsNo = pReaGoods.ReaGoodsNo;

                        pBbarcode.ProdGoodsNo = inDtl.ProdGoodsNo;
                        pBbarcode.CenOrgGoodsNo = inDtl.CenOrgGoodsNo;
                        pBbarcode.GoodsNo = pReaGoods.GoodsNo;
                        pBbarcode.ReaCompCode = inDtl.ReaCompCode;
                        pBbarcode.GoodsSort = pReaGoods.GoodsSort;

                        pBbarcode.CompGoodsLinkID = inDtl.CompGoodsLinkID;
                        pBbarcode.BarCodeType = pReaGoods.BarCodeMgr;
                        if (!pBbarcode.MinBarCodeQty.HasValue)
                        {
                            double gonvertQty = 1;
                            if (reaGoods != null) gonvertQty = pReaGoods.GonvertQty;
                            if (gonvertQty <= 0)
                            {
                                ZhiFang.Common.Log.Log.Warn("货品编码为:" + pBbarcode.ReaGoodsNo + ",货品名称为:" + pBbarcode.GoodsCName + ",货品包装单位的换算系数值为" + gonvertQty + ",维护不合理!");
                                gonvertQty = 1;
                            }
                            pBbarcode.MinBarCodeQty = gonvertQty;
                        }
                        if (pBbarcode.MinBarCodeQty <= 0) pBbarcode.MinBarCodeQty = 1;
                        //pBbarcode.ScanCodeQty = 0;
                        pBbarcode.ScanCodeGoodsID = pReaGoods.Id;
                        pBbarcode.OverageQty = pBbarcode.MinBarCodeQty;
                        //pBbarcode.ScanCodeGoodsID = pBbarcode.GoodsID;
                        if (!string.IsNullOrEmpty(pBbarcode.UsePackSerial))
                        {
                            barcodeList.Add(pBbarcode);
                            //生成当前父条码的子条码信息
                            if (reaGoods.IsPrintBarCode == 1)
                            {
                                double barCodeCount = 0;
                                if (pReaGoods.GonvertQty >= 0)
                                    barCodeCount = Math.Ceiling(pReaGoods.GonvertQty);
                                AddGetBoxBarcodeList(cenOrg, inDtl, qtyDtl, reaGoods, empID, empName, pBbarcode, barCodeCount, ref nextBarCode, ref barcodeList);
                            }
                        }
                    }
                }
                else if (reaGoods.IsPrintBarCode == 1)
                {
                    //父条码不生成,只生成小包装货品条码
                    double barCodeCount = 0;
                    if (qtyDtl.GoodsQty.HasValue)
                        barCodeCount = Math.Ceiling(qtyDtl.GoodsQty.Value);
                    AddGetBoxBarcodeList(cenOrg, inDtl, qtyDtl, reaGoods, empID, empName, null, barCodeCount, ref nextBarCode, ref barcodeList);
                }
                #endregion
            }

            if (barcodeList.Count > 0)
                tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperationOfList(barcodeList, 0, empID, empName, inDtl.LabID);
            else
                tempBaseResultBool.success = true;

            return tempBaseResultBool;
        }
        /// <summary>
        /// 生成库存的系统内部盒条码集合信息
        /// </summary>
        /// <param name="cenOrg"></param>
        /// <param name="inDtl"></param>
        /// <param name="qtyDtl"></param>
        /// <param name="reaGoods"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="pBarcode"></param>
        /// <param name="barCodeCount"></param>
        /// <param name="nextBarCode"></param>
        /// <param name="barcodeList"></param>
        private void AddGetBoxBarcodeList(CenOrg cenOrg, ReaBmsInDtl inDtl, ReaBmsQtyDtl qtyDtl, ReaGoods reaGoods, long empID, string empName, ReaGoodsBarcodeOperation pBarcode, double barCodeCount, ref long nextBarCode, ref IList<ReaGoodsBarcodeOperation> barcodeList)
        {
            if (barCodeCount <= 0)
                return;

            //小数点往上取整
            barCodeCount = Math.Ceiling(barCodeCount);
            for (int cIndex = 0; cIndex < barCodeCount; cIndex++)
            {
                //生成二维盒条码
                string usePackQRCode = AddGetSysPackSerial(qtyDtl, reaGoods);
                if (string.IsNullOrEmpty(usePackQRCode))
                {
                    ZhiFang.Common.Log.Log.Error("生成货品名称为:【" + reaGoods.CName + "】的系统内部盒条码错误!");
                    barcodeList.Clear();
                    break;
                }
                string pDispOrder = "";
                if (pBarcode != null)
                    pDispOrder = pBarcode.DispOrder.ToString();
                usePackQRCode = usePackQRCode + "|" + pDispOrder + (cIndex + 1) + "|" + qtyDtl.GoodsQty;
                if (!string.IsNullOrEmpty(reaGoods.UnitName))
                    usePackQRCode = usePackQRCode + "|" + reaGoods.UnitName;

                //生成一维盒条码
                string usePackSerial = IBReaBmsSerial.GetNextBarCode(inDtl.LabID, ReaBmsSerialType.库存条码.Key, cenOrg, ref nextBarCode);
                if (string.IsNullOrEmpty(usePackSerial))
                {
                    barcodeList.Clear();
                    throw new Exception(" Error:获取生成一维盒条码值(UsePackSerial)为空,请查看系统日志!");
                }
                ReaGoodsBarcodeOperation barcode = new ReaGoodsBarcodeOperation();
                barcode.LabID = inDtl.LabID;
                barcode.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key);
                barcode.OperTypeID = long.Parse(ReaGoodsBarcodeOperType.验货入库.Key);
                barcode.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[barcode.OperTypeID.ToString()].Name;
                barcode.BDocID = inDtl.InDocID;

                barcode.BDocNo = inDtl.InDocNo;
                barcode.BDtlID = inDtl.Id;
                barcode.QtyDtlID = qtyDtl.Id;
                barcode.Visible = true;
                barcode.CreaterID = empID;

                barcode.CreaterName = empName;
                barcode.DataUpdateTime = DateTime.Now;
                barcode.GoodsID = qtyDtl.GoodsID;
                barcode.ScanCodeGoodsID = qtyDtl.GoodsID;
                barcode.GoodsUnitID = qtyDtl.GoodsUnitID;
                barcode.GoodsCName = qtyDtl.GoodsName;

                barcode.GoodsUnit = qtyDtl.GoodsUnit;
                barcode.GoodsLotID = qtyDtl.GoodsLotID;
                barcode.LotNo = qtyDtl.LotNo;
                barcode.ReaCompanyID = qtyDtl.ReaCompanyID;
                barcode.CompanyName = qtyDtl.CompanyName;
                barcode.SysPackSerial = usePackQRCode;
                barcode.UsePackQRCode = usePackQRCode;
                barcode.UsePackSerial = usePackSerial;
                barcode.StorageID = qtyDtl.StorageID;
                barcode.PlaceID = qtyDtl.PlaceID;
                barcode.GoodsQty = qtyDtl.GoodsQty;
                if (!barcode.MinBarCodeQty.HasValue) barcode.MinBarCodeQty = reaGoods.GonvertQty;
                if (barcode.MinBarCodeQty <= 0) barcode.MinBarCodeQty = 1;
                barcode.OverageQty = barcode.MinBarCodeQty;
                barcode.UnitMemo = qtyDtl.UnitMemo;
                if (pBarcode != null)
                {
                    barcode.DispOrder = pBarcode.DispOrder * 10 + (cIndex + 1);
                    barcode.PUsePackSerial = pBarcode.Id.ToString();
                }
                else
                {
                    barcode.DispOrder = cIndex + 1;
                    barcode.PUsePackSerial = barcode.Id.ToString();
                }

                barcode.ReaGoodsNo = qtyDtl.ReaGoodsNo;
                barcode.ProdGoodsNo = qtyDtl.ProdGoodsNo;
                barcode.CenOrgGoodsNo = qtyDtl.CenOrgGoodsNo;
                barcode.GoodsNo = qtyDtl.GoodsNo;
                barcode.ReaCompCode = qtyDtl.ReaCompCode;

                barcode.GoodsSort = qtyDtl.GoodsSort;
                barcode.CompGoodsLinkID = qtyDtl.CompGoodsLinkID;
                barcode.BarCodeType = qtyDtl.BarCodeType;
                //barcode.ScanCodeQty = 0;
                barcodeList.Add(barcode);
            }
        }
        /// <summary>
        /// 生成单个系统内部盒条码
        /// </summary>
        /// <param name="qtyDtl"></param>
        /// <returns></returns>
        private string AddGetSysPackSerial(ReaBmsQtyDtl qtyDtl, ReaGoods reaGoods)
        {
            string mixSerial = "";
            string prodGoodsNo = "";
            string lotNo = "";
            string invalidDate = "";
            string dtlId = "";
            string prodOrgNo = ""; //厂商机构码(货品信息的厂商名称)
            string compOrgNo = "";//供应商机构码
            if (reaGoods == null)
                reaGoods = IDReaGoodsDao.Get(qtyDtl.GoodsID.Value, false);
            try
            {
                //2019-12-24:生成的二维码不包含生产厂商的中文信息
                //prodOrgNo = reaGoods.ProdOrgName;
                if (!string.IsNullOrEmpty(qtyDtl.CenOrgGoodsNo))
                    prodGoodsNo = qtyDtl.CenOrgGoodsNo;
                if (string.IsNullOrEmpty(prodGoodsNo))
                    prodGoodsNo = qtyDtl.ProdGoodsNo;
                if (string.IsNullOrEmpty(prodGoodsNo) && reaGoods != null)
                    prodGoodsNo = reaGoods.ProdGoodsNo;
                if (string.IsNullOrEmpty(prodGoodsNo) && reaGoods != null)
                    prodGoodsNo = reaGoods.ReaGoodsNo;

                if (!string.IsNullOrEmpty(qtyDtl.LotNo))
                    lotNo = qtyDtl.LotNo;
                if (qtyDtl.InvalidDate != null)
                    invalidDate = ((DateTime)qtyDtl.InvalidDate).ToString("yyyy-MM-dd");
                dtlId = qtyDtl.Id.ToString();
                if (!string.IsNullOrEmpty(qtyDtl.ReaServerCompCode))
                    compOrgNo = qtyDtl.ReaServerCompCode;
                mixSerial = "ZFRP|1|" + prodOrgNo + "|" + prodGoodsNo + "|" +
                  lotNo + "|" + invalidDate + "|" + compOrgNo + "|" + dtlId;
            }
            catch (Exception ex)
            {
                string hint = "";
                if (string.IsNullOrEmpty(qtyDtl.ProdGoodsNo))
                    hint = "库存信息：厂商产品编号为空！";
                if (string.IsNullOrEmpty(qtyDtl.LotNo))
                    hint = "库存信息：产品批号为空！";
                if (qtyDtl.InvalidDate == null)
                    hint = "库存信息：产品有效期为空！";
                if (reaGoods == null)
                    hint = "库存信息：产品为空！";
                else if (string.IsNullOrEmpty(qtyDtl.ReaServerCompCode))
                    hint = "库存信息：产品的供应商所属机构平台编码为空！";
                throw new Exception(hint + " Error:" + ex.Message);
            }
            return mixSerial;
        }

        #endregion

        #region 库存查询,按合并项分组处理
        public IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListByGroupType(int groupType, string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsQtyDtl> entityList = new List<ReaBmsQtyDtl>();
            //如果库存查询不需要按合并项进行合并处理
            if (!ReaBmsStatisticalType.GetStatusDic().Keys.Contains(groupType.ToString()) || groupType == int.Parse(ReaBmsStatisticalType.按库存记录.Key))
            {
                entityList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlListByHql(qtyHql, deptGoodsHql, reaGoodsHql, sort, page, limit);// this.SearchListByHQL(qtyHql, sort, page, limit).list;
                Dictionary<long, ReaGoods> goodsList = new Dictionary<long, ReaGoods>();
                for (int i = 0; i < entityList.Count; i++)
                {
                    long goodId = entityList[i].GoodsID.Value;
                    if (!goodsList.ContainsKey(goodId))
                    {
                        ReaGoods goods = IDReaGoodsDao.Get(goodId, false);
                        if (goods != null)
                        {
                            goodsList.Add(goodId, goods);
                            entityList[i] = SearchStoreUpperAndLowerValue(entityList[i], goodsList[goodId]);
                        }
                    }
                    else
                    {
                        entityList[i] = SearchStoreUpperAndLowerValue(entityList[i], goodsList[goodId]);
                    }
                }
                return entityList;
            }
            EntityList<ReaBmsQtyDtl> tempEntityQtyList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlEntityListByHql(qtyHql, deptGoodsHql, reaGoodsHql, sort, -1, -1);//this.SearchListByHQL(hqlWhere, sort, -1, -1);
            IList<ReaBmsQtyDtl> tempQtyList = new List<ReaBmsQtyDtl>();
            QtyListGroupByStrategy qtyListGroupByStrategy = null;
            if (groupType == int.Parse(ReaBmsStatisticalType.按货品批号.Key))
            {
                qtyListGroupByStrategy = new GroupByReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按供应商货品.Key))
            {
                qtyListGroupByStrategy = new GroupByCompReaGoodsNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按供应商货品批号.Key))
            {
                qtyListGroupByStrategy = new GroupByCompReaGoodsNoLotNo();
            }

            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房机构货品.Key))
            {
                qtyListGroupByStrategy = new GroupByStorageReaGoodsNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房货架机构货品.Key))
            {
                qtyListGroupByStrategy = new GroupByStoragePlaceReaGoodsNo();
            }

            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房货品批号.Key))
            {
                qtyListGroupByStrategy = new GroupByStorageReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房供应商货品批号.Key))
            {
                qtyListGroupByStrategy = new GroupByStorageCompReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房货架货品批号.Key))
            {
                qtyListGroupByStrategy = new GroupByStoragePlaceReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房货架供应商货品批号.Key))
            {
                qtyListGroupByStrategy = new GroupByStoragePlaceCompReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按货品规格.Key))
            {
                qtyListGroupByStrategy = new GroupByReaGoodsNoGoodsUnit();
            }
            if (qtyListGroupByStrategy != null)
            {
                QtyListGroupByContext context = new QtyListGroupByContext(IDReaGoodsDao, tempEntityQtyList.list, qtyListGroupByStrategy);
                tempQtyList = context.SearchReaBmsQtyDtlListOfGroupBy();
            }

            //分页处理
            if (limit > 0 && limit < tempQtyList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempQtyList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    tempQtyList = list.ToList();
            }
            entityList = tempQtyList;

            return entityList;
        }
        public EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListByHql(string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            return ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlEntityListByHql(qtyHql, deptGoodsHql, reaGoodsHql, sort, page, limit);
        }
        public EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListByGroupType(int groupType, string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            entityList.list = new List<ReaBmsQtyDtl>();
            //如果库存查询不需要按合并项进行合并处理
            if (!ReaBmsStatisticalType.GetStatusDic().Keys.Contains(groupType.ToString()) || groupType == int.Parse(ReaBmsStatisticalType.按库存记录.Key))
            {
                entityList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlEntityListByHql(qtyHql, deptGoodsHql, reaGoodsHql, sort, page, limit);// this.SearchListByHQL(qtyHql, sort, page, limit);
                Dictionary<long, ReaGoods> goodsList = new Dictionary<long, ReaGoods>();
                for (int i = 0; i < entityList.list.Count; i++)
                {
                    long goodId = entityList.list[i].GoodsID.Value;
                    if (!goodsList.ContainsKey(goodId))
                    {
                        ReaGoods goods = IDReaGoodsDao.Get(goodId, false);
                        if (goods != null)
                        {
                            goodsList.Add(goodId, goods);
                            entityList.list[i] = SearchStoreUpperAndLowerValue(entityList.list[i], goods);
                        }
                    }
                    else
                    {
                        entityList.list[i] = SearchStoreUpperAndLowerValue(entityList.list[i], goodsList[goodId]);
                    }
                }
                return entityList;
            }
            EntityList<ReaBmsQtyDtl> tempEntityQtyList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlEntityListByHql(qtyHql, deptGoodsHql, reaGoodsHql, sort, -1, -1);
            IList<ReaBmsQtyDtl> tempQtyList = SearchReaBmsQtyDtlEntityListOfList(groupType, tempEntityQtyList);

            entityList.count = tempQtyList.Count();
            //分页处理
            if (limit > 0 && limit < tempQtyList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempQtyList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    tempQtyList = list.ToList();
            }
            entityList.list = tempQtyList;
            return entityList;
        }
        public EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListOfQtyMarkByGroupType(string storageId, int groupType, string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            if (string.IsNullOrEmpty(storageId)) return entityList;
            entityList.list = new List<ReaBmsQtyDtl>();

            //先找出传入库房的库房试剂关系信息
            IList<ReaStorageGoodsLink> linkList = IDReaStorageGoodsLinkDao.GetListByHQL("reastoragegoodslink.StorageID=" + storageId);
            StringBuilder goodsIdStr = new StringBuilder();
            foreach (var item in linkList)
            {
                goodsIdStr.Append(item.GoodsID);
                goodsIdStr.Append(",");
            }
            if (goodsIdStr.Length > 0)
            {
                if (string.IsNullOrEmpty(qtyHql)) qtyHql = " 1=1 ";
                qtyHql = qtyHql + " and reabmsqtydtl.GoodsID not in (" + goodsIdStr.ToString().TrimEnd(',') + ")";
                ZhiFang.Common.Log.Log.Debug("qtyHql:" + qtyHql);
            }

            //如果库存查询不需要按合并项进行合并处理
            if (!ReaBmsStatisticalType.GetStatusDic().Keys.Contains(groupType.ToString()) || groupType == int.Parse(ReaBmsStatisticalType.按库存记录.Key))
            {
                entityList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlEntityListByHql(qtyHql, deptGoodsHql, reaGoodsHql, sort, page, limit);// this.SearchListByHQL(qtyHql, sort, page, limit);
                Dictionary<long, ReaGoods> goodsList = new Dictionary<long, ReaGoods>();
                for (int i = 0; i < entityList.list.Count; i++)
                {
                    long goodId = entityList.list[i].GoodsID.Value;
                    if (!goodsList.ContainsKey(goodId))
                    {
                        ReaGoods goods = IDReaGoodsDao.Get(goodId, false);
                        if (goods != null)
                        {
                            goodsList.Add(goodId, goods);
                            entityList.list[i] = SearchStoreUpperAndLowerValue(entityList.list[i], goods);
                        }
                    }
                    else
                    {
                        entityList.list[i] = SearchStoreUpperAndLowerValue(entityList.list[i], goodsList[goodId]);
                    }
                }
                return entityList;
            }
            EntityList<ReaBmsQtyDtl> tempEntityQtyList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlEntityListByHql(qtyHql, deptGoodsHql, reaGoodsHql, sort, -1, -1);
            IList<ReaBmsQtyDtl> tempQtyList = SearchReaBmsQtyDtlEntityListOfList(groupType, tempEntityQtyList);

            entityList.count = tempQtyList.Count();
            //分页处理
            if (limit > 0 && limit < tempQtyList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempQtyList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    tempQtyList = list.ToList();
            }
            entityList.list = tempQtyList;
            return entityList;
        }

        private IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListOfList(int groupType, EntityList<ReaBmsQtyDtl> tempEntityQtyList)
        {
            IList<ReaBmsQtyDtl> tempQtyList = new List<ReaBmsQtyDtl>();
            QtyListGroupByStrategy qtyListGroupByStrategy = null;
            if (groupType == int.Parse(ReaBmsStatisticalType.按货品批号.Key))
            {
                qtyListGroupByStrategy = new GroupByReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按供应商货品.Key))
            {
                qtyListGroupByStrategy = new GroupByCompReaGoodsNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按供应商货品批号.Key))
            {
                qtyListGroupByStrategy = new GroupByCompReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房机构货品.Key))
            {
                qtyListGroupByStrategy = new GroupByStorageReaGoodsNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房货架机构货品.Key))
            {
                qtyListGroupByStrategy = new GroupByStoragePlaceReaGoodsNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房货品批号.Key))
            {
                qtyListGroupByStrategy = new GroupByStorageReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房货架货品批号.Key))
            {
                qtyListGroupByStrategy = new GroupByStoragePlaceReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房供应商货品批号.Key))
            {
                qtyListGroupByStrategy = new GroupByStorageCompReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房货架供应商货品批号.Key))
            {
                qtyListGroupByStrategy = new GroupByStoragePlaceCompReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按货品规格.Key))
            {
                qtyListGroupByStrategy = new GroupByReaGoodsNoGoodsUnit();
            }
            if (qtyListGroupByStrategy != null)
            {
                QtyListGroupByContext context = new QtyListGroupByContext(IDReaGoodsDao, tempEntityQtyList.list, qtyListGroupByStrategy);
                tempQtyList = context.SearchReaBmsQtyDtlListOfGroupBy();
            }
            return tempQtyList;
        }
        #endregion

        #region 库存预警
        private void SearchReaBmsOutDtlList(int warningType, int groupType, float storePercent, string comparisonType, int monthValue, string reaGoodsHql, ref IList<ReaBmsOutDtl> outDtlList)
        {
            if (comparisonType == QtyWarningComparisonValueType.库存预设值.Key)
                return;
            if (comparisonType == QtyWarningComparisonValueType.理论月用量.Key)
                return;

            StringBuilder outHql = new StringBuilder();
            StringBuilder outDtlHql = new StringBuilder();
            outHql.Append(" reabmsoutdoc.Visible=1");
            outHql.Append(" and reabmsoutdoc.Status=" + ReaBmsOutDocStatus.出库完成.Key);

            //上个月的最后一天
            DateTime lastDayOfPreMonth = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddDays(-1);
            if (comparisonType == QtyWarningComparisonValueType.上月使用量.Key)
            {
                DateTime firstDayOfPreMonth = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-1);
                outHql.Append(" and reabmsoutdoc.DataAddTime>='" + firstDayOfPreMonth.ToString("yyyy-MM-dd") + " 00:00:00'");
                outHql.Append(" and reabmsoutdoc.DataAddTime<='" + lastDayOfPreMonth.ToString("yyyy-MM-dd") + " 23:59:59'");
            }
            else if (comparisonType == QtyWarningComparisonValueType.月均使用量.Key)
            {
                if (monthValue > 0)
                {
                    DateTime firstDayOfPreMonth = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-monthValue);
                    outHql.Append(" and reabmsoutdoc.DataAddTime>='" + firstDayOfPreMonth.ToString("yyyy-MM-dd") + " 00:00:00'");
                    outHql.Append(" and reabmsoutdoc.DataAddTime<='" + lastDayOfPreMonth.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
            }
            else if (comparisonType == QtyWarningComparisonValueType.月用量最大值.Key)
            {
                if (monthValue > 0)
                {
                    DateTime firstDayOfPreMonth = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-monthValue);
                    outHql.Append(" and reabmsoutdoc.DataAddTime>='" + firstDayOfPreMonth.ToString("yyyy-MM-dd") + " 00:00:00'");
                    outHql.Append(" and reabmsoutdoc.DataAddTime<='" + lastDayOfPreMonth.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
            }
            ZhiFang.Common.Log.Log.Debug("库存量预警:outHql" + outHql.ToString() + ",outDtlHql:" + outDtlHql.ToString() + ",monthValue:" + monthValue);
            outDtlList = IDReaBmsOutDtlDao.SearchReaBmsOutDtlSummaryByHQL(outHql.ToString(), outDtlHql.ToString(), reaGoodsHql, "", -1, -1);
            //ZhiFang.Common.Log.Log.Debug("库存量预警:按【" + QtyWarningComparisonValueType.GetStatusDic()[comparisonType].Name + "】获取使用出库明细总记录数为:" + outDtlList.Count());
        }
        public EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListByStockWarning(int warningType, int groupType, float storePercent, string comparisonType, int monthValue, string strWhere, string reaGoodsHql, int page, int limit, string sort)
        {
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            entityList.list = new List<ReaBmsQtyDtl>();
            string hqlStr = "";
            if (warningType == 2)
            {
                hqlStr = "reabmsqtydtl.GoodsQty>0";
            }
            else
            {
                hqlStr = "1=1";
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                hqlStr = hqlStr + " and (" + strWhere + ")";
            }

            ZhiFang.Common.Log.Log.Debug("库存量预警:warningType" + warningType + ",groupType:" + groupType + ",storePercent:" + storePercent + ",comparisonType:" + comparisonType + ",monthValue:" + monthValue + ",strWhere:" + strWhere + ",reaGoodsHql:" + reaGoodsHql + ",hqlStr:" + hqlStr + ",sort:" + sort);
            string sort2 = "", direction = "";
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.Contains("DESC"))
                {
                    direction = "DESC";
                }
                else if (sort.Contains("ASC"))
                {
                    direction = "ASC";
                }

                if (sort.Contains("MonthlyUsage"))
                {
                    sort = "";
                    sort2 = "MonthlyUsage";
                }
                else if (sort.Contains("StoreLower"))
                {
                    sort = "";
                    sort2 = "StoreLower";
                }
                else if (sort.Contains("StoreUpper"))
                {
                    sort = "";
                    sort2 = "StoreUpper";
                }
                else if (sort.Contains("ComparisonValue"))
                {
                    sort = "";
                    sort2 = "ComparisonValue";
                }
            }
            IList<ReaBmsQtyDtl> qtyDtlList = new List<ReaBmsQtyDtl>();
            if (string.IsNullOrEmpty(reaGoodsHql))
                qtyDtlList = ((IDReaBmsQtyDtlDao)base.DBDao).GetListByHQL(hqlStr, sort, -1, -1).list;
            else
                qtyDtlList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlListByHql(hqlStr, "", reaGoodsHql, sort, -1, -1);
            if (qtyDtlList == null || qtyDtlList.Count <= 0) return entityList;

            //需要考虑开瓶管理未使用完的库存量
            IList<ReaOpenBottleOperDoc> oBottleList = IDReaOpenBottleOperDocDao.GetListByHQL("reaopenbottleoperdoc.IsUseCompleteFlag=0");
            if (oBottleList.Count > 0)
            {
                for (int i = 0; i < oBottleList.Count; i++)
                {
                    if (oBottleList[i].QtyDtlID.HasValue)
                        oBottleList[i].ReaBmsQtyDtl = this.Get(oBottleList[i].QtyDtlID.Value);
                }
            }

            IList<ReaBmsQtyDtl> qtyWarningList = new List<ReaBmsQtyDtl>();
            IList<ReaBmsOutDtl> outDtlList = new List<ReaBmsOutDtl>();
            SearchReaBmsOutDtlList(warningType, groupType, storePercent, comparisonType, monthValue, reaGoodsHql, ref outDtlList);
            QtyWarningGroupByStrategy qtyListGroupByStrategy = null;

            if (groupType == int.Parse(ReaBmsStatisticalType.按货品批号.Key))
            {
                qtyListGroupByStrategy = new WarnGroupByReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按供应商货品.Key))
            {
                qtyListGroupByStrategy = new WarnGroupByCompReaGoodsNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按供应商货品批号.Key))
            {
                qtyListGroupByStrategy = new WarnGroupByCompReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房货品批号.Key))
            {
                qtyListGroupByStrategy = new WarnGroupByStorageReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房供应商货品批号.Key))
            {
                qtyListGroupByStrategy = new WarnGroupByStorageCompReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房货架货品批号.Key))
            {
                qtyListGroupByStrategy = new WarnGroupByStoragePlaceReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库房货架供应商货品批号.Key))
            {
                qtyListGroupByStrategy = new WarnGroupByStoragePlaceCompReaGoodsNoLotNo();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按货品规格.Key))
            {
                qtyListGroupByStrategy = new WarnGroupByReaGoodsNoGoodsUnit();
            }
            else if (groupType == int.Parse(ReaBmsStatisticalType.按库存记录.Key))
            {
                qtyListGroupByStrategy = new WarnGroupByReaBmsQtyDtl();
            }
            else
            {
                qtyListGroupByStrategy = new WarnGroupByReaGoodsNoGoodsUnit();
            }
            if (qtyListGroupByStrategy != null)
            {
                QtyWarningGroupByContext context = new QtyWarningGroupByContext(IDReaGoodsDao, qtyDtlList, qtyListGroupByStrategy);
                qtyWarningList = context.SearchQtyWarningListOfGroupBy(outDtlList, oBottleList, warningType, storePercent, comparisonType);
                SearchQtyWarningList(sort2, direction, ref qtyWarningList);
            }

            entityList.count = qtyWarningList.Count();
            //分页处理
            if (limit > 0 && limit < qtyWarningList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = qtyWarningList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    qtyWarningList = list.ToList();
            }
            entityList.list = qtyWarningList;

            return entityList;
        }
        private void SearchQtyWarningList(string sort2, string direction, ref IList<ReaBmsQtyDtl> qtyWarningList)
        {
            if (!string.IsNullOrEmpty(sort2))
            {
                if (direction == "DESC")
                {
                    if (sort2 == "MonthlyUsage")
                    {
                        qtyWarningList = qtyWarningList.OrderByDescending(p => p.MonthlyUsage).ToList();
                    }
                    else if (sort2 == "ComparisonValue")
                    {
                        qtyWarningList = qtyWarningList.OrderByDescending(p => p.ComparisonValue).ToList();
                    }
                    else if (sort2 == "StoreLower")
                    {
                        qtyWarningList = qtyWarningList.OrderByDescending(p => p.StoreLower).ToList();
                    }
                    else if (sort2 == "StoreUpper")
                    {
                        qtyWarningList = qtyWarningList.OrderByDescending(p => p.StoreUpper).ToList();
                    }
                }
                else if (direction == "ASC")
                {
                    if (sort2 == "MonthlyUsage")
                    {
                        qtyWarningList = qtyWarningList.OrderBy(p => p.MonthlyUsage).ToList();
                    }
                    else if (sort2 == "ComparisonValue")
                    {
                        qtyWarningList = qtyWarningList.OrderBy(p => p.ComparisonValue).ToList();
                    }
                    else if (sort2 == "StoreLower")
                    {
                        qtyWarningList = qtyWarningList.OrderBy(p => p.StoreLower).ToList();
                    }
                    else if (sort2 == "StoreUpper")
                    {
                        qtyWarningList = qtyWarningList.OrderBy(p => p.StoreUpper).ToList();
                    }
                }
            }
        }
        private ReaBmsQtyDtl SearchStoreUpperAndLowerValue(ReaBmsQtyDtl entity, ReaGoods goods)
        {
            if (goods == null) return entity;

            if (goods.StoreUpper.HasValue)
                entity.StoreUpper = goods.StoreUpper.Value;
            if (goods.StoreLower.HasValue)
                entity.StoreLower = goods.StoreLower;
            entity.ReaTestQty = goods.TestCount;
            //库存所剩理论测试数计算:库存数量乘以基础数据中每包装单位理论测试数
            if (goods.TestCount > 0)
                entity.StocSurplusTestQty = entity.GoodsQty * goods.TestCount;
            return entity;
        }
        #endregion

        #region 登录后获取预警提示信息
        public BaseResultDataValue SearchReaGoodsWarningAlertInfo(long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            //库存预警模块Id
            long qtyWarningId = 4768040038907260795;
            //效期预警模块Id
            long validityWarningId = 5604388363732090217;
            //注册证预警模块Id
            long registerWarningId = 4870229821762649453;
            //获取当前登录帐号的预警模块权限信息
            IList<RBACModule> empList = IDRBACModuleDao.SearchModuleByHREmpID(empID);

            JObject jresult = new JObject();
            //库存报警
            JObject jStoreAlarm = new JObject();
            //效期已过期
            JObject jExpirationAlarm = new JObject();
            //注册证预警
            JObject jRegistAlarm = new JObject();

            //启用库存报警
            BParameter isInventoryAlarmParam = IBBParameter.GetParameterByParaNo(SYSParaNo.启用库存报警.Key);
            //启用效期报警
            BParameter isExpirationAlarmParam = IBBParameter.GetParameterByParaNo(SYSParaNo.启用效期报警.Key);
            //启用注册证预警
            BParameter isRegistAlarmParam = IBBParameter.GetParameterByParaNo(SYSParaNo.启用注册证预警.Key);

            //启用库存报警
            bool isStoreAlarm = false;
            if (isInventoryAlarmParam != null && !string.IsNullOrEmpty(isInventoryAlarmParam.ParaValue))
            {
                isStoreAlarm = int.Parse(isInventoryAlarmParam.ParaValue) == 1 ? true : false;
            }
            //库存报警模块权限判断
            if (empList == null || empList.Count() <= 0)
            {
                isStoreAlarm = false;
                ZhiFang.Common.Log.Log.Info("当前登录用户:" + empName + ",未分配库存报警模块权限!");
            }
            if (isStoreAlarm == true)
            {
                var empList2 = empList.Where(p => p.Id == qtyWarningId);
                if (empList2 == null || empList2.Count() <= 0)
                {
                    isStoreAlarm = false;
                    ZhiFang.Common.Log.Log.Info("当前登录用户:" + empName + ",未分配库存报警模块权限!");
                }
            }
            jStoreAlarm.Add("IsStoreAlarm", isStoreAlarm);
            if (isStoreAlarm == false)
            {
                //低库存预警
                jStoreAlarm.Add("HasStoreLower", false);
                //高库存预警
                jStoreAlarm.Add("HasStoreUpper", false);
            }
            else
            {
                SearchHasStoreAlarm(ref jStoreAlarm);
            }

            //启用效期报警
            bool isExpirationAlarm = false;
            if (isExpirationAlarmParam != null && !string.IsNullOrEmpty(isExpirationAlarmParam.ParaValue))
            {
                isExpirationAlarm = int.Parse(isExpirationAlarmParam.ParaValue) == 1 ? true : false;
                ZhiFang.Common.Log.Log.Debug("系统运行参数-启用效期报警开关:" + isExpirationAlarm);
            }
            //效期报警模块权限判断
            if (empList == null || empList.Count() <= 0)
            {
                isExpirationAlarm = false;
                ZhiFang.Common.Log.Log.Info("当前登录用户:" + empName + ",未分配效期报警模块权限!");
            }
            if (isExpirationAlarm == true)
            {
                var empList2 = empList.Where(p => p.Id == validityWarningId);
                if (empList2 == null || empList2.Count() <= 0)
                {
                    isExpirationAlarm = false;
                    ZhiFang.Common.Log.Log.Info("当前登录用户:" + empName + ",未分配效期报警模块权限!");
                }
            }
            jExpirationAlarm.Add("IsExpirationAlarm", isExpirationAlarm);
            if (isExpirationAlarm == false)
            {
                //效期已过期
                jExpirationAlarm.Add("HasExpired", false);
                jExpirationAlarm.Add("ExpiredDays", 1);

                //效期将过期报警
                jExpirationAlarm.Add("HasWillexpire", false);
                jExpirationAlarm.Add("WillexpireDays", 10);
            }
            else
            {
                SearchHasExpirationAlarm(ref jExpirationAlarm);
            }

            //启用注册证预警
            bool isRegistAlarm = false;
            if (isRegistAlarmParam != null && !string.IsNullOrEmpty(isRegistAlarmParam.ParaValue))
            {
                isRegistAlarm = int.Parse(isRegistAlarmParam.ParaValue) == 1 ? true : false;
            }
            //注册证预警模块权限判断
            if (empList == null || empList.Count() <= 0)
            {
                isRegistAlarm = false;
                ZhiFang.Common.Log.Log.Info("当前登录用户:" + empName + ",未分配注册证预警模块权限!");
            }
            if (isExpirationAlarm == true)
            {
                var empList2 = empList.Where(p => p.Id == registerWarningId);
                if (empList2 == null || empList2.Count() <= 0)
                {
                    isRegistAlarm = false;
                    ZhiFang.Common.Log.Log.Info("当前登录用户:" + empName + ",未分配注册证预警模块权限!");
                }
            }

            jRegistAlarm.Add("IsRegistAlarm", isRegistAlarm);
            if (isRegistAlarm == false)
            {
                //注册证已过期
                jRegistAlarm.Add("HasExpired", false);
                jRegistAlarm.Add("ExpiredDays", 1);

                //注册证将过期报警
                jRegistAlarm.Add("HasWillexpire", false);
                jRegistAlarm.Add("WillexpireDays", 10);
            }
            else
            {
                SearchHasRegistAlarm(ref jRegistAlarm);
            }
            jresult.Add("StoreAlarm", jStoreAlarm);
            jresult.Add("ExpirationAlarm", jExpirationAlarm);
            jresult.Add("RegistAlarm", jRegistAlarm);
            baseResultDataValue.ResultDataValue = jresult.ToString();
            //ZhiFang.Common.Log.Log.Info("系统预警结果:" + jresult.ToString());
            return baseResultDataValue;
        }
        private void SearchHasStoreAlarm(ref JObject jresult)
        {
            string where = "reabmsqtydtl.GoodsQty>0";
            //低库存预警
            int warningType = 1;
            int groupType = 8;//按货品规格(库存总数量)
            float storePercent = 100;//低于下限100%的货品

            string comparisonType = QtyWarningComparisonValueType.库存预设值.Key;
            int monthValue = 3;
            string reaGoodsHql = "";
            EntityList<ReaBmsQtyDtl> qtyEntityList = this.SearchReaBmsQtyDtlListByStockWarning(warningType, groupType, storePercent, comparisonType, monthValue, where, reaGoodsHql, 1, 1, "");
            if (qtyEntityList != null && qtyEntityList.count > 0)
            {
                jresult.Add("HasStoreLower", true);
            }
            else
            {
                jresult.Add("HasStoreLower", false);
            }

            //高库存预警
            warningType = 2;
            storePercent = 100;//高于上限100%的货品
            qtyEntityList = this.SearchReaBmsQtyDtlListByStockWarning(warningType, groupType, storePercent, comparisonType, monthValue, where, reaGoodsHql, 1, 1, "");
            if (qtyEntityList != null && qtyEntityList.count > 0)
            {
                jresult.Add("HasStoreUpper", true);
            }
            else
            {
                jresult.Add("HasStoreUpper", false);
            }
        }
        private void SearchHasExpirationAlarm(ref JObject jresult)
        {
            DateTime curDate = DateTime.Now;
            //效期预警默认已过期天数
            BParameter expirationExpired = IBBParameter.GetParameterByParaNo(SYSParaNo.效期预警默认已过期天数.Key);
            int expiredDays = 10;
            if (expirationExpired == null)
            {
                ZhiFang.Common.Log.Log.Info("系统没配置运行参数[效期预警默认已过期天数],已过期天数按系统取值:1");
            }
            else if (expirationExpired != null && !string.IsNullOrEmpty(expirationExpired.ParaValue))
            {
                int.TryParse(expirationExpired.ParaValue, out expiredDays);
                ZhiFang.Common.Log.Log.Info("系统配置运行参数[效期预警默认已过期天数],已过期天数值:" + expiredDays);
            }
            jresult.Add("ExpiredDays", expiredDays);
            /***
             * 效期已过期符合数据条件:InvalidWarningDate
             * 取已过效期1天及以上,库存数量大于0 并且有效期小于当前服务器时间及有效期大于等于(当前服务器时间减去已过期的天数)的时间;
             **/
            string hqlStr = string.Format("reabmsqtydtl.GoodsQty>0 and reabmsqtydtl.InvalidDate>='{0} 00:00:00' and reabmsqtydtl.InvalidDate<'{1} 23:59:59'", curDate.AddDays(-expiredDays).ToString("yyyy-MM-dd"), curDate.ToString("yyyy-MM-dd"));
            IList<ReaBmsQtyDtl> qtyList = ((IDReaBmsQtyDtlDao)base.DBDao).GetListByHQL(hqlStr);
            bool hasExpired = false;
            if (qtyList != null && qtyList.Count > 0)
                hasExpired = true;
            jresult.Add("HasExpired", hasExpired);
            ZhiFang.Common.Log.Log.Debug("是否存在效期已过期信息:" + hasExpired);

            /**
             * 效期将过期报警
             * 符合数据条件:库存数量大于0 并且有效期大于当前服务器时间及有效期小于(当前服务器时间加上输入的天数值)的时间
             **/
            int willexpireDays = 10;
            //货品效期预警天数
            BParameter expirationWillexpire = IBBParameter.GetParameterByParaNo(SYSParaNo.货品效期预警天数.Key);
            if (expirationWillexpire == null)
            {
                ZhiFang.Common.Log.Log.Info("系统没配置运行参数[货品效期预警天数],将过期预警天数按系统取值:10");
            }
            else if (expirationWillexpire != null && !string.IsNullOrEmpty(expirationWillexpire.ParaValue))
            {
                int.TryParse(expirationWillexpire.ParaValue, out willexpireDays);
                ZhiFang.Common.Log.Log.Info("系统配置运行参数[货品效期预警天数],将过期预警天数值:" + willexpireDays);
            }
            jresult.Add("WillexpireDays", willexpireDays);
            if (willexpireDays > 0)
            {
                hqlStr = string.Format("reabmsqtydtl.GoodsQty>0 and reabmsqtydtl.InvalidDate>='{0} 00:00:00' and reabmsqtydtl.InvalidDate<'{1} 23:59:59'", curDate.ToString("yyyy-MM-dd"), curDate.AddDays(willexpireDays).ToString("yyyy-MM-dd"));
                qtyList = ((IDReaBmsQtyDtlDao)base.DBDao).GetListByHQL(hqlStr);
            }
            else
            {
                qtyList.Clear();
            }
            bool hasWillexpire = false;
            if (qtyList != null && qtyList.Count > 0)
                hasWillexpire = true;
            jresult.Add("HasWillexpire", hasWillexpire);
            ZhiFang.Common.Log.Log.Debug("是否存在效期将过期报警信息:" + hasWillexpire);
        }
        private void SearchHasRegistAlarm(ref JObject jresult)
        {
            DateTime curDate = DateTime.Now;
            //注册证预警默认已过期天数
            BParameter registExpired = IBBParameter.GetParameterByParaNo(SYSParaNo.注册证预警默认已过期天数.Key);
            int expiredDays = 1;
            if (registExpired == null)
            {
                ZhiFang.Common.Log.Log.Info("系统没配置运行参数[注册证预警默认已过期天数],已过期天数按系统取值:1");
            }
            else if (registExpired != null && !string.IsNullOrEmpty(registExpired.ParaValue))
            {
                int.TryParse(registExpired.ParaValue, out expiredDays);
                ZhiFang.Common.Log.Log.Info("系统配置运行参数[注册证预警默认已过期天数],已过期天数值:" + expiredDays);
            }
            jresult.Add("ExpiredDays", expiredDays);
            /***
             * 注册证已过期
             * 取已过效期1天及以上,并且还处于启用的注册证
             **/
            string hqlStr = string.Format("reagoodsregister.Visible=1 and reagoodsregister.RegisterInvalidDate>='{0} 00:00:00' and reagoodsregister.RegisterInvalidDate<'{1} 23:59:59'", curDate.AddDays(-expiredDays).ToString("yyyy-MM-dd"), curDate.ToString("yyyy-MM-dd"));
            IList<ReaGoodsRegister> tempList = IDReaGoodsRegisterDao.GetListByHQL(hqlStr);
            bool hasExpired = false;
            if (tempList != null && tempList.Count > 0)
                hasExpired = true;
            jresult.Add("HasExpired", hasExpired);
            ZhiFang.Common.Log.Log.Debug("是否存在注册证已过期信息:" + hasExpired);

            //注册证将过期预警天数
            BParameter registWillexpire = IBBParameter.GetParameterByParaNo(SYSParaNo.注册证将过期预警天数.Key);
            int willexpireDays = 10;
            if (registWillexpire == null)
            {
                ZhiFang.Common.Log.Log.Info("系统没配置运行参数[注册证将过期预警天数],将过期预警天数按系统取值:10");
            }
            else if (registWillexpire != null && !string.IsNullOrEmpty(registWillexpire.ParaValue))
            {
                int.TryParse(registWillexpire.ParaValue, out willexpireDays);
                ZhiFang.Common.Log.Log.Info("系统配置运行参数[注册证将过期预警天数],注册证将过期预警天数值:" + willexpireDays);
            }
            jresult.Add("WillexpireDays", willexpireDays);
            /**
              * 注册证将过期报警
              * 符合数据条件:库存数量大于0 并且有效期大于当前服务器时间及有效期小于(当前服务器时间加上输入的天数值)的时间
              **/
            if (willexpireDays > 0)
            {
                hqlStr = string.Format("reagoodsregister.Visible=1 and reagoodsregister.RegisterInvalidDate>='{0} 00:00:00' and reagoodsregister.RegisterInvalidDate<'{1} 23:59:59'", curDate.ToString("yyyy-MM-dd"), curDate.AddDays(willexpireDays).ToString("yyyy-MM-dd"));
                tempList = IDReaGoodsRegisterDao.GetListByHQL(hqlStr);
            }
            else
            {
                tempList.Clear();
            }
            bool hasWillexpire = false;
            if (tempList != null && tempList.Count > 0)
                hasWillexpire = true;
            jresult.Add("HasWillexpire", hasWillexpire);
            ZhiFang.Common.Log.Log.Debug("是否存在注册证将过期报警信息:" + hasWillexpire);
        }
        #endregion

        #region 出库/移库/退库入库扫码
        public EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlByBarCode(string storageId, string placeId, string barcode, string barcodeOperType, bool isMergeInDocNo, bool isAllowOfALLStorage, ref BaseResultDataValue baseResultDataValue)
        {
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            entityList.list = new List<ReaBmsQtyDtl>();
            //JObject jBarCode = new JObject();
            if (string.IsNullOrEmpty(barcode))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "条码值为空!";
                return entityList;
            }

            #region 库存批条码精确定位
            string qtyHql = string.Format("(reabmsqtydtl.LotSerial='{0}' or reabmsqtydtl.LotQRCode='{1}')", barcode, barcode);
            if (!string.IsNullOrEmpty(storageId))
                qtyHql = qtyHql + " and reabmsqtydtl.StorageID=" + storageId;
            else
                storageId = null;
            if (!string.IsNullOrEmpty(placeId))
                qtyHql = qtyHql + " and reabmsqtydtl.PlaceID=" + placeId;
            else
                placeId = null;

            IList<ReaBmsQtyDtl> qtyList = this.SearchListByHQL(qtyHql);
            if (qtyList != null && qtyList.Count > 0)
            {
                //当前的库存批条码的库存数量是否有效(大于0)
                var tempList = qtyList.Where(p => p.GoodsQty > 0);
                if (tempList != null && tempList.Count() > 0)
                {
                    entityList.list = tempList.ToList();
                    entityList.count = qtyList.Count();

                    if (isMergeInDocNo)
                    {
                        //按照入库批次合并显示
                        entityList = SearchQtyOfMergeInDocNo(entityList.list);
                    }

                    //封装条码信息
                    SearchJBarCode(storageId, placeId, barcode, barcodeOperType, true, null, null, ref entityList, ref baseResultDataValue);
                    return entityList;
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码值为:" + barcode + ",的所属库存货品当前库存量为0!";
                    return entityList;
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("条码值为:" + barcode + ",qtyHql:" + qtyHql + ",获取库存批条码信息为空!");
            }
            #endregion
            string barCodeHql = string.Format(" (reagoodsbarcodeoperation.UsePackSerial='{0}' or reagoodsbarcodeoperation.UsePackQRCode='{1}')", barcode, barcode);
            IList<ReaGoodsBarcodeOperation> barcodeAllList = IBReaGoodsBarcodeOperation.SearchListByHQL(barCodeHql);
            #region 优先判断货品条码是否已扫码出库(在全部库房货架查找)
            //在移库没有扫码,但条码其实已在其他库房货架被扫码出库情况
            if (barcodeAllList != null && barcodeAllList.Count > 0)
            {
                //如果已经存在报损出库或退供应商,使用出库,盘亏出库扫码记录,不能再扫码
                var barcodeOperationList1 = barcodeAllList.Where(p => p.BarcodeCreatType == long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key) && (p.OperTypeID == long.Parse(ReaGoodsBarcodeOperType.报损出库.Key) || p.OperTypeID == long.Parse(ReaGoodsBarcodeOperType.退供应商.Key) || p.OperTypeID == long.Parse(ReaGoodsBarcodeOperType.使用出库.Key) || p.OperTypeID == long.Parse(ReaGoodsBarcodeOperType.盘亏出库.Key))).OrderByDescending(p => p.DataAddTime);
                ReaGoodsBarcodeOperation lastBarcodeOperation1 = null;
                double minBarCodeQty = 0;
                if (barcodeOperationList1 != null && barcodeOperationList1.Count() > 0)
                {
                    lastBarcodeOperation1 = barcodeOperationList1.ElementAt(0);
                    if (lastBarcodeOperation1.MinBarCodeQty.HasValue)
                        minBarCodeQty = lastBarcodeOperation1.MinBarCodeQty.Value;
                }
                if (lastBarcodeOperation1 != null && minBarCodeQty <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码值为:" + barcode + ",已经被" + lastBarcodeOperation1.OperTypeName + ",当前剩余扫码次数为0!";
                    ZhiFang.Common.Log.Log.Debug(baseResultDataValue.ErrorInfo);
                    return entityList;
                }
            }
            #endregion
            //盒条码在指定的库房货架查找
            bool hasBarCode = true;
            barCodeHql = string.Format(" (reagoodsbarcodeoperation.UsePackSerial='{0}' or reagoodsbarcodeoperation.UsePackQRCode='{1}')", barcode, barcode);
            if (!string.IsNullOrEmpty(storageId))
                barCodeHql = barCodeHql + " and reagoodsbarcodeoperation.StorageID=" + storageId;
            if (!string.IsNullOrEmpty(placeId))
                barCodeHql = barCodeHql + " and reagoodsbarcodeoperation.PlaceID=" + placeId;
            barcodeAllList = IBReaGoodsBarcodeOperation.SearchListByHQL(barCodeHql);
            #region 如果已经存在报损出库或退供应商,使用出库,盘亏出库扫码记录,不能再扫码          
            if (barcodeAllList != null && barcodeAllList.Count > 0)
            {
                //如果已经存在报损出库或退供应商,使用出库,盘亏出库扫码记录,不能再扫码
                var barcodeOperationList2 = barcodeAllList.Where(p => p.BarcodeCreatType == long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key) && (p.OperTypeID == long.Parse(ReaGoodsBarcodeOperType.报损出库.Key) || p.OperTypeID == long.Parse(ReaGoodsBarcodeOperType.退供应商.Key) || p.OperTypeID == long.Parse(ReaGoodsBarcodeOperType.使用出库.Key) || p.OperTypeID == long.Parse(ReaGoodsBarcodeOperType.盘亏出库.Key))).OrderByDescending(p => p.DataAddTime);
                ReaGoodsBarcodeOperation lastBarcodeOperation2 = null;
                double minBarCodeQty = 0;
                if (barcodeOperationList2 != null && barcodeOperationList2.Count() > 0)
                {
                    lastBarcodeOperation2 = barcodeOperationList2.ElementAt(0);
                    if (lastBarcodeOperation2.MinBarCodeQty.HasValue)
                        minBarCodeQty = lastBarcodeOperation2.MinBarCodeQty.Value;
                }
                if (lastBarcodeOperation2 != null && minBarCodeQty <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码值为:" + barcode + ",已经被" + lastBarcodeOperation2.OperTypeName + ",当前剩余扫码次数为0!";
                    ZhiFang.Common.Log.Log.Debug(baseResultDataValue.ErrorInfo);
                    return entityList;
                }
            }
            #endregion
            if (barcodeAllList != null && barcodeAllList.Count > 0)
            {
                #region 当前扫码类型处理
                ReaGoodsBarcodeOperation lastBarcodeOperation = null;
                if (!string.IsNullOrEmpty(barcodeOperType))
                {
                    var barcodeOperationList3 = barcodeAllList.Where(p => p.BarcodeCreatType == long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key)).OrderByDescending(p => p.DataAddTime);
                    if (barcodeOperationList3 != null && barcodeOperationList3.Count() > 0)
                    {
                        lastBarcodeOperation = barcodeOperationList3.ElementAt(0);
                        ZhiFang.Common.Log.Log.Debug("条码值为:" + barcode + ",最后一次扫码操作类型为:" + lastBarcodeOperation.OperTypeName + "!");
                    }
                    //当前扫码对应的最后条码操作记录
                    if (lastBarcodeOperation != null)
                    {
                        #region 定位库存货品
                        if (lastBarcodeOperation.QtyDtlID.HasValue)
                            qtyHql = "reabmsqtydtl.Id=" + lastBarcodeOperation.QtyDtlID;
                        else
                            qtyHql = "reabmsqtydtl.Id=" + lastBarcodeOperation.BDtlID;

                        if (!string.IsNullOrEmpty(storageId))
                            qtyHql = qtyHql + " and reabmsqtydtl.StorageID=" + storageId;
                        if (!string.IsNullOrEmpty(placeId))
                            qtyHql = qtyHql + " and reabmsqtydtl.PlaceID=" + placeId;
                        qtyList = this.SearchListByHQL(qtyHql);
                        if (qtyList != null && qtyList.Count > 0)
                        {
                            var tempList = qtyList.Where(p => p.GoodsQty > 0);
                            if (tempList != null && tempList.Count() > 0)
                            {
                                entityList.list = tempList.ToList();
                                entityList.count = qtyList.Count();
                                //封装条码信息
                                SearchJBarCode(storageId, placeId, barcode, barcodeOperType, hasBarCode, barcodeAllList, lastBarcodeOperation, ref entityList, ref baseResultDataValue);
                                return entityList;
                            }
                            else
                            {
                                if (barcodeOperType == ReaGoodsBarcodeOperType.使用出库.Key || barcodeOperType == ReaGoodsBarcodeOperType.报损出库.Key || barcodeOperType == ReaGoodsBarcodeOperType.退供应商.Key || barcodeOperType == ReaGoodsBarcodeOperType.盘亏出库.Key)
                                {
                                    if (lastBarcodeOperation.OperTypeID.Value == long.Parse(barcodeOperType))
                                    {
                                        baseResultDataValue.success = false;
                                        baseResultDataValue.ErrorInfo = "条码值为:" + barcode + ",已经被" + lastBarcodeOperation.OperTypeName + "!";
                                        ZhiFang.Common.Log.Log.Debug(baseResultDataValue.ErrorInfo);
                                    }
                                    return entityList;
                                }

                                entityList = SearchReaBmsQtyDtlByAllBarCode(storageId, placeId, barcode, barcodeOperType, isMergeInDocNo, isAllowOfALLStorage, ref baseResultDataValue);
                                if (entityList.count > 0)
                                {
                                    return entityList;
                                }
                                else
                                {
                                    baseResultDataValue.success = false;
                                    baseResultDataValue.ErrorInfo = "条码值为:" + barcode + ",的所属库存货品当前库存量为0!";
                                    ZhiFang.Common.Log.Log.Debug(baseResultDataValue.ErrorInfo);
                                    return entityList;
                                }
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug("条码值为:" + barcode + ",qtyHql:" + qtyHql + ",定位库存货品信息为空!");
                            entityList = SearchReaBmsQtyDtlByAllBarCode(storageId, placeId, barcode, barcodeOperType, isMergeInDocNo, isAllowOfALLStorage, ref baseResultDataValue);
                            if (entityList.count > 0)
                                return entityList;
                        }
                        #endregion
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("条码值为:" + barcode + ",barCodeHql:" + barCodeHql + ",获取条码操作记录息为空!");
                }
                #endregion
                #region 入库确认后生成条码
                ZhiFang.Common.Log.Log.Debug("条码值为:" + barcode + ",按入库确认后生成条码获取库存货品处理开始:");
                var inBarCodeList = barcodeAllList.Where(p => p.BarcodeCreatType == long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key) && p.OperTypeID == long.Parse(ReaGoodsBarcodeOperType.验货入库.Key));
                if (inBarCodeList.Count() > 0)
                {
                    StringBuilder strb = new StringBuilder();
                    foreach (var item in inBarCodeList)
                    {
                        if (item.QtyDtlID.HasValue)
                            strb.Append(item.QtyDtlID + ",");
                        else
                            strb.Append(item.BDtlID + ",");
                    }
                    qtyHql = "reabmsqtydtl.Id in(" + strb.ToString().TrimEnd(',') + ")";
                    if (!string.IsNullOrEmpty(storageId))
                        qtyHql = qtyHql + " and reabmsqtydtl.StorageID=" + storageId;
                    if (!string.IsNullOrEmpty(placeId))
                        qtyHql = qtyHql + " and reabmsqtydtl.PlaceID=" + placeId;
                    qtyList = this.SearchListByHQL(qtyHql);
                    if (qtyList != null && qtyList.Count > 0)
                    {
                        var tempList = qtyList.Where(p => p.GoodsQty > 0);
                        if (tempList != null && tempList.Count() > 0)
                        {
                            entityList.list = tempList.ToList();
                            entityList.count = qtyList.Count();
                            //封装条码信息
                            SearchJBarCode(storageId, placeId, barcode, barcodeOperType, hasBarCode, barcodeAllList, inBarCodeList.ElementAt(0), ref entityList, ref baseResultDataValue);
                            return entityList;
                        }
                        else
                        {
                            entityList = SearchReaBmsQtyDtlByAllBarCode(storageId, placeId, barcode, barcodeOperType, isMergeInDocNo, isAllowOfALLStorage, ref baseResultDataValue);
                            if (entityList.count > 0)
                            {
                                return entityList;
                            }
                            else
                            {

                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "条码值为:" + barcode + ",所属库存货品当前库存量为0!";
                                return entityList;
                            }
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("条码值为:" + barcode + ",qtyHql:" + qtyHql + ",获取入库确认后生成条码的库存货品信息为空!");
                    }
                }
                #endregion
                #region 验收入库扫码
                inBarCodeList = barcodeAllList.Where(p => p.BarcodeCreatType == long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key) && (p.OperTypeID == long.Parse(ReaGoodsBarcodeOperType.验货入库.Key) || p.OperTypeID == long.Parse(ReaGoodsBarcodeOperType.库存初始化.Key)));
                if (inBarCodeList.Count() > 0)
                {
                    StringBuilder strb = new StringBuilder();
                    foreach (var item in inBarCodeList)
                    {
                        if (item.QtyDtlID.HasValue)
                            strb.Append(item.QtyDtlID + ",");
                        else
                            strb.Append(item.BDtlID + ",");
                    }
                    qtyHql = "reabmsqtydtl.Id in(" + strb.ToString().TrimEnd(',') + ")";
                    if (!string.IsNullOrEmpty(storageId))
                        qtyHql = qtyHql + " and reabmsqtydtl.StorageID=" + storageId;
                    if (!string.IsNullOrEmpty(placeId))
                        qtyHql = qtyHql + " and reabmsqtydtl.PlaceID=" + placeId;
                    qtyList = this.SearchListByHQL(qtyHql);
                    if (qtyList != null && qtyList.Count > 0)
                    {

                        var tempList = qtyList.Where(p => p.GoodsQty > 0);
                        if (tempList != null && tempList.Count() > 0)
                        {
                            entityList.list = tempList.ToList();
                            entityList.count = qtyList.Count();
                            //封装条码信息
                            SearchJBarCode(storageId, placeId, barcode, barcodeOperType, hasBarCode, barcodeAllList, inBarCodeList.ElementAt(0), ref entityList, ref baseResultDataValue);
                            return entityList;
                        }
                        else
                        {
                            entityList = SearchReaBmsQtyDtlByAllBarCode(storageId, placeId, barcode, barcodeOperType, isMergeInDocNo, isAllowOfALLStorage, ref baseResultDataValue);
                            if (entityList.count > 0)
                            {
                                return entityList;
                            }
                            else
                            {
                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "条码值为:" + barcode + ",的所属库存货品当前库存量为0!";
                                return entityList;
                            }
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("条码值为:" + barcode + ",qtyHql:" + qtyHql + ",获取定位验收入库扫码后的库存货品信息为空!");
                        entityList = SearchReaBmsQtyDtlByAllBarCode(storageId, placeId, barcode, barcodeOperType, isMergeInDocNo, isAllowOfALLStorage, ref baseResultDataValue);
                        if (entityList.count > 0)
                            return entityList;
                    }
                }
                #endregion
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("从条码操作记录表获取条码为:" + barcode + ",barCodeHql:" + barCodeHql + ",条码操作记录信息为空!");
                entityList = SearchReaBmsQtyDtlByAllBarCode(storageId, placeId, barcode, barcodeOperType, isMergeInDocNo, isAllowOfALLStorage, ref baseResultDataValue);
                if (entityList.count > 0) return entityList;
            }

            //是否精准定位，根据参数判断 by douss 2021-09-07
            BParameter parameter = IBBParameter.GetParameterByParaNo(SYSParaNo.出库货品扫码.Key);
            ZhiFang.Common.Log.Log.Info("参数出库货品扫码配置值:" + parameter.ParaValue);
            //配置为严格模式，则不再分析条码；混合模式，继续
            if (parameter.ParaValue.Trim() == "1")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "未能获取到该条码的货品信息!";
                return entityList;
            }

            #region 解析条码并定位库存货品
            if (barcode.Length < SysPublicSet.UseSerialNoLength)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "系统不识别当前的出库条码:" + barcode + ",条码长度小于系统设置的长度:" + SysPublicSet.UseSerialNoLength + "!";
                ZhiFang.Common.Log.Log.Debug(baseResultDataValue.ErrorInfo);
                return entityList;
            }

            ZhiFang.Common.Log.Log.Debug("通过条码规则解析扫码条码并定位库存货品处理开始:");
            Dictionary<string, Dictionary<string, string>> dicMultiKey = new Dictionary<string, Dictionary<string, string>>();
            int barCodeType = -1;//1:一维条码;2:二维条码
            BaseResultDataValue brdv = IBReaCenBarCodeFormat.DecodingSanBarCode(barcode, ref dicMultiKey, ref barCodeType);
            if (brdv.success == false)
            {
                return entityList;
            }

            foreach (KeyValuePair<string, Dictionary<string, string>> kv in dicMultiKey)
            {
                qtyHql = SearchQtyHqlByBarCodeFormat(kv.Value);
                if (!string.IsNullOrEmpty(storageId))
                    qtyHql = qtyHql + " and reabmsqtydtl.StorageID=" + storageId;
                if (!string.IsNullOrEmpty(placeId))
                    qtyHql = qtyHql + " and reabmsqtydtl.PlaceID=" + placeId;
                if (!string.IsNullOrEmpty(qtyHql))
                {
                    qtyHql = "(" + qtyHql + ") and reabmsqtydtl.GoodsQty>0";
                    IList<ReaBmsQtyDtl> tempList = this.SearchListByHQL(qtyHql);
                    if (tempList != null && tempList.Count > 0)
                        entityList.list = entityList.list.Union(tempList).ToList();
                }
            }
            if (entityList.list != null)
            {
                if (isMergeInDocNo == true)
                {
                    entityList = SearchQtyOfMergeInDocNo(entityList.list);
                }
                else
                {
                    entityList.count = entityList.list.Count;
                }
                //封装条码信息
                SearchJBarCode(storageId, placeId, barcode, barcodeOperType, false, null, null, ref entityList, ref baseResultDataValue);
            }
            if (entityList.count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "通过条码规则解析扫码条码并获取库存货品信息为空!";
            }
            #endregion
            return entityList;
        }
        /// <summary>
        /// 出库扫码时,按指定库房货架找不到库存货品时,再按所有库房货架查找
        /// </summary>
        /// <param name="storageId"></param>
        /// <param name="placeId"></param>
        /// <param name="barcode"></param>
        /// <param name="barcodeOperType"></param>
        /// <param name="isMergeInDocNo">(相同供货商+相同库房+相同货架+相同货品ID+相同货品批号+效期+入库批次)是否按入库批次合并显示库存货品</param>
        /// <param name="baseResultDataValue"></param>
        /// <returns></returns>
        private EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlByAllBarCode(string storageId, string placeId, string barcode, string barcodeOperType, bool isMergeInDocNo, bool isAllowOfALLStorage, ref BaseResultDataValue baseResultDataValue)
        {
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            entityList.list = new List<ReaBmsQtyDtl>();
            //移库或出库扫码是否允许从所有库房获取库存货品
            ZhiFang.Common.Log.Log.Debug("货品扫码时,按指定库房货架扫码找不到条码时,是否允许从所有库房获取库存货品:" + isAllowOfALLStorage);
            if (!isAllowOfALLStorage)
            {
                //baseResultDataValue.success = false;
                //baseResultDataValue.ErrorInfo = "货品扫码时,按指定库房货架扫码找不到条码时,系统运行参数设置为不允许从所有库房获取库存货品!";
                return entityList;
            }
            //盒条码是否在指定的库房货架里
            bool hasBarCode = true;
            //如果条码在指定的库房货架找不到,就在所有的条码操作记录里获取扫码条码信息
            if (barcodeOperType == ReaGoodsBarcodeOperType.使用出库.Key)
            {
                ZhiFang.Common.Log.Log.Debug("货品扫码时,按指定库房货架扫码找不到条码时,再按所有库房货架查找处理开始");
                hasBarCode = false;
                string barCodeHql = string.Format(" (reagoodsbarcodeoperation.UsePackSerial='{0}' or reagoodsbarcodeoperation.UsePackQRCode='{1}')", barcode, barcode);
                IList<ReaGoodsBarcodeOperation> barcodeAllList = IBReaGoodsBarcodeOperation.SearchListByHQL(barCodeHql);
                ReaGoodsBarcodeOperation lastBarcode = null;
                if (barcodeAllList.Count > 0)
                    lastBarcode = barcodeAllList.OrderByDescending(p => p.DataAddTime).ElementAt(0);
                if (lastBarcode != null)
                {
                    //获取扫码条码对应的机构货品信息,所属供应商信息及批号信息
                    string qtyHql = string.Format(" reabmsqtydtl.ReaCompanyID={0} and reabmsqtydtl.GoodsID={1} and reabmsqtydtl.LotNo='{2}' ", lastBarcode.ReaCompanyID, lastBarcode.GoodsID, lastBarcode.LotNo);
                    if (!string.IsNullOrEmpty(storageId))
                        qtyHql = qtyHql + " and reabmsqtydtl.StorageID=" + storageId;
                    if (!string.IsNullOrEmpty(placeId))
                        qtyHql = qtyHql + " and reabmsqtydtl.PlaceID=" + placeId;

                    IList<ReaBmsQtyDtl> qtyList = this.SearchListByHQL(qtyHql);
                    if (qtyList != null && qtyList.Count > 0)
                    {
                        var tempList = qtyList.Where(p => p.GoodsQty > 0);
                        if (tempList != null && tempList.Count() > 0)
                        {
                            if (isMergeInDocNo == true)
                            {
                                ZhiFang.Common.Log.Log.Debug("货品扫码--条码值为:" + barcode + ",按(相同供货商+相同库房+相同货架+相同货品ID+相同货品批号+效期+入库批次)按入库批次合并显示库存货品!");
                                entityList = SearchQtyOfMergeInDocNo(tempList.ToList());
                            }
                            else
                            {
                                entityList.list = tempList.ToList();
                                entityList.count = qtyList.Count();
                            }

                            //封装条码信息
                            SearchJBarCode(storageId, placeId, barcode, barcodeOperType, hasBarCode, barcodeAllList, lastBarcode, ref entityList, ref baseResultDataValue);
                            return entityList;
                        }
                        else
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "条码值为:" + barcode + ",的所属库存货品当前库存量为0!";
                            return entityList;
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("条码值为:" + barcode + ",qtyHql:" + qtyHql + ",定位库存货品信息为空!");
                    }
                }
            }
            return entityList;
        }
        private EntityList<ReaBmsQtyDtl> SearchQtyOfMergeInDocNo(IList<ReaBmsQtyDtl> qtyList)
        {
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            entityList.list = new List<ReaBmsQtyDtl>();
            if (qtyList == null || qtyList.Count <= 0) return entityList;

            var tempGroupBy = qtyList.GroupBy(p => new
            {
                p.ReaCompanyID,
                p.StorageID,
                p.PlaceID,
                p.GoodsID,
                p.LotNo,
                p.InvalidDate,
                p.TransportNo
                //p.InDocNo
            }).OrderBy(p => p.Key.InvalidDate);
            entityList.count = tempGroupBy.Count();
            foreach (var groupBy in tempGroupBy)
            {
                ReaBmsQtyDtl qty = ClassMapperHelp.GetMapper<ReaBmsQtyDtl, ReaBmsQtyDtl>(groupBy.ElementAt(0));
                qty.GoodsQty = groupBy.Sum(k => k.GoodsQty);
                qty.SumCurrentQty = groupBy.Sum(k => k.GoodsQty);
                qty.SumTotal = groupBy.Sum(k => k.SumTotal);
                entityList.list.Add(qty);
            }
            return entityList;
        }
        /// <summary>
        /// 按条码操作记录封装条码信息
        /// </summary>
        /// <param name="barcode">当前扫码条码值</param>
        /// <param name="barcodeOperType">当前扫码类型</param>
        /// <param name="barcodeAllList">当前扫码的条码所有记录信息</param>
        /// <param name="barcodeOperation">当前扫码的条码记录信息</param>
        /// <param name="entityList">当前扫码对应的库存记录</param>
        private void SearchJBarCode(string storageId, string placeId, string barcode, string barcodeOperType, bool hasBarCode, IList<ReaGoodsBarcodeOperation> barcodeAllList, ReaGoodsBarcodeOperation barcodeOperation, ref EntityList<ReaBmsQtyDtl> entityList, ref BaseResultDataValue baseResultDataValue)
        {
            IList<ReaGoods> reaGoodsList = new List<ReaGoods>();
            for (int i = 0; i < entityList.list.Count; i++)
            {
                if (!entityList.list[i].SumCurrentQty.HasValue)
                    entityList.list[i].SumCurrentQty = entityList.list[i].GoodsQty;
                JObject jBarCode = new JObject();
                ReaGoods reaGoods = null;
                var goodId = entityList.list[i].GoodsID.Value;
                if (barcodeOperation != null)
                    goodId = barcodeOperation.GoodsID.Value;
                var tempList1 = reaGoodsList.Where(p => p.Id == goodId);
                if (tempList1 != null && tempList1.Count() > 0)
                {
                    reaGoods = tempList1.ElementAt(0);
                }
                else
                {
                    reaGoods = IDReaGoodsDao.Get(goodId, false);
                    if (reaGoods != null && !reaGoodsList.Contains(reaGoods))
                        reaGoodsList.Add(reaGoods);
                }
                if (reaGoods == null)
                    continue;

                double price = entityList.list[i].Price.Value;
                //当前条码的本次扫码总次数
                jBarCode.Add("ScanCodeQty", 1);
                jBarCode.Add("StorageID", storageId);
                jBarCode.Add("PlaceID", placeId);
                double overageQty = 1;
                if (barcodeOperation == null)
                {
                    jBarCode.Add("UsePackSerial", barcode);
                    jBarCode.Add("UsePackQRCode", barcode);
                    jBarCode.Add("SysPackSerial", "");
                    jBarCode.Add("OtherPackSerial", barcode);
                    jBarCode.Add("PUsePackSerial", "");
                    jBarCode.Add("GoodsID", entityList.list[i].GoodsID.Value.ToString());
                    jBarCode.Add("ScanCodeGoodsID", entityList.list[i].GoodsID.Value.ToString());
                    jBarCode.Add("GoodsCName", entityList.list[i].GoodsName);
                    jBarCode.Add("ReaGoodsNo", entityList.list[i].ReaGoodsNo);
                    jBarCode.Add("ProdGoodsNo", entityList.list[i].ProdGoodsNo);
                    jBarCode.Add("CenOrgGoodsNo", entityList.list[i].CenOrgGoodsNo);
                    jBarCode.Add("BarCodeType", entityList.list[i].BarCodeType.ToString());
                    jBarCode.Add("GoodsUnit", reaGoods.UnitName);
                    jBarCode.Add("UnitMemo", reaGoods.UnitMemo);
                    //扫码条码的货品的转换系数值
                    jBarCode.Add("GonvertQty", reaGoods.GonvertQty);
                    //当前条码的最小包装单位货品的条码数
                    jBarCode.Add("MinBarCodeQty", reaGoods.GonvertQty);
                    //扫码条码绑定的库存记录ID
                    jBarCode.Add("QtyDtlID", entityList.list[i].Id.ToString());
                    jBarCode.Add("Price", price);
                    if (barcodeOperType == ReaGoodsBarcodeOperType.使用出库.Key || barcodeOperType == ReaGoodsBarcodeOperType.报损出库.Key || barcodeOperType == ReaGoodsBarcodeOperType.退供应商.Key || barcodeOperType == ReaGoodsBarcodeOperType.盘亏出库.Key)
                        overageQty = reaGoods.GonvertQty;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("库存记录ID:" + entityList.list[i].Id + ",当前扫码条码的库存记录ID:" + barcodeOperation.QtyDtlID);
                    long? qtyDtlID = barcodeOperation.QtyDtlID;
                    if (hasBarCode == false)
                    {
                        qtyDtlID = entityList.list[i].Id;
                    }
                    jBarCode.Add("UsePackSerial", barcodeOperation.UsePackSerial);
                    jBarCode.Add("UsePackQRCode", barcodeOperation.UsePackQRCode);
                    jBarCode.Add("SysPackSerial", barcodeOperation.SysPackSerial);
                    jBarCode.Add("OtherPackSerial", barcodeOperation.OtherPackSerial);
                    jBarCode.Add("PUsePackSerial", barcodeOperation.PUsePackSerial);
                    jBarCode.Add("GoodsID", barcodeOperation.GoodsID.Value.ToString());
                    string scanCodeGoodsID = "";
                    if (barcodeOperation.ScanCodeGoodsID.HasValue)
                    {
                        scanCodeGoodsID = barcodeOperation.ScanCodeGoodsID.Value.ToString();
                    }
                    else
                    {
                        scanCodeGoodsID = barcodeOperation.GoodsID.Value.ToString();
                    }
                    jBarCode.Add("ScanCodeGoodsID", scanCodeGoodsID);

                    jBarCode.Add("GoodsCName", barcodeOperation.GoodsCName);
                    jBarCode.Add("ReaGoodsNo", barcodeOperation.ReaGoodsNo);
                    jBarCode.Add("ProdGoodsNo", barcodeOperation.ProdGoodsNo);
                    jBarCode.Add("CenOrgGoodsNo", barcodeOperation.CenOrgGoodsNo);
                    jBarCode.Add("BarCodeType", barcodeOperation.BarCodeType.ToString());
                    jBarCode.Add("DispOrder", barcodeOperation.DispOrder);
                    jBarCode.Add("GoodsUnit", barcodeOperation.GoodsUnit);
                    jBarCode.Add("UnitMemo", barcodeOperation.UnitMemo);
                    jBarCode.Add("GonvertQty", reaGoods.GonvertQty);

                    jBarCode.Add("MinBarCodeQty", barcodeOperation.MinBarCodeQty);
                    if (barcodeOperation.QtyDtlID.HasValue && qtyDtlID.Value != entityList.list[i].Id)
                    {
                        ReaBmsQtyDtl qtyDtl = this.Get(qtyDtlID.Value);
                        if (qtyDtl != null)
                            price = qtyDtl.Price.Value;
                    }
                    jBarCode.Add("Price", price);
                    jBarCode.Add("QtyDtlID", qtyDtlID.Value.ToString());
                    if (entityList.list[i].BarCodeType.ToString() == ReaGoodsBarCodeType.盒条码.Key)
                    {
                        if (hasBarCode == false)
                        {
                            overageQty = reaGoods.GonvertQty;
                        }
                        else
                        {
                            //获取剩余的扫码条码数(最小包装单位)
                            overageQty = IBReaGoodsBarcodeOperation.SearchOverageQty(barcode, barcodeAllList);
                        }
                        //转换为当前移库扫码的包装单位数
                        if (barcodeOperType == ReaGoodsBarcodeOperType.移库入库.Key || barcodeOperType == ReaGoodsBarcodeOperType.退库入库.Key)
                            overageQty = overageQty / reaGoods.GonvertQty;
                        if (overageQty > entityList.list[i].GoodsQty.Value)
                            overageQty = entityList.list[i].GoodsQty.Value;
                    }
                }
                //非盒条码的库存数处理
                if (entityList.list[i].BarCodeType.ToString() != ReaGoodsBarCodeType.盒条码.Key)
                {
                    if (entityList.list[i].GoodsQty.HasValue)
                        overageQty = entityList.list[i].GoodsQty.Value;
                    else
                        overageQty = 0;
                }
                jBarCode.Add("OverageQty", overageQty);
                entityList.list[i].JObjectBarCode = jBarCode.ToString();
                //当前扫码条码的剩余库存数
                if (entityList.list[i].BarCodeType.ToString() == ReaGoodsBarCodeType.盒条码.Key)
                {
                    ZhiFang.Common.Log.Log.Debug("库存记录ID:" + entityList.list[i].Id + ",当前扫码条码的原剩余库存数:" + entityList.list[i].GoodsQty);
                    if (barcodeOperType == ReaGoodsBarcodeOperType.移库入库.Key || barcodeOperType == ReaGoodsBarcodeOperType.退库入库.Key)
                        entityList.list[i].GoodsQty = overageQty * reaGoods.GonvertQty;
                    else
                        entityList.list[i].GoodsQty = overageQty;
                    ZhiFang.Common.Log.Log.Debug("库存记录ID:" + entityList.list[i].Id + ",当前扫码条码的剩余库存数:" + entityList.list[i].GoodsQty);
                }
                if (overageQty <= 0)
                {
                    entityList.list.Remove(entityList.list[i]);
                    entityList.count = entityList.count - 1;
                }
            }
            if (entityList.count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "当前扫码条码的库存货品的剩余库存数为0,不能继续当前扫码操作!";
            }
        }

        private string SearchQtyHqlByBarCodeFormat(Dictionary<string, string> dicKey)
        {
            string compOrgNo = "";//供应商机构码
            string cenOrgGoodsNo = "";//供应商货品编码
            string lotNo = "";//批号
            string invalidDate = "";//效期
            char[] trimChars = new char[] { ' ', 'a', 'n', 'd' };
            if (dicKey.ContainsKey("ProdOrgNo") && dicKey["ProdOrgNo"] != null)
            {
                compOrgNo = dicKey["ProdOrgNo"];
            }
            if (dicKey.ContainsKey("ProdGoodsNo") && dicKey["ProdGoodsNo"] != null)
            {
                cenOrgGoodsNo = dicKey["ProdGoodsNo"];
            }
            if (dicKey.ContainsKey("LotNo") && dicKey["LotNo"] != null)
            {
                lotNo = dicKey["LotNo"];
            }
            if (dicKey.ContainsKey("InvalidDate") && dicKey["InvalidDate"] != null)
            {
                invalidDate = dicKey["InvalidDate"];
            }
            //合并条件
            StringBuilder qtyStrb = new StringBuilder();
            if (!string.IsNullOrEmpty(compOrgNo))
            {
                qtyStrb.Append(" reabmsqtydtl.ReaServerCompCode='" + compOrgNo + "' and");
            }
            if (!string.IsNullOrEmpty(cenOrgGoodsNo))
            {
                qtyStrb.Append(" (reabmsqtydtl.CenOrgGoodsNo='" + cenOrgGoodsNo + "' or reabmsqtydtl.ReaGoodsNo='" + cenOrgGoodsNo + "' or reabmsqtydtl.ProdGoodsNo='" + cenOrgGoodsNo + "') and");
            }
            if (!string.IsNullOrEmpty(lotNo))
            {
                qtyStrb.Append(" reabmsqtydtl.LotNo='" + lotNo + "' and");
            }

            string hql = qtyStrb.ToString().TrimEnd(trimChars);

            if (!string.IsNullOrEmpty(invalidDate))
            {
                //ZhiFang.Common.Log.Log.Debug("货品扫码解析出的货品有效期原始值为:" + invalidDate);
                DateTime dt;
                bool result = DateTime.TryParse(invalidDate, out dt);
                if (result == false && !invalidDate.Contains("-") && !invalidDate.Contains("/"))
                    dt = DateTime.ParseExact(invalidDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                if (dt != null)
                    hql = hql + " and (reabmsqtydtl.InvalidDate>='" + dt.ToString("yyyy-MM-dd") + "' and reabmsqtydtl.InvalidDate<='" + dt.ToString("yyyy-MM-dd") + "  23:59:59')";
            }
            return hql;
        }
        #endregion

        #region 库存导出Excel
        public FileStream SearchExportExcelReaBmsQtyDtlByGroupType(long labId, int qtyType, int groupType, string hqlWhere, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit, ref string fileName)
        {
            FileStream fileStream = null;

            IList<ReaBmsQtyDtl> qtyList = this.SearchReaBmsQtyDtlListByGroupType(groupType, hqlWhere, deptGoodsHql, reaGoodsHql, sort, page, limit);
            if (qtyList != null && qtyList.Count > 0)
            {
                Dictionary<string, string> columns = new Dictionary<string, string>();
                string strHeaderText = "";
                fileName = "";
                if (qtyType == 2)
                {
                    strHeaderText = "效期已过期报警信息";
                    fileName = "效期已过期报警信息.xlsx";
                    columns = ExcelReportHelp.GetValidityWarningColumns();
                }
                else if (qtyType == 3)
                {
                    strHeaderText = "效期将过期报警信息";
                    fileName = "效期将过期报警信息.xlsx";
                    columns = ExcelReportHelp.GetValidityWarningColumns();
                }
                else
                {
                    columns = ExcelReportHelp.GetReaBmsQtyDtlColumns();
                    strHeaderText = "库存清单信息";
                    fileName = "库存清单信息.xlsx";
                }
                DataTable dtSource = ExcelReportHelp.ExportExcelToDataTable<ReaBmsQtyDtl>(qtyList, columns);

                string savePath = NPOIExcelToExporHelp.GetSaveExcelPath(labId, qtyType == 1 ? "库存清单" : "效期预警");
                string filePath = savePath + "\\" + DateTime.Now.ToString("yyMMddhhmmss") + "-" + fileName;
                try
                {
                    //单元格字体颜色的处理
                    Dictionary<string, short> cellFontStyleList = new Dictionary<string, short>();
                    cellFontStyleList.Add("库存数量", NPOI.HSSF.Util.HSSFColor.Red.Index);

                    fileStream = NPOIExcelToExporHelp.ExportDatatoExcel(dtSource, strHeaderText, filePath, cellFontStyleList);
                    if (fileStream != null)
                    {
                        fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    }
                }
                catch (Exception ee)
                {
                    Common.Log.Log.Error(strHeaderText + "导出失败:" + ee.Message);
                    throw ee;
                }
            }
            return fileStream;
        }
        public FileStream SearchExportExcelByStockWarning(long labId, int warningType, int groupType, float storePercent, string comparisonType, int monthValue, string where, string reaGoodsHql, string sort, ref string fileName)
        {
            FileStream fileStream = null;
            EntityList<ReaBmsQtyDtl> qtyList = this.SearchReaBmsQtyDtlListByStockWarning(warningType, groupType, storePercent, comparisonType, monthValue, where, reaGoodsHql, -1, -1, sort);
            if (qtyList != null && qtyList.count > 0)
            {
                Dictionary<string, string> columns = new Dictionary<string, string>();
                string strHeaderText = "";
                fileName = "";
                if (warningType == 1)
                {
                    strHeaderText = "低库存预警信息";
                    fileName = "低库存预警信息.xlsx";
                }
                else if (warningType == 2)
                {
                    strHeaderText = "高库存预警信息";
                    fileName = "高库存预警信息.xlsx";
                }
                else
                {
                    return fileStream;
                }

                columns = ExcelReportHelp.GetStockWarningColumns();
                DataTable dtSource = ExcelReportHelp.ExportExcelToDataTable<ReaBmsQtyDtl>(qtyList.list, columns);

                string savePath = NPOIExcelToExporHelp.GetSaveExcelPath(labId, "库存预警");
                string filePath = savePath + "\\" + DateTime.Now.ToString("yyMMddhhmmss") + "-" + fileName;
                try
                {
                    //单元格字体颜色的处理
                    Dictionary<string, short> cellFontStyleList = new Dictionary<string, short>();
                    cellFontStyleList.Add("库存数量", NPOI.HSSF.Util.HSSFColor.Red.Index);

                    fileStream = NPOIExcelToExporHelp.ExportDatatoExcel(dtSource, strHeaderText, filePath, cellFontStyleList);
                    if (fileStream != null)
                    {
                        fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    }
                }
                catch (Exception ee)
                {
                    Common.Log.Log.Error(strHeaderText + "导出失败:" + ee.Message);
                    throw ee;
                }
            }
            return fileStream;
        }
        public Stream SearchReaBmsQtyDtlOfExcelByQtyHql(long labId, string labCName, string breportType, int groupType, string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, string frx, ref string fileName)
        {
            Stream stream = null;
            if (string.IsNullOrEmpty(sort)) sort = "";
            IList<ReaBmsQtyDtl> qtyList = this.SearchReaBmsQtyDtlListByGroupType(groupType, qtyHql, deptGoodsHql, reaGoodsHql, sort, -1, -1);
            if (string.IsNullOrEmpty(frx))
                frx = "库存清单.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = "库存清单" + fileExt;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsQtyDtl, ReaBmsQtyDtl>(null, qtyList, excelCommand, breportType, labId, frx, excelFile, ref saveFullPath);
            fileName = "库存清单信息" + fileExt;
            return stream;
        }
        #endregion

        #region 库存接口查询
        public string QueryReaGoodsStockXML(string goodsNo, string lastModifyTime, string resultFieldList, string resultType)
        {
            string result = "";
            string hqlGoodsNo = "";
            if (!string.IsNullOrWhiteSpace(goodsNo))
            {
                IList<ReaGoods> listReaGoods = IDReaGoodsDao.GetListByHQL(" reagoods.MatchCode=\'" + goodsNo + "\'");
                if (listReaGoods != null && listReaGoods.Count > 0)
                {
                    foreach (ReaGoods goods in listReaGoods)
                    {
                        hqlGoodsNo = (string.IsNullOrEmpty(hqlGoodsNo) ? goods.Id.ToString() : hqlGoodsNo + "," + goods.Id.ToString());
                    }
                    hqlGoodsNo = " and reabmsqtydtl.GoodsID in (" + hqlGoodsNo + ")";
                }
            }
            IList<ReaBmsQtyDtl> listReaBmsQtyDtl = this.SearchListByHQL(" reabmsqtydtl.DataUpdateTime>\'" + lastModifyTime + "\'" + hqlGoodsNo);
            if (listReaBmsQtyDtl != null && listReaBmsQtyDtl.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(resultFieldList))
                    resultFieldList = "Id,GoodsID,GoodsName,ReaGoodsNo,LotNo,InvalidDate,ProdDate,GoodsQty,Price,SumTotal,GoodsUnit,UnitMemo,DataUpdateTime";
                string[] listField = resultFieldList.Split(',');
                StringBuilder strXml = new StringBuilder();
                foreach (ReaBmsQtyDtl entity in listReaBmsQtyDtl)
                {
                    strXml.Append("<Row>");
                    ReaGoods goods = IDReaGoodsDao.Get((long)entity.GoodsID);
                    foreach (string field in listField)
                    {
                        if (string.IsNullOrWhiteSpace(field))
                            continue;
                        System.Reflection.PropertyInfo propertyInfo = entity.GetType().GetProperty(field);
                        if (propertyInfo != null)
                        {
                            object value = propertyInfo.GetValue(entity, null);
                            string strValue = (value == null ? "" : value.ToString());
                            strXml.Append("<" + field + ">" + strValue + "</" + field + ">");
                        }
                    }
                    if (goods != null)
                        strXml.Append("<MatchCode>" + goods.MatchCode + "</MatchCode>");
                    strXml.Append("</Row>");
                }
                result = strXml.ToString();
            }
            return result;
        }

        #endregion 

        public BaseResultBool UpdateVerificationByReaGoodsLot(ReaGoodsLot reaGoodsLot)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(reaGoodsLot.LotNo))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "货品批号不能为空!";
                return baseResultBool;
            }
            if (string.IsNullOrEmpty(reaGoodsLot.ReaGoodsNo) && reaGoodsLot.GoodsID.HasValue)
            {
                ReaGoods reaGoods = IDReaGoodsDao.Get(reaGoodsLot.GoodsID.Value);
                if (reaGoods != null)
                    reaGoodsLot.ReaGoodsNo = reaGoods.ReaGoodsNo;
            }
            if (string.IsNullOrEmpty(reaGoodsLot.ReaGoodsNo))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "货品批号的货品编码不能为空!";
                return baseResultBool;
            }
            if (!reaGoodsLot.VerificationStatus.HasValue)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "货品信息的性能验证结果为空!";
                return baseResultBool;
            }
            IList<ReaBmsQtyDtl> qtyList = this.SearchListByHQL(" reabmsqtydtl.LotNo='" + reaGoodsLot.LotNo + "' and reabmsqtydtl.ReaGoodsNo='" + reaGoodsLot.ReaGoodsNo + "'");
            if (qtyList == null || qtyList.Count <= 0)
            {
                baseResultBool.success = true;
                return baseResultBool;
            }
            List<string> tmpa = new List<string>();
            foreach (var qty in qtyList)
            {
                tmpa.Clear();

                tmpa.Add("Id=" + qty.Id + " ");
                tmpa.Add("VerificationStatus=" + reaGoodsLot.VerificationStatus + " ");
                tmpa.Add("IsNeedPerformanceTest=" + reaGoodsLot.IsNeedPerformanceTest + " ");

                //tmpa.Add("VerificationStatus=" + reaGoodsLot.VerificationStatus + " ");
                //tmpa.Add("IsNeedPerformanceTest=" + reaGoodsLot.IsNeedPerformanceTest + " ");
                //tmpa.Add("VerificationStatus=" + reaGoodsLot.VerificationStatus + " ");
                //tmpa.Add("IsNeedPerformanceTest=" + reaGoodsLot.IsNeedPerformanceTest + " ");

                this.Entity = qty;
                this.Update(tmpa.ToArray());
            }
            return baseResultBool;
        }
        public IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlAndReaGoodsListByAllJoinHql(string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsQtyDtl> entityList = new List<ReaBmsQtyDtl>();
            entityList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlAndReaGoodsListByAllJoinHql(qtyHql, deptGoodsHql, reaGoodsHql, sort, page, limit);
            return entityList;
        }
        public EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListOfQtyGEZeroByJoinHql(string where, string inDtlHql, string qtyHql, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            entityList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlEntityListOfQtyGEZeroByJoinHql(where, inDtlHql, qtyHql, reaGoodsHql, sort, page, limit);
            return entityList;
        }
        public IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListOfQtyGEZeroByJoinHql(string where, string inDtlHql, string qtyHql, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsQtyDtl> entityList = new List<ReaBmsQtyDtl>();
            entityList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlListOfQtyGEZeroByJoinHql(where, inDtlHql, qtyHql, reaGoodsHql, sort, page, limit);
            return entityList;
        }
        #region EChart图表统计
        public EntityList<EChartsVO> SearchQtyEChartsVOListByHql(int statisticType, bool showZero, string startDate, string endDate, string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort)
        {
            EntityList<EChartsVO> entityList = new EntityList<EChartsVO>();
            entityList.list = new List<EChartsVO>();
            IList<ReaBmsQtyDtl> dtlList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlAndReaGoodsListByAllJoinHql(qtyHql, deptGoodsHql, reaGoodsHql, sort, -1, -1);
            switch (statisticType)
            {
                case 1://按库房
                    entityList.list = SearchQtyEChartsVOListOfGroupByStorage(dtlList, showZero);
                    break;
                case 2://按供货商
                    entityList.list = SearchQtyEChartsVOListOfGroupByComp(dtlList, showZero);
                    break;
                case 3://按品牌
                    //SearchQtyEChartsVOListBySetGoodsInfo(ref dtlList);
                    entityList.list = SearchQtyEChartsVOListOfGroupByProdOrg(dtlList, showZero);
                    break;
                case 4://按货品一级分类
                    //SearchQtyEChartsVOListBySetGoodsInfo(ref dtlList);
                    entityList.list = SearchQtyEChartsVOListOfGroupByGoodsClass(dtlList, showZero);
                    break;
                case 5://按货品二级分类
                    //SearchQtyEChartsVOListBySetGoodsInfo(ref dtlList);
                    entityList.list = SearchQtyEChartsVOListOfGroupByProdOrg(dtlList, showZero);
                    break;
                default:

                    break;
            }
            if (entityList.list != null && entityList.list.Count > 0) entityList.list = entityList.list.OrderByDescending(p => p.SumTotal).ToList();
            entityList.count = entityList.list.Count();
            return entityList;
        }
        private void SearchQtyEChartsVOListBySetGoodsInfo(ref IList<ReaBmsQtyDtl> dtlList)
        {
            if (dtlList == null || dtlList.Count <= 0) return;

            IList<ReaGoods> allReaGoodsList = IDReaGoodsDao.LoadAll();//false
            for (int i = 0; i < dtlList.Count; i++)
            {
                if (!dtlList[i].GoodsID.HasValue) continue;

                long goodsID = dtlList[i].GoodsID.Value;
                var tempList = allReaGoodsList.Where(p => p.Id == goodsID);
                if (tempList != null && tempList.Count() > 0)
                {
                    dtlList[i].GoodsClass = tempList.ElementAt(0).GoodsClass;
                    dtlList[i].GoodsClassType = tempList.ElementAt(0).GoodsClassType;
                    dtlList[i].ProdOrgName = tempList.ElementAt(0).ProdOrgName;
                }
            }
        }
        private IList<EChartsVO> SearchQtyEChartsVOListOfGroupByStorage(IList<ReaBmsQtyDtl> dtlList, bool showZero)
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

            //没有库存信息的库房处理
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
        private IList<EChartsVO> SearchQtyEChartsVOListOfGroupByComp(IList<ReaBmsQtyDtl> dtlList, bool showZero)
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

            //没有库存信息的供货商处理
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
        private IList<EChartsVO> SearchQtyEChartsVOListOfGroupByGoodsClass(IList<ReaBmsQtyDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.GoodsClass
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

            //没有库存记录的一级分类处理
            string hql2 = "reagoods.Visible=1";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and reagoods.GoodsClass not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<ReaGoodsClassVO> goodsclassList = IDReaGoodsDao.SearchGoodsClassListByClassTypeAndHQL("goodsclass", false, hql2, "", -1, -1);
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
        private IList<EChartsVO> SearchQtyEChartsVOListOfGroupByGoodsClassType(IList<ReaBmsQtyDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.GoodsClassType
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

            //没有库存记录的二级分类处理
            string hql2 = "reagoods.Visible=1";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and reagoods.GoodsClassType not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<ReaGoodsClassVO> coodsClassTypeList = IDReaGoodsDao.SearchGoodsClassListByClassTypeAndHQL("goodsclasstype", false, hql2, "", -1, -1);
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
        private IList<EChartsVO> SearchQtyEChartsVOListOfGroupByProdOrg(IList<ReaBmsQtyDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.ProdOrgName
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

            //没有库存记录的库房处理
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
        public BaseResultDataValue SearchStackQtyEChartsVOListByHql(int statisticType, string startDate, string endDate, string dtlHql, string deptGoodsHql, string reaGoodsHql)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<ReaBmsQtyDtl> dtlList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlAndReaGoodsListByAllJoinHql(dtlHql, deptGoodsHql, reaGoodsHql, "", -1, -1);
            //SearchQtyEChartsVOListBySetGoodsInfo(ref dtlList);
            string goodsClassHql = "reagoods.Visible=1";
            IList<ReaGoodsClassVO> goodsclassList = IDReaGoodsDao.SearchGoodsClassListByClassTypeAndHQL("goodsclass", true, goodsClassHql, "", -1, -1);
            IList<ReaGoodsClassVO> goodsClassTypeList = IDReaGoodsDao.SearchGoodsClassListByClassTypeAndHQL("goodsclasstype", true, goodsClassHql, "", -1, -1);
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
            jresult.Add("allSumTotal", Math.Round(allSumTotal.Value, 2));

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
                case 11:
                    //一级分类金额及金额占比处理
                    JArray goodsClassList = new JArray();
                    var groupBy = dtlList.GroupBy(p => new
                    {
                        p.GoodsClass
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
                            var tempList = dtlList.Where(p => p.GoodsClass == voGoodsclass.Key.CName && p.GoodsClassType == voGoodsClassType.Key.CName);
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

        public BaseResultBool UpdateReaBmsQtyDtlByQtyDtlMark(QtyMarkVO entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "未指定更新信息!";
                return tempBaseResultBool;
            }
            if (!entity.StorageID.HasValue)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "未指定库房信息!";
                return tempBaseResultBool;
            }
            if (entity.ReaGoodNoList == null || entity.ReaGoodNoList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "未指定库存货品信息!";
                return tempBaseResultBool;
            }

            foreach (var reaGoodNo in entity.ReaGoodNoList)
            {
                if (string.IsNullOrEmpty(reaGoodNo)) continue;

                string hql = string.Format("update ReaBmsQtyDtl reabmsqtydtl set reabmsqtydtl.QtyDtlMark={0} where reabmsqtydtl.GoodsQty<=0 and reabmsqtydtl.StorageID={1} and reabmsqtydtl.ReaGoodsNo='{2}'", entity.QtyDtlMark, entity.StorageID, reaGoodNo);
                int counts = ((IDReaBmsQtyDtlDao)base.DBDao).UpdateByHql(hql);
                if (counts <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "更新库房ID为:" + entity.StorageID + ",库存货品编码为:" + reaGoodNo + ",库存标志值为:" + entity.QtyDtlMark + "失败!";
                    return tempBaseResultBool;
                }
            }

            return tempBaseResultBool;
        }

        #region 四川大家试剂投屏需求

        /// <summary>
        /// 获取投屏的数据列表
        /// </summary>
        public EntityList<ReaGoodsStockWarning> SearchReaGoodsStockWarningList(int page, int limit, string where, string sort, ref string warningPromptInfo)
        {
            EntityList<ReaGoodsStockWarning> entityList = new EntityList<ReaGoodsStockWarning>();
            string reaGoodsHql = "reagoods.Visible=1";

            IList<ReaBmsQtyDtl> qtyDtlList = ((IDReaBmsQtyDtlDao)base.DBDao).SearchReaBmsQtyDtlListByHql(where, "", reaGoodsHql, sort, -1, -1);
            if (qtyDtlList.Count == 0)
            {
                throw new Exception("系统内未获取到库存记录信息！");
            }

            IList<ReaGoodsStockWarning> warningList = ReaBmsQtyDtlWarningListOfGroupBy(qtyDtlList);
            IList<ReaGoodsStockWarning> warningList_return = ReaBmsQtyDtlWarningList_StockWarningByGoods(warningList);

            //获取预警提示的概要信息，比如：库存预警：低预警：2种   高库存预警：3种 未设置3种 ；效期预警： 已过期：1个 将过期：2个 未设置：0种
            StringBuilder sbPromptInfo = new StringBuilder();
            sbPromptInfo.Append("库存预警:");
            sbPromptInfo.Append(string.Format("正常:{0}种 ", warningList_return.Where(p => p.StockWarningByGoods == "正常").Select(p => p.GoodsID).Distinct().Count()));
            sbPromptInfo.Append(string.Format("低预警:{0}种 ", warningList_return.Where(p => p.StockWarningByGoods == "低预警").Select(p => p.GoodsID).Distinct().Count()));
            sbPromptInfo.Append(string.Format("高预警:{0}种 ", warningList_return.Where(p => p.StockWarningByGoods == "高预警").Select(p => p.GoodsID).Distinct().Count()));
            sbPromptInfo.Append(string.Format("未设置:{0}种 ;", warningList_return.Where(p => p.StockWarningByGoods == "未设置").Select(p => p.GoodsID).Distinct().Count()));
            sbPromptInfo.Append("效期预警:");
            sbPromptInfo.Append(string.Format("正常:{0}个 ", warningList_return.Where(p => p.ValidDateWarning == "正常").Sum(p => p.GoodsQty)));
            sbPromptInfo.Append(string.Format("已过期:{0}个 ", warningList_return.Where(p => p.ValidDateWarning == "已过期").Sum(p => p.GoodsQty)));
            sbPromptInfo.Append(string.Format("将过期:{0}个 ", warningList_return.Where(p => p.ValidDateWarning == "将过期").Sum(p => p.GoodsQty)));
            sbPromptInfo.Append(string.Format("未设置:{0}个 ", warningList_return.Where(p => p.ValidDateWarning == "未设置").Sum(p => p.GoodsQty)));
            warningPromptInfo = sbPromptInfo.ToString();
            //排序
            warningList_return.OrderBy(p => p.ReaCompanyID).ThenBy(p => p.GoodsClass).ThenBy(p => p.GoodsID).ThenBy(p => p.LotNo).ThenBy(p => p.ProdOrgName).ThenBy(p => p.UnitName).ThenBy(p => p.UnitMemo).ToList();
            entityList.count = warningList_return.Count;
            //分页处理
            if (limit > 0 && limit < warningList_return.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = warningList_return.Skip(startIndex).Take(endIndex);
                if (list != null)
                    warningList_return = list.ToList();
            }
            entityList.list = warningList_return;

            return entityList;
        }

        /// <summary>
        /// 只按照货品，获取总的库存预警状态
        /// 生成最终的list返回
        /// </summary>
        /// <returns></returns>
        private IList<ReaGoodsStockWarning> ReaBmsQtyDtlWarningList_StockWarningByGoods(IList<ReaGoodsStockWarning> warningList)
        {
            IList<ReaGoods> goodsList = IDReaGoodsDao.GetListByHQL("");

            foreach (var entity in warningList)
            {
                var l = goodsList.Where(p => p.Id == entity.GoodsID).ToList();
                if (l.Count() > 0)
                {
                    var goodsQtySum = warningList.Where(p => p.GoodsID == entity.GoodsID).Sum(p => p.GoodsQty.Value);
                    entity.StockWarningByGoods = GetStockWarning(l[0], goodsQtySum);
                }
            }

            return warningList;
        }

        /// <summary>
        /// 将库存表数据，转换到ReaGoodsStockWarning的list
        /// 并获取到库存预警、效期预警信息
        /// </summary>
        /// <param name="qtyDtlList">库存数据列表</param>
        /// <returns></returns>
        private IList<ReaGoodsStockWarning> ReaBmsQtyDtlWarningListOfGroupBy(IList<ReaBmsQtyDtl> qtyDtlList)
        {
            IList<ReaGoodsStockWarning> retnrnList = qtyDtlList.GroupBy(p => new
            {
                p.ReaCompanyID,
                p.GoodsID,
                p.GoodsClass,
                p.GoodsClassType,
                p.ReaGoodsNo,
                p.LotNo,
                p.ProdOrgName,
                p.GoodsUnit,
                p.UnitMemo,
                p.InvalidDate
            }).Select(g => new ReaGoodsStockWarning
            {
                ReaCompanyID = g.Key.ReaCompanyID,
                CompanyName = g.ElementAt(0).CompanyName,
                GoodsClass = g.ElementAt(0).GoodsClass,
                GoodsClassType = g.ElementAt(0).GoodsClassType,
                GoodsID = g.ElementAt(0).GoodsID,
                GoodsCName = g.ElementAt(0).GoodsName,
                ReaGoodsNo = g.ElementAt(0).ReaGoodsNo,
                LotNo = g.ElementAt(0).LotNo,
                UnitMemo = g.ElementAt(0).UnitMemo,
                UnitName = g.ElementAt(0).GoodsUnit,
                ProdOrgName = g.ElementAt(0).ProdOrgName,
                GoodsQty = g.Sum(k => k.GoodsQty),
                Price = g.ElementAt(0).Price,
                SumTotal = g.Sum(k => k.SumTotal),
                InvalidDate = g.ElementAt(0).InvalidDate,
                DataAddTime = g.ElementAt(0).DataAddTime,
                StockWarning = GetStockWarning(g.ElementAt(0).ReaGoods, g.Sum(k => k.GoodsQty).Value),
                ValidDateWarning = GetValidDateWarning(g.ElementAt(0).ReaGoods, g.ElementAt(0).InvalidDate.Value)

            }).ToList();
            return retnrnList;
        }

        /// <summary>
        /// 判断获取库存预警的状态：低预警、高预警、正常、未设置
        /// 这是按照库存表里货品+一级分类+二级分类+厂家+批号+单位+规格+效期判断后得到的货品的状态
        /// </summary>
        /// <returns></returns>
        private string GetStockWarning(ReaGoods reaGoods, double curGoodsQty)
        {
            string stockWarning = "";

            if (reaGoods.StoreLower != null && reaGoods.StoreLower.Value > 0 && reaGoods.StoreUpper != null && reaGoods.StoreUpper.Value > 0)
            {
                //上限和下限都设置：得出的状态：低预警、高预警、正常
                if (reaGoods.StoreLower.Value <= curGoodsQty && curGoodsQty <= reaGoods.StoreUpper.Value)
                {
                    stockWarning = "正常";
                }
                else if (curGoodsQty < reaGoods.StoreLower.Value)
                {
                    stockWarning = "低预警";
                }
                else if (curGoodsQty > reaGoods.StoreUpper.Value)
                {
                    stockWarning = "高预警";
                }
            }
            else if ((reaGoods.StoreLower == null || reaGoods.StoreLower.Value == 0) && (reaGoods.StoreUpper == null || reaGoods.StoreUpper.Value == 0))
            {
                //上限和下限都没设置：得出的状态：未设置
                stockWarning = "未设置";
            }
            else if (reaGoods.StoreLower != null && reaGoods.StoreLower.Value > 0 && (reaGoods.StoreUpper == null || reaGoods.StoreUpper.Value == 0))
            {
                //上限未设置，下限设置，比如下限=10：判断库存数<10，状态=低预警；库存数>=10，状态=上限未设置
                if (curGoodsQty >= reaGoods.StoreLower.Value)
                {
                    stockWarning = "未设置";
                }
                else
                {
                    stockWarning = "低预警";
                }
            }
            else if ((reaGoods.StoreLower == null || reaGoods.StoreLower.Value == 0) && reaGoods.StoreUpper != null && reaGoods.StoreUpper.Value > 0)
            {
                //下限未设置，上限设置，比如上限=20：判断库存数>=20，状态=高预警；库存数<20，状态=下限未设置
                if (curGoodsQty >= reaGoods.StoreUpper.Value)
                {
                    stockWarning = "高预警";
                }
                else
                {
                    stockWarning = "未设置";
                }
            }

            return stockWarning;
        }

        /// <summary>
        /// 判断获取效期预警的状态：正常、已过期、将过期、未设置
        /// </summary>
        /// <returns></returns>
        private string GetValidDateWarning(ReaGoods reaGoods, DateTime dtInvalidDate)
        {
            string validDateWarning = "";
            DateTime dtNow = DateTime.Now;
            if (reaGoods.BeforeWarningDay == null || reaGoods.BeforeWarningDay.Value == 0)
            {
                validDateWarning = "未设置";
            }
            else if (dtInvalidDate < dtNow)
            {
                validDateWarning = "已过期";
            }
            else
            {
                TimeSpan sp = dtInvalidDate.Subtract(dtNow);
                if (sp.Days > reaGoods.BeforeWarningDay)
                {
                    validDateWarning = "正常";
                }
                else
                {
                    validDateWarning = "将过期";
                }
            }

            return validDateWarning;
        }

        #endregion
    }
}


