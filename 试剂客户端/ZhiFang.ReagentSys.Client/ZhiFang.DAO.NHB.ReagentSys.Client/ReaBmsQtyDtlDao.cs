using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsQtyDtlDao : BaseDaoNHB<ReaBmsQtyDtl, long>, IDReaBmsQtyDtlDao
    {
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        public IList<ReaBmsQtyDtlWarning> SearchReaBmsQtyDtlWarningList(int warningType, int groupByType, float storePercent, string strWhere)
        {
            IList<ReaBmsQtyDtlWarning> tempEntityList = new List<ReaBmsQtyDtlWarning>();

            List<string> paranamea = new List<string> { "warningType", "groupByType", "storePercent", "strWhere" };
            object[] paravaluea = new string[paranamea.Count];

            for (int i = 0; i < paravaluea.Length; i++)
            {
                paravaluea[i] = "";
            }
            if (paranamea.IndexOf("warningType") >= 0)
                paravaluea[paranamea.IndexOf("warningType")] = warningType;
            if (paranamea.IndexOf("groupByType") >= 0)
                paravaluea[paranamea.IndexOf("groupByType")] = groupByType;
            if (paranamea.IndexOf("storePercent") >= 0)
                paravaluea[paranamea.IndexOf("storePercent")] = storePercent;
            if (!string.IsNullOrEmpty(strWhere))
            {
                if (paranamea.IndexOf("strWhere") >= 0)
                    paravaluea[paranamea.IndexOf("strWhere")] = " " + strWhere.ToString().Trim();
            }
            tempEntityList = base.HibernateTemplate.FindByNamedQueryAndNamedParam<ReaBmsQtyDtlWarning>("P_Rea_BmsQtyDtlWarning", paranamea.ToArray(), paravaluea);
            return tempEntityList;
        }
        public BaseResultBool UpdatePrintCount(IList<long> batchList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (batchList == null || batchList.Count <= 0)
                return tempBaseResultBool;

            foreach (var id in batchList)
            {
                string hql = "update ReaBmsQtyDtl reabmsqtydtl set reabmsqtydtl.PrintCount=(reabmsqtydtl.PrintCount+1) where reabmsqtydtl.Id=" + id;
                int counts = this.UpdateByHql(hql);
                if (counts > 0)
                    tempBaseResultBool.success = true;
                else
                    tempBaseResultBool.success = false;
            }
            return tempBaseResultBool;
        }
        public string GetCurrentQtyInfo(IList<ReaBmsQtyDtl> qtyList, string reaGoodsNo)
        {
            string currentQty = "";
            //将库存数按从大包装单位小包装单位进行转换处理
            IList<ReaGoods> reaGoodsList = IDReaGoodsDao.GetListByHQL("reagoods.ReaGoodsNo='" + reaGoodsNo + "'");
            var reaGoodsGroupBy = reaGoodsList.OrderByDescending(p => p.GonvertQty).ToList().GroupBy(p => p.UnitName);
            //最小包装单位的试剂
            ReaGoods minReaGoods = null;
            var minUnit = reaGoodsList.Where(p => p.GonvertQty == 1);
            if (minUnit != null && minUnit.Count() == 1)
            {
                minReaGoods = minUnit.ElementAt(0);
            }

            if (minReaGoods != null)
            {
                #region 存在最小包装单位的库存数描述处理
                //最小包装单位的库存总数
                double totalGoodsQty = qtyList.Where(p => p.GoodsID.Value == minReaGoods.Id && p.GoodsQty > 0).Sum(p => p.GoodsQty.Value);
                #endregion

                #region 将库存总数按从大到小的包装单位进行转换处理
                double tempGoodsQty = 0;
                foreach (var groupBy in reaGoodsGroupBy)
                {
                    if (groupBy.ElementAt(0).GonvertQty > 1)
                    {
                        tempGoodsQty = System.Math.Floor(totalGoodsQty / groupBy.ElementAt(0).GonvertQty);
                        if (tempGoodsQty > 0)
                        {
                            currentQty = tempGoodsQty + groupBy.Key + ";";
                        }
                        //剩下的库存总数=当前库存总数-大包装单位的库存数
                        totalGoodsQty = totalGoodsQty - tempGoodsQty * groupBy.ElementAt(0).GonvertQty;
                    }
                }
                #endregion

                //当前试剂只有最小包装单位
                if (string.IsNullOrEmpty(currentQty))
                {
                    var tempList = reaGoodsList.Where(p => p.GonvertQty > 1);
                    if (tempList == null || tempList.Count() <= 0)
                        currentQty = totalGoodsQty + minReaGoods.UnitName;
                }
            }
            else
            {
                foreach (var groupBy in reaGoodsGroupBy)
                {
                    var curGoodsQty = qtyList.Where(p => p.GoodsID.Value == groupBy.ElementAt(0).Id).Sum(p => p.GoodsQty.Value);
                    if (curGoodsQty > 0)
                    {
                        currentQty = curGoodsQty + groupBy.Key + ";";
                    }
                }
            }
            return currentQty;
        }
        public IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlAndReaGoodsListByAllJoinHql(string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsQtyDtl> entityList = new List<ReaBmsQtyDtl>();
            StringBuilder sqlHql = new StringBuilder();
            if (!string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select new ReaBmsQtyDtl(reabmsqtydtl,reagoods) from ReaBmsQtyDtl reabmsqtydtl,ReaGoods reagoods,ReaDeptGoods readeptgoods where reabmsqtydtl.GoodsID=reagoods.Id and reabmsqtydtl.GoodsID=readeptgoods.ReaGoods.Id ");
                sqlHql.Append(" and " + deptGoodsHql);
            }
            else
            {
                sqlHql.Append(" select new ReaBmsQtyDtl(reabmsqtydtl,reagoods) from ReaBmsQtyDtl reabmsqtydtl,ReaGoods reagoods where reabmsqtydtl.GoodsID=reagoods.Id ");
            }

            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                sqlHql.Append(" and " + reaGoodsHql);
            }
            if (!string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" and " + deptGoodsHql);
            }
            if (!string.IsNullOrEmpty(qtyHql))
            {
                sqlHql.Append(" and " + qtyHql);
            }

            string hql = sqlHql.ToString();
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
            {
                start1 = page;
            }
            if (limit > 0)
            {
                count1 = limit;
            }
            GetDataRowRoleHQLString("reabmsqtydtl");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsQtyDtl>, ReaBmsQtyDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsQtyDtl>, ReaBmsQtyDtl>(hql, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaBmsQtyDtl>>(action);
            if (entityList != null && entityList.Count > 0) entityList = entityList.Distinct().ToList();

            return entityList;
        }
        public IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListByHql(string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsQtyDtl> entityList = new List<ReaBmsQtyDtl>();
            StringBuilder sqlHql = new StringBuilder();
            if (string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select reabmsqtydtl from ReaBmsQtyDtl reabmsqtydtl where 1=1 ");
            }
            else if (!string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select  new ReaBmsQtyDtl(reabmsqtydtl,reagoods) from ReaBmsQtyDtl reabmsqtydtl,ReaGoods reagoods where reabmsqtydtl.GoodsID=reagoods.Id ");
                sqlHql.Append(" and " + reaGoodsHql);//DISTINCT
            }
            else if (string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select new ReaBmsQtyDtl(reabmsqtydtl,readeptgoods.ReaGoods) from ReaBmsQtyDtl reabmsqtydtl,ReaDeptGoods readeptgoods where reabmsqtydtl.GoodsID=readeptgoods.ReaGoods.Id ");//DISTINCT
                sqlHql.Append(" and " + deptGoodsHql);
            }
            else if (!string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select new ReaBmsQtyDtl(reabmsqtydtl,reagoods) from ReaBmsQtyDtl reabmsqtydtl,ReaGoods reagoods,ReaDeptGoods readeptgoods where reabmsqtydtl.GoodsID=reagoods.Id and reabmsqtydtl.GoodsID=readeptgoods.ReaGoods.Id ");//DISTINCT
                sqlHql.Append(" and " + deptGoodsHql);
                sqlHql.Append(" and " + reaGoodsHql);
            }
            if (!string.IsNullOrEmpty(qtyHql))
            {
                sqlHql.Append(" and " + qtyHql);
            }
            string hql = sqlHql.ToString();
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
            {
                start1 = page;
            }
            if (limit > 0)
            {
                count1 = limit;
            }
            GetDataRowRoleHQLString("reabmsqtydtl");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsQtyDtl>, ReaBmsQtyDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsQtyDtl>, ReaBmsQtyDtl>(hql, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaBmsQtyDtl>>(action);
            if (entityList != null && entityList.Count > 0) entityList = entityList.Distinct().ToList();
            return entityList;
        }
        public EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListByHql(string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            entityList.list = new List<ReaBmsQtyDtl>();
            entityList.list = SearchReaBmsQtyDtlListByHql(qtyHql, deptGoodsHql, reaGoodsHql, sort, page, limit);
            if (entityList.list == null || entityList.list.Count <= 0)
                return entityList;

            StringBuilder countHql = new StringBuilder();
            if (string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsqtydtl.Id) from ReaBmsQtyDtl reabmsqtydtl where 1=1 ");
            }
            else if (!string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsqtydtl.Id) from ReaBmsQtyDtl reabmsqtydtl,ReaGoods reagoods where reabmsqtydtl.GoodsID=reagoods.Id ");
                countHql.Append(" and " + reaGoodsHql);
            }
            else if (string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsqtydtl.Id) from ReaBmsQtyDtl reabmsqtydtl,ReaDeptGoods readeptgoods where reabmsqtydtl.GoodsID=readeptgoods.ReaGoods.Id ");
                countHql.Append(" and " + deptGoodsHql);
            }
            else if (!string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsqtydtl.Id) from ReaBmsQtyDtl reabmsqtydtl,ReaGoods reagoods,ReaDeptGoods readeptgoods where reabmsqtydtl.GoodsID=reagoods.Id and reabmsqtydtl.GoodsID=readeptgoods.ReaGoods.Id ");
                countHql.Append(" and " + deptGoodsHql);
                countHql.Append(" and " + reaGoodsHql);
            }
            if (!string.IsNullOrEmpty(qtyHql))
            {
                countHql.Append(" and " + qtyHql);
            }
            GetDataRowRoleHQLString("reabmsqtydtl");
            countHql.Append(" and " + DataRowRoleHQLString);
            string countHql2 = FilterMacroCommand(countHql.ToString());//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaBmsQtyDtl> actionCount = new DaoNHBGetCountByHqlAction<ReaBmsQtyDtl>(countHql2);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }
        public IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListOfQtyGEZeroByJoinHql(string where, string inDtlHql, string qtyHql, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsQtyDtl> entityList = new List<ReaBmsQtyDtl>();
            string listHqlStr = "";
            StringBuilder listHql = new StringBuilder();
            string onHql = " reabmsqtydtl.GoodsQty>0 and reabmsqtydtl.InDtlID=reabmsindtl.Id  and reabmsqtydtl.InDocNo=reabmsindtl.InDocNo and reabmsqtydtl.StorageID=reabmsindtl.StorageID and reabmsqtydtl.GoodsID=reagoods.Id ";
            listHql.Append(" select DISTINCT reabmsqtydtl from ReaBmsQtyDtl reabmsqtydtl,ReaBmsInDtl reabmsindtl,ReaGoods reagoods ");
            listHql.Append(" where ");
            listHql.Append(onHql);
            if (!string.IsNullOrEmpty(where))
            {
                listHql.Append(" and " + where);
            }
            if (!string.IsNullOrEmpty(inDtlHql))
            {
                listHql.Append(" and " + inDtlHql);
            }
            if (!string.IsNullOrEmpty(qtyHql))
            {
                listHql.Append(" and " + qtyHql);
            }
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                listHql.Append(" and " + reaGoodsHql);
            }
            listHqlStr = listHql.ToString();
            GetDataRowRoleHQLString("reabmsqtydtl");
            listHqlStr += " and " + DataRowRoleHQLString;
            listHqlStr = FilterMacroCommand(listHqlStr);//宏命令过滤
            if (!string.IsNullOrEmpty(sort))
                listHqlStr = listHqlStr + " order by " + sort;
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
            {
                start1 = page;
            }
            if (limit > 0)
            {
                count1 = limit;
            }

            DaoNHBSearchByHqlAction<List<ReaBmsQtyDtl>, ReaBmsQtyDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsQtyDtl>, ReaBmsQtyDtl>(listHqlStr, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaBmsQtyDtl>>(action).Distinct().ToList();
            return entityList;
        }
        public EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListOfQtyGEZeroByJoinHql(string where, string inDtlHql, string qtyHql, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            entityList.list = new List<ReaBmsQtyDtl>();
            entityList.list = SearchReaBmsQtyDtlListOfQtyGEZeroByJoinHql(where, inDtlHql, qtyHql, reaGoodsHql, sort, page, limit);

            string countHqlStr = "";
            StringBuilder countHql = new StringBuilder();
            //联查默认过滤条件and reabmsqtydtl.PlaceID=reabmsindtl.PlaceID
            string onHql = " reabmsqtydtl.GoodsQty>0 and reabmsqtydtl.InDtlID=reabmsindtl.Id  and reabmsqtydtl.InDocNo=reabmsindtl.InDocNo and reabmsqtydtl.StorageID=reabmsindtl.StorageID and reabmsqtydtl.GoodsID=reagoods.Id ";
            countHql.Append(" select count(DISTINCT reabmsqtydtl.Id) from ReaBmsQtyDtl reabmsqtydtl,ReaBmsInDtl reabmsindtl,ReaGoods reagoods ");
            countHql.Append(" where ");
            countHql.Append(onHql);

            if (!string.IsNullOrEmpty(where))
            {
                countHql.Append(" and " + where);
            }
            if (!string.IsNullOrEmpty(inDtlHql))
            {
                countHql.Append(" and " + inDtlHql);
            }
            if (!string.IsNullOrEmpty(qtyHql))
            {
                countHql.Append(" and " + qtyHql);
            }
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                countHql.Append(" and " + reaGoodsHql);
            }
            countHqlStr = countHql.ToString();
            GetDataRowRoleHQLString("reabmsqtydtl");
            countHqlStr += " and " + DataRowRoleHQLString;
            countHqlStr = FilterMacroCommand(countHqlStr);//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaBmsQtyDtl> action2 = new DaoNHBGetCountByHqlAction<ReaBmsQtyDtl>(countHqlStr);
            entityList.count = this.HibernateTemplate.Execute<int>(action2);
            return entityList;
        }

        /// <summary>
        /// 获取货运单号，根据入库明细表里的[供货验收明细单ID]找到Rea_BmsCenSaleDocConfirm_供货验收单表里的货运单号字段TransportNo
        /// </summary>
        /// <param name="SaleDtlConfirmID">供货验收明细单ID</param>
        /// <returns>货运单号</returns>
        public string GetTransportNo(long SaleDtlConfirmID)
        {
            string hql = "select b.TransportNo from ReaBmsCenSaleDtlConfirm a,ReaBmsCenSaleDocConfirm b where a.SaleDocConfirmID=b.Id and a.Id=" + SaleDtlConfirmID;
            var TransportNo = Session.CreateQuery(hql).UniqueResult();
            if (TransportNo != null)
            {
                return TransportNo.ToString();
            }
            else
            {
                return "";
            }
        }

    }
}