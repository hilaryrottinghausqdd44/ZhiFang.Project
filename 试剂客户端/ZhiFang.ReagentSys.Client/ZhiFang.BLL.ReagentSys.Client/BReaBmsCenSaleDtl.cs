using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using System.Collections.Generic;
using System.Linq;
using System;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client.ViewObject;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaSale;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    /// 供货明细的条码类型从机构货品关系表里取
    /// </summary>
    public class BReaBmsCenSaleDtl : BaseBLL<ReaBmsCenSaleDtl>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsCenSaleDtl
    {
        //IDBDictDao IDBDictDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IDReaBmsCenSaleDtlConfirmDao IDReaBmsCenSaleDtlConfirmDao { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        IDReaBmsCenSaleDocDao IDReaBmsCenSaleDocDao { get; set; }
        IBReaGoodsLot IBReaGoodsLot { get; set; }
        IBReaBmsSerial IBReaBmsSerial { get; set; }
        IDCenOrgDao IDCenOrgDao { get; set; }
        IDReaBmsCenOrderDocDao IDReaBmsCenOrderDocDao { get; set; }
        IDReaBmsCenOrderDtlDao IDReaBmsCenOrderDtlDao { get; set; }

        #region 保存验证处理
        public BaseResultBool EditSaleDtlListOfVOValid(IList<ReaBmsCenSaleDtlVO> dtlVOSaveList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            foreach (var vo in dtlVOSaveList)
            {
                var model = vo.ReaBmsCenSaleDtl;
                if (string.IsNullOrEmpty(model.ReaGoodsName))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("供货明细为【{0}】的货品名称为空，不能保存", model.Id);
                    break;
                }
                if (string.IsNullOrEmpty(model.ReaGoodsNo))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的产品编码为空，不能保存", model.ReaGoodsName);
                    break;
                }
                if (model.GoodsQty <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的供货数为零，不能保存", model.ReaGoodsName);
                    break;
                }
                if (string.IsNullOrEmpty(model.LotNo))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的批号信息为空，不能保存", model.ReaGoodsName);
                    break;
                }
                //if (!model.ProdDate.HasValue)
                //{
                //    tempBaseResultBool.success = false;
                //    tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的生产日期信息为空，不能保存", model.ReaGoodsName);
                //    break;
                //}
                if (!model.InvalidDate.HasValue)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的有效期信息为空，不能保存", model.ReaGoodsName);
                    break;
                }
                if (tempBaseResultBool.success == false)
                {
                    ZhiFang.Common.Log.Log.Warn(tempBaseResultBool.ErrorInfo);
                    break;
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditSaleDtlListOfValid(IList<ReaBmsCenSaleDtl> dtlSaveList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            foreach (var model in dtlSaveList)
            {
                if (string.IsNullOrEmpty(model.ReaGoodsName))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("供货明细为【{0}】的货品名称为空，不能保存", model.Id);
                    break;
                }
                if (string.IsNullOrEmpty(model.ReaGoodsNo))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的产品编码为空，不能保存", model.ReaGoodsName);
                    break;
                }
                if (model.GoodsQty <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的供货数为零，不能保存", model.ReaGoodsName);
                    break;
                }
                if (string.IsNullOrEmpty(model.LotNo))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的批号信息为空，不能保存", model.ReaGoodsName);
                    break;
                }
                //if (!model.ProdDate.HasValue)
                //{
                //    tempBaseResultBool.success = false;
                //    tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的生产日期信息为空，不能保存", model.ReaGoodsName);
                //    break;
                //}
                if (!model.InvalidDate.HasValue)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的有效期信息为空，不能保存", model.ReaGoodsName);
                    break;
                }
                if (tempBaseResultBool.success == false)
                {
                    ZhiFang.Common.Log.Log.Warn(tempBaseResultBool.ErrorInfo);
                    break;
                }
            }
            return tempBaseResultBool;
        }
        #endregion

        #region 新增/修改
        /// <summary>
        /// 针对订单转供单的供单，一订单对应多供单，在保存供单信息之前，进行校验供货数量
        /// 验证成功会修改订单的状态、已供数量、未供数量
        /// 验证失败不可以进行后续操作，前台直接提示错误信息
        /// </summary>
        /// <param name="saleDoc"></param>
        /// <param name="dtlAddList"></param>
        /// <param name="dtlEditList"></param>
        public BaseResultBool CompareAndGetOrderDtlQty(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> dtlAddList, IList<ReaBmsCenSaleDtl> dtlEditList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrWhiteSpace(saleDoc.OrderDocNo))
            {
                //非订单转供单，不做处理，直接返回成功
                return tempBaseResultBool;
            }
            if (saleDoc.Status >= int.Parse(ReaBmsCenSaleDocAndDtlStatus.审核通过.Key))
            {
                //供单的状态，是审核通过及其以后的操作，不做处理，直接返回成功
                return tempBaseResultBool;
            }

            string errorInfo = "";
            try
            {
                //获取库里当前订单对应的所有的供单（2确认提交、4审核通过、6供货提取、7部分验收、8全部验收）
                string where = string.Format("Status in (2,4,6,7,8) and OrderDocNo='{0}'", saleDoc.OrderDocNo);
                if (saleDoc.Status.ToString() == ReaBmsCenSaleDocAndDtlStatus.取消提交.Key)
                {
                    //如果是取消提交，由于当前供单的状态还没有改，所以获取时，要去除当前供单
                    where += string.Format(" and Id<>{0}", saleDoc.Id);
                }
                IList<ReaBmsCenSaleDoc> tempSaleDocList = IDReaBmsCenSaleDocDao.GetListByHQL(where);
                //根据供单Id，查询供单明细列表
                string saleDocIDs = string.Join(",", tempSaleDocList.Select(p => p.Id).Distinct().ToArray());
                IList<ReaBmsCenSaleDtl> tempSaleDtlList = new List<ReaBmsCenSaleDtl>();
                if (saleDocIDs.Trim() != "")
                {
                    tempSaleDtlList = this.SearchListByHQL(string.Format("SaleDocID in ({0})", saleDocIDs));
                    //根据货品编码分组，供货数量求和
                    tempSaleDtlList = tempSaleDtlList.GroupBy(p => new { p.ReaGoodsID }).Select(g => new ReaBmsCenSaleDtl { ReaGoodsID = g.Key.ReaGoodsID, GoodsQty = g.Sum(k => k.GoodsQty) }).ToList();
                }

                //当前新增/修改的供单货品和数量，将dtlAddList和dtlEditList整合在一起
                List<ReaBmsCenSaleDtl> temptempSaleDtlListCurrent = new List<ReaBmsCenSaleDtl>();
                if (saleDoc.Status.ToString() == ReaBmsCenSaleDocAndDtlStatus.确认提交.Key)
                {
                    if (dtlAddList != null)
                    {
                        temptempSaleDtlListCurrent.AddRange(dtlAddList);
                    }
                    if (dtlEditList != null)
                    {
                        temptempSaleDtlListCurrent.AddRange(dtlEditList);
                    }
                    temptempSaleDtlListCurrent = temptempSaleDtlListCurrent.GroupBy(p => new { p.ReaGoodsID }).Select(g => new ReaBmsCenSaleDtl { ReaGoodsID = g.Key.ReaGoodsID, GoodsQty = g.Sum(k => k.GoodsQty) }).ToList();
                }

                int supplyStatus = 0;//供货状态
                long orderDocID = 0;//订单表主键ID
                int suppliedQtyCount = 0;//已供数量=0的计数

                //获取订单里的货品列表
                IList<ReaBmsCenOrderDtl> tempOrderDtlList = IDReaBmsCenOrderDtlDao.GetListByHQL(string.Format("OrderDocNo='{0}'", saleDoc.OrderDocNo));
                foreach (var orderDtl in tempOrderDtlList)
                {
                    orderDocID = orderDtl.OrderDocID;
                    double GoodsQty = orderDtl.GoodsQty.Value;//订货数量
                    double SuppliedQty = 0;//已供数量
                    double UnSuppliedQty = 0;//未供数量

                    var l1 = tempSaleDtlList.Where(p => p.ReaGoodsID == orderDtl.ReaGoodsID).ToList();
                    var l2 = temptempSaleDtlListCurrent.Where(p => p.ReaGoodsID == orderDtl.ReaGoodsID).ToList();
                    SuppliedQty = (l1.Count() > 0 ? l1[0].GoodsQty.Value : 0) + (l2.Count() > 0 ? l2[0].GoodsQty.Value : 0);//已供数量=库里的已供数量+当前提交的已供数量
                    if (SuppliedQty > GoodsQty)
                    {
                        //已供数量>订货数量，中断操作，给出提示
                        errorInfo = "货品：(" + orderDtl.ReaGoodsNo + ")" + orderDtl.ReaGoodsName + "的供货数量已超过订货数量，不能保存！";
                        break;
                    }

                    //未供数量=订货数量-已供数量
                    UnSuppliedQty = GoodsQty - SuppliedQty;
                    if (UnSuppliedQty > 0)
                    {
                        //存在未供数量>0的货品，则整单的供货状态=部分供货
                        supplyStatus = int.Parse(ReaBmsOrderDocSupplyStatus.部分供货.Key);
                    }

                    //已供数量=0，则计数，如果已供数量=0的计数和订单的货品数量一致，则供货状态=未供货
                    if (SuppliedQty == 0)
                    {
                        suppliedQtyCount++;
                    }

                    //实体赋值
                    orderDtl.SuppliedQty = SuppliedQty;
                    orderDtl.UnSupplyQty = UnSuppliedQty;
                }

                if (errorInfo == "")
                {
                    if (supplyStatus == 0)
                    {
                        supplyStatus = int.Parse(ReaBmsOrderDocSupplyStatus.全部供货.Key);
                    }

                    if (suppliedQtyCount == tempOrderDtlList.Count())
                    {
                        supplyStatus = int.Parse(ReaBmsOrderDocSupplyStatus.未供货.Key);
                    }

                    //更新订单表状态、订单明细数量信息
                    IDReaBmsCenOrderDocDao.UpdateByHql(string.Format("update ReaBmsCenOrderDoc t set t.SupplyStatus={1},t.DataUpdateTime='{2}' where t.Id={0}", orderDocID, supplyStatus, DateTime.Now));
                    foreach (var orderDtl in tempOrderDtlList)
                    {
                        IDReaBmsCenOrderDtlDao.UpdateByHql(string.Format("update ReaBmsCenOrderDtl t set t.SuppliedQty={1},t.UnSupplyQty={2},t.DataUpdateTime='{3}' where t.Id={0}", orderDtl.Id, orderDtl.SuppliedQty, orderDtl.UnSupplyQty, DateTime.Now));
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = errorInfo;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("CompareAndGetOrderDtlQty操作异常->", ex);
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = ex.Message;
            }

            return tempBaseResultBool;
        }
        public BaseResultBool AddSaleDtlList(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> dtlAddList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtlAddList != null && dtlAddList.Count > 0)
            {
                int dispOrder = 1;
                foreach (var entity in dtlAddList)
                {
                    if (tempBaseResultBool.success == false) break;
                    try
                    {
                        entity.LabID = saleDoc.LabID;
                        entity.SaleDocID = saleDoc.Id;
                        entity.Status = saleDoc.Status;
                        entity.StatusName = saleDoc.StatusName;
                        entity.SaleDocNo = saleDoc.SaleDocNo;
                        if (string.IsNullOrEmpty(entity.SaleDtlNo))
                            entity.SaleDtlNo = this.GetSaleDtlNo();
                        entity.DataUpdateTime = DateTime.Now;
                        entity.DispOrder = dispOrder;
                        this.Entity = entity;
                        ReaGoodsLot reaGoodsLot = null;
                        AddReaGoodsLot(ref reaGoodsLot, empID, empName);
                        if (reaGoodsLot != null)
                            this.Entity.GoodsLotID = reaGoodsLot.Id;
                        tempBaseResultBool.success = this.Add();
                        dispOrder++;
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultBool.ErrorInfo = "新增供货明细失败!";
                        //ZhiFang.Common.Log.Log.Error("新增供货明细失败,错误信息:" + ex.Message);
                        tempBaseResultBool.success = false;
                        throw ex;
                    }
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool UpdateSaleDtlList(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> dtlEditList, string fields, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtlEditList != null && dtlEditList.Count > 0)
            {
                if (string.IsNullOrEmpty(fields))
                    fields = "Id,Status,StatusName,DataUpdateTime,ProdID,ProdOrgName,ProdGoodsNo,GoodsUnit,UnitMemo,StorageType,TempRange,GoodsQty,Price,SumTotal,TaxRate,LotNo,ProdDate,InvalidDate,BiddingNo,ApproveDocNo,RegisterNo,RegisterInvalidDate";//,Memo,GoodsLotID
                foreach (var entity in dtlEditList)
                {
                    if (tempBaseResultBool.success == false) break;

                    try
                    {
                        entity.SaleDocID = saleDoc.Id;
                        entity.Status = saleDoc.Status;
                        entity.StatusName = saleDoc.StatusName;
                        entity.DataUpdateTime = DateTime.Now;
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                        this.Entity = entity;
                        ReaGoodsLot reaGoodsLot = null;
                        AddReaGoodsLot(ref reaGoodsLot, empID, empName);
                        if (reaGoodsLot != null)
                            this.Entity.GoodsLotID = reaGoodsLot.Id;
                        tempBaseResultBool.success = this.Update(tempArray);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultBool.ErrorInfo = "保存供货明细失败!";
                        tempBaseResultBool.success = false;
                    }
                }
            }
            return tempBaseResultBool;
        }
        private void AddReaGoodsLot(ref ReaGoodsLot reaGoodsLot, long empID, string empName)
        {
            if (this.Entity == null) return;
            if (string.IsNullOrEmpty(this.Entity.LotNo)) return;
            if (this.Entity.ReaGoodsID == null) return;
            if (!this.Entity.InvalidDate.HasValue) return;

            ReaGoodsLot lot = new ReaGoodsLot();
            lot.LabID = this.Entity.LabID;
            lot.Visible = true;
            lot.GoodsID = this.Entity.ReaGoodsID.Value;
            lot.ReaGoodsNo = this.Entity.ReaGoodsNo;
            lot.LotNo = this.Entity.LotNo;
            lot.ProdDate = this.Entity.ProdDate;
            lot.GoodsCName = this.Entity.ReaGoodsName;
            lot.InvalidDate = this.Entity.InvalidDate;
            lot.CreaterID = empID;
            lot.CreaterName = empName;
            IBReaGoodsLot.Entity = lot;
            BaseResultBool baseResultBool = IBReaGoodsLot.AddAndValid(ref reaGoodsLot);
        }
        #endregion

        #region 供货验收
        public BaseResultBool AddDtlListOfVO(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtlVO> dtlVOAddList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtlVOAddList != null && dtlVOAddList.Count > 0)
            {
                if (saleDoc.DataTimeStamp == null)
                {
                    byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                    saleDoc.DataTimeStamp = dataTimeStamp;
                }
                foreach (var vo in dtlVOAddList)
                {
                    var model = vo.ReaBmsCenSaleDtl;
                    model.LabID = saleDoc.LabID;
                    model.SaleDocID = saleDoc.Id;
                    model.Status = saleDoc.Status;
                    model.StatusName = saleDoc.StatusName;
                    model.SaleDocNo = saleDoc.SaleDocNo;
                    if (string.IsNullOrEmpty(model.SaleDtlNo))
                        model.SaleDtlNo = this.GetSaleDtlNo();
                    model.DataUpdateTime = DateTime.Now;
                    this.Entity = model;
                    tempBaseResultBool.success = this.Add();
                    //供货条码明细操作记录添加
                    if (vo.BarcodeOperationList != null)
                        tempBaseResultBool = AddBarcodeOperationOfList(saleDoc, model, vo.BarcodeOperationList, empID, empName);
                    if (tempBaseResultBool.success == false) break;
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool AddBarcodeOperationOfList(ReaBmsCenSaleDoc saleDoc, ReaBmsCenSaleDtl model, IList<ReaGoodsBarcodeOperation> dtAddList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtAddList == null || dtAddList.Count <= 0) return tempBaseResultBool;

            foreach (ReaGoodsBarcodeOperation operation in dtAddList)
            {
                operation.LabID = model.LabID;
                operation.BDocID = model.SaleDocID;
                operation.BDocNo = model.SaleDocNo;
                operation.BDtlID = model.Id;
                operation.ReaCompanyID = model.ReaCompID;

                operation.CompanyName = model.ReaCompanyName;
                operation.GoodsLotID = model.GoodsLotID;
                operation.LotNo = model.LotNo;
                operation.GoodsID = model.ReaGoodsID;
                operation.ScanCodeGoodsID = model.GoodsID;
                operation.GoodsCName = model.ReaGoodsName;
                operation.GoodsUnit = model.GoodsUnit;

                operation.ReaGoodsNo = model.ReaGoodsNo;
                operation.ProdGoodsNo = model.ProdGoodsNo;
                operation.CenOrgGoodsNo = model.CenOrgGoodsNo;
                operation.GoodsNo = model.GoodsNo;
                operation.GoodsSort = model.GoodsSort;
                operation.ReaCompCode = saleDoc.ReaCompCode;

                operation.CompGoodsLinkID = model.CompGoodsLinkID;
                operation.BarCodeType = (int)model.BarCodeType;
                operation.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
                operation.OperTypeID = long.Parse(ReaGoodsBarcodeOperType.供货.Key);
                operation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[operation.OperTypeID.ToString()].Name;
            }
            tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperationOfList(dtAddList, 0, empID, empName, model.LabID);
            return tempBaseResultBool;
        }
        private string GetSaleDtlNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        public EntityList<ReaSaleDtlOfConfirmVO> SearchReaBmsCenSaleDtlOfConfirmVOListBySaleDocID(string strHqlWhere, string order, int page, int limit, long saleDocID)
        {
            EntityList<ReaSaleDtlOfConfirmVO> entityVOList = new EntityList<ReaSaleDtlOfConfirmVO>();
            entityVOList.list = new List<ReaSaleDtlOfConfirmVO>();
            if (string.IsNullOrEmpty(strHqlWhere))
                strHqlWhere = string.Format("reabmscensaledtl.SaleDocID={0}", saleDocID);
            else
                strHqlWhere = string.Format("reabmscensaledtl.SaleDocID={0} and {1}", saleDocID, strHqlWhere);
            EntityList<ReaBmsCenSaleDtl> el = this.SearchListByHQL(strHqlWhere, order, -1, -1);

            ReaBmsCenSaleDoc saleDoc = null;
            if (el.count > 0) saleDoc = IDReaBmsCenSaleDocDao.Get(el.list[0].SaleDocID.Value);
            if (saleDoc != null && saleDoc.Status == int.Parse(ReaBmsCenSaleDocAndDtlStatus.全部验收.Key))
                return entityVOList;

            IList<ReaSaleDtlOfConfirmVO> tempList = new List<ReaSaleDtlOfConfirmVO>();
            IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList = new List<ReaBmsCenSaleDtlConfirm>();
            foreach (var model in el.list)
            {
                ReaSaleDtlOfConfirmVO vo = ClassMapperHelp.GetMapper<ReaSaleDtlOfConfirmVO, ReaBmsCenSaleDtl>(model); ;
                dtlConfirmList = IDReaBmsCenSaleDtlConfirmDao.GetListByHQL(String.Format("reabmscensaledtlconfirm.SaleDtlID={0}", model.Id));
                GetReaBmsCenSaleDtlLinkVOListStr(vo);
                if (model.GoodsQty.HasValue)
                    vo.DtlGoodsQty = model.GoodsQty.Value;
                GetReaBmsCenSaleDtlConfirmLinkVOListStr(dtlConfirmList, vo);
                vo.OrderDocNo = saleDoc.OrderDocNo;//订货单号
                tempList.Add(vo);
            }
            //过滤可验收的供货明细(可验收数大于0)
            tempList = tempList.Where(p => p.ConfirmCount > 0).ToList();
            entityVOList.count = tempList.Count;
            //分页处理
            if (limit > 0 && limit < tempList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    tempList = list.ToList();
            }
            //供货货品明细条码类型赋值处理
            for (int i = 0; i < tempList.Count; i++)
            {
                ReaGoodsOrgLink reaGoodsOrgLink = IDReaGoodsOrgLinkDao.Get(tempList[i].CompGoodsLinkID.Value);
                if (reaGoodsOrgLink != null)
                {
                    tempList[i].ReaGoods = reaGoodsOrgLink.ReaGoods;
                    tempList[i].BarCodeType = reaGoodsOrgLink.BarCodeType;
                }
            }
            entityVOList.list = tempList;
            return entityVOList;
        }
        private void GetReaBmsCenSaleDtlLinkVOListStr(ReaSaleDtlOfConfirmVO vo)
        {
            IList<ReaGoodsBarcodeOperation> dtlBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(string.Format("reagoodsbarcodeoperation.BDtlID in({0}) and (reagoodsbarcodeoperation.OperTypeID={1})", vo.Id, ReaGoodsBarcodeOperType.供货.Key));
            IList<ReaGoodsBarcodeOperationVO> dtVOList = GetReaGoodsBarcodeOperationVO(dtlBarCodeList);
            ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
            vo.ReaBmsCenSaleDtlLinkVOListStr = tempParseObjectProperty.GetObjectPropertyNoPlanish(dtVOList);
        }
        /// <summary>
        /// 某一供货明细的验收扫码操作记录
        /// </summary>
        /// <param name="dtlConfirmList"></param>
        /// <param name="vo"></param>
        private void GetReaBmsCenSaleDtlConfirmLinkVOListStr(IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList, ReaSaleDtlOfConfirmVO vo)
        {
            if (dtlConfirmList != null)
            {
                var dtlList = dtlConfirmList.Where(p => p.SaleDtlID.Value == vo.Id).ToList();
                if (dtlList != null && dtlList.Count() > 0)
                {
                    StringBuilder strbIDS = new StringBuilder();
                    foreach (var item in dtlList)
                    {
                        strbIDS.Append(item.Id + ",");
                    }
                    IList<ReaGoodsBarcodeOperation> dtlBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(string.Format("reagoodsbarcodeoperation.BDtlID in({0}) and (reagoodsbarcodeoperation.OperTypeID={1} or reagoodsbarcodeoperation.OperTypeID={2})", strbIDS.ToString().TrimEnd(','), ReaGoodsBarcodeOperType.验货接收.Key, ReaGoodsBarcodeOperType.验货拒收.Key));

                    IList<ReaGoodsBarcodeOperationVO> dtVOList = GetReaGoodsBarcodeOperationVO(dtlBarCodeList);

                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                    vo.ReaBmsCenSaleDtlConfirmLinkVOListStr = tempParseObjectProperty.GetObjectPropertyNoPlanish(dtVOList);
                    vo.ReceivedCount = dtlList.Where(p => p.Status.Value != int.Parse(ReaBmsCenSaleDocConfirmStatus.待继续验收.Key)).Sum(p => p.AcceptCount);
                    vo.RejectedCount = dtlList.Where(p => p.Status.Value != int.Parse(ReaBmsCenSaleDocConfirmStatus.待继续验收.Key)).Sum(p => p.RefuseCount);
                }
                var goodsQty = vo.ReceivedCount + vo.RejectedCount;
                vo.ConfirmCount = vo.GoodsQty.Value - goodsQty;
                if (vo.ConfirmCount < 0) vo.ConfirmCount = 0;
                if (goodsQty >= vo.GoodsQty) vo.AcceptFlag = true; else vo.AcceptFlag = false;
            }
        }
        private IList<ReaGoodsBarcodeOperationVO> GetReaGoodsBarcodeOperationVO(IList<ReaGoodsBarcodeOperation> dtlBarCodeList)
        {
            IList<ReaGoodsBarcodeOperationVO> dtVOList = new List<ReaGoodsBarcodeOperationVO>();
            //获取每一盒条码(供货验收或供货拒收)的最后操作记录
            Dictionary<string, ReaGoodsBarcodeOperation> tempDictionary = new Dictionary<string, ReaGoodsBarcodeOperation>();
            //按验收明细ID进行分组
            var dtlIDGroupBy = dtlBarCodeList.GroupBy(p => p.BDtlID);
            foreach (var model in dtlIDGroupBy)
            {
                //某一验收明细的所有验收盒条码信息按使用盒条码进行分组
                var serialGroupBy = dtlBarCodeList.Where(p => p.BDtlID.Value == model.Key.Value).GroupBy(p => p.UsePackSerial);
                tempDictionary.Clear();
                foreach (var item in serialGroupBy)
                {
                    var tempList = item.OrderByDescending(p => p.DataAddTime).ToList();
                    if (!tempDictionary.Keys.Contains(item.Key))
                        tempDictionary.Add(item.Key, tempList[0]);
                }
                foreach (var item in tempDictionary)
                {
                    var operation = item.Value;
                    ReaGoodsBarcodeOperationVO operationVO = new ReaGoodsBarcodeOperationVO();
                    operationVO.Id = item.Value.Id;
                    operationVO.BDocNo = item.Value.BDocNo;
                    operationVO.BDocID = item.Value.BDocID;
                    operationVO.BDtlID = item.Value.BDtlID;
                    operationVO.OperTypeID = item.Value.OperTypeID;

                    //签收标志：2接收、3拒收
                    operationVO.ReceiveFlag = operationVO.OperTypeID.Value;
                    operationVO.SysPackSerial = item.Value.SysPackSerial;
                    operationVO.OtherPackSerial = item.Value.OtherPackSerial;
                    operationVO.UsePackSerial = item.Value.UsePackSerial;
                    operationVO.UsePackQRCode = item.Value.UsePackQRCode;

                    operationVO.LotNo = item.Value.LotNo;
                    operationVO.ReaGoodsNo = item.Value.ReaGoodsNo;
                    operationVO.ProdGoodsNo = item.Value.ProdGoodsNo;
                    operationVO.CenOrgGoodsNo = item.Value.CenOrgGoodsNo;
                    operationVO.GoodsNo = item.Value.GoodsNo;

                    operationVO.ReaCompCode = item.Value.ReaCompCode;
                    operationVO.GoodsSort = item.Value.GoodsSort;
                    operationVO.CompGoodsLinkID = item.Value.CompGoodsLinkID;
                    operationVO.BarCodeType = item.Value.BarCodeType;

                    dtVOList.Add(operationVO);
                }
            }
            return dtVOList;
        }
        #endregion

        #region 生成供货条码信息
        public BaseResultBool AddCreateBarcodeInfoOfSaleDocId(long saleDocId, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            ReaBmsCenSaleDoc saleDoc = IDReaBmsCenSaleDocDao.Get(saleDocId);
            if (saleDoc == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "供货单ID：" + saleDoc.Id + ",不存在！";
                return tempBaseResultBool;
            }
            if (saleDoc.Status.ToString() != ReaBmsCenSaleDocAndDtlStatus.审核通过.Key)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "供货单ID：" + saleDoc.Id + "的状态为：" + ReaBmsCenSaleDocAndDtlStatus.GetStatusDic()[saleDoc.Status.ToString()].Name + ",不能生成供货条码！";
                return tempBaseResultBool;
            }
            IList<ReaBmsCenSaleDtl> dtlList = this.SearchListByHQL("reabmscensaledtl.SaleDocID=" + saleDoc.Id);
            if (dtlList == null || dtlList.Count < 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "供货单ID：" + saleDoc.Id + ",供货明细信息为空！";
                return tempBaseResultBool;
            }
            var batchDtlList = dtlList.Where(p => p.BarCodeType == int.Parse(ReaGoodsBarCodeType.批条码.Key));
            var boxDtlList = dtlList.Where(p => p.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key));

            //条码是否已生成和打印过
            if (batchDtlList != null && batchDtlList.Count() > 0)
            {
                tempBaseResultBool = AddReaGoodsBarcodeOperationOfBatchDtlList(saleDoc, batchDtlList.ToList(), empID, empName);
            }
            if (tempBaseResultBool.success == true && boxDtlList != null && boxDtlList.Count() > 0)
            {
                tempBaseResultBool = AddReaGoodsBarcodeOperationOfBoxDtlList(saleDoc, boxDtlList.ToList(), empID, empName);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 生成批条码信息
        /// </summary>
        /// <param name="dtlList"></param>
        /// <returns></returns>
        private BaseResultBool AddReaGoodsBarcodeOperationOfBatchDtlList(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> dtlList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //当前机构所属机构码
            IList<CenOrg> cenOrgList = IDCenOrgDao.GetListByHQL("cenorg.LabID=" + saleDoc.LabID.ToString());
            long maxBarCode = -1;
            foreach (var saleDtl in dtlList)
            {
                if (tempBaseResultBool.success == false) break;
                //判断条件(条码类型,是否打印条码
                if (saleDtl.IsPrintBarCode == 1)
                {
                    string lotQRCode = AddGetSysPackSerial(saleDoc, saleDtl);
                    if (string.IsNullOrEmpty(lotQRCode))
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "生成货品名称为:【" + saleDtl.ReaGoodsName + "】的系统内部批条码错误!";
                        break;
                    }
                    else
                    {
                        //生成二维批条码
                        lotQRCode = lotQRCode + "|1|" + saleDtl.GoodsQty;
                        if (!string.IsNullOrEmpty(saleDtl.GoodsUnit))
                            lotQRCode = lotQRCode + "|" + saleDtl.GoodsUnit;
                        saleDtl.LotQRCode = lotQRCode;
                        saleDtl.SysLotSerial = lotQRCode;

                        //生成一维批条码
                        saleDtl.LotSerial = IBReaBmsSerial.GetNextBarCode(saleDtl.LabID, ReaBmsSerialType.供货条码.Key, cenOrgList[0], ref maxBarCode);
                        if (string.IsNullOrEmpty(saleDtl.LotSerial))
                        {
                            throw new Exception(" Error:获取生成一维批条码值(LotSerial)为空,请查看系统日志!");
                        }

                        this.Entity = saleDtl;
                        tempBaseResultBool.success = this.Edit();
                        if (tempBaseResultBool.success == false)
                            tempBaseResultBool.ErrorInfo = "生成货品名称为:【" + saleDtl.ReaGoodsName + "】的系统内部盒条码错误!";
                        //maxBarCode = maxBarCode + 1;
                    }
                }
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 生成盒条码信息
        /// </summary>
        /// <param name="dtlList"></param>
        /// <returns></returns>
        private BaseResultBool AddReaGoodsBarcodeOperationOfBoxDtlList(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> dtlList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            IList<ReaGoodsBarcodeOperation> barcodeList = new List<ReaGoodsBarcodeOperation>();
            foreach (var saleDtl in dtlList)
            {
                if (tempBaseResultBool.success == false) break;

                if (saleDtl.IsPrintBarCode == 1)
                {
                    //是否需要删除原来生成的条码信息?
                    int deleteCount = IBReaGoodsBarcodeOperation.DeleteByHql(string.Format(" From ReaGoodsBarcodeOperation reagoodsbarcodeoperation where  reagoodsbarcodeoperation.BDtlID={0} and reagoodsbarcodeoperation.BarcodeCreatType={1} and reagoodsbarcodeoperation.OperTypeID={2}", saleDtl.Id, long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key), long.Parse(ReaGoodsBarcodeOperType.供货.Key)));
                    if (deleteCount > 0)
                        ZhiFang.Common.Log.Log.Info("删除供货明细ID为:" + saleDtl.Id + ",原已生成的条码信息记录数为:" + deleteCount);

                    tempBaseResultBool = EditReaGoodsBarcodeOperation(saleDoc, saleDtl, ref barcodeList, empID, empName);
                }
            }
            if (tempBaseResultBool.success == true && barcodeList.Count > 0)
                tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperationOfList(barcodeList, 0, empID, empName, saleDoc.LabID);

            return tempBaseResultBool;
        }
        private BaseResultBool EditReaGoodsBarcodeOperation(ReaBmsCenSaleDoc saleDoc, ReaBmsCenSaleDtl saleDtl, ref IList<ReaGoodsBarcodeOperation> barcodeList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            if (!saleDtl.GoodsQty.HasValue)
            {
                tempBaseResultBool.success = true;
                return tempBaseResultBool;
            }
            //小数点往上取整
            double counts = Math.Ceiling(saleDtl.GoodsQty.Value);
            //当前机构所属机构码
            IList<CenOrg> cenOrgList = IDCenOrgDao.GetListByHQL("cenorg.LabID=" + saleDoc.LabID.ToString());
            long nextBarCode = -1;
            for (int i = 0; i < counts; i++)
            {
                //生成二维盒条码
                string usePackQRCode = AddGetSysPackSerial(saleDoc, saleDtl);
                if (string.IsNullOrEmpty(usePackQRCode))
                {
                    barcodeList.Clear();
                    string msg = "生成货品名称为:【" + saleDtl.ReaGoodsName + "】的系统内部盒条码错误!";
                    tempBaseResultBool.ErrorInfo = msg;
                    tempBaseResultBool.success = false;
                    ZhiFang.Common.Log.Log.Error(msg);
                    break;
                }
                usePackQRCode = usePackQRCode + "|" + (i + 1) + "|" + saleDtl.GoodsQty;
                if (!string.IsNullOrEmpty(saleDtl.GoodsUnit))
                    usePackQRCode = usePackQRCode + "|" + saleDtl.GoodsUnit;

                //生成一维盒条码(货品供应商生成的条码都在货品平台上生成,LabID以智方机构为主:LabID=1)
                string usePackSerial = "";
                usePackSerial = IBReaBmsSerial.GetNextBarCode(saleDtl.LabID, ReaBmsSerialType.供货条码.Key, cenOrgList[0], ref nextBarCode);
                if (string.IsNullOrEmpty(usePackSerial))
                {
                    throw new Exception(" Error:获取生成一维盒条码值(UsePackSerial)为空,请查看系统日志!");
                }

                ReaGoodsBarcodeOperation barcode = new ReaGoodsBarcodeOperation();

                barcode.LabID = saleDtl.LabID;
                barcode.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key);
                barcode.BDocID = saleDtl.SaleDocID;
                barcode.BDocNo = saleDtl.SaleDocNo;
                barcode.BDtlID = saleDtl.Id;
                barcode.OperTypeID = long.Parse(ReaGoodsBarcodeOperType.供货.Key);
                barcode.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[barcode.OperTypeID.ToString()].Name;

                barcode.DispOrder = i + 1;
                barcode.Visible = true;
                barcode.CreaterID = empID;
                barcode.CreaterName = empName;
                barcode.DataUpdateTime = DateTime.Now;

                barcode.GoodsID = saleDtl.ReaGoodsID;
                barcode.ScanCodeGoodsID = saleDtl.ReaGoodsID;
                barcode.GoodsCName = saleDtl.ReaGoodsName;
                barcode.GoodsUnit = saleDtl.GoodsUnit;
                barcode.UnitMemo = saleDtl.UnitMemo;
                barcode.GoodsLotID = saleDtl.GoodsLotID;
                barcode.LotNo = saleDtl.LotNo;
                barcode.GoodsQty = saleDtl.GoodsQty;
                if (!barcode.MinBarCodeQty.HasValue)
                {
                    //需要考虑供应商与实验室在同一数据库,机构货品(ReaGoodsID)为实验室所有
                    ReaGoods reaGoods = null;
                    if (saleDtl.ReaGoodsID.HasValue)
                        reaGoods = IDReaGoodsDao.Get(saleDtl.ReaGoodsID.Value, false);
                    double gonvertQty = 1;
                    if (reaGoods != null) gonvertQty = reaGoods.GonvertQty;
                    if (gonvertQty <= 0)
                    {
                        ZhiFang.Common.Log.Log.Warn("货品编码为:" + barcode.ReaGoodsNo + ",货品名称为:" + barcode.GoodsCName + ",货品包装单位的换算系数值为" + gonvertQty + ",维护不合理!");
                        gonvertQty = 1;
                    }
                }
                if (barcode.MinBarCodeQty <= 0) barcode.MinBarCodeQty = 1;
                barcode.OverageQty = barcode.MinBarCodeQty;
                barcode.ReaCompanyID = saleDtl.ReaCompID;
                barcode.CompanyName = saleDtl.ReaCompanyName;
                barcode.SysPackSerial = usePackQRCode;
                barcode.UsePackQRCode = usePackQRCode;
                barcode.UsePackSerial = usePackSerial;
                barcode.PUsePackSerial = barcode.Id.ToString();
                barcode.ReaGoodsNo = saleDtl.ReaGoodsNo;
                barcode.ProdGoodsNo = saleDtl.ProdGoodsNo;
                barcode.CenOrgGoodsNo = saleDtl.CenOrgGoodsNo;
                barcode.GoodsNo = saleDtl.GoodsNo;
                barcode.ReaCompCode = saleDoc.ReaCompCode;
                barcode.GoodsSort = saleDtl.GoodsSort;
                barcode.CompGoodsLinkID = saleDtl.CompGoodsLinkID;
                barcode.BarCodeType = (int)saleDtl.BarCodeType;
                barcodeList.Add(barcode);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 生成单个系统内部盒条码
        /// </summary>
        /// <param name="saleDtl"></param>
        /// <returns></returns>
        private string AddGetSysPackSerial(ReaBmsCenSaleDoc saleDoc, ReaBmsCenSaleDtl saleDtl)
        {
            string mixSerial = "";
            string prodGoodsNo = "";
            string lotNo = "";
            string invalidDate = "";
            string dtlId = "";
            string prodOrgNo = ""; //厂商机构码(货品信息的厂商名称)
            string compOrgNo = "";//供应商机构码
            //需要考虑供应商与实验室在同一数据库,机构货品(ReaGoodsID)为实验室所有
            ReaGoods reaGoods = null;
            if (saleDtl.ReaGoodsID.HasValue)
                reaGoods = IDReaGoodsDao.Get(saleDtl.ReaGoodsID.Value, false);
            try
            {
                if (!string.IsNullOrEmpty(saleDtl.CenOrgGoodsNo))
                    prodGoodsNo = saleDtl.CenOrgGoodsNo;
                if (string.IsNullOrEmpty(prodGoodsNo))
                    prodGoodsNo = saleDtl.ProdGoodsNo;
                if (string.IsNullOrEmpty(prodGoodsNo))
                    prodGoodsNo = saleDtl.ProdGoodsNo;
                if (string.IsNullOrEmpty(prodGoodsNo) && reaGoods != null)
                    prodGoodsNo = reaGoods.ProdGoodsNo;
                if (string.IsNullOrEmpty(prodGoodsNo) && reaGoods != null)
                    prodGoodsNo = reaGoods.ReaGoodsNo;

                if (!string.IsNullOrEmpty(saleDtl.LotNo))
                    lotNo = saleDtl.LotNo;
                if (saleDtl.InvalidDate != null)
                    invalidDate = ((DateTime)saleDtl.InvalidDate).ToString("yyyy-MM-dd");
                dtlId = saleDtl.Id.ToString();
                //货品厂商编码
                if (!string.IsNullOrEmpty(saleDtl.ProdOrgNo))
                    prodOrgNo = saleDtl.ProdOrgNo;
                //供应商所属机构平台编码
                if (!string.IsNullOrEmpty(saleDoc.ReaServerCompCode))
                    compOrgNo = saleDoc.ReaServerCompCode;
                mixSerial = "ZFRP|1|" + prodOrgNo + "|" + prodGoodsNo + "|" +
                  lotNo + "|" + invalidDate + "|" + compOrgNo + "|" + dtlId;
            }
            catch (Exception ex)
            {
                string hint = "";
                if (string.IsNullOrEmpty(saleDtl.ProdGoodsNo))
                    hint = "供货明细信息：厂商产品编号为空！";
                if (string.IsNullOrEmpty(saleDtl.LotNo))
                    hint = "供货明细信息：产品批号为空！";
                if (saleDtl.InvalidDate == null)
                    hint = "供货明细信息：产品有效期为空！";
                else if (string.IsNullOrEmpty(saleDoc.ReaServerCompCode))
                    hint = "供货明细信息：产品的供应商所属机构平台编码为空！";
                throw new Exception(hint + " Error:" + ex.Message);
            }
            return mixSerial;
        }

        #endregion

        #region 统计报表
        public IList<ReaBmsCenSaleDtl> SearchDtlAdnDocListByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit)
        {
            return ((IDReaBmsCenSaleDtlDao)base.DBDao).SearchDtlAdnDocListByHQL(docHql, dtlHql, reaGoodsHql, sort, page, limit);
        }
        public EntityList<ReaBmsCenSaleDtl> SearchNewEntityListByHQL(string where, string sort, int page, int limit)
        {
            return ((IDReaBmsCenSaleDtlDao)base.DBDao).SearchNewEntityListByHQL(where, sort, page, limit);
        }
        public EntityList<ReaBmsCenSaleDtl> SearchReaBmsCenSaleDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit)
        {
            EntityList<ReaBmsCenSaleDtl> entityList = new EntityList<ReaBmsCenSaleDtl>();
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                return entityList;

            IList<ReaBmsCenSaleDtl> dtlList = new List<ReaBmsCenSaleDtl>();
            dtlList = ((IDReaBmsCenSaleDtlDao)base.DBDao).SearchReaBmsCenSaleDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, order, -1, -1);
            switch (groupType)
            {
                case 1:
                    dtlList = GetReaBmsCenSaleDtlListOfGroupBy1(dtlList);
                    break;
                case 2:
                    dtlList = GetReaBmsCenSaleDtlListOfGroupBy2(dtlList);
                    break;
                default:
                    break;
            }
            entityList.count = dtlList.Count;
            //分页处理
            if (limit > 0 && limit < dtlList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = dtlList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    dtlList = list.ToList();
            }
            entityList.list = dtlList;
            return entityList;
        }
        /// <summary>
        /// 合并条件:货品产品编码+包装单位+规格
        /// </summary>
        /// <param name="dtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsCenSaleDtl> GetReaBmsCenSaleDtlListOfGroupBy1(IList<ReaBmsCenSaleDtl> dtlList)
        {
            return dtlList.GroupBy(p => new
            {
                p.ReaGoodsNo,
                p.GoodsUnit,
                p.UnitMemo
            }).Select(g => new ReaBmsCenSaleDtl
            {
                Id = g.ElementAt(0).Id,
                LabID = g.ElementAt(0).LabID,
                ReaCompID = g.ElementAt(0).ReaCompID,
                ReaCompanyName = g.ElementAt(0).ReaCompanyName,
                ReaGoodsNo = g.Key.ReaGoodsNo,

                ReaGoodsName = g.ElementAt(0).ReaGoodsName,
                GoodsName = g.ElementAt(0).GoodsName,
                ReaGoodsID = g.ElementAt(0).ReaGoodsID,
                GoodsUnit = g.ElementAt(0).GoodsUnit,
                UnitMemo = g.ElementAt(0).UnitMemo,

                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty),//g.ElementAt(0).Price,
                ProdID = g.ElementAt(0).ProdID,
                ProdOrgName = g.ElementAt(0).ProdOrgName,
                BarCodeType = g.ElementAt(0).BarCodeType,
                DataAddTime = g.ElementAt(0).DataAddTime
            }).ToList();
        }
        /// <summary>
        /// 合并条件:供货单号+供应商ID+货品产品编码+批号+包装单位+规格
        /// </summary>
        /// <param name="dtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsCenSaleDtl> GetReaBmsCenSaleDtlListOfGroupBy2(IList<ReaBmsCenSaleDtl> dtlList)
        {
            return dtlList.GroupBy(p => new
            {
                p.SaleDocNo,
                p.ReaCompID,
                p.ReaGoodsNo,
                p.LotNo,
                p.GoodsUnit,
                p.UnitMemo
            }).Select(g => new ReaBmsCenSaleDtl
            {
                Id = g.ElementAt(0).Id,
                LabID = g.ElementAt(0).LabID,
                ReaCompID = g.ElementAt(0).ReaCompID,
                ReaCompanyName = g.ElementAt(0).ReaCompanyName,
                ReaGoodsNo = g.Key.ReaGoodsNo,

                ReaGoodsName = g.ElementAt(0).ReaGoodsName,
                GoodsName = g.ElementAt(0).GoodsName,
                ReaGoodsID = g.ElementAt(0).ReaGoodsID,
                GoodsUnit = g.ElementAt(0).GoodsUnit,
                UnitMemo = g.ElementAt(0).UnitMemo,
                LotNo = g.ElementAt(0).LotNo,
                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty),//g.ElementAt(0).Price,
                ProdID = g.ElementAt(0).ProdID,
                ProdOrgName = g.ElementAt(0).ProdOrgName,
                BarCodeType = g.ElementAt(0).BarCodeType,
                DataAddTime = g.ElementAt(0).DataAddTime,
                ReaBmsCenSaleDoc = g.ElementAt(0).ReaBmsCenSaleDoc
            }).ToList();
        }

        #endregion

        #region 客户端与平台不在同一数据库--客户端部分
        public BaseResultDataValue AddDtlListOfPlatformExtract(ref IList<ReaBmsCenSaleDtl> saleDtlList, long empID, string empName)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            for (int i = 0; i < saleDtlList.Count; i++)
            {
                ReaBmsCenSaleDtl entity = saleDtlList[i];
                if (baseresultdata.success == false) break;
                try
                {
                    this.Entity = entity;
                    ReaGoodsLot reaGoodsLot = null;
                    AddReaGoodsLot(ref reaGoodsLot, empID, empName);
                    if (reaGoodsLot != null)
                        this.Entity.GoodsLotID = reaGoodsLot.Id;
                    saleDtlList[i] = this.Entity;
                    baseresultdata.success = this.Add();

                }
                catch (Exception ex)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = string.Format("新增供货单号为:{0},供货明细Id为:{1},货品名称为:{2}的供货明细信息失败", entity.SaleDocNo, entity.Id, entity.ReaGoodsName);
                    ZhiFang.Common.Log.Log.Error("新增供货明细失败,错误信息:" + baseresultdata.ErrorInfo + ex.Message);
                }
            }
            return baseresultdata;
        }
        #endregion
    }
}