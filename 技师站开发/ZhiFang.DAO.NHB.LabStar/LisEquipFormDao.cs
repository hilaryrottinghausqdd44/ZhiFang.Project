using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LisEquipFormDao : BaseDaoNHB<LisEquipForm, long>, IDLisEquipFormDao
    {

        public IList<LisEquipForm> QueryLisEquipFormDao(string strHqlWhere, IList<string> listEntityName = null)
        {
            string strFetchHQL = "";
            if (listEntityName != null && listEntityName.Count > 0)
            {
                foreach (string entityName in listEntityName)
                {
                    strFetchHQL += " left join fetch " + entityName;
                }
            }
            else
            {
                strFetchHQL = " left join fetch lisequipform.LisTestForm listestform " +
                              " left join fetch lisequipform.LBEquip lbequip ";
            }
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<LisEquipForm>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = "select lisequipform from LisEquipForm lisequipform " + strFetchHQL + " where 1=1 " + strHqlWhere;
            IList<LisEquipForm> listLisEquipForm = this.Session.CreateQuery(strHQL).List<LisEquipForm>();
            return listLisEquipForm;
        }

        public EntityList<LisEquipForm> QueryLisEquipFormDao(string strHqlWhere, string order, int start, int count, IList<string> listEntityName = null)
        {
            string strFetchHQL = "";
            string strNotFetchHQL = "";
            if (listEntityName != null && listEntityName.Count > 0)
            {
                foreach (string entityName in listEntityName)
                {
                    strFetchHQL += " left join fetch " + entityName;
                    strNotFetchHQL += " left join " + entityName;
                }
            }
            else
            {
                strFetchHQL = " left join fetch lisequipform.LisTestForm listestform " +
                              " left join fetch lisequipform.LBEquip lbequip ";
                strNotFetchHQL = " left join lisequipform.LisTestForm listestform " +
                                 " left join lisequipform.LBEquip lbequip ";
            }
                string strHQL = "select lisequipform from LisEquipForm lisequipform " + strFetchHQL;
            string strHQLCount = "select count(*) from LisEquipForm lisequipform " + strNotFetchHQL;
            EntityList<LisEquipForm> entityList = this.GetListByHQL(strHqlWhere, order, start, count, strHQL, strHQLCount);
            return entityList;
        }

    }
}