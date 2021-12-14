using System;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Common.Public;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsQtyDtlOperation : BaseBLL<ReaBmsQtyDtlOperation>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsQtyDtlOperation
    {
        public override bool Add()
        {
            this.Entity.SumTotal = this.Entity.Price * this.Entity.GoodsQty;
            return base.Add();
        }
        public BaseResultDataValue AddReaBmsQtyDtlOutOperation(ReaBmsOutDoc outDoc, ReaBmsOutDtl outDtl, ReaBmsQtyDtl qtyDtl, double curoutGoodsQty)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ReaBmsQtyDtlOperation entity = new ReaBmsQtyDtlOperation();
            long? empID = null;
            if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                empID = long.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            string empName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            entity.BDocID = outDoc.Id;
            entity.BDocNo = outDoc.OutDocNo;
            entity.BDtlID = outDtl.Id;
            entity.QtyDtlID = qtyDtl.Id;
            switch (outDoc.OutType)
            {
                case 1:
                    entity.OperTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.使用出库.Key);
                    break;
                case 2:
                    entity.OperTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.退库入库.Key);
                    break;
                case 3:
                    entity.OperTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.报损出库.Key);
                    break;
                case 4:
                    entity.OperTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.退供应商.Key);
                    break;
                case 5:
                    entity.OperTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.使用出库.Key);
                    break;
                case 6:
                    entity.OperTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.盘亏出库.Key);
                    break;
                default:
                    entity.OperTypeID = long.Parse(ReaBmsQtyDtlOperationOperType.使用出库.Key);
                    break;
            }
            entity.OperTypeName = ReaBmsQtyDtlOperationOperType.GetStatusDic()[entity.OperTypeID.ToString()].Name;
            entity.ReaCompanyID = qtyDtl.ReaCompanyID;
            entity.GoodsLotID = qtyDtl.GoodsLotID;
            if (!entity.GoodsLotID.HasValue)
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
            entity.GoodsQty = 0 - curoutGoodsQty;// outDtl.GoodsQty;
            entity.ChangeCount = System.Math.Abs(curoutGoodsQty);
            entity.Price = qtyDtl.Price;
            entity.SumTotal = entity.GoodsQty * entity.Price;// outDtl.SumTotal;

            entity.TaxRate = qtyDtl.TaxRate;
            entity.ReaCompanyID = qtyDtl.ReaCompanyID;
            entity.CompanyName = qtyDtl.CompanyName;
            entity.ReaServerCompCode = qtyDtl.ReaServerCompCode;
            entity.ReaCompCode = qtyDtl.ReaCompCode;
            entity.StorageID = outDtl.StorageID;

            entity.StorageName = outDtl.StorageName;
            entity.PlaceID = outDtl.PlaceID;
            entity.PlaceName = outDtl.PlaceName;
            entity.GoodsSerial = qtyDtl.GoodsSerial;
            entity.LotSerial = qtyDtl.LotSerial;

            entity.SysLotSerial = qtyDtl.SysLotSerial;
            entity.ZX1 = outDtl.ZX1;
            entity.ZX2 = outDtl.ZX2;
            entity.ZX3 = outDtl.ZX3;
            entity.Memo = outDtl.Memo;

            entity.Visible = true;
            entity.OutFlag = 1;
            entity.DataUpdateTime = DateTime.Now;
            entity.ReaGoodsNo = qtyDtl.ReaGoodsNo;
            entity.ProdGoodsNo = qtyDtl.ProdGoodsNo;
            entity.CenOrgGoodsNo = qtyDtl.CenOrgGoodsNo;
            entity.GoodsNo = qtyDtl.GoodsNo;
            this.Entity = entity;
            brdv.success = this.Add();
            if (brdv.success == false)
                brdv.ErrorInfo = string.Format("货品【{0}】库存操作记录（{1}）保存失败！", outDtl.GoodsCName, entity.OperTypeName);
            return brdv;
        }
        public BaseResultDataValue AddReaBmsQtyDtlTransferOperation(ReaBmsTransferDoc transferDoc, ReaBmsTransferDtl transferDtl, ReaBmsQtyDtl qtyDtl, double goodsQtyOut, long operTypeID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ReaBmsQtyDtlOperation sOperation = new ReaBmsQtyDtlOperation();
            long? empID = null;
            if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                empID = long.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            string empName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

            sOperation.LabID = transferDoc.LabID;
            sOperation.BDocID = transferDoc.Id;
            sOperation.BDocNo = transferDoc.TransferDocNo;
            sOperation.BDtlID = transferDoc.Id;
            sOperation.QtyDtlID = qtyDtl.Id;
            sOperation.OperTypeID = operTypeID;
            sOperation.OperTypeName = ReaBmsQtyDtlOperationOperType.GetStatusDic()[sOperation.OperTypeID.ToString()].Name;
            sOperation.ReaCompanyID = qtyDtl.ReaCompanyID;
            sOperation.GoodsLotID = qtyDtl.GoodsLotID;
            if (!sOperation.GoodsLotID.HasValue)
                sOperation.GoodsLotID = qtyDtl.GoodsLotID;
            sOperation.LotNo = qtyDtl.LotNo;
            sOperation.BarCodeType = qtyDtl.BarCodeType;
            sOperation.ProdDate = qtyDtl.ProdDate;
            sOperation.InvalidDate = qtyDtl.InvalidDate;
            sOperation.CreaterID = empID;

            sOperation.CreaterName = empName;
            sOperation.GoodsID = qtyDtl.GoodsID;
            sOperation.CompGoodsLinkID = qtyDtl.CompGoodsLinkID;
            sOperation.GoodsName = qtyDtl.GoodsName;
            sOperation.GoodsUnit = qtyDtl.GoodsUnit;
            sOperation.UnitMemo = qtyDtl.UnitMemo;
            if (operTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.移库入库.Key))
            {
                sOperation.GoodsQty = goodsQtyOut;// transferDtl.GoodsQty;
                sOperation.StorageID = transferDtl.DStorageID;
                sOperation.StorageName = transferDtl.DStorageName;
                sOperation.PlaceID = transferDtl.DPlaceID;
                sOperation.PlaceName = transferDtl.DPlaceName;
            }
            else
            {
                sOperation.StorageID = transferDtl.SStorageID;
                sOperation.StorageName = transferDtl.SStorageName;
                sOperation.PlaceID = transferDtl.SPlaceID;
                sOperation.PlaceName = transferDtl.SPlaceName;
                sOperation.GoodsQty = 0 - goodsQtyOut;// transferDtl.GoodsQty;
            }
            sOperation.ChangeCount = System.Math.Abs((double)goodsQtyOut);
            sOperation.Price = qtyDtl.Price;
            sOperation.SumTotal = sOperation.Price * sOperation.GoodsQty;//transferDtl.SumTotal;
            sOperation.TaxRate = qtyDtl.TaxRate;

            sOperation.ReaCompanyID = qtyDtl.ReaCompanyID;
            sOperation.ReaCompCode = qtyDtl.ReaCompCode;
            sOperation.CompanyName = qtyDtl.CompanyName;
            sOperation.ReaServerCompCode = qtyDtl.ReaServerCompCode;

            sOperation.GoodsSerial = transferDtl.GoodsSerial;
            sOperation.LotSerial = transferDtl.LotSerial;
            sOperation.SysLotSerial = transferDtl.SysLotSerial;

            sOperation.ZX1 = transferDtl.ZX1;
            sOperation.ZX2 = transferDtl.ZX2;
            sOperation.ZX3 = transferDtl.ZX3;
            sOperation.Memo = transferDtl.Memo;
            sOperation.Visible = true;

            sOperation.OutFlag = 0;
            sOperation.DataUpdateTime = DateTime.Now;
            sOperation.ReaGoodsNo = transferDtl.ReaGoodsNo;
            sOperation.ProdGoodsNo = transferDtl.ProdGoodsNo;
            sOperation.CenOrgGoodsNo = transferDtl.CenOrgGoodsNo;
            sOperation.GoodsNo = transferDtl.GoodsNo;

            this.Entity = sOperation;
            brdv.success = this.Add();
            if (brdv.success == false)
                brdv.ErrorInfo = string.Format("货品【{0}】库存操作记录（{1}）保存失败！", transferDtl.GoodsCName, sOperation.OperTypeName);
            return brdv;
        }
        public IList<ReaBmsQtyDtlOperation> SearchReaBmsQtyDtlOperationListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsQtyDtlOperation> entityList = new List<ReaBmsQtyDtlOperation>();
            entityList = ((IDReaBmsQtyDtlOperationDao)base.DBDao).SearchReaBmsQtyDtlOperationListByAllJoinHql(where, reaGoodsHql, sort, page, limit);
            return entityList;
        }
        public EntityList<ReaBmsQtyDtlOperation> SearchReaBmsQtyDtlOperationEntityListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaBmsQtyDtlOperation> entityList = new EntityList<ReaBmsQtyDtlOperation>();
            entityList = ((IDReaBmsQtyDtlOperationDao)base.DBDao).SearchReaBmsQtyDtlOperationEntityListByAllJoinHql(where, reaGoodsHql, sort, page, limit);
            return entityList;
        }
    }
}