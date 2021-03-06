
using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaGoodsDao : Base.BaseDaoNHB<Entity.ReagentSys.Client.ReaGoods, long>, IDReaGoodsDao
    {
        public IList<ReaGoodsClassVO> SearchGoodsClassListByClassTypeAndHQL(string classType, bool hasNull, string whereHql, string sort, int page, int limit)
        {
            IList<ReaGoodsClassVO> entityList = new List<ReaGoodsClassVO>();
            string hql = " select DISTINCT reagoods.GoodsClass from ReaGoods reagoods where 1=1 ";
            if (classType.ToLower() == "goodsclasstype")
            {
                hql = " select DISTINCT reagoods.GoodsClassType from ReaGoods reagoods where 1=1 ";
                if (hasNull == false)
                {
                    hql = hql + " and reagoods.GoodsClassType is not null and reagoods.GoodsClassType!='' ";
                }
            }
            else
            {
                if (hasNull == false)
                {
                    hql = hql + " and reagoods.GoodsClass is not null and reagoods.GoodsClass!='' ";
                }
            }
            if (!string.IsNullOrEmpty(whereHql))
            {
                hql = hql + " and " + whereHql;
            }

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
            GetDataRowRoleHQLString("reagoods");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<string>, string> action = new DaoNHBSearchByHqlAction<List<string>, string>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<string>>(action);

            if (list != null)
            {
                foreach (var goodsClass in list)
                {
                    ReaGoodsClassVO vo = new ReaGoodsClassVO();
                    vo.Id = goodsClass;
                    vo.CName = goodsClass;
                    entityList.Add(vo);
                }
            }

            return entityList;
        }
        public EntityList<ReaGoodsClassVO> SearchGoodsClassEntityListByClassTypeAndHQL(string classType, bool hasNull, string whereHql, string sort, int page, int limit)
        {
            EntityList<ReaGoodsClassVO> entityList = new EntityList<ReaGoodsClassVO>();

            entityList.list = SearchGoodsClassListByClassTypeAndHQL(classType, hasNull, whereHql, sort, page, limit);
            string hql = " select count(DISTINCT reagoods.GoodsClass) from ReaGoods reagoods where 1=1 ";
            if (classType.ToLower() == "goodsclasstype")
            {
                hql = " select  count(DISTINCT reagoods.GoodsClassType) from ReaGoods reagoods where 1=1 ";
                if (hasNull == false)
                {
                    hql = hql + " and reagoods.GoodsClassType is not null and reagoods.GoodsClassType!='' ";
                }
            }
            else
            {
                if (hasNull == false)
                {
                    hql = hql + " and reagoods.GoodsClass is not null and reagoods.GoodsClass!='' ";
                }
            }
            if (!string.IsNullOrEmpty(whereHql))
            {
                hql = hql + " and " + whereHql;
            }
            GetDataRowRoleHQLString("reagoods");
            hql += " and " + DataRowRoleHQLString;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBGetCountByHqlAction<string> actionCount = new DaoNHBGetCountByHqlAction<string>(hql);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }
        /// <summary>
        /// 获取产品编号最大值(不能添加LabId作过滤条件)
        /// </summary>
        /// <returns></returns>
        public int GetMaxGoodsSort()
        {
            try
            {
                DaoNHBGetMaxByHqlAction<ReaGoods> action = new DaoNHBGetMaxByHqlAction<ReaGoods>("select max(reagoods.GoodsSort) as GoodsSort  from ReaGoods reagoods where 1=1 ");
                long orgNo = this.HibernateTemplate.Execute(action);
                int maxOrgNo = (int)orgNo;
                maxOrgNo = maxOrgNo + 1;
                return maxOrgNo;
            }
            catch (Exception ee)
            {
                string strHQL = "select reagoods from ReaGoods reagoods where 1=1 ";
                IList<ReaGoods> tempList = this.HibernateTemplate.Find<ReaGoods>(strHQL);
                int orgNo = tempList.Max(p => p.GoodsSort);
                return (orgNo + 1);
            }
        }

        /// <summary>
        /// 获取产品编号最大值
        /// </summary>
        /// <returns></returns>
        public long GetMaxGoodsId(long labID)
        {
            try
            {
                DaoNHBGetMaxByHqlAction<ReaGoods> action = new DaoNHBGetMaxByHqlAction<ReaGoods>("select max(reagoods.Id) as GoodsSort  from ReaGoods reagoods where 1=1 and reagoods.LabID=" + labID);
                long curId = this.HibernateTemplate.Execute(action);
                if (curId > 0 && curId.ToString().Length < 19)
                {
                    curId = curId + 1;
                }
                return curId;
            }
            catch (Exception ee)
            {
                string strHQL = "select reagoods from ReaGoods reagoods where 1=1 and reagoods.LabID=" + labID;
                IList<ReaGoods> tempList = this.HibernateTemplate.Find<ReaGoods>(strHQL);
                long id = -1;

                if (tempList.Count > 0)
                {
                    tempList.Max(p => p.Id);
                    id = (id + 1);
                }
                return id;
            }
        }

        /// <summary>
        /// 获取最大的时间戳，接口同步货品使用
        /// </summary>
        /// <returns></returns>
        public string GetMaxTS()
        {
            try
            {
                DaoNHBGetTotalByHqlAction<ReaGoods> action = new DaoNHBGetTotalByHqlAction<ReaGoods>("select max(reagoods.TS) as TS from ReaGoods reagoods where 1=1 and TS is not null");
                object obj = this.HibernateTemplate.Execute(action);
                if (obj != null && obj.ToString().Trim() != "")
                {
                    return DateTime.Parse(obj.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            catch (Exception ee)
            {

            }
            return "0";
        }
<<<<<<< .mine

||||||| .r2673
=======

        /// <summary>
        /// 货品表结合库存表，查询二级分类并返回
        /// </summary>
        /// <returns></returns>
        public IList<ReaGoodsClassVO> SearchGoodsClassTypeJoinQtyDtlList(string where, string sort, int page, int limit)
        {
            IList<ReaGoodsClassVO> entityList = new List<ReaGoodsClassVO>();
            string hql = " select DISTINCT reagoods.GoodsClassType from ReaBmsQtyDtl reabmsqtydtl,ReaGoods reagoods where reabmsqtydtl.GoodsID=reagoods.Id and reagoods.GoodsClassType is not null and reagoods.GoodsClassType!='' ";

            if (!string.IsNullOrEmpty(where))
            {
                hql = hql + " and " + where;
            }

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

            GetDataRowRoleHQLString("reagoods");
            hql += " and " + DataRowRoleHQLString;

            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;

            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<string>, string> action = new DaoNHBSearchByHqlAction<List<string>, string>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<string>>(action);

            if (list != null)
            {
                foreach (var goodsClass in list)
                {
                    ReaGoodsClassVO vo = new ReaGoodsClassVO();
                    vo.Id = goodsClass;
                    vo.CName = goodsClass;
                    entityList.Add(vo);
                }
            }
            return entityList;
        }

        /// <summary>
        /// 货品表结合库存表，查询二级分类并返回
        /// </summary>
        /// <returns></returns>
        public EntityList<ReaGoodsClassVO> SearchGoodsClassTypeJoinQtyDtl(string where, string sort, int page, int limit)
        {
            EntityList<ReaGoodsClassVO> entityList = new EntityList<ReaGoodsClassVO>();

            entityList.list = SearchGoodsClassTypeJoinQtyDtlList(where, sort, page, limit);
            string hql = " select count(DISTINCT reagoods.GoodsClassType) from ReaBmsQtyDtl reabmsqtydtl,ReaGoods reagoods where reabmsqtydtl.GoodsID=reagoods.Id and reagoods.GoodsClassType is not null and reagoods.GoodsClassType!='' ";
            
            if (!string.IsNullOrEmpty(where))
            {
                hql = hql + " and " + where;
            }
            GetDataRowRoleHQLString("reagoods");
            hql += " and " + DataRowRoleHQLString;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBGetCountByHqlAction<string> actionCount = new DaoNHBGetCountByHqlAction<string>(hql);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }

>>>>>>> .r2783
    }
}