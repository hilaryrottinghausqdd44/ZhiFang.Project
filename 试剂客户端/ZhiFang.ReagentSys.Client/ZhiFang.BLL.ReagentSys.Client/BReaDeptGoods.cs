using System;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.RBAC;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Text;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaDeptGoods : BaseBLL<ReaDeptGoods>, ZhiFang.IBLL.ReagentSys.Client.IBReaDeptGoods
    {
        IBHRDept IBHRDept { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaBmsQtyDtlDao IDReaBmsQtyDtlDao { get; set; }

        public EntityList<ReaDeptGoods> SearchReaGoodsByHRDeptID(long deptID, string where, string sort, int page, int limit)
        {
            EntityList<ReaDeptGoods> entityList = new EntityList<ReaDeptGoods>();
            string strWhere = "";
            string strDeptID = IBHRDept.SearchHRDeptIdListByHRDeptId(deptID);

            if (!string.IsNullOrWhiteSpace(where))
                strWhere = where;
            if (!string.IsNullOrWhiteSpace(strDeptID))
            {
                if (!string.IsNullOrWhiteSpace(strWhere))
                {
                    strWhere += " and";
                }
                strWhere += " readeptgoods.DeptID in (" + strDeptID + ")";
            }
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = this.SearchListByHQL(strWhere, sort, page, limit);
                }
                else
                {
                    entityList = this.SearchListByHQL(strWhere, page, limit);
                }
            }
            return entityList;
        }
        /// <summary>
        /// 依部门Id获取该部门(包含子部门)下的所有部门的货品Id信息
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public string SearchReaDeptGoodsListByHRDeptId(long deptId)
        {
            string goodIdStr = "";
            string strDeptID = IBHRDept.SearchHRDeptIdListByHRDeptId(deptId);
            if (string.IsNullOrEmpty(strDeptID)) return goodIdStr;

            string strWhere = "";
            strWhere = "readeptgoods.Visible=1 and readeptgoods.DeptID in (" + strDeptID + ")";
            IList<ReaDeptGoods> tempList2 = this.SearchListByHQL(strWhere);
            //按货品进行分组,找出每组货品下的对应供应商信息
            var tempGroupBy = tempList2.GroupBy(p => p.ReaGoods).ToList();
            foreach (var listReaGoods in tempGroupBy)
            {
                for (int i = 0; i < listReaGoods.Count(); i++)
                {
                    var item = listReaGoods.ElementAt(i);
                    if (string.IsNullOrEmpty(goodIdStr))
                    {
                        goodIdStr = item.ReaGoods.Id.ToString();
                    }
                    else
                    {
                        if (!goodIdStr.Contains("," + item.ReaGoods.Id.ToString()))
                            goodIdStr += "," + item.ReaGoods.Id.ToString();
                    }
                }
            }
            return goodIdStr;
        }

        #region 获取采购申请部门货品信息
        public EntityList<ReaDeptGoods> SearchListByDeptIdAndHQL(long deptId, string strHqlWhere, string goodsQty, string order, int page, int limit)
        {
            EntityList<ReaDeptGoods> entityList = new EntityList<ReaDeptGoods>();
            entityList.list = new List<ReaDeptGoods>();
            if (string.IsNullOrEmpty(order))
                order = "";

            //不包含库存查询条件及按库存排序信息
            if (string.IsNullOrEmpty(goodsQty) && !order.Contains("reabmsqtydtl.GoodsQty"))
            {
                entityList = this.SearchListByHQL(strHqlWhere, order, page, limit);
                for (int i = 0; i < entityList.list.Count; i++)
                {
                    entityList.list[i].CurrentQtyVO = GetReaGoodsCurrentQtyVO(entityList.list[i]);
                }
                return entityList;
            }
            else
            {
                //先按部门获取全部的部门货品信息,并获取对应的库存数(转换为最小包装单位),再排序分页
                IList<ReaDeptGoods> deptGoodsList = new List<ReaDeptGoods>();
                //没有按库存数排序信息,部门货品直接按查询条件分页获取
                if (!string.IsNullOrEmpty(order) && !order.Contains("reabmsqtydtl.GoodsQty"))
                {
                    deptGoodsList = this.SearchListByHQL(strHqlWhere, order, page, limit).list;
                }
                else
                {
                    deptGoodsList = this.SearchListByHQL(strHqlWhere);
                }
                //获取库存信息
                for (int i = 0; i < deptGoodsList.Count; i++)
                {
                    deptGoodsList[i].CurrentQtyVO = GetReaGoodsCurrentQtyVO(deptGoodsList[i]);
                }
                //按库存条件过滤(库存数小于等于多少)
                if (!string.IsNullOrEmpty(goodsQty))
                    deptGoodsList = deptGoodsList.Where(p => p.CurrentQtyVO.GoodsQty.Value <= double.Parse(goodsQty)).ToList();

                //按库存数进行排序
                if (!string.IsNullOrEmpty(order) && order.Contains("reabmsqtydtl.GoodsQty"))
                {
                    if (order.ToUpper().Contains("DESC"))
                    {
                        deptGoodsList = deptGoodsList.OrderByDescending(p => p.CurrentQtyVO.GoodsQty).ToList();
                    }
                    else
                    {
                        deptGoodsList = deptGoodsList.OrderBy(p => p.CurrentQtyVO.GoodsQty).ToList();
                    }
                }
                else if (string.IsNullOrEmpty(order))
                {
                    deptGoodsList = deptGoodsList.OrderBy(p => p.CurrentQtyVO.GoodsQty).ToList();
                }
                entityList.count = deptGoodsList.Count;
                //最后将部门货品进行分页
                if (limit > 0 && limit < deptGoodsList.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = deptGoodsList.Skip(startIndex).Take(endIndex);
                    if (list != null)
                        deptGoodsList = list.ToList();
                }
                entityList.list = deptGoodsList;
            }

            return entityList;
        }

        private ReaGoodsCurrentQtyVO GetReaGoodsCurrentQtyVO(ReaDeptGoods deptGoods)
        {
            ReaGoodsCurrentQtyVO vo = new ReaGoodsCurrentQtyVO();
            //先找出部门货品的所的包装单位的机构货品信息
            string reagoodsHql = string.Format("(reagoods.Visible=1 and reagoods.ReaGoodsNo='{0}')", deptGoods.ReaGoods.ReaGoodsNo);
            //ZhiFang.Common.Log.Log.Debug("reagoodsHql:" + reagoodsHql);
            IList<ReaGoods> reaGoodsList = IDReaGoodsDao.GetListByHQL(reagoodsHql);
            //机构货品可能被停用,但库存记录还存在
            if (reaGoodsList == null || reaGoodsList.Count <= 0)
            {
                reagoodsHql = string.Format("(reagoods.ReaGoodsNo='{0}')", deptGoods.ReaGoods.ReaGoodsNo);
                reaGoodsList = IDReaGoodsDao.GetListByHQL(reagoodsHql);
            }
            //再按机构货品获取到库存信息
            StringBuilder searchHQL = new StringBuilder();
            for (int i = 0; i < reaGoodsList.Count; i++)
            {
                string tempHql = string.Format("(reabmsqtydtl.GoodsID={0})", reaGoodsList[i].Id);
                if (string.IsNullOrEmpty(searchHQL.ToString()))
                    searchHQL.Append(tempHql);
                else
                {
                    searchHQL.Append(" or ");
                    searchHQL.Append(tempHql);
                }
            }
            if (!string.IsNullOrEmpty(searchHQL.ToString()))
            {
                string qtyHQL = string.Format("reabmsqtydtl.GoodsQty>0 and reabmsqtydtl.Visible=1 and ({0})", searchHQL.ToString());
                //ZhiFang.Common.Log.Log.Debug("qtyHQL:" + qtyHQL);
                IList<ReaBmsQtyDtl> qtyList = IDReaBmsQtyDtlDao.GetListByHQL(qtyHQL);
                vo.GoodsQty = GetVOCurrentQty(qtyList, reaGoodsList, deptGoods.ReaGoods);
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("reagoodsHql:" + reagoodsHql);
                vo.GoodsQty = 0;
            }
            vo.CurGoodsId = deptGoods.ReaGoods.Id;
            vo.CurrentQty = "";

            //ZhiFang.Common.Log.Log.Debug("ReaGoodsNo:" + deptGoods.ReaGoods.ReaGoodsNo + ",GoodsQty:" + vo.GoodsQty);
            return vo;
        }
        public double? GetVOCurrentQty(IList<ReaBmsQtyDtl> qtyList, IList<ReaGoods> reaGoodsList, ReaGoods reaGoods)
        {
            double? currentQty = 0;
            //当前机构货品为最小包装单位货品时的当前库存数处理
            if (reaGoods.GonvertQty == 1)
                return currentQty = qtyList.Where(p => p.GoodsID.HasValue == true && p.GoodsID.Value == reaGoods.Id).Sum(p => p.GoodsQty);
            //最小包装单位的货品
            ReaGoods minReaGoods = null;
            var minUnit = reaGoodsList.Where(p => p.GonvertQty == 1);
            if (minUnit != null && minUnit.Count() == 1)
            {
                minReaGoods = minUnit.ElementAt(0);
            }
            if (minReaGoods != null)
            {
                var reaGoodsGroupBy = reaGoodsList.OrderBy(p => p.GonvertQty).ToList();
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
        #endregion

        public EntityList<ReaGoods> SearchReaGoodsListByHQL(int page, int limit, string where, string linkWhere, string sort)
        {
            EntityList<ReaGoods> entityList = new EntityList<ReaGoods>();

            IList<ReaDeptGoods> linkList1 = ((IDReaDeptGoodsDao)base.DBDao).GetListByHQL(linkWhere);
            if (linkList1 != null && linkList1.Count > 0)
            {
                IList<ReaGoods> entityList1 = IDReaGoodsDao.GetListByHQL(where, sort, -1, -1).list;
                //找出关系表里的机构货品集合信息
                var linkList = (from list2 in linkList1
                                select list2.ReaGoods).ToList<ReaGoods>();
                //比较生成两个序列的差集
                List<ReaGoods> reaGoodsList = entityList1.Except(linkList).ToList();

                entityList.count = reaGoodsList.Count;
                //进行分页
                if (limit > 0 && limit < reaGoodsList.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = reaGoodsList.Skip(startIndex).Take(endIndex);
                    if (list != null)
                        reaGoodsList = list.ToList();
                }
                entityList.list = reaGoodsList;
            }
            else
            {
                entityList = IDReaGoodsDao.GetListByHQL(where, sort, page, limit);
            }

            return entityList;
        }

    }
}