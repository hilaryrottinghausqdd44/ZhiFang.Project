using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.ProjectProgressMonitorManage.Common;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
	/// <summary>
	///
	/// </summary>
	public  class BEEquip : BaseBLL<EEquip>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBEEquip
	{

        public string QueryEquipNameByID(long equipID, bool isAddID)
        {
            string equipName = "";
            EEquip equip = this.Get(equipID);
            if (equip != null)
            {
                equipName = equip.CName;
                if (isAddID && (!string.IsNullOrEmpty(equipName)))
                    equipName += "_" + equipID.ToString();
                if (string.IsNullOrEmpty(equipName))
                    equipName = equipID.ToString();
            }
            else
                equipName = equipID.ToString();
            return equipName;
        }

        public DataSet GetEquipInfoByID(string idList, string where, string sort, string xmlPath)
        {
            EntityList<EEquip> entityList = null;
            if (string.IsNullOrEmpty(idList) && string.IsNullOrEmpty(where))
                return null;
            else
            {
                if (!string.IsNullOrEmpty(idList))
                    entityList = this.SearchListByHQL(" eequip.Id in (" + idList + ")", sort, 0, 0);
                else if (!string.IsNullOrEmpty(where))
                    entityList = this.SearchListByHQL(where, sort, 0, 0);
                if (entityList != null && entityList.count > 0)
                    return ExcelDataCommon.GetListObjectToDataSet<EEquip>(entityList.list, xmlPath);
                else
                    return null;
            }
        }

    }
}