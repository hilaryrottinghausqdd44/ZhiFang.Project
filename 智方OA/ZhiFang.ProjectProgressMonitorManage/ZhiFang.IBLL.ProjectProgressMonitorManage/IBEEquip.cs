using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
	/// <summary>
	///
	/// </summary>
	public  interface IBEEquip : IBGenericManager<EEquip>
	{

        string QueryEquipNameByID(long equipID, bool isAddID);

        DataSet GetEquipInfoByID(string idList, string where, string sort, string xmlPath);
    }
}