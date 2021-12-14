using System;
using System.Collections.Generic;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLisEquipItem : BaseBLL<LisEquipItem>, ZhiFang.IBLL.LabStar.IBLisEquipItem
    {
        public IList<LisEquipItem> QueryLisEquipItem(string strHqlWhere, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            return (DBDao as IDLisEquipItemDao).QueryLisEquipItemDao(strHqlWhere, listEntityName);
        }

        public EntityList<LisEquipItem> QueryLisEquipItem(string strHqlWhere, string order, int start, int count, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            listEntityName = LisCommonMethod.GetJoinEntityNameByOrderFields(listEntityName, ref order);
            return (DBDao as IDLisEquipItemDao).QueryLisEquipItemDao(strHqlWhere, order, start, count, listEntityName);
        }

        public bool QueryIsExistEquipItemResult(long sectionID, long itemID)
        {
            string strWhere = " and lisequipitem.ETestDate>=\'" + DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + "\'";
            EntityList<LisEquipItem> tempList = (this.DBDao as IDLisEquipItemDao).QueryEquipItemResultDao(strWhere, sectionID, itemID, 1, 1);
            return (tempList != null && tempList.count > 0);
        }


    }
}