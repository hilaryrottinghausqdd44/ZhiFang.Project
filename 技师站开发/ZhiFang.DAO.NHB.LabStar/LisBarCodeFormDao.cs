using System;
using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LisBarCodeFormDao : BaseDaoNHB<LisBarCodeForm, long>, IDLisBarCodeFormDao
    {
        public List<LisBarCodeForm> QueryLisBarCodeFormByHqlDao(string strHqlWhere, string Order, int start, int count,string strHQL)
        {

            List<LisBarCodeForm> List = new List<LisBarCodeForm>();
            if (!String.IsNullOrEmpty(strHQL))
            {
                strHQL = strHQL + " where 1=1 ";
                if (strHqlWhere != null && strHqlWhere.Length > 0)
                {
                    strHQL += " and " + strHqlWhere;
                }
                strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<LisBarCodeForm>();
                strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
                if (Order != null && Order.Trim().Length > 0)
                {
                    strHQL += " order by " + Order;
                }
                int? start1 = null;
                int? count1 = null;
                if (start > 0)
                {
                    start1 = start;
                }
                if (count > 0)
                {
                    count1 = count;
                }
                DaoNHBSearchByHqlAction<List<LisBarCodeForm>, LisBarCodeForm> action = new DaoNHBSearchByHqlAction<List<LisBarCodeForm>, LisBarCodeForm>(strHQL, start1, count1);

                List = this.HibernateTemplate.Execute<List<LisBarCodeForm>>(action);
                
            }
            return List;
        }
        public System.Collections.IList GetBarCodeFormList(string fields, string where)
        {
            string sql = "select distinct " + fields + " from Lis_BarCodeForm ";
            string hqlwhere = " 1=1 ";
            if (!string.IsNullOrEmpty(where))
            {
                hqlwhere += " and " + where;
            }
            sql += " where " + hqlwhere;
            var lisOrderFormVos = this.Session.CreateSQLQuery(sql).List();
            if (lisOrderFormVos.Count > 0)
            {
                return lisOrderFormVos;
            }
            else
            {
                return null;
            }
        }
    }
}