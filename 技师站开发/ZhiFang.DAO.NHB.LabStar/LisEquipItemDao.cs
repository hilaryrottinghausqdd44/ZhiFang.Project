using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LisEquipItemDao : BaseDaoNHB<LisEquipItem, long>, IDLisEquipItemDao
    {

        public IList<LisEquipItem> QueryLisEquipItemDao(string strHqlWhere, IList<string> listEntityName)
        {
            string strFetchHQL = "";
            foreach (string entityName in listEntityName)
            {
                strFetchHQL += " left join fetch " + entityName;
            }
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<LisEquipItem>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = "select lisequipitem from LisEquipItem lisequipitem " + strFetchHQL + " where 1=1 " + strHqlWhere;
            IList<LisEquipItem> listLisTestItem = this.Session.CreateQuery(strHQL).List<LisEquipItem>();
            return listLisTestItem;
        }

        public EntityList<LisEquipItem> QueryLisEquipItemDao(string strHqlWhere, string order, int start, int count, IList<string> listEntityName)
        {
            string strFetchHQL = "";
            string strNotFetchHQL = "";
            foreach (string entityName in listEntityName)
            {
                strFetchHQL += " left join fetch " + entityName;
                strNotFetchHQL += " left join " + entityName;
            }
            string strHQL = "select lisequipitem from LisEquipItem lisequipitem " + strFetchHQL;
            string strHQLCount = "select count(*) from LisEquipItem lisequipitem " + strNotFetchHQL;
            EntityList<LisEquipItem> entityList = this.GetListByHQL(strHqlWhere, order, start, count, strHQL, strHQLCount);
            return entityList;
        }

        public EntityList<LisEquipItem> QueryEquipItemResultDao(string strWhere, long equipID, long itemID, int page, int limit)
        {
            //EntityList<LisEquipItem> entityList = new EntityList<LisEquipItem>();  
            string strHQL = " select lisequipitem from LisEquipItem lisequipitem ";
            // " left join lisequipitem.LBItem lbitem " +
            //" left join lisequipitem.LisEquipForm.LBEquip lbequip ";
            //" left join lisequipitem.LisEquipForm lisequipform";
            string strHQLWhere = " lisequipitem.LisEquipForm.LBEquip.Id=" + equipID +
                                 " and lisequipitem.LBItem.Id=" + itemID + strWhere;
            EntityList<LisEquipItem> entityList = this.GetListByHQL(strHQLWhere, page, limit, strHQL);
            return entityList;
        }
    }
}