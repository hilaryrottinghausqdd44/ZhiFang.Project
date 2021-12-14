using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class BParaItemDao : BaseDaoNHB<BParaItem, long>, IDBParaItemDao
    {
        public IList<object> QueryParaSystemTypeInfoDao(string systemTypeCode, string paraTypeCode)
        {
            string strHqlWhere = " and bpara.TypeCode=\'" + paraTypeCode + "\'" + " and bpara.SystemCode=\'" + systemTypeCode + "\'";
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<BParaItem>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = "select cast(bparaitem.ObjectID as string), bparaitem.ObjectName " +
                " from BParaItem bparaitem " +
                " left join bparaitem.BPara bpara where 1=1 " + strHqlWhere +
                " group by bparaitem.ObjectID, bparaitem.ObjectName";
            IList<object> list = this.Session.CreateQuery(strHQL).List<object>();
            return list;
        }

        public IList<BParaItem> QuerySystemParaItemDao(string where)
        {
            string strHqlWhere = "";
            if (where != null && where.Trim().Length > 0)
                strHqlWhere = " and " + where;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<BParaItem>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = " select bparaitem from BParaItem bparaitem " +
                            " left join fetch bparaitem.BPara bpara where 1=1 " + strHqlWhere;
            IList<BParaItem> list = this.Session.CreateQuery(strHQL).List<BParaItem>();
            return list;
        }

        /// <summary>
        /// 根据参数编码查询查找对应的参数信息
        /// </summary>
        /// <param name="paraNo">参数信息</param>
        /// <returns></returns>
        public BPara QueryParaValueByParaNo(string paraNo, string objectID)
        {
            string strHqlWhere = " and bpara.ParaNo=\'" + paraNo + "\'";
            if (objectID != null && objectID.Trim().Length > 0)
                strHqlWhere += " and bparaitem.ObjectID=" + objectID;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<BParaItem>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = " select bparaitem from BParaItem bparaitem " +
                            " left join fetch bparaitem.BPara bpara where 1=1 " + strHqlWhere;
            IList<BParaItem> list = this.Session.CreateQuery(strHQL).List<BParaItem>();
            if (list != null && list.Count > 0)
            {
                list[0].BPara.ParaValue = list[0].ParaValue;
                return list[0].BPara;
            }
            else
                return null;

        }
        public IList<object> QueryParaSystemTypeInfoByParaTypeCodesDao(string systemTypeCode, string paraTypeCodes)
        {
            string strHqlWhere = " and bpara.TypeCode in (\'" + paraTypeCodes + "\')" + " and bpara.SystemCode=\'" + systemTypeCode + "\'";
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<BParaItem>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = "select cast(bparaitem.ObjectID as string), bparaitem.ObjectName " +
                " from BParaItem bparaitem " +
                " left join bparaitem.BPara bpara where 1=1 " + strHqlWhere +
                " group by bparaitem.ObjectID, bparaitem.ObjectName";
            IList<object> list = this.Session.CreateQuery(strHQL).List<object>();
            return list;
        }
    }
}