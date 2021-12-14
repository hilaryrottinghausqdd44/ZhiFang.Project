using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBPCustomerServiceAttachment : IBGenericManager<PCustomerServiceAttachment>
    {
        BaseResultDataValue AddPCustomerServiceAttachment(string fkObjectId, string fkObjectName, HttpPostedFile file, string parentPath, string tempPath, string fileExt, PCustomerServiceAttachment entity);
        PCustomerServiceAttachment GetAttachmentFilePathAndFileName(long id, ref string filePath);
    }
}