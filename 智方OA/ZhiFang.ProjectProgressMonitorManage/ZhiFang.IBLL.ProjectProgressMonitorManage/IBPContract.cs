using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBPContract : IBGenericManager<PContract>
    {
        BaseResultDataValue BPContractAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction);
        BaseResultBool UpdatePContractStatus(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long empID, string empName);
        BaseResultDataValue ExcelToPdfFile(long id, bool isPreview, string templetName, ref string fileName);
        /// <summary>
        /// 新增或修改合同时获取用户所属省份信息
        /// </summary>
        void GetEntityProvinceInfo();
        BaseResultDataValue SearchListTotalByHQL(string where, string fields);
    }
}