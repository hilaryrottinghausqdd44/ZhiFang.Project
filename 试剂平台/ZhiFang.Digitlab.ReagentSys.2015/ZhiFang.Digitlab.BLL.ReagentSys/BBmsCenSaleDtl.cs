
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using System.Collections;
using ZhiFang.Digitlab.IBLL.ReagentSys;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BBmsCenSaleDtl : BaseBLL<BmsCenSaleDtl>, ZhiFang.Digitlab.IBLL.ReagentSys.IBBmsCenSaleDtl
    {
        public IDBmsCenSaleDtlConfirmDao IDBmsCenSaleDtlConfirmDao { get; set; }

        #region 供货单多批次验收
        /// <summary>
        /// 获取某一供货单待验收的明细
        /// </summary>
        /// <param name="bmsCenSaleDocId"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<BmsCenSaleDtlOV> SearchListForCheckByBmsCenSaleDocId(long bmsCenSaleDocId, string sort, int page, int count)
        {
            EntityList<BmsCenSaleDtlOV> entityList = new EntityList<BmsCenSaleDtlOV>();
            //entityList.list = new List<BmsCenSaleDtlOV>();
            ////某一供货单的明细
            //EntityList<BmsCenSaleDtl> tempEntityList = new EntityList<BmsCenSaleDtl>();
            //string strHqlWhere = "bmscensaledtl.BmsCenSaleDoc.Id=" + bmsCenSaleDocId;
            //if ((sort != null) && (sort.Length > 0))
            //{
            //    //((IDBmsCenSaleDtlDao)base.DBDao).GetListByHQL
            //    tempEntityList = ((IDBmsCenSaleDtlDao)base.DBDao).GetListByHQL(strHqlWhere, sort, page, count);
            //}
            //else
            //{
            //    tempEntityList = ((IDBmsCenSaleDtlDao)base.DBDao).GetListByHQL(strHqlWhere, page, count);
            //}
            //if (tempEntityList.list == null || tempEntityList.list.Count == 0) return entityList;

            ////已验收的明细
            //IList<BmsCenSaleDtlConfirm> confirmDtList = new List<BmsCenSaleDtlConfirm>();
            //string strHqlWhere2 = "bmscensaledtlconfirm.BmsCenSaleDocConfirm.BmsCenSaleDoc.Id=" + bmsCenSaleDocId;
            //confirmDtList = IDBmsCenSaleDtlConfirmDao.GetListByHQL(strHqlWhere2);

            //entityList.count = tempEntityList.list.Count;
            //for (int i = 0; i < tempEntityList.list.Count; i++)
            //{
            //    string psaleDtlIDStr = GetSplitMixSerial(tempEntityList.list[i], true);
            //    BmsCenSaleDtl tempEntity = tempEntityList.list[i];
            //    BmsCenSaleDtlOV model = GetBmsCenSaleDtlOV(tempEntity);
            //    model.PSaleDtlIDStr = psaleDtlIDStr;

            //    model.ScanCodeMark = 0;
            //    model.AcceptCount = 0;
            //    model.RefuseCount = 0;

            //    //条码类型
            //    int barCodeMgr = tempEntity.BarCodeMgr;
            //    int dtlCount = tempEntity.DtlCount;
            //    //是否存在里供货验收明细里
            //    var tempConfirmDt = new List<BmsCenSaleDtlConfirm>();
            //    if (confirmDtList.Count > 0)
            //        tempConfirmDt = confirmDtList.Where(p => p.BmsCenSaleDtl.Id == tempEntity.Id).ToList();

            //    switch (barCodeMgr)
            //    {
            //        case 1://盒条码试剂
            //            //第三方接口时,dtlCount为0或者没有进行同一种试剂的明细数量计算时,dtlCount=同一种试剂的goodsQty累加
            //            if (dtlCount <= 0) dtlCount = CalcDtlCount(tempEntity, tempEntityList.list);

            //            if (tempConfirmDt != null && tempConfirmDt.Count > 0)
            //            {
            //                model = CalcStockSumTotal(model, tempConfirmDt[0], confirmDtList);

            //                if (tempConfirmDt[0].AcceptCount > 0)
            //                {
            //                    model.ScanCodeMark = 6;
            //                }
            //                else if (tempConfirmDt[0].RefuseCount > 0)
            //                {
            //                    model.ScanCodeMark = 7;
            //                }

            //                if (model.AcceptCounted > 0 && model.AcceptCounted == dtlCount)
            //                {
            //                    model.ScanCodeMark = 3;
            //                }
            //                else if (model.RefuseCounted > 0 && model.RefuseCounted == dtlCount)
            //                {
            //                    model.ScanCodeMark = 4;
            //                }
            //            }
            //            break;
            //        default:
            //            if (tempConfirmDt != null && tempConfirmDt.Count > 0)
            //            {
            //                foreach (var item in tempConfirmDt)
            //                {
            //                    model.AcceptCounted += item.AcceptCount;
            //                    model.RefuseCounted += item.RefuseCount;
            //                }
            //                model.AcceptMemo = tempConfirmDt[0].AcceptMemo;
            //            }
            //            //兼容因为在早期时的数据对DtlCount是没有赋值的(取接口的数据时也是为0),还是取GoodsQty
            //            int goodsQty = tempEntity.GoodsQty;
            //            if (dtlCount <= 0 && goodsQty > 0) dtlCount = goodsQty;
            //            model.StockSumTotal = model.AcceptCounted + model.RefuseCounted;
            //            if (model.AcceptCounted > 0 && model.AcceptCounted == dtlCount)
            //            {
            //                model.ScanCodeMark = 3;
            //            }
            //            else if (model.RefuseCounted > 0 && model.RefuseCounted == dtlCount)
            //            {
            //                model.ScanCodeMark = 4;
            //            }
            //            break;
            //    }
            //    model.DtlCount = dtlCount;

            //    if (model.AcceptCounted > 0 && model.RefuseCounted > 0 && (model.AcceptCounted + model.RefuseCounted) >= dtlCount)
            //    {
            //        model.ScanCodeMark = 5;
            //    }
            //    model.AcceptCount = 0;
            //    model.RefuseCount = 0;
            //    entityList.list.Add(model);
            //}
            return entityList;
        }
        private BmsCenSaleDtlOV GetBmsCenSaleDtlOV(BmsCenSaleDtl tempEntity)
        {
            BmsCenSaleDtlOV model = new BmsCenSaleDtlOV();
            //model.Id = tempEntity.Id;
            //model.PSaleDtlID = tempEntity.PSaleDtlID;
            //model.MixSerial = tempEntity.MixSerial;

            //model.BarCodeMgr = tempEntity.BarCodeMgr;
            //model.GoodsName = tempEntity.GoodsName;
            //model.GoodsUnit = tempEntity.GoodsUnit;
            //model.UnitMemo = tempEntity.UnitMemo;
            //model.LotNo = tempEntity.LotNo;

            //model.DtlCount = tempEntity.DtlCount;
            //model.AcceptCount = tempEntity.AcceptCount;
            //model.GoodsQty = tempEntity.GoodsQty;
            //model.Price = tempEntity.Price;
            //model.SumTotal = tempEntity.SumTotal;

            //if (tempEntity.InvalidDate.HasValue)
            //    model.InvalidDate = tempEntity.InvalidDate.Value.ToString();

            return model;
        }

        /// <summary>
        /// 获取某一供货单的(同一种试剂)合并处理后的明细
        /// </summary>
        /// <param name="bmsCenSaleDocId"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<BmsCenSaleDtl> SearchMergerDtListForCheckByBmsCenSaleDocId(long bmsCenSaleDocId, string sort, int page, int count)
        {
            //某一供货单的明细
            EntityList<BmsCenSaleDtl> entityList = new EntityList<BmsCenSaleDtl>();
            //string strHqlWhere = "bmscensaledtl.BmsCenSaleDoc.Id=" + bmsCenSaleDocId;
            //if ((sort != null) && (sort.Length > 0))
            //{
            //    entityList = ((IDBmsCenSaleDtlDao)base.DBDao).GetListByHQL(strHqlWhere, sort, page, count);
            //}
            //else
            //{
            //    entityList = ((IDBmsCenSaleDtlDao)base.DBDao).GetListByHQL(strHqlWhere, page, count);
            //}
            //if (entityList.list == null || entityList.list.Count == 0) return entityList;

            //entityList.list = MergerDtList(entityList.list);
            //entityList.count = entityList.list.Count;
            return entityList;
        }

        /// <summary>
        /// 对供货明细进行(同一种试剂)合并处理
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private IList<BmsCenSaleDtl> MergerDtList(IList<BmsCenSaleDtl> list)
        {
            IList<BmsCenSaleDtl> mergerList = new List<BmsCenSaleDtl>();
            //Dictionary<string, BmsCenSaleDtl> map = new Dictionary<string, BmsCenSaleDtl>();
            //for (int i = 0; i < list.Count; i++)
            //{
            //    var model = list[i];
            //    string mixSerial = GetSplitMixSerial(model, true);
            //    if (string.IsNullOrEmpty(mixSerial)) continue;

            //    if (!string.IsNullOrEmpty(mixSerial) && !map.ContainsKey(mixSerial))
            //    {
            //        //验收数量的处理
            //        model.AcceptCount = CalcDtlAcceptCount(model, list);
            //        map.Add(mixSerial, model);
            //    }
            //    else
            //    {
            //        int goodsQty = model.GoodsQty;
            //        int dtlCount = model.DtlCount;
            //        //条码类型
            //        int barCodeMgr = model.BarCodeMgr;
            //        switch (barCodeMgr)
            //        {
            //            case 1:
            //                //盒条码:第三方接口时,dtlCount为0或者没有进行同一种试剂的明细数量计算时,dtlCount=同一种试剂的goodsQty累加
            //                if (dtlCount == 0) dtlCount = CalcDtlCount(model, list);
            //                map[mixSerial].DtlCount = dtlCount;
            //                break;
            //            default:
            //                break;
            //        }
            //        //兼容因为在早期时的数据对DtlCount是没有赋值的,dtlCount还是取GoodsQty
            //        if (dtlCount == 0 && goodsQty > 0) dtlCount = goodsQty;
            //        map[mixSerial].GoodsQty = goodsQty + map[mixSerial].GoodsQty;
            //        map[mixSerial].SumTotal = dtlCount * map[mixSerial].Price;
            //    }
            //}
            //foreach (KeyValuePair<string, BmsCenSaleDtl> kvp in map)
            //{
            //    mergerList.Add(kvp.Value);
            //}
            return mergerList;
        }
        /// <summary>
        /// 试剂明细里的同一种试剂的已扫码总数(接收+拒收)
        /// </summary>
        /// <param name="confirmModel"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private BmsCenSaleDtlOV CalcStockSumTotal(BmsCenSaleDtlOV model, BmsCenSaleDtlConfirm confirmModel, IList<BmsCenSaleDtlConfirm> list)
        {
            //string mixSerial = GetSplitMixSerial(confirmModel.BmsCenSaleDtl, true);
            //for (var i = 0; i < list.Count; i++)
            //{
            //    int barCodeMgr = model.BarCodeMgr;
            //    switch (barCodeMgr)
            //    {
            //        case 1: //盒条码
            //            string mixSerial2 = GetSplitMixSerial(list[i].BmsCenSaleDtl, true);
            //            //ZhiFang.Common.Log.Log.Debug("mixSerial:" + mixSerial + ",mixSerial2:" + mixSerial2);
            //            if (mixSerial == mixSerial2)
            //            {
            //                if (list[i].AcceptCount > 0)
            //                {
            //                    model.AcceptCounted = model.AcceptCounted + list[i].AcceptCount;
            //                    model.StockSumTotal = model.StockSumTotal + list[i].AcceptCount;
            //                }
            //                if (list[i].RefuseCount > 0)
            //                {
            //                    model.RefuseCounted = model.RefuseCounted + list[i].RefuseCount;
            //                    model.StockSumTotal = model.StockSumTotal + list[i].RefuseCount;
            //                }
            //            }
            //            break;
            //        default: //批条码及无条码:原始数量*单价格
            //            if (confirmModel.BmsCenSaleDtl.Id == list[i].BmsCenSaleDtl.Id)
            //            {
            //                if (list[i].AcceptCount > 0)
            //                {
            //                    model.AcceptCounted = model.AcceptCounted + list[i].AcceptCount;
            //                    model.StockSumTotal = model.StockSumTotal + list[i].AcceptCount;
            //                }
            //                if (list[i].RefuseCount > 0)
            //                {
            //                    model.RefuseCounted = model.RefuseCounted + list[i].RefuseCount;
            //                    model.StockSumTotal = model.StockSumTotal + list[i].RefuseCount;
            //                }
            //            }
            //            break;
            //    }
            //}
            return model;
        }
        /// <summary>
        /// 明细总金额计算
        /// </summary>
        /// <param name="model"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private double CalcAllSumTotal(IList<BmsCenSaleDtl> list)
        {
            double allSumTotal = 0;
            double price = 0;
            //for (var i = 0; i < list.Count; i++)
            //{
            //    int barCodeMgr = list[i].BarCodeMgr;
            //    //验收总价格计算
            //    switch (barCodeMgr)
            //    {
            //        case 1: //盒条码
            //            if (list[i].Price > 0) price = list[i].Price;
            //            break;
            //        default: //批条码及无条码:原始数量*单价格
            //            int goodsQty = list[i].GoodsQty;
            //            price = goodsQty * list[i].Price;
            //            break;
            //    }
            //    allSumTotal += price;
            //}
            return allSumTotal;
        }
        /// <summary>
        /// 试剂明细里的同一种试剂的验收数量处理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private int CalcDtlAcceptCount(BmsCenSaleDtl model, IList<BmsCenSaleDtl> list)
        {
            int acceptCount = 0;
            ////条码类型
            //int barCodeMgr = model.BarCodeMgr;
            //if (model.AcceptCount > 0) acceptCount = model.AcceptCount;
            //if (barCodeMgr != 1)
            //{
            //    return acceptCount;
            //}
            //else
            //{
            //    acceptCount = 0;
            //    string mixSerial1 = GetSplitMixSerial(model, true);
            //    for (var i = 0; i < list.Count; i++)
            //    {
            //        string mixSerial2 = GetSplitMixSerial(list[i], true);
            //        if (mixSerial1 == mixSerial2 && list[i].AcceptCount > 0) acceptCount += list[i].AcceptCount;
            //    }
            //}

            return acceptCount;
        }
        /// <summary>
        /// 第三方接口时,dtlCount为0或者没有进行同一种试剂的明细数量计算时,dtlCount=同一种试剂的goodsQty累加
        /// </summary>
        /// <param name="model"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private int CalcDtlCount(BmsCenSaleDtl model, IList<BmsCenSaleDtl> list)
        {
            int dtlCount = 0;
            //string mixSerial1 = GetSplitMixSerial(model, true);
            //for (var i = 0; i < list.Count; i++)
            //{
            //    string mixSerial2 = GetSplitMixSerial(list[i], true);
            //    if (mixSerial1 == mixSerial2 && list[i].GoodsQty > 0) dtlCount += list[i].GoodsQty;
            //}
            return dtlCount;
        }
        /// <summary>
        /// 根据条码类型获取判断某一供货单里的明细是否为同一试剂的取值
        /// (1)优先使用,上级ID(指拆分后原明细的主键ID值)
        /// (2)兼容平台旧的数据,平台的盒装混全条码格式:ZFRP|1|1116|078-K138-01|T11131|2017-05-30|110001118|5459663006151236684|1|111
        /// (3)平台的非盒装混合条码格式:5459663006151236684,(验收待定)
        /// (4)第三方平台的混合条码格式:11131|1747070530|20170530|111,分割后取前三段
        /// (5)第三方平台接口:https:// m.roche-diag.cn/barcode?num=07404880340190(3.0)170413120355000546|20984201|20180930,分割后取后两段
        /// (6)第三方平台接口:https://m.roche-diag.cn/barcode?num=07403045838122(4.0)170711210905000302|20990002|20180531||0304583,分割后取后两段
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isgetpid"></param>
        /// <returns></returns>
        private string GetSplitMixSerial(BmsCenSaleDtl model, bool isgetpid)
        {
            //if (model == null) return "";
            string mixSerial = "";
            //string mixSerial = GetMixSerialByBarCodeMgr(model, isgetpid);
            //if (string.IsNullOrEmpty(mixSerial)) return "";
            ////条码类型
            //int barCodeMgr = model.BarCodeMgr;
            //switch (barCodeMgr)
            //{
            //    case 1:
            //        //盒条码试剂的混合条码处理
            //        //处理(5)(6)
            //        if (mixSerial.IndexOf("=") > -1 && mixSerial.Split('=').Length == 2)
            //        {
            //            mixSerial = mixSerial.Split('=')[1];
            //            string[] tempArr = mixSerial.Split('|');
            //            if (tempArr.Length == 0) return mixSerial;
            //            if (tempArr.Length >= 3)
            //            {
            //                mixSerial = "" + tempArr[1] + "|" + tempArr[2];
            //            }
            //        }
            //        else
            //        {
            //            //兼容旧的条码规则
            //            string[] tempArr = mixSerial.Split('|');
            //            if (tempArr.Length == 0) return mixSerial;
            //            if (tempArr.Length == 3)
            //            {
            //                mixSerial = "" + tempArr[1] + "|" + tempArr[2];
            //            }
            //            else if (tempArr.Length == 4)
            //            {
            //                mixSerial = tempArr[3];
            //            }
            //            else if (tempArr.Length > 4)
            //            {
            //                mixSerial = tempArr[7];
            //            }
            //            else
            //            {
            //                mixSerial = tempArr[0];
            //            }
            //        }
            //        break;
            //    default:
            //        break;
            //}
            return mixSerial;
        }
        /// <summary>
        /// 根据条码类型获取判断是否同一试剂的值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isgetpid"></param>
        /// <returns></returns>
        private string GetMixSerialByBarCodeMgr(BmsCenSaleDtl model, bool isgetpid)
        {
            string mixSerial = "";
            //if (isgetpid && isgetpid == true)
            //{
            //    //上级ID(指拆分后原明细的主键ID值)
            //    string psaleDtlID = model.PSaleDtlID.ToString();
            //    if (!string.IsNullOrEmpty(psaleDtlID) && (psaleDtlID != "0")) mixSerial = psaleDtlID;
            //}
            //if (!string.IsNullOrEmpty(mixSerial)) return mixSerial;

            //if (string.IsNullOrEmpty(mixSerial)) mixSerial = model.MixSerial;

            //if (!string.IsNullOrEmpty(mixSerial)) return mixSerial;

            ////盒条码试剂的混合条码处理
            //int barCodeMgr = model.BarCodeMgr;
            //switch (barCodeMgr)
            //{
            //    case 1:
            //        //如果混合条码还为空,厂商产品编号+货品批号
            //        if (!string.IsNullOrEmpty(mixSerial))
            //        {
            //            var ProdGoodsNo = model.ProdGoodsNo;
            //            var LotNo = model.LotNo;
            //            mixSerial = ProdGoodsNo + '+' + LotNo;
            //        }
            //        break;
            //    default:
            //        break;
            //}
            ////mixSerial还为空时取当前明细ID
            //if (string.IsNullOrEmpty(mixSerial)) mixSerial = model.Id.ToString();

            return mixSerial;
        }
        #endregion
    }
}