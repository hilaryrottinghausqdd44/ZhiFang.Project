using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BReaDeptGoods : BaseBLL<ReaDeptGoods>, ZhiFang.Digitlab.IBLL.ReagentSys.IBReaDeptGoods
    {
        IBLL.RBAC.IBHRDept IBHRDept { get; set; }

        public EntityList<ReaDeptGoods> SearchReaGoodsByHRDeptID(long deptID, string where, string sort, int page, int limit)
        {
            EntityList<ReaDeptGoods> entityList = new EntityList<ReaDeptGoods>();
            string strWhere = "";
            string strDeptID = IBHRDept.SearchHRDeptIdListByHRDeptId(deptID);

            if (!string.IsNullOrWhiteSpace(where))
                strWhere = where;
            if (!string.IsNullOrWhiteSpace(strDeptID))
            {
                if (!string.IsNullOrWhiteSpace(strWhere)) strWhere += " and";
                strWhere = " readeptgoods.HRDept.Id in (" + strDeptID + ")";
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
            strWhere = "readeptgoods.Visible=1 and readeptgoods.HRDept.Id in (" + strDeptID + ")";
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
    }
}