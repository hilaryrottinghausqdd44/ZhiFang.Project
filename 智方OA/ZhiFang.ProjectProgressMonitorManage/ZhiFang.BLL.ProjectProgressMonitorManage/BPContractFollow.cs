using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.OA;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;
using System.IO;
using ZhiFang.ProjectProgressMonitorManage.Common;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPContractFollow : BaseBLL<PContractFollow>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPContractFollow
    {
        public BaseResultDataValue SearchListTotalByHQL(string where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            object result = DBDao.GetTotalByHQL(where, fields);
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(result);
            return brdv;
        }
    }
}
