
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaConfirm;
using ZhiFang.Digitlab.IBLL.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BBmsCenOrderDtl : BaseBLL<BmsCenOrderDtl>, ZhiFang.Digitlab.IBLL.ReagentSys.IBBmsCenOrderDtl
    {
        IDBmsCenSaleDtlConfirmDao IDBmsCenSaleDtlConfirmDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaGoodsBarcodeOperationDao IDReaGoodsBarcodeOperationDao { get; set; }

        #region 客户端订单明细处理
        public BaseResultBool AddDtList(IList<BmsCenOrderDtl> dtAddList, BmsCenOrderDoc reaBmsOrderDoc, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtAddList != null && dtAddList.Count > 0)
            {
                if (reaBmsOrderDoc.DataTimeStamp == null)
                {
                    byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                    reaBmsOrderDoc.DataTimeStamp = dataTimeStamp;
                }
                foreach (var item in dtAddList)
                {
                    item.BmsCenOrderDoc = reaBmsOrderDoc;

                    item.OrderDocNo = reaBmsOrderDoc.OrderDocNo;
                    if (string.IsNullOrEmpty(item.OrderDtlNo))
                        item.OrderDtlNo = this.GetReqDocNo();
                    this.Entity = item;
                    tempBaseResultBool.success = this.Add();
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditDtList(IList<BmsCenOrderDtl> dtEditList, BmsCenOrderDoc reaBmsReqDoc)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtEditList != null && dtEditList.Count > 0)
            {
                List<string> tmpa = new List<string>();
                foreach (var item in dtEditList)
                {
                    tmpa.Clear();

                    tmpa.Add("Id=" + item.Id + " ");
                    tmpa.Add("GoodsQty=" + item.GoodsQty + "");
                    tmpa.Add("Price=" + item.Price + "");
                    //this.Entity = item;
                    tempBaseResultBool.success = this.Update(tmpa.ToArray());
                }
            }
            return tempBaseResultBool;
        }
        public ReaOrderDtlVO GetReaOrderDtlVO(long id)
        {
            ReaOrderDtlVO vo = new ReaOrderDtlVO();
            BmsCenOrderDtl model = this.Get(id);

            BmsCenOrderDoc orderDoc = null;
            IList<BmsCenSaleDtlConfirm> dtlConfirmList = null;
            if (model != null) orderDoc = model.BmsCenOrderDoc;
            if (orderDoc != null) dtlConfirmList = SearchBmsCenSaleDtlConfirmListByOrderId(orderDoc.Id);
            vo = ReaOrderDtlVOCopy<BmsCenOrderDtl, ReaOrderDtlVO>(model);
            changeReaOrderDtlVO(dtlConfirmList, vo);
            ReaGoods reaGoods = IDReaGoodsDao.Get(vo.ReaGoodsID.Value);
            vo.ReaGoods = reaGoods;
            if (reaGoods != null)
                vo.BarCodeMgr = reaGoods.BarCodeMgr;
            return vo;
        }
        /// <summary>
        /// 客户端订单验收,获取某一订单的待验收订单明细VO集合信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<ReaOrderDtlVO> SearchReaOrderDtlVOListByHQL(string strHqlWhere, string order, int page, int limit)
        {
            EntityList<ReaOrderDtlVO> entityVOList = new EntityList<ReaOrderDtlVO>();
            entityVOList.list = new List<ReaOrderDtlVO>();

            if (!string.IsNullOrEmpty(strHqlWhere)) strHqlWhere = strHqlWhere.Replace("reaorderdtlvo", "bmscenorderdtl");
            if (!string.IsNullOrEmpty(order)) order = order.Replace("reaorderdtlvo", "bmscenorderdtl");
            EntityList<BmsCenOrderDtl> el = this.SearchListByHQL(strHqlWhere, order, -1, -1);

            BmsCenOrderDoc orderDoc = null;

            IList<BmsCenSaleDtlConfirm> dtlConfirmList = null;
            if (el.count > 0) orderDoc = el.list[0].BmsCenOrderDoc;
            if (orderDoc != null)
            {
                dtlConfirmList = SearchBmsCenSaleDtlConfirmListByOrderId(orderDoc.Id);
                if (orderDoc.Status == int.Parse(ReaBmsOrderDocStatus.部分验收.Key) || orderDoc.Status == int.Parse(ReaBmsOrderDocStatus.全部验收.Key)) return entityVOList;
            }

            IList<ReaOrderDtlVO> tempList = new List<ReaOrderDtlVO>();

            foreach (var model in el.list)
            {
                ReaOrderDtlVO vo = ReaOrderDtlVOCopy<BmsCenOrderDtl, ReaOrderDtlVO>(model);
                changeReaOrderDtlVO(dtlConfirmList, vo);
                tempList.Add(vo);
            }
            //过滤已验收完的订单明细(可验收数大于0)
            tempList = tempList.Where(p => p.ConfirmCount > 0).ToList();
            entityVOList.count = tempList.Count;
            //分页处理
            if (limit >= 0 && limit < tempList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    tempList = list.ToList();
            }
            //订单货品明细条码类型赋值处理
            for (int i = 0; i < tempList.Count; i++)
            {
                ReaGoods reaGoods = IDReaGoodsDao.Get(tempList[i].ReaGoodsID.Value);
                tempList[i].ReaGoods = reaGoods;
                if (reaGoods != null)
                    tempList[i].BarCodeMgr = reaGoods.BarCodeMgr;
            }
            entityVOList.list = tempList;
            return entityVOList;
        }
        private IList<BmsCenSaleDtlConfirm> SearchBmsCenSaleDtlConfirmListByOrderId(long orderId)
        {
            IList<BmsCenSaleDtlConfirm> tempList = new List<BmsCenSaleDtlConfirm>();
            tempList = IDBmsCenSaleDtlConfirmDao.GetListByHQL(String.Format("bmscensaledtlconfirm.BmsCenOrderDoc.Id={0}", orderId));
            return tempList;
        }
        /// <summary>
        /// 某一订单明细的验收信息处理
        /// </summary>
        /// <param name="dtlConfirmList"></param>
        /// <param name="vo"></param>
        private void changeReaOrderDtlVO(IList<BmsCenSaleDtlConfirm> dtlConfirmList, ReaOrderDtlVO vo)
        {
            if (dtlConfirmList != null)
            {
                //某一订单明细的所有验收明细信息
                var dtlList = dtlConfirmList.Where(p => p.BmsCenOrderDtl.Id == vo.Id).ToList();
                if (dtlList != null && dtlList.Count() > 0)
                {
                    StringBuilder strbIDS = new StringBuilder();
                    foreach (var item in dtlList)
                    {
                        strbIDS.Append(item.Id + ",");
                    }
                    IList<ReaGoodsBarcodeOperation> dtlBarCodeList = IDReaGoodsBarcodeOperationDao.GetListByHQL(string.Format("reagoodsbarcodeoperation.BDtlID in({0}) and (reagoodsbarcodeoperation.OperTypeID={1} or reagoodsbarcodeoperation.OperTypeID={2})", strbIDS.ToString().TrimEnd(','), ReaGoodsBarcodeOperType.验货接收.Key, ReaGoodsBarcodeOperType.验货拒收.Key));
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
                            if (tempDictionary.Keys.Contains(item.Key))
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
                            operationVO.ReceiveFlag = operationVO.OperTypeID.Value;// (operationVO.OperTypeID.Value == long.Parse(ReaGoodsBarcodeOperType.验货接收.Key) ? 2 : 3);
                            operationVO.SysPackSerial = item.Value.SysPackSerial;
                            operationVO.OtherPackSerial = item.Value.OtherPackSerial;
                            operationVO.UsePackSerial = item.Value.UsePackSerial;
                            operationVO.LotNo = item.Value.LotNo;
                            dtVOList.Add(operationVO);
                        }
                    }

                    ServiceCommon.ParseObjectProperty tempParseObjectProperty = new ServiceCommon.ParseObjectProperty();
                    vo.ReaBmsCenSaleDtlConfirmLinkVOListStr = tempParseObjectProperty.GetObjectPropertyNoPlanish(dtVOList);
                    vo.ReceivedCount = dtlList.Where(p => p.Status.Value != int.Parse(BmsCenSaleDocConfirmStatus.待继续验收.Key)).Sum(p => p.AcceptCount);
                    vo.RejectedCount = dtlList.Where(p => p.Status.Value != int.Parse(BmsCenSaleDocConfirmStatus.待继续验收.Key)).Sum(p => p.RefuseCount);
                }
                var goodsQty = vo.ReceivedCount + vo.RejectedCount;
                vo.ConfirmCount = vo.GoodsQty - goodsQty;
                if (vo.ConfirmCount < 0) vo.ConfirmCount = 0;
                if (goodsQty >= vo.GoodsQty) vo.AcceptFlag = true; else vo.AcceptFlag = false;
            }
        }
        /// <summary>
        /// 将父类属性值拷贝给子类
        /// </summary>
        /// <typeparam name="BmsCenOrderDtl"></typeparam>
        /// <typeparam name="ReaOrderDtlVO"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        private ReaOrderDtlVO ReaOrderDtlVOCopy<BmsCenOrderDtl, ReaOrderDtlVO>(BmsCenOrderDtl parent) where ReaOrderDtlVO : BmsCenOrderDtl, new()
        {
            ReaOrderDtlVO vo = new ReaOrderDtlVO();
            var ParentType = typeof(BmsCenOrderDtl);
            var Properties = ParentType.GetProperties();
            foreach (var Propertie in Properties)
            {
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    //进行属性拷贝
                    Propertie.SetValue(vo, Propertie.GetValue(parent, null), null);
                }
            }
            return vo;
        }
        /// <summary>
        /// 获取订单明细单号
        /// </summary>
        /// <returns></returns>
        private string GetReqDocNo()
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