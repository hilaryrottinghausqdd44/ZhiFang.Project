

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
    public interface IBPGMProgram : IBGenericManager<PGMProgram>
    {
        BaseResultDataValue AddPGMProgramByFormData(PGMProgram entity, HttpPostedFile file, int fFileOperationType, string ffileOperationMemo, string programTyp);
        BaseResultBool UpdatePGMProgramByFieldAndFormData(string[] tempArray, PGMProgram entity, HttpPostedFile file, int fFileOperationType, string ffileOperationMemo);
        
        BaseResultDataValue UploadAttachment(HttpPostedFile file, string programType);
        bool UpdateCountsById(long id);
        bool UpdateStatusByStrIds(string strIds, string status);
        bool UpdateIsUseByStrIds(string strIds, bool isUse);
        EntityList<PGMProgram> SearchPGMProgramByBDictTreeId(string where, bool isSearchChildNode, int page, int limit, string sort, string maxLevelStr);
        /// <summary>
        /// 获取行数据权限的查询条件
        /// </summary>
        /// <returns></returns>
        string GetDataRowRoleHQL(bool isSearchChildNode);
    }
}