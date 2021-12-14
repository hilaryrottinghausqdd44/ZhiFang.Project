using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client
{
    public class BComprehensiveStatistics : BaseBLL<ReaGoodsOfMaxGonvertQtyVO>, IBComprehensiveStatistics
    {
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaBmsCenOrderDtlDao IDReaBmsCenOrderDtlDao { get; set; }
        IDReaBmsCenSaleDtlConfirmDao IDReaBmsCenSaleDtlConfirmDao { get; set; }
        IDReaBmsInDtlDao IDReaBmsInDtlDao { get; set; }
        IDReaBmsQtyDtlDao IDReaBmsQtyDtlDao { get; set; }
        IDReaBmsTransferDtlDao IDReaBmsTransferDtlDao { get; set; }
        IDReaBmsOutDtlDao IDReaBmsOutDtlDao { get; set; }

        #region 按机构货品的最大包装单位货品耗材信息进行综合统计报表
        public IList<ReaGoodsOfMaxGonvertQtyVO> SearchComprehensiveStatistics1OfMaxGonvertQtyListHQL(int groupType, string companyId, string deptId, string testEquipId, string reaGoodsNo, string startDate, string endDate, string sort, int page, int limit)
        {
            IList<ReaGoodsOfMaxGonvertQtyVO> entityList = new List<ReaGoodsOfMaxGonvertQtyVO>();

            if (string.IsNullOrEmpty(companyId) && string.IsNullOrEmpty(deptId) && string.IsNullOrEmpty(testEquipId) && string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                return entityList;
            }
            string goodsHql = "reagoods.Visible=1";
            if (!string.IsNullOrEmpty(reaGoodsNo))
                goodsHql = goodsHql + " and reagoods.ReaGoodsNo='" + reaGoodsNo + "'";
            IList<ReaGoods> allReaGoodsList = IDReaGoodsDao.GetListByHQL(goodsHql);
            //按机构货品的相同产品编码分组及排序
            var groupByReaGoodsNo = allReaGoodsList.GroupBy(p => p.ReaGoodsNo).OrderBy(p => p.Key);
            //机构货品订货信息
            IList<ReaBmsCenOrderDtl> orderDtlAllList = GetReaBmsCenOrderDtlList(companyId, deptId, reaGoodsNo, startDate, endDate);
            //机构货品验收信息
            IList<ReaBmsCenSaleDtlConfirm> dtlConfirmAllList = GeReaBmsCenSaleDtlConfirmList(companyId, reaGoodsNo, startDate, endDate);
            IList<ReaBmsInDtl> inDtlAllList = GeReaBmsInDtlList(companyId, reaGoodsNo, startDate, endDate);
            //机构货品当前库存信息
            IList<ReaBmsQtyDtl> qtyAllList = GeReaBmsQtyDtlList(companyId, reaGoodsNo);
            //机构货品领用信息
            IList<ReaBmsTransferDtl> transferDtlAllList = GetReaBmsTransferDtlList(companyId, deptId, reaGoodsNo, startDate, endDate);
            //机构货品使用出库上机信息
            IList<ReaBmsOutDtl> outDtlAllList = GetReaBmsOutDtlList(companyId, deptId, testEquipId, reaGoodsNo, startDate, endDate);

            foreach (var groupBy in groupByReaGoodsNo)
            {
                //获取相同产品编码的最大包装单位的机构货品信息
                ReaGoods maxGonvertQtyReaGoods = groupBy.OrderByDescending(p => p.GonvertQty).ElementAt(0);
                ReaGoodsOfMaxGonvertQtyVO vo = GetComprehensiveStatisticsVO(maxGonvertQtyReaGoods);
                //机构货品订货数统计处理
                GetOrderCount(groupBy, maxGonvertQtyReaGoods, orderDtlAllList, ref vo);
                GetAcceptCount(groupBy, maxGonvertQtyReaGoods, dtlConfirmAllList, ref vo);
                GetRefuseCount(groupBy, maxGonvertQtyReaGoods, dtlConfirmAllList, ref vo);
                //机构货品验收总数(接收数+拒收数)
                GetConfirmCount(groupBy, maxGonvertQtyReaGoods, dtlConfirmAllList, ref vo);
                //当前订货未到货数量(订货总数-(订单)已验收总数)
                if (vo.OrderCount.HasValue && vo.ConfirmCount.HasValue)
                    vo.UndeliveredCount = vo.OrderCount - vo.ConfirmCount;
                if (vo.UndeliveredCount.HasValue && vo.UndeliveredCount.Value < 0)
                    vo.UndeliveredCount = 0;
                //机构货品入库数
                GetInCount(groupBy, maxGonvertQtyReaGoods, inDtlAllList, ref vo);
                //当前库存量数统计处理
                GetCurQtyCount(groupBy, maxGonvertQtyReaGoods, qtyAllList, ref vo);
                //移库领用数
                GetTransferCount(groupBy, maxGonvertQtyReaGoods, transferDtlAllList, ref vo);
                //出库(上机)使用数量
                GetTestEquipOutCount(groupBy, maxGonvertQtyReaGoods, outDtlAllList, ref vo);
                entityList.Add(vo);
            }
            return entityList;
        }
        public EntityList<ReaGoodsOfMaxGonvertQtyVO> SearchReaGoodsStatisticsOfMaxGonvertQtyEntityListHQL(int groupType, string companyId, string deptId, string testEquipId, string reaGoodsNo, string startDate, string endDate, string sort, int page, int limit)
        {
            EntityList<ReaGoodsOfMaxGonvertQtyVO> entityList = new EntityList<ReaGoodsOfMaxGonvertQtyVO>();
            entityList.list = new List<ReaGoodsOfMaxGonvertQtyVO>();

            if (string.IsNullOrEmpty(companyId) && string.IsNullOrEmpty(deptId) && string.IsNullOrEmpty(testEquipId) && string.IsNullOrEmpty(reaGoodsNo) && string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                return entityList;
            }

            IList<ReaGoodsOfMaxGonvertQtyVO> vOList = SearchComprehensiveStatistics1OfMaxGonvertQtyListHQL(groupType, companyId, deptId, testEquipId, reaGoodsNo, startDate, endDate, sort, -1, -1);

            entityList.count = vOList.Count;
            //分页处理
            if (limit > 0 && limit < vOList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = vOList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    vOList = list.ToList();
            }
            entityList.list = vOList;
            return entityList;
        }
        private ReaGoodsOfMaxGonvertQtyVO GetComprehensiveStatisticsVO(ReaGoods reaGoods)
        {
            ReaGoodsOfMaxGonvertQtyVO vo = new ReaGoodsOfMaxGonvertQtyVO();
            vo.ReaGoodsNo = reaGoods.ReaGoodsNo;
            vo.GoodsCName = reaGoods.CName;
            vo.GoodsUnit = reaGoods.UnitName;
            vo.UnitMemo = reaGoods.UnitMemo;
            vo.ProdOrgName = reaGoods.ProdOrgName;

            vo.GoodsClass = reaGoods.GoodsClass;
            vo.GoodsClassType = reaGoods.GoodsClassType;
            vo.DeptName = reaGoods.DeptName;
            vo.SuitableType = reaGoods.SuitableType;
            vo.ReaCompanyName = reaGoods.ReaCompanyName;

            vo.Price = reaGoods.Price;
            vo.TestCount = reaGoods.TestCount;
            vo.MonthlyUsage = reaGoods.MonthlyUsage;
            vo.StoreLower = reaGoods.StoreLower;
            vo.StoreUpper = reaGoods.StoreUpper;

            return vo;
        }
        private IList<ReaBmsCenOrderDtl> GetReaBmsCenOrderDtlList(string companyId, string deptId, string reaGoodsNo, string startDate, string endDate)
        {
            IList<ReaBmsCenOrderDtl> dtlList = new List<ReaBmsCenOrderDtl>();
            StringBuilder docHql = new StringBuilder();
            StringBuilder dtlHql = new StringBuilder();
            //订单状态不等于暂存,申请,审核退回
            docHql.Append("reabmscenorderdoc.Status not in(0,1,2)");
            docHql.Append(" and ");

            long tempDeptId = -1;
            if (!string.IsNullOrEmpty(deptId) && long.TryParse(deptId, out tempDeptId))
            {
                docHql.Append("reabmscenorderdoc.DeptID=" + tempDeptId);
                docHql.Append(" and ");
            }
            DateTime startDate1;
            if (!string.IsNullOrEmpty(startDate) && DateTime.TryParse(startDate, out startDate1))
            {
                docHql.Append("reabmscenorderdoc.DataAddTime>='" + startDate1.ToString("yyyy-MM-dd 00:00:00") + "'");
                docHql.Append(" and ");
            }
            DateTime endDate1;
            if (!string.IsNullOrEmpty(endDate) && DateTime.TryParse(endDate, out endDate1))
            {
                docHql.Append("reabmscenorderdoc.DataAddTime<='" + endDate1.ToString("yyyy-MM-dd 23:59:59") + "'");
                docHql.Append(" and ");
            }

            long tempCompanyId = -1;
            if (!string.IsNullOrEmpty(companyId) && long.TryParse(companyId, out tempCompanyId))
            {
                dtlHql.Append("reabmscenorderdoc.ReaCompID=" + tempCompanyId);
                dtlHql.Append(" and ");
            }
            if (!string.IsNullOrEmpty(reaGoodsNo))
            {
                dtlHql.Append("reabmscenorderdtl.ReaGoodsNo='" + reaGoodsNo + "'");
                dtlHql.Append(" and ");
            }
            if (docHql.Length <= 0 && dtlHql.Length <= 0)
                return dtlList;

            //去除最后的 and 
            if (docHql.Length > 0)
            {
                docHql = docHql.Remove(docHql.Length - 5, 5);
            }
            if (dtlHql.Length > 0)
                dtlHql = dtlHql.Remove(dtlHql.Length - 5, 5);

            dtlList = IDReaBmsCenOrderDtlDao.SearchReaBmsCenOrderDtlSummaryByHQL(docHql.ToString(), dtlHql.ToString(), "", "", -1, -1);
            return dtlList;
        }
        private IList<ReaBmsCenSaleDtlConfirm> GeReaBmsCenSaleDtlConfirmList(string companyId, string reaGoodsNo, string startDate, string endDate)
        {
            IList<ReaBmsCenSaleDtlConfirm> dtlList = new List<ReaBmsCenSaleDtlConfirm>();
            StringBuilder docHql = new StringBuilder();
            StringBuilder dtlHql = new StringBuilder();

            DateTime startDate1;
            if (!string.IsNullOrEmpty(startDate) && DateTime.TryParse(startDate, out startDate1))
            {
                docHql.Append("reabmscensaledocconfirm.DataAddTime>='" + startDate1.ToString("yyyy-MM-dd 00:00:00") + "'");
                docHql.Append(" and ");
            }
            DateTime endDate1;
            if (!string.IsNullOrEmpty(endDate) && DateTime.TryParse(endDate, out endDate1))
            {
                docHql.Append("reabmscensaledocconfirm.DataAddTime<='" + endDate1.ToString("yyyy-MM-dd 23:59:59") + "'");
                docHql.Append(" and ");
            }

            long tempCompanyId = -1;
            if (!string.IsNullOrEmpty(companyId) && long.TryParse(companyId, out tempCompanyId))
            {
                dtlHql.Append("reabmscensaledocconfirm.ReaCompID=" + tempCompanyId);
                dtlHql.Append(" and ");
            }
            if (!string.IsNullOrEmpty(reaGoodsNo))
            {
                dtlHql.Append("reabmscensaledtlconfirm.ReaGoodsNo='" + reaGoodsNo + "'");
                dtlHql.Append(" and ");
            }
            if (docHql.Length <= 0 && dtlHql.Length <= 0)
                return dtlList;

            //去除最后的 and 
            if (docHql.Length > 0)
            {
                docHql = docHql.Remove(docHql.Length - 5, 5);
            }
            if (dtlHql.Length > 0)
                dtlHql = dtlHql.Remove(dtlHql.Length - 5, 5);

            dtlList = IDReaBmsCenSaleDtlConfirmDao.SearchReaBmsCenSaleDtlConfirmSummaryByHQL(docHql.ToString(), dtlHql.ToString(), "", -1, -1);
            return dtlList;
        }
        private IList<ReaBmsInDtl> GeReaBmsInDtlList(string companyId, string reaGoodsNo, string startDate, string endDate)
        {
            IList<ReaBmsInDtl> dtlList = new List<ReaBmsInDtl>();
            StringBuilder docHql = new StringBuilder();
            StringBuilder dtlHql = new StringBuilder();

            DateTime startDate1;
            if (!string.IsNullOrEmpty(startDate) && DateTime.TryParse(startDate, out startDate1))
            {
                docHql.Append("reabmsindoc.DataAddTime>='" + startDate1.ToString("yyyy-MM-dd 00:00:00") + "'");
                docHql.Append(" and ");
            }
            DateTime endDate1;
            if (!string.IsNullOrEmpty(endDate) && DateTime.TryParse(endDate, out endDate1))
            {
                docHql.Append("reabmsindoc.DataAddTime<='" + endDate1.ToString("yyyy-MM-dd 23:59:59") + "'");
                docHql.Append(" and ");
            }

            long tempCompanyId = -1;
            if (!string.IsNullOrEmpty(companyId) && long.TryParse(companyId, out tempCompanyId))
            {
                dtlHql.Append("reabmsindtl.ReaCompanyID=" + tempCompanyId);
                dtlHql.Append(" and ");
            }
            if (!string.IsNullOrEmpty(reaGoodsNo))
            {
                dtlHql.Append("reabmsindtl.ReaGoodsNo='" + reaGoodsNo + "'");
                dtlHql.Append(" and ");
            }
            if (docHql.Length <= 0 && dtlHql.Length <= 0)
                return dtlList;

            //去除最后的 and 
            if (docHql.Length > 0)
            {
                docHql = docHql.Remove(docHql.Length - 5, 5);
            }
            if (dtlHql.Length > 0)
                dtlHql = dtlHql.Remove(dtlHql.Length - 5, 5);

            dtlList = IDReaBmsInDtlDao.SearchReaBmsInDtlSummaryByHQL(docHql.ToString(), dtlHql.ToString(), "", "", -1, -1);
            return dtlList;
        }
        private IList<ReaBmsQtyDtl> GeReaBmsQtyDtlList(string companyId, string reaGoodsNo)
        {
            IList<ReaBmsQtyDtl> dtlList = new List<ReaBmsQtyDtl>();
            StringBuilder docHql = new StringBuilder();
            //StringBuilder dtlHql = new StringBuilder();

            docHql.Append("reabmsqtydtl.GoodsQty>0");
            docHql.Append(" and ");

            long tempCompanyId = -1;
            if (!string.IsNullOrEmpty(companyId) && long.TryParse(companyId, out tempCompanyId))
            {
                docHql.Append("reabmsqtydtl.ReaCompanyID=" + tempCompanyId);
                docHql.Append(" and ");
            }
            if (!string.IsNullOrEmpty(reaGoodsNo))
            {
                docHql.Append("reabmsqtydtl.ReaGoodsNo='" + reaGoodsNo + "'");
                docHql.Append(" and ");
            }
            if (docHql.Length <= 0 && docHql.Length <= 0)
                return dtlList;

            //去除最后的 and 
            if (docHql.Length > 0)
            {
                docHql = docHql.Remove(docHql.Length - 5, 5);
            }
            dtlList = IDReaBmsQtyDtlDao.GetListByHQL(docHql.ToString());
            return dtlList;
        }
        private IList<ReaBmsTransferDtl> GetReaBmsTransferDtlList(string companyId, string deptId, string reaGoodsNo, string startDate, string endDate)
        {
            IList<ReaBmsTransferDtl> dtlList = new List<ReaBmsTransferDtl>();
            StringBuilder docHql = new StringBuilder();
            StringBuilder dtlHql = new StringBuilder();

            long tempDeptId = -1;
            if (!string.IsNullOrEmpty(deptId) && long.TryParse(deptId, out tempDeptId))
            {
                docHql.Append("reabmstransferdoc.DeptID=" + tempDeptId);
                docHql.Append(" and ");
            }
            DateTime startDate1;
            if (!string.IsNullOrEmpty(startDate) && DateTime.TryParse(startDate, out startDate1))
            {
                docHql.Append("reabmstransferdoc.DataAddTime>='" + startDate1.ToString("yyyy-MM-dd 00:00:00") + "'");
                docHql.Append(" and ");
            }
            DateTime endDate1;
            if (!string.IsNullOrEmpty(endDate) && DateTime.TryParse(endDate, out endDate1))
            {
                docHql.Append("reabmstransferdoc.DataAddTime<='" + endDate1.ToString("yyyy-MM-dd 23:59:59") + "'");
                docHql.Append(" and ");
            }

            long tempCompanyId = -1;
            if (!string.IsNullOrEmpty(companyId) && long.TryParse(companyId, out tempCompanyId))
            {
                dtlHql.Append("reabmstransferdtl.ReaCompanyID=" + tempCompanyId);
                dtlHql.Append(" and ");
            }
            if (!string.IsNullOrEmpty(reaGoodsNo))
            {
                dtlHql.Append("reabmstransferdtl.ReaGoodsNo='" + reaGoodsNo + "'");
                dtlHql.Append(" and ");
            }
            if (docHql.Length <= 0 && dtlHql.Length <= 0)
                return dtlList;

            //去除最后的 and 
            if (docHql.Length > 0)
                docHql = docHql.Remove(docHql.Length - 5, 5);
            if (dtlHql.Length > 0)
                dtlHql = dtlHql.Remove(dtlHql.Length - 5, 5);
            dtlList = IDReaBmsTransferDtlDao.SearchReaBmsTransferDtlSummaryByHQL(docHql.ToString(), dtlHql.ToString(), "", "", -1, -1);
            return dtlList;
        }
        private IList<ReaBmsOutDtl> GetReaBmsOutDtlList(string companyId, string deptId, string testEquipId, string reaGoodsNo, string startDate, string endDate)
        {
            IList<ReaBmsOutDtl> dtlList = new List<ReaBmsOutDtl>();
            StringBuilder docHql = new StringBuilder();
            StringBuilder dtlHql = new StringBuilder();

            //出库类型为"使用出库"
            docHql.Append("reabmsoutdoc.OutType=" + ReaBmsOutDocOutType.使用出库.Key);
            docHql.Append(" and ");

            long tempDeptId = -1;
            if (!string.IsNullOrEmpty(deptId) && long.TryParse(deptId, out tempDeptId))
            {
                docHql.Append("reabmsoutdoc.DeptID=" + tempDeptId);
                docHql.Append(" and ");
            }
            DateTime startDate1;
            if (!string.IsNullOrEmpty(startDate) && DateTime.TryParse(startDate, out startDate1))
            {
                docHql.Append("reabmsoutdoc.DataAddTime>='" + startDate1.ToString("yyyy-MM-dd 00:00:00") + "'");
                docHql.Append(" and ");
            }
            DateTime endDate1;
            if (!string.IsNullOrEmpty(endDate) && DateTime.TryParse(endDate, out endDate1))
            {
                docHql.Append("reabmsoutdoc.DataAddTime<='" + endDate1.ToString("yyyy-MM-dd 23:59:59") + "'");
                docHql.Append(" and ");
            }

            long tempCompanyId = -1;
            if (!string.IsNullOrEmpty(companyId) && long.TryParse(companyId, out tempCompanyId))
            {
                dtlHql.Append("reabmsoutdtl.ReaCompanyID=" + tempCompanyId);
                dtlHql.Append(" and ");
            }
            long tempTestEquipId = -1;
            if (!string.IsNullOrEmpty(testEquipId) && long.TryParse(testEquipId, out tempTestEquipId))
            {
                dtlHql.Append("reabmsoutdtl.TestEquipID=" + tempTestEquipId);
                dtlHql.Append(" and ");
            }
            if (!string.IsNullOrEmpty(reaGoodsNo))
            {
                dtlHql.Append("reabmsoutdtl.ReaGoodsNo='" + reaGoodsNo + "'");
                dtlHql.Append(" and ");
            }
            if (docHql.Length <= 0 && dtlHql.Length <= 0)
                return dtlList;

            //去除最后的 and 
            if (docHql.Length > 0)
                docHql = docHql.Remove(docHql.Length - 5, 5);
            if (dtlHql.Length > 0)
                dtlHql = dtlHql.Remove(dtlHql.Length - 5, 5);
            dtlList = IDReaBmsOutDtlDao.SearchReaBmsOutDtlSummaryByHQL(docHql.ToString(), dtlHql.ToString(), "", "", -1, -1);
            return dtlList;
        }
        private void GetOrderCount(IGrouping<string, ReaGoods> groupBy, ReaGoods maxGonvertQtyReaGoods, IList<ReaBmsCenOrderDtl> dtlAllList, ref ReaGoodsOfMaxGonvertQtyVO vo)
        {
            if (dtlAllList == null || dtlAllList.Count < 0) return;

            var dtlCurList = dtlAllList.Where(p => p.ReaGoodsNo == maxGonvertQtyReaGoods.ReaGoodsNo).ToList();
            if (dtlCurList == null || dtlCurList.Count < 0) return;

            //当前机构货品为最小包装单位货品时的订货数量处理
            if (maxGonvertQtyReaGoods.GonvertQty == 1)
            {
                vo.OrderCount = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.GoodsQty.HasValue == true && p.GoodsQty.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.GoodsQty);
                return;
            }
            var minReaGoods = groupBy.Where(p => p.GonvertQty == 1).OrderBy(p => p.GonvertQty);
            //当前机构货品不存在最小包装单位或转换系数小于等于0时的订货数量处理
            if (minReaGoods == null || minReaGoods.Count() <= 0 || maxGonvertQtyReaGoods.GonvertQty <= 0)
            {
                vo.OrderCount = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.GoodsQty.HasValue == true && p.GoodsQty.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.GoodsQty);
                return;
            }
            //当前机构货品有最小包装单位时,订货数处理
            if (maxGonvertQtyReaGoods != minReaGoods.ElementAt(0) && maxGonvertQtyReaGoods.GonvertQty > 1)
            {
                double minTotalGoodsQty = 0;
                var tempReaGoodsList = groupBy.OrderBy(p => p.GonvertQty).ToList();
                //先将各订货货品转换为最小包装单位的订货数
                foreach (var curReaGoods in tempReaGoodsList)
                {
                    var sumGoodsQty = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.GoodsQty.HasValue == true && p.ReaGoodsID.Value == curReaGoods.Id).Sum(p => p.GoodsQty) * curReaGoods.GonvertQty;
                    if (sumGoodsQty.HasValue)
                        minTotalGoodsQty = minTotalGoodsQty + sumGoodsQty.Value;
                }
                //再将最小包装单位的订货数按最大包装单位转换系数进行转换
                vo.OrderCount = System.Math.Floor(minTotalGoodsQty / maxGonvertQtyReaGoods.GonvertQty);
                return;
            }
        }
        private void GetAcceptCount(IGrouping<string, ReaGoods> groupBy, ReaGoods maxGonvertQtyReaGoods, IList<ReaBmsCenSaleDtlConfirm> dtlAllList, ref ReaGoodsOfMaxGonvertQtyVO vo)
        {
            if (dtlAllList == null || dtlAllList.Count < 0) return;

            var dtlCurList = dtlAllList.Where(p => p.ReaGoodsNo == maxGonvertQtyReaGoods.ReaGoodsNo).ToList();
            if (dtlCurList == null || dtlCurList.Count < 0) return;
            if (maxGonvertQtyReaGoods.GonvertQty == 1)
            {
                vo.AcceptCount = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.ReaGoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.AcceptCount);
                return;
            }
            var minReaGoods = groupBy.Where(p => p.GonvertQty == 1).OrderBy(p => p.GonvertQty);
            if (minReaGoods == null || minReaGoods.Count() <= 0 || maxGonvertQtyReaGoods.GonvertQty <= 0)
            {
                vo.AcceptCount = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.ReaGoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.AcceptCount);
                return;
            }
            if (maxGonvertQtyReaGoods != minReaGoods.ElementAt(0) && maxGonvertQtyReaGoods.GonvertQty > 1)
            {
                double minTotalGoodsQty = 0;
                var tempReaGoodsList = groupBy.OrderBy(p => p.GonvertQty).ToList();
                //先将各验收货品转换为最小包装单位的验收接收数
                foreach (var curReaGoods in tempReaGoodsList)
                {
                    var sumGoodsQty = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.ReaGoodsID.Value == curReaGoods.Id).Sum(p => p.AcceptCount) * curReaGoods.GonvertQty;
                    minTotalGoodsQty = minTotalGoodsQty + sumGoodsQty;
                }
                //再将最小包装单位的验收接收数按最大包装单位转换系数进行转换
                vo.AcceptCount = System.Math.Floor(minTotalGoodsQty / maxGonvertQtyReaGoods.GonvertQty);
                return;
            }
        }
        private void GetRefuseCount(IGrouping<string, ReaGoods> groupBy, ReaGoods maxGonvertQtyReaGoods, IList<ReaBmsCenSaleDtlConfirm> dtlAllList, ref ReaGoodsOfMaxGonvertQtyVO vo)
        {
            if (dtlAllList == null || dtlAllList.Count < 0) return;

            var dtlCurList = dtlAllList.Where(p => p.ReaGoodsNo == maxGonvertQtyReaGoods.ReaGoodsNo).ToList();
            if (dtlCurList == null || dtlCurList.Count < 0) return;
            if (maxGonvertQtyReaGoods.GonvertQty == 1)
            {
                vo.RefuseCount = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.ReaGoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.RefuseCount);
                return;
            }
            var minReaGoods = groupBy.Where(p => p.GonvertQty == 1).OrderBy(p => p.GonvertQty);
            if (minReaGoods == null || minReaGoods.Count() <= 0 || maxGonvertQtyReaGoods.GonvertQty <= 0)
            {
                vo.RefuseCount = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.ReaGoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.RefuseCount);
                return;
            }
            if (maxGonvertQtyReaGoods != minReaGoods.ElementAt(0) && maxGonvertQtyReaGoods.GonvertQty > 1)
            {
                double minTotalGoodsQty = 0;
                var tempReaGoodsList = groupBy.OrderBy(p => p.GonvertQty).ToList();
                //先将各验收货品转换为最小包装单位的拒收接收数
                foreach (var curReaGoods in tempReaGoodsList)
                {
                    var sumGoodsQty = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.ReaGoodsID.Value == curReaGoods.Id).Sum(p => p.RefuseCount) * curReaGoods.GonvertQty;
                    minTotalGoodsQty = minTotalGoodsQty + sumGoodsQty;
                }
                //再将最小包装单位的验收拒收数按最大包装单位转换系数进行转换
                vo.RefuseCount = System.Math.Floor(minTotalGoodsQty / maxGonvertQtyReaGoods.GonvertQty);
                return;
            }
        }
        private void GetConfirmCount(IGrouping<string, ReaGoods> groupBy, ReaGoods maxGonvertQtyReaGoods, IList<ReaBmsCenSaleDtlConfirm> dtlAllList, ref ReaGoodsOfMaxGonvertQtyVO vo)
        {
            if (dtlAllList == null || dtlAllList.Count < 0) return;

            var dtlCurList = dtlAllList.Where(p => p.ReaGoodsNo == maxGonvertQtyReaGoods.ReaGoodsNo).ToList();
            if (dtlCurList == null || dtlCurList.Count < 0) return;
            if (maxGonvertQtyReaGoods.GonvertQty == 1)
            {
                vo.ConfirmCount = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.ReaGoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => (p.AcceptCount + p.RefuseCount));
                return;
            }
            var minReaGoods = groupBy.Where(p => p.GonvertQty == 1).OrderBy(p => p.GonvertQty);
            if (minReaGoods == null || minReaGoods.Count() <= 0 || maxGonvertQtyReaGoods.GonvertQty <= 0)
            {
                vo.ConfirmCount = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.ReaGoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => (p.AcceptCount + p.RefuseCount));
                return;
            }
            if (maxGonvertQtyReaGoods != minReaGoods.ElementAt(0) && maxGonvertQtyReaGoods.GonvertQty > 1)
            {
                double acceptCount = 0, refuseCount = 0;
                var tempReaGoodsList = groupBy.OrderBy(p => p.GonvertQty).ToList();
                //先将各验收货品转换为最小包装单位的验收接收数
                foreach (var curReaGoods in tempReaGoodsList)
                {
                    var sumAcceptCount = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.ReaGoodsID.Value == curReaGoods.Id).Sum(p => p.AcceptCount) * curReaGoods.GonvertQty;
                    acceptCount = acceptCount + sumAcceptCount;

                    var sumRefuseCount = dtlCurList.Where(p => p.ReaGoodsID.HasValue == true && p.ReaGoodsID.Value == curReaGoods.Id).Sum(p => p.RefuseCount) * curReaGoods.GonvertQty;
                    refuseCount = refuseCount + sumRefuseCount;
                }
                //再将最小包装单位的验收接收数按最大包装单位转换系数进行转换
                acceptCount = System.Math.Floor(acceptCount / maxGonvertQtyReaGoods.GonvertQty);
                //再将最小包装单位的验收拒收数按最大包装单位转换系数进行转换
                refuseCount = System.Math.Floor(refuseCount / maxGonvertQtyReaGoods.GonvertQty);

                vo.ConfirmCount = acceptCount + refuseCount;
                return;
            }
        }
        private void GetInCount(IGrouping<string, ReaGoods> groupBy, ReaGoods maxGonvertQtyReaGoods, IList<ReaBmsInDtl> dtlAllList, ref ReaGoodsOfMaxGonvertQtyVO vo)
        {
            if (dtlAllList == null || dtlAllList.Count < 0) return;

            var dtlCurList = dtlAllList.Where(p => p.ReaGoodsNo == maxGonvertQtyReaGoods.ReaGoodsNo).ToList();
            if (dtlCurList == null || dtlCurList.Count < 0) return;
            //当前机构货品为最小包装单位时,入库数处理
            if (maxGonvertQtyReaGoods.GonvertQty == 1)
            {
                vo.InCount = dtlCurList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.GoodsQty);
                return;
            }
            var minReaGoods = groupBy.Where(p => p.GonvertQty == 1).OrderBy(p => p.GonvertQty);
            //当前机构货品没有最小包装单位时,入库数处理
            if (minReaGoods == null || minReaGoods.Count() <= 0 || maxGonvertQtyReaGoods.GonvertQty <= 0)
            {
                vo.InCount = dtlCurList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.GoodsQty);
                return;
            }
            //当前机构货品有最小包装单位时,入库数处理
            if (maxGonvertQtyReaGoods != minReaGoods.ElementAt(0) && maxGonvertQtyReaGoods.GonvertQty > 1)
            {
                double minTotalGoodsQty = 0;
                var tempReaGoodsList = groupBy.OrderBy(p => p.GonvertQty).ToList();
                //先将各入库货品转换为最小包装单位的入库数
                foreach (var curReaGoods in tempReaGoodsList)
                {
                    var sumGoodsQty = dtlCurList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == curReaGoods.Id).Sum(p => p.GoodsQty) * curReaGoods.GonvertQty;
                    if (sumGoodsQty.HasValue)
                        minTotalGoodsQty = minTotalGoodsQty + sumGoodsQty.Value;
                }
                //再将最小包装单位的入库数按最大包装单位转换系数进行转换
                vo.InCount = System.Math.Floor(minTotalGoodsQty / maxGonvertQtyReaGoods.GonvertQty);
                return;
            }
        }
        private void GetCurQtyCount(IGrouping<string, ReaGoods> groupBy, ReaGoods maxGonvertQtyReaGoods, IList<ReaBmsQtyDtl> dtlAllList, ref ReaGoodsOfMaxGonvertQtyVO vo)
        {
            if (dtlAllList == null || dtlAllList.Count < 0) return;

            var dtlCurList = dtlAllList.Where(p => p.ReaGoodsNo == maxGonvertQtyReaGoods.ReaGoodsNo).ToList();
            if (dtlCurList == null || dtlCurList.Count < 0) return;
            //当前机构货品为最小包装单位货品时的当前库存数处理
            if (maxGonvertQtyReaGoods.GonvertQty == 1)
            {
                vo.CurQtyCount = dtlCurList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.GoodsQty);
                return;
            }
            var minReaGoods = groupBy.Where(p => p.GonvertQty == 1).OrderBy(p => p.GonvertQty);
            //当前机构货品不存在最小包装单位或转换系数小于等于0时的当前库存数处理
            if (minReaGoods == null || minReaGoods.Count() <= 0 || maxGonvertQtyReaGoods.GonvertQty <= 0)
            {
                vo.CurQtyCount = dtlCurList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.GoodsQty);
                return;
            }
            if (maxGonvertQtyReaGoods != minReaGoods.ElementAt(0) && maxGonvertQtyReaGoods.GonvertQty > 1)
            {
                double minTotalGoodsQty = 0;
                var tempReaGoodsList = groupBy.OrderBy(p => p.GonvertQty).ToList();
                //先将各库存货品转换为最小包装单位的库存数
                foreach (var curReaGoods in tempReaGoodsList)
                {
                    var sumGoodsQty = dtlCurList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == curReaGoods.Id).Sum(p => p.GoodsQty) * curReaGoods.GonvertQty;
                    if (sumGoodsQty.HasValue)
                        minTotalGoodsQty = minTotalGoodsQty + sumGoodsQty.Value;
                }
                //再将最小包装单位的库存数按最大包装单位转换系数进行转换
                vo.CurQtyCount = System.Math.Floor(minTotalGoodsQty / maxGonvertQtyReaGoods.GonvertQty);
                return;
            }
        }
        private void GetTransferCount(IGrouping<string, ReaGoods> groupBy, ReaGoods maxGonvertQtyReaGoods, IList<ReaBmsTransferDtl> dtlAllList, ref ReaGoodsOfMaxGonvertQtyVO vo)
        {
            if (dtlAllList == null || dtlAllList.Count < 0) return;

            var dtlCurList = dtlAllList.Where(p => p.ReaGoodsNo == maxGonvertQtyReaGoods.ReaGoodsNo).ToList();
            if (dtlCurList == null || dtlCurList.Count < 0) return;
            //当前机构货品为最小包装单位货品时的移库领用数处理
            if (maxGonvertQtyReaGoods.GonvertQty == 1)
            {
                vo.TransferCount = dtlCurList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.GoodsQty);
                return;
            }
            var minReaGoods = groupBy.Where(p => p.GonvertQty == 1).OrderBy(p => p.GonvertQty);
            //当前机构货品不存在最小包装单位或转换系数小于等于0时的移库领用数处理
            if (minReaGoods == null || minReaGoods.Count() <= 0 || maxGonvertQtyReaGoods.GonvertQty <= 0)
            {
                vo.TransferCount = dtlCurList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.GoodsQty);
                return;
            }
            if (maxGonvertQtyReaGoods != minReaGoods.ElementAt(0) && maxGonvertQtyReaGoods.GonvertQty > 1)
            {
                double minTotalGoodsQty = 0;
                var tempReaGoodsList = groupBy.OrderBy(p => p.GonvertQty).ToList();
                //先将各库存货品转换为最小包装单位的库存数
                foreach (var curReaGoods in tempReaGoodsList)
                {
                    var sumGoodsQty = dtlCurList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == curReaGoods.Id).Sum(p => p.GoodsQty) * curReaGoods.GonvertQty;
                    minTotalGoodsQty = minTotalGoodsQty + sumGoodsQty;
                }
                //再将最小包装单位的库存数按最大包装单位转换系数进行转换
                vo.TransferCount = System.Math.Floor(minTotalGoodsQty / maxGonvertQtyReaGoods.GonvertQty);
                return;
            }
        }
        private void GetTestEquipOutCount(IGrouping<string, ReaGoods> groupBy, ReaGoods maxGonvertQtyReaGoods, IList<ReaBmsOutDtl> dtlAllList, ref ReaGoodsOfMaxGonvertQtyVO vo)
        {
            if (dtlAllList == null || dtlAllList.Count < 0) return;

            var dtlCurList = dtlAllList.Where(p => p.ReaGoodsNo == maxGonvertQtyReaGoods.ReaGoodsNo).ToList();
            if (dtlCurList == null || dtlCurList.Count < 0) return;

            //当前机构货品为最小包装单位货品时的上机使用数量处理
            if (maxGonvertQtyReaGoods.GonvertQty == 1)
            {
                vo.TestEquipOutCount = dtlCurList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.GoodsQty);
                return;
            }
            var minReaGoods = groupBy.Where(p => p.GonvertQty == 1).OrderBy(p => p.GonvertQty);
            //当前机构货品不存在最小包装单位或转换系数小于等于0时的上机使用数量处理
            if (minReaGoods == null || minReaGoods.Count() <= 0 || maxGonvertQtyReaGoods.GonvertQty <= 0)
            {
                vo.TestEquipOutCount = dtlCurList.Where(p => p.GoodsID.Value == maxGonvertQtyReaGoods.Id).Sum(p => p.GoodsQty);
                return;
            }
            if (maxGonvertQtyReaGoods != minReaGoods.ElementAt(0) && maxGonvertQtyReaGoods.GonvertQty > 1)
            {
                double minTotalGoodsQty = 0;
                var tempReaGoodsList = groupBy.OrderBy(p => p.GonvertQty).ToList();
                //先将各库存货品转换为最小包装单位的库存数
                foreach (var curReaGoods in tempReaGoodsList)
                {
                    var sumGoodsQty = dtlCurList.Where(p => p.GoodsID.Value == curReaGoods.Id).Sum(p => p.GoodsQty) * curReaGoods.GonvertQty;
                    minTotalGoodsQty = minTotalGoodsQty + sumGoodsQty;
                }
                //再将最小包装单位的库存数按最大包装单位转换系数进行转换
                vo.TestEquipOutCount = System.Math.Floor(minTotalGoodsQty / maxGonvertQtyReaGoods.GonvertQty);
                return;
            }
        }
        public Stream GetReaGoodsStatisticsOfMaxGonvertQtyReportOfExcelByHql(long labID, string labCName, int groupType, string companyId, string deptId, string testEquipId, string reaGoodsNo, string startDate, string endDate, string sort, string breportType, string frx, ref string fileName)
        {
            if (string.IsNullOrEmpty(companyId) && string.IsNullOrEmpty(deptId) && string.IsNullOrEmpty(testEquipId) && string.IsNullOrEmpty(reaGoodsNo) && string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("统计条件信息为空!");
            }
            Stream stream = null;
            IList<ReaGoodsOfMaxGonvertQtyVO> dtlList = SearchComprehensiveStatistics1OfMaxGonvertQtyListHQL(groupType, companyId, deptId, testEquipId, reaGoodsNo, startDate, endDate, sort, -1, -1);

            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取按最大包装单位综合统计信息为空!");
            }

            //获取综合统计按机构货品模板
            if (string.IsNullOrEmpty(frx))
                frx = "按最大包装单位综合统计.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = frx;
            fileName = "按最大包装单位综合统计信息";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaGoodsOfMaxGonvertQtyVO, ReaGoodsOfMaxGonvertQtyVO>(null, dtlList, excelCommand, breportType, labID, frx, excelFile, ref saveFullPath);
            fileName = "按最大包装单位综合统计信息" + fileExt;
            return stream;
        }
        public Stream GetReaGoodsStatisticsOfMaxGonvertQtyReportOfPdfByHql(string reaReportClass, long labId, string labCName, int groupType, string companyId, string deptId, string testEquipId, string reaGoodsNo, string startDate, string endDate, string sort, string breportType, string frx, ref string pdfFileName)
        {
            if (string.IsNullOrEmpty(companyId) && string.IsNullOrEmpty(deptId) && string.IsNullOrEmpty(testEquipId) && string.IsNullOrEmpty(reaGoodsNo) && string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("统计条件信息为空!");
            }
            Stream stream = null;
            IList<ReaGoodsOfMaxGonvertQtyVO> dtlList = SearchComprehensiveStatistics1OfMaxGonvertQtyListHQL(groupType, companyId, deptId, testEquipId, reaGoodsNo, startDate, endDate, sort, -1, -1);

            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取按最大包装单位综合统计信息为空!");
            }

            pdfFileName = "按最大包装单位综合统计汇总.pdf";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = CreateReaGoodsStatisticsOfMaxGonvertQtyReportOfPdfByHql(labId, labCName, pdfFileName, dtlList, frx);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                string excelFileFullDir = "";
                //获取出库汇总模板
                if (string.IsNullOrEmpty(frx))
                    frx = "按最大包装单位综合统计.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = fileExt;
                Stream stream2 = null;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
                stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaGoodsOfMaxGonvertQtyVO, ReaGoodsOfMaxGonvertQtyVO>(null, dtlList, excelCommand, breportType, labId, frx, excelFile, ref excelFileFullDir);
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
        private Stream CreateReaGoodsStatisticsOfMaxGonvertQtyReportOfPdfByHql(long labId, string labCName, string pdfFileName, IList<ReaGoodsOfMaxGonvertQtyVO> dtlList, string frx)
        {
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";
            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaGoodsOfMaxGonvertQtyVO>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "Rea_GoodsOfMaxGonvertQtyVO";
                dataSet.Tables.Add(dtDtl);
            }
            if (string.IsNullOrEmpty(frx))
                frx = "按最大包装单位综合统计.frx";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, labId, pdfFileName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.综合统计.Key].Name, frx, false);
            return stream;
        }
        #endregion

    }
}
