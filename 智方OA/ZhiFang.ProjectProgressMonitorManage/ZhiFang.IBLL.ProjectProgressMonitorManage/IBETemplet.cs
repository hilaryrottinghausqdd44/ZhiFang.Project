using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBETemplet : IBGenericManager<ETemplet>
    {
        BaseResultDataValue AddETemplet(ETemplet entity);

        BaseResultBool EditETemplet(ETemplet entity, string fields);

        BaseResultDataValue EditETempletFillStruct(long id);

        void GetTempletStruct(ETemplet entity, string fileName);

        string QueryEquipNameByID(long templetID, bool isAddID);

        string QueryEquipTempletNameByID(long templetID, bool isAddID);

        EntityList<ETemplet> SearchETempletByHRDeptID(string where, int page, int limit, string sort);

        EntityList<ETemplet> SearchTempletByEmp(int relationType, long empID, string where, string resWhere, int page, int limit, string sort);

        IList<string> GetNeedContentByExpression(string strSource, string pattern);
    }
}